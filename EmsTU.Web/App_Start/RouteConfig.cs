using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmsTU.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //  name: null,
            //  url: "config",
            //  defaults: new { controller = "Home", action = "Config" });

            routes.MapRoute(
               name: null,
               url: "file",
               defaults: new { controller = "File", action = "Index" });

            routes.MapRoute(
               name: null,
               url: "login",
               defaults: new { controller = "Account", action = "Login" });

            routes.MapRoute(
               name: null,
               url: "logout",
               defaults: new { controller = "Account", action = "Logout" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "All",
               url: "{*all}",
               defaults: new { controller = "Home", action = "Index" });
        }
    }
}