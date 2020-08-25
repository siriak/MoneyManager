using FluentAssertions;
using System;
using Xunit;

namespace Core.Tests
{
	public class TransactionTests
	{
		[Fact]
		public void Constructor_TransactionIsLoaded_HashIsSet()
		{
			var transaction = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			transaction.GetHashCode().Should().NotBe(0);
		}

		[Fact]
		public void Constructor_TransactionsDataAreSame_HashesAreEqual()
		{
			var transaction1 = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			var transaction2 = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries"); 
			transaction1.GetHashCode().Should().Be(transaction2.GetHashCode());
		}

		[Fact]
		public void Constructor_HashIsProvided_HashIsCorrect()
		{
			var expectedHash = 123;
			var transaction = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries", expectedHash);
			transaction.GetHashCode().Should().Be(expectedHash);
		}

		[Fact]
		public void CompareTo_DatesAreDifferent_OrderIsCorrect()
		{
			var transaction1 = new Transaction("1111", new Date(2020, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			var transaction2 = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			transaction1.CompareTo(transaction2).Should().Be(1);
			transaction2.CompareTo(transaction1).Should().Be(-1);
		}

		[Fact]
		public void CompareTo_DatesAreSame_OrderIsCorrect()
		{
			var transaction1 = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			var transaction2 = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			transaction1.CompareTo(transaction2).Should().Be(0);
			transaction2.CompareTo(transaction1).Should().Be(0);
		}

		[Fact]
		public void Equals_TransactionsAreEqual_True()
		{
			var transaction1 = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			var transaction2 = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			transaction1.Equals(transaction2).Should().Be(true);
		}

		[Fact]
		public void Equals_TransactionsAreDifferent_False()
		{
			var transaction1 = new Transaction("1111", new Date(2000, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			var transaction2 = new Transaction("1111", new Date(2020, 12, 31), new Money(1.99, Currency.UAH), "food", "Groceries");
			transaction1.Equals(transaction2).Should().Be(false);
		}
	}
}
