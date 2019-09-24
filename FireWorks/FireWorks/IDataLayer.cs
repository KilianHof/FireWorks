using System.Collections.Generic;

namespace FireWorks
{
    interface IDataLayer
    {
        string[] CheckUserStatus();
        List<T> ReadAll<T>(string path);
        void SaveListToFile<T>(List<T> liste, string path);
        void SaveAllLists(object[] lists);
        int GetLastDeploymentNumber(List<Deployment> liste);
    }
}
