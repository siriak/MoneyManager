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

        public TimeSeries(double smoothingRatio) => SmoothingRatio = smoothingRatio;

        public TimeSeries(IEnumerable<Transaction> transactions, double smoothingRatio) : this(smoothingRatio) => AddMany(transactions);

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

            // Exponential smoothing
            // SmoothingRatio == 1 - completely smoothed series (always zero)
            // SmoothingRatio == 0 - raw unsmoothed series
            // Let s_n be n-th element of smoothed series
            // Then s_n / s_infinity > acc
            // if n >  log(1 - acc) base SmoothingRatio
            // e.g.
            // SmoothingRatio=0.99 and acc=0.99 => n>458
            // SmoothingRatio=0.99 and acc=0.95 => n>298
            // SmoothingRatio=0.95 and acc=0.99 => n>89
            // SmoothingRatio=0.95 and acc=0.95 => n>58
            var smoothed = 0d;
            foreach (var observation in RawSeries)
            {
                smoothed = SmoothingRatio * smoothed + (1 - SmoothingRatio) * observation;
                SmoothedSeries.Add(smoothed);
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
