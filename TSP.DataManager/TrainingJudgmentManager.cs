using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TSP.DataManager
{
    public class TrainingJudgmentManager : BaseObject
    {
        #region Utility Methods
        private static Boolean IsDBNullOrNullValue(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return true;
            if (string.IsNullOrEmpty(obj.ToString()))
                return true;
            return false;
        }
        #endregion

        #region WF Methods
        /// <summary>
        /// Perform the next tasks of Confirming ResearchActivityConfirmingRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfConfirming(int JudgeId, int CurrentUserAgentId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(JudgeId);
            if (this.Count == 1)
            {
                int IsConfirm = -1;
                if (!IsDBNullOrNullValue(this[0]["IsConfirmed"]))
                {
                    IsConfirm = int.Parse(this[0]["IsConfirmed"].ToString());
                    if (IsConfirm == 2)
                    {
                        this[0].BeginEdit();
                        this[0]["IsConfirmed"] = 1;
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
                        Per = (int)ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                        //"امکان ارسال پرونده درخواست انتخاب شده به مرحله بعد وجود ندارد.";
                    }
                }
                else
                {
                    Per = (int)ErrorRequest.LoseRequestInfo;
                    //"وضعیت درخواست انتخاب شده نامشخص است.";
                }
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting ResearchActivityConfirmingRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejecting(int JudgeId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(JudgeId);
            if (this.Count == 1)
            {
                int IsConfirm = -1;
                if (!IsDBNullOrNullValue(this[0]["IsConfirmed"]))
                {
                    IsConfirm = int.Parse(this[0]["IsConfirmed"].ToString());
                    if (IsConfirm == 2)
                    {
                        this[0].BeginEdit();
                        this[0]["IsConfirmed"] = 0;
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
                        Per = (int)ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                        //"امکان ارسال پرونده درخواست انتخاب شده به مرحله بعد وجود ندارد.";
                    }
                }
                else
                {
                    Per = (int)ErrorRequest.LoseRequestInfo;
                    //"وضعیت درخواست انتخاب شده نامشخص است.";
                }
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }
            return Per;
        }
      
        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TrainingJudgment);
        }
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblTrainingJudgment";
            tableMapping.ColumnMappings.Add("JudgeId", "JudgeId");
            tableMapping.ColumnMappings.Add("PkId", "PkId");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("MeetingId", "MeetingId");
            tableMapping.ColumnMappings.Add("MeetingDate", "MeetingDate");
            tableMapping.ColumnMappings.Add("FinancialValue", "FinancialValue");
            tableMapping.ColumnMappings.Add("JudgeViewPoint", "JudgeViewPoint");
            tableMapping.ColumnMappings.Add("JudgeGrade", "JudgeGrade");
            tableMapping.ColumnMappings.Add("JudgeGradeTime", "JudgeGradeTime");
            tableMapping.ColumnMappings.Add("IsConfirmed", "IsConfirmed");
            tableMapping.ColumnMappings.Add("NmcId", "NmcId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTrainingJudgment";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@JudgeId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@NmcId", SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTrainingJudgment";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_JudgeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "JudgeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTrainingJudgment";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PkId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PkId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeetingId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeetingId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeetingDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MeetingDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FinancialValue", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "FinancialValue", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@JudgeViewPoint", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "JudgeViewPoint", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@JudgeGrade", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "JudgeGrade", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@JudgeGradeTime", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "JudgeGradeTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NmcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NmcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTrainingJudgment";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PkId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PkId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeetingId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeetingId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeetingDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MeetingDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FinancialValue", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "FinancialValue", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@JudgeViewPoint", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "JudgeViewPoint", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@JudgeGrade", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "JudgeGrade", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@JudgeGradeTime", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "JudgeGradeTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NmcId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "NmcId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_JudgeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "JudgeId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@JudgeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "JudgeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblTrainingJudgmentDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }
        public void FindByCode(int JudgeId)
        {
            this.Adapter.SelectCommand.Parameters["@JudgeId"].Value = JudgeId;

            Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PkId"></param>
        /// <param name="Type">0:PeriodPresent 1:Seminar 2:ResearchActivity 3:FinancialStatus 4:ProjectJobHistory(Memeber jobHistory)</param>
        public void FindByPKCode(int PkId, int Type)
        {
            this.Adapter.SelectCommand.Parameters["@PkId"].Value = PkId;
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            Fill();
        }

        public void FindByNmcId(int NmcId, int PkId, int Type)
        {
            this.Adapter.SelectCommand.Parameters["@PkId"].Value = PkId;
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            this.Adapter.SelectCommand.Parameters["@NmcId"].Value = NmcId;

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByPkId(int PkId, int Type)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingJudgment", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int, 4, "PkId ").Value = PkId;
            adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.Int, 4, "Type").Value = Type;
            adapter.SelectCommand.Parameters.Add("@JudgeId", SqlDbType.Int, 4, "JudgeId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@NmcId", SqlDbType.Int, 4, "NmcId").Value = -1;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByResearchAct(int PkId, int JudgeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingJudgmentForResearchAct", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int, 4, "PkId").Value = PkId;
            adapter.SelectCommand.Parameters.Add("@JudgeId", SqlDbType.Int, 4, "JudgeId").Value = JudgeId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = (int)WorkFlows.MemberResearchActivity;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByResearchActLastVersion(int PkId)//, int JudgeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingJudgmentLastVersionForResearchAct", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int, 4, "PkId").Value = PkId;
            //  adapter.SelectCommand.Parameters.Add("@JudgeId", SqlDbType.Int, 4, "JudgeId").Value = JudgeId;

            adapter.Fill(dt);
            return (dt);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByFinancial(int PkId, int JudgeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingJudgmentForFinancial", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int, 4, "PkId").Value = PkId;
            adapter.SelectCommand.Parameters.Add("@JudgeId", SqlDbType.Int, 4, "JudgeId").Value = JudgeId;

            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByJobQuality(int PkId, int JudgeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingJudgmentForJobQuality", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int, 4, "PkId").Value = PkId;
            adapter.SelectCommand.Parameters.Add("@JudgeId", SqlDbType.Int, 4, "JudgeId").Value = JudgeId;

            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByJobHistory(int PkId, int JudgeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTrainingJudgmentForJobHistory", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PkId", SqlDbType.Int, 4, "PkId").Value = PkId;
            adapter.SelectCommand.Parameters.Add("@JudgeId", SqlDbType.Int, 4, "JudgeId").Value = JudgeId;

            adapter.Fill(dt);
            return (dt);
        }
    }
}
