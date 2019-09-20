using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FireWorks.Ressources
{
    public class Resources
    {

        public int ID { get; set; }
        public string Name { get; set; }

    }
}

namespace FireWorks.Ressources
{

    public class Resource
    {
        public static void Editor()
        {
            Console.Clear();
            Console.WriteLine("Edit what type of object?");
            Console.WriteLine("(r)esources / (v)ehicles / (u)sers / (g)asmeters / (h)oses, distributors and jethoses");
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
                case "g":
                    EGasmeters();
                    break;
                case "h":
                    EAmounts();
                    break;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    Editor();
                    break;
            }
            return;
        }

        public static void EAmounts()
        {
            Console.Clear();
            int line;
            Console.WriteLine("Edit which objects amount?");
            Console.WriteLine("(b5)/(b10)/(b20)/(b30)/(c5)/(c10)/(c20)/(c30)/(d5)/(d10)/(d20)/(d30)/(d)istributers/(j)ethoses");
            switch (Console.ReadLine())
            {
                case "b5":
                    line = 1;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "b10":
                    line = 2;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "b20":
                    line = 3;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "b30":
                    line = 4;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "c5":
                    line = 5;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "c10":
                    line = 6;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "c20":
                    line = 7;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "c30":
                    line = 8;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "d5":
                    line = 9;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "d10":
                    line = 10;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "d20":
                    line = 11;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "d30":
                    line = 12;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "d":
                    line = 13;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                case "j":
                    line = 14;
                    Console.WriteLine("Please enter the new amount.");
                    ObjectWriter.LineChanger(Console.ReadLine(), StoragePathClass.StoragePath + "/FireWorks/Storage/Amounts.txt", line);
                    break;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    EAmounts();
                    break;
            }
            return;
        }


        public static void EGasmeters()
        {
            Console.Clear();
            Console.WriteLine("Edit what value?");
            Console.WriteLine("(d)ay of checkup / (c)reate new gasmeter");
            switch (Console.ReadLine())
            {
                case "d":
                    break;
                case "c":
                    EGasmetersC();
                    return;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    EResources();
                    break;
            }


            Console.WriteLine("Enter a Gasmeter-ID.");
            string ID = Console.ReadLine();
            string EResource = "";
            ID = "\"ID\":" + ID; //Absicherung

            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Gasmeters.txt"))
                while (EResource.IndexOf(ID) == -1)
                {


                    EResource = UserText.ReadLine();

                    if (EResource == null)
                    {
                        Console.WriteLine("Invalid ID.");
                        Console.ReadLine();
                        EResources();
                        return;
                    }
                }

            Gasmeter Resource;
            Resource = JsonConvert.DeserializeObject<Gasmeter>(EResource);

            Console.WriteLine("Enter the chuckup-day for Gasmeter " + Resource.ID + ". (as dd format)");

            EResource = JsonConvert.SerializeObject(Resource);
            ObjectWriter.LineChanger(EResource, StoragePathClass.StoragePath + "/FireWorks/Storage/Resources.txt", Resource.ID);

            return;
        }




        public static void EGasmetersC()
        {
            int i = 0;
            string Text = "y e e t";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Resources.txt"))
                while (Text != null)
                {

                    Text = UserText.ReadLine();

                    i += 1;

                }
#pragma warning disable IDE0017 // Initialisierung von Objekten vereinfachen
            Gasmeter ResourceC = new Gasmeter();
#pragma warning restore IDE0017 // Initialisierung von Objekten vereinfachen
            ResourceC.ID = i;
            Console.WriteLine("Assigned resource-ID " + i + ".");

            Console.WriteLine("Enter the date of checkup for the gasmeter. (dd format)");
            ResourceC.Date = Console.ReadLine();

            ObjectWriter.WriteObject(ResourceC, StoragePathClass.StoragePath + "/FireWorks/Storage/Gasmeter.txt");

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
                    EResourcesC();
                    return;
                default:
                    Console.WriteLine("Invalid answer");
                    Console.ReadLine();
                    EResources();
                    break;
            }


            Console.WriteLine("Enter a user-ID.");
            string ID = Console.ReadLine();
            string EResource = "";
            ID = "\"ID\":" + ID; //Absicherung

            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt"))
                while (EResource.IndexOf(ID) == -1)
                {


                    EResource = UserText.ReadLine();

                    if (EResource == null)
                    {
                        Console.WriteLine("Invalid ID.");
                        Console.ReadLine();
                        EResources();
                        return;
                    }
                }

            Resources Resource;
            Resource = JsonConvert.DeserializeObject<Resources>(EResource);

            Console.WriteLine("Enter the new name for " + Resource.Name + ".");

            EResource = JsonConvert.SerializeObject(Resource);
            ObjectWriter.LineChanger(EResource, StoragePathClass.StoragePath + "/FireWorks/Storage/Resource.txt", Resource.ID);

            return;
        }

        public static void EResourcesC()
        {
            int i = 0;
            string Text = "y e e t";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Resources.txt"))
                while (Text != null)
                {

                    Text = UserText.ReadLine();

                    i += 1;

                }
#pragma warning disable IDE0017 // Initialisierung von Objekten vereinfachen
            Resources ResourceC = new Resources();
#pragma warning restore IDE0017 // Initialisierung von Objekten vereinfachen
            ResourceC.ID = i;
            Console.WriteLine("Assigned resource-ID " + i + ".");

            Console.WriteLine("Enter the name for the resource.");
            ResourceC.Name = Console.ReadLine();

            ObjectWriter.WriteObject(ResourceC, StoragePathClass.StoragePath + "/FireWorks/Storage/Resources.txt");

            return;
        }


        public static void EVehicles()
        {
            Console.Clear();
            string VehicleType;
            Console.WriteLine("Edit what vehicle type? Edit or Create?");
            Console.WriteLine("(g)eneric / (l)FZ / (t)urntableladder / (a)mbulace // (e)dit / (c)reate");
            VehicleType = Console.ReadLine();
            if (VehicleType != "ge") if (VehicleType != "le") if (VehicleType != "te") if (VehicleType != "ae") if (VehicleType != "gc") if (VehicleType != "lc") if (VehicleType != "tc") if (VehicleType != "ac")
                                        {
                                            Console.WriteLine("Invalid answer");
                                            Console.ReadLine();
                                            EVehicles();
                                            return;
                                        }
            switch (VehicleType)
            {
                case "ge":
                    EVehiclesG();
                    break;
                case "le":
                    EVehiclesLFZ();
                    break;
                case "te":
                    EVehiclesTTL();
                    break;
                case "ae":
                    EVehiclesA();
                    break;
                case "gc":
                    EVehiclesGCreate();
                    break;
                case "lc":
                    EVehiclesLFZCreate();
                    break;
                case "tc":
                    EVehiclesTTLCreate();
                    break;
                case "ac":
                    EVehiclesACreate();
                    break;
            }

        }

        public static void EVehiclesG()
        {
            Console.WriteLine("Enter the vehicle-ID");
            string ID = Console.ReadLine();
            string EVehicle = "";
            ID = "\"ID\":" + ID; //Absicherung
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Vehicles.txt"))
                while (EVehicle.IndexOf(ID) == -1)
                {


                    EVehicle = UserText.ReadLine();

                    if (EVehicle == null)
                    {
                        Console.WriteLine("Invalid ID.");
                        Console.ReadLine();
                        EUsers();
                        return;
                    }

                }
            string answer;
            int number;
            Vehicles EVehicles = JsonConvert.DeserializeObject<Vehicles>(EVehicle);
            Console.WriteLine("Enter the new values, leave the field empty to keep the current value.");
            Console.WriteLine("Type:");
            answer = Console.ReadLine();
            if (answer != "") EVehicles.Type = answer;
            Console.WriteLine("Seats:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.Seats = number;
            }
            Console.WriteLine("Horsepower:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.HP = number;
            }

            string EVehicleS = JsonConvert.SerializeObject(EVehicles);

            ObjectWriter.LineChanger(EVehicleS, StoragePathClass.StoragePath + "/FireWorks/Storage/Vehicles.txt", EVehicles.ID);
            return;
        }
        public static void EVehiclesTTL()
        {
            Console.WriteLine("Enter the vehicle-ID");
            string ID = Console.ReadLine();
            string EVehicle = "";
            ID = "\"ID\":" + ID; //Absicherung
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/TTLs.txt"))
                while (EVehicle.IndexOf(ID) == -1)
                {


                    EVehicle = UserText.ReadLine();

                    if (EVehicle == null)
                    {
                        Console.WriteLine("Invalid ID.");
                        Console.ReadLine();
                        EUsers();
                        return;
                    }

                }
            string answer;
            int number;
            TurntableLadder EVehicles = JsonConvert.DeserializeObject<TurntableLadder>(EVehicle);
            Console.WriteLine("Enter the new values, leave the field empty to keep the current value.");
            Console.WriteLine("Type:");
            answer = Console.ReadLine();
            if (answer != "") EVehicles.Type = answer;
            Console.WriteLine("Seats:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.Seats = number;
            }
            Console.WriteLine("Horsepower:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.HP = number;
            }
            Console.WriteLine("Height in m:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.Height = number;
            }
            Console.WriteLine("Does the vehicle contain a chainsaw? (y/n)");
            answer = Console.ReadLine();
            if (answer != "")
            {
                if (answer == "n") EVehicles.Saw = false;
                if (answer == "y") EVehicles.Saw = true;
            }

            string EVehicleS = JsonConvert.SerializeObject(EVehicles);

            ObjectWriter.LineChanger(EVehicleS, StoragePathClass.StoragePath + "/FireWorks/Storage/TTLs.txt", EVehicles.ID);

            return;
        }

        public static void EVehiclesTTLCreate()
        {
            string EVehicle = "";
            int i = 0;
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/TTLs.txt"))
                while (EVehicle != null)
                {


                    EVehicle = UserText.ReadLine();

                    i += 1;

                }

            Console.WriteLine("Vehicle-ID: " + i);

            string answer;
            TurntableLadder EVehicles = new TurntableLadder();
            Console.WriteLine("Enter the values.");
            EVehicles.ID = i;
            Console.WriteLine("Type:");
            answer = Console.ReadLine();
            EVehicles.Type = answer;
            Console.WriteLine("Seats:");
            answer = Console.ReadLine();

            int.TryParse(answer, out int number);
            EVehicles.Seats = number;

            Console.WriteLine("Horsepower:");
            answer = Console.ReadLine();

            int.TryParse(answer, out number);
            EVehicles.HP = number;

            Console.WriteLine("Height in m:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.Height = number;
            }
            Console.WriteLine("Does the vehicle contain a chainsaw? (y/n)");
            if (DeploymentListing.GetYesNo()) EVehicles.Saw = true;
            else EVehicles.Saw = false;



            ObjectWriter.WriteObject(EVehicles, StoragePathClass.StoragePath + "/FireWorks/Storage/TTLs.txt");

            return;
        }

        public static void EVehiclesLFZCreate()
        {
            string EVehicle = "";
            int i = 0;
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/LFZs.txt"))
                while (EVehicle != null)
                {


                    EVehicle = UserText.ReadLine();

                    i += 1;

                }

            Console.WriteLine("Vehicle-ID: " + i);

            string answer;
            LFZ EVehicles = new LFZ();
            Console.WriteLine("Enter the values.");
            EVehicles.ID = i;
            Console.WriteLine("Type:");
            answer = Console.ReadLine();
            EVehicles.Type = answer;
            Console.WriteLine("Seats:");
            answer = Console.ReadLine();

            int.TryParse(answer, out int number);
            EVehicles.Seats = number;

            Console.WriteLine("Horsepower:");
            answer = Console.ReadLine();

            int.TryParse(answer, out number);
            EVehicles.HP = number;

            Console.WriteLine("Filquantity in L:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.FillQuantity = number;
            }
            Console.WriteLine("Does the vehicle contain a chainsaw? (y/n)");
            if (DeploymentListing.GetYesNo()) EVehicles.Saw = true;
            else EVehicles.Saw = false;



            ObjectWriter.WriteObject(EVehicles, StoragePathClass.StoragePath + "/FireWorks/Storage/LFZs.txt");

            return;
        }


        public static void EVehiclesACreate()
        {
            string EVehicle = "";
            int i = 0;
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Ambulances.txt"))
                while (EVehicle != null)
                {


                    EVehicle = UserText.ReadLine();

                    i += 1;

                }

            Console.WriteLine("Vehicle-ID: " + i);

            string answer;
            Ambulance EVehicles = new Ambulance();
            Console.WriteLine("Enter the values.");
            EVehicles.ID = i;
            Console.WriteLine("Type:");
            answer = Console.ReadLine();
            EVehicles.Type = answer;
            Console.WriteLine("Seats:");
            answer = Console.ReadLine();

            int.TryParse(answer, out int number);
            EVehicles.Seats = number;

            Console.WriteLine("Horsepower:");
            answer = Console.ReadLine();

            int.TryParse(answer, out number);
            EVehicles.HP = number;

            Console.WriteLine("Maximum patientweight in kg:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.MaxWeight = number;
            }



            ObjectWriter.WriteObject(EVehicles, StoragePathClass.StoragePath + "/FireWorks/Storage/Ambulances.txt");

            return;
        }


        public static void EVehiclesLFZ()
        {
            Console.WriteLine("Enter the vehicle-ID");
            string ID = Console.ReadLine();
            string EVehicle = "";
            ID = "\"ID\":" + ID; //Absicherung
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt"))
                while (EVehicle.IndexOf(ID) == -1)
                {


                    EVehicle = UserText.ReadLine();

                    if (EVehicle == null)
                    {
                        Console.WriteLine("Invalid ID.");
                        Console.ReadLine();
                        EUsers();
                        return;
                    }

                }
            string answer;
            int number;
            LFZ EVehicles = JsonConvert.DeserializeObject<LFZ>(EVehicle);
            Console.WriteLine("Enter the new values, leave the field empty to keep the current value.");
            Console.WriteLine("Type:");
            answer = Console.ReadLine();
            if (answer != "") EVehicles.Type = answer;
            Console.WriteLine("Seats:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.Seats = number;
            }
            Console.WriteLine("Horsepower:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.HP = number;
            }
            Console.WriteLine("Fillquantity in L:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.FillQuantity = number;
            }
            Console.WriteLine("Does the vehicle contain a chainsaw? (y/n)");
            answer = Console.ReadLine();
            if (answer != "")
            {
                if (answer == "n") EVehicles.Saw = false;
                if (answer == "y") EVehicles.Saw = true;
            }

            string EVehicleS = JsonConvert.SerializeObject(EVehicles);

            ObjectWriter.LineChanger(EVehicleS, StoragePathClass.StoragePath + "/FireWorks/Storage/LFZs.txt", EVehicles.ID);
            return;
        }

        public static void EVehiclesGCreate()
        {
            string EVehicle = "";
            int i = 0;
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Vehicles.txt"))
                while (EVehicle != null)
                {


                    EVehicle = UserText.ReadLine();

                    i += 1;

                }

            Console.WriteLine("Vehicle-ID: " + i);

            string answer;
            Vehicles EVehicles = new Vehicles();
            Console.WriteLine("Enter the values.");
            EVehicles.ID = i;
            Console.WriteLine("Type:");
            answer = Console.ReadLine();
            EVehicles.Type = answer;
            Console.WriteLine("Seats:");
            answer = Console.ReadLine();

            int.TryParse(answer, out int number);
            EVehicles.Seats = number;

            Console.WriteLine("Horsepower:");
            answer = Console.ReadLine();

            int.TryParse(answer, out number);
            EVehicles.HP = number;


            ObjectWriter.WriteObject(EVehicles, StoragePathClass.StoragePath + "/FireWorks/Storage/Vehicles.txt");

            return;
        }


        public static void EVehiclesA()
        {
            Console.WriteLine("Enter the vehicle-ID");
            string ID = Console.ReadLine();
            string EVehicle = "";
            ID = "\"ID\":" + ID; //Absicherung
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt"))
                while (EVehicle.IndexOf(ID) == -1)
                {


                    EVehicle = UserText.ReadLine();

                    if (EVehicle == null)
                    {
                        Console.WriteLine("Invalid ID.");
                        Console.ReadLine();
                        EUsers();
                        return;
                    }

                }
            string answer;
            int number;
            Ambulance EVehicles = JsonConvert.DeserializeObject<Ambulance>(EVehicle);
            Console.WriteLine("Enter the new values, leave the field empty to keep the current value.");
            Console.WriteLine("Type:");
            answer = Console.ReadLine();
            if (answer != "") EVehicles.Type = answer;
            Console.WriteLine("Seats:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.Seats = number;
            }
            Console.WriteLine("Horsepower:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.HP = number;
            }
            Console.WriteLine("Maximum patient weight:");
            answer = Console.ReadLine();
            if (answer != "")
            {
                int.TryParse(answer, out number);
                EVehicles.MaxWeight = number;
            }

            string EVehicleS = JsonConvert.SerializeObject(EVehicles);

            ObjectWriter.LineChanger(EVehicleS, StoragePathClass.StoragePath + "/FireWorks/Storage/Ambulances.txt", EVehicles.ID);
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

            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt"))
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
            ObjectWriter.LineChanger(Usertext, StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt", User.ID);
            return;
        }
        public static void EUsersPin(Human User)
        {
            Console.WriteLine("Please enter a new PIN.");
            User.PIN = Console.ReadLine();
            string Usertext = JsonConvert.SerializeObject(User);
            ObjectWriter.LineChanger(Usertext, StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt", User.ID);
            return;
        }
        public static void EUsersDisable(Human User)
        {
            User.Status = 0;
            string Usertext = JsonConvert.SerializeObject(User);
            ObjectWriter.LineChanger(Usertext, StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt", User.ID);
            return;
        }
        public static void EUsersCreate()
        {
            Human User = new Human();
            int i = 0;
            string Text = "y e e t";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt"))
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
            ObjectWriter.WriteObject(User, StoragePathClass.StoragePath + "/FireWorks/Storage/Users.txt");
            return;
        }
    }
}
