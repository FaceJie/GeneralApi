using System.Web.Mvc;

namespace APICarrier.Areas.Quartz_NET
{
    public class Quartz_NETAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Quartz_NET";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Quartz_NET_default",
                "Quartz_NET/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[]{ "APICarrier.Areas.Quartz_NET.Controllers" }

            );
        }
    }
}