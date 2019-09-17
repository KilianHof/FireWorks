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

        public static void UserLock()
        {
            String SLCurrentUser = "";
            Console.Write("User-ID:");
            String IDenter = Console.ReadLine();
            IDenter = "\"ID\":" + IDenter; //Absicherung
            using (StreamReader UserText = new StreamReader(@"C:/Users/khof/Desktop/Users.txt"))
                while (SLCurrentUser.IndexOf(IDenter) == -1)
                {


                    SLCurrentUser = UserText.ReadLine();

                    if (SLCurrentUser == null)
                    {
                        Console.WriteLine("Invalid ID.");
                        Console.ReadLine();
                        System.Environment.Exit(1);
                    }
                }

            Human User;
            User = JsonConvert.DeserializeObject<Human>(SLCurrentUser);
            Console.WriteLine("Enter new Status: 0 (locked) , 1 (User) , 2 (Admin).");
            string answer = Console.ReadLine();
            int.TryParse(answer, out int answerint);
            if (answerint == 0) User.Status = 0;
            if (answerint == 1) User.Status = 1;
            if (answerint == 2) User.Status = 2;
            answer = JsonConvert.SerializeObject(User);
            ObjectWriter.LineChanger(answer, "C:/Users/khof/Desktop/Users.txt", User.ID);
        }
    }
}
