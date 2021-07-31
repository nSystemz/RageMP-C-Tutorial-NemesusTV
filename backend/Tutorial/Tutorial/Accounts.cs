using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class Accounts
    {
        public enum AdminRanks { Spieler, Moderator, Supporter, Administrator };
        public const string Account_Key = "Account_Data";
        public int ID;
        public string Name;
        public int Adminlevel;
        public long Geld;
        public Player _player;

        public Accounts()
        {
            this.Name = "";
            this.Adminlevel = 0;
            this.Geld = 5000;
        }

        public Accounts(string Name, Player player)
        {
            this.Name = Name;
            this._player = player;
            this.Adminlevel = 0;
            this.Geld = 5000;
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
        }

        public bool IstSpielerAdmin(int Adminlevel)
        {
            return this.Adminlevel >= Adminlevel;
        }
    }
}
