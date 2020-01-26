using System;

namespace Core
{
    public class Transaction : IComparable<Transaction>
    {
        public string CardNumber { get; }
        public string ApplicationCode { get; }
        public DateTimeOffset TimeStamp { get; }
        public Money Amount { get; }
        public bool IsIncome => isIncome;
        public bool IsExpence => !isIncome;
        public Money Rest { get; }
        public string Terminal { get; }
        public string Description { get; }

        private string cardNumber;
        private string applicationCode;
        private DateTimeOffset time;
        private Money amount;
        private Money cardAmount;
        private bool isIncome;
        private Money rest;
        private string terminal;
        private string description;

        public Transaction(string cardNumber, string appCode, DateTimeOffset timeStamp, Money amount, Money rest, string terminal, string description)
        {
            CardNumber = cardNumber;
            ApplicationCode = appCode;
            TimeStamp = timeStamp;
            Amount = amount;
            Rest = rest;
            Terminal = terminal;
            Description = description;
        }

        public int CompareTo(Transaction other)
        {
            return TimeStamp.CompareTo(other.TimeStamp);
        }
    }
}

//TODO: implement sequence analyzer
//TODO: create categories for transaction and classificator
//TODO: UI
//TODO: save setups 
//TODO: transaction -> time series for each day -> exponensial smoothing
