using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Tests
{
	[TestFixture]
	public class StateManagerTests
	{
		const string t1File = "test/t1.json";
		List<Transaction> t1 = new List<Transaction>();

		[Test]
		[Order(0)]
		public void Import()
		{
			var t1Json = File.ReadAllText(t1File);
			t1 = StateHelper.ParseTransactions(t1Json).ToList();
			t1.Should().HaveCount(2);
			//t1[0].CardNumber.Should().NotBe(t1[1].Hash);
			t1[0].Hash.Should().NotBe(t1[1].Hash);
			t1[0].Hash.Should().NotBe(t1[1].Hash);
			t1[0].Hash.Should().NotBe(t1[1].Hash);
			t1[0].Hash.Should().NotBe(t1[1].Hash);
		}

		[Test]
		[Order(1)]
		public void Edit()
		{
			StateManager.UpdateTransaction(new Transaction(t1[0].CardNumber, t1[0].Date, t1[0].Amount,
				t1[0].Description, "Restaurants", t1[0].Hash));
			t1.Should().HaveCount(2);
			t1[0].Hash.Should().NotBe(t1[1].Hash);
		}


		[Test]
		[Order(2)]
		public void Save()
		{
			File.WriteAllText(t1File, JsonConvert.SerializeObject(t1, Formatting.Indented));
		}
	}
}
