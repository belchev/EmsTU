﻿//using EmsTU.Model.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EmsTU.Model.DataObjects
//{
//    public class BuildingTypeDO
//    {
//        public BuildingTypeDO(BuildingType bt)
//        {
//            if (bt != null)
//            {
//                this.NomId = bt.BuildingTypeId;
//                this.Name = bt.Name;
//                this.Alias = bt.Alias;
//                this.IsActive = bt.IsActive;

//                this.Version = bt.Version;
//            }
//        }

//        public int NomId { get; set; }
//        public string Name { get; set; }
//        public string Alias { get; set; }
//        public bool IsActive { get; set; }

//        public bool IsSelected { get; set; }
//        public bool IsNew { get; set; }
//        public bool IsDeleted { get; set; }

//        public byte[] Version { get; set; }
//    }
//}
