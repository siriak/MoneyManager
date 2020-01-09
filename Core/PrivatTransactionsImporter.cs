using System;
using System.Linq; 
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core
{
    public class PrivatTransactionsImporter
    {
        public async void ImportTransactions(string cardNumber, string merchantPassword, Action<List<Transaction>> handler)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            for (; month > 0; month--)
            {
                for (; day > 0; day--)
                {
                    handler(await LoadData(year, month, day, cardNumber, merchantPassword));
                }
            }

            year--;
            for (; year > 2015; year--)
            {
                for (month = 12; month > 0; month--)
                {
                    for (day = 31; day > 0; day--)
                    {
                        handler(await LoadData(year, month, day, cardNumber, merchantPassword));
                    }
                }
            }
        }

        private async Task<List<Transaction>> LoadData(int year, int month, int day, string cardNumber, string merchantPassword)
        {           
            var data = $"<oper>cmt</oper><wait>60</wait><payment><prop name=\"sd\" value=\"{day}.{month}.{year}\"/><prop name=\"ed\" value=\"{day}.{month}.{year}\"/><prop name=\"card\" value=\"{cardNumber}\"/></payment>";
            var md5 = MD5.Create();
            var sha1 = SHA1.Create();

            var secret = data + merchantPassword;
            var bytes = Encoding.UTF8.GetBytes(secret);
            var md5h = md5.ComputeHash(bytes);
            var md5hs = ByteArrayToString(md5h);
            var sha1h = sha1.ComputeHash(Encoding.UTF8.GetBytes(md5hs));
            var signature = ByteArrayToString(sha1h);

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.privatbank.ua/p24api/rest_fiz"),
                Content = new StringContent($"<?xml version=\"1.0\" encoding=\"UTF-8\"?><request version=\"1.0\"><merchant><id>154628</id><signature>{signature}</signature></merchant><data>{data}</data></request>",
                    Encoding.Default, "application/xml"),
            };
            var res = await client.SendAsync(request);

            var xml = await res.Content.ReadAsStringAsync();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            string xpath = "response/data/info/statements/statement";
            var nodes = xmlDoc.SelectNodes(xpath);
            
            var list = new List<Transaction>();
            foreach (XmlNode childrenNode in nodes)
            {
                list.Add(GetTransactionFromXml(childrenNode));
            }
            return list;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private static Transaction GetTransactionFromXml(XmlNode node)
        {
            // rtb.Items.Add($"{day:d2}.{month:d2}.{year}) [{childrenNode.Attributes["cardamount"].Value}] {childrenNode.Attributes["description"].Value}");
            throw new NotImplementedException();
        }
    }
}
