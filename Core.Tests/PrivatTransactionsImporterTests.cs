using System;
using System.Xml;
using Xunit;

namespace Core.Tests
{
    public class PrivatTransactionsImporterTests
    {
        [Fact]
        public void GetTransactionFromXml_WhenCorrectXml_ReturnTransaction()
        {
            var expectedTransaction = new Transaction("5555555555555555", "188888", DateTimeOffset.Parse("01/03/2020 18:58:00"), new Money(-599.03M, Currency.UAH),
                new Money(1300.74M, Currency.UAH), "Wizz Air", "Payment");

            const string xmlContent = "<statement card=\"5555555555555555\" appcode=\"188888\" trandate=\"2020 - 01 - 03\" " +
                "trantime=\"18:58:00\" amount=\"585.00 UAH\" cardamount=\"-599.03 UAH\" rest=\"1300.74 UAH\" " +
                "terminal=\"Wizz Air\" description=\"Payment\"/>";

            var doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            XmlNode node = doc.DocumentElement;

            var actualTansaction = PrivatTransactionsImporter.GetTransactionFromXml(node);

            Assert.Equal(expectedTransaction.Amount, actualTansaction.Amount);
            Assert.Equal(expectedTransaction.ApplicationCode, actualTansaction.ApplicationCode);
            Assert.Equal(expectedTransaction.CardNumber, actualTansaction.CardNumber);
            Assert.Equal(expectedTransaction.Description, actualTansaction.Description);
            Assert.Equal(expectedTransaction.IsExpence, actualTansaction.IsExpence);
            Assert.Equal(expectedTransaction.IsIncome, actualTansaction.IsIncome);
            Assert.Equal(expectedTransaction.Rest, actualTansaction.Rest);
            Assert.Equal(expectedTransaction.Terminal, actualTansaction.Terminal);
            Assert.Equal(expectedTransaction.TimeStamp, actualTansaction.TimeStamp);
        }
    }
}
