using System.Collections.Generic;

namespace FireWorks
{
    public interface IDataLayer
    {
        string[] GetListOfUsers();
        List<T> ReadFile<T>();
        void SaveListToFile<T>(List<T> liste);
        void SaveAllLists(object[] lists);
    }
}
