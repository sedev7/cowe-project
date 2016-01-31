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

        public static int Count
        {
            get
            {
                return fileQueue.Count;
            }
        }
        public static void Enqueue(CurrentCaptureFile captureFile)
        {
            fileQueue.Enqueue(captureFile);
        }
        public static CurrentCaptureFile Dequeue()
        {
            if (fileQueue.Count > 0)
            {
                CurrentCaptureFile captureFile = new CurrentCaptureFile();
                captureFile = fileQueue.Dequeue();
                return captureFile;
            }
            else
            {
                return null;
            }
        }
        public static void Clear()
        {
            if(fileQueue.Count > 0)
            {
                fileQueue.Clear();
            }
        }
    }
}
