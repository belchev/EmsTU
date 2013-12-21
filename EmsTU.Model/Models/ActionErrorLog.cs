using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class ActionErrorLog
    {
        public int ActionErrorLogId { get; set; }
        public Nullable<System.Guid> RequestId { get; set; }
        public string RequestInfo { get; set; }
        public string ErrorInfo { get; set; }
        public Nullable<System.DateTime> ActionErrorDate { get; set; }
        public byte[] Version { get; set; }
    }

    public class ActionErrorLogMap : EntityTypeConfiguration<ActionErrorLog>
    {
        public ActionErrorLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ActionErrorLogId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ActionErrorLogs");
            this.Property(t => t.ActionErrorLogId).HasColumnName("ActionErrorLogId");
            this.Property(t => t.RequestId).HasColumnName("RequestId");
            this.Property(t => t.RequestInfo).HasColumnName("RequestInfo");
            this.Property(t => t.ErrorInfo).HasColumnName("ErrorInfo");
            this.Property(t => t.ActionErrorDate).HasColumnName("ActionErrorDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
