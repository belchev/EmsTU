using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmsTU.Web.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Index()
        {
            HttpPostedFileBase postedFile = this.Request.Files[0];
            string postedFileName = Path.GetFileName(postedFile.FileName);
            string postedFileExtension = Path.GetExtension(Request.Files[0].FileName);

            byte[] content = new byte[postedFile.InputStream.Length];
            using (MemoryStream ms = new MemoryStream(content))
            {
                postedFile.InputStream.CopyTo(ms);
            }

            var returnValue = "storage\\images\\aaa.jpg";

            return Content(JsonConvert.SerializeObject(returnValue, System.Web.Http.GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings));
        }

    }
}
