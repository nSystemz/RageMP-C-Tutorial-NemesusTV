using System;
using System.Collections.Generic;
using System.Text;
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
            //Sonstige Sachen
            NAPI.TextLabel.CreateTextLabel("~w~Willkommen auf dem NemesusTV Tutorial Server!", new Vector3(-425.50986, 1123.3857, 325.85443 + 1.0), 10.0f, 0.5f, 4, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(2, new Vector3(-425.50986, 1123.3857, 325.85443), new Vector3(), new Vector3(), 1.0f, new Color(255, 255, 255));
            Blip Willkommen = NAPI.Blip.CreateBlip(357, new Vector3(-425.50986, 1123.3857, 325.85443), 1.0f, 54);
            NAPI.Blip.SetBlipShortRange(Willkommen, true);
            NAPI.Blip.SetBlipScale(Willkommen, 0.8f);
            colWillkommen = NAPI.ColShape.CreateCylinderColShape(new Vector3(-425.50986, 1123.3857, 325.85443), 1.0f, 1.0f);
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

        [ServerEvent(Event.PlayerSpawn)]
        public void OnPlayerSpawn(Player player)
        {
            player.Health = 50;
            player.Armor = 50;
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
            if(colshape == colWillkommen)
            {
                player.SendChatMessage("~y~Hallo und Willkommen auf dem Server!");
            }
        }

        [RemoteEvent("SpawnVehicleServer")]
        public void OnSpawnVehicleServer(Player player, string vehiclename, bool sitIn)
        {
            uint vehhash = NAPI.Util.GetHashKey(vehiclename);
            if(vehhash <= 0)
            {
                player.SendChatMessage("~r~Ungültiges Fahrzeug!");
                return;
            }
            Vehicle veh = NAPI.Vehicle.CreateVehicle(vehhash, player.Position, player.Heading, 0, 0);
            veh.NumberPlate = "Tutorial";
            veh.Locked = false;
            veh.EngineStatus = true;
            if(sitIn)
            {
                player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
            }

        }
    }
}
