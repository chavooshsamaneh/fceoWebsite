using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class ObserverWorkRequestChangesManager : BaseObject
    {
        #region  private Managers
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager;
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager;
        #endregion
        #region Constructors
        public ObserverWorkRequestChangesManager(TransactionManager Transaction)
        {
            ObserverWorkRequestManager = new ObserverWorkRequestManager();
            ProjectCapacityDecrementManager = new ProjectCapacityDecrementManager();
            Transaction.Add(ObserverWorkRequestManager);
            Transaction.Add(ProjectCapacityDecrementManager);
        }
        public ObserverWorkRequestChangesManager()
        {
        }
        #endregion
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSObserverWorkRequestChanges";
            tableMapping.ColumnMappings.Add("ObsWorkReqChangeId", "ObsWorkReqChangeId");
            tableMapping.ColumnMappings.Add("ObsWorkReqId", "ObsWorkReqId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("MfId", "MfId");
            tableMapping.ColumnMappings.Add("ObsId", "ObsId");
            tableMapping.ColumnMappings.Add("ObsDate", "ObsDate");
            tableMapping.ColumnMappings.Add("MembershipDate", "MembershipDate");
            tableMapping.ColumnMappings.Add("Group1", "Group1");
            tableMapping.ColumnMappings.Add("Group2", "Group2");
            tableMapping.ColumnMappings.Add("Group3", "Group3");
            tableMapping.ColumnMappings.Add("Group4", "Group4");
            tableMapping.ColumnMappings.Add("CapacityAssignmentId", "CapacityAssignmentId");
            tableMapping.ColumnMappings.Add("City1", "City1");
            tableMapping.ColumnMappings.Add("City2", "City2");
            tableMapping.ColumnMappings.Add("MeAgentId", "MeAgentId");
            tableMapping.ColumnMappings.Add("IsObserverOff", "IsObserverOff");
            tableMapping.ColumnMappings.Add("StartOffDate", "StartOffDate");
            tableMapping.ColumnMappings.Add("EndOffDate", "EndOffDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("CurrentWfTasId", "CurrentWfTasId");
            tableMapping.ColumnMappings.Add("CurrentWfStateId", "CurrentWfStateId");
            tableMapping.ColumnMappings.Add("DocMeFileExpireDate", "DocMeFileExpireDate");
            tableMapping.ColumnMappings.Add("MasterMfMjParentId", "MasterMfMjParentId");
            tableMapping.ColumnMappings.Add("MfMjId", "MfMjId");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("WantCharityWork", "WantCharityWork");
            tableMapping.ColumnMappings.Add("WantShahrakSanatiMeter", "WantShahrakSanatiMeter");
            tableMapping.ColumnMappings.Add("IsFullTimeWorker", "IsFullTimeWorker");
            tableMapping.ColumnMappings.Add("SuburbsMeter", "SuburbsMeter");
            tableMapping.ColumnMappings.Add("ShahrakSanatiMeter", "ShahrakSanatiMeter");
            tableMapping.ColumnMappings.Add("KhanZenyanObserveMeter", "KhanZenyanObserveMeter");
            tableMapping.ColumnMappings.Add("LapooyObserveMeter", "LapooyObserveMeter");
            tableMapping.ColumnMappings.Add("ZarghanObserveMeter", "ZarghanObserveMeter");
            tableMapping.ColumnMappings.Add("DareyonObserveMeter", "DareyonObserveMeter");
            tableMapping.ColumnMappings.Add("BonyadMaskan", "BonyadMaskan");
            tableMapping.ColumnMappings.Add("BonyadMaskanDesignMeter", "BonyadMaskanDesignMeter");
            tableMapping.ColumnMappings.Add("ShirazMunicipalityMeter", "ShirazMunicipalityMeter");
            tableMapping.ColumnMappings.Add("ShirazMunicipalityDesignMeter", "ShirazMunicipalityDesignMeter");
            tableMapping.ColumnMappings.Add("ShirazMunicipulityUrbenismTarh", "ShirazMunicipulityUrbenismTarh");
            tableMapping.ColumnMappings.Add("ShirazMunicipulityUrbenismEntebaghShahri", "ShirazMunicipulityUrbenismEntebaghShahri");
            tableMapping.ColumnMappings.Add("CapacityDesign", "CapacityDesign");
            tableMapping.ColumnMappings.Add("CapacityObs", "CapacityObs");
            tableMapping.ColumnMappings.Add("TotalCapacity", "TotalCapacity");
            tableMapping.ColumnMappings.Add("CountRandomSelected", "CountRandomSelected");
            tableMapping.ColumnMappings.Add("CountRejectByObs", "CountRejectByObs");
            tableMapping.ColumnMappings.Add("CountWorks", "CountWorks");
            tableMapping.ColumnMappings.Add("CountInproccesWorks", "CountInproccesWorks");
            tableMapping.ColumnMappings.Add("WantedWorkType", "WantedWorkType");
            tableMapping.ColumnMappings.Add("UrlAttachment", "UrlAttachment");
            tableMapping.ColumnMappings.Add("HasGasCert", "HasGasCert");
            tableMapping.ColumnMappings.Add("CommitMuniciToll", "CommitMuniciToll");
            tableMapping.ColumnMappings.Add("WantEghdamMeliMaskan", "WantEghdamMeliMaskan");
            tableMapping.ColumnMappings.Add("UrlObserverCommitmentForm", "UrlObserverCommitmentForm");
            this.Adapter.TableMappings.Add(tableMapping);

            #region sp
            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectTSObserverWorkRequestChanges";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@ObsWorkReqChangeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ObsWorkReqId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.DeleteTSObserverWorkRequestChanges";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ObsWorkReqChangeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqChangeId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();

            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.InsertTSObserverWorkRequestChanges";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsWorkReqId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantedWorkType", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantedWorkType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MembershipDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MembershipDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group1", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group2", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group3", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group3", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group4", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group4", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityAssignmentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityAssignmentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@City1", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "City1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@City2", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "City2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeAgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeAgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsObserverOff", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsObserverOff", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartOffDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StartOffDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndOffDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndOffDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MfMjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MfMjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocMeFileExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocMeFileExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MasterMfMjParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MasterMfMjParentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWfTasId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWfTasId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWfStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWfStateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsFullTimeWorker", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsFullTimeWorker", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantCharityWork", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantCharityWork", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantShahrakSanatiMeter", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantShahrakSanatiMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SuburbsMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SuburbsMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShahrakSanatiMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShahrakSanatiMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@KhanZenyanObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "KhanZenyanObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LapooyObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LapooyObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ZarghanObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ZarghanObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DareyonObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DareyonObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BonyadMaskan", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BonyadMaskan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BonyadMaskanDesignMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BonyadMaskanDesignMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipalityMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipalityMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipalityDesignMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipalityDesignMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipulityUrbenismTarh", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipulityUrbenismTarh", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipulityUrbenismEntebaghShahri", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipulityUrbenismEntebaghShahri", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TotalCapacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TotalCapacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UrlAttachment", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UrlAttachment", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HasGasCert", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HasGasCert", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CommitMuniciToll", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CommitMuniciToll", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRandomSelected", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRandomSelected", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRejectByObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRejectByObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountWorks", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountWorks", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountInproccesWorks", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountInproccesWorks", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantEghdamMeliMaskan", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantEghdamMeliMaskan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UrlObserverCommitmentForm", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UrlObserverCommitmentForm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.UpdateTSObserverWorkRequestChanges";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsWorkReqId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantedWorkType", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantedWorkType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MembershipDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MembershipDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group1", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group2", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group3", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group3", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Group4", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Group4", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityAssignmentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityAssignmentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@City1", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "City1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@City2", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "City2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeAgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MeAgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsObserverOff", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsObserverOff", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartOffDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StartOffDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndOffDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndOffDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MfMjId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MfMjId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocMeFileExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocMeFileExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MasterMfMjParentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MasterMfMjParentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWfTasId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWfTasId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentWfStateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentWfStateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsFullTimeWorker", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsFullTimeWorker", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantCharityWork", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantCharityWork", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantShahrakSanatiMeter", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantShahrakSanatiMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SuburbsMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SuburbsMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShahrakSanatiMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShahrakSanatiMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@KhanZenyanObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "KhanZenyanObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LapooyObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LapooyObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ZarghanObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ZarghanObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DareyonObserveMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DareyonObserveMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BonyadMaskan", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BonyadMaskan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BonyadMaskanDesignMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BonyadMaskanDesignMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipalityMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipalityMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipalityDesignMeter", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipalityDesignMeter", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipulityUrbenismTarh", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipulityUrbenismTarh", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ShirazMunicipulityUrbenismEntebaghShahri", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ShirazMunicipulityUrbenismEntebaghShahri", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TotalCapacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TotalCapacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityDesign", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityDesign", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UrlAttachment", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UrlAttachment", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HasGasCert", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HasGasCert", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CommitMuniciToll", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CommitMuniciToll", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRandomSelected", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRandomSelected", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountRejectByObs", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountRejectByObs", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountWorks", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountWorks", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CountInproccesWorks", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CountInproccesWorks", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WantEghdamMeliMaskan", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WantEghdamMeliMaskan", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UrlObserverCommitmentForm", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UrlObserverCommitmentForm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ObsWorkReqChangeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqChangeId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsWorkReqChangeId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "ObsWorkReqChangeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            #endregion
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSObserverWorkRequestChangesDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        #region ObservationWorkRequest
        /// <summary>
        /// Perform the next tasks of Confirming ObservationWorkRequest
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMeObsDocConfirming(int TableId, int CurrentUserAgentId, int CurrentUserId, Int64 CurrentWFStateId, int SelectedTaskId)
        {
            int Per = 0;
            this.FindByObsWorkReqChangeId(TableId);
            if (this.Count <= 0)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 1;
            this[0]["UserId"] = CurrentUserId;
            this[0]["CurrentWfTasId"] = SelectedTaskId;
            this[0]["CurrentWfStateId"] = CurrentWFStateId;
            this[0]["ModifiedDate"] = DateTime.Now;

            this[0].EndEdit();
            if (this.Save() <= 0)
            {
                return (int)ErrorWFNextStep.Error;
            }
            ObserverWorkRequestManager.FindByCode(Convert.ToInt32(this[0]["ObsWorkReqId"]));
            if (ObserverWorkRequestManager.Count == 0)
                return (int)ErrorWFNextStep.Error;
            ObserverWorkRequestManager[0].BeginEdit();
            ObserverWorkRequestManager[0]["MeId"] = this[0]["MeId"];
            int MeId = Convert.ToInt32(this[0]["MeId"]);
            if (Convert.ToInt32(ObserverWorkRequestManager[0]["Status"]) == (int)TSObserverWorkRequestStatus.Pending)
                ObserverWorkRequestManager[0]["Status"] = (int)TSObserverWorkRequestStatus.Confirm;
            ObserverWorkRequestManager[0]["MfId"] = this[0]["MfId"];
            ObserverWorkRequestManager[0]["ObsId"] = this[0]["ObsId"];
            ObserverWorkRequestManager[0]["ObsDate"] = this[0]["ObsDate"];
            ObserverWorkRequestManager[0]["WantedWorkType"] = this[0]["WantedWorkType"];
            ObserverWorkRequestManager[0]["MembershipDate"] = this[0]["MembershipDate"];
            ObserverWorkRequestManager[0]["Group4"] = this[0]["Group4"];
            ObserverWorkRequestManager[0]["Group3"] = this[0]["Group3"];
            ObserverWorkRequestManager[0]["Group2"] = this[0]["Group2"];
            ObserverWorkRequestManager[0]["Group1"] = this[0]["Group1"];
            ObserverWorkRequestManager[0]["CapacityAssignmentId"] = this[0]["CapacityAssignmentId"];
            ObserverWorkRequestManager[0]["City2"] = this[0]["City2"];
            ObserverWorkRequestManager[0]["City1"] = this[0]["City1"];
            ObserverWorkRequestManager[0]["MeAgentId"] = this[0]["MeAgentId"];
            ObserverWorkRequestManager[0]["IsObserverOff"] = this[0]["IsObserverOff"];
            ObserverWorkRequestManager[0]["StartOffDate"] = this[0]["StartOffDate"];
            ObserverWorkRequestManager[0]["EndOffDate"] = this[0]["EndOffDate"];

            ObserverWorkRequestManager[0]["MfMjId"] = this[0]["MfMjId"];
            ObserverWorkRequestManager[0]["DocMeFileExpireDate"] = this[0]["DocMeFileExpireDate"];
            ObserverWorkRequestManager[0]["MasterMfMjParentId"] = this[0]["MasterMfMjParentId"];
            ObserverWorkRequestManager[0]["CurrentWfStateId"] = this[0]["CurrentWfStateId"];
            ObserverWorkRequestManager[0]["CurrentWfTasId"] = this[0]["CurrentWfTasId"];
            ObserverWorkRequestManager[0]["IsFullTimeWorker"] = this[0]["IsFullTimeWorker"];
            ObserverWorkRequestManager[0]["WantCharityWork"] = this[0]["WantCharityWork"];
            ObserverWorkRequestManager[0]["WantShahrakSanatiMeter"] = this[0]["WantShahrakSanatiMeter"];
            ObserverWorkRequestManager[0]["ShahrakSanatiMeter"] = this[0]["ShahrakSanatiMeter"];
            ObserverWorkRequestManager[0]["SuburbsMeter"] = this[0]["SuburbsMeter"];
            ObserverWorkRequestManager[0]["KhanZenyanObserveMeter"] = this[0]["KhanZenyanObserveMeter"];
            ObserverWorkRequestManager[0]["LapooyObserveMeter"] = this[0]["LapooyObserveMeter"];
            ObserverWorkRequestManager[0]["ZarghanObserveMeter"] = this[0]["ZarghanObserveMeter"];
            ObserverWorkRequestManager[0]["DareyonObserveMeter"] = this[0]["DareyonObserveMeter"];
            ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"] = this[0]["ShirazMunicipalityMeter"];
            ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"] = this[0]["ShirazMunicipalityDesignMeter"];
            ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"] = this[0]["ShirazMunicipulityUrbenismTarh"];
            ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"] = this[0]["ShirazMunicipulityUrbenismEntebaghShahri"];
            ObserverWorkRequestManager[0]["BonyadMaskan"] = this[0]["BonyadMaskan"];
            ObserverWorkRequestManager[0]["BonyadMaskanDesignMeter"] = this[0]["BonyadMaskanDesignMeter"];
            ObserverWorkRequestManager[0]["UrlAttachment"] = this[0]["UrlAttachment"];
            ObserverWorkRequestManager[0]["HasGasCert"] = this[0]["HasGasCert"];

            ObserverWorkRequestManager[0]["UserId"] = CurrentUserId;
            //ObserverWorkRequestManager[0]["ModifiedDate"] = DateTime.Now;
            ObserverWorkRequestManager[0]["CountInproccesWorks"] = this[0]["CountInproccesWorks"];
            ObserverWorkRequestManager[0]["CountWorks"] = this[0]["CountWorks"];
            ObserverWorkRequestManager[0]["CountRejectByObs"] = this[0]["CountRejectByObs"];
            ObserverWorkRequestManager[0]["CountRandomSelected"] = this[0]["CountRandomSelected"];
            ObserverWorkRequestManager[0]["CapacityDesign"] = this[0]["CapacityDesign"];
            ObserverWorkRequestManager[0]["CapacityObs"] = this[0]["CapacityObs"];
            ObserverWorkRequestManager[0]["TotalCapacity"] = this[0]["TotalCapacity"];
            ObserverWorkRequestManager[0].EndEdit();
            if (ObserverWorkRequestManager.Save() <= 0)
            {
                return (int)ErrorWFNextStep.Error;
            }
            ObserverWorkRequestManager.DataTable.AcceptChanges();
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            if (CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeId, CurrentUserId, -2, -2, false, TSProjectIngridientType.Nothing, null, false, false,false) != 0)
                return (int)ErrorWFNextStep.ErrorInUpdateWorkREquestCapacity;

            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting ObservationWorkRequest
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMeObsDocRejecting(int TableId, int CurrentUserId, Int64 CurrentWFStateId, int SelectedTaskId)
        {
            int Per = 0;
            this.FindByObsWorkReqChangeId(TableId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 2;
                this[0]["UserId"] = CurrentUserId;
                this[0]["CurrentWfTasId"] = SelectedTaskId;
                this[0]["CurrentWfStateId"] = CurrentWFStateId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                if (this.Save() > 0)
                {
                    Per = 0;
                }
                else
                {
                    Per = (int)ErrorWFNextStep.Error;
                }

                ObserverWorkRequestManager.FindByCode(Convert.ToInt32(this[0]["ObsWorkReqId"]));
                if (ObserverWorkRequestManager.Count == 0)
                    return (int)ErrorWFNextStep.Error;
                ObserverWorkRequestManager[0].BeginEdit();
                ObserverWorkRequestManager[0]["CurrentWfTasId"] = SelectedTaskId;
                ObserverWorkRequestManager[0]["CurrentWfStateId"] = CurrentWFStateId;
                ObserverWorkRequestManager[0].EndEdit();
                if (ObserverWorkRequestManager.Save() <= 0)
                {
                    Per = (int)ErrorWFNextStep.Error;
                }

            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }
        #endregion

        public void FindByObsWorkReqChangeId(int ObsWorkReqChangeId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@ObsWorkReqChangeId"].Value = ObsWorkReqChangeId;
            Fill();
        }
        public void FindByObsWorkReqId(int ObsWorkReqId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@ObsWorkReqId"].Value = ObsWorkReqId;
            Fill();
        }
        public bool HasValidObserverWorkReq(int MeId)
        {
            int count = 0;
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSHasValidObserverWorkReq", this.Connection);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.Fill(dt);
            count = Convert.ToInt32(dt.Rows[0]["count"].ToString());
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage(int ObsWorkReqId, int ObsWorkReqChangeId, int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSObserverWorkRequestChangesForManagmentPAge", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsWorkReqId", ObsWorkReqId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsWorkReqChangeId", ObsWorkReqChangeId);

            adapter.Fill(dt);

            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastRequest(int ObsWorkReqId, int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSObserverWorkRequestChangesLastRequest", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsWorkReqId", ObsWorkReqId);

            adapter.Fill(dt);

            return dt;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSObserverWorkRequestChangesLastRequestForInsertNewDataRow(int ObsWorkReqId, int MeId)
        {
            //DataTable dt = new DataTable();
            DataTable.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSObserverWorkRequestChangesLastRequestForInsertNewDataRow", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsWorkReqId", ObsWorkReqId);

            adapter.Fill(this.DataTable);

            return this.DataTable;
        }


        public DataTable SelectLastConfirmRequest(int ObsWorkReqId, int MeId, int ObsWorkReqChangeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSObserverWorkRequestChangesLastConfirmRequest", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsWorkReqId", ObsWorkReqId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsWorkReqChangeId", ObsWorkReqChangeId);

            adapter.Fill(dt);

            return dt;
        }

        public DataTable SelectLastConfirmRequest(int ObsWorkReqId, int MeId)
        {
            return SelectLastConfirmRequest(ObsWorkReqId, MeId, -1);
        }
        public DataTable SelectComperChangeRequest(int ObsWorkReqChangeId, int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSComperChangeObserverWorkRequestChanges", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsWorkReqChangeId", ObsWorkReqChangeId);

            adapter.Fill(dt);

            return dt;
        }
        public DataTable FindByMeId(int MeId, Int16 Type, Int16 IsConfirm)
        {
            DataTable dt = new DataTable();
            if (MeId == -1)
            {
                return (dt);
            }
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "SelectObserverWorkRequestChangeByMeId";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.SmallInt, 4, "Type").Value = Type;
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.SmallInt, 4, "IsConfirm").Value = IsConfirm;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.Fill(dt);
            return (dt);
        }

        /// <summary>
        /// در زمان ثبت در شورای انتظامی باید جلوی ادامه کار فرد به واسطه ی  تامعتبر کردن آماده به کاری گرفته شود
        /// در زمان تایید پروانه اشتفال بکار پایه و صلاخیت ها و تاریخ تمدید
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="observerWorkRequestManager"></param>
        /// <param name="workFlowStateManager"></param>
        /// <param name="DescriptionWFStart"></param>
        /// <param name="DescriptionWFConfirm"></param>
        /// <param name="Type"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="transactionManager"></param>
        /// <returns></returns>
        public int DoAutomaticObserverWorkRequestChange(int MeId, string DescriptionWFStart, string DescriptionWFConfirm, TSObserverWorkRequestChangeType TSObsWorkRequestChangeType, int CurrentUserId, int MfId, string DocMeFileExpireDate, int MeAgentId
        , TSP.DataManager.TransactionManager transactionManager)
        {
            int ObsId = -2; int MappingId = -2; int DesId = -2; int UrbenismId = -2; string ObsDate = ""; string MappingDate = "";
            int ReturnValue = -1;
            // TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new ObserverWorkRequestManager();
            ///transactionManager.Add(ObserverWorkRequestManager);
            if (ObserverWorkRequestManager == null)
            {
                ObserverWorkRequestManager = new ObserverWorkRequestManager();
                transactionManager.Add(ObserverWorkRequestManager);
            }
            ObserverWorkRequestManager.FindByMeId(MeId);
            if (ObserverWorkRequestManager.Count == 0)
            {
                return 0;//???????????????????????????????
            }
            int MjParentId = Convert.ToInt32(ObserverWorkRequestManager[0]["MasterMfMjParentId"]);
            TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new ObserverWorkRequestChangesManager();
            TSP.DataManager.WorkFlowTaskManager workFlowTaskManager = new WorkFlowTaskManager();
            TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateObserverWorkManager = new WorkFlowStateObserverWorkManager();
            TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
            TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager workFlowStateManager = new WorkFlowStateObserverWorkManager();
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager PrjCapacityDecrementManager = new ProjectCapacityDecrementManager();
            transactionManager.Add(MemberCapacityManager);
            transactionManager.Add(workFlowStateManager);
            transactionManager.Add(ObserverWorkRequestChangesManager);
            transactionManager.Add(workFlowTaskManager);
            transactionManager.Add(WorkFlowStateObserverWorkManager);
            transactionManager.Add(DocMemberFileDetailManager);
            transactionManager.Add(PrjCapacityDecrementManager);

            if (TSObsWorkRequestChangeType != TSObserverWorkRequestChangeType.InActiveObserverRestrictionMembership && Convert.ToInt32(ObserverWorkRequestManager[0]["Status"]) != (int)TSObserverWorkRequestStatus.Confirm)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.MeMberHasWorkRequestChangesFirstConfirmThat;
            }
            int ObsWorkReqId = Convert.ToInt32(ObserverWorkRequestManager[0]["ObsWorkReqId"]);
            #region MeDocDetail
            DataTable dtMeDetailObs = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation, -1, MjParentId);
            DataTable dtMeDetailMapping = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping, -1, MjParentId);
            DataTable dtMeDetailDesign = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design, -1, MjParentId);
            DataTable dtMeDetailUrbanism = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism, -1, MjParentId);
            if (dtMeDetailObs.Rows.Count <= 0 && dtMeDetailMapping.Rows.Count <= 0 && dtMeDetailDesign.Rows.Count <= 0 && dtMeDetailUrbanism.Rows.Count <= 0)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.CanNotFindeFileDetails;
            }
            #region نظارت
            if (dtMeDetailObs.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailObs.Rows[0]["GrdName"]) && !Utility.IsDBNullOrNullValue(dtMeDetailObs.Rows[0]["GrdId"]))
            {
                ObsDate = dtMeDetailObs.Rows[0]["Date"].ToString();
                ObsId = Convert.ToInt32(dtMeDetailObs.Rows[0]["GrdId"]);
            }
            #endregion
            #region نقشه برداری
            if (dtMeDetailMapping.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailMapping.Rows[0]["GrdName"]) && !Utility.IsDBNullOrNullValue(dtMeDetailMapping.Rows[0]["GrdId"]))
            {
                MappingDate = dtMeDetailMapping.Rows[0]["Date"].ToString();
                MappingId = Convert.ToInt32(dtMeDetailMapping.Rows[0]["GrdId"]);
            }
            #endregion
            #region طراحی        
            if (dtMeDetailDesign.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailDesign.Rows[0]["GrdName"]) && !Utility.IsDBNullOrNullValue(dtMeDetailDesign.Rows[0]["GrdId"]))
            {
                DesId = Convert.ToInt32(dtMeDetailDesign.Rows[0]["GrdId"]);
            }
            #endregion
            #region شهرسازی
            if (dtMeDetailUrbanism.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailUrbanism.Rows[0]["GrdName"]) && !Utility.IsDBNullOrNullValue(dtMeDetailUrbanism.Rows[0]["GrdId"]))
            {
                UrbenismId = Convert.ToInt32(dtMeDetailUrbanism.Rows[0]["GrdId"]);
            }
            #endregion
            #endregion
            #region Calculate Capcacity Value
            int ObservationCapacity = 0;
            if (ObsId != -2)
            {
                MemberCapacityManager.FindByGrdId(ObsId);
                if (MemberCapacityManager.Count > 0)
                {
                    ObservationCapacity = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]);
                }
            }
            int DesignCapacity = 0;
            if (DesId != -2)
            {
                MemberCapacityManager.FindByGrdId(DesId);
                if (MemberCapacityManager.Count > 0)
                {
                    DesignCapacity = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]);
                }
            }
            int MappingCapacity = 0;
            if (MappingId != -2)
            {
                MemberCapacityManager.FindByGrdId(MappingId);
                if (MemberCapacityManager.Count > 0)
                {
                    MappingCapacity = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]);
                }
            }
            int MemberUrbenismTarhShahriCapacity = 0;
            int MemberUrbenismEntebaghShahriSakhtemanCapacity = 0;
            if (UrbenismId != -2)
            {
                TSP.DataManager.TechnicalServices.UrbanistQualificationManager UrbanistQualificationManager = new TSP.DataManager.TechnicalServices.UrbanistQualificationManager();

                UrbanistQualificationManager.FindByGrade(UrbenismId, (int)TSUrbanismQualificationType.SumAmadeSaziAraziAndTafkikAraziAndEntebaghKarbariArazi, 0);
                if (UrbanistQualificationManager.Count > 0)
                {
                    MemberUrbenismTarhShahriCapacity = Convert.ToInt32(UrbanistQualificationManager[0]["QualificationMeter"]);

                }
                UrbanistQualificationManager.FindByGrade(UrbenismId, (int)TSUrbanismQualificationType.EntebaghShahriSakhteman, 0);
                if (UrbanistQualificationManager.Count > 0)
                {
                    MemberUrbenismEntebaghShahriSakhtemanCapacity = Convert.ToInt32(UrbanistQualificationManager[0]["QualificationMeter"]);
                }
            }
            if (MjParentId == (int)MainMajors.Mapping)//اگر رشته نقشه برداری باشد ظرفیت نظارت براساس پایه نقشه برداری در نظر گرفته می شود
            {
                ObservationCapacity = MappingCapacity;
            }
            if (TSObsWorkRequestChangeType != TSObserverWorkRequestChangeType.InActiveGasOfficeMembership &&
                (TSObsWorkRequestChangeType == TSObserverWorkRequestChangeType.GasOfficeMembership ||
                (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["HasGasCert"]) && Convert.ToBoolean(ObserverWorkRequestManager[0]["HasGasCert"])))
                )
            {
                //******اگر عضو دفتر گاز باشد 25 درصد کسر می شود
                DesignCapacity = DesignCapacity - (DesignCapacity / 4);
                ObservationCapacity = ObservationCapacity - (ObservationCapacity / 4);

            }
            #endregion
            #region FindTaskId
            int SaveInfoTaskId = -1;
            int ConfirmTSWorkRequestConfirminAndEndProccessTaskId = -1;

            workFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo);
            if (workFlowTaskManager.Count > 0)
            {
                SaveInfoTaskId = int.Parse(workFlowTaskManager[0]["TaskId"].ToString());
            }
            workFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ConfirmTSWorkRequestConfirminAndEndProccess);
            if (workFlowTaskManager.Count > 0)
            {
                ConfirmTSWorkRequestConfirminAndEndProccessTaskId = int.Parse(workFlowTaskManager[0]["TaskId"].ToString());
            }
            #endregion

            #region Insert WorkChange
            DataTable dtWorkReqChange = ObserverWorkRequestChangesManager.SelectTSObserverWorkRequestChangesLastRequestForInsertNewDataRow(ObsWorkReqId, MeId);
            if (dtWorkReqChange.Rows.Count == 0)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.Error;
            }
            if (Convert.ToInt32(dtWorkReqChange.Rows[0]["IsConfirm"]) == (int)WorkFlowRequestConfirmStatus.Pending)
                return (int)TSP.DataManager.ErrorWFNextStep.MeMberHasWorkRequestChangesFirstConfirmThat;
            DataRow drWorkReqChange = ObserverWorkRequestChangesManager.NewRow();
            #region setinfo change   
            drWorkReqChange["MeId"] = dtWorkReqChange.Rows[0]["MeId"];
            drWorkReqChange["ObsWorkReqId"] = dtWorkReqChange.Rows[0]["ObsWorkReqId"];
            drWorkReqChange["MfId"] = dtWorkReqChange.Rows[0]["MfId"];
            drWorkReqChange["ObsId"] = dtWorkReqChange.Rows[0]["ObsId"];
            drWorkReqChange["ObsDate"] = dtWorkReqChange.Rows[0]["ObsDate"];
            drWorkReqChange["MembershipDate"] = dtWorkReqChange.Rows[0]["MembershipDate"];
            drWorkReqChange["Group1"] = dtWorkReqChange.Rows[0]["Group1"];
            drWorkReqChange["Group2"] = dtWorkReqChange.Rows[0]["Group2"];
            drWorkReqChange["Group3"] = dtWorkReqChange.Rows[0]["Group3"];
            drWorkReqChange["Group4"] = dtWorkReqChange.Rows[0]["Group4"];
            drWorkReqChange["CapacityAssignmentId"] = dtWorkReqChange.Rows[0]["CapacityAssignmentId"];
            drWorkReqChange["City1"] = dtWorkReqChange.Rows[0]["City1"];
            drWorkReqChange["City2"] = dtWorkReqChange.Rows[0]["City2"];
            drWorkReqChange["MeAgentId"] = dtWorkReqChange.Rows[0]["MeAgentId"];
            drWorkReqChange["IsObserverOff"] = dtWorkReqChange.Rows[0]["IsObserverOff"];
            drWorkReqChange["StartOffDate"] = dtWorkReqChange.Rows[0]["StartOffDate"];
            drWorkReqChange["EndOffDate"] = dtWorkReqChange.Rows[0]["EndOffDate"];
            drWorkReqChange["Description"] = dtWorkReqChange.Rows[0]["Description"];
            drWorkReqChange["DocMeFileExpireDate"] = dtWorkReqChange.Rows[0]["DocMeFileExpireDate"];
            if (DocMeFileExpireDate == "%")
                DocMeFileExpireDate = dtWorkReqChange.Rows[0]["DocMeFileExpireDate"].ToString();
            drWorkReqChange["MasterMfMjParentId"] = dtWorkReqChange.Rows[0]["MasterMfMjParentId"];
            drWorkReqChange["MfMjId"] = dtWorkReqChange.Rows[0]["MfMjId"];
            drWorkReqChange["WantCharityWork"] = dtWorkReqChange.Rows[0]["WantCharityWork"];
            drWorkReqChange["WantShahrakSanatiMeter"] = dtWorkReqChange.Rows[0]["WantShahrakSanatiMeter"];
            drWorkReqChange["IsFullTimeWorker"] = dtWorkReqChange.Rows[0]["IsFullTimeWorker"];
            drWorkReqChange["SuburbsMeter"] = dtWorkReqChange.Rows[0]["SuburbsMeter"];
            drWorkReqChange["ShahrakSanatiMeter"] = dtWorkReqChange.Rows[0]["ShahrakSanatiMeter"];
            drWorkReqChange["KhanZenyanObserveMeter"] = dtWorkReqChange.Rows[0]["KhanZenyanObserveMeter"];
            drWorkReqChange["LapooyObserveMeter"] = dtWorkReqChange.Rows[0]["LapooyObserveMeter"];
            drWorkReqChange["ZarghanObserveMeter"] = dtWorkReqChange.Rows[0]["ZarghanObserveMeter"];
            drWorkReqChange["DareyonObserveMeter"] = dtWorkReqChange.Rows[0]["DareyonObserveMeter"];
            drWorkReqChange["BonyadMaskan"] = dtWorkReqChange.Rows[0]["BonyadMaskan"];
            drWorkReqChange["BonyadMaskanDesignMeter"] = dtWorkReqChange.Rows[0]["BonyadMaskanDesignMeter"];
            drWorkReqChange["ShirazMunicipalityMeter"] = dtWorkReqChange.Rows[0]["ShirazMunicipalityMeter"];
            drWorkReqChange["ShirazMunicipalityDesignMeter"] = dtWorkReqChange.Rows[0]["ShirazMunicipalityDesignMeter"];
            drWorkReqChange["ShirazMunicipulityUrbenismTarh"] = dtWorkReqChange.Rows[0]["ShirazMunicipulityUrbenismTarh"];
            drWorkReqChange["ShirazMunicipulityUrbenismEntebaghShahri"] = dtWorkReqChange.Rows[0]["ShirazMunicipulityUrbenismEntebaghShahri"];
            drWorkReqChange["CapacityDesign"] = dtWorkReqChange.Rows[0]["CapacityDesign"];
            drWorkReqChange["CapacityObs"] = dtWorkReqChange.Rows[0]["CapacityObs"];
            //drWorkReqChange["TotalCapacity"] = dtWorkReqChange.Rows[0]["TotalCapacity"];
            drWorkReqChange["CountRandomSelected"] = dtWorkReqChange.Rows[0]["CountRandomSelected"];
            drWorkReqChange["CountRejectByObs"] = dtWorkReqChange.Rows[0]["CountRejectByObs"];
            drWorkReqChange["CountWorks"] = dtWorkReqChange.Rows[0]["CountWorks"];
            drWorkReqChange["CountInproccesWorks"] = dtWorkReqChange.Rows[0]["CountInproccesWorks"];
            drWorkReqChange["WantedWorkType"] = dtWorkReqChange.Rows[0]["WantedWorkType"];
            drWorkReqChange["UrlAttachment"] = dtWorkReqChange.Rows[0]["UrlAttachment"];
            drWorkReqChange["HasGasCert"] = dtWorkReqChange.Rows[0]["HasGasCert"];
            drWorkReqChange["CommitMuniciToll"] = dtWorkReqChange.Rows[0]["CommitMuniciToll"];
            drWorkReqChange["CapacityObs"] = ObservationCapacity;
            drWorkReqChange["CapacityDesign"] = DesignCapacity;
            drWorkReqChange["TotalCapacity"] = Math.Max(ObservationCapacity, DesignCapacity);
            drWorkReqChange["IsConfirm"] = (int)WorkFlowRequestConfirmStatus.Confirm;
            #endregion
            ////////////////////////
            drWorkReqChange["Type"] = (int)TSObsWorkRequestChangeType;
            switch (TSObsWorkRequestChangeType)
            {
                case TSObserverWorkRequestChangeType.ShorayeEntezami:
                    break;
                case TSObserverWorkRequestChangeType.AnbohSazan:
                    break;
                case TSObserverWorkRequestChangeType.SherkatAzemayeshgahi:
                    break;
                case TSObserverWorkRequestChangeType.PeymanKar:
                    break;
                case TSObserverWorkRequestChangeType.InActive:
                    break;
                case TSObserverWorkRequestChangeType.MemberDocumentChange:
                    #region MemberDocumentChange
                    if (Convert.ToInt32(drWorkReqChange["MasterMfMjParentId"]) == (int)MainMajors.Mapping)
                    {
                        if (MappingId != -2)
                            drWorkReqChange["ObsId"] = MappingId;
                        else
                            drWorkReqChange["ObsId"] = -2;
                        drWorkReqChange["ObsDate"] = MappingDate;
                    }
                    else
                    {
                        if (ObsId != -2)
                            drWorkReqChange["ObsId"] = ObsId;
                        else
                            drWorkReqChange["ObsId"] = -2;
                        drWorkReqChange["ObsDate"] = ObsDate;
                    }
                    if (MfId != -2)
                        drWorkReqChange["MfId"] = MfId;
                    if (MeAgentId != -2)
                        drWorkReqChange["MeAgentId"] = MeAgentId;

                    drWorkReqChange["DocMeFileExpireDate"] = DocMeFileExpireDate;
                    #endregion
                    break;
                case TSObserverWorkRequestChangeType.AgentChange:
                    if (MeAgentId != -2)
                        drWorkReqChange["MeAgentId"] = MeAgentId;
                    drWorkReqChange["City1"] = DBNull.Value;
                    drWorkReqChange["City2"] = DBNull.Value;
                    drWorkReqChange["ShirazMunicipalityMeter"] = 0;
                    drWorkReqChange["ShirazMunicipalityDesignMeter"] = 0;
                    drWorkReqChange["ShirazMunicipulityUrbenismTarh"] = 0;
                    drWorkReqChange["ShirazMunicipulityUrbenismEntebaghShahri"] = 0;
                    break;
                case TSObserverWorkRequestChangeType.GasOfficeMembership:
                    drWorkReqChange["HasGasCert"] = true;
                    break;
                case TSObserverWorkRequestChangeType.InActiveGasOfficeMembership:
                    drWorkReqChange["HasGasCert"] = false;
                    break;
                case TSObserverWorkRequestChangeType.InActiveObserverRestrictionMembership:
                    break;

            }
            drWorkReqChange["CurrentWfTasId"] = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
            drWorkReqChange["CurrentWfStateId"] = "-2";
            drWorkReqChange["UserId"] = CurrentUserId;
            drWorkReqChange["CreateDate"] = Utility.GetDateOfToday();
            drWorkReqChange["ModifiedDate"] = DateTime.Now;
            ObserverWorkRequestChangesManager.AddRow(drWorkReqChange);
            ObserverWorkRequestChangesManager.Save();
            ObserverWorkRequestChangesManager.DataTable.AcceptChanges();
            int CurrentObsWorkReqChangeId = Convert.ToInt32(ObserverWorkRequestChangesManager[ObserverWorkRequestChangesManager.Count - 1]["ObsWorkReqChangeId"]);
            #endregion
            #region Update WorkReq
            ObserverWorkRequestManager[0].BeginEdit();

            ObserverWorkRequestManager[0]["CapacityObs"] = ObservationCapacity;
            ObserverWorkRequestManager[0]["CapacityDesign"] = DesignCapacity;
            ObserverWorkRequestManager[0]["TotalCapacity"] = Math.Max(ObservationCapacity, DesignCapacity);

            switch (TSObsWorkRequestChangeType)
            {
                case TSObserverWorkRequestChangeType.ShorayeEntezami:
                    ObserverWorkRequestManager[0]["Status"] = (int)TSObserverWorkRequestStatus.ShorayeEntezami;
                    break;
                case TSObserverWorkRequestChangeType.AnbohSazan:
                    ObserverWorkRequestManager[0]["Status"] = (int)TSObserverWorkRequestStatus.AnbohSazan;
                    break;
                case TSObserverWorkRequestChangeType.SherkatAzemayeshgahi:
                    ObserverWorkRequestManager[0]["Status"] = (int)TSObserverWorkRequestStatus.SherkatAzemayeshgahi;
                    break;
                case TSObserverWorkRequestChangeType.PeymanKar:
                    ObserverWorkRequestManager[0]["Status"] = (int)TSObserverWorkRequestStatus.PeymanKar;
                    break;
                case TSObserverWorkRequestChangeType.InActive:
                    ObserverWorkRequestManager[0]["Status"] = (int)TSObserverWorkRequestStatus.InActive;
                    break;
                case TSObserverWorkRequestChangeType.MemberDocumentChange:
                    #region MemberDocumentChange
                    if (Convert.ToInt32(ObserverWorkRequestManager[0]["MasterMfMjParentId"]) == (int)MainMajors.Mapping)
                    {
                        if (MappingId != -2)
                            ObserverWorkRequestManager[0]["ObsId"] = MappingId;
                        else
                            ObserverWorkRequestManager[0]["ObsId"] = -2;
                        ObserverWorkRequestManager[0]["ObsDate"] = MappingDate;
                    }
                    else
                    {
                        if (ObsId != -2)
                            ObserverWorkRequestManager[0]["ObsId"] = ObsId;
                        else
                            ObserverWorkRequestManager[0]["ObsId"] = -2;
                        ObserverWorkRequestManager[0]["ObsDate"] = ObsDate;
                    }
                    if (MfId != -2)
                        ObserverWorkRequestManager[0]["MfId"] = MfId;
                    if (MeAgentId != -2)
                        ObserverWorkRequestManager[0]["MeAgentId"] = MeAgentId;
                    ObserverWorkRequestManager[0]["DocMeFileExpireDate"] = DocMeFileExpireDate;
                    #endregion
                    break;
                case TSObserverWorkRequestChangeType.AgentChange:
                    if (MeAgentId != -2)
                        ObserverWorkRequestManager[0]["MeAgentId"] = MeAgentId;
                    ObserverWorkRequestManager[0]["City1"] = DBNull.Value;
                    ObserverWorkRequestManager[0]["City2"] = DBNull.Value;
                    ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"] = 0;
                    ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"] = 0;
                    ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"] = 0;
                    ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"] = 0;
                    break;
                case TSObserverWorkRequestChangeType.GasOfficeMembership:
                    #region MemberDocumentChange
                    ObserverWorkRequestManager[0]["HasGasCert"] = true;
                    if (Convert.ToInt32(ObserverWorkRequestManager[0]["MasterMfMjParentId"]) == (int)MainMajors.Mapping)
                    {
                        if (MappingId != -2)
                            ObserverWorkRequestManager[0]["ObsId"] = MappingId;
                        else
                            ObserverWorkRequestManager[0]["ObsId"] = -2;
                        ObserverWorkRequestManager[0]["ObsDate"] = MappingDate;
                    }
                    else
                    {
                        if (ObsId != -2)
                            ObserverWorkRequestManager[0]["ObsId"] = ObsId;
                        else
                            ObserverWorkRequestManager[0]["ObsId"] = -2;
                        ObserverWorkRequestManager[0]["ObsDate"] = ObsDate;
                    }
                    if (MfId != -2)
                        ObserverWorkRequestManager[0]["MfId"] = MfId;
                    if (MeAgentId != -2)
                        ObserverWorkRequestManager[0]["MeAgentId"] = MeAgentId;
                    ObserverWorkRequestManager[0]["DocMeFileExpireDate"] = DocMeFileExpireDate;
                    #endregion
                    break;
                case TSObserverWorkRequestChangeType.InActiveGasOfficeMembership:

                    #region MemberDocumentChange
                    ObserverWorkRequestManager[0]["HasGasCert"] = false;
                    if (Convert.ToInt32(ObserverWorkRequestManager[0]["MasterMfMjParentId"]) == (int)MainMajors.Mapping)
                    {
                        if (MappingId != -2)
                            ObserverWorkRequestManager[0]["ObsId"] = MappingId;
                        else
                            ObserverWorkRequestManager[0]["ObsId"] = -2;
                        ObserverWorkRequestManager[0]["ObsDate"] = MappingDate;
                    }
                    else
                    {
                        if (ObsId != -2)
                            ObserverWorkRequestManager[0]["ObsId"] = ObsId;
                        else
                            ObserverWorkRequestManager[0]["ObsId"] = -2;
                        ObserverWorkRequestManager[0]["ObsDate"] = ObsDate;
                    }
                    if (MfId != -2)
                        ObserverWorkRequestManager[0]["MfId"] = MfId;
                    if (MeAgentId != -2)
                        ObserverWorkRequestManager[0]["MeAgentId"] = MeAgentId;
                    ObserverWorkRequestManager[0]["DocMeFileExpireDate"] = DocMeFileExpireDate;
                    #endregion
                    break;
                case TSObserverWorkRequestChangeType.InActiveObserverRestrictionMembership:
                    ObserverWorkRequestManager[0]["Status"] = (int)TSObserverWorkRequestStatus.Confirm;
                    break;
            }

            ObserverWorkRequestManager[0]["CurrentWfTasId"] = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
            ObserverWorkRequestManager[0]["UserId"] = CurrentUserId;
            ObserverWorkRequestManager[0].EndEdit();
            ObserverWorkRequestManager.Save();
            ObserverWorkRequestManager.DataTable.AcceptChanges();
            #endregion
            #region Insert WF

            int NmcId = 0;
            int WfNmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.System;


            if (WorkFlowStateObserverWorkManager.InsertWorkFlowState(TableTypeManager.FindTtId(TableType.TSWorkRequest), CurrentObsWorkReqChangeId, SaveInfoTaskId, DescriptionWFStart, NmcId, WfNmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) > 0)
            {
                WorkFlowStateObserverWorkManager.DataTable.AcceptChanges();
            }
            else return ReturnValue;
            if (WorkFlowStateObserverWorkManager.InsertWorkFlowState(TableTypeManager.FindTtId(TableType.TSWorkRequest), CurrentObsWorkReqChangeId, ConfirmTSWorkRequestConfirminAndEndProccessTaskId, DescriptionWFConfirm, NmcId, WfNmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) > 0)
                WorkFlowStateObserverWorkManager.DataTable.AcceptChanges();
            else return ReturnValue;
            #endregion
            int CurrentWfStateId = Convert.ToInt32(WorkFlowStateObserverWorkManager[WorkFlowStateObserverWorkManager.Count - 1]["StateId"]);
            ObserverWorkRequestManager[0].BeginEdit();
            ObserverWorkRequestManager[0]["CurrentWfStateId"] = CurrentWfStateId;
            ObserverWorkRequestManager[0].EndEdit();
            ObserverWorkRequestManager.Save();
            ObserverWorkRequestManager.DataTable.AcceptChanges();
            ObserverWorkRequestChangesManager[ObserverWorkRequestChangesManager.Count - 1].BeginEdit();
            ObserverWorkRequestChangesManager[ObserverWorkRequestChangesManager.Count - 1]["CurrentWfStateId"] = CurrentWfStateId;
            ObserverWorkRequestChangesManager[ObserverWorkRequestChangesManager.Count - 1].EndEdit();
            ObserverWorkRequestChangesManager.Save();
            ObserverWorkRequestChangesManager.DataTable.AcceptChanges();
            if (Convert.ToInt32(ObserverWorkRequestChangesManager[ObserverWorkRequestChangesManager.Count - 1]["CurrentWfStateId"]) == -2)
                return (int)TSP.DataManager.ErrorWFNextStep.Error;

            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            if (CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, PrjCapacityDecrementManager, MeId, CurrentUserId, -2, -2, false, TSProjectIngridientType.Nothing, null, false, false,false) != 0)
                return (int)ErrorWFNextStep.ErrorInUpdateWorkREquestCapacity;

            return 0;

        }

    }
}
