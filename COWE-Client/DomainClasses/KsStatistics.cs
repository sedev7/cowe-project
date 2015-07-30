using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DomainClasses
{
    public class KsStatistics
    {
        public KsStatistics(double alpha, decimal zvalue)
        {
            this.Alpha = alpha;
            this.Zvalue = zvalue;
        }
        public decimal MarkedMean { get; set; }
        public decimal UnmarkedMean { get; set; }
        public decimal MarkedStdDev { get; set; }
        public decimal UnmarkedStdDev { get; set; }
        public decimal Zvalue { get; private set; }
        public double Alpha { get; private set; }
        public int MarkedIntervalCount { get; set; }
        public int UnmarkedIntervalCount { get; set; }

        public decimal MeanDifference 
        {
            get
            {
                return UnmarkedMean - MarkedMean;
            }
        }
        public decimal SigmaDifference
        {
            get
            {
                // Check for divide by zero errors
                if (MarkedIntervalCount != 0 && UnmarkedIntervalCount != 0)
                {
                    return (Convert.ToDecimal(Math.Sqrt(Math.Pow((double)MarkedStdDev, 2) / MarkedIntervalCount + Math.Pow((double)UnmarkedStdDev, 2) / UnmarkedIntervalCount)));
                }
                else
                {
                    return 0;
                }
            }
        }

        public decimal StandardDeviation
        {
            get
            {
                if (SigmaDifference != 0)
                {
                    return SigmaDifference * Zvalue;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
