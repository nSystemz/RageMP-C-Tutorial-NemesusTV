using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorial.Inventory
{
    class Item
    {
        public enum ItemTypes { Consumable };
        public string descriptionitem { get; set; }
        public string hash { get; set; }
        public int type { get; set; }

        public Item(string descriptionitem, string hash, int type)
        {
            this.descriptionitem = descriptionitem;
            this.hash = hash;
            this.type = type;
        }

        public static List<Item> ITEM_LIST = new List<Item>
        {
            new Item("Hot-Dog", "2565741261", (int)ItemTypes.Consumable),
        };

        public static string GetHashFromItem(string itemName)
        {
            string itemhash = null;
            foreach(Item item in ITEM_LIST)
            {
                if(item.descriptionitem == itemName)
                {
                    itemhash = item.hash;
                    break;
                }
            }
            return itemhash;
        }

        public static Item GetItemFromItem(string itemHash)
        {
            Item item = null;
            foreach(Item itemtemp in ITEM_LIST)
            {
                if(itemtemp.hash == itemHash)
                {
                    item = itemtemp;
                    break;
                }
            }
            return item;
        }

    }
}
