using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class CaptureBatch
    {
        public CaptureBatch() { }

        public int CaptureBatchId { get; set; }
        public string FileName { get; set; }
        public bool Marked { get; set; }
        public decimal Mean { get; set; }
        public virtual ICollection<BatchInterval> BatchIntervals { get; set; }
        public virtual ICollection<CapturePacket> CapturePackets { get; set; }
    }
}
