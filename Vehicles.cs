﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FireWorks
{
    
    public class Vehicles
    {

        public String Type { get; set; } // Fahrzeuginfo
        public int Water { get; set; } // in Liter
        public Boolean Saw { get; set; } // Kettensäge y/n


    }
}
