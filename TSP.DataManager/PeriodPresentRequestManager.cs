using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class PeriodPresentRequestManager : BaseObject
    {
        #region Private Manager
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager;
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager;
        TSP.DataManager.PeriodPresentManager PeriodPresentManager;
        #endregion

        #region Constructors
        public PeriodPresentRequestManager()
        {

        }
        public PeriodPresentRequestManager(TSP.DataManager.TransactionManager TransactionManager)
        {
            WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowTaskManager = new WorkFlowTaskManager();
            PeriodPresentManager = new PeriodPresentManager();
            TransactionManager.Add(WorkFlowStateManager);
            TransactionManager.Add(WorkFlowTaskManager);
            TransactionManager.Add(PeriodPresentManager);
        }
        #endregion

        #region WF Methods
        /// <summary>
        /// چک کردن شرایط قبل از باز شدن پنجره گردش کار در عضویت
        /// </summary>
        /// <param name="OfReId"></param>
        /// <param name="CurrentTaskCode"></param>
        /// <returns></returns>
        public int CheckPermissionPeriodConfirmingSendBackTask(int PPRId, int CurrentTaskCode)
        {
            int Per = 0;
            if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.PeriodSavePointsByTeachers)
            {
                TSP.DataManager.PeriodPresentRequestManager RequestManager = new PeriodPresentRequestManager();
                TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
                RequestManager.FindByCode(PPRId);
                if (RequestManager.Count <= 0)
                {
                    return (int)ErrorRequest.LoseRequestInfo;

                }
                int PPID = Convert.ToInt32(RequestManager[0]["PPId"]);
                MarkManager.SelectPeriodMarks(PPID);
                if (MarkManager.Count <= 0)
                {
                    return (int)ErrorRequest.PeriodRegisterMarksAreNotInserted;
                }
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Confirming PeriodPresentRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfConfirming(int PPRId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(PPRId);
            if (this.Count > 0)
            {
                int PPId = Convert.ToInt32(this[0]["PPId"]);
                this[0].BeginEdit();
                //  if (Convert.ToInt32(this[0]["Status"]) == (int)PeriodPresentStatus.Inserting)
                //this[0]["Status"] = (int)PeriodPresentStatus.PeriodRegister;//1;//تایید شده
                this[0]["Status"] = (int)PeriodPresentStatus.ConfirmedPeriod;//1;//تایید شده
                //else
                //    return ((int)ErrorRequest.LoseRequestInfo);
                this[0]["IsConfirm"] = 1;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                if (this.Save() > 0)
                {
                    #region Update PeriodPresent
                    PeriodPresentManager.FindByCode(PPId);
                    if (PeriodPresentManager.Count != 1) return (int)ErrorRequest.LoseRequestInfo;
                    PeriodPresentManager[0].BeginEdit();
                    //  if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) == (int)PeriodPresentStatus.Inserting)
                    //      PeriodPresentManager[0]["Status"] = (int)PeriodPresentStatus.PeriodRegister;
                    PeriodPresentManager[0]["IsConfirm"] = 1;
                    PeriodPresentManager[0]["Status"] = (int)PeriodPresentStatus.ConfirmedPeriod;
                    PeriodPresentManager[0]["PPCode"] = this[0]["PPCode"];
                    PeriodPresentManager[0]["InsId"] = this[0]["InsId"];
                    PeriodPresentManager[0]["CrsId"] = this[0]["CrsId"];
                    PeriodPresentManager[0]["PeriodType"] = this[0]["PeriodType"];
                    PeriodPresentManager[0]["StartDate"] = this[0]["StartDate"];
                    PeriodPresentManager[0]["EndDate"] = this[0]["EndDate"];
                    PeriodPresentManager[0]["Place"] = this[0]["Place"];
                    PeriodPresentManager[0]["Description"] = this[0]["Description"];
                    PeriodPresentManager[0]["Capacity"] = this[0]["Capacity"];
                    PeriodPresentManager[0]["TestDate"] = this[0]["TestDate"];
                    PeriodPresentManager[0]["TestHour"] = this[0]["TestHour"];
                    PeriodPresentManager[0]["TestPlace"] = this[0]["TestPlace"];
                    PeriodPresentManager[0]["TestCost"] = this[0]["TestCost"];
                    PeriodPresentManager[0]["PeriodCost"] = this[0]["PeriodCost"];
                    PeriodPresentManager[0]["Discount"] = this[0]["Discount"];
                    PeriodPresentManager[0]["StartRegisterDate"] = this[0]["StartRegisterDate"];
                    PeriodPresentManager[0]["EndRegisterDate"] = this[0]["EndRegisterDate"];
                    PeriodPresentManager[0]["UserId"] = CurrentUserId;
                    PeriodPresentManager[0]["ModifiedDate"] = DateTime.Now;
                    PeriodPresentManager[0].EndEdit();
                    if (PeriodPresentManager.Save() <= 0)
                        return (int)ErrorRequest.Error;
                    #endregion
                    Per = 0;
                }
                else
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

        /// <summary>
        /// Perform the next tasks of Rejecting PeriodPresentRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejecting(int PPId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(PPId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 2;//لغو شده
                this[0]["UserId"] = CurrentUserId;
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
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }

            return Per;
        }

        public int UpdatePeriodStatus(int PPRId, int CurrentUserId, PeriodPresentStatus PeriodStatus)
        {
            int Per = 0;
            this.FindByCode(PPRId);
            if (this.Count <= 0) return (int)ErrorRequest.LoseRequestInfo;
            int PPId = Convert.ToInt32(this[0]["PPId"]);
            PeriodPresentManager.FindByCode(PPId);
            if (PeriodPresentManager.Count != 1) return (int)ErrorRequest.LoseRequestInfo;
            PeriodPresentManager[0].BeginEdit();
            PeriodPresentManager[0]["Status"] = (int)PeriodStatus;
            PeriodPresentManager[0].EndEdit();
            if (PeriodPresentManager.Save() <= 0)
                return (int)ErrorRequest.Error;
            return Per;

        }

        #region WF Methods of Print Priod Certificates
        /// <summary>
        /// Perform the next tasks of Confirming PrintCertificatesWF
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfConfirmingPrintCertificatesWF(int PPRId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(PPRId);
            if (this.Count > 0)
            {
                int PPId = Convert.ToInt32(this[0]["PPId"]);
                this[0].BeginEdit();
                this[0]["Status"] = (int)PeriodPresentStatus.ConfirmedPeriod;//1;//تایید شده
                this[0]["IsConfirm"] = 1;
                this[0]["UserId"] = CurrentUserId;
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
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting PrintCertificatesWF
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejectingPrintCertificatesWF(int PPId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(PPId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 2;//لغو شده
                this[0]["UserId"] = CurrentUserId;
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
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }

            return Per;
        }
        #endregion
        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PeriodPresentRequest);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();     
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblPeriodPresentRequest";
            tableMapping.ColumnMappings.Add("PPRId", "PPRId");
            tableMapping.ColumnMappings.Add("PPId", "PPId");
            tableMapping.ColumnMappings.Add("PPCode", "PPCode");
            tableMapping.ColumnMappings.Add("InsId", "InsId");
            tableMapping.ColumnMappings.Add("CrsId", "CrsId");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("PeriodType", "PeriodType");
            tableMapping.ColumnMappings.Add("StartDate", "StartDate");
            tableMapping.ColumnMappings.Add("EndDate", "EndDate");
            tableMapping.ColumnMappings.Add("Place", "Place");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Capacity", "Capacity");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("TestDate", "TestDate");
            tableMapping.ColumnMappings.Add("TestHour", "TestHour");
            tableMapping.ColumnMappings.Add("TestPlace", "TestPlace");
            tableMapping.ColumnMappings.Add("TestCost", "TestCost");
            tableMapping.ColumnMappings.Add("PeriodCost", "PeriodCost");
            tableMapping.ColumnMappings.Add("Discount", "Discount");
            tableMapping.ColumnMappings.Add("Status", "Status");
            tableMapping.ColumnMappings.Add("StartRegisterDate", "StartRegisterDate");
            tableMapping.ColumnMappings.Add("EndRegisterDate", "EndRegisterDate");
            tableMapping.ColumnMappings.Add("FollowCode", "FollowCode");
            tableMapping.ColumnMappings.Add("AnswerDate", "AnswerDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("UltId", "UltId");
            tableMapping.ColumnMappings.Add("PollId", "PollId");
            tableMapping.ColumnMappings.Add("CapacityOnlyExam", "CapacityOnlyExam");
            this.Adapter.TableMappings.Add(tableMapping);

            Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            Adapter.SelectCommand.Connection = this.Connection;
            Adapter.SelectCommand.CommandText = "dbo.spSelectPeriodPresentRequest";
            Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PPId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PPRId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Type", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeletePeriodPresentRequest";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PPRId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PPRId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertPeriodPresentRequest";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PPId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PPId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PPCode", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PPCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CrsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CrsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PeriodType", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PeriodType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Place", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Place", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Capacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Capacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TestDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestHour", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TestHour", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestPlace", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TestPlace", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestCost", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TestCost", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PeriodCost", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PeriodCost", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Discount", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Discount", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartRegisterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StartRegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndRegisterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndRegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FollowCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FollowCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UltId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UltId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PollId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PollId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityOnlyExam", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityOnlyExam", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdatePeriodPresentRequest";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PPId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PPId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PPCode", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PPCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CrsId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CrsId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PeriodType", global::System.Data.SqlDbType.TinyInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PeriodType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Place", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Place", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Capacity", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Capacity", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TestDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestHour", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TestHour", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestPlace", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TestPlace", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TestCost", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TestCost", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PeriodCost", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PeriodCost", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Discount", global::System.Data.SqlDbType.Money, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Discount", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StartRegisterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StartRegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndRegisterDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndRegisterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FollowCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FollowCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UltId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UltId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PollId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PollId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PPRId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PPRId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PPRId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "PPRId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CapacityOnlyExam", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CapacityOnlyExam", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblPeriodPresentRequestDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int PPRId)
        {
            ResetAllParameters();
            Adapter.SelectCommand.Parameters["@PPRId"].Value = PPRId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByPeriodId(int PPId)
        {
            ResetAllParameters();
            Adapter.SelectCommand.Parameters["@PPId"].Value = PPId;
            Fill();
            return DataTable;
        }

        public void Select(int PPRId, int PPId, int Type, int IsConfirm)
        {
            ResetAllParameters();
            Adapter.SelectCommand.Parameters["@PPRId"].Value = PPRId;
            Adapter.SelectCommand.Parameters["@PPId"].Value = PPId;
            Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            Fill();
        }

        public void DeletePeriodPresentRequestInfo(int PPRId)
        {
            SqlCommand Command = new SqlCommand("spDeletePeriodPresentRequestInfo", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@PPRId", PPRId);
            Command.Parameters.AddWithValue("@Type", -1);
            Command.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.PeriodPresentRequest));
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
                Command.ExecuteNonQuery();
        }

        public void DeletePeriodPresentRequestInfo(int PPId, int PPRId, int Type)
        {
            SqlCommand Command = new SqlCommand("spDeletePeriodPresentRequestInfo", this.Connection);
            Command.Transaction = this.Transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@PPRId", PPRId);
            Command.Parameters.AddWithValue("@PPId", PPId);
            Command.Parameters.AddWithValue("@Type", Type);
            Command.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.PeriodPresentRequest));
            if (Transaction == null)
            {
                try
                {
                    this.Connection.Open();
                    Command.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    if (this.Connection.State == ConnectionState.Open)
                        this.Connection.Close();
                }
            }
            else
                Command.ExecuteNonQuery();
        }

        public DataTable SelectPeriodPresentRequestCount(int CurrentUserNmcId, int TaskCode, int InsId=-1) 
        {
            DataTable dt=new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectPeriodPresentRequestCount", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.PeriodPresentRequest));
            ad.SelectCommand.Parameters.AddWithValue("@CurrentUserNmcId", CurrentUserNmcId);
            ad.SelectCommand.Parameters.AddWithValue("@TaskCode", TaskCode);
            ad.SelectCommand.Parameters.AddWithValue("@InsId", InsId);
            

            ad.Fill(dt);
            return (dt);
        }
    }
}
