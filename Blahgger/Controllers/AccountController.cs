using Blahgger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blahgger.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Password"))
            {
                // TODO get the user record from DB.

                // Check if the password matches.
                if (!(user.Email == "james@smashdev.com" && user.Password == "password"))
                {
                    ModelState.AddModelError("", "Login failed.");
                }
            }

            if (ModelState.IsValid)
            {
                // Login the User.
                FormsAuthentication.SetAuthCookie(user.Email, false);

                // Send to homepage.
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}