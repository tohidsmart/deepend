using System.Web.Mvc;
using System.Web.Routing;

namespace deepend.api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "api/v1/{controller}/{action}",
                defaults: new { controller = "cheques", action = "all" }
            );
        }
    }
}
