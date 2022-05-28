using GTANetworkAPI;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tutorial
{
    class Events : Script
    {
        public ColShape colWillkommen;
        public Marker testMarker = null;

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
            //Fraktions
            Datenbank.LadeAllFraktionen();
            //Timer
            Timer paydayTimer = new Timer(OnPaydayTimer, null, 60000, 60000);
            //Sonstige Sachen
            NAPI.TextLabel.CreateTextLabel("~w~Willkommen auf dem NemesusTV Tutorial Server!", new Vector3(-425.50986, 1123.3857, 325.85443 + 1.0), 10.0f, 0.5f, 4, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(2, new Vector3(-425.50986, 1123.3857, 325.85443), new Vector3(), new Vector3(), 1.0f, new Color(255, 255, 255));
            Blip Willkommen = NAPI.Blip.CreateBlip(357, new Vector3(-425.50986, 1123.3857, 325.85443), 1.0f, 54);
            NAPI.Blip.SetBlipShortRange(Willkommen, true);
            NAPI.Blip.SetBlipScale(Willkommen, 0.8f);
            colWillkommen = NAPI.ColShape.CreateCylinderColShape(new Vector3(-425.50986, 1123.3857, 325.85443), 1.0f, 1.0f);
            //Marker
            testMarker = NAPI.Marker.CreateMarker(42, new Vector3(-417.766, 1133.68, 325.905), new Vector3(0.0, 0.0, 0.0), new Vector3(0.0, 0.0, 0.0), 1.0f, new Color(255, 255, 255), false, 0);
            //Police Carspawner
            NAPI.TextLabel.CreateTextLabel("~w~Benutze Taste ~y~[F]~w~ um ein Fraktionsfahrzeug zu spawnen!", new Vector3(441.07944, -981.0528, 30.689598 + 0.5), 20.0f, 0.5f, 4, new Color(255, 255, 255));
            //Adminlog
            Utils.adminLog("Der Server wurde erfolgreich gestartet", "Server");
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
            player.SetOwnSharedData("Account:Geld", 0);
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnect(Player player, DisconnectionType type, string reason)
        {
            Datenbank.AccountSpeichern(player);
            player.SetOwnSharedData("Account:Geld", 0);
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

        [RemoteEvent("SetPlayerGender")]
        public void OnSetPlayerGender(Player player, string gender)
        {
            if(gender.ToLower() == "männlich")
            {
                NAPI.Player.SetPlayerSkin(player, PedHash.FreemodeMale01);
            }
            else
            {
                NAPI.Player.SetPlayerSkin(player, PedHash.FreemodeFemale01);
            }
        }

        [RemoteEvent("CharacterCreated")]
        public static void OnCharacterCreated(Player player, string character, bool created)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);

            JObject obj = JObject.Parse(character);

            //Hair
            NAPI.Player.SetPlayerClothes(player, 2, (int)obj["hair"][0], 0);
            NAPI.Player.SetPlayerHairColor(player, (byte)obj["hair"][1], (byte)obj["hair"][1]);

            //Eyecolor
            NAPI.Player.SetPlayerEyeColor(player, (byte)obj["eyeColor"][0]);

            //Facefeatures
            for(int i = 0; i< 19; i++)
            {
                NAPI.Player.SetPlayerFaceFeature(player, i, (int)obj["faceFeatures"][i]);
            }

            //Headblend
            HeadBlend headblend = new HeadBlend();
            headblend.ShapeFirst = (byte)obj["blendData"][0];
            headblend.ShapeSecond = (byte)obj["blendData"][1];
            headblend.ShapeThird = 0;
            headblend.SkinFirst = (byte)obj["blendData"][2];
            headblend.SkinSecond = (byte)obj["blendData"][3];
            headblend.SkinThird = 0;
            headblend.ShapeMix = (byte)obj["blendData"][4];
            headblend.SkinMix = (byte)obj["blendData"][5];
            headblend.ThirdMix = 0;
            NAPI.Player.SetPlayerHeadBlend(player, headblend);

            //Clothing
            NAPI.Player.SetPlayerClothes(player, 11, (int)obj["clothing"][0], 0);
            NAPI.Player.SetPlayerClothes(player, 3, (int)obj["clothing"][1], 0);
            NAPI.Player.SetPlayerClothes(player, 8, (int)obj["clothing"][2], 0);
            NAPI.Player.SetPlayerClothes(player, 6, (int)obj["clothing"][3], 0);

            //Headoverlay
            for(int i = 0; i< 12; i++)
            {
                HeadOverlay headOverlay = new HeadOverlay();
                int headOverlayCheck = (int)obj["headOverlays"][i];
                if(headOverlayCheck == -1)
                {
                    headOverlayCheck = 255;
                }
                if(i != 1)
                {
                    headOverlay.Index = (byte)headOverlayCheck;
                    headOverlay.Opacity = 1.0f;
                    headOverlay.Color = (byte)obj["headOverlaysColors"][i];
                    headOverlay.SecondaryColor = (byte)obj["headOverlaysColors"][i];
                }
                else
                {
                    headOverlayCheck = (int)obj["beard"][0];
                    headOverlay.Index = (byte)headOverlayCheck;
                    headOverlay.Opacity = 1.0f;
                    headOverlay.Color = (byte)obj["beard"][1];
                    headOverlay.SecondaryColor = (byte)obj["headOverlaysColors"][2];
                }
                NAPI.Player.SetPlayerHeadOverlay(player, i, headOverlay);
            }
            account.CharacterData = character;
            player.Name = $"{obj["firstname"]}_{obj["lastname"]}";

            if(created == true)
            {
                player.TriggerEvent("showHideMoneyHud");
                player.TriggerEvent("charcreator-hide");
                NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);
                Utils.sendNotification(player, "Charakter erfolgreich erstellt, warte auf Einreise ...", "fas fa-user");
                player.TriggerEvent("showHideMoneyHud");
            }
        }

        [RemoteEvent("OnPlayerPressI")]
        public void OnPlayerPressI(Player player)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            Inventory.Inventory.CMD_inventory(player);
        }

        [RemoteEvent("OnPlayerPressF5")]
        public void OnPlayerPressF5(Player player)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            if(testMarker != null && player.Position.DistanceTo(testMarker.Position) < 2.5f)
            {
                Utils.sendNotification(player, "Du bist im Marker und hast F5 gedrückt!", "fas fa-user-secret");
            }
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
