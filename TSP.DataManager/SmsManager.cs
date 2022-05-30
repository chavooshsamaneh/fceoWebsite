using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace TSP.DataManager
{
    public enum ErrorSMSRequest
    {
        //***SendMessage Error
        ErrorInSendingSMS = 1,
        SendSuccessfully = 2,
        MobileNumberIsEmpty = 3,
        VirtualNumberIsEmpty = 4,
        MessageBodyIsInvalid = 5,
        MessageTypeIsInvalid = 6,
        MessageIsUnKnow = 7,
        MobileArrayIsEmpty = 8,
        MessageIsTooLong = 9,
        UserNotEnable = 10,
        NoCredit = 11,
        QuotaFull = 12,
        WrongNumber = 13,
        UsernameOrPasswordWrong = 14,
        UsernameOrPasswordIsNull = 15,
        NumberIsInBlackList = 16,
        UnRecognizedMessageBody = 17,

        //***GetStatus Error
        ServiceIsDisabled = 18,
        UsernameIsNullOrEmpty = 19,
        //UserNotEnable,
        PassWordIsNullOrEmpty = 20,
        MessageSendIDArrayIsNull = 21,
        MoreThan10MessageSendID = 22,
        //*GetStatus ReturnValue Types
        Indeterminate = 23,
        SentToMobile = 24,
        FailedToMobile = 25,
        SendToComunicationCenter = 26,
        FailedToComunicationCenter = 27,
        Pending = 28,
        UnKnown = 29,
        //***Others
        SMSExpired = 30,
        SMSInfoWasLost = 31,
    }

    public class SmsManager : BaseObject
    {
        public string FindSMSDeliveryMsg(int ErrorCode)
        {
            string ErrorMsg = "";
            switch (ErrorCode)
            {
                case (int)ErrorSMSRequest.SMSExpired:
                    ErrorMsg = "تاریخ اعتبار پیام کوتاه مورد نظر به پایان رسیده است.";
                    break;
                case (int)ErrorSMSRequest.MobileNumberIsEmpty:
                    ErrorMsg = "پارامتر شماره موبایل خالی است و حاوی هیچ مقداری نمی باشد.";
                    break;
                case (int)ErrorSMSRequest.VirtualNumberIsEmpty:
                    ErrorMsg = "پارامتر شماره اختصاصی خالی است و حاوی هیچ مقداری نمی باشد.";
                    break;
                case (int)ErrorSMSRequest.MessageBodyIsInvalid:
                    ErrorMsg = "متن پیام کوتاه معتبر نمی باشد.";
                    break;
                case (int)ErrorSMSRequest.MessageTypeIsInvalid:
                    ErrorMsg = "مقدار پارامتر Message Typeمعتبر نمی باشد.";
                    break;
                case (int)ErrorSMSRequest.MessageIsUnKnow:
                    ErrorMsg = "پیام معتبر نیست.";
                    break;
                case (int)ErrorSMSRequest.MobileArrayIsEmpty:
                    ErrorMsg = "لیست شماره موبایل ها خالی است.";
                    break;
                case (int)ErrorSMSRequest.MessageIsTooLong:
                    ErrorMsg = ".متن پیام طولانی تر از حد مجاز است";
                    break;
                case (int)ErrorSMSRequest.UserNotEnable:
                    ErrorMsg = "نام کاربری غیر فعال است.";
                    break;
                case (int)ErrorSMSRequest.NoCredit:
                    ErrorMsg = "اعتبار شما به اتمام رسیده است.";
                    break;
                case (int)ErrorSMSRequest.QuotaFull:
                    ErrorMsg = "محدودیت مصرف روزانه شما به اتمام رسیده است.";
                    break;
                case (int)ErrorSMSRequest.WrongNumber:
                    ErrorMsg = "شماره اختصاصی اشتباه است.";
                    break;
                case (int)ErrorSMSRequest.UsernameOrPasswordWrong:
                    ErrorMsg = "نام کاربری یا رمز عبور اشتباه است.";
                    break;
                case (int)ErrorSMSRequest.SendSuccessfully:
                    ErrorMsg = "ذخیره انجام شد.";//"پیام کوتاه با موفقیت ارسال شد.";
                    break;
                case (int)ErrorSMSRequest.ErrorInSendingSMS:
                    ErrorMsg = "در ارسال پیام کوتاه خطا ایجاد شد";
                    break;
            }
            return ErrorMsg;
        }

        #region WF Methods
        public int CheckPermissionSMSConfirmingSendBackTask(int SMSId, int CurrentTaskCode)
        {
            int Per = 0;
            this.FindByCode(SMSId);
            if (this.Count == 1)
            {
                if ((Convert.ToBoolean(this[0]["InActive"])))
                {
                    Per = (int)ErrorRequest.SMSWasInActiveBySender;
                }                
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        public int DoNextTaskOfConfirming(int TableId)
        {
            int Per = 0;
            this.FindByCode(TableId);
            if (this.Count == 1)
            {
                string SMSExpireDate = this[0]["ExpireDate"].ToString();
                string DateNow = Utility.GetDateOfToday();
                int IsExpired = string.Compare(SMSExpireDate, DateNow);
                if (IsExpired < 0)
                {
                    Per = (int)TSP.DataManager.ErrorRequest.SMSExpired;
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
            return BaseObject.GetUserPermission(UserId, ut, TableType.SMS);
        }

        public static Permission GetUserPermissionForSmsSetting(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.SmsSetting);
        }

        public static Permission GetUserPermissionForSmsInbox(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.SmsInbox);
        }

        public static Permission GetUserPermissionForTempPassSetting(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TempPassSetting);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblSms";
            tableMapping.ColumnMappings.Add("SmsId", "SmsId");
            tableMapping.ColumnMappings.Add("SMSDate", "SMSDate");
            tableMapping.ColumnMappings.Add("SMSTime", "SMSTime");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("IsFarsi", "IsFarsi");
            tableMapping.ColumnMappings.Add("SmsCount", "SmsCount");
            tableMapping.ColumnMappings.Add("CostId", "CostId");
            tableMapping.ColumnMappings.Add("SmsTypeId", "SmsTypeId");
            tableMapping.ColumnMappings.Add("SenderId", "SenderId");
            tableMapping.ColumnMappings.Add("SmsSubject", "SmsSubject");
            tableMapping.ColumnMappings.Add("SmsBody", "SmsBody");
            tableMapping.ColumnMappings.Add("SmsCost", "SmsCost");
            tableMapping.ColumnMappings.Add("IsDelivered", "IsDelivered");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("SmsDocId", "SmsDocId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("SMSSendDate", "SMSSendDate");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("PartId", "PartId");
            tableMapping.ColumnMappings.Add("SMSDotoDate", "SMSDotoDate");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectSms";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "SmsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteSms";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SmsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertSms";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSTime", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFarsi", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsFarsi", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsCount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsCount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CostId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CostId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SenderId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SenderId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsSubject", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsSubject", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsBody", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsBody", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsCost", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsCost", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDelivered", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDelivered", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsDocId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsDocId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PartId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PartId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSSendDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSSendDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSDotoDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSDotoDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateSms";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSTime", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsFarsi", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsFarsi", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsCount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsCount", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CostId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CostId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SenderId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SenderId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsSubject", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsSubject", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsBody", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsBody", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsCost", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsCost", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDelivered", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDelivered", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsDocId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsDocId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SmsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SmsId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SmsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "SmsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSSendDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSSendDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PartId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PartId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SMSDotoDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SMSDotoDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.SMSDataSet.tblSmsDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void FindByCode(int SMSId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@SmsId"].Value = SMSId;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSMSJoinSMSCost(int SMSId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSMSBySMSCost", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@SmsId", SqlDbType.Int, 4, "SmsId").Value = SMSId;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchSMS(string SMSStartDate, string SMSEndDate, int SaverEmpId, int IsFarsi, int SmsTypeId, int RecieverId, string RecieverCellPhone
    , string ExpireDateFrom, string ExpireDateTo, string SMSDotoDateTo, string SMSDotoDateFrom)
        {
            return SearchSMS("%", SMSStartDate,  SMSEndDate,  SaverEmpId,  IsFarsi,  SmsTypeId,  RecieverId,  RecieverCellPhone
     ,  ExpireDateFrom,  ExpireDateTo,  SMSDotoDateTo,  SMSDotoDateFrom);
        }
        // int InActive, string SMSDate, string SMSTime, int SMSId
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SearchSMS(string SmsSubject, string SMSStartDate, string SMSEndDate, int SaverEmpId, int IsFarsi, int SmsTypeId, int RecieverId, string RecieverCellPhone
            , string ExpireDateFrom, string ExpireDateTo, string SMSDotoDateTo, string SMSDotoDateFrom)
        {
            if (SMSStartDate == "9999/99/99" && SMSEndDate == "9999/99/99" && RecieverId == -1
                && IsFarsi == -1 && SmsTypeId == -1 && RecieverCellPhone == "%" &&  SmsSubject == "%"
                && ExpireDateFrom == "9999/99/99" && ExpireDateTo == "9999/99/99" && SMSDotoDateTo == "9999/99/99" && SMSDotoDateFrom == "9999/99/99")
                return new System.Data.DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSMSBySenderId", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@SmsSubject", SmsSubject);
            adapter.SelectCommand.Parameters.AddWithValue("@SMSStartDate", SMSStartDate);
            adapter.SelectCommand.Parameters.AddWithValue("@SMSEndDate", SMSEndDate);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverId", RecieverId);
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverCellPhone", RecieverCellPhone);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", (int)TableCodes.SMS);
            adapter.SelectCommand.Parameters.AddWithValue("@ExpireDateFrom", ExpireDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@ExpireDateTo", ExpireDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@SMSDotoDateTo", SMSDotoDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@SMSDotoDateFrom", SMSDotoDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@SmsTypeId", SmsTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsFarsi", IsFarsi);
            adapter.SelectCommand.Parameters.AddWithValue("@SaverEmpId", SaverEmpId);
            
            adapter.Fill(dt);
            return (dt);
      
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable ReceivedSMS(string SMSDateFrom, string SMSDateTo)
        {
            DataTable dt = new DataTable();
            if (SMSDateFrom == "1" && SMSDateTo == "2")
                return dt;
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectSMSInbox", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@SMSDateFrom", SMSDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@SMSDateTo", SMSDateTo);
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSMSByRecieverId(int RecieverId, int IsDelivered)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectSMSByRecieverId", this.Connection);
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@RecieverId", RecieverId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsDelivered", IsDelivered);

            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSMSByRecieverId(int RecieverId)
        {
            return SelectSMSByRecieverId(RecieverId, -1);
        }
    }
}
