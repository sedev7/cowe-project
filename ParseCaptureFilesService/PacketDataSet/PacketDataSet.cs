using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.PacketDataSet
{
    public class PacketDataSet
    {
        public PacketDataSet() { }

        public DataTable CreatePacketDataTable()
        {
            // Create the data columns (same as the database)
            DataColumn dcPacketId = new DataColumn("PacketId", typeof(int));
            dcPacketId.Caption = "PacketId";
            dcPacketId.ReadOnly = false;
            dcPacketId.AllowDBNull = false;
            dcPacketId.Unique = true;
            dcPacketId.AutoIncrement = true;
            dcPacketId.AutoIncrementSeed = 0;
            dcPacketId.AutoIncrementStep = 1;

            //DataColumn dcFileName = new DataColumn("FileName", typeof(string));
            //dcFileName.Caption = "File Name";
            //dcFileName.ReadOnly = false;
            //dcFileName.AllowDBNull = false;
            //dcFileName.Unique = false;

            DataColumn dcCaptureBatchId = new DataColumn("CaptureBatchId", typeof(string));
            dcCaptureBatchId.Caption = "CaptureBatchId";
            dcCaptureBatchId.ReadOnly = false;
            dcCaptureBatchId.AllowDBNull = false;
            dcCaptureBatchId.Unique = false;

            DataColumn dcPacketNumber = new DataColumn("PacketNumber", typeof(int));
            dcPacketNumber.Caption = "Packet Number";
            dcPacketNumber.ReadOnly = false;
            dcPacketNumber.AllowDBNull = false;
            dcPacketNumber.Unique = true;

            DataColumn dcTimeStamp = new DataColumn("TimeStamp", typeof(DateTime));
            dcTimeStamp.Caption = "TimeStamp";
            dcTimeStamp.ReadOnly = false;
            dcTimeStamp.AllowDBNull = false;
            dcTimeStamp.Unique = true;

            DataColumn dcMarked = new DataColumn("Marked", typeof(int));
            dcMarked.Caption = "Marked";
            dcMarked.ReadOnly = false;
            dcMarked.AllowDBNull = false;
            dcMarked.Unique = false;

            // Add the columns to a data table
            DataTable packetTable = new DataTable();
            //packetTable.Columns.AddRange(new DataColumn[] { dcPacketId, dcFileName, dcPacketNumber, dcTimeStamp, dcMarked });
            packetTable.Columns.AddRange(new DataColumn[] { dcPacketId, dcCaptureBatchId, dcPacketNumber, dcTimeStamp, dcMarked });
            
            return packetTable;
        }

        public DataSet CreateDataSet(DataTable dataTable)
        {
            DataSet packetDs = new DataSet();
            packetDs.Tables.Add(dataTable);
            return packetDs;
        }
    }
}
