/* ParseCaptureFilesService
 * 
 * Monitors a specified path/folder for incoming packet capture files, parses the files,
 * and adds them to a database.
 * 
 * Author: James A. Savage
 * Thesis/research project - CISE program, College of Engineering, Department of Electrical
 * Engineering, Tennessee State University.
 * 
 * version 001 05-02-2015
 * 
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using COWE.ParseFile;

namespace COWE.ParseCaptureFilesService
{
    public partial class ParseCaptureFilesService : ServiceBase
    {
        // Global variables
        bool runSrvc = true;

        string DbConnectionString = string.Empty;
        string InputDir = string.Empty;
        string serviceName = string.Empty;

        Thread workerThread;

        public ParseCaptureFilesService()
        {
            InitializeComponent();

            // Set the name of the service
            this.ServiceName = "Parse Capture Files Service";
            serviceName = this.ServiceName;

            // Configure the service level of control
            CanStop = true;

            // Configure application logging
            AutoLog = true;

            // Load values from the application configuration file
            try
            {
                NameValueCollection nvc = ConfigurationManager.AppSettings;
                InputDir = nvc["INPUT_DIR"];
                DbConnectionString = nvc["DB_CONN_STR"];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading app.config file: " + ex.Message);
            }
        }

        public void RunService()
        {
            while (runSrvc)
            {
                try
                {
                    // Sleep for 2 seconds
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred in Run Service: " + ServiceName + ex.Message);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(ServiceName + "starting up...");

            runSrvc = true;
            if ((workerThread == null) || ((workerThread.ThreadState &
                 (System.Threading.ThreadState.Unstarted | System.Threading.ThreadState.Stopped)) != 0))
            {
                EventLog.WriteEntry(serviceName + "... OnStart", DateTime.Now.ToLongTimeString() +
                    " - Starting the service worker thread.");

                workerThread = new Thread(new ThreadStart(RunService)); //ServiceWorkerMethod
                workerThread.Start();
            }
            if (workerThread != null)
            {
                EventLog.WriteEntry(serviceName + " ... OnStart", DateTime.Now.ToLongTimeString() +
                    " - Worker thread state = " + workerThread.ThreadState.ToString());
            }

            // Check for existing files
            if(!CheckForExistingFiles(InputDir))
            {
                Console.WriteLine("No files found");
            }
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry(ServiceName + "stopping");
        }

        //protected bool CheckForExistingFiles()
        public bool CheckForExistingFiles(string InputDir)
        {
            DirectoryInfo di = new DirectoryInfo(InputDir);
            FileInfo[] fiArr = di.GetFiles();
            if (fiArr.Length == 0)
                return false;
            else
            {
                foreach (FileInfo f in fiArr)
                {
                    ParseFile.ParseFile pf = new ParseFile.ParseFile(DbConnectionString);

                    if (!pf.ParseCaptureFile(f.FullName.ToString(), f.Name.ToString()))
                    {
                        string errMessage = "An error occurred while parsing the capture file: " + f.FullName.ToString();
                        Console.WriteLine(errMessage);
                    }
                }
            }
            return true;
        }
    }
}

