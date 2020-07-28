using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Importers;
using Core.TimeSeries;
using Newtonsoft.Json;

namespace Core
{
	public class StateManager
	{
		private static Dictionary<string, TransactionsImporter> importers = new Dictionary<string, TransactionsImporter>
		{
			["pb"] = new PrivatTransactionsImporter(),
		};

		public static void LoadState(string stateJson)
		{
			if (string.IsNullOrWhiteSpace(stateJson))
			{
				return;
			}

			State.Instance = JsonConvert.DeserializeObject<State>(stateJson);
		}

		public static string SaveToJson()
		{
			return JsonConvert.SerializeObject(State.Instance);
		}

		public static void LoadTransactions(IEnumerable<(string key, Stream stream)> files)
		{
			var newTransactions = new List<Transaction>();
			foreach (var file in files)
			{
				newTransactions.AddRange(importers[file.key].Load(file.stream));
			}

			var newCategories = newTransactions.Select(t => t.Category).Where(c => c is { } && State.Instance.Categories.All(sc => sc.Name != c))
				.Distinct().Select(c => new Category(c, new Rule[] { new Rule(".*", "*", ".*", c) }, 1, 10000)).ToList();

			var categories = new List<Category>(); 
			categories.AddRange(State.Instance.Categories);
			categories.AddRange(newCategories);

			var transactions = new List<Transaction>();
			transactions.AddRange(State.Instance.Transactions);
			transactions.AddRange(newTransactions);

			State.Instance = new State(categories, transactions);			
		}

	}
}
