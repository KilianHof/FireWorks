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
        private IUserLayer _t;
        private IDataLayer _filer;

        private string[] _path;  // Deploy Employ Vehicles Res FF
        private List<Deployment> AllDeployments;
        private List<User> AllEmployees;
        private List<Vehicle> AllVehicles;
        private List<Resources> AllResources;
        private List<FireFighter> AllFireFighter;

        public UserFunctions(IUserLayer t, IDataLayer filer, object[] lists, string[] paths) { _t = t; _filer = filer; _path = paths; Init(lists); }

        public List<T> ReadAll<T>(int dataSet)
        {
            return _filer.ReadAll<T>(_path[dataSet]);
        }
        public void Init(object[] lists)
        {
            AllDeployments = (List<Deployment>)lists[0];
            AllEmployees = (List<User>)lists[1];
            AllVehicles = (List<Vehicle>)lists[2];
            AllResources = (List<Resources>)lists[3];
            AllFireFighter = (List<FireFighter>)lists[4];
        }
        public List<Deployment> GetListDeployments() { return AllDeployments; }
        public List<User> GetListEmployees() { return AllEmployees; }
        public List<Vehicle> GetListVehicles() { return AllVehicles; }
        public List<Resources> GetListResources() { return AllResources; }
        public List<FireFighter> GetListFireFighter() { return AllFireFighter; }
        public void SaveAllListsToFile()
        {
            _filer.SaveListToFile<Deployment>(AllDeployments, _path[0]);
            _filer.SaveListToFile<User>(AllEmployees, _path[1]);
            _filer.SaveListToFile<Vehicle>(AllVehicles, _path[2]);
            _filer.SaveListToFile<Resources>(AllResources, _path[3]);
            _filer.SaveListToFile<FireFighter>(AllFireFighter, _path[4]);
        }
        //public void SaveSingleListToFile<T>(List<T> liste, int fileIndex)
        //{
        //    _filer.SaveListToFile<T>(liste, _path[fileIndex]);
        //}
        public void Routine(string mode)
        {
            switch (mode)
            {
                case "ADMIN":
                    ShowAdminOptions();
                    AdminMode(_t.GetString());
                    break;
                case "USER":
                    ShowUserOptions();
                    UserMode(_t.GetString());
                    break;
                case "LOCKED":
                    System.Environment.Exit(1);
                    break;
            }
        }
        public int ValidInputRange(int num, int from, int to)
        {
            while (num < from || num > to)
            {
                _t.Display("Invalid Input. Out of valid range:from(" + from + ") to(" + to + "). Try again.\n");
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
                    int dataSet = ValidInputRange(_t.GetInt(), 1, 4);
                    _t.Display("(1)View\n" +
                               "(2)New\n" +
                               "(3)Edit\n" +
                               "(4)Delete\n");
                    int userAction = ValidInputRange(_t.GetInt(), 1, 4);

                    Select(dataSet, userAction);

                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }
        }
        public void UserMode(string sel)
        {
            switch (sel)
            {
                case "-v":
                    _t.Display("Viewing Deployments:\n" +
                               "(1)List last X Deployments.\n" +
                               "(2)List by X.\n");
                    int UserChoice = ValidInputRange(_t.GetInt(), 1, 2);
                    int howMany;
                    if (UserChoice == 1)
                    {
                        _t.Display("How many deployments do you wish to view?(" + GetListDeployments().Count() + ") \n");
                        howMany = ValidInputRange(_t.GetInt(), 1, GetListDeployments().Count());
                        _t.Display("Last " + howMany + " Deployments: \n");
                        string toDisplay = "";

                        for (int i = 0; i < howMany; i++)
                        {
                            toDisplay += "(" + (i + 1) + ") At: " + GetListDeployments().ElementAt(i).Location + " Time: " + GetListDeployments().ElementAt(i).DateAndTime + "\n";
                        }
                        _t.Display(toDisplay);
                        _t.Display("To go into detail type corresponding number.\n");
                        howMany = ValidInputRange(_t.GetInt(), 1, GetListDeployments().Count());
                        _t.Display(GetListDeployments().ElementAt(howMany - 1).ToString() + "\n");
                    }
                    else
                    {
                        _t.Display("List by:\n" +
                        "(1)List by Firefighter.\n" +
                        "(2)List by Vehicle.\n");
                        UserChoice = ValidInputRange(_t.GetInt(), 1, 2);
                        _t.Display("How many deployments do you wish to view?(" + GetListDeployments().Count() + ") \n");
                        howMany = ValidInputRange(_t.GetInt(), 1, GetListDeployments().Count());



                            List<Deployment> deploys = GetListDeployments();
                            if (UserChoice == 1)
                            {
                            List<FireFighter> firefighters = GetListFireFighter();
                            FireFighter tmp;
                            _t.Display("Select for which Firefighter you wish to search:\n");
                            ViewList(firefighters);
                            UserChoice = ValidInputRange(_t.GetInt(), 1, firefighters.Count());
                            tmp = firefighters.ElementAt(UserChoice - 1);

                            int amountD = deploys.Count();
                            int amountF;
                            int counter = 0;
                            for (int k = 0; k < amountD; k++)
                            {
                                amountF = deploys.ElementAt(k).FireFighters.Length;
                                for (int j = 0; j < amountF; j++)
                                {
                                    if (deploys.ElementAt(k).FireFighters[j].Equals(tmp) && counter < howMany)
                                    {
                                        _t.Display("(" + (k + 1) + ") At: " + deploys.ElementAt(k).Location + " Time: " + deploys.ElementAt(k).DateAndTime + "\n");
                                        counter++;
                                    }
                                }
                            }
                            if (!(counter == 0))
                            {
                                _t.Display("To go into detail type corresponding number.\n");
                            UserChoice = ValidInputRange(_t.GetInt(), 1, counter);
                            _t.Display(GetListDeployments().ElementAt(UserChoice - 1).ToString() + "\n");
                            }
                            else
                            {
                                _t.Display("Couldnt find any Deployments with your selection.\n");
                            }
                        }
                        if(UserChoice == 2)
                        {
                            List<Vehicle> vehicles = GetListVehicles();
                            Vehicle tmp;
                            _t.Display("Select for which car you wish to search:\n");
                            ViewList(vehicles);
                            UserChoice = ValidInputRange(_t.GetInt(), 1, vehicles.Count());
                            tmp = vehicles.ElementAt(UserChoice - 1);

                            int amountD = deploys.Count();
                            int amountC;
                            int counter = 0;
                            for (int k = 0; k < amountD; k++)
                            {
                                amountC = deploys.ElementAt(k).Cars.Length;
                                for (int j = 0; j < amountC; j++)
                                {
                                    if (deploys.ElementAt(k).Cars[j].Equals(tmp) && counter < howMany)
                                    {
                                        _t.Display("(" + (k + 1) + ") At: " + deploys.ElementAt(k).Location + " Time: " + deploys.ElementAt(k).DateAndTime + "\n");
                                        counter++;
                                    }
                                }
                            }
                            if (!(counter == 0))
                            {
                                _t.Display("To go into detail type corresponding number.\n");
                                UserChoice = ValidInputRange(_t.GetInt(), 1, counter);
                                _t.Display(GetListDeployments().ElementAt(UserChoice - 1).ToString() + "\n");
                            }
                            else
                            {
                                _t.Display("Couldnt find any Deployments with your selection.\n");
                            }
                        }
                    }

                    break;
                case "-g":

                    List<Deployment> liste = GetListDeployments();


                    _t.Display("Where was the Deployment?\n");
                    string loc = _t.GetString();
                    _t.Display("How many Vehicles participated in the Deployment?\n");
                    List<Vehicle> cars = GetListVehicles();
                    int number = ValidInputRange(_t.GetInt(), 0, 999);
                    Vehicle[] v = new Vehicle[number];
                    for (int i = 0; i < number; i++)
                    {
                        v[i] = cars.ElementAt(ObjectSelection(cars) - 1);
                    }
                    _t.Display("How much staff participated in the Deployment?\n");
                    List<FireFighter> staff = GetListFireFighter();
                    number = ValidInputRange(_t.GetInt(), 0, 999);
                    FireFighter[] p = new FireFighter[number];
                    for (int i = 0; i < number; i++)
                    {
                        p[i] = staff.ElementAt(ObjectSelection(staff) - 1);
                    }
                    _t.Display("How many resources were used in the Deployment?\n");
                    List<Resources> res = GetListResources();
                    number = ValidInputRange(_t.GetInt(), 0, 999);
                    Resources[] r = new Resources[number];
                    for (int i = 0; i < number; i++)
                    {
                        r[i] = res.ElementAt(ObjectSelection(res) - 1);
                    }

                    _t.Display("Any comments?\n");
                    string com = _t.GetString();

                    DeploymentFactory DF = new DeploymentFactory();
                    Deployment test = DF.NewDeployment(loc, v, r, p, com, _filer.GetLastDeploymentNumber(liste));
                    liste.Add(test);
                    _filer.SaveListToFile<Deployment>(AllDeployments, _path[0]);
                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }
        }
        public int ObjectSelection<T>(List<T> list) // warum brauche ich <T> nicht im funktions aufruf?
        {
            int count = list.Count();
            for (int i = 0; i < count; i++)
            {
                _t.Display("(" + (i + 1) + ")"+ list.ElementAt(i)+"\n");
            }
            _t.Display("To select type corresponding number." + "\n");
            return ValidInputRange(_t.GetInt(), 1, count);
        }
        public void Select(int dataSet, int userAction)
        {
            switch (dataSet)
            {
                case 1:
                    ProcessList(userAction, GetListEmployees(), dataSet);
                    break;
                case 2:
                    ProcessList(userAction, GetListVehicles(), dataSet);
                    break;
                case 3:
                    ProcessList(userAction, GetListResources(), dataSet);
                    break;
                case 4:
                    ProcessList(userAction, GetListFireFighter(), dataSet);
                    break;
            }
        }
        public void ProcessList<T>(int userAction, List<T> liste, int dataSet)
        {
            T tmp;
            int index;
            switch (userAction)
            {
                case 1:
                    ViewList(liste);
                    break;
                case 2:
                    _t.Display("Generate new." + "\n");
                    switch (dataSet)
                    {
                        case 1:
                            User tmpU = new User("Firstname", "Lastname", 0, "USER", "15947562");
                            liste.Add((T)(object)Edit(tmpU));
                            break;
                        case 2:
                            _t.Display("What kind of Vehicle?" + "\n" +
                                       "(1) Car" + "\n" +
                                       "(2) Firetruck " + "\n" +
                                       "(3) Turntabelladdertruck" + "\n" +
                                       "(4) Ambulance" + "\n");
                            switch (ValidInputRange(_t.GetInt(), 1, 4))
                            {
                                case 1:
                                    liste.Add((T)(object)Edit(new Pkw("TYPE", 0, 0)));
                                    break;
                                case 2:
                                    liste.Add((T)(object)Edit(new FireTruck("TYPE", 0, 0, false, 0)));
                                    break;
                                case 3:
                                    liste.Add((T)(object)Edit(new TurntableLadder("TYPE", 0, 0, false, 0)));
                                    break;
                                case 4:
                                    liste.Add((T)(object)Edit(new Ambulance("TYPE", 0, 0, 0)));
                                    break;
                            }
                            break;
                        case 3:
                            _t.Display("What kind of Resource?" + "\n" +
                                      "(1) Hose" + "\n" +
                                      "(2) Other Items" + "\n");
                            switch (ValidInputRange(_t.GetInt(), 1, 2))
                            {

                                case 1:
                                    liste.Add((T)(object)Edit(new Hose("Hose", 0, ' ', 5)));
                                    break;
                                case 2:
                                    liste.Add((T)(object)Edit(new Resources("Description", 1, "Name")));
                                    break;
                            }
                            break;
                        case 4:
                            liste.Add((T)(object)new FireFighter("hi", "yo", 1));
                            break;
                    }
                    //SaveSingleListToFile<T>(liste, dataSet);
                    break;
                case 3:
                    _t.Display("Edit." + "\n");
                    ViewList(liste);
                    index = ValidInputRange(_t.GetInt(), 1, liste.Count());
                    index--;
                    tmp = liste.ElementAt(index);
                    liste.RemoveAt(index);
                    liste.Add(Edit(tmp));
                    //SaveSingleListToFile<T>(liste, dataSet);
                    break;
                //User tmp = (User)_filer.ReadObject<User>(_path[dataSet], Valid(_t.GetInt(), 1, c - 1));

                case 4:
                    _t.Display("Delete." + "\n");
                    ViewList(liste);
                    index = ValidInputRange(_t.GetInt(), 1, liste.Count());
                    index--;
                    liste.RemoveAt(index);
                    //SaveSingleListToFile<T>(liste, dataSet);
                    break;
            }
        }
        public string EditHelperString(string message)
        {
            string str;
            _t.Display(message);
            str = _t.GetString();
            if (str != ".")
                return str;
            return "";
        }
        public string EditHelperBool(string message)
        {
            string str;
            _t.Display(message);
            _t.Display("(y/n)?\n");
            str = _t.GetString();
            while (!((str == "y") || (str == "Y") || (str == "Yes") || (str == "yes") || (str == "n") || (str == "N") || (str == "no") || (str == "No") || (str == "")))
            {
                _t.Display("Only (y/n)?\n");
                str = _t.GetString();
            }

            return str;
        }
        public int EditHelperInt(string message)
        {
            int number;
            _t.Display(message);
            number = _t.GetInt();
            while (number < 0)
            {
                _t.Display("Please only enter positive intergers" + "\n");
                number = _t.GetInt();
            }
            return number;
        }
        public int EditHelperInt(string message, int min, int max) // range
        {
            int number;
            _t.Display(message);
            number = _t.GetInt();
            while (!(number >= min && number <= max))
            {
                _t.Display("Please only enter Intergers between " + min + " . " + max + ".");
                number = _t.GetInt();
            }
            return number;
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

        public T Edit<T>(T t)  // to use T or not to use T that is thy question(T to obj oder var)                                                     // ja ne is klar nice edit skillz
        {
            string Answer;
            int Number;
            if (t as FireTruck != null)
                if (t.GetType() == typeof(FireFighter))
                {
                    _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                    FireFighter tmp = (FireFighter)(object)t;

                    Answer = EditHelperString("Lastname: " + tmp.LastName + "\n");
                    if (Answer != "") tmp.LastName = Answer;

                    Answer = EditHelperString("Firstname: " + tmp.FirstName + "\n");
                    if (Answer != ".") tmp.FirstName = Answer;

                    Number = EditHelperInt("ID(number): " + tmp.Id + "\n");
                    if (Number != 0) tmp.Id = Number;
                }
            if (t.GetType() == typeof(User))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                User tmp = (User)(object)t;

                Answer = EditHelperString("Lastname: " + tmp.LastName + "\n");
                if (Answer != ".") tmp.LastName = Answer;

                Answer = EditHelperString("Firstname: " + tmp.FirstName + "\n");
                if (Answer == " hallo") Answer = ".";

                if (Answer != ".") tmp.FirstName = Answer;

                Number = EditHelperInt("ID(number): " + tmp.Id + "\n");
                if (Number != 0) tmp.Id = Number;

                Answer = EditHelperString("PIN(number(Hashed)): " + tmp.PIN + "\n");
                if (Answer != ".") tmp.PIN = Answer.GetHashCode().ToString();

                Number = EditHelperInt("Status(1=USER,2=ADMIN,3=LOCKED): " + tmp.Status + "\n", 0, 3);
                if (Number != 0) tmp.Status = ((UserStates)Number).ToString();
            }
            if (t.GetType() == typeof(FireTruck))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                FireTruck tmp = (FireTruck)(object)t;

                Answer = EditHelperString("Type: " + tmp.Type + "\n");
                if (Answer != ".") tmp.Type = Answer;

                Number = EditHelperInt("Seats: " + tmp.Seats + "\n");
                if (Number != 0) tmp.Seats = Number;

                Number = EditHelperInt("Fillquantity: " + tmp.FillQuantity + "\n");
                if (Number != 0) tmp.FillQuantity = Number;

                Number = EditHelperInt("Enginepower: " + tmp.EnginePower + "\n");
                if (Number != 0) tmp.EnginePower = Number;

                Answer = EditHelperBool("Chainsaw: " + tmp.Chainsaw + "\n");
                if (Answer != "")
                {
                    if ((Answer == "y") || (Answer == "Y") || (Answer == "Yes") || (Answer == "yes"))
                        tmp.Chainsaw = true;
                    else
                        tmp.Chainsaw = false;
                }
            }

            if (t.GetType() == typeof(TurntableLadder))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                TurntableLadder tmp = (TurntableLadder)(object)t;

                Answer = EditHelperString("Type: " + tmp.Type + "\n");
                if (Answer != ".") tmp.Type = Answer;

                Number = EditHelperInt("Seats: " + tmp.Seats + "\n");
                if (Number != 0) tmp.Seats = Number;

                Number = EditHelperInt("Ladderheight: " + tmp.LadderHeight + "\n");
                if (Number != 0) tmp.LadderHeight = Number;

                Number = EditHelperInt("Enginepower: " + tmp.EnginePower + "\n");
                if (Number != 0) tmp.EnginePower = Number;

                Answer = EditHelperBool("Chainsaw: " + tmp.Chainsaw + "\n");
                if (Answer != "")
                {
                    if ((Answer == "y") || (Answer == "Y") || (Answer == "Yes") || (Answer == "yes"))
                        tmp.Chainsaw = true;
                    else
                        tmp.Chainsaw = false;
                }
            }
            if (t.GetType() == typeof(Ambulance))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Ambulance tmp = (Ambulance)(object)t;

                Answer = EditHelperString("Type: " + tmp.Type + "\n");
                if (Answer != ".") tmp.Type = Answer;

                Number = EditHelperInt("Seats: " + tmp.Seats + "\n");
                if (Number != 0) tmp.Seats = Number;

                Number = EditHelperInt("Max Patientweight: " + tmp.PatientWeight + "\n");
                if (Number != 0) tmp.PatientWeight = Number;

                Number = EditHelperInt("Enginepower: " + tmp.EnginePower + "\n");
                if (Number != 0) tmp.EnginePower = Number;
            }

            if (t.GetType() == typeof(Pkw))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Pkw tmp = (Pkw)(object)t;

                Answer = EditHelperString(" \".\"Type: " + tmp.Type + "\n");

                if (Answer != ".") tmp.Type = Answer;

                Number = EditHelperInt("Seats: " + tmp.Seats + "\n");
                if (Number != 0) tmp.Seats = Number;

                Number = EditHelperInt("Enginepower: " + tmp.EnginePower + "\n");
                if (Number != 0) tmp.EnginePower = Number;
            }

            if (t.GetType() == typeof(Resources))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Resources tmp = (Resources)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "\n");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.InventoryNumber + "\n");
                if (Number != 0) tmp.InventoryNumber = Number;
            }
            if (t.GetType() == typeof(Hose))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Hose tmp = (Hose)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "\n");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.InventoryNumber + "\n");
                if (Number != 0) tmp.InventoryNumber = Number;

                Number = EditHelperInt("Hose length(5, 10, 20, 30): " + tmp.HoseLength + "\n");
                if ((Number != 0 && Number % 10 == 0 && Number <= 30) || Number == 5) tmp.HoseLength = Number;
                else _t.Display("Valid lenghts are 5, 10, 20, 30. No changes were made" + "\n");

                Answer = EditHelperString("Hose type(B,C,D): " + tmp.Letter + "\n");
                char letter = ' ';
                if (Answer.Length == 1) letter = Answer.ToCharArray().ElementAt(0);
                if (letter == 'B' || letter == 'C' || letter == 'D') tmp.Letter = letter;
            }
            return t;
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
                       "-g\tGenerate Deployment.\n" +
                       "-q\tQuits\n"
                       );
        }
    }
}
