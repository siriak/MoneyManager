using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class TimeSeries
    {
        private List<double> RawSeries { get; } = new List<double>();
        private List<double> SmoothedSeries { get; } = new List<double>();
        public double SmoothingRatio { get; }

        private Date Start = Date.MaxValue;
        private Date End = Date.MinValue;

        public TimeSeries(double smoothingRatio)
        {
            SmoothingRatio = smoothingRatio;
        }

        public TimeSeries(IEnumerable<Transaction> transactions, double smoothingRatio) : this(smoothingRatio)
        {
            AddMany(transactions);
        }

        public void AddMany(IEnumerable<Transaction> transactionss)
        {
            if (!transactionss.Any())
            {
                return;
            }
            if (!RawSeries.Any())
            {
                RawSeries.Add(0);
                Start = End = transactionss.First().TimeStamp.Date.ToDate();
            }

            for (var min = transactionss.Select(x => new Date(x.TimeStamp.Date)).Min(); min < Start; Start = Start.AddDays(-1))
            {
                RawSeries.Insert(0, 0);
            }


            for (var max = transactionss.Select(x => new Date(x.TimeStamp.Date)).Max(); max > End; End = End.AddDays(1))
            {
                RawSeries.Add(0);
            }

            foreach (var transaction in transactionss)
            {
                // TODO: Money to decimal
                RawSeries[(transaction.TimeStamp.Date.ToDate() - Start).Days] += (double)transaction.Amount.Amount;
            }

            SmoothedSeries.Clear();
            double accumulator = 0;
            foreach (var observation in RawSeries)
            {
                accumulator = SmoothingRatio * accumulator + (1 - SmoothingRatio) * observation;
                SmoothedSeries.Add((1 - SmoothingRatio) * accumulator);
            }
        }

        public double this[Date date]
        {
            get
            {
                if (date < Start)
                {
                    return 0;
                }

                var index = (date - Start).Days;
                if (index < SmoothedSeries.Count)
                {
                    return SmoothedSeries[index];
                }

                return Math.Pow(SmoothingRatio, index - SmoothedSeries.Count + 1) * SmoothedSeries.Last();
            }
        }
    }
}
