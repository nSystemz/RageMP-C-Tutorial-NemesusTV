using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class Haus : Script
    {
        public static List<Haus> hausListe = new List<Haus>();
        public int id { get; set; }
        public string ipl { get; set; }
        public Vector3 position { get; set; }
        public int preis { get; set; }
        public string besitzer { get; set; }
        public bool abgeschlossen { get; set; }
        public bool status { get; set; }
        public TextLabel hausLabel { get; set; }
        public Marker hausMarker { get; set; }
        public Blip hausBlip { get; set; }

        public static Haus HoleHausMitID(int id)
        {
            Haus house = null;
            foreach(Haus haus in hausListe)
            {
                if(haus.id == id)
                {
                    house = haus;
                    break;
                }
            }
            return house;
        }

        public static Haus holeHausInReichweite(Player player, float distance = 1.5f)
        {
            Haus house = null;
            foreach(Haus haus in hausListe)
            {
                if(haus != null && player.Position.DistanceTo(haus.position) < distance)
                {
                    house = haus;
                }
            }
            return house;
        }

    }
}
