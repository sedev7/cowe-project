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
        //public event ReceivedParsedFileEventHandler ReceivedParsedFile;
        #region Constructors
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
        #endregion

        #region Delegates
        // Define a delegate type
        public delegate void ReceivedParsedFileEventHandler(string msg);
        #endregion

        #region Private Variables
        // Define a member variable of this delegate
        private ReceivedParsedFileEventHandler listOfHandlers;

        #endregion

        #region Properties
        public int CaptureBatchId { get; set; }
        public string FileName { get; set; }
        public CaptureState Marked { get; set; }
        #endregion

        #region Methods
        // Add registration function for the caller
        public void RegisterWithCaptureFile(ReceivedParsedFileEventHandler methodToCall)
        {
            listOfHandlers = methodToCall;
        }
        #endregion
        public void ReceiveFile()
        {
            //if (ReceivedParsedFile != null)
            //{
            //    ReceivedParsedFile("received file");
            //}

            if (listOfHandlers != null)
            {
                listOfHandlers("received file");
            }
        }
    }
}
