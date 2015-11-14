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
    public class CumulativeHistogramData
    {
        private BindingList<CumulativeHistogram> _CumulativeHistogram = new BindingList<CumulativeHistogram>();
        private CaptureState _CaptureState = CaptureState.Unknown;
        private BatchType _BatchType = BatchType.Unknown;

        public CumulativeHistogramData(BindingList<CumulativeHistogram> CumulativeHistogram)
        {
            this._CumulativeHistogram = CumulativeHistogram;
        }

        public CumulativeHistogramData(CaptureState captureState)
        {
            this._CaptureState = captureState;
        }

        public BindingList<CumulativeHistogram> GetCumulativeHistogramData()
        {
            BindingList<CumulativeHistogram> CumulativeHistogramData = new BindingList<CumulativeHistogram>();

            using (var context = new PacketAnalysisEntity())
            {
                var histograms = from p in context.CumulativeHistograms
                                 where p.CaptureState == (int)_CaptureState
                                 select p;

                foreach (CumulativeHistogram h in histograms)
                {
                    CumulativeHistogramData.Add(h);
                }
            }
            return CumulativeHistogramData;
        }
        public void InsertCumulativeHistogramData()
        {
            using (var context = new PacketAnalysisEntity())
            {
                foreach (CumulativeHistogram h in this._CumulativeHistogram)
                {
                    context.CumulativeHistograms.Add(h);
                    //context.SaveChanges();
                }

                context.SaveChanges();
            }
        }

        public void DeleteCumulativeHistogramData()
        {
            int captureState = Convert.ToInt32(this._CaptureState);

            using (var context = new PacketAnalysisEntity())
            {
                var deleteHistogram = (from h in context.CumulativeHistograms
                                       where h.CaptureState == captureState
                                       select h).ToList();

                foreach (CumulativeHistogram h in deleteHistogram)
                {
                    context.CumulativeHistograms.Remove(h);
                }

                context.SaveChanges();
            }
        }
    }
}
