using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class Settings
    {
        public static Settings _Settings;
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public static bool LoadServerSettings()
        {
            string directory = "./serverdata/settings.json";
            if(File.Exists(directory))
            {
                string settings = File.ReadAllText(directory);
                _Settings = NAPI.Util.FromJson<Settings>(settings);
                NAPI.Util.ConsoleOutput("[Settings] -> Die Server Settings wurden erfolgreich geladen!");
                return true;
            }
            else
            {
                NAPI.Util.ConsoleOutput("[Settings] -> Die Server Settings konnten nicht geladen werden!");
                NAPI.Task.Run(() =>
                {
                   Environment.Exit(0);
                }, delayTime: 5000);
                return false;
            }
        }
    }
}
