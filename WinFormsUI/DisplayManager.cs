using Core;
using Core.Categories;
using System.Collections.Generic;

namespace WinFormsUI
{
    public static class DisplayManager
    {
        public static string FormatLedgerRecord(Transaction transaction, IEnumerable<string> categories)
        {
            return $"{transaction.Date.ToString("yyyy.MM.dd")}:" + $"{transaction.Amount.Amount}".PadLeft(10) 
                + $" {transaction.Amount.Currency} [{string.Join(", ", categories)}] {transaction.Description}";
        }

        public static string AddPrefixToCategory(double todayRelative, Category c)
        {
            var prefix = todayRelative switch
            {
                _ when todayRelative <= 0 => Levels.Empty.ToString(),
                _ when todayRelative <= 0.1 => Levels.Low.ToString(),
                _ when todayRelative < 1 => "",
                _ => Levels.Full.ToString(),
            };

            return string.IsNullOrEmpty(prefix)
                    ? c.Name
                    : string.Concat($"({prefix.ToUpper()}) ", c.Name);
        }
    }
}
