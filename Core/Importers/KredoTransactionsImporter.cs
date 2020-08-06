using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
                var listener = new FilteredEventListener();
                var extractionStrategy = listener.AttachEventListener(new SimpleTextExtractionStrategy());

                var transactions = new List<Transaction>();
                var pagesAmount = pdfDoc.GetNumberOfPages();

                for (int i = 0; i < pagesAmount; i++)
                {
                    new PdfCanvasProcessor(listener).ProcessPageContent(pdfDoc.GetPage(i + 1));

                    var actualText = extractionStrategy.GetResultantText().Split("\n").Skip(28).SkipLast(2).ToArray();
                    var cardNumber = actualText[0];
                    var category = "КредоБанк";
                    actualText = actualText.Skip(1).ToArray();

                    while (actualText.Length != 0)
                    {
                        var transactionData = actualText.Skip(2).TakeWhile(s => !Date.TryParse(s, out Date d)).ToArray();

                        if (Date.TryParse(transactionData.Last().Trim('/'), out Date d))
                        {
                            transactionData = transactionData.SkipLast(1).ToArray();
                        }

                        var data = transactionData.First().Split();
                        var date = actualText[0].Trim('/');
                        var description = string.Join(' ', data.Skip(1)
                            .TakeWhile(s => !double.TryParse(s.Split()[0], out double d)));
                        var amount = data.TakeLast(3).First();
                        var currency = data.TakeLast(2).First();
                        transactions.Add(new Transaction(cardNumber, Date.Parse(date),
                            new Money(double.Parse(amount), MoneyManager.ParseCurrency(currency)), description, category));

                        actualText = actualText.TakeLast(actualText.Length - transactionData.Length - 2).ToArray();
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
