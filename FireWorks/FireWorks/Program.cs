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

            Deployment test = AskForDeployment();
            FileIO.WriteToFile(test, _Path);
            Console.ReadLine();

        }

        public static Deployment AskForDeployment()
        {
            Console.WriteLine("Where?");
            string loc = Console.ReadLine();
            Console.WriteLine("Which vehicles were used?");
            object[] veh = new string[] { Console.ReadLine() };
            Console.WriteLine("What resources were used?");
            object[] res = new string[] { Console.ReadLine() };
            Console.WriteLine("Who was send?");
            FireFighter[] Ff = new FireFighter[] {new FireFighter("hallo","ibims",12),new FireFighter("hallo", "ibims", 12) };
            Console.WriteLine("Comments?");
            string com = Console.ReadLine();
            int num = GetLastDeploymentNumber() + 1;
            return DeploymentFactory.NewDeployment(loc, veh, res, Ff, com, num);
        }
        public static int GetLastDeploymentNumber()
        {
            int lineCount = File.ReadLines(_Path).Count();
            Deployment last = FileIO.ReadObjectFromFile(_Path, lineCount);
            return last.Number;
        }
    }
}
