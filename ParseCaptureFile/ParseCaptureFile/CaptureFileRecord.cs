using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCaptureFile
{
    class CaptureFileRecord
    {
        #region Automatic Properties
        public int PacketNumber { get; set; }
        public Decimal ArrivalTime { get; set; }
        public string SourceIP { get; set; }
        public string DestinationIP { get; set; }
        public string Protocol { get; set; }
        public int DataLength { get; set; }
        public string Information { get; set; }

        #endregion

        #region Constructor
        public CaptureFileRecord() { }
        #endregion
    }
}
