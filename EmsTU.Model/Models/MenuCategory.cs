using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class MenuCategory
    {
        public MenuCategory()
        {
            this.Menus = new List<Menu>();
        }

        public int MenuCategoryId { get; set; }
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }

    public class MenuCategoryMap : EntityTypeConfiguration<MenuCategory>
    {
        public MenuCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.MenuCategoryId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("MenuCategories");
            this.Property(t => t.MenuCategoryId).HasColumnName("MenuCategoryId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.MenuCategories)
                .HasForeignKey(d => d.BuildingId);

        }
    }
}
