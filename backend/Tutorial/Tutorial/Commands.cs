using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class Commands : Script
    {
        [Command("veh", "/veh um ein Fahrzeug zu spawnen")]
        public void cmd_veh(Player player, string vehname, int color1, int color2)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Supporter))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            uint vehash = NAPI.Util.GetHashKey(vehname);
            if (vehash <= 0)
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
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Supporter))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            NAPI.ClientEvent.TriggerClientEvent(target, "PlayerFreeze", freezestatus);
            string freezeText = (freezestatus) ? "eingefroren" : "entfroren";
            target.SendChatMessage($"Du wurdest {freezeText}!");
        }

        [Command("telexyz", "/telexyz [X] [Y] [Z]")]
        public void CMD_telexyz(Player player, float x, float y, float z)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Administrator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            Vector3 position = new Vector3(x, y, z + 0.2);
            player.Position = position;
            player.SendChatMessage("Du hast dich erfolgreich teleportiert");
            return;
        }

        [Command("me", "/me [Nachricht]", GreedyArg = true)]
        public void CMD_me(Player player, string nachricht)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            if (nachricht.Length > 0)
            {
                Utils.SendRadiusMessage("!{#EE82EE}* " + player.Name + " " + nachricht, 8, player);
            }
        }

        [Command("save", "/save [Position]", GreedyArg = true)]
        public void CMD_save(Player player, string position)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;

            string status = (player.IsInVehicle) ? "Im Fahrzeug" : "Zu Fuß";
            Vector3 pos = (player.IsInVehicle) ? player.Vehicle.Position : player.Position;
            Vector3 rot = (player.IsInVehicle) ? player.Vehicle.Rotation : player.Rotation;

            string message = 
            $"{status} -> {position}: {pos.X.ToString(new CultureInfo("en-US")):N3}, {pos.Y.ToString(new CultureInfo("en-US")):N3}, {pos.Z.ToString(new CultureInfo("en-US")):N3}, {rot.X.ToString(new CultureInfo("en-US")):N3}, {rot.Y.ToString(new CultureInfo("en-US")):N3}, {rot.Z.ToString(new CultureInfo("en-US")):N3}";

            player.SendChatMessage(message);

            using(StreamWriter file = new StreamWriter(@"./serverdata/savedpositions.txt", true))
            {
                file.WriteLine(message);
            }
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
