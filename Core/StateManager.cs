using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Core
{
	public class StateManager
	{
		public static SortedSet<Transaction> Load()
		{
			if (!File.Exists("state.json"))
			{
				return new SortedSet<Transaction>();
			}

			return JsonConvert.DeserializeObject<SortedSet<Transaction>>(File.ReadAllText("state.json"));
		}

		public static void Save()
		{
			var transactions = JsonConvert.SerializeObject(State.Transactions);
			File.WriteAllText("state.json", transactions);
		}
	}
}
