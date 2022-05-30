using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class ExpertFileManager:BaseObject
    {        
          public ExpertFileManager()
            : base()
        {
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ExpertFile);
        }
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "ExpertExpertFile";
            tableMapping.ColumnMappings.Add("EfId", "EfId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("IssueDate", "IssueDate");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("DocumentFrontImageURL", "DocumentFrontImageURL");
            tableMapping.ColumnMappings.Add("DocumentBackImageURL", "DocumentBackImageURL");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectExpertExpertFile";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@EFId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int); 

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteExpertExpertFile";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_EfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EfId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertExpertExpertFile";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IssueDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IssueDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentFrontImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentFrontImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentBackImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentBackImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateExpertExpertFile";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IssueDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IssueDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentFrontImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentFrontImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentBackImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentBackImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_EfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EfId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EfId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "EfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));          
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.ExpertDataSet.ExpertExpertFileDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        public void FindByCode(int Id)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EfId"].Value = Id;
            Fill();
        }

        public void FindByMeId(int MeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            Fill();
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectExpertFileForManagmantpage(int EfId, int MeId, String FirstName, String LastName,int TaskId)
        {
            if (EfId == -1 && MeId == -1 && FirstName == "%" && LastName == "%" && TaskId==-1)
                return new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectExpertFileForManagmantpage", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@EfId", EfId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        public DataTable SelectExpertFileForWebService(int MeId,int EfId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectExpertFileForWebService", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@EfId", EfId);

            adapter.Fill(dt);

            return dt;
        }
        //public void FindMeIdEtId(int MeId,int EtId)
        //{
        //    this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
        //    this.Adapter.SelectCommand.Parameters["@EtId"].Value = EtId;
        //    Fill();
        //}

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable FindMeId(int MeId)
        //{
        //    this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
        //    Fill();
        //    return this.DataTable;
        //}

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        //public virtual int Delete(int Original_EfId, byte[] Original_LastTimeStamp)
        //{
        //    this.Adapter.DeleteCommand.Parameters[1].Value = ((int)(Original_EfId));
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
        //public virtual int Insert(int MeId, short EtId, string FileNo, string IssueDate, string ValidDate, string RenewDate, string SessionDate, string SessionNo, string Body, int UserId, System.DateTime ModifiedDate)
        //{
        //    this.Adapter.InsertCommand.Parameters[1].Value = ((int)(MeId));
        //    this.Adapter.InsertCommand.Parameters[2].Value = ((short)(EtId));
        //    if ((FileNo == null))
        //    {
        //        throw new System.ArgumentNullException("FileNo");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[3].Value = ((string)(FileNo));
        //    }
        //    if ((IssueDate == null))
        //    {
        //        throw new System.ArgumentNullException("IssueDate");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[4].Value = ((string)(IssueDate));
        //    }
        //    if ((ValidDate == null))
        //    {
        //        throw new System.ArgumentNullException("ValidDate");
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[5].Value = ((string)(ValidDate));
        //    }
        //    if ((RenewDate == null))
        //    {
        //        this.Adapter.InsertCommand.Parameters[6].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[6].Value = ((string)(RenewDate));
        //    }
        //    if ((SessionDate == null))
        //    {
        //        this.Adapter.InsertCommand.Parameters[7].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[7].Value = ((string)(SessionDate));
        //    }
        //    if ((SessionNo == null))
        //    {
        //        this.Adapter.InsertCommand.Parameters[8].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[8].Value = ((string)(SessionNo));
        //    }
        //    if ((Body == null))
        //    {
        //        this.Adapter.InsertCommand.Parameters[9].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.InsertCommand.Parameters[9].Value = ((string)(Body));
        //    }
        //    this.Adapter.InsertCommand.Parameters[10].Value = ((int)(UserId));
        //    this.Adapter.InsertCommand.Parameters[11].Value = ((System.DateTime)(ModifiedDate));
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
        //public virtual int Update(int MeId, short EtId, string FileNo, string IssueDate, string ValidDate, string RenewDate, string SessionDate, string SessionNo, string Body, int UserId, System.DateTime ModifiedDate, int Original_EfId, byte[] Original_LastTimeStamp, int EfId)
        //{
        //    this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(MeId));
        //    this.Adapter.UpdateCommand.Parameters[2].Value = ((short)(EtId));
        //    if ((FileNo == null))
        //    {
        //        throw new System.ArgumentNullException("FileNo");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(FileNo));
        //    }
        //    if ((IssueDate == null))
        //    {
        //        throw new System.ArgumentNullException("IssueDate");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(IssueDate));
        //    }
        //    if ((ValidDate == null))
        //    {
        //        throw new System.ArgumentNullException("ValidDate");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(ValidDate));
        //    }
        //    if ((RenewDate == null))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[6].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[6].Value = ((string)(RenewDate));
        //    }
        //    if ((SessionDate == null))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[7].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[7].Value = ((string)(SessionDate));
        //    }
        //    if ((SessionNo == null))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[8].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[8].Value = ((string)(SessionNo));
        //    }
        //    if ((Body == null))
        //    {
        //        this.Adapter.UpdateCommand.Parameters[9].Value = System.DBNull.Value;
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[9].Value = ((string)(Body));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[10].Value = ((int)(UserId));
        //    this.Adapter.UpdateCommand.Parameters[11].Value = ((System.DateTime)(ModifiedDate));
        //    this.Adapter.UpdateCommand.Parameters[12].Value = ((int)(Original_EfId));
        //    if ((Original_LastTimeStamp == null))
        //    {
        //        throw new System.ArgumentNullException("Original_LastTimeStamp");
        //    }
        //    else
        //    {
        //        this.Adapter.UpdateCommand.Parameters[13].Value = ((byte[])(Original_LastTimeStamp));
        //    }
        //    this.Adapter.UpdateCommand.Parameters[14].Value = ((int)(EfId));
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

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SearchExpertFile(int MeId, short EtId, string FileNo, string IssueDateFrom, string IssueDateTo,string ValidDateFrom, string ValidDateTo)
        //{
        //    DataTable dt = new DataManager.ExpertDataSet.ExpertExpertFileDataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter("spSearchExpertFile", this.Connection);
        //    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;            
        //    adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
        //    adapter.SelectCommand.Parameters.Add("@EtId", SqlDbType.SmallInt);
        //    adapter.SelectCommand.Parameters.Add("@FileNo", SqlDbType.VarChar, 50);
        //    adapter.SelectCommand.Parameters.Add("@IssueDateFrom", SqlDbType.VarChar, 10);
        //    adapter.SelectCommand.Parameters.Add("@IssueDateTo", SqlDbType.VarChar, 10);
        //    adapter.SelectCommand.Parameters.Add("@ValidDateFrom", SqlDbType.VarChar, 10);
        //    adapter.SelectCommand.Parameters.Add("@ValidDateTo", SqlDbType.VarChar, 10);

        //    if (string.IsNullOrEmpty(MeId.ToString()))
        //        MeId=-1;
        //    adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
        //    if (string.IsNullOrEmpty(EtId.ToString()))
        //        EtId = -1;
        //    adapter.SelectCommand.Parameters["@EtId"].Value = EtId;
        //    if (string.IsNullOrEmpty(FileNo))
        //        FileNo = "%";
        //    adapter.SelectCommand.Parameters["@FileNo"].Value = FileNo;
        //    if (string.IsNullOrEmpty(IssueDateFrom))
        //        IssueDateFrom = "1";
        //    adapter.SelectCommand.Parameters["@IssueDateFrom"].Value = IssueDateFrom;
        //    if (string.IsNullOrEmpty(IssueDateTo.ToString()))
        //        IssueDateTo = "2";
        //    adapter.SelectCommand.Parameters["@IssueDateTo"].Value = IssueDateTo;
        //    if (string.IsNullOrEmpty(ValidDateFrom))
        //        ValidDateFrom = "1";
        //    adapter.SelectCommand.Parameters["@ValidDateFrom"].Value = ValidDateFrom;
        //    if (string.IsNullOrEmpty(ValidDateTo.ToString()))
        //        ValidDateTo = "2";
        //    adapter.SelectCommand.Parameters["@ValidDateTo"].Value = ValidDateTo;             

        //    adapter.Fill(dt);
        //    return (dt);


        //}

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectExpertFileByMeId(int ReId)
        //{
        //    DataTable dt = new DataManager.ExpertDataSet.ExpertExpertFileDataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter("spSelectExpertFileByMeId", this.Connection);
        //    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add("@ValidDate", SqlDbType.VarChar,10);
        //    adapter.SelectCommand.Parameters.Add("@ReId", SqlDbType.Int);


        //    adapter.SelectCommand.Parameters["@ValidDate"].Value = GetDateOfToday();
        //    adapter.SelectCommand.Parameters["@ReId"].Value = ReId;


        //    adapter.Fill(dt);
        //    return (dt);
        //}

        //private string GetDateOfToday()
        //{
        //    System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
        //    String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
        //    return PersianDate;
        //}

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectMaxExpertFileByMeId(int MeId)
        //{
        //    DataTable dt = new DataManager.ExpertDataSet.ExpertExpertFileDataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter("spSelectMaxExpertFile", this.Connection);
        //    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);

        //    adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

        //    adapter.Fill(dt);
        //    return (dt);
        //}

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectExpertFileMembers()
        //{
        //    DataTable dt = new DataManager.ExpertDataSet.ExpertExpertFileDataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter("spSelectExpertFileMembers", this.Connection);
        //    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

        //    adapter.Fill(dt);
        //    return (dt);
        //}

        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectExpertFileExpertMembers()
        //{
        //    DataTable dt = new DataManager.ExpertDataSet.ExpertExpertFileDataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter("spSelectExpertFileExpertMembers", this.Connection);
        //    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

        //    adapter.Fill(dt);
        //    return (dt);
        //}
    }
}
