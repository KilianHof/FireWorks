using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    /// <summary>
    /// Authenticator is being used to verify and Login users.
    /// </summary>
    class Authenticator
    {   
        /// <summary>
        /// Calls helper functions to ask for a PIN
        /// </summary>
        /// <returns>True for a successful login attempt.</returns>

        public static bool LogIn()
        {
            bool IsValidSuccess = CheckPIN();
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
        /// <summary>
        /// Prompts the user for input.
        /// Calls ValidateInput() to check for 4 digits.
        /// Then looks up Entries in PINS.txt
        /// </summary>
        /// <returns>Returns True for a Valid PIN that is found in PINS.txt</returns>
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
        /// <summary>
        /// Checks for Length =4.
        /// And if the string only contains digits.
        /// </summary>
        /// <param name="Input"></param>
        /// <returns>Returns true for a Valid format.</returns>
        public static bool ValidateInput(string Input)
        {
            if (!(Input.Length == 4)) 
                return false;
            for (int i = 0; i < Input.Length; i++)
            {
                if (!char.IsDigit(Input[i]))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Helper function to promt the user for yes or no
        /// </summary>
        /// <returns>True for yes.</returns>
        public static bool GetYesNo()
        {
            String str = Console.ReadLine();
            if ((str == "y") || (str == "Y") || (str == "Yes") || (str == "yes"))
                return true;
            else
                return false;
        }
    }

}