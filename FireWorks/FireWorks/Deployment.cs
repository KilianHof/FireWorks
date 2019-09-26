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
                if( this.Resources[i].GetType() == typeof(Hose))
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
            return "B: "+B+" C: "+C+ " D: "+D;
        }
    }
}
