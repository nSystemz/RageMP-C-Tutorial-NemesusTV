using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial.Controllers
{
    class HausController : Script
    {
        public static List<HausModel> hausListe = new List<HausModel>();

        public static HausModel HoleHausMitID(int id)
        {
            HausModel house = null;
            foreach (HausModel haus in hausListe)
            {
                if (haus.id == id)
                {
                    house = haus;
                    break;
                }
            }
            return house;
        }

        public static HausModel holeHausInReichweite(Player player, float distance = 1.5f)
        {
            HausModel house = null;
            foreach (HausModel haus in hausListe)
            {
                if (haus != null && player.Position.DistanceTo(haus.position) < distance)
                {
                    house = haus;
                }
            }
            return house;
        }

        public static bool HatSpielerSchluessel(Player Player, HausModel house)
        {
            return Player.Name == house.besitzer;
        }

        public static void VerarbeiteHausListAlsJson()
        {
            String json = NAPI.Util.ToJson(hausListe);
           //NAPI.Util.ConsoleOutput(json);
        }
    }
}
