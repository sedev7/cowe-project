using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class CapturePacket
    {
        public CapturePacket() { }
        public int CapturePacketId { get; set; }
        [Key, ForeignKey("CaptureBatch")]
        public int CaptureBatchId { get; set; }
        public int PacketNumber { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Marked { get; set; }
        public virtual CaptureBatch CaptureBatch { get; set; }
    }
}
