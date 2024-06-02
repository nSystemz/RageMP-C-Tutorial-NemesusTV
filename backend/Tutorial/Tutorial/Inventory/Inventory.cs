using GTANetworkAPI;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tutorial.Inventory
{
    class Inventory : Script
    {
        public static List<ItemModel> itemList;

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

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
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

                    if (item.ownerEntity == "Ground")
                    {
                        item.objectHandle = NAPI.Object.CreateObject(uint.Parse(item.hash), item.position, new Vector3(0.0f, 0.0f, 0.0f), 255);
                        item.textHandle = NAPI.TextLabel.CreateTextLabel("Hier liegt etwas - benutze /pickup!", new Vector3(posX, posY, posZ + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255));
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
            command.CommandText = "UPDATE items SET ownerEntity = @OwnerEntity, ownerIdentifier = @ownerIdentifier, amount = @amount, posX = @posX, posY = @posY, posZ = @posZ WHERE id = @id LIMIT 1";
            command.Parameters.AddWithValue("@ownerEntity", item.ownerEntity);
            command.Parameters.AddWithValue("@ownerIdentifier", item.ownerIdentifier);
            command.Parameters.AddWithValue("@amount", item.amount);
            command.Parameters.AddWithValue("@posX", item.position.X);
            command.Parameters.AddWithValue("@posY", item.position.Y);
            command.Parameters.AddWithValue("@posZ", item.position.Z);
            command.Parameters.AddWithValue("@id", item.id);

            command.ExecuteNonQuery();
        }

        private static List<InventoryModel> GetPlayerInventory(Player player)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);

            int playerId = account.ID;

            foreach (ItemModel item in itemList.ToList())
            {
                if (item != null && item.ownerEntity == "Player" && item.ownerIdentifier == playerId)
                {
                    InventoryModel inventoryItem = new InventoryModel();
                    Item getItem = Item.GetItemFromItem(item.hash);
                    inventoryItem.id = item.id;
                    inventoryItem.hash = item.hash;
                    inventoryItem.descriptionitem = getItem.descriptionitem;
                    inventoryItem.type = getItem.type;
                    inventoryItem.amount = item.amount;

                    inventory.Add(inventoryItem);
                }
            }
            return inventory;
        }


        [RemoteEvent("InventarAktionServer")]
        public void OnInventarAktionServer(Player player, int ItemId, string action)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);

            List<InventoryModel> inventory = new List<InventoryModel>();
            inventory = GetPlayerInventory(player);

            ItemModel item = ItemModel.GetItemModelFromId(ItemId);
            if (item == null) return;

            Item getItem = Item.GetItemFromItem(item.hash);

            switch (action.ToLower())
            {
                case "konsumieren":
                    {
                        if (getItem.type != (int)Item.ItemTypes.Consumable) return;
                        item.amount--;
                        string message = $"Du konsumierst ein/e/en {getItem.descriptionitem}";
                        NAPI.Chat.SendChatMessageToPlayer(player, message);

                        if (item.amount <= 0)
                        {
                            RemoveItem(item.id);
                            itemList.Remove(item);
                        }
                        else
                        {
                            Inventory.UpdateItem(item);
                        }
                        account.showInv = false;
                        player.TriggerEvent("hideInventory");
                        break;
                    }
                case "wegwerfen":
                    {
                        item.amount--;
                        ItemModel closestItem = ItemModel.GetClosestItemWithHash(player, item.hash);
                        if (closestItem != null)
                        {
                            closestItem.amount++;
                            Inventory.UpdateItem(item);
                        }
                        else
                        {
                            closestItem = item.Copy();
                            closestItem.ownerEntity = "Ground";
                            closestItem.position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.92f);
                            closestItem.objectHandle = NAPI.Object.CreateObject(uint.Parse(closestItem.hash), closestItem.position, new Vector3(0.0f, 0.0f, 0.0f), 255);
                            closestItem.textHandle = NAPI.TextLabel.CreateTextLabel("Hier liegt etwas - benutze /pickup!", new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255));
                            closestItem.amount = 1;
                            closestItem.id = AddNewItem(closestItem);
                            itemList.Add(closestItem);
                            UpdateItem(closestItem);

                            if (item.amount <= 0)
                            {
                                RemoveItem(item.id);
                                itemList.Remove(item);
                            }
                        }
                        account.showInv = false;
                        player.TriggerEvent("hideInventory");
                        break;
                    }
            }
        }

        public static ItemModel GetClosestItem(Player player)
        {
            ItemModel itemModel = null;
            foreach(ItemModel item in itemList)
            {
                if(item.ownerEntity == "Ground" && player.Position.DistanceTo(item.position) < 2.5f)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }

        public static ItemModel GetPlayerItemModelFromHash(int playerId, string hash)
        {
            ItemModel itemModel = null;
            foreach(ItemModel item in itemList)
            {
                if(item.ownerEntity == "Player" && item.ownerIdentifier == playerId && item.hash == hash)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }

        [Command("inventory", "Befehl: /inventory um dein Inventar zu öffnen")]
        public static void CMD_inventory(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (account.showInv == false)
            {
                account.showInv = true;
                player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(GetPlayerInventory(player)));
            }
            else
            {
                account.showInv = false;
                player.TriggerEvent("hideInventory");
            }
        }

        [Command("pickup", "Befehl: /pickup um um ein Gegenstand aufzuheben")]
        public void CMD_pickup(Player player)
        {
            if (player.IsInVehicle) return;
            ItemModel item = GetClosestItem(player);
            if(item != null)
            {
                Accounts account = player.GetData<Accounts>(Accounts.Account_Key);

                ItemModel playerItem = GetPlayerItemModelFromHash(account.ID, item.hash);

                if(playerItem != null)
                {
                    playerItem.amount += item.amount;
                }
                else
                {
                    playerItem = item;
                }
                item.objectHandle.Delete();
                item.textHandle.Delete();
                playerItem.ownerEntity = "Player";
                playerItem.ownerIdentifier = account.ID;
                playerItem.position = new Vector3(0.0f, 0.0f, 0.0f);
                UpdateItem(playerItem);
                RemoveItem(item.id);
                player.SendChatMessage($"Du hast erfolgreich etwas aufgehoben!");
            }
        }

        [RemoteEvent("updateInventoryServer")]
        public void OnUpdateInventoryServer(Player player, string json)
        {
            NAPI.Util.ConsoleOutput(json);
        }

    }
}
