using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DataLayer
{
    public class DataImport
    {
        string DbConnectionString = string.Empty;

        public DataImport(string dbConnectionString)
        {
            this.DbConnectionString = dbConnectionString;
        }

        public bool LoadPacketData(DataTable dataTable)
        {
            bool success = false;

            // Bulk load the DataSet into the database
            using (SqlConnection conn = new SqlConnection(DbConnectionString))
            {
                conn.Open();

                using (SqlBulkCopy bc = new SqlBulkCopy(conn))
                {
                    bc.DestinationTableName = "COWE.CapturePacket";

                    try
                    {
                        bc.WriteToServer(dataTable);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error bulk-inserting packet DataTable into database: " + ex.Message);
                    }
                }

                conn.Close();
            }

            return success;
        }

        public int CreateCaptureBatch(string fileName, bool marked)
        {
            // Create a record for the capture file - the ID will be referenced as a FK elsewhere
            //bool success = false;
            int batchId = -1;

            using (SqlConnection conn = new SqlConnection(DbConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[COWE].[CaptureBatchInsert]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FileName", SqlDbType.VarChar, 200, "FileName"));
                cmd.Parameters.Add(new SqlParameter("@Marked", SqlDbType.Bit, 1, "Marked"));
                cmd.Parameters.Add(new SqlParameter("@Mean", SqlDbType.Decimal, 18, "Mean"));
                cmd.Parameters.Add(new SqlParameter("@NewCaptureBatchId", SqlDbType.Int));
                cmd.Parameters["@NewCaptureBatchId"].Direction = ParameterDirection.Output;

                cmd.Parameters[0].Value = fileName;
                cmd.Parameters[1].Value = marked ? 1 : 0;
                cmd.Parameters[2].Value = 0;

                conn.Open();
                cmd.ExecuteNonQuery();
                var result = cmd.Parameters["@NewCaptureBatchId"].Value;

                batchId = Convert.ToInt32(result);
            }
            return batchId;
        }
    }
}
