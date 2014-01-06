using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Building
    {
        public Building()
        {
            this.Albums = new List<Album>();
            this.BuildingBuildingTypes = new List<BuildingBuildingType>();
            this.BuildingExtras = new List<BuildingExtra>();
            this.BuildingKitchenTypes = new List<BuildingKitchenType>();
            this.BuildingMusicTypes = new List<BuildingMusicType>();
            this.BuildingOccasionTypes = new List<BuildingOccasionType>();
            this.BuildingPaymentTypes = new List<BuildingPaymentType>();
            this.Comments = new List<Comment>();
            this.Events = new List<Event>();
            this.MenuCategories = new List<MenuCategory>();
            this.Offers = new List<Offer>();
            this.Ratings = new List<Rating>();
            this.Visitors = new List<Visitor>();
            this.Users = new List<User>();
        }

        public int BuildingId { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string WebSite { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> MunicipalityId { get; set; }
        public Nullable<int> SettlementId { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Info { get; set; }
        public string BuildingPhone { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> SeatsInside { get; set; }
        public Nullable<int> SeatsOutside { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<BuildingBuildingType> BuildingBuildingTypes { get; set; }
        public virtual ICollection<BuildingExtra> BuildingExtras { get; set; }
        public virtual ICollection<BuildingKitchenType> BuildingKitchenTypes { get; set; }
        public virtual ICollection<BuildingMusicType> BuildingMusicTypes { get; set; }
        public virtual ICollection<BuildingOccasionType> BuildingOccasionTypes { get; set; }
        public virtual ICollection<BuildingPaymentType> BuildingPaymentTypes { get; set; }
        public virtual District District { get; set; }
        public virtual Municipality Municipality { get; set; }
        public virtual Settlement Settlement { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<MenuCategory> MenuCategories { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

    public class BuildingMap : EntityTypeConfiguration<Building>
    {
        public BuildingMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Slogan)
                .HasMaxLength(50);

            this.Property(t => t.WebSite)
                .HasMaxLength(100);

            this.Property(t => t.ContactName)
                .HasMaxLength(100);

            this.Property(t => t.ContactPhone)
                .HasMaxLength(100);

            this.Property(t => t.BuildingPhone)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Buildings");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Slogan).HasColumnName("Slogan");
            this.Property(t => t.WebSite).HasColumnName("WebSite");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.SettlementId).HasColumnName("SettlementId");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            this.Property(t => t.Info).HasColumnName("Info");
            this.Property(t => t.BuildingPhone).HasColumnName("BuildingPhone");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.SeatsInside).HasColumnName("SeatsInside");
            this.Property(t => t.SeatsOutside).HasColumnName("SeatsOutside");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasMany(t => t.Users)
                .WithMany(t => t.Buildings)
                .Map(m =>
                {
                    m.ToTable("BuildingUsers");
                    m.MapLeftKey("BuildingId");
                    m.MapRightKey("UserId");
                });

            this.HasOptional(t => t.District)
                .WithMany(t => t.Buildings)
                .HasForeignKey(d => d.DistrictId);
            this.HasOptional(t => t.Municipality)
                .WithMany(t => t.Buildings)
                .HasForeignKey(d => d.MunicipalityId);
            this.HasOptional(t => t.Settlement)
                .WithMany(t => t.Buildings)
                .HasForeignKey(d => d.SettlementId);

        }
    }
}
