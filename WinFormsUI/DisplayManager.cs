using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsUI
{
    public class DisplayManager
	{
		public static string FormatLedgerRecord(Transaction transaction)
		{
			var categories = State.Instance.GetAllMatchingCategories(transaction);
			return $"{transaction.Date.ToLongString()}:" + $"{transaction.Amount.Amount}".PadLeft(10) 
				+ $" {transaction.Amount.Currency} [{string.Join(", ", categories)}] {transaction.Description}";
		}

	}
}
