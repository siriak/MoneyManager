using System.Collections.Generic;
using Core.Categories;
using Newtonsoft.Json;

namespace Core
{
    public class State
    {
        public static State Instance { get; internal set; } = new State(new HashSet<Category>(), new SortedSet<Transaction>());

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
