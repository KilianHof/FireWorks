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

        public string Type { get; set; }
        public int HP { get; set; }
        public int Seats { get; set; }

    }
    public class Resources
    {

        public int Amount { get; set; }
        public String Name { get; set; }

    }

    public class TurntableLadder : Vehicles
    {

        public Boolean Saw { get; set; }
        public int Height { get; set; }

    }

    public class LFZ : Vehicles
    {

        public Boolean Saw { get; set; }
        public int FillQuantity { get; set; }

    }

    public class Ambulance : Vehicles
    {

        public int MaxWeight { get; set; }

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
