using System;

namespace DLL_Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Deployer.DeployerProgram tmp = new Deployer.DeployerProgram();
            Deployer.Deployment yeet = tmp.Main();
        }
    }
}
