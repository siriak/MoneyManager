using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core
{
    public static class FileManager
    {
        static string WorkingDirectory => Directory.GetCurrentDirectory() + "/data/";
        static string CategoriesDirectory => WorkingDirectory + "categories/";
        static string AutoCategoriesFileName => CategoriesDirectory + "autoCategories.json";
        static string CompositeCategoriesFileName => CategoriesDirectory + "compositeCategories.json";
        static string RegexCategoriesFileName => CategoriesDirectory + "regexCategories.json";
        static string TransactionsFileName => WorkingDirectory + "transactions.json";

        static string UsbDirectory => WorkingDirectory + "ukrsibbank/";
        static string KredobankDirectory => WorkingDirectory + "kredobank/";
        static string PrivatebankDirectory => WorkingDirectory + "privatbank/";

        public static void SaveAutoCategoriesToFile()
        {
            File.WriteAllText(AutoCategoriesFileName, StateManager.SaveCategories().autoCategoriesJson);
        }

        public static void SaveUpdatedTransactions()
        {
            File.WriteAllText(TransactionsFileName, State.Instance.SaveTransactionsToJson());
        }

        public static string GetRegexCategories() =>
            ReadFile(RegexCategoriesFileName);

        public static string GetAutoCategories() =>
            ReadFile(AutoCategoriesFileName);

        public static string GetCompositeCategories() =>
            ReadFile(CompositeCategoriesFileName);

        public static string GetTransactions() =>
            ReadFile(TransactionsFileName);        

        private static string ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, "[]");
            }
            return File.ReadAllText(fileName);
        }

        public static IEnumerable<(string, Stream)> GetUsbFiles() =>
            Directory.GetFiles(UsbDirectory, "*.*", SearchOption.AllDirectories)
                .Select(f => ("usb", (Stream)File.OpenRead(f)));

        public static IEnumerable<(string, Stream)> GetPbFiles() =>
            Directory.GetFiles(PrivatebankDirectory, "*.*", SearchOption.AllDirectories)
                .Select(f => ("pb", (Stream)File.OpenRead(f)));

        public static IEnumerable<(string, Stream)> GetKbFiles() =>
            Directory.GetFiles(KredobankDirectory, "*.*", SearchOption.AllDirectories)
                .Select(f => ("kb", (Stream)File.OpenRead(f)));
    }
}
