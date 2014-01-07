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
            this.BuildingTypes = new List<BuildingType>();
        }

        public BuildingsListItemDO(Building b)
        {
            User u = new User();

            if (b != null)
            {
                if (b.BuildingBuildingTypes != null && b.BuildingBuildingTypes.Any())
                {
                    this.BuildingTypes.AddRange(b.BuildingBuildingTypes.Select(e => new BuildingType()));
                }

                this.BuildingId = b.BuildingId;
                this.Name = b.Name;
                this.Slogan = b.Slogan;
                this.WebSite = b.WebSite;

                this.Version = b.Version;
            }
        }

        public List<BuildingType> BuildingTypes { get; set; }

        public int BuildingId { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string WebSite { get; set; }

        public byte[] Version { get; set; }
    }
}
