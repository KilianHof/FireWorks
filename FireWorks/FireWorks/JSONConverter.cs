using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace FireWorks
{
    class JSONConverter
    {
        public static string ObjectToJSON(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.None);
        }
        public static object JSONToObject(string str)
        {
            return JsonConvert.DeserializeObject<Deployment>(str);
        }
    }
}
