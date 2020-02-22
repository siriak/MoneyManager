using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.TimeSeries
{
    public sealed class SmoothedTimeSeries : TimeSeries
    {
        private List<double> SmoothedSeries { get; } = new List<double>();
        public double SmoothingRatio { get; }
        
        public SmoothedTimeSeries(double smoothingRatio) => SmoothingRatio = smoothingRatio;

        public SmoothedTimeSeries(List<Transaction> transactions, double smoothingRatio) : this(smoothingRatio) =>
            AddMany(transactions);
        
        public override double this[Date date]
        {
            get
            {
                if (date < Start)
                {
                    return 0;
                }

                if (date <= End)
                {
                    return SmoothedSeries[(date - Start).Days];
                }

                return Math.Pow(SmoothingRatio, (date - End).Days * SmoothedSeries.Last());
            }
        }

        public override void AddMany(IEnumerable<Transaction> transactions)
        {
            base.AddMany(transactions);

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
            foreach (var observation in Series)
            {
                smoothed = SmoothingRatio * smoothed + (1 - SmoothingRatio) * observation;
                SmoothedSeries.Add(smoothed);
            }
        }
    }
}
