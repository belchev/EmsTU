using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingExtra
    {
        public int BuildingExtraId { get; set; }
        public int BuildingId { get; set; }
        public int ExtraId { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual Extra Extra { get; set; }
    }

    public class BuildingExtraMap : EntityTypeConfiguration<BuildingExtra>
    {
        public BuildingExtraMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingExtraId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingExtras");
            this.Property(t => t.BuildingExtraId).HasColumnName("BuildingExtraId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.ExtraId).HasColumnName("ExtraId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingExtras)
                .HasForeignKey(d => d.BuildingId);
            this.HasRequired(t => t.Extra)
                .WithMany(t => t.BuildingExtras)
                .HasForeignKey(d => d.ExtraId);

        }
    }
}
