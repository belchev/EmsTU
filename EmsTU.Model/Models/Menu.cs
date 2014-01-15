using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Menu
    {
        public int MenuId { get; set; }
        public int MenuCategoryId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public Nullable<int> Size { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual MenuCategory MenuCategory { get; set; }
    }

    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            // Primary Key
            this.HasKey(t => t.MenuId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Info)
                .HasMaxLength(200);

            this.Property(t => t.ImagePath)
                .HasMaxLength(100);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Menus");
            this.Property(t => t.MenuId).HasColumnName("MenuId");
            this.Property(t => t.MenuCategoryId).HasColumnName("MenuCategoryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Info).HasColumnName("Info");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.MenuCategory)
                .WithMany(t => t.Menus)
                .HasForeignKey(d => d.MenuCategoryId);

        }
    }
}
