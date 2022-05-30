using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
   public class WorkFlowTaskManager : BaseObject
    {
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.WorkFlowTask);
        }

        protected override void InitAdapter()
        {         
                     
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblWorkFlowTask";
            tableMapping.ColumnMappings.Add("TaskId", "TaskId");
            tableMapping.ColumnMappings.Add("TaskCode", "TaskCode");
            tableMapping.ColumnMappings.Add("TaskName", "TaskName");
            tableMapping.ColumnMappings.Add("TCode", "TCode");
            tableMapping.ColumnMappings.Add("WorkFlowId", "WorkFlowId");
            tableMapping.ColumnMappings.Add("TaskOrder", "TaskOrder");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("IsSmsSend", "IsSmsSend");
            tableMapping.ColumnMappings.Add("SmsBody", "SmsBody");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectWorkFlowTask";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskCode", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteWorkFlowTask";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertWorkFlowTask";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkFlowId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkFlowId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskOrder", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskOrder", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSmsSend", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSmsSend", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsBody", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsBody", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateWorkFlowTask";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkFlowId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkFlowId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskOrder", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskOrder", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSmsSend", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSmsSend", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsBody", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsBody", System.Data.DataRowVersion.Current, false, null, "", "", ""));            
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.WorkFlowDataSet.tblWorkFlowTaskDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int TaskId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectWorkFlowTask";
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskCode", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters["@TaskId"].Value = TaskId;
            Fill();
        }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public void FindByTaskCode(int TaskCode)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.Transaction = this.Transaction;
           this.Adapter.SelectCommand.CommandText = "dbo.spSelectWorkFlowTask";
           this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaskCode", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "TaskCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
           this.Adapter.SelectCommand.Parameters["@TaskCode"].Value = TaskCode;           
           Fill();
       }

       #region SelectByWorkId
       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectByWorkId(int WorkFlowId, int TaskOrder, int NcId, int OppositTaskId, int ShowEndProcess)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.CommandText="spSelectWorkFlowTaskByWorkId";
          DataTable dt = new DataTable();
           this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           this.Adapter.SelectCommand.Parameters.Add("@WorkFlowId", SqlDbType.Int, 4, "WorkFlowId").Value = WorkFlowId;
           this.Adapter.SelectCommand.Parameters.Add("@TaskOrder", SqlDbType.Int, 4, "TaskOrder").Value = TaskOrder;
           this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = -1;
           this.Adapter.SelectCommand.Parameters.Add("@NcId", SqlDbType.Int, 4, "NcId").Value = NcId;
           this.Adapter.SelectCommand.Parameters.Add("@ShowEndProcess", SqlDbType.Int, 4, "ShowEndProcess").Value = ShowEndProcess;
           this.Adapter.SelectCommand.Parameters.AddWithValue("@OppositTaskId", OppositTaskId);
           
           this.Adapter.Fill(dt);
           return (dt);
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectByWorkId(int WorkFlowId, int TaskOrder)
       {
           return SelectByWorkId(WorkFlowId,TaskOrder,-1,-1,1);
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectByWorkId(int WorkFlowId, int TaskOrder,int NcId)
       {
           return SelectByWorkId(WorkFlowId, TaskOrder, NcId, -1,1);
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="WorkFlowId"></param>
       /// <param name="TaskOrder"></param>
       /// <param name="NcId"></param>
       /// <param name="ShowEndProcess">0: Not Show,1:Show</param>
       /// <returns></returns>
       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectByWorkId(int WorkFlowId, int TaskOrder, int NcId, int ShowEndProcess)
       {
           return SelectByWorkId(WorkFlowId, TaskOrder, NcId, -1, ShowEndProcess);
       }

       public DataTable SelectByWorkId(int WorkFlowId)
       {
         return(this.SelectByWorkId(WorkFlowId, -1));
       }
       #endregion

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectByWorkCode(int WorkFlowCode)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.CommandText = "spSelectWorkFlowTaskByWorkId";
           DataTable dt = new DataTable();           
           this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           this.Adapter.SelectCommand.Parameters.Add("@WorkFlowId", SqlDbType.Int, 4, "WorkFlowId").Value = -1;
           this.Adapter.SelectCommand.Parameters.Add("@TaskOrder", SqlDbType.Int, 4, "TaskOrder").Value = -1;
           this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
           this.Adapter.SelectCommand.Parameters.Add("@NcId", SqlDbType.Int, 4, "NcId").Value = -1;
           this.Adapter.Fill(dt);
           return (dt);
       }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByWorkCodeList(string WorkFlowCodeList)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "spSelectWorkFlowTaskByWorkCodeList";
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCodeList", WorkFlowCodeList);
            this.Adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectWorkFlowTaskActiveTasks(int WorkFlowCode)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.CommandText = "SelectWorkFlowTaskActiveTasks";
           DataTable dt = new DataTable();           
           this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
           this.Adapter.Fill(dt);
           return (dt);
       }
       
       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectNextSteps(int TableType, int TaskCode, int WorkFlowCode)
       {
           DataTable dt = new DataTable();
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectWorkFlowNextTask", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
           adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
           adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4, "TaskCode").Value = TaskCode;
           adapter.Fill(dt);
           return (dt);
       }

       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectByTaskOrder(int WorkFlowId, int TaskOrder)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.CommandText = "spSelectWorkFlowTaskByTaskOrder";
           DataTable dt = new DataTable();
           this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           this.Adapter.SelectCommand.Parameters.Add("@WorkFlowId", SqlDbType.Int, 4, "WorkFlowId").Value = WorkFlowId;
           this.Adapter.SelectCommand.Parameters.Add("@TaskOrder", SqlDbType.Int, 4, "TaskOrder").Value = TaskOrder;
           this.Adapter.Fill(dt);
           return (dt);
       }

       public DataTable SelectNextTopSteps(int TableType, int TaskCode, int WorkFlowCode)
       {
           DataTable dt = new DataTable();
           SqlDataAdapter adapter = new SqlDataAdapter("spSelectWorkFlowNextTopTask", this.Connection);
           adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableType;
           adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
           adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4, "TaskCode").Value = TaskCode;
           adapter.SelectCommand.Transaction = this.Transaction;
           adapter.Fill(dt);
           return (dt);
       }


       [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
       public DataTable SelectByEmployee(int EmpId)
       {
           this.Adapter.SelectCommand.Parameters.Clear();
           this.Adapter.SelectCommand.CommandText = "spSelectWorkFlowTaskByEmployee";
           DataTable dt = new DataTable();
           //  SqlDataAdapter adapter = new SqlDataAdapter("spSelectWorkFlowTaskByWorkId", this.Connection);
           this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
           this.Adapter.SelectCommand.Parameters.Add("@EmpId", SqlDbType.Int, 4, "EmpId").Value = EmpId;

           this.Adapter.Fill(dt);
           return (dt);
       }

       //public int CheckWorkFlowPermissionForTask(int TaskCode,int CurrentSubOrder,int CurrentUserId,Boolean IsNextTask)
       //{
       //    int DoerId = -1;
       //    TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
       //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = this;
       //    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
       //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

       //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
       //    int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
       //    TaskDoerManager.FindByTaskId(TaskId);

       //    if (TaskDoerManager.Count > 0)
       //    {
       //        int DoerOrder = -1;
       //        if (!IsNextTask)
       //        {
       //            for (int i = 0; i < TaskDoerManager.Count; i++)
       //            {
       //                DoerOrder = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerOrder"].ToString());
       //                if ((DoerOrder != CurrentSubOrder) && (DoerOrder > CurrentSubOrder))
       //                {
       //                    DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
       //                    int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
       //                    NezamMemberChartManager.FindByNcId(NcId);

       //                    int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());
       //                    int UltId = int.Parse(NezamMemberChartManager[0]["UltId"].ToString());
       //                    LoginManager.FindByMeIdUltId(EmpId, UltId);
       //                    if (LoginManager.Count > 0)
       //                    {
       //                        int userId = int.Parse(LoginManager[0]["UserId"].ToString());
       //                        if (CurrentUserId == userId)
       //                        {
       //                            DoerId = DoerId;
       //                        }
       //                        else
       //                        {
       //                            DoerId = -1;
       //                        }
       //                    }
       //                    else
       //                    {
       //                        DoerId = -1;
       //                    }
       //                    break;
       //                }
       //            }
       //        }
       //        else
       //        {
       //            for (int i = 0; i < TaskDoerManager.Count; i++)
       //            {
       //                DoerOrder = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerOrder"].ToString());
       //                if (DoerOrder == CurrentSubOrder)
       //                {
       //                    DoerId = int.Parse(TaskDoerManager.DataTable.Rows[i]["DoerId"].ToString());
       //                    int NcId = int.Parse(TaskDoerManager[i]["NcId"].ToString());
       //                    NezamMemberChartManager.FindByNcId(NcId);

       //                    int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());
       //                    int UltId = int.Parse(NezamMemberChartManager[0]["UltId"].ToString());
       //                    LoginManager.FindByMeIdUltId(EmpId, UltId);
       //                    if (LoginManager.Count > 0)
       //                    {
       //                        int userId = int.Parse(LoginManager[0]["UserId"].ToString());
       //                        if (CurrentUserId == userId)
       //                        {
       //                            DoerId = DoerId;
       //                        }
       //                        else
       //                        {
       //                            DoerId = -1;
       //                        }
       //                    }
       //                    else
       //                    {
       //                        DoerId = -1;
       //                    }
       //                    break;
       //                }
       //            }
       //        }                         
       //    }
       //    else
       //    {
       //        DoerId= -1;
       //    }
       //    return DoerId;
       //}
    }
}
