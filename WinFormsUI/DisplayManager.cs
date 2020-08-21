using Core;

namespace WinFormsUI
{
    public static class DisplayManager
    {
        public static string FormatLedgerRecord(Transaction transaction)
        {
            var categories = State.Instance.GetAllMatchingCategories(transaction);
            return $"{transaction.Date.ToString("yyyy.MM.dd")}:" + $"{transaction.Amount.Amount}".PadLeft(10) 
                + $" {transaction.Amount.Currency} [{string.Join(", ", categories)}] {transaction.Description}";
        }

        public static string FormatLedgerRecordOnlyCustom(Transaction transaction)
        {
            var categories = State.Instance.GetOnlyCustomCategories(transaction);
            return $"{transaction.Date.ToString("yyyy.MM.dd")}:" + $"{transaction.Amount.Amount}".PadLeft(10)
                + $" {transaction.Amount.Currency} [{string.Join(", ", categories)}] {transaction.Description}";
        }
    }
}
