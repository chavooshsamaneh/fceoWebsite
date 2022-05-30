using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.DataManager.Session
{
    public class SessionRequestsManager : BaseObject
    {
        public static int TableTypeCode
        {
            get { return (int)TSP.DataManager.TableType.Session_SessionRequests; }
        }

        public static int TableTypeId
        {
            get { return TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Session_SessionRequests); }
        }

        private Session.SessionsManager SessionsManager;
        private DataManager.LoginManager LoginManager;
        private DataManager.NezamChartManager NezamChartManager;

        public SessionRequestsManager()
        {
        }

        public SessionRequestsManager(TransactionManager Transaction)
        {
            SessionsManager = new SessionsManager();
            LoginManager = new LoginManager();
            NezamChartManager = new NezamChartManager();

            Transaction.Add(SessionsManager);
            Transaction.Add(LoginManager);
            Transaction.Add(NezamChartManager);
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.Session_SessionRequests);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "SessionRequests";
            tableMapping.ColumnMappings.Add("RequestId", "RequestId");
            tableMapping.ColumnMappings.Add("SessionId", "SessionId");
            tableMapping.ColumnMappings.Add("SessionTitle", "SessionTitle");
            tableMapping.ColumnMappings.Add("SessionDescription", "SessionDescription");
            tableMapping.ColumnMappings.Add("StartDate", "StartDate");
            tableMapping.ColumnMappings.Add("StartTime", "StartTime");
            tableMapping.ColumnMappings.Add("EndTime", "EndTime");
            tableMapping.ColumnMappings.Add("EndDate", "EndDate");
            tableMapping.ColumnMappings.Add("IsSuspended", "IsSuspended");
            tableMapping.ColumnMappings.Add("LocationId", "LocationId");
            tableMapping.ColumnMappings.Add("StatusId", "StatusId");
            tableMapping.ColumnMappings.Add("RequestPartitionId", "RequestPartitionId");
            tableMapping.ColumnMappings.Add("OrdererNmcId", "OrdererNmcId");
            tableMapping.ColumnMappings.Add("SessionDeclare", "SessionDeclare");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("RequestType", "RequestType");
            tableMapping.ColumnMappings.Add("RequestDescription", "RequestDescription");
            tableMapping.ColumnMappings.Add("AnswerDate", "AnswerDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectSession_SessionRequests";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@RequestId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@SessionId", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteSession_SessionRequests";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_RequestId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RequestId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertSession_SessionRequests";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SessionId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionTitle", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "SessionTitle", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionDescription", global::System.Data.SqlDbType.NVarChar, 1023, global::System.Data.ParameterDirection.Input, 0, 0, "SessionDescription", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "StartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartTime", global::System.Data.SqlDbType.NVarChar, 5, global::System.Data.ParameterDirection.Input, 0, 0, "StartTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndTime", global::System.Data.SqlDbType.NVarChar, 5, global::System.Data.ParameterDirection.Input, 0, 0, "EndTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "EndDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LocationId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LocationId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsSuspended", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "IsSuspended", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StatusId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "StatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestPartitionId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RequestPartitionId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OrdererNmcId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "OrdererNmcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionDeclare", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SessionDeclare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 1023, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNo", global::System.Data.SqlDbType.NVarChar, 20, global::System.Data.ParameterDirection.Input, 0, 0, "LetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "LetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestType", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "RequestType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestDescription", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "RequestDescription", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateSession_SessionRequests";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SessionId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionTitle", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "SessionTitle", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionDescription", global::System.Data.SqlDbType.NVarChar, 1023, global::System.Data.ParameterDirection.Input, 0, 0, "SessionDescription", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "StartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartTime", global::System.Data.SqlDbType.NVarChar, 5, global::System.Data.ParameterDirection.Input, 0, 0, "StartTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndTime", global::System.Data.SqlDbType.NVarChar, 5, global::System.Data.ParameterDirection.Input, 0, 0, "EndTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "EndDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsSuspended", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "IsSuspended", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LocationId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LocationId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StatusId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "StatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestPartitionId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RequestPartitionId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OrdererNmcId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "OrdererNmcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SessionDeclare", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SessionDeclare", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 1023, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNo", global::System.Data.SqlDbType.NVarChar, 20, global::System.Data.ParameterDirection.Input, 0, 0, "LetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "LetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestType", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "RequestType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestDescription", global::System.Data.SqlDbType.NVarChar, 255, global::System.Data.ParameterDirection.Input, 0, 0, "RequestDescription", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_RequestId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RequestId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "RequestId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.Session.SessionDataSet.SessionRequestsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindById(int Id)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RequestId"].Value = Id;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindBySessionId(int SessionId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@SessionId"].Value = SessionId;
            Fill();
            return this.DataTable;
        }

        public String FindBySessionNumber(String SessionNumber)
        {
            ResetAllParameters();
            this.DataTable.Clear();
            //String AllowedRequestTypes = (int)RequestTypesManager.Types.Save + "," + (int)RequestTypesManager.Types.Edit;

            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_SessionRequestsBySessionNumber";
            objCommand.Parameters.AddWithValue("@SessionNumber", SessionNumber);
            //objCommand.Parameters.AddWithValue("@AllowRequestTypes", AllowedRequestTypes);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(this.DataTable);
            }
            catch (Exception)
            {
                return "خطایی در خواندن اطلاعات ایجاد گردیده است";
            }

            if (this.DataTable.Rows.Count > 0)
                return String.Empty;
            else
                return "برای این جلسه، درخواستی ثبت نشده است ویا این جلسه در سیستم ثبت نشده است";
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetDataWithPermission(int UserId)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_SessionsByMainRequest";
            objCommand.Parameters.AddWithValue("@UserId", UserId);
            objCommand.Parameters.AddWithValue("@WorkFlowCode", (int)TSP.DataManager.WorkFlows.Session);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return objTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSessionSubRequests(int SessionId)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectSession_SessionSubRequests";
            objCommand.Parameters.AddWithValue("@SessionId", SessionId);
            objCommand.Parameters.AddWithValue("@WorkFlowCode", (int)TSP.DataManager.WorkFlows.Session);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return objTable;
        }

        #region WF Methods
        /// <summary>
        /// بررسی سطح دسترسی برای مدیر مربوطه
        /// </summary>
        /// <param name="RequestId"></param>
        /// <param name="CurrentTaskCode"></param>
        /// <returns></returns>
        public int CheckPermissionOfConfirmingRelatedManager(int RequestId, int CurrentTaskCode, int CurrentUserId)
        {
            int Per = 0;
            if (CurrentTaskCode == (int)WorkFlowTask.ManagerConfirmingSession)
            {
                this.FindById(RequestId);
                if (this.Count == 1)
                {
                    Session.SessionsManager SessionsManager = new Session.SessionsManager();
                    SessionsManager.FindById(Convert.ToInt32(this[0]["SessionId"]));
                    if (SessionsManager.Count != 1)
                        return (int)ErrorRequest.LoseRequestInfo;

                    DataManager.LoginManager LoginManager = new DataManager.LoginManager();
                    LoginManager.FindByCode(CurrentUserId);
                    if (LoginManager.Count > 0)
                    {
                        int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                        int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
                        DataManager.NezamChartManager NezamChartManager = new DataManager.NezamChartManager();
                        NezamChartManager.FindByEmpId(EmpId, UltId, 0);
                        if (NezamChartManager.Count > 0)
                        {
                            for (int i = 0; i < NezamChartManager.Count; i++)
                            {
                                if (Convert.ToInt32(NezamChartManager[i]["NcId"]) == Convert.ToInt32(SessionsManager[0]["SessionTypeConfirmPosition"]))
                                    return 0; // سطح دسترسی دارد
                            }
                            Per = (int)ErrorRequest.SessionNoPermissionForType;
                        }
                        else
                        {
                            Per = (int)ErrorRequest.LoseRequestInfo;
                        }
                    }
                    else
                    {
                        Per = (int)ErrorRequest.LoseRequestInfo;
                    }
                }
                else
                {
                    Per = (int)ErrorRequest.LoseRequestInfo;
                }
            }

            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Confirming SessionRequest
        /// </summary>
        /// <param name="RequestId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfConfirming(int RequestId, int CurrentUserId)
        {
            int Per = 0;

            this.FindById(RequestId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = (int)WorkFlowRequestConfirmStatus.Confirm;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                if (this.Save() > 0)
                {
                    this.DataTable.AcceptChanges();

                    Per = 0;

                    SessionsManager.FindById(Convert.ToInt32(this[0]["SessionId"]));
                    if (SessionsManager.Count > 0)
                    {
                        SessionsManager[0].BeginEdit();
                        SessionsManager[0]["SessionTitle"] = this[0]["SessionTitle"].ToString();
                        SetSessionsValueFromRequest("SessionDescription", "SessionDescription");
                        SetSessionsValueFromRequest("StartDate", "StartDate");
                        SetSessionsValueFromRequest("StartTime", "StartTime");
                        SetSessionsValueFromRequest("EndTime", "EndTime");
                        SetSessionsValueFromRequest("EndDate", "EndDate");
                        SessionsManager[0]["IsSuspended"] = this[0]["IsSuspended"].ToString();
                        SessionsManager[0]["LocationId"] = this[0]["LocationId"].ToString();
                        SetSessionsValueFromRequest("RequestPartitionId", "RequestPartitionId");
                        SetSessionsValueFromRequest("OrdererNmcId", "OrdererNmcId");
                        SessionsManager[0]["SessionDeclare"] = this[0]["SessionDeclare"].ToString();
                        SetSessionsValueFromRequest("Description", "Description");
                        switch (Convert.ToInt32(this[0]["RequestType"]))
                        {
                            case (int)RequestTypesManager.Types.Save:
                                SessionsManager[0]["IsConfirm"] = (int)WorkFlowRequestConfirmStatus.Confirm;
                                SessionsManager[0]["StatusId"] = (int)SessionStatusManager.Status.Holding;
                                break;
                            case (int)RequestTypesManager.Types.Edit:
                                SessionsManager[0]["StatusId"] = (int)SessionStatusManager.Status.Holding;
                                break;
                            case (int)RequestTypesManager.Types.ChangeDateTime:
                                if (Convert.ToBoolean(this[0]["IsSuspended"]) == false)
                                    SessionsManager[0]["StatusId"] = (int)SessionStatusManager.Status.Holding;
                                else
                                    SessionsManager[0]["StatusId"] = (int)SessionStatusManager.Status.Suspended;
                                break;
                            case (int)RequestTypesManager.Types.Cancel:
                                SessionsManager[0]["StatusId"] = (int)SessionStatusManager.Status.Cancel;
                                break;
                            case (int)RequestTypesManager.Types.MeetingMinute:
                                SessionsManager[0]["StatusId"] = (int)SessionStatusManager.Status.Held;
                                break;
                        }
                        SessionsManager[0]["UserId"] = CurrentUserId;
                        SessionsManager[0]["ModifiedDate"] = DateTime.Now;
                        SessionsManager[0].EndEdit();
                        if (SessionsManager.Save() > 0)
                            SessionsManager.DataTable.AcceptChanges();
                        else
                            Per = (int)TSP.DataManager.ErrorRequest.LoseRequestInfo;
                    }
                    else
                    {
                        Per = (int)TSP.DataManager.ErrorRequest.LoseRequestInfo;
                    }
                }
                else
                {
                    Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
                }
            }
            else
            {
                Per = (int)TSP.DataManager.ErrorRequest.LoseRequestInfo;
            }

            return Per;
        }

        private void SetSessionsValueFromRequest(String SessionColumnName, String RequestColumnName)
        {
            if (Utility.IsDBNullOrNullValue(this[0][RequestColumnName]) == false)
                SessionsManager[0][SessionColumnName] = this[0][RequestColumnName].ToString();
            else
                SessionsManager[0][SessionColumnName] = DBNull.Value;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting SessionRequest
        /// </summary>
        /// <param name="RequestId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejecting(int RequestId, int CurrentUserId)
        {
            int Per = 0;
            this.FindById(RequestId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = (int)WorkFlowRequestConfirmStatus.Reject;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                if (this.Save() > 0)
                {
                    Per = 0;

                    SessionsManager.FindById(Convert.ToInt32(this[0]["SessionId"]));
                    SessionsManager[0].BeginEdit();
                    SessionsManager[0]["IsConfirm"] = (int)WorkFlowRequestConfirmStatus.Reject;
                    SessionsManager[0]["StatusId"] = (int)SessionStatusManager.Status.Reject;
                    SessionsManager[0]["UserId"] = CurrentUserId;
                    SessionsManager[0]["ModifiedDate"] = DateTime.Now;
                    SessionsManager[0].EndEdit();
                    if (SessionsManager.Save() > 0)
                        Per = 0;
                    else
                        Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
                }
                else
                {
                    Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
                }
            }
            else
            {
                Per = (int)TSP.DataManager.ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        #endregion
    }
}
