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
            NAPI.Server.SetGlobalServerChat(false);
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

        [ServerEvent(Event.ChatMessage)]
        public void OnPlayerChat(Player player, string message)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            //Adminchat
            if(message.StartsWith("@"))
            {
                if(account.IstSpielerAdmin((int)Accounts.AdminRanks.Moderator))
                {
                    Utils.SendAdminMessage(message, (int)Accounts.AdminRanks.Moderator, player);
                    return;
                }
            }
            //Normaler Chat
            string newmessage = player.Name + "[" + Utils.GetPlayerID(player.Name) + "] sagt: !{FFFFFF}" + message;
            Utils.SendRadiusMessage(newmessage, 25, player);
        }
    }
}
