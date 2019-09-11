using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class DeploymentFactory
    {
        public DeploymentFactory()
        {
        }
        public Deployment NewDeployment()
        {
            Console.WriteLine("Where?");
            string Loc = Console.ReadLine();
            Console.WriteLine("Which vehicles were used?");
            string Veh = Console.ReadLine();
            Console.WriteLine("What resources were used?");
            string Res = Console.ReadLine();
            Console.WriteLine("Who was send?");
            string Hum = Console.ReadLine();
            Console.WriteLine("Comments?");
            string Com = Console.ReadLine();
            return new Deployment(Loc, Veh, Res, Hum, Com);
        }
    }
}
