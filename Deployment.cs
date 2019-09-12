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
        public static void DeploymentList()
        {

            int i = 0;
            Console.WriteLine("Wie viele Einsätze sollen Maximal gezeigt werden?");
            int.TryParse(Console.ReadLine(), out int number);
            number += 1;
            string read;
            String SDeployments = "";
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Deployments.txt"))
          

                while (SDeployments != null)
                {


                   SDeployments  = UserText.ReadLine();

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
                    Console.WriteLine("Es sind nur "+iMax+" Einträge vorhanden.");
                    DeploymentList();
                    break;
                }
                Console.WriteLine("Datum: "+Deployments[number].Date + "    Ort: "+Deployments[number].Location+"    ID:"+Deployments[number].Number);
                number -= 1;
            }

            return;
            
        }
    }

}
