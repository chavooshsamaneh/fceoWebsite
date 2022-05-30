using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace TSP.DataManager.TechnicalServices
{
   
    public class TSEsupWebserviceCallingLogManager : BaseObject
    {
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSEsupWebserviceCallingLogDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSEsupWebserviceCallingLog";
            tableMapping.ColumnMappings.Add("TraceId", "TraceId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("Status", "Status");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("Lasttimestame", "Lasttimestame");
            tableMapping.ColumnMappings.Add("Type", "Type");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.DeleteTSEsupWebserviceCallingLog";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TraceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TraceId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_Lasttimestame", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Lasttimestame", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Lasttimestame", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Lasttimestame", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.InsertTSEsupWebserviceCallingLog";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Message", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Message", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.UpdateTSEsupWebserviceCallingLog";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TraceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TraceId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_Lasttimestame", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Lasttimestame", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Lasttimestame", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Lasttimestame", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TraceId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "TraceId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Message", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Message", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }
        public DataTable SelectEsupWebserviceCallingLogForTest()
        {

            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("EsupWebserviceCallingLog", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return dt;
        }
        public DataTable SelectNotSentToShahrdari()
        {

            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectNotSentToShahrdari", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return dt;
        }
        
    }
}
