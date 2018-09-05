using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using APICarrier.Content.Quartz.NET;
using log4net.Config;
using APICarrier.Content.Log;

namespace APICarrier
{
    public class Global : HttpApplication
    {
        
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //日志
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(Server.MapPath("~/App_Data/log4net.Config"));
            log4net.Config.XmlConfigurator.Configure(fileinfo);
            //任务调度
            App.Init();
        }

        protected void Application_End()
        {
            App.End();
        }
    }
}