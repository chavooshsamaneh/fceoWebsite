using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class WorkFlowStateManager : BaseObject
    {
        private TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager;
        private TSP.DataManager.TaskDoerManager TaskDoerManager;
        private TSP.DataManager.NezamChartManager NezamChartManager;
        private TSP.DataManager.LoginManager LoginManager;
        private TSP.DataManager.WorkFlowStateManager WorkFlowStateMng;
        // private TSP.DataManager.MessageManager MessageManager;
        //  private TSP.DataManager.MessageReceiverManager MessageReceiverManager;

        public string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }

        #region Constructors
        public WorkFlowStateManager()
        {
            WorkFlowTaskManager = new WorkFlowTaskManager();
        }

        public WorkFlowStateManager(TSP.DataManager.TransactionManager TransactionManager)
        {
            WorkFlowStateMng = this;
            WorkFlowTaskManager = new WorkFlowTaskManager();
            TaskDoerManager = new TaskDoerManager();
            NezamChartManager = new NezamChartManager();
            LoginManager = new LoginManager();
            //   MessageManager = new MessageManager();
            //   MessageReceiverManager = new MessageReceiverManager();

            if (TransactionManager != null)
            {
                TransactionManager.Add(WorkFlowTaskManager);
                TransactionManager.Add(TaskDoerManager);
                TransactionManager.Add(NezamChartManager);
                TransactionManager.Add(LoginManager);
                TransactionManager.Add(WorkFlowTaskManager);
                //  TransactionManager.Add(MessageManager);
                //  TransactionManager.Add(MessageReceiverManager);
            }
        }
        #endregion

        public enum Errors
        {
            YouCanNotSend = -1, ProcessEnd = -2, NoTaskFind = -3, Erorr = -4, NoStateFound = -5, CannotSendToCurrentState = -6, SendedToNextStep = -7,
            NoTaskDoerFind = -8, UnKnownSendBackTask = -9
        }
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.WorkFlowState);
        }

        public static Permission GetUserPermissionForWFUserTaskTrace(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.WFUserTaskTrace);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblWorkFlowState";
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
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LasttimeStamp", "LasttimeStamp");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("PriorityId", "PriorityId");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectWorkFlowState";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "StateId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskCode", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteWorkFlowState";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_StateId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "StateId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LasttimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LasttimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertWorkFlowState";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NmcIdType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "NmcIdType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NmcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NmcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SubOrder", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SubOrder", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "StateType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdateTableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UpdateTableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PriorityId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PriorityId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateWorkFlowState";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TableId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TableId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NmcIdType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "NmcIdType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NmcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NmcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SubOrder", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SubOrder", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "StateType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdateTableType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UpdateTableType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_StateId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "StateId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LasttimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LasttimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "StateId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PriorityId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PriorityId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.WorkFlowDataSet.tblWorkFlowStateDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int StateId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectWorkFlowState";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "StateId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskCode", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters["@StateId"].Value = StateId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByTaskId(int TaskId)
        {
            this.Adapter.SelectCommand.Parameters["@TaskId"].Value = TaskId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByTaskCode(int TaskCode)
        {
            this.Adapter.SelectCommand.Parameters["@TaskCode"].Value = TaskCode;
            Fill();
        }

        #region spSelectWorkFlowStateByTableType

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectWorkFlowStateByTableType(int TableType, int TableId, int StateType, int WorkFlowCode, Boolean FillMainDataTable)
        {
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectWorkFlowStateByTableType";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;
            adapter.SelectCommand.Parameters.Add("@StateType", SqlDbType.Int, 4, "StateType").Value = StateType;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
            if (FillMainDataTable)
            {
                adapter.Fill(DataTable);
                return (DataTable);
            }
            else
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return (dt);
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectWorkFlowStateByTableType(int TableType, int TableId, int StateType, int WorkFlowCode)
        {

            return SelectWorkFlowStateByTableType(TableType, TableId, StateType, WorkFlowCode, false);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByTableType(int TableType, int TableId)
        {
            return SelectWorkFlowStateByTableType(TableType, TableId, -1, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByWorkFlowCode(int WorkFlowCode, int TableId)
        {
            return SelectWorkFlowStateByTableType(-1, TableId, -1, WorkFlowCode);
        }

        public void SelectByWorkFlowCodeForDelete(int WorkFlowCode, int TableId)
        {
            SelectWorkFlowStateByTableType(-1, TableId, -1, WorkFlowCode, true);
        }
        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectStateReports(int TableId, int TableType, int NmcId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectWorkFlowStateReport";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@NmcId", SqlDbType.Int, 4, "NmcId").Value = NmcId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectStateReportsById(int TableId, int TableType, int WfCode, int NmcId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectWorkFlowStateReportByWfCode";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WfCode;
            adapter.SelectCommand.Parameters.Add("@NmcId", SqlDbType.Int, 4, "NmcId").Value = NmcId;

            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectWorkFlowStateReportForTSPlansConfirming(int TableId, int WfCode)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "SelectWorkFlowStateReportForTSPlansConfirming";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WfCode;
            adapter.Fill(dt);
            return (dt);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectStateReportsByWfCode(int TableId, int WfCode, int NmcId)
        {
            return (SelectStateReportsById(TableId, -1, WfCode, NmcId));
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectUserTasks(int UserId, int WfCode, int ShowAllTask)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "spSelectWorkFlowStateByTaskDoer";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int, 4, "UserId").Value = UserId;
            this.Adapter.SelectCommand.Parameters.Add("@WfCode", SqlDbType.Int, 4, "WfCode").Value = WfCode;
            this.Adapter.SelectCommand.Parameters.Add("@ShowAllTask", SqlDbType.Int, 4, "ShowAllTask").Value = ShowAllTask;

            Fill();
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDocumentTrace()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns["Id"].AutoIncrement = true;
            dt.Columns["Id"].AutoIncrementSeed = 1;

            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "spSelectWorkFlowStateByTaskDoer";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.CommandTimeout = 0;
            this.Adapter.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int, 4, "UserId").Value = -1;
            this.Adapter.SelectCommand.Parameters.Add("@WfCode", SqlDbType.Int, 4, "WfCode").Value = -1;
            this.Adapter.SelectCommand.Parameters.Add("@ShowAllTask", SqlDbType.Int, 4, "ShowAllTask").Value = 1;

            this.Adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastState(int TableType, int TableId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            if (adapter.SelectCommand.Transaction == null)
                adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectWorkFlowStateLastState";
            //new SqlDataAdapter("spSelectWorkFlowStateLastState", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = -1;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastState(int TableType, int TableId, int WorkFlowCode)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectWorkFlowStateLastState";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastState(int TableType, int TableId, int WorkFlowCode, int StateType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "spSelectWorkFlowStateLastState";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
            adapter.SelectCommand.Parameters.Add("@StateType", SqlDbType.Int, 4, "StateType").Value = StateType;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastStateByWfCode(int WorkFlowCode, int TableId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();

            switch (WorkFlowCode)
            {
                case (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming:
                    adapter.SelectCommand.CommandText = "SelectWorkFlowStateObserverWorkLastStateByWfCode";
                    break;
                default:
                    adapter.SelectCommand.CommandText = "spSelectWorkFlowStateLastStateByWfCode";
                    break;
            }

            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSendBackTask(int SendBackTask, int WorkFlowId, int OppositTaskId)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            DataTable dtSendBackTask = new DataTable();
            dtSendBackTask.Columns.Add("TaskId");
            dtSendBackTask.Columns.Add("TaskName");
            dtSendBackTask.Columns.Add("TaskOrder", typeof(int));
            dtSendBackTask.Columns.Add("TaskCode");
            dtSendBackTask.Columns.Add("Type");

            DataTable dtWorkFlowTask = WorkFlowTaskManager.SelectByWorkId(WorkFlowId, -1, -1, OppositTaskId, 1);
            DataRow CurrentRow;
            for (int i = 0; i < dtWorkFlowTask.Rows.Count; i++)
            {
                CurrentRow = dtWorkFlowTask.Rows[i];
                int TCode = int.Parse(CurrentRow["TCode"].ToString());
                if ((TCode &= SendBackTask) == int.Parse(CurrentRow["TCode"].ToString()))
                {
                    DataRow WorkFlowTask = dtSendBackTask.NewRow();
                    WorkFlowTask["TaskId"] = CurrentRow["TaskId"].ToString();
                    WorkFlowTask["TaskName"] = CurrentRow["TaskName"].ToString();
                    WorkFlowTask["TaskOrder"] = CurrentRow["TaskOrder"].ToString();
                    WorkFlowTask["TaskCode"] = CurrentRow["TaskCode"].ToString();
                    WorkFlowTask["Type"] = CurrentRow["Type"].ToString();

                    dtSendBackTask.Rows.Add(WorkFlowTask);
                }
            }
            // return dtWorkFlowTask;
            return dtSendBackTask;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSendBackTask(int SendBackTask, int WorkFlowId)
        {
            return SelectSendBackTask(SendBackTask, WorkFlowId, -1);
        }

        public string SelectEngOfficeWorkflowstateForWebServiceEsys(int TableId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectEngOfficeWorkflowstateForWebServiceEsys", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;

            adapter.Fill(dt);
            string TaskName = "";
            if (dt.Rows.Count != 0 && !Utility.IsDBNullOrNullValue(dt.Rows[0]["TaskName"]))
            {
                TaskName = dt.Rows[0]["TaskName"].ToString();
            }
            return TaskName;
            //SelectEngOfficeWorkflowstateForWebServiceEsys
            //SelectOfficeWorkflowstateForWebServiceEsys
        }

        public string SelectOfficeWorkflowstateForWebServiceEsys(int TableId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectOfficeWorkflowstateForWebServiceEsys", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableId", SqlDbType.Int, 4, "TableId").Value = TableId;

            adapter.Fill(dt);
            string TaskName = "";
            if (dt.Rows.Count != 0 && !Utility.IsDBNullOrNullValue(dt.Rows[0]["TaskName"]))
            {
                TaskName = dt.Rows[0]["TaskName"].ToString();
            }
            return TaskName;
        }
        #region StartWorkFlow
        /// <summary>
        /// This method starts workflow for the first time
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="TaskCode"></param>
        /// <param name="NmcId">if Doer in Nezamchart NmcId=NmcId ,if it is Member NmcId=MeId,if it is Office NmcId=OfId</param>
        /// <param name="CurrentUserId"></param>
        /// <param name="NmcIdType">if Doer in Nezamchart NmcIdType=0 ,if it is Member NmcIdType=1 , if it is Office NmcIdType=2, if it is Municipality NmcIdType=3</param>
        /// <returns></returns>
        public int StartWorkFlow(int TableId, int TaskCode, int NmcId, int CurrentUserId, int NmcIdType, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, string Description)
        {
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                DataRow StateRow = this.NewRow();
                StateRow["TaskId"] = TaskId;
                StateRow["TableId"] = TableId;
                StateRow["NmcIdType"] = NmcIdType;
                StateRow["NmcId"] = NmcId;
                StateRow["SubOrder"] = 1;
                StateRow["Description"] = Description;
                StateRow["StateType"] = 0;
                StateRow["Date"] = this.GetDateOfToday();
                StateRow["UpdateTableType"] = TableId;
                StateRow["UserId"] = CurrentUserId;
                StateRow["ModifiedDate"] = DateTime.Now;

                this.AddRow(StateRow);
                // int cn = this.Save();
                if (this.Save() > 0)
                {
                    return Convert.ToInt32(this.DataTable.Rows[0]["StateId"]);
                    //return Convert.ToInt32(this.DataTable.Rows[this.DataTable.Rows.Count - 1]["StateId"]);
                }
                else
                {
                    return ((int)Errors.Erorr);
                }
            }
            else
            {
                return ((int)Errors.NoTaskFind);
            }

        }

        /// <summary>
        /// This method starts workflow for the first time
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="TaskCode"></param>
        /// <param name="NmcId">if Doer in Nezamchart NmcId=NmcId ,if it is Member NmcId=MeId,if it is Office NmcId=OfId</param>
        /// <param name="CurrentUserId"></param>
        /// <param name="NmcIdType">if Doer in Nezamchart NmcIdType=0 ,if it is Member NmcIdType=1 , if it is Office NmcIdType=2, if it is Municipality NmcIdType=3</param>
        /// <returns></returns>
        public int StartWorkFlow(int TableId, int TaskCode, int NmcId, int CurrentUserId, int NmcIdType, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager)
        {
            return StartWorkFlow(TableId, TaskCode, NmcId, CurrentUserId, NmcIdType, WorkFlowTaskManager, "آغاز گردش کار");
        }

        /// <summary>
        /// This method starts workflow for the first time
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="TaskCode"></param>
        /// <param name="NmcId">if Doer in Nezamchart NmcId=NmcId ,if it is Member NmcId=MeId,if it is Office NmcId=OfId</param>
        /// <param name="CurrentUserId"></param>
        /// <param name="NmcIdType">if Doer in Nezamchart NmcIdType=0 ,if it is Member NmcIdType=1 , if it is Office NmcIdType=2, if it is Municipality NmcIdType=3</param>
        /// <returns></returns>
        public int StartWorkFlow(int TableId, int TaskCode, int NmcId, int CurrentUserId, int NmcIdType, string Description)
        {
            return StartWorkFlow(TableId, TaskCode, NmcId, CurrentUserId, NmcIdType, WorkFlowTaskManager, Description);
        }

        /// <summary>
        /// This method starts workflow for the first time
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="TaskCode"></param>
        /// <param name="NmcId">if Doer in Nezamchart NmcId=NmcId ,if it is Member NmcId=MeId,if it is Office NmcId=OfId</param>
        /// <param name="CurrentUserId"></param>
        /// <param name="NmcIdType">if Doer in Nezamchart NmcIdType=0 ,if it is Member NmcIdType=1 , if it is Office NmcIdType=2, if it is Municipality NmcIdType=3</param>
        /// <returns></returns>
        public int StartWorkFlow(int TableId, int TaskCode, int NmcId, int CurrentUserId, int NmcIdType)
        {
            return StartWorkFlow(TableId, TaskCode, NmcId, CurrentUserId, NmcIdType, WorkFlowTaskManager, "آغاز گردش کار");
        }

        public int StartWorkFlow(int TableId, int TaskCode, int NmcId, int CurrentUserId)
        {
            return StartWorkFlow(TableId, TaskCode, NmcId, CurrentUserId, 0, WorkFlowTaskManager, "آغاز گردش کار");
        }

        #endregion

        /// <summary>
        /// Finds the Tasks that user can send the Document to
        /// </summary>
        /// <param name="TableType">Table Type of Current WorkFlow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int CalculateSendBackTask(int TableType, int TableId, int CurrentUserId)
        {
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            TaskDoerManager.ClearBeforeFill = true;
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            DataTable dtWorkFlowState = this.SelectLastState(TableType, TableId);
            // int TaskDoerCount = 0;
            if (dtWorkFlowState.Rows.Count > 0)
            {
                int CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
                int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                int CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());
                int CurrentNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
                int CurrentWorkFlowCode = int.Parse(dtWorkFlowState.Rows[0]["WorkFlowCode"].ToString());
                int CurrentTaskType = int.Parse(dtWorkFlowState.Rows[0]["Type"].ToString());
                if (CurrentTaskType != 2 && CurrentTaskType != 3)
                {
                    DataTable dtNextTask = WorkFlowTaskManager.SelectNextSteps(TableType, CurrentTaskCode, CurrentWorkFlowCode);
                    if (dtNextTask.Rows.Count > 0)
                    {
                        int TaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
                        int WorkFlowId = int.Parse(dtNextTask.Rows[0]["WorkFlowId"].ToString());
                        int NextTaskCode = int.Parse(dtNextTask.Rows[0]["TaskCode"].ToString());
                        int CurrentTaskDoerId = -1;
                        CurrentTaskDoerId = TaskDoerManager.CheckWorkFlowPermissionForTask(CurrentTaskCode, CurrentSubOrder, CurrentUserId, false);
                        if (CurrentTaskDoerId > 0)//********If current task has an other Doer*********
                        {
                            TaskDoerManager.FindByCode(CurrentTaskDoerId);
                            int SendBackTask = 0;
                            if (TaskDoerManager.Count > 0)
                            {
                                SendBackTask = int.Parse(TaskDoerManager[0]["SendBackTask"].ToString());
                                if (SendBackTask != 0)
                                {
                                    return SendBackTask;
                                }
                                else
                                {
                                    return ((int)Errors.YouCanNotSend);
                                }
                            }
                            else
                            {
                                return ((int)Errors.YouCanNotSend);
                            }
                        }
                        else
                        {
                            int NextTaskDoerId = -1;
                            ////********
                            //NextTaskDoerId = TaskDoerManager.CheckWorkFlowPermissionForTask(NextTaskCode, 1, CurrentUserId, true);
                            NextTaskDoerId = TaskDoerManager.CheckWorkFlowPermissionForTask(CurrentTaskCode, 1, CurrentUserId, true);
                            if (NextTaskDoerId > 0)
                            {
                                TaskDoerManager.FindByCode(NextTaskDoerId);
                                int SendBackTask = 0;
                                if (TaskDoerManager.Count > 0)
                                {
                                    SendBackTask = int.Parse(TaskDoerManager[0]["SendBackTask"].ToString());
                                    if (SendBackTask != 0)
                                    {
                                        return SendBackTask;
                                    }
                                    else
                                    {
                                        return ((int)Errors.YouCanNotSend);
                                    }
                                }
                                else
                                {
                                    return ((int)Errors.YouCanNotSend);
                                }
                            }
                            else
                            {
                                return ((int)Errors.YouCanNotSend);
                            }
                        }
                    }
                    else
                    {
                        return ((int)Errors.ProcessEnd);
                    }
                }
                else
                {
                    return ((int)Errors.ProcessEnd);
                }
            }
            else
            {
                return ((int)Errors.NoTaskFind);
            }
        }

        /// <summary>
        /// Finds the Tasks that user can send the Document to
        /// </summary>
        /// <param name="WorkFlowCode">WorkFlowCode of Current WorkFlow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int CalculateSendBackTaskByWfCode(int WorkFlowCode, int TableId, int CurrentUserId)
        {
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            TaskDoerManager.ClearBeforeFill = true;
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            DataTable dtWorkFlowState = this.SelectLastStateByWfCode(WorkFlowCode, TableId);
            // int TaskDoerCount = 0;
            if (dtWorkFlowState.Rows.Count <= 0)
            {
                return ((int)Errors.NoTaskFind);
            }
            int CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            int CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowState.Rows[0]["WorkFlowCode"].ToString());
            int CurrentTableType = int.Parse(dtWorkFlowState.Rows[0]["TableType"].ToString());
            int CurrentTaskType = int.Parse(dtWorkFlowState.Rows[0]["Type"].ToString());
            if (CurrentTaskType != 2 && CurrentTaskType != 3)
            {
                DataTable dtNextTask = WorkFlowTaskManager.SelectNextSteps(CurrentTableType, CurrentTaskCode, CurrentWorkFlowCode);
                if (dtNextTask.Rows.Count > 0)
                {
                    int TaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
                    int WorkFlowId = int.Parse(dtNextTask.Rows[0]["WorkFlowId"].ToString());
                    int NextTaskCode = int.Parse(dtNextTask.Rows[0]["TaskCode"].ToString());
                    int CurrentTaskDoerId = -1;
                    CurrentTaskDoerId = TaskDoerManager.CheckWorkFlowPermissionForTask(CurrentTaskCode, CurrentSubOrder, CurrentUserId, false);
                    if (CurrentTaskDoerId > 0)//********If current task has an other Doer*********
                    {
                        TaskDoerManager.FindByCode(CurrentTaskDoerId);
                        int SendBackTask = 0;
                        if (TaskDoerManager.Count > 0)
                        {
                            SendBackTask = int.Parse(TaskDoerManager[0]["SendBackTask"].ToString());
                            if (SendBackTask != 0)
                            {
                                return SendBackTask;
                            }
                            else
                            {
                                return ((int)Errors.UnKnownSendBackTask);
                            }
                        }
                        else
                        {
                            return ((int)Errors.YouCanNotSend);
                        }
                    }
                    else
                    {
                        int NextTaskDoerId = -1;
                        ////********
                        //NextTaskDoerId = TaskDoerManager.CheckWorkFlowPermissionForTask(NextTaskCode, 1, CurrentUserId, true);
                        NextTaskDoerId = TaskDoerManager.CheckWorkFlowPermissionForTask(CurrentTaskCode, 1, CurrentUserId, true);
                        if (NextTaskDoerId > 0)
                        {
                            TaskDoerManager.FindByCode(NextTaskDoerId);
                            int SendBackTask = 0;
                            if (TaskDoerManager.Count > 0)
                            {
                                SendBackTask = int.Parse(TaskDoerManager[0]["SendBackTask"].ToString());
                                if (SendBackTask != 0)
                                {
                                    return SendBackTask;
                                }
                                else
                                {
                                    return ((int)Errors.UnKnownSendBackTask);
                                }
                            }
                            else
                            {
                                return ((int)Errors.YouCanNotSend);
                            }
                        }
                        else
                        {
                            return ((int)Errors.YouCanNotSend);
                        }
                    }
                }
                else
                {
                    return ((int)Errors.ProcessEnd);
                }
            }
            else
            {
                return ((int)Errors.ProcessEnd);
            }
        }

        #region SendDocToNextStep

        /// <summary>
        ///  Send Document To Next Step of workflow and Send Information Message for TaskDoers.Doer of current task can be from members
        /// </summary>
        /// <param name="TableType">TableType of current Workflow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="TaskId">Id of next step Task</param>
        /// <param name="Description"></param>
        /// <param name="NmcId">If currentuser is member NmcId=MeId if Offic NmcId=OfId else NmcId=NmcId</param>
        /// <param name="NmcIdType">If current user is Employee NmcIdType=0,if user is member NmcIdType=1, if is Office NmcIdType=2, if it is Municipality NmcIdType=3 </param>
        /// <param name="CurrentUserId"></param>
        /// <param name="MsgContent"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public int SendDocToNextStep(int TableType, int TableId, int TaskId, string Description, int NmcId, int NmcIdType, int CurrentUserId, string MsgContent, String Url, int PriorityId, string ExpireDate)
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
            WorkFlowRow["Date"] = this.GetDateOfToday();
            WorkFlowRow["UserId"] = CurrentUserId;
            if (PriorityId != -1)
                WorkFlowRow["PriorityId"] = PriorityId;
            if (!string.IsNullOrEmpty(ExpireDate))
                WorkFlowRow["ExpireDate"] = ExpireDate;
            WorkFlowRow["ModifiedDate"] = DateTime.Now;

            this.AddRow(WorkFlowRow);
            if (this.Save() <= 0)
            {
                return ((int)Errors.Erorr);
            }
            this.DataTable.AcceptChanges();
            return Convert.ToInt32(this[0]["StateId"]);
        }

        /// <summary>
        /// Send Document To Next Step of workflow,without Sending Information Message for TaskDoers
        /// </summary>
        /// <param name="TableType">TableType of current Workflow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="TaskId">Id of next step Task</param>
        /// <param name="Description"></param>
        /// <param name="NmcId">NmcId of Current User</param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int SendDocToNextStep(int TableType, int TableId, int TaskId, string Description, int NmcId, int CurrentUserId)
        {
            return SendDocToNextStep(TableType, TableId, TaskId, Description, NmcId, -1, CurrentUserId, "", "", -1, "");
        }

        //************************************
        /// <summary>
        /// Send Document To Next Step of workflow,without Sending Information Message for TaskDoers
        /// </summary>
        /// <param name="TableType">TableType of current Workflow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="TaskId">Id of next step Task</param>
        /// <param name="Description"></param>
        /// <param name="NmcId">NmcId of Current User</param>
        /// <param name="CurrentUserId"></param>
        /// <param name="PriorityId"></param>
        /// <param name="ExpireDate"></param>
        /// <returns></returns>
        public int SendDocToNextStep(int TableType, int TableId, int TaskId, string Description, int NmcId, int CurrentUserId, int PriorityId, string ExpireDate)
        {
            return SendDocToNextStep(TableType, TableId, TaskId, Description, NmcId, -1, CurrentUserId, "", "", PriorityId, ExpireDate);
        }
        //************************************

        /// <summary>
        /// Send Document To Next Step of workflow for specifice User,without Sending Information Message for TaskDoers
        /// </summary>
        /// <param name="TableType">TableType of current Workflow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="TaskId">Id of next step Task</param>
        /// <param name="Description"></param>
        /// <param name="NmcId">NmcId of Current User</param>
        /// <param name="CurrentUserId"></param>
        /// <param name="NmcIdType">if current User is member NmcIdType=1 , if is Employee NmcIdType=0  , if is Office NmcIdType=2, if it is Municipality NmcIdType=3</param>
        /// <returns></returns>
        public int SendDocToNextStep(int TableType, int TableId, int TaskId, string Description, int NmcId, int CurrentUserId, int NmcIdType)
        {
            return SendDocToNextStep(TableType, TableId, TaskId, Description, NmcId, NmcIdType, CurrentUserId, "", "", -1, "");
        }

        /// <summary>
        ///  Send Document To Next Step of workflow and Send Information Message for TaskDoers
        /// </summary>
        /// <param name="TableType">TableType of current Workflow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="TaskId">Id of next step Task</param>
        /// <param name="Description"></param>
        /// <param name="NmcId">NmcId of Current User</param>
        /// <param name="CurrentUserId"></param>
        /// <param name="MsgContent"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public int SendDocToNextStep(int TableType, int TableId, int TaskId, string Description, int NmcId, int CurrentUserId, string MsgContent, String Url)
        {
            return SendDocToNextStep(TableType, TableId, TaskId, Description, NmcId, -1, CurrentUserId, MsgContent, Url, -1, "");
        }

        //*******************************************
        /// <summary>
        ///  Send Document To Next Step of workflow and Send Information Message for TaskDoers
        /// </summary>
        /// <param name="TableType">TableType of current Workflow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="TaskId">Id of next step Task</param>
        /// <param name="Description"></param>
        /// <param name="NmcId">NmcId of Current User</param>
        /// <param name="CurrentUserId"></param>
        /// <param name="MsgContent"></param>
        /// <param name="Url"></param>        
        /// <param name="PriorityId"></param>
        /// <param name="ExpireDate"></param>
        /// <returns></returns>
        public int SendDocToNextStep(int TableType, int TableId, int TaskId, string Description, int NmcId, int CurrentUserId, string MsgContent, String Url, int PriorityId, string ExpireDate)
        {
            return SendDocToNextStep(TableType, TableId, TaskId, Description, NmcId, -1, CurrentUserId, MsgContent, Url, PriorityId, ExpireDate);
        }
        //*******************************************

        /// <summary>
        ///  Send Document To Next Step of workflow and Send Information Message for TaskDoers.Doer of current task can be from members
        /// </summary>
        /// <param name="TableType">TableType of current Workflow</param>
        /// <param name="TableId">Id of current Document</param>
        /// <param name="TaskId">Id of next step Task</param>
        /// <param name="Description"></param>
        /// <param name="NmcId">If currentuser is member NmcId=MeId if Offic NmcId=OfId else NmcId=NmcId</param>
        /// <param name="NmcIdType">If current user is Employee NmcIdType=0,if user is member NmcIdType=1, if is Office NmcIdType=2, if it is Municipality NmcIdType=3 </param>
        /// <param name="CurrentUserId"></param>
        /// <param name="MsgContent"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public int SendDocToNextStep(int TableType, int TableId, int TaskId, string Description, int NmcId, int NmcIdType, int CurrentUserId, string MsgContent, String Url)
        {
            return SendDocToNextStep(TableType, TableId, TaskId, Description, NmcId, NmcIdType, CurrentUserId, MsgContent, Url, -1, "");
            #region Comments
            //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            //TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
            //// TSP.DataManager.MessageManager MessageManager = new MessageManager();
            //TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new NezamMemberChartManager();
            //WorkFlowTaskManager.FindByCode(TaskId);
            //if (WorkFlowTaskManager.Count > 0)
            //{
            //    string WFName = "";
            //    int SelectedTaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            //    string TaskName = WorkFlowTaskManager[0]["TaskName"].ToString();//
            //    int TaskType = int.Parse(WorkFlowTaskManager[0]["Type"].ToString());
            //    DataTable dtWorkFlowState = this.SelectLastState(TableType, TableId);
            //    if (dtWorkFlowState.Rows.Count > 0)
            //    {
            //        WFName = dtWorkFlowState.Rows[0]["WorkFlowName"].ToString();
            //        int CurrentOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            //        if (CurrentOrder != SelectedTaskOrder)
            //        {
            //            DataRow WorkFlowRow = this.NewRow();

            //            WorkFlowRow["TaskId"] = TaskId;
            //            WorkFlowRow["TableId"] = TableId;
            //            WorkFlowRow["NmcIdType"] = NmcIdType;
            //            WorkFlowRow["NmcId"] = NmcId;
            //            WorkFlowRow["SubOrder"] = 1;
            //            WorkFlowRow["StateType"] = 0;
            //            WorkFlowRow["Description"] = Description;
            //            WorkFlowRow["Date"] = this.GetDateOfToday();
            //            WorkFlowRow["UserId"] = CurrentUserId;
            //            WorkFlowRow["ModifiedDate"] = DateTime.Now;

            //            this.AddRow(WorkFlowRow);
            //            int cn = this.Save();
            //            if (cn > 0)
            //            {
            //                if (TaskType != 2 && TaskType != 3)
            //                {
            //                    TaskDoerManager.FindByTaskId(TaskId);
            //                    if (TaskDoerManager.Count > 0)
            //                    {
            //                        DataRow MsgRow = MessageManager.NewRow();

            //                        MsgRow["SenderId"] = 0;//***System Create Msg
            //                        MsgRow["SenderType"] = 0;//***System Create Msg
            //                        MsgRow["IsSenderPart"] = 0;
            //                        MsgRow["MsgTypeId"] = 1;//***Type = Send Msg
            //                        MsgRow["NeedConfirm"] = 0;

            //                        // MsgRow["MsgSubject"] = "درخواست انجام عملیات " + TaskName;
            //                        //  MsgRow["MsgBody"] = MsgContent + "<br>" + Url;
            //                        MsgRow["MsgSubject"] = "درخواست:" + WFName + " _ عملیات: " + TaskName;
            //                        string Body = "";
            //                        Body += "مسئولیت انجام عملیات " + TaskName + " به شما محول گردیده است." + "<br>";
            //                        Body += "لطفا اقدامات لازم مربوط به این عملیات را انجام دهید.";
            //                        Body += "جهت مشاهده اطلاعات مربوط به درخواست از لینک زیر استفاده نمایید" + "<br>" + Url;

            //                        MsgRow["MsgBody"] = Body + "<br>" + MsgContent;

            //                        MsgRow["Date"] = this.GetDateOfToday();
            //                        //  MsgRow["TableType"] = "";
            //                        // MsgRow["TableId"] = "";
            //                        MsgRow["Priority"] = 0;
            //                        MsgRow["InActive"] = 0;
            //                        MsgRow["UserId"] = CurrentUserId;
            //                        MsgRow["ModifiedDate"] = DateTime.Now;

            //                        MessageManager.AddRow(MsgRow);
            //                        if (MessageManager.Save() > 0)
            //                        {
            //                            int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
            //                            NezamMemberChartManager.FindByNcId(NcId);
            //                            for (int i = 0; i < NezamMemberChartManager.Count; i++)
            //                            {
            //                                int MeId = int.Parse(NezamMemberChartManager[i]["EmpId"].ToString());
            //                                int UltId = int.Parse(NezamMemberChartManager[i]["UltId"].ToString());

            //                                DataRow MsgRecRow = MessageReceiverManager.NewRow();
            //                                MsgRecRow["MsgId"] = (int)MessageManager[0]["MsgId"];
            //                                MsgRecRow["IsRead"] = 0;
            //                                MsgRecRow["ReceiverId"] = MeId;
            //                                MsgRecRow["ReceiverType"] = UltId;
            //                                MsgRecRow["IsReceiverPart"] = 0;
            //                                MsgRecRow["Answer"] = 0;
            //                                MsgRecRow["IsResignation"] = 0;
            //                                MsgRecRow["InActive"] = 0;
            //                                MsgRecRow["UserId"] = CurrentUserId;
            //                                MsgRecRow["ModifiedDate"] = DateTime.Now;

            //                                MessageReceiverManager.AddRow(MsgRecRow);
            //                            }

            //                            int count = MessageReceiverManager.Save();
            //                            if (count > 0)
            //                            {
            //                                return count;
            //                            }
            //                            else
            //                            {
            //                                return ((int)Errors.Erorr);
            //                            }

            //                        }
            //                        else
            //                        {
            //                            return ((int)Errors.Erorr);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        return ((int)Errors.NoTaskDoerFind);
            //                    }
            //                }
            //                else
            //                {
            //                    return cn;
            //                }
            //            }
            //            else
            //            {
            //                return ((int)Errors.Erorr);
            //            }
            //        }
            //        else
            //        {
            //            return ((int)Errors.CannotSendToCurrentState);
            //        }
            //    }
            //    else
            //    {
            //        return ((int)Errors.NoStateFound);
            //    }
            //}
            //else
            //{
            //    return ((int)Errors.Erorr);
            //}
            #endregion
        }
        #endregion

        #region InsertWorkFlowViewState
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
            int NmcId = -1;// FindNmcIdByTaskId(CurrentTaskId, CurrentUserId, new TSP.DataManager.NezamChartManager(), new TSP.DataManager.LoginManager());
            int NmcIdType = -1;
            //  if (NmcId == -1)
            //  {
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
                // if (UltId == (int)UserType.Employee
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

            this.AddRow(WorkFlowRow);
            int cnt = this.Save();
            if (cnt > 0)
            {
                return cnt;
            }
            else
            {
                return ((int)Errors.Erorr);
            }
        }
        public int InsertWorkFlowViewState(int TableType, int TableId, string Description, int CurrentUserId)
        {
            TSP.DataManager.WorkFlowManager WorkFlowManager = new WorkFlowManager();
            WorkFlowManager.FindByTableType(TableType, -1);
            if (WorkFlowManager.Count == 0)
                return (int)Errors.Erorr;
            int WFCode = Convert.ToInt32(WorkFlowManager[0]["WorkFlowCode"]);
            return InsertWorkFlowStateLog(WFCode, TableId, -1, Description, CurrentUserId, WorkFlowStateType.ViewInfo);
        }

        public int InsertWorkFlowSaveLogForInActiveInfo(int WorkFlowCode, int TableId, string Description, int CurrentUserId)
        {
            return InsertWorkFlowStateLog(WorkFlowCode, TableId, -1, Description, CurrentUserId, WorkFlowStateType.InActiveInfo);
        }
        private int FindNmcIdByTaskId(int TaskId, int CurrentUserId, TSP.DataManager.NezamChartManager NezamChartManager, TSP.DataManager.LoginManager LoginManager)
        {
            int NmcId = -1;
            NmcId = NezamChartManager.FindNmcId(CurrentUserId, TaskId, LoginManager);
            if (NmcId > 0)
            {
                return NmcId;
            }
            else
            {
                return (-1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableType"> Type of Document (Teacher,Institue,...)</param>
        /// <param name="TableId">Id of specific Document</param>
        /// <param name="UpdateTableType">Table that is Updated</param>
        /// <param name="Description"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        /// 
        public int InsertWorkFlowUpdateState(int TableType, int TableId, int UpdateTableType, string Description, int CurrentUserId)
        {
            TSP.DataManager.WorkFlowManager WorkFlowManager = new WorkFlowManager();
            WorkFlowManager.FindByTableType(TableType, -1);
            if (WorkFlowManager.Count == 0)
                return (int)Errors.Erorr;
            int WFCode = Convert.ToInt32(WorkFlowManager[0]["WorkFlowCode"]);
            return InsertWorkFlowStateLog(WFCode, TableId, -1, Description, CurrentUserId, WorkFlowStateType.UpdateInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableType">Type of Document (Teacher,Institue,...)</param>
        /// <param name="TableId">Id of specific Document</param>
        /// <param name="UpdateTableType">Table that is Updated</param>
        /// <param name="Description"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="NmcId">if Current user is member NmcId=MeId</param>
        /// <param name="NmcIdType">if current User is member NmcIdType=1 , if is Employee NmcIdType=0  , if is Office NmcIdType=2, if it is Municipality NmcIdType=3</param>
        /// <returns></returns>
        public int InsertWorkFlowUpdateState(int TableType, int TableId, int UpdateTableType, string Description, int CurrentUserId, int NmcId, int NmcIdType)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            TaskDoerManager.ClearBeforeFill = true;
            DataTable dtWorkFlowState = this.SelectLastState(TableType, TableId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
                int CurrentType = int.Parse(dtWorkFlowState.Rows[0]["StateType"].ToString());
                int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                int CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());

                DataTable dtTaskDoer = TaskDoerManager.FindByDoerId(CurrentTaskId, -1, -1, -1, NmcId);
                int DoerOrder = 1;
                if (dtTaskDoer.Rows.Count > 0)
                {
                    DoerOrder = int.Parse(dtTaskDoer.Rows[0]["DoerOrder"].ToString());
                    DataRow WorkFlowRow = this.NewRow();

                    WorkFlowRow["TaskId"] = CurrentTaskId;
                    WorkFlowRow["TableId"] = TableId;
                    WorkFlowRow["NmcId"] = NmcId;
                    WorkFlowRow["NmcIdType"] = NmcIdType;
                    WorkFlowRow["SubOrder"] = DoerOrder;
                    WorkFlowRow["StateType"] = 2;
                    WorkFlowRow["UpdateTableType"] = UpdateTableType;
                    WorkFlowRow["Description"] = Description;
                    WorkFlowRow["UserId"] = CurrentUserId;
                    WorkFlowRow["Date"] = this.GetDateOfToday();
                    WorkFlowRow["ModifiedDate"] = DateTime.Now;

                    this.AddRow(WorkFlowRow);
                    int cnt = this.Save();
                    if (cnt > 0)
                    {
                        return cnt;
                    }
                    else
                    {
                        return ((int)Errors.Erorr);
                    }
                }
                else
                {
                    return ((int)Errors.YouCanNotSend);
                }
            }
            else
            {
                return ((int)Errors.Erorr);
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
            WorkFlowRow["Description"] = Description;
            WorkFlowRow["Date"] = this.GetDateOfToday();
            WorkFlowRow["PriorityId"] = PriorityId;
            WorkFlowRow["ExpireDate"] = ExpireDate;
            WorkFlowRow["UserId"] = CurrentUserId;
            WorkFlowRow["ModifiedDate"] = DateTime.Now;
            this.AddRow(WorkFlowRow);
            if (this.Save() <= 0)
            {
                return ((int)Errors.Erorr);
            }
            this.DataTable.AcceptChanges();
            return Convert.ToInt32(this[0]["StateId"]);
        }
        #endregion

        #region InsertWorkFlowUpdateStateByWfCode

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableType"> WfCode of Document (Teacher,Institue,...)</param>
        /// <param name="TableId">Id of specific Document</param>
        /// <param name="UpdateTableType">Table that is Updated</param>
        /// <param name="Description"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        /// 
        public int InsertWorkFlowUpdateStateByWfCode(int WfCode, int TableId, int UpdateTableType, string Description, int CurrentUserId)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            TSP.DataManager.LoginManager LoginManager = new LoginManager();
            TaskDoerManager.ClearBeforeFill = true;
            int CurrentTaskId = -1;
            int CurrentType = -1;
            int CurrentTaskCode = -1;
            int CurrentSubOrder = -1;
            DataTable dtWorkFlowState = this.SelectLastStateByWfCode(WfCode, TableId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
                CurrentType = int.Parse(dtWorkFlowState.Rows[0]["StateType"].ToString());
                CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());
            }
            else
            {
                return ((int)Errors.Erorr);
            }

            TaskDoerManager.FindByTaskId(CurrentTaskId);
            if (TaskDoerManager.Count == 0)
            {
                return ((int)Errors.YouCanNotSend);
            }

            string[] DoerNcId = new string[TaskDoerManager.Count];
            for (int i = 0; i < TaskDoerManager.Count; i++)
            {
                DoerNcId[i] = TaskDoerManager[i]["NcId"].ToString();
            }

            LoginManager.FindByCode(CurrentUserId);
            int NmcId = -1;
            if (LoginManager.Count > 0)
            {
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
                NezamChartManager.FindByEmpId(EmpId, UltId, 0);
                if (NezamChartManager.Count > 0)
                {
                    for (int j = 0; j < NezamChartManager.Count; j++)
                    {
                        for (int k = 0; k < DoerNcId.Length; k++)
                        {
                            if (DoerNcId[k] == NezamChartManager[j]["NcId"].ToString())
                            {
                                NmcId = int.Parse(NezamChartManager[j]["NmcId"].ToString());
                                break;
                            }
                        }
                    }

                    if (NmcId == -1)
                    {
                        return ((int)Errors.YouCanNotSend);
                    }


                    ////****Check If User view Page before
                    //Boolean IsRepetitive = false;
                    //DataTable dtWorkStates = this.SelectByWorkFlowCode(WfCode, TableId);
                    //for (int i = 0; i < dtWorkFlowState.Rows.Count; i++)
                    //{
                    //    int StateNmcId = int.Parse(dtWorkFlowState.Rows[i]["NmcId"].ToString());
                    //    int StateStateTypeId = int.Parse(dtWorkFlowState.Rows[i]["StateType"].ToString());
                    //    if ((StateStateTypeId == 2) && (StateNmcId == NmcId))
                    //    {
                    //        IsRepetitive = true;
                    //        break;
                    //    }
                    //}
                    //if (!IsRepetitive)
                    //{
                    DataTable dtTaskDoer = TaskDoerManager.FindByDoerId(CurrentTaskId, -1, -1, -1, NmcId);
                    int DoerOrder = 1;
                    if (dtTaskDoer.Rows.Count > 0)
                    {
                        DoerOrder = int.Parse(dtTaskDoer.Rows[0]["DoerOrder"].ToString());
                        DataRow WorkFlowRow = this.NewRow();

                        WorkFlowRow["TaskId"] = CurrentTaskId;
                        WorkFlowRow["TableId"] = TableId;
                        WorkFlowRow["NmcId"] = NmcId;
                        WorkFlowRow["SubOrder"] = DoerOrder;
                        WorkFlowRow["StateType"] = 2;
                        WorkFlowRow["UpdateTableType"] = UpdateTableType;
                        WorkFlowRow["Description"] = Description;
                        WorkFlowRow["UserId"] = CurrentUserId;
                        WorkFlowRow["Date"] = this.GetDateOfToday();
                        WorkFlowRow["ModifiedDate"] = DateTime.Now;

                        this.AddRow(WorkFlowRow);
                        int cnt = this.Save();
                        if (cnt > 0)
                        {
                            return cnt;
                        }
                        else
                        {
                            return ((int)Errors.Erorr);
                        }
                    }
                    else
                    {
                        return ((int)Errors.YouCanNotSend);
                    }
                    //}
                    //else
                    //{
                    //    return ((int)Errors.YouCanNotSend);
                    //}
                }
                else
                {
                    return ((int)Errors.Erorr);
                }
            }
            else
            {
                return ((int)Errors.YouCanNotSend);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableType">WfCode of Document (Teacher,Institue,...)</param>
        /// <param name="TableId">Id of specific Document</param>
        /// <param name="UpdateTableType">Table that is Updated</param>
        /// <param name="Description"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="NmcId">if Current user is member NmcId=MeId</param>
        /// <param name="NmcIdType">if current User is member NmcIdType=1 , if is Employee NmcIdType=0  , if is Office NmcIdType=2, if it is Municipality NmcIdType=3</param>
        /// <returns></returns>
        public int InsertWorkFlowUpdateStateByWfCode(int WfCode, int TableId, int UpdateTableType, string Description, int CurrentUserId, int NmcId, int NmcIdType)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            TaskDoerManager.ClearBeforeFill = true;
            DataTable dtWorkFlowState = this.SelectLastStateByWfCode(WfCode, TableId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
                int CurrentType = int.Parse(dtWorkFlowState.Rows[0]["StateType"].ToString());
                int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                int CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());

                ////****Check If User view Page before
                //Boolean IsRepetitive = false;
                //DataTable dtWorkStates = this.SelectLastStateByWfCode(WfCode, TableId);
                //for (int i = 0; i < dtWorkFlowState.Rows.Count; i++)
                //{
                //    int StateNmcId = int.Parse(dtWorkFlowState.Rows[i]["NmcId"].ToString());
                //    int StateStateTypeId = int.Parse(dtWorkFlowState.Rows[i]["StateType"].ToString());
                //    if ((StateStateTypeId == 2) && (StateNmcId == NmcId))
                //    {
                //        IsRepetitive = true;
                //        break;
                //    }
                //}
                //if (!IsRepetitive)
                //{
                DataTable dtTaskDoer = TaskDoerManager.FindByDoerId(CurrentTaskId, -1, -1, -1, NmcId);
                int DoerOrder = 1;
                if (dtTaskDoer.Rows.Count > 0)
                {
                    DoerOrder = int.Parse(dtTaskDoer.Rows[0]["DoerOrder"].ToString());
                    DataRow WorkFlowRow = this.NewRow();

                    WorkFlowRow["TaskId"] = CurrentTaskId;
                    WorkFlowRow["TableId"] = TableId;
                    WorkFlowRow["NmcId"] = NmcId;
                    WorkFlowRow["NmcIdType"] = NmcIdType;
                    WorkFlowRow["SubOrder"] = DoerOrder;
                    WorkFlowRow["StateType"] = 2;
                    WorkFlowRow["UpdateTableType"] = UpdateTableType;
                    WorkFlowRow["Description"] = Description;
                    WorkFlowRow["UserId"] = CurrentUserId;
                    WorkFlowRow["Date"] = this.GetDateOfToday();
                    WorkFlowRow["ModifiedDate"] = DateTime.Now;

                    this.AddRow(WorkFlowRow);
                    int cnt = this.Save();
                    if (cnt > 0)
                    {
                        return cnt;
                    }
                    else
                    {
                        return ((int)Errors.Erorr);
                    }
                }
                else
                {
                    return ((int)Errors.YouCanNotSend);
                }
                //}
                //else
                //{
                //    return ((int)Errors.YouCanNotSend);
                //}
            }
            else
            {
                return ((int)Errors.Erorr);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableType">WfCode of Document (Teacher,Institue,...)</param>
        /// <param name="TableId">Id of specific Document</param>
        /// <param name="UpdateTableType">Table that is Updated</param>
        /// <param name="Description"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="NmcId">if Current user is member NmcId=MeId</param>
        /// <param name="NmcIdType">if current User is member NmcIdType=1 , if is Employee NmcIdType=0  , if is Office NmcIdType=2, if it is Municipality NmcIdType=3</param>
        /// <returns></returns>
        public int InsertWorkFlowUpdateStateByWfCode(int WfCode, int TableId, int UpdateTableType, string Description, int CurrentUserId, int NmcId, int NmcIdType, WorkFlowStateType WFStateType)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            TaskDoerManager.ClearBeforeFill = true;
            DataTable dtWorkFlowState = this.SelectLastStateByWfCode(WfCode, TableId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                int CurrentTaskId = int.Parse(dtWorkFlowState.Rows[0]["TaskId"].ToString());
                int CurrentType = int.Parse(dtWorkFlowState.Rows[0]["StateType"].ToString());
                int CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                int CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());

                //****Check If User view Page before
                Boolean IsRepetitive = false;
                DataTable dtWorkStates = this.SelectLastStateByWfCode(WfCode, TableId);
                if (WFStateType == WorkFlowStateType.ViewInfo)
                {
                    for (int i = 0; i < dtWorkFlowState.Rows.Count; i++)
                    {
                        int StateNmcId = int.Parse(dtWorkFlowState.Rows[i]["NmcId"].ToString());
                        int StateStateTypeId = int.Parse(dtWorkFlowState.Rows[i]["StateType"].ToString());
                        if ((StateStateTypeId == 2) && (StateNmcId == NmcId))
                        {
                            IsRepetitive = true;
                            break;
                        }
                    }
                }
                if (!IsRepetitive)
                {
                    DataTable dtTaskDoer = TaskDoerManager.FindByDoerId(CurrentTaskId, -1, -1, -1, NmcId);
                    int DoerOrder = 1;
                    if (dtTaskDoer.Rows.Count > 0)
                    {
                        DoerOrder = int.Parse(dtTaskDoer.Rows[0]["DoerOrder"].ToString());
                        DataRow WorkFlowRow = this.NewRow();

                        WorkFlowRow["TaskId"] = CurrentTaskId;
                        WorkFlowRow["TableId"] = TableId;
                        WorkFlowRow["NmcId"] = NmcId;
                        WorkFlowRow["NmcIdType"] = NmcIdType;
                        WorkFlowRow["SubOrder"] = DoerOrder;
                        WorkFlowRow["StateType"] = 2;
                        WorkFlowRow["UpdateTableType"] = UpdateTableType;
                        WorkFlowRow["Description"] = Description;
                        WorkFlowRow["UserId"] = CurrentUserId;
                        WorkFlowRow["Date"] = this.GetDateOfToday();
                        WorkFlowRow["ModifiedDate"] = DateTime.Now;

                        this.AddRow(WorkFlowRow);
                        int cnt = this.Save();
                        if (cnt > 0)
                        {
                            return cnt;
                        }
                        else
                        {
                            return ((int)Errors.Erorr);
                        }
                    }
                    else
                    {
                        return ((int)Errors.YouCanNotSend);
                    }
                }
                else
                {
                    return ((int)Errors.YouCanNotSend);
                }
            }
            else
            {
                return ((int)Errors.Erorr);
            }
        }
        #endregion
    }
}
