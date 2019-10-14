using System.Collections.Generic;

namespace FireWorks
{
    /// <summary>
    /// Interface for communication with Files/databases.
    /// 
    /// Contains methods to save and read all lists.
    /// Aswell as a function to get a list of users.
    /// </summary>
    public interface IDataLayer
    {
        void SaveAllLists(object[] lists);
        object[] ReadAllLists();
    }
}
