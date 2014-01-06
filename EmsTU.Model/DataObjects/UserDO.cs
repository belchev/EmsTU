using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class UserDO
    {
        public UserDO()
        {
            this.Buildings = new List<BuildingsListDO>();
        }

        public UserDO(User u)
            : this()
        {
            if (u != null)
            {
                this.UserId = u.UserId;
                this.RoleId = u.RoleId;
                this.Username = u.Username;
                this.Fullname = u.Fullname;
                this.Password = u.Password;
                this.Notes = u.Notes;
                this.Email = u.Email;
                this.IsActive = u.IsActive;

                this.Version = u.Version;

                if (u.Buildings != null && u.Buildings.Any())
                {
                    this.Buildings.AddRange(u.Buildings.Select(e => new BuildingsListDO(e)).ToList());
                }
            }
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public List<BuildingsListDO> Buildings { get; set; }

        public byte[] Version { get; set; }
    }
}
