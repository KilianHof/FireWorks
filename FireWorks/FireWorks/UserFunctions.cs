using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class UserFunctions

    {
        private enum UserStates
        {
            USER = 1,
            ADMIN = 2,
            LOCKED = 3
        }
        private const int DEPLOYMENTS = 0;
        private const int EMPLOYEES = 1;
        private const int VEHICLES = 2;
        private const int RESOURCES = 3;
        private const int FIREFIGHTERS = 4;
        private readonly IUserLayer _t;
        private readonly IDataLayer _filer;

        private List<Deployment> AllDeployments;
        private List<User> AllEmployees;
        private List<Vehicle> AllVehicles;
        private List<Resources> AllResources;
        private List<FireFighter> AllFireFighter;

        public UserFunctions(IUserLayer t, IDataLayer filer, object[] lists) { _t = t; _filer = filer;  Init(lists); }

        public void Init(object[] lists)
        {
            AllDeployments = (List<Deployment>)lists[DEPLOYMENTS];
            AllEmployees = (List<User>)lists[EMPLOYEES];
            AllVehicles = (List<Vehicle>)lists[VEHICLES];
            AllResources = (List<Resources>)lists[RESOURCES];
            AllFireFighter = (List<FireFighter>)lists[FIREFIGHTERS];
        }
        public List<Deployment> GetListDeployments() { return AllDeployments; }
        public List<User> GetListEmployees() { return AllEmployees; }
        public List<Vehicle> GetListVehicles() { return AllVehicles; }
        public List<Resources> GetListResources() { return AllResources; }
        public List<FireFighter> GetListFireFighter() { return AllFireFighter; }
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
                    _t.Display("Your user is locked contact your local admin");
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
            string toDisplay;
            int DeployCount;
            switch (sel)
            {
                case "-v":
                    DeployCount = GetListDeployments().Count();
                    _t.Display("Viewing Deployments(" + DeployCount + "):\n" +
                               "(1)List last X Deployments.\n" +
                               "(2)List by X.\n");
                    if (DeployCount == 0)
                    {
                        _t.Display("The deployment file seems to be empty.\nBefore you view deployments you should create one!\n");
                        return;
                    }
                    int UserChoice = ValidInputRange(_t.GetInt(), 1, 2);
                    int howMany;
                    if (UserChoice == 1)
                    {
                        _t.Display("How many deployments do you wish to view?(" + DeployCount + ") \n");
                        howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);
                        _t.Display("Last " + howMany + " Deployments: \n");
                        toDisplay = "";

                        for (int i = 0; i < howMany; i++)
                        {
                            toDisplay += "(" + (i + 1) + ") At: " + GetListDeployments().ElementAt((DeployCount-1)-i).Location + " Time: " + GetListDeployments().ElementAt((DeployCount - 1) - i).DateAndTime + "\n";
                        }
                        _t.Display(toDisplay);
                        _t.Display("To go into detail type corresponding number.\n");
                        howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);
                        _t.Display(GetListDeployments().ElementAt((DeployCount-1)-(howMany - 1)).ToString() + "\n");
                    }
                    else
                    {
                        _t.Display("List by:\n" +
                        "(1)List by Firefighter.\n" +
                        "(2)List by Vehicle.\n");
                        UserChoice = ValidInputRange(_t.GetInt(), 1, 2);
                        _t.Display("How many deployments do you wish to view?(" + DeployCount + ") \n");
                        howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);

                        List<Deployment> deploys = GetListDeployments();
                        if (UserChoice == 1 && GetListFireFighter().Count == 0)
                        {
                            _t.Display("Seems like you dont have any Firefighters in your basedata!\n");
                            return;
                        }

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
                        if (UserChoice == 2 && GetListVehicles().Count == 0)
                        {
                            _t.Display("Seems like you dont have any Vehicles in your basedata!\n");
                            return;
                        }
                        if (UserChoice == 2)
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
                case "-d":
                    int number = 0;

                    Vehicle[] v = new Vehicle[number];
                    FireFighter[] p = new FireFighter[number];
                    Resources[] r = new Resources[number];

                    List<Deployment> liste = GetListDeployments();

                    _t.Display("Where was the Deployment?\n");
                    string loc = _t.GetString();
                    _t.Display("How many Vehicles participated in the Deployment?\n");
                    List<Vehicle> cars = GetListVehicles();
                    if (cars.Count == 0)
                    {
                        _t.Display("It appears as if you have no vehicles in your basedata\n" +
                            "You might wish to ask your local admin to create some for you.\n\n");
                    }
                    else
                    {
                        number = ValidInputRange(_t.GetInt(), 0, cars.Count);
                        v = new Vehicle[number];
                        for (int i = 0; i < number; i++)
                        {
                            v[i] = cars.ElementAt(ObjectSelection(cars) - 1);
                        }
                    }
                    _t.Display("How much staff participated in the Deployment?\n");
                    List<FireFighter> staff = GetListFireFighter();
                    if (staff.Count == 0)
                    {
                        _t.Display("It appears as if you have no staff in your basedata\n" +
                            "You might wish to ask your local admin to create some for you.\n\n");
                    }
                    else
                    {
                        number = ValidInputRange(_t.GetInt(), 0, staff.Count);
                        p = new FireFighter[number];
                        for (int i = 0; i < number; i++)
                        {
                            p[i] = staff.ElementAt(ObjectSelection(staff) - 1);
                        }
                    }
                    _t.Display("How many resources were used in the Deployment?\n");
                    List<Resources> res = GetListResources();
                    if (res.Count == 0)
                    {
                        _t.Display("It appears as if you have no resources in your basedata\n" +
                            "You might wish to ask your local admin to create some for you.\n\n");
                    }
                    else
                    {
                        number = ValidInputRange(_t.GetInt(), 0, res.Count);
                        r = new Resources[number];
                        for (int i = 0; i < number; i++)
                        {
                            r[i] = res.ElementAt(ObjectSelection(res) - 1);
                        }
                    }
                    _t.Display("Any comments?\n");
                    string com = _t.GetString();

                    DeploymentFactory DF = new DeploymentFactory();
                    Deployment test = DF.NewDeployment(loc, v, r, p, com, _filer.GetLastDeploymentNumber());
                    liste.Add(test);
                    _filer.SaveListToFile<Deployment>(AllDeployments);
                    GasExaminationCheck();
                    break;
                case "-g":
                    GasExaminationCheck();
                    break;
                case "-w":

                    DeployCount = GetListDeployments().Count();
                    if (DeployCount == 0)
                    {
                        _t.Display("The deployment file seems to be empty.\nBefore you view deployments you should create one!\n");
                        return;
                    }
                    _t.Display("How many deployments do you wish to view?(" + DeployCount + ") \n");
                    howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);
                    _t.Display("Last " + howMany + " Deployments: \n");
                    toDisplay = "";

                    for (int i = 0; i < howMany; i++)
                    {
                        toDisplay += "(" + (i + 1) + ") At: " + GetListDeployments().ElementAt((DeployCount - 1) - i).Location + " Time: " + GetListDeployments().ElementAt((DeployCount - 1) - i).DateAndTime + "\n";
                    }
                    _t.Display(toDisplay);
                    howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);
                    _t.Display(GetListDeployments().ElementAt((DeployCount - 1) - (howMany - 1)).GenerateWebReport() + "\n");
                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }
        }
        public void GasExaminationCheck()
        {
            bool clear = true;
            List<Resources> list = GetListResources();
            foreach (var item in list)
            {
                if (item.GetType() == typeof(Gasanalyzer))
                {
                    Gasanalyzer tmp = (Gasanalyzer)item;
                    if (tmp.GasTimerCheck())
                    {
                        clear = false;
                        _t.Display("A gasanalyzer needs to be examined!\n" +
                            "Inventorynumber= " + tmp.InventoryNumber + "\n");
                    }
                }
            }
            if (clear)
            {
                _t.Display("All good, all examinations up to date.\n");
            }
        }
        public int ObjectSelection<T>(List<T> list) // warum brauche ich <T> nicht im funktions aufruf?
        {
            int count = list.Count();
            if (count == 0)
            {
                return 0;
            }
            for (int i = 0; i < count; i++)
            {
                _t.Display("(" + (i + 1) + ")" + list.ElementAt(i) + "\n");
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
                            liste.Add((T)(object)Edit(new User("Firstname", "Lastname", 0, "USER", "15947562")));
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
                                    liste.Add((T)(object)Edit(new Firetruck("TYPE", 0, 0, false, 0)));
                                    break;
                                case 3:
                                    liste.Add((T)(object)Edit(new Turntableladder("TYPE", 0, 0, false, 0)));
                                    break;
                                case 4:
                                    liste.Add((T)(object)Edit(new Ambulance("TYPE", 0, 0, 0)));
                                    break;
                            }
                            break;
                        case 3:
                            _t.Display("What kind of Resource?" + "\n" +
                                      "(1) Hose" + "\n" +
                                      "(2) Gasanalyzer" + "\n" +
                                      "(3) Jetnozzle" + "\n" +
                                      "(4) Distributer" + "\n" +
                                      "(5) Other Items" + "\n");
                            switch (ValidInputRange(_t.GetInt(), 1, 5))
                            {

                                case 1:
                                    liste.Add((T)(object)Edit(new Hose("Hose", 0, ' ', 5)));
                                    break;
                                case 2:
                                    liste.Add((T)(object)Edit(new Gasanalyzer("Gasanalyzer", 0)));
                                    break;
                                case 3:
                                    liste.Add((T)(object)Edit(new Jetnozzle("Jetnozzle", 0)));
                                    break;
                                case 4:
                                    liste.Add((T)(object)Edit(new Distributer("Hose", 0)));
                                    break;
                                case 5:
                                    liste.Add((T)(object)Edit(new Resources("Description", 1, "Name")));
                                    break;
                            }
                            break;
                        case 4:
                            liste.Add((T)(object)Edit(new FireFighter("Firstname", "Lastname", 0)));
                            break;
                    }
                    break;
                case 3:
                    _t.Display("Edit." + "\n");
                    ViewList(liste);
                    index = ValidInputRange(_t.GetInt(), 1, liste.Count());
                    index--;
                    tmp = liste.ElementAt(index);
                    liste.RemoveAt(index);
                    liste.Add(Edit(tmp));
                    break;

                case 4:
                    _t.Display("Delete." + "\n");
                    ViewList(liste);
                    index = ValidInputRange(_t.GetInt(), 1, liste.Count());
                    index--;
                    liste.RemoveAt(index);
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
            if (t.GetType() == typeof(Firetruck))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Firetruck tmp = (Firetruck)(object)t;

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

            if (t.GetType() == typeof(Turntableladder))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Turntableladder tmp = (Turntableladder)(object)t;

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
            if (t.GetType() == typeof(Gasanalyzer))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Gasanalyzer tmp = (Gasanalyzer)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "\n");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.InventoryNumber + "\n");
                if (Number != 0) tmp.InventoryNumber = Number;
            }
            if (t.GetType() == typeof(Jetnozzle))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Jetnozzle tmp = (Jetnozzle)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "\n");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.InventoryNumber + "\n");
                if (Number != 0) tmp.InventoryNumber = Number;
            }
            if (t.GetType() == typeof(Distributer))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "\n");
                Distributer tmp = (Distributer)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "\n");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.InventoryNumber + "\n");
                if (Number != 0) tmp.InventoryNumber = Number;
            }
            return t;
        }
        public void ShowAdminOptions()
        {
            _t.Display("Options:\n" +
                       "-e    Edit base data.\n" +
                       "-q    Quit.\n");
        }
        public void ShowUserOptions()
        {
            _t.Display("Options:\n" +
                       "-v    View Deployments.\n" +
                       "-d    Generate Deployment.\n" +
                       "-g    Set Gasanalyzer examination.\n" +
                       "-w    Generate Deploymentreport .\n" +
                       "-q    Quits\n");
        }
    }
}
