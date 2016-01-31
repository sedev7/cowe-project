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
    public class CalculateProbability
    {
        #region Variables
        BindingList<BatchIntervalMarked> _markedIntervals = new BindingList<BatchIntervalMarked>();
        Dictionary<int, int> _histogram = new Dictionary<int, int>();
        #endregion

        #region Public Methods
        public CalculateProbability(Dictionary<int, int> histogram)
        {
            this._histogram = histogram;
        }

        public CalculateProbability(BindingList<BatchIntervalMarked> markedIntervals)
        {
            this._markedIntervals = markedIntervals;

            //foreach (BatchIntervalMarked bim in markedIntervals)
            //{
            //    this._histogram.Add(bim.IntervalNumber, bim.PacketCount);
            //}
        }

        public Dictionary<int,decimal> GetProbabilityValues()
        {
            Dictionary<int, int> consolidatedPacketCounts = new Dictionary<int, int>();
            Dictionary<int, decimal> probabilities = new Dictionary<int, decimal>();
            Dictionary<int, decimal> sortedProbabilities = new Dictionary<int, decimal>();

            // Get the total packet count
            int packetCountTotal = 0;
            foreach (KeyValuePair<int, int> pair in _histogram)
            {
                packetCountTotal += pair.Value;
            }

            // Consolidate packet counts so we don't have duplicates (packet is the new key)
            foreach (KeyValuePair<int,int> pair in _histogram)
            {
                if(consolidatedPacketCounts.ContainsKey(pair.Value))
                {
                    // Add the value to the value for this key
                    consolidatedPacketCounts[pair.Value] += pair.Value;
                }
                else
                {
                    // Add the key value pair
                    consolidatedPacketCounts.Add(pair.Value, pair.Value);
                }
            }

            foreach (KeyValuePair<int,int> pair in consolidatedPacketCounts)
            {
                probabilities.Add(pair.Key, (decimal)pair.Value / packetCountTotal);
            }

            var items = from p in probabilities
                    orderby p.Key ascending
                    select p;

            foreach (KeyValuePair<int,decimal> pair in items)
            {
                sortedProbabilities.Add(pair.Key, pair.Value);
            }

            return sortedProbabilities;
        }

        public SortedDictionary<int, decimal> GetProbabilityByPacketRange(bool trimZeroIntervals, int histogramBinSize)
        {
            // We are calculating the number of intervals that contain packets counts
            // in the specified range (packetRange)

            Dictionary<int, int> packetRangeCounts = new Dictionary<int, int>();

            int minPacketRange = histogramBinSize;
            int packetRange = minPacketRange;

            foreach (BatchIntervalMarked bim in _markedIntervals)
            {
                // Find the packet count range top
                int rangeTop = 0;
                int rem = bim.PacketCount % packetRange;
                if (rem == 0)
                {
                    if (bim.PacketCount > 0)
                    {
                        // We  are at the top of the range (PacketCount is a multiple of packetRange)
                        rangeTop = bim.PacketCount;

                        // Add this range to the interval count
                        if (packetRangeCounts.ContainsKey(rangeTop))
                        {
                            // Increment the interval count for this range
                            packetRangeCounts[rangeTop]++;
                        }
                        else
                        {
                            // We don't have this range yet - add a new one
                            packetRangeCounts.Add(rangeTop, 1);
                        }
                    }
                    else
                    {
                        // The packet count is zero
                        if (!trimZeroIntervals)
                        {
                            rangeTop = packetRange;

                            // Add this range to the interval count
                            if (packetRangeCounts.ContainsKey(rangeTop))
                            {
                                // Increment the interval count for this range
                                packetRangeCounts[rangeTop]++;
                            }
                            else
                            {
                                // We don't have this range yet - add a new one
                                packetRangeCounts.Add(rangeTop, 1);
                            }
                        }
                    }
                }
                else
                {
                    // We are below the top of the range - need to find the top of the range
                    rangeTop = bim.PacketCount + (packetRange - rem);

                    if (rangeTop == minPacketRange && !trimZeroIntervals)
                    {
                        // Add this range to the interval count
                        if (packetRangeCounts.ContainsKey(rangeTop))
                        {
                            // The key already exists - add the packet count
                            packetRangeCounts[rangeTop]++;
                        }
                        else
                        {
                            // Add a new key and packet count
                            packetRangeCounts.Add(rangeTop, 1);
                        }
                    }
                    else if(rangeTop != minPacketRange)
                    {
                        // Add this range to the interval count
                        if (packetRangeCounts.ContainsKey(rangeTop))
                        {
                            // The key already exists - add the packet count
                            packetRangeCounts[rangeTop]++;
                        }
                        else
                        {
                            // Add a new key and packet count
                            packetRangeCounts.Add(rangeTop, 1);
                        }
                    }
                }
            }

            // Get a count of the intervals for each range and the total number of intervals
            int rangeCountTotal = 0;
            SortedDictionary<int, int> intervalRangeCounts = new SortedDictionary<int, int>();
            foreach (KeyValuePair<int, int> pair in packetRangeCounts)
            {
                rangeCountTotal += pair.Value;

                intervalRangeCounts.Add(pair.Key, pair.Value);
            }

            SortedDictionary<int, decimal> probabilities = new SortedDictionary<int, decimal>();
            
            foreach (KeyValuePair<int, int> pair in intervalRangeCounts)
            {
                probabilities.Add(pair.Key, (decimal)pair.Value / rangeCountTotal);
            }

            return probabilities;
        }
        public SortedDictionary<int, decimal> GetCumulativeProbabilityDistribution(SortedDictionary<int, decimal> probability)
        {
            decimal cumulativeProbabilityValue = 0;

            SortedDictionary<int, decimal> cumulativeProbability = new SortedDictionary<int, decimal>();

            foreach (KeyValuePair<int,decimal> pair in probability)
            {
                cumulativeProbabilityValue += pair.Value;
                cumulativeProbability.Add(pair.Key, cumulativeProbabilityValue);
            }
            return cumulativeProbability;
        }
        #endregion
    }
}
