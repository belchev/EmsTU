using EmsTU.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.DataObjects
{
    public class MenuCategoryDO
    {
        public MenuCategoryDO()
        {
            this.Menus = new List<MenuDO>();
        }

        public MenuCategoryDO(MenuCategory mc) 
            : this()
        {
            if (mc != null)
            {
                if (mc.Menus != null && mc.Menus.Any())
                {
                    this.Menus.AddRange(mc.Menus.Select(e => new MenuDO(e)));
                }


                this.MenuCategoryId = mc.MenuCategoryId;
                this.BuildingId = mc.BuildingId;
                this.Name = mc.Name;
                this.IsActive = mc.IsActive;

                this.Version = mc.Version;
            }
        }

        public List<MenuDO> Menus { get; set; }

        public int MenuCategoryId { get; set; }
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }
}
