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
        void WriteObject(object o, string path);
    }
}
