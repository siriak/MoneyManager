using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.TimeSeries;
using Newtonsoft.Json;

namespace Core
{
	public class StateManager
	{
		public static void LoadFromJson(string stateJson)
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
		
		public static SmoothedTimeSeries GetSmoothedTimeSeries(string category, double smoothingRatio)
		{
			var filteredTransactions = State.Instance.Transactions.Where(State.Instance.CategoryFilters[category]).ToList();
			return new SmoothedTimeSeries(filteredTransactions, smoothingRatio);
		}

		public static CumulativeTimeSeries GetCumulativeTimeSeries(string category, double increment, double capacity)
		{
			var filteredTransactions = State.Instance.Transactions.Where(State.Instance.CategoryFilters[category]).ToList();
			return new CumulativeTimeSeries(filteredTransactions, increment, capacity);
		}

		public static SmoothedTimeSeries GetSmoothedTimeSeriesUnion(IEnumerable<string> categories, double smoothingRatio)
		{
			Func<Transaction, bool> filter = t => categories.Any(c => State.Instance.CategoryFilters[c](t));
			var filteredTransactions = State.Instance.Transactions.Where(filter).ToList();
			return new SmoothedTimeSeries(filteredTransactions, smoothingRatio);
		}

		public static CumulativeTimeSeries GetCumulativeTimeSeriesUnion(IEnumerable<string> categories, double smoothingRatio)
		{
			return new CumulativeTimeSeries(1, 1);
		}

		public static IEnumerable<Transaction> GetTransactions(string category, Date start, Date end)
		{
			var filteredTransactions = State.Instance.Transactions.SkipWhile(t => t.TimeStamp.DateTime < start)
			                                       .TakeWhile(t => t.TimeStamp.DateTime <= end)
			                                       .Where(State.Instance.CategoryFilters[category])
			                                       .ToList();
			return filteredTransactions;
		}

		public static IEnumerable<Transaction> GetTransactionsUnion(IEnumerable<string> categories, Date start, Date end)
		{
			Func<Transaction, bool> filter = t => categories.Any(c => State.Instance.CategoryFilters[c](t));
			return State.Instance.Transactions.SkipWhile(t => t.TimeStamp.DateTime < start)
			                   .TakeWhile(t => t.TimeStamp.DateTime <= end)
			                   .Where(filter)
			                   .ToList();
		}

	}
}
