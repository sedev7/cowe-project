using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class DisplayStatistic
    {
        public int DisplayStatisticId { get; private set; }
        public int IntervalCount { get; set; }
        public int TrimmedIntervalCount { get; set; }
        public decimal MeanPacketsPerInterval { get; set; }
        public decimal StandardDeviation { get; set; }
        public int MinPacketsPerInterval { get; set; }
        public int MaxPacketsPerInterval { get; set; }
        public decimal MeanOfMeans { get; set; }
        public decimal MeanOfMeansStandardDeviation { get; set; }
        public bool Marked { get; set; }
        public int BatchType { get; set; }
    }
}
