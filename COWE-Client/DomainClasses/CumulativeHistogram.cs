using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DomainClasses
{
    public class CumulativeHistogram
    {
        public int CumulativeHistogramId { get; private set; }
        public int Interval { get; set; }
        public decimal Probability { get; set; }
        public int CaptureState { get; set; }
        public int BatchType { get; set; }
    }
}
