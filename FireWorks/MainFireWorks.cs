using Newtonsoft.Json;
using System;

namespace FireWorks
{
    public static class StoragePathClass
    {
        public static string StoragePath = "C:/Users/khof/source/repos";
    }
    class MainFireWorks
    {

#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        public static void Main(string[] args)
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen
        {


            Console.WriteLine("Enter the path to your /FireWorks/Storage directory");
            StoragePathClass.StoragePath = Console.ReadLine();

            Human CurrentUser;
            var SCurrentUser = Authenticator.LogIn();   //checkt den PIN

            CurrentUser = JsonConvert.DeserializeObject<Human>(SCurrentUser);

            Authenticator.StatusCheck(CurrentUser); //ordnet Nutzer nach Status ein
            if (CurrentUser.Status == 1)
            {
                Console.Clear();
                Console.WriteLine("(d)etailed Report / show deploym(e)nts / (s)earch deployments");
                switch (Console.ReadLine())
                {
                case "e":
                    DeploymentListing.DeploymentList();
                    break;
                case "s":
                    DeploymentListing.DeploymentSearch();
                    break;
                case "d":
                    Console.Clear();
                    DeploymentListing.DeploymentDetail();
                    break;
                }
            }
            if (CurrentUser.Status == 2)
            {
                Console.Clear();

                Console.WriteLine("(detail)ed Report / (show) deployments / (search) deployments");
                Console.WriteLine("(create) deployment / (lock) or unlock user / (edit) data");

                switch (Console.ReadLine())
                {
                    case "show":
                        DeploymentListing.DeploymentList();
                        break;
                    case "search":
                        DeploymentListing.DeploymentSearch();
                        break;
                    case "detail":
                        DeploymentListing.DeploymentDetail();
                        break;
                    case "create":
                        DeploymentListing.CreateDeployment();
                        break;
                    case "lock":
                        Authenticator.UserLock();
                        break;
                    case "edit":
                        Resource.Editor();
                        break;
                }
            }
        }
    }
}
