using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class AlbumPhoto
    {
        public int AlbumPhotoId { get; set; }
        public int AlbumId { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumb { get; set; }
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
            this.Property(t => t.Image)
                .IsRequired();

            this.Property(t => t.ImageThumb)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AlbumPhotos");
            this.Property(t => t.AlbumPhotoId).HasColumnName("AlbumPhotoId");
            this.Property(t => t.AlbumId).HasColumnName("AlbumId");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.ImageThumb).HasColumnName("ImageThumb");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Album)
                .WithMany(t => t.AlbumPhotos)
                .HasForeignKey(d => d.AlbumId);

        }
    }
}
