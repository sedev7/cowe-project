using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.Client
{
    public class PcapNetworkInterface
    {
        public PcapNetworkInterface() { }

        public PcapNetworkInterface(string nicNumber, string nicName, string nicDescription)
        {
            this.NicNumber = nicNumber;
            this.NicName = nicName;
            this.NicDescription = nicDescription;
        }
        public string NicDescription { get; set; }
        public string PcapDescription { get; set; }
        public string NicNumber { get; set; }
        public string NicName { get; set; }
        public bool Connected { get; set; }
        public string IpAddress { get; set; }
    }
}
