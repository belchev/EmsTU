using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Nom
    {
        public Nom()
        {
            this.Buildings = new List<Building>();
        }

        public int NomId { get; set; }
        public int NomTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual NomType NomType { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
    }

    public class NomMap : EntityTypeConfiguration<Nom>
    {
        public NomMap()
        {
            // Primary Key
            this.HasKey(t => t.NomId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.Alias)
                .HasMaxLength(100);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Noms");
            this.Property(t => t.NomId).HasColumnName("NomId");
            this.Property(t => t.NomTypeId).HasColumnName("NomTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.NomType)
                .WithMany(t => t.Noms)
                .HasForeignKey(d => d.NomTypeId);

        }
    }
}
