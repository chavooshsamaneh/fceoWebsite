using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class AccountingDocBalanceDetailManager : BaseObject
    {
        //static AccountingDocBalanceDetailManager()
        //{
        //    _tableId = TableType.AccountingDocBalanceDetail;
        //}

        public AccountingDocBalanceDetailManager()
            : base()
        {

        }
        public AccountingDocBalanceDetailManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingDocBalanceDetail);
        }
        protected override void InitAdapter()
        {

            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Accounting.DocBalanceDetail";
            tableMapping.ColumnMappings.Add("DocBalanceDetailId", "DocBalanceDetailId");
            tableMapping.ColumnMappings.Add("DocBalanceId", "DocBalanceId");
            tableMapping.ColumnMappings.Add("TotalBes", "TotalBes");
            tableMapping.ColumnMappings.Add("TotalBed", "TotalBed");
            tableMapping.ColumnMappings.Add("AccId", "AccId");
            tableMapping.ColumnMappings.Add("PrjId", "PrjId");
            tableMapping.ColumnMappings.Add("CCId", "CCId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ProjectTitle", "ProjectTitle");
            tableMapping.ColumnMappings.Add("AccCode", "AccCode");
            tableMapping.ColumnMappings.Add("AccName", "AccName");
            tableMapping.ColumnMappings.Add("FilePath", "FilePath");
            tableMapping.ColumnMappings.Add("CoId", "CoId");
            tableMapping.ColumnMappings.Add("TTId", "TTId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Comment", "Comment");
            tableMapping.ColumnMappings.Add("BankDocNum", "BankDocNum");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocBalanceDetail";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@DocBalanceId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AccId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@PrjId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AccCode", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@BankDocNum", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.NVarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", SqlDbType.NVarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocBalanceDetail";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DocBalanceDetailId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceDetailId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocBalanceDetail";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocBalanceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TotalBes", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TotalBes", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TotalBed", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TotalBed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CCId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CCId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CoId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CoId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TTId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TTId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Comment", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Comment", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BankDocNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BankDocNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocBalanceDetail";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocBalanceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TotalBes", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TotalBes", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TotalBed", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TotalBed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CCId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CCId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DocBalanceDetailId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceDetailId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocBalanceDetailId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceDetailId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CoId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CoId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TTId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TTId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Comment", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Comment", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BankDocNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BankDocNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }



        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingDocBalanceDetailDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_DocBalanceDetailId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_DocBalanceDetailId));
            if ((Original_LastTimeStamp == null))
            {
                throw new global::System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(int DocBalanceId, decimal TotalBes, decimal TotalBed, int AccId, int PrjId, System.DateTime ModifiedDate)
        {
            this.Adapter.InsertCommand.Parameters[1].Value = ((int)(DocBalanceId));
            this.Adapter.InsertCommand.Parameters[2].Value = ((decimal)(TotalBes));
            this.Adapter.InsertCommand.Parameters[3].Value = ((decimal)(TotalBed));
            this.Adapter.InsertCommand.Parameters[4].Value = ((int)(AccId));
            this.Adapter.InsertCommand.Parameters[5].Value = ((int)(PrjId));
            this.Adapter.InsertCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(int DocBalanceId, decimal TotalBes, decimal TotalBed, int AccId, int PrjId, System.DateTime ModifiedDate, int Original_DocBalanceDetailId, byte[] Original_LastTimeStamp, int DocBalanceDetailId)
        {
            this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(DocBalanceId));
            this.Adapter.UpdateCommand.Parameters[2].Value = ((decimal)(TotalBes));
            this.Adapter.UpdateCommand.Parameters[3].Value = ((decimal)(TotalBed));
            this.Adapter.UpdateCommand.Parameters[4].Value = ((int)(AccId));
            this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(PrjId));
            this.Adapter.UpdateCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Original_DocBalanceDetailId));
            if ((Original_LastTimeStamp == null))
            {
                throw new global::System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(DocBalanceDetailId));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(int DocBalanceId, decimal TotalBes, decimal TotalBed, int AccId, int PrjId, System.DateTime ModifiedDate, int Original_DocBalanceDetailId, byte[] Original_LastTimeStamp)
        {
            return this.Update(DocBalanceId, TotalBes, TotalBed, AccId, PrjId, ModifiedDate, Original_DocBalanceDetailId, Original_LastTimeStamp, Original_DocBalanceDetailId);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByParentId(int DocBalanceId)
        {
            ResetAllParameters();


            if (string.IsNullOrEmpty(DocBalanceId.ToString()))
                DocBalanceId = -1;

            this.Adapter.SelectCommand.Parameters["@DocBalanceId"].Value = DocBalanceId;
            Fill();
            return this.DataTable;
        }

        public DataTable FindByParentIdForReport(int DocBalanceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocBalanceDetailForReport", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(DocBalanceId.ToString()))
                DocBalanceId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@DocBalanceId", DocBalanceId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAccId(int AccId)
        {
            ResetAllParameters();


            if (string.IsNullOrEmpty(AccId.ToString()))
                AccId = -1;

            this.Adapter.SelectCommand.Parameters["@AccId"].Value = AccId;
            Fill();
            return this.DataTable;
        }

        public void FindByBankDocNum(string BankDocNum)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(BankDocNum))
                BankDocNum = "%";

            this.Adapter.SelectCommand.Parameters["@BankDocNum"].Value = BankDocNum;
            Fill();
        }

        public void FindByAccIdBank( int AccId, string FromDate, string ToDate)
        {
            if (string.IsNullOrEmpty(AccId.ToString()))
                AccId = -1;

            if (string.IsNullOrEmpty(FromDate))
                FromDate = "1";

            if (string.IsNullOrEmpty(ToDate))
                ToDate = "2";

            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AccId"].Value = AccId;
            this.Adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByParentIdWithAccAndPrj(int DocBalanceId, int AccId, int PrjId)
        {
            ResetAllParameters();


            if (string.IsNullOrEmpty(DocBalanceId.ToString()))
                DocBalanceId = -1;
            this.Adapter.SelectCommand.Parameters["@DocBalanceId"].Value = DocBalanceId;


            if (string.IsNullOrEmpty(AccId.ToString()))
                AccId = -1;
            this.Adapter.SelectCommand.Parameters["@AccId"].Value = AccId;


            if (string.IsNullOrEmpty(PrjId.ToString()))
                PrjId = -1;
            this.Adapter.SelectCommand.Parameters["@PrjId"].Value = PrjId;


            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByParentIdWithAccCodeAndPrj(int DocBalanceId, string AccCode, int PrjId)
        {
            ResetAllParameters();


            if (string.IsNullOrEmpty(DocBalanceId.ToString()))
                DocBalanceId = -1;
            this.Adapter.SelectCommand.Parameters["@DocBalanceId"].Value = DocBalanceId;

            if (string.IsNullOrEmpty(AccCode))
                AccCode = "%";
            this.Adapter.SelectCommand.Parameters["@AccCode"].Value = AccCode;


            if (string.IsNullOrEmpty(PrjId.ToString()))
                PrjId = -1;
            this.Adapter.SelectCommand.Parameters["@PrjId"].Value = PrjId;


            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable JornalSelect(int AccTypeId, string DocDate, string DocDateFrom, string DocDateTo, string ProjectTitle, int TTId, int AgentId, int DocStatusId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportDocBalanceDetailJornal", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AccTypeId.ToString()))
                AccTypeId = -1;

            if (string.IsNullOrEmpty(DocDate))
                DocDate = "%";

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(ProjectTitle))
                ProjectTitle = "%";

            if (string.IsNullOrEmpty(TTId.ToString()))
                TTId = -1;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeId", AccTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDate", DocDate);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectTitle", ProjectTitle);
            adapter.SelectCommand.Parameters.AddWithValue("@TTId", TTId);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable ReportJornal(int AccTypeId, string DocDate, string DocDateFrom, string DocDateTo, int DocNoFrom, int DocNoTo, int AccLevele, int AgentId, int TTId, int DocStatusId
            ,string AgentParm,int CalculateType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportDocBalanceDetailJornal", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AccTypeId.ToString()))
                AccTypeId = -1;

            if (string.IsNullOrEmpty(DocDate))
                DocDate = "%";
            if (string.IsNullOrEmpty(AgentParm))
                AgentParm = "0";
            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";


            if (string.IsNullOrEmpty(TTId.ToString()))
                TTId = -1;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(AccLevele.ToString()) || AccLevele<-1)
                AccLevele = -1;

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeId", AccTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDate", DocDate);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNoFrom", DocNoFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNoTo", DocNoTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AccLevele", AccLevele);
            //adapter.SelectCommand.Parameters.AddWithValue("@ProjectTitle", ProjectTitle);
            adapter.SelectCommand.Parameters.AddWithValue("@TTId", TTId);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentParm", AgentParm);
            adapter.SelectCommand.Parameters.AddWithValue("@CalculateType", CalculateType);
            
            adapter.Fill(dt);
            return (dt);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable ReportDocBalanceSummary(string DocDateFrom, string DocDateTo, int DocNumberFrom, int DocNumberTo, int TTId, string AgentParm, int DocStatusId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportDocBalanceDetailSummary", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AgentParm))
                AgentParm = "0";
            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(DocNumberFrom.ToString()))
                DocNumberFrom = -1;

            if (string.IsNullOrEmpty(DocNumberTo.ToString()))
                DocNumberTo = -1;

            if (string.IsNullOrEmpty(TTId.ToString()))
                TTId = -1;


            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberFrom", DocNumberFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberTo", DocNumberTo);
            adapter.SelectCommand.Parameters.AddWithValue("@TTId", TTId);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentParm", AgentParm);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable BookSelect(int AccId, string DocDateFrom, string DocDateTo, int AgentId, int DocStatusId,string K,string M, int DocNumberFrom, int DocNumberTo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportDocBalanceDetailBook", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AccId.ToString()))
                AccId = -1;

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            if (string.IsNullOrEmpty(K))
                DocDateTo = "%";

            if (string.IsNullOrEmpty(M))
                DocDateTo = "%";

            if (string.IsNullOrEmpty(DocNumberFrom.ToString()))
                DocNumberFrom = -1;

            if (string.IsNullOrEmpty(DocNumberTo.ToString()))
                DocNumberTo = -1;

            dt.Columns.Add("Balance", typeof(decimal));
            adapter.SelectCommand.Parameters.AddWithValue("@AccId", AccId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);
            adapter.SelectCommand.Parameters.AddWithValue("@K", K);
            adapter.SelectCommand.Parameters.AddWithValue("@M", M);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberFrom", DocNumberFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberTo", DocNumberTo);

            adapter.Fill(dt);

            decimal Balance = PreBalanceSelect(AccId, DocDateFrom);
            dt.Rows[0]["Balance"] = 0;
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                decimal totalbes = (decimal)(dt.Rows[i]["TotalBes"]);
                decimal totalbed = (decimal)(dt.Rows[i]["TotalBed"]);
                Balance = Balance + (totalbes - totalbed);
                dt.Rows[i]["Balance"] = Balance;
            }

            return (dt);
        }

        public decimal PreBalanceSelect(int AccId, string DocDateFrom)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectPreBalance", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AccId.ToString()))
                AccId = -1;

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            adapter.SelectCommand.Parameters.AddWithValue("@AccId", AccId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);

            adapter.Fill(dt);

            decimal Balance = (decimal)(dt.Rows[0]["Balance"]);

            return Balance;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable BalanceSelect(int AccTypeId, string DocDateFrom, string DocDateTo, int AgentId, int DocNumberFrom, int DocNumberTo, int DocStatusId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportDocBalanceDetailBalance", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AccTypeId.ToString()))
                AccTypeId = -1;

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(DocNumberFrom.ToString()))
                DocNumberFrom = -1;

            if (string.IsNullOrEmpty(DocNumberTo.ToString()))
                DocNumberTo = -1;

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            dt.Columns.Add("Balance", typeof(decimal));
            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeId", AccTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberFrom", DocNumberFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberTo", DocNumberTo);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable BenefitSelect(string DocDateFrom, string DocDateTo, int AgentId, int DocNumberFrom, int DocNumberTo, int DocStatusId, int AccTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportDocBalanceDetailBenefit", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(DocNumberFrom.ToString()))
                DocNumberFrom = -1;

            if (string.IsNullOrEmpty(DocNumberTo.ToString()))
                DocNumberTo = -1;

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            if (string.IsNullOrEmpty(AccTypeId.ToString()))
                AccTypeId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberFrom", DocNumberFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberTo", DocNumberTo);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);
            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeId", AccTypeId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable BenefitDetailSelect(string DocDateFrom, string DocDateTo, int AgentId, int DocNumberFrom, int DocNumberTo, int AccGrpId, int DocStatusId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportDocBalancelBenefitDetai", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(DocNumberFrom.ToString()))
                DocNumberFrom = -1;

            if (string.IsNullOrEmpty(DocNumberTo.ToString()))
                DocNumberTo = -1;

            if (string.IsNullOrEmpty(AccGrpId.ToString()))
                AccGrpId = -1;

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberFrom", DocNumberFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocNumberTo", DocNumberTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AccGrpId", AccGrpId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable Balance3Select(string DocDateFrom, string DocDateTo, int AgentId, int DocStatusId, int Level)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportDocBalanceDetailBalance3", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            
            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            if (string.IsNullOrEmpty(Level.ToString()))
                Level = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);
            adapter.SelectCommand.Parameters.AddWithValue("@Level", Level);            

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable Balance3Select2(string DocDateFrom, string DocDateTo, int AgentId, int DocStatusId, int Level)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReaportDocBalanceDetailBalance3_2", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(DocStatusId.ToString()))
                DocStatusId = -1;

            if (string.IsNullOrEmpty(Level.ToString()))
                Level = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@DocDateFrom", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DocDateTo", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@DocStatusId", DocStatusId);
            adapter.SelectCommand.Parameters.AddWithValue("@Level", Level);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable Balance8Columns(string DocDateFrom, string DocDateTo, int AgentId,int AccTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportTestBalance8Columns", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(DocDateFrom))
                DocDateFrom = "1";

            if (string.IsNullOrEmpty(DocDateTo))
                DocDateTo = "2";

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(AccTypeId.ToString()))
                AccTypeId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", DocDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", DocDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeId", AccTypeId);

            adapter.Fill(dt);
            return (dt);
        }

        public decimal GetAccountBalance(int AccId,string ToDate)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spSelectAccountBalance]";

            c.Parameters.Add("@AccId", SqlDbType.Int, 4);
            c.Parameters.Add("@ToDate", SqlDbType.NVarChar, 50);
            c.Parameters.Add("@Balance", SqlDbType.Money, 50);

            c.Parameters["@AccId"].Direction = ParameterDirection.Input;
            c.Parameters["@ToDate"].Direction = ParameterDirection.Input;
            c.Parameters["@Balance"].Direction = ParameterDirection.Output;

            c.Parameters["@AccId"].Value = AccId;
            c.Parameters["@ToDate"].Value = ToDate;

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            c.ExecuteNonQuery();

            if (c.Parameters["@Balance"].Value == null)
                return 0;
            else
                return Convert.ToDecimal(c.Parameters["@Balance"].Value);
        }
        /************************************************************** Period *******************************************************************************/

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodDocBalanceDetail(int PeriodId, int DocBalanceId)
        {
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";

                string Command = "SELECT " + DocBalanceDetail + ".*,[Accounting.Project].ProjectTitle,[Accounting.Account].AccCode,[Accounting.Account].AccName" +
                                 " FROM " + DocBalanceDetail +
                                 " INNER JOIN [Accounting.Project] ON " + DocBalanceDetail + ".PrjId=[Accounting.Project].PrjId" +
                                 " INNER JOIN [Accounting.Account] ON " + DocBalanceDetail + ".AccId=[Accounting.Account].AccId" +
                                 " WHERE " + DocBalanceId + "=-1 OR" + DocBalanceDetail + ".DocBalanceId=" + DocBalanceId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        public DataTable FindPeriodByParentIdForReport(int PeriodId, int DocBalanceId)
        {
            DataTable dt = new DataTable();

            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = "(SELECT " + DocBalanceDetail + ".DocBalanceDetailId," + DocBalanceDetail + ".DocBalanceId,'' TotalBes,'' TotalBed," + DocBalanceDetail + ".Comment,[Accounting.Account].AccCode,(SELECT Ac.AccName FROM [Accounting.Account] as Ac WHERE Ac.AccCode=([Accounting.Account].K+'-'+[Accounting.Account].M))+'-'+[Accounting.Account].AccName AccName,[Accounting.Account].K,(case when " + DocBalanceDetail + ".TotalBes>0 then " + DocBalanceDetail + ".TotalBes else (-" + DocBalanceDetail + ".TotalBed) end)  Balance" +
	                             " FROM "+DocBalanceDetail+
                                 " INNER JOIN [Accounting.Project] ON " + DocBalanceDetail + ".PrjId=[Accounting.Project].PrjId" +
                                 " INNER JOIN [Accounting.Account] ON "+DocBalanceDetail+".AccId=[Accounting.Account].AccId  "+ 
                                 " INNER JOIN "+DocBalance+" ON "+DocBalanceDetail+".DocBalanceId="+DocBalance+".DocBalanceId"+
	                             " WHERE ("+DocBalanceId+"=-1 OR "+DocBalanceDetail+".DocBalanceId="+DocBalanceId+"))"+
                                 " UNION"+
                                 " (SELECT  '' DocBalanceDetailId,'' DocBalanceId,SUM(TotalBes),SUM(TotalBed),'***********'Comment,[Accounting.Account].K AccCode,(SELECT Ac2.AccName FROM [Accounting.Account] as Ac2 WHERE Ac2.AccCode=[Accounting.Account].K) AccName,[Accounting.Account].K,'' Balance"+
                                 " FROM "+DocBalanceDetail+
                                 " INNER JOIN [Accounting.Project] ON "+DocBalanceDetail+".PrjId=[Accounting.Project].PrjId"+
                                 " INNER JOIN [Accounting.Account] ON "+DocBalanceDetail+".AccId=[Accounting.Account].AccId "+  
                                 " INNER JOIN "+DocBalance+" ON "+DocBalanceDetail+".DocBalanceId="+DocBalance+".DocBalanceId"+                                 
                                 " WHERE ("+DocBalanceId+"=-1 OR "+DocBalanceDetail+".DocBalanceId="+DocBalanceId+")"+
                                 " GROUP BY K )"+
                                 " ORDER BY K";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                this.Adapter.Fill(dt);
            }
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable PeriodJornalSelect(int PeriodId,int AccTypeId, string DocDate, string DocDateFrom, string DocDateTo, string ProjectTitle, int TTId, int AgentId, int DocStatusId)
        {
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = "BEGIN IF (" + AccTypeId + " =-1 or " + AccTypeId + " =4)" +
                                " SELECT " + DocBalanceDetail + ".*,[Accounting.Project].ProjectTitle,[Accounting.Account].AccCode,[Accounting.Account].AccName," + DocBalance + ".DocDate,[Accounting.TT].*," + DocOperation + ".*" +
                                " FROM " + DocBalanceDetail +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " INNER JOIN [Accounting.Project] ON " + DocBalanceDetail + ".PrjId=[Accounting.Project].PrjId" +
                                " INNER JOIN [Accounting.Account] ON " + DocBalanceDetail + ".AccId=[Accounting.Account].AccId" +
                                " INNER JOIN [Accounting.TT] ON " + DocBalance + ".TTId=[Accounting.TT].TTId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + DocBalance + ".DocDate Like '" + DocDate + "')" +
                                " AND ([Accounting.Project].ProjectTitle Like '" + ProjectTitle + "')" +
                                " AND (" + TTId + "=-1 OR " + DocBalance + ".TTId=" + TTId + ")" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocStatusId + "=-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " ORDER BY " + DocBalance + ".DocDate END" +
                                " BEGIN IF (" + AccTypeId + " =1)" +
                                " SELECT " + DocBalanceDetail + ".*,[Accounting.Project].ProjectTitle,isnull((select AccCode from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=1 ),[Accounting.Account].AccCode) AccCode,isnull((select AccName from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode)) and b.AccTypeId=1 ),[Accounting.Account].AccName) AccName," + DocBalance + ".DocDate,[Accounting.TT].*," + DocOperation + ".*" +
                                " FROM " + DocBalanceDetail +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " INNER JOIN [Accounting.Project] ON " + DocBalanceDetail + ".PrjId=[Accounting.Project].PrjId" +
                                " INNER JOIN [Accounting.Account] ON " + DocBalanceDetail + ".AccId=[Accounting.Account].AccId" +
                                " INNER JOIN [Accounting.TT] ON " + DocBalance + ".TTId=[Accounting.TT].TTId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + DocBalance + ".DocDate Like '" + DocDate + "')" +
                                " AND ([Accounting.Project].ProjectTitle Like '" + ProjectTitle + "')" +
                                " AND (" + TTId + "=-1 OR " + DocBalance + ".TTId=" + TTId + ")" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocStatusId + "=-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " ORDER BY " + DocBalance + ".DocDate END" +
                                " BEGIN IF (" + AccTypeId + " =2)" +
                                " SELECT " + DocBalanceDetail + ".*,[Accounting.Project].ProjectTitle,isnull((select AccCode from [Accounting.Account] b where b.AccId=[Accounting.Account].ParentId ),[Accounting.Account].AccCode) AccCode,isnull((select AccName from [Accounting.Account] b where b.AccId=[Accounting.Account].ParentId ),[Accounting.Account].AccName) AccName," + DocBalance + ".DocDate,[Accounting.TT].*," + DocOperation + ".*" +
                                " FROM " + DocBalanceDetail +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " INNER JOIN [Accounting.Project] ON " + DocBalanceDetail + ".PrjId=[Accounting.Project].PrjId" +
                                " INNER JOIN [Accounting.Account] ON " + DocBalanceDetail + ".AccId=[Accounting.Account].AccId" +
                                " INNER JOIN [Accounting.TT] ON " + DocBalance + ".TTId=[Accounting.TT].TTId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + DocBalance + ".DocDate Like '" + DocDate + "')" +
                                " AND ([Accounting.Project].ProjectTitle Like '" + ProjectTitle + "')" +
                                " AND (" + TTId + "=-1 OR " + DocBalance + ".TTId=" + TTId + ")" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocStatusId + "=-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " ORDER BY " + DocBalance + ".DocDate END";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                try
                {
                    Fill();
                }
                catch (Exception err)
                {
                }
            }
            return this.DataTable;            
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable PeriodBookSelect(int PeriodId, int AccId, string DocDateFrom, string DocDateTo, int AgentId, int DocStatusId, string K, string M, int DocNumberFrom, int DocNumberTo)
        {
            DataTable dt = new DataTable();
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";
                
                

                this.DataTable.Columns.Add("Balance", typeof(decimal));

                string Command = " Declare @PreBalance money " +
                                " Declare @Balance money " +
                                " set @Balance=0 " +
                                " SET @PreBalance=(SELECT  isnull(SUM(" + DocBalanceDetail + ".TotalBes-" + DocBalanceDetail + ".TotalBed),0)" +
                                " FROM " + DocBalanceDetail +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " INNER JOIN [Accounting.Account] ON " + DocBalanceDetail + ".AccId=[Accounting.Account].AccId" +
                                " WHERE " + DocBalance + ".DocDate<'" + DocDateFrom + "'" +
                                " AND (" + DocNumberFrom + "=-1 OR cast(" + DocOperation + ".DocNumber as int) < " + DocNumberFrom + ")" +
                                " AND ((" + AccId + " =-1 AND '" + K + "'='%' AND '" + M + "'='%') OR (" + DocBalanceDetail + ".AccId=" + AccId + ")OR (" + AccId + " =-1 AND '" + K + "'<>'%' AND '" + K + "' IS NOT NULL AND '" + K + "'=K AND '" + M + "'<>'%' AND '" + M + "' IS NOT NULL AND '" + M + "'=M) OR (" + AccId + " =-1 AND '" + K + "'<>'%' AND '" + K + "' IS NOT NULL AND '" + K + "'=K AND ('" + M + "'='%' OR '" + M + "' IS NULL)) )" +
                                " AND (" + DocBalance + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " AND (" + DocStatusId + "=-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + "))" +
                                " SELECT * FROM (" +
                                " SELECT NULL as DocBalanceDetailId,(case when @PreBalance<0 then 0 else @PreBalance end) TotalBes ,(case when @PreBalance<0 then abs(@PreBalance) else 0 end) TotalBed ,NULL as AccId,NULL as AccCode,NULL as AccName,NULL as DocDate,NULL as TTId,NULL as Title,N'مانده از قبل' as Description,NULL as DocNumber,NULL as AgentId,0 as Balance " +
                                " UNION " +
                                " SELECT " + DocBalanceDetail + ".DocBalanceDetailId," + DocBalanceDetail + ".TotalBes," + DocBalanceDetail + ".TotalBed," + DocBalanceDetail + ".AccId,[Accounting.Account].AccCode,[Accounting.Account].AccName," + DocBalance + ".DocDate,[Accounting.TT].*," + DocOperation + ".Description," + DocOperation + ".DocNumber," + DocOperation + ".AgentId,@Balance as Balance" +
                                " FROM  " + DocBalanceDetail +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " INNER JOIN [Accounting.Project] ON " + DocBalanceDetail + ".PrjId=[Accounting.Project].PrjId" +
                                " INNER JOIN [Accounting.Account] ON " + DocBalanceDetail + ".AccId=[Accounting.Account].AccId" +
                                " INNER JOIN [Accounting.TT] ON " + DocBalance + ".TTId=[Accounting.TT].TTId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND ((" + AccId + " =-1 AND '" + K + "'='%' AND '" + M + "'='%') OR (" + DocBalanceDetail + ".AccId=" + AccId + ")OR (" + AccId + " =-1 AND '" + K + "'<>'%' AND '" + K + "' IS NOT NULL AND '" + K + "'=K AND '" + M + "'<>'%' AND '" + M + "' IS NOT NULL AND '" + M + "'=M) OR (" + AccId + " =-1 AND '" + K + "'<>'%' AND '" + K + "' IS NOT NULL AND '" + K + "'=K AND ('" + M + "'='%' OR '" + M + "' IS NULL)) )" +
                                " AND (" + DocStatusId + "=-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                ") t ORDER BY DocDate";
                
                SqlDataAdapter adapter = new SqlDataAdapter(Command, this.Connection);
                adapter.SelectCommand.CommandType = CommandType.Text;

                try
                {
                    adapter.Fill(dt);

                    decimal Balance = PreBalanceSelect(PeriodId, AccId, DocDateFrom);
                    dt.Rows[0]["Balance"] = 0;
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        decimal totalbes = (decimal)(dt.Rows[i]["TotalBes"]);
                        decimal totalbed = (decimal)(dt.Rows[i]["TotalBed"]);
                        Balance = Balance + (totalbes - totalbed);
                        dt.Rows[i]["Balance"] = Balance;
                    }
                }
                catch (Exception err)
                {
                }                
            }
            return (dt);
        }

        public decimal PreBalanceSelect(int PeriodId, int AccId, string DocDateFrom)
        {
            decimal Balance =0;
            DataTable dt = new DataTable();
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = " SELECT isnull(SUM(" + DocBalanceDetail + ".TotalBes-" + DocBalanceDetail + ".TotalBed),0) AS Balance " +
                                 " FROM " + DocBalanceDetail +
                                 " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId " +
                                 " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId " +
                                 " WHERE " + DocBalance + ".DocDate< " + DocDateFrom +
                                 " AND(" + AccId + " =-1 OR " + DocBalanceDetail + ".AccId=" + AccId + ")" +
                                 " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                 " AND (" + DocOperation + ".InActive=0)";

                SqlDataAdapter adapter = new SqlDataAdapter(Command, this.Connection);
                adapter.SelectCommand.CommandType = CommandType.Text;

                try
                {
                    adapter.Fill(dt);
                    Balance = (decimal)(dt.Rows[0]["Balance"]);
                }
                catch (Exception err)
                {
                }
            }
            return Balance;
        }

        public decimal PeriodPreBalanceSelect(int PeriodId, int AccId, string DocDateFrom)
        {
            decimal Balance = 0;
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = " SELECT isnull(SUM(" + DocBalanceDetail + ".TotalBes-" + DocBalanceDetail + ".TotalBed),0) AS Balance " +
                                " FROM " + DocBalanceDetail +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE	" + DocBalance + ".DocDate<'" + DocDateFrom + "'" +
                                " AND(" + AccId + "=-1 OR " + DocBalanceDetail + ".AccId=" + AccId + ")" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " AND (" + DocOperation + ".InActive=0)";

                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                try
                {
                    Fill();
                }
                catch (Exception err)
                {
                }

                Balance = (decimal)(this.DataTable.Rows[0]["Balance"]);
                
            }
            return Balance;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable PeriodBalanceSelect(int PeriodId, int AccTypeId, string DocDateFrom, string DocDateTo, int AgentId, int DocNumberFrom, int DocNumberTo, int DocStatusId)
        {
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = "BEGIN IF(" + AccTypeId + "=-1 or " + AccTypeId + "=4)" +
                                " SELECT SUM(" + DocBalanceDetail + ".TotalBes) AS TotalBes, SUM(" + DocBalanceDetail + ".TotalBed) AS TotalBed,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed, SUM(TotalBes-TotalBed) Balance,[Accounting.Account].AccId,[Accounting.Account].AccName, [Accounting.Account].AccCode" +
                                " FROM [Accounting.Account]" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY [Accounting.Account].AccName, [Accounting.Account].AccCode,[Accounting.Account].AccId" +
                                " ORDER BY [Accounting.Account].AccCode" +
                                " END" +
                                " BEGIN IF(" + AccTypeId + " =1)" +
                                " SELECT SUM(TotalBes)  TotalBes,SUM(TotalBed) TotalBed ,SUM(Bes) Bes,SUM(Bed) Bed,SUM(Balance) Balance ,AccId,AccName,AccCode FROM(" +
                                " SELECT SUM(" + DocBalanceDetail + ".TotalBes) AS TotalBes, SUM(" + DocBalanceDetail + ".TotalBed) AS TotalBed" +
                                " ,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes" +
                                " ,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed, SUM(TotalBes-TotalBed) Balance" +
                                " ,isnull((select AccName from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode)) and b.AccTypeId=1 ),[Accounting.Account].AccName) AccName, isnull((select AccCode from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=1 ),[Accounting.Account].AccCode) AccCode " +
                                " ,isnull((select AccId from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=1 ),[Accounting.Account].AccId) AccId " +
                                " FROM [Accounting.Account]" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.Account].AccCode,[Accounting.Account].AccId,[Accounting.Account].AccName)T" +
                                " GROUP BY   AccCode,AccId,AccName" +
                                " ORDER BY   AccCode" +
                                " END" +
                                " BEGIN IF(" + AccTypeId + " =2)" +
                                " SELECT SUM(TotalBes)  TotalBes,SUM(TotalBed) TotalBed ,SUM(Bes) Bes,SUM(Bed) Bed,SUM(Balance) Balance ,AccId,AccName,AccCode FROM(" +
                                " SELECT SUM(" + DocBalanceDetail + ".TotalBes) AS TotalBes, SUM(" + DocBalanceDetail + ".TotalBed) AS TotalBed" +
                                " ,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes" +
                                " ,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed, SUM(TotalBes-TotalBed) Balance" +
                                " ,isnull((select AccName from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode)) and b.AccTypeId=2 ),[Accounting.Account].AccName) AccName, isnull((select AccCode from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=2 ),[Accounting.Account].AccCode) AccCode " +
                                " ,isnull((select AccId from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=2 ),[Accounting.Account].AccId) AccId " +
                                " FROM [Accounting.Account]" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.Account].AccCode,[Accounting.Account].AccId,[Accounting.Account].AccName)T" +
                                " GROUP BY   AccCode,AccId,AccName" +
                                " ORDER BY   AccCode" +
                                " END";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                try
                {
                    Fill();
                }
                catch (Exception err)
                { 
                }
            }
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable PeriodBenefitSelect(int PeriodId, string DocDateFrom, string DocDateTo, int AgentId, int DocNumberFrom, int DocNumberTo, int DocStatusId,int AccTypeId)
        {
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = " SELECT * FROM (" +
                                 " SELECT  '' TotalBalance,'' Balance,'' AccGrpId,N'درآمد فروش' Title,'' GroupName,'' AccName, '' AccId, '' AccCode,'' Bes,'' Bed,'1' GrpStatusId,'5' GroupNatureId " +
                                 " UNION " +
                                 " SELECT '' TotalBalance,'' Balance,'' AccGrpId,N'هزینه های عملیاتی' Title,'' GroupName,'' AccName, '' AccId, '' AccCode,'' Bes,'' Bed,'2' GrpStatusId,'4' GroupNatureId " +
                                 " UNION " +
                                 " SELECT '' TotalBalance,'' Balance,'' AccGrpId,N'هزینه قیمت تمام شده كالای فروش رفته' Title,'' GroupName,'' AccName, '' AccId, '' AccCode,'' Bes,'' Bed,'2' GrpStatusId,'7' GroupNatureId " +
                                 " UNION " +
                                 " SELECT '' TotalBalance,SUM(TotalBes-TotalBed) Balance,99999999 AccGrpId,'' Title,'' GroupName,N'سود ناخالص' AccName, '' AccId, '' AccCode,'' Bes,'' Bed,'2' GrpStatusId,'7' GroupNatureId" +
                                 " FROM [Accounting.AccGroup] " +
                                 " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId " +
                                 " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                 " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId " +
                                 " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId " +
                                 " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                 " AND (" + AgentId + "=-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                 " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                 " AND [Accounting.AccGroup].GrpTypeId=2 " +
                                 " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                 " AND (" + DocOperation + ".InActive=0) " +
                                 " AND ([Accounting.AccGroup].GroupNatureId=5 OR [Accounting.AccGroup].GroupNatureId=7) " +
                                 " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                 " UNION " +
                                 " SELECT '' TotalBalance,SUM(TotalBes-TotalBed) Balance,9999999 AccGrpId,'' Title,'' GroupName,N'جمع' AccName, '' AccId, '' AccCode,'' Bes,'' Bed,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId " +
                                 " FROM [Accounting.AccGroup] " +
                                 " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId " +
                                 " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId  " +
                                 " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                 " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                 " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                 " AND (" + AgentId + "=-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                 " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                 " AND [Accounting.AccGroup].GrpTypeId=2 " +
                                 " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                 " AND (" + DocOperation + ".InActive=0) " +
                                 " GROUP BY   [Accounting.AccGroup].AccGrpId,[Accounting.AccGroup].GroupName,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId " +
                                 /*" UNION " +
                                 " SELECT SUM(TotalBes-TotalBed) TotalBalance,'' Balance,[Accounting.AccGroup].AccGrpId,'' Title,[Accounting.AccGroup].GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId " +
                                 " FROM [Accounting.AccGroup]" +
                                 " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId " +
                                 " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId  " +
                                 " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                 " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                 " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                 " AND (" + AgentId + "=-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                 " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                 " AND [Accounting.AccGroup].GrpTypeId=2 " +
                                 " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                 " AND (" + DocOperation + ".InActive=0) " +
                                 " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                 " GROUP BY   [Accounting.AccGroup].AccGrpId,[Accounting.AccGroup].GroupName,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId " +*/
                                 " UNION" +
                                 " SELECT '' TotalBalance,SUM(Balance) Balance,9999999 AccGrpId,'' Title,'' GroupName,N'سود (زیان) خالص' AccName, '' AccId, '' AccCode,0 Bes,0 Bed,99999999 GrpStatusId,0 GroupNatureId FROM(" +
                                 " SELECT '' TotalBalance,SUM(TotalBes-TotalBed) Balance,9999999 AccGrpId,'' Title,'' GroupName,N'جمع' AccName, '' AccId, '' AccCode,NULL Bes,NULL Bed,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId" +
                                 " FROM [Accounting.AccGroup]" +
                                 " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId " +
                                 " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                 " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                 " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                 " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                 " AND (" + AgentId + "=-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                 " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                 " AND [Accounting.AccGroup].GrpTypeId=2 " +
                                 " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                 " AND (" + DocOperation + ".InActive=0) " +
                                 " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                 " GROUP BY   [Accounting.AccGroup].AccGrpId,[Accounting.AccGroup].GroupName,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId)T5" +
                                 " UNION" +
                                 " SELECT '' TotalBalance,/*SUM(Balance)*/0 Balance ,AccGrpId,''Title,'' GroupName,AccName,AccId,AccCode,(case when SUM(Balance)<0 then 0 else ABS(SUM(Balance)) end) Bes,(case when SUM(Balance)<0 then ABS(SUM(Balance)) else 0 end) Bed,GrpStatusId,GroupNatureId FROM(" +
                                 " SELECT SUM(TotalBes-TotalBed) Balance,isnull((select AccId from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=" + AccTypeId + " ),[Accounting.Account].AccId) AccId,isnull((select AccCode from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=" + AccTypeId + " ),[Accounting.Account].AccCode) AccCode,isnull((select AccName from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode)) and b.AccTypeId=" + AccTypeId + " ),[Accounting.Account].AccName) AccName,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId" +
                                 " FROM [Accounting.Account]" +
                                 " INNER JOIN [Accounting.AccGroup] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                 " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                 " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                 " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                 " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                 " AND (" + AgentId + "=-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                 " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                 " AND [Accounting.AccGroup].GrpTypeId=2 " +
                                 " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                 " AND (" + DocOperation + ".InActive=0) " +
                                 " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                 " GROUP BY [Accounting.Account].AccName,[Accounting.Account].AccId,[Accounting.Account].AccCode,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId)T " +
                                 " GROUP BY   AccName,AccId,AccCode,AccGrpId,GrpStatusId,GroupNatureId)T2 " +
                                 " ORDER BY   GrpStatusId,GroupNatureId desc,AccGrpId,groupname  desc,AccName,AccId  desc";

                                 //" UNION " +
                                 //" SELECT '' TotalBalance,SUM(Balance) Balance ,AccGrpId,''Title,'' GroupName,AccName,AccId,AccCode,'' Bes,'' Bed,GrpStatusId,GroupNatureId FROM( " +
                                 //" SELECT SUM(TotalBes-TotalBed) Balance,isnull((select AccId from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=1 ),[Accounting.Account].AccId) AccId,isnull((select AccCode from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=1 ),[Accounting.Account].AccCode) AccCode,isnull((select AccName from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode)) and b.AccTypeId=1 ),[Accounting.Account].AccName) AccName,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId " +
                                 //" FROM [Accounting.Account] " +
                                 //" INNER JOIN [Accounting.AccGroup] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId " +
                                 //" INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                 //" INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                 //" INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                 //" WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                 //" AND (" + AgentId + "=-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                 //" AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                 //" AND [Accounting.AccGroup].GrpTypeId=2 " +
                                 //" AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                 //" AND (" + DocOperation + ".InActive=0) " +
                                 //" GROUP BY   [Accounting.Account].AccName,[Accounting.Account].AccId,[Accounting.Account].AccCode,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GrpStatusId,[Accounting.AccGroup].GroupNatureId	)T " +
                                 //" GROUP BY   AccName,AccId,AccCode,AccGrpId,GrpStatusId,GroupNatureId	)T2 " +
                                 //" ORDER BY   GrpStatusId,GroupNatureId desc,AccGrpId,groupname  desc,AccName,AccId  desc";

                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                try
                {
                    Fill();
                }
                catch (Exception err)
                {
                }
            }
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable PeriodBenefitDetailSelect(int PeriodId, string DocDateFrom, string DocDateTo, int AgentId, int DocNumberFrom, int DocNumberTo, int AccGrpId, int DocStatusId)
        {
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = " SELECT SUM(Bes)Bes,SUM(Bed)Bed,SUM(Balance) Balance ,AccGrpId,AccName,AccId,AccCode,GroupName FROM(" +
                                " SELECT SUM(TotalBes-TotalBed) Balance,[Accounting.Account].AccId,[Accounting.Account].AccCode, AccName,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GroupName," +
                                "(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed" +
                                " FROM [Accounting.Account]" +
                                " INNER JOIN [Accounting.AccGroup] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND ((" + DocNumberFrom + "=-1 AND " + DocNumberTo + "=-1) OR cast(" + DocOperation + ".DocNumber as int) between " + DocNumberFrom + " and " + DocNumberTo + ")" +
                                " AND [Accounting.AccGroup].GrpTypeId=2" +
                                " AND ("+AccGrpId+"=-1 OR [Accounting.AccGroup].AccGrpId="+AccGrpId+")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)"+
                                " GROUP BY [Accounting.Account].AccName,[Accounting.Account].AccId,[Accounting.Account].AccCode,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GroupName)T" +
                                " GROUP BY AccName,AccId,AccCode,AccGrpId,GroupName";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                try
                {
                    Fill();
                }
                catch (Exception err)
                {
                }
            }
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable PeriodBalance3Select(int PeriodId, string DocDateFrom, string DocDateTo, int AgentId, int DocStatusId, int Level)
        {
            DataTable dt = new DataTable();
            if (PeriodId != -1)
            {                
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                string Command = "BEGIN IF(" + Level + "=1 OR " + Level + "=-1)" +
                                " SELECT * FROM (" +
                                " SELECT '' TotalBalance,'' Balance,'' AccGrpId,N'دارایی ها' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'3' GroupNatureId" +
                                " UNION" +
                                " SELECT '' TotalBalance,'' Balance,'' AccGrpId,N'بدهی ها' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'2' GroupNatureId" +
                                " UNION " +
                                " SELECT '' TotalBalance,'' Balance,'' AccGrpId,N'سرمایه' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'1' GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع دارایی ها' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup]" +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=3)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT     isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع بدهی ها' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup]" +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=2)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع سرمایه' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup] " +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع بدهی و سرمایه' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,0 GroupNatureId" +
                                " FROM [Accounting.AccGroup]" +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1 OR [Accounting.AccGroup].GroupNatureId=2 )" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " HAVING isnull(SUM(TotalBes-TotalBed),0)<>0" +
                                " UNION " +
                                " SELECT SUM(TotalBes-TotalBed) TotalBalance,0 Balance,[Accounting.AccGroup].AccGrpId,'' Title,[Accounting.AccGroup].GroupName,'' AccName, NULL  AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup]" +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1 OR [Accounting.AccGroup].GroupNatureId=2 OR [Accounting.AccGroup].GroupNatureId=3)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY  [Accounting.AccGroup].AccGrpId,[Accounting.AccGroup].GroupName,[Accounting.AccGroup].GroupNatureId)T2" +
                                " ORDER BY   GroupNatureId DESC,AccGrpId,AccName" +
                                " END" +
                                " BEGIN IF(" + Level + "=2)" +
                                " SELECT * FROM (" +
                                " SELECT     '' TotalBalance,'' Balance,'' AccGrpId,N'دارایی ها' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'3' GroupNatureId" +
                                " UNION " +
                                " SELECT     '' TotalBalance,'' Balance,'' AccGrpId,N'بدهی ها' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'2' GroupNatureId" +
                                " UNION " +
                                " SELECT     '' TotalBalance,'' Balance,'' AccGrpId,N'سرمایه' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'1' GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع دارایی ها' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup] " +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=3)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع بدهی ها' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup] " +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=2)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع سرمایه' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup] " +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع بدهی و سرمایه' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,0 GroupNatureId" +
                                " FROM [Accounting.AccGroup]" +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1 OR [Accounting.AccGroup].GroupNatureId=2 )" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " HAVING isnull(SUM(TotalBes-TotalBed),0)<>0" +
                                " UNION " +
                                " SELECT     SUM(TotalBes-TotalBed) TotalBalance,'' Balance,[Accounting.AccGroup].AccGrpId,'' Title,[Accounting.AccGroup].GroupName,'' AccName, NULL  AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup]" +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1 OR [Accounting.AccGroup].GroupNatureId=2 OR [Accounting.AccGroup].GroupNatureId=3)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].AccGrpId,[Accounting.AccGroup].GroupName,[Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT '' TotalBalance,SUM(Balance) Balance ,AccGrpId,''Title,'' GroupName,AccName,AccId,AccCode,'' Bes,'' Bed,GroupNatureId FROM(" +
                                " SELECT SUM(TotalBes-TotalBed) Balance,isnull((select AccId from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=1 ),[Accounting.Account].AccId) AccId,isnull((select AccCode from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode))  and b.AccTypeId=1 ),[Accounting.Account].AccCode) AccCode,isnull((select AccName from [Accounting.Account] b where b.AccCode = substring([Accounting.Account].AccCode,1,len(b.AccCode)) and b.AccTypeId=1 ),[Accounting.Account].AccName) AccName,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GroupNatureId" +
                                " FROM  [Accounting.Account]" +
                                " INNER JOIN [Accounting.AccGroup] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1 OR [Accounting.AccGroup].GroupNatureId=2 OR [Accounting.AccGroup].GroupNatureId=3)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.Account].AccName,[Accounting.Account].AccId,[Accounting.Account].AccCode,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GroupNatureId)T" +
                                " GROUP BY   AccName,AccId,AccCode,AccGrpId,GroupNatureId)T2" +
                                " ORDER BY   GroupNatureId DESC,AccGrpId,AccName" +
                                " END " +
                                " BEGIN IF(" + Level + "=3)" +
                                " SELECT * FROM (" +
                                " SELECT     '' TotalBalance,'' Balance,'' AccGrpId,N'دارایی ها' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'3' GroupNatureId" +
                                " UNION " +
                                " SELECT     '' TotalBalance,'' Balance,'' AccGrpId,N'بدهی ها' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'2' GroupNatureId" +
                                " UNION " +
                                " SELECT     '' TotalBalance,'' Balance,'' AccGrpId,N'سرمایه' Title,'' GroupName,'' AccName, NULL  AccId, '' AccCode,'' Bes,'' Bed,'1' GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع دارایی ها' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup] " +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId " +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=3)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع بدهی ها' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup]" +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId " +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=2)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع سرمایه' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.AccGroup] " +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId " +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].GroupNatureId" +
                                " UNION" +
                                " SELECT isnull(SUM(TotalBes-TotalBed),0) TotalBalance,0 Balance,N'99999999' AccGrpId,'' Title,N'جمع بدهی و سرمایه' GroupName,'' AccName, '' AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,0 GroupNatureId" +
                                " FROM [Accounting.AccGroup] " +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId " +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId " +
                                " WHERE (" + DocBalance + ".DocDate between '"+DocDateFrom+"' and '"+DocDateTo+"')" +
                                " AND ("+AgentId+" =-1 OR " + DocOperation + ".AgentId="+AgentId+")" +
                                " AND ("+DocStatusId+" =-1 OR ("+DocStatusId+" =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId="+DocStatusId+")) OR " + DocOperation + ".DocStatusId="+DocStatusId+")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1 OR [Accounting.AccGroup].GroupNatureId=2 )" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " HAVING isnull(SUM(TotalBes-TotalBed),0)<>0" +
                                " UNION " +
                                " SELECT SUM(TotalBes-TotalBed) TotalBalance,'' Balance,[Accounting.AccGroup].AccGrpId,'' Title,[Accounting.AccGroup].GroupName,'' AccName, NULL  AccId, '' AccCode,(case when SUM(TotalBes-TotalBed)<0 then 0 else ABS(SUM(TotalBes-TotalBed)) end) Bes,(case when SUM(TotalBes-TotalBed)<0 then ABS(SUM(TotalBes-TotalBed)) else 0 end) Bed,[Accounting.AccGroup].GroupNatureId" +
                                " FROM  [Accounting.AccGroup]" +
                                " INNER JOIN [Accounting.Account] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1 OR [Accounting.AccGroup].GroupNatureId=2 OR [Accounting.AccGroup].GroupNatureId=3)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.AccGroup].AccGrpId,[Accounting.AccGroup].GroupName,[Accounting.AccGroup].GroupNatureId" +
                                " UNION " +
                                " SELECT '' TotalBalance,SUM(Balance) Balance ,AccGrpId,''Title,'' GroupName,AccName,AccId,AccCode,'' Bes,'' Bed,GroupNatureId FROM(" +
                                " SELECT SUM(TotalBes-TotalBed) Balance,[Accounting.Account].AccId AccId,[Accounting.Account].AccCode AccCode,[Accounting.Account].AccName AccName,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GroupNatureId" +
                                " FROM [Accounting.Account]" +
                                " INNER JOIN [Accounting.AccGroup] ON [Accounting.AccGroup].AccGrpId=[Accounting.Account].AccGrpId" +
                                " INNER JOIN " + DocBalanceDetail + " ON [Accounting.Account].AccId = " + DocBalanceDetail + ".AccId" +
                                " INNER JOIN " + DocBalance + " ON " + DocBalanceDetail + ".DocBalanceId = " + DocBalance + ".DocBalanceId" +
                                " INNER JOIN " + DocOperation + " ON " + DocBalance + ".DocOperationId = " + DocOperation + ".DocOperationId" +
                                " WHERE (" + DocBalance + ".DocDate between '" + DocDateFrom + "' and '" + DocDateTo + "')" +
                                " AND (" + AgentId + " =-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocStatusId + " =-1 OR (" + DocStatusId + " =2 AND (" + DocOperation + ".DocStatusId =8 OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")) OR " + DocOperation + ".DocStatusId=" + DocStatusId + ")" +
                                " AND ([Accounting.AccGroup].GrpStatusId=1 OR [Accounting.AccGroup].GrpStatusId=2)" +
                                " AND ([Accounting.AccGroup].GroupNatureId=1 OR [Accounting.AccGroup].GroupNatureId=2 OR [Accounting.AccGroup].GroupNatureId=3)" +
                                " AND (" + DocOperation + ".InActive=0)" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY   [Accounting.Account].AccName,[Accounting.Account].AccId,[Accounting.Account].AccCode,[Accounting.Account].AccGrpId,[Accounting.AccGroup].GroupNatureId)T" +
                                " GROUP BY   AccName,AccId,AccCode,AccGrpId,GroupNatureId)T2" +
                                " ORDER BY   GroupNatureId DESC,AccGrpId,AccName" +
                                " END";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                this.DataSet.EnforceConstraints = false;
                try
                {
                    Adapter.Fill(dt);
                }
                catch (Exception err)
                {
                }
            }
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable PeriodBalance8Columns(int PeriodId, string DocDateFrom, string DocDateTo, int AgentId, int AccTypeId)
        {
            int AccId=-1;
            if (PeriodId != -1)
            {
                string DocBalanceDetail = "[Accounting.DocBalanceDetail" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";
                string DocOperation = "[Accounting.DocOperation" + PeriodId + "]";

                //string Command = " SELECT " +
                //                    " (SELECT groupName FROM  [Accounting.AccGroup] WHERE AccGrpId=Acc.AccGrpId) GroupName, " +
                //                    " (SELECT AccName FROM [Accounting.Account] WHERE AccCode=Acc.K ) KolName, " +
                //                    " (SELECT AccName FROM [Accounting.Account] WHERE AccCode=Acc.K+'-'+Acc.M ) MoinName, " +
                //                    " Acc.AccGrpId,K,M,T,AdDetail.AccId,AccName,AccCode," + DocOperation + ".AgentId, " +
                //                    " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)>=0 THEN SUM(TotalBed-TotalBes) ELSE 0 END " +
                //                        " FROM " + DocBalanceDetail + " ad " +
                //                        " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                //                        " WHERE adMaster.InActive=0 AND RegisterDate<" +DocDateFrom + " AND AccId=AdDetail.AccId ) PrevRestBed, " +
                //                    " (SELECT CASE WHEN SUM(TotalBed-TotalBes)<0 THEN SUM(TotalBes-TotalBed) ELSE 0 END " +
                //                        " FROM " + DocBalanceDetail + " ad " +
                //                        " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                //                        " WHERE InActive=0 AND RegisterDate<" +DocDateFrom + " AND   AccId=AdDetail.AccId) PrevRestBes, " +
                //                    " (SELECT ISNULL(SUM(TotalBed),0) " +
                //                        " FROM " + DocBalanceDetail + " ad " +
                //                        " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                //                        " WHERE InActive=0 AND RegisterDate between " +DocDateFrom + " AND "+DocDateTo+" AND AccId=AdDetail.AccId) TotalCurrentBed, " +
                //                    " (SELECT ISNULL(SUM(TotalBes),0) " +
                //                        " FROM " + DocBalanceDetail + " ad " +
                //                        " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                //                        " WHERE InActive=0 AND RegisterDate between " +DocDateFrom + " AND @ToDate  AND AccId=AdDetail.AccId) TotalCurrentBes, " +
                //                    " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)>=0 THEN SUM(TotalBed-TotalBes) ELSE 0 END " +
                //                        " FROM " + DocBalanceDetail + " ad " +
                //                        " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                //                        " WHERE InActive=0 AND  RegisterDate between " +DocDateFrom + " AND "+DocDateTo+" AND AccId=AdDetail.AccId) TotalCurrentRestBed, " +
                //                    " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)<0 THEN SUM(TotalBes-TotalBed) ELSE 0 END " +
                //                        " FROM " + DocBalanceDetail + " ad " +
                //                        " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                //                        " WHERE InActive=0 AND  RegisterDate between " +DocDateFrom + " AND @ToDate AND AccId=AdDetail.AccId) TotalCurrentRestBes, " +
                //                    " TotalRestBed= CASE WHEN  SUM(TotalBed-TotalBes)>0 THEN   SUM(TotalBed-TotalBes) ELSE 0 END , " +
                //                    " TotalRestBes= CASE WHEN  SUM(TotalBed-TotalBes)<0 THEN   SUM(TotalBes-TotalBed) ELSE 0 END " +

                //                " FROM  " + DocBalanceDetail + " AdDetail " +
                //                " INNER JOIN " + DocBalance + "   ON  AdDetail.DocBalanceId=" + DocBalance + ".DocBalanceId AND InActive=0 " +
                //                " INNER JOIN [Accounting.DocOperation]   ON  " + DocOperation + ".DocOperationId=" + DocBalance + ".DocOperationId " +
                //                " INNER JOIN [Accounting.Account] Acc ON AdDetail.AccId=Acc.AccId " +

                //                " WHERE		(@AccId=-1 OR AdDetail.AccId=@AccId) " +
                //                        " AND (@AgentId=-1 OR " + DocOperation + ".AgentId=@AgentId) " +

                //                " GROUP BY AdDetail.AccId,Acc.AccName,Acc.AccGrpId,Acc.K,Acc.M,Acc.T,AccCode," + DocOperation + ".AgentId ";                              

                string Command = "BEGIN IF(" + AccTypeId + "=-1 OR " + AccTypeId + "=4)" +
                                " SELECT " +
                                " (SELECT groupName FROM  [Accounting.AccGroup] WHERE AccGrpId=Acc.AccGrpId) GroupName," +
                                " (SELECT AccName FROM [Accounting.Account] WHERE AccCode=Acc.K ) KolName," +
                                " (SELECT AccName FROM [Accounting.Account] WHERE AccCode=Acc.K+'-'+Acc.M ) MoinName," +
                                " Acc.AccGrpId,K,M,T,AdDetail.AccId,AccName,AccCode," + DocOperation + ".AgentId," +
                                " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)>=0 THEN SUM(TotalBed-TotalBes) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId" +
                                " WHERE adMaster.InActive=0 AND RegisterDate<'" + DocDateFrom + "' AND AccId=AdDetail.AccId ) PrevRestBed," +
                                "(SELECT CASE WHEN SUM(TotalBed-TotalBes)<0 THEN SUM(TotalBes-TotalBed) ELSE 0 END" +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId" +
                                " WHERE InActive=0 AND RegisterDate<'" + DocDateFrom + "' AND AccId=AdDetail.AccId) PrevRestBes," +
                                " (SELECT ISNULL(SUM(TotalBed),0)" +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND RegisterDate between '" + DocDateFrom + "' AND '" + DocDateTo + "'  AND AccId=AdDetail.AccId) TotalCurrentBed," +
                                " (SELECT ISNULL(SUM(TotalBes),0)" +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId" +
                                " WHERE InActive=0 AND RegisterDate between '" + DocDateFrom + "' AND '" + DocDateTo + "'  AND AccId=AdDetail.AccId) TotalCurrentBes," +
                                " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)>=0 THEN SUM(TotalBed-TotalBes) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND  RegisterDate between '" + DocDateFrom + "' AND '" + DocDateTo + "' AND AccId=AdDetail.AccId) TotalCurrentRestBed," +
                                " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)<0 THEN SUM(TotalBes-TotalBed) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND  RegisterDate between '" + DocDateFrom + "' AND '" + DocDateTo + "' AND AccId=AdDetail.AccId) TotalCurrentRestBes," +
                                " TotalRestBed= CASE WHEN  SUM(TotalBed-TotalBes)>0 THEN   SUM(TotalBed-TotalBes) ELSE 0 END ," +
                                " TotalRestBes= CASE WHEN  SUM(TotalBed-TotalBes)<0 THEN   SUM(TotalBes-TotalBed) ELSE 0 END" +
                                " FROM  " + DocBalanceDetail + " AdDetail" +
                                " INNER JOIN " + DocBalance + "   ON  AdDetail.DocBalanceId=" + DocBalance + ".DocBalanceId AND InActive=0" +
                                " INNER JOIN " + DocOperation + "   ON  " + DocOperation + ".DocOperationId=" + DocBalance + ".DocOperationId" +
                                " INNER JOIN [Accounting.Account] Acc ON AdDetail.AccId=Acc.AccId" +
                                " WHERE (" + AccId + "=-1 OR AdDetail.AccId=" + AccId + ")" +
                                " AND (" + AgentId + "=-1 OR " + DocOperation + ".AgentId=" + AgentId + ")" +
                                " AND (" + DocOperation + ".DocStatusId <> 16)" +
                                " GROUP BY AdDetail.AccId,Acc.AccName,Acc.AccGrpId,Acc.K,Acc.M,Acc.T,AccCode," + DocOperation + ".AgentId" +
                                " END" +
                                " BEGIN IF(" + AccTypeId + "=1)" +
                                " SELECT KolName AccName,K AccCode,K,NULL M,NULL T,SUM(PrevRestBed) PrevRestBed,SUM(PrevRestBes) PrevRestBes,SUM(TotalCurrentBed) TotalCurrentBed,SUM(TotalCurrentBes) TotalCurrentBes,SUM(TotalCurrentRestBed) TotalCurrentRestBed,SUM(TotalCurrentRestBes) TotalCurrentRestBes,SUM(TotalRestBed) TotalRestBed,SUM(TotalRestBes) TotalRestBes FROM(" +
                                " SELECT " +
                                " (SELECT groupName FROM  [Accounting.AccGroup] WHERE AccGrpId=Acc.AccGrpId) GroupName," +
                                " (SELECT AccName FROM [Accounting.Account] WHERE AccCode=Acc.K ) KolName," +
                                " (SELECT AccName FROM [Accounting.Account] WHERE AccCode=Acc.K+'-'+Acc.M ) MoinName," +
                                " Acc.AccGrpId,K,M,T,AdDetail.AccId,AccName," +
                                " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)>=0 THEN SUM(TotalBed-TotalBes) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND RegisterDate<'" + DocDateFrom + "' AND AccId=AdDetail.AccId) PrevRestBed," +
                                " (SELECT CASE WHEN SUM(TotalBed-TotalBes)<0 THEN SUM(TotalBes-TotalBed) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId" +
                                " WHERE InActive=0 AND RegisterDate<'" + DocDateFrom + "' AND   AccId=AdDetail.AccId) PrevRestBes," +
                                " (SELECT ISNULL(SUM(TotalBed),0)" +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND RegisterDate BETWEEN '" + DocDateFrom + "' AND '" + DocDateTo + "'  AND AccId=AdDetail.AccId) TotalCurrentBed," +
                                " (SELECT ISNULL(SUM(TotalBes),0)" +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND RegisterDate BETWEEN '" + DocDateFrom + "' AND '" + DocDateTo + "'  AND AccId=AdDetail.AccId) TotalCurrentBes," +
                                " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)>=0 THEN SUM(TotalBed-TotalBes) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND  RegisterDate BETWEEN '" + DocDateFrom + "' AND '" + DocDateTo + "' AND AccId=AdDetail.AccId) TotalCurrentRestBed," +
                                "(SELECT CASE  WHEN SUM(TotalBed-TotalBes)<0 THEN SUM(TotalBes-TotalBed) ELSE 0 END" +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId" +
                                " WHERE InActive=0 AND  RegisterDate BETWEEN '" + DocDateFrom + "' AND '" + DocDateTo + "' AND AccId=AdDetail.AccId) TotalCurrentRestBes," +
                                " TotalRestBed= CASE WHEN  SUM(TotalBed-TotalBes)>0 THEN   SUM(TotalBed-TotalBes) ELSE 0 END ," +
                                " TotalRestBes= CASE WHEN  SUM(TotalBed-TotalBes)<0 THEN   SUM(TotalBes-TotalBed) ELSE 0 END" +
                                " FROM  " + DocBalanceDetail + " AdDetail" +
                                " INNER JOIN " + DocBalance + " ON  AdDetail.DocBalanceId=" + DocBalance + ".DocBalanceId AND InActive=0" +
                                " INNER JOIN [Accounting.Account] Acc ON AdDetail.AccId=Acc.AccId" +
                                " WHERE (" + AccId + "=-1 OR AdDetail.AccId=" + AccId + ")" +
                                " GROUP BY AdDetail.AccId,Acc.AccName,Acc.AccGrpId,Acc.K,Acc.M,Acc.T)T" +
                                " GROUP BY KolName,K" +
                                " END" +
                                " BEGIN IF(" + AccTypeId + "=2)" +
                                " SELECT MoinName AccName,K+'-'+M AccCode,K,M,NULL T,SUM(PrevRestBed) PrevRestBed,SUM(PrevRestBes) PrevRestBes,SUM(TotalCurrentBed) TotalCurrentBed,SUM(TotalCurrentBes) TotalCurrentBes,SUM(TotalCurrentRestBed) TotalCurrentRestBed,SUM(TotalCurrentRestBes) TotalCurrentRestBes,SUM(TotalRestBed) TotalRestBed,SUM(TotalRestBes) TotalRestBes FROM(" +
                                " SELECT " +
                                " (SELECT groupName FROM  [Accounting.AccGroup] WHERE AccGrpId=Acc.AccGrpId) GroupName," +
                                " (SELECT AccName FROM [Accounting.Account] WHERE AccCode=Acc.K ) KolName," +
                                " (SELECT AccName FROM [Accounting.Account] WHERE AccCode=Acc.K+'-'+Acc.M ) MoinName," +
                                " Acc.AccGrpId,K,M,T,AdDetail.AccId,AccName," +
                                " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)>=0 THEN SUM(TotalBed-TotalBes) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND RegisterDate<'" + DocDateFrom + "' AND AccId=AdDetail.AccId) PrevRestBed," +
                                " (SELECT CASE WHEN SUM(TotalBed-TotalBes)<0 THEN SUM(TotalBes-TotalBed) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND RegisterDate<'" + DocDateFrom + "' AND   AccId=AdDetail.AccId) PrevRestBes," +
                                " (SELECT ISNULL(SUM(TotalBed),0)" +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND RegisterDate BETWEEN '" + DocDateFrom + "' AND '" + DocDateTo + "'  AND AccId=AdDetail.AccId) TotalCurrentBed," +
                                "(SELECT ISNULL(SUM(TotalBes),0)" +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND RegisterDate BETWEEN '" + DocDateFrom + "' AND '" + DocDateTo + "'  AND AccId=AdDetail.AccId) TotalCurrentBes," +
                                " (SELECT CASE  WHEN SUM(TotalBed-TotalBes)>=0 THEN SUM(TotalBed-TotalBes) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND  RegisterDate BETWEEN '" + DocDateFrom + "' AND '" + DocDateTo + "' AND AccId=AdDetail.AccId) TotalCurrentRestBed," +
                                "(SELECT CASE  WHEN SUM(TotalBed-TotalBes)<0 THEN SUM(TotalBes-TotalBed) ELSE 0 END " +
                                " FROM " + DocBalanceDetail + " ad " +
                                " INNER JOIN " + DocBalance + " adMaster  ON  ad.DocBalanceId=adMaster.DocBalanceId " +
                                " WHERE InActive=0 AND  RegisterDate BETWEEN '" + DocDateFrom + "' AND '" + DocDateTo + "' AND AccId=AdDetail.AccId) TotalCurrentRestBes," +
                                " TotalRestBed= CASE WHEN  SUM(TotalBed-TotalBes)>0 THEN   SUM(TotalBed-TotalBes) ELSE 0 END ," +
                                " TotalRestBes= CASE WHEN  SUM(TotalBed-TotalBes)<0 THEN   SUM(TotalBes-TotalBed) ELSE 0 END " +
                                " FROM  " + DocBalanceDetail + " AdDetail" +
                                " INNER JOIN " + DocBalance + " ON  AdDetail.DocBalanceId=" + DocBalance + ".DocBalanceId AND InActive=0" +
                                " INNER JOIN [Accounting.Account] Acc ON AdDetail.AccId=Acc.AccId" +
                                " WHERE (" + AccId + "=-1 OR AdDetail.AccId=" + AccId + ")" +
                                " GROUP BY AdDetail.AccId,Acc.AccName,Acc.AccGrpId,Acc.K,Acc.M,Acc.T)T" +
                                " GROUP BY MoinName,K,M" +
                                " END";
            
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                try
                {
                    Fill();
                }
                catch (Exception err)
                {
                }
            }
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOpeningDocData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOpeningDocData", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectClosingDocData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectClosingDocData", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.Fill(dt);
            return (dt);
        }
    }    
}
