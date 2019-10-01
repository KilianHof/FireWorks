using Newtonsoft.Json;
using System;
using System.IO;

namespace FireWorks
{
    public static class StoragePathClass
    {
        public static string StoragePath = "";
    }
    class MainFireWorks
    {

#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        public static void Main(string[] args)
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen
        {
            

            StoragePathClass.StoragePath = Directory.GetCurrentDirectory();

            Human CurrentUser;
            var SCurrentUser = Authenticator.LogIn();   //checkt den PIN

            CurrentUser = JsonConvert.DeserializeObject<Human>(SCurrentUser);

            Authenticator.StatusCheck(CurrentUser); //ordnet Nutzer nach Status ein
            if (CurrentUser.Status == 1)
            {
                UserMenu();
            }
            if (CurrentUser.Status == 2)
            {
                Console.Clear();
                AdminMenu();
            }
        }

        private static void UserMenu()
        {
            bool exit = false;
            while (exit == false)
            {
                Console.Clear();
                Console.WriteLine("(d)etailed Report / show deploym(e)nts / (s)earch deployments / (exit)");
                switch (Console.ReadLine())
                {
                    case "e":
                        DeploymentListing.DeploymentList();
                        break;
                    case "s":
                        DeploymentListing.DeploymentSearch();
                        break;
                    case "d":
                        DeploymentListing.DeploymentDetail();
                        break;
                    case "exit":
                        exit = true;
                        break;
                }
            }
        }

        private static void AdminMenu()
        {
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine("(detail)ed Report / (show) deployments / (search) deployments");
                Console.WriteLine("(create) deployment / (lock) or unlock user / (edit) data / (exit)");

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
                    case "exit":
                        exit = true;
                        break;
                }
            }
        }
    }
}