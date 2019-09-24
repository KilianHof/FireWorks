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
        /// <summary>
        /// Writing an object as JSON to a file. If it doesnt exist its being created and then written to.
        /// </summary>
        /// <param name="o"> The object you want to write into a file.</param>
        /// <param name="path">The file that is being written to.</param>
        private IUserLayer _t;
        private string[] _paths;
        public FileIO(IUserLayer tui,string[] paths) { _t = tui; _paths = paths; }

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
        public void SaveListToFile<T>(List<T> liste,string path)
        {
            if (!File.Exists(path)) { _t.Display("cannot read file: " + path + "\n"); return; }
            string[] str = new string[liste.Count];
            int i = 0;
            foreach (var item in liste)
            {
                str[i] += JSONConverter.ObjectToJSON(item);
                i++;
            }
            File.WriteAllLines(path, str);
        }
        public int GetLastDeploymentNumber(List<Deployment> liste)         // new version need test
        {
            if (liste is null) return 0;
            return liste.Count();
        }
        public void SaveAllLists(object[] lists)
        {
            SaveListToFile<Deployment>((List<Deployment>)lists[0], _paths[0]);
            SaveListToFile<User>((List<User>)lists[1], _paths[1]);
            SaveListToFile<Vehicle>((List<Vehicle>)lists[2], _paths[2]);
            SaveListToFile<Resources>((List<Resources>)lists[3], _paths[3]);
            SaveListToFile<FireFighter>((List<FireFighter>)lists[4], _paths[4]);
        }
    }
}
