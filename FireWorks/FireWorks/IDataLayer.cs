using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    interface IDataLayer
    {
        string ReadLine(string path, int line);
        List<T> ReadAll<T>(string path);
        //void WriteObject(object o, string path);
        //object ReadObject<T>(string path,int line);
        void SaveListToFile<T>(List<T> liste, string path);

        int GetLastDeploymentNumber(List<Deployment> liste);

    }
}
