using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class Deployment
    {
        public string DateAndTime { get; set; }
        public string Location { get; set; }
        public object Vehicles { get; set; }
        public object Resources { get; set; }
        public object Human { get; set; }
        public string Comment { get; set; }
        public int Number { get; set; }

        public Deployment() //null
        {

        }
        public Deployment(string Loc, object Veh, object Res, object Hum, string Com, int Num)
        {
            DateAndTime = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            Location = Loc;
            Vehicles = Veh;
            Resources = Res;
            Human = Hum;
            Comment = Com;
            Number = Num;
        }
    }
}
