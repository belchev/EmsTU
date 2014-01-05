using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Web.Helpers;

namespace EmsTU.Model.Models
{
    public partial class User
    {
        public User()
        {
            this.Buildings = new List<Building>();
            this.BuildingUsers = new List<BuildingUser>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Fullname { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<BuildingUser> BuildingUsers { get; set; }
        public virtual Role Role { get; set; }

        //client only
        public string Password { get; set; }

        public void SetPassword(string password)
        {
            if (password == null)
            {
                this.PasswordSalt = null;
                this.PasswordHash = null;
            }
            else
            {
                this.PasswordSalt = Crypto.GenerateSalt();
                this.PasswordHash = Crypto.HashPassword(password + this.PasswordSalt);
            }
        }

        public bool VerifyPassword(string password)
        {
            return Crypto.VerifyHashedPassword(this.PasswordHash, password + this.PasswordSalt);
        }   
    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.PasswordHash)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.PasswordSalt)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Fullname)
                .HasMaxLength(200);

            this.Property(t => t.Email)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.Fullname).HasColumnName("Fullname");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.RoleId);

            this.Ignore(u => u.Password);

        }
    }
}
