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
        public CurrentCaptureFile(string fileName, CaptureState captureState)
        {
            this.FileName = fileName;
            this.CaptureState = captureState;
        }
        public CurrentCaptureFile(int captureBatchId, CaptureState captureState)
        {
            this.CaptureBatchId = captureBatchId;
            this.CaptureState = captureState;
        }
        public CurrentCaptureFile(string fileName, int captureBatchId, CaptureState captureState)
        {
            this.FileName = fileName;
            this.CaptureBatchId = captureBatchId;
            this.CaptureState = captureState;
        }
        #endregion

        #region Delegates
        // Define a delegate type
        //public delegate void ReceivedParsedFileEventHandler(string msg);
        public delegate void ReceivedParsedFileEventHandler(CurrentCaptureFile captureFile);
        #endregion

        #region Events
        //public event ReceivedParsedFileEventHandler ReceivedParsedFile;
        #endregion

        #region Private Variables
        // Define a member variable of this delegate
        //private ReceivedParsedFileEventHandler listOfHandlers;
        #endregion
        #region Public Variables
        //public ReceivedParsedFileEventHandler listOfHandlers;
        #endregion

        #region Properties
        public int CaptureBatchId { get; set; }
        public string FileName { get; set; }
        public CaptureState CaptureState { get; set; }
        #endregion

        #region Methods
        // Add registration function for the caller
        //public void RegisterWithCaptureFile(ReceivedParsedFileEventHandler methodToCall)
        //{
        //    listOfHandlers = methodToCall;
        //}
        #endregion
        //public void ReceiveFile()
        //{
        //    if (ReceivedParsedFile != null)
        //    {
        //        ReceivedParsedFile("Received parsed file");
        //    }

        //    //if (listOfHandlers != null)
        //    //{
        //    //    listOfHandlers("received file");
        //    //}
        //}
        public void ReceiveFile(CurrentCaptureFile captureFile)
        {
            // No longer using this event
            //if (ReceivedParsedFile != null)
            //{
            //    ReceivedParsedFile(captureFile);
            //}
        }
    }
}
