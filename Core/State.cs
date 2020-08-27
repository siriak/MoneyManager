using System;
using System.Collections.Generic;
using Core.Categories;
using Newtonsoft.Json;

namespace Core
{
    public class State
    {
        private static State instance = new State(new HashSet<Category>(), new HashSet<Transaction>());

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
        public State(HashSet<Category> categories, HashSet<Transaction> transactions)
        {
            Transactions = transactions;
            Categories = categories;
        }
    }
}
