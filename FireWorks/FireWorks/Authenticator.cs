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
        private TUI _t;
        private FileIO filer;
        private string _path;
        public Authenticator( TUI tui,FileIO fil,string path) { _t = tui; filer = fil; _path = path; }
        private const int _pinLength = 4;

        /// <summary>
        /// Calls helper functions to ask for a PIN
        /// </summary>
        /// <returns>True for a successful login attempt.</returns>

        public string LogIn()
        {
            string result = CheckPIN();
            if (!(result == "No matching PIN found") && !(result == "Invalid Input"))
            {
                _t.Display("Login attempt successful! Logged in as: " + result + "\n");
                return result;
            }
            else
            {
                _t.Display("Login attempt failed: " + result + ". Try again?" + "\n");
                if (_t.GetBool())
                    return LogIn();
                else
                    System.Environment.Exit(1);
                return "LOCKED";
            }
        }
        /// <summary>
        /// Prompts the user for input.
        /// Calls ValidateInput() to check for 4 digits.
        /// Then looks up Entries in PINS.txt
        /// </summary>
        /// <returns>Returns True for a Valid PIN that is found in PINS.txt</returns>
        public string CheckPIN()
        {
            string Input = _t.GetString();
            if (ValidateInput(Input))
            {
                int lineCount = File.ReadLines(_path).Count();
                for (int i = 1; i <= lineCount; i++)
                {
                    User tmp = JSONConverter.JSONToGeneric<User>(filer.ReadLine(_path, i));
                    //tmp = JSONConverter.JSONToUser(filer.ReadLine(_path, i));
                    if (tmp.GetType() == typeof(User))
                    {
                        if (Input.GetHashCode().ToString() == tmp.PIN)
                        {
                            return tmp.Status;
                        }
                    }
                }
                return "No matching PIN found";
            }
            else
            {
                return "Invalid Input";
            }
        }
        /// <summary>
        /// Checks for Length = _pinLength.
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