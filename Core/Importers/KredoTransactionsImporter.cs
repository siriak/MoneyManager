using iText.IO.Font;
using iText.Kernel;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.IO.Font.Constants;
using iText.Layout.Element;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Importers
{
    class KredoTransactionsImporter : ITransactionsImporter
    {
        public IList<Transaction> Load(Stream file)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(file));

            FilteredEventListener listener = new FilteredEventListener();

            ITextExtractionStrategy extractionStrategy = listener
                .AttachEventListener(new SimpleTextExtractionStrategy());

            var transactions = new List<Transaction>();
            var pagesAmount = pdfDoc.GetNumberOfPages();
            for (int i = 0; i < pagesAmount; i++)
            {
                new PdfCanvasProcessor(listener).ProcessPageContent(pdfDoc.GetPage(i + 1));

                var actualText = extractionStrategy.GetResultantText().Split("\n").Skip(29).SkipLast(2).ToArray();

                while (actualText.Length != 0)
                {
                    var transactionData = actualText.Skip(2).TakeWhile(s => Date.TryParse(s, out Date d) == false).ToArray();
                    if (Date.TryParse(transactionData.Last().Trim('/'), out Date d) == true)
                    {
                        transactionData = transactionData.SkipLast(1).ToArray();
                    }
                    
                    var date = actualText[1];
                    var category = string.Empty;
                    var cardNumber = string.Empty;
                    var description = string.Concat(transactionData.Skip(1)
                        .TakeWhile(s => double.TryParse(s.Split(' ')[0], out double d) == false));
                    var amount = transactionData.Last().Split(' ')[0];
                    var currency = transactionData.Last().Split(' ')[1];
                    transactions.Add(new Transaction(cardNumber, Date.Parse(date),
                        new Money(double.Parse(amount), MoneyManager.ParseCurrency(currency)), description, category));

                    actualText = actualText.TakeLast(actualText.Length - transactionData.Length - 2).ToArray();
                }
            }

            pdfDoc.Close();
                       
            return transactions;
        }
    }
}
