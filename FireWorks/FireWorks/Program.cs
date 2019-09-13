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
        private static TUI _t = new TUI();
        static void Main(string[] args)
        {
            //@"C:\FireWorks\Deployments.txt"
            //Console.WriteLine("1234".GetHashCode());
            //Console.WriteLine("Hello and welcome to FireWorks! \n Please Enter a four digit PIN to continue.");
            //Authenticator.LogIn();
            

            Authenticator auth = new Authenticator();
            auth.NeedOutput += OutputEvent;
            auth.NeedBoolInput += BoolInputEvent;
            auth.LogIn();


            //Deployment test = DeploymentFactory.PromptDeployment();
            //FileIO filer = new FileIO();
            //filer.WriteObject(test, _Path);
            //Console.ReadLine();

        }
        static void OutputEvent(string str)
        {
            _t.Display(str);
        }
        static bool BoolInputEvent()
        {
            return _t.GetBool();
        }
    }
}
