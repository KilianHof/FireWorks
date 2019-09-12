using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class DeploymentFactory
    {
        public static Deployment NewDeployment(string loc, object veh, object res, object hum, string com, int num)
        {
            return new Deployment(loc, veh, res, hum, com, num);
        }
    }
}
