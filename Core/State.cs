using Core.TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core
{
	public class State
	{
		public static State Instance { get; set; } = new State(new List<Category>(), new SortedSet<Transaction>());
		
		public Dictionary<string, Func<Transaction, bool>> CategoryFilters { get; }

		public List<Category> Categories { get; }

		public SortedSet<Transaction> Transactions { get; }

		// todo: move to IReadOnlySet in .NET 5
		public IReadOnlyCollection<string> CategoriesNames => CategoryFilters.Keys;

		[JsonConstructor]
		public State(List<Category> categories, SortedSet<Transaction> transactions)
		{
			Transactions = transactions;
			Categories = categories;
			CategoryFilters = CategoriesManager.BuildFilters(categories);
		}
	}
}
