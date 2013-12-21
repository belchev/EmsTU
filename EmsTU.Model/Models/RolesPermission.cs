using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class RolesPermission
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public byte[] Version { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }

    public class RolesPermissionMap : EntityTypeConfiguration<RolesPermission>
    {
        public RolesPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.RolePermissionId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("RolesPermissions");
            this.Property(t => t.RolePermissionId).HasColumnName("RolePermissionId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.PermissionId).HasColumnName("PermissionId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Permission)
                .WithMany(t => t.RolesPermissions)
                .HasForeignKey(d => d.PermissionId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RolesPermissions)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
