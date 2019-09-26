using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;






namespace FireWorks
{
    class FireWorksMain
    {
        
        public FireWorksMain() { }
        public void Run()
        {
            TUI _t = new TUI();
            FileIO _filer = new FileIO(_t);
            _filer.CheckForFiles();
            

            Authenticator auth = new Authenticator(_t, _filer);
            object[] lists = _filer.ReadAllFiles();


            UserFunctions _uf = new UserFunctions(_t, _filer, lists);

            //_uf.AdminMode("-e");

            bool loop = true;
            string[] user = auth.LogIn();
            while (loop)
            {
                _uf.Routine(user[0]);
                _filer.SaveAllLists(lists);
                _t.Display("Continue?");
                loop = _t.GetBool();
            }
        }
    }
}
