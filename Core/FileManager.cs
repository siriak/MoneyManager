using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core
{
    public class FileManager
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

        public static string GetRegexCategories()
        {
            if (!File.Exists(RegexCategoriesFileName))
            {
                File.WriteAllText(RegexCategoriesFileName, "[]");
            }
            return File.ReadAllText(RegexCategoriesFileName);
        }

        public static string GetAutoCategories()
        {
            if (!File.Exists(AutoCategoriesFileName))
            {
                File.WriteAllText(AutoCategoriesFileName, "[]");
            }

            return File.ReadAllText(AutoCategoriesFileName);
        }

        public static string GetCompositeCategories()
        {
            if (!File.Exists(CompositeCategoriesFileName))
            {
                File.WriteAllText(CompositeCategoriesFileName, "[]");
            }

            return File.ReadAllText(CompositeCategoriesFileName);
        }

        public static string GetTransactions()
        {
            if (!File.Exists(TransactionsFileName))
            {
                File.WriteAllText(TransactionsFileName, "[]");
            }
            return File.ReadAllText(TransactionsFileName);
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
