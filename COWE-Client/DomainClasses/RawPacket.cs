using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class RawPacket
    {
        public RawPacket() {}

        public RawPacket(int capturePacketId, string fileName, int packetNumber, DateTime timeStamp, CaptureState marked)
        {
            this.CapturePacketId = capturePacketId;
            this.FileName = fileName;
            this.PacketNumber = packetNumber;
            this.TimeStamp = timeStamp;
            this.Marked = marked;
        }

        public RawPacket(int capturePacketId, int captureBatchId, int packetNumber, DateTime timeStamp, CaptureState marked)
        {
            this.CapturePacketId = capturePacketId;
            this.CaptureBatchId = captureBatchId;
            this.PacketNumber = packetNumber;
            this.TimeStamp = timeStamp;
            this.Marked = marked;
        }
        public int CaptureBatchId { get; set; }
        public int CapturePacketId { get; set; }
        public string FileName { get; set; }
        public int PacketNumber { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public CaptureState Marked { get; set; }
    }
}
