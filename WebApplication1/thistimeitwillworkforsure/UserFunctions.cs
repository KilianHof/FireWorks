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
        private const int DEPLOYMENTS = 0;  //quasi "Alias"
        private const int EMPLOYEES = 1;
        private const int VEHICLES = 2;
        private const int RESOURCES = 3;
        private const int FIREFIGHTERS = 4;
        private readonly IUserLayer _t;

        private List<Deployment> AllDeployments;    //erstellt Listen wo alle Objekte der Art gelagert werden.
        private List<User> AllEmployees;
        private List<Vehicle> AllVehicles;
        private List<Resource> AllResources;
        private List<FireFighter> AllFireFighter;

        public UserFunctions(IUserLayer t, object[] lists) { _t = t; Init(lists); }

        public void Init(object[] lists)
        {
            AllDeployments = (List<Deployment>)lists[DEPLOYMENTS];
            AllEmployees = (List<User>)lists[EMPLOYEES];
            AllVehicles = (List<Vehicle>)lists[VEHICLES];
            AllResources = (List<Resource>)lists[RESOURCES];
            AllFireFighter = (List<FireFighter>)lists[FIREFIGHTERS];
        }
        public List<Deployment> GetListDeployments() { return AllDeployments; }
        public List<User> GetListEmployees() { return AllEmployees; }
        public List<Vehicle> GetListVehicles() { return AllVehicles; }
        public List<Resource> GetListResources() { return AllResources; }
        public List<FireFighter> GetListFireFighter() { return AllFireFighter; }
        public void Routine(string mode)
        {
            switch (mode)   //Sortiert Nutzer ins richtige Menü ein basierend auf seinem Status
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
                _t.Display("Invalid Input. Out of valid range:from(" + from + ") to(" + to + "). Try again.<br />");
                num = _t.GetInt();
            }
            return num;
        }
        public void AdminMode(string sel)
        {
            switch (sel)    //Adminmenü
            {
                case "-e":
                    _t.Display("Choose Base data to work with:<br />" +
                               "(1)Employees<br />" +
                               "(2)Vehicles<br />" +
                               "(3)Resource<br />" +
                               "(4)Firefighters<br />");
                    int dataSet = ValidInputRange(_t.GetInt(), 1, 4);
                    _t.Display("(1)View<br />" +
                               "(2)New<br />" +
                               "(3)Edit<br />" +
                               "(4)Delete<br />");
                    int userAction = ValidInputRange(_t.GetInt(), 1, 4);

                    Select(dataSet, userAction);

                    break;
                case "-u":
                    ShowUserOptions();
                    UserMode(_t.GetString());
                    break;
                case "-q":
                    break;
            }
        }
        public void UserMode(string sel)
        {
            string toDisplay;
            int DeployCount;
            switch (sel)    //Nutzermenü
            {
                case "-v":
                    DeployCount = GetListDeployments().Count();
                    _t.Display("Viewing Deployments(" + DeployCount + "):<br />" +
                               "(1)List last X Deployments.<br />" +
                               "(2)List by X.<br />");
                    if (DeployCount == 0)   //Checkt die Anzahl der Einsätze
                    {
                        _t.Display("The deployment file seems to be empty.<br />Before you view deployments you should create one!<br />");
                        return;
                    }
                    int UserChoice = ValidInputRange(_t.GetInt(), 1, 2);    //untersucht ob die Angabe 1/2 oder dazwischen ist
                    int howMany;
                    if (UserChoice == 1)
                    {
                        _t.Display("How many deployments do you wish to view?(" + DeployCount + ") <br />");
                        howMany = ValidInputRange(_t.GetInt(), 1, DeployCount); //ist die gesuchte zahl zwischen 1 und der existierenden Anzahl?
                        _t.Display("Last " + howMany + " Deployments: <br />");
                        toDisplay = "";

                        for (int i = 0; i < howMany; i++)   //fügt die Einsätze einem String hinzu-
                        {
                            toDisplay += "(" + (i + 1) + ") At: " + GetListDeployments().ElementAt((DeployCount - 1) - i).Location + " Time: " + GetListDeployments().ElementAt((DeployCount - 1) - i).DateAndTime + "<br />";
                        }
                        _t.Display(toDisplay);  //-stellt diesen String dar
                        _t.Display("To go into detail type corresponding number.<br />");
                        howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);
                        _t.Display(GetListDeployments().ElementAt((DeployCount - 1) - (howMany - 1)).ToString() + "<br />");
                    }
                    else
                    {
                        _t.Display("List by:<br />" +
                        "(1)List by Firefighter.<br />" +
                        "(2)List by Vehicle.<br />");
                        UserChoice = ValidInputRange(_t.GetInt(), 1, 2);
                        _t.Display("How many deployments do you wish to view?(" + DeployCount + ") <br />");
                        howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);

                        List<Deployment> deploys = GetListDeployments(); // fügt der liste "deploys" alle einsätze hinzu
                        if (UserChoice == 1 && GetListFireFighter().Count == 0)
                        {
                            _t.Display("Seems like you dont have any Firefighters in your basedata!<br />");
                            return;
                        }

                        if (UserChoice == 1)
                        {
                            List<FireFighter> firefighters = GetListFireFighter();
                            FireFighter tmp;
                            _t.Display("Select for which Firefighter you wish to search:<br />");
                            ViewList(firefighters);
                            UserChoice = ValidInputRange(_t.GetInt(), 1, firefighters.Count());
                            tmp = firefighters.ElementAt(UserChoice - 1); //-1 da listen(und arrays) bei 0 anfangen

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
                                        _t.Display("(" + (k + 1) + ") At: " + deploys.ElementAt(k).Location + " Time: " + deploys.ElementAt(k).DateAndTime + "<br />"); //gibt nacheinander Feuerwehrmitglieder an
                                        counter++;
                                    }
                                }
                            }
                            if (!(counter == 0))
                            {
                                _t.Display("To go into detail type corresponding number.<br />");
                                UserChoice = ValidInputRange(_t.GetInt(), 1, counter);
                                _t.Display(GetListDeployments().ElementAt(UserChoice - 1).ToString() + "<br />");   //sucht Einsatz nach ID heraus (-1 da [siehe bei vorletztem "if"])
                            }
                            else
                            {
                                _t.Display("Couldnt find any Deployments with your selection.<br />");
                            }
                        }
                        if (UserChoice == 2 && GetListVehicles().Count == 0)
                        {
                            _t.Display("Seems like you dont have any Vehicles in your basedata!<br />");
                            return;
                        }
                        if (UserChoice == 2)
                        {
                            List<Vehicle> vehicles = GetListVehicles();
                            Vehicle tmp;
                            _t.Display("Select for which car you wish to search:<br />");
                            ViewList(vehicles);
                            UserChoice = ValidInputRange(_t.GetInt(), 1, vehicles.Count()); //sucht Fahrzeug nach ID
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
                                        _t.Display("(" + (k + 1) + ") At: " + deploys.ElementAt(k).Location + " Time: " + deploys.ElementAt(k).DateAndTime + "<br />"); //sucht nach Einsätzen mit gewähltem Fahrzeug
                                        counter++;
                                    }
                                }
                            }
                            if (!(counter == 0))
                            {
                                _t.Display("To go into detail type corresponding number.<br />");
                                UserChoice = ValidInputRange(_t.GetInt(), 1, counter);
                                _t.Display(GetListDeployments().ElementAt(UserChoice - 1).ToString() + "<br />");
                            }
                            else
                            {
                                _t.Display("Couldnt find any Deployments with your selection.<br />");
                            }
                        }
                    }

                    break;
                case "-d":
                    {
                        List<Deployment> liste = GetListDeployments();
                        int number = 0; //erstellt einen Einsatzeintrag

                        Vehicle[] v = new Vehicle[number];
                        FireFighter[] p = new FireFighter[number];
                        Resource[] r = new Resource[number];

                        _t.Display("Where was the Deployment?<br />");
                        string loc = _t.GetString();
                        _t.Display("How many Vehicles participated in the Deployment?<br />");
                        List<Vehicle> cars = GetListVehicles();
                        if (cars.Count == 0)
                        {
                            _t.Display("It appears as if you have no vehicles in your basedata<br />" +
                                "You might wish to ask your local admin to create some for you.<br /><br />");
                        }
                        else
                        {
                            number = ValidInputRange(_t.GetInt(), 0, cars.Count);   //ist die Zahl der Fahrzeuge valide?
                            v = new Vehicle[number];
                            for (int i = 0; i < number; i++)
                            {
                                v[i] = cars.ElementAt(ObjectSelection(cars) - 1);
                            }
                        }
                        _t.Display("How much staff participated in the Deployment?<br />");
                        List<FireFighter> staff = GetListFireFighter();
                        if (staff.Count == 0)
                        {
                            _t.Display("It appears as if you have no staff in your basedata<br />" +
                                "You might wish to ask your local admin to create some for you.<br /><br />");
                        }
                        else
                        {
                            number = ValidInputRange(_t.GetInt(), 0, staff.Count);  //ist die Zahl der Feuerwehrmänner valide?
                            p = new FireFighter[number];
                            for (int i = 0; i < number; i++)
                            {
                                p[i] = staff.ElementAt(ObjectSelection(staff) - 1);
                            }
                        }
                        _t.Display("How many resources were used in the Deployment?<br />");
                        List<Resource> res = GetListResources();
                        if (res.Count == 0)
                        {
                            _t.Display("It appears as if you have no resources in your basedata<br />" +
                                "You might wish to ask your local admin to create some for you.<br /><br />");
                        }
                        else
                        {
                            number = ValidInputRange(_t.GetInt(), 0, res.Count);    //ist die zahl der resourcen valide?
                            r = new Resource[number];
                            for (int i = 0; i < number; i++)
                            {
                                r[i] = res.ElementAt(ObjectSelection(res) - 1);
                            }
                        }
                        _t.Display("Any comments?<br />");
                        string com = _t.GetString();

                        if (Globals.DLL == false)
                        {
                            DeploymentFactory DF = new DeploymentFactory();
                            Deployment test = DF.NewDeployment(loc, v, r, p, com, AllDeployments.Count() + 1);
                            liste.Add(test);
                        }
                        else
                        {
                            var DeploymentDLL = new DeploymentDLL();
                            var DF = DeploymentDLL.NewDeployment(loc, v, r, p, com, AllDeployments.Count() + 1);
                            Deployment tmp = new Deployment(DF.Location, DF.Cars, DF.Resources, DF.FireFighters, DF.Comment, DF.Id);
                            liste.Add(tmp);
                        }


                    }
                    GasExaminationCheck();
                    break;
                case "-g":
                    GasExaminationCheck();
                    break;
                case "-w":

                    DeployCount = GetListDeployments().Count();
                    if (DeployCount == 0)
                    {
                        _t.Display("The deployment file seems to be empty.<br />Before you view deployments you should create one!<br />");
                        return;
                    }
                    _t.Display("How many deployments do you wish to view?(" + DeployCount + ") <br />");
                    howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);
                    _t.Display("Last " + howMany + " Deployments: <br />");
                    toDisplay = "";

                    for (int i = 0; i < howMany; i++)
                    {
                        toDisplay += "(" + (i + 1) + ") At: " + GetListDeployments().ElementAt((DeployCount - 1) - i).Location + " Time: " + GetListDeployments().ElementAt((DeployCount - 1) - i).DateAndTime + "<br />";
                    }
                    _t.Display(toDisplay);
                    howMany = ValidInputRange(_t.GetInt(), 1, DeployCount);
                    _t.Display(GetListDeployments().ElementAt((DeployCount - 1) - (howMany - 1)).GenerateWebReport() + "<br />");
                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }
        }
        public void GasExaminationCheck()
        {
            bool clear = true;
            List<Resource> list = GetListResources();
            foreach (var item in list)
            {
                if (item.GetType() == typeof(Gasanalyzer))
                {
                    Gasanalyzer tmp = (Gasanalyzer)item;
                    if (tmp.GasTimerCheck())
                    {
                        clear = false;
                        _t.Display("A gasanalyzer needs to be examined!<br />" +
                            "Inventorynumber= " + tmp.Id + "<br />");
                    }
                }
            }
            if (clear)
            {
                _t.Display("All good, all examinations up to date.<br />");
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
                _t.Display("(" + (i + 1) + ")" + list.ElementAt(i) + "<br />");
            }
            _t.Display("To select type corresponding number." + "<br />");
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
                    _t.Display("Generate new." + "<br />");
                    switch (dataSet)
                    {
                        case 1:
                            liste.Add((T)(object)Edit(new User("Firstname", "Lastname", 0, "USER", "0000")));
                            break;
                        case 2:
                            _t.Display("What kind of Vehicle?" + "<br />" +
                                       "(1) Car" + "<br />" +
                                       "(2) Firetruck " + "<br />" +
                                       "(3) Turntabelladdertruck" + "<br />" +
                                       "(4) Ambulance" + "<br />");
                            switch (ValidInputRange(_t.GetInt(), 1, 4))
                            {
                                case 1:
                                    liste.Add((T)(object)Edit(new Pkw("TYPE", 0, 0))); //erstellt ein neues objekt als teil der Liste
                                    break;
                                case 2:
                                    liste.Add((T)(object)Edit(new Firetruck("TYPE", 0, 0, false, 0))); //erstellt ein neues objekt als teil der Liste
                                    break;
                                case 3:
                                    liste.Add((T)(object)Edit(new Turntableladder("TYPE", 0, 0, false, 0))); //erstellt ein neues objekt als teil der Liste
                                    break;
                                case 4:
                                    liste.Add((T)(object)Edit(new Ambulance("TYPE", 0, 0, 0))); //erstellt ein neues objekt als teil der Liste
                                    break;
                            }
                            break;
                        case 3:
                            _t.Display("What kind of Resource?" + "<br />" +
                                      "(1) Hose" + "<br />" +
                                      "(2) Gasanalyzer" + "<br />" +
                                      "(3) Jetnozzle" + "<br />" +
                                      "(4) Distributer" + "<br />" +
                                      "(5) Other Items" + "<br />");
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
                                    liste.Add((T)(object)Edit(new Resource("Description", 1, "Name")));
                                    break;
                            }
                            break;
                        case 4:
                            liste.Add((T)(object)Edit(new FireFighter("Firstname", "Lastname", 0)));
                            break;
                    }
                    break;
                case 3:
                    _t.Display("Edit." + "<br />");
                    ViewList(liste);
                    index = ValidInputRange(_t.GetInt(), 1, liste.Count());
                    index--;
                    tmp = liste.ElementAt(index);
                    liste.RemoveAt(index);
                    liste.Add(Edit(tmp));
                    break;

                case 4:
                    _t.Display("Delete." + "<br />");
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
            _t.Display("(y/n)?<br />");
            str = _t.GetString();
            while (!((str == "y") || (str == "Y") || (str == "Yes") || (str == "yes") || (str == "n") || (str == "N") || (str == "no") || (str == "No") || (str == "")))
            {
                _t.Display("Only (y/n)?<br />");
                str = _t.GetString();
            }

            return str;
        }
        public int EditHelperInt(string message)    //overload, wenn min und max angegeben sind wird an die andere function weitergeleitet
        {
            int number;
            _t.Display(message);
            number = _t.GetInt();
            while (number < 0)
            {
                _t.Display("Please only enter positive intergers" + "<br />");
                number = _t.GetInt();
            }
            return number;
        }
        public int EditHelperInt(string message, int min, int max) // min-max größe des integers
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
                _t.Display("(" + c + ") " + ele.ToString() + "<br />");
                c++;
            }
        }

        public T Edit<T>(T t)  // to use T or not to use T that is thy question(T to obj oder var)                                                     // ja ne is klar nice edit skillz
        {
            string Answer;
            int Number;
            if (t.GetType() == typeof(FireFighter))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                FireFighter tmp = (FireFighter)(object)t;

                Answer = EditHelperString("Lastname: " + tmp.LastName + "<br />");
                if (Answer != "") tmp.LastName = Answer;

                Answer = EditHelperString("Firstname: " + tmp.FirstName + "<br />");
                if (Answer != ".") tmp.FirstName = Answer;

                Number = EditHelperInt("ID(number): " + tmp.Id + "<br />");
                if (Number != 0) tmp.Id = Number;
            }
            if (t.GetType() == typeof(User))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                User tmp = (User)(object)t;

                Answer = EditHelperString("Lastname: " + tmp.LastName + "<br />");
                if (Answer != ".") tmp.LastName = Answer;

                Answer = EditHelperString("Firstname: " + tmp.FirstName + "<br />");
                if (Answer == " hallo") Answer = ".";

                if (Answer != ".") tmp.FirstName = Answer;

                Number = EditHelperInt("ID(number): " + tmp.Id + "<br />");
                if (Number != 0) tmp.Id = Number;

                Answer = EditHelperString("PIN(number(Hashed)): " + tmp.PIN + "<br />");
                if (Answer != ".") tmp.PIN = Answer.GetHashCode().ToString();

                Number = EditHelperInt("Status(1=USER,2=ADMIN,3=LOCKED): " + tmp.Status + "<br />", 0, 3);
                if (Number != 0) tmp.Status = ((UserStates)Number).ToString();
            }
            if (t.GetType() == typeof(Firetruck))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Firetruck tmp = (Firetruck)(object)t;

                Answer = EditHelperString("Type: " + tmp.Type + "<br />");
                if (Answer != ".") tmp.Type = Answer;

                Number = EditHelperInt("Seats: " + tmp.Seats + "<br />");
                if (Number != 0) tmp.Seats = Number;

                Number = EditHelperInt("Fillquantity: " + tmp.FillQuantity + "<br />");
                if (Number != 0) tmp.FillQuantity = Number;

                Number = EditHelperInt("Enginepower: " + tmp.EnginePower + "<br />");
                if (Number != 0) tmp.EnginePower = Number;

                Answer = EditHelperBool("Chainsaw: " + tmp.Chainsaw + "<br />");
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
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Turntableladder tmp = (Turntableladder)(object)t;

                Answer = EditHelperString("Type: " + tmp.Type + "<br />");
                if (Answer != ".") tmp.Type = Answer;

                Number = EditHelperInt("Seats: " + tmp.Seats + "<br />");
                if (Number != 0) tmp.Seats = Number;

                Number = EditHelperInt("Ladderheight: " + tmp.LadderHeight + "<br />");
                if (Number != 0) tmp.LadderHeight = Number;

                Number = EditHelperInt("Enginepower: " + tmp.EnginePower + "<br />");
                if (Number != 0) tmp.EnginePower = Number;

                Answer = EditHelperBool("Chainsaw: " + tmp.Chainsaw + "<br />");
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
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Ambulance tmp = (Ambulance)(object)t;

                Answer = EditHelperString("Type: " + tmp.Type + "<br />");
                if (Answer != ".") tmp.Type = Answer;

                Number = EditHelperInt("Seats: " + tmp.Seats + "<br />");
                if (Number != 0) tmp.Seats = Number;

                Number = EditHelperInt("Max Patientweight: " + tmp.PatientWeight + "<br />");
                if (Number != 0) tmp.PatientWeight = Number;

                Number = EditHelperInt("Enginepower: " + tmp.EnginePower + "<br />");
                if (Number != 0) tmp.EnginePower = Number;
            }

            if (t.GetType() == typeof(Pkw))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Pkw tmp = (Pkw)(object)t;

                Answer = EditHelperString(" \".\"Type: " + tmp.Type + "<br />");

                if (Answer != ".") tmp.Type = Answer;

                Number = EditHelperInt("Seats: " + tmp.Seats + "<br />");
                if (Number != 0) tmp.Seats = Number;

                Number = EditHelperInt("Enginepower: " + tmp.EnginePower + "<br />");
                if (Number != 0) tmp.EnginePower = Number;
            }

            if (t.GetType() == typeof(Resource))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Resource tmp = (Resource)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "<br />");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.Id + "<br />");
                if (Number != 0) tmp.Id = Number;
            }
            if (t.GetType() == typeof(Hose))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Hose tmp = (Hose)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "<br />");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.Id + "<br />");
                if (Number != 0) tmp.Id = Number;

                Number = EditHelperInt("Hose length(5, 10, 20, 30): " + tmp.HoseLength + "<br />");
                if ((Number != 0 && Number % 10 == 0 && Number <= 30) || Number == 5) tmp.HoseLength = Number;
                else _t.Display("Valid lenghts are 5, 10, 20, 30. No changes were made" + "<br />");

                Answer = EditHelperString("Hose type(B,C,D): " + tmp.Letter + "<br />");
                char letter = ' ';
                if (Answer.Length == 1) letter = Answer.ToCharArray().ElementAt(0);
                if (letter == 'B' || letter == 'C' || letter == 'D') tmp.Letter = letter;
            }
            if (t.GetType() == typeof(Gasanalyzer))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Gasanalyzer tmp = (Gasanalyzer)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "<br />");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.Id + "<br />");
                if (Number != 0) tmp.Id = Number;
            }
            if (t.GetType() == typeof(Jetnozzle))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Jetnozzle tmp = (Jetnozzle)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "<br />");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.Id + "<br />");
                if (Number != 0) tmp.Id = Number;
            }
            if (t.GetType() == typeof(Distributer))
            {
                _t.Display(" \".\"(dot) for no changes (0 in case of number)" + "<br />");
                Distributer tmp = (Distributer)(object)t;

                Answer = EditHelperString("Description: " + tmp.Description + "<br />");
                if (Answer != ".") tmp.Description = Answer;

                Number = EditHelperInt("Inventory Number: " + tmp.Id + "<br />");
                if (Number != 0) tmp.Id = Number;
            }
            return t;
        }
        public void ShowAdminOptions()
        {
            _t.Display("Options:<br />" +
                       "-e    Edit base data.<br />" +
                       "-u    Use Userfunctions.<br />" +
                       "-q    Quit.<br />");
        }
        public void ShowUserOptions()
        {
            _t.Display("Options:<br />" +
                       "-v    View Deployments.<br />" +
                       "-d    Generate Deployment.<br />" +
                       "-g    Set Gasanalyzer examination.<br />" +
                       "-w    Generate Deploymentreport .<br />" +
                       "-q    Quits<br />");
        }
    }
}
