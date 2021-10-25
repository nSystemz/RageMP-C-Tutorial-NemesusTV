using GTANetworkAPI;
using System.Threading;
using System.Threading.Tasks;

namespace Tutorial
{
    class Events : Script
    {
        public ColShape colWillkommen;

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Server.SetGlobalServerChat(false);
            if (Settings.LoadServerSettings())
            {
                Datenbank.InitConnection();
            }
            //Häuser
            Haus.hausListe = Datenbank.LadeAlleHäuser();
            //Items
            Inventory.Inventory.itemList = Inventory.Inventory.LoadAllItems();
            //Timer
            Timer paydayTimer = new Timer(OnPaydayTimer, null, 60000, 60000);
            //Sonstige Sachen
            NAPI.TextLabel.CreateTextLabel("~w~Willkommen auf dem NemesusTV Tutorial Server!", new Vector3(-425.50986, 1123.3857, 325.85443 + 1.0), 10.0f, 0.5f, 4, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(2, new Vector3(-425.50986, 1123.3857, 325.85443), new Vector3(), new Vector3(), 1.0f, new Color(255, 255, 255));
            Blip Willkommen = NAPI.Blip.CreateBlip(357, new Vector3(-425.50986, 1123.3857, 325.85443), 1.0f, 54);
            NAPI.Blip.SetBlipShortRange(Willkommen, true);
            NAPI.Blip.SetBlipScale(Willkommen, 0.8f);
            colWillkommen = NAPI.ColShape.CreateCylinderColShape(new Vector3(-425.50986, 1123.3857, 325.85443), 1.0f, 1.0f);
            //Police Carspawner
            NAPI.TextLabel.CreateTextLabel("~w~Benutze Taste ~y~[F]~w~ um ein Fraktionsfahrzeug zu spawnen!", new Vector3(441.07944, -981.0528, 30.689598 + 0.5), 20.0f, 0.5f, 4, new Color(255, 255, 255));

        }

        public static void OnPaydayTimer(object state)
        {
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    foreach (Player player in NAPI.Pools.GetAllPlayers())
                    {
                        //Payday
                        Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
                        if (account != null && Accounts.IstSpielerEingeloggt(player))
                        {
                            account.Payday--;
                            if (account.Payday <= 0)
                            {
                                account.Payday = 60;
                                player.SendNotification("Payday: 500$");
                                account.Geld += 500;
                            }
                        }
                    }
                });
            });
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", true);
            player.SendChatMessage("~y~Serverdaten werden geladen ...");
            /*if(Datenbank.IstAccountBereitsVorhanden(player.Name))
            {
                player.SendChatMessage("~w~Dein Account wurde in unserer Datenbank gefunden, bitte logge dich mit /login ein!");
            }
            else
            {
                player.SendChatMessage("~w~Bitte erstellte dir mit /register einen Account!");
            }*/
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnect(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (account != null)
            {
                Datenbank.AccountSpeichern(account);
            }
        }

        [ServerEvent(Event.PlayerSpawn)]
        public void OnPlayerSpawn(Player player)
        {
            player.Health = 50;
            player.Armor = 50;
            player.Dimension = 0;
        }

        [ServerEvent(Event.ChatMessage)]
        public void OnPlayerChat(Player player, string message)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            //Adminchat
            if (message.StartsWith("@"))
            {
                if (account.IstSpielerAdmin((int)Accounts.AdminRanks.Moderator))
                {
                    Utils.SendAdminMessage(message, (int)Accounts.AdminRanks.Moderator, player);
                    return;
                }
            }
            //Normaler Chat
            string newmessage = player.Name + "[" + Utils.GetPlayerID(player.Name) + "] sagt: !{FFFFFF}" + message;
            Utils.SendRadiusMessage(newmessage, 25, player);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColShape(ColShape colshape, Player player)
        {
            if (colshape == colWillkommen)
            {
                player.SendChatMessage("~y~Hallo und Willkommen auf dem Server!");
            }
        }

        [RemoteEvent("SpawnVehicleServer")]
        public void OnSpawnVehicleServer(Player player, string vehiclename, bool sitIn)
        {
            uint vehhash = NAPI.Util.GetHashKey(vehiclename);
            if (vehhash <= 0)
            {
                player.SendChatMessage("~r~Ungültiges Fahrzeug!");
                return;
            }
            Vehicle veh = NAPI.Vehicle.CreateVehicle(vehhash, player.Position, player.Heading, 0, 0);
            veh.NumberPlate = "Tutorial";
            veh.Locked = true;
            veh.EngineStatus = true;
            if (sitIn)
            {
                player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
            }

        }

        [RemoteEvent("successLockpickingServer")]
        public void OnSuccessLockpickingServer(Player player)
        {
            Vehicle veh = Utils.GetClosestVehicle(player);
            if(veh != null && veh.Locked == true)
            {
                veh.Locked = false;
                player.SendChatMessage("~g~Fahrzeug erfolgreich geknackt!");
                NAPI.ClientEvent.TriggerClientEvent(player, "hideLockpicking");
            }
        }

        [RemoteEvent("failedLockpickingServer")]
        public void OnFailedLockpickingServer(Player player)
        {
            player.SendChatMessage("~r~Fahrzeug konnte nicht geknackt werden");
            NAPI.ClientEvent.TriggerClientEvent(player, "hideLockpicking");
        }

        [RemoteEvent("OnPlayerPressI")]
        public void OnPlayerPressI(Player player)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            Inventory.Inventory.CMD_inventory(player);
        }

        [RemoteEvent("OnPlayerPressF")]
        public void OnPlayerPressF(Player player)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            Vector3 npcPosition = new Vector3(-420.6354, 1120.6459, 325.85843);
            if (player.Position.DistanceTo(npcPosition) < 1.5f)
            {
                player.SendChatMessage("~g~Du hast dir erfolgreich ein Hot-Dog erworben!");
                if (player.Health <= 75)
                {
                    player.Health = player.Health + 25;
                }
            }
            Vector3 npcPoliceSpawner = new Vector3(441.07944, -981.0528, 30.689598);
            if(player.Position.DistanceTo(npcPoliceSpawner) < 1.5f)
            {
                if(account.IstSpielerInFraktion(1))
                {
                    Vehicle tempVehicle;
                    tempVehicle = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey("police"), new Vector3(452.96118, -1020.051, 27.976496), -93.67f, 0, 0);
                    tempVehicle.Locked = true;
                    tempVehicle.SetData<int>("VEHICLE_FRAKTION", 1);
                    player.SetIntoVehicle(tempVehicle, (int)VehicleSeat.Driver);
                }
            }
        }
    }
}
