using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FireWorks
{
    class FileWriter
    {
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
    }
}
