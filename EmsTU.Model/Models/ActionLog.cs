using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EmsTU.Model.Models
{
    public partial class ActionLog
    {
        public int ActionLogId { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public string IP { get; set; }
        public string Action { get; set; }
        public string ObjectId { get; set; }
        public string RawUrl { get; set; }
        public string Form { get; set; }
        public string BrowserInfo { get; set; }
        public string SessionId { get; set; }
        public string LoginName { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.Guid> RequestId { get; set; }
    }

    public class ActionLogMap : EntityTypeConfiguration<ActionLog>
    {
        public ActionLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ActionLogId);

            // Properties
            this.Property(t => t.IP)
                .HasMaxLength(50);

            this.Property(t => t.Action)
                .HasMaxLength(200);

            this.Property(t => t.ObjectId)
                .HasMaxLength(200);

            this.Property(t => t.RawUrl)
                .HasMaxLength(500);

            this.Property(t => t.Form)
                .HasMaxLength(500);

            this.Property(t => t.BrowserInfo)
                .HasMaxLength(200);

            this.Property(t => t.SessionId)
                .HasMaxLength(50);

            this.Property(t => t.LoginName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ActionLogs");
            this.Property(t => t.ActionLogId).HasColumnName("ActionLogId");
            this.Property(t => t.ActionDate).HasColumnName("ActionDate");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.ObjectId).HasColumnName("ObjectId");
            this.Property(t => t.RawUrl).HasColumnName("RawUrl");
            this.Property(t => t.Form).HasColumnName("Form");
            this.Property(t => t.BrowserInfo).HasColumnName("BrowserInfo");
            this.Property(t => t.SessionId).HasColumnName("SessionId");
            this.Property(t => t.LoginName).HasColumnName("LoginName");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.RequestId).HasColumnName("RequestId");
        }
    }
}
