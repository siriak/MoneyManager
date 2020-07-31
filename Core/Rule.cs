namespace Core
{
    public readonly struct Rule
    {
        public string CardNumber { get; }
        public string Description { get; }
        public string Amount { get; }
        public string Category { get; }

        public Rule(string cardNumber, string amount, string description, string category)
        {
            CardNumber = cardNumber;
            Amount = amount;
            Description = description;
            Category = category;
        }
    }
}
