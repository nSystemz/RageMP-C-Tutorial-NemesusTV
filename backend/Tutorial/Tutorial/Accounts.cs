﻿using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

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
            player.SetOwnSharedData("Account:Geld", Geld);
            if (Einreise == 1)
            {
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
            }
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
            return FraktionsDaten[Fraktion, 0];
        }

        public String HoleRangName()
        {
            return FraktionsDaten[Fraktion, Rang];
        }
    }
}
