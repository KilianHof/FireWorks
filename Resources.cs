using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FireWorks
{

    public class Vehicles
    {

        public string Type { get; set; } // Fahrzeuginfo
        public int HP { get; set; } // Motorleistung
        public int Seats { get; set; } // Sitzplätze

    }
    public class Resources
    {

        public String ID { get; set; } // Inventarnummer
        public String Name { get; set; } // Objektname

    }

    public class TurntableLadder : Vehicles
    {

        public Boolean Saw { get; set; } // Kettensäge y/n

        public int Height { get; set; } // Höhe in M

    }

    public class LFZ : Vehicles
    {

        public Boolean Saw { get; set; } // Kettensäge y/n

        public int FillQuantity { get; set; } // Wasser in L

    }

    public class Ambulance : Vehicles
    {

        public int MaxWeight { get; set; } // Patientengewicht

    }

}
