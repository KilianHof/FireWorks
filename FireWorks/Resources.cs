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

            Console.WriteLine("Enter a user-ID. Enter \"0\" to create a new user.");
            string ID = Console.ReadLine();
            string EUser = "";
            if (ID == "0")
            {
                EUsersCreate();
                return;
            }
            ID = "\"ID\":" + ID; //Absicherung
            
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Users.txt"))
                while (EUser.IndexOf(ID) == -1)
                {


                    EUser = UserText.ReadLine();

                    if (EUser == null)
                    {
                        Console.WriteLine("Invalid ID.");
                        Console.ReadLine();
                        EUsers();
                        return;
                    }
                }

            Human User;
            User = JsonConvert.DeserializeObject<Human>(EUser);


            Console.Clear();
            Console.WriteLine("Edit what value?");
            Console.WriteLine("(n)ame / (p)IN / (d)isable user");
            switch (Console.ReadLine())
            {
                case "n":
                    EUsersName(User);
                    break;
                case "p":
                    EUsersPin(User);
                    break;
                case "d":
                    EUsersDisable(User);
                    break;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    EUsers();
                    break;
            }
            return;
        }

        public static void EUsersName(Human User)
        {
            Console.WriteLine("Please enter a new first name.");
            User.FName = Console.ReadLine();
            Console.WriteLine("Please enter a new last name.");
            User.LName = Console.ReadLine();
            string Usertext = JsonConvert.SerializeObject(User);
            ObjectWriter.LineChanger(Usertext, "C:/Users/khof/Desktop/Users.txt", User.ID);
            return;
        }
        public static void EUsersPin(Human User)
        {
            Console.WriteLine("Please enter a new PIN.");
            User.PIN = Console.ReadLine();
            string Usertext = JsonConvert.SerializeObject(User);
            ObjectWriter.LineChanger(Usertext, "C:/Users/khof/Desktop/Users.txt", User.ID);
            return;
        }
        public static void EUsersDisable(Human User)
        {
            User.Status = 0;
            string Usertext = JsonConvert.SerializeObject(User);
            ObjectWriter.LineChanger(Usertext, "C:/Users/khof/Desktop/Users.txt", User.ID);
            return;
        }
        public static void EUsersCreate()
        {
            Human User = new Human();
            int i = 0;
            string Text = "y e e t";
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Users.txt"))
                while (Text != null)
                {
                    Text = UserText.ReadLine();

                    i += 1;
                }
            User.ID = i;
            Console.WriteLine("Assigned user-ID " + i + ".");
            Console.WriteLine("Please enter a first name.");
            User.FName = Console.ReadLine();
            Console.WriteLine("Please enter a last name.");
            User.LName = Console.ReadLine();
            Console.WriteLine("Please enter a PIN.");
            User.PIN = Console.ReadLine();
            Console.WriteLine("Please enter status number. (0 locked / 1 user / 2 admin)");
            string answer = Console.ReadLine();
            int.TryParse(answer, out int status);
            User.Status = status;
            ObjectWriter.WriteObject(User, "C:/Users/khof/Desktop/Users.txt");
            return;
        }
    }
}
