using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class RequestInActivesManager : BaseObject
    {
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblRequestInActives";
            tableMapping.ColumnMappings.Add("ReqInId", "ReqInId");
            tableMapping.ColumnMappings.Add("TableId", "TableId");
            tableMapping.ColumnMappings.Add("TableType", "TableType");
            tableMapping.ColumnMappings.Add("ReqId", "ReqId");
            tableMapping.ColumnMappings.Add("ReqType", "ReqType");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("SysInActive", "SysInActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("InActiveRow", "InActiveRow");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "spSelectRequestInActives";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ReqId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ReqType", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActiveRow", SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@SysInActive", SqlDbType.SmallInt);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "spDeleteRequestInActives";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReqInId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReqInId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "spInsertRequestInActives";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReqId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReqId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReqType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReqType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SysInActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "SysInActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActiveRow", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "InActiveRow", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "spUpdateRequestInActives";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReqId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReqId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReqType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReqType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SysInActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "SysInActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReqInId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReqInId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReqInId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ReqInId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActiveRow", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "InActiveRow", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblRequestInActivesDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }


        #region FindByTableIdTableType
        public void FindByTableIdTableType(int TableId, int TableType, int ReqId, int InActiveRow, int SysInActive)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TableId"].Value = TableId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableType;
            this.Adapter.SelectCommand.Parameters["@ReqId"].Value = ReqId;
            this.Adapter.SelectCommand.Parameters["@InActiveRow"].Value = InActiveRow;
            this.Adapter.SelectCommand.Parameters["@SysInActive"].Value = SysInActive;

            Fill();
        }

        public void FindByTableIdTableType(int TableId, int TableType, int ReqId, int InActiveRow)
        {
            FindByTableIdTableType(TableId, TableType, ReqId, InActiveRow, -1);
        }

        public void FindByTableIdTableType(int TableId, int TableType, int ReqId)
        {
            FindByTableIdTableType(TableId, TableType, ReqId, -1,-1);
        }
        #endregion

        public void FindByReqId(int ReqId, int ReqType)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ReqId"].Value = ReqId;
            this.Adapter.SelectCommand.Parameters["@ReqType"].Value = ReqType;
            Fill();
        }

        public void FindByReqIdAndTableId(int ReqId, int TableId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ReqId"].Value = ReqId;
            this.Adapter.SelectCommand.Parameters["@TableId"].Value = TableId;
            Fill();
        }

        public void UpdateInActiveRowByRequest(int ReqId, int ReqType, int InActiveRow)
        {
            Boolean result = false;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            sqlDataAdapter.UpdateCommand.Connection = this.Connection;
            sqlDataAdapter.UpdateCommand.CommandText = "spUpdateRequestInActiveRowByRequest";
            sqlDataAdapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlDataAdapter.UpdateCommand.Transaction = this.Transaction;
            sqlDataAdapter.UpdateCommand.Parameters.AddWithValue("@ReqId", ReqId);
            sqlDataAdapter.UpdateCommand.Parameters.AddWithValue("@ReqType", ReqType);
            sqlDataAdapter.UpdateCommand.Parameters.AddWithValue("@InActiveRow", InActiveRow);
            sqlDataAdapter.UpdateCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReqId"></param>
        /// <param name="TableId"></param>
        /// <param name="TableType"></param>
        /// <param name="RequestInActivesManager">برای مواردی که چند تا کار دیگر هم انجام می شود و تمام منیجر ها باید در ترنزکشن اضافه شوند </param>
        /// <returns> 0 successful 1 not exist 2 error</returns>
        public static int DeleteRequestInActive(int ReqId, int TableId, int TableType
           , TSP.DataManager.RequestInActivesManager RequestInActivesManager)
        {
            int result = 0;  // 0 successful 1 not exist 2 error = new TSP.DataManager.RequestInActivesManager();
            RequestInActivesManager.FindByTableIdTableType(TableId, TableType, ReqId);
            if (RequestInActivesManager.Count == 1)
            {
                RequestInActivesManager[0].Delete();
                if (RequestInActivesManager.Save() > 0)
                    result = 0;
                else result = 2;
            }
            else result = 1;

            return result;

        }

        public static int DeleteRequestInActive(int ReqId, int TableId, int TableType)
        {
            return DeleteRequestInActive(ReqId, TableId, TableType, new RequestInActivesManager());         

        }

    }
}
