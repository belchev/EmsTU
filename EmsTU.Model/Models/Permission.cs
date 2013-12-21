using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Permission
    {
        public Permission()
        {
            this.RolesPermissions = new List<RolesPermission>();
        }

        public int PermissionsId { get; set; }
        public string Name { get; set; }
        public string ConstName { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<RolesPermission> RolesPermissions { get; set; }
    }

    public class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.PermissionsId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ConstName)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Permissions");
            this.Property(t => t.PermissionsId).HasColumnName("PermissionsId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ConstName).HasColumnName("ConstName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
