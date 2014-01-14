using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class NomDO
    {
        public NomDO(Nom nom)
        {
            if (nom != null)
            {
                this.NomId = nom.NomId;
                this.Name = nom.Name;
                this.Alias = nom.Alias;
                this.IsActive = nom.IsActive;
            }
        }

        public int NomId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }
    }
}
