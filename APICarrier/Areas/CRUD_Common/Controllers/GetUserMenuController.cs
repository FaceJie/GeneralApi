using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace APICarrier.Areas.CRUD_Common.Controllers
{
    public class GetUserMenuController : Controller
    {
        // GET: CRUD_Common/GetUserMenu
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取登录用户可访问的所有模块，及模块的操作菜单
        /// </summary>
        public JsonResult GetModulesTree()
        {
            string jsonfile = "D://ProjectDocument//GeneralApi//APICarrier//Content//json//menu.json";
            StreamReader sr = new StreamReader(jsonfile, Encoding.Default);
            String line;
            string jsonobj = "";
            while ((line = sr.ReadLine()) != null)
            {
                jsonobj = jsonobj + line.ToString();
            }
            return Json(jsonobj, JsonRequestBehavior.AllowGet);
        }
    }
}