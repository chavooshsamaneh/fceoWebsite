using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
namespace TSP.DataManager
{
    public class OfficeMemberManager : BaseObject
    {
        static OfficeMemberManager()
        {
            _tableId = TableType.OfficeMember;
        }
        public OfficeMemberManager()
            : base()
        {
        }
        #region Perimissions
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeMember);
        }

        public static Permission GetUserPermissionForOffDoc(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeMemberDocument);
        }

        public static Permission GetUserPermissionForEngOffice(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EngOfficeMember);
        }
        #endregion
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblOfficeMember";
            tableMapping.ColumnMappings.Add("OfmId", "OfmId");
            tableMapping.ColumnMappings.Add("OfId", "OfId");
            tableMapping.ColumnMappings.Add("PersonId", "PersonId");
            tableMapping.ColumnMappings.Add("OfmType", "OfmType");
            tableMapping.ColumnMappings.Add("OfpId", "OfpId");
            tableMapping.ColumnMappings.Add("OfReId", "OfReId");
            tableMapping.ColumnMappings.Add("StartDate", "StartDate");
            tableMapping.ColumnMappings.Add("EndDate", "EndDate");
            tableMapping.ColumnMappings.Add("HasSignRight", "HasSignRight");
            tableMapping.ColumnMappings.Add("IsFullTime", "IsFullTime");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("SignImg", "SignImg");
            tableMapping.ColumnMappings.Add("SignUrl", "SignUrl");
            tableMapping.ColumnMappings.Add("OfKind", "OfKind");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("ConfirmDate", "ConfirmDate");
            tableMapping.ColumnMappings.Add("MfId", "MfId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("HasEfficientGrade", "HasEfficientGrade");
            tableMapping.ColumnMappings.Add("HasGasCert", "HasGasCert");
            tableMapping.ColumnMappings.Add("SelfreportedImageURL", "SelfreportedImageURL");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectOfficeMember";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@OfmId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@OfReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@PersonId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MemberIsConfirm", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@OfmType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@SysInActive", 0);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            this.Adapter.SelectCommand.Parameters.Add("@HasEfficientGrade", System.Data.SqlDbType.SmallInt);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteOfficeMember";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OfmId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfmId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertOfficeMember";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PersonId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PersonId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfmType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OfmType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HasSignRight", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "HasSignRight", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFullTime", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsFullTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignImg", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "SignImg", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfKind", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OfKind", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConfirmDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ConfirmDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HasEfficientGrade", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "HasEfficientGrade", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HasGasCert", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "HasGasCert", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SelfreportedImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SelfreportedImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateOfficeMember";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PersonId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PersonId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfmType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OfmType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HasSignRight", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "HasSignRight", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFullTime", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsFullTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignImg", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "SignImg", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfKind", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OfKind", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConfirmDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ConfirmDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OfmId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfmId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfmId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "OfmId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HasEfficientGrade", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "HasEfficientGrade", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HasGasCert", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "HasGasCert", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SelfreportedImageURL", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SelfreportedImageURL", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.OfficeDataSet.tblOfficeMemberDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchMember(int OfId, short OfmType, int OfmId)
        {
            DataTable dt = new OfficeDataSet.tblOfficeMemberDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeMe_Ka_Ot", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfId", System.Data.SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@OfmType", System.Data.SqlDbType.SmallInt, 2);
            adapter.SelectCommand.Parameters.Add("@OfmId", System.Data.SqlDbType.Int, 4);
            if (string.IsNullOrEmpty(OfId.ToString()))
                OfId = -1;
            adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            if (string.IsNullOrEmpty(OfmType.ToString()))
                OfmType = -1;
            adapter.SelectCommand.Parameters["@OfmType"].Value = OfmType;
            if (string.IsNullOrEmpty(OfmId.ToString()))
                OfmId = -1;
            adapter.SelectCommand.Parameters["@OfmId"].Value = OfmId;
            adapter.Fill(dt);
            return (dt);
        }

        #region FindByCode Methods
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int OfmId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OfmId"].Value = OfmId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            Fill();
        }

        public void FindByOfficeCode(int OfId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            Fill();
        }

        public void FindByOfficeCode(int OfId, int InActive)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            Fill();
        }
        #endregion

        #region Office *** اعضای شرکت ها
        public void FindForDelete(int OfReId, int OfKind)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeMemberDelete", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfReId", SqlDbType.Int, 4, "OfReId").Value = OfReId;
            adapter.SelectCommand.Parameters.Add("@OfKind", SqlDbType.Int, 4, "OfKind").Value = OfKind;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);

            adapter.Fill(DataTable);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeMemberForOfficeUserControl(int PersonId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectOfficeMemberForOfficeUserControl", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;           
            adapter.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int, 4, "PersonId").Value = PersonId;          
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);

            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByOffRequest(int OfId, int OfReId, short IsConfirm, int InActive, int PersonType, int PersonId, int MrsId, int JustThisRequestMembers
            , int HasEfficientGrade, int DocumentStatus)
        {
            if (this.ClearBeforeFill)
                this.DataTable.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeMemberByOfId", this.Connection);
            if (adapter.SelectCommand.Transaction == null) adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int).Value = OfId;
            adapter.SelectCommand.Parameters.Add("@OfReId", SqlDbType.Int).Value = OfReId;
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.SmallInt).Value = IsConfirm;
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int).Value = InActive;
            adapter.SelectCommand.Parameters.Add("@PersonType", SqlDbType.Int).Value = PersonType;
            adapter.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int, 4, "PersonId").Value = PersonId;
            adapter.SelectCommand.Parameters.Add("@MrsId", SqlDbType.Int, 4, "MrsId").Value = MrsId;
            adapter.SelectCommand.Parameters.Add("@JustThisRequestMembers", SqlDbType.Int, 4, "JustThisRequestMembers").Value = JustThisRequestMembers;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            adapter.SelectCommand.Parameters.Add("@HasEfficientGrade", SqlDbType.Int, 4, "HasEfficientGrade").Value = HasEfficientGrade;
            adapter.SelectCommand.Parameters.Add("@DocumentStatus", SqlDbType.Int, 4, "DocumentStatus").Value = DocumentStatus;

            adapter.Fill(DataTable);
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByOffRequest(int OfId, int OfReId, short IsConfirm, int InActive, int PersonType, int PersonId, int MrsId, int JustThisRequestMembers)
        {
            return FindByOffRequest(OfId, OfReId, IsConfirm, InActive, PersonType, PersonId, MrsId, JustThisRequestMembers, -1, -1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByActivePersonId(int OfId, int PersonId, short InActive)
        {
            return FindByOffRequest(OfId, -1, -1, InActive, -1, PersonId, -1, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByOffRequest(int OfId, int OfReId, short IsConfirm)
        {
            return FindByOffRequest(OfId, OfReId, IsConfirm, -1, -1, -1, -1, -1);
        }

        public DataTable FindByOffIdOffRequestAndPersonType(int OfId, int OfReId, int PersonType)
        {
            return FindByOffRequest(OfId, OfReId, -1, -1, PersonType, -1, -1, -1);
        }

        /// <summary>
        ///  Find Members By Id and TypeOf Member or OtherPerson
        /// </summary>
        /// <param name="PersonId">MeId or OtpId</param>
        /// <param name="PersonType">1:tblOtherperson  2:tblMember</param>
        /// <returns></returns>
        public DataTable FindOffMemberByPersonId(int PersonId, int PersonType)
        {
            return FindByOffRequest(-1, -1, -1, -1, PersonType, PersonId, -1, -1);
        }

        public DataTable FindOffMemberByPersonId(int PersonId, int PersonType, int MrsId)
        {
            return FindByOffRequest(-1, -1, -1, -1, PersonType, PersonId, MrsId, -1);
        }

        public DataTable FindOffMemberByPersonId(int PersonId, int PersonType, int MrsId, int HasEfficientGrade, int DocumentStatus)
        {
            return FindByOffRequest(-1, -1, -1, -1, PersonType, PersonId, MrsId, -1, HasEfficientGrade, DocumentStatus);
        }

        public DataTable FindOffMemberByOfReId(int OfReId)
        {
            return FindByOffRequest(-1, OfReId, -1, -1, -1, -1, -1, 1);
        }

        public DataTable FindOffMemberByOfReId(int OfReId, Int16 InActive)
        {
            return FindByOffRequest(-1, OfReId, -1, InActive, -1, -1, -1, 1);
        }

        public DataTable FindOfficeActiveMember(int OfId)
        {
            return FindByOffRequest(OfId, -1, -1, 0, -1, -1, -1, -1);
        }


        public DataTable SelectOfficeMember(int OfId, short OfmType, int OfmId, int InActive)
        {
            DataTable dt = new OfficeDataSet.tblOfficeMemberDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeMemberByOfId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfId", System.Data.SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@OfmType", System.Data.SqlDbType.SmallInt, 2);
            adapter.SelectCommand.Parameters.Add("@OfmId", System.Data.SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            adapter.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int).Value = InActive;
            if (string.IsNullOrEmpty(OfId.ToString()))
                OfId = -1;
            adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            if (string.IsNullOrEmpty(OfmType.ToString()))
                OfmType = -1;
            adapter.SelectCommand.Parameters["@OfmType"].Value = OfmType;
            if (string.IsNullOrEmpty(OfmId.ToString()))
                OfmId = -1;
            adapter.SelectCommand.Parameters["@OfmId"].Value = OfmId;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectOfficeMember(int OfId, short OfmType, int OfmId)
        {
            return SelectOfficeMember(OfId, OfmType, OfmId, -1);
        }

        public DataTable SelectOfficeMemberByMeId(int PersonId)
        {
            Adapter.SelectCommand.Parameters["@PersonId"].Value = PersonId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            Adapter.Fill(DataTable);
            return this.DataTable;
        }

        public DataTable SelectActiveOfficeMemberByMeId(int PersonId)
        {
            Adapter.SelectCommand.Parameters["@PersonId"].Value = PersonId;
            Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            Adapter.Fill(DataTable);
            return this.DataTable;
        }

        /// <summary>
        /// اعضای شرکت را بر می گرداند بر اساس کد شرکت
        /// </summary>
        public void FindOfficeActiveMembers(int OfId, int OfmType, int InActive, int MemberIsConfirm)
        {
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            this.Adapter.SelectCommand.Parameters["@MemberIsConfirm"].Value = MemberIsConfirm;
            this.Adapter.SelectCommand.Parameters["@OfmType"].Value = OfmType;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="PersonId"></param>
        /// <param name="sysInActive">غیر فعال شده توسط سیستم به صورت اتوماتیک</param>
        public void FindOfficeMembers(int OfId, int PersonId, int sysInActive)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@PersonId"].Value = PersonId;
            this.Adapter.SelectCommand.Parameters["@sysInActive"].Value = sysInActive;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            Fill();
        }

        /// <summary>
        ///   اعضای شرکت را بر می گرداند بر اساس کد شرکت و کد درخواست پروانه شرکت
        /// </summary>
        public void FindOfficeActiveMembersByOfReId(int OfId, int OfReId, int OfmType, int InActive, int MemberIsConfirm)
        {
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            this.Adapter.SelectCommand.Parameters["@OfReId"].Value = OfReId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = InActive;
            this.Adapter.SelectCommand.Parameters["@MemberIsConfirm"].Value = MemberIsConfirm;
            this.Adapter.SelectCommand.Parameters["@OfmType"].Value = OfmType;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            Fill();
        }

        /// <summary>
        /// اعضای شرکت را بر می گرداند بر اساس کد شرکت
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindOfficeAllActiveMembers(int OfId, int InActive, int MemberIsConfirm)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeMemberActiveMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberIsConfirm", MemberIsConfirm);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        /// <summary>
        /// اعضای فعال و تایید شده شرکت را بر می گرداند بر اساس کد شرکت و تاریخ خاص
        /// </summary>
        public void FindOfficeActiveMembersByDate(int OfId, int OfmType, string Date)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeMemberByDate", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@OfmType", OfmType);
            adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);

            adapter.Fill(DataTable);

        }

        /// <summary>
        /// اعضای شرکت را بر اساس آخرین درخواست تایید شده شرکت برمیگرداند
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastRequestOfficeMember(int OfId, int InActive, int PersonId, int HasEfficientGrade, int DocumentStatus)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectLastRequestOfficeMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@PersonId", PersonId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberKind", 0);
            adapter.SelectCommand.Parameters.AddWithValue("@HasEfficientGrade", HasEfficientGrade);
            adapter.SelectCommand.Parameters.AddWithValue("@DocumentStatus", DocumentStatus);

            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return (dt);
        }


        public DataTable SelectLastRequestOfficeMember(int OfId, int InActive, int PersonId)
        {
            return SelectLastRequestOfficeMember(OfId, InActive, PersonId, -1, -1);
        }

        /// <summary>
        /// اعضای شرکت را بر میگرداند
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="InActive"></param>
        /// <param name="PersonId"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectReportOfficeMembers(int OfId, int MeId, int MFType, int OfReId)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spReportOfficeMembers", this.Connection);
            if (adapter.SelectCommand.Transaction == null)
                adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@MFType", MFType);
            adapter.SelectCommand.Parameters.AddWithValue("@OfReId", OfReId);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectOfficeByKardan(int PersonId, int OfmType)
        {
            if (PersonId == -2 && OfmType == -2)
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeMemberForSearchKardan", this.Connection);
            if (adapter.SelectCommand.Transaction == null)
                adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PersonId", PersonId);
            adapter.SelectCommand.Parameters.AddWithValue("@OfmType", OfmType);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            adapter.Fill(dt);
            return (dt);
        }
        #endregion

        #region EngOffice **** اعضای دفاتر

        public void FindForDeleteEngOffice(int OfReId, int OfKind)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeMemberDelete", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@OfReId", SqlDbType.Int, 4, "OfReId").Value = OfReId;
            adapter.SelectCommand.Parameters.Add("@OfKind", SqlDbType.Int, 4, "OfKind").Value = OfKind;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);

            adapter.Fill(DataTable);
        }
        #region selectEngOfficeMember

        public DataTable spReportEngOfficeMembers(int EngOfId)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter ad = new SqlDataAdapter("spReportEngOfficeMembers", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@EngOfId", EngOfId);
            ad.SelectCommand.Parameters.AddWithValue("@WithManager", -1);
            ad.SelectCommand.Parameters.AddWithValue("@TableTypeEngOfMe", TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember));
            ad.SelectCommand.Parameters.AddWithValue("@TableTypeDocMajor", TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberFileMajor));
            ad.SelectCommand.Parameters.AddWithValue("@TableTypeDocDetail", TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberFileDetail));
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        #region Methods selectEngOfficeMember

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectEngOfficeMemberForWFCheck(int EOfId, int EngOfId, int InActive)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectEngOfficeMemberForWfCheck", this.Connection);
            if (ad.SelectCommand.Transaction == null) ad.SelectCommand.Transaction = this.Transaction;
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@OfReId", EOfId);
            ad.SelectCommand.Parameters.AddWithValue("@OfId", EngOfId);
            ad.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            ad.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember));
            ad.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectEngOfficeMember(int EOfId, int EngOfId, Int16 IsConfirm, int OfmId, int PersonId, int JustThisRequestMembers, int OfmType
             , int InActive, int MemberIsConfirm, int OfpId)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter ad = new SqlDataAdapter("spSelectEngOfficeMember", this.Connection);
            if (ad.SelectCommand.Transaction == null) ad.SelectCommand.Transaction = this.Transaction;
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@OfReId", System.Data.SqlDbType.Int);
            ad.SelectCommand.Parameters.Add("@OfId", System.Data.SqlDbType.Int);
            ad.SelectCommand.Parameters["@OfReId"].Value = EOfId;
            ad.SelectCommand.Parameters["@OfId"].Value = EngOfId;
            ad.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int, 4, "PersonId").Value = PersonId;
            ad.SelectCommand.Parameters.Add("@OfmId", SqlDbType.Int, 4, "OfmId").Value = OfmId;
            ad.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4, "IsConfirm").Value = IsConfirm;
            ad.SelectCommand.Parameters.Add("@JustThisRequestMembers", SqlDbType.Int, 4, "JustThisRequestMembers").Value = JustThisRequestMembers;
            ad.SelectCommand.Parameters.Add("@OfmType", SqlDbType.Int, 4, "OfmType").Value = OfmType;
            ad.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            ad.SelectCommand.Parameters.Add("@MemberIsConfirm", SqlDbType.Int, 4, "MemberIsConfirm").Value = MemberIsConfirm;
            ad.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember));
            ad.SelectCommand.Parameters.AddWithValue("@OfpId", OfpId);

            ad.Fill(DataTable);
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectEngOfficeMember(int EOfId, int EngOfId, Int16 IsConfirm, int OfmId, int PersonId, int JustThisRequestMembers, int OfmType
             , int InActive, int MemberIsConfirm)
        {
            return selectEngOfficeMember(EOfId, EngOfId, IsConfirm, OfmId, PersonId, JustThisRequestMembers, OfmType, InActive, MemberIsConfirm, -1);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectEngOfficeMember(int EOfId, int EngOfId, Int16 IsConfirm, int OfmId)
        {
            return selectEngOfficeMember(EOfId, EngOfId, IsConfirm, OfmId, -1, -1, -1, -1, -1);
        }
        //تابع با این ورودی ها تمام درخواست های کوچکتر مساوی مقدار داده شده را باز می گرداند
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectEngOfficeMember(int EOfId, Int16 IsConfirm, int OfmId)
        {
            return selectEngOfficeMember(EOfId, -1, IsConfirm, OfmId, -1, -1, -1, -1, -1);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectEngOfficeMember(int EOfId, int EngOfId)
        {
            return selectEngOfficeMember(EOfId, EngOfId, -1, -1, -1, -1, -1, -1, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectEngOfficeMember(int OfmId)
        {
            return selectEngOfficeMember(-1, -1, -1, OfmId, -1, -1, -1, -1, -1);
        }
        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectActiveEngOfficeMember(int EOfId, int EngOfId)
        {
            return selectEngOfficeMember(EOfId, EngOfId, -1, -1, -1, -1, -1, 0, -1);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectActiveEngOfficemanager(int EngOfId)
        {
            return selectEngOfficeMember(-1, EngOfId, -1, -1, -1, -1, -1, 0, -1, (int)OfficePosition.EngOfficeManager);
        }
        #endregion

        #region FindEngOfficeMemberByPersonId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="IsConfirm">EngOfficeIsConfirm</param>
        /// <returns></returns>
        public DataTable FindEngOfficeMemberByPersonId(int PersonId, int EngOfficeIsConfirm, int IsConfirm, int InActive)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter ad = new SqlDataAdapter("spSelectEngOfficeMember", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (this.Transaction != null)
                ad.SelectCommand.Transaction = this.Transaction;
            ad.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int, 4, "PersonId").Value = PersonId;
            ad.SelectCommand.Parameters.Add("@EngOfficeIsConfirm", SqlDbType.Int, 4, "EngOfficeIsConfirm").Value = EngOfficeIsConfirm;
            ad.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            ad.SelectCommand.Parameters.AddWithValue("@IsConfirm", IsConfirm);
            ad.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember));

            ad.Fill(DataTable);
            return this.DataTable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="IsConfirm">EngOfficeIsConfirm</param>
        /// <returns></returns>
        public DataTable SelectEngOfficeMemberForUserControlMeEngOfInfo(int PersonId, string EngOfficeConfirmationTypeList, int InActive)
        {
            SqlDataAdapter ad = new SqlDataAdapter("spSelectEngOfficeMemberForUserControlMeEngOfInfo", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (this.Transaction != null)
                ad.SelectCommand.Transaction = this.Transaction;
            ad.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int, 4, "PersonId").Value = PersonId;
            ad.SelectCommand.Parameters.AddWithValue("@EngOfficeConfirmationTypeList", EngOfficeConfirmationTypeList);
            ad.SelectCommand.Parameters.Add("@InActive", SqlDbType.Int, 4, "InActive").Value = InActive;
            ad.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember));

            ad.Fill(DataTable);
            return this.DataTable;
        }
        
        public DataTable IsMemberOfImpOffice(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectIsMemberOfImpOffice", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int, 4).Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }
        public DataTable FindEngOfficeMemberForWebServiceEsys(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spEngOfficeMembersForWebServiceEsys", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int, 4).Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }
        

        public DataTable FindOfficeMemberForWebServiceEsys(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectOfficeMembersForWebServiceEsys", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PersonId", SqlDbType.Int, 4).Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="IsConfirm">EngOfficeIsConfirm</param>
        /// <returns></returns>
        public DataTable FindEngOfficeMemberByPersonId(int PersonId, int EngOfficeIsConfirm, int IsConfirm)
        {
            return FindEngOfficeMemberByPersonId(PersonId, EngOfficeIsConfirm, IsConfirm, -1);
        }

        public DataTable FindEngOfficeMemberByPersonId(int PersonId)
        {
            return FindEngOfficeMemberByPersonId(PersonId, -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="IsConfirm">EngOfficeIsConfirm</param>
        /// <returns></returns>
        public DataTable FindEngOfficeMemberByPersonId(int PersonId, int EngOfficeIsConfirm)
        {
            return FindEngOfficeMemberByPersonId(PersonId, EngOfficeIsConfirm, -1);
        }
        #endregion

        public DataTable FindEngOfficeMemberByOfmCode(int OfmId)
        {
            return selectEngOfficeMember(-1, -1, -1, OfmId, -1, -1, -1, -1, -1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]

        public DataTable selectEngOfficeMemberByEOfId(int EOfId)
        {
            return selectEngOfficeMember(EOfId, -1, -1, -1, -1, 1, -1, -1, -1);
        }

        public DataTable FindEngOfficeActiveMembers(int EngOfId, int OfmType, int InActive, int MemberIsConfirm)
        {
            return selectEngOfficeMember(-1, EngOfId, -1, -1, -1, -1, OfmType, InActive, MemberIsConfirm);
        }

        /// <summary>
        /// اعضای دفتر را بر می گرداند بر اساس کد دفتر
        /// </summary>
        public void FindEngOfficeActiveMembers(int OfId, int InActive, int MemberIsConfirm)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberIsConfirm", MemberIsConfirm);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember));

            adapter.Fill(DataTable);
        }

        /// <summary>
        /// اعضای دفتر را بر می گرداند بر اساس کد دفتر
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindEngAllOfficeActiveMembers(int OfId, int InActive, int MemberIsConfirm)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberIsConfirm", MemberIsConfirm);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember));

            adapter.Fill(DataTable);
            return (DataTable);
        }

        /// <summary>
        /// اعضای فعال و تایید شده دفتر را بر می گرداند بر اساس کد دفتر و تاریخ خاص
        /// </summary>
        public void FindEngOfficeActiveMembersByDate(int OfId, string Date)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeActiveMemberByDate", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);

            adapter.Fill(DataTable);

        }

        /// <summary>
        /// اعضای فعال و تایید شده دفتر را بر می گرداند بر اساس کد پروانه دفتر
        /// </summary>
        public void FindEngOfficeActiveMembersByEOfId(int OfId, int OfReId)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOfficeActiveMemberByEOfId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@OfReId", OfReId);

            adapter.Fill(DataTable);

        }

        /// <summary>
        /// اعضای دفتر را بر اساس آخرین درخواست تایید شده دفتر برمیگرداند
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastRequestEngOfficeMember(int EngOfId, int InActive, int PersonId)
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectLastRequestOfficeMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", EngOfId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@PersonId", PersonId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberKind", 1);
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int).Value = TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return (dt);
        }


        public DataTable SelectLastOfmIdConfirmRequest(int EngOfId, int IsConfirm,int LastOfmId, int PersonId)
        {
            
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectLastOfmIdConfirmRequest", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", EngOfId); 
            adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", IsConfirm); 
            adapter.SelectCommand.Parameters.AddWithValue("@LastOfmId", LastOfmId);
            adapter.SelectCommand.Parameters.AddWithValue("@PersonId", PersonId);
  
            adapter.Fill(dt);
            return (dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="PersonId"></param>
        /// <param name="sysInActive">غیر فعال شده توسط سیستم به صورت اتوماتیک</param>
        public DataTable  FindEngOfficeMembers(int OfId, int PersonId, int sysInActive)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectEngOfficeMember", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (this.Transaction != null)
                ad.SelectCommand.Transaction = this.Transaction;
            ad.SelectCommand.Parameters.AddWithValue("@PersonId", PersonId);
            ad.SelectCommand.Parameters.AddWithValue("@EngOfficeIsConfirm", -1);
            ad.SelectCommand.Parameters.AddWithValue("@InActive", -1);
            ad.SelectCommand.Parameters.AddWithValue("@sysInActive", sysInActive);
            ad.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember));

            ad.Fill(DataTable);
            return this.DataTable;
        }
        #endregion

        #region Find Office Implement Grad

        #region FindOffImpGrade/Name
        /// <summary>
        /// پایه شخص حقوقی - اجرا Id
        /// </summary>
        public int FindOffImpGrade(int OfId, int OfReId)
        {
            int CountGrd3 = 0;
            int CountGrd2 = 0;
            int CountGrd1 = 0;
            int CountGrdArshad = 0;
            int OfficeGradeCode = (int)DocumentGrads.Grade3;
            DataTable dtMembers = this.SelectReportOfficeMembers(OfId, -1, 2, OfReId);
            for (int i = 0; i < dtMembers.Rows.Count; i++)
            {
                if (!Utility.IsDBNullOrNullValue(dtMembers.Rows[i]["ImpCode"]))
                {

                    if (Convert.ToInt32(dtMembers.Rows[i]["ImpCode"]) == (int)DocumentGrads.Arshad)
                        CountGrdArshad++;
                    else if (Convert.ToInt32(dtMembers.Rows[i]["ImpCode"]) == (int)DocumentGrads.Grade1)
                        CountGrd1++;
                    else if (Convert.ToInt32(dtMembers.Rows[i]["ImpCode"]) == (int)DocumentGrads.Grade2)
                        CountGrd2++;
                    else if (Convert.ToInt32(dtMembers.Rows[i]["ImpCode"]) == (int)DocumentGrads.Grade3)
                        CountGrd3++;
                }
            }
            ArrayList ArrGrdCount = new ArrayList();
            ArrGrdCount.Add(CountGrdArshad);
            ArrGrdCount.Add(CountGrd1);
            ArrGrdCount.Add(CountGrd2);
            ArrGrdCount.Add(CountGrd3);
            OfficeGradeCode = CalculateOfficeGrade(ArrGrdCount);
            return OfficeGradeCode;
        }

        /// <summary>
        /// پایه شخص حقوقی - اجرا Id
        /// </summary>
        public int FindOffImpGrade(int OfId)
        {
            return FindOffImpGrade(OfId, -1);
        }

        /// <summary>
        /// پایه شخص حقوقی - اجرا Id
        /// </summary>
        public string FindOffImpGradeName(int OfId, int OfReId)
        {
            string OfficeGrade = "";
            int OfficeGradeCode = FindOffImpGrade(OfId, OfReId);
            switch (OfficeGradeCode)
            {
                case (int)DocumentGrads.Grade3:
                    OfficeGrade = "سه";
                    break;
                case (int)DocumentGrads.Grade2:
                    OfficeGrade = "دو";
                    break;
                case (int)DocumentGrads.Grade1:
                    OfficeGrade = "یک";
                    break;
                case (int)DocumentGrads.Arshad:
                    OfficeGrade = "ارشد";
                    break;
            }
            return OfficeGrade;
        }

        /// <summary>
        /// نام پایه شخص حقوقی - اجرا
        /// </summary>
        public string FindOffImpGradeName(int OfId)
        {
            return FindOffImpGradeName(OfId, -1);
        }

        /// <summary>
        /// پایه شرکت را بر اساس تعداد پایه افراد بدست می آورد
        /// </summary>
        /// <param name="ArrayGradeCount">ArrayGradeCount[0]:CountArshad;ArrayGradeCount[1]:Count1;ArrayGradeCount[2]:Count2;ArrayGradeCount[3]:Count3</param>
        /// <returns>پایه شرکت را بر می گرداند</returns>
        private static int CalculateOfficeGrade(ArrayList ArrayGradeCount)
        {
            int OfficeGradeCode = (int)DocumentGrads.Grade3;
            Boolean GetGrade = false;
            int CountGrdArshad = Convert.ToInt32(ArrayGradeCount[0]);
            int CountGrd1 = Convert.ToInt32(ArrayGradeCount[1]);
            int CountGrd2 = Convert.ToInt32(ArrayGradeCount[2]);
            int CountGrd3 = Convert.ToInt32(ArrayGradeCount[3]);
            if (CountGrd2 >= 2)
            {
                OfficeGradeCode = (int)DocumentGrads.Grade2;
                GetGrade = true;
            }
            if (CountGrd1 >= 2)
            {
                OfficeGradeCode = (int)DocumentGrads.Grade1;
                GetGrade = true;
            }
            if (CountGrdArshad >= 2)
            {
                OfficeGradeCode = (int)DocumentGrads.Arshad;
                GetGrade = true;
            }
            if (!GetGrade)
            {
                if (CountGrd2 == 1 && CountGrd1 == 1)
                    OfficeGradeCode = (int)DocumentGrads.Grade2;
                if (CountGrd1 == 1 && CountGrdArshad == 1)
                    OfficeGradeCode = (int)DocumentGrads.Grade1;
            }
            return OfficeGradeCode;
        }
        #endregion

        /// <summary>
        ///  اطلاعات پایه و نوع ترکیب اعضای شخص حقوقی - اجرا بر اساس تاریخ
        /// ArrayList[0]: OfGrade , ArrayList[1]: DocOffType, ArrayList[2]: GrdId,ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId
        /// </summary>
        public ArrayList FindOfficeImpGrade(int OfId, string Date)
        {
            int OfGrade = -1;
            int DocOffType = -1;
            int GrdId1 = 0;
            int GrdId2 = 0;
            int MeId1 = -1;
            int MeId2 = -1;
            int Type1 = (int)OfficeMemberType.Kardan;
            int Type2 = (int)OfficeMemberType.Kardan;
            string OfGrdName = "";
            string GrdName1 = "";
            string GrdName2 = "";


            ArrayList arr = new ArrayList();

            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectOfficeBoardMembersByDate", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            ad.SelectCommand.Parameters.AddWithValue("@Date", Date);

            ad.Fill(dt);

            dt.DefaultView.RowFilter = "MjId=" + (int)MainMajors.Civil + "OR MjId=" + (int)MainMajors.Architecture;
            //this.CurrentFilter = "MjId=" + (int)MainMajors.Civil + "OR MjId=" + (int)MainMajors.Architecture;
            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                if (Type1 == Convert.ToInt32(dt.DefaultView[i]["OfmType"]) || Convert.ToInt32(dt.DefaultView[i]["OfmType"]) == (int)OfficeMemberType.Member)
                {
                    if (GrdId1 == 0 || Convert.ToInt32(dt.DefaultView[i]["ImpId"]) < GrdId1)
                    {
                        GrdId1 = Convert.ToInt32(dt.DefaultView[i]["ImpId"]);
                        MeId1 = Convert.ToInt32(dt.DefaultView[i]["MeId"]);
                        Type1 = Convert.ToInt32(dt.DefaultView[i]["OfmType"]);
                        GrdName1 = dt.DefaultView[i]["ImpName"].ToString();

                    }
                }

            }
            dt.DefaultView.RowFilter = "MeId<>" + MeId1;

            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                if (Type2 == Convert.ToInt32(dt.DefaultView[i]["OfmType"]) || Convert.ToInt32(dt.DefaultView[i]["OfmType"]) == (int)OfficeMemberType.Member)
                {
                    if (GrdId2 == 0 || Convert.ToInt32(dt.DefaultView[i]["ImpId"]) < GrdId2)
                    {
                        GrdId2 = Convert.ToInt32(dt.DefaultView[i]["ImpId"]);
                        MeId2 = Convert.ToInt32(dt.DefaultView[i]["MeId"]);
                        Type2 = Convert.ToInt32(dt.DefaultView[i]["OfmType"]);
                        GrdName2 = dt.DefaultView[i]["ImpName"].ToString();

                    }
                }

            }

            if (GrdId1 >= GrdId2)
            {
                OfGrade = GrdId1;
                OfGrdName = GrdName1;
            }
            else
            {
                OfGrade = GrdId2;
                OfGrdName = GrdName2;

            }


            if (Type1 == (int)OfficeMemberType.Member && Type2 == (int)OfficeMemberType.Member)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Engineer_Engineer;
            else if (Type1 == (int)OfficeMemberType.Member && Type2 == (int)OfficeMemberType.Kardan)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Kardan_Engineer;
            else if (Type1 == (int)OfficeMemberType.Kardan && Type2 == (int)OfficeMemberType.Kardan)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Kardan_Kardan;

            arr.Add(OfGrade);
            arr.Add(DocOffType);
            arr.Add(GrdId1);
            arr.Add(MeId1);
            arr.Add(MeId2);
            arr.Add(OfGrdName);
            arr.Add(Type1);
            arr.Add(Type2);

            return arr;
        }

        /// <summary>
        /// اطلاعات پایه و نوع ترکیب اعضای شخص حقوقی - اجرا
        /// ArrayList[0]: OfGrade , ArrayList[1]: DocOffType, ArrayList[2]: GrdId,ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId
        /// </summary>
        public ArrayList FindOfficeImpGrade(int OfId)
        {
            int OfGrade = -1;
            int DocOffType = -1;
            int GrdId1 = 0;
            int GrdId2 = 0;
            int MeId1 = -1;
            int MeId2 = -1;
            int Type1 = (int)OfficeMemberType.Kardan;
            int Type2 = (int)OfficeMemberType.Kardan;
            string OfGrdName = "";
            string GrdName1 = "";
            string GrdName2 = "";


            ArrayList arr = new ArrayList();

            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectOfficeBoardMembers", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            ad.Fill(dt);

            dt.DefaultView.RowFilter = "MjId=" + (int)MainMajors.Civil + "OR MjId=" + (int)MainMajors.Architecture;
            //this.CurrentFilter = "MjId=" + (int)MainMajors.Civil + "OR MjId=" + (int)MainMajors.Architecture;
            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                if (Type1 == Convert.ToInt32(dt.DefaultView[i]["OfmType"]) || Convert.ToInt32(dt.DefaultView[i]["OfmType"]) == (int)OfficeMemberType.Member)
                {
                    if (GrdId1 == 0 || Convert.ToInt32(dt.DefaultView[i]["ImpId"]) < GrdId1)
                    {
                        if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["ImpId"]))
                            GrdId1 = Convert.ToInt32(dt.DefaultView[i]["ImpId"]);
                        if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["MeId"]))
                            MeId1 = Convert.ToInt32(dt.DefaultView[i]["MeId"]);
                        if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["OfmType"]))
                            Type1 = Convert.ToInt32(dt.DefaultView[i]["OfmType"]);
                        if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["ImpName"]))
                            GrdName1 = dt.DefaultView[i]["ImpName"].ToString();

                    }
                }

            }
            dt.DefaultView.RowFilter = "MeId<>" + MeId1;

            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                if (Type2 == Convert.ToInt32(dt.DefaultView[i]["OfmType"]) || Convert.ToInt32(dt.DefaultView[i]["OfmType"]) == (int)OfficeMemberType.Member)
                {
                    if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["ImpId"]))
                        if (GrdId2 == 0 || Convert.ToInt32(dt.DefaultView[i]["ImpId"]) < GrdId2)
                        {
                            if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["ImpId"]))
                                GrdId2 = Convert.ToInt32(dt.DefaultView[i]["ImpId"]);
                            if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["MeId"]))
                                MeId2 = Convert.ToInt32(dt.DefaultView[i]["MeId"]);
                            if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["OfmType"]))
                                Type2 = Convert.ToInt32(dt.DefaultView[i]["OfmType"]);
                            if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["ImpName"]))
                                GrdName2 = dt.DefaultView[i]["ImpName"].ToString();

                        }
                }

            }

            if (GrdId1 >= GrdId2)
            {
                OfGrade = GrdId1;
                OfGrdName = GrdName1;
            }
            else
            {
                OfGrade = GrdId2;
                OfGrdName = GrdName2;

            }


            if (Type1 == (int)OfficeMemberType.Member && Type2 == (int)OfficeMemberType.Member)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Engineer_Engineer;
            else if (Type1 == (int)OfficeMemberType.Member && Type2 == (int)OfficeMemberType.Kardan)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Kardan_Engineer;
            else if (Type1 == (int)OfficeMemberType.Kardan && Type2 == (int)OfficeMemberType.Kardan)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Kardan_Kardan;

            arr.Add(OfGrade);
            arr.Add(DocOffType);
            arr.Add(GrdId1);
            arr.Add(MeId1);
            arr.Add(MeId2);
            arr.Add(OfGrdName);
            arr.Add(Type1);
            arr.Add(Type2);

            return arr;
        }

        /// <summary>
        /// اطلاعات پایه و نوع ترکیب اعضای شخص حقوقی بر اساس پروانه- اجرا
        /// ArrayList[0]: OfGrade , ArrayList[1]: DocOffType, ArrayList[2]: GrdId,ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId
        /// </summary>
        public ArrayList FindOfficeImpGradebyOfReId(int OfId, int OfReId)
        {
            int OfGrade = -1;
            int DocOffType = -1;
            int GrdId1 = 0;
            int GrdId2 = 0;
            int MeId1 = -1;
            int MeId2 = -1;
            int Type1 = (int)OfficeMemberType.Kardan;
            int Type2 = (int)OfficeMemberType.Kardan;
            string OfGrdName = "";
            string GrdName1 = "";
            string GrdName2 = "";


            ArrayList arr = new ArrayList();

            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectOfficeBoardMemberByOfReId", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            ad.SelectCommand.Parameters.AddWithValue("@OfReId", OfReId);

            ad.Fill(dt);

            dt.DefaultView.RowFilter = "MjId=" + (int)MainMajors.Civil + "OR MjId=" + (int)MainMajors.Architecture;
            //this.CurrentFilter = "MjId=" + (int)MainMajors.Civil + "OR MjId=" + (int)MainMajors.Architecture;
            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                if (Type1 == Convert.ToInt32(dt.DefaultView[i]["OfmType"]) || Convert.ToInt32(dt.DefaultView[i]["OfmType"]) == (int)OfficeMemberType.Member)
                {
                    if (GrdId1 == 0 || Convert.ToInt32(dt.DefaultView[i]["ImpId"]) < GrdId1)
                    {
                        GrdId1 = Convert.ToInt32(dt.DefaultView[i]["ImpId"]);
                        MeId1 = Convert.ToInt32(dt.DefaultView[i]["MeId"]);
                        Type1 = Convert.ToInt32(dt.DefaultView[i]["OfmType"]);
                        GrdName1 = dt.DefaultView[i]["ImpName"].ToString();

                    }
                }

            }
            dt.DefaultView.RowFilter = "MeId<>" + MeId1;

            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                if (Type2 == Convert.ToInt32(dt.DefaultView[i]["OfmType"]) || Convert.ToInt32(dt.DefaultView[i]["OfmType"]) == (int)OfficeMemberType.Member)
                {
                    if (GrdId2 == 0 || Convert.ToInt32(dt.DefaultView[i]["ImpId"]) < GrdId2)
                    {
                        GrdId2 = Convert.ToInt32(dt.DefaultView[i]["ImpId"]);
                        MeId2 = Convert.ToInt32(dt.DefaultView[i]["MeId"]);
                        Type2 = Convert.ToInt32(dt.DefaultView[i]["OfmType"]);
                        GrdName2 = dt.DefaultView[i]["ImpName"].ToString();

                    }
                }

            }

            if (GrdId1 >= GrdId2)
            {
                OfGrade = GrdId1;
                OfGrdName = GrdName1;
            }
            else
            {
                OfGrade = GrdId2;
                OfGrdName = GrdName2;

            }


            if (Type1 == (int)OfficeMemberType.Member && Type2 == (int)OfficeMemberType.Member)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Engineer_Engineer;
            else if (Type1 == (int)OfficeMemberType.Member && Type2 == (int)OfficeMemberType.Kardan)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Kardan_Engineer;
            else if (Type1 == (int)OfficeMemberType.Kardan && Type2 == (int)OfficeMemberType.Kardan)
                DocOffType = (int)DocOffOfficeMembersQualificationType.Kardan_Kardan;

            arr.Add(OfGrade);
            arr.Add(DocOffType);
            arr.Add(GrdId1);
            arr.Add(MeId1);
            arr.Add(MeId2);
            arr.Add(OfGrdName);
            arr.Add(Type1);
            arr.Add(Type2);

            return arr;
        }
        #endregion

        #region Finde Observation-Design Grade
        /// <summary>
        /// پایه نظارت و طراحی اعضای شرکت بر اساس 7 رشته پروانه - حقوقی طراح و ناظر
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindOfficeDsngAndObsGrade(int OfId)
        {
            DataTable dt = new DataTable();
            DataTable Mjdt = new DataTable();

            SqlDataAdapter ad = new SqlDataAdapter("spReportOfficeMembers", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            ad.Fill(dt);

            Mjdt.Columns.Add("ObsGrdId");
            Mjdt.Columns.Add("DsgnGrdId");
            Mjdt.Columns.Add("MjId");
            Mjdt.Columns.Add("MjName");
            Mjdt.Columns.Add("IsExist");
            Mjdt.Columns.Add("DsgnCapacity");
            Mjdt.Columns.Add("ObsCapacity");
            //Mjdt.Constraints.Add("PK_ID", Mjdt.Columns["MjId"], true);
            Mjdt.Columns.Add("Id");
            Mjdt.Columns["Id"].AutoIncrement = true;
            Mjdt.Columns["Id"].AutoIncrementSeed = 1;
            Mjdt.Constraints.Add("PK_ID", Mjdt.Columns["Id"], true);

            DataRow drArch = Mjdt.NewRow();
            drArch = GetMaxGradeOfMajor((int)MainMajors.Architecture, dt, drArch, "معماری");
            Mjdt.Rows.Add(drArch);

            DataRow drCiv = Mjdt.NewRow();
            drCiv = GetMaxGradeOfMajor((int)MainMajors.Civil, dt, drCiv, "عمران");
            Mjdt.Rows.Add(drCiv);

            DataRow drMech = Mjdt.NewRow();
            drMech = GetMaxGradeOfMajor((int)MainMajors.Mechanic, dt, drMech, "تاسیسات مکانیکی");
            Mjdt.Rows.Add(drMech);

            DataRow drElec = Mjdt.NewRow();
            drElec = GetMaxGradeOfMajor((int)MainMajors.Electronic, dt, drElec, "تاسسیسات برقی");
            Mjdt.Rows.Add(drElec);

            DataRow drUrb = Mjdt.NewRow();
            drUrb = GetMaxGradeOfMajor((int)MainMajors.Urbanism, dt, drUrb, "شهر سازی");
            Mjdt.Rows.Add(drUrb);

            DataRow drMap = Mjdt.NewRow();
            drMap = GetMaxGradeOfMajor((int)MainMajors.Mapping, dt, drMap, "نقشه برداری");
            Mjdt.Rows.Add(drMap);

            DataRow drTr = Mjdt.NewRow();
            drTr = GetMaxGradeOfMajor((int)MainMajors.Traffic, dt, drTr, "ترافیک");
            Mjdt.Rows.Add(drTr);
            return Mjdt;
        }

        public DataRow GetMaxGradeOfMajor(int MjId, DataTable dt, DataRow dr, string MjName)
        {
            int MaxObsId = 0;
            int MaxDsgnId = 0;
            string ObsGrade = "";
            string DsgnGrade = "";


            bool IsExist = false;

            dt.DefaultView.RowFilter = "FMjId=" + MjId;
            if (dt.DefaultView.Count > 0)
                IsExist = true;

            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["ObsId"]))
                {
                    if (MaxObsId == 0 || Convert.ToInt32(dt.DefaultView[i]["ObsId"]) < MaxObsId)
                    {
                        MaxObsId = Convert.ToInt32(dt.DefaultView[i]["ObsId"]);
                        ObsGrade = dt.DefaultView[i]["ObsName"].ToString();
                    }
                }
                if (!Utility.IsDBNullOrNullValue(dt.DefaultView[i]["DesId"]))
                {
                    if (MaxDsgnId == 0 || Convert.ToInt32(dt.DefaultView[i]["DesId"]) < MaxDsgnId)
                    {
                        MaxDsgnId = Convert.ToInt32(dt.DefaultView[i]["DesId"]);
                        DsgnGrade = dt.DefaultView[i]["DesName"].ToString();

                    }
                }
            }


            if (MaxObsId != 0)
                dr["ObsGrdId"] = ObsGrade;

            else
                dr["ObsGrdId"] = "---";
            if (MaxDsgnId != 0)
                dr["DsgnGrdId"] = DsgnGrade;
            else
                dr["DsgnGrdId"] = "---";
            dr["MjId"] = MjId;
            dr["MjName"] = MjName;
            dr["IsExist"] = IsExist;

            return dr;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="OfId">OfId Or EngOfId</param>
        /// <returns>result[0]=boolean ( Is Member Or Not),
        /// result[1]=OfKine(0=Office 1=EngOffice),
        /// result[2]=string(Message)</returns>
        public static ArrayList CheckMemberMembershipInOfficeAndEngOffice(int MeId, int Id, OfficeMemberKind OfKind)
        {
            ArrayList Result = new ArrayList();
            Result.Add(true);//*
            Result.Add(-1);
            Result.Add("");
            string MembershipType = "";
            if (OfKind == OfficeMemberKind.Office) MembershipType = "شرکت";
            else if (OfKind == OfficeMemberKind.EngOffice) MembershipType = "دفتر";
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            OfMeManager.FindEngOfficeMemberByPersonId(MeId, -1);//* (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
            OfMeManager.DataTable.DefaultView.RowFilter = "OfId <>" + Id.ToString() + "and (EngIsConfirm =" + (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed + "OR EngIsConfirm =" + (int)TSP.DataManager.EngOfficeConfirmationType.Pending + ")";
            if (OfMeManager.Count > 0)
            {
                int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                if (OfKind == OfficeMemberKind.Office || EngOfIdMember != Id)
                {
                    OfMeManager.DataTable.DefaultView.RowFilter = "";
                    DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, MeId);
                    if (dtEngOffReq.Rows.Count > 0)
                    {
                        string str = "امکان ثبت عضویت " + MeId.ToString() + " در این " + MembershipType + " وجود ندارد.عضو مورد نظر در دفتر ";
                        str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
                        str += " مشغول به کار می باشد";
                        Result[0] = false;//*
                        Result[1] = (int)OfficeMemberKind.EngOffice;
                        Result[2] = str;
                        return Result;
                    }
                }

            }
            OfMeManager.DataTable.DefaultView.RowFilter = "";
            OfMeManager.FindOffMemberByPersonId(MeId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed, 1, -1);//* (int)TSP.DataManager.OfficeDocumentStatus.Confirmed);
            OfMeManager.DataTable.DefaultView.RowFilter = "OfId <>" + Id.ToString() + "and (DocumentStatus =" + (int)TSP.DataManager.OfficeDocumentStatus.Confirmed + "OR DocumentStatus =" + (int)TSP.DataManager.OfficeDocumentStatus.Pending + ")";
            if (OfMeManager.Count > 0)
            {
                int OfId = Convert.ToInt32(OfMeManager[0]["OfId"]);
                if (OfKind == OfficeMemberKind.EngOffice || OfId != Id)
                {
                    OfMeManager.DataTable.DefaultView.RowFilter = "";
                    DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfId, 0, MeId, 1, (int)TSP.DataManager.OfficeDocumentStatus.Confirmed);
                    if (dtOffReq.Rows.Count > 0)
                    {
                        string str = "امکان ثبت عضویت " + MeId.ToString() + " در این " + MembershipType + " وجود ندارد.عضو مورد نظر در شرکت ";
                        str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
                        str += " مشغول به کار می باشد";
                        Result[0] = false;//*
                        Result[1] = (int)OfficeMemberKind.Office;
                        Result[2] = str;
                        return Result;
                    }
                }
            }
            OfMeManager.DataTable.DefaultView.RowFilter = "";
            return Result;
        }

        /// <summary>
        /// چک کردن شرایط عضویت یک شخص حقیقی در دفتر طراحی
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="EngOfId"></param>
        /// <returns>result[0]=boolean ( Is Condition Acceptable Or not),
        /// result[1]=string(Message),
        /// result[2]=MemberMfId</returns>
        public static ArrayList CheckEngOfficeMembershipCondition(int MeId, int EngOfId)
        {
            ArrayList Result = new ArrayList();
            Result.Add(true);
            Result.Add("");
            Result.Add(-1);
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();

            DataTable dtOffMe = OfMeManager.selectEngOfficeMember(-1, EngOfId, -1, -1, MeId, -1, -1, 0, -1);
            if (dtOffMe.Rows.Count > 0)
            {
                Result[0] = false;
                Result[1] = "پیش از این عضو انتخاب شده در این دفتر ثبت شده است";
                return Result;
            }



            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
            if (dtMeFile.Rows.Count <= 0)
            {
                Result[0] = false;
                Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار تایید شده نمی باشد.";
                return Result;
            }
            if (string.Compare(dtMeFile.Rows[0]["ExpireDate"].ToString(), Utility.GetDateOfToday()) < 0)
            {
                Result[0] = false;
                Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.مدت زمان اعتبار پروانه عضو انتخاب شده به پایان رسیده است.";
                return Result;

            }
            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            Result[2] = MemberFileId;
            ///*******Comment*******/////
            ///اگر نوع دفتر اجرا می توانست باشد کد تغییر می کند تا با نوع دفتر مجوز فرد چک شود
            //////***************
            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
                if (DocMemberFileMajorManager.Count <= 0)
                {
                    Result[0] = false;
                    Result[1] = "رشته موضوع پروانه شخص انتخاب شده نامشخص است";
                    return Result;
                }
                //اگر رشته موضوع پروانه نقشه برداری/ترافیک و یا شهرسازی باشد می توانند بدون داشتن صلاحیت طراحی عضو دفتر شوند
                if (Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Mapping
                    && Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Traffic
                    && Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Urbanism
                    && Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["MappingIdInLastConfirmedReq"])
                    && Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["TrafficIdLastConfirmedReq"]) 
                    && Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["UrbanismIdInLastConfirmedReq"]))
                {
                    DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
                    if (dtMeDetail.Rows.Count == 0)
                    {
                        Result[0] = false;
                        Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر هیج یک از صلاحیت های طراحی/نفشه برداری/ترافیک/شهرسازی را ندارد";
                        return Result;
                    }
                }
            }
            else
            {
                Result[0] = false;
                Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.وضعیت پروانه اشتغال عضو نا مشخص می باشد.";
                return Result;
            }

            DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
            if (DocMemberFileManager.Count > 0)
            {
                Result[0] = false;
                Result[1] = MeId.ToString() + " " + "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای مجوز اجرا می باشد.";
                return Result;
            }


            ArrayList ResultMembershipanother = TSP.DataManager.OfficeMemberManager.CheckMemberMembershipInOfficeAndEngOffice(MeId, EngOfId, TSP.DataManager.OfficeMemberKind.EngOffice);
            if (!Convert.ToBoolean(ResultMembershipanother[0]))
            {
                Result[0] = false;//*
                Result[1] = ResultMembershipanother[2].ToString();
                return Result;
            }

            OfMeManager.FindEngOfficeMemberByPersonId(MeId, -1);
            OfMeManager.DataTable.DefaultView.RowFilter = "OfId <>" + EngOfId.ToString() + "and (EngIsConfirm =" + (int)TSP.DataManager.EngOfficeConfirmationType.Cancel + ")";
            if (OfMeManager.Count > 0)
            {
                int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                OfMeManager.DataTable.DefaultView.RowFilter = "";
                DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, MeId);
                if (dtEngOffReq.Rows.Count > 0)
                {
                    string str = "هشدار!عضویت " + MeId.ToString() + " پیش از این در دفتر باطل شده ";
                    str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
                    str += " مشغول به کار بوده است.در صورتی که امکان احیا دفتر قبلی برای نامبرده وجود دارد درخواست جدید ثبت شده را لغو و دفتر قبلی وی را احیاء نمایید";
                    Result[0] = true;
                    Result[1] = str;
                    return Result;
                }
            }
            return Result;
        }

        /// <summary>
        /// چک کردن شرایط عضویت یک شخص حقیقی در دفتر طراحی
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="EngOfId"></param>
        /// <returns>result[0]=boolean ( Is Condition Acceptable Or not),
        /// result[1]=string(Message),
        /// result[2]=MemberMfId</returns>
        public static ArrayList CheckEngOfficeMembershipConditionForWFConfirm(int MeId, int EngOfId)
        {
            ArrayList Result = new ArrayList();
            Result.Add(true);
            Result.Add("");
            Result.Add(-1);
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            //TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            ArrayList ResultMembershipanother = TSP.DataManager.OfficeMemberManager.CheckMemberMembershipInOfficeAndEngOffice(MeId, EngOfId, TSP.DataManager.OfficeMemberKind.EngOffice);
            if (!Convert.ToBoolean(ResultMembershipanother[0]))
            {
                Result[0] = false;//*
                Result[1] = ResultMembershipanother[2].ToString();
                return Result;
            }

            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
            if (dtMeFile.Rows.Count <= 0)
            {
                Result[0] = false;
                Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار تایید شده نمی باشد.";
                return Result;
            }
            if (string.Compare(dtMeFile.Rows[0]["ExpireDate"].ToString(), Utility.GetDateOfToday()) < 0)
            {
                Result[0] = false;
                Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.مدت زمان اعتبار پروانه عضو انتخاب شده به پایان رسیده است.";
                return Result;

            }
            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            Result[2] = MemberFileId;
            ///*******Comment*******/////
            ///اگر نوع دفتر اجرا می توانست باشد کد تغییر می کند تا با نوع دفتر مجوز فرد چک شود
            //////***************
            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
                if (DocMemberFileMajorManager.Count <= 0)
                {
                    Result[0] = false;
                    Result[1] = "رشته موضوع پروانه شخص انتخاب شده نامشخص است";
                    return Result;
                }
                //اگر رشته موضوع پروانه نقشه برداری/ترافیک و یا شهرسازی باشد می توانند بدون داشتن صلاحیت طراحی عضو دفتر شوند
                if (Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Mapping
                    && Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Traffic
                    && Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]) != (int)TSP.DataManager.MainMajors.Urbanism)
                {
                    DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
                    if (dtMeDetail.Rows.Count == 0)
                    {
                        Result[0] = false;
                        Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت طراحی ندارد";
                        return Result;
                    }
                }
            }
            else
            {
                Result[0] = false;
                Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.وضعیت پروانه اشتغال عضو نا مشخص می باشد.";
                return Result;
            }

            DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
            if (DocMemberFileManager.Count > 0)
            {
                Result[0] = false;
                Result[1] = MeId.ToString() + " " + "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای مجوز اجرا می باشد.";
                return Result;
            }
            return Result;
        }
        public static ArrayList CheckOfficeMembershipcondition(int MeId, int OfId, int OfReId, Boolean CheckifRepeatative = true)
        {
            ArrayList Result = new ArrayList();
            Result.Add(true);
            Result.Add("");
            Result.Add(-1);
            int MemberFileId = -1;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            if (CheckifRepeatative)
            {
                OfMeManager.FindOfficeActiveMembers(OfId, (int)TSP.DataManager.OfficeMemberType.Member, 0, -1);
                for (int i = 0; i < OfMeManager.Count; i++)
                {
                    if (Convert.ToInt32(OfMeManager[i]["PersonId"]) == MeId && OfMeManager[i]["Active"].ToString() == "فعال")
                    {
                        Result[0] = false;
                        Result[1] = "اطلاعات وارد شده تکراری می باشد";
                        return Result;
                    }
                }
            }
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count == 0)
            {
                Result[0] = false;
                Result[1] = "خطایی در بازیابی اطلاعات صورت گرفته است.";
                return Result;
            }
            if (Convert.ToInt32(ReqManager[0]["MembershipRequstType"]) != 6)//مجری لوله کشی گاز می تواند در دفتر و شرکت دیگر عضو باشد و شرایط چک نمی شود
            {
                ArrayList ResultMembershipanother = TSP.DataManager.OfficeMemberManager.CheckMemberMembershipInOfficeAndEngOffice(MeId, OfId, TSP.DataManager.OfficeMemberKind.Office);
                if (!Convert.ToBoolean(ResultMembershipanother[0]))
                {
                    Result[0] = false;//*
                    Result[1] = ResultMembershipanother[2].ToString();
                    return Result;
                }
            }

            DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
            if (DocMemberFileManager.Count > 0 && Convert.ToInt32(DocMemberFileManager[0]["Type"]) != (int)DocumentOfMemberRequestType.InActive)
            {
                Result[0] = false;
                Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای مجوز اجرا می باشد.";
                return Result;
            }

            #region Check MemberFileConditions
          
            //*****اگر دارای شرکت دارای پروانه باشد شرایط پروانه شخص مورد بررسی قرار می گیرد
            if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
            {
                int OffType = Convert.ToInt32(ReqManager[0]["MFType"]);
                if (OffType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//tarah o nazer
                {
                    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                    if (dtMeFile.Rows.Count > 0)/////<==0
                    {
                        //////////Result[0] = false;
                        //////////Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار نمی باشد.";
                        //////////return Result;
                        //////}
                        Result[2] = MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                        string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
                        ///*******Comment*******/////
                        ///اگر نوع شرکت اجرا می توانست باشد کد تغییر می کند تا با نوع شرکت مجوز فرد چک شود.در ارسال به مرحله بعد چک می شود
                        //////***************

                        if (Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) || Convert.ToInt32(dtMeFile.Rows[0]["IsConfirm"]) != 1)
                        {
                            Result[0] = false;
                            Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.وضعیت پروانه اشتغال عضو نا مشخص می باشد.";
                            return Result;
                        }
                        #region Comment
                        //?????????????چک شود آیا در گردش کار این شرایط وجود دارد یا خیر
                        //if (_Dprt == "Document")
                        //{
                        //    Boolean HasDes = true;
                        //    Boolean HasObs = true;
                        //    DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
                        //    if (dtMeDetail.Rows.Count == 0)
                        //    {
                        //        HasDes = false;
                        //        //"امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت طراحی ندارد  ";
                        //    }
                        //    DataTable dtMeDetail2 = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
                        //    if (dtMeDetail2.Rows.Count == 0)
                        //    {
                        //        HasObs = false;
                        //        //"امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت نظارت ندارد  ";
                        //    }
                        //    if (HasObs == false && HasDes == false)
                        //    {
                        //        Result[0] = false;
                        //        Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر بایستی حداقل یکی از صلاحیت های نظارت یا طراحی را دارا باشد.";
                        //        return Result;
                        //    }
                        //}
                        ////else if (OffType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//ejra
                        ////{
                        ////    DataTable dtMeDetail3 = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
                        ////    if (dtMeDetail3.Rows.Count == 0)
                        ////    {
                        ////        this.DivReport.Visible = true;
                        ////        this.LabelWarning.Text = "امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت اجرا ندارد  ";
                        ////        return false;
                        ////    }
                        ////}
                        #endregion
                        if (!string.IsNullOrEmpty(ExpireDate))
                        {
                            if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                            {
                                Result[0] = false;
                                Result[1] = "امکان ثبت عضو مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.";
                                return Result;
                            }
                        }
                    }
                }
            }

            if (MemberFileId == -1)
            {
                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
                if (dtMeFile.Rows.Count == 1)
                    Result[2] = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            }
            #endregion
            return Result;
        }
    }
}
