using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public static class State
    {
        public static SortedSet<Transaction> Transactions { get; } = new SortedSet<Transaction>();
        public static HashSet<string> Categories { get; } = new HashSet<string>() { "All", "Income", "Expences" };
        public static event Action OnStateUpdated;
        private static Dictionary<string, Func<Transaction, bool>> categoryFilters { get; } = new Dictionary<string, Func<Transaction, bool>>();

        static State()
        {
            categoryFilters.Add("All", t => true);
            categoryFilters.Add("Income", t => t.IsIncome);
            categoryFilters.Add("Expences", t => t.IsExpence);
        }

        public static Task Init()
        {
            Transactions.UnionWith(StateManager.Load());
            OnStateUpdated?.Invoke();

            // TODO: Set up category filters with custom filters from config file

            var credentials = ConfigManager.GetCredentials();

            return Task.WhenAll(
                credentials.Select(c => PrivatTransactionsImporter.ImportTransactions(c, ts =>
                {
                    if (ts.All(Transactions.Contains))
                    {
                        return;
                    }
                    Transactions.UnionWith(ts);
                    StateManager.Save();
                    OnStateUpdated?.Invoke();
                })));
        }

        public static TimeSeries GetTimeSeries(string category, double smoothingRatio = 0) => GetTimeSeriesUnion(new[] { category }, smoothingRatio);

        public static TimeSeries GetTimeSeriesUnion(IEnumerable<string> categories, double smoothingRatio = 0)
        {
            Func<Transaction, bool> filter = t => categories.Any(c => categoryFilters[c](t));
            var filtered = Transactions.Where(filter);
            return new TimeSeries(filtered, smoothingRatio);
        }

        public static IEnumerable<Transaction> GetTransactions(string category, Date start, Date end) => GetTransactionsUnion(new[] { category }, start, end);

        public static IEnumerable<Transaction> GetTransactionsUnion(IEnumerable<string> categories, Date start, Date end)
        {
            Func<Transaction, bool> filter = t => categories.Any(c => categoryFilters[c](t));
            return Transactions
                    .SkipWhile(t => t.TimeStamp.DateTime < start)
                    .TakeWhile(t => t.TimeStamp.DateTime <= end)
                    .Where(filter);
        }
    }
}
