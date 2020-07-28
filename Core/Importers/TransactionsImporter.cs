using System.Collections.Generic;
using System.IO;

namespace Core
{
	interface ITransactionsImporter
	{
		public List<Transaction> Load(Stream file);
	}
}
