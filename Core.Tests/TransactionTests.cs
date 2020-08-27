using FluentAssertions;
using Xunit;

namespace Core.Tests
{
	public class TransactionTests
	{
		private const string category = "Groceries";
		private const string description = "Food";
		private const string card = "1111";

		[Fact]
		public void Constructor_TransactionIsLoaded_HashIsSet()
		{
			var transaction = new Transaction(card, new Date(2000, 12, 31), new Money(1.99, Currency.UAH), description, category);
			transaction.GetHashCode().Should().NotBe(0);
		}

		[Fact]
		public void Constructor_TransactionsDataAreSame_HashesAreEqual()
		{
			var transaction1 = new Transaction(card, new Date(2000, 12, 31), new Money(1.99, Currency.UAH), description, category);
			var transaction2 = new Transaction(card, new Date(2000, 12, 31), new Money(1.99, Currency.UAH), description, category); 
			transaction1.GetHashCode().Should().Be(transaction2.GetHashCode());
		}

		[Fact]
		public void Constructor_HashIsProvided_HashIsCorrect()
		{
			const int expectedHash = 123;
			var transaction = new Transaction(card, new Date(2000, 12, 31), new Money(1.99, Currency.UAH), description, category, expectedHash);
			transaction.GetHashCode().Should().Be(expectedHash);
		}

		[Fact]
		public void Equals_TransactionsAreEqual_True()
		{
			var transaction1 = new Transaction(card, new Date(2000, 12, 31), new Money(1.99, Currency.UAH), description, category);
			var transaction2 = new Transaction(card, new Date(2000, 12, 31), new Money(1.99, Currency.UAH), description, category);
			transaction1.Equals(transaction2).Should().Be(true);
			transaction2.Equals(transaction1).Should().Be(true);
		}

		[Fact]
		public void Equals_TransactionsAreDifferent_False()
		{
			var transaction1 = new Transaction(card, new Date(2000, 12, 31), new Money(1.99, Currency.UAH), description, category);
			var transaction2 = new Transaction(card, new Date(2020, 12, 31), new Money(1.99, Currency.UAH), description, category);
			transaction1.Equals(transaction2).Should().Be(false);
			transaction2.Equals(transaction1).Should().Be(false);
		}
	}
}
