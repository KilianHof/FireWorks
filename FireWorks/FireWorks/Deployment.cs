﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    /// <summary>
    /// Deployment onbject contains all the information of a given Deployment
    /// </summary>
    class Deployment
    {
        public string DateAndTime { get; set; }
        public string Location { get; set; }
        public object[] Vehicles { get; set; }
        public object[] Resources { get; set; }
        public FireFighter[] FireFighters { get; set; }
        public string Comment { get; set; }
        public int Number { get; set; }

        public Deployment() //null
        {
            Number = 0;
        }
        /// <summary>
        /// Constructor for Deployments
        /// </summary>
        /// <param name="Loc">Location</param>
        /// <param name="Veh">Vehicles</param>
        /// <param name="Res">Resources</param>
        /// <param name="Ff">FireFighters</param>
        /// <param name="Com">Comment</param>
        /// <param name="Num">Number</param>
        public Deployment(string Loc, object[] Veh, object[] Res, FireFighter[] Ff, string Com, int Num)
        {
            DateAndTime = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            Location = Loc;
            Vehicles = Veh;
            Resources = Res;
            FireFighters = Ff;
            Comment = Com;
            Number = Num;
        }
    }
}
