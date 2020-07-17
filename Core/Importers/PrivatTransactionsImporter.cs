using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Core.Importers
{
    public class PrivatTransactionsImporter : TransactionsImporter
    {
        public override List<Transaction> Load(Stream file)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var reader = ExcelReaderFactory.CreateBinaryReader(file);
            var dataSet = reader.AsDataSet();
            var dataTable = dataSet.Tables[0];

            var transactions = new List<Transaction>();
            for (var i = 2; i < dataTable.Rows.Count; i++)
            {                
                var date = dataTable.Rows[i][0].ToString();
                var category = dataTable.Rows[i][2].ToString(); 
                var cardNumber = dataTable.Rows[i][3].ToString();
                var description = dataTable.Rows[i][4].ToString();
                var amount = dataTable.Rows[i][5].ToString();
                //todo: read currency from file
                var currency = dataTable.Rows[i][6].ToString();
                transactions.Add(new Transaction(cardNumber, Date.Parse(date), 
                    new Money(double.Parse(amount), Currency.UAH), description, category));
            }

            return transactions;
        }
    }
}
