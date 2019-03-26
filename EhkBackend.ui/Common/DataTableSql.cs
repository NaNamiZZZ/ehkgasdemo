using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EhkBackend.ui.Common
{
    public static class DataTableSql
    {

        public static void SqlBulkCopyByDatatable(string connectionsString, string TableName, DataTable dtSelect)
        {
            using (SqlConnection conn = new SqlConnection(connectionsString))
            {

                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionsString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        sqlbulkcopy.BatchSize = 20000;
                        sqlbulkcopy.BulkCopyTimeout = 0;
                        for (int i = 0; i < dtSelect.Columns.Count; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(dtSelect.Columns[i].ColumnName, dtSelect.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(dtSelect);

                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}