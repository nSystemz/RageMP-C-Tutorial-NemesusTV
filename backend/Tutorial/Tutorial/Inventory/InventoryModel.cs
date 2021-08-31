using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorial.Inventory
{
    class InventoryModel
    {
        public int id { get; set; }
        public string hash { get; set; }
        public string oldhash { get; set; }
        public string descriptionitem { get; set; }
        public int type { get; set; }
        public int amount { get; set; }
        public int oldamount { get; set; }
    }
}
