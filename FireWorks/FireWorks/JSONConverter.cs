﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace FireWorks
{
    /// <summary>
    /// Wrapper for JSON conversion.
    /// </summary>
    class JSONConverter
    {
        /// <summary>
        /// Takes an object and returns it as JSON.
        /// </summary>
        /// <param name="o">Object that is being converted.</param>
        /// <returns> returns the JSON string</returns>
        public static string ObjectToJSON(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.None);
        }
        /// <summary>
        /// Takes JSON and turns into a Deployment object.
        /// </summary>
        /// <param name="str">String thats being converted.</param>
        /// <returns>A Deployment object.</returns>
        public static object JSONToDeployment(string str)
        {
            return JsonConvert.DeserializeObject<Deployment>(str);
        }
    }
}
