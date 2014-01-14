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
                if (b.Noms.Any(e => e.NomType.Alias == "KitchenTypes"))
                {
                    this.BuildingTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "KitchenTypes").Select(e => new NomDO(e)));
                }
                if (b.Noms.Any(e => e.NomType.Alias == "Extras"))
                {
                    this.BuildingTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "Extras").Select(e => new NomDO(e)));
                }
                if (b.Noms.Any(e => e.NomType.Alias == "PaymentTypes"))
                {
                    this.BuildingTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "PaymentTypes").Select(e => new NomDO(e)));
                }
                if (b.Noms.Any(e => e.NomType.Alias == "OccasionTypes"))
                {
                    this.BuildingTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "OccasionTypes").Select(e => new NomDO(e)));
                }
                if (b.Noms.Any(e => e.NomType.Alias == "BuildingTypes"))
                {
                    this.BuildingTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "BuildingTypes").Select(e => new NomDO(e)));
                }
                if (b.Noms.Any(e => e.NomType.Alias == "MusicTypes"))
                {
                    this.BuildingTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "MusicTypes").Select(e => new NomDO(e)));
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
