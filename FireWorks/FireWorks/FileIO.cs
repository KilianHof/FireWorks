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
    class FileIO : IDataLayer
    {
        /// <summary>
        /// Writing an object as JSON to a file. If it doesnt exist its being created and then written to.
        /// </summary>
        /// <param name="o"> The object you want to write into a file.</param>
        /// <param name="path">The file that is being written to.</param>
        public FileIO() { }
        public event OutputEvent NeedOutput;

        public void WriteObject(object o, string path)
        {
            string Json = JSONConverter.ObjectToJSON(o) + Environment.NewLine;
            try
            {
                if (File.Exists(path))
                {
                    File.AppendAllText(path, Json);
                }
                else
                {
                    using (FileStream Fs = File.Create(path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(Json);
                        Fs.Write(info, 0, info.Length);
                    }
                }
            }
            catch (IOException e)
            {
                NeedOutput("Couldnt read and/or create the File.(" + path + ")");
                NeedOutput(e.Message);
            }
        }
        /// <summary>
        /// Reads a specific line and turns it into Deployment object 
        /// </summary>
        /// <param name="path">Where the file is located.</param>
        /// <param name="line">The line to be read.</param>
        /// <returns>Deployment object from specified line.</returns>
        //public Deployment ReadObject(string path, int line)
        //{
        //    if (File.Exists(path))
        //    {
        //        if ((line > 0))
        //        {
        //            string json = File.ReadLines(path).Skip(line - 1).Take(1).First();
        //            Deployment o = (Deployment)JSONConverter.JSONToDeployment(json);
        //            return o;
        //        }
        //        NeedOutput("cannot read line \"0\" or negative.");
        //        return new Deployment();
        //    }
        //    NeedOutput("cannot read file: " + path);
        //    return new Deployment();
        //}
        public object ReadObject<T>(string path, int line)
        {
            if (File.Exists(path))
            {
                if ((line > 0))
                {
                    string json = File.ReadLines(path).Skip(line - 1).Take(1).First();
                    object o = JSONConverter.JSONToGeneric<T>(json);
                    return o;
                }
                NeedOutput("cannot read line \"0\" or negative." + "\n");
                return new object();
            }
            NeedOutput("cannot read file: " + path + "\n");
            return new object();
        }
        public void DeleteObject(string path, int line)
        {
            if (File.Exists(path))
            {
                if ((line > 0))
                {
                    List<string> quotelist = File.ReadAllLines(path).ToList();
                    quotelist.RemoveAt(line-1);
                    File.WriteAllLines(path, quotelist.ToArray());
                    //string json = File.ReadLines(path).Skip(line - 1).Take(1).First();
                    //object o = JSONConverter.JSONToGeneric<T>(json);
                }
                else
                    NeedOutput("cannot read line \"0\" or negative." + "\n");
            }
            else
                NeedOutput("cannot read file: " + path + "\n");
        }
        public string ReadLine(string path, int line)
        {
            if (File.Exists(path))
            {

                if (line > 0)
                {
                    return File.ReadLines(path).Skip(line - 1).Take(1).First();
                }

                NeedOutput("cannot read line \"0\" or negative." + "\n");
                return "";
            }
            NeedOutput("cannot read file: " + path + "\n");
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
            NeedOutput("cannot read file: " + path + "\n");
            return tmp;
            // NeedOutput("cannot read line \"0\" or negative");
            // return "";
        }
        public int GetLastDeploymentNumber(string path)
        {
            if (File.Exists(path))
            {
                int lineCount = File.ReadLines(path).Count();
                Deployment last = (Deployment)ReadObject<Deployment>(path, lineCount);
                return last.Number;
            }
            NeedOutput("cannot read file: " + path +"\n");
            return 0;
        }
    }
}
