
namespace FireWorks
{
    /// <summary>
    /// Authenticator is being used to verify and Login users.
    /// A List of users is read from file.
    /// The entered PIN gets compared with each pin in the Users file.
    /// If a match is found the status of the user is returned as well as his name.
    /// </summary>
    class Authenticator
    {
        private readonly IUserLayer _t;
        private readonly IDataLayer filer;
        public Authenticator(IUserLayer tui, IDataLayer fil) { _t = tui; filer = fil; }
        private const int _pinLength = 4;
        /// <summary>
        /// Calls helper functions to ask for a PIN
        /// </summary>
        /// <returns>
        /// String array[2] containing Status and User name for a successful login attempt.
        /// returns error in boths slots of the array for invalid inputs or any error while reading file.
        /// </returns>

        public string[] LogIn()
        {
            _t.Display("Type your PIN:\n");
            string[] result = CheckPIN();
            if (!(result[0] == "error") && !(result[1] == "error"))
            {
                _t.Display("Login attempt successful! Logged in as: " + result[0] + " : " + result[1] + "\n");

            }
            else
            {
                _t.Display("Login attempt failed. Try again?" + "\n");
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
                    string[] users = filer.GetListOfUsers();
                    foreach (var item in users)
                    {
                        User tmp = JSONConverter.JSONToGeneric<User>(item);
                        if (tmp.GetType() == typeof(User))
                        {
                            if (Input.GetHashCode().ToString().Equals(tmp.PIN))
                            {
                                return new string[2] { tmp.Status, tmp.FirstName };
                            }
                        }
                    }
                }
                _t.Display("No matching PIN found\n");
            }
            else
            {
                _t.Display("Invalid Input\n");
            }
            return new string[2] { "error", "error" };
        }
        /// <summary>
        /// Checks for Length = _pinLength.
        /// And if the string only contains digits.
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns>Returns true for a Valid format.</returns>
        private bool IsValidInput(string toCheck)
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