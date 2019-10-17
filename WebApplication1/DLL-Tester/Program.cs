using System;

namespace DLL_Tester
{
    class Program
    {
        static void Main()
        {
            Deployer.DeployerProgram tmp = new Deployer.DeployerProgram();
            Deployer.Deployment test = tmp.Main();
        }
    }
}
