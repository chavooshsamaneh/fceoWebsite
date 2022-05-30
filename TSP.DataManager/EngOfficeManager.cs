using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class EngOfficeManager : BaseObject
    {
        public static int MFType = 5;

        public EngOfficeManager()
            : base()
        {

        }      

        #region Permissions Methods
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EngOffice);
        }

        public static Permission GetUserPermissionForSeach(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EngOfficeSearch);
        }

        public static Permission GetUserPermissionForPrintEngOfficeSearch(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PrintEngOfficeSearch);
        }

        public static Permission GetUserPermissionForExportExcelEngOfficeSeach(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ExportExcelEngOfficeSearch);
        }
        #endregion

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblEngOffice";
            tableMapping.ColumnMappings.Add("EngOfId", "EngOfId");
            tableMapping.ColumnMappings.Add("EOfTId", "EOfTId");
            tableMapping.ColumnMappings.Add("EngOffName", "EngOffName");
            tableMapping.ColumnMappings.Add("ParticipateLetterNo", "ParticipateLetterNo");
            tableMapping.ColumnMappings.Add("ParticipateLetterDate", "ParticipateLetterDate");
            tableMapping.ColumnMappings.Add("EngOffNo", "EngOffNo");
            tableMapping.ColumnMappings.Add("EngOffLoc", "EngOffLoc");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsLock", "IsLock");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("TellNo", "TellNo");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("FaxNo", "FaxNo");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("ManagerOfmId", "ManagerOfmId");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "spSelectEngOffice";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@EngOfId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsRequest", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@FollowCode", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@EndDateFrom", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@EndDateTo", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ManagerName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@Managerfamily", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@EngOffName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@EngOffExactName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeEngOfMe", TableTypeManager.FindTtId(TableType.EngOfficeMember));
            this.Adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.EngOfficeConfirming);
            this.Adapter.SelectCommand.Parameters.Add("@TaskCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ReqType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CreateDateFrom", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@CreateDateTo", System.Data.SqlDbType.NVarChar);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "spDeleteEngOffice";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EngOfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EngOfId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "spInsertEngOffice";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TellNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TellNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FaxNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FaxNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ManagerOfmId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ManagerOfmId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EOfTId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EOfTId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParticipateLetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ParticipateLetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParticipateLetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ParticipateLetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EngOffNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EngOffNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EngOffLoc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EngOffLoc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EngOffName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EngOffName", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "spUpdateEngOffice";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TellNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TellNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FaxNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FaxNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EngOffName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EngOffName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ManagerOfmId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ManagerOfmId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EOfTId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EOfTId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParticipateLetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ParticipateLetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParticipateLetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ParticipateLetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EngOffNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EngOffNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EngOffLoc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EngOffLoc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EngOfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "EngOfId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EngOfId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EngOfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.OfficeDataSet.tblEngOfficeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int EngOfId)
        {
            this.Adapter.SelectCommand.Parameters["@EngOfId"].Value = EngOfId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTaskCode(int TaskCode)
        {
            this.Adapter.SelectCommand.Parameters["@TaskCode"].Value = TaskCode;
            Fill();
            return this.DataTable;
        }

        #region SelectEngOfficeForRequest
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForRequest(String FollowCode, String EndDateFrom, String EndDateTo, int MeId
            , string ManagerName, string Managerfamily, string EngOffName, int EngOfId, int ReqType, string CreateDateFrom, string CreateDateTo)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TableTypeEngOfMe"].Value = TableTypeManager.FindTtId(TableType.EngOfficeMember);
            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)WorkFlows.EngOfficeConfirming;
            this.Adapter.SelectCommand.Parameters["@EngOfId"].Value = EngOfId;
            this.Adapter.SelectCommand.Parameters["@IsRequest"].Value = 1;
            this.Adapter.SelectCommand.Parameters["@FollowCode"].Value = FollowCode;
            this.Adapter.SelectCommand.Parameters["@EndDateFrom"].Value = EndDateFrom;
            this.Adapter.SelectCommand.Parameters["@EndDateTo"].Value = EndDateTo;
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@ManagerName"].Value = ManagerName;
            this.Adapter.SelectCommand.Parameters["@Managerfamily"].Value = Managerfamily;
            this.Adapter.SelectCommand.Parameters["@EngOffName"].Value = EngOffName;
            this.Adapter.SelectCommand.Parameters["@ReqType"].Value = ReqType;
            this.Adapter.SelectCommand.Parameters["@CreateDateFrom"].Value = CreateDateFrom;
            this.Adapter.SelectCommand.Parameters["@CreateDateTo"].Value = CreateDateTo;
            Fill();
            return this.DataTable;
        }

        #region SelectEngOfficeForManagmentPage
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo
            , string ManagerName, string Managerfamily, string EngOffName, int EngOfId, int ReqType, string CreateDateFrom, string CreateDateTo, int TaskId, int TaskCode, string WFDateFrom, string WFDateTo,
            string WFDoerName)
        {
            if (FollowCode == "%" && EndDateFrom == "1" && EndDateTo == "2" 
           && ManagerName == "%" && Managerfamily == "%" && EngOffName == "%" && EngOfId == -1 && ReqType == -1 && CreateDateFrom == "1" && CreateDateTo == "2" && TaskId == -1 && TaskCode == -1 && WFDateFrom == "1" && WFDateTo == "2" && WFDoerName=="%")
                return new System.Data.DataTable();
            //ResetAllParameters();
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectEngOfficeForManagmentPage", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@EngOfId", EngOfId);
            ad.SelectCommand.Parameters.AddWithValue("@IsRequest", 1);
            ad.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);
            ad.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@ManagerName", ManagerName);
            ad.SelectCommand.Parameters.AddWithValue("@Managerfamily", Managerfamily);
            ad.SelectCommand.Parameters.AddWithValue("@EngOffName", EngOffName);
            ad.SelectCommand.Parameters.AddWithValue("@ReqType", ReqType);
            ad.SelectCommand.Parameters.AddWithValue("@CreateDateFrom", CreateDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@CreateDateTo", CreateDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@TableTypeEngOfMe", TableTypeManager.FindTtId(TableType.EngOfficeMember));
            ad.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.EngOfficeConfirming);
            ad.SelectCommand.Parameters.AddWithValue("@EngOffExactName", "%");
            ad.SelectCommand.Parameters.AddWithValue("@TaskCode", TaskCode);
            ad.SelectCommand.Parameters.AddWithValue("@WFDateFrom", WFDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@WFDateTo", WFDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);
            ad.SelectCommand.Parameters.AddWithValue("@WFDoerName", WFDoerName);

            ad.Fill(dt);
            return dt;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo
            , string ManagerName, string Managerfamily, string EngOffName, int EngOfId, int ReqType, string CreateDateFrom, string CreateDateTo, int TaskId)
        {
            return SelectEngOfficeForManagmentPage(FollowCode, EndDateFrom, EndDateTo, ManagerName, Managerfamily, EngOffName, EngOfId, ReqType, CreateDateFrom, CreateDateTo,TaskId,-1,"1","2","");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForManagmentPage(int TaskCode)
        {
            return SelectEngOfficeForManagmentPage("%", "1", "2", "%", "%", "%", -1, -1, "1", "2", -1, TaskCode, "1", "2","");
        }
        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForRequest(String FollowCode, String EndDateFrom, String EndDateTo, int MeId
            , string ManagerName, string Managerfamily, string EngOffName, int EngOfId)
        {
            return SelectEngOfficeForRequest(FollowCode, EndDateFrom, EndDateTo, MeId, ManagerName, Managerfamily, EngOffName, EngOfId, -1, "1", "2");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForRequest(String FollowCode, String EndDateFrom, String EndDateTo)
        {
            return SelectEngOfficeForRequest(FollowCode, EndDateFrom, EndDateTo, -1, "%", "%", "%", -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForRequest(String FollowCode, String EndDateFrom, String EndDateTo, int MeId
            , string ManagerName, string Managerfamily, string EngOffName)
        {

            return SelectEngOfficeForRequest(FollowCode, EndDateFrom, EndDateTo, MeId, ManagerName, Managerfamily, EngOffName, -1);

        }

        #endregion

        public void SelectEngOfficeByName(String EngOffName)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EngOffExactName"].Value = EngOffName;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectTopEngOffice(int EOfCode)
        {
            // DataTable dt = new DataManager.NezamFarsDataSet.tblOfficeMemberDataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectTopEngOffice", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@EOfCode", System.Data.SqlDbType.Int);
            if (string.IsNullOrEmpty(EOfCode.ToString()))
                EOfCode = -1;
            ad.SelectCommand.Parameters["@EOfCode"].Value = EOfCode;
            //if (string.IsNullOrEmpty(EOfId.ToString()))
            //    EOfId = -1;
            //ad.SelectCommand.Parameters["@EOfId"].Value = EOfId;
            ad.Fill(DataTable);


        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeManagerByOfId(int OfId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeManager", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeMeJobHistoryByOfId(int OfId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeMemberJobHistory", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@OfId"].Value = OfId;

            adapter.Fill(dt);
            return (dt);



        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeByMeId(int MeId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeByMeId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int);
            adapter.SelectCommand.Parameters["@PersonId"].Value = MeId;
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", 0);
            adapter.Fill(DataTable);
            return this.DataTable;

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForSearch(int EngOfId, int MeId, int EOfTId, string FromDate, string ToDate)
        {
            return SelectEngOfficeForSearch(EngOfId, MeId, EOfTId, FromDate, ToDate, "%", "%", "%", "(1)", -1, "1", "2", "1", "2", "1", "2");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForSearch(int EngOfId, int MeId, int EOfTId, string FromDate,
            string ToDate, string TellNo, string MeName, string EngOffName, string IsConfirm)
        {
            return SelectEngOfficeForSearch(EngOfId, MeId, EOfTId, FromDate, ToDate, TellNo, MeName, EngOffName, IsConfirm, -1, "1", "2", "1", "2", "1", "2");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectEngOfficeForSearch(int EngOfId, int MeId, int EOfTId, string FromDate,
            string ToDate, string TellNo, string MeName, string EngOffName, string IsConfirm, int ReqType, string EndDateFrom, string EndDateTo,
            string FirstRegDateFrom, string FirstRegDateTo, string LastRegDateFrom, string LastRegDateTo)
        {
            DataTable dt = new DataTable();
            if (EngOfId == -2)
                return dt;

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeForSearch", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (EngOfId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@EngOfId", EngOfId);
            if (MeId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);

            if (!string.IsNullOrEmpty(IsConfirm))
                adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", IsConfirm);

            if (FromDate != null && String.IsNullOrEmpty(FromDate.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            else
                FromDate = "1";
            if (ToDate != null && String.IsNullOrEmpty(ToDate.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            else
                ToDate = "2";
            if (EOfTId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@EOfTId", EOfTId);

            if (TellNo != "%")
                adapter.SelectCommand.Parameters.AddWithValue("@TellNo", TellNo);
            if (MeName != "%")
                adapter.SelectCommand.Parameters.AddWithValue("@MeName", MeName);
            if (EngOffName != "%")
                adapter.SelectCommand.Parameters.AddWithValue("@EngOffName", EngOffName);

            if (ReqType != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@ReqType", ReqType);

            if (EndDateFrom != null && String.IsNullOrEmpty(EndDateFrom.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            else
                EndDateFrom = "1";

            if (EndDateTo != null && String.IsNullOrEmpty(EndDateTo.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            else
                EndDateTo = "2";

            if (FirstRegDateFrom != null && String.IsNullOrEmpty(FirstRegDateFrom.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@FirstRegDateFrom", FirstRegDateFrom);
            else
                FirstRegDateFrom = "1";

            if (FirstRegDateTo != null && String.IsNullOrEmpty(FirstRegDateTo.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@FirstRegDateTo", FirstRegDateTo);
            else
                FirstRegDateTo = "2";

            if (LastRegDateFrom != null && String.IsNullOrEmpty(LastRegDateFrom.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@LastRegDateFrom", LastRegDateFrom);
            else
                LastRegDateFrom = "1";

            if (LastRegDateTo != null && String.IsNullOrEmpty(LastRegDateTo.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@LastRegDateTo", LastRegDateTo);
            else
                LastRegDateTo = "2";

            adapter.SelectCommand.Parameters.AddWithValue("@EngOffFileTableCode", TableTypeManager.FindTtId(TableType.EngOffFile));

            adapter.Fill(dt);
            return (dt);
        }

        /// <summary>
        /// پایه دفتر را بر اساس رشته و صلاحیت مورد نظر بر می گرداند
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="OfKind"></param>
        /// <param name="ResId"></param>
        /// <param name="MjId"></param>
        /// <returns></returns>
        public int GetMaxGradeOfEngOffice(int OfId, int ResId, int MjId)
        {
            int GrdId = -1;
            DataTable dtOfMe = new DataTable();
            int MaxObsId = 0;
            int MaxDsgnId = 0;

            SqlDataAdapter ad = new SqlDataAdapter("spReportEngOfficeMembers", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@EngOfId", OfId);
            ad.Fill(dtOfMe);


            dtOfMe.DefaultView.RowFilter = "FMjId=" + MjId;

            for (int i = 0; i < dtOfMe.DefaultView.Count; i++)
            {

                if (MaxObsId == 0 || (dtOfMe.DefaultView[i]["ObsId"] != DBNull.Value && dtOfMe.DefaultView[i]["ObsId"] != null && Convert.ToInt32(dtOfMe.DefaultView[i]["ObsId"]) < MaxObsId))
                {
                    MaxObsId = Convert.ToInt32(dtOfMe.DefaultView[i]["ObsId"]);
                }
                if (MaxDsgnId == 0 || (dtOfMe.DefaultView[i]["DesId"] != DBNull.Value && dtOfMe.DefaultView[i]["DesId"] != null && Convert.ToInt32(dtOfMe.DefaultView[i]["DesId"]) < MaxDsgnId))
                {
                    MaxDsgnId = Convert.ToInt32(dtOfMe.DefaultView[i]["DesId"]);

                }
            }


            if (ResId == (int)DocumentResponsibilityType.Design)
                GrdId = MaxDsgnId;
            else if (ResId == (int)DocumentResponsibilityType.Observation)
                GrdId = MaxObsId;
            return GrdId;
        }

        public DataTable SelectEngOfficeInfoForTsWebservice(int EngOfId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectEngOfficeInfoForTsWebservice", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@EngOfId", SqlDbType.Int, 4).Value = EngOfId;
            adapter.Fill(dt);
            return (dt);
        }
        /// <summary>
        /// برای استفاده در جستجوی اعضای دفتر اضافه شد اما استفاده نشد
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="PersonId"></param>
        /// <returns></returns>
        public DataTable SelectEngOfficeMemberForSearch(int OfId,int PersonId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectEngOfficeMemberForSearch", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@PersonId", PersonId);
            adapter.Fill(dt);
            return (dt);
        }        

    }
}