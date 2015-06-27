using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParseCaptureFile
{
    public partial class MainForm : Form
    {
        #region Variables
        BindingList<CaptureFileRecord> _Records = new BindingList<CaptureFileRecord>();
        BindingList<ParsedRecord> _ParsedRecords = new BindingList<ParsedRecord>();

        const int PacketHeaderLength = 54;        // Packet header length in bytes
        string _ClientIP = string.Empty;
        string _ServerIP = string.Empty;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            // Initialize variables
            _ClientIP = "10.10.10.2";
            _ServerIP = "10.10.10.100";

#if(DEBUG)
            //FilePathTextBox.Text = "C:\\Users\\Jim\\Documents\\MATLAB\\Exp3-Tr1_NoFlood_CWND_ML_Analysis_Test.csv";
            FilePathTextBox.Text = "C:\\Users\\Jim\\Documents\\MATLAB\\Exp3-Tr1_NoFlood_CWND_ML_Analysis.csv";
#endif
            if (FilePathTextBox.Text.Length > 0)
            {
                ParseFileButton.Enabled = true;
            }
            else
            {
                ParseFileButton.Enabled = false;
            }


        }

        private void GetFileButton_Click(object sender, EventArgs e)
        {
            // Open a file dialog and navigate to the file to be parsed
            GetFileOpenFileDialog.ShowDialog();
            FilePathTextBox.Text = GetFileOpenFileDialog.FileName;
            if(FilePathTextBox.Text.Length > 0)
            {
                ParseFileButton.Enabled = true;
            }
        }

        private void ParseFileButton_Click(object sender, EventArgs e)
        {
            OutputRichTextBox.Text = "\r\nParsing the file ...\r\n\r\n";

            int recordCount = 0;

            BindingList<CaptureFileRecord> records = new BindingList<CaptureFileRecord>();

            try
            {
                using (CsvReadWrite.CsvFileReader reader = new CsvReadWrite.CsvFileReader(FilePathTextBox.Text))
                {
                    CsvReadWrite.CsvRow row = new CsvReadWrite.CsvRow();
                    while (reader.ReadRow(row))
                    {
                        recordCount++;

                        //foreach (string s in row)
                        //{
                        //    OutputRichTextBox.Text += s;
                        //    OutputRichTextBox.Text += " | ";
                        //}
                        //OutputRichTextBox.Text += "\r\n";

                        // Parse the fields of the CSV file and put each into a CaptureFileRecord
                        CaptureFileRecord rec = new CaptureFileRecord();

                        if (Convert.ToString(row[0]) == "No" && Convert.ToString(row[1]) == "Time")
                        {
                            // Skip the first row - it has column labels
                            continue;
                        }
                        else
                        {
                            rec.PacketNumber = Convert.ToInt32(row[0]);
                            rec.ArrivalTime = Convert.ToDecimal(row[1]);
                            rec.SourceIP = row[2];
                            rec.DestinationIP = row[3];
                            rec.Protocol = row[4];
                            rec.DataLength = Convert.ToInt32(row[5]);
                            rec.Information = row[6];

                            records.Add(rec);
                        }
                    }
                }
                _Records = records;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing CSV file (CsvReader):" + ex.Message);
            }

            // Display parsed records 
            OutputRichTextBox.Text += "Parsed " + recordCount + " records.\r\n\r\n";
            OutputRichTextBox.Text += "Displaying the first ten parsed records...\r\n\r\n";

            int displayCount = 0;

            foreach (CaptureFileRecord item in records)
            {
                StringBuilder sb = new StringBuilder();
                OutputRichTextBox.Text += sb.AppendFormat("{0} | {1} | {2} | {3} | {4} | {5} | {6} \r\n", item.PacketNumber, item.ArrivalTime, item.SourceIP, item.DestinationIP, item.Protocol, item.DataLength, item.Information);
                if(displayCount == 10) { break; }
                else { displayCount++; }
            }
        }

        private void ProcessPacketsButton_Click(object sender, EventArgs e)
        {
            if (_Records.Count > 0)
            {
                bool fileTransferRequested = false;
                decimal startTime = 0;
                decimal endTime = 0;
                decimal transferTime = 0;
                int ackNumber = 0;
                int beginPacketNumber = 0;
                int endPacketNumber = 0;
                int dataTransfered = 0;
                int totalDataTransferred = 0;
                int recordCount = 0;

                string messageHeader = "\r\n\r\nProcessing packet file ...\r\n\r\n" + "Found " + _Records.Count + " records to process...\r\n\r\n";
                OutputRichTextBox.Text = "\r\n\r\nProcessing packet file ...\r\n\r\n";
                OutputRichTextBox.Text += "Found " + _Records.Count + " records to process...\r\n\r\n";

                foreach (CaptureFileRecord rec in _Records)
                {
                    recordCount++;

                    // Check if this packet is from the client and contains the request for the file transfer
                    if (rec.SourceIP.Trim() == _ClientIP && rec.DestinationIP.Trim() == _ServerIP && rec.Protocol.Trim() == "HTTP" && rec.Information.Trim() == "GET /w3/GetFile2.php HTTP/1.1")
                    {
                        // Need to keep track of the starting packet number and packet arrival time
                        fileTransferRequested = true;
                        beginPacketNumber = rec.PacketNumber;
                        startTime = rec.ArrivalTime;
                    }

                    // Check if this packet is from the server and contains part of the requested file
                    if(fileTransferRequested && rec.SourceIP.Trim() == _ServerIP && rec.DestinationIP.Trim() == _ClientIP && rec.Protocol.Trim() == "TCP" && rec.Information.Trim().Contains("TCP segment of a reassembled PDU"))
                    {
                        if(beginPacketNumber == 0)
                        {
                            // This is the start of a new segment of a reassembled PDU
                            beginPacketNumber = rec.PacketNumber;
                            startTime = rec.ArrivalTime;
                        }
                        // Need to keep track of the amount of data transfered
                        dataTransfered += rec.DataLength - PacketHeaderLength;
                    }

                    // Check if this packet is an ACK from the client
                    if(fileTransferRequested && rec.SourceIP.Trim() == _ClientIP && rec.DestinationIP.Trim() == _ServerIP && rec.Protocol.Trim() == "TCP" && rec.Information.Trim().Contains("http [ACK]"))
                    {
                        // This is the end of the file transfer

                        // This is the end of series of segment transfers
                        ackNumber += 1;
                        endTime = rec.ArrivalTime;
                        transferTime = endTime - startTime;
                        endPacketNumber = rec.PacketNumber;

                        // Add this data to the processed packets list
                        ParsedRecord prec = new ParsedRecord();
                        prec.AckNumber = ackNumber;
                        prec.StartingPacketNumber = beginPacketNumber;
                        prec.EndingPacketNumber = endPacketNumber;
                        prec.TransmitTime = transferTime;
                        prec.DataTransmitted = dataTransfered;
                        _ParsedRecords.Add(prec);

                        // Keep track of total data transferred
                        totalDataTransferred += dataTransfered;

                        // Reset variables for next group of packets
                        beginPacketNumber = 0;
                        endPacketNumber = 0;
                        startTime = 0;
                        endTime = 0;
                        transferTime = 0;
                        dataTransfered = 0;
                    }

                    // Check if this packet is a final response from the the server
                    if (fileTransferRequested && rec.SourceIP.Trim() == _ServerIP && rec.DestinationIP.Trim() == _ClientIP && rec.Protocol.Trim() == "HTTP" && rec.Information.Trim().Contains("HTTP/1.1 200 OK"))
                    {
                        // This is the end of the file transfer

                        // This is the end of series of segment transfers
                        ackNumber += 1;
                        endTime = rec.ArrivalTime;
                        transferTime = endTime - startTime;
                        endPacketNumber = rec.PacketNumber;

                        // Add this data to the processed packets list
                        ParsedRecord prec = new ParsedRecord();
                        prec.AckNumber = ackNumber;
                        prec.StartingPacketNumber = beginPacketNumber;
                        prec.EndingPacketNumber = endPacketNumber;
                        prec.TransmitTime = transferTime;
                        prec.DataTransmitted = dataTransfered;
                        _ParsedRecords.Add(prec);

                        // Keep track of total data transferred
                        totalDataTransferred += dataTransfered;

                        break;
                    }

                    // Display the results for each 25 records processed
                    if (recordCount % 100 == 0)
                    {
                        OutputRichTextBox.Text = messageHeader;
                        OutputRichTextBox.Text += "\r\nParsed records processed: " + recordCount + "\r\n\r\n";
                        OutputRichTextBox.Text += "\r\nTotal records processed: " + ackNumber + "\r\n\r\n";
                        OutputRichTextBox.Text += "Total data transferred: " + totalDataTransferred + "\r\n\r\n";
                        Application.DoEvents();
                    }
                }

                // Report the final counts and the end of file processing message
                OutputRichTextBox.Text = messageHeader;
                OutputRichTextBox.Text += "\r\nParsed records processed: " + recordCount + "\r\n\r\n";
                OutputRichTextBox.Text += "\r\nFile transfer records processed: " + ackNumber + "\r\n\r\n";
                OutputRichTextBox.Text += "Total data transferred: " + totalDataTransferred + "\r\n\r\n";
                OutputRichTextBox.Text += "Finished processing file!\r\n";
                Application.DoEvents();
            }
            else
            {
                OutputRichTextBox.Text = "No capture records found to process!\r\n\r\n (Did you parse the file?)";
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveProcessedFileButton_Click(object sender, EventArgs e)
        {
            Stream filestream;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "ProcessedPacketFile_" + DateTime.Now.Ticks.ToString() + ".csv";
            sfd.InitialDirectory = @"C:\Jim's Folders\School\TSU\CISE\H-VM-R Project\Co-Residency Watermark Experiment\Experiment 3 - Different Client Same Subnet 10MB File\CongestionWindow";

            sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if ((filestream = sfd.OpenFile()) != null)
                {
                    StreamWriter sw = new StreamWriter(filestream);
                    // Add each line
                    foreach (ParsedRecord rec in _ParsedRecords)
                    {
                        var csv = new StringBuilder();
                        csv.AppendFormat("{0},{1},{2},{3},{4}", rec.AckNumber, rec.StartingPacketNumber, rec.EndingPacketNumber, rec.TransmitTime, rec.DataTransmitted);
                        sw.WriteLine(csv);
                    }

                    sw.Flush();
                    sw.Close();
                    filestream.Close();
                }
            }
        }
    }
}
