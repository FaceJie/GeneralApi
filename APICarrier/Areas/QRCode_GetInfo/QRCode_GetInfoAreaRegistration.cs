using System.Web.Mvc;

namespace APICarrier.Areas.QRCode_GetInfo
{
    public class QRCode_GetInfoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QRCode_GetInfo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QRCode_GetInfo_default",
                "QRCode_GetInfo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "APICarrier.Areas.QRCode_GetInfo" }
            );
        }
    }
}