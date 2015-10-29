using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DomainClasses
{
    public class SingleHistogram
    {
        public int SingleHistogramId { get; private set; }

        [Key, ForeignKey("CaptureBatch")]
        public int CaptureBatchId { get; set; }
        public int Interval { get; set; }
        public decimal Probability { get; set; }
        public int CaptureState { get; set; }
        public int BatchType { get; set; }
        public virtual CaptureBatch CaptureBatch { get; set; }
    }
}
