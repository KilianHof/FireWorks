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
        public string Name { get; set; }


        public Resources(string d, int inv,string n)
        {
            Description = d;
            InventoryNumber = inv;
            Name = n;
        }
        public override string ToString()
        {
            return Description + " " + InventoryNumber;
        }
    }
    public class Hose : Resources
    {
        public char Letter {
            get {return Letter; }
            set
            {
                if (value == 'B' || value == 'C' || value == 'D')
                    Letter = value;
                //else throw excep?
            }
        }
        public int HoseLength {
            get { return HoseLength; }
            set
            {
                if (value == 5 || value == 10 || value == 20 || value == 30)
                    HoseLength = value;
                //else throw excep?
            }
        }
        public Hose(string d, int inv, char l, int len) :
            base(d, inv, "Hose")
        {
            Name = "Hose";
            Letter = l;
            HoseLength = len;
        }
    }
}
