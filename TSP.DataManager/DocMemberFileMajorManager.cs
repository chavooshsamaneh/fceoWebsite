using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class DocMemberFileMajorManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.DocMemberFileMajor);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "DocMemberFileMajor";
            tableMapping.ColumnMappings.Add("MFMjId", "MFMjId");
            tableMapping.ColumnMappings.Add("MFId", "MFId");
            tableMapping.ColumnMappings.Add("MlId", "MlId");
            tableMapping.ColumnMappings.Add("FMjId", "FMjId");
            tableMapping.ColumnMappings.Add("IsMaster", "IsMaster");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsPrinted", "IsPrinted");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("MailDate", "MailDate");
            tableMapping.ColumnMappings.Add("MailNo", "MailNo");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "spSelectDocMemberFileMajor";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFMjId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MFMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MlId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MlId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileMajor));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "spDeleteDocMemberFileMajor";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MFMjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFMjId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "spInsertDocMemberFileMajor";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MlId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MlId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FMjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "FMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMaster", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMaster", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsPrinted", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsPrinted", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "spUpdateDocMemberFileMajor";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MlId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MlId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FMjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "FMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMaster", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMaster", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsPrinted", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsPrinted", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MFMjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFMjId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFMjId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MFMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.DocumentDataSet.DocMemberFileMajorDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        //********************************************************************************************
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int MFMjId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFMjId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MFMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MFId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters["@MFId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@MFMjId"].Value = MFMjId;

            Fill();
        }

        public void FindByMlId(int MlId, int InActive)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters.Clear();
            if (Adapter.SelectCommand.Transaction == null)
                Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MlId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MlId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters["@MlId"].Value = MlId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;

            Fill();
        }

        public void FindByMFId(int MfId, int MeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileMajor));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            if (MfId == -1)
                this.Adapter.SelectCommand.Parameters.AddWithValue("@JustConfirmedReq", 2);
            this.Adapter.SelectCommand.Parameters["@MfId"].Value = MfId;
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDocMemberFileMajorForTSWorkRequest(int MFId, int MeId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "SelectDocMemberFileMajorForTSWorkRequest";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MFId", SqlDbType.Int, 4, "MFId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@JustConfirmedReq", SqlDbType.Int, 4, "JustConfirmedReq").Value = 2;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileMajor));

            adapter.Fill(dt);
            return (dt);
        }
        
        #region SelectMemberFileById
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberFileById(int MFId, int MeId, int InActive, int IsMaster)
        {
            DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByCode(MFId, 0);
            int JustConfirmedReq = 2;
            if (DocMemberFileManager.Count == 1)
            {
                if (Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 2)
                    JustConfirmedReq = -1;
                else
                    JustConfirmedReq = 2;
            }
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileMajor";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MFMjId", SqlDbType.Int, 4, "MFMjId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MFId", SqlDbType.Int, 4, "MFId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.Add("@IsMaster", SqlDbType.Int, 4, "IsMaster").Value = IsMaster;
            adapter.SelectCommand.Parameters.Add("@JustConfirmedReq", SqlDbType.Int, 4, "JustConfirmedReq").Value = JustConfirmedReq;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileMajor));

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberFileById(int MFId, int MeId, int InActive)
        {
            return (SelectMemberFileById(MFId, MeId, InActive, -1));
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberFile(int MFId, int InActive)
        {
            return (SelectMemberFileById(MFId, -1, InActive, -1));
        }
        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMFFileMajor(int MFId, int InActive)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileMajorFMj";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MFId", SqlDbType.Int, 4, "MFId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileMajor));

            adapter.Fill(dt);
            return (dt);
        }

        /// <summary>
        /// رشته موضوع پروانه بر اساس آخرین درخواست تایید شده برای یک کد عضویت بدست می آورد
        /// </summary>
        /// <param name="MeId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberMasterMajor(int MeId)
        {
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileMasterMajorByMeId";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileMajor));

            adapter.Fill(DataTable);
            return this.DataTable;
        }

        /// <summary>
        /// مقداردهی رشته موضوع پروانه در جدول رشته های پروانه بر اساس مقدار ثبت شده در یک درخواست
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="MasterMfMjId"></param>
        /// <returns></returns>
        public bool UpdateMasterInDocMemberFileMajor(int MeId, int MasterMfMjId)
        {
            bool result = false;
            SqlCommand Command = new SqlCommand("spUpdateMasterInDocMemberFileMajor", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@MeId", MeId);
            Command.Parameters.AddWithValue("@MasterMFMjId", MasterMfMjId);
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception) { result = false; }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
            {
                Command.ExecuteNonQuery();
                result = true;
            }
            return result;
        }

        public DataTable SelectDocMemberFileMajorByMFMjId(int MfMjId)
        {
            SqlDataAdapter adapter = this.Adapter;
            DataTable dt = new DataTable();
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "SelectDocMemberFileMajorByMFMjId";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MfMjId", MfMjId);

            adapter.Fill(dt);
            return dt;
        }
        
    }
}
