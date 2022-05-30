using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace TSP.DataManager
{
    public class DocMemberFileDetailManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.DocMemberFileDetail);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Doc.MemberFileDetail";
            tableMapping.ColumnMappings.Add("MfdId", "MfdId");
            tableMapping.ColumnMappings.Add("MfId", "MfId");
            tableMapping.ColumnMappings.Add("GMRId", "GMRId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ActTypeId", "ActTypeId");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("ResRangeId", "ResRangeId");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocMemberFileDetail";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@DateOfToday", Utility.GetDateOfToday());
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfdId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MfdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GMRId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "GMRId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileDetail));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocMemberFileDetail";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MfdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MfdId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocMemberFileDetail";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ActTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResRangeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResRangeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GMRId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GMRId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocMemberFileDetail";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ActTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResRangeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ResRangeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GMRId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GMRId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MfdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MfdId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfdId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MfdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        public void FindByCode(int MfdId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MfdId"].Value = MfdId;
            Fill();
        }
        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.DocumentDataSet.DocMemberFileDetailDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }
                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectById(int MFId, int MeId, int InActive)
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
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetail";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@DateOfToday", Utility.GetDateOfToday());
            adapter.SelectCommand.Parameters.Add("@MfdId", SqlDbType.Int, 4, "MfdId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@GMRId", SqlDbType.Int, 4, "GMRId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileDetail));
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4, "IsConfirm").Value = -1;
            adapter.SelectCommand.Parameters.Add("@JustConfirmedReq", SqlDbType.Int, 4, "JustConfirmedReq").Value = JustConfirmedReq;
            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable spSelectDocMemberFileDetailForManagementPage(int MFId, int MeId, int InActive)
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
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailForManagementPage";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@DateOfToday", Utility.GetDateOfToday());
            adapter.SelectCommand.Parameters.Add("@MfdId", SqlDbType.Int, 4, "MfdId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@GMRId", SqlDbType.Int, 4, "GMRId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileDetail));
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4, "IsConfirm").Value = -1;
            adapter.SelectCommand.Parameters.Add("@JustConfirmedReq", SqlDbType.Int, 4, "JustConfirmedReq").Value = JustConfirmedReq;
            adapter.Fill(dt);
            return (dt);
        }
        
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByMemberFile(int MFId, int InActive)
        {
            return (SelectById(MFId, -1, InActive));
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMeFileDetailMaxGrad(int MFId, int MeId, int InActive)
        {
            DataTable dt = new DataTable();
            //DataTable.Clear();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailMaxGrade";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfdId", SqlDbType.Int, 4, "MfdId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@GMRId", SqlDbType.Int, 4, "GMRId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.DocMemberFileDetail);
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMeFileDetailMaxGrad(int MFId, int MeId, int InActive, int ResId)
        {
            TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
            DocMemberFileAcceptTypeManager.FindByActTypeCode((int)DocumentMemberFileAcceptType.GradeJumping);
            int ActTypId = -1;
            if (DocMemberFileAcceptTypeManager.Count == 1)
            {
                ActTypId = Convert.ToInt32(DocMemberFileAcceptTypeManager[0]["ActTypeId"]);
            }
            DataTable dt = new DataTable();
            //DataTable.Clear();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailMaxGrade";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfdId", SqlDbType.Int, 4, "MfdId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@GMRId", SqlDbType.Int, 4, "GMRId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.DocMemberFileDetail);
            adapter.SelectCommand.Parameters.AddWithValue("@ResId", ResId);
            adapter.SelectCommand.Parameters.AddWithValue("@ActTypeId", ActTypId);
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMeFileDetailMaxGrad(int MFId, int MeId, int InActive, int ResId, int MjId)
        {
            TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
            DocMemberFileAcceptTypeManager.FindByActTypeCode((int)DocumentMemberFileAcceptType.GradeJumping);
            int ActTypId = -1;
            if (DocMemberFileAcceptTypeManager.Count == 1)
            {
                ActTypId = Convert.ToInt32(DocMemberFileAcceptTypeManager[0]["ActTypeId"]);
            }
            DataTable dt = new DataTable();
            //DataTable.Clear();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailMaxGrade";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfdId", SqlDbType.Int, 4, "MfdId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@GMRId", SqlDbType.Int, 4, "GMRId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.DocMemberFileDetail);
            adapter.SelectCommand.Parameters.AddWithValue("@ResId", ResId);
            adapter.SelectCommand.Parameters.AddWithValue("@ActTypeId", ActTypId);
            adapter.SelectCommand.Parameters.AddWithValue("@MjId", MjId);
            adapter.Fill(dt);
            return (dt);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMeFileMaxGradForAllResponsiblity( int MeId, int ResId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileMaxGrade";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.DocMemberFileDetail);
            adapter.SelectCommand.Parameters.AddWithValue("@ResId", ResId);
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchMeFileDetail(int MFId, int MeId, int GMRId, int InActive)
        {
            // DataTable dt = new DataTable();
            DataTable.Clear();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetail";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@DateOfToday", Utility.GetDateOfToday());
            adapter.SelectCommand.Parameters.Add("@MfdId", SqlDbType.Int, 4, "MfdId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@GMRId", SqlDbType.Int, 4, "GMRId").Value = GMRId;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.DocMemberFileDetail));
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4, "IsConfirm").Value = -1;

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByResponsibility(int MFId, int MeId, int ResId,int GrdId, int MjParentId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailByResponsibility";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.Add("@GrdId", SqlDbType.Int, 4, "GrdId").Value = GrdId;
            adapter.SelectCommand.Parameters.Add("@MjParentId", SqlDbType.Int, 4, "MjParentId").Value = MjParentId; 
            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByResponsibility(int MFId, int MeId, int ResId, int GrdId)
        {
            return FindByResponsibility(MFId, MeId, ResId, GrdId, -1);
        }
            

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByResponsibility(int MFId, int MeId, int ResId)
        {
            return FindByResponsibility(MFId, MeId, ResId, -1);
        }

        public DataTable SelectDocMemberFileAcceptTypeForReport(int MFId, int MeId, int ResId, int TableType, int MjId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileAcceptTypeForReport";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByResponsibilityAndMainMajor(int MFId, int MeId, int ResId)
        {
            int MjId = -1;
            //System.Collections.ArrayList arlRes = new ArrayList();
            DocMemberFileMajorManager DocMemberFileMajorManager = new DocMemberFileMajorManager();
            DataTable dtDocMajor = new DataTable();
            dtDocMajor = DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            DataTable dt = new DataTable();
            if (dtDocMajor.Rows.Count == 1)
            {
                MjId = int.Parse(dtDocMajor.Rows[0]["FMjId"].ToString());
                this.Adapter.SelectCommand.Parameters.Clear();
                SqlDataAdapter adapter = this.Adapter;
                adapter.SelectCommand.Transaction = this.Transaction;
                adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailByResponsibility";
                adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
                adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
                adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
                adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;

                adapter.Fill(dt);
            }
            return (dt);
        }

        /// <summary>
        /// Find By Master Major
        /// ArrayList[0] :GradeId, ArrayList[1] :GradeName
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="ResId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ArrayList FindActiveResByResponsibility(int MeId, int ResId)
        {
            int MjId = -1;
            System.Collections.ArrayList arlRes = new ArrayList();
            DocMemberFileMajorManager DocMemberFileMajorManager = new DocMemberFileMajorManager();
            DataTable dtDocMajor = new DataTable();
            dtDocMajor = DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            DocMemberFileManager DocMemberFileManager = new DocMemberFileManager();
            DocMemberFileManager.SelectLastVersion(MeId, (int)DocumentTypesOfMember.DocMemberFile, 1);
            int MfId = -2;
            if (DocMemberFileManager.Count != 0)
            {
                MfId = Convert.ToInt32(DocMemberFileManager[0]["MfId"]);
            }
            if (dtDocMajor.Rows.Count == 1)
            {
                MjId = int.Parse(dtDocMajor.Rows[0]["FMjId"].ToString());
                DataTable dt = new DataTable();
                this.Adapter.SelectCommand.Parameters.Clear();
                SqlDataAdapter adapter = this.Adapter;
                adapter.SelectCommand.Transaction = this.Transaction;
                adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailByResponsibility";
                adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MfId;
                adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
                adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
                adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    arlRes.Add(dt.Rows[0]["GrdId"].ToString());
                    arlRes.Add(dt.Rows[0]["GrdName"].ToString());
                }
                //else
                //{

                //    adapter.SelectCommand.Parameters["@ResId"].Value = -1;
                //    adapter.Fill(dt);
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (Convert.ToInt32(dt.Rows[0]["MjParentId"]) == (int)MainMajors.Mapping && ResId == (int)TSP.DataManager.DocumentResponsibilityType.Observation)// && ResId != (int)TSP.DataManager.DocumentResponsibilityType.Mapping)
                //        {
                //            adapter.SelectCommand.Parameters["@ResId"].Value = (int)TSP.DataManager.DocumentResponsibilityType.Mapping;
                //            adapter.Fill(dt);
                //            if (dt.Rows.Count > 0)
                //            {
                //                arlRes.Add(dt.Rows[0]["GrdId"].ToString());
                //                arlRes.Add(dt.Rows[0]["GrdName"].ToString());
                //            }
                //        }
                //        else
                //        {
                //            adapter.SelectCommand.Parameters["@ResId"].Value = ResId;
                //            adapter.SelectCommand.Parameters["@MjId"].Value = -1;
                //            adapter.Fill(dt);
                //            if (dt.Rows.Count > 0)
                //            {
                //                arlRes.Add(dt.Rows[0]["GrdId"].ToString());
                //                arlRes.Add(dt.Rows[0]["GrdName"].ToString());
                //            }
                //        }
                //    }
                //    else
                //    {
                //        adapter.SelectCommand.Parameters["@ResId"].Value = ResId;
                //        adapter.SelectCommand.Parameters["@MjId"].Value = -1;
                //        adapter.Fill(dt);
                //        if (dt.Rows.Count > 0)
                //        {
                //            arlRes.Add(dt.Rows[0]["GrdId"].ToString());
                //            arlRes.Add(dt.Rows[0]["GrdName"].ToString());
                //        }
                //    }
                //}
            }
            return (arlRes);
        }

        /// <summary>
        /// Find By Master Major
        /// ArrayList[0] :GradeId, ArrayList[1] :GradeName
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="ResId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ArrayList FindActiveResByResponsibility(int MeId, int ResId, string Date)
        {
            int MjId = -1;
            System.Collections.ArrayList arlRes = new ArrayList();
            DocMemberFileMajorManager DocMemberFileMajorManager = new DocMemberFileMajorManager();
            DataTable dtDocMajor = new DataTable();
            dtDocMajor = DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            if (dtDocMajor.Rows.Count == 1)
            {
                MjId = int.Parse(dtDocMajor.Rows[0]["MjId"].ToString());
                DataTable dt = new DataTable();
                this.Adapter.SelectCommand.Parameters.Clear();
                SqlDataAdapter adapter = this.Adapter;
                adapter.SelectCommand.Transaction = this.Transaction;
                adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailByResponsibility";
                adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = -1;
                adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
                adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
                adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);
                adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;


                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    arlRes.Add(dt.Rows[dt.Rows.Count - 1]["GrdId"].ToString());
                    arlRes.Add(dt.Rows[dt.Rows.Count - 1]["GrdName"].ToString());
                }
            }
            return (arlRes);
        }


        /// <summary>
        /// ArrayList[0] :GradeId, ArrayList[1] :GradeName
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="ResId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ArrayList FindActiveResByResponsibilityAndMajor(int MeId, int ResId, int MjId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailByResponsibility";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;

            adapter.Fill(dt);

            System.Collections.ArrayList arlRes = new ArrayList();
            if (dt.Rows.Count > 0)
            {
                arlRes.Add(dt.Rows[0]["GrdId"].ToString());
                arlRes.Add(dt.Rows[0]["GrdName"].ToString());
            }

            return (arlRes);
        }


        /// <summary>
        /// ArrayList[0] :GradeId, ArrayList[1] :GradeName
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="ResId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public ArrayList FindActiveResByResponsibilityAndMajor(int MeId, int ResId, string Date, int MjId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileDetailByResponsibility";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.Fill(dt);
            System.Collections.ArrayList arlRes = new ArrayList();
            if (dt.Rows.Count > 0)
            {
                arlRes.Add(dt.Rows[dt.Rows.Count - 1]["GrdId"].ToString());
                arlRes.Add(dt.Rows[dt.Rows.Count - 1]["GrdName"].ToString());
            }
            return (arlRes);
        }

        public DataTable FindDocMemberFileMaxResponsibility(int MFId, int MeId, int MjId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spReportDocMemberFileMaxResponsibility";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MFId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TableTypeDocMj", SqlDbType.Int, 4, "TableTypeDocMj").Value = TSP.DataManager.TableTypeManager.FindTtId(TableType.DocMemberFileMajor);
            adapter.SelectCommand.Parameters.Add("@TableTypeDocDetail", SqlDbType.Int, 4, "TableTypeDocDetail").Value = TSP.DataManager.TableTypeManager.FindTtId(TableType.DocMemberFileDetail);
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectReportMemberDetailFileRegDate(int MeId, int MfId, int ResId, int MjId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectReportMemberDetailFileRegDate";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4, "MfId").Value = MfId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TableTypeDocDetail", SqlDbType.Int, 4, "TableTypeDocDetail").Value = TableTypeManager.FindTtId(TableType.DocMemberFileDetail);
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectDocMemberFileDetailForSafaRayanehWebservice(int MeId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "SelectDocMemberFileDetailForWebService";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId",MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType",TableTypeManager.FindTtId(TableType.DocMemberFileDetail));
            adapter.SelectCommand.Parameters.AddWithValue("@JustConfirmedReq", 2);            
            adapter.Fill(dt);
            return (dt);
        }
        public DataTable SelectDocMemberFileDetailForEsys(int MeId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "SelectDocMemberFileDetailForEsys";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId",MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType",TableTypeManager.FindTtId(TableType.DocMemberFileDetail));
            adapter.SelectCommand.Parameters.AddWithValue("@JustConfirmedReq", 2);            
            adapter.Fill(dt);
            return (dt);
        }
        
    }
}
