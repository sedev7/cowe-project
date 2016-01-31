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

using COWE.Enumerations;
using COWE.ParseFile;

namespace COWE.ParseCaptureFilesService
{
    public partial class ParseCaptureFilesService : ServiceBase
    {
        #region Global Variables
        // Global variables
        bool IncludeParsedFileLog = false;      // Write out log file for each parsed file
        bool IsOnStart = false;                 // Executing the OnStart method
        bool runSrvc = true;

        LogLevel _LogLevel = LogLevel.INFO;

        string DbConnectionString = string.Empty;
        string ErrorFilesDir = string.Empty;
        string InputDir = string.Empty;
        string LogDir = string.Empty;
        string LogFileName = string.Empty;
        string ParsedFilesDir = string.Empty;
        string serviceName = string.Empty;

        Thread workerThread;
        #endregion

        #region Event Handlers
        protected void FileSystemEventHandler_OnChanged(object sender, FileSystemEventArgs e)
        {
            if (_LogLevel == LogLevel.DEBUG)
            {
                WriteLog("In [FileSystemEventHandler_OnChanged]...sleeping 2 seconds; file name [" + e.Name + "]; full path [" + e.FullPath + "]");
            }
            // Wait two seconds - allow time for process copying files into the InputDir to release their file handle
            Thread.Sleep(2000);

            if (!ProcessFile(e.Name, e.FullPath))
            {
                string errMessage = "In [FileSystemEventHandler_OnChanged]...An error occurred processing the file: " + e.FullPath.ToString();
                WriteLog(errMessage);
            }
        }
        #endregion

        #region Public Methods
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
                ErrorFilesDir = nvc["ERROR_FILES_DIR"];
                InputDir = nvc["INPUT_FILE_DIR"];
                LogDir = nvc["LOG_FILE_DIR"];
                ParsedFilesDir = nvc["PARSED_FILES_DIR"];
                // Validate value passed in
                string includeParsedFileLog = nvc["PARSED_FILES_LOG"];
                if (includeParsedFileLog.Trim() == "true" || includeParsedFileLog.Trim() == "True" || includeParsedFileLog.Trim() == "TRUE")
                {
                    IncludeParsedFileLog = true;
                }
                DbConnectionString = nvc["DB_CONN_STR"];
                switch(nvc["LOG_LEVEL"])
                {
                    case "info":
                        _LogLevel = LogLevel.INFO;
                        break;
                    case "warn":
                        _LogLevel = LogLevel.WARN;
                        break;
                    case "error":
                        _LogLevel = LogLevel.ERROR;
                        break;
                    case "debug":
                        _LogLevel = LogLevel.DEBUG;
                        break;
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Error reading app.config file: " + ex.Message);
            }

            try
            {
                // Set up the log file
                // First verify that the directory exists; create it if not 
                if (!Directory.Exists(LogDir))
                {
                    Directory.CreateDirectory(LogDir);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Error creating log directory [ " + LogDir + " ]: " + ex.Message);
            }
            try
            {
                // Set up the error files directory
                // First verify that the directory exists; create it if not 
                if (!Directory.Exists(ErrorFilesDir))
                {
                    Directory.CreateDirectory(ErrorFilesDir);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Error creating Error Files directory [ " + ErrorFilesDir + " ]: " + ex.Message);
            }
            try
            {
                // Set up the input directory
                // First verify that the directory exists; create it if not 
                if (!Directory.Exists(InputDir))
                {
                    Directory.CreateDirectory(InputDir);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Error creating file input directory [ " + InputDir + " ]: " + ex.Message);
            }
            try
            {
                // Set up the parsed files directory (i.e., "done" folder)
                // First verify that the directory exists; create it if not 
                if (!Directory.Exists(ParsedFilesDir))
                {
                    Directory.CreateDirectory(ParsedFilesDir);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Error creating log directory [ " + ParsedFilesDir + " ]: " + ex.Message);
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
                    EventLog.WriteEntry("Error occurred in Run Service: " + ServiceName + " " + ex.Message);
                    if(_LogLevel == LogLevel.DEBUG)
                    {
                        WriteLog("Error occurred in Run Service: " + ServiceName + " " + ex.Message);
                    }
                }
            }
        }
        public bool CheckForExistingFiles(string InputDir)
        {
            if (_LogLevel == LogLevel.DEBUG)
            {
                WriteLog("In [CheckForExistingFiles]...");
            }

            DirectoryInfo di = new DirectoryInfo(InputDir);
            FileInfo[] fiArr = di.GetFiles();
            if (fiArr.Length == 0)
            {
                if (_LogLevel == LogLevel.DEBUG)
                {
                    WriteLog("In [CheckForExistingFiles]... no files found.");
                }
                return false;
            }
            else
            {
                if (_LogLevel == LogLevel.DEBUG)
                {
                    WriteLog("In [CheckForExistingFiles]... found " + fiArr.Length + " files.");
                }
                foreach (FileInfo f in fiArr)
                {
                    //ParseFile.ParseFile pf = new ParseFile.ParseFile(DbConnectionString);
                    //pf.ParsedFilesDir = this.ParsedFilesDir;
                    //WriteLog("In [CheckForExistingFiles]... parsing capture file: " + f.Name.ToString());

                    //if (!pf.ParseCaptureFile(f.FullName.ToString(), f.Name.ToString()))
                    //{
                    //    string errMessage = "In [CheckForExistingFiles]... an error occurred while parsing the capture file: " + f.FullName.ToString();
                    //    WriteLog(errMessage);
                    //}

                    // If the service is starting and there are existing capture files [from a previous session] 
                    // delete them; otherwise the files must be from this session so process them
                    if (IsOnStart)
                    {
                        if (_LogLevel == LogLevel.DEBUG)
                        {
                            WriteLog("In [CheckForExistingFiles]... found old file [" + f.FullName + "] - deleting it.");
                        }
                        // Delete any existing files
                        if (File.Exists(f.FullName))
                        {
                            File.Delete(f.FullName);
                            // Write message to the log file
                            string deleteMessage = "In [CheckForExistingFiles]... Deleted existing file: " + f.FullName;
                            WriteLog(deleteMessage);
                        }
                    }
                    else
                    {
                        if (ProcessFile(f.Name, f.FullName))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region Public Static Methods
        // Open the log file - creates the file if it doesn't already exist
        // FileStream object reads/writes bytes
        public static FileStream logStr = new FileStream(ConfigurationManager.AppSettings["LOG_FILE_DIR"].ToString() + "\\ParseCaptureServiceLog_" + DateTime.Now.Ticks.ToString() + ".txt", FileMode.Append);
        // Wrap FileStream object in StreamWriter which encodes bytes as characters
        StreamWriter logWriter = new StreamWriter(logStr);  
        #endregion

        #region Protected Methods
        protected override void OnStart(string[] args)
        {
            IsOnStart = true;

            EventLog.WriteEntry(ServiceName + "starting up...");

            runSrvc = true;
            if ((workerThread == null) || ((workerThread.ThreadState &
                 (System.Threading.ThreadState.Unstarted | System.Threading.ThreadState.Stopped)) != 0))
            {
                EventLog.WriteEntry(serviceName + "... OnStart", DateTime.Now.ToLongTimeString() + " - Starting the service worker thread.");
                WriteLog(serviceName + "... OnStart - Starting the service worker thread.");

                workerThread = new Thread(new ThreadStart(RunService)); //ServiceWorkerMethod
                workerThread.Start();
            }
            if (workerThread != null)
            {
                EventLog.WriteEntry(serviceName + " ... OnStart", DateTime.Now.ToLongTimeString() + " - Worker thread state = " + workerThread.ThreadState.ToString());
                WriteLog(serviceName + " ... OnStart - Worker thread state = " + workerThread.ThreadState.ToString());
            }

            // Check for existing files
            if (!CheckForExistingFiles(InputDir))
            {
                //Console.WriteLine("No files found");
                EventLog.WriteEntry("ParseCaptureFilesService [OnStarte] - no files found in input directory [ " + InputDir + " ]");
                WriteLog("In [OnStart] - no files found in input directory [ " + InputDir + " ]");
            }

            // Start the FileSystemWatcher instance
            //FileSystemWatcher fsw = new FileSystemWatcher();
            _FileSystemWatcher.Path = InputDir;
            _FileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            _FileSystemWatcher.Created += new FileSystemEventHandler(FileSystemEventHandler_OnChanged);
            //_FileSystemWatcher.Changed += new FileSystemEventHandler(FileSystemEventHandler_OnChanged);

            IsOnStart = false;
        }
        protected override void OnStop()
        {
            EventLog.WriteEntry(ServiceName + " stopping");
            WriteLog(serviceName + "... OnStop - stopping the service.");

            // Stop the service
            runSrvc = false;

            //EventLog.WriteEntry(serviceName + "... in OnStop() - aborting thread ...");
            
            // make a log entry
            logWriter.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " Stopping " + serviceName);
            logWriter.Flush();
            logWriter.Close();

            //// kill the running thread
            //workerThread.Abort();

            Dispose();
        }
        protected bool ProcessFile(string fileName, string fullPath)
        {
            ParseFile.ParseFile pf = new ParseFile.ParseFile(DbConnectionString);
            pf.ParsedFilesDir = this.ParsedFilesDir;
            WriteLog("In [ProcessFile]... parsing capture file: " + fullPath);

            try
            {
                if (pf.ParseCaptureFile(fullPath, fileName, IncludeParsedFileLog))
                {
                    WriteLog("In [ProcessFile]... moving file to done folder: " + ParsedFilesDir);
                    // Move the file to the done folder
                    MoveFile(fullPath, ParsedFilesDir + "\\" + fileName);
                    return true;
                }
                else
                {
                    // Move the file to the error files folder
                    MoveFile(fullPath, ErrorFilesDir + "\\" + fileName);
                    string errMessage = "In [ProcessFile]... an error occurred while parsing the capture file: " + fullPath;
                    WriteLog(errMessage);
                    return false;
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ServiceName + " - In [ProcessFile]...error parsing capture file [" + fileName + "]: " + ex.Message);
                WriteLog("In [ProcessFile]...error parsing capture file [" + fileName + "]: " + ex.Message );
                return false;
            }
            
        }
        protected void WriteLog(string msg)
        {
            logWriter.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + msg);
            logWriter.Flush();
        }
        protected void MoveFile(string sourcePath, string destinationPath)
        {
            // Move a file from the source path to the destination path
            try
            {
                // Move the file
                // If the file already exists, delete it
                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                    // Write message to the log file
                    string deleteMessage = "Deleted existing file in destination path: " + destinationPath.ToString();
                    WriteLog(deleteMessage);
                }
                System.IO.Directory.Move(sourcePath, destinationPath);

                // Write success message to the log file
                string logMessage = "File successfully moved to " + destinationPath.ToString();
                WriteLog(logMessage);
            }
            // if error(s), write the reason to the log file
            catch (ArgumentNullException)
            {
                string logMessage = "Path is a null reference.";
                WriteLog(logMessage);
            }
            catch (System.Security.SecurityException)
            {
                string logMessage = "The caller does not have the " +
                    "required permission.";
                WriteLog(logMessage);
            }
            catch (ArgumentException)
            {
                string logMessage = "Path is an empty string, " +
                    "contains only white spaces, " +
                    "or contains invalid characters.";
                WriteLog(logMessage);
            }
            catch (System.IO.IOException)
            {
                string logMessage = "An attempt was made to move a " +
                    "directory to a different " +
                    "volume, or destDirName " +
                    "already exists.";
                WriteLog(logMessage);
            }
        }
        #endregion
    }
}

