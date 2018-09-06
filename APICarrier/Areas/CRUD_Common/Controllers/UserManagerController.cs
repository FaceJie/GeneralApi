using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APICarrier.Areas.CRUD_Common.Controllers
{
    public class UserManagerController : Controller
    {
        // GET: CRUD_Common/UserManager
        public JsonResult Load()
        {
            var user = new
            {
                Id = "49df1602 - f5f3 - 4d52 - afb7 - 3802da619558",
                Account = "admin",
                Sex = 1,
                Status = 1,
                Type = 0,
                CreateTime = "2017 - 12 - 11 16:18:53",
                CreateUser = "",
                Organizations = "研发小组,研发部",
                OrganizationIds = "08f41bf6 - 4388 - 4b1e - bd3e - 2ff538b44b1b,990cb229 - cc18 - 41f3 - 8e2b - 13f0f0110798",

            };
            var list = Enumerable.Repeat(user, 1).ToList();

            var result = new
            {
                code = 0,
                msg = "null",
                count = 6,
                data = list
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}