using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Visitor
    {
        public int VisitorId { get; set; }
        public int BuildingId { get; set; }
        public string Ip { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
    }

    public class VisitorMap : EntityTypeConfiguration<Visitor>
    {
        public VisitorMap()
        {
            // Primary Key
            this.HasKey(t => t.VisitorId);

            // Properties
            this.Property(t => t.Ip)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Visitors");
            this.Property(t => t.VisitorId).HasColumnName("VisitorId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.Ip).HasColumnName("Ip");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.Visitors)
                .HasForeignKey(d => d.BuildingId);

        }
    }
}
