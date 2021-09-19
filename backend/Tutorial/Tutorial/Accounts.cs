using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class Accounts
    {
        public static String[] Fraktionen = new String[3] { "Keine Fraktion", "Los Santos Police Department", "Newsfirma" };
        public static String[] RangNamen = new String[7] { "Kein Rang", "Praktikant", "Auszubildener", "Angestellter", "Abteilungsleiter", "Ausbilder", "Chef" };

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

        public Accounts()
        {
            this.Name = "";
            this.Adminlevel = 0;
            this.Geld = 5000;
            this.Payday = 60;
            this.Fraktion = 0;
            this.Rang = 0;
            this.showInv = false;
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
        }

        public static bool IstSpielerEingeloggt(Player player)
        {
            if (player != null) return player.HasData(Account_Key);
            return false;
        }

        public void Register(string name, string password)
        {
            Datenbank.NeuenAccountErstellen(this, password);
            Login(_player, true);
        }

        public void Login(Player player, bool firstLogin)
        {
            Datenbank.AccountLaden(this);
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
        }

        public bool IstSpielerAdmin(int Adminlevel)
        {
            return this.Adminlevel >= Adminlevel;
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
            return Fraktionen[Fraktion];
        }

        public String HoleRangName()
        {
            return RangNamen[Rang];
        }
    }
}
