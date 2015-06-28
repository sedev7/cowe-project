using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.DataLayer
{
    public class CumulativeIntervalData
    {
        #region Constructors
        public CumulativeIntervalData() { }
        public CumulativeIntervalData(string dbConnectionString)
        {
            this.DbConnectionString = dbConnectionString;
        }
        #endregion

        #region Properties
        public string DbConnectionString { get; set; }
        public int LastIntervalNumber { 
        
            get 
            {
                using (var context = new PacketAnalysisEntity())
                {
                    var intervalNumber = (from i in context.CumulativeIntervals
                                          select i.CumulativeIntervalNumber).Max();
                    return intervalNumber;
                }
            }
        }
        #endregion

        #region Public Methods

        public BindingList<CumulativeInterval> GetCumulativeIntervals()
        {
            BindingList<CumulativeInterval> intervals = new BindingList<CumulativeInterval>();

            using (var context = new PacketAnalysisEntity())
            {
                var cumulativeIntervals = (from i in context.CumulativeIntervals
                                          orderby i.CumulativeIntervalNumber
                                          select i).ToList();

                if (cumulativeIntervals.Count > 0)
                {
                    foreach (CumulativeInterval ci in cumulativeIntervals)
                    {
                        intervals.Add(ci);
                    }
                }
            }

            return intervals;
        }

        public BindingList<CumulativeInterval> GetMarkedCumulativeIntervals(CaptureState marked)
        {
            // Get the cumulative intervals for the passed in marked type
            BindingList<CumulativeInterval> intervals = new BindingList<CumulativeInterval>();

            bool Marked = marked == CaptureState.Marked? true : false;

            using (var context = new PacketAnalysisEntity())
            {
                var cumulativeIntervals = (from i in context.CumulativeIntervals
                                           orderby i.CumulativeIntervalNumber
                                           where i.Marked == Marked
                                           select i).ToList();

                if (cumulativeIntervals.Count > 0)
                {
                    foreach (CumulativeInterval ci in cumulativeIntervals)
                    {
                        intervals.Add(ci);
                    }
                }
            }

            return intervals;
        }

        public bool DeleteCumulativeIntervals(CaptureState marked)
        {
            bool success = false;

            bool Marked = marked == CaptureState.Marked ? true : false;

            // Delete the cumulative interval counts for a marked type
            using(var context = new PacketAnalysisEntity())
            {
                var rowCount = (from c in context.CumulativeIntervals
                                where c.Marked == Marked
                                select c.CumulativeIntervalNumber).Count();

                if (rowCount > 0)
                {
                    var deleteParameter = new SqlParameter("@Marked", Marked);
                    var retval = new SqlParameter("@RowsDeleted", SqlDbType.Int);
                    retval.Value = 0;
                    retval.Direction = ParameterDirection.Output;

                    var result = context.Database
                        .SqlQuery<CumulativeInterval>("[COWE].[CumulativeIntervalDelete] @Marked, @RowsDeleted out", deleteParameter, retval);
                    //context.Database.ExecuteSqlCommand("exec @retval = [COWE].[CumulativeIntervalDelete] @Marked", retval, deleteParameter);

                    //var rows = result.Single();

                    ////if (result.Count() == rowCount)
                    //if ((int)retval.Value == (int)rowCount)
                    //{
                    //    success = true;
                    //}
                }
            }

            return success;
        }
        public bool LoadCumulativeIntervals(DataTable dataTable)
        {
            bool success = false;

            // Bulk load the DataTable into the database
            using (SqlConnection conn = new SqlConnection(DbConnectionString))
            {
                conn.Open();

                using (SqlBulkCopy bc = new SqlBulkCopy(conn))
                {
                    bc.DestinationTableName = "COWE.CumulativeInterval";

                    try
                    {
                        bc.WriteToServer(dataTable);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error bulk-inserting CumulativeInterval DataTable into database: " + ex.Message);
                    }
                }

                conn.Close();
            }

            return success;
        }
        #endregion
    }
}
