using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager.Automation
{
    public enum AutomationErrors
    {
        SaveSuccesfully = 0, SelectOneRecord = -1, EndedLetterCanNotBeNumbered = -2, LetterCanNotReNumbered = -3,
        CanNotFindLetterSubscriberNmcId = -4, CanNotFindRefrenceForSign = -5, CanNotFindMainAssignerRefrence = -6,
        MainAsignerDidNotAsignLetter = -7, UnNounCreationType = -8,
        CanNotFindRecord = -9, Error = -10, SercreteriateCanNotBeNumberingWithOutRequest = -11, DraftLetterCanNotBeNumbered = -12
    }

    public class LettersManager : BaseObject
    {
        #region Public Parameters
        LetterReferenceActionsManager LetterReferenceActionsManager;
        TSP.DataManager.EmployeeManager EmployeeManager;
        TSP.DataManager.NezamManager NezamManager;
        #endregion

        #region Constructors
        public LettersManager()
        {

        }

        public LettersManager(TransactionManager Trans)
        {

            LetterReferenceActionsManager = new LetterReferenceActionsManager();
            EmployeeManager = new EmployeeManager();
            NezamManager = new NezamManager();
            if (Trans != null)
            {
                Trans.Add(NezamManager);
                Trans.Add(EmployeeManager);
                Trans.Add(LetterReferenceActionsManager);

            }
        }
        #endregion

        /// <summary>
        /// Return the suitable Message for specific ErrorCode
        /// </summary>
        /// <param name="ErrorCode"></param>
        /// <returns></returns>
        public string FindErrorMsg(int ErrorCode)
        {
            string ErrorMsg = "";
            switch (ErrorCode)
            {

                case (int)AutomationErrors.SaveSuccesfully:
                    ErrorMsg = "سند با موفقیت شماره گذاری شد.";
                    break;

                case (int)AutomationErrors.SelectOneRecord:
                    ErrorMsg = "رکوردی انتخاب نشده است";
                    break;

                case (int)AutomationErrors.Error:
                    ErrorMsg = "خطایی در ذخیره انجام گرفته است";
                    break;
                case (int)AutomationErrors.EndedLetterCanNotBeNumbered:
                    ErrorMsg = "امکان شماره دار کردن سند مختومه وجود ندارد.";
                    break;

                case (int)AutomationErrors.DraftLetterCanNotBeNumbered:
                    ErrorMsg = "امکان شماره دار کردن سند پیش نویس وجود ندارد.";
                    break;

                case (int)AutomationErrors.LetterCanNotReNumbered:
                    ErrorMsg = "امکان شماره دار کردن مجدد سند وجود ندارد.";
                    break;

                case (int)AutomationErrors.CanNotFindLetterSubscriberNmcId:
                    ErrorMsg = "امضا کننده اصلی سند نامشخص است.";
                    break;
                case (int)AutomationErrors.CanNotFindRefrenceForSign:
                    ErrorMsg = "برای سند انتخاب شده ارجاع جهت امضا وجود ندارد.";
                    break;
                case (int)AutomationErrors.CanNotFindMainAssignerRefrence:
                    ErrorMsg = "سند انتخاب شده به امضا کننده اصلی سند ارجاع داده نشده است.";
                    break;
                case (int)AutomationErrors.MainAsignerDidNotAsignLetter:
                    ErrorMsg = "امضا کننده اصلی سند  ،برروی سند اقدام انجام نداده است.";
                    break;
                case (int)AutomationErrors.UnNounCreationType:
                    ErrorMsg = "نوع سند نامشخص است.";
                    break;
                case (int)AutomationErrors.CanNotFindRecord:
                    ErrorMsg = "خطایی در بازیابی اطلاعات ایجاد شده است.";
                    break;
                case (int)AutomationErrors.SercreteriateCanNotBeNumberingWithOutRequest:
                    ErrorMsg = "امکان شماره گذاری این نوع سند بدون درخواست توسط کاربر امکان پذیر نمی باشد.";
                    break;
            }
            return ErrorMsg;
        }

        #region Numbring Methods
        /// <summary>
        /// چک کردن شرایط جهت ثبت درخواست شماره گذاری سند 
        /// </summary>
        /// <param name="LetterId"></param>
        /// <param name="LetterCreationType"></param>
        /// <param name="SubscriberNmcId"></param>
        /// <returns></returns>
        public int CheckPermissionForNumberingRequest(int LetterId, int LetterCreationType, int SubscriberNmcId, int LetterType, int LetterStatus)
        {
            LetterReferenceRecieversManager LetterReferenceRecieversManager = new LetterReferenceRecieversManager();
            LetterReferencesManager LetterReferencesManager = new LetterReferencesManager();
            LetterReferenceActionsManager LetterReferenceActionsManager = new LetterReferenceActionsManager();
            NezamMemberChartManager NezamMemberChartManager = new NezamMemberChartManager();
            ResignationManager ResignationManager = new ResignationManager();

            int Permission = (int)AutomationErrors.CanNotFindRecord;
            Boolean IsSigned = false;
            int ReceptiveId = -1;
            int RefId = -1;
            Boolean NeedSubscriber = false;
            //  int HowToSetDocNo = -1;

            if (LetterCreationType < 0)
            {//  ShowMessage("نوع سند نامشخص است.");
                return (int)AutomationErrors.UnNounCreationType;
            }

            //**************************Check Condition****************************
            int CrtTypePer = CheckCreationTypeCondition(LetterCreationType, (int)AutomationHowToSetDocNo.ByUser);
            if (CrtTypePer > 0)
            {
                NeedSubscriber = true;
            }
            else if (CrtTypePer < 0)
            {
                return CrtTypePer;
            }
            //********************************************************************

            if (LetterStatus == (int)AutomationLetterStauts.HasNumber)
            {// ShowMessage("امکان شماره دار کردن مجدد سند وجود ندارد.");
                return (int)AutomationErrors.LetterCanNotReNumbered;
            }

            if (LetterType == (int)TSP.DataManager.AutomationLetterTypes.EndedLetter)
            {// ShowMessage("امکان شماره دار کردن سند مختومه وجود ندارد.");
                return (int)AutomationErrors.EndedLetterCanNotBeNumbered;
            }

            if (LetterCreationType == (int)TSP.DataManager.AutomationLetterCreationType.In || !NeedSubscriber)
            {
                Permission = 0;
                return Permission;
            }


            //************************Check Subscriber Sign*****************************
            #region Check Subscriber
            if (SubscriberNmcId < 0)
            {// ShowMessage("امضا کننده اصلی سند نامشخص است.");
                return (int)AutomationErrors.CanNotFindLetterSubscriberNmcId;
            }
            LetterReferencesManager.FindByLetterId(LetterId);
            LetterReferencesManager.CurrentFilter = "Aim =" + ((int)TSP.DataManager.AutomationLetterReferenceAims.Signing).ToString();
            if (LetterReferencesManager.Count == 0)
            {//ShowMessage("برای سند انتخاب شده ارجاع جهت امضا وجود ندارد.");
                return (int)AutomationErrors.CanNotFindRefrenceForSign;
            }

            Boolean IsMainResign = false;
            for (int i = 0; i < LetterReferencesManager.Count; i++)
            {
                RefId = int.Parse(LetterReferencesManager[i]["RefId"].ToString());
                LetterReferenceRecieversManager.CurrentFilter = "";
                LetterReferenceRecieversManager.FindByReference(RefId);
                LetterReferenceRecieversManager.CurrentFilter = "Reciever =" + SubscriberNmcId.ToString();
                if (LetterReferenceRecieversManager.Count > 0)
                {
                    IsMainResign = true;
                    DataTable dtLetRefAct = LetterReferenceActionsManager.FindByReferenceRecievers(int.Parse(LetterReferenceRecieversManager[0]["RrId"].ToString()));
                    if (dtLetRefAct.Rows.Count == 1)
                    {
                        IsSigned = true;
                        break;
                    }
                }

                NezamMemberChartManager.FindByNmcId(SubscriberNmcId);
                if (NezamMemberChartManager.Count == 1)
                {
                    ResignationManager.SelectValidResignationForAutomation(Convert.ToInt32(NezamMemberChartManager[0]["EmpId"]), LetterCreationType);
                    if (ResignationManager.Count > 0)
                    {
                        NezamMemberChartManager.FindByEmpId(Convert.ToInt32(ResignationManager[0]["ReceptiveId"]));
                        for (int j = 0; j < NezamMemberChartManager.Count; j++)
                        {
                            ReceptiveId = Convert.ToInt32(NezamMemberChartManager[0]["NmcId"]);
                            if (ReceptiveId > 0)
                            {
                                LetterReferenceRecieversManager.CurrentFilter = "";

                                LetterReferenceRecieversManager.CurrentFilter = "Reciever =" + ReceptiveId.ToString();
                                if (LetterReferenceRecieversManager.Count > 0)
                                {
                                    IsMainResign = true;
                                    DataTable dtLetRefAct = LetterReferenceActionsManager.FindByReferenceRecievers(int.Parse(LetterReferenceRecieversManager[0]["RrId"].ToString()));
                                    if (dtLetRefAct.Rows.Count == 1)
                                    {
                                        IsSigned = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!IsMainResign)
            {//ShowMessage("سند انتخاب شده به امضا کننده اصلی سند ارجاع داده نشده است.");
                return (int)AutomationErrors.CanNotFindMainAssignerRefrence;
            }

            if (!IsSigned)
            {//ShowMessage("امضا کننده اصلی سند  ،برروی سند اقدام انجام نداده است.");
                return (int)AutomationErrors.MainAsignerDidNotAsignLetter;
                ;
            }
            #endregion
            //**************************************************************************
            Permission = 0;
            return Permission;
        }

        /// <summary>
        /// چک کردن شرایط برای شماره گذاری سند
        /// </summary>
        /// <param name="LetterId"></param>
        /// <param name="LetterType"></param>
        /// <param name="LetterStatus"></param>
        /// <param name="LetterCreationType"></param>
        /// <param name="SubscriberNmcId"></param>
        /// <returns></returns>
        public int CheckPermissionForNumbering(int LetterId, int LetterType, int LetterStatus, int LetterCreationType, int SubscriberNmcId, int DocNumberingRequestType)
        {
            LetterReferenceRecieversManager LetterReferenceRecieversManager = new LetterReferenceRecieversManager();
            LetterReferencesManager LetterReferencesManager = new LetterReferencesManager();
            //LetterReferenceActionsManager LetterReferenceActionsManager = new LetterReferenceActionsManager();
            NezamMemberChartManager NezamMemberChartManager = new NezamMemberChartManager();
            ResignationManager ResignationManager = new ResignationManager();

            int RefActionId = -1;
            int ReceptiveId = -1;
            int RefId = -1;
            Boolean NeedSubscriber = false;
            // int HowToSetDocNo = -1;

            //*******Check If Subscriber is required********************
            int CrtTypePer = CheckCreationTypeCondition(LetterCreationType, DocNumberingRequestType);
            if (CrtTypePer > 0)
            {
                NeedSubscriber = true;
            }
            else if (CrtTypePer < 0)
            {
                return CrtTypePer;
            }
            //**********************************************************


            if (LetterType == (int)TSP.DataManager.AutomationLetterTypes.EndedLetter)
            {// ShowMessage("امکان شماره دار کردن سند مختومه وجود ندارد.");
                return (int)AutomationErrors.EndedLetterCanNotBeNumbered;
            }
            if (LetterType == (int)TSP.DataManager.AutomationLetterTypes.Draft)
            {// ShowMessage("امکان شماره دار کردن سند پیش نویس وجود ندارد.");
                return (int)AutomationErrors.DraftLetterCanNotBeNumbered;
            }
            if (LetterStatus == (int)AutomationLetterStauts.HasNumber)
            {// ShowMessage("امکان شماره دار کردن مجدد سند وجود ندارد.");
                return (int)AutomationErrors.LetterCanNotReNumbered;
            }
            if (LetterCreationType < 0)
            {//  ShowMessage("نوع سند نامشخص است.");
                return -1;
            }
            if (LetterCreationType == (int)TSP.DataManager.AutomationLetterCreationType.In)
            {
                RefActionId = 0;
                return RefActionId;
            }

            #region Check Subscriber Sign
            if (!NeedSubscriber)
            {
                RefActionId = 0;
                return RefActionId;
            }
            if (SubscriberNmcId < 0)
            {//ShowMessage("امضا کننده اصلی سند نامشخص است.");
                return (int)AutomationErrors.CanNotFindLetterSubscriberNmcId;
            }

            LetterReferencesManager.FindByLetterId(LetterId);
            LetterReferencesManager.CurrentFilter = "Aim =" + ((int)TSP.DataManager.AutomationLetterReferenceAims.Signing).ToString();
            if (LetterReferencesManager.Count == 0)
            {//ShowMessage("برای سند انتخاب شده ارجاع جهت امضا وجود ندارد.");
                return (int)AutomationErrors.CanNotFindRefrenceForSign;
            }
            Boolean IsMainResign = false;
            for (int i = 0; i < LetterReferencesManager.Count; i++)
            {
                RefId = int.Parse(LetterReferencesManager[i]["RefId"].ToString());
                LetterReferenceRecieversManager.CurrentFilter = "";
                LetterReferenceRecieversManager.FindByReference(RefId);
                LetterReferenceRecieversManager.CurrentFilter = "Reciever =" + SubscriberNmcId.ToString();
                if (LetterReferenceRecieversManager.Count > 0)
                {
                    IsMainResign = true;
                    DataTable dtLetRefAct = LetterReferenceActionsManager.FindByReferenceRecievers(int.Parse(LetterReferenceRecieversManager[0]["RrId"].ToString()));
                    if (dtLetRefAct.Rows.Count == 1)
                    {
                        RefActionId = int.Parse(dtLetRefAct.Rows[0]["RAId"].ToString());
                        break;
                    }
                }

                NezamMemberChartManager.FindByNmcId(Convert.ToInt32(SubscriberNmcId));
                if (NezamMemberChartManager.Count == 1)
                {
                    ResignationManager.SelectValidResignationForAutomation(Convert.ToInt32(NezamMemberChartManager[0]["EmpId"]), LetterCreationType);
                    if (ResignationManager.Count > 0)
                    {
                        NezamMemberChartManager.FindByEmpId(Convert.ToInt32(ResignationManager[0]["ReceptiveId"]));
                        for (int j = 0; j < NezamMemberChartManager.Count; j++)
                        {
                            ReceptiveId = Convert.ToInt32(NezamMemberChartManager[0]["NmcId"]);
                            if (ReceptiveId > 0)
                            {
                                LetterReferenceRecieversManager.CurrentFilter = "";

                                LetterReferenceRecieversManager.CurrentFilter = "Reciever =" + ReceptiveId.ToString();
                                if (LetterReferenceRecieversManager.Count > 0)
                                {
                                    IsMainResign = true;
                                    DataTable dtLetRefAct = LetterReferenceActionsManager.FindByReferenceRecievers(int.Parse(LetterReferenceRecieversManager[0]["RrId"].ToString()));
                                    if (dtLetRefAct.Rows.Count == 1)
                                    {
                                        RefActionId = int.Parse(dtLetRefAct.Rows[0]["RAId"].ToString());
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!IsMainResign)
            {
                //ShowMessage("سند انتخاب شده به امضا کننده اصلی سند ارجاع داده نشده است.");
                return (int)AutomationErrors.CanNotFindMainAssignerRefrence;
            }

            if (RefActionId < 0)
            {
                // ShowMessage("امضا کننده اصلی سند  ،برروی سند اقدام انجام نداده است.");
                return (int)AutomationErrors.MainAsignerDidNotAsignLetter;
            }
            #endregion

            return RefActionId;
        }

        /// <summary>
        /// چک کردن شرایط شماره دار شدن سند بر اساس نوع آن و نوع درخواست شماره گذاری
        /// </summary>
        /// <param name="LetterCreationType"></param>
        /// <param name="DocNumberingRequestType"></param>
        /// <returns></returns>
        private int CheckCreationTypeCondition(int LetterCreationType, int DocNumberingRequestType)
        {
            LetterCreationTypesManager LetterCreationTypeManager = new LetterCreationTypesManager();

            int HowToSetDocNo = -1;
            int NeedSubscriber = 0;
            //*******Check If Subscriber is required*************
            if (LetterCreationType >= (int)TSP.DataManager.AutomationLetterCreationType.MinFormCreatorIndex)
            {
                LetterCreationTypeManager.FindById(LetterCreationType);
                if (LetterCreationTypeManager.Count != 1)
                {
                    return (int)AutomationErrors.CanNotFindRecord;
                }
                if (!Utility.IsDBNullOrNullValue(LetterCreationTypeManager[0]["MainSubscriber"])
                    && Convert.ToBoolean(LetterCreationTypeManager[0]["MainSubscriber"]))
                {
                    if (!Utility.IsDBNullOrNullValue(LetterCreationTypeManager[0]["MainSubscriberRequiredField"])
                    && Convert.ToBoolean(LetterCreationTypeManager[0]["MainSubscriberRequiredField"]))
                    {
                        NeedSubscriber = 1;
                    }
                }
                if (!Utility.IsDBNullOrNullValue(LetterCreationTypeManager[0]["HowToSetDocNo"]))
                {
                    HowToSetDocNo = Convert.ToInt32(LetterCreationTypeManager[0]["HowToSetDocNo"]);
                }

                switch (DocNumberingRequestType)
                {
                    case (int)AutomationHowToSetDocNo.BySecreteriat:
                        if (HowToSetDocNo != (int)AutomationHowToSetDocNo.BySecreteriat)
                        {// "امکان شماره گذاری این نوع سند بدون درخواست توسط کاربر امکان پذیر نمی باشد."
                            return (int)AutomationErrors.SercreteriateCanNotBeNumberingWithOutRequest;
                        }
                        break;
                    case (int)AutomationHowToSetDocNo.ByUser:
                        break;
                    case (int)AutomationHowToSetDocNo.Automatic:
                        if (HowToSetDocNo == (int)AutomationHowToSetDocNo.BySecreteriat
                            || HowToSetDocNo == (int)AutomationHowToSetDocNo.ByUser)
                        {// "امکان شماره گذاری این نوع سند بدون درخواست توسط کاربر امکان پذیر نمی باشد."
                            return (int)AutomationErrors.SercreteriateCanNotBeNumberingWithOutRequest;
                        }
                        break;
                }
            }
            else
            {
                NeedSubscriber = 1;
            }
            return NeedSubscriber;
        }

        /// <summary>
        /// شماره گذاری سند
        /// </summary>
        /// <param name="RAId"></param>
        /// <param name="LetterId"></param>
        /// <returns></returns>
        public int LetterNumbering(int RAId, int LetterId)
        {
            int Save = 0;
            Boolean IsSignImage = false;
            Boolean IsStampImage = false;
            if (RAId != 0)
            {
                LetterReferenceActionsManager.FindByRAId(RAId);
                if (LetterReferenceActionsManager.Count == 1)
                {
                    IsSignImage = Convert.ToBoolean(LetterReferenceActionsManager[0]["IsSignImage"]);
                    IsStampImage = Convert.ToBoolean(LetterReferenceActionsManager[0]["IsStampImage"]);
                }
                else
                { //ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
                    return (int)AutomationErrors.CanNotFindRecord;
                }
            }

            this.FindById(LetterId);
            if (this.Count == 1)
            {

                this[0].BeginEdit();
                if (!Utility.IsDBNullOrNullValue(this[0]["LetterNumber_P1"]))
                {
                    string NummberP1 = this[0]["LetterNumber_P1"].ToString();
                    this[0]["LetterNumber_P2"] = this.SelectAutomationLetterNewNumber(NummberP1);
                    this[0]["Status"] = (int)TSP.DataManager.AutomationLetterStauts.HasNumber;
                    this[0]["NumberingDate"] = Utility.GetDateOfToday();
                    this[0]["NumberingTime"] = Utility.GetCurrentTime();
                    //LetterManager[0]["Body"] = Body;
                    this[0].EndEdit();

                    if (this.Save() > 0)
                    {
                        this.DataTable.AcceptChanges();
                        if (Convert.ToInt32(this[0]["CreationType"]) == (int)TSP.DataManager.AutomationLetterCreationType.In)
                        {// ShowMessage("سند با موفقیت شماره گذاری شد.");
                            return (int)AutomationErrors.SaveSuccesfully;
                        }
                        else
                        {
                            string Body = this[0]["Body"].ToString();
                            int EmpId = int.Parse(this[0]["EmpId"].ToString());
                            string LetNo = this[0]["LetterNumber"].ToString();
                            string LetDate = this[0]["LetterDate"].ToString();
                            string LetFoloowCode = "- - -";
                            if (!Utility.IsDBNullOrNullValue(this[0]["LetterSerialNumber"]))
                                LetFoloowCode = this[0]["LetterSerialNumber"].ToString();
                            Body = AttachSignImageToLetter(Body, LetDate, LetNo, LetFoloowCode, EmpId, IsSignImage, IsStampImage, EmployeeManager, NezamManager);
                            this[0].BeginEdit();
                            this[0]["Body"] = Body;
                            this[0].EndEdit();
                            if (this.Save() > 0)
                            {  // ShowMessage("سند با موفقیت شماره گذاری شد.");
                                return (int)AutomationErrors.SaveSuccesfully;
                            }
                            else
                            {// ShowMessage("خطایی در ذخیره ایجاد شده است.");
                                return (int)AutomationErrors.Error;

                            }
                        }
                    }
                    else
                    {//  ShowMessage("خطایی در ذخیره ایجاد شده است.");
                        return (int)AutomationErrors.Error;
                    }
                }
                else
                { //  ShowMessage("خطایی در ذخیره ایجاد شده است.");
                    return (int)AutomationErrors.Error;

                }
            }
            else
            {// ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
                return (int)AutomationErrors.CanNotFindRecord;
            }
        }

        /// <summary>
        /// اضافه کردن تصویر امضا و مهر سازمان به متن سند
        /// </summary>
        /// <param name="LetterBody"></param>
        /// <param name="LetterDate"></param>
        /// <param name="LetterNumber"></param>
        /// <param name="LetFoloowCode"></param>
        /// <param name="EmpId"></param>
        /// <param name="IsSignImage"></param>
        /// <param name="IsStampImage"></param>
        /// <param name="EmployeeManager"></param>
        /// <param name="NezamManager"></param>
        /// <returns></returns>
        private string AttachSignImageToLetter(string LetterBody, string LetterDate, String LetterNumber, string LetFoloowCode, int EmpId, Boolean IsSignImage, Boolean IsStampImage, TSP.DataManager.EmployeeManager EmployeeManager, TSP.DataManager.NezamManager NezamManager)
        {
            EmployeeManager.FindByCode(EmpId);
            string OldValuePersonSign = "\"../../Images/SignImage.png\"";
            string OldValueNezamSign = "\"../../Images/NezamStamp.png\"";
            string NezamStamp = "\"../../Images/blank signature.png\"";
            string EmployeeSign = "\"../../Images/blank signature.png\"";
            if (IsSignImage)
            {
                if (EmployeeManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(EmployeeManager[0]["SignUrl"]))
                    {
                        EmployeeSign = EmployeeManager[0]["SignUrl"].ToString();

                    }
                }
                else
                {
                    LetterBody = "";

                }
            }

            if (IsStampImage)
            {
                NezamManager.Fill();
                if (NezamManager.Count > 0)
                {

                    if (!Utility.IsDBNullOrNullValue(NezamManager[NezamManager.Count - 1]["SignUrl"]))
                    {
                        NezamStamp = NezamManager[NezamManager.Count - 1]["SignUrl"].ToString();
                    }

                }
            }
            LetterBody = LetterBody.Replace(OldValuePersonSign, EmployeeSign);
            LetterBody = LetterBody.Replace(OldValueNezamSign, NezamStamp);
            LetterBody = LetterBody.Replace("*#Number#*", LetterNumber);
            LetterBody = LetterBody.Replace("*#Date#*", ReversedDate(LetterDate));
            LetterBody = LetterBody.Replace("*#FCode#*", LetFoloowCode);

            return LetterBody;
        }

        private string ReversedDate(string Date)
        {
            string ReversedDate = "";

            string[] SDate = Date.Split('/');
            for (int i = SDate.Length - 1; i >= 0; i--)
            {
                if (i != 0)
                    ReversedDate += SDate[i] + "/";
                else
                    ReversedDate += SDate[i];
            }
            if (string.IsNullOrEmpty(ReversedDate))
                return Date;
            else
                return ReversedDate;
        }
        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AutomationLetters);
        }

        private int _TtId = TableTypeManager.FindTtId(TableType.AutomationLetters);
        public int TtId
        {
            get { return _TtId; }
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Letters";
            tableMapping.ColumnMappings.Add("LetterId", "LetterId");
            tableMapping.ColumnMappings.Add("LetterNumber_P1", "LetterNumber_P1");
            tableMapping.ColumnMappings.Add("LetterNumber_P2", "LetterNumber_P2");
            tableMapping.ColumnMappings.Add("LetterNumber", "LetterNumber");
            tableMapping.ColumnMappings.Add("LetterSerialNumber", "LetterSerialNumber");
            tableMapping.ColumnMappings.Add("CreationType", "CreationType");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("TitleType", "TitleType");
            tableMapping.ColumnMappings.Add("SendRecieveType", "SendRecieveType");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("KeyWords", "KeyWords");
            tableMapping.ColumnMappings.Add("Title", "Title");
            tableMapping.ColumnMappings.Add("CreationDate", "CreationDate");
            tableMapping.ColumnMappings.Add("CreationTime", "CreationTime");
            tableMapping.ColumnMappings.Add("SenderName", "SenderName");
            tableMapping.ColumnMappings.Add("SenderLastName", "SenderLastName");
            tableMapping.ColumnMappings.Add("SenderOPersonId", "SenderOPersonId");
            tableMapping.ColumnMappings.Add("SenderMeId", "SenderMeId");
            tableMapping.ColumnMappings.Add("SenderOrgId", "SenderOrgId");
            tableMapping.ColumnMappings.Add("SenderOfId", "SenderOfId");
            tableMapping.ColumnMappings.Add("SenderNmcId", "SenderNmcId");
            tableMapping.ColumnMappings.Add("SenderType", "SenderType");
            tableMapping.ColumnMappings.Add("Creator", "Creator");
            tableMapping.ColumnMappings.Add("Distributor", "Distributor");
            tableMapping.ColumnMappings.Add("DistributorName", "DistributorName");
            tableMapping.ColumnMappings.Add("Secretariat", "Secretariat");
            tableMapping.ColumnMappings.Add("CopyLetterId", "CopyLetterId");
            tableMapping.ColumnMappings.Add("Body", "Body");
            tableMapping.ColumnMappings.Add("UsePassword", "UsePassword");
            tableMapping.ColumnMappings.Add("Password", "Password");
            tableMapping.ColumnMappings.Add("PasswordCreatorUserId", "PasswordCreatorUserId");
            tableMapping.ColumnMappings.Add("PasswordCreateDate", "PasswordCreateDate");
            tableMapping.ColumnMappings.Add("PasswordCreateTime", "PasswordCreateTime");
            tableMapping.ColumnMappings.Add("IndexNo", "IndexNo");
            tableMapping.ColumnMappings.Add("IndexDate", "IndexDate");
            tableMapping.ColumnMappings.Add("EmpId", "EmpId");
            tableMapping.ColumnMappings.Add("SubscriberNmcId", "SubscriberNmcId");
            tableMapping.ColumnMappings.Add("PhyscArchiveId", "PhyscArchiveId");
            tableMapping.ColumnMappings.Add("DivId", "DivId");
            tableMapping.ColumnMappings.Add("ArcId", "ArcId");
            tableMapping.ColumnMappings.Add("Status", "Status");
            tableMapping.ColumnMappings.Add("NumberingDate", "NumberingDate");
            tableMapping.ColumnMappings.Add("NumberingTime", "NumberingTime");
            tableMapping.ColumnMappings.Add("FileAddr", "FileAddr");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectAutomationLetters";
            this.Adapter.SelectCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@LetterId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@LetterNumber", System.Data.SqlDbType.NVarChar, 4000);
            this.Adapter.SelectCommand.Parameters.Add("@LetterSerialNumber", System.Data.SqlDbType.NVarChar, 15);
            this.Adapter.SelectCommand.Parameters.Add("@IndexNo", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@ArcId", System.Data.SqlDbType.Int, 4);
            this.Adapter.SelectCommand.Parameters.Add("@TitleType", System.Data.SqlDbType.Int, 4);

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteAutomationLetters";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LetterId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertAutomationLetters";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNumber_P1", global::System.Data.SqlDbType.NVarChar, 30, global::System.Data.ParameterDirection.Input, 0, 0, "LetterNumber_P1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNumber_P2", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterNumber_P2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterSerialNumber", global::System.Data.SqlDbType.NVarChar, 15, global::System.Data.ParameterDirection.Input, 0, 0, "LetterSerialNumber", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreationType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CreationType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "LetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TitleType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "TitleType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SendRecieveType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SendRecieveType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@KeyWords", global::System.Data.SqlDbType.NVarChar, 1024, global::System.Data.ParameterDirection.Input, 0, 0, "KeyWords", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Title", global::System.Data.SqlDbType.NVarChar, 150, global::System.Data.ParameterDirection.Input, 0, 0, "Title", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreationDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "CreationDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreationTime", global::System.Data.SqlDbType.NVarChar, 5, global::System.Data.ParameterDirection.Input, 0, 0, "CreationTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderMeId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderMeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderOrgId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderOrgId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderName", global::System.Data.SqlDbType.NVarChar, 100, global::System.Data.ParameterDirection.Input, 0, 0, "SenderName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderLastName", global::System.Data.SqlDbType.NVarChar, 100, global::System.Data.ParameterDirection.Input, 0, 0, "SenderLastName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderOPersonId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderOPersonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderNmcId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderNmcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderOfId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderOfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Creator", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Creator", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Distributor", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Distributor", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DistributorName", global::System.Data.SqlDbType.NVarChar, 100, global::System.Data.ParameterDirection.Input, 0, 0, "DistributorName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Secretariat", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Secretariat", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CopyLetterId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CopyLetterId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Body", global::System.Data.SqlDbType.NText, 1073741823, global::System.Data.ParameterDirection.Input, 0, 0, "Body", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsePassword", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "UsePassword", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Password", global::System.Data.SqlDbType.NVarChar, 50, global::System.Data.ParameterDirection.Input, 0, 0, "Password", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PasswordCreatorUserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "PasswordCreatorUserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PasswordCreateDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "PasswordCreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PasswordCreateTime", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "PasswordCreateTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IndexNo", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "IndexNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileAddr", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "FileAddr", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IndexDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "IndexDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EmpId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "EmpId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PhyscArchiveId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "PhyscArchiveId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DivId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "DivId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SubscriberNmcId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SubscriberNmcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ArcId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ArcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NumberingDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "NumberingDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NumberingTime", global::System.Data.SqlDbType.NVarChar, 5, global::System.Data.ParameterDirection.Input, 0, 0, "NumberingTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateAutomationLetters";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.ReturnValue, 10, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNumber_P1", global::System.Data.SqlDbType.NVarChar, 30, global::System.Data.ParameterDirection.Input, 0, 0, "LetterNumber_P1", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterNumber_P2", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterNumber_P2", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterSerialNumber", global::System.Data.SqlDbType.NVarChar, 15, global::System.Data.ParameterDirection.Input, 0, 0, "LetterSerialNumber", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "LetterDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TitleType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "TitleType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SendRecieveType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SendRecieveType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Type", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Type", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@KeyWords", global::System.Data.SqlDbType.NVarChar, 1024, global::System.Data.ParameterDirection.Input, 0, 0, "KeyWords", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Title", global::System.Data.SqlDbType.NVarChar, 150, global::System.Data.ParameterDirection.Input, 0, 0, "Title", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreationDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "CreationDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CreationTime", global::System.Data.SqlDbType.NVarChar, 5, global::System.Data.ParameterDirection.Input, 0, 0, "CreationTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderMeId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderMeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderOrgId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderOrgId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderNmcId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderNmcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderOfId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderOfId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderType", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderName", global::System.Data.SqlDbType.NVarChar, 100, global::System.Data.ParameterDirection.Input, 0, 0, "SenderName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderLastName", global::System.Data.SqlDbType.NVarChar, 100, global::System.Data.ParameterDirection.Input, 0, 0, "SenderLastName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SenderOPersonId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SenderOPersonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Creator", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Creator", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Distributor", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Distributor", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DistributorName", global::System.Data.SqlDbType.NVarChar, 100, global::System.Data.ParameterDirection.Input, 0, 0, "DistributorName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Secretariat", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "Secretariat", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CopyLetterId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "CopyLetterId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Body", global::System.Data.SqlDbType.NText, 1073741823, global::System.Data.ParameterDirection.Input, 0, 0, "Body", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsePassword", global::System.Data.SqlDbType.Bit, 1, global::System.Data.ParameterDirection.Input, 1, 0, "UsePassword", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Password", global::System.Data.SqlDbType.NVarChar, 50, global::System.Data.ParameterDirection.Input, 0, 0, "Password", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PasswordCreatorUserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "PasswordCreatorUserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PasswordCreateDate", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "PasswordCreateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PasswordCreateTime", global::System.Data.SqlDbType.NVarChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "PasswordCreateTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IndexNo", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "IndexNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IndexDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "IndexDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EmpId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "EmpId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PhyscArchiveId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "PhyscArchiveId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DivId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "DivId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 8, global::System.Data.ParameterDirection.Input, 23, 3, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LetterId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 8, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LetterId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "LetterId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SubscriberNmcId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "SubscriberNmcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ArcId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 10, 0, "ArcId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Status", global::System.Data.SqlDbType.SmallInt, 2, global::System.Data.ParameterDirection.Input, 5, 0, "Status", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NumberingDate", global::System.Data.SqlDbType.NChar, 10, global::System.Data.ParameterDirection.Input, 0, 0, "NumberingDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NumberingTime", global::System.Data.SqlDbType.NVarChar, 5, global::System.Data.ParameterDirection.Input, 0, 0, "NumberingTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileAddr", global::System.Data.SqlDbType.NVarChar, 2147483647, global::System.Data.ParameterDirection.Input, 0, 0, "FileAddr", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.Automation.AutomationDataSet.LettersDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindById(int Id)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@LetterId"].Value = Id;
            Fill();
        }

        public void FindArcId(int ArcId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ArcId"].Value = ArcId;
            Fill();
        }

        public void FindByLetterNumber(String LetterNumber)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@LetterNumber"].Value = LetterNumber;
            Fill();
        }

        public void FindByIndexNo(String IndexNo)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@IndexNo"].Value = IndexNo;
            Fill();
        }

        public void FindByLetterSerialNumber(String LetterSerialNumber)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@LetterSerialNumber"].Value = LetterSerialNumber;
            Fill();
        }

        public void FindByTitleType(int TitleType)
        {
            DataTable.Clear();
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@TitleType"].Value = TitleType;
            Fill();
        }

        public DataTable SelectAutomationLettersSerialNumber(String LetterSerialNumber)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectAutomationLettersSerialNumber";
            objCommand.Parameters.AddWithValue("LetterSerialNumber", LetterSerialNumber);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return objTable;
        }

        public int SelectAutomationLetterNewNumber(String LetterNumber_P1)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.Transaction = this.Transaction;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectAutomationLetterNewNumber";
            objCommand.Parameters.AddWithValue("@LetterNumber_P1", LetterNumber_P1);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return int.Parse(objTable.Rows[0]["NewLetterNumber_P2"].ToString());
        }

        public Boolean CheckLetterPassword(int LetterId, String Password)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectAutomationLetterCheckPassword";
            objCommand.Parameters.AddWithValue("@LetterId", LetterId);
            objCommand.Parameters.AddWithValue("@Password", Password);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            if (objTable.Rows.Count > 0)
                return true;
            return false;
        }

        public DataTable SelectAutomationLettersForFormCreator(int LetterCreationTypeId)
        {
            DataTable objTable = new DataTable();
            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = this.Connection;
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "spSelectAutomationLettersForFormCreator";
            objCommand.Parameters.AddWithValue("@LetterCreationTypeId", LetterCreationTypeId);
            SqlDataAdapter objAdapter = new System.Data.SqlClient.SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(objTable);
            }
            catch (Exception) { }

            return objTable;
        }

    }
}
