using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class UserFunctions

    {
        private static TUI _t;

        private static FileIO _filer;

        private static string _path;
        public UserFunctions(TUI t,FileIO filer, string path) { _t = t; _filer = filer; _path = path; }
        public void Foo(string mode)
        {
            string selection;
            switch (mode)
            {
                case "ADMIN":
                    ShowAdminOptions();
                    AdminMode(_t.GetString());
                    break;
                case "USER":
                    ShowUserOptions();
                    selection = _t.GetString();
                    break;
                case "LOCKED":
                    System.Environment.Exit(1);
                    break;
            }
        }
    public int Valid(int num, int from, int to)
        {
            while (num < from || num > to)
            {
                _t.Display("Invalid Input. Try again.\n");
                num = _t.GetInt();
            }
            return num;
        }
        public void AdminMode(string sel)
        {
            switch (sel)
            {
                case "-e":
                    _t.Display("Choose Base data to work with:\n" +
                               "(1)Employees\n" +
                               "(2)Vehicles\n" +
                               "(3)Resources\n");
                    int data = Valid(_t.GetInt(), 1, 3);
                    _t.Display("(1)New\n" +
                               "(2)Edit\n" +
                               "(3)Delete\n");
                    int mode = Valid(_t.GetInt(), 1, 3);
                    Editing(1, 1);
                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }


        }
        public void Editing(int file, int mode)
        {
            switch (file)
            {
                case 1:
                    List<Human> employees = _filer.ReadAll<Human>(_path);
                    foreach (var emp in employees)
                    {
                        _t.Display(emp.ToString() + "\n");
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
        public void ShowAdminOptions()
        {
            _t.Display("Options:\n" +
                       "-e\tEdit base data.\n" +
                       "-q\tQuit.\n");
        }
        public void ShowUserOptions()
        {
            _t.Display("Options:\n" +
                       "-v\tView Deployments.\n" +
                       "-u\tManage Users\n"
                       );
        }
    }
}
