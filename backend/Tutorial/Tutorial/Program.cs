using System;
using dotenv.net;
using GTANetworkAPI;

namespace Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            DotEnv.Fluent()
                .WithExceptions()
                .WithEnvFiles("./serverdata/.env")
                .Load();
        }
    }
}
