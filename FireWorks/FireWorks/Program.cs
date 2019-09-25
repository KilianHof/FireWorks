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

        static void Main(string[] args)
        {
            string f = Directory.GetCurrentDirectory();
            f += @"\Files";
            if (!Directory.Exists(f))
            {
                Directory.CreateDirectory(f);
            }
            FireWorksMain program = new FireWorksMain();
            program.Run();
        }
    }
}
