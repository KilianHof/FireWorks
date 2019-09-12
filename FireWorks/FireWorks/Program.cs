using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FireWorks
{
    class Program
    {
        private const string _Path = @"C:\FireWorks\Deployments.txt";

        static void Main(string[] args)
        {
            //@"C:\FireWorks\Deployments.txt"
            //Console.WriteLine("1234".GetHashCode());
            //Console.WriteLine("Hello and welcome to FireWorks! \n Please Enter a four digit PIN to continue.");
            //Authenticator.LogIn();

            Deployment test = DeploymentFactory.PromptDeployment();
            FileIO filer = new FileIO();
            filer.WriteObject(test, _Path);
            Console.ReadLine();
            TUI t = new TUI();
            

        }
    }
}
