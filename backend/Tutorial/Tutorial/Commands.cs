using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class Commands : Script
    {
        [Command("veh", "/veh um ein Fahrzeug zu spawnen")]
        public void cmd_veh(Player player, string vehname, int color1, int color2)
        {
            uint vehash = NAPI.Util.GetHashKey(vehname);
            if(vehash <= 0)
            {
                player.SendChatMessage("~r~Ungültiges Fahrzeug!");
                return;
            }
            Vehicle veh = NAPI.Vehicle.CreateVehicle(vehash, player.Position, player.Heading, color1, color2);
            veh.NumberPlate = "Tutorial";
            veh.Locked = false;
            veh.EngineStatus = true;
            player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
        }

        [Command("freeze", "/freeze einen Spieler einfrieren")]
        public void CMD_FreezePlayer(Player player, Player target, bool freezestatus)
        {
            NAPI.ClientEvent.TriggerClientEvent(target, "PlayerFreeze", freezestatus);
            string freezeText = (freezestatus) ? "eingefroren" : "entfroren";
            target.SendChatMessage($"Du wurdest {freezeText}!");
        }


        /*[Command("login", "/login um dich einzuloggen")]
        public void CMD_Login(Player player, string password)
        {
            if(Accounts.IstSpielerEingeloggt(player))
            {
                player.SendNotification("~r~Du bist bereits eingeloggt!");
                return;
            }
            if(!Datenbank.IstAccountBereitsVorhanden(player.Name))
            {
                player.SendNotification("~r~Keinen Account gefunden!");
                return;
            }
            if(!Datenbank.PasswortCheck(player.Name, password))
            {
                player.SendNotification("~r~Falsches Passwort!");
                return;
            }
            Accounts account = new Accounts(player.Name, player);
            account.Login(player, false);
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);
        }

        [Command("register", "/register um dich zu registrieren")]
        public void CMD_Register(Player player, string password)
        {
            if (Accounts.IstSpielerEingeloggt(player))
            {
                player.SendNotification("~r~Du bist bereits eingeloggt!");
                return;
            }
            if(Datenbank.IstAccountBereitsVorhanden(player.Name))
            {
                player.SendNotification("~r~Account bereits vorhanden!");
                return;
            }
            Accounts account = new Accounts(player.Name, player);
            account.Register(player.Name, password);
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);
        }*/
    }
}
