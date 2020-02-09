namespace Core
{
    public class Rule
    {
		public string CardNumber { get; }
		public string ApplicationCode { get; }
		public string IsIncome { get; }
		public string IsExpence { get; }
		public string Terminal { get; }
		public string Description { get; }

		public Rule(string cardNumber, string applicationCode, string isIncome,
			string isExpence, string terminal, string description)
		{
			CardNumber = cardNumber;
			ApplicationCode = applicationCode;
			IsIncome = isIncome;
			IsExpence = isExpence;
			Terminal = terminal;
			Description = description;
		}
	}
}
