using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;

namespace COWE.BusinessLayer
{
    public class NewFileNotifier
    {
        FileSystemWatcher _FileWatcher = null;
        string _CaptureFilesPath = string.Empty;

        public NewFileNotifier() { }

        public NewFileNotifier(string captureFilesPath)
        {
            this._CaptureFilesPath = captureFilesPath;
        }

        public BindingList<CurrentCaptureFile> CheckForNewFiles(string inputFilePath, string outputFilePath)
        {
            BindingList<CurrentCaptureFile> currentCaptureFiles = new BindingList<CurrentCaptureFile>();

            FileInfo[] fi = null;

            string currentCaptureFileName = string.Empty;
            
            // Process the file
            ProcessCapturePackets pcp = new ProcessCapturePackets();

            try
            {
                if (inputFilePath != "")
                {
                    DirectoryInfo di = new DirectoryInfo(inputFilePath);
                    fi = di.GetFiles();

                    foreach (var file in fi)
                    {
                        // Why are we calling the following method?  We are not doing anything with it...
                        pcp.ProcessPacketFile(file.Name);

                        if (currentCaptureFileName != file.Name)
                        {
                            CurrentCaptureFile ccf = new CurrentCaptureFile();
                            ccf = pcp.GetCurrentCaptureFile(file.Name);
                            currentCaptureFiles.Add(ccf);
                        }

                        // Move the file to the done folder
                        File.Move(inputFilePath + "\\" + file.Name, outputFilePath + "\\" + file.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                // Possible conflict with parse files service - ignore this error for now - we'll try 
                // again in a few seconds
            }

            return currentCaptureFiles;
        }
       
    }
}
