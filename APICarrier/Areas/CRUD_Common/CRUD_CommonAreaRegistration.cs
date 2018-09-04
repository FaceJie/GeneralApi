using System.Web.Mvc;

namespace APICarrier.Areas.CRUD_Common
{
    public class CRUD_CommonAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CRUD_Common";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CRUD_Common_default",
                "CRUD_Common/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "APICarrier.Areas.CRUD_Common.Controllers" }
            );
        }
    }
}