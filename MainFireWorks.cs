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




            Human CurrentUser;
            var SCurrentUser = Authenticator.LogIn();

            CurrentUser = JsonConvert.DeserializeObject<Human>(SCurrentUser);

            Authenticator.StatusCheck(CurrentUser);
            Console.Clear();
            String Answer;
            
                Console.WriteLine("(d)etailed Report / deploym(e)nts / (s)earch deployments");
                Answer = Console.ReadLine();

                if (Answer == "e")
                {
                    DeploymentListing.DeploymentList();
                }
                //if (Answer == "s")
                //{
                //    DeploymentListing.DeploymentSearch();
                //}
                if (Answer == "d")
                {
                Console.Clear();
                DeploymentListing.DeploymentDetail();
                }


            Console.ReadLine();

        }
    }
}
