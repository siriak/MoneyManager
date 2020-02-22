using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.TimeSeries
{
	public class TimeSeries
	{
		protected Date End = Date.MinValue;

		protected Date Start = Date.MaxValue;

		protected IList<double> Series { get; } = new List<double>();

		public virtual double this[Date date]
		{
			get
			{
				if (date < Start || date > End)
				{
					return 0;
				}

				var index = (date - Start).Days;
				return Series[index];
			}
		}

		public virtual void AddMany(IEnumerable<Transaction> transactions)
		{
			transactions = transactions ?? throw new ArgumentNullException(nameof(transactions));
			
			if (!transactions.Any())
			{
				return;
			}

			var dates = transactions.Select(x => new Date(x.TimeStamp.Date)).ToList();
			if (!Series.Any())
			{
				Series.Add(0);
				Start = End = dates.First();
			}

			for (var min = dates.Min(); min < Start; Start = Start.AddDays(-1))
			{
				Series.Insert(0, 0);
			}


			for (var max = dates.Max(); max > End; End = End.AddDays(1))
			{
				Series.Add(0);
			}

			foreach (var transaction in transactions)
			{
				// TODO: Money to decimal
				Series[(transaction.TimeStamp.Date.ToDate() - Start).Days] += (double) transaction.Amount;
			}
		}
	}
}
