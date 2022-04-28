using System;
using System.Collections.Generic;
using System.Net;
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
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.Expect100Continue = false;

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
                command.CommandText = "INSERT INTO accounts (password, name, adminlevel, geld, payday, einreise) VALUES (@password, @name, @adminlevel, @geld, @payday, @einreise)";

                command.Parameters.AddWithValue("@password", saltedpw);
                command.Parameters.AddWithValue("@name", account.Name);
                command.Parameters.AddWithValue("@adminlevel", account.Adminlevel);
                command.Parameters.AddWithValue("@geld", account.Geld);
                command.Parameters.AddWithValue("@payday", account.Payday);
                command.Parameters.AddWithValue("@einreise", account.Einreise);

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
                    account.Payday = reader.GetInt16("payday");
                    account.Fraktion = reader.GetInt16("fraktion");
                    account.Rang = reader.GetInt16("rang");
                    account.positions[0] = reader.GetFloat("posx");
                    account.positions[1] = reader.GetFloat("posy");
                    account.positions[2] = reader.GetFloat("posz");
                    account.positions[3] = reader.GetFloat("posa");
                    account.Einreise = reader.GetInt16("einreise");
                }
            }
        }

        public static void AccountSpeichern(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (account == null) return;
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE accounts SET adminlevel=@adminlevel, geld=@geld, payday=@payday, fraktion=@fraktion, rang=@rang, posx=@posx, posy=@posy, posz=@posz, posa=@posa, einreise=@einreise WHERE id=@id";

            command.Parameters.AddWithValue("@adminlevel", account.Adminlevel);
            command.Parameters.AddWithValue("@geld", account.Geld);
            command.Parameters.AddWithValue("@payday", account.Payday);
            command.Parameters.AddWithValue("@fraktion", account.Fraktion);
            command.Parameters.AddWithValue("@rang", account.Rang);
            command.Parameters.AddWithValue("@posx", player.Position.X);
            command.Parameters.AddWithValue("@posy", player.Position.Y);
            command.Parameters.AddWithValue("@posz", player.Position.Z);
            command.Parameters.AddWithValue("@posa", player.Rotation.Z);
            command.Parameters.AddWithValue("@einreise", account.Einreise);
            command.Parameters.AddWithValue("@id", account.ID);

            command.ExecuteNonQuery();

            NAPI.Util.ConsoleOutput($"[AccountSpeichern]: Account mit der ID: " + account.ID + " wurde gespeichert!");
        }

        public static bool PasswortCheck(string name, string passwordinput)
        {
            string password = null;

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

        public static bool AccountCheck(string name)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT id FROM accounts WHERE name=@name LIMIT 1";
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

        //Haussystem

        public static int ErstelleHaus(Haus house)
        {
            int houseID = 0;
            try
            {
                MySqlCommand command = Connection.CreateCommand();

                command.CommandText = "INSERT INTO house (ipl, posX, posY, posZ, preis, abgeschlossen, besitzer) VALUES (@ipl, @posX, @posY, @posZ, @preis, @abgeschlossen, @besitzer)";
                command.Parameters.AddWithValue("@ipl", house.ipl);
                command.Parameters.AddWithValue("@posX", house.position.X);
                command.Parameters.AddWithValue("@posY", house.position.Y);
                command.Parameters.AddWithValue("@posZ", house.position.Z);
                command.Parameters.AddWithValue("@preis", house.preis);
                command.Parameters.AddWithValue("@abgeschlossen", house.abgeschlossen);
                command.Parameters.AddWithValue("@besitzer", house.besitzer);

                command.ExecuteNonQuery();
                houseID = (int)command.LastInsertedId;
            }
            catch(Exception e)
            {
                NAPI.Util.ConsoleOutput("[ErstelleHaus]:" + e.ToString());
            }
            return houseID;
        }

        public static void HausSpeichern(Haus house)
        {
            try
            {
                MySqlCommand command = Connection.CreateCommand();

                command.CommandText = "UPDATE house SET ipl = @ipl, posX = @posX, posY = @posY, preis = @preis, besitzer = @besitzer, status = @status, abgeschlossen = @abgeschlossen";
                command.CommandText += " WHERE id = @id";

                command.Parameters.AddWithValue("@ipl", house.ipl);
                command.Parameters.AddWithValue("@posX", house.position.X);
                command.Parameters.AddWithValue("@posY", house.position.Y);
                command.Parameters.AddWithValue("@posZ", house.position.Z);
                command.Parameters.AddWithValue("@preis", house.preis);
                command.Parameters.AddWithValue("@besitzer", house.besitzer);
                command.Parameters.AddWithValue("@status", house.status);
                command.Parameters.AddWithValue("@abgeschlossen", house.abgeschlossen);
                command.Parameters.AddWithValue("@id", house.id);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                NAPI.Util.ConsoleOutput("[HausSpeichern]:" + e.ToString());
            }
        }

        public static List<Haus> LadeAlleHäuser()
        {
            List<Haus> hausListeTemp = new List<Haus>();

            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT * FROM house";

            using(MySqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Haus house = new Haus();

                    float posX = reader.GetFloat("posX");
                    float posY = reader.GetFloat("posY");
                    float posZ = reader.GetFloat("posZ");

                    house.id = reader.GetInt32("id");
                    house.ipl = reader.GetString("ipl");
                    house.position = new Vector3(posX, posY, posZ);
                    house.preis = reader.GetInt16("preis");
                    house.besitzer = reader.GetString("besitzer");
                    house.status = reader.GetBoolean("status");
                    house.abgeschlossen = reader.GetBoolean("abgeschlossen");

                    house.hausLabel = NAPI.TextLabel.CreateTextLabel($"Dieses Haus steht für {house.preis}$ zum Verkauf, benutze /buyhouse um es zu kaufen", new Vector3(house.position.X, house.position.Y, house.position.Z + 0.8), 5.0f, 0.75f, 4, new Color(255, 255, 255));
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

                    hausListeTemp.Add(house);
                }
            }

            //IPLs laden
            foreach(HausInterior hausinterior in HausInterior.Interior_Liste)
            {
                NAPI.World.RequestIpl(hausinterior.ipl);
            }

            return hausListeTemp;
        }
    }
}
