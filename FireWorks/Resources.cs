﻿using System;
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

        public int Amount { get; set; } // Menge
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

    public class Resource
    {
        public static void Editor()
        {

            Console.WriteLine("Edit what?");
            Console.WriteLine("(r)esources / (v)ehicles / (u)sers");
            string answer = Console.ReadLine();
            switch (answer)
            {
                case "r":
                    break;
                case "v":
                    break;
                case "u":
                    break;
            }
            return;
        }
    }

}
