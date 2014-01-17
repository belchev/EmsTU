using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class AlbumDO
    {
        public AlbumDO()
        {
            this.AlbumPhotos = new List<AlbumPhotoDO>();
        }

        public AlbumDO(Album a) 
            : this()
        {
            if (a != null)
            {
                if (a.AlbumPhotos != null && a.AlbumPhotos.Any())
                {
                    this.AlbumPhotos.AddRange(a.AlbumPhotos.Select(e => new AlbumPhotoDO(e)));
                }

                this.AlbumId = a.AlbumId;
                this.BuildingId = a.BuildingId;
                this.Name = a.Name;
                this.IsActive = a.IsActive;
                this.IsDeleted = false;

                this.Version = a.Version;
            }
        }

        public List<AlbumPhotoDO> AlbumPhotos { get; set; }
        public int AlbumId { get; set; }
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        //
        public bool IsNew { get; set; }
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }
    }
}
