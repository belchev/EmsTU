using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class MenuDO
    {
        public MenuDO(Menu m) 
        {
            if (m != null)
            {
                this.MenuId = m.MenuId;
                this.MenuCategoryId = m.MenuCategoryId;
                this.Name = m.Name;
                this.Info = m.Info;
                this.Size = m.Size;
                this.Price = m.Price;
                this.ImagePath = m.ImagePath;
                this.IsActive = m.IsActive;
                this.HasImage = m.ImagePath == "app\\img\\nopicsmall.jpg" ? false : true;
                this.IsDeleted = false;

                this.Version = m.Version;
            }
        }

        public int MenuId { get; set; }
        public int MenuCategoryId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public Nullable<int> Size { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ImagePath { get; set; }

        public bool IsActive { get; set; }

        //
        public bool IsNew { get; set; }
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasImage { get; set; }

        public byte[] Version { get; set; }
    }
}
