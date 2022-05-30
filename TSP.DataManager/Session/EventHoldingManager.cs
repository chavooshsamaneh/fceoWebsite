using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.DataManager.Session
{
    public class EventHoldingManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Session_EventHolding);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "EventHolding";
            tableMapping.ColumnMappings.Add("HoldingId", "HoldingId");
            tableMapping.ColumnMappings.Add("EventId", "EventId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("MeType", "MeType");
            tableMapping.ColumnMappings.Add("HoldingDate", "HoldingDate");
            tableMapping.ColumnMappings.Add("HoldingTime", "HoldingTime");
            tableMapping.ColumnMappings.Add("HoldingLocationId", "HoldingLocationId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectSession_EventHolding";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@EventId", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteSession_EventHolding";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_HoldingId", global::System.Data.SqlDbType.BigInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertSession_EventHolding";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EventId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EventId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeType", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HoldingDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HoldingTime", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HoldingLocationId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingLocationId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateSession_EventHolding";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EventId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EventId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeType", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HoldingDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HoldingTime", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HoldingLocationId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingLocationId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_HoldingId", global::System.Data.SqlDbType.BigInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HoldingId", global::System.Data.SqlDbType.BigInt, 8, global::System.Data.ParameterDirection.Input, 0, 0, "HoldingId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.Session.SessionDataSet.EventHoldingDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByEventId(int EventId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EventId"].Value = EventId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEventHoldingGroupByMembers(int EventId)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_EventHoldingGroupByMembers";
            objCommand.Parameters.AddWithValue("EventId", EventId);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return objTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEventHoldingMemberDetails(int EventId, int MeId, int MeType)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_EventHoldingMemberDetails";
            objCommand.Parameters.AddWithValue("EventId", EventId);
            objCommand.Parameters.AddWithValue("MeId", MeId);
            objCommand.Parameters.AddWithValue("MeType", MeType);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return objTable;
        }
    }
}
