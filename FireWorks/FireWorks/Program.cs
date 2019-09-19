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
        const string _pathEmployee = @"C:\FireWorks\Employee.txt";
        const string _pathDeployment = @"C:\FireWorks\Deployments.txt";
        const string _pathVehicle = @"C:\FireWorks\Vehicles.txt";
        const string _pathResource = @"C:\FireWorks\Resources.txt";
        const string _pathFireFighter = @"C:\FireWorks\FireFighter.txt";
        public static string[] _paths = new string[] { _pathDeployment, _pathEmployee, _pathVehicle, _pathResource, _pathFireFighter };

        //public static List<Deployment> AllDeployments;

        private static TUI _t = new TUI();
        private static FileIO _filer = new FileIO(_t);
        static void Main(string[] args)
        {
            //Console.WriteLine("0000".GetHashCode());

            Authenticator auth = new Authenticator(_t, _filer, _paths[1]);
            UserFunctions uf = new UserFunctions(_t, _filer, _paths);
            bool loop = true;
            string str = auth.LogIn();
            while (loop)
            {
                uf.Routine(str);
                uf.SaveAllListsToFile();
                _t.Display("Continue?");
                loop = _t.GetBool();
            }

            //Deployment test = DeploymentFactory.PromptDeployment(AllDeployments,_filer);

            //_filer.WriteObject(test, _path);

            //User user = new User("m", "p", 2, "USER", "15947562");
            //_filer.WriteObject(user, _pathEmployee);
        }
    }
}
