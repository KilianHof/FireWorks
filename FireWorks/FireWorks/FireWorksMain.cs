using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;






namespace FireWorks
{
    class FireWorksMain
    {
        private string[] _paths;
        public FireWorksMain(string[] paths) {_paths = paths; }
        public void Run() 
        {

            TUI _t = new TUI();
            FileIO _filer = new FileIO(_t, _paths);
            Authenticator auth = new Authenticator(_t, _filer, _paths[1]);
            List<Deployment> Deploys = _filer.ReadAll<Deployment>(_paths[0]);
            List<User> Employs = _filer.ReadAll<User>(_paths[1]);
            List<Vehicle> Vehicles = _filer.ReadAll<Vehicle>(_paths[2]);
            List<Resources> Resources = _filer.ReadAll<Resources>(_paths[3]);
            List<FireFighter> FireFighters = _filer.ReadAll<FireFighter>(_paths[4]);
            object[] lists = { Deploys, Employs, Vehicles, Resources, FireFighters };


            UserFunctions _uf = new UserFunctions(_t, _filer, lists, _paths);



            bool loop = true;
            string[] str = auth.LogIn();
            while (loop)
            {
                _uf.Routine(str[0]);
                _filer.SaveAllLists(lists);
                _t.Display("Continue?");
                loop = _t.GetBool();
            }
        }
    }
}
