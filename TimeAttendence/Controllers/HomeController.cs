using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using TimeAttendence.Models;
using TimeAttendence.Utilities;
using System.Web.Security;
using System.Security.Principal;
using System.Web.Configuration;


namespace TimeAttendence.Controllers
{
    
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Token = TokenHelper.GetLastToken();
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public JsonResult CheckIn(string fullname, string email, string token)
        {
            if (TokenHelper.Tokens.Contains(token))
            {
                using (TimeAttendence2Entities db = new TimeAttendence2Entities())
                {
                    var checking = new TestCheckedTime
                    {
                        fullname = fullname,
                        email = email,
                        date_at = DateTime.Now,
                        checkin_at = DateTime.Now
                    };
                    //insert 
                    db.TestCheckedTimes.Add(checking);
                    db.SaveChanges();
                } 
                TokenHelper.GenToken();
                var context = GlobalHost.ConnectionManager.GetHubContext<AutoRefreshHub>();
                context.Clients.All.refreshToken(TokenHelper.GetLastToken());
                return GetSuccess(); //success
            }
            else
            {
                return GetError(); //error
            }
        }

        public JsonResult GetSuccess()
        {
            return Json(new { Success = true, Message = "SUCCESS" });
        }
        public JsonResult GetError()
        {
            return Json(new { Success = false, Message = "ERROR" });
        }

    }
}