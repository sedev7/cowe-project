
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.Client
{
    public class ReceivedParsedFileEventArgs : EventArgs
    {
        #region Automatic Properties

        public string ReceivedParsedFile { get; set; }
        #endregion

        #region Constructor
        public ReceivedParsedFileEventArgs(string fileName)
        {
            this.ReceivedParsedFile = fileName;
        }
        #endregion
    }
}
