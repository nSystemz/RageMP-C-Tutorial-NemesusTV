using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using MySqlConnector;

namespace Tutorial
{
    class Utils
    {
        public static void SendRadiusMessage(string message, int radius, Player player)
        {
            foreach (Player p in NAPI.Pools.GetAllPlayers())
            {
                if (Accounts.IstSpielerEingeloggt(p) && IsInRangeOfPoint(p.Position, player.Position, radius))
                {
                    NAPI.Chat.SendChatMessageToPlayer(p, message);
                }
            }
        }

        public static void SendAdminMessage(string message, int adminlevel, Player player)
        {
            string nachricht = message;
            Accounts account = player.GetData<Accounts>(Accounts.Account_Key);
            foreach(Player p in NAPI.Pools.GetAllPlayers())
            {
                Accounts pacc = p.GetData<Accounts>(Accounts.Account_Key);
                if(Accounts.IstSpielerEingeloggt(p) && pacc.IstSpielerAdmin(adminlevel))
                {
                    NAPI.Chat.SendChatMessageToPlayer(p, "!{#0099ff}[Admin Chat] " + player.Name + "[" + Utils.GetPlayerID(player.Name) + "]: " + nachricht.Remove(0,1));
                }
            }
        }

        public static bool IsInRangeOfPoint(Vector3 playerPos, Vector3 target, float range)
        {
            var direct = new Vector3(target.X - playerPos.X, target.Y - playerPos.Y, target.Z - playerPos.Z);
            var len = direct.X * direct.X + direct.Y * direct.Y + direct.Z * direct.Z;
            return range * range > len;
        }

        public static int GetPlayerID(string name)
        {
            int counter = 0;
            foreach (Player p in NAPI.Pools.GetAllPlayers())
            {
                if (p.Handle.ToString() == name || p.Name.ToLower().Contains(name.ToLower()))
                {
                    return counter;
                }
                counter++;
            }
            return 0;
        }

        public static void sendNotification(Player player, string text, string iconpic)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "showNotification", text, iconpic);
        }

        public static Vehicle GetClosestVehicle(Player player, float distance = 2.75f)
        {
            Vehicle vehicle = null;
            foreach(Vehicle veh in NAPI.Pools.GetAllVehicles())
            {
                Vector3 vehPos = veh.Position;
                float distanceVehicleToPlayer = player.Position.DistanceTo(vehPos);

                if(distanceVehicleToPlayer < distance && player.Dimension == veh.Dimension)
                {
                    distance = distanceVehicleToPlayer;
                    vehicle = veh;
                }
            }
            return vehicle;
        }

        public static Player GetPlayerByNameOrID(string nameOrID)
        {
            foreach(Player p in NAPI.Pools.GetAllPlayers())
            {
                if(p.Handle.ToString() == nameOrID || p.Name.ToLower().Contains(nameOrID.ToLower()))
                {
                    return p;
                }
            }
            return null;
        }

        public static void adminLog(string text, string username)
        {
            HTTP.Post("HIEREIGENENWEBHOOKEINTRAGEN", new System.Collections.Specialized.NameValueCollection()
            {
                {
                    "username",
                    username
                },
                {
                    "content",
                    text
                }
            });
        }

        public static bool IsPlayerWhitelisted(Player player)
        {
            MySqlCommand command = Datenbank.Connection.CreateCommand();
            command.CommandText = "SELECT id FROM whitelist WHERE socialclubid=@socialclubid LIMIT 1";
            command.Parameters.AddWithValue("socialclubid", player.SocialClubId);
            using(MySqlDataReader reader = command.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    return true;
                }
            }
            return false;
        }
        public static Vector3 GetPositionInFrontOfPlayer(Player player, float dist = 1.1f)
        {
            double radians = -player.Heading * Math.PI / 180;
            double nx = player.Position.X + (dist * Math.Sin(radians));
            double ny = player.Position.Y + (dist * Math.Cos(radians));
            return new Vector3(nx, ny, player.Position.Z);
        }


        public static IEnumerable<T[]> Filter<T>(T[,] source, Func<T[], bool> predicate)
        {
            for (int i = 0; i < source.GetLength(0); ++i)
            {
                T[] values = new T[source.GetLength(1)];
                for (int j = 0; j < values.Length; ++j)
                {
                    values[j] = source[i, j];
                }
                if (predicate(values))
                {
                    yield return values;
                }
            }
        }
    }
}
