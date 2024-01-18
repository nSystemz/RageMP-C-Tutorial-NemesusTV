using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Tutorial.Controllers;

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
            veh.SetSharedData("Vehicle:Tuning", "n/A");
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

        [Command("aduty", "/aduty um den Admindienst zu beginnen/beenden")]
        public void cmd_aduty(Player player, string password, int color = 1)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if(!account.IstSpielerAdmin((int)Accounts.AdminRanks.Moderator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            if (password == "test" || account.Aduty == true)
            {
                if (!account.IsSpielerADuty())
                {
                    account.Aduty = true;
                    player.SendChatMessage("~g~Du bist jetzt Aduty!");
                    NAPI.Player.SetPlayerClothes(player, 8, 15, color);
                    NAPI.Player.SetPlayerClothes(player, 3, 2, color);
                    NAPI.Player.SetPlayerClothes(player, 10, 0, color);
                    NAPI.Player.SetPlayerClothes(player, 4, 114, color);
                    NAPI.Player.SetPlayerClothes(player, 6, 78, color);
                    NAPI.Player.SetPlayerClothes(player, 1, 135, color);
                    NAPI.Player.SetPlayerClothes(player, 7, 0, color);
                    NAPI.Player.SetPlayerClothes(player, 11, 287, color);
                    NAPI.Player.SetPlayerAccessory(player, 0, -1, color);
                    NAPI.Player.SetPlayerAccessory(player, 1, -1, color);
                    NAPI.Player.SetPlayerAccessory(player, 2, -1, color);
                    NAPI.Player.SetPlayerAccessory(player, 6, -1, color);
                    NAPI.Player.SetPlayerAccessory(player, 7, -1, color);
                }
                else
                {
                    account.Aduty = false;
                    player.SendChatMessage("~g~Du bist nicht mehr Aduty!");
                    Events.OnCharacterCreated(player, account.CharacterData, false);

                }
            }
            else
            {
                player.SendChatMessage("~r~Falsches Passwort!");
            }
        }

        [Command("playmusic", "/playmusic um Musik abzuspiefen!")]
        public void cmd_playmusic(Player player, string music)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Supporter))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayMusic", music);
        }

        [Command("createroulette", "/createroulette um ein Roulette Tisch zu erstellen")]
        public void cmd_createroulette(Player player)
        {
            player.TriggerEvent("createRoulette");
        }

        [Command("startroulette", "/startroulette um das Roullete Spiel zu starten")]
        public void cmd_startroulette(Player player)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 38);
            player.TriggerEvent("startRoulette", number);
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

        [Command("cayo", "/cayo um dich nach Cayo Perico zu teleportieren")]
        public void CMD_cayo(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Supporter))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            player.Position = new Vector3(4840.571, -5174.425, 2.0);
            player.SendChatMessage("Du hast dich nach Cayo Perico teleportiert!");
        }

        [Command("einreise", "/einreise um einen Spieler einreisen zu lassen")]
        public void CMD_einreise(Player player, string playertarget)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Moderator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            Player target = Utils.GetPlayerByNameOrID(playertarget);
            if (target == null)
            {
                player.SendChatMessage("~r~Ungültiger Spieler");
                return;
            }
            Accounts accountTarget = player.GetData<Accounts>(Accounts.Account_Key);
            if (accountTarget.Einreise == 0)
            {
                accountTarget.Einreise = 1;
                player.Position = new Vector3(-420.678, 1182.97, 325.642);
                player.SendChatMessage("~g~Einreise erfolgreich!");
                Utils.sendNotification(player, "Einreise erfolgreich!", "fas fa-user");
                target.SendChatMessage("~g~Einreise erfolgreich!");
                Utils.sendNotification(target, "Einreise erfolgreich!", "fas fa-user");
                Datenbank.AccountSpeichern(target);
            }
            else
            {
                player.SendChatMessage("~r~Der Spieler muss nicht mehr einreisen!");
            }
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

        [Command("dog", "/dog")]
        public void CMD_dog(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Moderator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            if(account.Dog == false)
            {
                account.Dog = true;
                player.TriggerEvent("dogFollowMe");
            }
        }

        [Command("testcloth", "Befehl: /testcloths [Component-ID] [Drawable] [Color*]")]
        public void CMD_testcloths(Player player, int componentid, int drawable, int color = 0)
        {
            NAPI.Task.Run(() =>
            {
                Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
                if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Administrator))
                {
                    player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                    return;
                }
                if (componentid == 0)
                {
                    NAPI.Player.SetPlayerAccessory(player, 0, drawable, color);
                }
                else
                {
                    NAPI.Player.SetPlayerClothes(player, componentid, drawable, color);
                }
                player.SendChatMessage("Testkleidung gesetzt!");
                return;
            });
        }

         [Command("testeupoutfit", "Befehl: /testeupoutfit [Outfit-Name]", GreedyArg = true)]
         public void CMD_testeupoutfit(Player player, String outfitname)
            {
            NAPI.Task.Run(() => {
                try
                {
                    String json1 = "";
                    String json2 = "";
                    if (!Accounts.IstSpielerEingeloggt(player)) return;
                    if (outfitname.Length < 5 || outfitname.Length > 35)
                    {
                        player.SendChatMessage("~r~Ungültiger Outfitname!");
                        return;
                    }

                    MySqlCommand command = Datenbank.Connection.CreateCommand();
                    command.CommandText = "SELECT json1,json2 FROM eupoutfits WHERE owner='EUP' LIMIT 1000";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            json1 = reader.GetString("json1");
                            json2 = reader.GetString("json2");
                        }
                    }

                    string[] json1Array = new string[14];
                    string[] json2Array = new string[14];

                    json1 = json1.Substring(1, json1.Length - 2);
                    json2 = json2.Substring(1, json2.Length - 2);

                    json1Array = json1.Split(",");
                    json2Array = json2.Split(",");

                    NAPI.Player.ClearPlayerAccessory(player, 0);
                    NAPI.Player.ClearPlayerAccessory(player, 1);
                    NAPI.Player.ClearPlayerAccessory(player, 2);
                    NAPI.Player.ClearPlayerAccessory(player, 6);
                    NAPI.Player.ClearPlayerAccessory(player, 7);
                    NAPI.Player.SetPlayerClothes(player, 10, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 9, 0, 0);

                    //Top
                    NAPI.Player.SetPlayerClothes(player, 11, Convert.ToInt32(json1Array[5]) - 1, Convert.ToInt32(json2Array[5]) - 1);
                    //Torso
                    NAPI.Player.SetPlayerClothes(player, 3, Convert.ToInt32(json1Array[6]) - 1, Convert.ToInt32(json2Array[6]) - 1);
                    //Legs
                    NAPI.Player.SetPlayerClothes(player, 4, Convert.ToInt32(json1Array[9]) - 1, Convert.ToInt32(json2Array[9]) - 1);
                    //Shoes
                    NAPI.Player.SetPlayerClothes(player, 6, Convert.ToInt32(json1Array[10]) - 1, Convert.ToInt32(json2Array[10]) - 1);
                    //Undershirt
                    NAPI.Player.SetPlayerClothes(player, 8, Convert.ToInt32(json1Array[8]) - 1, Convert.ToInt32(json2Array[8]) - 1);
                    //Bag
                    NAPI.Player.SetPlayerClothes(player, 5, Convert.ToInt32(json1Array[13]) - 1, Convert.ToInt32(json2Array[13]) - 1);
                    //Glasses
                    NAPI.Player.SetPlayerAccessory(player, 1, Convert.ToInt32(json1Array[1]) - 1, Convert.ToInt32(json2Array[1]) - 1);
                    //Hat
                    NAPI.Player.SetPlayerAccessory(player, 0, Convert.ToInt32(json1Array[0]) - 1, Convert.ToInt32(json2Array[0]) - 1);
                    //Mask
                    NAPI.Player.SetPlayerClothes(player, 1, Convert.ToInt32(json1Array[4]) - 1, Convert.ToInt32(json2Array[4]) - 1);
                    //Ears
                    NAPI.Player.SetPlayerAccessory(player, 2, Convert.ToInt32(json1Array[2]) - 1, Convert.ToInt32(json2Array[2]) - 1);
                    //Watches
                    NAPI.Player.SetPlayerClothes(player, 6, Convert.ToInt32(json1Array[3]) - 1, Convert.ToInt32(json2Array[3]) - 1);
                    //Bracelets
                    NAPI.Player.SetPlayerAccessory(player, 7, Convert.ToInt32(json1Array[7]) - 1, Convert.ToInt32(json2Array[7]) - 1);
                    //Accessories
                    NAPI.Player.SetPlayerClothes(player, 7, Convert.ToInt32(json1Array[11]) - 1, Convert.ToInt32(json2Array[11]) - 1);
                    //Armor
                    NAPI.Player.SetPlayerClothes(player, 9, Convert.ToInt32(json1Array[12]) - 1, Convert.ToInt32(json2Array[12]) - 1);
                    player.SendChatMessage("~g~Testoutfit (EUP) gesetzt!");
                }
                catch (Exception e)
                {
                    NAPI.Util.ConsoleOutput($"[CMD_testeupoutfit]: " + e.ToString());
                }
                return;
            });
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
            HausModel house = HausController.holeHausInReichweite(player);
            if (house != null)
            {
                player.SendChatMessage("~r~Hier ist bereits ein Haus!");
                return;
            }
            string hausLabel = string.Empty;
            house = new HausModel();
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

                    HausController.hausListe.Add(house);

                    player.SendChatMessage("~g~ Das Haus wurde erfolgreich erstellt!");
                });

            });
        }

        [Command("enter", "/enter um ein Haus zu betreten!")]
        public void CMD_enter(Player player)
        {
            foreach (HausModel house in HausController.hausListe)
            {
                if (player.Position.DistanceTo(house.position) <= 2.5f)
                {
                    if (!HausController.HatSpielerSchluessel(player, house) && house.abgeschlossen)
                    {
                        player.SendChatMessage("~r~Türe ist abgeschlossen!");
                    }
                    else
                    {
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
            foreach (HausModel house in HausController.hausListe)
            {
                if (player.Position.DistanceTo(HausInterior.GetHausAusgang(house.ipl)) <= 2.5f)
                {
                    if (!HausController.HatSpielerSchluessel(player, house) && house.abgeschlossen)
                    {
                        player.SendChatMessage("~r~Türe ist abgeschlossen!");
                    }
                    else
                    {
                        player.Position = house.position;
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
            HausModel house = null;
            house = HausController.holeHausInReichweite(player);
            if (house != null)
            {
                house = HausController.holeHausInReichweite(player);
            }
            else
            {
                house = HausController.HoleHausMitID(player.GetData<int>("Haus_ID"));
            }
            if (house != null)
            {
                if (HausController.HatSpielerSchluessel(player, house))
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
            HausModel house = HausController.holeHausInReichweite(player);
            if (house == null || house.besitzer != "Keiner")
            {
                player.SendChatMessage("~r~Du bist nicht in der Nähe von einem freien Haus!");
                return;
            }
            if (account.Geld < house.preis)
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
            HausModel house = HausController.holeHausInReichweite(player);
            if (house == null)
            {
                player.SendChatMessage("~r~Du bist nicht in der Nähe von einem Haus!");
                return;
            }
            if (house.besitzer != player.Name)
            {
                player.SendChatMessage("~r~Dieses Haus gehört dir nicht!");
                return;
            }
            account.Geld += house.preis / 2;
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
            if (vehicle != null)
            {
                if (vehicle.Locked == true)
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

        [Command("tuning", "/tuning um dein Fahrzeug zu tunen!")]
        public void CMD_tuning(Player player)
        {
            Vehicle vehicle = player.Vehicle;
            if (vehicle != null)
            {
                string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(vehicle)},{NAPI.Vehicle.GetVehicleSecondaryColor(vehicle)},{NAPI.Vehicle.GetVehiclePearlescentColor(vehicle)},{NAPI.Vehicle.GetVehicleWheelColor(vehicle)}";
                vehicle.SetSharedData("Vehicle:Color", color);
                player.TriggerEvent("Client:ShowTuning");
            }
            else
            {
                player.SendChatMessage("~r~Du sitzt in keinem Fahrzeug!");
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
            if (accounttarget != null && frak < 0 || frak > Accounts.FraktionsDaten.Length)
            {
                player.SendChatMessage("~r~Ungültige Fraktion!");
                return;
            }
            accounttarget.Fraktion = frak;
            accounttarget.Rang = 10;
            player.SendChatMessage($"Du hast {target.Name} zum Chef der Fraktion {Accounts.FraktionsDaten[frak, 0]} gemacht!");
            target.SendChatMessage($"Du wurdest von {player.Name} zum Chef der Fraktion {Accounts.FraktionsDaten[frak, 0]} gemacht!");
        }

        [Command("whitelist", "/whitelist um einen Spieler auf die Whitelist zu setzen")]
        public void CMD_whitelist(Player player, ulong socialclubid)
        {
            bool found = false;
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (!account.IstSpielerAdmin((int)Accounts.AdminRanks.Administrator))
            {
                player.SendChatMessage("~r~Dein Adminlevel ist zu gering!");
                return;
            }
            if (socialclubid < 10000)
            {
                player.SendChatMessage("~r~Ungültige Socialclubid!");
                return;
            }
            MySqlCommand command = Datenbank.Connection.CreateCommand();
            command.CommandText = "SELECT id from whitelist WHERE socialclubid=@socialclubid LIMIT 1";
            command.Parameters.AddWithValue("socialclubid", socialclubid);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    found = true;
                }
            }
            if (found == true)
            {
                MySqlCommand command2 = Datenbank.Connection.CreateCommand();
                command2.CommandText = "DELETE FROM whitelist WHERE socialclubid=@socialclubid LIMIT 1";
                command2.Parameters.AddWithValue("socialclubid", socialclubid);
                command2.ExecuteNonQuery();
                player.SendChatMessage("~g~Whitelisteintrag entfernt!");
            }
            else
            {
                MySqlCommand command3 = Datenbank.Connection.CreateCommand();
                command3.CommandText = "INSERT INTO whitelist (socialclubid) VALUES (@socialclubid)";
                command3.Parameters.AddWithValue("socialclubid", socialclubid);
                command3.ExecuteNonQuery();
                player.SendChatMessage("~g~Whitelisteintrag erfolgreich hinzugefügt!");
            }
        }

        [Command("invite", "/invite um einen zu deiner Fraktion einzuladen!")]
        public void CMD_invite(Player player, String playertarget)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (account.Fraktion == 0)
            {
                player.SendChatMessage("~r~Du bist in keiner Fraktion!");
                return;
            }
            if (account.Rang < 10)
            {
                player.SendChatMessage($"~r~Du bist kein {Accounts.FraktionsDaten[account.Fraktion, 10]}");
                return;
            }
            Player target = Utils.GetPlayerByNameOrID(playertarget);
            Accounts accounttarget = target.GetData<Accounts>(Accounts.Account_Key);
            accounttarget.Fraktion = account.Fraktion;
            accounttarget.Rang = 1;
            player.SendChatMessage($"Du hast {target.Name} in die Fraktion {Accounts.FraktionsDaten[account.Fraktion, 0]} eingeladen!");
            target.SendChatMessage($"Du wurdest von {player.Name} in die Fraktion {Accounts.FraktionsDaten[account.Fraktion, 0]} eingeladen!");
        }

        [Command("pistole", "/pistole um dir eine Pistole zu geben")]
        public void CMD_pistole(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (account.Fraktion != 1)
            {
                player.SendChatMessage($"~r~Du bist kein Mitglied des {Accounts.FraktionsDaten[1, 0]}!");
                return;
            }
            NAPI.Player.GivePlayerWeapon(player, NAPI.Util.WeaponNameToModel("pistol"), 500);
            player.SendChatMessage("Du hast dir eine Pistole gegeben!");
        }

        [Command("carlock", "/carlock um ein Fahrzeug auf/ab zu schließen!")]
        public void CMD_carlock(Player player)
        {
            Vehicle getVehicle = Utils.GetClosestVehicle(player);
            if (getVehicle != null)
            {
                Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
                if (getVehicle.GetData<int>("VEHICLE_FRAKTION") == account.Fraktion)
                {
                    if (getVehicle.Locked == true)
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

        [Command("charcreator")]
        public void CMD_charcreator(Player player)
        {
            player.TriggerEvent("showHideMoneyHud");
            player.TriggerEvent("charcreator-show");
        }

        [Command("crosshair")]
        public void CMD_crosshair(Player player, int crosshair)
        {
            if (crosshair < 0 || crosshair > 18)
            {
                Utils.sendNotification(player, "Ungültiges Crosshair", "fas fa-user");
                return;
            }
            player.TriggerEvent("showcrosshair", crosshair);
            Utils.sendNotification(player, "Crosshair gesetzt!", "fas fa-user");
        }

        [Command("crosshairhide")]
        public void CMD_crosshairhide(Player player)
        {
            player.TriggerEvent("hidecrosshair");
            Utils.sendNotification(player, "Crosshair deaktiviert!", "fas fa-user");
        }

        [Command("geld")]
        public void CMD_geld(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            account.Geld += 50;
            player.SetOwnSharedData("Account:Geld", account.Geld);
            player.SendChatMessage("~g~Geld gegeben!");
        }

        [Command("docs")]
        public void CMD_docs(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (account.Fraktion == 1)
            {
                player.TriggerEvent("ShowDocsWindow");
            }
        }
    }
}
