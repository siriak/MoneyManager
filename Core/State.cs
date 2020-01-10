using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class State
    {
        public static SortedSet<Transaction> Transactions { get; } = new SortedSet<Transaction>();

        //comparer

        public static void Init()
        {
            var credentials = ConfigManager.GetCredentials();

            initTask = Task.WhenAll(
            credentials.Select(c => PrivatTransactionsImporter.ImportTransactions(c, (ts) =>
            {
                Transactions.UnionWith(ts);
                OnStateUpdated?.Invoke();
            })));
        }

        public static event Action OnStateUpdated;

        private static Task initTask;
    }
}
