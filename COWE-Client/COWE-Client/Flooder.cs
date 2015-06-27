using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace COWE.Client
{
    public class Flooder
    {
        public Flooder() { }
        public Flooder(int pid, IPAddress connectionIpAddress, int connectionPortNumber, string sourceIpAddress, int sourcePortNumber, string destinationIpAddress, int destinationPortNumber)
        {
            this.Pid = pid;
            this.ConnectionIpAddress = connectionIpAddress;
            this.ConnectionPortNumber = connectionPortNumber;
            this.SourceIpAddress = sourceIpAddress;
            this.SourcePortNumber = sourcePortNumber;
            this.DestinationIpAddress = destinationIpAddress;
            this.DestinationPortNumber = destinationPortNumber;
        }
        public int Pid { get; set; }
        public IPAddress ConnectionIpAddress { get; set; }
        public int ConnectionPortNumber { get; set; }
        public string SourceIpAddress { get; set; }
        public int SourcePortNumber { get; set; }
        public string DestinationIpAddress { get; set; }
        public int DestinationPortNumber { get; set; }
        public string Status { get; set; }
    }
}
