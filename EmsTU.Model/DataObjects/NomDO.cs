using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class NomDO
    {
        public NomDO(object obj, string type)
        {
            switch (type)
            {
                case "BuildingTypes":
                    var bt = (BuildingType)obj;
                    this.NomId = bt.BuildingTypeId;
                    this.Name = bt.Name;
                    this.Alias = bt.Alias;
                    this.IsActive = bt.IsActive;
                    break;
                case "KitchenTypes":
                    var kt = (KitchenType)obj;
                    this.NomId = kt.KitchenTypeId;
                    this.Name = kt.Name;
                    this.Alias = kt.Alias;
                    this.IsActive = kt.IsActive;
                    break;
                case "MusicTypes":
                    var mt = (MusicType)obj;
                    this.NomId = mt.MusicTypeId;
                    this.Name = mt.Name;
                    this.Alias = mt.Alias;
                    this.IsActive = mt.IsActive;
                    break;
                case "OccasionTypes":
                    var ot = (OccasionType)obj;
                    this.NomId = ot.OccasionTypeId;
                    this.Name = ot.Name;
                    this.Alias = ot.Alias;
                    this.IsActive = ot.IsActive;
                    break;
                case "PaymentTypes":
                    var pt = (PaymentType)obj;
                    this.NomId = pt.PaymentTypeId;
                    this.Name = pt.Name;
                    this.Alias = pt.Alias;
                    this.IsActive = pt.IsActive;
                    break;
                case "Extras":
                    var e = (Extra)obj;
                    this.NomId = e.ExtraId;
                    this.Name = e.Name;
                    this.Alias = e.Alias;
                    this.IsActive = e.IsActive;
                    break;
                default:
                    break;
            }
        }

        public int NomId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }
    }
}
