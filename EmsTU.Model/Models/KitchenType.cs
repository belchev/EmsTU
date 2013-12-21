using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class KitchenType
    {
        public KitchenType()
        {
            this.BuildingKitchenTypes = new List<BuildingKitchenType>();
        }

        public int KitchenTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<BuildingKitchenType> BuildingKitchenTypes { get; set; }
    }

    public class KitchenTypeMap : EntityTypeConfiguration<KitchenType>
    {
        public KitchenTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.KitchenTypeId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("KitchenTypes");
            this.Property(t => t.KitchenTypeId).HasColumnName("KitchenTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
