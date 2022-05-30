using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
    public class WebsiteErrorsManager : BaseObject
    {
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblWebsiteErrors";
            tableMapping.ColumnMappings.Add("Id", "Id");
            tableMapping.ColumnMappings.Add("Message", "Message");
            tableMapping.ColumnMappings.Add("StackTrace", "StackTrace");
            tableMapping.ColumnMappings.Add("Url", "Url");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("Time", "Time");
            tableMapping.ColumnMappings.Add("IsSolved", "IsSolved");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectWebsiteErrors";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Id", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Id", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteWebsiteErrors";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Id", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Id", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertWebsiteErrors";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Message", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "Message", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StackTrace", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "StackTrace", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Url", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "Url", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Time", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "Time", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateWebsiteErrors";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Message", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "Message", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StackTrace", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "StackTrace", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Url", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "Url", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Time", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "Time", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsSolved", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "IsSolved", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Id", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Id", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Id", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Id", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblWebsiteErrorsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        /// <summary>
        /// Save error information in database
        /// </summary>
        /// <param name="err">Error exception</param>
        /// <param name="Url">Web page url (Request.Url.AbsoluteUri)</param>
        /// <param name="UserId">Current user id</param>
        public void InsertError(Exception err, String Url, int UserId)
        {
            try
            {
                System.Data.DataRow dr = this.DataTable.NewRow();
                dr["Message"] = err.Message;
                dr["StackTrace"] = err.StackTrace;
                dr["Url"] = Url;
                dr["Date"] = Utility.GetDateOfToday();
                dr["Time"] = Utility.GetCurrentTime();
                if (UserId > 0)
                    dr["UserId"] = UserId;
                dr["ModifiedDate"] = DateTime.Now;
                this.DataTable.Rows.Add(dr);
                this.Save();
            }
            catch (Exception) { }
        }

        public void FindByCode(int Id)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@Id"].Value = Id;
            Fill();
        }
    }
}
