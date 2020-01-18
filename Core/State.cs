using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public static class State
    {
        public static SortedSet<Transaction> Transactions { get; } = new SortedSet<Transaction>();
        public static TimeSeries TimeSeries { get; private set; }
        public static event Action OnStateUpdated;
        public static bool IsUpdating => task.Status == TaskStatus.Running;
        private static Task task; 

        public static void Init()
        {
            var credentials = ConfigManager.GetCredentials();

            task = Task.WhenAll(
                credentials.Select(c => PrivatTransactionsImporter.ImportTransactions(c, (ts) =>
                {
                    Transactions.UnionWith(ts);
                    TimeSeries = CreateTimeSeries();
                    OnStateUpdated?.Invoke();
                })));
        }

        private static TimeSeries CreateTimeSeries()
        {
            var t = new TimeSeries() { Series = new List<Observation>() };
            t.Start = Date.MinValue;
            t.End = Date.MaxValue;
            t.Series.Add(new Observation() { Date = new Date(2020, 01, 16), Value = 200 });
            t.Series.Add(new Observation() { Date = new Date(2020, 01, 17), Value = 300 });
            t.Series.Add(new Observation() { Date = new Date(2020, 01, 18), Value = 400 });
            return t;
        }
    }
}
