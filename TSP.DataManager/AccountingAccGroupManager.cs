using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace TSP.DataManager
{
    public class AccountingAccGroupManager : BaseObject
    {


        public AccountingAccGroupManager()
            : base()
        {
        }
        public AccountingAccGroupManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingAccGroup);
        }

        public static Permission GetUserPermissionAccountHoverDetail(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingAccGroupHoverDetail);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Accounting.AccGroup";
            tableMapping.ColumnMappings.Add("AccGrpId", "AccGrpId");
            tableMapping.ColumnMappings.Add("GroupName", "GroupName");
            tableMapping.ColumnMappings.Add("GrpTypeId", "GrpTypeId");
            tableMapping.ColumnMappings.Add("GrpStatusId", "GrpStatusId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("GroupNatureId", "GroupNatureId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("GroupTypeName", "GroupTypeName");
            tableMapping.ColumnMappings.Add("GroupStatusName", "GroupStatusName");
            tableMapping.ColumnMappings.Add("IsHoverGroup", "IsHoverGroup");


            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectAccGroup";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@AccGrpId", SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@GroupName", SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@GrpTypeId", SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@GrpStatusId", SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@IsHoverGroup", SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteAccGroup";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_AccGrpId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccGrpId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertAccGroup";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccGrpId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccGrpId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GrpTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GrpTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GrpStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GrpStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupNatureId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupNatureId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsHoverGroup", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsHoverGroup", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateAccGroup";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AccGrpId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccGrpId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GrpTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GrpTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GrpStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GrpStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupNatureId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupNatureId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_AccGrpId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AccGrpId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsHoverGroup", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsHoverGroup", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.AccountingDataSet.AccountingAccGroupDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }


        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_AccGrpId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_AccGrpId));
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
        public virtual int Insert(int AccGrpId, string GroupName, int GrpTypeId, int GrpStatusId, string Description, int UserId, System.DateTime ModifiedDate)
        {
            this.Adapter.InsertCommand.Parameters[1].Value = ((int)(AccGrpId));
            if ((GroupName == null))
            {
                throw new global::System.ArgumentNullException("GroupName");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(GroupName));
            }
            this.Adapter.InsertCommand.Parameters[3].Value = ((int)(GrpTypeId));
            this.Adapter.InsertCommand.Parameters[4].Value = ((int)(GrpStatusId));
            if ((Description == null))
            {
                this.Adapter.InsertCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[5].Value = ((string)(Description));
            }
            this.Adapter.InsertCommand.Parameters[6].Value = ((int)(UserId));
            this.Adapter.InsertCommand.Parameters[7].Value = ((System.DateTime)(ModifiedDate));
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
        public virtual int Update(int AccGrpId, string GroupName, int GrpTypeId, int GrpStatusId, string Description, int UserId, System.DateTime ModifiedDate, int Original_AccGrpId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(AccGrpId));
            if ((GroupName == null))
            {
                throw new global::System.ArgumentNullException("GroupName");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(GroupName));
            }
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(GrpTypeId));
            this.Adapter.UpdateCommand.Parameters[4].Value = ((int)(GrpStatusId));
            if ((Description == null))
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(Description));
            }
            this.Adapter.UpdateCommand.Parameters[6].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[8].Value = ((int)(Original_AccGrpId));
            if ((Original_LastTimeStamp == null))
            {
                throw new global::System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[10].Value = ((byte[])(Original_LastTimeStamp));
            }
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
        public virtual int Update(string GroupName, int GrpTypeId, int GrpStatusId, string Description, int UserId, System.DateTime ModifiedDate, int Original_AccGrpId, byte[] Original_LastTimeStamp)
        {
            return this.Update(Original_AccGrpId, GroupName, GrpTypeId, GrpStatusId, Description, UserId, ModifiedDate, Original_AccGrpId, Original_LastTimeStamp);
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchAccGroup(string GroupName, int GrpTypeId, int GrpStatusId)
        {


            if (string.IsNullOrEmpty(GroupName))
                GroupName = "%";
            this.Adapter.SelectCommand.Parameters["@GroupName"].Value = GroupName;

            if (string.IsNullOrEmpty(GrpTypeId.ToString()))
                GrpTypeId = -1;
            this.Adapter.SelectCommand.Parameters["@GrpTypeId"].Value = GrpTypeId;


            if (string.IsNullOrEmpty(GrpStatusId.ToString()))
                GrpTypeId = -1;
            this.Adapter.SelectCommand.Parameters["@GrpStatusId"].Value = GrpStatusId;


            Fill();
            return this.DataTable;


        }

        public void FindByGroupName(string name)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@GroupName"].Value = name;
            Fill();

        }

        public void FindByGroupId(int id)
        {

            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AccGrpId"].Value = id;
            Fill();

        }

        public int GetNewCode()
        {


            SqlCommand c = new SqlCommand();

            c.Connection = this.Connection;
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "[spGetMaxAccGroupId]";
            c.Parameters.Add("@ReturnCode", SqlDbType.Int, 4);
            c.Parameters["@ReturnCode"].Direction = ParameterDirection.Output;

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            c.ExecuteNonQuery();

            int code = Convert.ToInt32(c.Parameters[0].Value);

            if (code == 0)
                return 1;
            else
                return code * 2;


        }
        
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAccountType(int IsHoverGroup)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@IsHoverGroup"].Value = IsHoverGroup;
            Fill();
            return DataTable;
        }


    }
}
