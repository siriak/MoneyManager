using System;
using Newtonsoft.Json;

namespace Core
{
	public class Transaction : IComparable<Transaction>
	{
		[JsonConstructor]
		public Transaction(string cardNumber, Date date, Money amount, string description, string category)
		{
			CardNumber = cardNumber;
			Date = date;
			Amount = amount;
			Description = description;
			Category = category;
		}

		public string CardNumber { get; }
		public Date Date { get; }
		public Money Amount { get; }

		[JsonIgnore]
		public Money AbsoluteAmount => new Money(Math.Abs(Amount.Amount), Amount.Currency);

		public string Description { get; }
		public string Category { get; }

		public int CompareTo(Transaction other) => Date.CompareTo(other.Date);

		public new string ToString()
		{
			return $"{Amount.Amount} {Amount.Currency}: {Description}";
		}
	}
}
