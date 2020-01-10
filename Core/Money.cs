namespace Core
{
    public struct Money
    {
        public Money(string amount, string currency) : this()
        {
            Amount = decimal.Parse(amount);
            Currency = currency;
        }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
