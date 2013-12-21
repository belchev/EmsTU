using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingUserRole
    {
        public int BuildingUserRoleId { get; set; }
        public int BuildingId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }

    public class BuildingUserRoleMap : EntityTypeConfiguration<BuildingUserRole>
    {
        public BuildingUserRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingUserRoleId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingUserRoles");
            this.Property(t => t.BuildingUserRoleId).HasColumnName("BuildingUserRoleId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingUserRoles)
                .HasForeignKey(d => d.BuildingId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.BuildingUserRoles)
                .HasForeignKey(d => d.RoleId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.BuildingUserRoles)
                .HasForeignKey(d => d.UserId);

        }
    }
}
