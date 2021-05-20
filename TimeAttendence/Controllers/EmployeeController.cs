using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeAttendence.Models;

namespace TimeAttendence.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private TimeAttendence2Entities db = new TimeAttendence2Entities();

        //Today
        public ActionResult viewEmployeeDay()
        {
            DateTime now = DateTime.Now;
            var user = db.TestCheckedTimes.Where(x => x.date_at.Value.Day == now.Day 
                                        && x.date_at.Value.Month == now.Month 
                                        && x.date_at.Value.Year == now.Year).ToList();
            
            var userInAscOrder = user.OrderBy(u => u.checkin_at);
            return View("viewEmployeeDay", userInAscOrder);
        }

        //SearchingDays
        [HttpPost]
        public ActionResult viewEmployeeDay(DateTime? days)
        {
            //SELECT * FROM [dbo].[TestCheckedTime] WHERE date_at LIKE @day 
            return View("viewEmployeeDay", db.GetFunctionDay(days));
        }


        //Current Month
        public ActionResult viewEmployeeMonth()
        {
            DateTime now = DateTime.Now;
            var user = db.TestCheckedTimes.Where(x => x.date_at.Value.Month == now.Month 
                                        && x.date_at.Value.Year == now.Year).ToList();

            var userInAscOrder = user.OrderBy(u => u.date_at);
            return View("viewEmployeeMonth", userInAscOrder);
        }

        //SearchingMonths
        [HttpPost]
        public ActionResult viewEmployeeMonth(int? month, int? year)
        {
            //SELECT * FROM [dbo].[TestCheckedTime] WHERE MONTH(date_at) LIKE @month AND YEAR(date_at) LIKE @year
            return View("viewEmployeeMonth", db.GetFunctionMonth(month, year));
        }

    }
}