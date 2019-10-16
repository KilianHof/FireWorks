using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer
{
    /// <summary>
    /// Factroy for creating Deployment objects.
    /// </summary>
    public class DeploymentFactory
    {
        /// <summary>
        /// Calls the Constructor of Deployment objects with needed params.
        /// </summary>
        /// <param name="loc">Location.</param>
        /// <param name="veh">Vehicles</param>
        /// <param name="res">Resources</param>
        /// <param name="Ff">FireFighter</param>
        /// <param name="com">Comment</param>
        /// <param name="num">Number</param>
        /// <returns></returns>
        public static Deployment NewDeployment(string loc, Vehicle[] veh, Resources[] res, FireFighter[] Ff, string com, int num)
        {
            return new Deployment(loc, veh, res, Ff, com, num);
        }
    }
}
