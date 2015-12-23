using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COWE.CaptureAgent
{
    public class CaptureAgent
    {
        #region Constructors
        public CaptureAgent() { }

        #endregion
        #region Global Variables
        //string _CurrentCaptureFileName = string.Empty;
        //string _CaptureFolderPath = @"C:\temp";
        //string _ParseFolderPath = @"C:\temp\ParseCaptureFiles";
        #endregion
        #region Public Methods
        //public bool StartCaptureSession(string sourceHostIp, string destinationHostIp, int captureInterval, string capturePath, string captureFileName, string captureInterface, out int pid)
        public bool StartCaptureSession(string sourceHostIp, string localHostIp, int captureInterval, string capturePath, string captureFileName, string captureInterface, out int pid)
        {
            // Start a capture session
            bool success = false;

            // Create a file to hold the captured data
            //string fileName = string.Format("CaptureFile{0}{1}.pcap", DateTime.Now.Ticks.ToString(), marked ? "d" : "u");

            // Start the capture - don't show the command window
            // Using Wireshark Dumpcap here...
            // Dumpcap arguments:
            // -i 3 : use the third interface in the interface list
            // -a duration:value : stop capturing after 'value' seconds have elapsed 
            // -f : capture filter (e.g., 'src host', 'dst host')
            // -P : force file output to be written in '.pcap' format
            // -w : write to path\file
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = @"C:\\Program Files\\Wireshark\\dumpcap.exe";
            StringBuilder sb = new StringBuilder();
            //sb.AppendFormat(@" -P -i 3 -f ""src host {0} && dst host {1}"" -a duration:{2} -w {3}\\{4}", sourceHostIp, destinationHostIp, captureInterval, capturePath, captureFileName);
            //sb.AppendFormat(@" -P -i {0} -f ""src host {1} && dst host {2}"" -a duration:{3} -w {4}\\{5}", captureInterface, sourceHostIp, destinationHostIp, captureInterval, capturePath, captureFileName);
            sb.AppendFormat(@" -P -i {0} -f ""src host {1} && dst host {2}"" -a duration:{3} -w {4}\\{5}", captureInterface, sourceHostIp, localHostIp, captureInterval, capturePath, captureFileName);
            string cmdArguments = sb.ToString();
            startInfo.Arguments = cmdArguments;
            process.StartInfo = startInfo;
            process.Start();
            pid = process.Id;

            // Verify that the process started - check for the file
            Thread.Sleep(2000);
            if (File.Exists(capturePath + "\\" + captureFileName))
            {
                success = true;
            }

            return success;
        }
        
        public bool MoveCaptureFileToParseFolder(string captureFolderPath, string parseFolderPath, string captureFileName)
        {
            bool success = false;

            // Move the capture file to the parser folder - 

            // Verify the folder exists...
            
            if(!Directory.Exists(parseFolderPath))
            {
                Directory.CreateDirectory(parseFolderPath);
            }

            // Move the capture file
            if(File.Exists(captureFolderPath + "\\" + captureFileName))
            {
                File.Move(captureFolderPath + "\\" + captureFileName, parseFolderPath + "\\" + captureFileName);
            }
            // Is successful, the parser service will parse it and add it to the database
            success = true;

            return success;
        }
        #endregion

    }
}
