using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    public class Resources
    {
        public string Description { get; set; }
        public int InventoryNumber { get; set; }


        public Resources(string d, int inn)
        {
            Description = d;
            InventoryNumber = inn;
        }
        public override string ToString()
        {
            return Description + " " + InventoryNumber;
        }
    }
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
