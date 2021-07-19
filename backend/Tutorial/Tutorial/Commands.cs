using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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

        [Command("vehspawner", "/vehspawner um das Fahrzeugmenü zu öffnen!")]
        public void cmd_vehspawner(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if(!account.IstSpielerAdmin((int)Accounts.AdminRanks.Supporter))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            NAPI.ClientEvent.TriggerClientEvent(player, "vSpawner");
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

        [Command("createhouse", "Befehl: /createhouse um ein Haus zu erstellen")]
        public void CMD_createhouse(Player player, int ipl, int preis)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Administrator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            Haus house = Haus.holeHausInReichweite(player);
            if (house != null)
            {
                player.SendChatMessage("~r~Hier kann kein Haus erstellt werden!");
                return;
            }
            string houseLabel = string.Empty;
            house = new Haus();
            house.ipl = HausInterior.Interior_Liste[ipl].ipl;
            house.position = player.Position;
            house.preis = preis;
            house.besitzer = "Keiner";
            house.status = false;
            house.abgeschlossen = false;

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    house.id = Datenbank.ErstelleHaus(house);

                    house.hausLabel = NAPI.TextLabel.CreateTextLabel($"Dieses Haus steht für {preis}$ zum Verkauf, benutzt /buyhouse um es zu kaufen!", new Vector3(house.position.X, house.position.Y, house.position.Z + 0.8), 5.0f, 0.75f, 4, new Color(255, 255, 255));

                    if (house.status == false)
                    {
                        house.hausBlip = NAPI.Blip.CreateBlip(40, house.position, 1.0f, 2);
                        house.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(38, 230, 0), false);
                    }
                    else
                    {
                        house.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(255, 255, 255), false);
                        house.hausBlip = NAPI.Blip.CreateBlip(40, house.position, 1.0f, 1);
                    }
                    NAPI.Blip.SetBlipName(house.hausBlip, "Hausnummer: " + house.id); NAPI.Blip.SetBlipShortRange(house.hausBlip, true);
                    Haus.hausListe.Add(house);

                    player.SendChatMessage("~g~Das Haus wurde erfolgreich erstellt!");
                    return;
                });
            });
            return;
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
