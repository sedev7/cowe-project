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
    /// <summary>
    /// Expands packet counts into sequential units and uses a step function between cumulative probability data points to 
    /// to assign a cumulative probability to each unit in the sequence.
    /// </summary>
    public class ExpandPacketCountStep : IExpandPacketCount
    {
        private BindingList<CumulativeProbabilityDistribution> _cpdPackets;
        private int _maxPacketCount;
        public ExpandPacketCountStep(BindingList<CumulativeProbabilityDistribution> cpdPackets, int maxPacketCount)
        {
            this._cpdPackets = cpdPackets;
            this._maxPacketCount = maxPacketCount;
        }
        public SortedDictionary<int, decimal> ExpandPacketCount()
        {
            int lastInterval = 0;
            decimal newProbability = 0M;
            SortedDictionary<int, decimal> expandedPacketCount = new SortedDictionary<int, decimal>();

            foreach (var item in _cpdPackets)
            {
                if (item.Interval > lastInterval && item.Interval <= _maxPacketCount)
                {
                    int numberOfIntervals = item.Interval - lastInterval - 1;
                    //decimal intervalProbability = (item.Probability - newProbability) / numberOfIntervals;
                    decimal intervalProbability = newProbability;

                    // Probability of first expanded packet count will be zero
                    if (_cpdPackets.IndexOf(item) == 0)
                    {
                        expandedPacketCount.Add(0, 0M);
                    }

                    // Add each expanded packet count and probability in the range to the dictionary
                    for (int i = 1; i <= numberOfIntervals; i++)
                    {
                        //newProbability = newProbability + intervalProbability;
                        expandedPacketCount.Add(lastInterval + i, newProbability);
                    }
                    // Last packet count is the current interval value
                    // Reset the last interval to the current interval
                    lastInterval = item.Interval - 1;
                }
                // Move to next interval and reset the probability
                newProbability = item.Probability;
            }

            // Check for packet counts that are less than the maximum packet count and assign a probability of 1
            // to any that are found
            int maxMarkedPacketCount = _cpdPackets[_cpdPackets.Count - 1].Interval;
            if (_maxPacketCount > maxMarkedPacketCount)
            {
                // We have fewer packet counts in this distribution than the max packet count, so add one to the packet
                // count with a probability of 1.0 for each incremental packet count, up to maxMarkedPacketCount
                for (int i = 0; i < _maxPacketCount - maxMarkedPacketCount; i++)
                {
                    expandedPacketCount.Add(++lastInterval, 1.0M);
                }
            }
            return expandedPacketCount;
        }
    }
}
