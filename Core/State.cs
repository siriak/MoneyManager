using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public static class State
    {
        public static ConcurrentBag<Transaction> Transactions { get; } = new ConcurrentBag<Transaction>();

        public static void Init()
        {
            var credentials = ConfigManager.GetCredentials();
            var transactions = new List<Transaction>();

            credentials.Select(c => PrivatTransactionsImporter.ImportTransactions(c, OnTransactionsLoaded));
        }

        private static void OnTransactionsLoaded(List<Transaction> transactions)
        {
            //(ts, d) => {
            //                transactions.AddRange(ts);
            //                rtb.Invoke(new Action(() => rtb.Items.AddRange(ts.Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Descriprtion}").ToArray())));
            //                label.Text = d.ToString();
            //        }
            //    }
        }
    }
}
