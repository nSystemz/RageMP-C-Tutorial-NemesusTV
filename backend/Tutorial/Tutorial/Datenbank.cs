using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Bcrypt;
using dotenv.net.Utilities;
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
            this.Host = EnvReader.GetStringValue("DB_HOST");
            this.Username = EnvReader.GetStringValue("DB_USER");
            this.Password = EnvReader.GetStringValue("DB_PASSWD");
            this.Database = EnvReader.GetStringValue("DB_NAME");
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
                    account.CharacterData = reader.GetString("characterdata");
                }
            }
        }

        public static void AccountSpeichern(Player player)
        {
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            if (account == null) return;
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE accounts SET adminlevel=@adminlevel, geld=@geld, payday=@payday, fraktion=@fraktion, rang=@rang, posx=@posx, posy=@posy, posz=@posz, posa=@posa, einreise=@einreise, characterdata=@characterdata WHERE id=@id";

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
            command.Parameters.AddWithValue("characterdata", account.CharacterData);
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

        public static int ErstelleHaus(HausModel house)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(Datenbank.Connection);
                db.Insert(house);
            }
            catch(Exception e)
            {
                NAPI.Util.ConsoleOutput("[ErstelleHaus]:" + e.ToString());
            }
            return house.id;
        }

        public static void HausSpeichern(HausModel house)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(Datenbank.Connection);
                db.Save(house);
            }
            catch (Exception e)
            {
                NAPI.Util.ConsoleOutput("[HausSpeichern]:" + e.ToString());
            }
        }

        public static List<HausModel> LadeAlleHäuser()
        {
            List<HausModel> hausListeTemp = new List<HausModel>();

            PetaPoco.Database db = new PetaPoco.Database(Datenbank.Connection);
            foreach(HausModel hausModel in db.Fetch<HausModel>("SELECT * FROM house"))
            {
                hausModel.position = new Vector3(hausModel.posx, hausModel.posy, hausModel.posz);
                hausModel.hausLabel = NAPI.TextLabel.CreateTextLabel($"Dieses Haus steht für {hausModel.preis}$ zum Verkauf, benutze /buyhouse um es zu kaufen", new Vector3(hausModel.position.X, hausModel.position.Y, hausModel.position.Z + 0.8), 5.0f, 0.75f, 4, new Color(255, 255, 255));
                if (hausModel.status == false)
                {
                    hausModel.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(hausModel.position.X, hausModel.position.Y, hausModel.position.Z - 1.1), hausModel.position, new Vector3(), 1.0f, new Color(38, 230, 0), false);
                    hausModel.hausBlip = NAPI.Blip.CreateBlip(40, hausModel.position, 1.0f, 2);
                }
                else
                {
                    hausModel.hausMarker = NAPI.Marker.CreateMarker(1, new Vector3(hausModel.position.X, hausModel.position.Y, hausModel.position.Z - 1.1), hausModel.position, new Vector3(), 1.0f, new Color(255, 255, 255), false);
                    hausModel.hausBlip = NAPI.Blip.CreateBlip(40, hausModel.position, 1.0f, 1);
                }
                NAPI.Blip.SetBlipName(hausModel.hausBlip, "Hausnummer: " + hausModel.id);
                NAPI.Blip.SetBlipShortRange(hausModel.hausBlip, true);
                hausListeTemp.Add(hausModel);
            }


            //IPLs laden
            foreach(HausInterior hausinterior in HausInterior.Interior_Liste)
            {
                NAPI.World.RequestIpl(hausinterior.ipl);
            }

            return hausListeTemp;
        }

        //Fraktionen
        public static void LadeAllFraktionen()
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT * from factions";

            int countFaction = 0;

            using(MySqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Accounts.FraktionsDaten[countFaction, 0] = reader.GetString("factionname");
                    for (int i = 1; i<=10; i++)
                    {
                        Accounts.FraktionsDaten[countFaction, i] = reader.GetString("rang" + i);
                    }
                    countFaction ++;
                }
            }
        }
    }
}
