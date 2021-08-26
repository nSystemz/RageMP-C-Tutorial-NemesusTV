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
        public string ownerEntity { get; set; }
        public int ownerIdentifier { get; set; }
        public int amount { get; set; }
        public Vector3 position { get; set; }
        public GTANetworkAPI.Object objectHandle { get; set; }
        public TextLabel textHandle { get; set; }

        public ItemModel Copy()
        {
            ItemModel itemModel = new ItemModel();
            itemModel.id = id;
            itemModel.hash = hash;
            itemModel.ownerEntity = ownerEntity;
            itemModel.ownerIdentifier = ownerIdentifier;
            itemModel.amount = amount;
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
    }
}
