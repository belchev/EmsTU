using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class BuildingsListDO
    {
        public BuildingsListDO()
        {
        }

        public BuildingsListDO(Building b)
        {
            if (b != null)
            {
                this.BuildingId = b.BuildingId;
                this.Name = b.Name;
                this.Slogan = b.Slogan;
                this.WebSite = b.WebSite;

                this.Version = b.Version;
            }
        }

        public int BuildingId { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string WebSite { get; set; }

        public bool IsSelected { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }
    }
}
