using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;






namespace FireWorks
{
    class FireWorksMain
    {
        private readonly string[] _paths;
        public FireWorksMain(string[] paths) { _paths = paths; }
        public void Run()
        {

            TUI _t = new TUI();
            FileIO _filer = new FileIO(_t);
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
            _t.Display("type your PIN\n");
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
