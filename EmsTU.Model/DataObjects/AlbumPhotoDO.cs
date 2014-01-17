using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class AlbumPhotoDO
    {
        public AlbumPhotoDO(AlbumPhoto ap) 
        {
            if (ap != null)
            {
                this.AlbumPhotoId = ap.AlbumPhotoId;
                this.AlbumId = ap.AlbumId;
                this.ImagePath = ap.ImagePath;
                this.ImageThumbPath = ap.ImageThumbPath;
           
                this.HasImage = ap.ImagePath == "app\\img\\nopic.jpg" ? false : true;
                this.IsDeleted = false;

                this.Version = ap.Version;
            }
        }

        public int AlbumPhotoId { get; set; }
        public int AlbumId { get; set; }
        public string ImagePath { get; set; }
        public string ImageThumbPath { get; set; }

        //
        public bool IsNew { get; set; }
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasImage { get; set; }

        public byte[] Version { get; set; }
    }
}
