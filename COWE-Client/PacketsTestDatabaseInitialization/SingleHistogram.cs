//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace COWE.PacketsTestDatabaseInitialization
{
    using System;
    using System.Collections.Generic;
    
    public partial class SingleHistogram
    {
        public int SingleHistogramId { get; set; }
        public int CaptureBatchId { get; set; }
        public int Interval { get; set; }
        public decimal Probability { get; set; }
        public int CaptureState { get; set; }
    
        public virtual CaptureBatch CaptureBatch { get; set; }
    }
}
