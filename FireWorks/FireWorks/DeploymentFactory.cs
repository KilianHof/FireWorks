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
        public Deployment NewDeployment(string loc, Vehicle[] veh, object[] res, FireFighter[] Ff, string com, int num)
        {
            return new Deployment(loc, veh, res, Ff, com, num);
        }
        /// <summary>
        /// Prompts the user to enter a new Deployment object.
        /// </summary>
        /// <returns>The generated Deployment object.</returns>
        //public Deployment PromptDeployment(List<Deployment> deploy, FileIO filer)
        //{
        //    Console.WriteLine("Where?");
        //    string loc = Console.ReadLine();

        //    Console.WriteLine("How many vehicles were used?");

        //    int amount = int.Parse(Console.ReadLine());

        //    Vehicle[] veh = new Vehicle[amount];

        //    for (int i = 0; i < amount; i++)
        //    {
        //        Console.WriteLine("Type? (0) PKW,(1) Firetruck,(2) Ambulance,(3) Turntableladder");
        //        string answer = Console.ReadLine();

        //        switch (answer)
        //        {
        //            case "0":
        //                veh[i] = new Pkw("hi", 1, 2);

        //                break;
        //            case "1":
        //                veh[i] = new FireTruck("hi", 1, 2, true, 232);

        //                break;
        //            case "2":
        //                veh[i] = new Ambulance("hi", 1, 2, 232);
        //                break;
        //            case "3":
        //                veh[i] = new TurntableLadder("hi", 1, 2, true, 43534);
        //                break;
        //            default:
        //                Console.WriteLine("couldnt read input please try again");
        //                i--;
        //                break;
        //        }
        //    }
        //    Console.WriteLine("What resources were used?");
        //    object[] res = new string[] { Console.ReadLine() };

        //    Console.WriteLine("How Many personel was send?");
        //    amount = int.Parse(Console.ReadLine());
        //    FireFighter[] Ff = new FireFighter[amount];
        //    Console.WriteLine("Who was send?");                     // read from file?
        //    for (int i = 0; i < amount; i++)
        //    {
        //        Ff[i] = new FireFighter(
        //                                Console.ReadLine(),
        //                                Console.ReadLine(),
        //                      int.Parse(Console.ReadLine()));
        //    }

        //    Console.WriteLine("Comments?");
        //    string com = Console.ReadLine();


        //    int num = filer.GetLastDeploymentNumber(deploy) + 1;

        //    return new Deployment(loc, veh, res, Ff, com, num);
        //}
    }
}
