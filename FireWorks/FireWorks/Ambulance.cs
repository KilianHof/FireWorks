using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class Ambulance : Vehicles
    {

        public int PatientWeight { get; set; }
        public Ambulance(string t, int ep, int s, int pw) :
        base(t, ep, s)
        {
            PatientWeight = pw;
        }
    }
}
