using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class PriceArchiveManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TS_PriceArchive);
        }
        public static Permission ChoosePriceArchiveForObserverAndDesign(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectChoosePriceArchiveForObserverAndDesign);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TS.PriceArchive";
            tableMapping.ColumnMappings.Add("PriceArchiveId", "PriceArchiveId");
            tableMapping.ColumnMappings.Add("DocumentDate", "DocumentDate");
            tableMapping.ColumnMappings.Add("DocumentNo", "DocumentNo");
            tableMapping.ColumnMappings.Add("Year", "Year");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Remark", "Remark");
            tableMapping.ColumnMappings.Add("FileUrl", "FileUrl");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSPriceArchive";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PriceArchiveId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@Year", System.Data.SqlDbType.NVarChar, 4);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSPriceArchive";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PriceArchiveId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriceArchiveId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSPriceArchive";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Year", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Year", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Remark", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Remark", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrl", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrl", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSPriceArchive";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Year", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Year", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Remark", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Remark", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrl", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrl", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PriceArchiveId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriceArchiveId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriceArchiveId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "PriceArchiveId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet._TS_PriceArchiveDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindById(int Id)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PriceArchiveId"].Value = Id;
            Fill();
        }

        public void FindByYear(string Year)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@Year"].Value = Year;
            Fill();
        }

        public void DeletePriceArchive(int PriceArchiveId)
        {
            SqlCommand cmd = new SqlCommand("spDeleteTSPriceArchiveById", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@PriceArchiveId", PriceArchiveId);
                cmd.ExecuteNonQuery();
                if (this.Transaction == null)
                    this.Connection.Close();
            }
            finally
            {
                cmd.Connection = null;
                cmd = null;

            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Date"> مقدار این پارامتر در محاسبات این تابع تاثیری ندارد</param>
        ///// <returns></returns>
        //public int FindCurrentPriceArchiveId(string Date)
        //{
        //    //string[] DateArr = Date.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        //    //string Year = DateArr[0];
        //    //this.FindByYear(Year);
        //    this.FindLastPriceArchive();
        //    if (this.Count <= 0) return -1;
        //    else return Convert.ToInt32(this[0]["PriceArchiveId"]);
        //}

        public int FindCurrentPriceArchiveId()
        {
            //this.FindByYear(Year);
            this.FindLastPriceArchive();
            if (this.Count <= 0) return -1;
            else return Convert.ToInt32(this[0]["PriceArchiveId"]);
        }

        public void FindLastPriceArchive()
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSPriceArchiveLastOne";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.DataTable.Clear();
            adapter.Fill(this.DataTable);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectActivePriceArchive()
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
            return this.DataTable;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSPriceArchiveForHomePage()
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSPriceArchiveForHomePage";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", 0);
            this.DataTable.Clear();
            adapter.Fill(this.DataTable);
            return this.DataTable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Date"></param>
        /// <returns>PriceArchiveId</returns>
        public int SelectTSPriceArchiveByDate(string Date)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            DataTable dt = new DataTable();
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "SelectTSPriceArchiveByDate";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);           
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
                return Convert.ToInt32(dt.Rows[0]["PriceArchiveId"]);
            else
                return -2;
        }

    }
}
