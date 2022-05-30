using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class PeriodRegisterManager : BaseObject
    {
        #region WF Methods
        /// <summary>
        /// Perform the next tasks of Confirming Request
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfConfirming(int PRId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(PRId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 1;//تایید شده
                this[0]["UserId"] = CurrentUserId;
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
        /// Perform the next tasks of Rejecting Request
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejecting(int PRId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(PRId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 0;//لغو شده
                this[0]["UserId"] = CurrentUserId;
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

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PeriodRegister);
        }

        public static Permission GetUserPermissionReportMemberPeriods(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.ReportMemberPeriods);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblPeriodRegister";
            tableMapping.ColumnMappings.Add("PRId", "PRId");
            tableMapping.ColumnMappings.Add("PPId", "PPId");
            tableMapping.ColumnMappings.Add("IsSeminar", "IsSeminar");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("RegisterType", "RegisterType");
            tableMapping.ColumnMappings.Add("PaymentType", "PaymentType");
            tableMapping.ColumnMappings.Add("IsMember", "IsMember");
            tableMapping.ColumnMappings.Add("RegisterDate", "RegisterDate");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("SerialNo", "SerialNo");//برای هر دوره از 1 شروع می شود و مقدارهی با استفاده از trigger است
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("IsPassed", "IsPassed");
            tableMapping.ColumnMappings.Add("ConfAttachUrl", "ConfAttachUrl");
            tableMapping.ColumnMappings.Add("IsPresent", "IsPresent");
            tableMapping.ColumnMappings.Add("TotalTimePresent", "TotalTimePresent");
            tableMapping.ColumnMappings.Add("TotalPresentSessions", "TotalPresentSessions");





            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectPeriodRegister";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PRId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PPId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PPId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSeminar", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "IsSeminar", System.Data.DataRowVersion.Current, false, null, "", "", ""));


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeletePeriodRegister";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PRId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PRId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;

            this.Adapter.InsertCommand.CommandText = "dbo.spInsertPeriodRegister";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PPId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PPId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSeminar", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSeminar", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMember", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMember", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsPassed", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsPassed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConfAttachUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ConfAttachUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsPresent", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsPresent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalTimePresent", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TotalTimePresent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalPresentSessions", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TotalPresentSessions", System.Data.DataRowVersion.Current, false, null, "", "", ""));


            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdatePeriodRegister";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PPId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PPId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSeminar", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSeminar", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "PaymentType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMember", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMember", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PRId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PRId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PRId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsPassed", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsPassed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConfAttachUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ConfAttachUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsPresent", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsPresent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalTimePresent", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TotalTimePresent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalPresentSessions", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "TotalPresentSessions", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblPeriodRegisterDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int PRId)
        {
            //this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PPId"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = -1;
            //this.Adapter.SelectCommand.Parameters["@IsSeminar"].Value = -1;
            this.Adapter.SelectCommand.Parameters["@PRId"].Value = PRId;
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodRegister(int PRId, int MeId, int PPId, int InsId, int IsSeminar)
        {
            return SelectPeriodRegister(PRId, MeId, PPId, InsId, IsSeminar, -1, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodRegister(int PRId, int MeId, int PPId, int InsId, int IsSeminar, int InActive = -1)
        {
            return SelectPeriodRegister(PRId, MeId, PPId, InsId, IsSeminar, -1, InActive);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodRegister(int PRId, int MeId, int PPId, int InsId, int IsSeminar, int IsConfirm, int InActive = -1)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectPeriodRegisterView", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PRId", SqlDbType.Int, 4, "PRId").Value = PRId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4, "PPId").Value = PPId;
            adapter.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4, "InsId").Value = InsId;
            adapter.SelectCommand.Parameters.Add("@IsSeminar", SqlDbType.Int, 4, "IsSeminar").Value = IsSeminar;
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4, "IsConfirm").Value = IsConfirm;
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@TabeTypePeriodRegister", TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodRegisterForTeachers(int PRId, int MeId, int PPId, int InsId, int IsSeminar, int IsConfirm, int InActive = -1)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectPeriodRegisterViewForTeachers", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PRId", SqlDbType.Int, 4, "PRId").Value = PRId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4, "PPId").Value = PPId;
            adapter.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4, "InsId").Value = InsId;
            adapter.SelectCommand.Parameters.Add("@IsSeminar", SqlDbType.Int, 4, "IsSeminar").Value = IsSeminar;
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4, "IsConfirm").Value = IsConfirm;
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@TabeTypePeriodRegister", TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }
        public DataTable SelectPeriodRegister(int PPId, int IsSeminar)
        {
            return (SelectPeriodRegister(-1, -1, PPId, -1, IsSeminar));
        }

        public DataTable SelectPeriodRegister(int MeId, int PPId, int IsSeminar)
        {
            return (SelectPeriodRegister(-1, MeId, PPId, -1, IsSeminar));
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectCourseDocReport(int PPId, int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spReportPeriodMembers", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4, "PPId").Value = PPId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.AddWithValue("@FilterTaskCode", 0);
            adapter.SelectCommand.Parameters.AddWithValue("@FilterTeststatus", 0);

            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodRegisterForPeriod(int PRId, int PPId, int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectPeriodRegisterForPeriods", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4, "PPId").Value = PPId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@PRId", SqlDbType.Int, 4, "PRId").Value = PRId;
            adapter.SelectCommand.Parameters.AddWithValue("@TabeTypePeriodRegister", TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodRegisterForPeriodsForSMS(int PPId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectPeriodRegisterForPeriodsForSMS", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4, "PPId").Value = PPId;
            adapter.SelectCommand.Parameters.AddWithValue("@TabeTypePeriodRegister", TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }
        

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodRegisterForSeminar(int PRId, int PPId, int MeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectPeriodRegisterForSeminar", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4, "PPId").Value = PPId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@PRId", SqlDbType.Int, 4, "PRId").Value = PRId;
            adapter.SelectCommand.Parameters.AddWithValue("@TabeTypePeriodRegister", TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.Fill(dt);
            return (dt);
        }

        public DataTable spReportPeriodMembersWithGradeDate(int CrsId, int MeId, string FromStartDate, string ToStartDate, string FromEndDate, string ToEndDate
            , int ImpGrdId, int ObsGrdId, int DesGrdId, int UrbanismGrdId, int MappingGrdId, int TrafficGrdId,int MjParentId)
        {
            DataTable dt = new DataTable();
            if (CrsId == -1 && MeId == -1 && FromStartDate == "1" && ToStartDate == "2" && FromEndDate == "1" && ToEndDate == "2")// &&
           //  ImpGrdId == -1 && ObsGrdId == -1 && DesGrdId == -1 && UrbanismGrdId == -1 && MappingGrdId == -1 && TrafficGrdId == -1)
                return dt;
            SqlDataAdapter adapter = new SqlDataAdapter("spReportPeriodMembersWithGradeDate", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@CrsId",  CrsId);
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@FromStartDate",FromStartDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToStartDate",  ToStartDate);
            adapter.SelectCommand.Parameters.AddWithValue("@ToEndDate",  ToEndDate);
            adapter.SelectCommand.Parameters.AddWithValue("@FromEndDate", FromEndDate);

            adapter.SelectCommand.Parameters.AddWithValue("@ImpGrdId", ImpGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@ObsGrdId",ObsGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@DesGrdId",  DesGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@UrbanismGrdId",  UrbanismGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@TrafficGrdId", TrafficGrdId);
            adapter.SelectCommand.Parameters.AddWithValue("@MjParentId",  MjParentId);
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.Fill(dt);
            return (dt);
        }

        public DataTable UpdatePeriodRegisterForSeminarExcelPresent(int PPId, DataTable ExcelPresent, int UserId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("UpdatePeriodRegisterForSeminarExcelPresent", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4, "PPId").Value = PPId;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@ExcelPresentTableType", ExcelPresent));
            adapter.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int, 4, "UserId").Value = UserId;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.Fill(dt);
            return (dt);
        }

        public DataTable GetRegisterPaymentIdAndUpdatePeriodRegister(int PRId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("GetRegisterPaymentIdAndUpdatePeriodRegister", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PRId", PRId);
            //adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }

        public static string GetPeriodREgisterTafziliCode(int MeId, int RegisterPaymentId)
        {
            string MeIdString = MeId.ToString();
            string RegisterPaymentIdString = RegisterPaymentId.ToString();
            if (MeIdString.Length < 5)
            {
                int difLenght = 5 - MeIdString.Length;
                for (int i = 0; i < difLenght; i++)
                {
                    MeIdString = "0" + MeIdString;
                }
            }
            if (RegisterPaymentIdString.Length < 4)
            {
                int difLenght = 4 - RegisterPaymentIdString.Length;
                for (int i = 0; i < difLenght; i++)
                {
                    RegisterPaymentIdString = "0" + RegisterPaymentIdString;
                }
            }
            return MeIdString + RegisterPaymentIdString;
        }

        public static Boolean CheckIfRepititiveRegister(int SeId, int MeId)
        {
            TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
            DataTable dtRegister = PeriodRegisterManager.SelectPeriodRegisterForSeminar(-1, SeId, MeId);
            if (dtRegister.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < dtRegister.Rows.Count; i++)
                {
                    if (!Convert.ToBoolean(dtRegister.Rows[i]["InActive"]))
                    {
                        // ShowMessage("شما قبلا در این سمینار ثبت نام کرده اید.");
                        return true;
                    }
                }
                return false;
            }
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectPeriodRegisterForMemberUpgrade(int MeId, int MjId, int ResId, int CurrentGrdId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("selectPeriodRegisterForMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.Add("@GrdIdOrigin", SqlDbType.Int, 4, "GrdIdOrigin").Value = CurrentGrdId;

            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.Fill(dt);
            return (dt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="MjId"></param>
        /// <param name="ResId"></param>
        /// <param name="CurrentGrdId"></param>
        /// <param name="IsMemberPeriod"></param>
        /// <param name="RequestDate">تاریخ آزمون نباید قبل از سه سال  از تاریخ ثبت درخواست ارتقا باشد</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectPeriodRegisterForMemberReport(int MeId, int MjId, int ResId, int CurrentGrdId, int IsMemberPeriod,string RequestDate)
        {

            string ExamValidDate = Utility.AddMonths(RequestDate, -36);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("selectPeriodRegisterForMemberReport", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.Add("@MjId", SqlDbType.Int, 4, "MjId").Value = MjId;
            adapter.SelectCommand.Parameters.Add("@ResId", SqlDbType.Int, 4, "ResId").Value = ResId;
            adapter.SelectCommand.Parameters.Add("@GrdIdOrigin", SqlDbType.Int, 4, "GrdIdOrigin").Value = CurrentGrdId;
            adapter.SelectCommand.Parameters.AddWithValue("@IsMemberPeriod", IsMemberPeriod);
            adapter.SelectCommand.Parameters.AddWithValue("@ValidDate", ExamValidDate);
            

            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectPeriodRegisterForMemberReport(int MeId, int MjId, int ResId, int CurrentGrdId, int IsMemberPeriod)
        {
            return selectPeriodRegisterForMemberReport(MeId, MjId, ResId, CurrentGrdId, IsMemberPeriod,"");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable selectPeriodRegisterForMemberReport(int MeId, int MjId, int ResId, int CurrentGrdId)
        {
            return selectPeriodRegisterForMemberReport(MeId, MjId, ResId, CurrentGrdId, 0);
        }

    }
}
