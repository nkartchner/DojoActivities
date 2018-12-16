using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CBelt.Models;
using Microsoft.AspNetCore.Http;
using CBelt.Data;
using Microsoft.EntityFrameworkCore;

namespace CBelt.Controllers
{
    public class ActivityController : Controller
    {
        private BeltContext dbContext;

        public ActivityController(BeltContext context)
        {
            dbContext = context;
        }



        public static bool HasOverlap(Event PotentialEvent, User user)
        {
            DateTime startTimeB = PotentialEvent.Date;
            DateTime endTimeB = PotentialEvent.Date + PotentialEvent.Duration;
            foreach (var e in user.Participating)
            {
                DateTime startTimeA = e.Event.Date;
                DateTime endTimeA = e.Event.Date + e.Event.Duration;
                //if(max(PotentialEvent.Date, user.Participating) < min(end1, end2))
                if (startTimeA < endTimeB && startTimeB < endTimeA)
                    return true;
            }
            return false;
        }





        // ################################################### Load Home page ################################################### //
        [HttpGet("Home")]
        public IActionResult Home(bool? error)
        {
            TempData.Clear();
            int? SessionUserId = HttpContext.Session.GetInt32("user_id");

            if (SessionUserId == null) return RedirectToAction("Login", "Login");

            User user = dbContext.users
                                .Include(u=>u.Participating)
                                .ThenInclude(p=>p.Event)
                                .FirstOrDefault(u => u.UserId == SessionUserId);

            List<Event> modelList = dbContext.events
                                .Include(e => e.Participants)
                                .ThenInclude(p => p.User).ToList();

            HomeView model = new HomeView()
            {
                events = modelList,
                LoggedIn = user
            };
            if (error != null)
            {
                TempData["JoinErrorMessage"] = "You cannot join this even as it overlaps a current event your already attending!";
            }
            return View(model);
        }



        // ################################################### Load page to Create a new Event ################################################### //
        [HttpGet("New")]
        public IActionResult New()
        {
            int? SessionUserId = HttpContext.Session.GetInt32("user_id");
            if (SessionUserId == null) return RedirectToAction("Login", "Login");
            return View();
        }


        // ################################################### Event Details ################################################### //
        [HttpGet("activity/{EventId}")]
        public IActionResult Activity(int EventId)
        {
            int? SessionUserId = HttpContext.Session.GetInt32("user_id");
            if (SessionUserId == null) return RedirectToAction("Login", "Login");

            HttpContext.Session.SetInt32("event_id",EventId);

            Event eventt = dbContext.events
                                .Include(e => e.Participants)
                                .ThenInclude(p => p.User)
                                .FirstOrDefault(ev => ev.EventId == EventId);
            
            User modelUser = dbContext.users
                                .FirstOrDefault(u => u.UserId == eventt.UserId);

            eventt.User = modelUser;

            EventView model = new EventView()
            {
                Event = eventt,
                User = dbContext.users
                                    .Include(u=>u.Participating)
                                    .ThenInclude(p=>p.Event)
                                    .FirstOrDefault(u => u.UserId == SessionUserId)
            };

            return View(model);
        }



        // ################################################### ProcEvent ################################################### //
        [HttpPost("ProcEvent")]
        public IActionResult ProcEvent(Event evt, int Duration, string durSpec)
        {
            int? SessionUserId = HttpContext.Session.GetInt32("user_id");
            if (SessionUserId == null) return RedirectToAction("Login", "Login");

            if (ModelState.IsValid)
            {
                if(DateTime.Compare(evt.Date, DateTime.Now) < 0)
                {
                    ModelState.AddModelError("Date", "Date must be held in the future!");
                    return View("New", evt);
                }
                
                switch (Int32.Parse(durSpec))
                {
                    case 0:
                        evt.Duration = TimeSpan.FromDays(Duration);
                        break;
                    case 1:
                        evt.Duration = TimeSpan.FromHours(Duration);
                        break;
                    case 2:
                        evt.Duration = TimeSpan.FromMinutes(Duration);
                        break;
                    default:
                        ModelState.AddModelError("Duration", "Invalid Duration entry. Please try again");
                        return View("New", evt);
                }
                User modelUser = dbContext.users
                                    .FirstOrDefault(u => u.UserId == SessionUserId);



                evt.Date += evt.Time;
                evt.UserId = (int)SessionUserId;
                evt.CreatedBy = modelUser.firstName;
                dbContext.events.Add(evt);
                dbContext.SaveChanges();

                return RedirectToAction("Activity", new { evt.EventId });
            }

            return View("New", evt);
        }


        // ################################################### Join Event ################################################### //
        [HttpGet("Join/{userId}/{EId}")]
        public IActionResult Join(int EId, int userId)
        {
            int? SessionUserId = HttpContext.Session.GetInt32("user_id");

            if (SessionUserId == null) return RedirectToAction("Login", "Login");

            Event EventToJoin = dbContext.events.FirstOrDefault(e => e.EventId == EId);
            User CurrentUser = dbContext.users
                                    .Include(u => u.Participating)
                                    .ThenInclude(p => p.Event)
                                    .FirstOrDefault(u => u.UserId == userId);

            foreach(var part in CurrentUser.Participating)
            {
                Console.WriteLine($"$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ Event Date: {part.Event.Title} $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                Console.WriteLine($"$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ Event Date: {part.Event.Date} $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                Console.WriteLine($"$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ Event Raw Duration: {part.Event.Duration} $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                Console.WriteLine($"$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ Event String Duration {part.Event.DurationToString()} $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                Console.WriteLine($"$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ Event Date-Duration: {part.Event.Date.Subtract(part.Event.Duration) } $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            }


            if (HasOverlap(EventToJoin, CurrentUser))
            {
                return RedirectToAction("Home", new { error = true });
            }


            Participant newP = new Participant()
            {
                EventId = EId,
                UserId = userId,
                Event = EventToJoin,
                User = CurrentUser
            };


            dbContext.participants.Add(newP);
            dbContext.SaveChanges();

            return RedirectToAction("Home");
        }



        // ################################################### Un-Join Event ################################################### //
        [HttpGet("Leave/{userId}/{EId}")]
        public IActionResult Leave(int EId, int userId)
        {
            int? SessionUserId = HttpContext.Session.GetInt32("user_id");

            if (SessionUserId == null) return RedirectToAction("Login", "Login");

            if (userId == SessionUserId)
            {
                Participant toDelete = dbContext.participants
                                    .Where(p => p.UserId == userId)
                                    .Include(p => p.Event)
                                    .FirstOrDefault(e => e.EventId == EId);

                dbContext.participants.Remove(toDelete);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Home");
        }




        // ################################################### Delete Event ################################################### //
        [HttpGet("Delete/{userId}/{EventId}")]
        public IActionResult Delete(int EventId, int userId)
        {
            int? SessionUserId = HttpContext.Session.GetInt32("user_id");
            if (SessionUserId == null || userId != SessionUserId) return RedirectToAction("Login", "Login");

            Event toDelete = dbContext.events
                                .Where(e => e.UserId == userId)
                                .Include(e => e.Participants)
                                .FirstOrDefault(e => e.EventId == EventId);

            dbContext.events.Remove(toDelete);
            dbContext.SaveChanges();

            return RedirectToAction("Home");
        }
    }
}
