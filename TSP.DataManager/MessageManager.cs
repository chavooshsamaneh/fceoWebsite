using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace TSP.DataManager
{
    public class MessageManager:BaseObject
    {
        //static MessageManager()
        //{
        //    _tableId = TableType.Message;
        //}
         public MessageManager()
            : base()
        {
        }
        public MessageManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Message);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblMessage";
            tableMapping.ColumnMappings.Add("MsgId", "MsgId");
            tableMapping.ColumnMappings.Add("SenderId", "SenderId");
            tableMapping.ColumnMappings.Add("SenderType", "SenderType");
            tableMapping.ColumnMappings.Add("IsSenderPart", "IsSenderPart");
            tableMapping.ColumnMappings.Add("MsgTypeId", "MsgTypeId");
            tableMapping.ColumnMappings.Add("NeedConfirm", "NeedConfirm");
            tableMapping.ColumnMappings.Add("MsgSubject", "MsgSubject");
            tableMapping.ColumnMappings.Add("MsgBody", "MsgBody");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("RefMsg", "RefMsg");
            tableMapping.ColumnMappings.Add("TableType", "TableType");
            tableMapping.ColumnMappings.Add("TableId", "TableId");
            tableMapping.ColumnMappings.Add("GrId", "GrId");
            tableMapping.ColumnMappings.Add("Priority", "Priority");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("FutureStatus", "FutureStatus");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("RelateTo", "RelateTo");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectMessage";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefMsg", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefMsg", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SenderId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SenderId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SenderType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SenderType", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSenderPart", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSenderPart", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgTypeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Ult", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Ult", System.Data.DataRowVersion.Original, false, null, "", "", ""));


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteMessage";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MsgId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertMessage";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SenderId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SenderId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SenderType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SenderType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSenderPart", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSenderPart", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgTypeId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NeedConfirm", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "NeedConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgSubject", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgSubject", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgBody", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgBody", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefMsg", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefMsg", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Priority", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Priority", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FutureStatus", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "FutureStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RelateTo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RelateTo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateMessage";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SenderId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SenderId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SenderType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SenderType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSenderPart", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSenderPart", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgTypeId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NeedConfirm", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "NeedConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgSubject", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgSubject", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgBody", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgBody", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefMsg", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefMsg", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Priority", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Priority", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FutureStatus", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "FutureStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RelateTo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RelateTo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MsgId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MsgId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblMessageDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByMsgId(int Id)
        {
            this.ResetAllParameters();            
            this.Adapter.SelectCommand.Parameters["@MsgId"].Value = Id;

            this.Adapter.SelectCommand.Parameters["@RefMsg"].Value=-1;
            this.Adapter.SelectCommand.Parameters["@SenderId"].Value=-1;
            this.Adapter.SelectCommand.Parameters["@SenderType"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@IsSenderPart"].Value = -1;
            //this.Adapter.SelectCommand.Parameters["@MsgId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@MsgTypeId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@Ult"].Value = -1;


            Fill();
        }

        public void FindByCode(int Id,int SeId,int SeTId,bool IsSePId,int MsgTypeId)
        {
            this.Adapter.SelectCommand.Parameters["@RefMsg"].Value = Id;
            this.Adapter.SelectCommand.Parameters["@SenderId"].Value = SeId;
            this.Adapter.SelectCommand.Parameters["@SenderType"].Value = SeTId;
            this.Adapter.SelectCommand.Parameters["@IsSenderPart"].Value = IsSePId;
            this.Adapter.SelectCommand.Parameters["@MsgTypeId"].Value = MsgTypeId;
            Fill();
        }

        public void FindByRefMsgId(int Id)
        {
            this.Adapter.SelectCommand.Parameters["@RefMsg"].Value = Id;
            this.Adapter.SelectCommand.Parameters["@MsgTypeId"].Value = 2;            
            Fill();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_MsgId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_MsgId));
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
        public virtual int Insert(
                    int SenderId,
                    short SenderType,
                    bool IsSenderPart,
                    int RecieverId,
                    short RecieverType,
                    bool IsRecieverPart,
                    short MsgTypeId,
                    bool NeedConfirm,
                    string MsgBody,
                    string Date,
                    byte IsRead,
                    System.Nullable<int> RefMsg,
                    System.Nullable<int> TableType,
                    System.Nullable<int> TableId,
                    int UserId,
                    System.DateTime ModifiedDate)
        {
            this.Adapter.InsertCommand.Parameters[1].Value = ((int)(SenderId));
            this.Adapter.InsertCommand.Parameters[2].Value = ((short)(SenderType));
            this.Adapter.InsertCommand.Parameters[3].Value = ((bool)(IsSenderPart));
            this.Adapter.InsertCommand.Parameters[4].Value = ((int)(RecieverId));
            this.Adapter.InsertCommand.Parameters[5].Value = ((short)(RecieverType));
            this.Adapter.InsertCommand.Parameters[6].Value = ((bool)(IsRecieverPart));
            this.Adapter.InsertCommand.Parameters[7].Value = ((short)(MsgTypeId));
            this.Adapter.InsertCommand.Parameters[8].Value = ((bool)(NeedConfirm));
            if ((MsgBody == null))
            {
                this.Adapter.InsertCommand.Parameters[9].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[9].Value = ((string)(MsgBody));
            }
            if ((Date == null))
            {
                throw new System.ArgumentNullException("Date");
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[10].Value = ((string)(Date));
            }
            this.Adapter.InsertCommand.Parameters[11].Value = ((byte)(IsRead));
            if ((RefMsg.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[12].Value = ((int)(RefMsg.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[12].Value = System.DBNull.Value;
            }
            if ((TableType.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[13].Value = ((int)(TableType.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[13].Value = System.DBNull.Value;
            }
            if ((TableId.HasValue == true))
            {
                this.Adapter.InsertCommand.Parameters[14].Value = ((int)(TableId.Value));
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[14].Value = System.DBNull.Value;
            }
            this.Adapter.InsertCommand.Parameters[15].Value = ((int)(UserId));
            this.Adapter.InsertCommand.Parameters[16].Value = ((System.DateTime)(ModifiedDate));
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
        public virtual int Update(
                    int SenderId,
                    short SenderType,
                    bool IsSenderPart,
                    int RecieverId,
                    short RecieverType,
                    bool IsRecieverPart,
                    short MsgTypeId,
                    bool NeedConfirm,
                    string MsgBody,
                    string Date,
                    byte IsRead,

                    System.Nullable<int> RefMsg,
                    System.Nullable<int> TableType,
                    System.Nullable<int> TableId,
                    int UserId,
                    System.DateTime ModifiedDate,
                    int Original_MsgId,
                    byte[] Original_LastTimeStamp,
                    int MsgId)
        {
            this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(SenderId));
            this.Adapter.UpdateCommand.Parameters[2].Value = ((short)(SenderType));
            this.Adapter.UpdateCommand.Parameters[3].Value = ((bool)(IsSenderPart));
            this.Adapter.UpdateCommand.Parameters[4].Value = ((int)(RecieverId));
            this.Adapter.UpdateCommand.Parameters[5].Value = ((short)(RecieverType));
            this.Adapter.UpdateCommand.Parameters[6].Value = ((bool)(IsRecieverPart));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((short)(MsgTypeId));
            this.Adapter.UpdateCommand.Parameters[8].Value = ((bool)(NeedConfirm));
            if ((MsgBody == null))
            {
                this.Adapter.UpdateCommand.Parameters[9].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[9].Value = ((string)(MsgBody));
            }
            if ((Date == null))
            {
                throw new System.ArgumentNullException("Date");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[10].Value = ((string)(Date));
            }
            this.Adapter.UpdateCommand.Parameters[11].Value = ((byte)(IsRead));
            if ((RefMsg.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[12].Value = ((int)(RefMsg.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[12].Value = System.DBNull.Value;
            }
            if ((TableType.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[13].Value = ((int)(TableType.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[13].Value = System.DBNull.Value;
            }
            if ((TableId.HasValue == true))
            {
                this.Adapter.UpdateCommand.Parameters[14].Value = ((int)(TableId.Value));
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[14].Value = System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[15].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[16].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[17].Value = ((int)(Original_MsgId));
            if ((Original_LastTimeStamp == null))
            {
                throw new System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[18].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[19].Value = ((int)(MsgId));
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
        public int CountOfUnRead(int ReceiverId,int ReceiverType,bool IsReceiverPart)
        {
            int Count = 0;
            SqlCommand SqlCom=new SqlCommand("spCountOfUnread", this.Connection);

            SqlCom.CommandType=System.Data.CommandType.StoredProcedure;
            SqlCom.Parameters.Add("@ReceiverId", SqlDbType.Int);
            SqlCom.Parameters.Add("@ReceiverType", SqlDbType.Int);
            SqlCom.Parameters.Add("@IsReceiverPart", SqlDbType.Bit);

         
            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            SqlCom.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            SqlCom.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(IsReceiverPart.ToString()))
                IsReceiverPart = false;
            SqlCom.Parameters["@IsReceiverPart"].Value = IsReceiverPart;

            this.Connection.Open();
            Count=int.Parse(SqlCom.ExecuteScalar().ToString());
            this.Connection.Close();
            return Count;          

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public int CountOfUnRead2(int ReceiverId, int ReceiverType, bool IsReceiverPart)
        {
            int Count = 0;
            SqlCommand SqlCom = new SqlCommand("spCountOfUnread2", this.Connection);

            SqlCom.CommandType = System.Data.CommandType.StoredProcedure;
            SqlCom.Parameters.Add("@ReceiverId", SqlDbType.Int);
            SqlCom.Parameters.Add("@ReceiverType", SqlDbType.Int);
            SqlCom.Parameters.Add("@IsReceiverPart", SqlDbType.Bit);


            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            SqlCom.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            SqlCom.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(IsReceiverPart.ToString()))
                IsReceiverPart = false;
            SqlCom.Parameters["@IsReceiverPart"].Value = IsReceiverPart;

            this.Connection.Open();
            Count = int.Parse(SqlCom.ExecuteScalar().ToString());
            this.Connection.Close();
            return Count;

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public int CountOfReceived(int MsgId)
        {
            int Count = 0;
            DataTable dt = new DataTable();
            SqlCommand SqlCom = new SqlCommand("spCountOfReceived", this.Connection);
            SqlCom.CommandType = System.Data.CommandType.StoredProcedure;

            SqlCom.Parameters.Add("@MsgId", SqlDbType.Int);
           

            if (string.IsNullOrEmpty(MsgId.ToString()))
                MsgId = -1;
            SqlCom.Parameters["@MsgId"].Value = MsgId;

            this.Connection.Open();
            Count = int.Parse(SqlCom.ExecuteScalar().ToString());
            this.Connection.Close();
            return Count;             

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public int CountOfReply(int MsgId,int ReceiverId,int ReceiverType,bool IsReceiverPart)
        {
            int Count = 0;
            DataTable dt = new DataTable();
            SqlCommand SqlCom = new SqlCommand("spCountOfReply", this.Connection);
            SqlCom.CommandType = System.Data.CommandType.StoredProcedure;

            SqlCom.Parameters.Add("@MsgId", SqlDbType.Int);
             SqlCom.Parameters.Add("@ReceiverId", SqlDbType.Int);
            SqlCom.Parameters.Add("@ReceiverType", SqlDbType.Int);
            SqlCom.Parameters.Add("@IsReceiverPart", SqlDbType.Bit);

            if (string.IsNullOrEmpty(MsgId.ToString()))
                MsgId = -1;
            SqlCom.Parameters["@MsgId"].Value = MsgId;

            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            SqlCom.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            SqlCom.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(IsReceiverPart.ToString()))
                IsReceiverPart = false;
            SqlCom.Parameters["@IsReceiverPart"].Value = IsReceiverPart;

            

            this.Connection.Open();
            Count = int.Parse(SqlCom.ExecuteScalar().ToString());
            this.Connection.Close();
            return Count;

        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMsgSent(int SenderId, int SenderType, bool IsSenderPart, int Ult)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessage", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@SenderId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@SenderType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsSenderPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@Ult", SqlDbType.Int);

       
            if (string.IsNullOrEmpty(SenderId.ToString()))
                SenderId = -1;
            adapter.SelectCommand.Parameters["@SenderId"].Value = SenderId;

            if (string.IsNullOrEmpty(SenderType.ToString()))
                SenderType = -1;
            adapter.SelectCommand.Parameters["@SenderType"].Value = SenderType;

            if (string.IsNullOrEmpty(IsSenderPart.ToString()))
                IsSenderPart = false;
            adapter.SelectCommand.Parameters["@IsSenderPart"].Value = IsSenderPart;

            if (string.IsNullOrEmpty(Ult.ToString()))
                Ult = -1;
            adapter.SelectCommand.Parameters["@Ult"].Value = Ult;           
           
         
            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMsgSentConfirm(int SenderId, int SenderType, bool IsSenderPart, int Ult)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessage", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@SenderId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@SenderType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsSenderPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@Ult", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@RefMsg", SqlDbType.Int);



            if (string.IsNullOrEmpty(SenderId.ToString()))
                SenderId = -1;
            adapter.SelectCommand.Parameters["@SenderId"].Value = SenderId;

            if (string.IsNullOrEmpty(SenderType.ToString()))
                SenderType = -1;
            adapter.SelectCommand.Parameters["@SenderType"].Value = SenderType;

            if (string.IsNullOrEmpty(IsSenderPart.ToString()))
                IsSenderPart = false;
            adapter.SelectCommand.Parameters["@IsSenderPart"].Value = IsSenderPart;

            if (string.IsNullOrEmpty(Ult.ToString()))
                Ult = -1;
            adapter.SelectCommand.Parameters["@Ult"].Value = Ult;

            adapter.SelectCommand.Parameters["@RefMsg"].Value = System.DBNull.Value;

            adapter.Fill(dt);
            return (dt);

        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMsgSentOfThis(int SenderId, int SenderType, bool IsSenderPart, int RefMsg)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessage", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@SenderId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@SenderType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsSenderPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@RefMsg", SqlDbType.Int);


            if (string.IsNullOrEmpty(SenderId.ToString()))
                SenderId = -1;
            adapter.SelectCommand.Parameters["@SenderId"].Value = SenderId;

            if (string.IsNullOrEmpty(SenderType.ToString()))
                SenderType = -1;
            adapter.SelectCommand.Parameters["@SenderType"].Value = SenderType;

            if (string.IsNullOrEmpty(IsSenderPart.ToString()))
                IsSenderPart = false;
            adapter.SelectCommand.Parameters["@IsSenderPart"].Value = IsSenderPart;

            if (string.IsNullOrEmpty(RefMsg.ToString()))
                RefMsg = -1;
            adapter.SelectCommand.Parameters["@RefMsg"].Value = RefMsg;


            adapter.Fill(dt);
            return (dt);

        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMsgReceived(int ReceiverId, int ReceiverType, bool IsReceiverPart, int Ult)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessage2", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsReceiverPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@Ult", SqlDbType.Int);  


            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            adapter.SelectCommand.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            adapter.SelectCommand.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(IsReceiverPart.ToString()))
                IsReceiverPart = false;
            adapter.SelectCommand.Parameters["@IsReceiverPart"].Value = IsReceiverPart;

            if (string.IsNullOrEmpty(Ult.ToString()))
                Ult = -1;
            adapter.SelectCommand.Parameters["@Ult"].Value = Ult;




            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMsgReceivdConfirm(int ReceiverId, int ReceiverType, bool IsReceiverPart, int Ult)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessage2", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsReceiverPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@Ult", SqlDbType.Int);
           // adapter.SelectCommand.Parameters.Add("@RefMsg", SqlDbType.Int);




            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            adapter.SelectCommand.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            adapter.SelectCommand.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(IsReceiverPart.ToString()))
                IsReceiverPart = false;
            adapter.SelectCommand.Parameters["@IsReceiverPart"].Value = IsReceiverPart;

            if (string.IsNullOrEmpty(Ult.ToString()))
                Ult = -1;
            adapter.SelectCommand.Parameters["@Ult"].Value = Ult;

            //adapter.SelectCommand.Parameters["@RefMsg"].Value = System.DBNull.Value;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMsgReceivedOfthis(int ReceiverId, int ReceiverType, bool IsReceiverPart, int RefMsg)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessage2", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsReceiverPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@RefMsg", SqlDbType.Int);


            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            adapter.SelectCommand.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            adapter.SelectCommand.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(IsReceiverPart.ToString()))
                IsReceiverPart = false;
            adapter.SelectCommand.Parameters["@IsReceiverPart"].Value = IsReceiverPart;

            if (string.IsNullOrEmpty(RefMsg.ToString()))
                RefMsg = -1;
            adapter.SelectCommand.Parameters["@RefMsg"].Value = RefMsg;


            adapter.Fill(dt);
            return (dt);

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAllMsg(int SenderId, int SenderType, bool IsSenderPart, int Ult,int Tabletype,int TableId,bool NeedConfirm)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessages", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@SenderId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@SenderType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsSenderPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@Ult", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@NeedConfirm", SqlDbType.Bit);


            if (string.IsNullOrEmpty(SenderId.ToString()))
                SenderId = -1;
            adapter.SelectCommand.Parameters["@SenderId"].Value = SenderId;

            if (string.IsNullOrEmpty(SenderType.ToString()))
                SenderType = -1;
            adapter.SelectCommand.Parameters["@SenderType"].Value = SenderType;

            if (string.IsNullOrEmpty(IsSenderPart.ToString()))
                IsSenderPart = false;
            adapter.SelectCommand.Parameters["@IsSenderPart"].Value = IsSenderPart;

            if (string.IsNullOrEmpty(Ult.ToString()))
                Ult = -1;
            adapter.SelectCommand.Parameters["@Ult"].Value = Ult;

            if (string.IsNullOrEmpty(Tabletype.ToString()))
                Tabletype = -1;
            adapter.SelectCommand.Parameters["@TableType"].Value = Tabletype;


            if (string.IsNullOrEmpty(TableId.ToString()))
                TableId = -1;
            adapter.SelectCommand.Parameters["@TableId"].Value = TableId;

            if (string.IsNullOrEmpty(NeedConfirm.ToString()))
                NeedConfirm = true;
            adapter.SelectCommand.Parameters["@NeedConfirm"].Value = NeedConfirm;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectAllMsgOfThis(int SenderId, int SenderType, bool IsSenderPart, int RefMsg,int Tabletype,int TableId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessages", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@SenderId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@SenderType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsSenderPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@RefMsg", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int);



            if (string.IsNullOrEmpty(SenderId.ToString()))
                SenderId = -1;
            adapter.SelectCommand.Parameters["@SenderId"].Value = SenderId;

            if (string.IsNullOrEmpty(SenderType.ToString()))
                SenderType = -1;
            adapter.SelectCommand.Parameters["@SenderType"].Value = SenderType;

            if (string.IsNullOrEmpty(IsSenderPart.ToString()))
                IsSenderPart = false;
            adapter.SelectCommand.Parameters["@IsSenderPart"].Value = IsSenderPart;

            if (string.IsNullOrEmpty(RefMsg.ToString()))
                RefMsg = -1;
            adapter.SelectCommand.Parameters["@RefMsg"].Value = RefMsg;

            if (string.IsNullOrEmpty(Tabletype.ToString()))
                Tabletype = -1;
            adapter.SelectCommand.Parameters["@TableType"].Value = Tabletype;


            if (string.IsNullOrEmpty(TableId.ToString()))
                TableId = -1;
            adapter.SelectCommand.Parameters["@TableId"].Value = TableId;

            adapter.Fill(dt);
            return (dt);

        }
        /// <summary>
        /// Use For Confirming Messages Of Memmbers
        /// </summary>
        /// <param name="ReceiverId"></param>
        /// <param name="ReceiverType"></param>
        /// <param name="MsgId"></param>
        /// <param name="NeedConfirm"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMsgOfMember(int ReceiverId, int ReceiverType,int MsgId,short NeedConfirm)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessageOfMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@MsgId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@NeedConfirm", SqlDbType.SmallInt);

            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            adapter.SelectCommand.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            adapter.SelectCommand.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(MsgId.ToString()))
                MsgId = -1;
            adapter.SelectCommand.Parameters["@MsgId"].Value = MsgId;

            if (string.IsNullOrEmpty(NeedConfirm.ToString()))
                NeedConfirm = -1;
            adapter.SelectCommand.Parameters["@NeedConfirm"].Value = NeedConfirm;

            adapter.Fill(dt);
            return (dt);

        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMsgOfConfirmSMS(int TableType, int TableId, int Recieverid)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessageOfConfirmSMS", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4,"TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4,"TableId").Value = TableId;
            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int,4, "ReceiverId").Value = Recieverid;
          
            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMessageOFInbox(int ReceiverId, int ReceiverType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessageOfInbox", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int, 4, "ReceiverId").Value = ReceiverId;
            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int, 4, "ReceiverType").Value = ReceiverType;

            adapter.Fill(dt);
            return (dt);
        }
    }
}
