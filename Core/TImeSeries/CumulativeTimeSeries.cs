using System.Collections.Generic;
using System;
using System.Linq;

namespace Core.TimeSeries
{
    public sealed class CumulativeTimeSeries : TimeSeries
    {
        private List<double> AccumulatedSeries { get; } = new List<double>();
        public double Increment { get; }
        public double Capacity { get; }
        
        public CumulativeTimeSeries(double increment, double capacity)
        {
            Increment = increment;
            Capacity = capacity;
        }

        public CumulativeTimeSeries(IEnumerable<Transaction> transactions, double increment, double capacity) : this(increment, capacity) =>
            AddMany(transactions);
        
        public override double this[Date date]
        {
            get
            {
                if (date < Start)
                {
                    return Capacity;
                }

                if (date <= End)
                {
                    return AccumulatedSeries[(date - Start).Days];
                }

                return Math.Min(((date - End).Days) * Increment + AccumulatedSeries.Last(), Capacity);
            }
        }

        public override void AddMany(IEnumerable<Transaction> transactions)
        {
            base.AddMany(transactions);

            AccumulatedSeries.Clear();

            // Accumulation algorithm
            var accumulated = Capacity;
            foreach (var observation in Series)
            {
                accumulated = Math.Min(accumulated - observation + Increment, Capacity);
                AccumulatedSeries.Add(accumulated);
            }
        }
    }
}
