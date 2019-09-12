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
    class FileIO
    {
        /// <summary>
        /// Writing an object as JSON to a file. If it doesnt exist its being created and then written to.
        /// </summary>
        /// <param name="o"> The object you want to write into a file.</param>
        /// <param name="path">The file that is being written to.</param>
        public static void WriteToFile(object o, string path)
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
                Console.WriteLine("Couldnt read and/or create the File.(" + path + ")");
                Console.WriteLine(e.Message);
            }
        }
        public static Deployment ReadObjectFromFile(string path,int line)
        {
            string json = File.ReadLines(path).Skip(line - 1).Take(1).First();
            Deployment o = (Deployment) JSONConverter.JSONToObject(json);
            return o;
        }
    }
}
