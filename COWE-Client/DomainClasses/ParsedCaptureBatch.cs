using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DomainClasses
{
    public class ParsedCaptureBatch
    {
        public ParsedCaptureBatch() { }
        public ParsedCaptureBatch(int captureBatchId, bool marked, BindingList<PacketInterval> packetIntervals)
        {
            this.CaptureBatchId = captureBatchId;
            this.Marked = marked;
            this.PacketIntervals = packetIntervals;
        }

        public int CaptureBatchId { get; set; }
        public bool Marked { get; set; }
        public BindingList<PacketInterval> PacketIntervals { get; set; }
    }
}
