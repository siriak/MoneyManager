﻿using System;

namespace Core
{
    public static class MoneyManager
    {
        public static Money Convert(this Money money, Currency currency)
        {
            if (money.Currency == currency)
            {
                return money;
            }
            throw new ArgumentException("Invalid Currency");
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
