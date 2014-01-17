using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public int BuildingId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string ImagePath { get; set; }
        public string ImageThumbPath { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
    }

    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.EventId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ImagePath)
                .HasMaxLength(100);

            this.Property(t => t.ImageThumbPath)
                .HasMaxLength(100);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Events");
            this.Property(t => t.EventId).HasColumnName("EventId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Info).HasColumnName("Info");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.ImageThumbPath).HasColumnName("ImageThumbPath");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.BuildingId);

        }
    }
}
