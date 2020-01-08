using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{
    public class ConfigManager
    {
        public static List<Credentials> GetCredentials()
        {
            var path = "credentials.txt";
            var lines = File.ReadAllLines(path);

            var credentialsList = new List<Credentials>();

            for (int i = 0; i < lines.Length; i += 4)
            {
                credentialsList.Add(new Credentials(lines[i], lines[i + 1], lines[i + 2]));
            }

            return credentialsList;
        }
    }
}
