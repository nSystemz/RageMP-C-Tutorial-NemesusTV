using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial.Inventory
{
    class Inventory : Script
    {
        public static List<ItemModel> itemList;

        public static ItemModel GetItemModelFromID(int itemId)
        {
            ItemModel item = null;
            foreach(ItemModel itemModel in itemList)
            {
                if(itemModel != null && itemModel.id == itemId)
                {
                    item = itemModel;
                    break;
                }
            }
            return item;
        }

        public static void RemoveItem(int id)
        {
            try
            {
                MySqlCommand command = Datenbank.Connection.CreateCommand();
                command.CommandText = "DELETE FROM items WHERE id = @id LIMIT 1";
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                NAPI.Util.ConsoleOutput($"[RemoveItem]: " + e.ToString());
            }
        }

        public static List<ItemModel> LoadAllItems()
        {
            List<ItemModel> itemList = new List<ItemModel>();
            MySqlCommand command = Datenbank.Connection.CreateCommand();
            command.CommandText = "SELECT * from items";

            using(MySqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    ItemModel item = new ItemModel();

                    float posX = reader.GetFloat("posX");
                    float posY = reader.GetFloat("posY");
                    float posZ = reader.GetFloat("posZ");

                    item.id = reader.GetInt32("id");
                    item.hash = reader.GetString("hash");
                    item.ownerEntity = reader.GetString("ownerEntity");
                    item.ownerIdentifier = reader.GetInt32("ownerIdentifier");
                    item.amount = reader.GetInt32("amount");
                    item.position = new Vector3(posX, posY, posZ);

                    if(item.ownerEntity == "Ground")
                    {
                        item.objectHandle = NAPI.Object.CreateObject(uint.Parse(item.hash), item.position, new Vector3(0.0f, 0.0f, 0.0f), 255);
                    }
                    itemList.Add(item);
                }
            }
            return itemList;
        }

        public static int AddNewItem(ItemModel item)
        {
            int itemId = 0;
            MySqlCommand command = Datenbank.Connection.CreateCommand();
            command.CommandText = "INSERT INTO items (hash, ownerEntity, ownerIdentifier, amount, posX, posY, posZ) VALUES (@hash, @ownerEntity, @ownerIdentifier, @amount, @posX, @posY, @posZ)";
            command.Parameters.AddWithValue("@hash", item.hash);
            command.Parameters.AddWithValue("@ownerEntity", item.ownerEntity);
            command.Parameters.AddWithValue("@ownerIdentifier", item.ownerIdentifier);
            command.Parameters.AddWithValue("@amount", item.amount);
            command.Parameters.AddWithValue("@posX", item.position.X);
            command.Parameters.AddWithValue("@posY", item.position.Y);
            command.Parameters.AddWithValue("@posZ", item.position.Z);

            command.ExecuteNonQuery();

            itemId = (int)command.LastInsertedId;

            return itemId;
        }

        public static void UpdateItem(ItemModel item)
        {
            MySqlCommand command = Datenbank.Connection.CreateCommand();
            command.CommandText = "UPDATE items SET ownerEntity = @OwnerEntity, ownerIdentifier = @ownerIdentifiert, amount = @amount, posX = @posX, posY = @posY, posZ = @posZ WHERE id = @id LIMIT 1";
            command.Parameters.AddWithValue("@ownerEntity", item.ownerEntity);
            command.Parameters.AddWithValue("@ownerIdentifier", item.ownerIdentifier);
            command.Parameters.AddWithValue("@amount", item.amount);
            command.Parameters.AddWithValue("@posX", item.position.X);
            command.Parameters.AddWithValue("@posY", item.position.Y);
            command.Parameters.AddWithValue("@posZ", item.position.Z);
            command.Parameters.AddWithValue("@id", item.id);

            command.ExecuteNonQuery();
        }

        private List<InventoryModel> GetPlayerInventory(Player player)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);

            int playerId = account.ID;

            foreach(ItemModel item in itemList.ToList())
            {
                if(item != null && item.ownerEntity == "Player" && item.ownerIdentifier == playerId)
                {
                    InventoryModel inventoryItem = new InventoryModel();
                    inventoryItem.id = item.id;
                    inventoryItem.hash = item.hash;
                    inventoryItem.descriptionitem = "";
                    inventoryItem.type = 0;
                    inventoryItem.amount = item.amount;

                    inventory.Add(inventoryItem);
                }
            }
            return inventory;
        }
    }
}
