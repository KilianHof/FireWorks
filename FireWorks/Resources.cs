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

        public int ID { get; set; }
        public String Name { get; set; }

    }

    public class Gasmeter
    {

        public int ID { get; set; }
        public String Date { get; set; }

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
            Console.Clear();
            Console.WriteLine("Edit what type of object?");
            Console.WriteLine("(r)esources / (v)ehicles / (u)sers");
            switch (Console.ReadLine())
            {
                case "r":
                    EResources();
                    break;
                case "v":
                    EVehicles();
                    break;
                case "u":
                    EUsers();
                    break;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    Editor();
                    break;
            }
            return;
        }

        public static void EResources()
        {
            Console.Clear();
            Console.WriteLine("Edit what value?");
            Console.WriteLine("(n)ame / (c)reate new resource");
            switch (Console.ReadLine())
            {
                case "n":
                    break;
                case "c":
                    break;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    EResources();
                    break;
            }
            return;
        }
        public static void EVehicles()
        {
            Console.Clear();
            string VehicleType;
            Console.WriteLine("Edit what vehicle type?");
            Console.WriteLine("(g)eneric / (l)FZ / (t)urntableladder / (a)ambulace");
            VehicleType = Console.ReadLine();
            if (VehicleType != "g") if (VehicleType != "l") if (VehicleType != "t") if (VehicleType != "a")
                        {
                            Console.WriteLine("Invalid answer");
                            Console.ReadLine();
                            EVehicles();
                            return;
                        }
            Console.WriteLine("Edit what value?");
            Console.WriteLine("(t)ype / (s)eats / (h)orsepower / (c)reate new resource");
            switch (Console.ReadLine())
            {
                case "t":
                    break;
                case "s":
                    break;
                case "h":
                    break;
                case "c":
                    break;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    EVehicles();
                    break;
            }
            return;
        }
        public static void EUsers()
        {
            Console.Clear();
            Console.WriteLine("Edit what value?");
            Console.WriteLine("(n)ame / (p)IN / (d)isable user / (c)reate user");
            switch (Console.ReadLine())
            {
                case "n":
                    break;
                case "p":
                    break;
                case "d":
                    break;
                case "c":
                    break;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    EUsers();
                    break;
            }
            return;
        }


    }

}
