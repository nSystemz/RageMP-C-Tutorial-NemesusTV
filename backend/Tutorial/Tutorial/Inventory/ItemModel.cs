using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorial.Inventory
{
    class ItemModel : Script
    {
        public int id { get; set; }
        public string hash { get; set; }
        public string oldhash { get; set; }
        public string ownerEntity { get; set; }
        public int ownerIdentifier { get; set; }
        public int amount { get; set; }
        public int oldamount { get; set; }
        public Vector3 position { get; set; }
        public GTANetworkAPI.Object objectHandle { get; set; }
        public TextLabel textHandle { get; set; }

        public ItemModel Copy()
        {
            ItemModel itemModel = new ItemModel();
            itemModel.id = id;
            itemModel.hash = hash;
            itemModel.oldhash = hash;
            itemModel.ownerEntity = ownerEntity;
            itemModel.ownerIdentifier = ownerIdentifier;
            itemModel.amount = amount;
            itemModel.oldamount = amount;
            itemModel.position = position;
            itemModel.objectHandle = objectHandle;
            itemModel.textHandle = textHandle;
            return itemModel;
        }

        public static ItemModel GetItemModelFromId(int itemId)
        {
            ItemModel item = null;
            foreach (ItemModel itemModel in Inventory.itemList)
            {
                if (itemModel != null && itemModel.id == itemId)
                {
                    item = itemModel;
                    break;
                }
            }
            return item;
        }

        public static ItemModel GetClosestItemWithHash(Player player, string hash)
        {
            ItemModel itemModel = null;
            foreach(ItemModel item in Inventory.itemList)
            {
                if(item.ownerEntity == "Ground" && item.hash == hash && player.Position.DistanceTo(item.position) < 2.0f)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }
    }
}
