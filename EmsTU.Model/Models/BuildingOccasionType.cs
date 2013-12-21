using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingOccasionType
    {
        public int BuildingOccasionTypeId { get; set; }
        public int BuildingId { get; set; }
        public int OccasionTypeId { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual OccasionType OccasionType { get; set; }
    }

    public class BuildingOccasionTypeMap : EntityTypeConfiguration<BuildingOccasionType>
    {
        public BuildingOccasionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingOccasionTypeId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingOccasionTypes");
            this.Property(t => t.BuildingOccasionTypeId).HasColumnName("BuildingOccasionTypeId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.OccasionTypeId).HasColumnName("OccasionTypeId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingOccasionTypes)
                .HasForeignKey(d => d.BuildingId);
            this.HasRequired(t => t.OccasionType)
                .WithMany(t => t.BuildingOccasionTypes)
                .HasForeignKey(d => d.OccasionTypeId);

        }
    }
}
