using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class NewBuildingDO
    {
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
