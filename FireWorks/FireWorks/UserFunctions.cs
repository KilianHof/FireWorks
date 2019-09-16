using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class UserFunctions

    {
        public enum UserStates
        {
            USER = 1,
            ADMIN = 2,
            LOCKED = 3
        }
        private static TUI _t;

        private static FileIO _filer;

        private static string[] _path;  // Deploy Employ Vehicles Res FF
        private List<object>[] _allData;
        private List<Deployment> AllDeployments;
        private List<User> AllEmployees;
        private List<Vehicle> AllVehicles;
        private List<Resources> AllResources;
        private List<FireFighter> AllFireFighter;


        public UserFunctions(TUI t, FileIO filer, string[] path) { _t = t; _filer = filer; _path = path; Init();
            //_allData = new List<object>[]{ (object)AllDeployments, AllEmployees, AllVehicles, AllResources, AllFireFighter };
        }

        public List<T> ReadAll<T>(int dataSet)
        {
            return _filer.ReadAll<T>(_path[dataSet]);
        }
        public void Init()
        {
            AllDeployments = ReadAll<Deployment>(0);
            AllEmployees = ReadAll<User>(1);
            AllVehicles = ReadAll<Vehicle>(2);
            AllResources = ReadAll<Resources>(3);
            AllFireFighter = ReadAll<FireFighter>(4);
        }
        public List<Deployment> GetListDeployments() { return AllDeployments; }
        public List<User> GetListEmployees() { return AllEmployees; }
        public List<Vehicle> GetListVehicles() { return AllVehicles; }
        public List<Resources> GetListResources() { return AllResources; }
        public List<FireFighter> GetListFireFighter() { return AllFireFighter; }
        public void SaveAll()
        {
            _filer.SaveListToFile<Deployment>(AllDeployments, _path[0]);
            _filer.SaveListToFile<User>(AllEmployees, _path[1]);
            _filer.SaveListToFile<Vehicle>(AllVehicles, _path[2]);
            _filer.SaveListToFile<Resources>(AllResources, _path[3]);
            _filer.SaveListToFile<FireFighter>(AllFireFighter, _path[4]);
        }

        public void SaveSingle<T>(List<T> liste, int fileIndex)
        {
            _filer.SaveListToFile<T>(liste, _path[fileIndex]);
        }
        public void Routine(string mode)
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
                               "(3)Resources\n" +
                               "(4)Firefighters\n");
                    int dataSet = Valid(_t.GetInt(), 1, 4);
                    _t.Display("(1)View\n" +
                               "(2)New\n" +
                               "(3)Edit\n" +
                               "(4)Delete\n");
                    int mode = Valid(_t.GetInt(), 1, 4);

                    Select(dataSet, mode);

                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }
        }
        public void Select(int dataSet, int mode)
        {
            switch (dataSet)
            {
                case 1:
                    ProcessList(mode, GetListEmployees(), dataSet);
                    break;
                case 2:
                    ProcessList(mode, GetListVehicles(), dataSet);
                    break;
                case 3:
                    ProcessList(mode, GetListResources(), dataSet);
                    break;
                case 4:
                    ProcessList(mode, GetListFireFighter(), dataSet);
                    break;
            }
        }
        public void ProcessList<T>(int mode, List<T> liste, int dataSet)
        {
            T tmp;
            switch (mode)
            {
                case 1:
                    ViewList(liste);
                    break;
                case 2:
                    _t.Display("Generate new." + "\n");
                    switch (dataSet)
                    {
                        case 1:
                            liste.Add((T)(object)new User("yeah", "boiiii", 2, "USER", "15947562"));
                            break;
                        case 2:
                            liste.Add((T)(object)new Vehicle("hi", 1, 2));
                            break;
                        case 3:
                            liste.Add((T)(object)new Resources("hi", 1));
                            break;
                        case 4:
                            liste.Add((T)(object)new FireFighter("hi", "yo", 1));
                            break;
                    }
                    SaveSingle<T>(liste,dataSet);
                    break;
                case 3:
                    _t.Display("Edit." + "\n");
                    ViewList(liste);
                    int index = Valid(_t.GetInt(), 1, liste.Count());
                    index--;
                    tmp = liste.ElementAt(index);
                    liste.RemoveAt(index);
                    liste.Add(Edit(tmp));
                    SaveSingle<T>(liste, dataSet);
                    break;
                //User tmp = (User)_filer.ReadObject<User>(_path[dataSet], Valid(_t.GetInt(), 1, c - 1));

                case 4:
                    _t.Display("Delete." + "\n");
                    ViewList(liste);
                    liste.RemoveAt(Valid(_t.GetInt(), 1, liste.Count() - 1));                 //wirklich count-1??        .GetType()
                    SaveSingle<T>(liste, dataSet);
                    break;
            }
        }
        public T Edit<T>(T t)                                                       // ja ne is klar nice edit skillz
        {
            string Answer;

            if (t.GetType() == typeof(FireFighter))
            {
                _t.Display("Leave empty for no changes" + "\n");
                FireFighter tmp = (FireFighter)(object)t;

                _t.Display("Lastname: " + tmp.LastName + "\n");
                Answer = _t.GetString();
                if (Answer != "") tmp.LastName = Answer;

                _t.Display("Firstname: " + tmp.FirstName + "\n");
                Answer = _t.GetString();
                if (Answer != "") tmp.FirstName = Answer;

                _t.Display("ID: " + tmp.Id + "\n");
                if (Answer != "") tmp.Id = _t.GetInt();
            }
            if (t.GetType() == typeof(User))
            {
                _t.Display("Leave empty for no changes" + "\n");
                User tmp = (User)(object)t;

                _t.Display("Lastname: " + tmp.LastName + "\n");
                Answer = _t.GetString();
                if (Answer != "") tmp.LastName = Answer;

                _t.Display("Firstname: " + tmp.FirstName + "\n");
                Answer = _t.GetString();
                if (Answer != "") tmp.FirstName = Answer;

                _t.Display("ID: " + tmp.Id + "\n");
                if (Answer != "") tmp.Id = _t.GetInt();

                _t.Display("PIN(Hashed): " + tmp.PIN + "\n");
                Answer = _t.GetString();
                if (Answer != "") tmp.PIN = Answer.GetHashCode().ToString();

                _t.Display("Status: " + tmp.Status + "\n");
                Answer = _t.GetString();
                if (Answer != "") tmp.Status = Answer;
            }
            if (t.GetType() == typeof(Vehicle))
            {
                _t.Display("Leave empty for no changes" + "\n");
                Vehicle tmp = (Vehicle)(object)t;
            }




            if (t.GetType() == typeof(Resources)) { }

            return t;
        }
        public void ViewList<T>(List<T> liste)
        {
            int c = 1;
            foreach (var ele in liste)
            {
                _t.Display("(" + c + ") " + ele.ToString() + "\n");
                c++;
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
                       "-q\tQuits\n"
                       );
        }
    }
}
