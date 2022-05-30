using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class ProjectManager : BaseObject
    {
        public ProjectManager()
            : base()
        {

        }
        public ProjectManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProject);
        }
        public static Permission GetUserPermissionTSProjectRequest(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectRequest);
        }
        public static Permission GetUserPermissionTSBuildingsLicense(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSBuildingsLicense);
        }
        public static Permission GetUserPermissionEndProject(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.EndProject);
        }

        public static Permission GetUserPermissionObserverChangeRequest(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ObserverChangeRequest);
        }

        public static Permission GetUserPermissionImpChangeRequest(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ImpChangeRequest);
        }

        public static Permission GetUserPermissionDesignerChangeRequest(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.DesignerChangeRequest);
        }

        public static Permission GetUserPermissionbtnChangePlanMethod(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSPlansMethod);
        }
        public static Permission GetUserPermissionSaveProjectWithOutDesigner(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.SaveTSProjectWithOutDesigner);
        }
        public static Permission GetUserPermissionSaveTSProjectWithOutObserver(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.SaveTSProjectWithOutObserver);
        }
        
        public static Permission GetUserPermissionChangeAgent(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectChooseAgent);
        }

        public static Permission GetUserPermissionProjectDeletRequestAllInfo(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectDeletRequestAllInfo);
        }
        public static Permission GetUserPermissionTSChangeProjectRequestType(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSChangeProjectRequestType);
        }
        
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSProject";
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("ProjectName", "ProjectName");
            tableMapping.ColumnMappings.Add("TraceCode", "TraceCode");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("FileDate", "FileDate");
            tableMapping.ColumnMappings.Add("UsageId", "UsageId");
            tableMapping.ColumnMappings.Add("GroupId", "GroupId");
            tableMapping.ColumnMappings.Add("ReconstructionCode", "ReconstructionCode");
            tableMapping.ColumnMappings.Add("Foundation", "Foundation");
            tableMapping.ColumnMappings.Add("Area", "Area");
            tableMapping.ColumnMappings.Add("RecessArea", "RecessArea");
            tableMapping.ColumnMappings.Add("RemainArea", "RemainArea");
            tableMapping.ColumnMappings.Add("DocumentArea", "DocumentArea");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("MunId", "MunId");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("ComputerCode", "ComputerCode");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("DiscountPercentId", "DiscountPercentId");
            tableMapping.ColumnMappings.Add("ProjectStatusId", "ProjectStatusId");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("OwnershipTypeId", "OwnershipTypeId");
            tableMapping.ColumnMappings.Add("DesignerSaved", "DesignerSaved");
            tableMapping.ColumnMappings.Add("ArchiveNo", "ArchiveNo");
            tableMapping.ColumnMappings.Add("ProjectNo", "ProjectNo");
            tableMapping.ColumnMappings.Add("OwnerFullName", "OwnerFullName");
            tableMapping.ColumnMappings.Add("MainRegisterNo", "MainRegisterNo");
            tableMapping.ColumnMappings.Add("MainRegion", "MainRegion");
            tableMapping.ColumnMappings.Add("MainSection", "MainSection");
            tableMapping.ColumnMappings.Add("FileUrlBuildingLicence", "FileUrlBuildingLicence");
            tableMapping.ColumnMappings.Add("FileUrlTechnicalBooklet", "FileUrlTechnicalBooklet");
            tableMapping.ColumnMappings.Add("MaxStageNum", "MaxStageNum");
            tableMapping.ColumnMappings.Add("FileUrlBuildingCertificate", "FileUrlBuildingCertificate");
            tableMapping.ColumnMappings.Add("BuildingCertificateStartDate", "BuildingCertificateStartDate");
            tableMapping.ColumnMappings.Add("BuildingCertificateExpirDate", "BuildingCertificateExpirDate");
            tableMapping.ColumnMappings.Add("BuildingCertificateNum", "BuildingCertificateNum");
            tableMapping.ColumnMappings.Add("FoundationMixSkeleton", "FoundationMixSkeleton");
            tableMapping.ColumnMappings.Add("RoofTypeId", "RoofTypeId");
            tableMapping.ColumnMappings.Add("StructureSkeletonId", "StructureSkeletonId");
            tableMapping.ColumnMappings.Add("FileUrlEndProject", "FileUrlEndProject");
            tableMapping.ColumnMappings.Add("EndProjectStartDate", "EndProjectStartDate");
            tableMapping.ColumnMappings.Add("EndProjectExpirDate", "EndProjectExpirDate");
            tableMapping.ColumnMappings.Add("EndProjectNum", "EndProjectNum");
            tableMapping.ColumnMappings.Add("BuldingCheckDate", "BuldingCheckDate");
            tableMapping.ColumnMappings.Add("FileURLBuldingCheck", "FileURLBuldingCheck");
            tableMapping.ColumnMappings.Add("ObserverSaved", "ObserverSaved");
            
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            SetDefaultSelectCommandValue();

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSProject";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ProjectId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSProject";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TraceCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TraceCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsageId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsageId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ReconstructionCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ReconstructionCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Foundation", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Foundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Area", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Area", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RecessArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RecessArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CitId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CitId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MunId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MunId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ComputerCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ComputerCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DiscountPercentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DiscountPercentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OwnershipTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OwnershipTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DesignerSaved", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DesignerSaved", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ArchiveNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ArchiveNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectNo", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OwnerFullName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OwnerFullName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainRegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainRegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainRegion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainRegion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainSection", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainSection", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlBuildingLicence", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlBuildingLicence", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlTechnicalBooklet", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlTechnicalBooklet", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxStageNum", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MaxStageNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlBuildingCertificate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlBuildingCertificate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateExpirDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateExpirDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FoundationMixSkeleton", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FoundationMixSkeleton", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RoofTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RoofTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StructureSkeletonId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StructureSkeletonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlEndProject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlEndProject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectExpirDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectExpirDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuldingCheckDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuldingCheckDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileURLBuldingCheck", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileURLBuldingCheck", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserverSaved", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserverSaved", global::System.Data.DataRowVersion.Current, false, null, "", "", "")); 
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSProject";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TraceCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TraceCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileDate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsageId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsageId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ReconstructionCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ReconstructionCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Foundation", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Foundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Area", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Area", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RecessArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RecessArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CitId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CitId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MunId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MunId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ComputerCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ComputerCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DiscountPercentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DiscountPercentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OwnershipTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OwnershipTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DesignerSaved", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DesignerSaved", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ArchiveNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ArchiveNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectNo", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OwnerFullName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OwnerFullName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainRegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainRegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainRegion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainRegion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainSection", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainSection", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlBuildingLicence", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlBuildingLicence", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlTechnicalBooklet", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlTechnicalBooklet", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxStageNum", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MaxStageNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlBuildingCertificate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlBuildingCertificate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateExpirDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateExpirDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FoundationMixSkeleton", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FoundationMixSkeleton", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RoofTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RoofTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StructureSkeletonId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StructureSkeletonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlEndProject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlEndProject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectExpirDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectExpirDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ProjectId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuldingCheckDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuldingCheckDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileURLBuldingCheck", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileURLBuldingCheck", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserverSaved", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserverSaved", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSProjectDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByProjectId(int ProjectId)
        {
            SetDefaultSelectCommandValue();
            ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable Search(int ProjectId, int GroupId, int CitId, int MunId, string ProjectName, string RegisteredNo, string FileNo, string PlansMethodNo, string FromDate, string ToDate)
        {
            SetDefaultSelectCommandValue();
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@GroupId"].Value = GroupId;
            this.Adapter.SelectCommand.Parameters["@CitId"].Value = CitId;
            this.Adapter.SelectCommand.Parameters["@MunId"].Value = MunId;
            this.Adapter.SelectCommand.Parameters["@ProjectName"].Value = ProjectName;
            this.Adapter.SelectCommand.Parameters["@RegisteredNo"].Value = RegisteredNo;
            this.Adapter.SelectCommand.Parameters["@FileNo"].Value = FileNo;
            this.Adapter.SelectCommand.Parameters["@PlansMethodNo"].Value = PlansMethodNo;
            this.Adapter.SelectCommand.Parameters["@FromDate"].Value = FromDate;
            this.Adapter.SelectCommand.Parameters["@ToDate"].Value = ToDate;
            Fill();
            return this.DataTable;
        }

        public DataTable SearchProjectByArchiveNo(string ArchiveNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectByArchiveNo", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ArchiveNo", ArchiveNo);
            adapter.Fill(dt);

            return dt;
        }
        public DataTable SearchProjectByMainRegisterNo(string MainRegisterNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectByMainRegisterNo", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MainRegisterNo", MainRegisterNo);
            adapter.Fill(dt);

            return dt;
        }

        #region SearchForManagmentPage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="GroupId"></param>
        /// <param name="CitId"></param>
        /// <param name="MunId"></param>
        /// <param name="ProjectName"></param>
        /// <param name="RegisteredNo"></param>
        /// <param name="FileNo"></param>
        /// <param name="PlansMethodNo"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="LicenseNo"></param>
        /// <param name="OwnerName"></param>
        /// <param name="ArchiveNo"></param>
        /// <param name="AgentId"></param>        
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage(int ProjectId, int GroupId, int CitId, int MunId, string ProjectName, string RegisteredNo, string FileNo, string PlansMethodNo, string FromDate, string ToDate, string LicenseNo, string OwnerName
            , string ArchiveNo, int AgentId, string MunParentIdList, int ProjectNo, string MainRegion, string MainSection, int TaskId,int DiscountPercentId)
        {
            if (ProjectId == -1 && GroupId == -1 && CitId == -1 && MunId == -1 && ProjectName == "%" && RegisteredNo == "%" && FileNo == "%"
                && PlansMethodNo == "%" && FromDate == "1" && ToDate == "2" && LicenseNo == "%" && OwnerName == "%" && ArchiveNo == "%" && AgentId == -1
                && ProjectNo == -1 && MainRegion == "%" && MainSection == "%" && TaskId == -1 && DiscountPercentId==-1)
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@GroupId", GroupId);
            adapter.SelectCommand.Parameters.AddWithValue("@CitId", CitId);
            adapter.SelectCommand.Parameters.AddWithValue("@MunId", MunId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectName", ProjectName);
            adapter.SelectCommand.Parameters.AddWithValue("@RegisteredNo", RegisteredNo);
            adapter.SelectCommand.Parameters.AddWithValue("@FileNo", FileNo);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansMethodNo", PlansMethodNo);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@LicenseNo", LicenseNo);
            adapter.SelectCommand.Parameters.AddWithValue("@OwnerName", OwnerName);
            adapter.SelectCommand.Parameters.AddWithValue("@ArchiveNo", ArchiveNo);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);

            adapter.SelectCommand.Parameters.AddWithValue("@MunParentIdList", MunParentIdList);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectNo", ProjectNo);
            adapter.SelectCommand.Parameters.AddWithValue("@MainRegion", MainRegion);
            adapter.SelectCommand.Parameters.AddWithValue("@MainSection", MainSection);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);
            adapter.SelectCommand.Parameters.AddWithValue("@DiscountPercentId", DiscountPercentId);
            
            adapter.Fill(dt);
            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage(int ProjectId, int GroupId, int CitId, int MunId, string ProjectName, string RegisteredNo, string FileNo, string PlansMethodNo, string FromDate, string ToDate, string LicenseNo, string OwnerName
            , string ArchiveNo)
        {
            return SearchForManagmentPage(ProjectId, GroupId, CitId, MunId, ProjectName, RegisteredNo, FileNo, PlansMethodNo, FromDate, ToDate, LicenseNo, OwnerName, ArchiveNo, -1, "(0)", -1, "%", "%", -1,-1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage(int ProjectId, int GroupId, int CitId, int MunId, string ProjectName, string RegisteredNo, string FileNo, string PlansMethodNo, string FromDate, string ToDate, string LicenseNo, string OwnerName)
        {
            return SearchForManagmentPage(ProjectId, GroupId, CitId, MunId, ProjectName, RegisteredNo, FileNo, PlansMethodNo, FromDate, ToDate, LicenseNo, OwnerName, "%");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage(int ProjectId)
        {
            return SearchForManagmentPage(ProjectId, -1, -1, -1, "%", "%", "%", "%", "1", "2", "%", "%");
        }
        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForMunicipulityManagmentPage(int AgentId, int MunId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@MunId", MunId);
            adapter.Fill(dt);
            return dt;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByAgentId(int AgentId)
        {
            SetDefaultSelectCommandValue();
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@AgentId"].Value = AgentId;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectProjectForMembers(int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectForMembers", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.Fill(dt);

            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectProjectForOffice(int OfficeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectForOffice", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@OfficeId", OfficeId);
            adapter.Fill(dt);

            return dt;
        }

        public DataTable SelectProjectIngridients(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectIngridients", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.Fill(dt);
            return dt;

        }

        public DataTable SelectImplementersCurrentProjects(int MeOfficeId, int MemberTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSImplementersCurrentProjects", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeId", MeOfficeId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberTypeId", MemberTypeId);
            adapter.Fill(dt);
            return dt;

        }

        public DataTable SelectObserversCurrentProjects(int MeOfficeOthPEngOId, int MemberTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSObserversCurrentProjects", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeOfficeOthPEngOId", MeOfficeOthPEngOId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberTypeId", MemberTypeId);
            adapter.Fill(dt);
            return dt;

        }

        public DataTable SelectDesignerCurrentProjects(int OfficeEngOId, int MemberTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSDesignerCurrentProjects", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@OfficeEngOId", OfficeEngOId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberTypeId", MemberTypeId);
            adapter.Fill(dt);
            return dt;

        }      
        /// <summary>
        /// چک می کند که پروِژه اسکلت بنایی است یا نه
        /// </summary>
        public bool CheckIfBrickSkeleton(int ProjectId)
        {
            BlockManager Block = new BlockManager();
            DataTable dtBlock =Block.SelectTSBlockByProject(ProjectId);
            if (dtBlock.Rows.Count == 1)
            {
                if (Convert.ToInt32(dtBlock.Rows[0]["StructureSkeletonId"]) == (int)TSStructureSkeleton.Ajory)
                    return true;
            }

            return false;
        }

        private void SetDefaultSelectCommandValue()
        {
            this.Adapter.SelectCommand.CommandText = "spSelectTSProject";
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@AgentId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@GroupId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CitId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MunId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@RegisteredNo", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@FileNo", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@PlansMethodNo", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@FromDate", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.Add("@ToDate", System.Data.SqlDbType.NChar);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@TableTypeProjectReq", (int)TableTypeManager.FindTtId(TableType.TSProjectRequest));
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectProjectMembersReport(int MeId, string MemberFileNo, string FromDate, string ToDate, string FromDateDecreased, string ToDateDecreased, int AgentId,int IsFree)
        {
            if (MeId == -1 && MemberFileNo == "%" && FromDate == "1" && ToDate == "2" && FromDateDecreased == "1" && ToDateDecreased == "2" && AgentId == -1 && IsFree == -1)
                return new DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectForMembers", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@MemberFileNo", MemberFileNo);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDateDecreased", FromDateDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@ToDateDecreased", ToDateDecreased);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsFree", IsFree);
            

            adapter.Fill(dt);

            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectProjectMembersReport(int MeId, string MemberFileNo, string FromDate, string ToDate, string FromDateDecreased, string ToDateDecreased)
        {
            return SelectProjectMembersReport(MeId, MemberFileNo, FromDate, ToDate, FromDateDecreased, ToDateDecreased, -1,-1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectProjectMembersReport(int MeId, string MemberFileNo, string FromDate, string ToDate)
        {
            return SelectProjectMembersReport(MeId, MemberFileNo, FromDate, ToDate);
        }

        public DataTable SelectTSProjectForWebService(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectForWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);

            adapter.Fill(dt);
            return dt;
        }
        public DataTable SelectTSProjectAgentCod(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectAgentCod", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);

            adapter.Fill(dt);
            return dt;
        }

        public DataTable SelectNewTSProjectIdForWebService(int ProjectId, int AgentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectNewTSProjectIdForWebService", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable SelectTSAccountingDocumentReportRemainValue(int AccDocId, string MunParentIdList)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSAccountingDocumentReportRemainValue", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.Parameters.AddWithValue("@AccDocId", AccDocId);
            adapter.SelectCommand.Parameters.AddWithValue("@MunParentIdList", MunParentIdList);
            adapter.Fill(dt);
            return dt;
        }
    }
}
