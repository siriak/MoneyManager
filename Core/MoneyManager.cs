﻿using System;
using System.Collections.Generic;

namespace Core
{
    public static class MoneyManager
    {
        public static Money Convert(this Money money, Currency currency)
        {
            if (!Enum.IsDefined(typeof(Currency), (int)currency))
            {
                throw new ArgumentException($"{nameof(currency)} is invalid");
            }
            if (!Enum.IsDefined(typeof(Currency), (int)money.Currency))
            {
                throw new ArgumentException($"{nameof(money.Currency)} is invalid");
            }
            var exchangeToUsd = new Dictionary<Currency, double> 
            { 
                [Currency.UAH] = 25,
            };
            if (!exchangeToUsd.ContainsKey(currency))
            {
                throw new NotSupportedException($"{currency} is not supported");
            }
            if (!exchangeToUsd.ContainsKey(money.Currency))
            {
                throw new NotSupportedException($"{money.Currency} is not supported");
            }
            var newAmount = money.Amount * exchangeToUsd[currency] / exchangeToUsd[money.Currency];
            return new Money(newAmount, currency);
        }

        public static Currency ParseCurrency(string currency)
        {
            switch (currency.ToUpperInvariant())
            {
                case "UAH":
                case "ГРН":
                    return Currency.UAH;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
