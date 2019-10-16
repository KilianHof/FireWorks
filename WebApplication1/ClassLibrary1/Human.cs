using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer
{
    public class Human
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Human()
        {

        }
        public Human(int id, string fname, string lname)
        {
            Id = id;
            FirstName = fname;
            LastName = lname;
        }
        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Id;
        }
    }
    public class FireFighter : Human
    {
        public FireFighter()
        {

        }
        public FireFighter(string fname, string lname, int id)
        {
            LastName = lname;
            FirstName = fname;
            Id = id;
        }
    }
}
