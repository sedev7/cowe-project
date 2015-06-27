using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.Enumerations;

namespace COWE.DomainClasses
{
    public class CurrentCaptureFile
    {
        public CurrentCaptureFile() { }
        public CurrentCaptureFile(string fileName, CaptureState marked)
        {
            this.FileName = fileName;
            this.Marked = marked;
        }
        public CurrentCaptureFile(int captureBatchId, CaptureState marked)
        {
            this.CaptureBatchId = captureBatchId;
            this.Marked = marked;
        }
        public CurrentCaptureFile(string fileName, int captureBatchId, CaptureState marked)
        {
            this.FileName = fileName;
            this.CaptureBatchId = captureBatchId;
            this.Marked = marked;
        }
        public int CaptureBatchId { get; set; }
        public string FileName { get; set; }
        public CaptureState Marked { get; set; }
    }
}
