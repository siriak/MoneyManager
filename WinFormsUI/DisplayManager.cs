using Core;

namespace WinFormsUI
{
	public static class DisplayManager
	{
		public static string FormatLedgerRecord(Transaction transaction)
		{
			var categories = State.Instance.GetAllMatchingCategories(transaction);
			return $"{transaction.Date.ToLongString()}:" + $"{transaction.Amount.Amount}".PadLeft(10) 
				+ $" {transaction.Amount.Currency} [{string.Join(", ", categories)}] {transaction.Description}";
		}

	}
}
