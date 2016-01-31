using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DomainClasses
{
    public class BaseStatistics
    {
        private BindingList<BatchIntervalMarked> _BatchIntervals;
        public BaseStatistics(BindingList<BatchIntervalMarked> batchIntervals)
        {
            _BatchIntervals = batchIntervals;
        }
        public int Count
        {
            get
            {
                return _BatchIntervals.Count;
            }
        }
        public int Minimum
        {
            get
            {
                return (from t in _BatchIntervals select t.PacketCount).Min();
            }
        }
        public int Maximum
        {
            get
            {
                return (from t in _BatchIntervals select t.PacketCount).Max();
            }
        }
        public decimal Mean
        {
            get
            {
                return Convert.ToDecimal((from t in _BatchIntervals select t.PacketCount).Average());
            }
        }
        public decimal Variance
        {
            get
            {
                return Convert.ToDecimal(CalculateVariance());
            }
        }
        public decimal StdDev
        {
            get
            {
                return Convert.ToDecimal(Math.Sqrt(CalculateVariance()));
            }
        }

        private double CalculateVariance()
        {
            // Calculate standard deviation
            double varianceSum = 0;
            double variance = 0;

            var packets = (from t in _BatchIntervals select t.PacketCount).ToList();
            var packetAverage = packets.Sum() / (double)this.Count;

            foreach (var item in packets)
            {
                double packetVariance = Math.Pow((Convert.ToDouble(item) - Convert.ToDouble(packetAverage)), 2);
                varianceSum += packetVariance;
            }
            variance = (Convert.ToDouble(varianceSum) / (this.Count - 1));

            return variance;
        }
        
    }
}
