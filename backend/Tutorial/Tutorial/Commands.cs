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
            veh.Locked = true;
            veh.EngineStatus = true;
            player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
            Utils.sendNotification(player, "Fahrzeug erfolgreich gespawnt!", "fas fa-car");
        }

        [Command("vehspawner", "/vehspawner um das Fahrzeugmenü zu öffnen!")]
        public void cmd_vehspawner(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Supporter))
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

            using (StreamWriter file = new StreamWriter(@"./serverdata/savedpositions.txt", true))
            {
                file.WriteLine(message);
            }
        }

        [Command("createhouse", "/createhouse um ein Haus zu erstellen!")]
        public void CMD_createhouse(Player player, int preis, int ipl)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Administrator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            Haus house = Haus.holeHausInReichweite(player);
            if (house != null)
            {
                player.SendChatMessage("~r~Hier ist bereits ein Haus!");
                return;
            }
            string hausLabel = string.Empty;
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
                    house.hausLabel = NAPI.TextLabel.CreateTextLabel($"Dieses Haus steht für {preis}$ zum Verkauf, benutze /buyhouse um es zu kaufen!", new Vector3(house.position.X, house.position.Y, house.position.Z + 0.8), 5.0f, 0.75f, 4, new Color(255, 255, 255));
                    if (house.status == false)
                    {
                        house.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(38, 230, 0), false);
                        house.hausBlip = NAPI.Blip.CreateBlip(40, house.position, 1.0f, 2);
                    }
                    else
                    {
                        house.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(255, 255, 255), false);
                        house.hausBlip = NAPI.Blip.CreateBlip(40, house.position, 1.0f, 1);
                    }
                    NAPI.Blip.SetBlipName(house.hausBlip, "Hausnummer: " + house.id);
                    NAPI.Blip.SetBlipShortRange(house.hausBlip, true);

                    Haus.hausListe.Add(house);

                    player.SendChatMessage("~g~ Das Haus wurde erfolgreich erstellt!");
                });

            });
        }

        [Command("enter", "/enter um ein Haus zu betreten!")]
        public void CMD_enter(Player player)
        {
            foreach (Haus house in Haus.hausListe)
            {
                if (player.Position.DistanceTo(house.position) <= 2.5f)
                {
                    if (!Haus.HatSpielerSchluessel(player, house) && house.abgeschlossen)
                    {
                        player.SendChatMessage("~r~Türe ist abgeschlossen!");
                    }
                    else
                    {
                        NAPI.World.RequestIpl(house.ipl);
                        player.Position = HausInterior.GetHausAusgang(house.ipl);
                        player.SetData("Haus_ID", house.id);
                        player.Dimension = (uint)house.id;
                        player.SendNotification("~g~Haus betreten!");
                    }
                }
            }
        }

        [Command("exit", "/exit um ein Haus zu verlassen!")]
        public void CMD_exit(Player player)
        {
            foreach (Haus house in Haus.hausListe)
            {
                if (player.Position.DistanceTo(HausInterior.GetHausAusgang(house.ipl)) <= 2.5f)
                {
                    if (!Haus.HatSpielerSchluessel(player, house) && house.abgeschlossen)
                    {
                        player.SendChatMessage("~r~Türe ist abgeschlossen!");
                    }
                    else
                    {
                        player.Position = house.position;
                        NAPI.World.RemoveIpl(house.ipl);
                        player.SetData("Haus_ID", -1);
                        player.Dimension = 0;
                        player.SendNotification("~r~Haus verlassen!");
                    }
                }
            }
        }

        [Command("lock", "/lock um ein Haus auf/ab zu schließen!")]
        public void CMD_lock(Player player)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            Haus house = null;
            house = Haus.holeHausInReichweite(player);
            if(house != null)
            {
                house = Haus.holeHausInReichweite(player);
            }
            else
            {
                house = Haus.HoleHausMitID(player.GetData<int>("Haus_ID"));
            }
            if (house != null)
            {
                if (Haus.HatSpielerSchluessel(player, house))
                {
                    if (house.abgeschlossen == false)
                    {
                        house.abgeschlossen = true;
                        player.SendNotification("~r~Haus abgeschlossen");
                    }
                    else
                    {
                        house.abgeschlossen = false;
                        player.SendNotification("~g~ Haus aufgeschlossen");
                    }
                }
            }
            else
            {
                player.SendChatMessage("~r~Du bist nicht in der Nähe von einem Haus!");
            }
        }
        [Command("buyhouse", "/buyhouse um ein Haus zu kaufen!")]
        public void CMD_buyhouse(Player player)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            Haus house = Haus.holeHausInReichweite(player);
            if(house == null || house.besitzer != "Keiner")
            {
                player.SendChatMessage("~r~Du bist nicht in der Nähe von einem freien Haus!");
                return;
            }
            if(account.Geld < house.preis)
            {
                player.SendChatMessage("~r~ Du hast nicht genügend Geld dabei!");
                return;
            }
            account.Geld -= house.preis;
            house.status = true;
            house.besitzer = player.Name;

            NAPI.Entity.DeleteEntity(house.hausMarker);
            NAPI.Entity.DeleteEntity(house.hausBlip);
            NAPI.Entity.DeleteEntity(house.hausLabel);

            house.hausLabel = NAPI.TextLabel.CreateTextLabel($"Dieses Haus gehört {house.besitzer}, benutze /enter um es zu betreten!", new Vector3(house.position.X, house.position.Y, house.position.Z + 0.8), 5.0f, 0.75f, 4, new Color(255, 255, 255));
            if (house.status == false)
            {
                house.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(38, 230, 0), false);
                house.hausBlip = NAPI.Blip.CreateBlip(40, house.position, 1.0f, 2);
            }
            else
            {
                house.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(255, 255, 255), false);
                house.hausBlip = NAPI.Blip.CreateBlip(40, house.position, 1.0f, 1);
            }
            NAPI.Blip.SetBlipName(house.hausBlip, "Hausnummer: " + house.id);
            NAPI.Blip.SetBlipShortRange(house.hausBlip, true);

            player.SendChatMessage("~g~Du hast das Haus erfolgreich erworben!");

            Task.Factory.StartNew(() =>
            {
                Datenbank.HausSpeichern(house);
            });
        }

        [Command("sellhouse", "/sellhouse um ein Haus zu verkaufen!")]
        public void CMD_sellhouse(Player player)
        {
            if (!Accounts.IstSpielerEingeloggt(player)) return;
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            Haus house = Haus.holeHausInReichweite(player);
            if (house == null)
            {
                player.SendChatMessage("~r~Du bist nicht in der Nähe von einem Haus!");
                return;
            }
            if(house.besitzer != player.Name)
            {
                player.SendChatMessage("~r~Dieses Haus gehört dir nicht!");
                return;
            }
            account.Geld += house.preis/2;
            house.status = false;
            house.abgeschlossen = false;
            house.besitzer = "Keiner";

            NAPI.Entity.DeleteEntity(house.hausMarker);
            NAPI.Entity.DeleteEntity(house.hausBlip);
            NAPI.Entity.DeleteEntity(house.hausLabel);

            house.hausLabel = NAPI.TextLabel.CreateTextLabel($"Dieses Haus steht für {house.preis}$ zum Verkauf, benutze /buyhouse um es zu kaufen!", new Vector3(house.position.X, house.position.Y, house.position.Z + 0.8), 5.0f, 0.75f, 4, new Color(255, 255, 255));
            if (house.status == false)
            {
                house.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(38, 230, 0), false);
                house.hausBlip = NAPI.Blip.CreateBlip(40, house.position, 1.0f, 2);
            }
            else
            {
                house.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(255, 255, 255), false);
                house.hausBlip = NAPI.Blip.CreateBlip(40, house.position, 1.0f, 1);
            }
            NAPI.Blip.SetBlipName(house.hausBlip, "Hausnummer: " + house.id);
            NAPI.Blip.SetBlipShortRange(house.hausBlip, true);

            player.SendChatMessage("~g~Du hast das Haus erfolgreich verkauft!");

            Task.Factory.StartNew(() =>
            {
                Datenbank.HausSpeichern(house);
            });
        }

        [Command("lockpicking", "/lockpicking um ein Fahrzeug aufzuknacken!")]
        public void CMD_lockpicking(Player player)
        {
            Vehicle vehicle = Utils.GetClosestVehicle(player);
            if(vehicle != null)
            {
                if(vehicle.Locked == true)
                {
                    NAPI.ClientEvent.TriggerClientEvent(player, "showLockpicking");
                }
                else
                {
                    player.SendChatMessage("~r~Das Fahrzeug ist bereits offen!");
                }
            }
            else
            {
                player.SendChatMessage("~r~Du bist nicht in der Nähe von einem Fahrzeug!");
            }
        }

        //Nicht in der Tutorialreihe erstellt
        [Command("setskin", "/setskin um dir ein Skin zu setzen!")]
        public void CMD_setskin(Player player, string model)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Administrator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            uint skinhash = NAPI.Util.GetHashKey(model);
            NAPI.Player.SetPlayerSkin(player, skinhash);
            player.SendChatMessage("~g~Skin erfolgreich gesetzt!");
        }

        [Command("loadipl", "/loadipl um eine IPL zu laden!")]
        public void CMD_loadipl(Player player, string ipl)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Administrator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            NAPI.World.RequestIpl(ipl);
            player.SendChatMessage("~g~IPL Erfolgreich geladen!");
        }

        [Command("fraktionsinfo", "/fraktionsinfo für eine Fraktionsübersicht")]
        public void CMD_fraktionsinfo(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            player.SendChatMessage($"Du bist in der Fraktion {account.HoleFraktionsName()} und hast den Rang {account.HoleRangName()}!");
        }

        [Command("makeleader", "/makeleader um einen zum Leader zu machen!")]
        public void CMD_makeleader(Player player, String playertarget, int frak)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Administrator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            Player target = Utils.GetPlayerByNameOrID(playertarget);
            Accounts accounttarget = target.GetData<Accounts>(Accounts.Account_Key);
            if(accounttarget != null && frak < 0 || frak > Accounts.Fraktionen.Length)
            {
                player.SendChatMessage("~r~Ungültige Fraktion!");
                return;
            }
            accounttarget.Fraktion = frak;
            accounttarget.Rang = 6;
            player.SendChatMessage($"Du hast {target.Name} zum Chef der Fraktion {Accounts.Fraktionen[frak]} gemacht!");
            target.SendChatMessage($"Du wurdest von {player.Name} zum Chef der Fraktion {Accounts.Fraktionen[frak]} gemacht!");
        }

        [Command("invite", "/invite um einen zu deiner Fraktion einzuladen!")]
        public void CMD_invite(Player player, String playertarget)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if(account.Fraktion == 0)
            {
                player.SendChatMessage("~r~Du bist in keiner Fraktion!");
                return;
            }
            if(account.Rang < 6)
            {
                player.SendChatMessage($"~r~Du bist kein {Accounts.RangNamen[6]}");
                return;
            }
            Player target = Utils.GetPlayerByNameOrID(playertarget);
            Accounts accounttarget = target.GetData<Accounts>(Accounts.Account_Key);
            accounttarget.Fraktion = account.Fraktion;
            accounttarget.Rang = 1;
            player.SendChatMessage($"Du hast {target.Name} in die Fraktion {Accounts.Fraktionen[account.Fraktion]} eingeladen!");
            target.SendChatMessage($"Du wurdest von {player.Name} in die Fraktion {Accounts.Fraktionen[account.Fraktion]} eingeladen!");
        }

        [Command("pistole","/pistole um dir eine Pistole zu geben")]
        public void CMD_pistole(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if(account.Fraktion != 1)
            {
                player.SendChatMessage($"~r~Du bist kein Mitglied des {Accounts.Fraktionen[1]}!");
                return;
            }
            NAPI.Player.GivePlayerWeapon(player, NAPI.Util.WeaponNameToModel("pistol"), 500);
            player.SendChatMessage("Du hast dir eine Pistole gegeben!");
        }

        [Command("carlock", "/carlock um ein Fahrzeug auf/ab zu schließen!")]
        public void CMD_carlock(Player player)
        {
            Vehicle getVehicle = Utils.GetClosestVehicle(player);
            if(getVehicle != null)
            {
                Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
                if(getVehicle.GetData<int>("VEHICLE_FRAKTION") == account.Fraktion)
                {
                    if(getVehicle.Locked == true)
                    {
                        getVehicle.Locked = false;
                        Utils.sendNotification(player, "Aufgeschlossen", "fas fa-car");
                    }
                    else
                    {
                        getVehicle.Locked = true;
                        Utils.sendNotification(player, "Abgeschlossen", "fas fa-car");
                    }
                }
            }
        }
    }
}
