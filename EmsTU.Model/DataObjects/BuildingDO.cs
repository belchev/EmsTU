using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class BuildingDO
    {
        public BuildingDO()
        {
            this.BuildingTypes = new List<BuildingTypeDO>();
        }

        public BuildingDO(Building b)
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
                this.ImagePath = b.ImagePath;
                this.Slogan = b.Slogan;
                this.WebSite = b.WebSite;
                this.DistrictId = b.DistrictId;
                this.MunicipalityId = b.MunicipalityId;
                this.SettlementId = b.SettlementId;
                this.Address = b.Address;
                this.ContactName = b.ContactName;
                this.ContactPhone = b.ContactPhone;
                this.Info = b.Info;
                this.WorkingTime = b.WorkingTime;
                this.Price = b.Price;
                this.SeatsInside = b.SeatsInside;
                this.SeatsOutside = b.SeatsOutside;

                this.Version = b.Version;
            }
        }

        public List<BuildingTypeDO> BuildingTypes { get; set; }

        public int BuildingId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Slogan { get; set; }
        public string WebSite { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> MunicipalityId { get; set; }
        public Nullable<int> SettlementId { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Info { get; set; }
        public string WorkingTime { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> SeatsInside { get; set; }
        public Nullable<int> SeatsOutside { get; set; }

        public byte[] Version { get; set; }
    }
}
