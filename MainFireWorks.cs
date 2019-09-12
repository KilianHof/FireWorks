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
        public static void Main(string[] args)
        {




            Human CurrentUser = new Human();
            var SCurrentUser = Authenticator.LogIn();
            Console.WriteLine(CurrentUser);

            CurrentUser = JsonConvert.DeserializeObject<Human>(SCurrentUser);

            Authenticator.StatusCheck(CurrentUser);

           

        }
    }
}
