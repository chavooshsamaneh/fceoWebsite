using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class AccountingChequeStatusChangeDetailManager:BaseObject
    {
        public AccountingChequeStatusChangeDetailManager()
            : base()
        {
        }

        public AccountingChequeStatusChangeDetailManager(System.Data.DataSet ds)
            : base(ds)
        {
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingChequeStatusChangeDetail);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Accounting.ChequeStatusChangeDetail";
            tableMapping.ColumnMappings.Add("CSCDId", "CSCDId");
            tableMapping.ColumnMappings.Add("CSCId", "CSCId");
            tableMapping.ColumnMappings.Add("ChequeId", "ChequeId");
            tableMapping.ColumnMappings.Add("ChequeStatusId", "ChequeStatusId");
            tableMapping.ColumnMappings.Add("PreChequeStatusId", "PreChequeStatusId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectChequeStatusChangeDetail";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@CSCId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@ChequeId", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteChequeStatusChangeDetail";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CSCDId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CSCDId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertChequeStatusChangeDetail";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CSCId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CSCId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeStatusId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeStatusId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PreChequeStatusId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PreChequeStatusId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateChequeStatusChangeDetail";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CSCId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CSCId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ChequeStatusId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ChequeStatusId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PreChequeStatusId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PreChequeStatusId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CSCDId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CSCDId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CSCDId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "CSCDId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
       
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingChequeStatusChangeDetailDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByParentId(int CSCId)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(CSCId.ToString()))
                CSCId = -1;

            this.Adapter.SelectCommand.Parameters["@CSCId"].Value = CSCId;
            Fill();
            return this.DataTable;
        }

        public DataTable FindByCheque(int CSCId, int ChequeId)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(CSCId.ToString()))
                CSCId = -1;

            if (string.IsNullOrEmpty(ChequeId.ToString()))
                ChequeId = -1;

            this.Adapter.SelectCommand.Parameters["@CSCId"].Value = CSCId;
            this.Adapter.SelectCommand.Parameters["@ChequeId"].Value = ChequeId;

            Fill();
            return this.DataTable;
        }

        public DataTable FindByChequeId(int ChequeId)
        {
            ResetAllParameters();
                        
            if (string.IsNullOrEmpty(ChequeId.ToString()))
                ChequeId = -1;

            this.Adapter.SelectCommand.Parameters["@ChequeId"].Value = ChequeId;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodChequeStatusChangeDetail(int PeriodId, int AgentId)
        {
            if (PeriodId != -1)
            {
                string ChequeStatusChangeDetail = "[Accounting.ChequeStatusChangeDetail" + PeriodId + "]";
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + ChequeStatusChangeDetail + ".*," + Cheque + ".* FROM " + ChequeStatusChangeDetail + " INNER JOIN " + Cheque + " ON " + Cheque + ".ChequeId=" + ChequeStatusChangeDetail + ".ChequeId";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        public void FindPeriodById(int PeriodId, int CSCDId)
        {
            if (PeriodId != -1)
            {
                string ChequeStatusChangeDetail = "[Accounting.ChequeStatusChangeDetail" + PeriodId + "]";
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + ChequeStatusChangeDetail + ".*," + Cheque + ".* FROM " + ChequeStatusChangeDetail + " INNER JOIN " + Cheque + " ON " + Cheque + ".ChequeId=" + ChequeStatusChangeDetail + ".ChequeId WHERE " + CSCDId + "=-1 OR " + ChequeStatusChangeDetail + ".CSCDId=" + CSCDId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }

        public void FindPeriodByCSCId(int PeriodId, int CSCId)
        {
            if (PeriodId != -1)
            {
                string ChequeStatusChangeDetail = "[Accounting.ChequeStatusChangeDetail" + PeriodId + "]";
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT " + ChequeStatusChangeDetail + ".*," + Cheque + ".* FROM " + ChequeStatusChangeDetail + " INNER JOIN " + Cheque + " ON " + Cheque + ".ChequeId=" + ChequeStatusChangeDetail + ".ChequeId WHERE " + CSCId + "=-1 OR " + ChequeStatusChangeDetail + ".CSCId=" + CSCId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }  
    }
}
