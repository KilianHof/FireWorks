using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FireWorks
{
    /// <summary>
    /// Wrapper for writing to a file.
    /// </summary>
    /// 

    public class FileIO : IDataLayer
    {
        private static string[] _paths;

        private const int Deployments = 0;
        private const int Employees = 1;
        private const int Vehicles = 2;
        private const int Resources = 3;
        private const int Firefighters = 4;

        private readonly IUserLayer _t;
        public FileIO(IUserLayer tui) { _t = tui; }
        /// <summary>
        /// Checks if the needed files exist and calls Init() to create missing files.
        /// </summary>
        public void CheckForFiles()
        {
            string f = @"C:\Users\khof\Desktop\FWFiles";
            f += @"\Files";
            if (!Directory.Exists(f))
            {
                Directory.CreateDirectory(f);
            }
            string[] _path = new string[] {
                f +@"\Deployments.txt",
                f +@"\Employee.txt",
                f +@"\Vehicles.txt",
                f +@"\Resources.txt",
                f +@"\FireFighters.txt",
            };
            _paths = _path;
            bool[] pathsExist = new bool[] { true, true, true, true, true };
            for (int i = 0; i < _paths.Length; i++)
            {
                if (!(File.Exists(_paths[i]))) { pathsExist[i] = false; }
            }
            Init(pathsExist);
        }
        /// <summary>
        /// Intialization of FileIO missing files are created.
        /// </summary>
        /// <param name="existing"></param>
        private void Init(bool[] existing)
        {

            bool isFine = true; //checkt ob eine datei fehlt. wenn eine datei fehlt wird "isFine" auf falsch gesetzt.
            foreach (var item in existing)
            {
                if (!item)
                    isFine = false;
            }
            if (!isFine)
            {
                _t.Display("Seems like some files dont exist. Do you wish to initialize missing files?(Admin PIN=0000)<br />");
                if (_t.GetBool())
                {

                    _t.Display("Do you wish to create some default basedata?");
                    bool CreationMode = _t.GetBool();

                    for (int i = 0; i < _paths.Length; i++)
                    {
                        if (!existing[i])
                        {
                            Byte[] info;
#pragma warning disable IDE0063 // Use simple 'using' statement
                            using (FileStream fs = File.Create(_paths[i]))
#pragma warning restore IDE0063 // Use simple 'using' statement
                            {
                                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                                if (i == 1)
                                {
                                    string PIN = "0000";
                                    info = new UTF8Encoding(true).GetBytes(@"{""PIN"":""" + PIN.GetHashCode().ToString() + @""",""Status"":""ADMIN"",""Id"":1,""FirstName"":""FirstName"",""LastName"":""LastName""}");
                                    fs.Write(info, 0, info.Length);

                                }
                                else
                                {
                                    if (CreationMode)
                                    {
                                        Byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                                        string temp = "";
                                        switch (i)
                                        {
                                            case 2:
                                                Pkw p = new Pkw("Pkw", 80, 4);
                                                Firetruck ft = new Firetruck("LFZ", 200, 4, false, 400);
                                                Ambulance a = new Ambulance("Ambulance", 150, 6, 300);
                                                Turntableladder tl = new Turntableladder("Turntableladder", 300, 4, true, 20);

                                                temp = JSONConverter.ObjectToJSON(p);
                                                sw.WriteLine(temp);

                                                temp = JSONConverter.ObjectToJSON(ft);
                                                sw.WriteLine(temp);

                                                temp = JSONConverter.ObjectToJSON(a);
                                                sw.WriteLine(temp);

                                                temp = JSONConverter.ObjectToJSON(tl);
                                                sw.Write(temp);
                                                break;
                                            case 3:
                                                Hose h = new Hose("A regular Hose", 1, 'B', 20);
                                                Gasanalyzer ga = new Gasanalyzer("A regular gasanalyzer", 2);
                                                Distributer d = new Distributer("A regular Distributer", 3);
                                                Jetnozzle jn = new Jetnozzle("A regular Jetnozzle", 4);

                                                temp = JSONConverter.ObjectToJSON(h);
                                                sw.WriteLine(temp);

                                                temp = JSONConverter.ObjectToJSON(ga);
                                                sw.WriteLine(temp);

                                                temp = JSONConverter.ObjectToJSON(d);
                                                sw.WriteLine(temp);

                                                temp = JSONConverter.ObjectToJSON(jn);
                                                sw.Write(temp);
                                                break;
                                            case 4:
                                                FireFighter ff1 = new FireFighter("Max", "Mustermann", 1);
                                                FireFighter ff2 = new FireFighter("Marina", "Musterfrau", 2);

                                                temp = JSONConverter.ObjectToJSON(ff1);
                                                sw.WriteLine(temp);

                                                temp = JSONConverter.ObjectToJSON(ff2);
                                                sw.Write(temp);
                                                break;
                                            default:
                                                info = new UTF8Encoding(true).GetBytes("");
                                                fs.Write(info, 0, info.Length);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        info = new UTF8Encoding(true).GetBytes("");
                                        fs.Write(info, 0, info.Length);
                                    }
                                }
                                sw.Close();
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Gets a list of all users from file.
        /// </summary>
        /// <returns>returns a string[] containing Users</returns>
        public string[] GetListOfUsers()
        {
            List<string> results = new List<string>();
            string Path = _paths[Employees];
            if (File.Exists(Path))
            {
                int lineCount = File.ReadLines(Path).Count();
                if (!(lineCount == 0))
                    for (int i = 1; i <= lineCount; i++)
                    {
                        results.Add(File.ReadLines(Path).Skip(i - 1).Take(1).First());
                    }
                else
                {
                    _t.Display("cannot read line \"0\" or negative.(file empty?)" + "<br />");
                    _t.Display("No lines indicates no stored users." + "<br />");
                }
                return results.ToArray();
            }
            _t.Display("cannot read file: " + Path + "<br />");
            return results.ToArray();
        }
        /// <summary>
        /// reads files.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>returns a list of objects of type T</returns>
        public List<T> ReadFile<T>()
        {
            bool needCast = false;
            int path = Pathfinder<T>();

            List<object> tmp = new List<object>();
            List<T> test = new List<T>();
            object trial;
            if (File.Exists(_paths[path]))
            {
                foreach (string line in File.ReadLines(_paths[path]))
                {
                    trial = JSONConverter.JSONToGeneric<T>(line);
                    if (trial.GetType() == typeof(Vehicle))
                    {
                        tmp.Add(ObjectCast<Vehicle>(line));
                        needCast = true;
                    }
                    if (trial.GetType() == typeof(Resources))
                    {
                        tmp.Add(ObjectCast<Resources>(line));
                        needCast = true;
                    }
                    if (trial.GetType() == typeof(Deployment))
                    {
                        Deployment prototype = (Deployment)trial;

                        List<Vehicle> v = new List<Vehicle>();
                        List<Resources> r = new List<Resources>();
                        List<FireFighter> f = new List<FireFighter>();
                        string cutting = line;

                        object testNull;

                        int firstIndex = cutting.IndexOf('[') + 1;          // first occourence marks array
                        int lastIndex = cutting.IndexOf(']');           // first occourence marks end of array

                        string cutted = cutting.Substring(firstIndex, lastIndex - firstIndex); // extract everything between [ and ]
                        cutting = cutting.Substring(lastIndex + 1, cutting.Length - lastIndex - 1);    // remove everything from start till end of array"]"

                        testNull = CreateArray(cutted);
                        string[] VehObjects;
                        if (!(testNull == null))
                        {
                            VehObjects = (string[])testNull;
                            foreach (var item in VehObjects)
                            {
                                v.Add((Vehicle)ObjectCast<Vehicle>(item));
                            }
                        }
                        firstIndex = cutting.IndexOf('[') + 1;          // first occourence marks array
                        lastIndex = cutting.IndexOf(']');           // first occourence marks end of array

                        cutted = cutting.Substring(firstIndex, lastIndex - firstIndex); // extract everything between [ and ]
                        cutting = cutting.Substring(lastIndex + 1, cutting.Length - lastIndex - 1);    // remove everything from start till end of array"]"

                        testNull = CreateArray(cutted);
                        string[] ResObjects;
                        if (!(testNull == null))
                        {
                            ResObjects = (string[])testNull;
                            foreach (var item in ResObjects)
                            {
                                r.Add((Resources)ObjectCast<Resources>(item));
                            }
                        }
                        firstIndex = cutting.IndexOf('[') + 1;          // first occourence marks array
                        lastIndex = cutting.IndexOf(']');           // first occourence marks end of array

                        cutted = cutting.Substring(firstIndex, lastIndex - firstIndex); // extract everything between [ and ]
                        cutting = cutting.Substring(lastIndex + 1, cutting.Length - lastIndex - 1);    // remove everything from start till end of array"]"

                        testNull = CreateArray(cutted);
                        string[] FFObjects;
                        if (!(testNull == null))
                        {
                            FFObjects = (string[])testNull;
                            foreach (var item in FFObjects)
                            {
                                f.Add(JSONConverter.JSONToGeneric<FireFighter>(item));
                            }
                        }
                        Deployment fin = new Deployment(prototype.DateAndTime, prototype.Location, v.ToArray(), r.ToArray(), f.ToArray(), prototype.Comment, prototype.Id);
                        tmp.Add(fin);
                        needCast = true;
                    }
                    test.Add(JSONConverter.JSONToGeneric<T>(line));

                }

                if (needCast) return ConvertList<T>(tmp);
                return test;
            }
            _t.Display("cannot read file: " + _paths[path] + "<br />");
            return test;
        }
        /// <summary>
        /// A helper function used to Disassemble a Json string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string[] CreateArray(string value)
        {
            if (value == "") return null;
            List<string> toArray = new List<string>();
            int lastIndex;
            string obj;
            do
            {
                lastIndex = value.IndexOf('}') + 1;                            // set last index to end of 
                obj = value.Substring(0, lastIndex);
                toArray.Add(obj);

                lastIndex++;
                value = value.Substring(lastIndex, value.Length - lastIndex);


            } while (value[0] == ',');
            lastIndex = value.IndexOf('}') + 1;                            // set last index to end of 
            obj = value.Substring(0, lastIndex);
            toArray.Add(obj);

            return toArray.ToArray();

        }
        /// <summary>
        /// Used to turn Json strings to Objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="line"></param>
        /// <returns> An object of type T</returns>
        private object ObjectCast<T>(string line)
        {
            object trial = JSONConverter.JSONToGeneric<T>(line);
            if (trial.GetType() == typeof(Resources))
            {
                Resources prototype = (Resources)trial;
                if (prototype.GetIdentifier() == "Hose")
                {
                    return (JSONConverter.JSONToGeneric<Hose>(line));
                }
                if (prototype.GetIdentifier() == "Jetnozzle")
                {
                    return (JSONConverter.JSONToGeneric<Jetnozzle>(line));
                }
                if (prototype.GetIdentifier() == "Distributer")
                {
                    return (JSONConverter.JSONToGeneric<Distributer>(line));
                }
                if (prototype.GetIdentifier() == "Gasanalyzer")
                {
                    return (JSONConverter.JSONToGeneric<Gasanalyzer>(line));
                }
            }
            if (trial.GetType() == typeof(Vehicle))
            {
                trial = JSONConverter.JSONToGeneric<Vehicle>(line);
                Vehicle prototype = (Vehicle)trial;
                if (prototype.GetIdentifier() == "Pkw")
                {
                    return (JSONConverter.JSONToGeneric<Pkw>(line));
                }
                if (prototype.GetIdentifier() == "Firetruck")
                {
                    return (JSONConverter.JSONToGeneric<Firetruck>(line));
                }
                if (prototype.GetIdentifier() == "Ambulance")
                {
                    return (JSONConverter.JSONToGeneric<Ambulance>(line));
                }
                if (prototype.GetIdentifier() == "Turntableladder")
                {
                    return (JSONConverter.JSONToGeneric<Turntableladder>(line));
                }
            }
            return null;
        }
        /// <summary>
        /// turns List of objects to List of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns> a List of type T</returns>
        private List<T> ConvertList<T>(List<object> value)
        {
            List<T> tmp = new List<T>();

            for (int i = 0; i < value.Count(); i++)
            {
                tmp.Add((T)value.ElementAt(i));
            }

            return tmp;
        }

        public object[] ReadAllLists()
        {
            List<Deployment> Deploys = ReadFile<Deployment>();
            List<User> Employs = ReadFile<User>();
            List<Vehicle> Vehicles = ReadFile<Vehicle>();
            List<Resources> Resources = ReadFile<Resources>();
            List<FireFighter> FireFighters = ReadFile<FireFighter>();
            return new object[] { Deploys, Employs, Vehicles, Resources, FireFighters };

        }
        public void SaveListToFile<T>(List<T> liste)
        {
            int path = Pathfinder<T>();
            string[] str = new string[liste.Count];
            int i = 0;

            if (liste.Count == 0) { return; }
            if (path == -1) { return; }
            if (!File.Exists(_paths[path])) { _t.Display("cannot read file: " + _paths[path] + "<br />"); return; }

            foreach (var item in liste)
            {
                str[i] += JSONConverter.ObjectToJSON(item);
                i++;
            }


            File.WriteAllLines(_paths[path], str);

        }
        public void SaveAllLists(object[] lists)
        {
            SaveListToFile<Deployment>((List<Deployment>)lists[Deployments]); //warum kann ich das vereinfachen?
            SaveListToFile<User>((List<User>)lists[Employees]);
            SaveListToFile<Vehicle>((List<Vehicle>)lists[Vehicles]);
            SaveListToFile<Resources>((List<Resources>)lists[Resources]);
            SaveListToFile<FireFighter>((List<FireFighter>)lists[Firefighters]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generic"></param>
        /// <param name="toCheck"></param>
        /// <returns>true if toCheck is a subclass of generic</returns>
        private bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur) { return true; }
                toCheck = toCheck.BaseType;
            }

            return false;
        }
        /// <summary>
        /// Determines wich path an object is saved to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>int to reference path array</returns>
        private int Pathfinder<T>()
        {
            int path = -1;

            if (IsSubclassOfRawGeneric(typeof(T), typeof(Deployment))) { path = Deployments; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(User))) { path = Employees; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Vehicle))) { path = Vehicles; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Resources))) { path = Resources; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(FireFighter))) { path = Firefighters; }

            return path;
        }
    }
}
