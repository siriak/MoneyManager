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
            return JsonConvert.SerializeObject(State.Instance.Categories.Where(c => c is RegexCategory));
        }

        public static string SaveAuto()
        {
            return JsonConvert.SerializeObject(State.Instance.Categories.Where(c => c is AutoCategory));
        }

        public static string SaveComposite()
        {
            return JsonConvert.SerializeObject(State.Instance.Categories.Where(c => c is CompositeCategory));
        }

        public static void LoadCategories(string regexCategoriesFileName, string autoCategoriesFileName, string compositeCategoriesFileName)
        {
            var regex = LoadRegex(regexCategoriesFileName);
            var auto = LoadAuto(autoCategoriesFileName);
            var composite = LoadComposite(compositeCategoriesFileName);

            State.Instance = new State(regex.Cast<Category>().Concat(auto).Concat(composite).ToHashSet(), State.Instance.Transactions.ToHashSet());
        }

        public static ICollection<RegexCategory> LoadRegex(string regexCategoriesJson)
        {
            if (string.IsNullOrWhiteSpace(regexCategoriesJson))
            {
                return new List<RegexCategory>();
            }

            return JsonConvert.DeserializeObject<ICollection<RegexCategory>>(regexCategoriesJson);
        }

        public static ICollection<AutoCategory> LoadAuto(string autoCategoriesJson)
        {           
            if (string.IsNullOrWhiteSpace(autoCategoriesJson))
            {
                return new List<AutoCategory>();
            }

            return JsonConvert.DeserializeObject<ICollection<AutoCategory>>(autoCategoriesJson);
        }

        public static ICollection<CompositeCategory> LoadComposite(string compositeCategoriesJson)
        {           
            if (string.IsNullOrWhiteSpace(compositeCategoriesJson))
            {
                return new List<CompositeCategory>();
            }

            return JsonConvert.DeserializeObject<ICollection<CompositeCategory>>(compositeCategoriesJson);
        }

        public static void LoadTransactions(IEnumerable<(string key, Stream stream)> files)
        {
            var newTransactions = new List<Transaction>();
            foreach (var file in files)
            {
                newTransactions.AddRange(importers[file.key].Load(file.stream));
            }

            var newCategories = newTransactions.Select(t => t.Category).Where(c => c is { } && State.Instance.Categories.All(sc => sc.Name != c))
                .Select(c => new AutoCategory(string.Join(' ', "[Auto]", c), 1, 10000, c)).Distinct().ToList();

            var categories = new List<Category>(); 
            categories.AddRange(State.Instance.Categories);
            categories.AddRange(newCategories);

            var transactions = new List<Transaction>();
            transactions.AddRange(State.Instance.Transactions);
            transactions.AddRange(newTransactions);

            State.Instance = new State(categories.ToHashSet(), transactions.ToHashSet());
        }
    }
}
