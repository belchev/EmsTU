using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public string Comment1 { get; set; }
        public System.DateTime Date { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
    }

    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.CommentId);

            // Properties
            this.Property(t => t.Comment1)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Comments");
            this.Property(t => t.CommentId).HasColumnName("CommentId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Comment1).HasColumnName("Comment");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.BuildingId);

        }
    }
}
