using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Offer
    {
        public int OfferId { get; set; }
        public int BuildingId { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
    }

    public class OfferMap : EntityTypeConfiguration<Offer>
    {
        public OfferMap()
        {
            // Primary Key
            this.HasKey(t => t.OfferId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Offers");
            this.Property(t => t.OfferId).HasColumnName("OfferId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Info).HasColumnName("Info");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.Offers)
                .HasForeignKey(d => d.BuildingId);

        }
    }
}
