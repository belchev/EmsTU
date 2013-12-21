using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingKitchenType
    {
        public int BuildingKitchenTypeId { get; set; }
        public int BuildingId { get; set; }
        public int KitchenTypeId { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual KitchenType KitchenType { get; set; }
    }

    public class BuildingKitchenTypeMap : EntityTypeConfiguration<BuildingKitchenType>
    {
        public BuildingKitchenTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingKitchenTypeId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingKitchenTypes");
            this.Property(t => t.BuildingKitchenTypeId).HasColumnName("BuildingKitchenTypeId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.KitchenTypeId).HasColumnName("KitchenTypeId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingKitchenTypes)
                .HasForeignKey(d => d.BuildingId);
            this.HasRequired(t => t.KitchenType)
                .WithMany(t => t.BuildingKitchenTypes)
                .HasForeignKey(d => d.KitchenTypeId);

        }
    }
}
