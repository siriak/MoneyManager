using Newtonsoft.Json;
using System;

namespace Core
{
    public class Transaction : IComparable<Transaction>
    {
        public string CardNumber { get; }
        public string ApplicationCode { get; }
        public DateTimeOffset TimeStamp { get; }
        public Money Amount { get; }
        public bool IsIncome { get; }
        public bool IsExpence { get; }
        public Money Rest { get; }
        public string Terminal { get; }
        public string Description { get; }

        public Transaction(string cardNumber, string appCode, DateTimeOffset timeStamp, Money amount, Money rest, string terminal, string description)
        {
            CardNumber = cardNumber;
            ApplicationCode = appCode;
            TimeStamp = timeStamp;
            Amount = amount.Amount >= 0 ? amount : new Money(-amount.Amount, amount.Currency);
            IsIncome = amount.Amount > 0;
            IsExpence = amount.Amount < 0;
            Rest = rest;
            Terminal = terminal;
            Description = description;
        }

        [JsonConstructor]
        public Transaction(string cardNumber, string applicationCode, DateTimeOffset timeStamp, Money amount, bool isIncome, bool isExpence, Money rest, string terminal, string description)
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

        public int CompareTo(Transaction other) => TimeStamp.CompareTo(other.TimeStamp);
    }
}
