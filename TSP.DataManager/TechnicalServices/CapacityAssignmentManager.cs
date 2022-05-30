using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace TSP.DataManager.TechnicalServices
{
    public class CapacityAssignmentManager : BaseObject
    {
        public CapacityAssignmentManager()
            : base()
        {

        }
        public CapacityAssignmentManager(System.Data.DataSet ds)
            : base(ds)
        {

        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSCapacityAssignment);
        }

        protected override void InitAdapter()
        {

            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TS.CapacityAssignment";
            tableMapping.ColumnMappings.Add("CapacityAssignmentId", "CapacityAssignmentId");
            tableMapping.ColumnMappings.Add("Year", "Year");
            tableMapping.ColumnMappings.Add("Stage", "Stage");
            tableMapping.ColumnMappings.Add("CapacityPrcnt", "CapacityPrcnt");
            tableMapping.ColumnMappings.Add("JobCountPrcnt", "JobCountPrcnt");
            tableMapping.ColumnMappings.Add("RemainIsWaste", "RemainIsWaste");
            tableMapping.ColumnMappings.Add("IsAssigned", "IsAssigned");
            tableMapping.ColumnMappings.Add("AssignmentDate", "AssignmentDate");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("EndDate", "EndDate");
            tableMapping.ColumnMappings.Add("CurrentJ", "CurrentJ");
            tableMapping.ColumnMappings.Add("EndDateOtherAgents", "EndDateOtherAgents");
            tableMapping.ColumnMappings.Add("AssignmentDateOtherAgent", "AssignmentDateOtherAgent");
            tableMapping.ColumnMappings.Add("WorkCountUnder400OtherAgents", "WorkCountUnder400OtherAgents");
            tableMapping.ColumnMappings.Add("WorkCountOtherAgents", "WorkCountOtherAgents");
            tableMapping.ColumnMappings.Add("FreeDesCapacityOtherAgents", "FreeDesCapacityOtherAgents");
            tableMapping.ColumnMappings.Add("FreeObsCapacityOtherAgents", "FreeObsCapacityOtherAgents");
            tableMapping.ColumnMappings.Add("WorkCountUnder400MainAgent", "WorkCountUnder400MainAgent");
            tableMapping.ColumnMappings.Add("WorkCountMainAgent", "WorkCountMainAgent");
            tableMapping.ColumnMappings.Add("FreeDesCapacityMainAgent", "FreeDesCapacityMainAgent");
            tableMapping.ColumnMappings.Add("FreeObsCapacityMainAgent", "FreeObsCapacityMainAgent");
            tableMapping.ColumnMappings.Add("StopmandatoryFileUploadingMainAgent", "StopmandatoryFileUploadingMainAgent");
            tableMapping.ColumnMappings.Add("StopmandatoryFileUploadingOtherAgent", "StopmandatoryFileUploadingOtherAgent");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSCapacityAssignment";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@CapacityAssignmentId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Year", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@Stage", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@LastStage", System.Data.SqlDbType.Bit);
            this.Adapter.SelectCommand.Parameters.Add("@ThisStage", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CurrentStage", System.Data.SqlDbType.Bit);
            this.Adapter.SelectCommand.Parameters.Add("@AllCurrentStages", System.Data.SqlDbType.Bit);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSCapacityAssignment";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CapacityAssignmentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityAssignmentId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSCapacityAssignment";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Year", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Year", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Stage", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Stage", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityPrcnt", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityPrcnt", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@JobCountPrcnt", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "JobCountPrcnt", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainIsWaste", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainIsWaste", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsAssigned", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsAssigned", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AssignmentDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AssignmentDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentJ", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentJ", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FreeObsCapacityMainAgent", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FreeObsCapacityMainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FreeDesCapacityMainAgent", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FreeDesCapacityMainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkCountMainAgent", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkCountMainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkCountUnder400MainAgent", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkCountUnder400MainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FreeObsCapacityOtherAgents", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FreeObsCapacityOtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FreeDesCapacityOtherAgents", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FreeDesCapacityOtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkCountOtherAgents", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkCountOtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkCountUnder400OtherAgents", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkCountUnder400OtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AssignmentDateOtherAgent", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AssignmentDateOtherAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndDateOtherAgents", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndDateOtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StopmandatoryFileUploadingMainAgent", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StopmandatoryFileUploadingMainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StopmandatoryFileUploadingOtherAgent", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StopmandatoryFileUploadingOtherAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSCapacityAssignment";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Year", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Year", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Stage", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Stage", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityPrcnt", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityPrcnt", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@JobCountPrcnt", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "JobCountPrcnt", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainIsWaste", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainIsWaste", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsAssigned", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsAssigned", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AssignmentDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AssignmentDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CurrentJ", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CurrentJ", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_CapacityAssignmentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityAssignmentId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityAssignmentId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityAssignmentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FreeObsCapacityMainAgent", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FreeObsCapacityMainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FreeDesCapacityMainAgent", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FreeDesCapacityMainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkCountMainAgent", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkCountMainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkCountUnder400MainAgent", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkCountUnder400MainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FreeObsCapacityOtherAgents", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FreeObsCapacityOtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FreeDesCapacityOtherAgents", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FreeDesCapacityOtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkCountOtherAgents", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkCountOtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkCountUnder400OtherAgents", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkCountUnder400OtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AssignmentDateOtherAgent", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AssignmentDateOtherAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndDateOtherAgents", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndDateOtherAgents", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StopmandatoryFileUploadingMainAgent", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StopmandatoryFileUploadingMainAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StopmandatoryFileUploadingOtherAgent", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StopmandatoryFileUploadingOtherAgent", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSCapacityAssignmentDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCapacityAssignmentId(int CapacityAssignmentId)
        {
            ResetAllParameters();
            this.DataTable.Clear();
            this.Adapter.SelectCommand.Parameters["@CapacityAssignmentId"].Value = CapacityAssignmentId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByYear(string Year)
        {
            ResetAllParameters();
            this.DataTable.Clear();
            this.Adapter.SelectCommand.Parameters["@Year"].Value = Year;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByYearAndStage(string Year, int Stage)
        {
            ResetAllParameters();
            this.DataTable.Clear();
            this.Adapter.SelectCommand.Parameters["@Year"].Value = Year;
            this.Adapter.SelectCommand.Parameters["@Stage"].Value = Stage;
            Fill();
        }

        public DataTable FindLastStageInYear(string Year)
        {
            ResetAllParameters();
            this.DataTable.Clear();
            this.Adapter.SelectCommand.Parameters["@Year"].Value = Year;
            this.Adapter.SelectCommand.Parameters["@LastStage"].Value = 1;
            Fill();
            return this.DataTable;
        }

        public int GetNextStageNum(string Year)
        {
            DataTable dt = FindLastStageInYear(Year);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["Stage"]) + 1;
            else
                return 1;
        }

        private DataTable SelectPrcntsSum(string Year, int CapacityAssignmentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSCapacityAssignmentPrcntsSum", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@Year", Year);
            adapter.SelectCommand.Parameters.AddWithValue("@CapacityAssignmentId", CapacityAssignmentId);
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum
        /// </summary>        
        public ArrayList GetPrcntsSum(string Year)
        {
            ArrayList SumArr = new ArrayList();
            DataTable dt = SelectPrcntsSum(Year, -1);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CapacityPrcntSum"] != DBNull.Value)
                    SumArr.Add(Convert.ToInt32(dt.Rows[0]["CapacityPrcntSum"]));
                else
                    SumArr.Add(0);

                if (dt.Rows[0]["JobCountPrcntSum"] != DBNull.Value)
                    SumArr.Add(Convert.ToInt32(dt.Rows[0]["JobCountPrcntSum"]));
                else
                    SumArr.Add(0);
            }
            else
            {
                SumArr.Add(0);
                SumArr.Add(0);
            }

            return SumArr;
        }

        /// <summary>
        /// ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum
        /// </summary>        
        public ArrayList GetPrcntsSum(string Year, int CapacityAssignmentId)
        {
            ArrayList SumArr = new ArrayList();
            DataTable dt = SelectPrcntsSum(Year, CapacityAssignmentId);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CapacityPrcntSum"] != DBNull.Value)
                    SumArr.Add(Convert.ToInt32(dt.Rows[0]["CapacityPrcntSum"]));
                else
                    SumArr.Add(0);

                if (dt.Rows[0]["JobCountPrcntSum"] != DBNull.Value)
                    SumArr.Add(Convert.ToInt32(dt.Rows[0]["JobCountPrcntSum"]));
                else
                    SumArr.Add(0);
            }
            else
            {
                SumArr.Add(0);
                SumArr.Add(0);
            }

            return SumArr;
        }

        public DataTable FindPrvStagesInYear(string Year, int ThisStage)
        {
            ResetAllParameters();
            this.DataTable.Clear();
            this.Adapter.SelectCommand.Parameters["@Year"].Value = Year;
            this.Adapter.SelectCommand.Parameters["@ThisStage"].Value = ThisStage;
            Fill();
            return this.DataTable;
        }

        public bool PrvIsAssigned(string Year, int ThisStage)
        {
            DataTable dt = FindPrvStagesInYear(Year, ThisStage);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(dt.Rows[i]["IsAssigned"]))
                    return false;
            }
            return true;
        }

        public DataTable FindCurrentCapacityAssignment()
        {
            ResetAllParameters();
            this.DataTable.Clear();
            this.Adapter.SelectCommand.Parameters["@CurrentStage"].Value = 1;
            Fill();
            return this.DataTable;
        }

        public DataTable FindAllCurrentCapacityAssignments()
        {
            ResetAllParameters();
            this.DataTable.Clear();
            this.Adapter.SelectCommand.Parameters["@AllCurrentStages"].Value = 1;
            Fill();
            return this.DataTable;
        }

        private DataTable SelectCurrentPrcntsSum()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSCapacityAssignmentPrcntsSum", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@CurrentSum", 1);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable SelectCurrentYearAndStage(Int16 IsMainAgent)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSCapacityAssignmentCurrentYear", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@IsMainAgent", IsMainAgent);
            adapter.SelectCommand.Parameters.AddWithValue("@DateOfToday", Utility.GetDateOfToday());
            adapter.Fill(this.DataTable);
            return this.DataTable;
        }

        /// <summary>
        /// ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum
        /// </summary>        
        public ArrayList GetCurrentPrcntsSum()
        {
            ArrayList SumArr = new ArrayList();
            DataTable dt = SelectCurrentPrcntsSum();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CapacityPrcntSum"] != DBNull.Value)
                    SumArr.Add(Convert.ToInt32(dt.Rows[0]["CapacityPrcntSum"]));
                else
                    SumArr.Add(0);

                if (dt.Rows[0]["JobCountPrcntSum"] != DBNull.Value)
                    SumArr.Add(Convert.ToInt32(dt.Rows[0]["JobCountPrcntSum"]));
                else
                    SumArr.Add(0);
            }
            else
            {
                SumArr.Add(0);
                SumArr.Add(0);
            }

            return SumArr;
        }

        private DataTable SelectPrcntsSumByDate(string Date)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSCapacityAssignmentPrcntsSum", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            string Year = "";
            if (!String.IsNullOrEmpty(Date))
                Year = Date.Substring(0, 4);

            adapter.SelectCommand.Parameters.AddWithValue("@Year", Year);
            adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);
            adapter.SelectCommand.Parameters.AddWithValue("@SumByDate", 1);
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum
        /// </summary>        
        public ArrayList GetPrcntsSumByDate(string Date)
        {
            ArrayList SumArr = new ArrayList();
            DataTable dt = SelectPrcntsSumByDate(Date);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CapacityPrcntSum"] != DBNull.Value)
                    SumArr.Add(Convert.ToInt32(dt.Rows[0]["CapacityPrcntSum"]));
                else
                    SumArr.Add(0);

                if (dt.Rows[0]["JobCountPrcntSum"] != DBNull.Value)
                    SumArr.Add(Convert.ToInt32(dt.Rows[0]["JobCountPrcntSum"]));
                else
                    SumArr.Add(0);
            }
            else
            {
                SumArr.Add(0);
                SumArr.Add(0);
            }

            return SumArr;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSCapacityAssignmentYears(Int16 IsMainAgent)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSCapacityAssignmentYears", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@IsMainAgent", IsMainAgent);
            adapter.SelectCommand.Parameters.AddWithValue("@DateOfToday", Utility.GetDateOfToday());
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return dt;
        }
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //public DataTable SelectYears()
        //{
        //    DataTable dt = SelectTSCapacityAssignmentYears();

        //    string YearOfToday = GetYearOfToday();
        //    DataRow[] Rows = dt.Select("Year=" + YearOfToday);
        //    if (Rows.Length == 0)
        //    {
        //        DataRow row = dt.NewRow();
        //        row["Year"] = YearOfToday;
        //        dt.Rows.Add(row);
        //    }

        //    return dt;
        //}        

        public static string GetYearOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today).ToString();
            return PersianDate;
        }

        public DataTable SelectTSCapacityAssignmentByDate(string Date)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSCapacityAssignmentByDate", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@Date", Date);
            adapter.Fill(dt);
            return dt;
        }
        
    }
}
