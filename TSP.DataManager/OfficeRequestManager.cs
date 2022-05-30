using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;

namespace TSP.DataManager
{
    public class OfficeRequestManager : BaseObject
    {

        #region Utility Methods

        private string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }

        private static Boolean IsDBNullOrNullValue(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return true;
            if (string.IsNullOrEmpty(obj.ToString()))
                return true;
            return false;
        }
        #endregion

        #region Private Managers
        TSP.DataManager.OfficeManager OfficeManager;
        TSP.DataManager.OfficeMemberManager OfficeMeManager;
        TSP.DataManager.RequestInActivesManager RequestInActivesManager;
        #endregion

        #region Constructors
        public OfficeRequestManager()
        {
        }

        public OfficeRequestManager(TransactionManager TransactionManager, int CurrentUser_AgentId)
        {
            OfficeManager = new OfficeManager();
            OfficeMeManager = new TSP.DataManager.OfficeMemberManager();
            RequestInActivesManager = new RequestInActivesManager();

            if (TransactionManager != null)
            {
                TransactionManager.Add(OfficeManager);
                TransactionManager.Add(OfficeMeManager);
                TransactionManager.Add(RequestInActivesManager);
            }
        }

        #endregion

        #region WF Methods

        #region WFOfficeMembershipConfirming
        /// <summary>
        /// چک کردن شرایط قبل از باز شدن پنجره گردش کار در عضویت
        /// </summary>
        /// <param name="OfReId"></param>
        /// <param name="CurrentTaskCode"></param>
        /// <returns></returns>
        public int CheckPermissionOfficeConfirmingSendBackTask(int OfReId, int CurrentTaskCode)
        {
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            TSP.DataManager.OtherPersonManager OthPersonManager = new TSP.DataManager.OtherPersonManager();
            TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();

            int Per = 0;

            this.FindByCode(OfReId);
            if (this.Count <= 0)
            {
                return (int)TSP.DataManager.ErrorRequest.LoseRequestInfo;
            }
            if (Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo
                || Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.Invalid
                || Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.DocumentInvalid
                 || Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo
                || Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                return Per;
            if (!Utility.IsDBNullOrNullValue(this[0]["MFType"]))
            {
                Per = CheckOfficeMemberConditions(Convert.ToInt32(this[0]["OfId"])
                                                , Convert.ToInt32(this[0]["MFType"])
                                                , Convert.ToInt32(this[0]["OtId"])
                                                , OfMeManager, DocMemberFileManager, OthPersonManager, GradeManager);
            }

            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Confirming OfficeRequest
        /// تایید درخواست شرکت در عضویت
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfOfficeConfirming(int OfReId, int CurrentUserAgentId, int CurrentUserId, ref  ArrayList ArrayReturnValue)
        {
            int Per = 0;
            int ManagerId = -1;
            this.FindByCode(OfReId);

            #region InsertConfirm
            int OfId = int.Parse(this[0]["OfId"].ToString());
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 1;//تایید
            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = GetDateOfToday();
            this[0].EndEdit();

            if (this.Save() <= 0)
            {
                return (int)ErrorWFNextStep.Error;
            }
            //--------find office manager-----
            DataTable dt = OfficeManager.SelectOfficeManagerByOfId(OfId, 0, OfReId);
            if (dt.Rows.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(dt.Rows[0]["OfmId"]))
                    ManagerId = Convert.ToInt32(dt.Rows[0]["OfmId"]);
            }

            OfficeManager.FindByCode(OfId);
            #region SetOfficeFromRequest
            OfficeManager[0].BeginEdit();
            OfficeManager[0]["OfName"] = this[0]["OfName"];
            OfficeManager[0]["OfNameEn"] = this[0]["OfNameEn"];
            OfficeManager[0]["Tel1"] = this[0]["Tel1"];
            OfficeManager[0]["Tel2"] = this[0]["Tel2"];
            OfficeManager[0]["Fax"] = this[0]["Fax"];
            OfficeManager[0]["Address"] = this[0]["Address"];
            OfficeManager[0]["MobileNo"] = this[0]["MobileNo"];
            OfficeManager[0]["Website"] = this[0]["Website"];
            OfficeManager[0]["Email"] = this[0]["Email"];
            OfficeManager[0]["SignUrl"] = this[0]["SignUrl"];
            OfficeManager[0]["ArmUrl"] = this[0]["ArmUrl"];
            OfficeManager[0]["FileNo"] = this[0]["MFNo"];
            OfficeManager[0]["FileDate"] = this[0]["ExpireDate"];
            OfficeManager[0]["MFType"] = this[0]["MFType"];
            OfficeManager[0]["Subject"] = this[0]["Subject"];
            OfficeManager[0]["ActivityType"] = this[0]["ActivityType"];
            OfficeManager[0]["OtId"] = this[0]["OtId"];
            OfficeManager[0]["Subject"] = this[0]["Subject"];
            OfficeManager[0]["RegOfNo"] = this[0]["RegOfNo"];
            OfficeManager[0]["VolumeInvest"] = this[0]["VolumeInvest"];
            OfficeManager[0]["Stock"] = this[0]["Stock"];
            OfficeManager[0]["MFType"] = this[0]["MFType"];
            OfficeManager[0]["GrdId"] = this[0]["GrdId"];
            OfficeManager[0]["Description"] = this[0]["OfficeDescription"];
            if (this[0]["MembershipRequstType"] != null)
                OfficeManager[0]["MembershipRequstType"] = this[0]["MembershipRequstType"];


            if (ManagerId != -1)
                OfficeManager[0]["ManagerOfmId"] = ManagerId;
            #endregion
            switch (Convert.ToInt32(this[0]["Type"]))
            {
                case (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo://درخواست ثبت نام اولیه
                    if (Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval;
                    else
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
                    break;
                case (int)TSP.DataManager.OfficeRequestType.Invalid:
                    OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Cancel;//باطل شدن عضویت حقوقی                                
                    break;
                case (int)TSP.DataManager.OfficeRequestType.DocumentInvalid:
                    //  OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.DocumentCancel;//باطل شدن پروانه                                
                    OfficeManager[0]["DocumentStatus"] = (int)TSP.DataManager.OfficeDocumentStatus.DocumentCancel;//باطل شدن پروانه
                    if (Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval;
                    else
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
                    break;
                default:
                    if (Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval;
                    else
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
                    break;
            }

            OfficeManager[0]["UserId"] = CurrentUserId;
            OfficeManager[0].EndEdit();
            if (OfficeManager.Save() <= 0)
            {
                return (int)ErrorWFNextStep.Error;
            }

            if (Utility.IsOfficeMemberConfirmRequestNeeded())
            {
                DataTable dtOfficeMember = OfficeMeManager.FindOffMemberByOfReId(OfReId);
                for (int i = 0; i < dtOfficeMember.Rows.Count; i++)
                {
                    if ((Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[i]["IsConfirm"])) || (Utility.IsDBNullOrNullValue(dtOfficeMember.Rows[i]["ConfirmDate"])))
                    {
                        //"به دلیل عدم پاسخ تمامی اعضای دفتر،امکان تغییر وضعیت وجود ندارد";
                        return (int)ErrorRequest.AllEngOfficeMembersNotAcceptCompany;
                    }
                }
            }
            else
            {
                DataTable dtOfficeMember = OfficeMeManager.FindOffMemberByOfReId(OfReId);
                for (int i = 0; i < dtOfficeMember.Rows.Count; i++)
                {
                    OfficeMeManager[i].BeginEdit();
                    OfficeMeManager[i]["IsConfirm"] = 1;
                    OfficeMeManager[i]["ConfirmDate"] = Utility.GetDateOfToday();
                    OfficeMeManager[i].EndEdit();
                    if (OfficeMeManager.Save() <= 0)
                        return (int)ErrorWFNextStep.Error;
                    OfficeMeManager.DataTable.AcceptChanges();
                }
            }

            #endregion
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting OfficeRequest
        /// عدم تایید درخواست شرکت در عضویت
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfOfficeRejecting(int OfReId, int CurrentUserId)
        {
            int Per = 0;

            this.FindByCode(OfReId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }

            int OfId = (int)this[0]["OfId"];
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 2;//تایید نشده;
            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = GetDateOfToday();

            this[0].EndEdit();
            if (this.Save() <= 0)
                return (int)ErrorWFNextStep.Error;

            if (this[0]["Type"].ToString() == "0")
            {
                OfficeManager.FindByCode(OfId);
                OfficeManager[0].BeginEdit();
                OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.NotConfirmed;//تایید نشده
                OfficeManager[0]["UserId"] = CurrentUserId;
                OfficeManager[0].EndEdit();
                if (OfficeManager.Save() <= 0)
                {
                    return (int)ErrorWFNextStep.Error;
                }
            }

            RequestInActivesManager.UpdateInActiveRowByRequest(OfReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest), 1);
            return Per;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="OfficeMFType"></param>
        /// <param name="OfficeTypeId">سهامی عام /سهامی خاص</param>
        /// <param name="OfMeManager"></param>
        /// <param name="DocMemberFileManager"></param>
        /// <returns></returns>
        public int CheckOfficeMemberConditions(int OfId, int OfficeMFType, int OfficeTypeId
            , TSP.DataManager.OfficeMemberManager OfMeManager
            , TSP.DataManager.DocMemberFileManager DocMemberFileManager
            , TSP.DataManager.OtherPersonManager OthPersonManager
            , TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager)
        {
            // return 0;
            int per = 0;
            bool HasManager = false;
            //int minME = 0;
            //bool BoardHasMemberFile = false;
            int OffMeCountWithDoc = 0;
            //  int CountOffManager = 0;

            OfMeManager.FindOfficeActiveMember(OfId);
            if (OfMeManager.Count == 0)
                return (int)TSP.DataManager.ErrorRequest.NoMemberDefinedForOffice;
            if (OfficeTypeId == (int)OfficeType.LimitedJointStock && OfMeManager.Count < 2)//مسئولیت محدود
                return (int)TSP.DataManager.ErrorRequest.MemberNumberOfLimitedJointStockShouldBeGreaterThanTwo;
            if (OfficeTypeId == (int)OfficeType.PrivateJointStock && OfMeManager.Count < 3)//سهامی خاص
                return (int)TSP.DataManager.ErrorRequest.MemberNumberOfPrivateAndPublicJointStockShouldBeGreaterThanThree;

            for (int i = 0; i < OfMeManager.Count; i++)
            {
                int OfpId = Convert.ToInt32(OfMeManager[i]["OfpId"]);
                int MeId = int.Parse(OfMeManager[i]["PersonId"].ToString());
                int OfmType = Convert.ToInt32(OfMeManager[i]["OfmType"]);
                if (OfpId == (int)OfficePosition.Manager || OfpId == (int)OfficePosition.ManagerAndBoard)//مدیر عامل
                {
                    HasManager = true;
                    if (OfmType != (int)TSP.DataManager.OfficeMemberType.Member
                        && OfficeMFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
                        return (int)TSP.DataManager.ErrorRequest.OtherPersonDoesntAllowToBeManager;
                }
                if (OfmType == (int)TSP.DataManager.OfficeMemberType.Member && OfpId != (int)OfficePosition.ShareHolder)
                {

                    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
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
                            OffMeCountWithDoc++;
                        }
                    }
                }
                else if (OfmType == (int)TSP.DataManager.OfficeMemberType.Kardan)
                {
                    OthPersonManager.FindByCode(MeId);
                    if (OthPersonManager.Count == 1)
                    {
                        if (!Utility.IsDBNullOrNullValue(OthPersonManager[0]["FileNo"]))
                            OffMeCountWithDoc++;
                    }
                }

            }

            if (HasManager == false)
                return (int)TSP.DataManager.ErrorRequest.NoOfficeManager;
            if (OffMeCountWithDoc < 2)
            {
                if (OfficeMFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)
                    return (int)TSP.DataManager.ErrorRequest.AtLeastTwoMemberOfObserverDesignOfficeShouldHaveMemberFile;

                if (OfficeMFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
                    return (int)TSP.DataManager.ErrorRequest.AtLeastTwoMemberOfImpOfficeShouldHaveMemberFile;
            }   
            return per;
        }


        #region WFOfficeDocumentConfirming

        /// <summary>
        /// چک کردن شرایط قبل از باز شدن پنجره گردش کار در پروانه
        /// </summary>
        /// <param name="OfReId"></param>
        /// <param name="CurrentTaskCode"></param>
        /// <returns></returns>
        public int CheckPermissionOfficeDocConfirmingSendBackTask(int OfReId, int CurrentTaskCode)
        {
            int Per = 0;
            if (CurrentTaskCode == (int)WorkFlowTask.settlementAgentConfiringDocumentOff || CurrentTaskCode == (int)WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff)
                return Per;
            this.FindByCode(OfReId);
            if (this.Count <= 0)
            {
                return (int)TSP.DataManager.ErrorRequest.LoseRequestInfo;
            }

            if (Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo
                || Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.Invalid
                || Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.DocumentInvalid
                || Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                return Per;


            #region CheckMembers


            if (Utility.IsDBNullOrNullValue(this[0]["MFType"]))
                return (int)TSP.DataManager.ErrorRequest.MFTypeIsNotRecognized;
            //int MFType = Convert.ToInt32(this[0]["MFType"]);
            //int OfId = Convert.ToInt32(this[0]["OfId"]);
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            TSP.DataManager.OtherPersonManager OthPersonManager = new TSP.DataManager.OtherPersonManager();
            TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();

            Per = CheckOfficeMemberConditions(Convert.ToInt32(this[0]["OfId"])
                                  , Convert.ToInt32(this[0]["MFType"])
                                  , Convert.ToInt32(this[0]["OtId"])
                                  , OfMeManager, DocMemberFileManager, OthPersonManager, GradeManager);
            #endregion

            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Confirming OfficeRequest
        /// تایید درخواست شرکت در پروانه
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfDocConfirming(int OfReId, int CurrentUserAgentId, int CurrentUserId, ref  ArrayList ArrayReturnValue)
        {
            int Per = 0;
            int ManagerId = -1;
            this.FindByCode(OfReId);
            if (this.Count != 1)
                return (int)ErrorWFNextStep.Error;
            int OfId = Convert.ToInt32(this[0]["OfId"]);
            if (Convert.ToInt32(this[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.Invalid
                && Convert.ToInt32(this[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.DocumentInvalid
                )
            {
                if ((Utility.IsDBNullOrNullValue(this[0]["serialNo"])) || (Utility.IsDBNullOrNullValue(this[0]["ExpireDate"])))
                {
                    //"شماره سریال و تاریخ اعتبار پروانه مشخص نشده است.امکان تغییر وضعیت وجود ندارد";
                    if (Convert.ToInt32(this[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo)
                        return (int)ErrorRequest.SerialNoAndExpDateNotFilled;
                }

                //***چک کردن عضویت در شرکت یا دفتر دیگر
                DataTable dtOffMember = OfficeMeManager.FindOffMemberByOfReId(OfReId, 0);
                for (int i = 0; i < dtOffMember.Rows.Count; i++)
                {
                    ArrayList ResultMembershipanother = TSP.DataManager.OfficeMemberManager.CheckMemberMembershipInOfficeAndEngOffice(Convert.ToInt32(dtOffMember.Rows[i]["PersonId"]), OfId, TSP.DataManager.OfficeMemberKind.Office);
                    if (!Convert.ToBoolean(ResultMembershipanother[0]))
                    {
                        ArrayReturnValue.Add(ResultMembershipanother[2].ToString());
                        return (int)ErrorRequest.OneOfMemberIsInAnotherOffice;
                    }
                }
            }
            #region Members Confirming
            if (Utility.IsOfficeMemberConfirmRequestNeeded())
            {
                DataTable dtOfficeMember = OfficeMeManager.FindOffMemberByOfReId(OfReId, 0);//return Member From OfficeMember
                for (int i = 0; i < OfficeMeManager.Count; i++)
                {
                    if ((Utility.IsDBNullOrNullValue(OfficeMeManager[i]["IsConfirm"])) || (Utility.IsDBNullOrNullValue(OfficeMeManager[i]["ConfirmDate"])))
                    {
                        //"به دلیل عدم پاسخ تمامی اعضای شرکت،امکان تغییر وضعیت وجود ندارد";
                        return (int)ErrorRequest.AllOfficeMembersNotAcceptCompany;
                    }
                }
            }
            else
            {
                DataTable dtOfficeMember = OfficeMeManager.FindOffMemberByOfReId(OfReId);//return Member From OfficeMember
                for (int i = 0; i < OfficeMeManager.Count; i++)
                {
                    OfficeMeManager[i].BeginEdit();
                    OfficeMeManager[i]["IsConfirm"] = 1;
                    OfficeMeManager[i]["ConfirmDate"] = Utility.GetDateOfToday();
                    OfficeMeManager[i].EndEdit();
                    if (OfficeMeManager.Save() <= 0)
                        return (int)ErrorWFNextStep.Error;
                    OfficeMeManager.DataTable.AcceptChanges();
                }
            }
            #endregion

            #region InsertConfirm
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 1;//تایید
            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = Utility.GetDateOfToday();
            this[0].EndEdit();

            if (this.Save() <= 0)
            {
                return (int)ErrorWFNextStep.Error;
            }

            //--------find office manager-----
            DataTable dt = OfficeManager.SelectOfficeManagerByOfId(OfId, 0, OfReId);
            if (dt.Rows.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(dt.Rows[0]["OfmId"]))
                    ManagerId = Convert.ToInt32(dt.Rows[0]["OfmId"]);
            }

            OfficeManager.FindByCode(OfId);
            #region SetOfficeFromRequest
            OfficeManager[0].BeginEdit();
            OfficeManager[0]["OfName"] = this[0]["OfName"];
            OfficeManager[0]["OfNameEn"] = this[0]["OfNameEn"];
            OfficeManager[0]["Tel1"] = this[0]["Tel1"];
            OfficeManager[0]["Tel2"] = this[0]["Tel2"];
            OfficeManager[0]["Fax"] = this[0]["Fax"];
            OfficeManager[0]["Address"] = this[0]["Address"];
            OfficeManager[0]["MobileNo"] = this[0]["MobileNo"];
            OfficeManager[0]["Website"] = this[0]["Website"];
            OfficeManager[0]["Email"] = this[0]["Email"];
            OfficeManager[0]["SignUrl"] = this[0]["SignUrl"];
            OfficeManager[0]["ArmUrl"] = this[0]["ArmUrl"];
            OfficeManager[0]["FileNo"] = this[0]["MFNo"];
            OfficeManager[0]["FileDate"] = this[0]["ExpireDate"];
            OfficeManager[0]["MFType"] = this[0]["MFType"];
            OfficeManager[0]["Subject"] = this[0]["Subject"];
            OfficeManager[0]["ActivityType"] = this[0]["ActivityType"];
            if (this[0]["MembershipRequstType"] != null)
                OfficeManager[0]["MembershipRequstType"] = this[0]["MembershipRequstType"];

            OfficeManager[0]["OtId"] = this[0]["OtId"];
            OfficeManager[0]["Subject"] = this[0]["Subject"];
            OfficeManager[0]["RegOfNo"] = this[0]["RegOfNo"];
            OfficeManager[0]["VolumeInvest"] = this[0]["VolumeInvest"];
            OfficeManager[0]["Stock"] = this[0]["Stock"];
            OfficeManager[0]["MFType"] = this[0]["MFType"];
            OfficeManager[0]["GrdId"] = this[0]["GrdId"];
            if (ManagerId != -1)
                OfficeManager[0]["ManagerOfmId"] = ManagerId;
            #endregion

            switch (Convert.ToInt32(this[0]["Type"]))
            {
                case (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo://درخواست ثبت نام اولیه                
                    if (Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval;
                    else
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
                    break;
                case (int)TSP.DataManager.OfficeRequestType.Invalid:
                    OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Cancel;//باطل شدن عضویت حقوقی                                                    
                    break;
                case (int)TSP.DataManager.OfficeRequestType.Change:
                    if (Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval;
                    else
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
                    break;
                case (int)TSP.DataManager.OfficeRequestType.DocumentInvalid:
                    if (Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval;
                    else
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
                    break;
                default:
                    if (Convert.ToInt32(this[0]["ConditionalApproval"]) == 1)
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval;
                    else
                        OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
                    break;
            }

            switch (Convert.ToInt32(this[0]["Type"]))
            {
                case (int)TSP.DataManager.OfficeRequestType.DocumentInvalid:
                    OfficeManager[0]["DocumentStatus"] = (int)TSP.DataManager.OfficeDocumentStatus.DocumentCancel;//باطل شدن پروانه
                    break;
                default:
                    OfficeManager[0]["DocumentStatus"] = (int)TSP.DataManager.OfficeDocumentStatus.Confirmed;
                    break;
            }
            OfficeManager[0]["UserId"] = CurrentUserId;
            OfficeManager[0].EndEdit();
            if (OfficeManager.Save() <= 0)
                return (int)ErrorWFNextStep.Error;
            #endregion
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting OfficeRequest
        /// عدم تایید درخواست شرکت در پروانه
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfDocRejecting(int OfReId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(OfReId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo; ;
            }
            int OfId = (int)this[0]["OfId"];
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 2;//تایید نشده;
            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = GetDateOfToday();

            this[0].EndEdit();
            if (this.Save() <= 0)
                return (int)ErrorWFNextStep.Error;        
            switch (Convert.ToInt32(this[0]["Type"]))
            {
                case (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo:
                    OfficeManager.FindByCode(OfId);
                    OfficeManager[0].BeginEdit();
                    OfficeManager[0]["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.NotConfirmed;//تایید نشده
                    OfficeManager[0]["UserId"] = CurrentUserId;
                    OfficeManager[0].EndEdit();
                    if (OfficeManager.Save() <= 0)
                        return (int)ErrorWFNextStep.Error;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.SaveFileDocument:
                      OfficeManager.FindByCode(OfId);
                    OfficeManager[0].BeginEdit();
                    OfficeManager[0]["DocumentStatus"] =  (int)TSP.DataManager.OfficeDocumentStatus.NotConfirmed;
                    OfficeManager[0]["UserId"] = CurrentUserId;
                    OfficeManager[0].EndEdit();
                    if (OfficeManager.Save() <= 0)
                        return (int)ErrorWFNextStep.Error;
                    break;
            }

            RequestInActivesManager.UpdateInActiveRowByRequest(OfReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest), 1);
            return Per;
        }
        #endregion

        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.OfficeRequest);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblOfficeRequest";
            tableMapping.ColumnMappings.Add("OfReId", "OfReId");
            tableMapping.ColumnMappings.Add("OfId", "OfId");
            tableMapping.ColumnMappings.Add("OfName", "OfName");
            tableMapping.ColumnMappings.Add("OfNameEn", "OfNameEn");
            tableMapping.ColumnMappings.Add("Tel1", "Tel1");
            tableMapping.ColumnMappings.Add("Tel2", "Tel2");
            tableMapping.ColumnMappings.Add("Fax", "Fax");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("Website", "Website");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("OtId", "OtId");
            tableMapping.ColumnMappings.Add("Subject", "Subject");
            tableMapping.ColumnMappings.Add("RegOfDate", "RegOfDate");
            tableMapping.ColumnMappings.Add("RegOfNo", "RegOfNo");
            tableMapping.ColumnMappings.Add("RegOfPlace", "RegOfPlace");
            tableMapping.ColumnMappings.Add("VolumeInvest", "VolumeInvest");
            tableMapping.ColumnMappings.Add("Stock", "Stock");
            tableMapping.ColumnMappings.Add("SignUrl", "SignUrl");
            tableMapping.ColumnMappings.Add("ArmUrl", "ArmUrl");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("RequestDesc", "RequestDesc");
            tableMapping.ColumnMappings.Add("AnswerDesc", "AnswerDesc");
            tableMapping.ColumnMappings.Add("AnswerDate", "AnswerDate");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("MFSerialNo", "MFSerialNo");
            tableMapping.ColumnMappings.Add("SerialNo", "SerialNo");
            tableMapping.ColumnMappings.Add("RegDate", "RegDate");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("RegPlaceId", "RegPlaceId");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("AssuranceDate", "AssuranceDate");
            tableMapping.ColumnMappings.Add("PrId", "PrId");
            tableMapping.ColumnMappings.Add("MFNo", "MFNo");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("Requester", "Requester");
            tableMapping.ColumnMappings.Add("MFType", "MFType");
            tableMapping.ColumnMappings.Add("IsTemp", "IsTemp");
            tableMapping.ColumnMappings.Add("FollowCode", "FollowCode");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ActivityType", "ActivityType");
            tableMapping.ColumnMappings.Add("GrdId", "GrdId");
            tableMapping.ColumnMappings.Add("OfficeDescription", "OfficeDescription");
            tableMapping.ColumnMappings.Add("ConditionalApproval", "ConditionalApproval");
            tableMapping.ColumnMappings.Add("MembershipRequstType", "MembershipRequstType");


            this.Adapter.TableMappings.Add(tableMapping);
            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectOfficeRequest";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@OfReId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@OfId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@Type", SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCodeOffConf", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)WorkFlows.OfficeConfirming;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCodeOffConf"].Value = (int)WorkFlows.OfficeMembershipConfirming;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@JustDocumentRequest", 0);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteOfficeRequest";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OfReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertOfficeRequest";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //this.Adapter.InsertCommand.Parameters.Add("@OfficeDescription", SqlDbType.NVarChar);
            //this.Adapter.InsertCommand.Parameters.Add("@ConditionalApproval", SqlDbType.Bit);
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfficeDescription", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfficeDescription", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConditionalApproval", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "ConditionalApproval", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfNameEn", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel1", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Fax", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Website", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Website", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OtId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Subject", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Subject", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegOfDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegOfDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegOfNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegOfNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegOfPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegOfPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VolumeInvest", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "VolumeInvest", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Stock", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Stock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArmUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ArmUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDesc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RequestDesc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AnswerDesc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AnswerDesc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AnswerDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFSerialNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFSerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegPlaceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RegPlaceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AssuranceDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "AssuranceDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MFNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Requester", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "Requester", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MFType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsTemp", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsTemp", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActivityType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ActivityType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MembershipRequstType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MembershipRequstType", System.Data.DataRowVersion.Current, false, null, "", "", ""));


            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateOfficeRequest";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //this.Adapter.UpdateCommand.Parameters.Add("@OfficeDescription", SqlDbType.NVarChar);
            //this.Adapter.UpdateCommand.Parameters.Add("@ConditionalApproval", SqlDbType.Bit);
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfficeDescription", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfficeDescription", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConditionalApproval", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "ConditionalApproval", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfNameEn", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "OfNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel1", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Tel2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Tel2", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Fax", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Website", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Website", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Address", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OtId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "OtId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Subject", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Subject", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegOfDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegOfDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegOfNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegOfNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegOfPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RegOfPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VolumeInvest", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, 0, 0, "VolumeInvest", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Stock", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "Stock", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArmUrl", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ArmUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDesc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RequestDesc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AnswerDesc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AnswerDesc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AnswerDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFSerialNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFSerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegPlaceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RegPlaceId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AssuranceDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "AssuranceDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MFNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Requester", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "Requester", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFType", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MFType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsTemp", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsTemp", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OfReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "OfReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OfReId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "OfReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActivityType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "ActivityType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MembershipRequstType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MembershipRequstType", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.OfficeDataSet.tblOfficeRequestDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        #region Functions
        public void FindByCode(int OfReId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@OfReId"].Value = OfReId;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = WorkFlows.OfficeConfirming;
            Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="IsConfirm"></param>
        /// <param name="Type"></param>
        /// <param name="JustDocumentRequest">0:False , 1:True</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByOfficeId(int OfId, Int16 IsConfirm, Int16 Type, int JustDocumentRequest)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@OfId"].Value = OfId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            this.Adapter.SelectCommand.Parameters["@Type"].Value = Type;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)WorkFlows.OfficeConfirming;
            this.Adapter.SelectCommand.Parameters["@WorkFlowCodeOffConf"].Value = (int)WorkFlows.OfficeMembershipConfirming;
            this.Adapter.SelectCommand.Parameters["@JustDocumentRequest"].Value = JustDocumentRequest;

            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByOfficeId(int OfId, Int16 IsConfirm, Int16 Type)
        {
            return FindByOfficeId(OfId, IsConfirm, Type, 0);
        }

        public void DeleteRequest(int OfReId, int OfId)
        {
            SqlCommand cmd = new SqlCommand("spDeleteOfficeRequestsForOfficeInfo", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@OfReId", OfReId);
                cmd.Parameters.AddWithValue("@OfId", OfId);
                cmd.Parameters.AddWithValue("@TableType", (int)TableCodes.OfficeRequest);
                cmd.ExecuteNonQuery();
                if (this.Transaction == null)
                    this.Connection.Close();
            }
            finally
            {
                cmd.Connection = null;
                cmd = null;

            }
        }

        public DataTable FindOfficeRequestByDate(int OfId, string FromDate, string EndDate)
        {
            DataTable.Clear();
            // ArrayList ArrDocMember = new ArrayList();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectOfficeRequestByDate", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@OfId", OfId);
            adapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", EndDate);
            adapter.Fill(DataTable);
            return DataTable;
        }

        /// <summary>
        /// کلیه پروانه هایی که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// </summary>
        /// <param name="OfId"></param>
        /// <param name="FromDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>((DataRow)ArrayList[i])["OfReId"] , ((DataRow)ArrayList[i])["MFNo"]: شماره پروانه,((DataRow)ArrayList[i])["AnswerDate"]:تاریخ تایید , ((DataRow)ArrayList[i])["ExpireDate"] :تاریخ پایان اعتبار پروانه</returns>
        public ArrayList FindActiveOfficeRequestByDate(int OfId, string FromDate, string EndDate)
        {
            DocMemberFileManager DocMemberFileManager = new DocMemberFileManager();
            ArrayList ArrOffRequest = new ArrayList();
            DataTable dtOffRequestIn = new DataTable();
            DataTable dtOffRequestOut = new DataTable();
            dtOffRequestIn = FindOfficeRequestByDate(OfId, FromDate, EndDate).Copy();
            if (dtOffRequestIn.Rows.Count > 0)
            {
                string FirstDocDate = dtOffRequestIn.Rows[0]["AnswerDate"].ToString();
                FirstDocDate = DocMemberFileManager.AddDays(FirstDocDate, -1);
                if (string.Compare(FromDate, FirstDocDate) < 0)
                // if (FirstDocDate > FromDate)
                {
                    dtOffRequestOut = FindOfficeRequestByDate(OfId, "0000/00/00", FirstDocDate);
                    if (dtOffRequestOut.Rows.Count > 0)
                    {
                        ArrOffRequest.Add(dtOffRequestOut.Rows[dtOffRequestOut.Rows.Count - 1]);
                    }
                }
            }
            else
            {
                dtOffRequestOut = FindOfficeRequestByDate(OfId, "0000/00/00", FromDate);
                if (dtOffRequestOut.Rows.Count > 0)
                {
                    ArrOffRequest.Add(dtOffRequestOut.Rows[dtOffRequestOut.Rows.Count - 1]);
                }
            }
            for (int i = 0; i < dtOffRequestIn.Rows.Count; i++)
            {
                ArrOffRequest.Add(dtOffRequestIn.Rows[i]);
            }

            return ArrOffRequest;
        }
        #endregion
    }
}
