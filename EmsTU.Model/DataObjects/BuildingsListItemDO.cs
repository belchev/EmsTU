﻿using EmsTU.Model.Models;
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
            this.BuildingTypes = new List<NomDO>();
        }

        public BuildingsListItemDO(Building b)
            : this()
        {
            if (b != null)
            {
                if (b.Noms.Any(e => e.NomType.Alias == "BuildingTypes"))
                {
                    this.BuildingTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "BuildingTypes").Select(e => new NomDO(e)));
                }
                if (b.Settlement != null)
                {
                    this.DisplayAddress = b.Settlement.Name + ", ";
                }
                this.DisplayAddress = this.DisplayAddress + b.Address;

                this.BuildingId = b.BuildingId;
                this.Name = b.Name;
                this.Address = b.Address;
                this.WorkingTime = b.WorkingTime;
                this.Slogan = b.Slogan;
                this.ImagePath = b.ImagePath;
                this.WebSite = b.WebSite;

                if (b.IsDeleted)
                {
                    this.Status = "Изтрит";
                }
                else if (b.IsActive)
                {
                    this.Status = "Активен";
                }
                else
                {
                    this.Status = "Неактивен";
                }

                this.IsActive = b.IsActive;
                this.IsSoftDeleted = b.IsDeleted;
                this.ModifyDate = b.ModifyDate;

                if (b.ModifyUser != null)
                {
                    this.ModifyUsername = b.ModifyUser.Username;
                }

                this.Version = b.Version;
            }
        }

        public List<NomDO> BuildingTypes { get; set; }

        public int BuildingId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DisplayAddress { get; set; }
        public string WorkingTime { get; set; }

        public string Slogan { get; set; }
        public string ImagePath { get; set; }
        public string WebSite { get; set; }
        public string Status { get; set; }
        public Nullable<DateTime> ModifyDate { get; set; }
        public string ModifyUsername { get; set; }

        public bool IsActive { get; set; }
        public bool IsSoftDeleted { get; set; }
        public bool IsSelected { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }
    }
}
