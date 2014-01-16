using EmsTU.Common.Utils;
using EmsTU.Web.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmsTU.Model.Data.RepositoryExtensions;
using EmsTU.Common.Data;
using EmsTU.Model.Models;

namespace EmsTU.Web.Controllers
{
    public class FileController : Controller
    {
        protected IUnitOfWork unitOfWork;

        public FileController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index(string type)
        {
            HttpPostedFileBase postedFile = this.Request.Files[0];
            string postedFileName = Path.GetFileName(postedFile.FileName);
            string postedFileExtension = Path.GetExtension(Request.Files[0].FileName);
            byte[] content = new byte[postedFile.InputStream.Length];

            int x, y;
            string returnValue, fullPath, shortPath, imageName;

            switch (type)
            {
                case "ImageBuilding":
                    x = 117; 
                    y = 87;
                    fullPath = Statics.PathStorage + Statics.PathImageBuilding;
                    shortPath = Statics.PathImageBuilding;
                    break;
                case "ImageMenuItem":
                    x = 55;
                    y = 41;
                    fullPath = Statics.PathStorage + Statics.PathImageThumb;
                    shortPath = Statics.PathImageThumb;
                    break;
                default:
                    x = 100;
                    y = 100;
                    fullPath = Statics.PathStorage + Statics.PathImageThumb;
                    shortPath = Statics.PathImageThumb;
                    break;
            }

            using (MemoryStream ms = new MemoryStream(content))
            {
                postedFile.InputStream.CopyTo(ms);

                var image = Image.FromStream(ms);
                var newImage = ScaleImage(image, x, y);

                imageName = GenerateImageName();

                fullPath = fullPath + imageName + ".jpg";

                newImage.Save(fullPath, ImageFormat.Jpeg);
            }

            returnValue = shortPath + imageName + ".jpg";


            return Content(JsonConvert.SerializeObject(returnValue, System.Web.Http.GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings));
        }

        #region Protected

        protected static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        protected string GenerateImageName()
        {
            CodeGenerator codeGenerator = new CodeGenerator();
            codeGenerator.Minimum = 7;
            codeGenerator.Maximum = 7;
            codeGenerator.ConsecutiveCharacters = true;
            codeGenerator.RepeatCharacters = true;

            while (true)
            {
                string name = codeGenerator.Generate();

                if (!this.unitOfWork.Repo<Building>().CheckForExistingImageName(name))
                {
                    return name;
                }
            }
        }

        #endregion
    }
}
