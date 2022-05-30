using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class TeacherCertificateManager : BaseObject
    {
        #region WF Methods

        #endregion
        public int DoNextTaskOfConfirming(int TableId, int CurrentUserId)
        {
            int per = 0;
            DataTable dt = this.SelectLastVersion(TableId);
            if (dt.Rows.Count <= 0)
                return (int)ErrorRequest.LoseRequestInfo;
            this.FindByCode((int)dt.Rows[0]["TcId"]);
            if (this.Count <= 0)
                return (int)ErrorRequest.LoseRequestInfo;
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 1;
            this[0]["UserId"] = CurrentUserId; ;
            this[0].EndEdit();
            this.Save();
            return per;
        }

        public int DoNextTaskOfRejecting(int TableId, int CurrentUserId)
        {
            int per = 0;
            DataTable dt = this.SelectLastVersion(TableId);
            if (dt.Rows.Count <= 0)
                return (int)ErrorRequest.LoseRequestInfo;
            this.FindByCode((int)dt.Rows[0]["TcId"]);
            if (this.Count <= 0)
                return (int)ErrorRequest.LoseRequestInfo;
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 0;
            this[0]["UserId"] = CurrentUserId; ;
            this[0].EndEdit();
            this.Save();
            return per;
        }
        public enum RequestType
        {
            New = 0, Revival = 1, Change = 2
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TeacherCertificate);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblTeacherCertificate";
            tableMapping.ColumnMappings.Add("TcId", "TcId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("TeId", "TeId");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("SerialNo", "SerialNo");
            tableMapping.ColumnMappings.Add("StartDate", "StartDate");
            tableMapping.ColumnMappings.Add("EndDate", "EndDate");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTeacherCertificate";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TcId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTeacherCertificate";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TcId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTeacherCertificate";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTeacherCertificate";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TcId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TcId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblTeacherCertificateDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int TcId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTeacherCertificate";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TcId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.SelectCommand.Parameters["@TcId"].Value = TcId;
            Fill();
        }

        public void FindByTeacherId(int TeId, int Type)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTeacherCertificate";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int, 4, "TeId").Value = TeId;
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int, 4, "Type").Value = Type;
            this.Adapter.SelectCommand.Parameters.Add("@TcId", SqlDbType.Int, 4, "TcId").Value = -1;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastVersion(int TeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectTeacherCertificateLastVersion";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int, 4, "TeId").Value = TeId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTeachersCertificate(int TcId, int TeId, int Type)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacherCertificate", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TcId", SqlDbType.Int, 4, "TcId").Value = TcId;
            adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int, 4, "TeId").Value = TeId;
            adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int, 4, "Type").Value = Type;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTeachersByTeacher(int TeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacherCertificate", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TcId", SqlDbType.Int, 4, "TcId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@TeId", SqlDbType.Int, 4, "TeId").Value = TeId;
            adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int, 4, "Type").Value = -1;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTeachersByFileNo(string FileNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTeacherCertificateByFilNo", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@FileNo", FileNo);

            adapter.Fill(dt);
            return (dt);
        }
    }
}
