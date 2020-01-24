using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{
    public class StateManager
    {
        public static SortedSet<Transaction> LoadedState { get; set; } = new SortedSet<Transaction>();

        public static void Load()
        {
            if (!File.Exists("state.json"))
            {
                File.WriteAllText("state.json", JsonConvert.SerializeObject(State.Transactions));
            }
            LoadedState = JsonConvert.DeserializeObject<SortedSet<Transaction>>(File.ReadAllText("state.json"));
        }

        public static void Save()
        {
            if (State.Transactions.Count > (LoadedState?.Count ?? 0))
            {
                var transactions = JsonConvert.SerializeObject(State.Transactions);                
                File.WriteAllText("state.json", transactions);
                Load();
            }
        }
    }
}
