using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Album
    {
        public Album()
        {
            this.AlbumPhotos = new List<AlbumPhoto>();
        }

        public int AlbumId { get; set; }
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<AlbumPhoto> AlbumPhotos { get; set; }
        public virtual Building Building { get; set; }
    }

    public class AlbumMap : EntityTypeConfiguration<Album>
    {
        public AlbumMap()
        {
            // Primary Key
            this.HasKey(t => t.AlbumId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(150);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Albums");
            this.Property(t => t.AlbumId).HasColumnName("AlbumId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.Albums)
                .HasForeignKey(d => d.BuildingId);

        }
    }
}
