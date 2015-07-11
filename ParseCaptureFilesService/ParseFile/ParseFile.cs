#region Application Information
/*
 * ParseCaptureFilesService Solution
 * 
 * Service application for Client application, which is used to parse capture files and 
 * load the capture file data into a database.
 * 
 * J. Savage  06-11-2015
 * 
 * NOTES: 
 * 
 * v1 06-11-2015:
 *  - Modify original beta version to create CaptureBatch data for each file.
 *
 * TO DO:
 * 
 */
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PcapDotNet.Core;
using PcapDotNet.Packets;

using COWE.DataLayer;
using COWE.PacketDataSet;

namespace COWE.ParseFile
{
    public class ParseFile
    {
        string DbConnectionString = string.Empty;
        
        public string ParsedFilesDir { get; set; }
        
        public ParseFile(string dbConnectionString)
        {
            this.DbConnectionString = dbConnectionString;
        }

        public bool ParseCaptureFile(string fileNameAndPath, string fileName, bool includeParsedFileLog)
        {
            
            bool result = false;

            // Create an offline device for the capture file
            OfflinePacketDevice opd = new OfflinePacketDevice(fileNameAndPath);

            // Create a container to hold the captured packets
            BindingList<Packet> packets = new BindingList<Packet>();

            // Open the capture file (snapshot length, attributes, read timeout)
            using (PacketCommunicator com = opd.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                // Retrieve the packets
                Packet packet;
                string pktResult = string.Empty;

                do
                {
                    PacketCommunicatorReceiveResult rec = com.ReceivePacket(out packet);
                    pktResult = rec.ToString();
                    switch(rec)
                    {
                        case PacketCommunicatorReceiveResult.Timeout:
                            // Timeout elapsed
                            continue;
                        case PacketCommunicatorReceiveResult.Ok:
                            packets.Add(packet);
                            break;
                        case PacketCommunicatorReceiveResult.Eof:
                            break;
                        default:
                            throw new InvalidOperationException("Unknown error occurred while reading packet: " + result);
                    }

                } while (pktResult != "Eof");
            }
            if(packets.Count > 0)
            {
                // We successfully parsed the capture file
                // Note: in future version we may need to compare the number of rows in the file with the number of parsed packets obtained
                result = true;
            }

            // Write packets out to a file
            if (result && includeParsedFileLog)
            {
                int pktno = 0;
                using (StreamWriter sw = new StreamWriter(ParsedFilesDir + "\\ParsePacketTest_" + DateTime.Now.Ticks.ToString() + ".txt"))
                {
                    foreach (Packet pkt in packets)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("{0}|{1}|{2}", fileName, pktno++, Convert.ToDateTime(pkt.Timestamp).TimeOfDay);
                        sw.WriteLine(sb.ToString());
                    }
                }
            }

            // Check if packets are marked (i.e., flooder was running during packet capture 'd', or unmarked, 'u')
            int marked = fileName.Substring(fileName.Length - 6, 1) == "u" ? 0 : 1;
            int captureBatchId = -1;

            // Write a CaptureBatch record to the database for this file
            if(result)
            {
                try
                {
                    DataImport di = new DataImport(DbConnectionString);
                    captureBatchId = di.CreateCaptureBatch(fileName, marked == 1? true : false);

                    result = captureBatchId > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error creating CaptureBatch record in database for file [" + fileName + "]: " + ex.Message);
                }
            }

            // Write packets to a DataTable and import them into the database
            if (result)
            {
                DataSet packetDs = new DataSet();
                DataTable packetTable = new DataTable();
                PacketDataSet.PacketDataSet pds = new PacketDataSet.PacketDataSet();

                try
                {
                    int pktno = 0;
                    packetTable = pds.CreatePacketDataTable();
                    {
                        foreach (Packet pkt in packets)
                        {
                            DataRow dr = packetTable.NewRow();
                            dr["CaptureBatchId"] = captureBatchId;
                            dr["PacketNumber"] = pktno++;
                            dr["TimeStamp"] = Convert.ToDateTime(pkt.Timestamp).TimeOfDay.ToString();
                            dr["Marked"] = marked;
                            packetTable.Rows.Add(dr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding packet data row to DataTable: " + ex.Message);
                }

                try
                {
                    // Import into the database
                    COWE.DataLayer.DataImport di = new DataImport(DbConnectionString);
                    bool success = di.LoadPacketData(packetTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error importing packet data into database: " + ex.Message);
                }
            }

            return result;
        }
    }
}
