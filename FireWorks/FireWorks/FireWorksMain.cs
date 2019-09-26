using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;






namespace FireWorks
{
    class FireWorksMain
    {
        
        public FireWorksMain() { }
        public const bool debug = false;
        public void Run()
        {
            TUI _t = new TUI();
            FileIO _filer = new FileIO(_t);
            _filer.CheckForFiles();
            Authenticator auth = new Authenticator(_t, _filer);
            object[] lists = _filer.ReadAllFiles();
            UserFunctions _uf = new UserFunctions(_t, _filer, lists);


            if (debug)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Deployment test;
                for (int i = 1; i < 1001; i++)
                {
                    test = new Deployment("here", new Vehicle[0], new Resources[0], new FireFighter[0], "hi", _filer.GetLastDeploymentNumber());
                    List<Deployment> testing = (List<Deployment>)lists[0];
                    testing.Add(test);
                    _filer.SaveListToFile(testing);
                    if (i % 100 == 0)
                        _t.Display((i / 10).ToString() + "%\n");
                }
                for (int i = 1; i < 1001; i++)
                {
                    List<Deployment> testing = (List<Deployment>)lists[0];
                    testing.RemoveAt(_filer.GetLastDeploymentNumber() - 1);
                    _filer.SaveListToFile(testing);
                    if (i % 100 == 0)
                        _t.Display((i / 10).ToString() + "%\n");
                }
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                _t.Display(ts.ToString() + "\n");
            }



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
