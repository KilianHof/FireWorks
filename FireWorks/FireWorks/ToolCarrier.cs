using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    public class ToolCarrier : Vehicles
    {
        public bool Chainsaw { get; set; }
        public ToolCarrier(string t, int ep, int s, bool cs) :
        base(t,ep,s)
        {
            Chainsaw = cs;
        }
    }
}
