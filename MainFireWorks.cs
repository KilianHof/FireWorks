using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FireWorks
{
    class MainFireWorks
    {
#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        public static void Main(string[] args)
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen
        {


            Deployment test = new Deployment();
            string testtext;
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Deployments.txt"))
            testtext = UserText.ReadLine();
                    test = JsonConvert.DeserializeObject<Deployment>(testtext);
            Console.WriteLine(test.Location+test.Date);
                

            Human CurrentUser = new Human();
            var SCurrentUser = Authenticator.LogIn();
            Console.WriteLine(CurrentUser);

            CurrentUser = JsonConvert.DeserializeObject<Human>(SCurrentUser);

            Authenticator.StatusCheck(CurrentUser);

            Console.Clear();
            Console.WriteLine("(e)insatzliste / Einsatz(s)uche");
            String Answer = Console.ReadLine();

           if (Answer == "e")
            {
                DeploymentListing.DeploymentList();
            }

            Console.ReadLine();

        }
    }
}
