using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
	public static class State
	{
		private static readonly Dictionary<string, Func<Transaction, bool>> categoryFilters = new Dictionary<string, Func<Transaction, bool>>();

		public static SortedSet<Transaction> Transactions { get; } = new SortedSet<Transaction>();

		public static HashSet<string> Categories => new HashSet<string>(categoryFilters.Keys);

		public static event Action OnStateUpdated = () => { };

		public static Task Init()
		{
			Transactions.UnionWith(StateManager.Load());

			foreach (var c in CategoriesManager.Load())
			{
				categoryFilters.Add(c.Key, c.Value);
			}
			OnStateUpdated();
			return Task.CompletedTask;
		}

		public static TimeSeries GetTimeSeries(string category, double smoothingRatio)
		{
			var filteredTransactions = Transactions.Where(categoryFilters[category]).ToList();
			return new TimeSeries(filteredTransactions, smoothingRatio);
		}

		public static TimeSeries GetTimeSeriesUnion(IEnumerable<string> categories, double smoothingRatio)
		{
			Func<Transaction, bool> filter = t => categories.Any(c => categoryFilters[c](t));
			var filteredTransactions = Transactions.Where(filter).ToList();
			return new TimeSeries(filteredTransactions, smoothingRatio);
		}

		public static IEnumerable<Transaction> GetTransactions(string category, Date start, Date end)
		{
			var filteredTransactions = Transactions.SkipWhile(t => t.TimeStamp.DateTime < start)
			                                       .TakeWhile(t => t.TimeStamp.DateTime <= end)
			                                       .Where(categoryFilters[category])
			                                       .ToList();
			return filteredTransactions;
		}

		public static IEnumerable<Transaction> GetTransactionsUnion(IEnumerable<string> categories, Date start, Date end)
		{
			Func<Transaction, bool> filter = t => categories.Any(c => categoryFilters[c](t));
			return Transactions.SkipWhile(t => t.TimeStamp.DateTime < start)
			                   .TakeWhile(t => t.TimeStamp.DateTime <= end)
			                   .Where(filter)
			                   .ToList();
		}
	}
}
