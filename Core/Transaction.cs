using System;
using Newtonsoft.Json;

namespace Core
{
	public class Transaction : IComparable<Transaction>
	{
		public Transaction(string cardNumber, string appCode, DateTimeOffset timeStamp, double amount, 
			double rest, string terminal, string description)
		{
			CardNumber = cardNumber;
			ApplicationCode = appCode;
			TimeStamp = timeStamp;
			Amount = amount >= 0 ? amount : -amount;
			IsIncome = amount > 0;
			IsExpence = amount < 0;
			Rest = rest;
			Terminal = terminal;
			Description = description;
		}

		[JsonConstructor]
		public Transaction(string cardNumber, string applicationCode, DateTimeOffset timeStamp, double amount, 
			bool isIncome, bool isExpence, double rest, string terminal, string description)
		{
			CardNumber = cardNumber;
			ApplicationCode = applicationCode;
			TimeStamp = timeStamp;
			Amount = amount;
			IsIncome = isIncome;
			IsExpence = isExpence;
			Rest = rest;
			Terminal = terminal;
			Description = description;
		}

		public string CardNumber { get; }
		public string ApplicationCode { get; }
		public DateTimeOffset TimeStamp { get; }
		public double Amount { get; }
		public bool IsIncome { get; }
		public bool IsExpence { get; }
		public double Rest { get; }
		public string Terminal { get; }
		public string Description { get; }

		public int CompareTo(Transaction other) => TimeStamp.CompareTo(other.TimeStamp);
	}
}
