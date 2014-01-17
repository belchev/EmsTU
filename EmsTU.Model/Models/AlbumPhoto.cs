using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class AlbumPhoto
    {
        public int AlbumPhotoId { get; set; }
        public int AlbumId { get; set; }
        public string ImagePath { get; set; }
        public string ImageThumbPath { get; set; }
        public byte[] Version { get; set; }
        public virtual Album Album { get; set; }
    }

    public class AlbumPhotoMap : EntityTypeConfiguration<AlbumPhoto>
    {
        public AlbumPhotoMap()
        {
            // Primary Key
            this.HasKey(t => t.AlbumPhotoId);

            // Properties
            this.Property(t => t.ImagePath)
                .HasMaxLength(100);

            this.Property(t => t.ImageThumbPath)
                .HasMaxLength(100);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AlbumPhotos");
            this.Property(t => t.AlbumPhotoId).HasColumnName("AlbumPhotoId");
            this.Property(t => t.AlbumId).HasColumnName("AlbumId");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.ImageThumbPath).HasColumnName("ImageThumbPath");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Album)
                .WithMany(t => t.AlbumPhotos)
                .HasForeignKey(d => d.AlbumId);

        }
    }
}
