using EmsTU.Web.Common.LogFilters;
using System.Web;
using System.Web.Mvc;

namespace EmsTU.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ActionLogFilter());
            filters.Add(new ActionErrorLogFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}