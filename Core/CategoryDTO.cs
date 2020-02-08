namespace Core
{
    public class CategoryDto
    {
		public CategoryDto(string name, string cardNumber, string applicationCode, string isIncome, 
			string isExpence, string terminal, string description)
		{
			Name = name;
			CardNumber = cardNumber;
			ApplicationCode = applicationCode;
			IsIncome = isIncome;
			IsExpence = isExpence;
			Terminal = terminal;
			Description = description;
		}

		public string Name { get; }
		public string CardNumber { get; }
		public string ApplicationCode { get; }
		public string IsIncome { get; }
		public string IsExpence { get; }
		public string Terminal { get; }
		public string Description { get; }
	}
}
