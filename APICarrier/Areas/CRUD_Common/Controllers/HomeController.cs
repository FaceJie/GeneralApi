using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APICarrier.Areas.CRUD_Common.Controllers
{
    public class HomeController : Controller
    {
        // GET: CRUD_Common/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }



        public ActionResult Git()
        {
            return View();
        }
    }
}