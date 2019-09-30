using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FireWorks
{
    /// <summary>
    /// Deployment onbject contains all the information of a given Deployment
    /// </summary>
    public class Deployment : IComparable      
    {
        public string DateAndTime { get; set; }
        public string Location { get; set; }
        public Vehicle[] Cars { get; set; }
        public Resources[] Resources { get; set; }
        public FireFighter[] FireFighters { get; set; }
        public string Comment { get; set; }
        public int Number { get; set; }

        public Deployment() //nullconstructor only to be used if there is no entry yet.
        {
            Number = 0;
        }
        /// <summary>
        /// Constructor for Deployments
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
            Number = Num;
        }
        public Deployment(string date,string Loc, Vehicle[] Veh, Resources[] Res, FireFighter[] Ff, string Com, int Num)
        {
            DateAndTime = date;
            Location = Loc;
            Cars = Veh;
            Resources = Res;
            FireFighters = Ff;
            Comment = Com;
            Number = Num;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public int CompareTo(object obj)
        {
            if (!(obj is Deployment)) throw new FormatException("Only compare Deployment to another Deployment");
               
            Deployment dep = (Deployment)obj;

            if (Number < dep.Number) return 1;
            if (Number > dep.Number) return -1;
            //if both "if's" arent true objects must be equal. No number is given twice.
            return 0;
        }
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
        public string GenerateWebReport()
        {
            string report = "\n\n";
            report += "Heute am "+DateAndTime+" gab es in "+Location+" einen Einsatz.\n";
            int numberOfFireFighters = FireFighters.Length;
            report += "An diesem Einsatz waren " + numberOfFireFighters + " Einsatzkräfte beteiligt.\n";
            if (numberOfFireFighters != 0)
            {
                report += "Folgende Personen haben geholfen:\n";
                foreach (var item in FireFighters)
                {
                    report += "    "+item.FirstName + " " + item.LastName + "\n";
                }
            }
            int numberOfVehicles = Cars.Length;
            report += "Desweiteren wurden " + numberOfVehicles + " Einsatzfahrzeuge benötigt.\n";
            if (numberOfVehicles != 0)
            {
                report += "Genutzte Fahrzeuge:\n";
                foreach (var item in Cars)
                {
                    report += "    " + item.Type + "\n";
                }
            }
            int numberOfResources = Resources.Length;
            report += "Desweiteren wurden " + numberOfResources + " Einsatzfahrzeuge benötigt.\n";
            if (numberOfResources != 0)
            {
                report += "Genutzte Ressourcen:\n";
                foreach (var item in Resources)
                {
                    report += "    " + item.Name+" "+ item.Description+ "\n";
                }
            }
            report += "Gesamt genutzte schlauchlänge:\n";
            report += "    "+SumUpHoses()+"\n";
            report += "Der Einsatzleiter gab diesen Kommentar zum Einsatz:\n";
            report += Comment + "\n";
            report += "Wir danken dem Team der Circlon Werksfeuerwehr für ihre Leistung und den " +Number+ ". Abgeschlossenen Einsatz.\n\n";
            return report;
        }
    }
}
