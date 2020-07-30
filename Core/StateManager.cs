using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Importers;
using Newtonsoft.Json;

namespace Core
{
	public class StateManager
	{
		private static Dictionary<string, ITransactionsImporter> importers = new Dictionary<string, ITransactionsImporter>
		{
			["pb"] = new PrivatTransactionsImporter(),
			//["usb"] = new UkrSibTransactionsImporter()
		};

		public static string SaveCategoriesToJson()
		{
			return JsonConvert.SerializeObject(State.Instance.Categories);
		}

		public static void LoadCategories(string categoriesJson)
		{
			if (string.IsNullOrWhiteSpace(categoriesJson))
			{
				return;
			}

			var categories = JsonConvert.DeserializeObject<ICollection<Category>>(categoriesJson);
			State.Instance = new State(categories, State.Instance.Transactions);
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

			State.Instance = new State(categories, transactions.ToHashSet());			
		}
	}
}
