using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DomainClasses
{
    public class BatchInterval
    {
        public BatchInterval() { }

        public int BatchIntervalId { get; set; }

        [Key, ForeignKey("CaptureBatch")]
        public int CaptureBatchId { get; set; }
        public int IntervalNumber { get; set; }
        public int PacketCount { get; set; }
        public virtual CaptureBatch CaptureBatch { get; set; }
    }
}