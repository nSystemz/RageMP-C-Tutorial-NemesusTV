using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using GTANetworkAPI;
using GTANetworkMethods;
using Tutorial.Models;
using Player = GTANetworkAPI.Player;

namespace Tutorial
{
    class Accounts
    {
        public static String[,] FraktionsDaten = new string[3, 11];

        public enum ProgressBars { Healthbar = 1, Hungerbar, Thirstbar };

        public enum AdminRanks { Spieler, Moderator, Supporter, Administrator };

        public const string Account_Key = "Account_Data";
        public int ID;
        public string Name;
        public int Adminlevel;
        public long Geld;
        public Player _player;
        public int Payday;
        public int Fraktion;
        public int Rang;
        public bool showInv;
        public float[] positions = new float[4];
        public int Einreise;
        public string CharacterData;
        public bool Aduty;
        public bool Dog;
        public string DiscordID;

        public Accounts()
        {
            this.Name = "";
            this.Adminlevel = 0;
            this.Geld = 5000;
            this.Payday = 60;
            this.Fraktion = 0;
            this.Rang = 0;
            this.showInv = false;
            this.Einreise = 0;
            this.CharacterData = "";
            this.Aduty = false;
            this.Dog = false;
            this.DiscordID = "";
        }

        public Accounts(string Name, Player player)
        {
            this.Name = Name;
            this._player = player;
            this.Adminlevel = 0;
            this.Geld = 5000;
            this.Payday = 60;
            this.Fraktion = 0;
            this.Rang = 0;
            this.showInv = false;
            this.Einreise = 0;
            this.CharacterData = "";
            this.Aduty = false;
            this.Dog = false;
            this.DiscordID = "";
        }

        public static bool IstSpielerEingeloggt(Player player)
        {
            if (player != null) return player.HasData(Account_Key);
            return false;
        }

        public void RegisterLoginWithDiscord(string name, string id)
        {
            this.Name = name;
            this.DiscordID = id;
            if (!Datenbank.AccountCheck(this.Name))
            {
                Datenbank.NeuenAccountErstellen(this, null);
            }
            if(Datenbank.DiscordIdCheck(this.Name, this.DiscordID) && this.DiscordID != null)
            {
                Login(_player, true);
            }
            else
            {
                _player.SendNotification("~r~Falsches Passwort");
            }
        }

        public void Register(string name, string password)
        {
            Datenbank.NeuenAccountErstellen(this, password);
            Login(_player, true);
        }

        public void Login(Player player, bool firstLogin)
        {
            Datenbank.AccountLaden(this);
            player.SetSharedData("Client:Name", player.Name);
            if (firstLogin)
            {
                player.SendChatMessage("Willkommen auf unserem Server!");
            }
            else
            {
                player.SendChatMessage("Willkommen zurück!");
            }
            player.SetData(Accounts.Account_Key, this);
            NAPI.ClientEvent.TriggerClientEvent(player, "showHUD");
            NAPI.ClientEvent.TriggerClientEvent(player, "updatePB", (int)Accounts.ProgressBars.Healthbar, 0.5);
            NAPI.ClientEvent.TriggerClientEvent(player, "updatePB", (int)Accounts.ProgressBars.Hungerbar, 0.8);
            NAPI.ClientEvent.TriggerClientEvent(player, "updatePB", (int)Accounts.ProgressBars.Thirstbar, 1.0);
            player.SetOwnSharedData("Account:Geld", Geld);
            if (Einreise == 1)
            {
                player.TriggerEvent("showHideMoneyHud");
                Stats stats = new Stats(Name, 5, (int)Geld, Payday);
                player.TriggerEvent("showStats", NAPI.Util.ToJson(stats));
                Events.OnCharacterCreated(player, CharacterData, false);
                if (this.positions[0] != 0.0 && this.positions[1] != 0.0 && this.positions[2] != 0.0)
                {
                    player.Position = new Vector3(this.positions[0], this.positions[1], this.positions[2]);
                    player.Rotation = new Vector3(0.0, 0.0, this.positions[3]);
                }
            }
            else
            {
                player.Position = new Vector3(405.64282, -993.8147, -99.004036);
                player.Rotation = new Vector3(0.0, 0.0, 175.28539);
                if (CharacterData.Length <= 0)
                {
                    player.TriggerEvent("showHideMoneyHud");
                    Stats stats = new Stats(Name, 5, (int)Geld, Payday);
                    player.TriggerEvent("showStats", NAPI.Util.ToJson(stats));
                    player.TriggerEvent("charcreator-show");
                }
                else
                {
                    player.TriggerEvent("showHideMoneyHud");
                    Stats stats = new Stats(Name, 5, (int)Geld, Payday);
                    player.TriggerEvent("showStats", NAPI.Util.ToJson(stats));
                    Events.OnCharacterCreated(player, CharacterData, false);
                }
            }
        }

        public bool IstSpielerAdmin(int Adminlevel)
        {
            return this.Adminlevel >= Adminlevel;
        }

        public bool IsSpielerADuty()
        {
            return this.Aduty;
        }

        public bool IstSpielerInFraktion(int frak)
        {
            return Fraktion == frak;
        }

        public int HoleRangLevel()
        {
            return Rang;
        }

        public string HoleFraktionsName()
        {
            return FraktionsDaten[Fraktion, 0];
        }

        public String HoleRangName()
        {
            return FraktionsDaten[Fraktion, Rang];
        }
    }
}
