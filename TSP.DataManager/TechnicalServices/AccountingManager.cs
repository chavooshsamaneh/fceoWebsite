using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace TSP.DataManager.TechnicalServices
{
    public class AccountingManager : BaseObject
    {
        public AccountingManager()
            : base()
        {

        }

        #region Permission Method
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSAccounting);
        }

        public static Permission GetUserPermissionForAccountingFish(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ReportAccountingFish);
        }

        public static Permission GetUserPermissionForEpaymentPeriodRegister(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EpaymentPeriodRegister);
        }

        public static Permission GetUserPermissionForEpaymentPeriodMembership(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EpaymentPeriodMembership);
        }
        public static Permission GetUserPermissionForTSAccountingFishPrint(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSReport_PrintedAccountingFish);
        }

        public static Permission GetUserPermissionForTSEpaymentObserver(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSReportEpaymentObserverFish);
        }
        public static Permission GetUserPermissionForTSEpaymentDesinger(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSReportEpaymentDesignerFish);
        }

        public static Permission GetUserPermissionForTSAccountingFishChangeStatus(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSChangeAccountingStatus);
        }

        public static Permission GetUserPermissionForTSAccountingFishEditPayedFishNumber(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSEditPayedFhish);
        }
        #endregion

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TS.Accounting";
            tableMapping.ColumnMappings.Add("AccountingId", "AccountingId");
            tableMapping.ColumnMappings.Add("TableTypeId", "TableTypeId");
            tableMapping.ColumnMappings.Add("TableType", "TableType");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("Bank", "Bank");
            tableMapping.ColumnMappings.Add("BranchCode", "BranchCode");
            tableMapping.ColumnMappings.Add("BranchName", "BranchName");
            tableMapping.ColumnMappings.Add("Number", "Number");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("Amount", "Amount");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("AccType", "AccType");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("CardBank", "CardBank");
            tableMapping.ColumnMappings.Add("CardNumber", "CardNumber");
            tableMapping.ColumnMappings.Add("FollowNumber", "FollowNumber");
            tableMapping.ColumnMappings.Add("SerialTrans", "SerialTrans");
            tableMapping.ColumnMappings.Add("TerminalNumber", "TerminalNumber");
            tableMapping.ColumnMappings.Add("ImgUrl", "ImgUrl");
            tableMapping.ColumnMappings.Add("Status", "Status");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Time", "Time");
            tableMapping.ColumnMappings.Add("PaymentId", "PaymentId");
            tableMapping.ColumnMappings.Add("PaymentIdPOS", "PaymentIdPOS");
            tableMapping.ColumnMappings.Add("Token", "Token");
            tableMapping.ColumnMappings.Add("PaymentDate", "PaymentDate");
            tableMapping.ColumnMappings.Add("IsSMSSent", "IsSMSSent");
            tableMapping.ColumnMappings.Add("SendSMSDate", "SendSMSDate");

            this.Adapter.TableMappings.Add(tableMapping);


            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            SetDefaultSqlCommant();

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSAccounting";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_AccountingId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountingId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSAccounting";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Time", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Time", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResultCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResultCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentResult", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentResult", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReferenceId", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ReferenceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentId", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentIdPOS", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentIdPOS", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Token", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Token", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Bank", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Bank", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BranchCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BranchName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BranchName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Number", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Number", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amount", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "Amount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CardBank", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CardBank", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CardNumber", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CardNumber", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowNumber", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowNumber", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialTrans", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalNumber", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalNumber", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Status", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsSMSSent", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsSMSSent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendSMSDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SendSMSDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));


            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSAccounting";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Time", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Time", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResultCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResultCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentResult", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentResult", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReferenceId", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ReferenceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentId", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentIdPOS", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentIdPOS", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Token", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Token", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Bank", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Bank", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BranchCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BranchName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BranchName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Number", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Number", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amount", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "Amount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_AccountingId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccountingId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "AccountingId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CardBank", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CardBank", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CardNumber", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CardNumber", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowNumber", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowNumber", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialTrans", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TerminalNumber", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TerminalNumber", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Status", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsSMSSent", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsSMSSent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendSMSDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SendSMSDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSAccountingDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        private void SetDefaultSqlCommant()
        {
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSAccounting";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.Add("@AccountingId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableTypeId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@AccType", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Number", SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar);
            ResetAllParameters();
        }

        private void SetSqlCommantspSelectTSAccountingInfo()
        {
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSAccountingInfo";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.Add("@AccountingId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableTypeId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@AccType", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Number", SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar);
            ResetAllParameters();
        }

        public void FindByAccountingId(int AccountingId)
        {
            SetDefaultSqlCommant();
            this.Adapter.SelectCommand.Parameters["@AccountingId"].Value = AccountingId;
            Fill();
        }


        public DataTable SelectByAccountingIdForTableTypeId(int AccountingId)
        {

            DataTable dt = new System.Data.DataTable();
            if (AccountingId == -1)
                return dt;
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSAccountingByAccountingId", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@AccountingId", AccountingId);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable SelectTSAccountingPayerMobileNo(int AccountingId)
        {

            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSAccountingPayerMobileNo", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@AccountingId", AccountingId);
            adapter.Fill(dt);
            return dt;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTableTypeId(int TableTypeId, int TableType)
        {
            SetDefaultSqlCommant();
            this.Adapter.SelectCommand.Parameters["@TableTypeId"].Value = TableTypeId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableType;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForReport(string Number, int Type, string FromDate, string ToDate, string Amount)
        {
            DataTable dt = new System.Data.DataTable();
            if (Number == "%" && Type == -1 && FromDate == "1" && ToDate == "2" && Amount == "%")
                return dt;
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSAccountingReport", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@Number", Number);
            adapter.SelectCommand.Parameters.AddWithValue("@Type", Type);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@Amount", Amount);

            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// فیش های ثبت شده از اولین درخواست تا درخواست مورد نظر را بر می گرداند
        /// </summary>
        /// <param name="MfId"></param>
        /// <param name="MeId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindAccountingFishForMeDocument(int MfId, int MeId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "spSelectAccountingFishForMeDocument";
            this.Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeId", MfId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile));
            Fill();
            return this.DataTable;
        }

        /// <summary>
        /// فیش های ثبت شده از اولین درخواست تا درخواست مورد نظر را برای مالی مالکین بر می گرداند
        /// </summary>
        /// <param name="PrjReId"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindAccountingFishForTsOwner(int PrjReId, int ProjectId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "spSelectAccountingFishForTsOwner";
            this.Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeId", PrjReId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest));
            Fill();
            return this.DataTable;
        }

        public DataTable FindByTableTypeIdAndAccType(int TableTypeId, int TableType, int AccType)
        {
            SetDefaultSqlCommant();
            this.Adapter.SelectCommand.Parameters["@TableTypeId"].Value = TableTypeId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableType;
            this.Adapter.SelectCommand.Parameters["@AccType"].Value = AccType;

            Fill();
            return this.DataTable;
        }

        public Boolean CheckAccountingNumber(String Number)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = Connection;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spselectTSAccountingWithNumber";
            objCommand.Parameters.AddWithValue("@Number", Number);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            if (objTable.Rows.Count > 0)
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns>ArrayList[0]: Boolean ; ArrayList[1]:String(Fish Info)</returns>
        public ArrayList CheckAccountingNumberAndReturnFishInfo(String Number)
        {
            ArrayList Result = new ArrayList();
            Result.Add(true);
            Result.Add("");
            Result.Add("-1");
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = Connection;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spselectTSAccountingWithNumber";
            objCommand.Parameters.AddWithValue("@Number", Number);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            if (objTable.Rows.Count > 0)
            {
                Result[0] = false;
                Result[1] = "این شماره فیش قبلا جهت " + objTable.Rows[0]["AccTypeName"].ToString() + " و به نام " + objTable.Rows[0]["FishUseName"].ToString() + " در سیستم ثبت شده است.";
                Result[2] = objTable.Rows[0]["AccountingId"];
                return Result;
            }
            return Result;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForProject(int ProjectId, int TableTypeId, int ProjectIngridientTypeId, int AccType, int Status, int FishPayerId, int Type, string FromDate = "1", string ToDate = "2", string AccTypeList = "")
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            int TableType = -1;
            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Implementer);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Owner:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSOwner);
                    break;
                default:
                    TableType = -1;
                    break;
            }
            this.Adapter.SelectCommand.CommandText = "spSelectTSAccountingForProject";
            this.Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeId", TableTypeId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableType);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@AccType", AccType);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@Status", Status);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@FishPayerId", FishPayerId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@AccTypeList", AccTypeList);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeObserver", TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers));
            this.Adapter.SelectCommand.Parameters.AddWithValue("@Type", Type);

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForProjectForProjectObserverPage(int ProjectId, int PrjReId, int ProjectIngridientTypeId)
        {
            int TableType = -1;
            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Implementer);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Owner:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSOwner);
                    break;
                default:
                    TableType = -1;
                    break;
            }
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSAccountingEpaymentDesingerReport", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.CommandText = "spSelectTSAccountingForProjectObserverPage";
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableType);
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeObserver", TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers));
            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSAccountingEpaymentDesingerReport(int AgentId, int ProjectId, int TableTypeId, int ProjectIngridientTypeId, int AccType, int Status, int FishPayerId, string FromDate = "1", string ToDate = "2", string AccTypeList = "")
        {
            int TableType = -1;
            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Implementer);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Owner:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.TSOwner);
                    break;
                default:
                    TableType = -1;
                    break;
            }
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSAccountingEpaymentDesingerReport", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeId", TableTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableType);
            adapter.SelectCommand.Parameters.AddWithValue("@AccType", AccType);
            adapter.SelectCommand.Parameters.AddWithValue("@Status", Status);
            adapter.SelectCommand.Parameters.AddWithValue("@FishPayerId", FishPayerId);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeList", AccTypeList);
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeObserver", TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers));
            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForProject(int ProjectId, int TableTypeId, int ProjectIngridientTypeId, int AccType, int Status, int FishPayerId, string FromDate = "1", string ToDate = "2", string AccTypeList = "")
        {
            return SelectAccountingForProject(ProjectId, TableTypeId, ProjectIngridientTypeId, AccType, Status, FishPayerId, -1, FromDate, ToDate, AccTypeList);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForProject(int ProjectId, int TableTypeId, int ProjectIngridientTypeId, int AccType, int Status, int FishPayerId, string FromDate = "1", string ToDate = "2")
        {
            return SelectAccountingForProject(ProjectId, TableTypeId, ProjectIngridientTypeId, AccType, Status, FishPayerId, FromDate, ToDate, "");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForProject(int ProjectId, int TableTypeId, int ProjectIngridientTypeId)
        {
            return SelectAccountingForProject(ProjectId, TableTypeId, ProjectIngridientTypeId, -1, -1, -1, "1", "2");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForProject(int ProjectId, int TableTypeId, int ProjectIngridientTypeId, string FromDate = "1", string ToDate = "2")
        {
            return SelectAccountingForProject(ProjectId, TableTypeId, ProjectIngridientTypeId, -1, -1, -1, FromDate, ToDate);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForProject(int ProjectId, int FishPayerId)
        {
            return SelectAccountingForProject(ProjectId, -1, -1, -1, -1, FishPayerId, "1", "2");
        }

        /// <summary>
        /// تمام فیش های ثبت شده برای یک پروژه (خدمات مهندسی) را بر می گرداند 
        /// </summary>
        /// <param name="MfId"></param>
        /// <param name="MeId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForProject(int ProjectId)
        {
            return SelectAccountingForProject(ProjectId, -1, -1);
        }

        public DataTable SelectExistAccountingByAccTypeList(int TableTypeId, int TableType, int ProjectId, string AccTypeList = "(0)")
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSAccountingByAccTypeList", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeId", TableTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableType);
            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeList", AccTypeList);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable SelectExistAccountingByAccTypeListForDocumentDetailInsert(int TableTypeId, int TableType, int ProjectId, string AccTypeList = "(0)")
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSAccountingByAccTypeListForDocumentDetailInsert", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeId", TableTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableType);
            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeList", AccTypeList);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForEpayemnt(int TableTypeId, int AccType, int Status, int FishPayerId, int PPId, int CrsId, int TMeId, int IsPayerTempMe, int Type, string AccTypeList = "(0)")
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            int TableType = -1;
            switch (AccType)
            {
                case (int)TSAccountingAccType.DocMemberFile:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
                    break;
                case (int)TSAccountingAccType.Entrance:
                case (int)TSAccountingAccType.Registeration:
                case (int)TSAccountingAccType.Registeration_Entrance:
                    TableType = TableTypeManager.FindTtId(TSP.DataManager.TableType.Member);
                    break;
            }

            this.Adapter.SelectCommand.CommandText = "spSelectTSAccountingForEPayment";
            this.Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeId", TableTypeId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableType);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@AccType", AccType);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@Status", Status);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@FishPayerId", FishPayerId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@PPId", PPId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypePeriodRegister", TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
            this.Adapter.SelectCommand.Parameters.AddWithValue("@CrsId", CrsId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@AccTypeList", AccTypeList);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TMeId", TMeId);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@IsPayerTempMe", IsPayerTempMe);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@Type", Type);

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForEpayemnt(int TableTypeId, int AccType, int Status, int FishPayerId, int PPId, int CrsId, string AccTypeList = "(0)")
        {
            return SelectAccountingForEpayemnt(TableTypeId, AccType, Status, FishPayerId, PPId, CrsId, -1, -1, -1, AccTypeList);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForEpayemnt(int TableTypeId, int AccType, int Status, int FishPayerId, int PPId, int CrsId)
        {
            return SelectAccountingForEpayemnt(TableTypeId, AccType, Status, FishPayerId, -1, -1, "(0)");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForEpayemnt(int TableTypeId, int AccType, int Status, int FishPayerId)
        {
            return SelectAccountingForEpayemnt(TableTypeId, AccType, Status, FishPayerId, -1, -1);
        }

        public DataTable SelectAccountingForEpayemnt(int TableTypeId, int AccType, int Status, int FishPayerId, int TMeId)
        {
            return SelectAccountingForEpayemnt(TableTypeId, AccType, Status, FishPayerId, -1, -1, TMeId, -1, -1, "(0)");
        }
        public DataTable SelectAccountingForEpayemnt(int TableTypeId, int AccType, int Status, int FishPayerId, int TMeId, Boolean IsPayerTempMe)
        {
            return SelectAccountingForEpayemnt(TableTypeId, AccType, Status, FishPayerId, -1, -1, TMeId, Convert.ToInt32(IsPayerTempMe), -1, "(0)");
        }
        public DataTable SelectAccountingForEpayemntForMemberPortal(int TableTypeId, int AccType, int Status, int FishPayerId, int TMeId, Boolean IsPayerTempMe, int Type)
        {
            return SelectAccountingForEpayemnt(TableTypeId, AccType, Status, FishPayerId, -1, -1, TMeId, Convert.ToInt32(IsPayerTempMe), Type, "(0)");
        }
        public static int FindPaymentTableTypeByAccType(int AccType)
        {

            int TableType = -1;
            switch (AccType)
            {
                case (int)TSAccountingAccType.DocMemberFile:
                    TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
                    break;
                case (int)TSAccountingAccType.Entrance:
                    TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);

                    break;
                case (int)TSAccountingAccType.Registeration:
                    TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);
                    break;
                case (int)TSAccountingAccType.Registeration_Entrance:
                    TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);
                    break;
            }
            return TableType;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAccountingForObserverReport(string FromDate, string ToDate, string FromDateDecreased, string ToDateDecreased, string FromDateAccounting, string ToDateAccounting
            , string FromDateBuildingsLicenses, string ToDateBuildingsLicenses,
             int ProjectStatusId, int IsillInfo, int projectId, string RegisteredNo
            , string ToDateObsPayed, string FromDateObsPayed, int MeId, int IsPayed, int AccountingId = -1)
        {
            DataTable dt = new System.Data.DataTable();
            if (IsillInfo == 0)
                return dt;
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectObserverWageReportFishes", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeProjectObserver", TableTypeManager.FindTtId(TableType.TSProject_Observers));
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDateDecreased", FromDateDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDateDecreased", ToDateDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDateAccounting", FromDateAccounting);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDateAccounting", ToDateAccounting);

            adapter.SelectCommand.Parameters.AddWithValue("@FromDateBuildingsLicenses", FromDateBuildingsLicenses);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDateBuildingsLicenses", ToDateBuildingsLicenses);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectStatusId", ProjectStatusId);

            adapter.SelectCommand.Parameters.AddWithValue("@projectId", projectId);

            adapter.SelectCommand.Parameters.AddWithValue("@RegisteredNo", RegisteredNo);

            adapter.SelectCommand.Parameters.AddWithValue("@ToDateObsPayed", ToDateObsPayed);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDateObsPayed", FromDateObsPayed);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsPayed", IsPayed);
            adapter.SelectCommand.Parameters.AddWithValue("@AccountingId", AccountingId);

            adapter.Fill(dt);
            return dt;
        }


        public decimal SelectTSAccountingForProjectObserverByProject(int ProjectId,int ProjectReqId)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSAccountingForProjectObserverByProject", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectReqId", ProjectReqId); 
            adapter.Fill(dt);
            if (dt.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dt.Rows[0]["SumAmount"]))
                return Convert.ToDecimal(dt.Rows[0]["SumAmount"]);
            return 0;
        }

    }
}
