#region Application Information
/*
 * COWE-Client Solution
 * 
 * Client application used to connect to, and control, flooder application.
 * 
 * J. Savage  03-08-2015
 * 
 * TO DO:
 * - Reset UI when socket connection fails (i.e., throws exception).
 * 
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COWEClient
{
    public partial class Client : Form
    {
        #region Global Variables

        int _PID = 1;                   // Process Id for flooder
        int _MaxGridDisplayRows = 8;    // Maximum number of rows to display in grid
        int _MaxGridHeight = 0;         // Height of grid when maximum number of rows are displayed

        // Form colors
        Color NotConnected = Color.LightCoral;
        Color NonResident = Color.Khaki;
        Color CoResident = Color.LightGreen;

        DataGridView _FlooderStatusDataGridView = new DataGridView();
        #endregion

        #region Public Methods
        public Client()
        {
            InitializeComponent();
            InitializeFlooderStatusDataGridViewControl();
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

            _FlooderStatusDataGridView.Rows[row].Cells[col].Value = pid.ToString();  // PID
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
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                var cellButton = (DataGridViewButtonCell)senderGrid[col, row];

                if (cellButton.Value.ToString() == "Start")
                {
                    cellButton.UseColumnTextForButtonValue = false;
                    cellButton.Value = "Stop";
                    //cellButton.Style.BackColor = Color.LightBlue;
                    //cellButton.FlatStyle = FlatStyle.Popup;
                    cellButton.Style.BackColor = System.Drawing.Color.Red;

                    // Update the status column for this row (i.e., this flooder instance)
                    _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Value = "Running";
                    _FlooderStatusDataGridView.Rows[row].Cells["FlooderStatus"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    _FlooderStatusDataGridView["FlooderStatus", row].Style.BackColor = NonResident;

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

                    IPAddress connectionIp = new IPAddress(remoteHostIpAddrByteArr);
                    int timerInterval = Convert.ToInt32(FlooderIntervalTextBox.Text);
                    //bool success = StartFlooder(ip, portNum, Convert.ToInt32(FlooderIntervalTextBox.Text));
                    bool success = StartFlooder(connectionIp, flooderConnectionPortNumber, flooderIpAddress, flooderSourcePortNumber, flooderDestinationIpAddress, flooderDestinationPortNumber, timerInterval);

                    //SocketAsync sa = new SocketAsync(ip, portNum, timerInterval);
                    //AsynchronousClient ac = new AsynchronousClient();
                    //ac.StartClient(sa);
                    //string response = string.Empty;
                   
                }
                else
                {
                    // Stop flooder
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
                }
            }
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

            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = pid.ToString();  // PID
            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = "10.10.10.208";   // Flooder IP Address
            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = "8080";   // Flooder port
            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = "10.10.10.9";   // Target
            _FlooderStatusDataGridView.Rows[row].Cells[col++].Value = "";   // Runtime
            _FlooderStatusDataGridView.Rows[row].Cells[col].Value = "Not Connected";   // Status
            _FlooderStatusDataGridView.Rows[row].Cells[col].Style.BackColor = NotConnected;

            // Hide the row header column
            _FlooderStatusDataGridView.RowHeadersVisible = false;

            ResizeFlooderGrid();
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

        //private bool StartFlooder(IPAddress ipAddress, int portNo, int timeInterval)
        //private bool StartFlooder(IPAddress sourceIpAddress, int sourcePortNo, IPAddress destIpAddress, int destPortNo, int timeInterval)
        private bool StartFlooder(IPAddress connectionIpAddress, int connectionPortNo, string sourceIpAddress, int sourcePortNumber, string destIpAddress, int destPortNumber, int timeInterval)
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Create a TCP socket
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to a remote flooder
            try
            {
                //IPEndPoint remoteEP = new IPEndPoint(ipAddress, portNo);
                IPEndPoint remoteEP = new IPEndPoint(connectionIpAddress, connectionPortNo);

                //// Create a TCP/IP  socket.
                //Socket s = new Socket(AddressFamily.InterNetwork,
                //SocketType.Stream, ProtocolType.Tcp);

                s.Connect(remoteEP);
               
                string strSocketMessage = "";

                // Get the demographic data

                //strSocketMessage = "Socket connected to GTC:" + s.RemoteEndPoint.ToString() + "\r\n";

                //strRtbMessage = strRtbMessage + strSocketMessage + "\r\n";
                //rtbMessages.Text = strRtbMessage;
                //rtbMessages.Refresh();

                // Send message to flooder with time increment to run
                string msgStart = "start";
                string msgRunTime = timeInterval.ToString();
                string msgEnd = "end";

                // Encode the data string into a byte array.
                //byte[] msg = Encoding.ASCII.GetBytes(msgStart + "<EOF>");
                byte[] msg = Encoding.ASCII.GetBytes(msgStart + "\r\n");
                //byte[] msg = Encoding.ASCII.GetBytes(msgStart + ' ' + msgEnd);

                // Construct the 'start' file string
                string msgStartFile = sourceIpAddress + "|" + destIpAddress + "|" + sourcePortNumber.ToString() + "|" + destPortNumber + "|" + timeInterval.ToString() + "|";
                
                // Send the data through the socket.
                int bytesSent = s.Send(msg);
                int bytesRecvd = s.Receive(bytes);
                string response = Encoding.ASCII.GetString(bytes, 0, bytesRecvd);

                //msg = Encoding.ASCII.GetBytes(msgRunTime + "\r\n");
                msg = Encoding.ASCII.GetBytes(msgStartFile + "\r\n");
                bytesSent = s.Send(msg);
                bytesRecvd = s.Receive(bytes);
                response = Encoding.ASCII.GetString(bytes, 0, bytesRecvd);

                msg = Encoding.ASCII.GetBytes(msgEnd + "\r\n");
                bytesSent = s.Send(msg);
                bytesRecvd = s.Receive(bytes);
                response = Encoding.ASCII.GetString(bytes, 0, bytesRecvd);

                // Receive the response from the remote device.
                bytesRecvd = s.Receive(bytes);
                response = Encoding.ASCII.GetString(bytes, 0, bytesRecvd);

                //strSocketMessage = "Sending BKARR (CLOSE) message..." + "\r\n" + "\r\n" +
                //                   "GTC Acknowledgement: " + "\r\n" + Encoding.ASCII.GetString(bytes, 0, bytesRec);

                //strRtbMessage = strRtbMessage + strSocketMessage;
                //rtbMessages.Text = strRtbMessage;
                //rtbMessages.Refresh();    

                return true;

            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("ArgumentNullException for " + sourceIpAddress.ToString() + ": " + ane.ToString(),
                    "Argument Null Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException for " + sourceIpAddress.ToString() + ": " + se.ToString(),
                    "Socket Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected exception for " + sourceIpAddress.ToString() + ": " + ex.ToString(),
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

        #endregion

    }
}
