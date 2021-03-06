﻿using EmsTU.Model.Models;
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
            this.BuildingTypes = new List<int>();
            this.KitchenTypes = new List<int>();
            this.MusicTypes = new List<int>();
            this.OccasionTypes = new List<int>();
            this.PaymentTypes = new List<int>();
            this.Extras = new List<int>();

            this.MenuCategories = new List<MenuCategoryDO>();
            this.Albums = new List<AlbumDO>();
            this.Events = new List<EventDO>();
            this.Comments = new List<CommentDO>();
        }

        public BuildingDO(Building b, bool isAdmin)
            : this()
        {
            if (b != null)
            {
                if (b.Comments != null && b.Comments.Any())
                {
                    this.Comments.AddRange(b.Comments.OrderByDescending(e => e.Date).Select(e => new CommentDO(e)));
                    this.CommentsNum = b.Comments.Count;
                }
                if (b.Events != null && b.Events.Any())
                {
                    this.Events.AddRange(b.Events.Select(e => new EventDO(e)));
                    this.EventsNum = b.Events.Count;
                }

                if (b.Albums != null && b.Albums.Any())
                {
                    this.Albums.AddRange(b.Albums.Select(e => new AlbumDO(e)));
                    this.AlbumPhotosNum = b.Albums.Sum(e => e.AlbumPhotos.Count);
                }

                if (b.MenuCategories != null && b.MenuCategories.Any())
                {
                    this.MenuCategories.AddRange(b.MenuCategories.Select(e => new MenuCategoryDO(e)));
                    this.MenuItemsNum = b.MenuCategories.Sum(e => e.Menus.Count);
                }

                if (b.Noms.Any(e => e.NomType.Alias == "KitchenTypes"))
                {
                    this.KitchenTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "KitchenTypes").Select(e => e.NomId));
                }

                if (b.Noms.Any(e => e.NomType.Alias == "BuildingTypes"))
                {
                    this.BuildingTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "BuildingTypes").Select(e => e.NomId));
                }

                if (b.Noms.Any(e => e.NomType.Alias == "Extras"))
                {
                    this.Extras.AddRange(b.Noms.Where(e => e.NomType.Alias == "Extras").Select(e => e.NomId));
                }

                if (b.Noms.Any(e => e.NomType.Alias == "PaymentTypes"))
                {
                    this.PaymentTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "PaymentTypes").Select(e => e.NomId));
                }

                if (b.Noms.Any(e => e.NomType.Alias == "OccasionTypes"))
                {
                    this.OccasionTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "OccasionTypes").Select(e => e.NomId));
                }

                if (b.Noms.Any(e => e.NomType.Alias == "MusicTypes"))
                {
                    this.MusicTypes.AddRange(b.Noms.Where(e => e.NomType.Alias == "MusicTypes").Select(e => e.NomId));
                }

                this.HasImage = b.ImagePath == "app\\img\\nopic.jpg" ? false : true;
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
                this.ModifyDate = b.ModifyDate;
                this.IsActive = b.IsActive;
                this.IsDeleted = b.IsDeleted;
                this.IsAdmin = isAdmin;

                this.Version = b.Version;
            }
        }

        public List<CommentDO> Comments { get; set; }
        public List<EventDO> Events { get; set; }
        public List<AlbumDO> Albums { get; set; }
        public List<MenuCategoryDO> MenuCategories { get; set; }

        public int CommentsNum { get; set; }
        public int EventsNum { get; set; }
        public int AlbumPhotosNum { get; set; }
        public int MenuItemsNum { get; set; }

        public List<int> BuildingTypes { get; set; }
        public List<int> KitchenTypes { get; set; }
        public List<int> MusicTypes { get; set; }
        public List<int> OccasionTypes { get; set; }
        public List<int> PaymentTypes { get; set; }
        public List<int> Extras { get; set; }

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
        public Nullable<DateTime> ModifyDate { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasImage { get; set; }
        public string ErrorString { get; set; }

        public byte[] Version { get; set; }
    }
}
