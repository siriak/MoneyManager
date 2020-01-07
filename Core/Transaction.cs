using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
        
        //ctor
    }
}

//TODO: implement sequence analyzer
//TODO: load config from file
//TODO: create categories for transaction and classificator
//TODO: UI
//TODO: save setups 
