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
        private static TUI _t = new TUI();
        private static FileIO _filer = new FileIO(_t);
        static void Main(string[] args)
        {
            //Console.WriteLine("0000".GetHashCode());
            //Console.WriteLine("Hello and welcome to FireWorks! \n Please Enter a four digit PIN to continue.");


            Authenticator auth = new Authenticator(_t, _filer, _pathEmployee);
            string[] paths = new string[] {_pathDeployment, _pathEmployee,  _pathVehicle, _pathResource };
            UserFunctions uf = new UserFunctions(_t, _filer, paths);

            
            uf.Foo(auth.LogIn());


            //Deployment test = DeploymentFactory.PromptDeployment(_pathDeployment,_filer);

            //_filer.WriteObject(test, _path);

            //User user = new User("m", "p", 2, "USER", "15947562");
            //_filer.WriteObject(user, _pathEmployee);
            Console.ReadLine();
        }
    }
}
