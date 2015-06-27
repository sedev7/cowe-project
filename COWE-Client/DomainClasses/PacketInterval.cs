using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class PacketInterval
    {
        public PacketInterval() { }
        
        public PacketInterval(int interval, int packetCount, CaptureState packetState)
        {
            this.Interval = interval;
            this.PacketCount = packetCount;
            this.PacketState = packetState;
        }
        public PacketInterval(int captureBatchId, int interval, int packetCount, CaptureState packetState)
        {
            this.CaptureBatchId = captureBatchId;
            this.Interval = interval;
            this.PacketCount = packetCount;
            this.PacketState = packetState;
        }
        public int CaptureBatchId { get; set; }
        public int Interval { get; set; }
        public int PacketCount { get; set; }

        // Packet state is marked/unmarked; this data will be added to the database so use an enum value (i.e., an int)
        public CaptureState PacketState { get; set; }
    }
}
