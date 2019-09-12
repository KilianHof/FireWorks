using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FireWorks
{
    class MainFireWorks
    {
        static void Main(string[] args)
        {




            Human CurrentUser = new Human();

            Authenticator.LogIn(CurrentUser);

            Authenticator.StatusCheck(CurrentUser);

           

        }
    }
}
