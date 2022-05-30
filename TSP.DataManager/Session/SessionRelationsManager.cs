using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.DataManager.Session
{
    public class SessionRelationsManager:BaseObject
    {
        public static int TableTypeCode
        {
            get { return (int)TSP.DataManager.TableType.Session_SessionRelations; }
        }

        public static int TableTypeId
        {
            get { return TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Session_SessionRelations); }
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "SessionRelations";
            tableMapping.ColumnMappings.Add("RelationId", "RelationId");
            tableMapping.ColumnMappings.Add("RequestId", "RequestId");
            tableMapping.ColumnMappings.Add("SessionFrom", "SessionFrom");
            tableMapping.ColumnMappings.Add("SessionTo", "SessionTo");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectSession_SessionRelations";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@RelationId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@RequestId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteSession_SessionRelations";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_RelationId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RelationId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertSession_SessionRelations";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RequestId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionFrom", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SessionFrom", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionTo", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SessionTo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 511, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateSession_SessionRelations";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RequestId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionFrom", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SessionFrom", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionTo", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SessionTo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 511, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_RelationId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RelationId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RelationId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RelationId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.Session.SessionDataSet.SessionRelationsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindById(int Id)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RelationId"].Value = Id;
            Fill();
        }

        public void FindByRequestId(int RequestId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RequestId"].Value = RequestId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySessionId(int SessionId)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_SessionRelationsForSession";
            objCommand.Parameters.AddWithValue("@SessionId", SessionId);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return objTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectRelationsForRequest(int SessionId, int RequestId)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_SessionRelationsForSessionRequest";
            objCommand.Parameters.AddWithValue("@SessionId", SessionId);
            objCommand.Parameters.AddWithValue("@RequestId", RequestId);
            objCommand.Parameters.AddWithValue("@TableType", TableTypeId);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return objTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Boolean CheckExistSessionRelation(int SessionId,int RelatedSessionId)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_SessionRelationsExistForSession";
            objCommand.Parameters.AddWithValue("@SessionId", SessionId);
            objCommand.Parameters.AddWithValue("@RelatedSessionId", RelatedSessionId);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return (objTable.Rows.Count > 0);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Boolean CheckExistSessionRelationForRequest(int SessionId, int RequestId, int RelatedSessionId, int SessionRelationTableTypeId)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_SessionRelationsExistForSessionRequest";
            objCommand.Parameters.AddWithValue("@SessionId", SessionId);
            objCommand.Parameters.AddWithValue("@RequestId", RequestId);
            objCommand.Parameters.AddWithValue("@RelatedSessionId", RelatedSessionId);
            objCommand.Parameters.AddWithValue("@TableType", SessionRelationTableTypeId);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return (objTable.Rows.Count > 0);
        }
    }
}
