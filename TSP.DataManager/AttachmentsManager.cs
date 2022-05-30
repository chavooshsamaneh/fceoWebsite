using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class AttachmentsManager : BaseObject
    {
        // static AttachmentsManager()
        //{
        //    _tableId = TableType.Attachments;
        //}
        public AttachmentsManager()
            : base()
        {
        }
        public AttachmentsManager(System.Data.DataSet ds)
            : base(ds)
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Attachments);
        }

        public static Permission GetUserPermissionForEngOffice(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EngOfficeAttachments);
        }

        public static Permission GetUserPermissionForOffice(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeAttachments);
        }

        public static Permission GetUserPermissionForOffDoc(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeDocumentAttachments);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblAttachments";
            tableMapping.ColumnMappings.Add("AttachId", "AttachId");
            tableMapping.ColumnMappings.Add("TtId", "TtId");
            tableMapping.ColumnMappings.Add("RefTable", "RefTable");
            tableMapping.ColumnMappings.Add("AttId", "AttId");
            tableMapping.ColumnMappings.Add("AtContent", "AtContent");
            tableMapping.ColumnMappings.Add("FileName", "FileName");
            tableMapping.ColumnMappings.Add("FilePath", "FilePath");
            tableMapping.ColumnMappings.Add("IsValid", "IsValid");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModfiedDate", "ModfiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectAttachments";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@AttachId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@TtId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@RefTable", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@AttId", System.Data.SqlDbType.SmallInt);
      
            

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteAttachments";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_AttachId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AttachId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertAttachments";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TtId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefTable", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefTable", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AttId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "AttId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AtContent", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "AtContent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FilePath", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FilePath", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsValid", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsValid", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModfiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModfiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateAttachments";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TtId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefTable", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefTable", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AttId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "AttId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AtContent", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "AtContent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FilePath", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FilePath", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsValid", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsValid", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModfiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModfiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_AttachId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AttachId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AttachId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "AttachId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.NezamFarsDataSet.tblAttachmentsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByTableType(int ttid)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TtId"].Value = ttid;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTablePrimaryKey(int ttid, int code)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TtId"].Value = ttid;
            this.Adapter.SelectCommand.Parameters["@RefTable"].Value = code;
            Fill();
            return this.DataTable;

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTablePrimaryKey_AttId(int ttid, int code, Int16 AttId, int IsMeTemp)
        {

            if (AttId == (int)AttachType.IdNo
                || AttId == (int)AttachType.ResidentDoc
                || AttId == (int)AttachType.SoldierCard
                || AttId == (int)AttachType.SSN)
            {
                DataTable.Clear();
                SqlDataAdapter adapter = new SqlDataAdapter("spSelectAttachmentsForMemberInfo", this.Connection);
                adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@TtId", ttid);
                adapter.SelectCommand.Parameters.AddWithValue("@RefTable", code);
                adapter.SelectCommand.Parameters.AddWithValue("@AttId", AttId);
                adapter.SelectCommand.Parameters.AddWithValue("@IsMeTemp", IsMeTemp);
                
                adapter.SelectCommand.Transaction = this.Transaction;

                adapter.Fill(DataTable);
                return (DataTable);
            }
            else
            {
                ResetAllParameters();
                this.Adapter.SelectCommand.Parameters["@TtId"].Value = ttid;
                this.Adapter.SelectCommand.Parameters["@RefTable"].Value = code;
                this.Adapter.SelectCommand.Parameters["@AttId"].Value = AttId;                
                Fill();
                return this.DataTable;
            }
        }

        /// <summary>
        /// For Perminent Member
        /// </summary>
        /// <param name="ttid"></param>
        /// <param name="code"></param>
        /// <param name="AttId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTablePrimaryKey_AttId(int ttid, int code, Int16 AttId)
        {

            return FindByTablePrimaryKey_AttId(ttid, code, AttId, 0);
        }

     [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTablePrimaryKey_AttIdTemp(int ttid, int code, Int16 AttId)
        {
            return FindByTablePrimaryKey_AttId(ttid, code, AttId,1);
        }

        public void FindByCode(int AttachId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AttachId"].Value = AttachId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTrainingArchive(int SeTtId, int CrsTtId, string Name)
        {
            DataTable dt = new DataTable();
            //DataTable dt = new DataManager.NezamFarsDataSet.tblAttachmentsDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingArchive", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SeTtId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@CrsTtId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 64);

            adapter.SelectCommand.Parameters["@SeTtId"].Value = SeTtId;
            adapter.SelectCommand.Parameters["@CrsTtId"].Value = CrsTtId;
            adapter.SelectCommand.Parameters["@Name"].Value = Name;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEntezamiAttachments(int AttachId, int RefTable, int Complain, int Reply, int Rivision, int ClnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectAttachmentsForEntezami", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@AttachId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@RefTable", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@Complain", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@Reply", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@Rivision", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@ClnId", SqlDbType.Int);

            adapter.SelectCommand.Parameters["@AttachId"].Value = AttachId;
            adapter.SelectCommand.Parameters["@RefTable"].Value = RefTable;
            adapter.SelectCommand.Parameters["@Complain"].Value = Complain;
            adapter.SelectCommand.Parameters["@Reply"].Value = Reply;
            adapter.SelectCommand.Parameters["@Rivision"].Value = Rivision;
            adapter.SelectCommand.Parameters["@ClnId"].Value = ClnId;
            adapter.Fill(dt);
            return (dt);
        }

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        //public virtual int Delete(int Original_AttachId, byte[] Original_LastTimeStamp)
        //{
        //    this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_AttachId));
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
        //public virtual int Insert(int TtId, int RefTable, short AttId, byte[] AtContent, string FilePath, byte IsValid, string Description, int UserId, System.DateTime ModfiedDate)
        //{
        //    this.Adapter.InsertCommand.Parameters[1].Value = ((int)(TtId));
        //    this.Adapter.InsertCommand.Parameters[2].Value = ((int)(RefTable));
        //    this.Adapter.InsertCommand.Parameters[3].Value = ((short)(AttId));
        //    if ((AtContent == null))
        //    {
        //        throw new System.ArgumentNullException("AtContent");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[4].Value = ((byte[])(AtContent));
        //    }
        //    if ((FilePath == null))
        //    {
        //        throw new System.ArgumentNullException("FilePath");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[5].Value = ((string)(FilePath));
        //    }
        //    this.Adapter.InsertCommand.Parameters[6].Value = ((byte)(IsValid));
        //    if ((Description == null))
        //    {
        //        this.Adapter.InsertCommand.Parameters[7].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[7].Value = ((string)(Description));
        //    }
        //    this.Adapter.InsertCommand.Parameters[8].Value = ((int)(UserId));
        //    this.Adapter.InsertCommand.Parameters[9].Value = ((System.DateTime)(ModfiedDate));
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
        //public virtual int Update(int TtId, int RefTable, short AttId, byte[] AtContent, string FilePath, byte IsValid, string Description, int UserId, System.DateTime ModfiedDate, int Original_AttachId, byte[] Original_LastTimeStamp, int AttachId)
        //{
        //    this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(TtId));
        //    this.Adapter.UpdateCommand.Parameters[2].Value = ((int)(RefTable));
        //    this.Adapter.UpdateCommand.Parameters[3].Value = ((short)(AttId));
        //    if ((AtContent == null))
        //    {
        //        throw new System.ArgumentNullException("AtContent");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[4].Value = ((byte[])(AtContent));
        //    }
        //    if ((FilePath == null))
        //    {
        //        throw new System.ArgumentNullException("FilePath");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(FilePath));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[6].Value = ((byte)(IsValid));
        //    if ((Description == null))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[7].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[7].Value = ((string)(Description));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[8].Value = ((int)(UserId));
        //    this.Adapter.UpdateCommand.Parameters[9].Value = ((System.DateTime)(ModfiedDate));
        //    this.Adapter.UpdateCommand.Parameters[10].Value = ((int)(Original_AttachId));
        //    if ((Original_LastTimeStamp == null))
        //    {
        //        throw new System.ArgumentNullException("Original_LastTimeStamp");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[11].Value = ((byte[])(Original_LastTimeStamp));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[12].Value = ((int)(AttachId));
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

    }
}
