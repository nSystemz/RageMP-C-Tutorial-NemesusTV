using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    class HausInterior : Script
    {
        public string ipl { get; set; }
        public Vector3 position { get; set; }

        public HausInterior ()
        {
        }

        public HausInterior(string ipl, Vector3 position)
        {
            this.ipl = ipl;
            this.position = position;
        }

        public static List<HausInterior> Interior_Liste = new List<HausInterior>
        {
            new HausInterior("apa_v_mp_h_01_a", new Vector3(-786.8663, 315.7642, 217.6385))
        };

        public static Vector3 GetHausAusgang(string ipl)
        {
            Vector3 position = new Vector3();
            foreach(HausInterior iplModel in Interior_Liste)
            {
                if(iplModel.ipl == ipl)
                {
                    position = iplModel.position;
                    break;
                }
            }
            return position;
        }
    }
}
