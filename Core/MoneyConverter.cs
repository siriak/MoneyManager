using System;

namespace Core
{
    public static class MoneyConverter
    {
        public static Money Convert(this Money money, Currency currency)
        {
            if (money.Currency == currency)
            {
                return money;
            }
            throw new ArgumentException("Invalid Currency");
        }

        public static Currency Convert(string currency)
        {
            switch (currency.ToLowerInvariant())
            {
                case "uah":
                case "грн":
                    return Currency.UAH;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
