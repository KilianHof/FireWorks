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
        public void Run()
        {













            TUI _t = new TUI();
            FileIO _filer = new FileIO(_t);
            _filer.CheckForFiles();
            Authenticator auth = new Authenticator(_t, _filer);
            object[] lists = _filer.ReadAllFiles();
            UserFunctions _uf = new UserFunctions(_t, lists);

            if (Globals.debug)
            {
                int testsize = 100000;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Deployment test;
                List<Deployment> l = (List<Deployment>)lists[0];
                test = new Deployment("here", new Vehicle[0], new Resources[0], new FireFighter[0], "hi", (l.Count + 1));
                List<Deployment> list = (List<Deployment>)lists[0];
                for (int i = 1; i < testsize+1; i++)
                {
                    list.Add(test);
                    if (i % (testsize/20) == 0)
                        _t.Display((i / (testsize/100)).ToString() + "%\n");
                }
                for (int i = 1; i < testsize+1; i++)
                {
                    list.Remove(test);
                    if (i % (testsize/20) == 0)
                        _t.Display((i / (testsize / 100)).ToString() + "%\n");
                }
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                _t.Display(ts.ToString() + "\n");
            


            
                LLRBTree<int, Deployment> tree = new LLRBTree<int, Deployment>();
                stopWatch = new Stopwatch();
                stopWatch.Start();
                test = new Deployment("here", new Vehicle[0], new Resources[0], new FireFighter[0], "hi", (l.Count+1));
                for (int i = 1; i < testsize+1; i++)
                {
                    tree.Insert(i, test);

                    if (i % (testsize/20) == 0)
                        _t.Display((i / (testsize/100)).ToString() + "%\n");
                }
    
                for (int i = 1; i < testsize + 1; i++)
                {
                    tree.Delete(i);
                    if (i % (testsize / 20) == 0)
                        _t.Display((i / (testsize / 100)).ToString() + "%\n");
                }
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                ts = stopWatch.Elapsed;
                _t.Display(ts.ToString() + "\n");
            }

            bool loop = true;
            string[] user = auth.LogIn();
            while (loop)
            {
                _uf.Routine(user[0]);
                _t.Display("Continue?");
                loop = _t.GetBool();
            }
            _t.Display("Saving data to files\n");
            _filer.SaveAllLists(lists);
        }
    }
    public static class Globals
    {
        public const bool debug = false;
    }
}