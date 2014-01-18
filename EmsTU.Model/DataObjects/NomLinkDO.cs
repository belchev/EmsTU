using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class NomLinkDO
    {
        public NomLinkDO()
        {
        }

        public NomLinkDO(int id, string url, string name)
            : this()
        {
            this.Id = id;
            this.Url = url;
            this.Name = name;
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
