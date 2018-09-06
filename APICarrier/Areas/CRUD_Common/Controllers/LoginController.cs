using Common;
using DataPojo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APICarrier.Areas.CRUD_Common.Controllers
{
    public class LoginController : Controller
    {
        // GET: CRUD_Common/Login
        public ActionResult Index()
        {
            return View();
        }

        public string Login(string username, string password)
        {
            MsgModel ret = new MsgModel();
            try
            {
                var result = true;
                if (result)
                {
                    ret.scu = true;
                    ret.msg = "登陆成功！";
                }
                else
                {
                    ret.scu = false;
                    ret.msg = "登录失败！";
                }
            }
            catch (Exception e)
            {
                ret.scu = false;
                ret.msg = e.Message.ToString();
            }
            return GlobalFuc.ModelToJson<MsgModel>(ret);
        }
    }
}