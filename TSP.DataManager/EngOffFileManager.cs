using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Data.SqlClient;

namespace TSP.DataManager
{
    public class EngOffFileManager : BaseObject
    {

        #region Utility
        private string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }
        #endregion

        #region Private Managers
        EngOfficeManager EngOfficeManager;
        TSP.DataManager.OfficeMemberManager OfficeMeManager;
        TSP.DataManager.RequestInActivesManager RequestInActivesManager;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager;
        #endregion

        #region Constructors
        public EngOffFileManager()
            : base()
        {

        }

        public EngOffFileManager(TransactionManager TransactionManager)
        {
            EngOfficeManager = new EngOfficeManager();
            OfficeMeManager = new OfficeMemberManager();
            RequestInActivesManager = new RequestInActivesManager();
            DocMemberFileManager = new DocMemberFileManager();
            if (TransactionManager != null)
            {
                TransactionManager.Add(EngOfficeManager);
                TransactionManager.Add(OfficeMeManager);
                TransactionManager.Add(RequestInActivesManager);
                TransactionManager.Add(DocMemberFileManager);
            }
        }
        #endregion

        #region WF Methods
        public int CheckPermissionEngOffConfirmingSendBackTask(int EOfId, int CurrentTaskCode)
        {
            int Per = 0;
            
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            DocMemberFileManager DocMemberFileManager = new DocMemberFileManager();
            this.FindByCode(EOfId);
            if (this.Count > 0)
            {
                int EngOfId = Convert.ToInt32(this[0]["EngOfId"]);
                if (Convert.ToInt32(this[0]["Type"]) != (int)TSP.DataManager.EngOffFileType.Invalid
                && Convert.ToInt32(this[0]["Type"]) != (int)TSP.DataManager.EngOffFileType.ConditionalAprrove && CurrentTaskCode!= (int)WorkFlowTask.settlementAgentConfiringDocumentEngOffice)
                {
                    DataTable dtOfMe = OfficeMemberManager.selectEngOfficeMemberForWFCheck(EOfId, Convert.ToInt32(this[0]["EngOfId"]), 0);
                    if (dtOfMe.Rows.Count == 0)
                    {
                        Per = (int)TSP.DataManager.ErrorRequest.EngOffManagerNotSave;
                    }
                    else
                    {
                        bool DefaultValue = false;
                        for (int i = 0; i < dtOfMe.Rows.Count; i++)
                        {
                            if (dtOfMe.Rows[i]["OfpId"].ToString() == "4")
                                DefaultValue = true;
                            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(Convert.ToInt32(dtOfMe.Rows[i]["PersonId"]), 0);
                            if (dtMeFile.Rows.Count > 0)
                            {
                                if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && Convert.ToInt32(dtMeFile.Rows[0]["IsConfirm"]) == 1)
                                {
                                    string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
                                    if (!string.IsNullOrEmpty(ExpireDate))
                                    {
                                        if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                                            return (int)TSP.DataManager.ErrorRequest.OfficeMemberJobCertificateHasExpired;
                                    }
                                }
                            }
                        }
                        if (DefaultValue == false)
                        {
                            Per = (int)TSP.DataManager.ErrorRequest.EngOffManagerNotSave;

                        }
                    }
                }
            }
            else
            {
                Per = (int)TSP.DataManager.ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Confirming EngOfficeDocRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfEngOfficeDocConfirming(int EOfId, int CurrentUserAgentId, int CurrentUserId, ref ArrayList ArrayReturnValue)
        {
            int Per = 0;
            this.FindByCode(EOfId);
            if (this.Count != 1)
                return (int)ErrorRequest.LoseRequestInfo;
            int EngOfId = Convert.ToInt32(this[0]["EngOfId"]);
            if (Convert.ToInt32(this[0]["Type"]) != (int)TSP.DataManager.EngOffFileType.Invalid
            && Convert.ToInt32(this[0]["Type"]) != (int)TSP.DataManager.EngOffFileType.ConditionalAprrove)
            {
                if ((Utility.IsDBNullOrNullValue(this[0]["serialNo"])) || (Utility.IsDBNullOrNullValue(this[0]["ExpireDate"])))
                {
                    // "شماره سریال و تاریخ اعتبار پروانه مشخص نشده است.امکان تغییر وضعیت وجود ندارد";
                    if (Convert.ToInt32(this[0]["Type"]) != (int)TSP.DataManager.EngOffFileType.ChangeBaseInfo)
                        return (int)ErrorRequest.EngOffSerialNoAndExpDateNotFilled;
                }

                //***چک کردن عضویت در شرکت یا دفتر دیگر
                DataTable dtEngOffMember = OfficeMeManager.selectEngOfficeMemberForWFCheck(EOfId, Convert.ToInt32(this[0]["EngOfId"]), 0);
                for (int i = 0; i < dtEngOffMember.Rows.Count; i++)
                {
                    ArrayList ResultMembershipanother = TSP.DataManager.OfficeMemberManager.CheckMemberMembershipInOfficeAndEngOffice(Convert.ToInt32(dtEngOffMember.Rows[i]["PersonId"]), EngOfId, TSP.DataManager.OfficeMemberKind.EngOffice);
                    if (!Convert.ToBoolean(ResultMembershipanother[0]))
                    {
                        ArrayReturnValue.Add(ResultMembershipanother[2].ToString());
                        return (int)ErrorRequest.OneOfMemberIsInAnotherOffice;
                    }
                }


              //  DataTable dtOfMe = OfficeMemberManager.selectEngOfficeMemberForWFCheck(EOfId, Convert.ToInt32(this[0]["EngOfId"]), 0);
                if (dtEngOffMember.Rows.Count == 0)
                {
                    Per = (int)TSP.DataManager.ErrorRequest.EngOffManagerNotSave;
                }
                else
                {
                    bool DefaultValue = false;
                    for (int i = 0; i < dtEngOffMember.Rows.Count; i++)
                    {
                        if (dtEngOffMember.Rows[i]["OfpId"].ToString() == "4")
                            DefaultValue = true;
                        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(Convert.ToInt32(dtEngOffMember.Rows[i]["PersonId"]), 0);
                        if (dtMeFile.Rows.Count > 0)
                        {
                            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && Convert.ToInt32(dtMeFile.Rows[0]["IsConfirm"]) == 1)
                            {
                                string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
                                if (!string.IsNullOrEmpty(ExpireDate))
                                {
                                    if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                                        return (int)TSP.DataManager.ErrorRequest.OfficeMemberJobCertificateHasExpired;
                                }
                            }
                        }
                    }
                    if (DefaultValue == false)
                    {
                        Per = (int)TSP.DataManager.ErrorRequest.EngOffManagerNotSave;

                    }
                }
            }

            DataTable dtEngOfficeMember = OfficeMeManager.selectEngOfficeMemberForWFCheck(EOfId, Convert.ToInt32(this[0]["EngOfId"]), 0);//.selectEngOfficeMemberByEOfId(EOfId);
            for (int i = 0; i < dtEngOfficeMember.Rows.Count; i++)
            {
                OfficeMeManager.selectEngOfficeMember(Convert.ToInt32(dtEngOfficeMember.Rows[i]["OfmId"]));
                if (OfficeMeManager.Count != 1)
                    return (int)ErrorWFNextStep.Error;
                OfficeMeManager[0].BeginEdit();
                OfficeMeManager[0]["IsConfirm"] = 1;
                OfficeMeManager[0]["ConfirmDate"] = Utility.GetDateOfToday();
                OfficeMeManager[0].EndEdit();
                if (OfficeMeManager.Save() <= 0)
                    return (int)ErrorWFNextStep.Error;
                OfficeMeManager.DataTable.AcceptChanges();
            }

            #region InsertConfirm
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 1;//تایید
            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = Utility.GetDateOfToday();
            this[0].EndEdit();

            if (this.Save() <= 0)
                return (int)ErrorRequest.Error;

            EngOfficeManager.FindByCode(EngOfId);
            if (EngOfficeManager.Count != 1)
                return (int)ErrorRequest.Error;

            #region SetOfficeFromRequest
            //--------find engoffice manager-----
            int ManagerId = -1;
            DataTable dt = EngOfficeManager.SelectEngOfficeManagerByOfId(EngOfId);
            if (dt.Rows.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(dt.Rows[0]["OfmId"]))
                    ManagerId = Convert.ToInt32(dt.Rows[0]["OfmId"]);
            }
            EngOfficeManager[0].BeginEdit();
            EngOfficeManager[0]["EOfTId"] = this[0]["EOfTId"];
            EngOfficeManager[0]["EngOffName"] = this[0]["EngOffName"];
            EngOfficeManager[0]["ParticipateLetterNo"] = this[0]["ParticipateLetterNo"];
            EngOfficeManager[0]["ParticipateLetterDate"] = this[0]["ParticipateLetterDate"];
            EngOfficeManager[0]["EngOffNo"] = this[0]["EngOffNo"];
            EngOfficeManager[0]["EngOffLoc"] = this[0]["EngOffLoc"];
            EngOfficeManager[0]["FileNo"] = this[0]["FileNo"];
            EngOfficeManager[0]["Description"] = this[0]["Description"];
            EngOfficeManager[0]["Address"] = this[0]["Address"];
            EngOfficeManager[0]["TellNo"] = this[0]["TellNo"];
            EngOfficeManager[0]["MobileNo"] = this[0]["MobileNo"];
            EngOfficeManager[0]["FaxNo"] = this[0]["FaxNo"];
            EngOfficeManager[0]["Email"] = this[0]["Email"];
            EngOfficeManager[0]["CreateDate"] = this[0]["CreateDate"];
            if (ManagerId != -1)
                EngOfficeManager[0]["ManagerOfmId"] = ManagerId;
            #endregion

            if (this[0]["Type"].ToString() == "0")//درخواست صدور اولیه
            {
                EngOfficeManager[0]["IsConfirm"] = 1;//تایید شده                 
            }
            else
                if (this[0]["Type"].ToString() == "4")//ابطال
            {
                EngOfficeManager[0]["IsConfirm"] = 3;
                EngOfficeManager[0]["InActive"] = 1;
            }

            switch (Convert.ToInt32(this[0]["Type"]))
            {
                case (int)EngOffFileType.SaveFileDocument:
                case (int)EngOffFileType.Change:
                case (int)EngOffFileType.ChangeBaseInfo:
                case (int)EngOffFileType.Revival:
                case (int)EngOffFileType.Reduplicate:
                    EngOfficeManager[0]["IsConfirm"] = (int)EngOfficeConfirmationType.Confirmed;//تایید شده
                    break;
                case (int)EngOffFileType.Invalid://ابطال                    
                    EngOfficeManager[0]["IsConfirm"] = (int)EngOfficeConfirmationType.Cancel;
                    EngOfficeManager[0]["InActive"] = 1;
                    break;
                case (int)EngOffFileType.Activate://احیاء
                    EngOfficeManager[0]["IsConfirm"] = (int)EngOfficeConfirmationType.Confirmed;
                    EngOfficeManager[0]["InActive"] = 0;
                    break;
                case (int)EngOffFileType.ConditionalAprrove:
                    EngOfficeManager[0]["IsConfirm"] = (int)EngOfficeConfirmationType.ConditionalApprove;
                    break;
            }

            EngOfficeManager[0]["UserId"] = CurrentUserId;
            EngOfficeManager[0]["ModifiedDate"] = DateTime.Now;
            EngOfficeManager[0].EndEdit();
            if (EngOfficeManager.Save() <= 0)
                return (int)ErrorRequest.Error;

            #endregion

            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting EngOfficeDocRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfEngOfficeDocRejecting(int EOfId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(EOfId);
            if (this.Count == 1)
            {
                int EngOfId = (int)this[0]["EngOfId"];
                this[0].BeginEdit();
                this[0]["IsConfirm"] = 2;//تایید نشده;
                this[0]["UserId"] = CurrentUserId;
                this[0]["AnswerDate"] = GetDateOfToday();

                this[0].EndEdit();
                if (this.Save() > 0)
                {
                    if (this[0]["Type"].ToString() == "0")
                    {
                        EngOfficeManager.FindByCode(EngOfId);
                        EngOfficeManager[0].BeginEdit();
                        EngOfficeManager[0]["IsConfirm"] = 2;//تایید نشده
                        EngOfficeManager[0]["UserId"] = CurrentUserId;
                        EngOfficeManager[0].EndEdit();
                        if (EngOfficeManager.Save() <= 0)
                        {
                            Per = (int)ErrorWFNextStep.Error;
                        }
                    }
                    RequestInActivesManager.UpdateInActiveRowByRequest(EOfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), 1);
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
            return BaseObject.GetUserPermission(UserId, ut, TableType.EngOffFile);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblEngOffFile";
            tableMapping.ColumnMappings.Add("EOfId", "EOfId");
            tableMapping.ColumnMappings.Add("EngOfId", "EngOfId");
            tableMapping.ColumnMappings.Add("EOfTId", "EOfTId");
            tableMapping.ColumnMappings.Add("ParticipateLetterNo", "ParticipateLetterNo");
            tableMapping.ColumnMappings.Add("ParticipateLetterDate", "ParticipateLetterDate");
            tableMapping.ColumnMappings.Add("EngOffName", "EngOffName");
            tableMapping.ColumnMappings.Add("EngOffNo", "EngOffNo");
            tableMapping.ColumnMappings.Add("EngOffLoc", "EngOffLoc");
            tableMapping.ColumnMappings.Add("FileNo", "FileNo");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("TellNo", "TellNo");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("FaxNo", "FaxNo");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("MFSerialNo", "MFSerialNo");
            tableMapping.ColumnMappings.Add("SerialNo", "SerialNo");
            tableMapping.ColumnMappings.Add("RegDate", "RegDate");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("PrId", "PrId");
            tableMapping.ColumnMappings.Add("RegPlaceId", "RegPlaceId");
            tableMapping.ColumnMappings.Add("FileLetterNo", "FileLetterNo");
            tableMapping.ColumnMappings.Add("FileLetterDate", "FileLetterDate");
            tableMapping.ColumnMappings.Add("Requester", "Requester");
            tableMapping.ColumnMappings.Add("FollowCode", "FollowCode");
            tableMapping.ColumnMappings.Add("IsTemp", "IsTemp");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("AnswerDate", "AnswerDate");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("RequestDesc", "RequestDesc");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ImageOwnership", "ImageOwnership");
            tableMapping.ColumnMappings.Add("Imagepartnership", "Imagepartnership");
            tableMapping.ColumnMappings.Add("ImagePartnerDisclaimer", "ImagePartnerDisclaimer");
            tableMapping.ColumnMappings.Add("ImageInqueryMembers", "ImageInqueryMembers");

            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectEngOffFile";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@EngOfId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@EOfId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int);


            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteEngOffFile";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_EOfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EOfId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertEngOffFile";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EngOfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EngOfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EOfTId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EOfTId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParticipateLetterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParticipateLetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParticipateLetterDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParticipateLetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EngOffName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EngOffName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EngOffNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EngOffNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EngOffLoc", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EngOffLoc", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TellNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TellNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MobileNo", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MobileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FaxNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FaxNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Email", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Email", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //  this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MFSerialNo", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MFSerialNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SerialNo", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SerialNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegPlaceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegPlaceId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileLetterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileLetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileLetterDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileLetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Requester", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Requester", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FollowCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FollowCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsTemp", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsTemp", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestDesc", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RequestDesc", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImageOwnership", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImageOwnership", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Imagepartnership", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Imagepartnership", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImagePartnerDisclaimer", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImagePartnerDisclaimer", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImageInqueryMembers", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImageInqueryMembers", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateEngOffFile";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EngOfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EngOfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EOfTId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EOfTId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParticipateLetterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParticipateLetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParticipateLetterDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParticipateLetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EngOffName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EngOffName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EngOffNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EngOffNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //  this.Adapter.UpdateCommand.Parameters.Add("@EngOffNo",SqlDbType.VarChar);
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EngOffLoc", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EngOffLoc", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileNo", global::System.Data.SqlDbType.VarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TellNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TellNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MobileNo", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MobileNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FaxNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FaxNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Email", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Email", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreateDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MFSerialNo", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MFSerialNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SerialNo", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SerialNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ExpireDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RegPlaceId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RegPlaceId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileLetterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileLetterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileLetterDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileLetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Requester", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Requester", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FollowCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FollowCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsTemp", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsTemp", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirm", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AnswerDate", global::System.Data.SqlDbType.Char, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_EOfId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EOfId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EOfId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "EOfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestDesc", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RequestDesc", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImageOwnership", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImageOwnership", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Imagepartnership", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Imagepartnership", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImagePartnerDisclaimer", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImagePartnerDisclaimer", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ImageInqueryMembers", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ImageInqueryMembers", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.OfficeDataSet.tblEngOffFileDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByCode(int EOfId)
        {
            ResetAllParameters();
            if (this.Adapter.SelectCommand.Transaction == null)
                this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@EOfId"].Value = EOfId;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
            Fill();
        }

        public void FindByEngOffCode(int EngOfId, Int16 IsConfirm, Int16 Type)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EngOfId"].Value = EngOfId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByEngOfficeId(int EngOfId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@EngOfId"].Value = EngOfId;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectForManagmentPage(int EngOfId)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOffFileForManagmentPage", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@EngOfId", EngOfId);
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)TSP.DataManager.WorkFlows.EngOfficeConfirming);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable FindEngOffFileByDate(int EngOfId, string FromDate, string EndDate)
        {
            DataTable.Clear();
            // ArrayList ArrDocMember = new ArrayList();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectEngOffFileByDate", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@EngOfId", EngOfId);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", EndDate);
            adapter.Fill(DataTable);
            return DataTable;
        }

        /// <summary>
        /// کلیه پروانه هایی که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// </summary>
        /// <param name="EngOfId"></param>
        /// <param name="FromDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>((DataRow)ArrayList[i])["EOfId"] , ((DataRow)ArrayList[i])["FileNo"]: شماره پروانه,((DataRow)ArrayList[i])["AnswerDate"]:تاریخ تایید , ((DataRow)ArrayList[i])["ExpireDate"] :تاریخ پایان اعتبار پروانه</returns>
        public ArrayList FindActiveEngOffFileByDate(int EngOfId, string FromDate, string EndDate)
        {
            DocMemberFileManager DocMemberFileManager = new DocMemberFileManager();
            ArrayList ArrEngOffFile = new ArrayList();
            DataTable dtFileIn = new DataTable();
            DataTable dtFileOut = new DataTable();
            dtFileIn = FindEngOffFileByDate(EngOfId, FromDate, EndDate).Copy();
            if (dtFileIn.Rows.Count > 0)
            {
                string FirstDocDate = dtFileIn.Rows[0]["AnswerDate"].ToString();
                FirstDocDate = DocMemberFileManager.AddDays(FirstDocDate, -1);
                if (string.Compare(FromDate, FirstDocDate) < 0)
                // if (FirstDocDate > FromDate)
                {
                    dtFileOut = FindEngOffFileByDate(EngOfId, "0000/00/00", FirstDocDate);
                    if (dtFileOut.Rows.Count > 0)
                    {
                        ArrEngOffFile.Add(dtFileOut.Rows[dtFileOut.Rows.Count - 1]);
                    }
                }
            }
            else
            {
                dtFileOut = FindEngOffFileByDate(EngOfId, "0000/00/00", FromDate);
                if (dtFileOut.Rows.Count > 0)
                {
                    ArrEngOffFile.Add(dtFileOut.Rows[dtFileOut.Rows.Count - 1]);
                }
            }
            for (int i = 0; i < dtFileIn.Rows.Count; i++)
            {
                ArrEngOffFile.Add(dtFileIn.Rows[i]);
            }

            return ArrEngOffFile;
        }


    }
}

