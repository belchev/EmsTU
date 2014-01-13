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
            this.BuildingTypes = new List<NomDO>();
            this.KitchenTypes = new List<NomDO>();
            this.MusicTypes = new List<NomDO>();
            this.OccasionTypes = new List<NomDO>();
            this.PaymentTypes = new List<NomDO>();
            this.Extras = new List<NomDO>();
        }

        public BuildingDO(Building b, bool isAdmin)
            : this()
        {
            if (b != null)
            {
                if (b.BuildingTypes != null && b.BuildingTypes.Any())
                {
                    this.BuildingTypes.AddRange(b.BuildingTypes.Select(e => new NomDO(e, "BuildingTypes")));
                }
                if (b.KitchenTypes != null && b.KitchenTypes.Any())
                {
                    this.KitchenTypes.AddRange(b.KitchenTypes.Select(e => new NomDO(e, "KitchenTypes")));
                }
                if (b.MusicTypes != null && b.MusicTypes.Any())
                {
                    this.MusicTypes.AddRange(b.MusicTypes.Select(e => new NomDO(e, "MusicTypes")));
                }
                if (b.OccasionTypes != null && b.OccasionTypes.Any())
                {
                    this.OccasionTypes.AddRange(b.OccasionTypes.Select(e => new NomDO(e, "OccasionTypes")));
                }
                if (b.PaymentTypes != null && b.PaymentTypes.Any())
                {
                    this.PaymentTypes.AddRange(b.PaymentTypes.Select(e => new NomDO(e, "PaymentTypes")));
                }
                if (b.Extras != null && b.Extras.Any())
                {
                    this.Extras.AddRange(b.Extras.Select(e => new NomDO(e, "Extras")));
                }

                this.HasLogo = b.ImagePath == "app\\img\\nopic.jpg" ? false : true;
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
                this.IsActive = b.IsActive;
                this.IsDeleted = b.IsDeleted;
                this.IsAdmin = isAdmin;

                this.Version = b.Version;
            }
        }

        public List<NomDO> BuildingTypes { get; set; }
        public List<NomDO> KitchenTypes { get; set; }
        public List<NomDO> MusicTypes { get; set; }
        public List<NomDO> OccasionTypes { get; set; }
        public List<NomDO> PaymentTypes { get; set; }
        public List<NomDO> Extras { get; set; }

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

        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasLogo { get; set; }
        public string ErrorString { get; set; }

        public byte[] Version { get; set; }
    }
}
