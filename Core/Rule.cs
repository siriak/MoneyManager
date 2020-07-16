namespace Core
{
    public class Rule
    {
		public string CardNumber { get; }
		public string Description { get; }
		public string Amount { get; }
		public Rule(string cardNumber, string amount, string description)
		{
			CardNumber = cardNumber;
			Amount = amount;
			Description = description;
		}
	}
}
