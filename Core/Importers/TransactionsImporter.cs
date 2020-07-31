using System.Collections.Generic;
using System.IO;

namespace Core
{
    interface ITransactionsImporter
    {
        public IList<Transaction> Load(Stream file);
    }
}
