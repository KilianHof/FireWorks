using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FireWorks
{
    class Program
    {
        private const string _pathDeployment = @"C:\FireWorks\Deployments.txt";
        private const string _pathEmployee = @"C:\FireWorks\Employee.txt";
        private const string _pathVehicle = @"C:\FireWorks\Vehicles.txt";
        private const string _pathResource = @"C:\FireWorks\Resources.txt";
        private static TUI _t = new TUI();
        static void Main(string[] args)
        {
            //Console.WriteLine("0000".GetHashCode());
            //Console.WriteLine("Hello and welcome to FireWorks! \n Please Enter a four digit PIN to continue.");


            Authenticator auth = new Authenticator();
            auth.NeedOutput += OutputEvent;
            auth.NeedBoolInput += BoolInputEvent;
            auth.NeedStringInput += StringInputEvent;
            string mode = auth.LogIn();

            //Deployment test = DeploymentFactory.PromptDeployment();
            //FileIO filer = new FileIO();
            //filer.NeedOutput += OutputEvent;
            //filer.WriteObject(test, _path);

            //User user = new User("m", "p", 2, "USER", "15947562");
            //filer.WriteObject(user, _pathEmployee);


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
            }






            Console.ReadLine();
        }
        public static int Valid(int num, int from, int to)
        {
            while (num < from || num > to)
            {
                _t.Display("Invalid Input. Try again.\n");
                num = _t.GetInt();
            }
            return num;
        }
        public static void AdminMode(string sel)
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

                    break;
                case "-q":
                    System.Environment.Exit(1);
                    break;
            }


        }
        public static void Editing(int file, int mode)
        {
            switch (file)
            {
                case 1:
                    List<object> employees = new List<object>();

                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
        public static void ShowAdminOptions()
        {
            _t.Display("Options:\n" +
                       "-e\tEdit base data.\n" +
                       "-q\tQuit.\n");
        }
        public static void ShowUserOptions()
        {
            _t.Display("Options:\n" +
                       "-v\tView Deployments.\n" +
                       "-u\tManage Users\n"
                       );
        }
        static void OutputEvent(string str)
        {
            _t.Display(str);
        }
        static bool BoolInputEvent()
        {
            return _t.GetBool();
        }
        static string StringInputEvent()
        {
            return _t.GetString();
        }
    }
}
