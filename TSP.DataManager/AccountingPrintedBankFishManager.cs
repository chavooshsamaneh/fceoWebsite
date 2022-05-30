using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public  class AccountingPrintedBankFishManager:BaseObject
    {

        // static AccountingPrintedBankFishManager()
        //{
        //    _tableId = TableType.AccountingPrintedBankFish;
        //}
        public AccountingPrintedBankFishManager()
            : base()
        {
        }
        public AccountingPrintedBankFishManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingPrintedBankFish);
        }
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Accounting.PrintedBankFish";
            tableMapping.ColumnMappings.Add("PrnBnkFishId", "PrnBnkFishId");
            tableMapping.ColumnMappings.Add("PrnBnkFishCode", "PrnBnkFishCode");
            tableMapping.ColumnMappings.Add("RegisterDate", "RegisterDate");
            tableMapping.ColumnMappings.Add("PayDate", "PayDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("AccIdTo", "AccIdTo");
            tableMapping.ColumnMappings.Add("AccIdFrom", "AccIdFrom");
            tableMapping.ColumnMappings.Add("Price", "Price");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("IsPayed", "IsPayed");
            tableMapping.ColumnMappings.Add("PrjId", "PrjId");
            tableMapping.ColumnMappings.Add("CCId", "CCId");
            tableMapping.ColumnMappings.Add("DocBalanceId", "DocBalanceId");
            tableMapping.ColumnMappings.Add("DirectAccess", "DirectAccess");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectPrintedBankFish";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PrnBnkFishId", SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@PrnBnkFishCode", SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@DocNumber", SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@FromRegisterDate", SqlDbType.NVarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@ToRegisterDate", SqlDbType.NVarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@FromPayDate", SqlDbType.NVarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@ToPayDate", SqlDbType.NVarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@AccIdTo", SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AccIdFrom", SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@IsPayed", SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AccCodeTo", SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@AccCodeFrom", SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@DocBalanceId", SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeletePrintedBankFish";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PrnBnkFishId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrnBnkFishId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertPrintedBankFish";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));            
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrnBnkFishCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrnBnkFishCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PayDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PayDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccIdTo", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccIdTo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccIdFrom", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccIdFrom", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Price", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Price", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsPayed", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsPayed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CCId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CCId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocBalanceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DirectAccess", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DirectAccess", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdatePrintedBankFish";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));            
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrnBnkFishCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrnBnkFishCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegisterDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PayDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PayDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccIdTo", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccIdTo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccIdFrom", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccIdFrom", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Price", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Price", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsPayed", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsPayed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CCId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CCId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocBalanceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocBalanceId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DirectAccess", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DirectAccess", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PrnBnkFishId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrnBnkFishId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrnBnkFishId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "PrnBnkFishId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingPrintedBankFishDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public DataTable SearchPrintedBankFish(int AgentId, string DocNumber, string FromRegisterDate, string ToRegisterDate, string FromPayDate, string ToPayDate, int AccIdTo, int AccIdFrom, int IsPayed)
        {
            ResetAllParameters();
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;

            if (string.IsNullOrEmpty(DocNumber))
                DocNumber = "%";
            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = DocNumber;

            if (!string.IsNullOrEmpty(FromRegisterDate) && string.IsNullOrEmpty(ToRegisterDate))
                ToRegisterDate = FromRegisterDate;

            if (string.IsNullOrEmpty(FromRegisterDate) && !string.IsNullOrEmpty(ToRegisterDate))
                FromRegisterDate = ToRegisterDate;

            this.Adapter.SelectCommand.Parameters["@FromRegisterDate"].Value = FromRegisterDate;
            this.Adapter.SelectCommand.Parameters["@ToRegisterDate"].Value = ToRegisterDate;
            
            if (!string.IsNullOrEmpty(FromPayDate) && string.IsNullOrEmpty(ToPayDate))
                ToPayDate = FromPayDate;

            if (string.IsNullOrEmpty(FromPayDate) && !string.IsNullOrEmpty(ToPayDate))
                FromPayDate = ToPayDate;

            this.Adapter.SelectCommand.Parameters["@FromPayDate"].Value = FromPayDate;
            this.Adapter.SelectCommand.Parameters["@ToPayDate"].Value = ToPayDate;
            
            if (string.IsNullOrEmpty(AccIdTo.ToString()))
                AccIdTo = -1;
            this.Adapter.SelectCommand.Parameters["@AccIdTo"].Value = AccIdTo;
            
            if (string.IsNullOrEmpty(AccIdFrom.ToString()))
                AccIdFrom = -1;
            this.Adapter.SelectCommand.Parameters["@AccIdFrom"].Value = AccIdFrom;
            
            if (string.IsNullOrEmpty(IsPayed.ToString()))
                IsPayed = 0;
            this.Adapter.SelectCommand.Parameters["@IsPayed"].Value = IsPayed;
            
            Fill();

            return this.DataTable;
        }

        public DataTable SearchPrintedBankFishWithAccCode(int AgentId, string DocNumber, string FromRegisterDate, string ToRegisterDate, string FromPayDate, string ToPayDate, string AccCodeTo, string  AccCodeFrom, int IsPayed)
        {
            ResetAllParameters();
            
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;

            if (string.IsNullOrEmpty(DocNumber))
                DocNumber = "%";
            this.Adapter.SelectCommand.Parameters["@DocNumber"].Value = DocNumber;

            if (!string.IsNullOrEmpty(FromRegisterDate) && string.IsNullOrEmpty(ToRegisterDate))
                ToRegisterDate = FromRegisterDate;

            if (string.IsNullOrEmpty(FromRegisterDate) && !string.IsNullOrEmpty(ToRegisterDate))
                FromRegisterDate = ToRegisterDate;

            this.Adapter.SelectCommand.Parameters["@FromRegisterDate"].Value = FromRegisterDate;
            this.Adapter.SelectCommand.Parameters["@ToRegisterDate"].Value = ToRegisterDate;
            
            if (!string.IsNullOrEmpty(FromPayDate) && string.IsNullOrEmpty(ToPayDate))
                ToPayDate = FromPayDate;

            if (string.IsNullOrEmpty(FromPayDate) && !string.IsNullOrEmpty(ToPayDate))
                FromPayDate = ToPayDate;

            this.Adapter.SelectCommand.Parameters["@FromPayDate"].Value = FromPayDate;
            this.Adapter.SelectCommand.Parameters["@ToPayDate"].Value = ToPayDate;
            
            if (string.IsNullOrEmpty(AccCodeTo))
                AccCodeTo = "%";
            this.Adapter.SelectCommand.Parameters["@AccCodeTo"].Value = AccCodeTo;
            
            if (string.IsNullOrEmpty(AccCodeFrom))
                AccCodeFrom = "%";
            this.Adapter.SelectCommand.Parameters["@AccCodeFrom"].Value = AccCodeFrom;
            
            if (string.IsNullOrEmpty(IsPayed.ToString()))
                IsPayed = 0;
            this.Adapter.SelectCommand.Parameters["@IsPayed"].Value = IsPayed;
            
            Fill();

            return this.DataTable;
        }

        public void FindById(int id)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrnBnkFishid"].Value = id;
            Fill();
        }

        public void FindByPrnBnkFishCode(string PrnBnkFishCode)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PrnBnkFishCode"].Value = PrnBnkFishCode;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgentId(int AgentId)
        {
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();

            return this.DataTable;
        }

        public void FindByDocBalanceId(int DocBalanceId)
        {
            if (string.IsNullOrEmpty(DocBalanceId.ToString()))
                DocBalanceId = -1;

            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@DocBalanceId"].Value = DocBalanceId;
            Fill();
        }

        public void FindByAccIdTo(int AgentId,int AccIdTo,string FromDate,string ToDate)
        {
            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;
            
            if (string.IsNullOrEmpty(AccIdTo.ToString()))
                AccIdTo = -1;

            if (string.IsNullOrEmpty(FromDate))
                FromDate = "1";

            if (string.IsNullOrEmpty(ToDate))
                ToDate = "2";

            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@AccIdTo"].Value = AccIdTo;
            this.Adapter.SelectCommand.Parameters["@IsPayed"].Value = 1;
            this.Adapter.SelectCommand.Parameters["@FromPayDate"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@ToPayDate"].Value = ToDate;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodPrintedBankFishManager(int PeriodId, int AgentId)
        {
            if (PeriodId != -1)
            {
                string PrintedBankFish = "[Accounting.PrintedBankFish" + PeriodId + "]";
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT "+PrintedBankFish+".*,a1.AccName AccNameFrom,a1.AccCode AccCodeFrom,a2.AccName AccNameTo,a2.AccCode AccCodeTo FROM "+PrintedBankFish+" inner join  [Accounting.Account] a1 ON "+PrintedBankFish+".AccIdFrom= a1.AccId inner join  [Accounting.Account] a2 ON "+PrintedBankFish+".AccIdTo= a2.AccId";
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
            return this.DataTable;
        }

        public void FindPeriodById(int PeriodId, int PrnBnkFishId)
        {
            if (PeriodId != -1)
            {
                string PrintedBankFish = "[Accounting.PrintedBankFish" + PeriodId + "]";
                string ChequeStatusChangeDetail = "[Accounting.ChequeStatusChangeDetail" + PeriodId + "]";
                string Cheque = "[Accounting.Cheque" + PeriodId + "]";

                string Command = "SELECT "+PrintedBankFish+".*,a1.AccName AccNameFrom,a1.AccCode AccCodeFrom,a2.AccName AccNameTo,a2.AccCode AccCodeTo FROM "+PrintedBankFish+" inner join  [Accounting.Account] a1 ON "+PrintedBankFish+".AccIdFrom= a1.AccId inner join  [Accounting.Account] a2 ON "+PrintedBankFish+".AccIdTo= a2.AccId WHERE "+PrnBnkFishId+"=-1 or "+PrintedBankFish+".PrnBnkFishId="+PrnBnkFishId;
                this.Adapter.SelectCommand.Parameters.Clear();
                this.Adapter.SelectCommand.CommandText = Command;
                this.Adapter.SelectCommand.CommandType = CommandType.Text;
                Fill();
            }
        }
    }
}
