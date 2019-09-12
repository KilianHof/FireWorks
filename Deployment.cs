using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Fireworks
{


    public class Deployment
    {
        public string Date { get; set; } // Einsatzdatum
        public string Location { get; set; } // Einsatzort
        public object Vehicles { get; set; } // Einsatzfahrzeuge
        public object Resources { get; set; } // Einsatzmittel
        public object Human { get; set; } // Einsatz-Anwesende
        public string Comment { get; set; } // Kommentar
        public int Number { get; set; } // Einsatz-ID
    }
}
