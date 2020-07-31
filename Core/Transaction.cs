using System;
using Newtonsoft.Json;

namespace Core
{
    public struct Transaction : IComparable<Transaction>
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

        public int CompareTo(Transaction other) => Date.CompareTo(other.Date) is var dateComparison && dateComparison == 0 ? GetHashCode().CompareTo(other.GetHashCode()) : dateComparison;

        public override int GetHashCode()
        {
            return Date.GetHashCode() + Category.GetHashCode() + Amount.GetHashCode() + CardNumber.GetHashCode() + Description.GetHashCode();
        }
    }
}
