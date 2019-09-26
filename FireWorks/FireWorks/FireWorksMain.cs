using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBTREE;





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
                int testsize = 10000000;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Deployment test;
                test = new Deployment("here", new Vehicle[0], new Resources[0], new FireFighter[0], "hi", _filer.GetLastDeploymentNumber());
                List<Deployment> list = (List<Deployment>)lists[0];
                for (int i = 1; i < testsize+1; i++)
                {
                    list.Add(test);
                    if (i % (testsize/10) == 0)
                        _t.Display((i / (testsize/100)).ToString() + "%\n");
                }
                for (int i = 1; i < testsize+1; i++)
                {
                    list.Remove(test);
                    if (i % (testsize/10) == 0)
                        _t.Display((i / (testsize / 100)).ToString() + "%\n");
                }
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                _t.Display(ts.ToString() + "\n");
            }


            if (debug)
            {
                int testsize = 10000000;
                LLRBTree<int, Deployment> tree = new LLRBTree<int, Deployment>();
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                    Deployment test;
                    test = new Deployment("here", new Vehicle[0], new Resources[0], new FireFighter[0], "hi", _filer.GetLastDeploymentNumber());
                for (int i = 1; i < testsize+1; i++)
                {
                    tree.Insert(i, test);

                    if (i % (testsize/10) == 0)
                        _t.Display((i / (testsize/100)).ToString() + "%\n");
                }
    
                for (int i = 1; i < testsize + 1; i++)
                {
                    tree.Delete(i);
                    if (i % (testsize / 10) == 0)
                        _t.Display((i / (testsize / 100)).ToString() + "%\n");
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
