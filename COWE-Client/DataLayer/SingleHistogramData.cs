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
    public class SingleHistogramData
    {
        private BindingList<SingleHistogram> _SingleHistograms = new BindingList<SingleHistogram>();
        //private CaptureState _CaptureState = CaptureState.Unknown;
        //private BatchType _BatchType = BatchType.Unknown;
        //private int _CaptureBatchId = 0;

        public SingleHistogramData() { }
        public SingleHistogramData(BindingList<SingleHistogram> singleHistograms)
        {
            this._SingleHistograms = singleHistograms;
        }

        //public SingleHistogramData(int captureBatchId, CaptureState captureState)
        //{
        //    this._CaptureBatchId = captureBatchId;
        //    this._CaptureState = captureState;
        //}

        public BindingList<SingleHistogram> GetSingleHistogramData(int captureBatchId)
        {
            BindingList<SingleHistogram> SingleHistogramData = new BindingList<SingleHistogram>();

            using (var context = new PacketAnalysisEntity())
            {
                var histograms = from p in context.SingleHistograms
                                 where p.CaptureBatchId == captureBatchId
                                 select p;

                foreach (SingleHistogram h in histograms)
                {
                    SingleHistogramData.Add(h);
                }
            }
            return SingleHistogramData;
        }
        public void InsertSingleHistogramData()
        {
            using (var context = new PacketAnalysisEntity())
            {
                foreach (SingleHistogram h in this._SingleHistograms)
                {
                    context.SingleHistograms.Add(h);
                    //context.SaveChanges();
                }
                
                context.SaveChanges();
            }
        }

        //public void DeleteSingleHistogramData()
        //{
        //    int captureState = Convert.ToInt32(this._CaptureState);

        //    using (var context = new PacketAnalysisEntity())
        //    {
        //        var deleteHistogram = (from h in context.SingleHistograms
        //                               where h.BatchType == batchType && h.CaptureState == captureState
        //                               select h).ToList();

        //        foreach (SingleHistogram h in deleteHistogram)
        //        {
        //            context.SingleHistograms.Remove(h);    
        //        }
                
        //        context.SaveChanges();
        //    }
        //}
    }
}
