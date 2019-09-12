using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    interface IUserLayer
    {
        void Display(string str);
        int GetInt();

        string GetString();
        bool GetBool();



    }
}
