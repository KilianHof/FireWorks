﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class Human
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Human()
        {
        }
        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Id;
        }
        public void Edit()
        {


        }
    }
    class FireFighter : Human
    {
        public FireFighter(string fname, string lname, int id)
        {
            LastName = lname;
            FirstName = fname;
            Id = id;
        }

    }
    class User : Human
    {
        public string PIN { get; set; }
        public string Status { get; set; }

        public User(string fname, string lname, int id, string status, string pin)
        {
            LastName = lname;
            FirstName = fname;
            Id = id;
            PIN = pin;
            Status = status;
        }
        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Id + " " + PIN + " " + Status;
        }
    }
}
