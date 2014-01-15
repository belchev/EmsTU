using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingRequest
    {
        public int BuildingRequestId { get; set; }
        public string BuildingName { get; set; }
        public string ContactName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public bool HasRegisteredUser { get; set; }
        public bool HasRegisteredBuilding { get; set; }
        public byte[] Version { get; set; }
    }

    public class BuildingRequestMap : EntityTypeConfiguration<BuildingRequest>
    {
        public BuildingRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingRequestId);

            // Properties
            this.Property(t => t.BuildingName)
                .HasMaxLength(100);

            this.Property(t => t.ContactName)
                .HasMaxLength(100);

            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.WebSite)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingRequests");
            this.Property(t => t.BuildingRequestId).HasColumnName("BuildingRequestId");
            this.Property(t => t.BuildingName).HasColumnName("BuildingName");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.WebSite).HasColumnName("WebSite");
            this.Property(t => t.HasRegisteredUser).HasColumnName("HasRegisteredUser");
            this.Property(t => t.HasRegisteredBuilding).HasColumnName("HasRegisteredBuilding");
            this.Property(t => t.Version).HasColumnName("Version");

        }
    }
}
