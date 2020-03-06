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
    }
}
