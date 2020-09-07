using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Categories;
using Core.Importers;
using Newtonsoft.Json;

namespace Core
{
    public static class StateManager
    {
        private static Dictionary<string, ITransactionsImporter> importers = new Dictionary<string, ITransactionsImporter>
        {
            ["pb"] = new PrivatTransactionsImporter(),
            ["usb"] = new UkrSibTransactionsImporter(),
            ["kb"] = new KredoTransactionsImporter(),
        };

        public static (string regexCategoriesJson, string autoCategoriesJson, string compositeCategoriesJson) SaveCategories()
        {
            var regexCategoriesJson = SaveRegex();
            var autoCategoriesJson = SaveAuto();
            var compositeCategoriesJson = SaveComposite();

            return (regexCategoriesJson, autoCategoriesJson, compositeCategoriesJson);
        }

        public static string SaveRegex()
        {
            return JsonConvert.SerializeObject(State.Instance.Categories.Where(c => c is RegexCategory), Formatting.Indented);
        }

        public static string SaveAuto()
        {
            return JsonConvert.SerializeObject(State.Instance.Categories.Where(c => c is AutoCategory), Formatting.Indented);
        }

        public static string SaveComposite()
        {
            return JsonConvert.SerializeObject(State.Instance.Categories.Where(c => c is CompositeCategory), Formatting.Indented);
        }

        public static void LoadCategories(string regexCategoriesFileName, string autoCategoriesFileName, string compositeCategoriesFileName)
        {
            var regex = LoadRegex(regexCategoriesFileName);
            var auto = LoadAuto(autoCategoriesFileName);
            var composite = LoadComposite(compositeCategoriesFileName);

            State.Instance = new State(regex.Cast<Category>().Concat(auto).Concat(composite).Concat(State.Instance.Categories).ToHashSet(), State.Instance.Transactions.ToHashSet());
        }

        public static IEnumerable<RegexCategory> LoadRegex(string regexCategoriesJson) =>
            string.IsNullOrWhiteSpace(regexCategoriesJson)
                ? Array.Empty<RegexCategory>()
                : JsonConvert.DeserializeObject<RegexCategory[]>(regexCategoriesJson);

        public static IEnumerable<AutoCategory> LoadAuto(string autoCategoriesJson) =>
            string.IsNullOrWhiteSpace(autoCategoriesJson)
                ? Array.Empty<AutoCategory>()
                : JsonConvert.DeserializeObject<AutoCategory[]>(autoCategoriesJson);

        public static IEnumerable<CompositeCategory> LoadComposite(string compositeCategoriesJson) =>
            string.IsNullOrWhiteSpace(compositeCategoriesJson)
                ? Array.Empty<CompositeCategory>()
                : JsonConvert.DeserializeObject<CompositeCategory[]>(compositeCategoriesJson);

        public static void LoadTransactions(IEnumerable<(string key, Stream stream)> files, string transactionsJson)
        {
            var newTransactions = new List<Transaction>();
            foreach (var (key, stream) in files)
            {
                newTransactions.AddRange(importers[key].Load(stream));
            }

            var transactions = new List<Transaction>();
            transactions.AddRange(StateHelper.ParseTransactions(transactionsJson));
            transactions.AddRange(newTransactions);
            transactions.AddRange(State.Instance.Transactions);

            State.Instance = new State(State.Instance.Categories.ToHashSet(), transactions.ToHashSet());

            Func<string, string> suggestName = s => $"[Auto] {s}";
            var newCategories = State.Instance.Transactions.Select(t => t.Category).Where(c => c is { } && State.Instance.Categories.All(sc => sc.Name != suggestName(c)))
                .Select(c => new AutoCategory(suggestName(c), 1, 10000, c)).ToList();

            var categories = new List<Category>(); 
            categories.AddRange(State.Instance.Categories);
            categories.AddRange(newCategories);
            
            State.Instance = new State(categories.ToHashSet(), State.Instance.Transactions.ToHashSet());
            
            var transactionsWithoutCategory = State.Instance.Transactions.Where(t => string.IsNullOrWhiteSpace(t.Category));

            foreach (var t in transactionsWithoutCategory)
            {
                var c = State.Instance.GetAllMatchingCategoriesOfType<CompositeCategory>(t).ToList();
                if (c.Count == 1)
                {
                    var newTransaction = new Transaction(t.CardNumber, t.Date, t.Amount, t.Description, c[0], t.GetHashCode());
                    UpdateTransaction(newTransaction);
                }
            }
        }

        public static void UpdateTransaction(Transaction transaction)
        {
            var transactions = State.Instance.Transactions.ToHashSet();
            if (!transactions.Contains(transaction))
            {
                throw new ArgumentException($"{nameof(transaction)} not found");
            }
            transactions.Remove(transaction);
            transactions.Add(transaction);
            State.Instance = new State(State.Instance.Categories.ToHashSet(), transactions);
        }
    }
}
