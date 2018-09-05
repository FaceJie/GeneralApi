using APICarrier.Content.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APICarrier.Areas.CRUD_Common.Controllers
{
    public class BaseController : Controller
    {
        // GET: CRUD_Common/Base
        public ActionResult Index()
        {
            return View();
        }
    }
}