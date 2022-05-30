using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class AccountingChequeHeaderManager:BaseObject
    {
        public AccountingChequeHeaderManager()
            : base()
        {
        }

        public AccountingChequeHeaderManager(System.Data.DataSet ds)
            : base(ds)
        {
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingChequeHeader);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "AccountingChequeHeader";
            tableMapping.ColumnMappings.Add("CHId", "CHId");
            tableMapping.ColumnMappings.Add("PayAccId", "PayAccId");
            tableMapping.ColumnMappings.Add("RegisterDate", "RegisterDate");
            tableMapping.ColumnMappings.Add("DocBalanceId", "DocBalanceId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("Total", "Total");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("DirectAccess", "DirectAccess");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectChequeHeader";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@CHId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@Type",System.Data.SqlDbType.SmallInt,2);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteChequeHeader";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CHId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CHId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertChequeHeader";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PayAccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PayAccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocBalanceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Total", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, 0, 0, "Total", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DirectAccess", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "DirectAccess", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateChequeHeader";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PayAccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PayAccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocBalanceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Total", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, 0, 0, "Total", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DirectAccess", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "DirectAccess", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CHId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CHId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CHId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "CHId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
      
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingChequeHeaderDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCHId(int CHId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CHId"].Value = CHId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgentId(int AgentId,int Type)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(Type.ToString()))
                Type = 0;
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodChequeHeader(int PeriodId, int AgentId)
        {
            if (PeriodId != -1)
            {
                string ChequeHeader = "[Accounting.ChequeHeader" + PeriodId + "]";
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = "SELECT " + ChequeHeader + ".*,[Accounting.Account].AccName,[Accounting.Account].AccCode,(SELECT SUM(t.Amount) FROM " + Cheque + " AS t WHERE t.CHId=" + ChequeHeader + ".CHId)Amount FROM " + ChequeHeader + " INNER JOIN	[Accounting.Account] ON [Accounting.Account].AccId=" + ChequeHeader + ".PayAccId INNER JOIN  " + DocBalance + " ON " + DocBalance + ".DocBalanceId=" + ChequeHeader + ".DocBalanceId WHERE	" + AgentId + "=-1 OR " + ChequeHeader + ".AgentId=" + AgentId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodChequeHeaderType(int PeriodId, int AgentId, int Type)
        {
            if (PeriodId != -1)
            {
                string ChequeHeader = "[Accounting.ChequeHeader" + PeriodId + "]";
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = "SELECT " + ChequeHeader + ".*,[Accounting.Account].AccName,[Accounting.Account].AccCode,(SELECT SUM(t.Amount) FROM " + Cheque + " AS t WHERE t.CHId=" + ChequeHeader + ".CHId)Amount FROM " + ChequeHeader + " INNER JOIN	[Accounting.Account] ON [Accounting.Account].AccId=" + ChequeHeader + ".PayAccId INNER JOIN  " + DocBalance + " ON " + DocBalance + ".DocBalanceId=" + ChequeHeader + ".DocBalanceId WHERE	" + AgentId + "=-1 OR " + ChequeHeader + ".AgentId=" + AgentId + " AND " + Type + "=-1 OR " + ChequeHeader + ".Type=" + Type;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        public void FindPeriodByCHId(int PeriodId, int CHId)
        {
            if (PeriodId != -1)
            {
                string ChequeHeader = "[Accounting.ChequeHeader" + PeriodId + "]";
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = "SELECT " + ChequeHeader + ".*,[Accounting.Account].AccName,[Accounting.Account].AccCode,(SELECT SUM(t.Amount) FROM " + Cheque + " AS t WHERE t.CHId=" + ChequeHeader + ".CHId)Amount FROM " + ChequeHeader + " INNER JOIN	[Accounting.Account] ON [Accounting.Account].AccId=" + ChequeHeader + ".PayAccId INNER JOIN  " + DocBalance + "ON " + DocBalance + ".DocBalanceId=" + ChequeHeader + ".DocBalanceId WHERE	" + CHId + "=-1 OR " + ChequeHeader + ".CHId=" + CHId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }
    }
}
