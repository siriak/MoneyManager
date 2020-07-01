namespace Core
{
	public struct Money
	{
		public Money(double amount, Currency currency)
		{
			Amount = amount;
			Currency = currency;
		}

		public double Amount { get; set; }
		public Currency Currency { get; set; }
	}
}
