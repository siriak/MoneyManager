using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public static class State
    {
        public static SortedSet<Transaction> Transactions { get; } = new SortedSet<Transaction>();
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
                    OnStateUpdated?.Invoke();
                })));
        }
    }
}
