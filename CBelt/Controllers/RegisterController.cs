using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CBelt.Data;
using CBelt.Models;

namespace CBelt.Controllers
{
    public class RegisterController : Controller
    {
        private BeltContext dbContext;

        public RegisterController(BeltContext context)
        {
            dbContext = context;
        }


        [HttpPost]
        [Route("proc_reg")]
        public IActionResult ProcRegister(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.users.Any(u => u.email == newUser.email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("_RegistrationPartial", newUser);
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.password = Hasher.HashPassword(newUser, newUser.password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    User user = dbContext.users.FirstOrDefault(u => u.email == newUser.email);
                    HttpContext.Session.SetString("logged", "yes");
                    HttpContext.Session.SetInt32("user_id", user.UserId);
                    return RedirectToAction("Home", "Activity");
                }
            }
            else
            {
                return View("_RegistrationPartial", newUser);
            }
        }

    }
}