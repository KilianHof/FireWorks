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
        static void Main(string[] args)
        {
            //Console.WriteLine("1234".GetHashCode());
            //Console.WriteLine("Hello and welcome to FireWorks! \n Please Enter a four digit PIN to continue.");
            //Authenticator.LogIn();


            Deployment test = AskForDeployment();
            FileWriter.WriteToFile(test , @"C:\FireWorks\Deployments.txt");
            Console.ReadLine();

            //@"C:\FireWorks\Deployments.txt"





        }

        public static Deployment AskForDeployment()
        {
            Console.WriteLine("Where?");
            string loc = Console.ReadLine();
            Console.WriteLine("Which vehicles were used?");
            string veh = Console.ReadLine();
            Console.WriteLine("What resources were used?");
            string res = Console.ReadLine();
            Console.WriteLine("Who was send?");
            string hum = Console.ReadLine();
            Console.WriteLine("Comments?");
            string com = Console.ReadLine();
            // todo add get number
            return DeploymentFactory.NewDeployment(loc,veh,res,hum,com,3);
        }
       
    }
}
