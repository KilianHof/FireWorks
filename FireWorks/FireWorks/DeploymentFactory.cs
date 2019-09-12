using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    /// <summary>
    /// Factroy for creating Deployment objects.
    /// </summary>
    class DeploymentFactory
    {   
        /// <summary>
        /// Calls the Constructor of Deployment objects with needed params.
        /// </summary>
        /// <param name="loc">Location.</param>
        /// <param name="veh">Vehicles</param>
        /// <param name="res">Resources</param>
        /// <param name="hum">Humans</param>
        /// <param name="com">Comment</param>
        /// <param name="num">Number</param>
        /// <returns></returns>
        public static Deployment NewDeployment(string loc, object veh, object res, object hum, string com, int num)
        {
            return new Deployment(loc, veh, res, hum, com, num);
        }
    }
}
