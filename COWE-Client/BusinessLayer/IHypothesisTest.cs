using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;

namespace COWE.BusinessLayer
{
    interface ICalculateHypothesisTest
    {
        HypothesisTest GetHypothesisTestResult();
    }
}
