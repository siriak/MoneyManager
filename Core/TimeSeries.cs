using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class TimeSeries
    {
        public List<Observation> Series { get; set; }

        public Date Start { get; set; }

        public Date End { get; set; }
    }
}
