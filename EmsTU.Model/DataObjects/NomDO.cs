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
                if (nom.NomType != null)
                {
                    this.NomTypeId = nom.NomType.NomTypeId;
                    this.NomTypeAlias = nom.NomType.Alias;
                }

                this.NomId = nom.NomId;
                this.Name = nom.Name;
                this.Alias = nom.Alias;
                this.IsActive = nom.IsActive;
                this.Version = nom.Version;
            }
        }

        public int NomId { get; set; }
        public int NomTypeId { get; set; }
        public string NomTypeAlias { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }
    }
}
