using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FireWorks
{


    public class Deployment
    {
        public string Date { get; set; } // Deploymentdatum
        public string Location { get; set; } // DeploymentLocation
        public Vehicles[] Vehicles { get; set; }
        public int VehiclesNumber { get; set; }
        public LFZ[] LFZ { get; set; }
        public int LFZNumber { get; set; }
        public TurntableLadder[] TurntableLadder { get; set; } // Deploymentfahrzeuge
        public int TurntableLadderNumber { get; set; } // Deploymentfahrzeuge
        public Ambulance[] Ambulance { get; set; } // Deploymentfahrzeuge
        public int AmbulanceNumber { get; set; } // Deploymentfahrzeuge
        public Resources[] Resources { get; set; }
        public int ResourcesNumber { get; set; } // Deploymentmittel
        public Human[] Human { get; set; }
        public int ALength { get; set; }
        public int BLength { get; set; }
        public int CLength { get; set; }
        public int DLength { get; set; }
        public int HumanNumber { get; set; } // Deployment-Anwesende
        public string Comment { get; set; } // Kommentar
        public int Number { get; set; } // Deployment-ID
    }

    public class DeploymentListing
    {



        public static void DeploymentDetail()
        {



            String Deploytext = "";
            Deployment DetailDeployment;
            Console.Write("Deployment ID:");
            String IDenter = Console.ReadLine();
            IDenter = "\"Number\":" + IDenter + "}"; //Absicherung
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt"))
                while (Deploytext.IndexOf(IDenter) == -1)   //zählt sucht nach dem ersten Einsatz mit der ID
                {


                    Deploytext = UserText.ReadLine();

                    if (Deploytext == null) //wenn keine vorhanden sind
                    {
                        Console.WriteLine("Invalid ID");
                        Console.ReadLine();
                        return;
                    }




                }
            DetailDeployment = JsonConvert.DeserializeObject<Deployment>(Deploytext);   //Macht den Einsatz zum Objekt / Im folgenden wird er angezeigt
            Console.Clear();
            Console.WriteLine("Deployment-ID:      " + DetailDeployment.Number);
            Console.WriteLine("Deployment-Location:" + DetailDeployment.Location);
            Console.Write("Vehicles:           ");

            while (DetailDeployment.TurntableLadderNumber > 0)
            {
                DetailDeployment.TurntableLadderNumber -= 1;
                if (DetailDeployment.TurntableLadder[DetailDeployment.TurntableLadderNumber].Saw) Console.Write("Chainsaw-carrying, ");
                Console.Write(DetailDeployment.TurntableLadder[DetailDeployment.TurntableLadderNumber].Height + "m tall, ");
                Console.Write(DetailDeployment.TurntableLadder[DetailDeployment.TurntableLadderNumber].Seats + " seater ");
                Console.Write(DetailDeployment.TurntableLadder[DetailDeployment.TurntableLadderNumber].Type + " (" + DetailDeployment.TurntableLadder[DetailDeployment.TurntableLadderNumber].HP + "hp), ");
            }

            while (DetailDeployment.LFZNumber > 0)
            {
                DetailDeployment.LFZNumber -= 1;
                if (DetailDeployment.LFZ[DetailDeployment.LFZNumber].Saw) Console.Write("Chainsaw-carrying, ");
                Console.Write(DetailDeployment.LFZ[DetailDeployment.LFZNumber].FillQuantity + "l water containing ");
                Console.Write(DetailDeployment.LFZ[DetailDeployment.LFZNumber].Seats + " seater ");
                Console.Write(DetailDeployment.LFZ[DetailDeployment.LFZNumber].Type + " (" + DetailDeployment.LFZ[DetailDeployment.LFZNumber].HP + "hp), ");
            }





            while (DetailDeployment.VehiclesNumber > 0)
            {
                DetailDeployment.VehiclesNumber -= 1;
                Console.Write(DetailDeployment.Vehicles[DetailDeployment.VehiclesNumber].Seats + " seater ");
                Console.Write(DetailDeployment.Vehicles[DetailDeployment.VehiclesNumber].Type + " (" + DetailDeployment.Vehicles[DetailDeployment.VehiclesNumber].HP + "hp), ");
            }

            Console.WriteLine();

            Console.WriteLine("Resources:          A-Hoselength: " + DetailDeployment.ALength + ", B-Hoselength: " + DetailDeployment.BLength + ", C-Hoselength: " + DetailDeployment.CLength + ", D-Hoselength: " + DetailDeployment.DLength);
            while (DetailDeployment.ResourcesNumber > 0)
            {
                DetailDeployment.ResourcesNumber -= 1;
                Console.Write(DetailDeployment.Resources[DetailDeployment.ResourcesNumber].Name + ", ");
            }
            Console.WriteLine();
            Console.WriteLine("Deployed units:     ");
            while (DetailDeployment.HumanNumber > 0)
            {
                DetailDeployment.HumanNumber -= 1;
                Console.Write(DetailDeployment.Human[DetailDeployment.HumanNumber].FName + " ");
                Console.Write(DetailDeployment.Human[DetailDeployment.HumanNumber].LName + ", ");
            }
            Console.WriteLine();
            Console.WriteLine("Comment:            " + DetailDeployment.Comment);
            Console.ReadLine();
            return;
        }

        public static void DeploymentList()
        {
            String Gasmeters = "";
            int i = 0;
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Gasmeters.txt"))

                while (Gasmeters != null)
                {
                    Gasmeters = UserText.ReadLine();

                    i += 1;
                }
            int iMax = i - 1;
            i -= 1;
            Gasmeter[] Gasmeterlist = new Gasmeter[i];
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Gasmeters.txt"))
                while (i > 0)
                {


                    Gasmeterlist[i - 1] = JsonConvert.DeserializeObject<Gasmeter>(UserText.ReadLine());

                    i -= 1;

                }
            DateTime date = DateTime.Now;
            string datestring = date.ToString("dd");
            while (i != iMax)
            {
                if (Gasmeterlist[i].Date == datestring) Console.WriteLine("Gasmeter with ID " + Gasmeterlist[i].ID + " has to be checked today.");

                i += 1;
            }
            Console.WriteLine("How many deployments are to be shown?");
            int.TryParse(Console.ReadLine(), out int number);
            number += 1;
            String SDeployments = "";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt"))

                while (SDeployments != null)
                {

                    SDeployments = UserText.ReadLine();

                    i += 1;

                }

            Deployment[] Deployments = new Deployment[i];
            i = 0;
            SDeployments = "";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt"))

                while (SDeployments != null)
                {

                    SDeployments = UserText.ReadLine();
                    if (SDeployments != null)
                    {
                        Deployments[i] = JsonConvert.DeserializeObject<Deployment>(SDeployments);
                    }
                    i += 1;

                }

            i -= 1;
            iMax = i;
            number -= 2;
            while (number > -1)
            {
                if (number > iMax)
                {
                    Console.WriteLine("Only " + iMax + " entries exist.");
                    DeploymentList();
                    break;
                }
                Console.WriteLine("Date: " + Deployments[number].Date + "    Location: " + Deployments[number].Location + "    ID:" + Deployments[number].Number);
                number -= 1;
            }

            Console.Write("Go into details for a deployment?");
            if (GetYesNo())
            {
                DeploymentDetail();
                return;
            }

            return;

        }

        public static void DeploymentSearch()
        {
            Console.WriteLine("Search by (v)ehicle or by (n)ame?");
            string answer = Console.ReadLine();
            if (answer == "v") DeploymentSearchV();
            if (answer == "n") DeploymentSearchN();
            else DeploymentSearch();
            return;
        }

        public static void DeploymentSearchV()
        {

            Console.Write("Vehicle name:");
            String IDenter = Console.ReadLine();
            int i = 0;

            String SDeployments = "";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt"))


                while (SDeployments != null)
                {

                    SDeployments = UserText.ReadLine();
                    if (SDeployments == null) break;
                    if (SDeployments.IndexOf(IDenter) != -1) i += 1;

                }
            Deployment[] Deployments = new Deployment[i];
            i = 0;
            SDeployments = "";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt"))

                while (SDeployments != null)
                {

                    SDeployments = UserText.ReadLine();
                    if (SDeployments != null)
                    {
                        if (SDeployments.IndexOf(IDenter) != -1)
                        {
                            Deployments[i] = JsonConvert.DeserializeObject<Deployment>(SDeployments);
                            i += 1;
                        }
                    }

                }

            i -= 1;
            int iMax = i + 1;

            if (iMax == 1) Console.WriteLine(iMax + " entry exists.");
            else Console.WriteLine(iMax + " entries exist.");

            Console.WriteLine("How many deployments are to be shown at maximum?");

            int.TryParse(Console.ReadLine(), out int number);
            int runs = 0;
            while (number > 0)
            {
                if (runs == iMax) break;

                number -= 1;
                Console.WriteLine("Date: " + Deployments[runs].Date + "    Location: " + Deployments[runs].Location + "    ID:" + Deployments[runs].Number);
                runs += 1;
            }

            Console.Write("Go into details for a deployment?");
            if (GetYesNo())
            {
                DeploymentDetail();
                return;
            }

            return;

        }

        public static bool GetYesNo() //Funktion zum y/n Abfragen.
        {
            Console.WriteLine("(y/n)");
            string answer;
            answer = Console.ReadLine();
            if (answer.Equals("y"))
            {
                return true;
            }
            if (answer.Equals("n"))
            {
                return false;
            }
            Console.WriteLine("Please enter y or n");
            return GetYesNo();
        }
        public static void DeploymentSearchN()
        {

            Console.Write("First name:");
            String IDenterF = Console.ReadLine();
            Console.Write("Last name:");
            String IDenterL = Console.ReadLine();
            int i = 0;

            String SDeployments = "";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt"))

                while (SDeployments != null)
                {

                    SDeployments = UserText.ReadLine();
                    if (SDeployments == null) break;
                    if (SDeployments.IndexOf(IDenterF) != -1)
                    {
                        if (SDeployments.IndexOf(IDenterL) != -1) i += 1;
                    }
                }
            Deployment[] Deployments = new Deployment[i];
            SDeployments = "";
            i = 0;
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt"))
                while (SDeployments != null)
                {
                    SDeployments = UserText.ReadLine();
                    if (SDeployments != null)
                    {
                        if (SDeployments.IndexOf(IDenterF) != -1)
                        {
                            if (SDeployments.IndexOf(IDenterL) != -1)
                            {
                                Deployments[i] = JsonConvert.DeserializeObject<Deployment>(SDeployments);
                                i += 1;
                            }
                        }
                    }
                }

            i -= 1;
            int iMax = i + 1;

            if (iMax == 1) Console.WriteLine(iMax + " entry exists.");
            else Console.WriteLine(iMax + " entries exist.");

            Console.WriteLine("How many deployments are to be shown at maximum?");

            int.TryParse(Console.ReadLine(), out int number);
            int runs = 0;
            while (number > 0)
            {
                if (runs == iMax) break;

                number -= 1;
                Console.WriteLine("Date: " + Deployments[runs].Date + "    Location: " + Deployments[runs].Location + "    ID:" + Deployments[runs].Number);
                runs += 1;
            }

            Console.Write("Go into details for a deployment?");
            if (GetYesNo())
            {
                DeploymentDetail();
                return;
            }

            return;

        }


        public static void CreateDeployment()
        {
            Console.Clear();
            Console.WriteLine("Create a new deployment entry?");
            if (GetYesNo() == false) return;
            Deployment NewDeployment = new Deployment();
            int i = 0;
            String readtext = "yeet";
            using (StreamReader UserText = new StreamReader(@StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt"))

                while (readtext != null)
                {
                    readtext = UserText.ReadLine();
                    i += 1;
                }

            Console.WriteLine("Found " + (i - 1) + " deployment entries.");
            Console.WriteLine("Assign deployment-ID: " + i + "?");
            int Number;
            if (GetYesNo() == false)
            {
                Console.WriteLine("Please enter deployment-ID:");

                int.TryParse(Console.ReadLine(), out Number);
                NewDeployment.Number = Number;
            }
            else NewDeployment.Number = i;
            Console.Clear();
            Console.WriteLine("Please enter the date of the deployment:");
            NewDeployment.Date = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Please enter the location of the deployment:");
            NewDeployment.Location = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Please enter the number of ambulances used:");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.AmbulanceNumber = Number;
            Ambulance[] ambulances = new Ambulance[Number];
            int IntNumber;
            while (Number > 0)
            {
                Ambulance tmp = new Ambulance();
                ambulances[Number - 1] = tmp;
                Console.Clear();
                Console.WriteLine(Number + " ambulances left.");
                Console.WriteLine("Enter the type of ambulance:");
                ambulances[Number - 1].Type = Console.ReadLine();
                Console.WriteLine("Enter the amount of horsepower:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                ambulances[Number - 1].HP = IntNumber;
                Console.WriteLine("Enter the amount of seats:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                ambulances[Number - 1].Seats = IntNumber;
                Console.WriteLine("Enter the maximum weight for patients:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                ambulances[Number - 1].MaxWeight = IntNumber;
                Number -= 1;
            }
            NewDeployment.Ambulance = ambulances;

            Console.Clear();
            Console.WriteLine("Please enter the number of turntableladders used:");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.TurntableLadderNumber = Number;
            TurntableLadder[] turntableLadders = new TurntableLadder[Number];
            while (Number > 0)
            {
                Console.Clear();
                TurntableLadder tmp = new TurntableLadder();
                turntableLadders[Number - 1] = tmp;
                Console.WriteLine(Number + " turntableladders left.");
                Console.WriteLine("Enter the type of turntableladder:");
                turntableLadders[Number - 1].Type = Console.ReadLine();
                Console.WriteLine("Enter the amount of horsepower:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                turntableLadders[Number - 1].HP = IntNumber;
                Console.WriteLine("Enter the amount of seats:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                turntableLadders[Number - 1].Seats = IntNumber;
                Console.WriteLine("Enter the maximum height for rescue:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                turntableLadders[Number - 1].Height = IntNumber;
                Console.WriteLine("Was a saw present?");
                if (GetYesNo()) turntableLadders[Number - 1].Saw = true;
                else turntableLadders[Number - 1].Saw = false;
                Number -= 1;
            }
            NewDeployment.TurntableLadder = turntableLadders;

            Console.Clear();
            Console.WriteLine("Please enter the number of LFZs used:");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.LFZNumber = Number;
            LFZ[] LFZs = new LFZ[Number];
            while (Number > 0)
            {
                LFZ tmp = new LFZ();
                LFZs[Number - 1] = tmp;
                Console.Clear();
                Console.WriteLine(Number + " LFZs left.");
                Console.WriteLine("Enter the type of LFZ:");
                LFZs[Number - 1].Type = Console.ReadLine();
                Console.WriteLine("Enter the amount of horsepower:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                LFZs[Number - 1].HP = IntNumber;
                Console.WriteLine("Enter the amount of seats:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                LFZs[Number - 1].Seats = IntNumber;
                Console.WriteLine("Enter the maximum water amount:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                LFZs[Number - 1].FillQuantity = IntNumber;
                Console.WriteLine("Was a saw present?");
                if (GetYesNo()) LFZs[Number - 1].Saw = true;
                else LFZs[Number - 1].Saw = false;
                Number -= 1;
            }
            NewDeployment.LFZ = LFZs;

            Console.Clear();
            Console.WriteLine("Please enter the number of other vehicles used:");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.VehiclesNumber = Number;
            Vehicles[] Vehicles = new Vehicles[Number];
            while (Number > 0)
            {
                Vehicles tmp = new Vehicles();
                Vehicles[Number - 1] = tmp;
                Console.Clear();
                Console.WriteLine(Number + " vehicles left.");
                Console.WriteLine("Enter the type of vehicle:");
                Vehicles[Number - 1].Type = Console.ReadLine();
                Console.WriteLine("Enter the amount of horsepower:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                Vehicles[Number - 1].HP = IntNumber;
                Console.WriteLine("Enter the amount of seats:");
                int.TryParse(Console.ReadLine(), out IntNumber);
                Vehicles[Number - 1].Seats = IntNumber;
                Number -= 1;
            }

            NewDeployment.Vehicles = Vehicles;

            Console.Clear();
            Console.WriteLine("Please enter the number of different resources used:");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.ResourcesNumber = Number;
            Resources[] resources = new Resources[Number];
            while (Number > 0)
            {
                Resources tmp = new Resources();
                resources[Number - 1] = tmp;
                Console.Clear();
                Console.WriteLine(Number + " resources left.");
                Console.WriteLine("Enter the name of the resource:");
                resources[Number - 1].Name = Console.ReadLine();
                Number -= 1;
            }
            NewDeployment.Resources = resources;

            Console.Clear();
            Console.WriteLine("Please enter the number of units deployed:");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.HumanNumber = Number;
            Human[] humans = new Human[Number];
            while (Number > 0)
            {
                Human tmp = new Human();
                humans[Number - 1] = tmp;
                Console.Clear();
                Console.WriteLine(Number + " units left.");
                Console.WriteLine("Enter the first name of the unit:");
                humans[Number - 1].FName = Console.ReadLine();
                Console.WriteLine("Enter the last name of the resource:");
                humans[Number - 1].LName = Console.ReadLine();
                Number -= 1;
            }
            NewDeployment.Human = humans;

            Console.Clear();
            Console.WriteLine("How many meters of A-Hoses were used?");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.ALength = Number;
            Console.WriteLine("How many meters of B-Hoses were used?");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.BLength = Number;
            Console.WriteLine("How many meters of C-Hoses were used?");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.CLength = Number;
            Console.WriteLine("How many meters of D-Hoses were used?");
            int.TryParse(Console.ReadLine(), out Number);
            NewDeployment.DLength = Number;

            Console.WriteLine("Enter any comments you might have for the deployment:");
            NewDeployment.Comment = Console.ReadLine();

            ObjectWriter.WriteObject(NewDeployment, StoragePathClass.StoragePath+"/FireWorks/Storage/Deployments.txt");
        }
    }
}

