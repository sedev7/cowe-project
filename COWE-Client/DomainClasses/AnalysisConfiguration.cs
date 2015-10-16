using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public static class AnalysisConfiguration
    {
        public static bool TrimSmallPackets { get; set; }
        public static int HistogramBinSize { get; set; }
        public static int IntervalSize { get; set; }
        public static HypothesisTest HypothesisTest { get; set; }
    }
}
