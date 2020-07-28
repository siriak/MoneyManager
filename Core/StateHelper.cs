using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.TimeSeries;
using System.Text.RegularExpressions;

namespace Core
{
    public static class StateHelper
    {
		public static Func<Transaction, bool> GetFilterForCategory(string categoryName)
		{
			var c = State.Instance.Categories.Single(c => c.Name == categoryName);
			return t => c.Rules.Any(r => Regex.IsMatch(t.CardNumber, r.CardNumber)
											&& Regex.IsMatch(t.Description, r.Description)
											&& (r.Category is "*" || t.Category == r.Category)
											&& r.Amount[1..] is var ruleAmount
											&& r.Amount[0] switch
											{
												'>' => t.Amount.Amount > int.Parse(ruleAmount),
												'<' => t.Amount.Amount < int.Parse(ruleAmount),
												'=' => t.Amount.Amount == int.Parse(ruleAmount),
												'*' => true,
												_ => throw new NotSupportedException()
											}
											);
		}

		public static IEnumerable<string> GetAllMatchingCategories(this State state, Transaction transaction)
        {
            return State.Instance.Categories.Select(c => c.Name).Where(c => GetFilterForCategory(c)(transaction)).ToList();
        }

		public static SmoothedTimeSeries GetSmoothedTimeSeries(string category, double smoothingRatio)
		{
			var filteredTransactions = State.Instance.Transactions.Where(GetFilterForCategory(category)).ToList();
			return new SmoothedTimeSeries(filteredTransactions, smoothingRatio);
		}

		public static CumulativeTimeSeries GetCumulativeTimeSeries(string category, double increment, double capacity)
		{
			var filteredTransactions = State.Instance.Transactions.Where(GetFilterForCategory(category)).ToList();
			return new CumulativeTimeSeries(filteredTransactions, increment, capacity);
		}

		public static SmoothedTimeSeries GetSmoothedTimeSeriesUnion(IEnumerable<string> categories, double smoothingRatio)
		{
			Func<Transaction, bool> filter = t => categories.Any(c => GetFilterForCategory(c)(t));
			var filteredTransactions = State.Instance.Transactions.Where(filter).ToList();
			return new SmoothedTimeSeries(filteredTransactions, smoothingRatio);
		}

		public static CumulativeTimeSeries GetCumulativeTimeSeriesUnion(IEnumerable<string> categories, double smoothingRatio)
		{
			return new CumulativeTimeSeries(1, 1);
		}

		public static IEnumerable<Transaction> GetTransactions(string category, Date start, Date end)
		{
			var filteredTransactions = State.Instance.Transactions.SkipWhile(t => t.Date < start)
												   .TakeWhile(t => t.Date <= end)
												   .Where(GetFilterForCategory(category))
												   .ToList();
			return filteredTransactions;
		}

		public static IEnumerable<Transaction> GetTransactionsUnion(IEnumerable<string> categories, Date start, Date end)
		{
			Func<Transaction, bool> filter = t => categories.Any(c => GetFilterForCategory(c)(t));
			return State.Instance.Transactions.SkipWhile(t => t.Date < start)
							   .TakeWhile(t => t.Date <= end)
							   .Where(filter)
							   .ToList();
		}
	}
}
