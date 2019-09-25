using System.Collections.Generic;

namespace FireWorks
{
    interface IDataLayer
    {
        string[] GetListOfUsers();
        List<T> ReadAll<T>(string path);
        void SaveListToFile<T>(List<T> liste);
        void SaveAllLists(object[] lists);
        int GetLastDeploymentNumber(List<Deployment> liste);
    }
}
