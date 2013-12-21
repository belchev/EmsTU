using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class BuildingPaymentType
    {
        public int BuildingPaymentTypeId { get; set; }
        public int BuildingId { get; set; }
        public int PaymentTypeId { get; set; }
        public byte[] Version { get; set; }
        public virtual Building Building { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }

    public class BuildingPaymentTypeMap : EntityTypeConfiguration<BuildingPaymentType>
    {
        public BuildingPaymentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BuildingPaymentTypeId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BuildingPaymentTypes");
            this.Property(t => t.BuildingPaymentTypeId).HasColumnName("BuildingPaymentTypeId");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.PaymentTypeId).HasColumnName("PaymentTypeId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingPaymentTypes)
                .HasForeignKey(d => d.BuildingId);
            this.HasRequired(t => t.PaymentType)
                .WithMany(t => t.BuildingPaymentTypes)
                .HasForeignKey(d => d.PaymentTypeId);

        }
    }
}
