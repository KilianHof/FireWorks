using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FireWorks
{
    /// <summary>
    /// Wrapper for writing to a file.
    /// </summary>
    public class FileIO : IDataLayer
    {
        const string _pathEmployee = @"C:\FireWorks\Employee.txt";
        const string _pathDeployment = @"C:\FireWorks\Deployments.txt";
        const string _pathVehicle = @"C:\FireWorks\Vehicles.txt";
        const string _pathResource = @"C:\FireWorks\Resources.txt";
        const string _pathFireFighter = @"C:\FireWorks\FireFighter.txt";
        public static string[] _paths = new string[] { _pathDeployment, _pathEmployee, _pathVehicle, _pathResource, _pathFireFighter };

        private const int Deployments = 0;
        private const int Employees = 1;
        private const int Vehicles = 2;
        private const int Resources = 3;
        private const int Firefighters = 4;
        /// <summary>
        /// Writing an object as JSON to a file. If it doesnt exist its being created and then written to.
        /// </summary>
        /// <param name="o"> The object you want to write into a file.</param>
        /// <param name="path">The file that is being written to.</param>
        private readonly IUserLayer _t;
        public FileIO(IUserLayer tui) { _t = tui; }

        public bool[] CheckForFiles()
        {
            bool[] pathsExist = new bool[] { true, true, true, true, true };
            for (int i = 0; i < _paths.Length; i++)
            {
                if (!(File.Exists(_paths[i]))) { pathsExist[i] = false; }
            }
            return pathsExist;
        }
        public void Init(bool[] existing)
        {
            for (int i = 0; i < _paths.Length; i++)
            {
                if (!existing[i])
                {
                    Byte[] info;
                    using (FileStream fs = File.Create(_paths[i]))
                    {
                        if (i == 1)
                        {
                            string PIN = "0000";
                            info = new UTF8Encoding(true).GetBytes(@"{""PIN"":""" + PIN.GetHashCode().ToString() + @""",""Status"":""ADMIN"",""Id"":1,""FirstName"":""FirstName"",""LastName"":""LastName""}");
                        }
                        else
                            info = new UTF8Encoding(true).GetBytes("");
                        fs.Write(info, 0, info.Length);
                    }
                }
            }
        }
        public string[] GetListOfUsers()
        {
            List<string> results = new List<string>();
            string Path = _paths[Employees];
            if (File.Exists(Path))
            {
                int lineCount = File.ReadLines(Path).Count();
                if (!(lineCount == 0))
                    for (int i = 1; i <= lineCount; i++)
                    {
                        results.Add(File.ReadLines(Path).Skip(i - 1).Take(1).First());
                    }
                else
                {
                    _t.Display("cannot read line \"0\" or negative.(file empty?)" + "\n");
                    _t.Display("No lines indicates no stored users." + "\n");
                }
                return results.ToArray();
            }
            _t.Display("cannot read file: " + Path + "\n");
            return results.ToArray();
        }
        public List<T> ReadFile<T>()
        {
            int path = Pathfinder<T>();
            List<T> tmp = new List<T>();
            if (File.Exists(_paths[path]))
            {
                foreach (string line in File.ReadLines(_paths[path]))
                {
                    tmp.Add(JSONConverter.JSONToGeneric<T>(line));
                }
                return tmp;
            }
            _t.Display("cannot read file: " + _paths[path] + "\n");
            return tmp;
        }

        public object[] ReadAllFiles()
        {
            List<Deployment> Deploys = ReadFile<Deployment>();
            List<User> Employs = ReadFile<User>();
            List<Vehicle> Vehicles = ReadFile<Vehicle>();
            List<Resources> Resources = ReadFile<Resources>();
            List<FireFighter> FireFighters = ReadFile<FireFighter>();
            return new object[] { Deploys, Employs, Vehicles, Resources, FireFighters };

        }
        public void SaveListToFile<T>(List<T> liste)
        {
            if (liste.Count == 0) { return; }

            int path = Pathfinder<T>();
            if (path == -1) { return; }
            if (!File.Exists(_paths[path])) { _t.Display("cannot read file: " + _paths[path] + "\n"); return; }

            string[] str = new string[liste.Count];
            int i = 0;
            foreach (var item in liste)
            {
                str[i] += JSONConverter.ObjectToJSON(item);
                i++;
            }
            File.WriteAllLines(_paths[path], str);

        }
        public int GetLastDeploymentNumber()         // new version need test
        {
            List<Deployment> liste = ReadFile<Deployment>();
            if (liste is null) return 0;
            return liste.Count();
        }
        public void SaveAllLists(object[] lists)
        {
            SaveListToFile<Deployment>((List<Deployment>)lists[Deployments]); //warum kann ich das vereinfachen?
            SaveListToFile<User>((List<User>)lists[Employees]);
            SaveListToFile<Vehicle>((List<Vehicle>)lists[Vehicles]);
            SaveListToFile<Resources>((List<Resources>)lists[Resources]);
            SaveListToFile<FireFighter>((List<FireFighter>)lists[Firefighters]);
        }
        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur) { return true; }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
        public int Pathfinder<T>()
        {
            int path = -1;
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Deployment))) { path = Deployments; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(User))) { path = Employees; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Vehicle))) { path = Vehicles; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Resources))) { path = Resources; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(FireFighter))) { path = Firefighters; }
            return path;
        }
    }
}
