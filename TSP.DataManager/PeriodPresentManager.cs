using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TSP.DataManager
{
    public class PeriodPresentManager : BaseObject
    {
        #region WF Methods
        /// <summary>
        /// Perform the next tasks of Confirming PeriodPresent
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfConfirming(int PPId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(PPId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                if (Convert.ToInt32(this[0]["Status"]) == (int)PeriodPresentStatus.Inserting)
                    this[0]["Status"] = (int)PeriodPresentStatus.PeriodRegister;//1;//تایید شده
                else
                    return ((int)ErrorRequest.LoseRequestInfo);
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
        /// Perform the next tasks of Rejecting PeriodPresent
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
                this[0]["Status"] = 3;//لغو شده
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

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TrainingDataSet.tblPeriodPresentDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.PeriodPresent);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblPeriodPresent";
            tableMapping.ColumnMappings.Add("PPId", "PPId");
            tableMapping.ColumnMappings.Add("PPCode", "PPCode");
            tableMapping.ColumnMappings.Add("InsId", "InsId");
            tableMapping.ColumnMappings.Add("CrsId", "CrsId");
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
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("PollId", "PollId"); 
            tableMapping.ColumnMappings.Add("CapacityOnlyExam", "CapacityOnlyExam");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.SelectPeriodPresent";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PPId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InsId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Status", System.Data.SqlDbType.Int);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeletePeriodPresent";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PPId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PPId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertPeriodPresent";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PPCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PPCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "InsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CrsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "PeriodType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Place", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Place", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Capacity", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Capacity", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "TestDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestHour", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "TestHour", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TestPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestCost", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "TestCost", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodCost", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "PeriodCost", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Discount", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "Discount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Status", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartRegisterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "StartRegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndRegisterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EndRegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PollId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PollId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CapacityOnlyExam", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CapacityOnlyExam", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSMSSent", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSMSSent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendSMSDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SendSMSDate", System.Data.DataRowVersion.Current, false, null, "", "", "")); 

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdatePeriodPresent";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PPCode", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PPCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "InsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CrsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CrsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "PeriodType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "StartDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "EndDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Place", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Place", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Capacity", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Capacity", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "TestDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestHour", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "TestHour", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TestPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodCost", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "PeriodCost", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestCost", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "TestCost", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Discount", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "Discount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Status", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartRegisterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "StartRegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndRegisterDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "EndRegisterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PPId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PPId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PPId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PPId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PollId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PollId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CapacityOnlyExam", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CapacityOnlyExam", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsSMSSent", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsSMSSent", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SendSMSDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SendSMSDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public void FindByCode(int PPId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@PPId"].Value = PPId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindMembersByDate()    //Show special Members of tblMember
        {
            return FindMembersByDate(-1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindMembersByDate(int PType)   
        {
            DataTable dt = new DataManager.TrainingDataSet.tblPeriodPresentDataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectActivePeriodPresent", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@Date", SqlDbType.Char, 10);
            ad.SelectCommand.Parameters["@Date"].Value = Utility.GetDateOfToday();// PerDate;
            ad.SelectCommand.Parameters.AddWithValue("@PType", PType);

            ad.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchPeriod(string PeriodTitle)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrWhiteSpace(PeriodTitle) || PeriodTitle == "%")
                return (dt);
            SqlDataAdapter ad = new SqlDataAdapter("spSelectActivePeriodPresent", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@Date", SqlDbType.Char, 10);
            ad.SelectCommand.Parameters["@Date"].Value = Utility.GetDateOfToday();// PerDate;
            ad.SelectCommand.Parameters.AddWithValue("@PeriodTitle", PeriodTitle);
            ad.SelectCommand.Parameters.AddWithValue("@PType", -1);

            ad.Fill(dt);
            return (dt);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindPeriodPresentByDateForMemberRegister(int PType)    //Show special Members of tblMember
        {
            DataTable dt = new DataManager.TrainingDataSet.tblPeriodPresentDataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectActivePeriodPresentForMemberRegister", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@Date", SqlDbType.Char, 10);

            DateTime date = new DateTime();
            date = DateTime.Now;
            System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
            string PerDate = string.Format("{0},{1},{2}", pDate.GetYear(date).ToString().PadLeft(4, '0'), pDate.GetMonth(date).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(date).ToString().PadLeft(2, '0'));


            if (string.IsNullOrEmpty(PerDate.ToString()))
            {
                PerDate = "1";
            }
            else
            {
                PerDate = PerDate.Replace(',', '/');
            }
            ad.SelectCommand.Parameters["@Date"].Value = PerDate;
            ad.SelectCommand.Parameters.AddWithValue("@PType", PType);

            ad.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriods(int PPId, int InsId, int Status)
        {
            DataTable dt = new DataManager.TrainingDataSet.tblPeriodPresentDataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectPeriodPresent", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4).Value = PPId;
            ad.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4).Value = InsId;
            ad.SelectCommand.Parameters.Add("@Status", SqlDbType.Int, 4).Value = Status;
            ad.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)WorkFlows.PeriodConfirming;

            ad.Fill(dt);
            return (dt);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodsAndSeminars(int PPId, int InsId, string TodayDate)
        {
            //DataTable dt = new DataManager.NezamFarsDataSet.tblPeriodPresentDataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectPeriodAndSeminar", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4).Value = PPId;
            ad.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4).Value = InsId;
            ad.SelectCommand.Parameters.Add("@TodayDate", SqlDbType.Char, 10).Value = TodayDate;
            ad.SelectCommand.Parameters.AddWithValue("@ISOutTimePeriodReg", 0);
            ad.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodsAndSeminars(int PPId, int InsId, string TodayDate, int ISOutTimePeriodReg)
        {
            //DataTable dt = new DataManager.NezamFarsDataSet.tblPeriodPresentDataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectPeriodAndSeminar", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4).Value = PPId;
            ad.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4).Value = InsId;
            ad.SelectCommand.Parameters.Add("@TodayDate", SqlDbType.Char, 10).Value = TodayDate;
            ad.SelectCommand.Parameters.AddWithValue("@ISOutTimePeriodReg", ISOutTimePeriodReg);
            ad.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodPresentForManagmentPage(int PPId, int PPRId, int InsId, int Status, string StartDateFrom, string StartDateTo, string EndDateFrom, string EndDateTo, string TestDateFrom, string TestDateTo, int CrsId, int TaskId, string PPCode)
        {
            DataTable dt = new DataTable();
            if (PPId == -1 && PPRId == -1 && InsId == -1 && Status == -1 && StartDateFrom == "1" && StartDateTo == "2" && EndDateFrom == "1" && EndDateTo == "2" && TestDateFrom == "1" && TestDateTo == "2" && CrsId == -1 && TaskId==-1 && PPCode=="%")
                return dt;

            SqlDataAdapter ad = new SqlDataAdapter("spSelectPeriodPresentForManagment", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4).Value = PPId;
            ad.SelectCommand.Parameters.Add("@PPRId", SqlDbType.Int, 4).Value = PPRId;
            ad.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4).Value = InsId;
            ad.SelectCommand.Parameters.Add("@Status", SqlDbType.Int, 4).Value = Status;
            ad.SelectCommand.Parameters.AddWithValue("@CrsId", CrsId);
            ad.SelectCommand.Parameters.AddWithValue("@StartDateFrom", StartDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@StartDateTo", StartDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@TestDateFrom", TestDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@TestDateTo", TestDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);
            ad.SelectCommand.Parameters.AddWithValue("@PPCode", PPCode);
            ad.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.PeriodPresentRequest));
            
            ad.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodPresentForManagmentPageForSettlement(int PPId, int PPRId, int InsId, int Status, string StartDateFrom, string StartDateTo, string EndDateFrom, string EndDateTo, string TestDateFrom, string TestDateTo, int CrsId, int TaskId, string PPCode)
        {
            DataTable dt = new DataTable();
            if (PPId == -1 && PPRId == -1 && InsId == -1 && Status == -1 && StartDateFrom == "1" && StartDateTo == "2" && EndDateFrom == "1" && EndDateTo == "2" && TestDateFrom == "1" && TestDateTo == "2" && CrsId == -1 && TaskId == -1 && PPCode == "%")
                return dt;

            SqlDataAdapter ad = new SqlDataAdapter("spSelectPeriodPresentForManagmentForSettlment", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4).Value = PPId;
            ad.SelectCommand.Parameters.Add("@PPRId", SqlDbType.Int, 4).Value = PPRId;
            ad.SelectCommand.Parameters.Add("@InsId", SqlDbType.Int, 4).Value = InsId;
            ad.SelectCommand.Parameters.Add("@Status", SqlDbType.Int, 4).Value = Status;
            ad.SelectCommand.Parameters.AddWithValue("@CrsId", CrsId);
            ad.SelectCommand.Parameters.AddWithValue("@StartDateFrom", StartDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@StartDateTo", StartDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@TestDateFrom", TestDateFrom);
            ad.SelectCommand.Parameters.AddWithValue("@TestDateTo", TestDateTo);
            ad.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);
            ad.SelectCommand.Parameters.AddWithValue("@PPCode", PPCode);
            ad.SelectCommand.Parameters.AddWithValue("@TaskCode", (int)WorkFlowTask.LearningManagerConfirmingPeriod);
            ad.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.PeriodPresentRequest));

            ad.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodPresentForManagmentPage(int PPId, int PPRId, int InsId, int Status)
        {         
            return SelectPeriodPresentForManagmentPage(PPId, PPRId, InsId, Status, "1", "2", "1", "2", "1", "2", -1,-1,"%");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodPresentHasEpayment(Boolean IsFill, int PPId)
        {
            DataTable dt = new DataTable();
            if (!IsFill)
                return dt;
            SqlDataAdapter ad = new SqlDataAdapter("spSelectPriodPresentHasEpayment", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.AddWithValue("@TableTypePeriodRegister", TableTypeManager.FindTtId(TableType.PeriodRegister));
            ad.SelectCommand.Parameters.AddWithValue("@PPId", PPId);

            ad.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodPresentHasEpayment(Boolean IsFill)
        {
            return SelectPeriodPresentHasEpayment(IsFill, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPeriodPresentHasEpayment(int PPId)
        {
            return SelectPeriodPresentHasEpayment(true, PPId);
        }

        public DataTable spReportPeriods(int PPId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spReportPeriods", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4).Value = PPId;
            ad.Fill(dt);
            return (dt);
        }

        public DataTable spReportPeriodTeachers(int PPId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spReportPeriodTeachers", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4).Value = PPId;
            ad.Fill(dt);
            return (dt);
        }

        public DataTable SelectPeriodPresentWorkflow(int PPId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectPeriodPresentWorkflow", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PPId", SqlDbType.Int, 4, "PPId").Value = PPId;
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)(WorkFlows.PeriodConfirming));
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCodeRiasat", (int)(WorkFlowTask.PeriodConfirmingByRiasatSazemanAndSign));
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCodeEndConfirm", (int)(WorkFlowTask.ConfirmPeriodAndEndProccess));

            adapter.SelectCommand.Transaction = this.Transaction;           
            adapter.Fill(dt);
            return (dt);
        }
    }
}
