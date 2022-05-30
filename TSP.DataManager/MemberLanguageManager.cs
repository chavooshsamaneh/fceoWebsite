using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class MemberLanguageManager:BaseObject
    {
        // static MemberLanguageManager()
           
        //{
        //    _tableId = TableType.MemberLanguage;
        //}
        public MemberLanguageManager()
            : base()
        {
        }
        public MemberLanguageManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberLanguage);
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblMemberLanguage";
            tableMapping.ColumnMappings.Add("MlanId", "MlanId");
            tableMapping.ColumnMappings.Add("LanId", "LanId");
            tableMapping.ColumnMappings.Add("LqId", "LqId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("MReId", "MReId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectMemberLanguage";
            this.Adapter.SelectCommand.Parameters.Add("@MlanId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@JustActive", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = (int)TableCodes.MemberLanguage;

            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteMemberLanguage";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MlanId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MlanId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertMemberLanguage";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LanId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "LanId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LqId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "LqId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateMemberLanguage";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LanId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "LanId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LqId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "LqId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MlanId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MlanId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MlanId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MlanId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }
        public void FindByCode(int MlanId)
        {
            this.Adapter.SelectCommand.Parameters["@MlanId"].Value = MlanId;

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByMeId(int MeId)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            Fill();
            return this.DataTable;
        }
        public DataTable FindByMeRequest(int MeId, int MReId, short IsConfirm)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@MReId"].Value = MReId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;


            Fill();
            return this.DataTable;

        }
        public DataTable FindByMeRequest(int MeId, int MReId, short IsConfirm, short JustActive) 
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@MReId"].Value = MReId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            this.Adapter.SelectCommand.Parameters["@JustActive"].Value = JustActive;
            Fill();
            return this.DataTable;

        }
        public void FindForDelete(int MeId, int MeRId)
        {
  
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberLanguageDelete", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MReId", MeRId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = (int)TableCodes.MemberLanguage;

            adapter.Fill(DataTable);


        }
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.MemberDataSet.tblMemberLanguageDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_MlanId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_MlanId));
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
        public virtual int Insert(short LanId, short LqId, int MeId, string Description, int UserId, System.DateTime ModifiedDate)
        {
            this.Adapter.InsertCommand.Parameters[1].Value = ((short)(LanId));
            this.Adapter.InsertCommand.Parameters[2].Value = ((short)(LqId));
            this.Adapter.InsertCommand.Parameters[3].Value = ((int)(MeId));
            if ((Description == null))
            {
                throw new System.ArgumentNullException("Description");
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
        public virtual int Update(short LanId, short LqId, int MeId, string Description, int UserId, System.DateTime ModifiedDate, int Original_MlanId, byte[] Original_LastTimeStamp, int MlanId)
        {
            this.Adapter.UpdateCommand.Parameters[1].Value = ((short)(LanId));
            this.Adapter.UpdateCommand.Parameters[2].Value = ((short)(LqId));
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(MeId));
            if ((Description == null))
            {
                throw new System.ArgumentNullException("Description");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(Description));
            }
            this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Original_MlanId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(MlanId));
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
    }
}
