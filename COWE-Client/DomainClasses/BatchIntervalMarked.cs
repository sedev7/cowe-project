using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class BatchIntervalMarked
    {
        public BatchIntervalMarked() { }
        public int BatchIntervalId { get; set; }
        public int CaptureBatchId { get; set; }
        public int IntervalNumber { get; set; }
        public int PacketCount { get; set; }
        public CaptureState Marked { get; set; }
    }
}
