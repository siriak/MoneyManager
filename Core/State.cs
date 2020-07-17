using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core
{
	public class State
	{
		public static State Instance { get; set; } = new State(new List<Category>(), new SortedSet<Transaction>());

		public SortedSet<Transaction> Transactions { get; }

		public IList<Category> Categories { get; }
		
		[JsonIgnore]
		public Dictionary<string, Func<Transaction, bool>> CategoryFilters { get; }

		// todo: move to IReadOnlySet in .NET 5
		[JsonIgnore]
		public IReadOnlyCollection<string> CategoriesNames => CategoryFilters.Keys;

		[JsonConstructor]
		public State(IList<Category> categories, SortedSet<Transaction> transactions)
		{
			Transactions = transactions;
			Categories = categories;
			CategoryFilters = (Dictionary <string, Func<Transaction, bool>>)CategoriesManager.BuildFilters(categories);
		}
	}
}
