using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DomainClasses
{
    public class HypothesisTest
    {
        public int HypothesisTestId { get; private set; }
        public decimal MeanOfMeansVariance { get; set; }
        public decimal MeansVarianceStandardDeviation { get; set; }
        public bool MeansTestResult { get; set; }
        public bool KsTestResult { get; set; }
        public bool HasValues { get; set; }
    }
}
