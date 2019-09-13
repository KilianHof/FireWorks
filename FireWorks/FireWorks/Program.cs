﻿using System;
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
        private const string _pathDeployment = @"C:\FireWorks\Deployments.txt";
        private static TUI _t = new TUI();
        static void Main(string[] args)
        {
            //Console.WriteLine("1234".GetHashCode());
            //Console.WriteLine("Hello and welcome to FireWorks! \n Please Enter a four digit PIN to continue.");


            Authenticator auth = new Authenticator();
            auth.NeedOutput += OutputEvent;
            auth.NeedBoolInput += BoolInputEvent;
            auth.NeedStringInput += StringInputEvent;
            auth.LogIn();


            //Deployment test = DeploymentFactory.PromptDeployment();
            FileIO filer = new FileIO();
            //filer.NeedOutput += OutputEvent;
            //filer.WriteObject(test, _path);
            //Console.ReadLine();

            User user = new User("mo","pa",1,"ADMIN", "-1152142086");
            filer.WriteObject(user);

        }
        static void OutputEvent(string str)
        {
            _t.Display(str);
        }
        static bool BoolInputEvent()
        {
            return _t.GetBool();
        }
        static string StringInputEvent()
        {
            return _t.GetString();
        }
    }
}
