using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class MemberResearchActivityManager:BaseObject
    {
        //static MemberResearchActivityManager()
        //{
        //    _tableId = TableType.MemberResearchActivity;
        //}
        public MemberResearchActivityManager()
            : base()
        {
        }
        public MemberResearchActivityManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberResearchActivity);
        }
        protected override void InitAdapter()
        {          
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblMemberResearchActivity";
            tableMapping.ColumnMappings.Add("MraId", "MraId");
            tableMapping.ColumnMappings.Add("RaId", "RaId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("MReId", "MReId");
            tableMapping.ColumnMappings.Add("Name", "Name");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectMemberResearchActivity";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MraId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MraId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MReId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteMemberResearchActivity";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MraId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MraId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertMemberResearchActivity";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RaId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RaId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Name", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateMemberResearchActivity";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RaId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RaId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Name", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MraId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MraId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MraId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MraId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        public void FindByCode(int MraId)
        {
            this.Adapter.SelectCommand.Parameters["@MraId"].Value = MraId;

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]

        public DataTable FindByMeId(int MeId)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            Fill();
            return this.DataTable;
        }
        public DataTable FindByMeRequest(int MeId, int MReId, Int16 IsConfirm)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@MReId"].Value = MReId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;


            Fill();
            return this.DataTable;

        }
        public void FindForDelete(int MeId, int MeRId)
        {
           
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberResearchActivityDelete", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters["@MReId"].Value = MeRId;
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            adapter.Fill(DataTable);

        }
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.MemberDataSet.tblMemberResearchActivityDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_MraId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_MraId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
            }
            System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
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
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(int MraId, short RaId, int MeId, string Description, int UserId, System.DateTime ModifiedDate)
        {
            this.Adapter.InsertCommand.Parameters[1].Value = ((int)(MraId));
            this.Adapter.InsertCommand.Parameters[2].Value = ((short)(RaId));
            this.Adapter.InsertCommand.Parameters[3].Value = ((int)(MeId));
            if ((Description == null))
            {
                this.Adapter.InsertCommand.Parameters[4].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[4].Value = ((string)(Description));
            }
            this.Adapter.InsertCommand.Parameters[5].Value = ((int)(UserId));
            this.Adapter.InsertCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
            System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
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
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(int MraId, short RaId, int MeId, string Description, int UserId, System.DateTime ModifiedDate, int Original_MraId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(MraId));
            this.Adapter.UpdateCommand.Parameters[2].Value = ((short)(RaId));
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(MeId));
            if ((Description == null))
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(Description));
            }
            this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Original_MraId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((byte[])(Original_LastTimeStamp));
            }
            System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
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
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByMember(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberResearchActivityByMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int).Value = MeId;

            adapter.Fill(dt);
            return (dt);
        }
    }
}

