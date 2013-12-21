using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingType
    {
        public BuildingType()
        {
            this.BuildingBuildingTypes = new List<BuildingBuildingType>();
        }

        public int BuildingTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<BuildingBuildingType> BuildingBuildingTypes { get; set; }
    }

    public class BuildingTypeMap : EntityTypeConfiguration<BuildingType>
    {
        public BuildingTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingTypeId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.Alias)
                .HasMaxLength(100);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingTypes");
            this.Property(t => t.BuildingTypeId).HasColumnName("BuildingTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
