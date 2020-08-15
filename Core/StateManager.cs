using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Categories;
using Core.Importers;
using Newtonsoft.Json;

namespace Core
{
    public class StateManager
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

            State.Instance = new State(regex.Cast<Category>().Concat(auto).Concat(composite).ToHashSet(), State.Instance.Transactions.ToHashSet());
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

        public static void LoadTransactions(IEnumerable<(string key, Stream stream)> files, string customTransactionsJson)
        {
            var newTransactions = new List<Transaction>();
            foreach (var (key, stream) in files)
            {
                newTransactions.AddRange(importers[key].Load(stream));
            }

            newTransactions.AddRange(LoadCustomTransactions(customTransactionsJson));
            Func<string, string> suggestName = s => $"[Auto] {s}";
            var newCategories = newTransactions.Select(t => t.Category).Where(c => c is { } && State.Instance.Categories.All(sc => sc.Name != suggestName(c)))
                .Select(c => new AutoCategory(suggestName(c), 1, 10000, c)).ToList();

            var categories = new List<Category>(); 
            categories.AddRange(State.Instance.Categories);
            categories.AddRange(newCategories);

            var transactions = new List<Transaction>();
            transactions.AddRange(State.Instance.Transactions);
            transactions.AddRange(newTransactions);

            State.Instance = new State(categories.ToHashSet(), transactions.ToHashSet());
        }

        private static IEnumerable<Transaction> LoadCustomTransactions(string customTransactionsJson) =>
            string.IsNullOrWhiteSpace(customTransactionsJson)
                ? new List<Transaction>()
                : JsonConvert.DeserializeObject<ICollection<Transaction>>(customTransactionsJson);
    }
}
