using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    public class Hose : Resources
    {
        public char Letter { get; set; }
        public Hose(string d, int inn, char l) :
            base(d, inn)
        {
            Letter = l;
        }
    }
}
