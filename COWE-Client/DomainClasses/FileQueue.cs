using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DomainClasses
{
    public static class FileQueue
    {
        private static Queue<CurrentCaptureFile> fileQueue = new Queue<CurrentCaptureFile>();

        public static void Enqueue(CurrentCaptureFile captureFile)
        {
            fileQueue.Enqueue(captureFile);
        }
        public static CurrentCaptureFile Dequeue()
        {
            CurrentCaptureFile captureFile = new CurrentCaptureFile();
            captureFile = fileQueue.Dequeue();
            return captureFile;
        }
    }
}
