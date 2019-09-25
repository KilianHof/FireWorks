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
        public enum DataSets
        {
            Deployments = 0,
            Employees = 1,
            Vehicles = 2,
            Resources = 3,
            Firefighters = 4,
        }
        /// <summary>
        /// Writing an object as JSON to a file. If it doesnt exist its being created and then written to.
        /// </summary>
        /// <param name="o"> The object you want to write into a file.</param>
        /// <param name="path">The file that is being written to.</param>
        private readonly IUserLayer _t;
        private readonly string[] _paths;
        public FileIO(IUserLayer tui, string[] paths) { _t = tui; _paths = paths; }

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
            string Path = _paths[1];
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
        public List<T> ReadAll<T>(string path)
        {
            List<T> tmp = new List<T>();
            if (File.Exists(path))
            {
                foreach (string line in File.ReadLines(path))
                {
                    tmp.Add(JSONConverter.JSONToGeneric<T>(line));
                }
                return tmp;
            }
            _t.Display("cannot read file: " + path + "\n");
            return tmp;
        }
        public void SaveListToFile<T>(List<T> liste)
        {
            if (liste.Count == 0) { return; }

            int path = -1;
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Vehicle))) { path = 0; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Vehicle))) { path = 1; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Vehicle))) { path = 2; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Vehicle))) { path = 3; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Vehicle))) { path = 4; }
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
        public int GetLastDeploymentNumber(List<Deployment> liste)         // new version need test
        {
            if (liste is null) return 0;
            return liste.Count();
        }
        public void SaveAllLists(object[] lists)
        {
            SaveListToFile<Deployment>((List<Deployment>)lists[0]); //warum kann ich das vereinfachen?
            SaveListToFile<User>((List<User>)lists[1]);
            SaveListToFile<Vehicle>((List<Vehicle>)lists[2]);
            SaveListToFile<Resources>((List<Resources>)lists[3]);
            SaveListToFile<FireFighter>((List<FireFighter>)lists[4]);
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
    }
}
