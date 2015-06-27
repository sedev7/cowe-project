using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class BatchStatistics
    {
        public BatchStatistics() { }

        public int IntervalCount { get; set; }
        public decimal PacketCountMean { get; set; }
        public decimal PacketCountStandardDeviation { get; set; }
        public int PacketCountMinimum { get; set; }
        public int PacketCountMaximum { get; set; }
    }
}
