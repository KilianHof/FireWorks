
namespace FireWorks
{
    /// <summary>
    /// Authenticator is being used to verify and Login users.
    /// </summary>
    class Authenticator
    {
        private IUserLayer _t;
        private IDataLayer filer;
        public Authenticator(IUserLayer tui, IDataLayer fil, string path) { _t = tui; filer = fil; }
        private const int _pinLength = 4;
        /// <summary>
        /// Calls helper functions to ask for a PIN
        /// </summary>
        /// <returns>True for a successful login attempt.</returns>

        public string[] LogIn()
        {
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
        public string[] CheckPIN()
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
                            if (Input.GetHashCode().ToString() == tmp.PIN)
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
        /// <param name="Input"></param>
        /// <returns>Returns true for a Valid format.</returns>
        public bool IsValidInput(string Input)
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