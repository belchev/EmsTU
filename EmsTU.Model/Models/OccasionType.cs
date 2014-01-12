using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class OccasionType
    {
        public OccasionType()
        {
            this.Buildings = new List<Building>();
        }

        public int OccasionTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
    }

    public class OccasionTypeMap : EntityTypeConfiguration<OccasionType>
    {
        public OccasionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.OccasionTypeId);

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
            this.ToTable("OccasionTypes");
            this.Property(t => t.OccasionTypeId).HasColumnName("OccasionTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
