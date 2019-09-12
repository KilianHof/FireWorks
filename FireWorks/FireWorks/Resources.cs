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
    }
}
