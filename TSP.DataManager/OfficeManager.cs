using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class OfficeManager : BaseObject
    {
        public static int ObservationAndDesignMFType = 4;
        public static int ImplementMFType = 2;

        public OfficeManager()
            : base()
        {
        }

        #region Permissions Methods
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Office);
        }

        public static Permission GetUserPermissionForOffDoc(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeDocument);
        }

        public static Permission GetUserPermissionForSearchOffice(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeSearch);
        }

        public static Permission GetUserPermissionForPrintOfficeSearch(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PrintOfficeSearch);
        }

        public static Permission GetUserPermissionForExportExcelOfficeSeach(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ExportExcelOfficeSearch);
        }
        #endregion

        protected override void InitAdapter()
        {

            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblOffice";
            tableMapping.ColumnMappings.Add("OfId", "OfId");
            tableMapping.ColumnMappings.Add("OfName", "OfName");
            tableMapping.ColumnMappings.Add("OfNameEn", "OfNameEn");
            tableMapping.ColumnMappings.Add("PrefixCode", "PrefixCode");
            tableMapping.ColumnMappings.Add("OtId", "OtId");
            tableMapping.ColumnMappings.Add("OatId", "OatId");
            tableMapping.ColumnMappings.Add("Tel1", "Tel1");
            tableMapping.ColumnMappings.Add("Tel2", "Tel2");
            tableMapping.ColumnMappings.Add("Fax", "Fax");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("Website", "Website");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("Subject", "Subject");
            tableMapping.ColumnMappings.Add("RegDate", "RegDate");
            tableMapping.ColumnMappings.Add("RegOfNo", "RegOfNo");
            tableMapping.ColumnMappings.Add("RegPlace", "RegPlace");
            tableMapping.ColumnMappings.Add("VolumeInvest", "VolumeInvest");
            tableMapping.ColumnMappings.Add("Stock", "Stock");
            tableMapping.ColumnMappings.Add("MeNo", "MeNo");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("FileDate", "FileDate");
            tableMapping.ColumnMappings.Add("SignImage", "SignImage");
            tableMapping.ColumnMappings.Add("ArmImage", "ArmImage");
            tableMapping.ColumnMappings.Add("SignUrl", "SignUrl");
            tableMapping.ColumnMappings.Add("ArmUrl", "ArmUrl");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("MrsId", "MrsId");
            tableMapping.ColumnMappings.Add("AccId", "AccId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("MFType", "MFType");
            tableMapping.ColumnMappings.Add("IsLock", "IsLock");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ActivityType", "ActivityType");
            tableMapping.ColumnMappings.Add("ManagerOfmId", "ManagerOfmId");
            tableMapping.ColumnMappings.Add("GrdId", "GrdId");
            tableMapping.ColumnMappings.Add("DocumentStatus", "DocumentStatus");
            tableMapping.ColumnMappings.Add("MembershipRequstType", "MembershipRequstType");

            this.Adapter.TableMappings.Add(tableMapping);


            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectOffice";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@OfId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@OfName", System.Data.SqlDbType.NVarChar, 80);
            this.Adapter.SelectCommand.Parameters.Add("@PrefixCode", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@OtId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@Subject", System.Data.SqlDbType.NVarChar, 100);
            this.Adapter.SelectCommand.Parameters.Add("@FromRegDate", System.Data.SqlDbType.Char, 10);
            this.Adapter.SelectCommand.Parameters.Add("@ToRegDate", System.Data.SqlDbType.Char, 10);
            this.Adapter.SelectCommand.Parameters.Add("@RegOfNo", System.Data.SqlDbType.VarChar, 20);
            this.Adapter.SelectCommand.Parameters.Add("@RegPlace", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@FileNo", System.Data.SqlDbType.VarChar, 30);
            this.Adapter.SelectCommand.Parameters.Add("@OatId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@MrsId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCodeOffConf", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@IsRequest", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@FollowCode", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@EndDateFrom", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@EndDateTo", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeOfMe", (int)TableTypeManager.FindTtId(TableType.OfficeMember));
            this.Adapter.SelectCommand.Parameters.Add("@TaskCode", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@HiddenPendingOffice", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteOffice";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertOffice";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocumentStatus", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "DocumentStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrefixCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrefixCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OtId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OatId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel1", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Fax", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Website", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Website", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Subject", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Subject", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegOfNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegOfNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VolumeInvest", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "VolumeInvest", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Stock", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Stock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MeNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "FileDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignImage", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "SignImage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArmImage", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "ArmImage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArmUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ArmUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MrsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ManagerOfmId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ManagerOfmId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MFType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActivityType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ActivityType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MembershipRequstType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MembershipRequstType", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateOffice";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocumentStatus", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "DocumentStatus", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrefixCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrefixCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OtId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OatId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OatId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel1", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Fax", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Website", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Website", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Subject", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Subject", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegOfNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegOfNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VolumeInvest", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "VolumeInvest", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Stock", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Stock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MeNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "FileDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignImage", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "SignImage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArmImage", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "ArmImage", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArmUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ArmUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MrsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MFType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "OfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActivityType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ActivityType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ManagerOfmId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ManagerOfmId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MembershipRequstType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MembershipRequstType", System.Data.DataRowVersion.Current, false, null, "", "", ""));


            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)WorkFlows.OfficeConfirming;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCodeOffConf"].Value = (int)WorkFlows.OfficeMembershipConfirming;

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.OfficeDataSet.tblOfficeDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int OfId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            Fill();
        }

        public void FindByRegNoAndRegDate(string RegOfNo, string RegDate)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RegOfNo"].Value = RegOfNo;
            this.Adapter.SelectCommand.Parameters["@FromRegDate"].Value = RegDate;
            this.Adapter.SelectCommand.Parameters["@ToRegDate"].Value = RegDate;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByTaskCode(int TaskCode)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TaskCode"].Value = TaskCode;

            Fill();
            return this.DataTable;
        }

        public void FindOfficeConfirmed(int OfId)
        {
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            this.Adapter.SelectCommand.Parameters["@MrsId"].Value = 1;

            Fill();
        }

        public static void UpdateMeNo(int ofid)
        {
            SqlConnection scon = new SqlConnection(DBManager.CnnStr);
            SqlCommand scom = new SqlCommand("CreateOfficeMeNo", scon);
            scom.CommandType = CommandType.StoredProcedure;
            scom.Parameters.Add("@OfId", SqlDbType.Int);
            scom.Parameters[0].Value = ofid;
            if (scon.State != ConnectionState.Open)
                scon.Open();
            scom.ExecuteNonQuery();
            scon.Close();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchOffice(string OfName, short PrefixCode, int OtId, string Subject, string FromRegDate, string ToRegDate, string RegOfNo, string RegPlace, string FileNo, int OatId)
        {

            //if (string.IsNullOrEmpty(OfId.ToString()))
            //    OfId = -1;
            //this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;

            if (string.IsNullOrEmpty(OfName))
                OfName = "%";
            this.Adapter.SelectCommand.Parameters["@OfName"].Value = OfName;

            if (string.IsNullOrEmpty(PrefixCode.ToString()))
                PrefixCode = -1;
            this.Adapter.SelectCommand.Parameters["@PrefixCode"].Value = PrefixCode;

            if (string.IsNullOrEmpty(OtId.ToString()))
                OtId = -1;
            this.Adapter.SelectCommand.Parameters["@OtId"].Value = OtId;

            if (string.IsNullOrEmpty(Subject))
                Subject = "%";
            this.Adapter.SelectCommand.Parameters["@Subject"].Value = Subject;

            if (string.IsNullOrEmpty(FromRegDate))
                FromRegDate = "1";
            this.Adapter.SelectCommand.Parameters["@FromRegDate"].Value = FromRegDate;

            if (string.IsNullOrEmpty(ToRegDate))
                ToRegDate = "2";
            this.Adapter.SelectCommand.Parameters["@ToRegDate"].Value = ToRegDate;

            if (string.IsNullOrEmpty(RegOfNo))
                RegOfNo = "%";
            this.Adapter.SelectCommand.Parameters["@RegOfNo"].Value = RegOfNo;

            if (string.IsNullOrEmpty(RegPlace))
                RegPlace = "%";
            this.Adapter.SelectCommand.Parameters["@RegPlace"].Value = RegPlace;

            if (string.IsNullOrEmpty(FileNo))
                FileNo = "%";
            this.Adapter.SelectCommand.Parameters["@FileNo"].Value = FileNo;

            if (string.IsNullOrEmpty(OatId.ToString()))
                OatId = -1;
            this.Adapter.SelectCommand.Parameters["@OatId"].Value = OatId;
            Fill();
            return DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeManager(string OfName, string LastName, int OfId, int OatId, string RegDateFrom, string RegDateTo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeManagerMe_Ka_Ot", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfName", SqlDbType.NVarChar, 80);
            adapter.SelectCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@OatId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@RegDateFrom", SqlDbType.VarChar, 10);
            adapter.SelectCommand.Parameters.Add("@RegDateTo", SqlDbType.VarChar, 10);

            if (string.IsNullOrEmpty(OfName))
                OfName = "%";
            adapter.SelectCommand.Parameters["@OfName"].Value = OfName;

            if (string.IsNullOrEmpty(LastName))
                LastName = "%";
            adapter.SelectCommand.Parameters["@LastName"].Value = LastName;

            if (string.IsNullOrEmpty(OfId.ToString()))
                OfId = -1;
            adapter.SelectCommand.Parameters["@OfId"].Value = OfId;

            if (string.IsNullOrEmpty(OatId.ToString()))
                OatId = -1;
            adapter.SelectCommand.Parameters["@OatId"].Value = OatId;
            if (string.IsNullOrEmpty(RegDateFrom))
                RegDateFrom = "1";
            adapter.SelectCommand.Parameters["@RegDateFrom"].Value = RegDateFrom;
            if (string.IsNullOrEmpty(RegDateTo))
                RegDateTo = "2";
            adapter.SelectCommand.Parameters["@RegDateTo"].Value = RegDateTo;

            adapter.Fill(dt);
            return (dt);



        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeMember(int OfId, int OfmId, int OfmType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeMe_Ka_Ot", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@OfmId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@OfmType", SqlDbType.Int);

            if (string.IsNullOrEmpty(OfId.ToString()))
                OfId = -1;
            adapter.SelectCommand.Parameters["@OfId"].Value = OfId;

            if (string.IsNullOrEmpty(OfmId.ToString()))
                OfmId = -1;
            adapter.SelectCommand.Parameters["@OfmId"].Value = OfmId;

            if (string.IsNullOrEmpty(OfmType.ToString()))
                OfmType = -1;
            adapter.SelectCommand.Parameters["@OfmType"].Value = OfmType;


            adapter.Fill(dt);
            return (dt);



        }

        #region SelectOfficeManagerForRequest
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeManagerForRequest(String FollowCode, String EndDateFrom, String EndDateTo, int OfId, string OfName, string FileNo, int MeId, int IsRequest, int HiddenPendingOffice)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@IsRequest"].Value = IsRequest;
            this.Adapter.SelectCommand.Parameters["@FollowCode"].Value = FollowCode;
            this.Adapter.SelectCommand.Parameters["@EndDateFrom"].Value = EndDateFrom;
            this.Adapter.SelectCommand.Parameters["@EndDateTo"].Value = EndDateTo;
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            this.Adapter.SelectCommand.Parameters["@OfName"].Value = OfName;
            this.Adapter.SelectCommand.Parameters["@FileNo"].Value = FileNo;
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@HiddenPendingOffice"].Value = HiddenPendingOffice;
            Fill();
            return this.DataTable;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeManagerForRequest(String FollowCode, String EndDateFrom, String EndDateTo)
        {
            return SelectOfficeManagerForRequest(FollowCode, EndDateFrom, EndDateTo, -1, "%", "%", -1, 0, 0);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeManagerForRequest(String FollowCode, String EndDateFrom, String EndDateTo, int OfId, string OfName, string FileNo, int MeId)
        {
            return SelectOfficeManagerForRequest(FollowCode, EndDateFrom, EndDateTo, OfId, OfName, FileNo, MeId, 1, 0);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeManagerForRequest(String FollowCode, String EndDateFrom, String EndDateTo, int OfId, string OfName, string FileNo, int MeId, int HiddenPendingOffice)
        {
            return SelectOfficeManagerForRequest(FollowCode, EndDateFrom, EndDateTo, OfId, OfName, FileNo, MeId, 1, HiddenPendingOffice);
        }
        #endregion

        //FollowCode, EndDateFrom, EndDateTo, OfName, FileNo, OfId, MeId
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo, string OfName, string FileNo, int OfId, int MeId, string CreateDateFrom, string CreateDateTo, int TaskId
            , string CreateDateLastReqFrom, string CreateDateLastReqTo)
        {
            if (FollowCode == "%" && EndDateFrom == "1" && EndDateTo == "2" && OfName == "%" && FileNo == "%" && OfId == -1 && MeId == -1 && CreateDateFrom == "1" && CreateDateTo == "2" && TaskId == -1
                && CreateDateLastReqFrom == "1" && CreateDateLastReqTo == "2")
                return new DataTable();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("spSelectOfficeForOfficeManagementPage", this.Connection);
            DataTable dt = new System.Data.DataTable();
            SqlAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@OfName", OfName);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@FileNo", FileNo);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@TableTypeOfMe", (int)TableTypeManager.FindTtId(TableType.OfficeMember));
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCodeOffConf", (int)WorkFlows.OfficeMembershipConfirming);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@CreateDateFrom", CreateDateFrom);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@CreateDateTo", CreateDateTo);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@CreateDateLastReqFrom", CreateDateLastReqFrom);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@CreateDateLastReqTo", CreateDateLastReqTo);
            SqlAdapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo, string OfName, string FileNo, int OfId, int MeId, string CreateDateFrom, string CreateDateTo, int TaskId)
        {
            if (FollowCode == "%" && EndDateFrom == "1" && EndDateTo == "2" && OfName == "%" && FileNo == "%" && OfId == -1 && MeId == -1 && CreateDateFrom == "1" && CreateDateTo == "2" && TaskId == -1)
                return new DataTable();
            return SelectOfficeForOfficeManagmentPage(FollowCode, EndDateFrom, EndDateTo, OfName, FileNo, OfId, MeId, CreateDateFrom, CreateDateTo, TaskId, "1", "2");
        }
        #region SelectOfficeForOfficeDocManagmentPage
        //
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeDocManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo, string OfName, string FileNo, int OfId, int MeId, int TaskCode, int MFType, String CreateDateFrom, String CreateDateTo, int DocStatus, int TaskId, string CreateDateLastReqFrom, string CreateDateLastReqTo)
        {
            DataTable dt = new System.Data.DataTable();
            if (FollowCode == "%" && EndDateFrom == "1" && EndDateTo == "2" && OfName == "%" && FileNo == "%" && OfId == -1 && MeId == -1 && TaskCode == -1 && MFType == -1 && CreateDateFrom == "1" && CreateDateTo == "2" && DocStatus == -1 && TaskId == -1
                && CreateDateLastReqFrom == "1" && CreateDateLastReqTo == "2")
                return dt;
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("spSelectOfficeForOfficeDocManagmentPage", this.Connection);
            SqlAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@OfName", OfName);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@FileNo", FileNo);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@TableTypeOfMe", (int)TableTypeManager.FindTtId(TableType.OfficeMember));
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.OfficeConfirming);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@TaskCode", TaskCode);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@MFType", MFType);

            SqlAdapter.SelectCommand.Parameters.AddWithValue("@CreateDateFrom", CreateDateFrom);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@CreateDateTo", CreateDateTo);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@DocStatus", DocStatus);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@CreateDateLastReqFrom", CreateDateLastReqFrom);
            SqlAdapter.SelectCommand.Parameters.AddWithValue("@CreateDateLastReqTo", CreateDateLastReqTo);

            SqlAdapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeDocManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo, string OfName, string FileNo, int OfId, int MeId, int TaskCode, int MFType, String CreateDateFrom, String CreateDateTo, int DocStatus, int TaskId)
        {
            if (FollowCode == "%" && EndDateFrom == "1" && EndDateTo == "2" && OfName == "%" && FileNo == "%" && OfId == -1 && MeId == -1 && TaskCode == -1 && MFType == -1 && CreateDateFrom == "1" && CreateDateTo == "2" && DocStatus == -1 && TaskId == -1)
                return new DataTable();
            return SelectOfficeForOfficeDocManagmentPage(FollowCode, EndDateFrom, EndDateTo, OfName, FileNo, OfId, MeId, TaskCode, MFType, CreateDateFrom, CreateDateTo, DocStatus, TaskId, "1", "2");
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeDocManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo, string OfName, string FileNo, int OfId, int MeId, int TaskCode, int MFType, String CreateDateFrom, String CreateDateTo, int DocStatus)
        {
            return SelectOfficeForOfficeDocManagmentPage(FollowCode, EndDateFrom, EndDateTo, OfName, FileNo, OfId, MeId, TaskCode, MFType, "1", "2", -1, -1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeDocManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo, string OfName, string FileNo, int OfId, int MeId, int TaskCode, int MFType)
        {
            return SelectOfficeForOfficeDocManagmentPage(FollowCode, EndDateFrom, EndDateTo, OfName, FileNo, OfId, MeId, TaskCode, MFType, "1", "2", -1);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeDocManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo, string OfName, string FileNo, int OfId, int MeId, int TaskCode)
        {
            return SelectOfficeForOfficeDocManagmentPage(FollowCode, EndDateFrom, EndDateTo, OfName, FileNo, OfId, MeId, TaskCode, -1);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeDocManagmentPage(String FollowCode, String EndDateFrom, String EndDateTo, string OfName, string FileNo, int OfId, int MeId)
        {
            return SelectOfficeForOfficeDocManagmentPage(FollowCode, EndDateFrom, EndDateTo, OfName, FileNo, OfId, MeId, -1);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForOfficeDocManagmentPage(int TaskCode)
        {
            return SelectOfficeForOfficeDocManagmentPage("%", "1", "2", "%", "%", -1, -1, TaskCode);
        }
        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeManagerByOfId(int OfId)
        {
            return SelectOfficeManagerByOfId(OfId, -1, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeManagerByOfId(int OfId, int InActive, int OfReId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeManager", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@OfReId", SqlDbType.Int);
            if (string.IsNullOrEmpty(OfId.ToString()))
                OfId = -1;
            adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            adapter.SelectCommand.Parameters["@OfReId"].Value = OfReId;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForSearch(int OfId, int MeId, String OfName, Int16 MFType, string FromDate, string ToDate, int DocumentStatus)
        {
            DataTable dt = new DataTable();
            if (OfId == -2)
                return dt;

            SqlDataAdapter adapter = new SqlDataAdapter("SelectOfficeForSearch", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (OfId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            if (MeId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            if (OfName != null && String.IsNullOrEmpty(OfName.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@OfName", OfName);
            if (FromDate != null && String.IsNullOrEmpty(FromDate.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            else
                FromDate = "1";
            if (ToDate != null && String.IsNullOrEmpty(ToDate.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            else
                ToDate = "2";
            if (MFType != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@MFType", MFType);
            adapter.SelectCommand.Parameters.AddWithValue("@DocumentStatus", DocumentStatus);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForSearchPortal(int OfId, string MeNo, String OfName, string ManagerFullName, Int16 MFType,int GrdId)
        {

            DataTable dt = new DataTable();
            if (OfId == -1 &&  MeNo == "%" && OfName == "%" && ManagerFullName =="%" && MFType == -1 && GrdId == -1)
                return dt;

            SqlDataAdapter adapter = new SqlDataAdapter("SelectOfficeForSearchPortal", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeNo", MeNo);
            adapter.SelectCommand.Parameters.AddWithValue("@OfName", OfName);
            adapter.SelectCommand.Parameters.AddWithValue("@ManagerFullName", ManagerFullName);
            adapter.SelectCommand.Parameters.AddWithValue("@MFType", MFType);
            adapter.SelectCommand.Parameters.AddWithValue("@GrdId", GrdId);

            adapter.Fill(dt);
            return (dt);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeForSearch(int OfId, int MeId, String OfName, Int16 MFType, string FromDate, string ToDate)
        {
            return SelectOfficeForSearch(OfId, MeId, OfName, MFType, FromDate, ToDate);
        }

        /// <summary>
        /// پایه شرکت را بر اساس رشته و صلاحیت مورد نظر بر می گرداند
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="OfKind"></param>
        /// <param name="ResId"></param>
        /// <param name="MjId"></param>
        /// <returns></returns>
        public int GetMaxGradeOfOffice(int OfId, int ResId, int MjId)
        {
            int GrdId = -1;
            DataTable dtOfMe = new DataTable();
            int MaxObsId = 0;
            int MaxDsgnId = 0;
            int ObsId = -1;
            int DesId = -1;

            SqlDataAdapter ad = new SqlDataAdapter("spReportOfficeMembers", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            ad.Fill(dtOfMe);


            dtOfMe.DefaultView.RowFilter = "FMjId=" + MjId;

            for (int i = 0; i < dtOfMe.DefaultView.Count; i++)
            {
                if (!Utility.IsDBNullOrNullValue(dtOfMe.DefaultView[i]["ObsId"]))
                    ObsId = Convert.ToInt32(dtOfMe.DefaultView[i]["ObsId"]);

                if ((MaxObsId == 0 || ObsId < MaxObsId) && ObsId != -1)
                {
                    MaxObsId = ObsId;
                }


                if (!Utility.IsDBNullOrNullValue(dtOfMe.DefaultView[i]["DesId"]))
                    DesId = Convert.ToInt32(dtOfMe.DefaultView[i]["DesId"]);

                if ((MaxDsgnId == 0 || DesId < MaxDsgnId) && DesId != -1)
                {
                    MaxDsgnId = DesId;

                }
            }


            if (ResId == (int)DocumentResponsibilityType.Design)
                GrdId = MaxDsgnId;
            else if (ResId == (int)DocumentResponsibilityType.Observation)
                GrdId = MaxObsId;
            return GrdId;
        }

        public DataTable SelectOfficeForWebService(int OfficeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectOfficeForWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfficeId);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable SelectOfficeForWebServiceBasedOnConfirmDateTime(int OfficeId,DateTime ConfirmDateTime)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectOfficeForWebServiceBasedOnConfirmDateTime", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfficeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ConfirmDateTime", ConfirmDateTime);
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCodeConfirmOfficeRequest", (int)WorkFlowTask.ConfirmOfficeAndEndProccess);
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCodeOffConf", (int)WorkFlows.OfficeMembershipConfirming);
            
            adapter.Fill(dt);
            return dt;
        }

        public DataTable SelectNewOfficeIdForWebService(int OfficeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectNewOfficeIdForWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfficeId);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable SelectNewOfficeIdForWebServiceBasedOnConfirmDateTime(int OfficeId, DateTime ConfirmDateTime)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectNewOfficeIdForWebServiceBasedOnConfirmDateTime", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfficeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ConfirmDateTime", ConfirmDateTime);
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCodeConfirmOfficeRequest", (int)WorkFlowTask.ConfirmOfficeAndEndProccess);
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCodeOffConf", (int)WorkFlows.OfficeMembershipConfirming);

            adapter.Fill(dt);
            return dt;
        }
        public DataTable SelectOfficeInfoForTSWebService(int OfId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectOfficeInfoForTSWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int, 4).Value = OfId;
            adapter.Fill(dt);
            return (dt);
        }

        public DataTable spSelectOfficeRequestCount()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeRequestCount", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCodeOffConf", (int)WorkFlows.OfficeMembershipConfirming);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCode", (int)WorkFlowTask.MembershipUnitConfirmingOffice);
            adapter.Fill(dt);
            return (dt);
        }

    }

}
