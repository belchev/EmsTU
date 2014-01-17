using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class EventDO
    {
        public EventDO(Event ev) 
        {
            if (ev != null)
            {
                this.EventId = ev.EventId;
                this.BuildingId = ev.BuildingId;
                this.Date = ev.Date;
                this.Name = ev.Name;
                this.ImagePath = ev.ImagePath;
                this.ImageThumbPath = ev.ImageThumbPath;
                this.Info = ev.Info;
                this.IsActive = ev.IsActive;
                this.HasImage = ev.ImagePath == "app\\img\\nopic.jpg" ? false : true;
                this.IsDeleted = false;

                this.Version = ev.Version;
            }
        }

        public int EventId { get; set; }
        public int BuildingId { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string ImagePath { get; set; }
        public string ImageThumbPath { get; set; }
        public bool IsActive { get; set; }

        //
        public bool IsNew { get; set; }
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasImage { get; set; }

        public byte[] Version { get; set; }
    }
}
