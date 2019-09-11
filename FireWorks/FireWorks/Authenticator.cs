using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class Authenticator
    {
        public Authenticator()
        {

        }
        public bool LogIn()
        {
            bool IsValidSuccess = CheckPIN();
            Console.WriteLine(IsValidSuccess);
            if (IsValidSuccess)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Log in attempt failed. Try again? (y/n)");
                if (GetYesNo())
                    return LogIn();
                else
                    System.Environment.Exit(1);
                return false;
            }
        }
        public static bool CheckPIN()
        {
            bool IsSuccess = false;
            string Input = Console.ReadLine();
            if (ValidateInput(Input))
            {
                try
                {
                    using (StreamReader Sr = new StreamReader(@"C:\FireWorks\PINS.txt"))
                    {
                        while (!Sr.EndOfStream)
                        {
                            string Line = Sr.ReadLine();
                            if (Input.GetHashCode().ToString() == Line)
                                IsSuccess = true;
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Couldnt read: PIN data file.");
                    Console.WriteLine(e.Message);
                }
                return IsSuccess;
            }
            else
            {
                return IsSuccess;
            }
        }
        public static bool ValidateInput(string Input)
        {
            bool IsValid = true;
            if (!(Input.Length == 4))
                IsValid = false;
            for (int i = 0; i < Input.Length; i++)
            {
                if (!char.IsDigit(Input[i]))
                {
                    IsValid = false;
                }
            }
            return IsValid;
        }
        public static bool GetYesNo()
        {
            String str = Console.ReadLine();
            if ((str == "y") || (str == "Y"))
                return true;
            else
                return false;
        }
    }

}