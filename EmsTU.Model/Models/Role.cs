using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Role
    {
        public Role()
        {
            this.BuildingUserRoles = new List<BuildingUserRole>();
            this.RolesPermissions = new List<RolesPermission>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<BuildingUserRole> BuildingUserRoles { get; set; }
        public virtual ICollection<RolesPermission> RolesPermissions { get; set; }
    }

    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.RoleId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Roles");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
