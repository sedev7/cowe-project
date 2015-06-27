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
        BindingList<BatchIntervalMarked> _markedIntervals = new BindingList<BatchIntervalMarked>();
        Dictionary<int, int> _histogram = new Dictionary<int, int>();
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

        public SortedDictionary<int, decimal> GetProbabilityByPacketRange()
        {
            Dictionary<int, int> packetRangeCounts = new Dictionary<int, int>();

            int packetRange = 25;

            foreach (BatchIntervalMarked bim in _markedIntervals)
            {
                // Find the packet count range top
                int rangeTop = 0;
                int rem = bim.PacketCount % 25;
                if(rem == 0)
                {
                    if (bim.PacketCount > 0)
                    {
                        // We  are at the top of the range
                        rangeTop = bim.PacketCount;
                    }
                    else
                    {
                        // The packet count is zero
                        rangeTop = packetRange;
                    }
                }
                else
                {
                    // We are below the top of the range
                    rangeTop = bim.PacketCount + (packetRange - rem);
                }
                packetRangeCounts.Add(bim.IntervalNumber, rangeTop);
            }

            // Get a count of the intervals for each range and the total number of interval
            int rangeCountTotal = 0;
            SortedDictionary<int, int> intervalRangeCounts = new SortedDictionary<int, int>();
            foreach (KeyValuePair<int, int> pair in packetRangeCounts)
            {
                if (intervalRangeCounts.ContainsKey(pair.Value))
                {
                    // Add the value to the value for this key
                    intervalRangeCounts[pair.Value]++; // += pair.Value;
                    rangeCountTotal++;
                }
                else
                {
                    //// Add the key value pair
                    //intervalRangeCounts.Add(pair.Value, pair.Value);
                    intervalRangeCounts.Add(pair.Value, 1);
                    rangeCountTotal++;
                }
            }

            // Get the max packet count
            //var maxPacketCount = (from m in _markedIntervals select m.PacketCount).Max();

            //Dictionary<int, int> consolidatedPacketCounts = new Dictionary<int, int>();
            SortedDictionary<int, decimal> probabilities = new SortedDictionary<int, decimal>();
            //Dictionary<int, decimal> sortedProbabilities = new Dictionary<int, decimal>();

            //// Get the total packet count
            //int packetCountTotal = 0;
            //foreach (KeyValuePair<int, int> pair in intervalRangeCounts)
            //{
            //    packetCountTotal += pair.Value;
            //}

            // Consolidate packet counts so we don't have duplicates (packet is the new key)
            //foreach (KeyValuePair<int, int> pair in intervalRangeCounts)
            //{
            //    if (consolidatedPacketCounts.ContainsKey(pair.Value))
            //    {
            //        // Add the value to the value for this key
            //        consolidatedPacketCounts[pair.Value] += pair.Value;
            //    }
            //    else
            //    {
            //        // Add the key value pair
            //        consolidatedPacketCounts.Add(pair.Value, pair.Value);
            //    }
            //}

            foreach (KeyValuePair<int, int> pair in intervalRangeCounts)
            {
                probabilities.Add(pair.Key, (decimal)pair.Value / rangeCountTotal);
            }

            //var items = from p in probabilities
            //            orderby p.Key ascending
            //            select p;

            //foreach (KeyValuePair<int, decimal> pair in items)
            //{
            //    sortedProbabilities.Add(pair.Key, pair.Value);
            //}

            //return sortedProbabilities;
            return probabilities;
        }

    }
}
