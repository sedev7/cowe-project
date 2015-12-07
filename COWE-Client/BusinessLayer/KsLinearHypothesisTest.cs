using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.BusinessLayer
{
    public class KsLinearHypothesisTest : ICalculateHypothesisTest
    {
        public HypothesisTest GetHypothesisTestResult()
        {
            HypothesisTest ht = new HypothesisTest(); ;

            // Get cumulative probability distribution data and find the max difference between marked and unmarked distributions
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            BindingList<CumulativeProbabilityDistribution> markedCPD = new BindingList<CumulativeProbabilityDistribution>();
            BindingList<CumulativeProbabilityDistribution> unmarkedCPD = new BindingList<CumulativeProbabilityDistribution>();
            markedCPD = pcp.GetCumulativeProbabilityDistributionData(CaptureState.Marked);
            unmarkedCPD = pcp.GetCumulativeProbabilityDistributionData(CaptureState.Unmarked);

            if (markedCPD.Count > 0 && unmarkedCPD.Count > 0)
            {
                decimal maxVariance = 0M;
                int intervalCount = 0;

                // Only compare intervals from each distribution with a corresponding interval in the other distribution 
                if (unmarkedCPD.Count > markedCPD.Count)
                {
                    intervalCount = markedCPD.Count;
                }
                else
                {
                    intervalCount = unmarkedCPD.Count;
                }

                // Expand each distribution into equal discrete steps for comparison of cumulative probabilities
                // First, find the largest cumulative packet count (= interval)
                int maxPacketCount = 0;
                if (markedCPD[markedCPD.Count - 1].Interval >= unmarkedCPD[unmarkedCPD.Count - 1].Interval)
                {
                    maxPacketCount = markedCPD[markedCPD.Count - 1].Interval;
                }
                else
                {
                    maxPacketCount = unmarkedCPD[unmarkedCPD.Count - 1].Interval;
                }

                // Second, expand the packet counts by interpolating between packet counts (intervals) using an average probability
                // for each packet count in the range and successively adding up to the next packet count (interval); add these 
                // interpolated packets to a dictionary; outcome is a dictionary for each distribution containing packet counts and
                // probabilities from packet count = 0 to packet count = largest packet count (interval) of both distributions and 
                // the associated probabilities for each packet count.  We are basically calculating a linear estimate of packet
                // counts and probabilities between each packet count and probability in the actual distributions.

                //// Third, check for packet counts that are less than the maximum packet count and assign a probability of 1
                //// to any that are found

                ExpandPacketCountLinear markedExpPktCount = new ExpandPacketCountLinear(markedCPD, maxPacketCount);
                ExpandPacketCountLinear unmarkedExpPktCount = new ExpandPacketCountLinear(unmarkedCPD, maxPacketCount);
                SortedDictionary<int, decimal> markedCPDExpanded = new SortedDictionary<int, decimal>();
                SortedDictionary<int, decimal> unmarkedCPDExpanded = new SortedDictionary<int, decimal>();
                //markedCPDExpanded = ExpandPacketCount(markedCPD, maxPacketCount);
                //unmarkedCPDExpanded = ExpandPacketCount(unmarkedCPD, maxPacketCount);
                markedCPDExpanded = markedExpPktCount.ExpandPacketCount();
                unmarkedCPDExpanded = unmarkedExpPktCount.ExpandPacketCount();

                // Find the maximum variance between the cumulative probabilities in each distribution
                for (int i = 0; i < maxPacketCount; i++)
                {
                    #region Debug
#if(DEBUG)
                    System.Diagnostics.Debug.WriteLine("unmarkedCPDExpanded[{0}]:[{1}] - markedCPDExpanded[{2}]:[{3}] = {4}", i, unmarkedCPDExpanded[i], i, markedCPDExpanded[i], Math.Abs(unmarkedCPDExpanded[i] - markedCPDExpanded[i]));
#endif
                    #endregion
                    if (Math.Abs(unmarkedCPDExpanded[i] - markedCPDExpanded[i]) > maxVariance)
                    {
                        maxVariance = Math.Abs(unmarkedCPDExpanded[i] - markedCPDExpanded[i]);
                    }
                }

                // Compare the maximum variance with the hypothesis test threshold
                // For significance level alpha = 0.05, the K-S statistic is computed as 1.36/N^(1/2), where N is the number of samples
                decimal ksStatistic = Convert.ToDecimal(1.36 / Math.Pow(intervalCount, 0.5));
                ht.KsStatistic = ksStatistic;
                ht.MaxCpdVariance = maxVariance;
                if (maxVariance > ksStatistic)
                {
                    // Reject the null hypothesis
                    ht.KsTestResult = true;
                }
            }
            else
            {
                // Not enough data to perform the test
                ht.KsStatistic = 0;
                ht.MaxCpdVariance = 0;
                ht.KsTestResult = false;
            }
            return ht;
        }
    }
}
