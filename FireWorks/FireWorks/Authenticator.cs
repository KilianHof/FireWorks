using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    public delegate void OutputEvent(string str);
    public delegate bool BoolInputEvent();
    /// <summary>
    /// Authenticator is being used to verify and Login users.
    /// </summary>
    class Authenticator
    {
        public Authenticator() { }
        private int _pinLength = 4;

        public event OutputEvent NeedOutput;
        public event BoolInputEvent NeedBoolInput;
        /// <summary>
        /// Calls helper functions to ask for a PIN
        /// </summary>
        /// <returns>True for a successful login attempt.</returns>

        public bool LogIn()
        {
            bool IsValidSuccess = CheckPIN();
            if (IsValidSuccess)
            {
                return true;
            }
            else
            {
                NeedOutput("Log in attempt failed. Try again? (y/n)");
                if (NeedBoolInput())
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
        public bool CheckPIN()
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
                    NeedOutput("Couldnt read: PIN data file.");
                    NeedOutput(e.Message);
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
        public bool ValidateInput(string Input)
        {
            if (!(Input.Length == _pinLength))
                return false;
            for (int i = 0; i < Input.Length; i++)
            {
                if (!char.IsDigit(Input[i]))
                    return false;
            }
            return true;
        }
    }
}