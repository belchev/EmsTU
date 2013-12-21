using EmsTU.Web.Common.LogFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EmsTU.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            RegisterGlobalFilters(config);

            RegisterRoutes(config);
        }

        private static void RegisterGlobalFilters(HttpConfiguration config)
        {
            config.Filters.Add(new ActionLogFilter());
            config.Filters.Add(new ActionErrorLogFilter());
        }

        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
