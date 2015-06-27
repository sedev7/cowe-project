#region Application Information
/*
 * COWE-Client Solution
 * 
 * Client application used to connect to, and control, flooder application.
 * 
 * J. Savage  03-08-2015
 * 
 * v3:
 *  - Clean out old code
 *  - Add timer flooder for run-time.
 *  
 * TO DO:
 *  - Get the client ip address to use for packet capture
 *  x Reset UI when socket connection fails (i.e., throws exception).
 *  x Add Flooder class.
 *  x On start/stop flooder, check flooders list for existence before adding to list.
 * 
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Threading;

using COWE.CaptureAgent;

namespace COWE.Client
{
    public partial class Client : Form
    {
        #region Global Variables

        bool IsFlooding = false;        // True while a flooder is running
        bool IsMarked = false;          // True when a flooder is sending packets

        int _PID = 1;                   // Process Id for flooder
        int _MaxGridDisplayRows = 8;    // Maximum number of rows to display in grid
        int _MaxGridHeight = 0;         // Height of grid when maximum number of rows are displayed
        int _SelectedRow = 0;           // Row index of currently selected grid row
        int _FlooderTimerInterval = 0;  // Number of seconds to run flooder

        BindingList<Flooder> _Flooders = new BindingList<Flooder>();

        // Form colors
        Color NotConnected = Color.LightCoral;
        Color NonResident = Color.Khaki;
        Color CoResident = Color.LightGreen;

        DataGridView _FlooderStatusDataGridView = new DataGridView();

        private DispatcherTimer timer = new DispatcherTimer();
        private Stopwatch stopWatch = new Stopwatch();

        string currentTime = string.Empty;
        string _HostIpAddress = string.Empty;
        string _CaptureFolderPath = @"C:\temp";
        string _ParseFolderPath = @"C:\temp\ParseCaptureFiles";
        string _CurrentCaptureFileName = string.Empty;

        private BackgroundWorker bgWorker;

        //System.Timers.Timer _FlooderIntervalTimer = new System.Timers.Timer();
        //_FlooderIntervalTimer.Elapsed += new ElapsedEventHandler(OnTimedFlooderInterval);

        //public delegate void TimerElapsedEventHandler(object sender, ElapsedEventArgs);
        private static System.Timers.Timer _FlooderIntervalTimer;

        #endregion

        #region Public Methods
        public Client()
        {
            InitializeComponent();
            InitializeForm();
            InitializeFlooderStatusDataGridViewControl();

            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 1);

            this.bgWorker = new BackgroundWorker();
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            InitializeBackgroundWorkerTheads();
            InitializeProgressSpinner();
            InitializeProgressLabel();
            this.ClockButton.Visible = false;
        }
        #endregion

        #region Event Handlers
        private void AddFlooderButton_Click(object sender, EventArgs e)
        {
            int pid = _PID++;

            _FlooderStatusDataGridView.Rows.Add();

            int row = _FlooderStatusDataGridView.Rows.Count - 1;
            int col = 1;

            // Resize the row height to match the other rows
            _FlooderStatusDataGridView.Rows[row].Height = 30;
     
            // Expand the grid to accommodate the new row
            ResizeFlooderGrid();

            _FlooderStatusDataGridView.Rows[row].Cells[col].Value = pid.ToString();                // PID
            _FlooderStatusDataGridView.Rows[row].Cells["FlooderRunTime"].Value = "0";              // Elapsed run-time
            _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Value = "Not Connected";   // Status
            _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Style.BackColor = NotConnected;
        }
        private void DeleteFlooderButton_Click(object sender, EventArgs e)
        {
            int rowsToDelete = 0;

            foreach (DataGridViewRow row in _FlooderStatusDataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["SelectFlooder"].Value))
                    rowsToDelete++;
            }

            if (rowsToDelete > 0)
            {
                DialogResult dr = MessageBox.Show("You are about to delete " + rowsToDelete + " flooder(s) - are you sure?", "Delete Flooder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    // Delete flooder rows
                    foreach (DataGridViewRow row in _FlooderStatusDataGridView.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["SelectFlooder"].Value))
                            _FlooderStatusDataGridView.Rows.Remove(row);
                    }

                    ResizeFlooderGrid();
                }
            }
            else
            {
                MessageBox.Show("No flooder rows have been selected!", "Delete Flooder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FlooderStatusDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                bool IsStarting = false;
                int col = e.ColumnIndex;
                int row = e.RowIndex;
                _SelectedRow = e.RowIndex;

                // Verify that flooder data has been entered
                if(_FlooderStatusDataGridView.Rows[row].Cells["FlooderIpAddress"].Value == null)
                {
                    MessageBox.Show("Flooder IP address required", "Flooder IP Address");
                    _FlooderStatusDataGridView.Rows[row].Cells["FlooderIpAddress"].Selected = true;
                }
                else if(_FlooderStatusDataGridView.Rows[row].Cells["FlooderPort"].Value == null)
                {
                    MessageBox.Show("Flooder port number required", "Flooder Port Number");
                    _FlooderStatusDataGridView.Rows[row].Cells["FlooderPort"].Selected = true;
                }
                else if(_FlooderStatusDataGridView.Rows[row].Cells["FlooderDestination"].Value == null)
                {
                    MessageBox.Show("Flooder destination IP address required", "Flooder Destination IP Address");
                    _FlooderStatusDataGridView.Rows[row].Cells["FlooderDestination"].Selected = true;
                }
                else
                {
                    // Get the process ID
                    int flooderPid = Convert.ToInt32(_FlooderStatusDataGridView.Rows[row].Cells["PID"].Value.ToString());

                    // Get the flooder connection information
                    string flooderIpAddress = _FlooderStatusDataGridView.Rows[row].Cells["FlooderIpAddress"].Value.ToString();
                    int flooderConnectionPortNumber = Convert.ToInt32(_FlooderStatusDataGridView.Rows[row].Cells["FlooderPort"].Value.ToString());
                    string flooderDestinationIpAddress = _FlooderStatusDataGridView.Rows[row].Cells["FlooderDestination"].Value.ToString();

                    // Hardcode flooder source and destination ports 
                    int flooderSourcePortNumber = 403;
                    int flooderDestinationPortNumber = 80;

                    // Convert the IP Address string into a byte array
                    string[] sourceIpAddress = flooderIpAddress.Split(new char[1] { '.' });
                    byte[] remoteHostIpAddrByteArr = new byte[4];

                    // Parse the flooder source IP address
                    if (flooderIpAddress != "")
                    {
                        if (sourceIpAddress.Length != 4)
                        {
                            MessageBox.Show("Error parsing Flooder IP Address: " + flooderIpAddress + "Please check the Flooder IP Address.", "Parse Remote Host IP Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                remoteHostIpAddrByteArr[i] = byte.Parse(sourceIpAddress[i]);
                            }
                        }
                    }

                    // Assign the flooder IP address
                    IPAddress connectionIp = new IPAddress(remoteHostIpAddrByteArr);

                    Flooder flooder = new COWE.Client.Flooder(flooderPid, connectionIp, flooderConnectionPortNumber, flooderIpAddress, flooderSourcePortNumber, flooderDestinationIpAddress, flooderDestinationPortNumber);

                    // Add the flooder to the _Flooders collection if it isn't already a member
                    bool IsMember = false;
                    foreach (Flooder f in _Flooders)
                    {
                        if (flooder.Pid == f.Pid) { IsMember = true; }
                    }
                    if (!IsMember) { _Flooders.Add(flooder); }

                    var cellButton = (DataGridViewButtonCell)senderGrid[col, row];

                    if (cellButton.Value.ToString() == "Start")
                    {
                        IsStarting = true;

                        // Get the timer interval (start/stop interval for flooder)
                        _FlooderTimerInterval = Convert.ToInt32(FlooderIntervalTextBox.Text);

                        bool success = false;
                        // Open the socket connection to the flooder
                        if (OpenFlooderConnection(IsStarting, flooder, _FlooderTimerInterval))
                        {
                            cellButton.UseColumnTextForButtonValue = false;
                            cellButton.Value = "Stop";
                            cellButton.Style.BackColor = System.Drawing.Color.Red;

                            // Update the status column for this row (i.e., this flooder instance)
                            _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Value = "Running";
                            _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            _FlooderStatusDataGridView["FlooderStatus", row].Style.BackColor = NonResident;
                            success = true;
                            IsFlooding = true;
                            IsMarked = true;
                        }
                        else
                        {
                            MessageBox.Show("Unable to start flooder", "Start Flooder");
                        }

                        if (success)
                        {
                            // Start the packet capture

                            // Get our local IP address
                            string host = Dns.GetHostName();
                            IPHostEntry ip = Dns.GetHostEntry(host);
                            //string hostIpAddress = ip.AddressList[3].ToString();
                            _HostIpAddress = ip.AddressList[3].ToString();

                            // Note: source host is web server (target), destination host is client
                            //if(StartPacketCapture(TargetIpAddressTextBox.Text.Trim(), "10.10.10.136", timerInterval))
                            if (StartPacketCapture(TargetIpAddressTextBox.Text.Trim(), _HostIpAddress, _FlooderTimerInterval))
                            {
                                // Packet capture started successfully
                                this.ProgressSpinnerPictureBox.Visible = true;
                                this.ProgressLabel.Text = "Capturing marked packet data...";
                                this.ProgressLabel.Visible = true;

                                // Start the timer (ms increments)
                                _FlooderIntervalTimer = new System.Timers.Timer(_FlooderTimerInterval * 1000);
                                _FlooderIntervalTimer.Elapsed += new ElapsedEventHandler(OnFlooderTimerElapsedEvent);
                                _FlooderIntervalTimer.Start();
                                StopWatchStart();

                                //// Start the timer with a backgroundworker thread
                                //bgWorker.RunWorkerAsync();
                            }
                        }
                    }
                    else
                    {
                        // Stop flooder
                        IsStarting = false;
                        cellButton.UseColumnTextForButtonValue = false;
                        cellButton.Value = "Start";
                        cellButton.FlatStyle = FlatStyle.Standard;
                        cellButton.Style.BackColor = System.Drawing.SystemColors.Control;

                        _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Value = "Not Connected";
                        _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Style.BackColor = NotConnected;
                        _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        // Note - original cell color name was "0" or "Empty"
                        //_FlooderStatusDataGridView["FlooderStatus", row].Style.BackColor = System.Drawing.Color.Empty;
                        _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Style.BackColor = NotConnected;

                        // Open the socket connection to the flooder
                        if (OpenFlooderConnection(IsStarting, flooder, 0))
                        {
                            IsFlooding = false;

                            // Stop the packet capture and reset the capture message
                            this.ProgressSpinnerPictureBox.Visible = false;
                            this.ProgressLabel.Text = "";
                            this.ProgressLabel.Visible = false;

                            _FlooderIntervalTimer.Stop();
                            StopWatchStop();
                        }
                        else
                        {
                            MessageBox.Show("Unable to stop flooder", "Stop Flooder");
                        }

                        // Move the last packet capture file
                        MovePacketCaptureFile(_CurrentCaptureFileName);
                    }
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                //currentTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                currentTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
                clockLabel.Text = currentTime;
                //Application.DoEvents();
                _FlooderStatusDataGridView.Rows[_SelectedRow].Cells["FlooderRunTime"].Value = currentTime;
                //Application.DoEvents();
            }
        }
        private void ClockButton_Click(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                timer.Stop();
                ClockButton.Text = "Start Clock";
            }
            else
            {
                // Reset the elapsed time
                stopWatch.Reset();
                stopWatch.Start();
                timer.Start();
                ClockButton.Text = "Stop Clock";
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            if(bw.CancellationPending)
            {
                e.Cancel = true;
            }

            // Start the timer (ms increments)
            _FlooderIntervalTimer = new System.Timers.Timer(_FlooderTimerInterval * 1000);
            _FlooderIntervalTimer.Elapsed += new ElapsedEventHandler(OnFlooderTimerElapsedEvent);
            _FlooderIntervalTimer.Start();

            // Need keep alive to prevent garbage collection of timer
            GC.KeepAlive(_FlooderIntervalTimer);
            bgWorker.ReportProgress(0);
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Operation cancelled", "Background Worker - RunWorker Completed");
            }
            else if (e.Error != null)
            {
                string msg = String.Format("An error occurred : {0}", e.Error.Message);
                MessageBox.Show(msg, "Background Worker - RunWorker Completed");
            }
            else
            {
                // The operation completed normally.

            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the elapsed time using a callback
            //DisplayElapsedTime(e.ToString());

            // Start a new packet capture session
            StartPacketCapture(TargetIpAddressTextBox.Text.Trim(), _HostIpAddress, _FlooderTimerInterval);

            // Update the packet capture display
            if (IsFlooding)
            {
                if (IsMarked)
                {
                    this.ProgressLabel.Text = "Capturing unmarked packet data...";
                    IsMarked = false;
                    StartPacketCapture(TargetIpAddressTextBox.Text.Trim(), _HostIpAddress, _FlooderTimerInterval);
                }
                else
                {
                    this.ProgressLabel.Text = "Capturing marked packet data...";
                    IsMarked = true;
                }
            }
        }

        //// Delegate and function to populate elapsed time
        //delegate void DisplayElapsedTimeCallback(string et);

        // Delegate to update capture progress
        delegate void DisplayCaptureProgressCallback(Label l);

        // Method to update the capture progress label
        private void DisplayCaptureProgress(Label l)
        {
            if (this.ProgressLabel.InvokeRequired)
            {
                // Use try-catch block to avoid cross-threading exception while debugging
                try
                {
                    DisplayCaptureProgressCallback d = new DisplayCaptureProgressCallback(DisplayCaptureProgress);
                    this.Invoke(d, new object[] { l });
                }
                catch (InvalidOperationException ioe)
                { }
            }
            else
                try
                {
                    string progress = string.Format("Capturing{0}marked packet data...", IsMarked ? " " : " un");
                    this.ProgressLabel.Text = progress;
                    IsMarked = IsMarked ? false : true;
                }
                catch (InvalidOperationException ioe)
                { }
        }
        private void OnFlooderTimerElapsedEvent(object sender, ElapsedEventArgs e)
        {
            // Capture the name of the current capture file
            string currentCaptureFileName = _CurrentCaptureFileName;

            // Start the next packet capture file
            if (IsFlooding)
            {
                if (IsMarked)
                {
                    if(this.ProgressLabel.InvokeRequired)
                    {
                        BeginInvoke(new Action(() => this.ProgressLabel.Text = "Capturing unmarked packet data..."), null);
                    }
                    IsMarked = false;
                    StartPacketCapture(TargetIpAddressTextBox.Text.Trim(), _HostIpAddress, _FlooderTimerInterval);
                }
                else
                {
                    if (this.ProgressLabel.InvokeRequired)
                    {
                        BeginInvoke(new Action(() => this.ProgressLabel.Text = "Capturing marked packet data..."), null);
                    }
                    IsMarked = true;
                    StartPacketCapture(TargetIpAddressTextBox.Text.Trim(), _HostIpAddress, _FlooderTimerInterval);
                }
            }

            // Move the current packet capture file to a folder where it will be parsed
            // Do this at the end of the call so that the file can be closed properly
            MovePacketCaptureFile(currentCaptureFileName);
            //bgWorker.ReportProgress(0);
        }

        #endregion

        #region Private Methods
        private void AddFlooderRow(int pid)
        {
            int col = 0; // Start with PID
            int row = _FlooderStatusDataGridView.Rows.Count; // Next row

            // Create a check box column
            DataGridViewCheckBoxColumn flooderSelectCheckBox = new DataGridViewCheckBoxColumn();
            flooderSelectCheckBox.ValueType = typeof(bool);
            flooderSelectCheckBox.HeaderText = "";
            flooderSelectCheckBox.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderSelectCheckBox.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderSelectCheckBox.Width = 30;
            flooderSelectCheckBox.ReadOnly = false;
            flooderSelectCheckBox.Name = "SelectFlooder";
            // Add the check box column to the grid
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderSelectCheckBox);    // Select checkbox

            DataGridViewTextBoxColumn flooderPid = new DataGridViewTextBoxColumn();
            flooderPid.Name = "PID";
            flooderPid.HeaderText = "PID";
            flooderPid.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderPid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderPid.Width = 60;
            flooderPid.ReadOnly = true;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderPid);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderIpAddress = new DataGridViewTextBoxColumn();
            flooderIpAddress.Name = "FlooderIpAddress";
            flooderIpAddress.HeaderText = "Flooder";
            flooderIpAddress.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderIpAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderIpAddress.Width = 150;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderIpAddress);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderPort = new DataGridViewTextBoxColumn();
            flooderPort.Name = "FlooderPort";
            flooderPort.HeaderText = "Port";
            flooderPort.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderPort.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderPort.Width = 80;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderPort);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderTargetIpAddress = new DataGridViewTextBoxColumn();
            flooderTargetIpAddress.Name = "FlooderDestination";
            flooderTargetIpAddress.HeaderText = "Destination";
            flooderTargetIpAddress.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderTargetIpAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderTargetIpAddress.Width = 150;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderTargetIpAddress);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderRunTime = new DataGridViewTextBoxColumn();
            flooderRunTime.Name = "FlooderRunTime";
            flooderRunTime.HeaderText = "Run Time (sec)";
            flooderRunTime.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderRunTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderRunTime.Width = 140;
            flooderRunTime.ReadOnly = true;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderRunTime);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderStatus = new DataGridViewTextBoxColumn();
            flooderStatus.Name = "FlooderStatus";
            flooderStatus.HeaderText = "Status";
            flooderStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderStatus.Width = 130;
            flooderStatus.ReadOnly = true;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderStatus);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Create a button for flooder control
            DataGridViewButtonColumn flooderControlButton = new DataGridViewButtonColumn();
            flooderControlButton.Name = "FlooderControl";
            flooderControlButton.HeaderText = "";
            flooderControlButton.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderControlButton.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderControlButton.Text = "Start";
            flooderControlButton.UseColumnTextForButtonValue = true;
            // Add the button column to the grid
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderControlButton);   // Flooder control button

            // Initialize the row
            DataGridViewRow flooderGridRow = new DataGridViewRow();
            //flooderGridRow.Height = 100;
            _FlooderStatusDataGridView.Rows.Add();
            _FlooderStatusDataGridView.Rows[row].Height = 30;

            ResizeFlooderGrid();
        }
        private void InitializeFlooderStatusDataGridViewControl()
        {
            int pid = _PID++;

            _FlooderStatusDataGridView = FlooderStatusDataGridView;
            _FlooderStatusDataGridView.Columns.Clear();
            // Allow user to select a row and change data
            _FlooderStatusDataGridView.ReadOnly = false;
            //_FlooderStatusDataGridView.AllowUserToAddRows = true;
            //_FlooderStatusDataGridView.AllowUserToDeleteRows = true;

             //AddFlooderRow(pid);
            int col = 0; // Start with PID
            int row = 0;// Next row

            // Create a check box column
            DataGridViewCheckBoxColumn flooderSelectCheckBox = new DataGridViewCheckBoxColumn();
            flooderSelectCheckBox.ValueType = typeof(bool);
            flooderSelectCheckBox.HeaderText = "";
            flooderSelectCheckBox.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderSelectCheckBox.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderSelectCheckBox.Width = 30;
            flooderSelectCheckBox.ReadOnly = false;
            flooderSelectCheckBox.Name = "SelectFlooder";
            // Add the check box column to the grid
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderSelectCheckBox);    // Select checkbox

            DataGridViewTextBoxColumn flooderPid = new DataGridViewTextBoxColumn();
            flooderPid.Name = "PID";
            flooderPid.HeaderText = "PID";
            flooderPid.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderPid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderPid.Width = 60;
            flooderPid.ReadOnly = true;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderPid);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderIpAddress = new DataGridViewTextBoxColumn();
            flooderIpAddress.Name = "FlooderIpAddress";
            flooderIpAddress.HeaderText = "Flooder";
            flooderIpAddress.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderIpAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderIpAddress.Width = 150;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderIpAddress);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderPort = new DataGridViewTextBoxColumn();
            flooderPort.Name = "FlooderPort";
            flooderPort.HeaderText = "Port";
            flooderPort.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderPort.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderPort.Width = 80;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderPort);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderTargetIpAddress = new DataGridViewTextBoxColumn();
            flooderTargetIpAddress.Name = "FlooderDestination";
            flooderTargetIpAddress.HeaderText = "Destination";
            flooderTargetIpAddress.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderTargetIpAddress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderTargetIpAddress.Width = 150;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderTargetIpAddress);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderRunTime = new DataGridViewTextBoxColumn();
            flooderRunTime.Name = "FlooderRunTime";
            flooderRunTime.HeaderText = "Run Time (sec)";
            flooderRunTime.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderRunTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderRunTime.Width = 140;
            flooderRunTime.ReadOnly = true;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderRunTime);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn flooderStatus = new DataGridViewTextBoxColumn();
            flooderStatus.Name = "FlooderStatus";
            flooderStatus.HeaderText = "Status";
            flooderStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderStatus.Width = 130;
            flooderStatus.ReadOnly = true;
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderStatus);
            //_FlooderStatusDataGridView.Columns[col++].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Create a button for flooder control
            DataGridViewButtonColumn flooderControlButton = new DataGridViewButtonColumn();
            flooderControlButton.Name = "FlooderControl";
            flooderControlButton.HeaderText = "";
            flooderControlButton.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderControlButton.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            flooderControlButton.Text = "Start";
            flooderControlButton.UseColumnTextForButtonValue = true;
            // Add the button column to the grid
            _FlooderStatusDataGridView.Columns.Insert(col++, flooderControlButton);   // Flooder control button

            // Initialize the row
            DataGridViewRow flooderGridRow = new DataGridViewRow();
            //flooderGridRow.Height = 100;
            _FlooderStatusDataGridView.Rows.Add();
            _FlooderStatusDataGridView.Rows[row].Height = 30;

            ResizeFlooderGrid();

            // Add data to the first row
            row = _FlooderStatusDataGridView.Rows.Count - 1; // Next row
            col = 1;

            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = pid.ToString();   // PID
            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = "10.10.10.208";   // Flooder IP Address
            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = "8080";           // Flooder port
            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = "10.10.10.118";   // Flooder Destination
            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = "0";              // Runtime
            _FlooderStatusDataGridView.Rows[row].Cells[col].Value = "Not Connected";    // Status
            _FlooderStatusDataGridView.Rows[row].Cells[col].Style.BackColor = NotConnected;

            // Hide the row header column
            _FlooderStatusDataGridView.RowHeadersVisible = false;

            ResizeFlooderGrid();
        }
        private void InitializeForm()
        {
            // Set initial values
            TargetIpAddressTextBox.Text = "10.10.10.100";
            TargetPortTextBox.Text = "80";
            FlooderIntervalTextBox.Text = "20";
        }
        private void ResizeFlooderGrid()
        {
            int height = 0;
            

            foreach (DataGridViewRow row in _FlooderStatusDataGridView.Rows)
            {
                height += row.Height;
            }
            height += _FlooderStatusDataGridView.ColumnHeadersHeight;

            int width = 0;
            foreach (DataGridViewColumn col in _FlooderStatusDataGridView.Columns)
            {
                width += col.Width;
            }
            //width += _FlooderStatusDataGridView.RowHeadersWidth;

            if(_FlooderStatusDataGridView.Rows.Count == _MaxGridDisplayRows)
            {
                _MaxGridHeight = height;
            }

            if(_FlooderStatusDataGridView.Rows.Count > _MaxGridDisplayRows)
            {
                // Allow additional width for scroll bar
                _FlooderStatusDataGridView.ClientSize = new Size(width + 20, _MaxGridHeight + 4);
            }
            else
            {
                _FlooderStatusDataGridView.ClientSize = new Size(width + 3, height + 6);
            }

        }
        private bool OpenFlooderConnection(bool isStarting, Flooder flooder, int timeInterval)
        {
            bool success = false;

            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Create a TCP socket
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to a remote flooder
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(flooder.ConnectionIpAddress, flooder.ConnectionPortNumber);

                // Connect to the flooder via TCP/IP socket.
                s.Connect(remoteEP);

                string response = string.Empty;

                // Send message to flooder with time increment to run
                string msgStart = string.Empty;
                string msgStartFile = string.Empty;
                string msgEnd = "end";

                if (isStarting)
                {
                    msgStart = "start";

                    // Construct the 'start' file string
                    msgStartFile = flooder.SourceIpAddress + "|" + flooder.DestinationIpAddress + "|" + flooder.SourcePortNumber.ToString() + "|" + flooder.DestinationPortNumber.ToString() + "|" + timeInterval.ToString(); // +"\\0";
                }
                else
                {
                    msgStart = "stop";
                }

                // Encode the data string into a byte array.
                byte[] msg = Encoding.ASCII.GetBytes(msgStart + "\r\n");

                // Send the data through the socket.
                int bytesSent = s.Send(msg);
                int bytesRecvd = s.Receive(bytes);
                response = Encoding.ASCII.GetString(bytes, 0, bytesRecvd);
                if(response.Trim().Substring(0,12) == "ACK-Received")
                {
                    success = true;
                }
                else
                {
                    MessageBox.Show("Error sending message - start token not acknowledged", "Send Start Token", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (isStarting)
                {
                    //msg = Encoding.ASCII.GetBytes(msgRunTime + "\r\n");
                    msg = Encoding.ASCII.GetBytes(msgStartFile + "\r\n");
                    bytesSent = s.Send(msg);
                    // Check the flooder response for an acknowledgement
                    bytesRecvd = s.Receive(bytes);
                    response = Encoding.ASCII.GetString(bytes, 0, bytesRecvd);
                    if (response.Trim().Substring(0, 12) == "ACK-Received")
                    {
                        success = true;
                    }
                    else
                    {
                        MessageBox.Show("Error sending message - start file not acknowledged", "Send Start File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                msg = Encoding.ASCII.GetBytes(msgEnd + "\r\n");
                bytesSent = s.Send(msg);
                // Check the flooder response for an acknowledgement
                bytesRecvd = s.Receive(bytes);
                response = Encoding.ASCII.GetString(bytes, 0, bytesRecvd);
                if (response.Trim().Substring(0, 12) == "ACK-Received")
                {
                    success = true;
                }
                else
                {
                    MessageBox.Show("Error sending message - end token not acknowledged", "Send End Token", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return success;

            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("ArgumentNullException for " + flooder.SourceIpAddress.ToString() + ": " + ane.ToString(),
                    "Argument Null Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException for " + flooder.SourceIpAddress.ToString() + ": " + se.ToString(),
                    "Socket Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected exception for " + flooder.SourceIpAddress.ToString() + ": " + ex.ToString(),
                    "Socket Connection - General Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            finally
            {
                // Release the socket.
                if (s.Connected == true)
                    s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
        }
        //private void StartPacketCapture(bool marked, string sourceHostIp, string destinationHostIp, int captureInterval)
        private bool StartPacketCapture(string sourceHostIp, string destinationHostIp, int captureInterval)
        {
            bool result = false;
            // Start capturing packet data
            //bool marked = true;
            //string _CurrentCaptureFileName = string.Empty;
            //string _CaptureFolderPath = @"C:\temp";
            //string _ParseFolderPath = @"C:\temp\ParseCaptureFiles";

            string fileName = string.Format("CaptureFile{0}{1}.pcap", DateTime.Now.Ticks.ToString(), IsMarked ? "d" : "u");
            _CurrentCaptureFileName = fileName;

            try
            {
                // Call the CaptureAgent
                CaptureAgent.CaptureAgent ca = new CaptureAgent.CaptureAgent();
                ca.StartCaptureSession(sourceHostIp, destinationHostIp, captureInterval, _CaptureFolderPath, fileName);
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting packet capture: " + ex.Message, "Start Packet Capture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }
        }

        private void MovePacketCaptureFile(string currentCaptureFileName)
        {
            // Move the packet capture file to the parser service folder

            // Wait for any process to finish writing to the file
            Thread.Sleep(500);

            try
            {
                // Call the CaptureAgent
                CaptureAgent.CaptureAgent ca = new CaptureAgent.CaptureAgent();
                ca.MoveCaptureFileToParseFolder(_CaptureFolderPath, _ParseFolderPath, currentCaptureFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error moving packet capture: " + ex.Message, "Move Packet Capture File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeBackgroundWorkerTheads()
        {
            this.bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }
        private void InitializeProgressSpinner()
        {
            this.ProgressSpinnerPictureBox.Image = COWEClient.Properties.Resources.fedora_spinner;
            this.ProgressSpinnerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.ProgressSpinnerPictureBox.Visible = false;
        }
        private void InitializeProgressLabel()
        {
            this.clockLabel.Visible = false;
            this.ProgressLabel.Text = "";
            this.ProgressLabel.Visible = false;
        }
        private void ResetProgressControls()
        {
            this.ProgressSpinnerPictureBox.Visible = false;
            this.ProgressLabel.Text = "";
            this.ProgressLabel.Visible = false;
        }
        private void DisplayElapsedTime(string et)
        {
            // if (this._FlooderStatusDataGridView.Rows[_SelectedRow].Cells["FlooderRunTime"].invokeRequired)
        }
        private void StopWatchStart()
        {
            // Reset the elapsed time
            stopWatch.Reset();
            stopWatch.Start();
            timer.Start();
        }
        private void StopWatchStop()
        {
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                timer.Stop();
            }
        }
        #endregion
    }
}
