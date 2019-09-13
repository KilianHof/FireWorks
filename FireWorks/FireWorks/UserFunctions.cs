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

        private static string[] _path;  // Deploy Employ Vehicles Res
        public UserFunctions(TUI t, FileIO filer, string[] path) { _t = t; _filer = filer; _path = path; }
        public void Foo(string mode)
        {
            string selection;
            switch (mode)
            {
                case "ADMIN":
                    ShowAdminOptions();
                    AdminMode(_t.GetString());
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
                    int dataSet = Valid(_t.GetInt(), 1, 3);
                    _t.Display("(1)View\n" +
                               "(2)New\n" +
                               "(3)Edit\n" +
                               "(4)Delete\n");
                    int mode = Valid(_t.GetInt(), 1, 4);

                    select(dataSet, mode);
                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }
        }
        public void select(int dataSet, int mode)
        {
            switch (dataSet)
            {
                case 1:
                    List<Human> emp = GetList<Human>(dataSet);
                    ProcessList<Human>(mode, emp, dataSet);
                    break;
                case 2:

                    List<Vehicle> veh = GetList<Vehicle>(dataSet);
                    ProcessList<Vehicle>(mode, veh, dataSet);
                    break;
                case 3:

                    List<Resources> res = GetList<Resources>(dataSet);
                    ProcessList<Resources>(mode, res, dataSet);
                    break;
            }
        }
        public void ProcessList<T>(int mode, List<T> liste, int dataSet)
        {
            int c = 1;
            switch (mode)
            {
                case 1:
                    c = 1;
                    foreach (var ele in liste)
                    {
                        _t.Display("(" + c + ") " + ele.ToString() + "\n");
                        c++;
                    }
                    break;
                case 2:
                    _t.Display("Generate new." + "\n");

                    switch (dataSet)
                    {
                        case 1:
                            User usr = GenerateHuman();
                            _filer.WriteObject(usr, _path[dataSet]);
                            break;
                        case 2:
                            Vehicle veh = new Vehicle("hi",1,2);
                            _filer.WriteObject(veh, _path[dataSet]);
                            break;
                        case 3:
                            Resources res = new Resources("hi", 1);
                            _filer.WriteObject(res, _path[dataSet]);
                            break;
                    }
                    break;
                case 3:
                    _t.Display("Edit." + "\n");
                    c = 1;
                    foreach (var ele in liste)
                    {
                        _t.Display("(" + c + ") " + ele.ToString() + "\n");
                        c++;
                    }

                    switch (dataSet)
                    {
                        case 1:
                            User usr = GenerateHuman();
                            _filer.WriteObject(usr, _path[dataSet]);
                            break;
                        case 2:
                            Vehicle veh = new Vehicle("hi", 1, 2);
                            _filer.WriteObject(veh, _path[dataSet]);
                            break;
                        case 3:
                            Resources res = new Resources("hi", 1);
                            _filer.WriteObject(res, _path[dataSet]);
                            break;
                    }
                    break;
                    //User tmp = (User)_filer.ReadObject<User>(_path[dataSet], Valid(_t.GetInt(), 1, c - 1));


                case 4:
                    _t.Display("Delete." + "\n");
                    c = 1;
                    foreach (var ele in liste)
                    {
                        _t.Display("(" + c + ") " + ele.ToString() + "\n");
                        c++;
                    }
                    _filer.DeleteObject(_path[dataSet], Valid(_t.GetInt(), 1, c - 1));
                    break;
            }
        }
        public User GenerateHuman()
        {
            return new User("yeah", "boiiii", 2, "USER", "15947562");
        }



        public List<T> GetList<T>(int dataSet)
        {
            List<T> employees = _filer.ReadAll<T>(_path[dataSet]);
            return employees;
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
                       "-q\tQuits\n"
                       );
        }
    }
}
