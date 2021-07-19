using System;
using System.Collections.Generic;
using System.Text;
using Bcrypt;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace Tutorial
{
    class Datenbank
    {
        public static bool DatenbankVerbindung = false;
        public static MySqlConnection Connection;
        public String Host { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Database { get; set; }

        public Datenbank()
        {
            this.Host = Settings._Settings.Host;
            this.Username = Settings._Settings.Username;
            this.Password = Settings._Settings.Password;
            this.Database = Settings._Settings.Database;
        }

        public static void InitConnection()
        {
            Datenbank sql = new Datenbank();
            String SQLConnection = $"SERVER={sql.Host}; DATABASE={sql.Database}; UID={sql.Username}; PASSWORD={sql.Password}";
            NAPI.Util.ConsoleOutput(SQLConnection);
            Connection = new MySqlConnection(SQLConnection);

            try
            {
                Connection.Open();
                DatenbankVerbindung = true;
                NAPI.Util.ConsoleOutput("[MYSQL] -> Verbindung erfolgreich aufgebaut!");
            } catch(Exception e)
            {
                DatenbankVerbindung = false;
                NAPI.Util.ConsoleOutput("[MYSQL] -> Verbindung konnte nicht aufgebaut werden!");
                NAPI.Util.ConsoleOutput("[MYSQL] ->" + e.ToString());
                NAPI.Task.Run(() =>
                {
                    Environment.Exit(0);
                }, delayTime: 5000);
            }
        }

        //Accountsystem

        public static bool IstAccountBereitsVorhanden(string name)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT * FROM accounts WHERE name=@name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    return true;
                }
            }
            return false;
        }

        public static void NeuenAccountErstellen(Accounts account, string password)
        {
            string saltedpw = Bcrypt.BCrypt.HashPassword(password, Bcrypt.BCrypt.GenerateSalt());

            try
            {
                MySqlCommand command = Connection.CreateCommand();
                command.CommandText = "INSERT INTO accounts (password, name, adminlevel, geld) VALUES (@password, @name, @adminlevel, @geld)";

                command.Parameters.AddWithValue("@password", saltedpw);
                command.Parameters.AddWithValue("@name", account.Name);
                command.Parameters.AddWithValue("@adminlevel", account.Adminlevel);
                command.Parameters.AddWithValue("@geld", account.Geld);

                command.ExecuteNonQuery();

                account.ID = (int)command.LastInsertedId;
            }
            catch (Exception e)
            {
                NAPI.Util.ConsoleOutput($"[NeuenAccountErstellen]: " + e.ToString());
            }
        }

        public static void AccountLaden(Accounts account)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT * FROM accounts WHERE name=@name LIMIT 1";

            command.Parameters.AddWithValue("@name", account.Name);

            using(MySqlDataReader reader = command.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    account.ID = reader.GetInt16("id");
                    account.Adminlevel = reader.GetInt16("adminlevel");
                    account.Geld = reader.GetInt32("geld");
                }
            }
        }

        public static void AccountSpeichern(Accounts account)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE accounts SET adminlevel=@adminlevel, geld=@geld WHERE id=@id";

            command.Parameters.AddWithValue("@adminlevel", account.Adminlevel);
            command.Parameters.AddWithValue("@geld", account.Geld);
            command.Parameters.AddWithValue("@id", account.ID);
        }

        public static bool PasswortCheck(string name, string passwordinput)
        {
            string password = "";

            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT password FROM accounts WHERE name=@name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    password = reader.GetString("password");
                }
            }

            if (Bcrypt.BCrypt.CheckPassword(passwordinput, password)) return true;
            return false;

        }

        //Haussystem

        public static int ErstelleHaus(Haus house)
        {
            int houseID = 0;
            try
            {
                MySqlCommand command = Connection.CreateCommand();

                command.CommandText = "INSERT INTO houses (ipl, posX, posY, posZ, preis, abgeschlossen) VALUES (@ipl, @posX, @posY, @posZ, @preis, @abgeschlossen");
                command.Parameters.AddWithValue("@ipl", house.ipl);
                command.Parameters.AddWithValue("@posX", house.position.X);
                command.Parameters.AddWithValue("@posY", house.position.Y);
                command.Parameters.AddWithValue("@posZ", house.position.Z);
                command.Parameters.AddWithValue("@preis", house.preis);
                command.Parameters.AddWithValue("@ageschlossen", house.abgeschlossen);

                command.ExecuteNonQuery();
                houseID = (int)command.LastInsertedId;
            }
            catch(Exception e)
            {
                NAPI.Util.ConsoleOutput("[ErstelleHaus]:" + e.ToString());
            }
            return houseID;
        }
    }
}
