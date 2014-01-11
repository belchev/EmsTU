using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class BuildingsListItemDO
    {
        public BuildingsListItemDO()
        {
            this.BuildingTypes = new List<BuildingTypeDO>();
        }

        public BuildingsListItemDO(Building b)
            : this()
        {
            if (b != null)
            {
                if (b.BuildingBuildingTypes != null && b.BuildingBuildingTypes.Any())
                {
                    this.BuildingTypes.AddRange(b.BuildingBuildingTypes.Select(e => new BuildingTypeDO(e.BuildingType)));
                }

                this.BuildingId = b.BuildingId;
                this.Name = b.Name;
                this.Address = b.Address;
                this.WorkingTime = b.WorkingTime;
                this.Slogan = b.Slogan;
                this.ImagePath = b.ImagePath;
                this.WebSite = b.WebSite;

                this.Version = b.Version;
            }
        }

        public List<BuildingTypeDO> BuildingTypes { get; set; }

        public int BuildingId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string WorkingTime { get; set; }

        public string Slogan { get; set; }
        public string ImagePath { get; set; }
        public string WebSite { get; set; }

        public bool IsSelected { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }
    }
}
