using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Collections.Concurrent;

namespace EmsTU.Web.Tools
{
    public static class HtmlHelpers
    {
        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        private static ConcurrentDictionary<string, string> cachedFileContents = new ConcurrentDictionary<string, string>();
        public static IHtmlString RawFile(this HtmlHelper helper, string virtualFilePath, bool useCache = true)
        {
            string contents;
#if DEBUG
            contents = GetFileContents(virtualFilePath);
#else
            contents = cachedFileContents.GetOrAdd(virtualFilePath, GetFileContents);
#endif
            return new HtmlString(contents);
        }

        private static string GetFileContents(string virtualFilePath)
        {
            return File.ReadAllText(HttpContext.Current.Server.MapPath(virtualFilePath));
        }
    }
}