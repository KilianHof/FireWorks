﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FireWorks
{
    public class Human
    {

        public int ID { get; set; }
        public string PIN { get; set; }
        public int Status { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

    }
}
