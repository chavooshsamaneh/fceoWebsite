using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class AccountingChequeManager : BaseObject
    {
        public AccountingChequeManager()
            : base()
        {
        }

        public AccountingChequeManager(System.Data.DataSet ds)
            : base(ds)
        {
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingCheque);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Accounting.Cheque";
            tableMapping.ColumnMappings.Add("ChequeId", "ChequeId");
            tableMapping.ColumnMappings.Add("ChequeCode", "ChequeCode");
            tableMapping.ColumnMappings.Add("CHId", "CHId");
            tableMapping.ColumnMappings.Add("Bank", "Bank");
            tableMapping.ColumnMappings.Add("BranchCode", "BranchCode");
            tableMapping.ColumnMappings.Add("BranchName", "BranchName");
            tableMapping.ColumnMappings.Add("AccountNo", "AccountNo");
            tableMapping.ColumnMappings.Add("Amount", "Amount");
            tableMapping.ColumnMappings.Add("RegisterDate", "RegisterDate");
            tableMapping.ColumnMappings.Add("PassDate", "PassDate");
            tableMapping.ColumnMappings.Add("Place", "Place");
            tableMapping.ColumnMappings.Add("RecAccId", "RecAccId");
            tableMapping.ColumnMappings.Add("PayAccId", "PayAccId");
            tableMapping.ColumnMappings.Add("ChequeStatusId", "ChequeStatusId");
            tableMapping.ColumnMappings.Add("DocBalanceId", "DocBalanceId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("TempId", "TempId");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectCheque";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ChequeId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@ChequeCode", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@ChequeStatusId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@CHId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@Type", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@Bank", System.Data.SqlDbType.NVarChar, 64);
            this.Adapter.SelectCommand.Parameters.Add("@PassDateFrom", System.Data.SqlDbType.VarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@PassDateTo", System.Data.SqlDbType.VarChar, 10);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteCheque";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ChequeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertCheque";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CHId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CHId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Bank", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Bank", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BranchCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BranchName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BranchName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amount", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "Amount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PassDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "PassDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Place", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Place", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecAccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RecAccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PayAccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PayAccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeStatusId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeStatusId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocBalanceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TempId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TempId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateCheque";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CHId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CHId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Bank", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Bank", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BranchCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BranchName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BranchName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amount", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "Amount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PassDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "PassDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Place", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Place", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecAccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RecAccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PayAccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PayAccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeStatusId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeStatusId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocBalanceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ChequeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "ChequeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TempId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TempId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingChequeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByChequeId(int ChequeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ChequeId"].Value = ChequeId;
            Fill();
        }

        public void FindByChequeCode(string ChequeCode)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ChequeCode"].Value = ChequeCode;
            Fill();
        }

        public void FindByCHId(int CHId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CHId"].Value = CHId;
            Fill();
        }

        public void FindByCHIdType(int CHId,int Type)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CHId"].Value = CHId;
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgentId(int AgentId, int ChequeStatusId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@ChequeStatusId"].Value = ChequeStatusId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgentIdType(int AgentId, int ChequeStatusId,int Type)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@ChequeStatusId"].Value = ChequeStatusId;
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodCheque(int PeriodId, int AgentId)
        {
            if (PeriodId != -1)
            {
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + Cheque + ".*,[Accounting.ChequeStatus].StatusTitle,[Accounting.Account].AccCode RecAccCode,[Accounting.Account].AccName RecAccName,acc2.AccCode PayAccCode,acc2.AccName PayAccName FROM " + Cheque + "INNER JOIN [Accounting.Account] ON [Accounting.Account].AccId = " + Cheque + ".RecAccId INNER JOIN [Accounting.Account] as acc2 ON acc2.AccId = " + Cheque + ".PayAccId INNER JOIN [Accounting.ChequeStatus] ON [Accounting.ChequeStatus].ChequeStatusId = " + Cheque + ".ChequeStatusId WHERE " + AgentId + " =-1 OR " + Cheque + ".AgentId=" + AgentId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;            
        }

        public void  FindByAgentIdBank(int AgentId, int ChequeStatusId, string Bank,string FromDate,string ToDate)
        {
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(ChequeStatusId.ToString()))
                ChequeStatusId = -1;

            if (string.IsNullOrEmpty(Bank))
                Bank = "%";

            if (string.IsNullOrEmpty(FromDate))
                FromDate = "1";

            if (string.IsNullOrEmpty(ToDate))
                ToDate = "2";

            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@ChequeStatusId"].Value = ChequeStatusId;
            this.Adapter.SelectCommand.Parameters["@Bank"].Value = Bank;
            this.Adapter.SelectCommand.Parameters["@PassDateFrom"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@PassDateTo"].Value = ToDate;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodChequeType(int PeriodId, int AgentId,int Type)
        {
            if (PeriodId != -1)
            {
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + Cheque + ".*,[Accounting.ChequeStatus].StatusTitle,[Accounting.Account].AccCode RecAccCode,[Accounting.Account].AccName RecAccName,acc2.AccCode PayAccCode,acc2.AccName PayAccName " +
                                 " FROM " + Cheque +
                                 " INNER JOIN [Accounting.Account] ON [Accounting.Account].AccId = " + Cheque + ".RecAccId " +
                                 " INNER JOIN [Accounting.Account] as acc2 ON acc2.AccId = " + Cheque + ".PayAccId " +
                                 " INNER JOIN [Accounting.ChequeStatus] ON [Accounting.ChequeStatus].ChequeStatusId = " + Cheque + ".ChequeStatusId " +
                                 " WHERE " + AgentId + " =-1 OR " + Cheque + ".AgentId=" + AgentId +
                                 " AND " + Type + "=-1 OR " + Cheque + ".Type=" + Type;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        public void FindPeriodById(int PeriodId, int ChequeId)
        {
            if (PeriodId != -1)
            {
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + Cheque + ".*,[Accounting.ChequeStatus].StatusTitle,[Accounting.Account].AccCode RecAccCode,[Accounting.Account].AccName RecAccName,acc2.AccCode PayAccCode,acc2.AccName PayAccName FROM " + Cheque + "INNER JOIN [Accounting.Account] ON [Accounting.Account].AccId = " + Cheque + ".RecAccId INNER JOIN [Accounting.Account] as acc2 ON acc2.AccId = " + Cheque + ".PayAccId INNER JOIN [Accounting.ChequeStatus] ON [Accounting.ChequeStatus].ChequeStatusId = " + Cheque + ".ChequeStatusId WHERE " + ChequeId + " =-1 OR " + Cheque + ".ChequeId=" + ChequeId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }

        public void FindPeriodByCHId(int PeriodId, int CHId)
        {
            if (PeriodId != -1)
            {
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + Cheque + ".*,[Accounting.ChequeStatus].StatusTitle,[Accounting.Account].AccCode RecAccCode,[Accounting.Account].AccName RecAccName,acc2.AccCode PayAccCode,acc2.AccName PayAccName FROM " + Cheque + "INNER JOIN [Accounting.Account] ON [Accounting.Account].AccId = " + Cheque + ".RecAccId INNER JOIN [Accounting.Account] as acc2 ON acc2.AccId = " + Cheque + ".PayAccId INNER JOIN [Accounting.ChequeStatus] ON [Accounting.ChequeStatus].ChequeStatusId = " + Cheque + ".ChequeStatusId WHERE " + CHId + " =-1 OR " + Cheque + ".CHId=" + CHId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }

        public void FindPeriodByCHIdType(int PeriodId, int CHId, int Type)
        {
            if (PeriodId != -1)
            {
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + Cheque + ".*,[Accounting.ChequeStatus].StatusTitle,[Accounting.Account].AccCode RecAccCode,[Accounting.Account].AccName RecAccName,acc2.AccCode PayAccCode,acc2.AccName PayAccName" +
                                 " FROM " + Cheque +
                                 " INNER JOIN [Accounting.Account] ON [Accounting.Account].AccId = " + Cheque + ".RecAccId " +
                                 " INNER JOIN [Accounting.Account] as acc2 ON acc2.AccId = " + Cheque + ".PayAccId " +
                                 " INNER JOIN [Accounting.ChequeStatus] ON [Accounting.ChequeStatus].ChequeStatusId = " + Cheque + ".ChequeStatusId " +
                                 " WHERE " + CHId + " =-1 OR " + Cheque + ".CHId=" + CHId +
                                 " AND (" + Type + " =0 OR " + Cheque + ".Type=" + Type + ")";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }

        public void FindPeriodByDocBalanceId(int PeriodId, int DocBalanceId)
        {
            if (PeriodId != -1)
            {
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + Cheque + ".*,[Accounting.ChequeStatus].StatusTitle,[Accounting.Account].AccCode RecAccCode,[Accounting.Account].AccName RecAccName,acc2.AccCode PayAccCode,acc2.AccName PayAccName FROM " + Cheque + "INNER JOIN [Accounting.Account] ON [Accounting.Account].AccId = " + Cheque + ".RecAccId INNER JOIN [Accounting.Account] as acc2 ON acc2.AccId = " + Cheque + ".PayAccId INNER JOIN [Accounting.ChequeStatus] ON [Accounting.ChequeStatus].ChequeStatusId = " + Cheque + ".ChequeStatusId WHERE " + DocBalanceId + " =-1 OR " + Cheque + ".DocBalanceId=" + DocBalanceId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }

        public void FindPeriodByCSCDocBalanceId(int PeriodId, int DocBalanceId)
        {
            if (PeriodId != -1)
            {
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";
                string ChequeStatusChange = "[Accounting.ChequeStatusChange" + PeriodId + "]";
                string ChequeStatusChangeDetail = "[Accounting.ChequeStatusChangeDetail" + PeriodId + "]";

                string Command = " SELECT " + Cheque + ".*,[Accounting.ChequeStatus].StatusTitle,[Accounting.Account].AccCode RecAccCode,[Accounting.Account].AccName RecAccName,acc2.AccCode PayAccCode,acc2.AccName PayAccName " +
                                 " FROM " + Cheque + "INNER JOIN [Accounting.Account] ON [Accounting.Account].AccId = " + Cheque + ".RecAccId " +
                                 " INNER JOIN [Accounting.Account] as acc2 ON acc2.AccId = " + Cheque + ".PayAccId " +
                                 " INNER JOIN [Accounting.ChequeStatus] ON [Accounting.ChequeStatus].ChequeStatusId = " + Cheque + ".ChequeStatusId " +
                                 " WHERE " + Cheque + ".ChequeId in" +
                                 " (SELECT " + ChequeStatusChangeDetail + ".ChequeId FROM " + ChequeStatusChangeDetail + " WHERE " + ChequeStatusChangeDetail + ".CSCId in " +
                                 " (SELECT " + ChequeStatusChange + ".CSCID FROM " + ChequeStatusChange + " WHERE " + ChequeStatusChange + ".DocBalanceId=" + DocBalanceId + "))";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }
    }
}
