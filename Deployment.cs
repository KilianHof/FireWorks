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
        public string Date { get; set; } // Einsatzdatum
        public string Location { get; set; } // Einsatzort
        public object Vehicles { get; set; } // Einsatzfahrzeuge
        public object Resources { get; set; } // Einsatzmittel
        public object Human { get; set; } // Einsatz-Anwesende
        public string Comment { get; set; } // Kommentar
        public int Number { get; set; } // Einsatz-ID
    }

    public class DeploymentListing
    {

        public static void DeploymentDetail()
        {
            String Deploytext = "";
            Deployment DetailDeployment = new Deployment();
            Console.Write("Einsatz ID:");
            String IDenter = Console.ReadLine();
            IDenter = "\"Number\":" + IDenter + "}"; //Absicherung
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Deployments.txt"))
                while (Deploytext.IndexOf(IDenter) == -1)
                {


                    Deploytext = UserText.ReadLine();
                    
                    if (Deploytext == null)
                    {
                        Console.WriteLine("Ungültige ID");
                        Console.ReadLine();
                        return;
                    }


                    

                }
                    DetailDeployment = JsonConvert.DeserializeObject<Deployment>(Deploytext);
                    Console.Clear();
                    Console.WriteLine("Einsatz-ID:      " + DetailDeployment.Number);
                    Console.WriteLine("Einsatz-Ort:     " + DetailDeployment.Location);
                    Console.WriteLine("Fahrzeuge:       " + DetailDeployment.Vehicles);
                    // Extrawerte für Fahrzeuge hier
                    Console.WriteLine("Einsatzmittel:   " + DetailDeployment.Resources);
                    Console.WriteLine("Einsatzkräfte:   " + DetailDeployment.Human);
                    // Hier "Schlauchlängen"
                    Console.WriteLine("Kommentar:       " + DetailDeployment.Comment);
            return;
        }

        public static void DeploymentList()
        {

            int i = 0;
            Console.WriteLine("Wie viele Einsätze sollen maximal gezeigt werden?");
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
                    Console.WriteLine("Es sind nur " + iMax + " Einträge vorhanden.");
                    DeploymentList();
                    break;
                }
                Console.WriteLine("Datum: " + Deployments[number].Date + "    Ort: " + Deployments[number].Location + "    ID:" + Deployments[number].Number);
                number -= 1;
            }


            Console.Write("In die Detailansicht übergehen?");
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
            Console.WriteLine("Bitte geben sie y oder n ein");
            return GetYesNo();

        }
    }

}
