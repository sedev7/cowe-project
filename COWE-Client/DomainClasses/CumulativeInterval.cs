using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class CumulativeInterval
    {
        public CumulativeInterval() { }
        public int CumulativeIntervalNumber { get; set; }
        public int PacketCount { get; set; }
        public bool Marked { get; set; }
    }
}
