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




            if (CurrentUser.Status == 0)
            {
                Console.WriteLine("This account is locked.");
                Console.ReadLine();
                System.Environment.Exit(1);
            }

            if (CurrentUser.Status == 1)
            {
                Console.WriteLine("User with ID " + CurrentUser.ID + " has logged in.");
                Console.WriteLine("Greetings, " + CurrentUser.FName + " " + CurrentUser.LName + ".");
                Console.ReadLine();
                return CurrentUser;
            }

            if (CurrentUser.Status == 2)
            {
                Console.WriteLine("Admin with ID " + CurrentUser.ID + " has logged in.");
                Console.WriteLine("Greetings, " + CurrentUser.FName + " " + CurrentUser.LName + ".");
                Console.ReadLine();
                return CurrentUser;
            }

            else
            {
                Console.WriteLine("UserStatus Invalid.");
                Console.ReadLine();
                System.Environment.Exit(1);
                return CurrentUser;
            }

        }


        public static String LogIn()
        {
            String SCurrentUser = "";
            Console.Write("PIN:");
            String PINenter = Console.ReadLine();
            PINenter = "\"PIN\":\"" + PINenter + "\",\""; //Absicherung
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Users.txt"))
                while (SCurrentUser.IndexOf(PINenter) == -1)
                {


                    SCurrentUser = UserText.ReadLine();

                    if (SCurrentUser == null)
                    {
                        Console.WriteLine("Invalid PIN.");
                        Console.ReadLine();
                        System.Environment.Exit(1);
                    }
                }


            return SCurrentUser;
        }
    }
}
