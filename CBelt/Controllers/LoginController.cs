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
    public class LoginController : Controller
    {

        private BeltContext dbContext;


        public LoginController(BeltContext context)
        {
            dbContext = context;
        }



        [HttpGet]
        [Route("")]
        public IActionResult Login()
        {

            return View("Login");
        }


        [HttpPost]
        [Route("proc_login")]
        public IActionResult ProcLogin(Login userSubmission)
        {
            //Console.WriteLine("************************************************ Begin ************************************************");
            if (ModelState.IsValid)
            {
                //Console.WriteLine("************************************************ Look if user exists Begin ************************************************");

                if (dbContext.users.Any(u => u.email == userSubmission.Email))
                {
                    //Console.WriteLine("************************************************ User exists. Pulling user ************************************************");
                    User user = dbContext.users.FirstOrDefault(u => u.email == userSubmission.Email);
                    PasswordHasher<Login> hasher = new PasswordHasher<Login>();
                    PasswordVerificationResult result = hasher.VerifyHashedPassword(userSubmission, user.password, userSubmission.Password);

                    if (result == 0)
                    {
                        //Console.WriteLine("************************************************ Result is 0! Password IS NOT CORRECT ************************************************");
                        ModelState.AddModelError("Password", "The email or password you entered is incorrect");
                        return View("_LoginPartial", userSubmission);
                    }
                    //Console.WriteLine("************************************************ Result is NOT 0! Password IS............ CORRECT ************************************************");
                    HttpContext.Session.SetInt32("user_id", user.UserId);
                    //Console.WriteLine("************************************************ Redirecting ************************************************");
                    return RedirectToAction("Home", "Activity");

                }
                else
                {
                    //Console.WriteLine("************************************************ Email is not correct ************************************************");
                    ModelState.AddModelError("Email", "The email or password you entered is incorrect");
                    return View("_LoginPartial", userSubmission);
                }
            }
            else
            {
                //Console.WriteLine("************************************************ MODELSTATE IS NOT VALID ************************************************");

                return View("_LoginPartial", userSubmission);
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}