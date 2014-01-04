using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingUser
    {
        public int BuildingUserId { get; set; }
        public int BuildingId { get; set; }
        public int UserId { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual User User { get; set; }
    }

    public class BuildingUserMap : EntityTypeConfiguration<BuildingUser>
    {
        public BuildingUserMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingUserId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingUsers");
            this.Property(t => t.BuildingUserId).HasColumnName("BuildingUserId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingUsers)
                .HasForeignKey(d => d.BuildingId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.BuildingUsers)
                .HasForeignKey(d => d.UserId);

        }
    }
}
