using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.DataLayer
{
    public class CumulativeProbabilityDistributionData
    {
        // need to create insert and delete functions
        public BindingList<CumulativeProbabilityDistribution> GetCumulativeProbabilityDistribution(CaptureState captureState)
        {
            BindingList<CumulativeProbabilityDistribution> probabilities = new BindingList<CumulativeProbabilityDistribution>();

           using (var context = new PacketAnalysisEntity())
           {
               var cpd = from c in context.CumulativeProbabilityDistributions
                         where c.CaptureState == (int)captureState
                         select c;

               foreach (CumulativeProbabilityDistribution dist in cpd)
               {
                   probabilities.Add(dist);
               }
           }
           return probabilities;
        }
        public void InsertCumulativeProbabilityDistribution(BindingList<CumulativeProbabilityDistribution> cumulativeProbabilityDistribution)
        {
            using (var context = new PacketAnalysisEntity())
            {
                foreach (CumulativeProbabilityDistribution cpd in cumulativeProbabilityDistribution)
                {
                    context.CumulativeProbabilityDistributions.Add(cpd);
                }
                context.SaveChanges();
            }
        }
        public void DeleteCumulativeProbabilityDistribution(CaptureState captureState)
        {
            using (var context = new PacketAnalysisEntity())
            {
                var distribution = from d in context.CumulativeProbabilityDistributions
                                   where d.CaptureState == (int)captureState
                                   select d;

                foreach (CumulativeProbabilityDistribution cpd in distribution)
                {
                    context.CumulativeProbabilityDistributions.Remove(cpd);
                }

                context.SaveChanges();
            }
        }
    }
}
