using System;
using System.Collections.Generic;
using Core.Categories;
using Newtonsoft.Json;

namespace Core
{
    public class State
    {
        private static State instance = new State(new HashSet<Category>(), new SortedSet<Transaction>());

        public static State Instance
        {
            get => instance;
            internal set
            {
                instance = value;
                OnStateChanged();
            }
        }

        public static event Action OnStateChanged = () => { };

        public IReadOnlyCollection<Category> Categories { get; }

        public IReadOnlyCollection<Transaction> Transactions { get; }

        [JsonConstructor]
        public State(ISet<Category> categories, ISet<Transaction> transactions)
        {
            Transactions = new SortedSet<Transaction>(transactions);
            Categories = new List<Category>(categories);
        }
    }
}
