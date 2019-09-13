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
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Deployments.txt"))
                while (Deploytext.IndexOf(IDenter) == -1)
                {


                    Deploytext = UserText.ReadLine();

                    if (Deploytext == null)
                    {
                        Console.WriteLine("Invalid ID");
                        Console.ReadLine();
                        return;
                    }




                }
            DetailDeployment = JsonConvert.DeserializeObject<Deployment>(Deploytext);
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

            Console.WriteLine();
            while (DetailDeployment.LFZNumber > 0)
            {
                DetailDeployment.LFZNumber -= 1;
                if (DetailDeployment.LFZ[DetailDeployment.LFZNumber].Saw) Console.Write("Chainsaw-carrying, ");
                Console.Write(DetailDeployment.LFZ[DetailDeployment.LFZNumber].FillQuantity + "l water containing ");
                Console.Write(DetailDeployment.LFZ[DetailDeployment.LFZNumber].Seats + " seater ");
                Console.Write(DetailDeployment.LFZ[DetailDeployment.LFZNumber].Type + " (" + DetailDeployment.LFZ[DetailDeployment.LFZNumber].HP + "hp), ");
            }





            Console.WriteLine();
            while (DetailDeployment.VehiclesNumber > 0)
            {
                DetailDeployment.VehiclesNumber -= 1;
                Console.Write(DetailDeployment.Vehicles[DetailDeployment.VehiclesNumber].Seats + " seater ");
                Console.Write(DetailDeployment.Vehicles[DetailDeployment.VehiclesNumber].Type + " (" + DetailDeployment.Vehicles[DetailDeployment.VehiclesNumber].HP + "hp), ");
            }

            Console.WriteLine();

            // Extrawerte für Fahrzeuge hier
            Console.WriteLine("Resources:          " + DetailDeployment.Resources);
            Console.WriteLine("Deployed units:     " + DetailDeployment.Human);
            // Hier "Schlauchlängen"
            Console.WriteLine("Comment:            " + DetailDeployment.Comment);
            return;
        }

        public static void DeploymentList()
        {



            int i = 0;
            Console.WriteLine("How many deployments are to be shown?");
            int.TryParse(Console.ReadLine(), out int number);
            number += 1;
            String SDeployments = "";
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Deployments.txt"))


                while (SDeployments != null)
                {


                    SDeployments = UserText.ReadLine();

                    i += 1;

                }

            Deployment[] Deployments = new Deployment[i];
            i = 0;
            SDeployments = "";
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Deployments.txt"))

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
            int iMax = i;
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

        //public static void DeploymentSearch();



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
    }

}
