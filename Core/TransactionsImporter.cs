using System.Collections.Generic;
using System.IO;

namespace Core
{
	public abstract class TransactionsImporter
	{
		public bool CanLoad(Stream file)
		{
			try
			{
				Load(file);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public abstract List<Transaction> Load(Stream file);
	}
}
