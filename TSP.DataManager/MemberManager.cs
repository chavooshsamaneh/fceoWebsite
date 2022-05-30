using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class MemberManager : BaseObject
    {

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.MemberDataSet.tblMemberDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        #region Permissions Methods
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Member);
        }
        public static Permission GetUserPermissionForSearchMember(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberSearch);
        }
        public static Permission GetUserPermissionForPrintMeEnvelope(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PrintMeEnvelope);
        }
        public static Permission GetUserPermissionForPrintMemberSearch(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PrintMemberSearch);
        }
        public static Permission GetUserPermissionForPrintTempMemberCardPrint(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TempMemberCardPrint);
        }
        public static Permission GetUserPermissionForExportExcelMemberSearch(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ExportExcelMemberSearch);
        }
        public static Permission GetUserPermissionForExportExcelMembers(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ExportExcelMembers);
        }
        public static Permission GetUserPermissionForMemberCardRequest(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberCardRequestPrint);
        }
        public static Permission GetUserPermissionForReportMemberCardRequest(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ReportMemberCardRequest);
        }
        public static Permission GetUserPermissionForOfficeDocumentPrintReport(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeDocumentPrintReport);
        }
        public static Permission GetUserPermissionForEngOfficeDocumentPrintReport(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EngOfficeDocumentPrintReport);
        }
        public static Permission GetUserPermissionForMemberLicenceInqueryButton(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberLicenceInqueryPrint);
        }
        public static Permission GetUserPermissionForMemberLicenceInqueryForm(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ReportMemberLicenceInquery);
        }
        public static Permission GetUserPermissionForChangeAgent(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ChangeAgentRequest);
        }
        public static Permission GetUserPermissionForMemberInfoReport(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberInfoReport);
        }

        public static Permission GetUserPermissionForChangeBankAccNo(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ChangeBankAccNoRequest);
        }

        public static Permission GetUserPermissionForReportDocMemberBySparateMajor(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ReportDocMemberSeparateByMajor);
        }
        public static Permission GetUserPermissionForCancelMembership(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.CancelMembership);
        }
        #endregion

        protected override void InitAdapter()
        {

            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblMember";
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("FirstName", "FirstName");
            tableMapping.ColumnMappings.Add("LastName", "LastName");
            tableMapping.ColumnMappings.Add("FirstNameEn", "FirstNameEn");
            tableMapping.ColumnMappings.Add("LastNameEn", "LastNameEn");
            tableMapping.ColumnMappings.Add("TiId", "TiId");
            tableMapping.ColumnMappings.Add("FatherName", "FatherName");
            tableMapping.ColumnMappings.Add("BirhtDate", "BirhtDate");
            tableMapping.ColumnMappings.Add("BirthPlace", "BirthPlace");
            tableMapping.ColumnMappings.Add("IdNo", "IdNo");
            tableMapping.ColumnMappings.Add("IssuePlace", "IssuePlace");
            tableMapping.ColumnMappings.Add("SSN", "SSN");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("HomeAdr", "HomeAdr");
            tableMapping.ColumnMappings.Add("HomeTel", "HomeTel");
            tableMapping.ColumnMappings.Add("HomePO", "HomePO");
            tableMapping.ColumnMappings.Add("WorkAdr", "WorkAdr");
            tableMapping.ColumnMappings.Add("WorkTel", "WorkTel");
            tableMapping.ColumnMappings.Add("WorkPO", "WorkPO");
            tableMapping.ColumnMappings.Add("FaxNo", "FaxNo");
            tableMapping.ColumnMappings.Add("BankAccNo", "BankAccNo");
            tableMapping.ColumnMappings.Add("AccId", "AccId");
            tableMapping.ColumnMappings.Add("LoanPayAccId", "LoanPayAccId");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("SoId", "SoId");
            tableMapping.ColumnMappings.Add("MsId", "MsId");
            tableMapping.ColumnMappings.Add("MrsId", "MrsId");
            tableMapping.ColumnMappings.Add("SexId", "SexId");
            tableMapping.ColumnMappings.Add("MarId", "MarId");
            tableMapping.ColumnMappings.Add("MeNo", "MeNo");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("FileDate", "FileDate");
            tableMapping.ColumnMappings.Add("ImpId", "ImpId");
            tableMapping.ColumnMappings.Add("ObsId", "ObsId");
            tableMapping.ColumnMappings.Add("DesId", "DesId");
            tableMapping.ColumnMappings.Add("RelId", "RelId");
            tableMapping.ColumnMappings.Add("ComId", "ComId");
            tableMapping.ColumnMappings.Add("AtId", "AtId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("Nationality", "Nationality");
            tableMapping.ColumnMappings.Add("Website", "Website");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Image", "Image");
            tableMapping.ColumnMappings.Add("ImageUrl", "ImageUrl");
            tableMapping.ColumnMappings.Add("SignUrl", "SignUrl");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsLock", "IsLock");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("UserInfoType", "UserInfoType");
            tableMapping.ColumnMappings.Add("MembershipDate", "MembershipDate");
            tableMapping.ColumnMappings.Add("RecieveMagazine", "RecieveMagazine");
            tableMapping.ColumnMappings.Add("UrbanismId", "UrbanismId");
            tableMapping.ColumnMappings.Add("TrafficId", "TrafficId");
            tableMapping.ColumnMappings.Add("MappingId", "MappingId");
            tableMapping.ColumnMappings.Add("MasterMlId", "MasterMlId");
            tableMapping.ColumnMappings.Add("MasterMFMjId", "MasterMFMjId");
            tableMapping.ColumnMappings.Add("ArchitectorCode", "ArchitectorCode");
            tableMapping.ColumnMappings.Add("TMeId", "TMeId");
            tableMapping.ColumnMappings.Add("GasId", "GasId");
            tableMapping.ColumnMappings.Add("MilitaryCommitment", "MilitaryCommitment");
            tableMapping.ColumnMappings.Add("NezamKardanConfirmURL", "NezamKardanConfirmURL");            
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectMember";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 30);
            this.Adapter.SelectCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50);
            this.Adapter.SelectCommand.Parameters.Add("@IdNo", System.Data.SqlDbType.VarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar, 10);
            this.Adapter.SelectCommand.Parameters.Add("@MobileNo", System.Data.SqlDbType.VarChar, 20);
            this.Adapter.SelectCommand.Parameters.Add("@SoId", System.Data.SqlDbType.SmallInt, 2);
            this.Adapter.SelectCommand.Parameters.Add("@MarId", System.Data.SqlDbType.SmallInt, 2);
            //this.Adapter.SelectCommand.Parameters.Add("@GrId", System.Data.SqlDbType.Int);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteMember";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.InsertMember";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GasId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GasId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TMeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TMeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MasterMlId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MasterMlId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MasterMFMjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MasterMFMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UrbanismId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UrbanismId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TrafficId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TrafficId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MappingId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MappingId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TiId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TiId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FatherName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FatherName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirhtDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "BirhtDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IdNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IssuePlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IssuePlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SSN", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomeAdr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "HomeAdr", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomeTel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "HomeTel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomePO", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "HomePO", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkAdr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkAdr", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkTel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkTel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkPO", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkPO", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FaxNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FaxNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BankAccNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BankAccNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LoanPayAccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LoanPayAccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SoId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SoId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MrsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SexId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SexId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MarId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MarId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MeNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "FileDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ImpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ObsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ObsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DesId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DesId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RelId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RelId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ComId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AtId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "AtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Nationality", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Nationality", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Website", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Website", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Image", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "Image", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImageUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImageUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserInfoType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "UserInfoType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieveMagazine", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RecieveMagazine", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MembershipDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "MembershipDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArchitectorCode", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ArchitectorCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MilitaryCommitment", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "MilitaryCommitment", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NezamKardanConfirmURL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "NezamKardanConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.UpdateMember";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GasId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GasId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TMeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TMeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UrbanismId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UrbanismId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TrafficId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TrafficId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MappingId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MappingId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecieveMagazine", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RecieveMagazine", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MembershipDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "MembershipDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserInfoType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "UserInfoType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TiId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TiId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FatherName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FatherName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirhtDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "BirhtDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IdNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IssuePlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IssuePlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SSN", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomeAdr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "HomeAdr", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomeTel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "HomeTel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomePO", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "HomePO", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkAdr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkAdr", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkTel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkTel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkPO", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkPO", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FaxNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FaxNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BankAccNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BankAccNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LoanPayAccId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LoanPayAccId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SoId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SoId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MrsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SexId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SexId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MarId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MarId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MeNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "FileDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImpId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ImpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ObsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ObsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DesId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DesId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RelId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RelId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ComId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AtId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "AtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Nationality", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Nationality", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Website", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Website", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Image", System.Data.SqlDbType.Image, 0, System.Data.ParameterDirection.Input, 0, 0, "Image", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImageUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImageUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsLock", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsLock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MasterMlId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MasterMlId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MasterMFMjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MasterMFMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArchitectorCode", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ArchitectorCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MilitaryCommitment", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "MilitaryCommitment", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NezamKardanConfirmURL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "NezamKardanConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public void FindByCode(int MeId)
        {
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            Fill();
        }

        public static void UpdateMeNo(int Meid, TransactionManager trans)
        {
            SqlConnection scon = null;
            //new SqlConnection(DBManager.CnnStr);
            SqlCommand scom = new SqlCommand("CreateMemberMeNo", scon);
            scom.CommandType = CommandType.StoredProcedure;
            if (trans != null)
            {
                scom.Transaction = trans.Transaction;
                scon = trans.Transaction.Connection;

            }
            else
                scon = new SqlConnection(DBManager.CnnStr);
            scom.Connection = scon;
            scom.Parameters.Add("@MeId", SqlDbType.Int);
            scom.Parameters[0].Value = Meid;
            if (scon.State != ConnectionState.Open)
                scon.Open();
            scom.ExecuteNonQuery();
            if (trans == null)
                scon.Close();
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchMembers(string FirstName, string LastName, string IdNo, string SSN, string MobileNo, short SoId, short MarId)
        {
            if (string.IsNullOrEmpty(FirstName))
                FirstName = "%";
            this.Adapter.SelectCommand.Parameters["@FirstName"].Value = FirstName;

            if (string.IsNullOrEmpty(LastName))
                LastName = "%";
            this.Adapter.SelectCommand.Parameters["@LastName"].Value = LastName;

            if (string.IsNullOrEmpty(IdNo))
                IdNo = "%";
            this.Adapter.SelectCommand.Parameters["@IdNo"].Value = IdNo;

            if (string.IsNullOrEmpty(SSN))
                SSN = "%";
            this.Adapter.SelectCommand.Parameters["@SSN"].Value = SSN;

            if (string.IsNullOrEmpty(MobileNo))
                MobileNo = "%";
            this.Adapter.SelectCommand.Parameters["@MobileNo"].Value = MobileNo;

            if (string.IsNullOrEmpty(SoId.ToString()))
                SoId = -1;
            this.Adapter.SelectCommand.Parameters["@SoId"].Value = SoId;

            if (string.IsNullOrEmpty(MarId.ToString()))
                MarId = -1;
            this.Adapter.SelectCommand.Parameters["@MarId"].Value = MarId;

            Fill();
            return DataTable;
        }

        #region SearchMemberByMjId
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SearchMemberByMjId(int MeId, string FirstName, string LastName, string MobileNo, short MjId, string FileNo, string DateFrom, string DateTo, int IsConfirm
            , int IsMemberSearch, string FollowCode)
        {
            DataTable dt = new DataManager.MemberDataSet.tblMemberDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSearchMemberByMjId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.Add("@TableType", System.Data.SqlDbType.Int).Value = (int)TableCodes.MemberRequest;
            adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50);
            adapter.SelectCommand.Parameters.Add("@MobileNo", System.Data.SqlDbType.VarChar, 20);
            adapter.SelectCommand.Parameters.Add("@MjId", System.Data.SqlDbType.SmallInt);
            adapter.SelectCommand.Parameters.Add("@FileNo", System.Data.SqlDbType.VarChar);
            adapter.SelectCommand.Parameters.Add("@DateFrom", System.Data.SqlDbType.Char);
            adapter.SelectCommand.Parameters.Add("@DateTo", System.Data.SqlDbType.Char);
            adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@IsMemberSearch", System.Data.SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@FollowCode", System.Data.SqlDbType.NVarChar);



            //if (MeId <= 0)
            //    MeId = -1;
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            if (string.IsNullOrEmpty(FirstName))
                FirstName = "%";
            adapter.SelectCommand.Parameters["@FirstName"].Value = FirstName;
            if (string.IsNullOrEmpty(LastName))
                LastName = "%";
            adapter.SelectCommand.Parameters["@LastName"].Value = LastName;
            if (string.IsNullOrEmpty(MobileNo))
                MobileNo = "%";
            adapter.SelectCommand.Parameters["@MobileNo"].Value = MobileNo;
            if (MjId <= 0)
                MjId = -1;
            adapter.SelectCommand.Parameters["@MjId"].Value = MjId;
            if (string.IsNullOrEmpty(FileNo))
                FileNo = "%";
            adapter.SelectCommand.Parameters["@FileNo"].Value = FileNo;
            if (string.IsNullOrEmpty(DateFrom))
                DateFrom = "1";
            adapter.SelectCommand.Parameters["@DateFrom"].Value = DateFrom;
            if (string.IsNullOrEmpty(DateTo))
                DateTo = "3";
            adapter.SelectCommand.Parameters["@DateTo"].Value = DateTo;
            adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            adapter.SelectCommand.Parameters["@IsMemberSearch"].Value = @IsMemberSearch;

            if (string.IsNullOrEmpty(FollowCode))
                FollowCode = "%";
            adapter.SelectCommand.Parameters["@FollowCode"].Value = FollowCode;


            if (MeId != -2)
                adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SearchMemberByMjId(int MeId, string FirstName, string LastName, string MobileNo, short MjId, string FileNo, string DateFrom, string DateTo, int IsConfirm)
        {
            return SearchMemberByMjId(MeId, FirstName, LastName, MobileNo, MjId, FileNo, DateFrom, DateTo, IsConfirm, -1, "%");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SearchMemberByMjId(int MeId, string FirstName, string LastName, string MobileNo, short MjId, string FileNo, string DateFrom, string DateTo, int IsConfirm, int IsMemberSearch)
        {
            return SearchMemberByMjId(MeId, FirstName, LastName, MobileNo, MjId, FileNo, DateFrom, DateTo, IsConfirm, IsMemberSearch, "%");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SearchMemberByMjId(int MeId, string FirstName, string LastName, string MobileNo, short MjId, string FileNo, string DateFrom, string DateTo, int IsConfirm, string FollowCode)
        {
            return SearchMemberByMjId(MeId, FirstName, LastName, MobileNo, MjId, FileNo, DateFrom, DateTo, IsConfirm, -1, FollowCode);
        }
        #endregion

        //Use In: MembersInfo/Members
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SearchMemberForPublic(int MeId, string FirstName, string LastName, Int32 MjId, string MeNo, Int16 HasImpDoc)
        {
            if (MeId == -1 && MjId == -1 && LastName == "%" && FirstName == "%" && MeNo == "%" && HasImpDoc == -1)
                return new DataTable();
            DataTable dt = new DataTable(); //DataManager.MemberDataSet.tblMemberDataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("spSearchMemberForPublic", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.SelectCommand.Parameters.AddWithValue("@MjId", MjId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeNo", MeNo);
            adapter.SelectCommand.Parameters.AddWithValue("@HasImpDoc", HasImpDoc);
            adapter.Fill(dt);
            return (dt);
        }


        public System.Data.DataTable SelectMemberInfoForWorkRequest(int MeId)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SelectMemberInfoForWorkRequest", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

           adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.Fill(dt);
            return (dt);
        }

        public System.Data.DataTable SearchMemberByMobileNo(string MobileNo)
        {
            string NewMobileNo = MobileNo.Substring(MobileNo.Length - 10);
            //return SearchMemberForPublic(-1, "%", "%", NewMobileNo, -1, "%", "1", "3", -1, -1);
            DataTable dt = new DataTable();// DataManager.MemberDataSet.tblMemberDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberByMobileNo", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MobileNo", NewMobileNo);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindMembers1(int GrId, int MeId, string FName, string LName)   //Find Members of a special group from tblGroupDetail
        {
            DataTable dt = new DataManager.MemberDataSet.tblMemberDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberByGroup3", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@GrId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@FName", SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LName", SqlDbType.NVarChar, 50);


            if (string.IsNullOrEmpty(GrId.ToString()))
                GrId = -1;
            adapter.SelectCommand.Parameters["@GrId"].Value = GrId;

            if (string.IsNullOrEmpty(MeId.ToString()))
                MeId = -1;
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            if (string.IsNullOrEmpty(FName))
                FName = "%";
            adapter.SelectCommand.Parameters["@FName"].Value = FName;

            if (string.IsNullOrEmpty(LName))
                LName = "%";
            adapter.SelectCommand.Parameters["@LName"].Value = LName;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindMembers(int GrId, int MeId, string FName, string LName)   //Select All Members of a special group from tblGroupDetail or All Member
        {
            DataTable dt = new DataManager.MemberDataSet.tblMemberDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberByGroup", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@GrId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@FName", SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LName", SqlDbType.NVarChar, 50);


            if (string.IsNullOrEmpty(GrId.ToString()))
                GrId = -1;
            adapter.SelectCommand.Parameters["@GrId"].Value = GrId;

            if (string.IsNullOrEmpty(MeId.ToString()))
                MeId = -1;
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            if (string.IsNullOrEmpty(FName))
                FName = "%";
            adapter.SelectCommand.Parameters["@FName"].Value = FName;

            if (string.IsNullOrEmpty(LName))
                LName = "%";
            adapter.SelectCommand.Parameters["@LName"].Value = LName;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindAllMembers(int GrId, int MeId, string FName, string LName)    //Show special Members of tblMember
        {
            DataTable dt = new DataManager.MemberDataSet.tblMemberDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberByGroup2", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@GrId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@FName", SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LName", SqlDbType.NVarChar, 50);


            if (string.IsNullOrEmpty(GrId.ToString()))
                GrId = -1;

            if (string.IsNullOrEmpty(GrId.ToString()))
                GrId = -1;
            adapter.SelectCommand.Parameters["@GrId"].Value = GrId;

            if (string.IsNullOrEmpty(MeId.ToString()))
                MeId = -1;
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            if (string.IsNullOrEmpty(FName))
                FName = "%";
            adapter.SelectCommand.Parameters["@FName"].Value = FName;

            if (string.IsNullOrEmpty(LName))
                LName = "%";
            adapter.SelectCommand.Parameters["@LName"].Value = LName;


            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindMembersByMjId(int MeId, int MjId)    //Show special Members of tblMember
        {
            DataTable dt = new DataManager.MemberDataSet.tblMemberDataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectMemberByMjId", this.Connection);

            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            ad.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int);

            if (string.IsNullOrEmpty(MeId.ToString()))
                MeId = -1;
            ad.SelectCommand.Parameters["@MeId"].Value = MeId;

            if (string.IsNullOrEmpty(MjId.ToString()))
                MjId = -1;
            ad.SelectCommand.Parameters["@MjId"].Value = MjId;
            ad.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectUniqueName()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberUniqueName", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectUniqueFamily(string FirstName, string LastName)
        {
            if (string.IsNullOrEmpty(FirstName))
                FirstName = "%";
            if (string.IsNullOrEmpty(LastName))
                LastName = "%";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberUniqueFamily", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.Fill(dt);
            return (dt);



        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberFirstName(string FirstName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberFirstName", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (string.IsNullOrEmpty(FirstName))
                FirstName = "%";
            adapter.SelectCommand.Parameters["@FirstName"].Value = FirstName;

            adapter.Fill(dt);
            return (dt);



        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberByName(string FirstName, string LastName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberByName", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            if (string.IsNullOrEmpty(FirstName))
                FirstName = "%";
            adapter.SelectCommand.Parameters["@FirstName"].Value = FirstName;

            if (string.IsNullOrEmpty(LastName))
                LastName = "%";
            adapter.SelectCommand.Parameters["@LastName"].Value = LastName;
            adapter.Fill(dt);
            return (dt);



        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberByNameCode(string FirstName, string LastName, int Code)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberByNameCode", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4);
            if (string.IsNullOrEmpty(FirstName))
                FirstName = "%";
            adapter.SelectCommand.Parameters["@FirstName"].Value = FirstName;

            if (string.IsNullOrEmpty(LastName))
                LastName = "%";
            adapter.SelectCommand.Parameters["@LastName"].Value = LastName;

            if (Code == 0)
                Code = -1;
            adapter.SelectCommand.Parameters["@MeId"].Value = Code;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMembersForSms()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberForSMS", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 10);
            adapter.SelectCommand.Parameters.Add("@IdNo", SqlDbType.NChar, 10);
            adapter.SelectCommand.Parameters.Add("@SSN", SqlDbType.NChar, 10);
            adapter.SelectCommand.Parameters.Add("@MobileNo", SqlDbType.NVarChar, 20);
            adapter.SelectCommand.Parameters.Add("@SoId", SqlDbType.SmallInt, 4);
            adapter.SelectCommand.Parameters.Add("@MarId", SqlDbType.SmallInt, 4);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMembersName()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberName", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMembersName(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberName", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberForSearch(int MeId, int MjId, String FirstName, String LastName, int GrId)
        {
            DataTable dt = new DataTable();
            if (MeId == -2)
                return dt;

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberForSearch", this.Connection);

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (MeId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            if (MjId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@MjId", MjId);
            if (FirstName != null && String.IsNullOrEmpty(FirstName.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            if (LastName != null && String.IsNullOrEmpty(LastName.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            if (GrId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@GrId", GrId);

            adapter.Fill(dt);
            return (dt);
        }

        #region SelectMemberForSearchByExec
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberForSearchByExec(int MeId, string MjParam, string FirstName, string LastName, string GrParam, int ComId)
        {
            return SelectMemberForSearchByExec(-1, MeId, MjParam, FirstName, LastName, Convert.ToInt32(GrParam), ComId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", -1, "");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberForSearchByExec(int MeIdFrom, int MeIdTo, string MjParam, string FirstName, string LastName, string GrParam, int ComId,
            String CreateDateFrom, String CreateDateTo, String FileDateFrom, String FileDateTo)
        {
            return SelectMemberForSearchByExec(MeIdFrom, MeIdTo, MjParam, FirstName, LastName, Convert.ToInt32(GrParam), ComId, CreateDateFrom, CreateDateTo, FileDateFrom, FileDateTo, "", "", "", "", "", "", "", "", "", "", "", "", "", -1, "");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberForSearchByExec(int MeIdFrom, int MeIdTo, string MjParam, string FirstName, string LastName, string GrParam, int ComId,
            String CreateDateFrom, String CreateDateTo, String FileDateFrom, String FileDateTo, String MembershipDateFrom, String MembershipDateTo,
             String BirthDateFrom, String BirthDateTo, String LicenseParam, String RegistrationStatusParam, String AgentParam, String DocGradeParam,
            String FirstDocRegDateFrom, String FirstDocRegDateTo, String RevivalDocRegDateFrom, String RevivalDocRegDateTo, String FileMjIdParam, int LicenseInquiryStatus)
        {
            return SelectMemberForSearchByExec(MeIdFrom, MeIdTo, MjParam, FirstName, LastName,Convert.ToInt32( GrParam), ComId, CreateDateFrom, CreateDateTo, FileDateFrom, FileDateTo, MembershipDateFrom, MembershipDateTo, BirthDateFrom, BirthDateTo, LicenseParam, RegistrationStatusParam, AgentParam, DocGradeParam, FirstDocRegDateFrom, FirstDocRegDateTo, RevivalDocRegDateFrom, RevivalDocRegDateTo, FileMjIdParam, LicenseInquiryStatus, "");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberForSearchByExec(int MeIdFrom, int MeIdTo, string MjParam, string FirstName, string LastName, int GrParam, int ComId,
            String CreateDateFrom, String CreateDateTo, String FileDateFrom, String FileDateTo, String MembershipDateFrom, String MembershipDateTo,
             String BirthDateFrom, String BirthDateTo, String LicenseParam, String RegistrationStatusParam, String AgentParam, String DocGradeParam,
            String FirstDocRegDateFrom, String FirstDocRegDateTo, String RevivalDocRegDateFrom, String RevivalDocRegDateTo, String FileMjIdParam, int LicenseInquiryStatus, string FileMjParentIdParam)
        {
            DataTable dt = new DataTable();
            if (MeIdFrom == -2)
                return dt;

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberForSearchByExec", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //if(!String.IsNullOrEmpty(MeIdFrom))
            adapter.SelectCommand.Parameters.AddWithValue("@MeIdFrom", MeIdFrom);
            //if(!String.IsNullOrEmpty(MeIdTo))
            adapter.SelectCommand.Parameters.AddWithValue("@MeIdTo", MeIdTo);
           // if (!string.IsNullOrEmpty(MjParam))
                adapter.SelectCommand.Parameters.AddWithValue("@MjParam", MjParam);
           // if (FirstName != null && String.IsNullOrEmpty(FirstName.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
          //  if (LastName != null && String.IsNullOrEmpty(LastName.Trim()) == false)
                adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            if (GrParam!=-1)
                adapter.SelectCommand.Parameters.AddWithValue("@GrParam", GrParam);
            if (ComId != -1)
                adapter.SelectCommand.Parameters.AddWithValue("@ComId", ComId);
            if (!String.IsNullOrEmpty(CreateDateFrom))
                adapter.SelectCommand.Parameters.AddWithValue("@CreateDateFrom", CreateDateFrom);
            else
                adapter.SelectCommand.Parameters.Add("@CreateDateFrom", SqlDbType.VarChar).Value = "1";
            if (!String.IsNullOrEmpty(CreateDateTo))
                adapter.SelectCommand.Parameters.AddWithValue("@CreateDateTo", CreateDateTo);
            if (!String.IsNullOrEmpty(FileDateFrom))
                adapter.SelectCommand.Parameters.AddWithValue("@FileDateFrom", FileDateFrom);
            if (!String.IsNullOrEmpty(FileDateTo))
                adapter.SelectCommand.Parameters.AddWithValue("@FileDateTo", FileDateTo);

            if (!String.IsNullOrEmpty(MembershipDateTo))
                adapter.SelectCommand.Parameters.AddWithValue("@MembershipDateTo", MembershipDateTo);

            if (!String.IsNullOrEmpty(MembershipDateFrom))
                adapter.SelectCommand.Parameters.AddWithValue("@MembershipDateFrom", MembershipDateFrom);

            if (!String.IsNullOrEmpty(BirthDateFrom))
                adapter.SelectCommand.Parameters.AddWithValue("@BirthDateFrom", BirthDateFrom);
            if (!String.IsNullOrEmpty(BirthDateTo))
                adapter.SelectCommand.Parameters.AddWithValue("@BirthDateTo", BirthDateTo);

            if (!string.IsNullOrEmpty(LicenseParam))
                adapter.SelectCommand.Parameters.AddWithValue("@LicenseParam", LicenseParam);
            if (!string.IsNullOrEmpty(RegistrationStatusParam))
                adapter.SelectCommand.Parameters.AddWithValue("@RegistrationStatusParam", RegistrationStatusParam);
            if (!string.IsNullOrEmpty(AgentParam))
                adapter.SelectCommand.Parameters.AddWithValue("@AgentParam", AgentParam);
            if (!string.IsNullOrEmpty(DocGradeParam))
                adapter.SelectCommand.Parameters.AddWithValue("@DocGradeParam", DocGradeParam);

            if (!String.IsNullOrEmpty(FirstDocRegDateFrom))
                adapter.SelectCommand.Parameters.AddWithValue("@FirstDocRegDateFrom", FirstDocRegDateFrom);
            if (!String.IsNullOrEmpty(FirstDocRegDateTo))
                adapter.SelectCommand.Parameters.AddWithValue("@FirstDocRegDateTo", FirstDocRegDateTo);

            if (!String.IsNullOrEmpty(RevivalDocRegDateFrom))
                adapter.SelectCommand.Parameters.AddWithValue("@RevivalDocRegDateFrom", RevivalDocRegDateFrom);
            if (!String.IsNullOrEmpty(RevivalDocRegDateTo))
                adapter.SelectCommand.Parameters.AddWithValue("@RevivalDocRegDateTo", RevivalDocRegDateTo);

            if (!string.IsNullOrEmpty(FileMjIdParam))
                adapter.SelectCommand.Parameters.AddWithValue("@FileMjIdParam", FileMjIdParam);

            if (!string.IsNullOrEmpty(FileMjParentIdParam))
                adapter.SelectCommand.Parameters.AddWithValue("@FileMjParentIdParam", FileMjParentIdParam);

            adapter.SelectCommand.Parameters.AddWithValue("@LicenseInquiryStatus", LicenseInquiryStatus);
            //  adapter.SelectCommand.Parameters.AddWithValue("@TableTypeDocMeMajor", TableTypeManager.FindTtId(TableType.DocMemberFileMajor));

            adapter.Fill(dt);
            return (dt);
        }

        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SelectMembersAndTemporaryMembers(int MeId, string FirstName, string LastName, string MobileNo, int MjId, string FileNo, string DateFrom, string DateTo, int IsConfirm
            , string FollowCode, int Requester)
        {
            DataTable dt = new DataTable(); //DataManager.MemberDataSet.tblMemberDataTable();
            dt.Columns.Add("Id");
            dt.Columns["Id"].AutoIncrement = true;
            dt.Columns["Id"].AutoIncrementSeed = 1;
            dt.Constraints.Add("PK_ID", dt.Columns["Id"], true);
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMembersAndTemporaryMembers", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", System.Data.SqlDbType.Int).Value = (int)TableCodes.MemberRequest;
            adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 30);
            adapter.SelectCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50);
            adapter.SelectCommand.Parameters.Add("@MobileNo", System.Data.SqlDbType.VarChar, 20);
            adapter.SelectCommand.Parameters.Add("@MjId", System.Data.SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@FileNo", System.Data.SqlDbType.VarChar);
            adapter.SelectCommand.Parameters.Add("@DateFrom", System.Data.SqlDbType.Char);
            adapter.SelectCommand.Parameters.Add("@DateTo", System.Data.SqlDbType.Char);
            adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.Int);
            adapter.SelectCommand.Parameters.Add("@FollowCode", System.Data.SqlDbType.NVarChar);
            adapter.SelectCommand.Parameters.Add("@Requester", System.Data.SqlDbType.Int);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandTimeout = 60;
            //if (MeId <= 0)
            //    MeId = -1;
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            if (string.IsNullOrEmpty(FirstName))
                FirstName = "%";
            adapter.SelectCommand.Parameters["@FirstName"].Value = FirstName;
            if (string.IsNullOrEmpty(LastName))
                LastName = "%";
            adapter.SelectCommand.Parameters["@LastName"].Value = LastName;
            if (string.IsNullOrEmpty(MobileNo))
                MobileNo = "%";
            adapter.SelectCommand.Parameters["@MobileNo"].Value = MobileNo;
            if (MjId <= 0)
                MjId = -1;
            adapter.SelectCommand.Parameters["@MjId"].Value = MjId;
            if (string.IsNullOrEmpty(FileNo))
                FileNo = "%";
            adapter.SelectCommand.Parameters["@FileNo"].Value = FileNo;
            if (string.IsNullOrEmpty(DateFrom))
                DateFrom = "1";
            adapter.SelectCommand.Parameters["@DateFrom"].Value = DateFrom;
            if (string.IsNullOrEmpty(DateTo))
                DateTo = "3";
            adapter.SelectCommand.Parameters["@DateTo"].Value = DateTo;
            adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;

            if (string.IsNullOrEmpty(FollowCode))
                FollowCode = "%";
            adapter.SelectCommand.Parameters["@FollowCode"].Value = FollowCode;

            if (Requester <= 0)
                Requester = -1;
            adapter.SelectCommand.Parameters["@Requester"].Value = Requester;

            if (MeId != -2)
                adapter.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SelectMemberForManagmentPage(int MeId, string FirstName, string LastName, string MobileNo, int MjId, string FileNo, string DateFrom, string DateTo, int IsConfirm
            , string FollowCode, int Requester, int AgentId, string ReqCreateDateFrom, string ReqCreateDateTo, int TaskId,string WFDate, string WFDateTo)
        {
            if (MeId == -1 && FirstName == "%" && LastName == "%" && MobileNo == "%" && MjId == -1 && FileNo == "%" && DateFrom == "1" && DateTo == "3"
                && ReqCreateDateFrom == "1" && ReqCreateDateTo == "3" && WFDate== "1" && WFDateTo=="2"
                && IsConfirm == -1 && FollowCode == "%" && Requester == -1 && TaskId == -1)
                return new System.Data.DataTable();

            DataTable dt = new DataTable(); //new DataManager.MemberDataSet.tblMemberDataTable();// new DataTable(); //DataManager.MemberDataSet.tblMemberDataTable();
            //dt.Columns.Add("Id");
            //dt.Columns["Id"].AutoIncrement = true;
            //dt.Columns["Id"].AutoIncrementSeed = 1;
            //dt.Constraints.Add("PK_ID", dt.Columns["Id"], true);
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", (int)TableCodes.MemberRequest);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.SelectCommand.Parameters.AddWithValue("@MobileNo", MobileNo);
            adapter.SelectCommand.Parameters.AddWithValue("@MjId", MjId);
            adapter.SelectCommand.Parameters.AddWithValue("@FileNo", FileNo);
            adapter.SelectCommand.Parameters.AddWithValue("@DateFrom", DateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@DateTo", DateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", IsConfirm);
            adapter.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);
            adapter.SelectCommand.Parameters.AddWithValue("@Requester", Requester);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@ReqCreateDateFrom", ReqCreateDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@ReqCreateDateTo", ReqCreateDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@WFDate", WFDate);
            adapter.SelectCommand.Parameters.AddWithValue("@WFDateTo", WFDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowId", 22);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandTimeout = 0;
            #region Comment
            //if (MeId <= 0)
            //    MeId = -1;
            //adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            //if (string.IsNullOrEmpty(FirstName))
            //    FirstName = "%";
            //adapter.SelectCommand.Parameters["@FirstName"].Value = FirstName;
            //if (string.IsNullOrEmpty(LastName))
            //    LastName = "%";
            //adapter.SelectCommand.Parameters["@LastName"].Value = LastName;
            //if (string.IsNullOrEmpty(MobileNo))
            //    MobileNo = "%";
            //adapter.SelectCommand.Parameters["@MobileNo"].Value = MobileNo;
            //if (MjId <= 0)
            //    MjId = -1;
            //adapter.SelectCommand.Parameters["@MjId"].Value = MjId;
            //if (string.IsNullOrEmpty(FileNo))
            //    FileNo = "%";
            //adapter.SelectCommand.Parameters["@FileNo"].Value = FileNo;
            //if (string.IsNullOrEmpty(DateFrom))
            //    DateFrom = "1";
            //adapter.SelectCommand.Parameters["@DateFrom"].Value = DateFrom;
            //if (string.IsNullOrEmpty(DateTo))
            //    DateTo = "3";
            //adapter.SelectCommand.Parameters["@DateTo"].Value = DateTo;
            //adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;

            //if (string.IsNullOrEmpty(FollowCode))
            //    FollowCode = "%";
            //adapter.SelectCommand.Parameters["@FollowCode"].Value = FollowCode;

            //if (Requester <= 0)
            //    Requester = -1;
            //adapter.SelectCommand.Parameters["@Requester"].Value = Requester;

            //if (MeId != -2)
            #endregion
            adapter.Fill(dt);
            return (dt);

        }

        public String SelectMemberWithoutMobileNoForSMS(String MeIds)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberWithoutMobileNoForSMS", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeIds", MeIds);

            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["MeIds"].ToString();
            return "";
        }

        public String SelectInvalidMemberNo(String MeIds)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SelectInvalidMemberNo", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeIds", MeIds);

            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["MeIds"].ToString();
            return "";
        }

        public DataTable SelectMemberForEnvelopePrint(String MeIdParameters)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberForEnvelopePrint", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeIdParameters", MeIdParameters);

            adapter.Fill(dt);

            return dt;
        }

        public int GetUserInfoType(int MeId)
        {
            this.FindByCode(MeId);
            if (this.Count == 0)
                return -1;
            if (Utility.IsDBNullOrNullValue(this[0]["UserInfoType"]))
                return -1;
            return (Convert.ToInt32(this[0]["UserInfoType"]));
        }

        public int GetRecieveMagazineType(int MeId)
        {
            this.FindByCode(MeId);
            if (this.Count == 0)
                return -1;
            if (Utility.IsDBNullOrNullValue(this[0]["RecieveMagazine"]))
                return -1;
            return (Convert.ToInt32(this[0]["RecieveMagazine"]));
        }

        public DataTable SelectActiveMembers(int MeId, string SSN, string FirstName, string LastName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectActiveMembers", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@SSN", SSN);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable SelectActiveMembers(int MeId, string SSN)
        {
            return SelectActiveMembers(MeId, SSN, "%", "%");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberForAgent(int MeId, int AgentId, string FirstName, string LastName)
        {
            if (MeId == -1 && AgentId == -1 && FirstName == "%" && LastName == "%")
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberForAgent", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberForBanckAccNo(int MeId, string BankAccNo, string FirstName, string LastName,int AgentId)
        {
            if (MeId == -1 && BankAccNo == "%" && FirstName == "%" && LastName == "%")
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberForBankAccNo", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@BankAccNo", BankAccNo);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);            
            adapter.Fill(dt);
            return dt;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable spSelectMemberForDebtor(int MeId, string BankAccNo, string FirstName, string LastName)
        {
            if (MeId == -1 && BankAccNo == "%" && FirstName == "%" && LastName == "%")
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberForDebtor", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@BankAccNo", BankAccNo);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.Fill(dt);
            return dt;
        }


        #region SelectMemberRequestForChangInfoReq
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberRequestForAgent(int MeId)
        {
            return SelectMemberRequestForChangInfoReq(MeId, MemberRequestType.AgentChange);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberRequestForBankAcc(int MeId)
        {
            return SelectMemberRequestForChangInfoReq(MeId, MemberRequestType.BankAccNoChange);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberRequestForCancelMembership(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberRequestForCancelMembership", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.AddWithValue("@ReqType", ((int)MemberRequestType.ActivateDebtorMember).ToString() + "," + ((int)MemberRequestType.CancelDebtorMember).ToString());
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.MemberRequest));
            adapter.Fill(dt);
            return dt;


        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMemberRequestForChangInfoReq(int MeId, MemberRequestType MemberRequestType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberRequestForAgent", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ReqType", (int)MemberRequestType);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.MemberRequest));
            adapter.Fill(dt);
            return dt;
        }
        #endregion


        public DataTable SelectMemberForWebservice(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectMemberForWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable SelectNewMembersCodeForWebService(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectNewMembersCodeForWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable DocMemberInfoWebService(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectForDocMemberInfoWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.Fill(dt);
            return (dt);

        }

        public DataTable SelectMemberInfoForTSWebService(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectMemberInfoForTsWebservice", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="HasFileNo"></param>
        /// <param name="MajorParentCode">100-200-300-400-500-600-700</param>
        /// <param name=""></param>
        /// <returns></returns>
        public DataTable SelectMemberAndDocInfoForWebService(Int32 MeId,int HasFileNo,int MajorParentCode,int AgentCode)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectMemberAndDocInfoForWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@HasFileNo", HasFileNo);
            adapter.SelectCommand.Parameters.AddWithValue("@MajorParentCode", MajorParentCode);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentCode", AgentCode);
            adapter.Fill(dt);
            return (dt);
        }
        

        public DataTable SelectMemberInfoForEsys(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectMemberInfoForEsys", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectMemberDocMajorCountForEsys(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectDocMemberFileDocMajorCountForEsys", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }
        public DataTable SelectMemberListByMajorForTafkik(int MajorId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectMemberListByMajorForTafkik", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MajorId", SqlDbType.Int, 4).Value = MajorId;
            adapter.Fill(dt);
            return (dt);
        }
        public DataTable SelectMemberInfoForPhoneApp(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectMemberInfoForPhoneApp", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectMemberGradeIdsFromtblMember(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectMemberGradeIds", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }

        public static Boolean CheckMembershipValidation(int MeId, ref string Message, Boolean IsMembershipRequest)
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                Message = "خطایی در بازیابی اطلاعات بوجود آمده است.";
                return false;
            }
            int MRsId = Convert.ToInt32(MemberManager[0]["MrsId"]);
            if (IsMembershipRequest && MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.CancelDebtorMember)
            {
                Message = "امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی قطع شده می باشد.";
                return false;
            }
            else if (!IsMembershipRequest && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
            {

                if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.Pending)
                {
                    Message = "امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی در جریان می باشد.";
                    return false;
                }
                else if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.Cancel)
                {
                    Message = "امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی لغو شده می باشد.";
                    return false;
                }
                else if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.CancelDebtorMember)
                {
                    Message = "امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی قطع شده می باشد.";
                    return false;
                }
                else
                {
                    Message = "امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی تایید شده نمی باشد.";
                    return false;
                }
            }
            return true;
        }

        public static Boolean CheckMembershipValidation(int MeId, ref string Message)
        {
            return CheckMembershipValidation(MeId, ref Message, false);
        }

    }
}

