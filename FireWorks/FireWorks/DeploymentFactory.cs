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
    class DeploymentFactory
    {
        private const string _Path = @"C:\FireWorks\Deployments.txt";
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
        public static Deployment NewDeployment(string loc, object[] veh, object[] res, FireFighter[] Ff, string com, int num)
        {
            return new Deployment(loc, veh, res, Ff, com, num);
        }
        public static Deployment PromptDeployment()
        {
            Console.WriteLine("Where?");
            string loc = Console.ReadLine();

            Console.WriteLine("Which vehicles were used?");
            object[] veh = new string[] { Console.ReadLine() };

            Console.WriteLine("What resources were used?");
            object[] res = new string[] { Console.ReadLine() };

            Console.WriteLine("How Many personel was send?");
            int amount = int.Parse(Console.ReadLine());

            FireFighter[] Ff = new FireFighter[amount];
            Console.WriteLine("Who was send?");
            for (int i = 0; i < amount; i++)
            {
                Ff[i] = new FireFighter(Console.ReadLine(), Console.ReadLine(), int.Parse(Console.ReadLine()));
            }
            Console.WriteLine("Comments?");
            string com = Console.ReadLine();
            int num = GetLastDeploymentNumber() + 1;
            return new Deployment(loc, veh, res, Ff, com, num);
        }
        public static int GetLastDeploymentNumber()
        {
            int lineCount = File.ReadLines(_Path).Count();
            Deployment last = FileIO.ReadObjectFromFile(_Path, lineCount);
            return last.Number;
        }
    }
}
