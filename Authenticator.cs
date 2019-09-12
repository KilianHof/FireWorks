using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class Authenticator
    {
        public static object StatusCheck(Human CurrentUser)
        {
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
                return CurrentUser;
            }

            if (CurrentUser.Status == 2)
            {
                Console.WriteLine("Admin mit ID " + CurrentUser.ID + " erfolgreich eingeloggt.");
                Console.ReadLine();
                return CurrentUser;
            }
            else
            {
                Console.WriteLine("Nutzerstatus Invalid.");
                Console.ReadLine();
                System.Environment.Exit(1);
                return CurrentUser;
            }

        }


            public static object LogIn(Human CurrentUser)
        {

            Console.Write("Please enter PIN:");
            String PINenter = Console.ReadLine();
            PINenter = "\"PIN\":\"" + PINenter + "\",\""; //Absicherung
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

            return CurrentUser;
        }
    }
}
