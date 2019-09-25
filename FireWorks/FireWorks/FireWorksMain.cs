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
            string f = Directory.GetCurrentDirectory();
            f += @"\Files";
            if (!Directory.Exists(f))
            {
                Directory.CreateDirectory(f);
            }


            string[] _paths = new string[] {
                f +@"\Deployments.txt",
                f +@"\Employee.txt",
                f +@"\Vehicles.txt",
                f +@"\Resources.txt",
                f +@"\FireFighters.txt",
            };


            TUI _t = new TUI();
            FileIO _filer = new FileIO(_t,_paths);
            bool[] PathsExist = _filer.CheckForFiles();
            bool isFine = true;
            foreach (var item in PathsExist)
            {
                if (!item)
                    isFine = false;
            }
            if (!isFine)
            {
                _t.Display("Seems like some files dont exist. Do you wish to initialize missing files?(Admin PIN=0000)");
                if (_t.GetBool())
                {
                    _filer.Init(PathsExist);
                }
            }

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
