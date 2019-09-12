using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class FireTruck : ToolCarrier
    {
        public int FillQuantity { get; set; }
        public FireTruck(string t, int ep, int s,bool cs, int fq) :
        base(t, ep, s, cs)
        {
            FillQuantity = fq;
        }
    }
}
