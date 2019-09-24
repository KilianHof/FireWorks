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
        public FileIO(IUserLayer tui) { _t = tui; }

        public string ReadLine(string path, int line)
        {
            if (File.Exists(path))
            {

                if (line > 0)
                {
                    return File.ReadLines(path).Skip(line - 1).Take(1).First();
                }

                _t.Display("cannot read line \"0\" or negative." + "\n");
                return "";
            }
            _t.Display("cannot read file: " + path + "\n");
            return "";
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
    }
}
