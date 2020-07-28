using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core
{
    public class State
	{
		public static State Instance { get; internal set; } = new State(new List<Category>(), new SortedSet<Transaction>());

		public IReadOnlyCollection<Category> Categories { get; }

		public IReadOnlyCollection<Transaction> Transactions { get; }

		[JsonConstructor]
		public State(IReadOnlyCollection<Category> categories, IReadOnlyCollection<Transaction> transactions)
		{
			Transactions = transactions;
			Categories = categories;
		}
	}
}
