using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class WorkFlowStateObserverWorkManager : BaseObject
    {
        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblWorkFlowStateObserverWork";
            tableMapping.ColumnMappings.Add("StateId", "StateId");
            tableMapping.ColumnMappings.Add("TaskId", "TaskId");
            tableMapping.ColumnMappings.Add("TableId", "TableId");
            tableMapping.ColumnMappings.Add("NmcIdType", "NmcIdType");
            tableMapping.ColumnMappings.Add("NmcId", "NmcId");
            tableMapping.ColumnMappings.Add("SubOrder", "SubOrder");
            tableMapping.ColumnMappings.Add("StateType", "StateType");
            tableMapping.ColumnMappings.Add("UpdateTableType", "UpdateTableType");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("PriorityId", "PriorityId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LasttimeStamp", "LasttimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectWorkFlowStateObserverWork";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_StateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StateId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LasttimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LasttimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));


            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.DeleteWorkFlowStateObserverWork";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_StateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StateId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LasttimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LasttimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.InsertWorkFlowStateObserverWork";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TaskId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TaskId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NmcIdType", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NmcIdType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NmcId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NmcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SubOrder", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SubOrder", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StateType", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StateType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UpdateTableType", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UpdateTableType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriorityId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.UpdateWorkFlowStateObserverWork";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TaskId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TaskId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TableId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TableId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NmcIdType", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NmcIdType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NmcId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NmcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SubOrder", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SubOrder", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StateType", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StateType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UpdateTableType", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UpdateTableType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Date", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Date", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PriorityId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PriorityId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_StateId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StateId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LasttimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LasttimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StateId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "StateId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.tblWorkFlowStateObserverWorkDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastStateByWfCode(int WorkFlowCode, int TableId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "SelectWorkFlowStateObserverWorkLastStateByWfCode";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;

            adapter.Fill(dt);
            return (dt);
        }

        public int SendDocToNextStep(int TableId, int TaskId, string Description, int NmcId, int NmcIdType, int CurrentUserId, int PriorityId, string ExpireDate)
        {
            DataRow WorkFlowRow = this.NewRow();
            WorkFlowRow["TaskId"] = TaskId;
            WorkFlowRow["TableId"] = TableId;
            if (NmcIdType != -1)
                WorkFlowRow["NmcIdType"] = NmcIdType;
            WorkFlowRow["NmcId"] = NmcId;
            WorkFlowRow["SubOrder"] = 1;
            WorkFlowRow["StateType"] = 0;
            WorkFlowRow["Description"] = Description;
            WorkFlowRow["Date"] = Utility.GetDateOfToday();
            WorkFlowRow["UserId"] = CurrentUserId;
            WorkFlowRow["InActive"] = 0;
            if (PriorityId != -1)
                WorkFlowRow["PriorityId"] = PriorityId;
            if (!string.IsNullOrEmpty(ExpireDate))
                WorkFlowRow["ExpireDate"] = ExpireDate;
            WorkFlowRow["ModifiedDate"] = DateTime.Now;

            this.AddRow(WorkFlowRow);
            if (this.Save() <= 0)
            {
                return ((int)WorkFlowStateManager.Errors.Erorr);
            }
            this.DataTable.AcceptChanges();
            return Convert.ToInt32(this[0]["StateId"]);
        }

        public int InsertWorkFlowViewState(int TableType, int TableId, string Description, int CurrentUserId, int NmcId, int NmcIdType)
        {
            TSP.DataManager.LoginManager LoginManager = new LoginManager();
            TSP.DataManager.NezamChartManager NezamChartManager = new NezamChartManager();
            DataTable dtWorkFlowState = this.SelectLastStateByWfCode((int)WorkFlows.ObservationDocumentConfirming, TableId);
            if (dtWorkFlowState.Rows.Count <= 0)
                return -1;
            int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
            if (NmcIdType != (int)UserType.Member)
            {
                NmcId = NezamChartManager.FindNmcId(CurrentUserId, CurrentUserId, new TSP.DataManager.LoginManager());
                if (NmcId == -1)
                {
                    LoginManager.FindByCode(CurrentUserId);
                    if (LoginManager.Count <= 0)
                    {
                        return -1;
                    }
                    int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                    int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
                    NezamChartManager.FindByEmpId(EmpId, UltId, 0);
                    if (NezamChartManager.Count <= 0)
                        return -1;
                    NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
                }
            }
            DataRow WorkFlowRow = this.NewRow();

            WorkFlowRow["TaskId"] = CurrentTaskId;
            WorkFlowRow["TableId"] = TableId;
            WorkFlowRow["NmcId"] = NmcId;
            WorkFlowRow["NmcIdType"] = NmcIdType;
            WorkFlowRow["SubOrder"] = 1;
            WorkFlowRow["StateType"] = (int)WorkFlowStateType.ViewInfo;
            WorkFlowRow["Description"] = Description;
            WorkFlowRow["InActive"] = 0;
            WorkFlowRow["Date"] = Utility.GetDateOfToday();
            WorkFlowRow["UserId"] = CurrentUserId;
            WorkFlowRow["ModifiedDate"] = DateTime.Now;

            this.AddRow(WorkFlowRow);
            int cnt = this.Save();
            if (cnt > 0)
            {
                return cnt;
            }
            else
            {
                return ((int)WorkFlowStateManager.Errors.Erorr);
            }
        }


        public int InsertWorkFlowState(int TableType, int TableId, int TaskId, string Description, int NmcId, int NmcIdType, int CurrentUserId, int PriorityId, string ExpireDate)
        {
            DataRow WorkFlowRow = this.NewRow();
            WorkFlowRow["TaskId"] = TaskId;
            WorkFlowRow["TableId"] = TableId;
            WorkFlowRow["NmcId"] = NmcId;
            WorkFlowRow["NmcIdType"] = NmcIdType;
            WorkFlowRow["SubOrder"] = 1;
            WorkFlowRow["StateType"] = 0;
            WorkFlowRow["InActive"] = 0;            
            WorkFlowRow["Description"] = Description;
            WorkFlowRow["Date"] = Utility.GetDateOfToday();
            WorkFlowRow["PriorityId"] = PriorityId;
            WorkFlowRow["ExpireDate"] = ExpireDate;
            WorkFlowRow["UserId"] = CurrentUserId;
            WorkFlowRow["ModifiedDate"] = DateTime.Now;

            this.AddRow(WorkFlowRow);
            int CountSave = this.Save();
            if (CountSave > 0)
            {
                return (CountSave);
            }
            else
            {
                return ((int)WorkFlowStateManager.Errors.Erorr);
            }

        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectWorkFlowStateReportForTSWorkRequest(int TableId, int WfCode)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "SelectWorkFlowStateReportForTSWorkRequest";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WfCode;
            adapter.Fill(dt);
            return (dt);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WorkFlowCode"></param>
        /// <param name="TableId"></param>
        /// <param name="UpdateTableType">جدولی از پرونده که بروزرسانی شده است.از این فیلد برای ثبت تاریحچه ویرایش پرونده استفاده می شود.مثلا مدرک تحصیلی در پرونده عضویت.</param>
        /// <param name="Description"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int InsertWorkFlowStateLog(int WorkFlowCode, int TableId, int UpdateTableType, string Description, int CurrentUserId, WorkFlowStateType WorkFlowStateType)
        {
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            TSP.DataManager.LoginManager LoginManager = new LoginManager();
            DataTable dtWorkFlowState = this.SelectLastStateByWfCode(WorkFlowCode, TableId);
            if (dtWorkFlowState.Rows.Count <= 0)
                return -1;
            int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
            int NmcId = -1;
            int NmcIdType = -1;
            LoginManager.FindByCode(CurrentUserId);
            if (LoginManager.Count <= 0)
            {
                return -1;
            }
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId, 0);
            if (NezamChartManager.Count > 0)
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            switch (UltId)
            {
                case (int)UserType.Employee:
                case (int)UserType.Institute:
                case (int)UserType.Settlement:
                    NmcIdType = (int)WorkFlowStateNmcIdType.NmcId;
                    break;
                case (int)UserType.Member:
                    NmcId = EmpId;
                    NmcIdType = (int)WorkFlowStateNmcIdType.MeId;
                    break;
            }
            DataRow WorkFlowRow = this.NewRow();

            WorkFlowRow["TaskId"] = CurrentTaskId;
            WorkFlowRow["TableId"] = TableId;
            WorkFlowRow["NmcId"] = NmcId;
            WorkFlowRow["NmcIdType"] = NmcIdType;
            WorkFlowRow["SubOrder"] = 1;
            WorkFlowRow["StateType"] = (int)WorkFlowStateType;
            if (UpdateTableType != -1)
                WorkFlowRow["UpdateTableType"] = UpdateTableType;
            WorkFlowRow["Description"] = Description;
            WorkFlowRow["Date"] = this.GetDateOfToday();
            WorkFlowRow["UserId"] = CurrentUserId;
            WorkFlowRow["ModifiedDate"] = DateTime.Now;
            WorkFlowRow["InActive"] = 0;
            this.AddRow(WorkFlowRow);
            int cnt = this.Save();
            if (cnt > 0)
            {
                return cnt;
            }
            else
            {
                return 0;
            }
        }

        //public int InsertWorkFlowViewState(int TableType, int TableId, string Description, int CurrentUserId)
        //{
        //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //    TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
        //    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        //    TSP.DataManager.LoginManager LoginManager = new LoginManager();
        //    //  TaskDoerManager.ClearBeforeFill = true;
        //    DataTable dtWorkFlowState = this.SelectLastState(TableType, TableId);
        //    if (dtWorkFlowState.Rows.Count <= 0)
        //        return -1;
        //    int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());

        //    //TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        //    //TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        //    int NmcId = FindNmcIdByTaskId(CurrentTaskId, CurrentUserId, new TSP.DataManager.NezamChartManager(), new TSP.DataManager.LoginManager());
        //    if (NmcId == -1)
        //    {
        //        LoginManager.FindByCode(CurrentUserId);
        //        if (LoginManager.Count <= 0)
        //        {
        //            return -1;
        //        }
        //        int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
        //        int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
        //        NezamChartManager.FindByEmpId(EmpId, UltId, 0);
        //        if (NezamChartManager.Count <= 0)
        //            return -1;
        //        NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
        //    }

        //    DataRow WorkFlowRow = this.NewRow();

        //    WorkFlowRow["TaskId"] = CurrentTaskId;
        //    WorkFlowRow["TableId"] = TableId;
        //    WorkFlowRow["NmcId"] = NmcId;
        //    WorkFlowRow["SubOrder"] = 1;
        //    WorkFlowRow["StateType"] = (int)WorkFlowStateType.ViewInfo;
        //    WorkFlowRow["Description"] = Description;
        //    WorkFlowRow["Date"] = this.GetDateOfToday();
        //    WorkFlowRow["UserId"] = CurrentUserId;
        //    WorkFlowRow["ModifiedDate"] = DateTime.Now;

        //    this.AddRow(WorkFlowRow);
        //    int cnt = this.Save();
        //    if (cnt > 0)
        //    {
        //        return cnt;
        //    }
        //    else
        //    {
        //        return ((int)Errors.Erorr);
        //    }
        //}
    }
}
