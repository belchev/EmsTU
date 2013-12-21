using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int BuildingId { get; set; }
        public Nullable<int> Rating1 { get; set; }
        public string Ip { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
    }

    public class RatingMap : EntityTypeConfiguration<Rating>
    {
        public RatingMap()
        {
            // Primary Key
            this.HasKey(t => t.RatingId);

            // Properties
            this.Property(t => t.Ip)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Ratings");
            this.Property(t => t.RatingId).HasColumnName("RatingId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.Rating1).HasColumnName("Rating");
            this.Property(t => t.Ip).HasColumnName("Ip");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.Ratings)
                .HasForeignKey(d => d.BuildingId);

        }
    }
}
