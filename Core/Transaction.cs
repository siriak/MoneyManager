using System;

namespace Core
{
    public class Transaction
    {
        public string CardNumber { get; }
        public string ApplicationCode { get; }
        public DateTimeOffset Time { get; }
        public Money Amount { get; }
        public bool IsIncome => isIncome;
        public bool IsExpence => !isIncome;
        public Money Rest { get; }
        public string Terminal { get; }
        public string Descriprtion { get; }

        private string cardNumber;
        private string applicationCode;
        private DateTimeOffset time;
        private Money amount;
        private Money cardAmount;
        private bool isIncome;
        private Money rest;
        private string terminal;
        private string descriprion;

        public Transaction(Money amount, string description)
        {
            Amount = amount;
            Descriprtion = description;
        }

        //ctor
    }
}

//TODO: implement sequence analyzer
//TODO: create categories for transaction and classificator
//TODO: UI
//TODO: save setups 
//TODO: transaction -> time series for each day -> exponensial smoothing
