using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class TaskDoerManager : BaseObject
    {
        private string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }


        public enum Errors
        {
            YouCanNotSend = -1, NoTaskFind = -2, Erorr = -3, NoStateFound = -4, TaskIsNotInWF = -5, NoDoerFound = -6, IsNotInChart = -7
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TaskDoer);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblTaskDoer";
            tableMapping.ColumnMappings.Add("DoerId", "DoerId");
            tableMapping.ColumnMappings.Add("TaskId", "TaskId");
            tableMapping.ColumnMappings.Add("SendBackTask", "SendBackTask");
            tableMapping.ColumnMappings.Add("NcId", "NcId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("DoerOrder", "DoerOrder");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTaskDoer";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DoerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "DoerId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EmpId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "EmpId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "UltId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NmcId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "NmcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTaskDoer";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DoerId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DoerId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTaskDoer";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendBackTask", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SendBackTask", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DoerOrder", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DoerOrder", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTaskDoer";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendBackTask", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SendBackTask", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DoerOrder", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DoerOrder", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DoerId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "DoerId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DoerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "DoerId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.WorkFlowDataSet.tblTaskDoerDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int DoerId)
        {
            this.Adapter.SelectCommand.Parameters["@TaskId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@NmcId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@NcId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@DoerId"].Value = DoerId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByNcId(int NcId)
        {
            this.Adapter.SelectCommand.Parameters["@TaskId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@NmcId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@NcId"].Value = NcId;
            this.Adapter.SelectCommand.Parameters["@DoerId"].Value = -1;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByTaskId(int TaskId)
        {
            this.Adapter.SelectCommand.Parameters["@DoerId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@EmpId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@UltId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@NmcId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@TaskId"].Value = TaskId;
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            Fill();
        }

        #region spSelectTaskDoer --- FindByDoerId
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByDoerId(int TaskId, int DoerId, int EmpId, int UltId, int NmcId, int UserId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTaskDoer", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@UltId", SqlDbType.Int, 4, "UltId").Value = UltId;
            adapter.SelectCommand.Parameters.Add("@EmpId", SqlDbType.Int, 4, "EmpId").Value = EmpId;
            adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4, "TaskId").Value = TaskId;
            adapter.SelectCommand.Parameters.Add("@DoerId", SqlDbType.Int, 4, "DoerId").Value = DoerId;
            adapter.SelectCommand.Parameters.Add("@NmcId", SqlDbType.Int, 4, "NmcId").Value = NmcId;
            adapter.SelectCommand.Parameters.Add("@NcId", SqlDbType.Int, 4, "NcId");
            adapter.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByDoerId(int TaskId, int DoerId, int EmpId, int UltId, int NmcId)
        {
            return FindByDoerId(TaskId, DoerId, EmpId, UltId, NmcId, -1);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByDoerId(int TaskId, int UserId)
        {
            return FindByDoerId(TaskId, -1, -1, -1, -1, UserId);
        }
        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySendBack(int SendBackTask, int DoerId, int WorkFlowId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTaskDoerSendBackTask", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SendBackTask", SqlDbType.Int, 4, "SendBackTask").Value = SendBackTask;
            adapter.SelectCommand.Parameters.Add("@DoerId", SqlDbType.Int, 4, "DoerId").Value = DoerId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowId", SqlDbType.Int, 4, "WorkFlowId").Value = WorkFlowId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMaxDoerOrder(int TaskId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTaskDoerMaxDoerOrder", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4, "TaskId").Value = TaskId;

            adapter.Fill(dt);
            return (dt);
        }

        public int CheckWorkFlowPermissionForTask(int TaskCode, int CurrentSubOrder, int CurrentUserId, Boolean IsNextTask)
        {
            int DoerId = -1;
            TSP.DataManager.TaskDoerManager TaskDoerManager = this;
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();

            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());


            // int DoerOrder = -1;
            if (!IsNextTask)
            {
                DataTable dtTaskDoer = TaskDoerManager.FindByDoerId(TaskId, CurrentUserId);
                if (dtTaskDoer.Rows.Count > 0)
                {
                    DoerId = int.Parse(dtTaskDoer.Rows[0]["DoerId"].ToString());
                }

                #region Comments

                //for (int i = 0; i < TaskDoerManager.Count; i++)
                //{
                //    //***Is Not Next Task
                //    DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
                //    int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
                //    NezamMemberChartManager.FindByNcId(NcId);
                //    if (NezamMemberChartManager.Count > 0)
                //    {
                //        for (int j = 0; j < NezamMemberChartManager.Count; j++)
                //        {
                //            int EmpId = int.Parse(NezamMemberChartManager[j]["EmpId"].ToString());
                //            int UltId = int.Parse(NezamMemberChartManager[j]["UltId"].ToString());
                //            LoginManager.FindByMeIdUltId(EmpId, UltId);
                //            if (LoginManager.Count > 0)
                //            {
                //                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                //                if (CurrentUserId != userId)
                //                {
                //                    #region Resignation
                //                    ResignationTaskManager.FindByTaskId(EmpId, TaskId, GetDateOfToday());
                //                    ResignationTaskManager.CurrentFilter = "InActive= 0" + " and " + "IsAccepted=2";
                //                    if (ResignationTaskManager.Count > 0)
                //                    {
                //                        for (int k = 0; k < ResignationTaskManager.Count; k++)
                //                        {
                //                            int CurrentReceptiveUserId = int.Parse(ResignationTaskManager[k]["ReceptiveUserId"].ToString());                                               
                //                            if (CurrentUserId == CurrentReceptiveUserId)
                //                            {
                //                                break;
                //                            }
                //                            else
                //                            {
                //                                DoerId = -1;
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {
                //                        DoerId = -1;
                //                    }
                //                    #endregion
                //                }
                //                else
                //                {
                //                    break;
                //                }
                //            }
                //            else
                //            {
                //                DoerId = -1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        DoerId = -1;
                //    }
                //    if (DoerId > 0)
                //        break;
                //}
                #endregion
            }
            else
            {
                TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
                TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
                TSP.DataManager.ResignationTaskManager ResignationTaskManager = new ResignationTaskManager();
                TaskDoerManager.FindByTaskId(TaskId);
                for (int i = 0; i < TaskDoerManager.Count; i++)
                {
                    DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
                    int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
                    NezamMemberChartManager.FindByNcId(NcId);
                    //$$$$$$ if (NezamMemberChartManager.Count == 1)                            
                    if (NezamMemberChartManager.Count > 0)
                    {
                        for (int j = 0; j < NezamMemberChartManager.Count; j++)
                        {
                            int EmpId = int.Parse(NezamMemberChartManager[j]["EmpId"].ToString());
                            int UltId = int.Parse(NezamMemberChartManager[j]["UltId"].ToString());
                            LoginManager.FindByMeIdUltId(EmpId, UltId);
                            if (LoginManager.Count > 0)
                            {
                                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                                if (CurrentUserId != userId)
                                {
                                    #region Resignation
                                    ResignationTaskManager.FindByTaskId(EmpId, TaskId, GetDateOfToday());
                                    ResignationTaskManager.CurrentFilter = "InActive= 0" + " and " + "IsAccepted=2";
                                    if (ResignationTaskManager.Count > 0)
                                    {
                                        for (int k = 0; k < ResignationTaskManager.Count; k++)
                                        {
                                            int CurrentReceptiveUserId = int.Parse(ResignationTaskManager[k]["ReceptiveUserId"].ToString()); if (CurrentUserId == CurrentReceptiveUserId)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                DoerId = -1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DoerId = -1;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                DoerId = -1;
                            }
                        }
                    }
                    else
                    {
                        DoerId = -1;
                    }

                }
            }

            return DoerId;
        }

        public int CheckWorkFlowPermissionForStateView(int TaskCode, int CurrentSubOrder, int CurrentUserId)
        {
            int DoerId = -1;
            TSP.DataManager.TaskDoerManager TaskDoerManager = this;
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            TaskDoerManager.FindByTaskId(TaskId);

            if (TaskDoerManager.Count > 0)
            {
                int DoerOrder = -1;
                for (int i = 0; i < TaskDoerManager.Count; i++)
                {
                    DoerOrder = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerOrder"].ToString());
                    if (DoerOrder == CurrentSubOrder)
                    {
                        DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
                        int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
                        NezamMemberChartManager.FindByNcId(NcId);
                        if (NezamMemberChartManager.Count == 1)
                        {
                            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());
                            int UltId = int.Parse(NezamMemberChartManager[0]["UltId"].ToString());
                            LoginManager.FindByMeIdUltId(EmpId, UltId);
                            if (LoginManager.Count > 0)
                            {
                                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                                if (CurrentUserId != userId)
                                {
                                    //    DoerId = DoerId;
                                    //}
                                    //else
                                    //{
                                    DoerId = -1;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                DoerId = -1;
                            }
                        }
                        else
                        {
                            DoerId = -1;
                        }

                    }
                }
            }
            else
            {
                DoerId = -1;
            }
            return DoerId;
        }

        public int CheckWorkFlowPermissionForStateUpdate(int TaskCode, int CurrentSubOrder, int CurrentUserId)
        {
            int DoerId = -1;
            TSP.DataManager.TaskDoerManager TaskDoerManager = this;
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            TaskDoerManager.FindByTaskId(TaskId);

            if (TaskDoerManager.Count > 0)
            {
                int DoerOrder = -1;
                for (int i = 0; i < TaskDoerManager.Count; i++)
                {
                    DoerOrder = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerOrder"].ToString());
                    if (DoerOrder == CurrentSubOrder)
                    {
                        DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
                        int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
                        NezamMemberChartManager.FindByNcId(NcId);

                        int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());
                        int UltId = int.Parse(NezamMemberChartManager[0]["UltId"].ToString());
                        LoginManager.FindByMeIdUltId(EmpId, UltId);
                        if (LoginManager.Count > 0)
                        {
                            int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                            if (CurrentUserId != userId)
                            {
                                //    DoerId = DoerId;
                                //}
                                //else
                                //{
                                DoerId = -1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            DoerId = -1;
                        }
                    }
                }
            }
            else
            {
                DoerId = -1;
            }
            return DoerId;
        }

        public int CheckWorkFlowPermissionForEditInfo(int TableType, int WorkFlowCode, int TableId, int TaskCode, int CurrentUserId)
        {
            int DoerId = -1;
            TSP.DataManager.TaskDoerManager TaskDoerManager = this;
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new WorkFlowStateManager();
            if (WorkFlowCode != -1)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WorkFlowCode, TableId);
                if (dtWorkFlowState.Rows.Count <= 0)
                    return DoerId;
                if (Convert.ToInt32(dtWorkFlowState.Rows[0]["TaskCode"]) != TaskCode)
                    return DoerId;
            }
            if (TableType != -1)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowState.Rows.Count <= 0)
                {
                    return DoerId;
                }
            }
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                DataTable dtTaskDoer = TaskDoerManager.FindByDoerId(int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString()), CurrentUserId);
                if (dtTaskDoer.Rows.Count > 0)
                {
                    DoerId = int.Parse(dtTaskDoer.Rows[0]["DoerId"].ToString());
                }
            }

            return DoerId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableType"></param>
        /// <param name="TableId"></param>
        /// <param name="TaskCode">TaskCode of SaveInfo Task</param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int CheckWorkFlowPermissionForEditInfo(int TableType, int TableId, int TaskCode, int CurrentUserId)
        {
            return CheckWorkFlowPermissionForEditInfo(TableType, -1, TableId, TaskCode, CurrentUserId);
            #region Comment
            //int CurrentTaskOrder = -1;
            //int TaskOrder = 0;
            //int DoerId = -1;
            //int CurrentSubOrder = -1;
            //TSP.DataManager.TaskDoerManager TaskDoerManager = this;
            //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new WorkFlowStateManager();
            //TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
            //TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            //WorkFlowTaskManager.ClearBeforeFill = true;

            //DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, TableId);
            //if (dtWorkFlowState.Rows.Count > 0)
            //{
            //    CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            //    CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());

            //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
            //    if (WorkFlowTaskManager.Count > 0)
            //    {
            //        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            //    }

            //    if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
            //    {
            //        WorkFlowTaskManager.FindByTaskCode(TaskCode);
            //        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            //        TaskDoerManager.FindByTaskId(TaskId);

            //        if (TaskDoerManager.Count > 0)
            //        {
            //            Boolean IsFound = false;
            //            int DoerOrder = -1;
            //            for (int i = 0; i < TaskDoerManager.Count; i++)
            //            {
            //                DoerOrder = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerOrder"].ToString());

            //                if ((DoerOrder == CurrentSubOrder) || (DoerOrder == CurrentSubOrder + 1))
            //                {
            //                    DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
            //                    int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
            //                    NezamMemberChartManager.FindByNcId(NcId);
            //                    for (int j = 0; j < NezamMemberChartManager.Count; j++)
            //                    {
            //                        int EmpId = int.Parse(NezamMemberChartManager[j]["EmpId"].ToString());
            //                        int UltId = int.Parse(NezamMemberChartManager[j]["UltId"].ToString());
            //                        LoginManager.FindByMeIdUltId(EmpId, UltId);
            //                        if (LoginManager.Count > 0)
            //                        {
            //                            int userId = int.Parse(LoginManager[0]["UserId"].ToString());
            //                            if (CurrentUserId != userId)
            //                            {
            //                                DoerId = -1;
            //                            }
            //                            else
            //                            {
            //                                IsFound = true;
            //                                break;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            DoerId = -1;
            //                        }
            //                    }
            //                }
            //                if (IsFound)
            //                {
            //                    break;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            DoerId = -1;
            //        }
            //    }
            //}
            //return DoerId;
            #endregion
        }

        public int CheckWorkFlowPermissionForEditInfoByWfCode(int WorkFlowCode, int TableId, int TaskCode, int CurrentUserId)
        {
            return CheckWorkFlowPermissionForEditInfo(-1, WorkFlowCode, TableId, TaskCode, CurrentUserId);
            /////  int CurrentTaskOrder = -1;
            // int TaskOrder = 0;
            /////   int DoerId = -1;
            //////////////  int CurrentSubOrder = -1;
            ////////////TSP.DataManager.TaskDoerManager TaskDoerManager = this;
            ////////////TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            ////////////TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new WorkFlowStateManager();
            #region  Comment
            //
            //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new WorkFlowStateManager();
            //TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
            //TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            //WorkFlowTaskManager.ClearBeforeFill = true;

            //DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WorkFlowCode, TableId);
            //if (dtWorkFlowState.Rows.Count > 0)
            //{
            //  CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            //  CurrentSubOrder = int.Parse(dtWorkFlowState.Rows[0]["SubOrder"].ToString());

            // WorkFlowTaskManager.FindByTaskCode(TaskCode);
            // if (WorkFlowTaskManager.Count > 0)
            // {
            //      TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            //   }

            //  if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
            // {
            //WorkFlowTaskManager.FindByTaskCode(TaskCode);
            //int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            //TaskDoerManager.FindByTaskId(TaskId);

            //if (TaskDoerManager.Count > 0)
            //{
            //    Boolean IsFound = false;
            //  int DoerOrder = -1;
            //for (int i = 0; i < TaskDoerManager.Count; i++)
            //{
            //  DoerOrder = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerOrder"].ToString());

            // if ((DoerOrder == CurrentSubOrder) || (DoerOrder == CurrentSubOrder + 1))
            // {
            //      DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
            //      int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
            //      NezamMemberChartManager.FindByNcId(NcId);
            //      for (int j = 0; j < NezamMemberChartManager.Count; j++)
            //      {
            //          int EmpId = int.Parse(NezamMemberChartManager[j]["EmpId"].ToString());
            //          int UltId = int.Parse(NezamMemberChartManager[j]["UltId"].ToString());
            //          LoginManager.FindByMeIdUltId(EmpId, UltId);
            //          if (LoginManager.Count > 0)
            //          {
            //              int userId = int.Parse(LoginManager[0]["UserId"].ToString());
            //              if (CurrentUserId != userId)
            //              {
            //                  DoerId = -1;
            //              }
            //              else
            //              {
            //                  IsFound = true;
            //                  break;
            //              }
            //          }
            //          else
            //          {
            //              DoerId = -1;
            //          }
            //      }
            ////  }
            //if (TaskOrder > 0)
            //    break;
            //        if (IsFound)
            //            break;
            //    }
            //}
            //else
            //{
            //    DoerId = -1;
            //}
            //  }
            //  }
            #endregion
            ////////////DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WorkFlowCode, TableId);
            ////////////if (dtWorkFlowState.Rows.Count > 0)
            ////////////{
            ////////////    WorkFlowTaskManager.FindByTaskCode(TaskCode);
            ////////////    if (WorkFlowTaskManager.Count > 0)
            ////////////    {
            ////////////        DataTable dtTaskDoer = TaskDoerManager.FindByDoerId(int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString()), CurrentUserId);
            ////////////        if (dtTaskDoer.Rows.Count > 0)
            ////////////        {
            ////////////            DoerId = int.Parse(dtTaskDoer.Rows[0]["DoerId"].ToString());
            ////////////        }
            ////////////    }
            ////////////}
            ///// return DoerId;
        }

        /// <summary>
        /// Check user permission for Create New Document in for current WorkFlow
        /// </summary>
        /// <param name="TableType"></param>
        /// <param name="TaskCode">TaskCode of SaveInfo Task in current WorkFlow</param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int CheckWorkFlowPermissionForSaveInfo(int TableType, int TaskCode, int CurrentUserId)
        {
            //int CurrentTaskOrder = -1;
            int TaskOrder = 0;
            //int DoerId = -1;
            //int CurrentSubOrder = -1;
            TSP.DataManager.TaskDoerManager TaskDoerManager = this;
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new WorkFlowStateManager();
            //TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
            //TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count <= 0)
            {
                return (int)Errors.NoStateFound;
            }
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder == 0)
            {
                return (int)Errors.TaskIsNotInWF;
            }
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            TaskDoerManager.FindByTaskId(TaskId);
            if (TaskDoerManager.Count <= 0)
            {
                return (int)Errors.NoDoerFound;
            }
            DataTable dtTaskdoer = TaskDoerManager.FindByDoerId(TaskId, CurrentUserId);
            if (dtTaskdoer.Rows.Count <= 0)
                return (int)Errors.NoDoerFound;
            return Convert.ToInt32(dtTaskdoer.Rows[0]["DoerId"]);

            #region Comment
            //Boolean IsFound = false;
            //int DoerOrder = -1;
            //for (int i = 0; i < TaskDoerManager.Count; i++)
            //{
            //    DoerOrder = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerOrder"].ToString());

            //    if (DoerOrder == 1)// != CurrentSubOrder) && (DoerOrder > CurrentSubOrder))
            //    {
            //        DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
            //        int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
            //        NezamMemberChartManager.FindByNcId(NcId);
            //        if (NezamMemberChartManager.Count > 0)
            //        {
            //            for (int j = 0; j < NezamMemberChartManager.Count; j++)
            //            {
            //                int EmpId = int.Parse(NezamMemberChartManager[j]["EmpId"].ToString());
            //                int UltId = int.Parse(NezamMemberChartManager[j]["UltId"].ToString());
            //                LoginManager.FindByMeIdUltId(EmpId, UltId);
            //                if (LoginManager.Count > 0)
            //                {
            //                    int userId = int.Parse(LoginManager[0]["UserId"].ToString());
            //                    if (CurrentUserId != userId)
            //                    {
            //                        DoerId = (int)Errors.YouCanNotSend;
            //                    }
            //                    else
            //                    {
            //                        IsFound = true;
            //                        break;
            //                    }
            //                }
            //                else
            //                {
            //                    DoerId = (int)Errors.Erorr;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            DoerId = (int)Errors.IsNotInChart;
            //        }
            //    }
            //    if (IsFound)
            //        break;
            //}   
            //return DoerId;
            #endregion
        }
    }
}
