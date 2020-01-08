using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Core
{
    public class ConfigManager
    {
        public static List<Credentials> GetCredentials()
        {            
            return JsonConvert.DeserializeObject<List<Credentials>>(File.ReadAllText("credentials.json"));
        }
    }
}
