using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class Events : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            if(Settings.LoadServerSettings())
            {
                Datenbank.InitConnection();
            }
        }
        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", true);
            player.SendChatMessage("~y~Hallo und Willkommen auf meinem Server!");
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
    }
}
