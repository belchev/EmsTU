using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class CommentDO
    {
        public CommentDO(Comment com) 
        {
            if (com != null)
            {
                this.CommentId = com.CommentId;
                this.BuildingId = com.BuildingId;
                this.Date = com.Date;
                this.Name = com.Name;
                this.Comment = com.Comment1;
                this.IsDeleted = false;

                this.Version = com.Version;
            }
        }

        public int CommentId { get; set; }
        public int BuildingId { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }
    }
}
