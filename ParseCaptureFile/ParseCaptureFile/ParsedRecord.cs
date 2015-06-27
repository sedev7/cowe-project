using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCaptureFile
{
    class ParsedRecord
    {
        #region Automatic Properties
        public int AckNumber { get; set; }
        public int StartingPacketNumber { get; set; }
        public int EndingPacketNumber { get; set; }
        public decimal TransmitTime { get; set; }
        public int DataTransmitted { get; set; }
        #endregion

        #region Constructor
        public ParsedRecord() { }
        #endregion

    }
}
