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
    public class HistogramData
    {
        private BindingList<Histogram> _Histogram = new BindingList<Histogram>();
        private CaptureState _CaptureState = CaptureState.Unknown;
        private BatchType _BatchType = BatchType.Unknown;

        public HistogramData(BindingList<Histogram> histogram)
        {
            this._Histogram = histogram;
        }

        public HistogramData(BatchType batchType, CaptureState captureState)
        {
            this._BatchType = batchType;
            this._CaptureState = captureState;
        }

        public void InsertHistogramData()
        {
            using (var context = new PacketAnalysisEntity())
            {
                foreach (Histogram h in this._Histogram)
                {
                    context.Histograms.Add(h);
                    //context.SaveChanges();
                }
                
                context.SaveChanges();
            }
        }

        public void DeleteCumulativeHistogramData()
        {
            int batchType = Convert.ToInt32(this._BatchType);
            int captureState = Convert.ToInt32(this._CaptureState);

            using (var context = new PacketAnalysisEntity())
            {
                var deleteHistogram = (from h in context.Histograms
                                       where h.BatchType == batchType && h.CaptureState == captureState
                                       select h).ToList();

                foreach (Histogram h in deleteHistogram)
                {
                    context.Histograms.Remove(h);    
                }
                
                context.SaveChanges();
            }
        }
    }
}
