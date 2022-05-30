using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class GovManagerTitleManager : BaseObject
    {
        public enum Titles
        {
            SettlementManager = 1, NezamManager, ExecutiveOffice
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.GovManagerTitle);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblGovManagerTitle";
            tableMapping.ColumnMappings.Add("GmtId", "GmtId");
            tableMapping.ColumnMappings.Add("GmtName", "GmtName");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("InActiveDate", "InActiveDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
         
            Adapter.TableMappings.Add(tableMapping);
            Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.SelectCommand.Connection = this.Connection;
            Adapter.SelectCommand.CommandText = "dbo.spSelectGovManagerTitle";
            Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GmtId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "GmtId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
       
            Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.DeleteCommand.Connection = this.Connection;
            Adapter.DeleteCommand.CommandText = "dbo.spDeleteGovManagerTitle";
            Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_GmtId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "GmtId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
       
            Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.InsertCommand.Connection = this.Connection;
            Adapter.InsertCommand.CommandText = "dbo.spInsertGovManagerTitle";
            Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GmtName", global::System.Data.SqlDbType.NVarChar, 200, global::System.Data.ParameterDirection.Input, 0, 0, "GmtName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActiveDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        
            Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.UpdateCommand.Connection = this.Connection;
            Adapter.UpdateCommand.CommandText = "dbo.spUpdateGovManagerTitle";
            Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GmtName", global::System.Data.SqlDbType.NVarChar, 200, global::System.Data.ParameterDirection.Input, 0, 0, "GmtName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActiveDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "InActiveDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_GmtId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "GmtId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GmtId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "GmtId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblGovManagerTitleDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        public void FindByCode(int GmtId)
        {
            this.Adapter.SelectCommand.Parameters["@GmtId"].Value = GmtId;
            Fill();
        }

        public DataTable FindByName(string GmtName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectGovManagerTitleByName", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@GmtName", SqlDbType.NVarChar);
            adapter.SelectCommand.Parameters["@GmtName"].Value = GmtName;
            adapter.Fill(dt);
            return (dt);
        }

         public DataTable SelectActivManagerTitle()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectGovManagerTitle", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@GmtId", SqlDbType.NVarChar).Value=-1;
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", 0);
            adapter.Fill(dt);
            return (dt);
        }
        
    }
}
