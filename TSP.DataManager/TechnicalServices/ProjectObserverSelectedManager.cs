using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TSP.DataManager.TechnicalServices
{
    public class ProjectObserverSelectedManager : BaseObject
    {
        #region Permissions
        public static Permission GetUserPermissionReportObserverSelected(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ReportObserverSelected);
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectObserverSelected);
        }
        #endregion
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSProjectObserverSelected";
            tableMapping.ColumnMappings.Add("ProjectObserverSelectedId", "ProjectObserverSelectedId");
            tableMapping.ColumnMappings.Add("ObsWorkReqId", "ObsWorkReqId");
            tableMapping.ColumnMappings.Add("ReasonType", "ReasonType");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("ProjectRequestId", "ProjectRequestId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("MeType", "MeType");
            tableMapping.ColumnMappings.Add("RandomMembersList", "RandomMembersList");
            tableMapping.ColumnMappings.Add("IsObserverConfirmed", "IsObserverConfirmed");
            tableMapping.ColumnMappings.Add("CapacityDecrement", "CapacityDecrement");
            tableMapping.ColumnMappings.Add("PercentOfPrjFundation", "PercentOfPrjFundation");
            tableMapping.ColumnMappings.Add("ProjectIngridientMajorsId", "ProjectIngridientMajorsId");
            tableMapping.ColumnMappings.Add("ProjectObserversId", "ProjectObserversId");
            tableMapping.ColumnMappings.Add("RejectDate", "RejectDate");
            tableMapping.ColumnMappings.Add("PlusOneGroupMajor", "PlusOneGroupMajor");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectTSProjectObserverSelected";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectRequestId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectObserverSelectedId", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.DeleteTSProjectObserverSelected";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ProjectObserverSelectedId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectObserverSelectedId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.InsertTSProjectObserverSelected";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsWorkReqId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ObsWorkReqId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ReasonType", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "ReasonType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ProjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectRequestId", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectRequestId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeType", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "MeType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RandomMembersList", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "RandomMembersList", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsObserverConfirmed", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "IsObserverConfirmed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityDecrement", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CapacityDecrement", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PercentOfPrjFundation", global::System.Data.SqlDbType.Float, 8, global::System.Data.ParameterDirection.Input, 53, 0, "PercentOfPrjFundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectIngridientMajorsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ProjectIngridientMajorsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectObserversId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ProjectObserversId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RejectDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "RejectDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PlusOneGroupMajor", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PlusOneGroupMajor", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.UpdateTSProjectObserverSelected";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObsWorkReqId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ObsWorkReqId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ReasonType", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "ReasonType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ProjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectRequestId", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectRequestId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "MeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MeType", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "MeType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RandomMembersList", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "RandomMembersList", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsObserverConfirmed", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "IsObserverConfirmed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityDecrement", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CapacityDecrement", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PercentOfPrjFundation", global::System.Data.SqlDbType.Float, 8, global::System.Data.ParameterDirection.Input, 53, 0, "PercentOfPrjFundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectIngridientMajorsId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ProjectIngridientMajorsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ProjectObserverSelectedId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ProjectObserverSelectedId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectObserverSelectedId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ProjectObserverSelectedId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectObserversId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ProjectObserversId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RejectDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "RejectDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PlusOneGroupMajor", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PlusOneGroupMajor", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSProjectObserverSelectedDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        public void FindByCode(int ProjectObserverSelectedId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectObserverSelectedId"].Value = ProjectObserverSelectedId;
            Fill();
        }


        public void SearchObserverSelected(int MeId, int ProjectId, int ProjectRequestId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@ProjectRequestId"].Value = ProjectRequestId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage(int MeId, int ProjectId, int AgentId)
        {
            return SearchForManagmentPage(MeId, ProjectId, AgentId, "%", "%");
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchForManagmentPage(int MeId, int ProjectId, int AgentId, string CreateDateTo, string CreateDateFrom)
        {
            if (MeId == -1 && ProjectId == -1 && AgentId == -1 && CreateDateTo == "%" && CreateDateFrom == "%")
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserverSelectedForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@CreateDateTo", CreateDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@CreateDateFrom", CreateDateFrom);
            adapter.Fill(dt);

            return dt;
        }
        public double ObserverSelectedAssignedFundationForFunctionA(string MajorList, int ProjectId)
        {
            return ObserverSelectedAssignedFundationForFunctionA(MajorList, ProjectId, -1);
        }
        public double ObserverSelectedAssignedFundationForFunctionA(string MajorList, int ProjectId, int ProjectReqId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserverSelectedAssignedFundationForFunctionA", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MajorList", MajorList);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjObsTableType", TableTypeManager.FindTtId(TableType.TSProject_Observers));
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectReqId", ProjectReqId);
            adapter.Fill(dt);

            if (dt.Rows.Count != 0 && !Utility.IsDBNullOrNullValue(dt.Rows[0]["SumAssignedCapacityDecrement"]))
                return Convert.ToDouble(dt.Rows[0]["SumAssignedCapacityDecrement"]);
                return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MajorList"></param>
        /// <param name="ProjectId"></param>
        /// <param name="ProjectReqId"></param>
        /// <param name="IsMother">-1: All , 1 : ناظر هماهنگ کننده می باشد</param>
        /// <returns></returns>
        public double SelectTSProjectObserverAssignedFundationForFish(string MajorList, int ProjectId, int ProjectReqId,int IsMother)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserverAssignedFundationForFish", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@MajorList", MajorList);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjObsTableType", TableTypeManager.FindTtId(TableType.TSProject_Observers));
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectReqId", ProjectReqId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsMother", IsMother);            
            adapter.Fill(dt);

            if (dt.Rows.Count != 0 && !Utility.IsDBNullOrNullValue(dt.Rows[0]["SumAssignedCapacityDecrement"]))
                return Convert.ToDouble(dt.Rows[0]["SumAssignedCapacityDecrement"]);
            return 0;
        }

        

        public string SelectTSProjectObserverSelectedMemberForFunctionA(int ProjectId)
        {
            string MeIdList = "";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserverSelectedMemberForFunctionA", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MeIdList = dt.Rows[0]["MeIdList"].ToString();
            }
            return MeIdList;
        }

        public int SelectObserverSelectedMajorOnePlusCount(int ProjectId, int ProjectIngridientMajorsId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserverSelectedMajorOnePlusCount", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectIngridientMajorsId", ProjectIngridientMajorsId);
            adapter.Fill(dt);

            return Convert.ToInt32(dt.Rows[0]["count"]);
        }

        public int SelectObserverSelectedCount(int ProjectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectObserverSelectedCount", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);

            adapter.Fill(dt);

            return Convert.ToInt32(dt.Rows[0]["count"]);
        }
        public int SelectAvrageConsumedByCity(int MjParentId, int ObsId, int CitId1, int CitId2, int Avg1, int Avg2)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("AvrageConsumedByCity", this.Connection);
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjObsTableType", TableTypeManager.FindTtId(TableType.TSProject_Observers));
            adapter.SelectCommand.Parameters.AddWithValue("@MjParentId", MjParentId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsId", ObsId);
            adapter.SelectCommand.Parameters.AddWithValue("@CitId1", CitId1);
            adapter.SelectCommand.Parameters.AddWithValue("@CitId2", CitId2);
            adapter.SelectCommand.Parameters.AddWithValue("@Avg1", Avg1);
            adapter.SelectCommand.Parameters.AddWithValue("@Avg2", Avg2);

            adapter.Fill(dt);

            return Convert.ToInt32(dt.Rows[0]["Value"]);
        }

    }
}
