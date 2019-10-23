using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;
namespace FireWorks
{
    /// <summary>
    /// Authenticator is being used to verify and Login users.
    /// A List of users is read from file.
    /// The entered PIN gets compared with each pin in the Users file.
    /// If a match is found the status of the user is returned as well as his name.
    /// </summary>
    public class Authenticator
    {
        private readonly IUserLayer _t;
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IDataLayer filer;
#pragma warning restore IDE0052 // Remove unread private members
        public Authenticator(IUserLayer ux, IDataLayer fil) { _t = ux; filer = fil; }
        private const int _pinLength = 4;
        /// <summary>
        /// Calls helper functions to ask for a PIN.
        /// </summary>
        /// <returns>
        /// String array[2] containing Status and User name for a successful login attempt.
        /// returns error in boths slots of the array for invalid inputs or any error while reading file.
        /// </returns>

        public string[] LogIn()
        {
            _t.Display("Type your PIN:<br />");
            string[] result = CheckPIN();
            if (!(result[0] == "error") && !(result[1] == "error"))
            {
                _t.Display("Login attempt successful! Logged in as: " + result[0] + " : " + result[1] + "<br />");
            }
            else
            {
                _t.Display("Login attempt failed. Try again?" + "<br />");
                if (_t.GetBool())
                    return LogIn();
            }
            return result;
        }
        /// <summary>
        /// Prompts the user for input.
        /// Calls ValidateInput() to check for 4 digits.
        /// Then looks up Entries in PINS.txt
        /// </summary>
        /// <returns>Returns True for a Valid PIN that is found in PINS.txt</returns>
        private string[] CheckPIN()
        {
            string Input = _t.GetString();
            if (IsValidInput(Input))
            {
                {
                    List<User> Employs;
                    using (var context = new thistimeitwillworkforsure.DBContext())
                    {
                        Employs = context.Users.ToList();
                    }
                    List<User> tmpUsers = new List<User>();
                    foreach (var User in Employs)
                    {
                        tmpUsers.Add(JsonConvert.DeserializeObject<User>(User.JSON));
                    }
                    bool matchingPIN = tmpUsers.Any(User => User.PIN == Input);
                    try
                    {
                    User currentuser = tmpUsers.Single(x => x.PIN == Input);
                        //from User in Employs.Single
                        //where User.PIN == Input
                        //select User;
                    return new string[2] { currentuser.Status, currentuser.FirstName };
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            else
            {
                _t.Display("Invalid Input<br />");
            }
            return new string[2] { "error", "error" };
        }
        /// <summary>
        /// Checks for Length = _pinLength.
        /// Also checks if the string only contains digits.
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns>Returns true for a valid format.</returns>
        public bool IsValidInput(string toCheck)
        {
            if (!(toCheck.Length == _pinLength))
                return false;
            for (int i = 0; i < toCheck.Length; i++)
            {
                if (!char.IsDigit(toCheck[i]))
                    return false;
            }
            return true;
        }
    }
}