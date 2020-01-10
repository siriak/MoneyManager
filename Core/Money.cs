namespace Core
{
    public struct Money
    {
        public Money(decimal amount, string currency) : this()
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
