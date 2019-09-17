﻿using System;
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
            if (CurrentUser.Status == 1)
            {
                Console.Clear();
                String Answer;

                Console.WriteLine("(d)etailed Report / show deploym(e)nts / (s)earch deployments");
                Answer = Console.ReadLine();

                if (Answer == "e")
                {
                    DeploymentListing.DeploymentList();
                }
                if (Answer == "s")
                {
                    DeploymentListing.DeploymentSearch();
                }
                if (Answer == "d")
                {
                    Console.Clear();
                    DeploymentListing.DeploymentDetail();
                }
            }
            if (CurrentUser.Status == 2)
            {
                Console.Clear();
                String Answer;

                Console.WriteLine("(detail)ed Report / (show) deployments / (search) deployments");
                Console.WriteLine("(create) deployment / (lock) or unlock user / (edit) data");
                Answer = Console.ReadLine();

                switch (Answer)
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
                        Authenticator.UserLock(); // kann auch admin machen
                        break;
                    case "edit":
                        Resource.Editor();
                        break;
                }
            }
        }
    }
}
