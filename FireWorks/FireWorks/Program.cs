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


            List<Deployment> Deploys = _filer.ReadAll<Deployment>(_paths[0]);
            List<User> Employs = _filer.ReadAll<User>(_paths[1]);
            List<Vehicle> Vehicles = _filer.ReadAll<Vehicle>(_paths[2]);
           // Vehicles.Add(new Pkw("TYPE", 0, 0));
            List<Resources> Resources = _filer.ReadAll<Resources>(_paths[3]);
            List<FireFighter> FireFighters = _filer.ReadAll<FireFighter>(_paths[4]);
            object[] lists = { Deploys, Employs, Vehicles, Resources, FireFighters };


            UserFunctions uf = new UserFunctions(_t, _filer, lists, _paths);
            //UserFunctions uf = new UserFunctions(_t, _filer, _paths);
            bool loop = true;
            string str = auth.LogIn();
            while (loop)
            {
                uf.Routine(str);
                uf.SaveAllListsToFile();
                _t.Display("Continue?");
                loop = _t.GetBool();
            }
            //test
            //Deployment test = DeploymentFactory.PromptDeployment(AllDeployments,_filer);
            //aawdawdawdawd
            //asdasd
            //_filer.WriteObject(test, _path);

            //User user = new User("m", "p", 2, "USER", "15947562");
            //_filer.WriteObject(user, _pathEmployee);
        }
    }
}
