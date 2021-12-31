using System.Web.Mvc;

namespace BlogTema.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Yorum_Update",
                "admin/Yorumlar/YorumDetay/{ID}",
                new { controller = "Yorumlar", action = "Yorumdetay", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_Blog_Update",
                "admin/Default/Update/{ID}",
                new { controller = "Default", action = "Update", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "admin_Login",
                "admin",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}