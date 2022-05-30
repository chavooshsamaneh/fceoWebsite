using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class AccountingChequeStatusChangeManager:BaseObject
    {
        public AccountingChequeStatusChangeManager()
            : base()
        {
        }

        public AccountingChequeStatusChangeManager(System.Data.DataSet ds)
            : base(ds)
        {
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingChequeStatusChange);
        }

        protected override void InitAdapter()
        {            
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "AccountingChequeStatusChange";
            tableMapping.ColumnMappings.Add("CSCId", "CSCId");
            tableMapping.ColumnMappings.Add("DocBalanceId", "DocBalanceId");
            tableMapping.ColumnMappings.Add("RegisterDate", "RegisterDate");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectChequeStatusChange";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@CSCId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@DocBalanceId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@Type", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteChequeStatusChange";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CSCId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CSCId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertChequeStatusChange";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocBalanceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateChequeStatusChange";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocBalanceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CSCId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CSCId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CSCId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "CSCId", System.Data.DataRowVersion.Current, false, null, "", "", ""));    
            
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingChequeStatusChangeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCSCId(int CSCId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CSCId"].Value = CSCId;
            Fill();
        }

        public void FindByDocBalanceId(int DocBalanceId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocBalanceId"].Value = DocBalanceId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgentIdType(int AgentId, int Type)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            if (string.IsNullOrEmpty(Type.ToString()))
                Type = 0;

            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodChequeStatusChange(int PeriodId)
        {
            if (PeriodId != -1)
            {
                string ChequeStatusChange = "[Accounting.ChequeStatusChange" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = "SELECT " + ChequeStatusChange + ".*," + DocBalance + ".Description," + DocBalance + ".TTId,[Accounting.TT].Title FROM " + ChequeStatusChange + " INNER JOIN " + DocBalance + " ON " + DocBalance + ".DocBalanceId=" + ChequeStatusChange + ".DocBalanceId INNER JOIN  [Accounting.TT] ON " + DocBalance + ".TTId=[Accounting.TT].TTId ";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodChequeStatusChangeType(int PeriodId, int AgentId, int Type)
        {
            if (PeriodId != -1)
            {
                string ChequeStatusChange = "[Accounting.ChequeStatusChange" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = " SELECT " + ChequeStatusChange + ".*," + DocBalance + ".Description," + DocBalance + ".TTId,[Accounting.TT].Title " +
                                 " FROM " + ChequeStatusChange +
                                 " INNER JOIN " + DocBalance + " ON " + DocBalance + ".DocBalanceId=" + ChequeStatusChange + ".DocBalanceId " +
                                 " INNER JOIN  [Accounting.TT] ON " + DocBalance + ".TTId=[Accounting.TT].TTId " +
                                 " WHERE " + AgentId + " =-1 OR " + ChequeStatusChange + ".AgentId=" + AgentId +
                                 " AND " + Type + "=-1 OR " + ChequeStatusChange + ".Type=" + Type;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        public void FindPeriodById(int PeriodId, int CSCId)
        {
            if (PeriodId != -1)
            {
                string ChequeStatusChange = "[Accounting.ChequeStatusChange" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = "SELECT " + ChequeStatusChange + ".*," + DocBalance + ".Description," + DocBalance + ".TTId,[Accounting.TT].Title FROM " + ChequeStatusChange + " INNER JOIN " + DocBalance + " ON " + DocBalance + ".DocBalanceId=" + ChequeStatusChange + ".DocBalanceId INNER JOIN  [Accounting.TT] ON " + DocBalance + ".TTId=[Accounting.TT].TTId WHERE " + CSCId + "=-1 OR " + ChequeStatusChange + ".CSCId=" + CSCId;
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
                string ChequeStatusChange = "[Accounting.ChequeStatusChange" + PeriodId + "]";
                string DocBalance = "[Accounting.DocBalance" + PeriodId + "]";

                string Command = " SELECT " + ChequeStatusChange + ".*," + DocBalance + ".Description," + DocBalance + ".TTId,[Accounting.TT].Title "+
                                 " FROM " + ChequeStatusChange + 
                                 " INNER JOIN " + DocBalance + " ON " + DocBalance + ".DocBalanceId=" + ChequeStatusChange + ".DocBalanceId "+
                                 " INNER JOIN  [Accounting.TT] ON " + DocBalance + ".TTId=[Accounting.TT].TTId "+
                                 " WHERE " + DocBalanceId + "=-1 OR " + ChequeStatusChange + ".DocBalanceId=" + DocBalanceId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }
    }
}
