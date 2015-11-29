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
        public static HypothesisTestType HypothesisTestType { get; set; }
        public static Double Alpha { get; set; }    // Hypothesis test significance level
        public static Decimal Zvalue { get; set; }  // Z value for (1-_alpha), from standard normal distribution table
                                                    // (note: one-tailed test because we are looking at the distribution 
                                                    // for the difference of the means)
        public static string ProcessedCaptureFilesPath { get; set; }
    }
}
