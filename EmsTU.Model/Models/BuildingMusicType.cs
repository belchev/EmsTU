using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingMusicType
    {
        public int BuildingMusicTypeId { get; set; }
        public int BuildingId { get; set; }
        public int MusicTypeId { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual MusicType MusicType { get; set; }
    }

    public class BuildingMusicTypeMap : EntityTypeConfiguration<BuildingMusicType>
    {
        public BuildingMusicTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingMusicTypeId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingMusicTypes");
            this.Property(t => t.BuildingMusicTypeId).HasColumnName("BuildingMusicTypeId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.MusicTypeId).HasColumnName("MusicTypeId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingMusicTypes)
                .HasForeignKey(d => d.BuildingId);
            this.HasRequired(t => t.MusicType)
                .WithMany(t => t.BuildingMusicTypes)
                .HasForeignKey(d => d.MusicTypeId);

        }
    }
}
