using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FireWorks
{
    class MainFireWorks
    {
        static void Main(string[] args)
        {


            Human Master = new Human();
            Master.PIN = "2019";
            Master.ID = 1;
            Master.Status = 1;

            string Master1 = JsonConvert.SerializeObject(Master);
            Console.WriteLine(Master1);

            Human Master2 = JsonConvert.DeserializeObject<Human>(Master1);
            Console.WriteLine(Master2.PIN);


            using (StreamWriter writer = new StreamWriter(@"C:/Users/khof/Desktop/Users.txt"))
            {
                
                    writer.WriteLine(Master1);

            }

            
            using (StreamReader reader = new StreamReader(@"C:/Users/khof/Desktop/Users.txt"))
            {

                string read;
                    read = reader.ReadLine();

                Console.WriteLine(read);


            }
            Console.ReadLine();
            
        }
    }
}
