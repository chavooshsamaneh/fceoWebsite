using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
    public class MessageReceiverManager:BaseObject
    {
        // static MessageReceiverManager()
        //{
        //    _tableId = TableType.Message;
        //}
        public MessageReceiverManager()
            : base()
        {
        }
        public MessageReceiverManager(System.Data.DataSet ds)
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
            tableMapping.DataSetTable = "tblMessageReceivers";
            tableMapping.ColumnMappings.Add("MsgrId", "MsgrId");
            tableMapping.ColumnMappings.Add("MsgId", "MsgId");
            tableMapping.ColumnMappings.Add("IsRead", "IsRead");
            tableMapping.ColumnMappings.Add("ReadDate", "ReadDate");
            tableMapping.ColumnMappings.Add("ReceiverId", "ReceiverId");
            tableMapping.ColumnMappings.Add("ReceiverType", "ReceiverType");
            tableMapping.ColumnMappings.Add("IsReceiverPart", "IsReceiverPart");
            tableMapping.ColumnMappings.Add("Answer", "Answer");
            tableMapping.ColumnMappings.Add("ConfirmDate", "ConfirmDate");
            tableMapping.ColumnMappings.Add("Body", "Body");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("IsResignation", "IsResignation");
            tableMapping.ColumnMappings.Add("ResignId", "ResignId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);



            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectMessageReceivers";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgId", System.Data.DataRowVersion.Current, false, null, "", "", ""));


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteMessageReceivers";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MsgrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertMessageReceivers";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsRead", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsRead", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReadDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ReadDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsReceiverPart", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsReceiverPart", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Answer", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Answer", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConfirmDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ConfirmDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Body", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Body", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsResignation", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsResignation", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateMessageReceivers";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsRead", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsRead", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReadDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ReadDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ReceiverType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsReceiverPart", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsReceiverPart", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Answer", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Answer", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConfirmDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ConfirmDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Body", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Body", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsResignation", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsResignation", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResignId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResignId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MsgrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsgrId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgrId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MsgrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblMessageReceiversDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        public void FindByCode(int MsgrId, int ReceiverId, int ReceiverType, int MsgId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectMessageReceivers";
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverId", System.Data.SqlDbType.Int, 0, "ReceiverId"));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverType", System.Data.SqlDbType.Int, 0, "ReceiverType"));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgrId", System.Data.SqlDbType.Int, 0, "MsgrId"));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgId", System.Data.SqlDbType.Int, 0, "MsgId"));

            this.Adapter.SelectCommand.Parameters["@ReceiverId"].Value = ReceiverId;
            this.Adapter.SelectCommand.Parameters["@ReceiverType"].Value = ReceiverType;
            this.Adapter.SelectCommand.Parameters["@MsgrId"].Value = MsgrId;
            this.Adapter.SelectCommand.Parameters["@MsgId"].Value = MsgId;
          
            Fill();
        }


        public void FindByMsgrId(int MsgrId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectMessageReceivers";
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceiverId", System.Data.SqlDbType.Int, 0, "ReceiverId"));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgrId", System.Data.SqlDbType.Int, 0, "MsgrId"));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsgId", System.Data.SqlDbType.Int, 0, "MsgId"));

            this.Adapter.SelectCommand.Parameters["@ReceiverId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@MsgrId"].Value = MsgrId;
            this.Adapter.SelectCommand.Parameters["@MsgId"].Value = -1;

            Fill();
        }

        public DataTable FindByMsgId(int Id, int ReId, int ReTId, bool IsRePId,int Answered)
        {
            DataTable dt = new DataTable();
            //SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessageSendAndReceivers", this.Connection);
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectMessageSendAndReceivers";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@MsgId", SqlDbType.Int).Value= Id;

            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int).Value = ReId;

            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int).Value = ReTId;


            adapter.SelectCommand.Parameters.Add("@Answered", SqlDbType.Int).Value = Answered;
            
            adapter.Fill(dt);
            return (dt);
        }

        

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_MsgrId, byte[] Original_LastTimeStamp)
        {
            this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_MsgrId));
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
        public virtual int Insert(int MsgId, int ReceiverId, int ReceiverType, bool IsReceiverPart, bool InActive, int UserId, System.DateTime ModifiedDate)
        {
            this.Adapter.InsertCommand.Parameters[1].Value = ((int)(MsgId));
            this.Adapter.InsertCommand.Parameters[2].Value = ((int)(ReceiverId));
            this.Adapter.InsertCommand.Parameters[3].Value = ((int)(ReceiverType));
            this.Adapter.InsertCommand.Parameters[4].Value = ((bool)(IsReceiverPart));
            this.Adapter.InsertCommand.Parameters[5].Value = ((bool)(InActive));
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
        public virtual int Update(int MsgId, int ReceiverId, int ReceiverType, bool IsReceiverPart, bool InActive, int UserId, System.DateTime ModifiedDate, int Original_MsgrId, byte[] Original_LastTimeStamp, int MsgrId)
        {
            this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(MsgId));
            this.Adapter.UpdateCommand.Parameters[2].Value = ((int)(ReceiverId));
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(ReceiverType));
            this.Adapter.UpdateCommand.Parameters[4].Value = ((bool)(IsReceiverPart));
            this.Adapter.UpdateCommand.Parameters[5].Value = ((bool)(InActive));
            this.Adapter.UpdateCommand.Parameters[6].Value = ((int)(UserId));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((System.DateTime)(ModifiedDate));
            this.Adapter.UpdateCommand.Parameters[8].Value = ((int)(Original_MsgrId));
            if ((Original_LastTimeStamp == null))
            {
                throw new global::System.ArgumentNullException("Original_LastTimeStamp");
            }
            else
            {
                this.Adapter.UpdateCommand.Parameters[9].Value = ((byte[])(Original_LastTimeStamp));
            }
            this.Adapter.UpdateCommand.Parameters[10].Value = ((int)(MsgrId));
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

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMessageExpired(string dtNow, int ReceiverId, int ReceiverType, bool IsReceiverPart)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessageReceivers2", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsReceiverPart", SqlDbType.Bit);
            adapter.SelectCommand.Parameters.Add("@dtNow", SqlDbType.VarChar, 10);
            adapter.SelectCommand.Parameters.Add("@Expired", SqlDbType.Bit);



            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            adapter.SelectCommand.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            adapter.SelectCommand.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(IsReceiverPart.ToString()))
                IsReceiverPart = false;
            adapter.SelectCommand.Parameters["@IsReceiverPart"].Value = IsReceiverPart;

            if (string.IsNullOrEmpty(dtNow))
                dtNow = "1";
            adapter.SelectCommand.Parameters["@dtNow"].Value = dtNow;

            adapter.SelectCommand.Parameters["@Expired"].Value = 1;

            adapter.Fill(dt);
            return (dt);

        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectReceiversOfThis(int MsgId, int ReceiverId, int ReceiverType, bool IsReceiverPart)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessageReceivers2", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@MsgId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsReceiverPart", SqlDbType.Bit);
          



            if (string.IsNullOrEmpty(MsgId.ToString()))
                MsgId = -1;
            adapter.SelectCommand.Parameters["@MsgId"].Value = MsgId;

            if (string.IsNullOrEmpty(ReceiverId.ToString()))
                ReceiverId = -1;
            adapter.SelectCommand.Parameters["@ReceiverId"].Value = ReceiverId;

            if (string.IsNullOrEmpty(ReceiverType.ToString()))
                ReceiverType = -1;
            adapter.SelectCommand.Parameters["@ReceiverType"].Value = ReceiverType;

            if (string.IsNullOrEmpty(IsReceiverPart.ToString()))
                IsReceiverPart = false;
            adapter.SelectCommand.Parameters["@IsReceiverPart"].Value = IsReceiverPart;

             
            
            adapter.Fill(dt);
            return (dt);

        }

        public DataTable FindByMsgrID(int MsgrId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SpSelectMessageRecieverByMsgrId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@MsgrId", SqlDbType.Int);

            adapter.SelectCommand.Parameters["@MsgrId"].Value = MsgrId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSendAndReceiveMsg(int MsgId, int MsgReceiverId, int ReceiverType,int Answered)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMessageSendAndReceivers", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add("@MsgId", SqlDbType.Int).Value = MsgId;

            adapter.SelectCommand.Parameters.Add("@ReceiverId", SqlDbType.Int).Value = MsgReceiverId;

            adapter.SelectCommand.Parameters.Add("@ReceiverType", SqlDbType.Int).Value = ReceiverType;

            adapter.SelectCommand.Parameters.Add("@Answered", SqlDbType.Int).Value = Answered;

            adapter.Fill(dt);
            return (dt);
        }
    }
}
