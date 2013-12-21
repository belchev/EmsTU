using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingBuildingType
    {
        public int BuildingBuildingTypeId { get; set; }
        public int BuildingId { get; set; }
        public int BuildingTypeId { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual BuildingType BuildingType { get; set; }
    }

    public class BuildingBuildingTypeMap : EntityTypeConfiguration<BuildingBuildingType>
    {
        public BuildingBuildingTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingBuildingTypeId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingBuildingTypes");
            this.Property(t => t.BuildingBuildingTypeId).HasColumnName("BuildingBuildingTypeId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.BuildingTypeId).HasColumnName("BuildingTypeId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingBuildingTypes)
                .HasForeignKey(d => d.BuildingId);
            this.HasRequired(t => t.BuildingType)
                .WithMany(t => t.BuildingBuildingTypes)
                .HasForeignKey(d => d.BuildingTypeId);

        }
    }
}
