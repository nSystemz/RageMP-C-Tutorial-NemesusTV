using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class Connections : Script
    {
        [RemoteEvent("Auth.OnLogin")]
        public void OnLogin(Player player, string password)
        {
            if(Accounts.IstSpielerEingeloggt(player))
            {
                player.SendNotification("~r~Du bist bereits eingeloggt!");
                return;
            }
            if (!Datenbank.AccountCheck(player.Name))
            {
                player.SendNotification("~r~Ungültiger Account!");
                return;
            }
            if (!Datenbank.PasswortCheck(player.Name, password))
            {
                player.SendNotification("~r~Falsches Passwort");
                NAPI.Task.Run(() =>
                {
                   player.Kick();
                }, delayTime: 1500);
                return;
            }
            Accounts account = new Accounts(player.Name, player);
            account.Login(player, false);
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);
        }

        [RemoteEvent("Auth.OnRegister")]
        public void OnRegister(Player player, string password)
        {
            if(Datenbank.IstAccountBereitsVorhanden(player.Name))
            {
                player.SendNotification("~r~Dieser Account existiert bereits!");
                NAPI.Task.Run(() =>
                {
                    player.Kick();
                }, delayTime: 1500);
                return;
            }
            Accounts accounts = new Accounts(player.Name, player);
            accounts.Register(player.Name, password);
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);
        }
    }
}
