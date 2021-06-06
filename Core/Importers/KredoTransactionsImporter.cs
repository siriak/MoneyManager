using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Core.Importers
{
    class KredoTransactionsImporter : ITransactionsImporter
    {
        public IList<Transaction> Load(Stream file)
        {
            var saved = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var pdfDoc = new PdfDocument(new PdfReader(file));

                var transactions = new List<Transaction>();
                var cardNumber = string.Empty;
                var pagesAmount = pdfDoc.GetNumberOfPages();

                for (var i = 0; i < pagesAmount; i++)
                {
                    var listener = new FilteredEventListener();
                    var extractionStrategy = listener.AttachEventListener(new SimpleTextExtractionStrategy());

                    var actualText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i + 1), extractionStrategy)
                        .Split("\n").ToList();

                    actualText = actualText.Skip(13).ToList();
                    if (i == 0)
                    {
                        actualText = actualText.Skip(15).ToList();
                        cardNumber = actualText[0];
                        actualText = actualText.Skip(1).ToList();
                    }
                                        
                    var toSkip = actualText.Where(a => a.Contains("Разом")).ToList();
                    actualText.RemoveAll(a => a.Contains("Разом"));
                    
                    while (actualText.Any())
                    {
                        var transactionData = actualText.Skip(2).TakeWhile(s => !Date.TryParse(s, out var d)).ToArray();

                        transactionData = Date.TryParse(transactionData.Last().Trim('/'), out var d)
                            ? transactionData.SkipLast(1).ToArray()
                            : transactionData;

                        var data = string.Join(' ', transactionData).Split();
                        var date = actualText[0].Trim('/');
                        var description = string.Join(' ', data.Skip(1)
                            .TakeWhile(s => !double.TryParse(s.Split()[0], out var d)));
                        var amount = data.TakeLast(3).First();
                        var currency = data.TakeLast(2).First();
                        transactions.Add(new Transaction(
                            cardNumber,
                            Date.Parse(date),
                            new Money(double.Parse(amount),
                            MoneyManager.ParseCurrency(currency)),
                            description,
                            string.Empty));

                        actualText = actualText.TakeLast(actualText.Count() - transactionData.Length - 2).ToList();
                    }
                }

                pdfDoc.Close();

                return transactions;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = saved;
            }
        }
    }
}
