using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    public class Vehicles
    {
        public string Type { get; set; }   
        public int EnginePower { get; set; }
        public int Seats { get; set; }


        public Vehicles(string t, int ep, int s)
        {
            Type = t;
            EnginePower = ep;
            Seats = s;
        }
    }
}
