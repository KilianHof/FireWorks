using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    public class Authenticator
    {
        public static object StatusCheck(Human CurrentUser)
        {


            Console.WriteLine(CurrentUser.Status);


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


            public static String LogIn()
        {
            String SCurrentUser = "";
            Human CurrentUser = new Human();
            Console.Write("Please enter PIN:");
            String PINenter = Console.ReadLine();
            PINenter = "\"PIN\":\"" + PINenter + "\",\""; //Absicherung
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Users.txt"))
                while (SCurrentUser.IndexOf(PINenter) == -1)
                {


                    SCurrentUser = UserText.ReadLine();

                    if (SCurrentUser == null)
                    {
                        Console.WriteLine("Falscher PIN.");
                        Console.ReadLine();
                        System.Environment.Exit(1);
                    }
                }

            Console.WriteLine(CurrentUser.Status);


            Console.WriteLine(CurrentUser.Status);

            return SCurrentUser;
        }
    }
}
