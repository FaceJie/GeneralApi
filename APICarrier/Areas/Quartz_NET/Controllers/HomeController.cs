using APICarrier.Content.Quartz.NET;
using Common.Quartz.NET;
using Common.Quartz.NET.QuartzViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APICarrier.Areas.Quartz_NET.Controllers
{
    public class HomeController : Controller
    {
        // GET: Quartz_NET/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Stat()
        {
            var intervalTask = IntervalTask.Current;

            var vm = new TaskStats
            {
                Counter = App.Counter.ToString(),
                TimerWokeup = formatDateTime(intervalTask.TimerWokeup),
                TimerStarted = formatDateTime(intervalTask.TimerStarted),
                TaskStarted = formatDateTime(intervalTask.TaskStarted),
                TaskEnded = formatDateTime(intervalTask.TaskEnded)
            };
            return Json(vm);
        }

        protected string formatDateTime(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return "";

            return dateTime.ToLocalTime().ToString("h:mm:sstt").ToLower();
        }
    }
}