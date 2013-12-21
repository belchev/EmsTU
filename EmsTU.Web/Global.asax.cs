using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using EmsTU.Common.Utils;
using Ninject.Web.Common;
using EmsTU.Web.App_Start;
using Ninject;

namespace EmsTU.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectDependencyResolver dependencyResolver = new NinjectDependencyResolver(new Bootstrapper().Kernel);

            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
            DependencyResolver.SetResolver(dependencyResolver);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
#if DEBUG
                Formatting = Newtonsoft.Json.Formatting.Indented,
#endif
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

        }


    }
}