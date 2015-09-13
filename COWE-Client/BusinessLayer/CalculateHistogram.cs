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
    public class HistogramChartData
    {
        KeyValuePair<int, int> _xyPoint = new KeyValuePair<int, int>();
        KeyValuePair<int, double> _xyProbability = new KeyValuePair<int, double>();
        public HistogramChartData(KeyValuePair<int, int> xyPoint)
        {
            this._xyPoint = xyPoint;
        }
        public HistogramChartData(KeyValuePair<int, double> xyProbability)
        {
            this._xyProbability = xyProbability;
        }

        public KeyValuePair<int, int> XYPoint
        {
            get
            {
                return _xyPoint;
            }
            set
            {
                _xyPoint = value;
            }
        }

        public KeyValuePair<int, double> XYProbability
        {
            get
            {
                return _xyProbability;
            }
            set
            {
                _xyProbability = value;
            }
        }
    }

    public class CalculateHistogram
    {
        public CalculateHistogram() { }

        BindingList<HistogramChartData> _histogramValues = new BindingList<HistogramChartData>();
        BindingList<HistogramChartData> _histogramPMF = new BindingList<HistogramChartData>();

        public BindingList<HistogramChartData> HistogramValues
        {
            get
            {
                return _histogramValues;
            }
            set
            {
                _histogramValues = value;
            }
        }

        public BindingList<HistogramChartData> HistogramPMF
        {
            get
            {
                return _histogramPMF;
            }
            set
            {
                _histogramPMF = value;
            }
        }

        public BindingList<HistogramChartData> CalculatePMF(BindingList<HistogramChartData> histogramValues)
        {
            BindingList<HistogramChartData> _histogramPMF = new BindingList<HistogramChartData>();
            return _histogramPMF;
            // Calculate the Probability Mass Function
            throw new Exception("Not yet implemented...");
        }

        //public BindingList<HistogramChartData> CalculateHistogramValues(BindingList<BatchIntervalMarked> batchIntervals)
        public Dictionary<int, int> CalculateHistogramValues(BindingList<BatchIntervalMarked> batchIntervals)
        {
            //BindingList<HistogramChartData> _histogramValues = new BindingList<HistogramChartData>();

            Dictionary<int, int> histValues = new Dictionary<int, int>();

            // Calculate the Histogram values
            int sampleCount = batchIntervals.Count;
            var maxValue = (from m in batchIntervals select m.PacketCount).Max();
            var minValue = (from m in batchIntervals select m.PacketCount).Min();
            int binCount = Convert.ToInt32(Math.Round(Math.Sqrt(sampleCount)));
            decimal binSize = (maxValue - minValue) / (decimal)binCount;

            // Create the bins
            int i = 0;
            while (i < binCount)
            {
                //KeyValuePair<int, int> kvp = new KeyValuePair<int,int>(i,0);
                //HistogramChartData hd = new HistogramChartData(kvp);
                //_histogramValues.Add(hd);
                histValues.Add(i, 0);
                i++;
            }

            // Assign values to then bins
            foreach (BatchIntervalMarked bim in batchIntervals)
            {
                int j = 1;
                while (j <= binCount)
                {
                    if (bim.PacketCount <= j * binSize)
                    {
                        histValues[j - 1] += bim.PacketCount;
                        break;
                    }
                    j++;
                }
            }
            //return _histogramValues;
            return histValues;
        }

        public Dictionary<int, int> CalculateHistogramValues(BindingList<CapturePacket> capturePackets)
        {
            //BindingList<HistogramChartData> _histogramValues = new BindingList<HistogramChartData>();

            if (capturePackets.Count > 0)
            {
                Dictionary<int, int> histValues = new Dictionary<int, int>();

                // Calculate the Histogram values
                int sampleCount = capturePackets.Count;
                var maxValue = (from t in capturePackets select t.TimeStamp.Ticks).Max();
                var minValue = (from t in capturePackets select t.TimeStamp.Ticks).Min();
                int binCount = Convert.ToInt32(Math.Round(Math.Sqrt(sampleCount)));
                //double interval = maxValue.Subtract(minValue).TotalMilliseconds;
                decimal interval = maxValue - minValue;
                // Note: use ceilin function to add one millisecond to binSize to account for rounding (no fractional milliseconds in a TimeSpan - must be Int64)
                //TimeSpan binSize = new TimeSpan(Convert.ToInt64(Math.Ceiling(interval / binCount)) * Convert.ToInt64(TimeSpan.TicksPerMillisecond));
                //TimeSpan binSize = new TimeSpan(Convert.ToInt64(Math.Ceiling(interval / binCount)));
                long binSize = (Convert.ToInt64(Math.Ceiling(interval / binCount)));

                // Create the bins
                int i = 0;
                while (i < binCount)
                {
                    //KeyValuePair<int, int> kvp = new KeyValuePair<int,int>(i,0);
                    //HistogramChartData hd = new HistogramChartData(kvp);
                    //_histogramValues.Add(hd);
                    histValues.Add(i, 0);
                    i++;
                }

                int j = 1;
                //DateTime currentBin = minValue.AddMilliseconds(binSize.Milliseconds);
                var startTime = (from t in capturePackets select t.TimeStamp).Min();
                var endTime = (from t in capturePackets select t.TimeStamp).Max();
                DateTime currentBin = startTime.AddTicks(binSize);

                // Assign values to then bins
                foreach (CapturePacket cp in capturePackets)
                {
                    if (j <= binCount)
                    {
                        if (cp.TimeStamp <= currentBin)
                        {
                            histValues[j - 1]++;
                        }
                        else
                        {
                            j++;
                            //currentBin = currentBin.AddMilliseconds(binSize.Milliseconds);
                            currentBin = currentBin.AddTicks(binSize);
                        }
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("[CalculateHistogramValues] - error: current bin count [{0}] exceeds number of histogram bins [{1}] - bin size: [{2}] ticks, minimum time: [{3}] ticks, maximum time: [{4}] ticks", j, binCount, binSize, minValue, maxValue);
                        throw new Exception(sb.ToString());
                    }

                }
                //return _histogramValues;
                return histValues;
            }
            else
            {
                return null;
            }
        }
    }
}
