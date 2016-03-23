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
    
    public partial class CaptureBatch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaptureBatch()
        {
            this.BatchIntervals = new HashSet<BatchInterval>();
            this.CapturePackets = new HashSet<CapturePacket>();
            this.SingleHistograms = new HashSet<SingleHistogram>();
        }
    
        public int CaptureBatchId { get; set; }
        public string FileName { get; set; }
        public bool Marked { get; set; }
        public decimal Mean { get; set; }
        public decimal TrimmedMean { get; set; }
        public bool Parsed { get; set; }
        public bool SingleStatistics { get; set; }
        public bool CumulativeStatistics { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchInterval> BatchIntervals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CapturePacket> CapturePackets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SingleHistogram> SingleHistograms { get; set; }
    }
}