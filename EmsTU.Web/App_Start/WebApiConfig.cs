using EmsTU.Web.Common.LogFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

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
                name: null,
                routeTemplate: "api/noms/buildings",
                defaults: new { controller = "Nom", action = "GetBuildings", name = "", limit = "", offset = "" }, //email = "",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            #region noms

            config.Routes.MapHttpRoute(
               name: null,
               routeTemplate: "api/noms/districts",
               defaults: new { controller = "Nom", action = "GetDistricts" },
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/noms/municipalities",
                defaults: new { controller = "Nom", action = "GetMunicipalities", districtId = "" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/noms/settlements",
                defaults: new { controller = "Nom", action = "GetSettlements", municipalityId = "" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/noms/yesNoOptions",
                defaults: new { controller = "Nom", action = "GetYesNoOptions" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/noms/roles",
                defaults: new { controller = "Nom", action = "GetRoles" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            #endregion

            #region Users

            //Users
            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/users",
                defaults: new { controller = "user", action = "GetUsers", username = "", fullname = "", exact = false, showactive = "" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );
            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/users",
                defaults: new { controller = "user", action = "PostUser" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );
            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/users/{userId}",
                defaults: new { controller = "user", action = "GetUser" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );
            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/users/{userId}/",
                defaults: new { controller = "user", action = "PutUser" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) }
            );
            config.Routes.MapHttpRoute(
              name: null,
              routeTemplate: "api/user/changepassword",
              defaults: new { controller = "user", action = "PostNewUserPassword" },
              constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );

            #endregion

        }
    }
}
