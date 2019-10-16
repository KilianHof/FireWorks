using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace Deployer
{
    public class DeployerProgram
    {
        public Deployment Main()
        {
            DeploymentFactory tmp = new DeploymentFactory();
            Deployment depl = tmp.NewDeployment("Ort", null, null, null, "Lorem Ipsum", 1);
            return depl;
        }
    }

    public class Deployment : IComparable
    {
        public string DateAndTime { get; set; }
        public string Location { get; set; }
        public Vehicle[] Cars { get; set; }
        public Resources[] Resources { get; set; }
        public FireFighter[] FireFighters { get; set; }
        public string Comment { get; set; }
        public int Id { get; set; }

        public Deployment() //nullconstructor only to be used if there is no entry yet.
        {

        }
        /// <summary>
        /// Default constructor for Deployments
        /// </summary>
        /// <param name="Loc">Location</param>
        /// <param name="Veh">Cars</param>
        /// <param name="Res">Resources</param>
        /// <param name="Ff">FireFighters</param>
        /// <param name="Com">Comment</param>
        /// <param name="Num">Number</param>
        public Deployment(string Loc, Vehicle[] Veh, Resources[] Res, FireFighter[] Ff, string Com, int Num)
        {
            DateAndTime = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            Location = Loc;
            Cars = Veh;
            Resources = Res;
            FireFighters = Ff;
            Comment = Com;
            Id = Num;
        }
        /// <summary>
        /// Copy constructor only to be used when reading from files.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="Loc"></param>
        /// <param name="Veh"></param>
        /// <param name="Res"></param>
        /// <param name="Ff"></param>
        /// <param name="Com"></param>
        /// <param name="Num"></param>
        public Deployment(string date, string Loc, Vehicle[] Veh, Resources[] Res, FireFighter[] Ff, string Com, int Num)
        {
            DateAndTime = date;
            Location = Loc;
            Cars = Veh;
            Resources = Res;
            FireFighters = Ff;
            Comment = Com;
            Id = Num;
        }
        /// <summary>
        /// CompareTo implementation to be able to sort deployments
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Deployment)) throw new FormatException("Only compare Deployment to another Deployment");

            Deployment dep = (Deployment)obj;

            if (Id < dep.Id) return 1;
            if (Id > dep.Id) return -1;
            //if both "if's" arent true objects must be equal. No number is given twice.
            return 0;
        }
        /// <summary>
        /// Sums up all used Hoses
        /// </summary>
        /// <returns>
        /// returns string containing the summed Hoses
        /// </returns>
        public string SumUpHoses()
        {
            int B = 0, C = 0, D = 0;
            for (int i = 0; i < Resources.Length; i++)
            {
                if (this.Resources[i].GetType() == typeof(Hose))
                {
                    Hose tmp = (Hose)this.Resources[i];
                    switch (tmp.Letter)
                    {
                        case 'B':
                            B += tmp.HoseLength;
                            break;
                        case 'C':
                            C += tmp.HoseLength;
                            break;
                        case 'D':
                            D += tmp.HoseLength;
                            break;
                    }
                }
            }
            return "B: " + B + " C: " + C + " D: " + D;
        }
        /// <summary>
        /// Generate a Report in flowng text.
        /// </summary>
        /// <returns>
        /// A string containing the report.
        /// </returns>
        public string GenerateWebReport()
        {
            string report = "<br /><br />";
            report += "Heute am " + DateAndTime + " gab es in " + Location + " einen Einsatz.<br />";
            int numberOfFireFighters = FireFighters.Length;
            report += "An diesem Einsatz waren " + numberOfFireFighters + " Einsatzkräfte beteiligt.<br />";
            if (numberOfFireFighters != 0)
            {
                report += "Folgende Personen haben geholfen:<br />";
                foreach (var item in FireFighters)
                {
                    report += "    " + item.FirstName + " " + item.LastName + "<br />";
                }
            }
            int numberOfVehicles = Cars.Length;
            report += "Desweiteren wurden " + numberOfVehicles + " Einsatzfahrzeuge benötigt.<br />";
            if (numberOfVehicles != 0)
            {
                report += "Genutzte Fahrzeuge:<br />";
                foreach (var item in Cars)
                {
                    report += "    " + item.Type + "<br />";
                }
            }
            int numberOfResources = Resources.Length;
            report += "Desweiteren wurden " + numberOfResources + " Einsatzfahrzeuge benötigt.<br />";
            if (numberOfResources != 0)
            {
                report += "Genutzte Ressourcen:<br />";
                foreach (var item in Resources)
                {
                    report += "    " + item.Name + " " + item.Description + "<br />";
                }
            }
            report += "Gesamt genutzte schlauchlänge:<br />";
            report += "    " + SumUpHoses() + "<br />";
            report += "Der Einsatzleiter gab diesen Kommentar zum Einsatz:<br />";
            report += Comment + "<br />";
            report += "Wir danken dem Team der Circlon Werksfeuerwehr für ihre Leistung und den " + (Id) + ". Abgeschlossenen Einsatz.<br /><br />";
            return report;
        }
    }
}
