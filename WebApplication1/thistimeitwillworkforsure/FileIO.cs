using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;

namespace FireWorks
{
    /// <summary>
    /// Wrapper for writing to a file.
    /// </summary>
    /// 

    public class FileIO : IDataLayer
    {
        private static readonly string[] _paths;

        private const int Deployments = 0;
        private const int Employees = 1;
        private const int Vehicles = 2;
        private const int Resources = 3;
        private const int Firefighters = 4;

        private readonly IUserLayer _t;
        public FileIO(IUserLayer tui) { _t = tui; }
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
                    if (trial.GetType() == typeof(Resource))
                    {
                        tmp.Add(ObjectCast<Resource>(line));
                        needCast = true;
                    }
                    if (trial.GetType() == typeof(Deployment))
                    {
                        Deployment prototype = (Deployment)trial;

                        List<Vehicle> v = new List<Vehicle>();
                        List<Resource> r = new List<Resource>();
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
                                r.Add((Resource)ObjectCast<Resource>(item));
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
            if (trial.GetType() == typeof(Resource))
            {
                Resource prototype = (Resource)trial;
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
            if (Globals.sql == true)
            {
                List<Deployment> Deploys;
                using (var context = new thistimeitwillworkforsure.DBContext())
                {
                    Deploys = context.Deployments.ToList();
                }
                List<Deployment> tmpDeploys = new List<Deployment>();
                foreach (var Deployment in Deploys)
                {
                    tmpDeploys.Add(JsonConvert.DeserializeObject<Deployment>(Deployment.JSON));
                }

                List<Resource> Resources;       //need cast
                using (var context = new thistimeitwillworkforsure.DBContext())
                {
                    Resources = context.Resources.ToList();
                }
                List<object> tmpResources = new List<object>();
                foreach (var Resource in Resources)
                {
                    tmpResources.Add(ObjectCast<Resource>(Resource.JSON));
                }
                Resources = ConvertList<Resource>(tmpResources);

                List<Vehicle> Vehicles;     //need cast
                using (var context = new thistimeitwillworkforsure.DBContext())
                {
                    Vehicles = context.Vehicles.ToList();
                }
                List<object> tmpVehicles = new List<object>();
                foreach (var Vehicle in Vehicles)
                {
                    tmpVehicles.Add(ObjectCast<Vehicle>(Vehicle.JSON));
                }
                Vehicles = ConvertList<Vehicle>(tmpVehicles);

                List<User> Employs;
                using (var context = new thistimeitwillworkforsure.DBContext())
                {
                    Employs = context.Users.ToList();
                }
                List<User> tmpUsers = new List<User>();
                foreach (var User in Employs)
                {
                    tmpUsers.Add(JsonConvert.DeserializeObject<User>(User.JSON));
                }

                List<FireFighter> FireFighters;
                using (var context = new thistimeitwillworkforsure.DBContext())
                {
                    FireFighters = context.FireFighters.ToList();
                }
                List<FireFighter> tmpFireFighters = new List<FireFighter>();
                foreach (var FireFighter in FireFighters)
                {
                    tmpFireFighters.Add(JsonConvert.DeserializeObject<FireFighter>(FireFighter.JSON));
                }
                //
                return new object[] { tmpDeploys, tmpUsers, Vehicles, Resources, tmpFireFighters };

            }
        }
        public void SaveAllLists(object[] lists)
        {

            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = thistimeitwillworkforsure.DBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            con.Open();
            string sql = @"TRUNCATE TABLE Users;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();

            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = thistimeitwillworkforsure.DBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            con.Open();
            sql = @"TRUNCATE TABLE FireFighters;";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();

            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = thistimeitwillworkforsure.DBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            con.Open();
            sql = @"TRUNCATE TABLE Vehicles;";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();

            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = thistimeitwillworkforsure.DBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            con.Open();
            sql = @"TRUNCATE TABLE Deployments;";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();

            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = thistimeitwillworkforsure.DBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            con.Open();
            sql = @"TRUNCATE TABLE Resources;";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();

            using (var context = new thistimeitwillworkforsure.DBContext())
            {
                foreach (var User in (List<User>)lists[Employees])
                {
                    User.JSON = JsonConvert.SerializeObject(User, Formatting.None);
                }
                foreach (var User in (List<User>)lists[Employees])
                {
                    context.Users.Add(User);
                }
                context.SaveChanges();
            }
            using (var context = new thistimeitwillworkforsure.DBContext())
            {
                foreach (var fireFighter in (List<FireFighter>)lists[Firefighters])
                {
                    fireFighter.JSON = JsonConvert.SerializeObject(fireFighter, Formatting.None);
                }
                foreach (var fireFighter in (List<FireFighter>)lists[Firefighters])
                {
                    context.FireFighters.Add(fireFighter);
                }
                context.SaveChanges();
            }
            using (var context = new thistimeitwillworkforsure.DBContext())
            {
                foreach (var vehicle in (List<Vehicle>)lists[Vehicles])
                {
                    vehicle.JSON = JsonConvert.SerializeObject(vehicle, Formatting.None);
                }
                foreach (var vehicle in (List<Vehicle>)lists[Vehicles])
                {
                    context.Vehicles.Add(vehicle);
                }
                context.SaveChanges();
            }
            using (var context = new thistimeitwillworkforsure.DBContext())
            {
                foreach (var deployment in (List<Deployment>)lists[Deployments])
                {
                    deployment.JSON = JsonConvert.SerializeObject(deployment, Formatting.None);
                }
                foreach (var deployment in (List<Deployment>)lists[Deployments])
                {
                    context.Deployments.Add(deployment);
                }
                context.SaveChanges();
            }
            using (var context = new thistimeitwillworkforsure.DBContext())
            {
                foreach (var resources in (List<Resource>)lists[Resources])
                {
                    resources.JSON = JsonConvert.SerializeObject(resources, Formatting.None);
                }
                foreach (var resources in (List<Resource>)lists[Resources])
                {
                    context.Resources.Add(resources);
                }
                context.SaveChanges();
            }
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
            if (IsSubclassOfRawGeneric(typeof(T), typeof(Resource))) { path = Resources; }
            if (IsSubclassOfRawGeneric(typeof(T), typeof(FireFighter))) { path = Firefighters; }

            return path;
        }
    }
}
