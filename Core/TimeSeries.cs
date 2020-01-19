using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class TimeSeries
    {
        private List<Observation> Series { get; } = new List<Observation>();

        public Date Start { get; private set; } = Date.MaxValue;

        public Date End { get; private set; } = Date.MinValue;

        public void AddMany(List<Transaction> ts)
        {
            if (ts.Any())
            {
                if (!Series.Any())
                {
                    Series.Add(new Observation() { Date = ts[0].TimeStamp.Date.ToDate() });
                }
                var min = ts.Select(x => new Date(x.TimeStamp.Date)).Min();
                Start = Start.CompareTo(min) > 0 ? min : Start;
                while (Series.First().Date > Start)
                {
                    Series.Insert(0, new Observation() { Date = Series[0].Date.AddDays(-1) });
                }

                var max = ts.Select(x => new Date(x.TimeStamp.Date)).Max();
                End = End.CompareTo(max) < 0 ? max : End;
                while (Series.Last().Date < End)
                {
                    Series.Add(new Observation() { Date = Series.Last().Date.AddDays(1) });
                }

                foreach (var transaction in ts)
                {
                    // TODO: Money to decimal
                    this[transaction.TimeStamp.Date.ToDate()].Value += transaction.Amount.Amount;
                }
            }
        }

        public Observation this[Date date] => Series[(date - Start).Days];
    }
}
