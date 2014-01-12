using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class MusicType
    {
        public MusicType()
        {
            this.Buildings = new List<Building>();
        }

        public int MusicTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
    }

    public class MusicTypeMap : EntityTypeConfiguration<MusicType>
    {
        public MusicTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.MusicTypeId);

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
            this.ToTable("MusicTypes");
            this.Property(t => t.MusicTypeId).HasColumnName("MusicTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
