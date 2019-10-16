
namespace FireWorks
{
    /// <summary>
    /// The Interface for communication with the User
    /// contains methods to call vor strings ints and bools
    /// aswell as a method to display information.
    /// </summary>
    public interface IUserLayer
    {
        void Display(string str);
        string GetString();
        int GetInt();
        bool GetBool();
    }
}
