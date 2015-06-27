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
    public class AnalyzeData
    {
        BindingList<BatchIntervalMarked> _batchIntervals = new BindingList<BatchIntervalMarked>();
        CaptureState _marked = CaptureState.Unknown;
        public AnalyzeData(BindingList<BatchIntervalMarked> batchIntervals)
        {
            this._batchIntervals = batchIntervals;
            this._marked = (from m in batchIntervals
                            select m.Marked).FirstOrDefault();
        }

        public void DisplayData()
        {
            
        }



        
    }
}
