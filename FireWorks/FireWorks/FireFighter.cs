using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class FireFighter : Human
    {
        public FireFighter(string fname, string lname, int id)
        {
            LastName = lname;
            FirstName = fname;
            Id = id;
        }
        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Id; 
        }
    }
}
