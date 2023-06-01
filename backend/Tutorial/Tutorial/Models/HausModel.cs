using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Tutorial
{
    [PetaPoco.TableName("house")]
    [PetaPoco.PrimaryKey("id")]
    class HausModel
    {
        [PetaPoco.Column("newid")]
        public int id { get; set; }
        public string ipl { get; set; }
        public float posx { get; set; }
        public float posy { get; set; }
        public float posz { get; set; }
        [PetaPoco.ResultColumn]
        public Vector3 position { get; set; }
        public int preis { get; set; }
        public string besitzer { get; set; }
        public bool abgeschlossen { get; set; }
        public bool status { get; set; }
        [PetaPoco.ResultColumn]
        public TextLabel hausLabel { get; set; }
        [PetaPoco.ResultColumn]
        public Marker hausMarker { get; set; }
        [PetaPoco.ResultColumn]
        public Blip hausBlip { get; set; }
    }
}
