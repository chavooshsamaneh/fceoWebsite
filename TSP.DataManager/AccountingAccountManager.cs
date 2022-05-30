using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;



namespace TSP.DataManager
{

    public class AccountingAccountManager : BaseObject
    {
        #region Members
        SqlDataAdapter sqlDataAdapter  = new SqlDataAdapter();        

        private DataTable dtAccountTree;
        private DataTable dtFullAccount;

        public static int MaxLevel = 3;

        public static int KolLength = 2;
        public static int MoinLength = 3;
        public static int TafziliLength = 5;
        #endregion

        #region Constructors
        public AccountingAccountManager()
            : base()
        {

        }
        public AccountingAccountManager(System.Data.DataSet ds)
            : base(ds)
        {

        }      
        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingAccount);
        }

        public static Permission GetUserPermissionForHoverDetail(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingAccountHoverDetail);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Accounting.Account";
            tableMapping.ColumnMappings.Add("AccId", "AccId");
            tableMapping.ColumnMappings.Add("AccCode", "AccCode");
            tableMapping.ColumnMappings.Add("AccName", "AccName");
            tableMapping.ColumnMappings.Add("AccTypeId", "AccTypeId");
            tableMapping.ColumnMappings.Add("AccGrpId", "AccGrpId");
            tableMapping.ColumnMappings.Add("AccDescription", "AccDescription");
            tableMapping.ColumnMappings.Add("FirstInvoice", "FirstInvoice");
            tableMapping.ColumnMappings.Add("ParentId", "ParentId");
            tableMapping.ColumnMappings.Add("Image", "Image");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("K", "K");
            tableMapping.ColumnMappings.Add("M", "M");
            tableMapping.ColumnMappings.Add("T", "T");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("AccessPath", "AccessPath");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("AccTypeName", "AccTypeName");
            tableMapping.ColumnMappings.Add("GroupName", "GroupName");
            tableMapping.ColumnMappings.Add("GroupTypeName", "GroupTypeName");
            tableMapping.ColumnMappings.Add("GroupStatusName", "GroupStatusName");
            tableMapping.ColumnMappings.Add("InActiveName", "InActiveName");
            tableMapping.ColumnMappings.Add("ParentName", "ParentName");
            tableMapping.ColumnMappings.Add("CurrentInvoice", "CurrentInvoice");
            tableMapping.ColumnMappings.Add("BranchLength", "BranchLength");
            tableMapping.ColumnMappings.Add("CreditValue", "CreditValue");
            tableMapping.ColumnMappings.Add("DebtValue", "DebtValue");
            tableMapping.ColumnMappings.Add("HasTimeOutDate", "HasTimeOutDate");
            tableMapping.ColumnMappings.Add("AccLevele", "AccLevele");
            
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectAccount";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@AccId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AccCode", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@AccName", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@AccTypeId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AccGrpId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@GrpTypeId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@GrpStatusId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@GroupNatureId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@ParentId", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteAccount";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_AccId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertAccount";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccGrpId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccGrpId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccDescription", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccDescription", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FirstInvoice", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FirstInvoice", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Image", global::System.Data.SqlDbType.Image, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Image", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@K", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "K", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@M", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "M", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@T", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "T", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccessPath", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccessPath", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BranchLength", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BranchLength", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DebtValue", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DebtValue", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreditValue", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreditValue", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HasTimeOutDate", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HasTimeOutDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccLevele", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccLevele", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));            

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateAccount";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccGrpId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccGrpId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccDescription", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccDescription", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FirstInvoice", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FirstInvoice", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Image", global::System.Data.SqlDbType.Image, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Image", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@K", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "K", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@M", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "M", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@T", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "T", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccessPath", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccessPath", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_AccId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "AccId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BranchLength", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BranchLength", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DebtValue", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DebtValue", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreditValue", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreditValue", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HasTimeOutDate", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HasTimeOutDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccLevele", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccLevele", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));            
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingAccountDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }



        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_AccId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_AccId));
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
        public virtual int Insert(string AccCode, string AccName, global::System.Nullable<int> AccTypeId, int AccGrpId, string AccDescription, decimal FirstInvoice, global::System.Nullable<int> ParentId, byte[] Image, string Comment, bool InActive, string AccessPath, int UserId, System.DateTime ModifiedDate)
        {
            if ((AccCode == null))
            {
                throw new global::System.ArgumentNullException("AccCode");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(AccCode));
            }
            if ((AccName == null))
            {
                throw new global::System.ArgumentNullException("AccName");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(AccName));
            }
            if ((AccTypeId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[3].Value = ((int)(AccTypeId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[3].Value = global::System.DBNull.Value;
            }
            this.Adapter.InsertCommand.Parameters[4].Value = ((int)(AccGrpId));
            if ((AccDescription == null))
            {
                this.Adapter.InsertCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[5].Value = ((string)(AccDescription));
            }
            this.Adapter.InsertCommand.Parameters[6].Value = ((decimal)(FirstInvoice));
            if ((ParentId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[7].Value = ((int)(ParentId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[7].Value = global::System.DBNull.Value;
            }
            if ((Image == null))
            {
                this.Adapter.InsertCommand.Parameters[8].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[8].Value = ((byte[])(Image));
            }
            if ((Comment == null))
            {
                this.Adapter.InsertCommand.Parameters[9].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[9].Value = ((string)(Comment));
            }
            this.Adapter.InsertCommand.Parameters[10].Value = ((bool)(InActive));
            if ((AccessPath == null))
            {
                this.Adapter.InsertCommand.Parameters[11].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[11].Value = ((string)(AccessPath));
            }
            this.Adapter.InsertCommand.Parameters[12].Value = ((int)(UserId));
            this.Adapter.InsertCommand.Parameters[13].Value = ((System.DateTime)(ModifiedDate));
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
        public virtual int Update(
                    string AccCode,
                    string AccName,
                    global::System.Nullable<int> AccTypeId,
                    int AccGrpId,
                    string AccDescription,
                    decimal FirstInvoice,
                    global::System.Nullable<int> ParentId,
                    byte[] Image,
                    string Comment,
                    bool InActive,
                    string AccessPath,
                    int UserId,
                    System.DateTime ModifiedDate,
                    int Original_AccId,
                    byte[] Original_LastTimeStamp,
                    int AccId)
        {
            if ((AccCode == null))
            {
                throw new global::System.ArgumentNullException("AccCode");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(AccCode));
            }
            if ((AccName == null))
            {
                throw new global::System.ArgumentNullException("AccName");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(AccName));
            }
            if ((AccTypeId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(AccTypeId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[3].Value = global::System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[4].Value = ((int)(AccGrpId));
            if ((AccDescription == null))
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(AccDescription));
            }
            this.Adapter.UpdateCommand.Parameters[6].Value = ((decimal)(FirstInvoice));
            if ((ParentId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(ParentId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[7].Value = global::System.DBNull.Value;
            }
            if ((Image == null))
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((byte[])(Image));
            }
            if ((Comment == null))
            {
                this.Adapter.UpdateCommand.Parameters[9].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[9].Value = ((string)(Comment));
            }
            this.Adapter.UpdateCommand.Parameters[10].Value = ((bool)(InActive));
            if ((AccessPath == null))
            {
                this.Adapter.UpdateCommand.Parameters[11].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[11].Value = ((string)(AccessPath));
            }
            this.Adapter.UpdateCommand.Parameters[12].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[13].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[14].Value = ((int)(Original_AccId));
            if ((Original_LastTimeStamp == null))
            {
                throw new global::System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[15].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[16].Value = ((int)(AccId));
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
        public virtual int Update(string AccCode, string AccName, global::System.Nullable<int> AccTypeId, int AccGrpId, string AccDescription, decimal FirstInvoice, global::System.Nullable<int> ParentId, byte[] Image, string Comment, bool InActive, string AccessPath, int UserId, System.DateTime ModifiedDate, int Original_AccId, byte[] Original_LastTimeStamp)
        {
            return this.Update(AccCode, AccName, AccTypeId, AccGrpId, AccDescription, FirstInvoice, ParentId, Image, Comment, InActive, AccessPath, UserId, ModifiedDate, Original_AccId, Original_LastTimeStamp, Original_AccId);
        }



        public string AfterInsertUpdateAccessPath(int accid, int? parentid)
        {

            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spAfterInsertAccount]";

            c.Parameters.Add("@AccId", SqlDbType.Int, 4);
            c.Parameters.Add("@ParentId", SqlDbType.Int, 4);
            c.Parameters.Add("@AccessPath", SqlDbType.VarChar, 50);


            c.Parameters["@AccId"].Direction = ParameterDirection.Input;
            c.Parameters["@ParentId"].Direction = ParameterDirection.Input;
            c.Parameters["@AccessPath"].Direction = ParameterDirection.Output;


            c.Parameters["@AccId"].Value = accid;

            if (parentid == null)
                c.Parameters["@ParentId"].Value = -1;
            else
                c.Parameters["@ParentId"].Value = parentid;



            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            c.ExecuteNonQuery();


            if (c.Parameters["@AccessPath"].Value == null)
                return "";
            else
                return c.Parameters["@AccessPath"].Value.ToString();

        }


        public void AfterEditUpdateAccessPath(int accid, int? parentid, string oldaccesspath, bool subactive)
        {

            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spAfterEditAccount]";

            c.Parameters.Add("@AccId", SqlDbType.Int, 4);
            c.Parameters.Add("@ParentId", SqlDbType.Int, 4);
            c.Parameters.Add("@OldAccessPath", SqlDbType.VarChar, 50);
            c.Parameters.Add("@SubActive", SqlDbType.Bit, 1);



            c.Parameters["@AccId"].Direction = ParameterDirection.Input;
            c.Parameters["@ParentId"].Direction = ParameterDirection.Input;
            c.Parameters["@OldAccessPath"].Direction = ParameterDirection.Input;
            c.Parameters["@SubActive"].Direction = ParameterDirection.Input;



            c.Parameters["@AccId"].Value = accid;

            if (parentid == null)
                c.Parameters["@ParentId"].Value = -1;
            else
                c.Parameters["@ParentId"].Value = parentid;

            c.Parameters["@OldAccessPath"].Value = oldaccesspath;

            if (subactive)
                c.Parameters["@SubActive"].Value = 1;
            else
                c.Parameters["@SubActive"].Value = 0;


            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            c.ExecuteNonQuery();



        }

        #region Account Methods
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchAccount(string AccCode, string AccName, int AccTypeId, int AccGrpId, int GrpTypeId, int GrpStatusId)
        {


            if (string.IsNullOrEmpty(AccCode))
                AccCode = "%";
            this.Adapter.SelectCommand.Parameters["@AccCode"].Value = AccCode;


            if (string.IsNullOrEmpty(AccName))
                AccName = "%";
            this.Adapter.SelectCommand.Parameters["@AccName"].Value = AccName;


            if (string.IsNullOrEmpty(AccTypeId.ToString()))
                AccTypeId = -1;
            this.Adapter.SelectCommand.Parameters["@AccTypeId"].Value = AccTypeId;

            if (string.IsNullOrEmpty(AccGrpId.ToString()))
                AccGrpId = -1;
            this.Adapter.SelectCommand.Parameters["@AccGrpId"].Value = AccGrpId;


            if (string.IsNullOrEmpty(GrpTypeId.ToString()))
                GrpTypeId = -1;
            this.Adapter.SelectCommand.Parameters["@GrpTypeId"].Value = GrpTypeId;


            if (string.IsNullOrEmpty(GrpStatusId.ToString()))
                GrpStatusId = -1;
            this.Adapter.SelectCommand.Parameters["@GrpStatusId"].Value = GrpStatusId;




            Fill();

            return this.DataTable;


        }

        public void FindByAccId(int AccId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AccId"].Value = AccId;
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            Fill();
        }

        public void FindByAccCode(string Acccode)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AccCode"].Value = Acccode;
            Fill();
        }

        public void FindByAccName(string AccName)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AccName"].Value = AccName;
            Fill();
        }

        public void FindByTypeId(int TypeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AccTypeId"].Value = TypeId;
            Fill();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByType(int TypeId, int AgentId)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(TypeId.ToString()))
                TypeId = -1;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            this.Adapter.SelectCommand.Parameters["@AccTypeId"].Value = TypeId;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;
        }

        public void FindByGoupId(int GroupId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AccGrpId"].Value = GroupId;
            Fill();
        }

        public DataTable FillByAgentId(int AgentId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindNoChildByAgent(int AgentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAccountESP", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindTafziliByAgent(int AgentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAccountESPTafzili", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindMoinByAgent(int AgentId)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            int AccTypeId = (int)AccountingAccType.Moin;

            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@AccTypeId"].Value = AccTypeId;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindPayRecTafziliByAgent(int AgentId, char Flag)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAccountPayTafzili", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@Flag", Flag);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgent(int AgentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAccountESPAgent", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable Search(int AccTypeId, string AccName, string AccCode, int AccGrpId, int AgentId)
        {
            ResetAllParameters();
            if (string.IsNullOrEmpty(AccCode))
                AccCode = "%";
            else if (AccCode != "%")
                AccCode = AccCode + "%";

            if (string.IsNullOrEmpty(AccName))
                AccName = "%";

            if (string.IsNullOrEmpty(AccTypeId.ToString()))
                AccTypeId = -1;

            if (string.IsNullOrEmpty(AccGrpId.ToString()))
                AccGrpId = -1;

            //this.Adapter.SelectCommand.Parameters["@AccId"].Value = -1;
            //this.Adapter.SelectCommand.Parameters["@GrpTypeId"].Value = -1;
            //this.Adapter.SelectCommand.Parameters["@GrpStatusId"].Value = -1;
            //this.Adapter.SelectCommand.Parameters["@GroupNatureId"].Value = -1;

            this.Adapter.SelectCommand.Parameters["@AccCode"].Value = AccCode;
            this.Adapter.SelectCommand.Parameters["@AccTypeId"].Value = AccTypeId;
            this.Adapter.SelectCommand.Parameters["@AccName"].Value = AccName;
            this.Adapter.SelectCommand.Parameters["@AccGrpId"].Value = AccGrpId;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindTafziliTaraznameSarmaye(int AgentId)
        {
            int AccTypeId, GroupNatureId, GrpTypeIde;

            ResetAllParameters();

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            AccTypeId = (int)AccountingAccType.Tafzili;
            GroupNatureId = (int)AccountingGroupNature.Sarmayeh;
            GrpTypeIde = (int)AccountingGroupType.Taraznameh;

            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            this.Adapter.SelectCommand.Parameters["@AccTypeId"].Value = AccTypeId;
            this.Adapter.SelectCommand.Parameters["@GroupNatureId"].Value = GroupNatureId;
            this.Adapter.SelectCommand.Parameters["@GrpTypeId"].Value = GrpTypeIde;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public string SelectMaxAccCode(string AccCode)
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = this.Connection;
            sc.CommandType = CommandType.StoredProcedure;
            sc.CommandText = "[spMaxAccCode]";

            AccCode = AccCode + "%";

            sc.Connection.Open();
            sc.Parameters.Add("@AccCode", System.Data.SqlDbType.NVarChar, 50);
            sc.Parameters["@AccCode"].Value = AccCode;

            sc.Parameters.Add("@Max", System.Data.SqlDbType.NVarChar, 50);
            sc.Parameters["@Max"].Direction = ParameterDirection.Output;

            sc.ExecuteNonQuery();
            sc.Connection.Close();
            if (sc.Parameters["@Max"].Value != DBNull.Value)
                return ((sc.Parameters["@Max"].Value).ToString());
            else
                return "";
        }

        public int GetLastKolCode(int AgentCdoe)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spGetLastKolCode]";

            c.Parameters.AddWithValue("@AgentId", AgentCdoe);

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object DocNumber = c.ExecuteScalar();

            if (DocNumber == DBNull.Value)
                return 1;
            return int.Parse(DocNumber.ToString());
        }

        public int GetLastMoinCode(int AgentCdoe, string Kol)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spGetLastMoinCode]";

            c.Parameters.AddWithValue("@AgentId", AgentCdoe);
            c.Parameters.AddWithValue("@Kol", Kol);

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object DocNumber = c.ExecuteScalar();

            if (DocNumber == DBNull.Value)
                return 1;
            return int.Parse(DocNumber.ToString());
        }

        public int GetLastTafsiliCode(int AgentCdoe, string Kol, string Moin)
        {
            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spGetLastTafsiliCode]";
            c.Transaction = this.Transaction;

            c.Parameters.AddWithValue("@AgentId", AgentCdoe);
            c.Parameters.AddWithValue("@Kol", Kol);
            c.Parameters.AddWithValue("@Moin", Moin);

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            object DocNumber = c.ExecuteScalar();

            if (DocNumber == DBNull.Value)
                return 1;
            return int.Parse(DocNumber.ToString());
        }

        public void InsertAccount(string TafziliCode, string AccName, int ParentId, int AgentId, int UserId)
        {
            AccountingAccountManager Manager = new AccountingAccountManager();
            Manager.FindByAccId(ParentId);

            DataRow rowAccount = this.NewRow();

            rowAccount["AccCode"] = Manager[0]["AccCode"].ToString() + "-" + TafziliCode;
            rowAccount["AccName"] = AccName;
            rowAccount["AccTypeId"] = (int)AccountingAccType.Tafzili;
            rowAccount["AccGrpId"] = int.Parse(Manager[0]["AccGrpId"].ToString());
            //rowAccount["AccDescription"] = txtDescription.Text;
            rowAccount["FirstInvoice"] = 0;
            rowAccount["ParentId"] = ParentId;
            rowAccount["AgentId"] = AgentId;
            rowAccount["K"] = Manager[0]["K"].ToString();
            rowAccount["M"] = Manager[0]["M"].ToString();
            rowAccount["T"] = TafziliCode;
            rowAccount["Inactive"] = 0;
            rowAccount["UserId"] = UserId; ;
            rowAccount["ModifiedDate"] = DateTime.Now;

            this.AddRow(rowAccount);
        }

        private int GetGroupId(int ParentId)
        {
            AccountingAccountManager Manager = new AccountingAccountManager();
            Manager.FindByAccId(ParentId);
            int GetGroupId = int.Parse(Manager[0]["AccGrpId"].ToString());
            return GetGroupId;
        }

        private string GetAccCode(int ParentId)
        {
            AccountingAccountManager Manager = new AccountingAccountManager();
            Manager.FindByAccId(ParentId);
            string AccCode = Manager[0]["AccCode"].ToString();
            return AccCode;
        }

        private string GetKolCode(string AccCode)
        {
            int Length = AccountingAccountManager.KolLength;
            string Kol = AccCode.Substring(0, Length);
            return Kol;
        }

        private string GetMoinCode(string AccCode)
        {
            string Moin = "";
            int MoinLength = AccountingAccountManager.MoinLength;
            int KolLength = AccountingAccountManager.KolLength;
            if (AccCode.Length > KolLength + 1)
                Moin = AccCode.Substring(KolLength + 1, MoinLength);
            return Moin;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable AccountSearch(int AccTypeId, string K, string M, string T, int AgentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAccountSearch", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            switch (AccTypeId)
            {
                case 1:

                    break;
                case 2:
                    if (string.IsNullOrEmpty(K))
                        return (dt);
                    break;

                case 4:
                    if (string.IsNullOrEmpty(K) && string.IsNullOrEmpty(M))
                        return (dt);
                    break;
            }

            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeId", AccTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@K", K);
            adapter.SelectCommand.Parameters.AddWithValue("@M", M);
            adapter.SelectCommand.Parameters.AddWithValue("@T", T);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable AccountSearch(int AccTypeId, int AgentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAccountSearch", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            adapter.SelectCommand.Parameters.AddWithValue("@AccTypeId", AccTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@K", "");
            adapter.SelectCommand.Parameters.AddWithValue("@M", "");
            adapter.SelectCommand.Parameters.AddWithValue("@T", "");
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.Fill(dt);
            return (dt);
        }

        public DataTable FindTafsiliByAccCode(string AccCode, int AgentId)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(AccCode))
                AccCode = "";

            int AccTypeId = (int)AccountingAccType.Tafzili;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            this.Adapter.SelectCommand.Parameters["@AccCode"].Value = AccCode;
            this.Adapter.SelectCommand.Parameters["@AccTypeId"].Value = AccTypeId;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;

            Fill();
            return this.DataTable;
        }

        public DataTable FindMoinByAccCode(string AccCode, int AgentId)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(AccCode))
                AccCode = "";

            int AccTypeId = (int)AccountingAccType.Moin;

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            this.Adapter.SelectCommand.Parameters["@AccCode"].Value = AccCode;
            this.Adapter.SelectCommand.Parameters["@AccTypeId"].Value = AccTypeId;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;

            Fill();
            return this.DataTable;
        }

        public void FindByAccCode(string AccCode, int AgentId)
        {
            ResetAllParameters();

            if (string.IsNullOrEmpty(AccCode))
                AccCode = "";

            if (string.IsNullOrEmpty(AgentId.ToString()))
                AgentId = -1;

            this.Adapter.SelectCommand.Parameters["@AccCode"].Value = AccCode;
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
        }

        public void FindByParentId(int ParentId)
        {
            ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@ParentId"].Value = ParentId;
            Fill();
        }

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectAccountTree(int AccTypeId, string AccName, string AccCode, int AccGrpId, int AgentId)
        //{
        //    dtAccountTree = new DataTable();
        //    dtFullAccount = new DataTable();
        //    dtAccountTree.Columns.Add("AccId", Type.GetType("System.Int32"));
        //    dtAccountTree.Columns.Add("Id", Type.GetType("System.Int32"));
        //    dtAccountTree.Columns["Id"].AutoIncrement = true;
        //    dtAccountTree.Columns.Add("AccCode", Type.GetType("System.String"));
        //    dtAccountTree.Columns.Add("ParentId", Type.GetType("System.Int32"));
        //    dtAccountTree.Columns.Add("AccName", Type.GetType("System.String"));
        //    dtAccountTree.Columns.Add("AccTypeName", Type.GetType("System.String"));
        //    dtAccountTree.Columns.Add("GroupName", Type.GetType("System.String"));
        //    dtAccountTree.Columns.Add("GroupTypeName", Type.GetType("System.String"));
        //    dtAccountTree.Columns.Add("GroupStatusName", Type.GetType("System.String"));
        //    dtAccountTree.Columns.Add("InActiveName", Type.GetType("System.String"));


        //    TSP.DataManager.AccountingAccountManager AccountManager = new TSP.DataManager.AccountingAccountManager();
        //    //TSP.DataManager.NezamMemberChartManager NmcManager = new TSP.DataManager.NezamMemberChartManager();
        //    dtFullAccount = AccountManager.Search(AccTypeId, AccName,  AccCode,  AccGrpId, AgentId);

        //    FillTreeView("NULL", -1, AccountManager);

        //    return (dtAccountTree);

        //}

        //private void FillTreeView(string ParentId, int Id,TSP.DataManager.AccountingAccountManager AccountManager)
        //{
        //    DataRow d = AccountManager.NewRow();
        //    DataRow[] dataRow;
        //    if (ParentId == "NULL")
        //        dataRow = dtFullAccount.Select("ParentId is null");
        //    else
        //        dataRow = dtFullAccount.Select("ParentId = " + ParentId);

        //    foreach (DataRow AccRow in dataRow)
        //    {
        //        d = dtAccountTree.NewRow();
        //        //d["NodeType"] = 0;
        //       // d["NmcId"] = -1;
        //        d["AccId"] = int.Parse(AccRow["AccId"].ToString());
        //        int NextParentId = int.Parse(AccRow["AccId"].ToString());
        //        if (ParentId == "NULL")
        //            d["ParentId"] = DBNull.Value;
        //        else
        //            d["ParentId"] = Id;

        //        d["NcName"] = AccRow["NcName"];
        //        dtAccountTree.Rows.Add(d);

        //        NmcManager.FindByNcId(int.Parse(AccRow["NcId"].ToString()));
        //        int CurrentNcId = int.Parse(dtAccountTree.Rows[(dtAccountTree.Rows.Count) - 1]["Id"].ToString());

        //        for (int i = 0; i < NmcManager.Count; i++)
        //        {
        //            d = dtAccountTree.NewRow();
        //            d["NcId"] = int.Parse(NmcManager[i]["NcId"].ToString());
        //            d["NmcId"] = int.Parse(NmcManager[i]["NmcId"].ToString());
        //            d["ParentId"] = CurrentNcId;
        //            d["FirstName"] = NmcManager[i]["FirstName"];
        //            d["LastName"] = NmcManager[i]["LastName"];
        //            d["FullName"] = NmcManager[i]["FirstName"].ToString() + " " + NmcManager[i]["LastName"].ToString();
        //            d["IsMaster"] = NmcManager[i]["IsMaster"];
        //            d["IsMasterPosition"] = NmcManager[i]["IsMasterPosition"];
        //            d["InActive"] = NmcManager[i]["InActive"];
        //            d["NodeType"] = 1;
        //            dtAccountTree.Rows.Add(d);
        //        }

        //        FillTreeView(NextParentId.ToString(), CurrentNcId, NmcManager, NezamChartManager);
        //    }
        //}
        #endregion

        #region AccountHoverDetail
        private void ResetAllParametersForAccHover()
        {
            for (int i = 0; i < this.sqlDataAdapter.SelectCommand.Parameters.Count; i++)
               this.sqlDataAdapter.SelectCommand.Parameters[i].Value = null;
        }

        private  int FillHoverDetail()
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            int returnValue = this.sqlDataAdapter.Fill(DataTable);
            return returnValue;
        }
        private void SetSqlDataAdapter()
        {
            this.sqlDataAdapter.SelectCommand = new SqlCommand();
            this.sqlDataAdapter.SelectCommand.Connection = this.Connection;
            this.sqlDataAdapter.SelectCommand.CommandText = "spSelectAccountHoverDetail";
            this.sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccCode", System.Data.SqlDbType.NVarChar, 50);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccName", System.Data.SqlDbType.NVarChar, 50);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccTypeId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccGrpId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@GrpTypeId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@GrpStatusId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@GroupNatureId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@ParentId", System.Data.SqlDbType.Int, 4);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetDataAccountHoverDetail()
        {
            this.sqlDataAdapter.SelectCommand = new SqlCommand();
            this.sqlDataAdapter.SelectCommand.Connection = this.Connection;
            this.sqlDataAdapter.SelectCommand.CommandText = "spSelectAccountHoverDetail";
            this.sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccCode", System.Data.SqlDbType.NVarChar, 50);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccName", System.Data.SqlDbType.NVarChar, 50);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccTypeId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AccGrpId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@GrpTypeId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@GrpStatusId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@GroupNatureId", System.Data.SqlDbType.Int, 4);
            this.sqlDataAdapter.SelectCommand.Parameters.Add("@ParentId", System.Data.SqlDbType.Int, 4);

            FillHoverDetail();            
            return DataTable;
        }

        public void FindAccHoverDetailByAccId(int AccId)
        {
            SetSqlDataAdapter();
            ResetAllParametersForAccHover();
            this.sqlDataAdapter.SelectCommand.Parameters["@AccId"].Value = AccId;
            FillHoverDetail();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchHoverDetail(int AccTypeId, string AccName, string AccCode, int AccGrpId, int AgentId)
        {
            SetSqlDataAdapter();
            ResetAllParametersForAccHover();
            if (string.IsNullOrEmpty(AccCode))
                AccCode = "%";
            else if (AccCode != "%")
                AccCode = AccCode + "%";

            if (string.IsNullOrEmpty(AccName))
                AccName = "%";

            if (string.IsNullOrEmpty(AccTypeId.ToString()))
                AccTypeId = -1;

            if (string.IsNullOrEmpty(AccGrpId.ToString()))
                AccGrpId = -1;

            //this.Adapter.SelectCommand.Parameters["@AccId"].Value = -1;
            //this.Adapter.SelectCommand.Parameters["@GrpTypeId"].Value = -1;
            //this.Adapter.SelectCommand.Parameters["@GrpStatusId"].Value = -1;
            //this.Adapter.SelectCommand.Parameters["@GroupNatureId"].Value = -1;

            this.sqlDataAdapter.SelectCommand.Parameters["@AccCode"].Value = AccCode;
            this.sqlDataAdapter.SelectCommand.Parameters["@AccTypeId"].Value = AccTypeId;
            this.sqlDataAdapter.SelectCommand.Parameters["@AccName"].Value = AccName;
            this.sqlDataAdapter.SelectCommand.Parameters["@AccGrpId"].Value = AccGrpId;
            this.sqlDataAdapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            FillHoverDetail();
            return this.DataTable;
        }

        public void FindHoverDetailByAccCode(string Acccode)
        {
            SetSqlDataAdapter();
            ResetAllParametersForAccHover();
            this.sqlDataAdapter.SelectCommand.Parameters["@AccCode"].Value = Acccode;
            FillHoverDetail();
        }
        #endregion
    }
}
