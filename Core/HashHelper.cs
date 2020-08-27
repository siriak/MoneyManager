namespace Core
{
    public static class HashHelper
    {
        public static int GetStableHashCode(this string str)
        {
            unchecked
            {
                var hash1 = 5381;
                var hash2 = hash1;

                for(var i = 0; i < str.Length && str[i] != '\0'; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1 || str[i+1] == '\0')
                    {
                        break;
                    }

                    hash2 = ((hash2 << 5) + hash2) ^ str[i+1];
                }

                return hash1 + hash2 * 1_566_083_941;
            }
        }

        public static int GetStableHashCode(this Money money)
        {
            unchecked
            {
                return 31 * (int) money.Amount + (int) money.Currency;
            }
        }

        public static int GetStableHashCode(this Date date)
        {
            unchecked
            {
                var hash = date.Year;
                hash = 31 * hash + date.Month;
                hash = 31 * hash + date.Day;
                return hash;
            }
        }
    }
}
