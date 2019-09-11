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

            Console.Write("Please enter PIN:");
            String PINenter = Console.ReadLine();
            PINenter = "\"PIN\":\"" + PINenter + "\",\""; //Absicherung
            Human CurrentUser = new Human();
                string read = "";
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Users.txt"))
                while (read.IndexOf(PINenter) == -1)
            {

                    
                read = UserText.ReadLine();

                    if (read == null)
                    {
                        Console.WriteLine("Falscher PIN.");
                        Console.ReadLine();
                        System.Environment.Exit(1);
                    }
                }

            Console.WriteLine(read);
            Console.ReadLine();

            CurrentUser = JsonConvert.DeserializeObject<Human>(read);

            if (CurrentUser.Status == 0)
            {
                Console.WriteLine("Nutzerkonto gesperrt.");
                Console.ReadLine();
                System.Environment.Exit(1);
            }

            if (CurrentUser.Status == 1)
            {
                Console.WriteLine("Nutzer mit ID " + CurrentUser.ID + " erfolgreich eingeloggt.");
                Console.ReadLine();


                System.Environment.Exit(1);
            }

            if (CurrentUser.Status == 2)
            {
                Console.WriteLine("Admin mit ID " + CurrentUser.ID + " erfolgreich eingeloggt.");
                Console.ReadLine();


                System.Environment.Exit(1);
            }



            //Human Master = new Human();
            //Master.PIN = "2019";
            //Master.ID = 1;
            //Master.Status = 1;

            //string Master1 = JsonConvert.SerializeObject(Master);
            //Console.WriteLine(Master1);

            //Human Master2 = JsonConvert.DeserializeObject<Human>(Master1);
            //Console.WriteLine(Master2.PIN);


            //using (StreamWriter writer = new StreamWriter(@"C:/Users/khof/Desktop/Users.txt"))
            //{

            //        writer.WriteLine(Master1);

            //}


            //using (StreamReader reader = new StreamReader(@"C:/Users/khof/Desktop/Users.txt"))
            //{

            //    string read;
            //        read = reader.ReadLine();

            //    Console.WriteLine(read);


            //}
            //Console.ReadLine();

        }
    }
}
