using System;
using System.Collections.Generic;
using System.Linq;
using Core.TimeSeries;
using System.Text.RegularExpressions;
using Core.Categories;
using Newtonsoft.Json;

namespace Core
{
    public static class StateHelper
    {
        public static Func<Transaction, bool> GetFilterForCategory(string categoryName)
        {
            if (State.Instance.Categories.All(c=>c.Name != categoryName))
            {
                return t => false;
            }
            var c = State.Instance.Categories.FirstOrDefault(c => c.Name == categoryName);
            switch (c)
            {
                case AutoCategory autoCategory:
                    return t => t.Category == autoCategory.Category;
                case RegexCategory regexCategory:
                    return t => regexCategory.Rules.Any(r => Regex.IsMatch(t.CardNumber, r.CardNumber)
                                                && Regex.IsMatch(t.Description, r.Description)
                                                && Regex.IsMatch(t.Category, r.Category)
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
                case CompositeCategory compositeCategory:
                {
                    var filters = compositeCategory.Categories.Select(GetFilterForCategory).ToArray();
                    return t => filters.Any(f => f(t));
                }
                case null:
                    return t => false;
                default:
                    throw new NotSupportedException($"Category type {c.GetType()} is not supported");
            }
        }

        public static IEnumerable<string> GetAllMatchingCategories(this State state, Transaction transaction)
        {
            return state.Categories
                .Where(c => GetFilterForCategory(c.Name)(transaction))
                .Select(c => c.Name)
                .ToList();
        }

        public static IEnumerable<string> GetAllMatchingCategoriesOfType<T>(this State state, Transaction transaction) where T : Category
        {
            return state.Categories
                .OfType<T>()
                .Where(c => GetFilterForCategory(c.Name)(transaction))
                .Select(c => c.Name)
                .ToList();
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
            var filteredTransactions = State.Instance.Transactions
                                                   .Where(t => t.Date >= start && t.Date <= end)
                                                   .Where(GetFilterForCategory(category))
                                                   .OrderBy(t => t.Date)
                                                   .ToList();
            return filteredTransactions;
        }

        public static IEnumerable<Transaction> GetTransactionsUnion(IEnumerable<string> categories, Date start, Date end)
        {
            Func<Transaction, bool> filter = t => categories.Any(c => GetFilterForCategory(c)(t));
            return State.Instance.Transactions
                               .Where(t => t.Date >= start && t.Date <= end)
                               .Where(filter)
                               .OrderBy(t => t.Date)
                               .ToList();
        }

        public static string SaveTransactionsToJson(this State state)
        {
            return JsonConvert.SerializeObject(state.Transactions, Formatting.Indented);
        }

        public static IEnumerable<Transaction> ParseTransactions(string transactionsJson) =>
            string.IsNullOrWhiteSpace(transactionsJson)
                ? new List<Transaction>()
                : JsonConvert.DeserializeObject<ICollection<Transaction>>(transactionsJson);
    }
}
