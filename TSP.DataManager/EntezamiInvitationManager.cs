using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
    public class EntezamiInvitationManager:BaseObject
    {
        //static EntezamiInvitationManager()
        //{
        //    _tableId = TableType.EntezamiInvitation;
        //}
         public EntezamiInvitationManager()
            : base()
        {
        }
        public EntezamiInvitationManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EntezamiInvitation);
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Entezami.Invitation";
            tableMapping.ColumnMappings.Add("EiId", "EiId");
            tableMapping.ColumnMappings.Add("ClnId", "ClnId");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("TimeHour", "TimeHour");
            tableMapping.ColumnMappings.Add("TimeMin", "TimeMin");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectInvitation";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@EiId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ClnId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteInvitation";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EiId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EiId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
      
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertInvitation";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ClnId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TimeHour", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TimeHour", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TimeMin", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TimeMin", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateInvitation";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClnId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ClnId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TimeHour", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TimeHour", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TimeMin", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TimeMin", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EiId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EiId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EiId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EiId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.EntezamiDataSet.EntezamiInvitationDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int EiId)
        {
            this.Adapter.SelectCommand.Parameters["@EiId"].Value = EiId;
            this.Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByCode(int EiId , int ClnId)
        {
            this.Adapter.SelectCommand.Parameters["@EiId"].Value = EiId;
            this.Adapter.SelectCommand.Parameters["@ClnId"].Value = ClnId;
            this.Fill();
            return this.DataTable;
        }

        #region old
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        //public virtual int Delete(int Original_EiId, byte[] Original_LastTimeStamp)
        //{
        //    this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_EiId));
        //    if ((Original_LastTimeStamp == null))
        //    {
        //        throw new System.ArgumentNullException("Original_LastTimeStamp");
        //    }
        //    else
        //    {
        //        this.Adapter.DeleteCommand.Parameters[2].Value = ((byte[])(Original_LastTimeStamp));
        //    }
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
        //    if (((this.Adapter.DeleteCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.DeleteCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.DeleteCommand.Connection.Close();
        //        }
        //    }
        //}

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        //public virtual int Insert(int ClnId, string Date, System.Nullable<byte> TimeHour, System.Nullable<byte> TimeMin, int UserId, System.DateTime ModifiedDate)
        //{
        //    this.Adapter.InsertCommand.Parameters[1].Value = ((int)(ClnId));
        //    if ((Date == null))
        //    {
        //        throw new System.ArgumentNullException("Date");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[2].Value = ((string)(Date));
        //    }
        //    if ((TimeHour.HasValue == true))
        //    {
        //        this.Adapter.InsertCommand.Parameters[3].Value = ((byte)(TimeHour.Value));
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[3].Value = System.DBNull.Value;
        //    }
        //    if ((TimeMin.HasValue == true))
        //    {
        //        this.Adapter.InsertCommand.Parameters[4].Value = ((byte)(TimeMin.Value));
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[4].Value = System.DBNull.Value;
        //    }
        //    this.Adapter.InsertCommand.Parameters[5].Value = ((int)(UserId));
        //    this.Adapter.InsertCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
        //    if (((this.Adapter.InsertCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.InsertCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.InsertCommand.Connection.Close();
        //        }
        //    }
        //}

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        //public virtual int Update(int ClnId, string Date, System.Nullable<byte> TimeHour, System.Nullable<byte> TimeMin, int UserId, System.DateTime ModifiedDate, int Original_EiId, byte[] Original_LastTimeStamp, int EiId)
        //{
        //    this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(ClnId));
        //    if ((Date == null))
        //    {
        //        throw new System.ArgumentNullException("Date");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(Date));
        //    }
        //    if ((TimeHour.HasValue == true))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[3].Value = ((byte)(TimeHour.Value));
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[3].Value = System.DBNull.Value;
        //    }
        //    if ((TimeMin.HasValue == true))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[4].Value = ((byte)(TimeMin.Value));
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[4].Value = System.DBNull.Value;
        //    }
        //    this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(UserId));
        //    this.Adapter.UpdateCommand.Parameters[6].Value = ((System.DateTime)(ModifiedDate));
        //    this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Original_EiId));
        //    if ((Original_LastTimeStamp == null))
        //    {
        //        throw new System.ArgumentNullException("Original_LastTimeStamp");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[8].Value = ((byte[])(Original_LastTimeStamp));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(EiId));
        //    System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
        //    if (((this.Adapter.UpdateCommand.Connection.State & System.Data.ConnectionState.Open)
        //                != System.Data.ConnectionState.Open))
        //    {
        //        this.Adapter.UpdateCommand.Connection.Open();
        //    }
        //    try
        //    {
        //        int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
        //        return returnValue;
        //    }
        //    finally
        //    {
        //        if ((previousConnectionState == System.Data.ConnectionState.Closed))
        //        {
        //            this.Adapter.UpdateCommand.Connection.Close();
        //        }
        //    }
        //}
        #endregion
    }
}
