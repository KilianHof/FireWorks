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
        public char Letter { get; set; }
        public int HoseLength { get; set; }
        public static new string Name = "Hose";
        public Hose(string d, int inv, char l, int len) :
            base(d, inv, Name)
        {
            Letter = l;
            HoseLength = len;
        }
    }
}
