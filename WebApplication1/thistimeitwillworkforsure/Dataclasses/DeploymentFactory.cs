using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    /// <summary>
    /// Factroy for creating Deployment objects.
    /// </summary>


    public interface IDeploy
    {
        Deployment NewDeployment(string loc, Vehicle[] veh, Resource[] res, FireFighter[] Ff, string com, int num);
    }


    public class DeploymentFactory : IDeploy
    {
        /// <summary>
        /// Calls the Constructor of Deployment objects with needed parameters.
        /// </summary>
        /// <param name="loc">Location.</param>
        /// <param name="veh">Vehicles</param>
        /// <param name="res">Resources</param>
        /// <param name="Ff">FireFighter</param>
        /// <param name="com">Comment</param>
        /// <param name="num">Number</param>
        /// <returns></returns>
        public Deployment NewDeployment(string loc, Vehicle[] veh, Resource[] res, FireFighter[] Ff, string com, int num)
        {
               var tmp = new Deployment(loc, veh, res, Ff, com, num);
            return tmp;
        }
    }
    public class DeploymentDLL : IDeploy
    {

        public Deployment NewDeployment(string loc, Vehicle[] veh, Resource[] res, FireFighter[] Ff, string com, int num)
        {
            Deployer.DeployerProgram tmp = new Deployer.DeployerProgram();
            Deployer.Deployment test = tmp.Main();
            DeploymentFactory tempo = new DeploymentFactory();
            Deployment temporary = tempo.NewDeployment(test.Location, null, null, null, test.Comment, test.Id);
            return temporary;
        }
    }
}
