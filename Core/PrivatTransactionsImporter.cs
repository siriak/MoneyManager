using System;
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
		public static async Task ImportTransactions(Credentials credentials, Action<List<Transaction>> handler)
		{
			for (var date = DateTime.Now.ToDate(); date.Year > 2015; date -= TimeSpan.FromDays(1))
			{
				handler(await LoadData(date, credentials));
			}
		}

		private static async Task<List<Transaction>> LoadData(Date date, Credentials credentials)
		{
			var data =
				$"<oper>cmt</oper><wait>60</wait><payment><prop name=\"sd\" value=\"{date.Day}.{date.Month}.{date.Year}\"/><prop name=\"ed\" value=\"{date.Day}.{date.Month}.{date.Year}\"/><prop name=\"card\" value=\"{credentials.CardNumber}\"/></payment>";
			var md5 = MD5.Create();
			var sha1 = SHA1.Create();

			var secret = data + credentials.MerchantPassword;
			var bytes = Encoding.UTF8.GetBytes(secret);
			var md5h = md5.ComputeHash(bytes);
			var md5hs = ByteArrayToString(md5h);
			var sha1h = sha1.ComputeHash(Encoding.UTF8.GetBytes(md5hs));
			var signature = ByteArrayToString(sha1h);

			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://api.privatbank.ua/p24api/rest_fiz"),
				Content = new StringContent(
					$"<?xml version=\"1.0\" encoding=\"UTF-8\"?><request version=\"1.0\"><merchant><id>{credentials.MerchantId}</id><signature>{signature}</signature></merchant><data>{data}</data></request>",
					Encoding.Default,
					"application/xml")
			};
			var res = await client.SendAsync(request);

			var xml = await res.Content.ReadAsStringAsync();

			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);

			const string xpath = "response/data/info/statements/statement";
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
			var hex = new StringBuilder(ba.Length * 2);
			foreach (var b in ba)
			{
				_ = hex.AppendFormat("{0:x2}", b);
			}

			return hex.ToString();
		}

		public static Transaction GetTransactionFromXml(XmlNode node)
		{
			if (node is null)
			{
				throw new ArgumentNullException($"{nameof(node)} cannot be null");
			}

			var cartNumber = node.Attributes["card"].Value;
			var appCode = node.Attributes["appcode"].Value;
			var timeStamp = DateTimeOffset.Parse(node.Attributes["trandate"].Value + " " + node.Attributes["trantime"].Value);
			var amount = ParseMoney(node.Attributes["cardamount"]);
			var rest = ParseMoney(node.Attributes["rest"]);
			var terminal = node.Attributes["terminal"].Value;
			var description = node.Attributes["description"].Value;
			return new Transaction(cartNumber, appCode, timeStamp, amount, rest, terminal, description);
		}

		private static Money ParseMoney(XmlAttribute money)
		{
			var amountSplit = money.Value.Split(' ');
			var amount = double.Parse(amountSplit[0]);
			var currency = (Currency) Enum.Parse(typeof(Currency), amountSplit[1]);
			return new Money(amount, currency);
		}
	}
}
