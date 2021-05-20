using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TimeAttendence.Models;

namespace TimeAttendence.Controllers
{
    public class LoginAccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        //Login to information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!IsCorrectSystemAdmin(model))
            {
                ModelState.AddModelError("","Invalid username and password");
                return View("Login");
            }

            Session["username"] = model.Username;
            return RedirectToAction("Index", "Employee/viewEmployeeDay");

        }

        //check username and password
        public bool IsCorrectSystemAdmin(LoginModel model)
        {
            return model.Username == System.Configuration.ConfigurationManager.AppSettings["AdminUser"]
                && model.Password == System.Configuration.ConfigurationManager.AppSettings["AdminPassword"] ? true : false;
        }

        //Logout from information
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

    }
}