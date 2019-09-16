using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FireWorks
{

    class ObjectWriter
    {
        public static void WriteObject(object Text, string FilePath)
        {
            string Json = JsonConvert.SerializeObject(Text) + Environment.NewLine;
            try
            {
                    File.AppendAllText(FilePath, Json);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error while handling file.");
                Console.WriteLine(e.Message);
            }
        }

    }
}
