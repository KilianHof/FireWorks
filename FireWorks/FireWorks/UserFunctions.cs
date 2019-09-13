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
        public UserFunctions(TUI t,FileIO filer, string[] path) { _t = t; _filer = filer; _path = path; }
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
                    int dataSet = Valid(_t.GetInt(), 1, 3);
                    _t.Display("(1)View\n" +
                               "(2)New\n" +
                               "(3)Edit\n" +
                               "(4)Delete\n");
                    int mode = Valid(_t.GetInt(), 1, 4);
                    
                    ProcessList(mode,GetList(dataSet));
                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }
        }
        public void ProcessList(int mode,List<Human> employees)
        {
            switch (mode)
            {
                case 1:
                    int c = 1;
                    foreach (var ele in employees)
                    {
                        _t.Display("("+c+") "+ele.ToString() + "\n");
                            c++;
                    }
                    break;
                case 2:
                    _t.Display("Generate new Employee." + "\n");

                    User user = new User("yeah", "boiiii", 2, "USER", "15947562");
                    _filer.WriteObject(user, _path[1]);
                    break;
                case 3:
                    _t.Display("Edit Employee." + "\n");
                    break;
                case 4:
                    _t.Display("Delete Employee." + "\n");

                    break;
            }
        }
        public List<Human> GetList(int file)
        {
            //switch (file)
            //{
            //    case 1:
                    List<Human> employees = _filer.ReadAll<Human>(_path[1]);
                    return employees;
                //case 2:
                //    List<Vehicle> vehicles = _filer.ReadAll<Vehicle>(_path[2]);
                //   // return vehicles;
                //case 3:
                //    List<Resources> resources = _filer.ReadAll<Resources>(_path[3]);
                //  //  return resources;
            //}
          //  return new List<object>();
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
