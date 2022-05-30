using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.Automation
{
    public enum RefrenceErrorCode
    {
        SavedSuccefully = 0,
        Error = -1,
        CanNotFindRefReciever = -2,
        CanNotFindTemplate = -3,
        CanNotSaveRefrenceByUser = -4,
        OnlyEndedLettersCanBeSigned = -5,
        CanNotReferLockLetter = -6,
        EndedLettersCanNotBeRefrenced = -7,
        CanNotReferHiddenAndCopyReference = -8,
        CanNotFindUserNmcId = -9
    }

    public class ReferenceSettingManager : BaseObject
    {

        #region Public Parameters
        NezamMemberChartManager NezamMeChartManager;
        LetterReferencesManager ReferenceManager;
        LetterReferenceRecieversManager RecieverManager;
        LetterInReferenceManager LetterInReferenceManager;
        SecretariatNezamChartManager SecretariatNezamChartManager;
        LettersManager LettersManager;
        ResignationManager ResignationManager;
        CartablesManager CartableManager;
        LoginManager LoginManager;
        ReferenceSettingReceiverManager ReferenceSetReceiverManager;
        #endregion

        #region Constructors
        public ReferenceSettingManager(TransactionManager Trans)
        {
            NezamMeChartManager = new NezamMemberChartManager();
            ReferenceManager = new LetterReferencesManager();
            RecieverManager = new LetterReferenceRecieversManager();
            LetterInReferenceManager = new LetterInReferenceManager();
            CartableManager = new CartablesManager();
            SecretariatNezamChartManager = new SecretariatNezamChartManager();
            LettersManager = new LettersManager();
            ResignationManager = new ResignationManager();
            LoginManager = new LoginManager();
            ReferenceSetReceiverManager = new ReferenceSettingReceiverManager();
            if (Trans != null)
            {
                Trans.Add(NezamMeChartManager);
                Trans.Add(ReferenceManager);
                Trans.Add(RecieverManager);
                Trans.Add(LetterInReferenceManager);
                Trans.Add(CartableManager);
                Trans.Add(SecretariatNezamChartManager);
                Trans.Add(LettersManager);
                Trans.Add(ResignationManager);
                Trans.Add(LoginManager);
                Trans.Add(ReferenceSetReceiverManager);
            }
        }

        public ReferenceSettingManager()
        {
        }
        #endregion

        /// <summary>
        /// Return the suitable Message for specific ErrorCode
        /// </summary>
        /// <param name="ErrorCode"></param>
        /// <returns></returns>
        public string FindErrorMsg(int ErrorCode, string Title)
        {
            string ErrorMsg = "";
            switch (ErrorCode)
            {
                case (int)RefrenceErrorCode.Error:
                    ErrorMsg = "خطایی در ذخیره انجام گرفته است";
                    break;
                case (int)RefrenceErrorCode.CanNotFindRefReciever:
                    ErrorMsg = "گیرندگان ارجاع نامشخص می باشد.";
                    break;
                case (int)RefrenceErrorCode.CanNotFindTemplate:
                    ErrorMsg = "برای  سند با مشخصات جاری ، قالبی جهت گردش سند وجود ندارد.";
                    break;

                case (int)RefrenceErrorCode.CanNotSaveRefrenceByUser:
                    ErrorMsg = "برای سند با مشخصات جاری ، امکان گردش سند توسط کاربر وجود ندارد.";
                    break;
                case (int)RefrenceErrorCode.OnlyEndedLettersCanBeSigned:
                    ErrorMsg = "تنها در صورت نهایی بودن سند امکان ارجاع جهت امضا وجود دارد.";
                    break;
                case (int)RefrenceErrorCode.CanNotReferLockLetter:
                    ErrorMsg = "سند قفل شده و امکان ارجاع آن وجود ندارد.";
                    break;
                case (int)RefrenceErrorCode.EndedLettersCanNotBeRefrenced:
                    ErrorMsg = "امکان  ارجاع سند مختومه وجود ندارد.";
                    break;
                case (int)RefrenceErrorCode.CanNotReferHiddenAndCopyReference:
                    ErrorMsg = "ارجاع سندی که از نوع ارجاع مخفی یا رونوشت است، امکان پذیر نمی باشد.";
                    break;
                case (int)RefrenceErrorCode.SavedSuccefully:
                    ErrorMsg = "گردش سند طبق قالب از پیش تعریف شده " + "'" + Title + "'" + " با موفقیت انجام شد.";
                    break;
                case (int)RefrenceErrorCode.CanNotFindUserNmcId:
                    ErrorMsg = "اطلاعات شما در چارت سازمانی ثبت نشده است";
                    break;

            }
            return ErrorMsg;
        }

        #region Automatic Refrence Methods

        public class AutoRefrenceInfo
        {
            public int RefSetId;
            //public int RefType;
            public string Title;
            public int Aim;
            public int Priority;
            public int Division;
            public string RefSetName;
            public string ActionExpireDate;
            public int RefSetExeTime;
            public int ParentId;
        }

        public AutoRefrenceInfo ResetAutoRefRefrenceInfo(AutoRefrenceInfo AutoRefInfo)
        {
            AutoRefInfo.RefSetId = -1;
            AutoRefInfo.ParentId = -1;
            AutoRefInfo.Aim = -1;
            AutoRefInfo.Division = -1;
            AutoRefInfo.Priority = -1;
            AutoRefInfo.RefSetExeTime = -1;
            AutoRefInfo.Title = "";
            return AutoRefInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RefSetSId">RefSetSId >0 : from secreteriate's Cartable , RefSetSId=-1 : From Employee's Cartable </value></param>
        /// <param name="CartableId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="RefExeTimeType">Current Execute Time</param>
        /// <returns></returns>
        public AutoRefrenceInfo CheckConditonForAutoRefrence(int RefSetSId, int CartableId, int CurrentUserId, int RefExeTimeType, AutoRefrenceInfo AutoRefInfo)
        {
            //  AutoRefrenceInfo AutoRefInfo = new AutoRefrenceInfo();           
            //  CartablesManager CartableManager = new CartablesManager();


            int CartableLetterType = -1;
            int CreationType = -1;
            int SId = -1;
            int LetterTitleTypeId = -1;
            int LetterTypeId = -1;
            int LetterDivId = -1;
            int AssignerId = -1;

            int LetRefPriorityId = -1;
            int RefDivId = -1;
            int LetRefAimId = -1;
            int LetRefTypeId = -1;
            int LetRefSenderId = -1;
            int LetterId = -1;

            CartableManager.FindById(CartableId);
            LetterId = Convert.ToInt32(CartableManager[0]["LetterId"]);
            CartableLetterType = Convert.ToInt32(CartableManager[0]["CartableLetterType"]);
            CreationType = Convert.ToInt32(CartableManager[0]["CreationType"]);
            SId = Convert.ToInt32(CartableManager[0]["Secretariat"]);
            LetterTitleTypeId = Convert.ToInt32(CartableManager[0]["TitleType"]);
            LetterTypeId = Convert.ToInt32(CartableManager[0]["Type"]);
            LetterDivId = Convert.ToInt32(CartableManager[0]["DivId"]);
            if (!Utility.IsDBNullOrNullValue(CartableManager[0]["SubscriberNmcId"]))
                AssignerId = Convert.ToInt32(CartableManager[0]["SubscriberNmcId"]);
            if (AutoRefInfo.ParentId != -1)
            {
                RecieverManager.FindById(AutoRefInfo.ParentId);
                if (Convert.ToInt32(RecieverManager[0]["RefType"]) != (int)LetterReferenceTypesManager.ReferenceTypes.Normal)
                {
                    AutoRefInfo.RefSetId = (int)RefrenceErrorCode.CanNotReferHiddenAndCopyReference;
                    return AutoRefInfo;
                }
                LetRefPriorityId = Convert.ToInt32(RecieverManager[0]["Priority"]);
                RefDivId = Convert.ToInt32(RecieverManager[0]["LetterDivision"]);
                LetRefAimId = Convert.ToInt32(RecieverManager[0]["Aim"]);
                LetRefTypeId = Convert.ToInt32(RecieverManager[0]["RefType"]);
                LetRefSenderId = Convert.ToInt32(RecieverManager[0]["ReferenceSender"]);
            }
            FindRefrenceSetting(AutoRefInfo, RefSetSId, CurrentUserId, CartableLetterType, CreationType, SId, LetterTitleTypeId, LetterTypeId, LetterDivId, AssignerId,
                 LetRefPriorityId, RefDivId, LetRefAimId, LetRefTypeId, LetRefSenderId);

            if (LetterTypeId == (int)AutomationLetterTypes.EndedLetter)
            {
                AutoRefInfo.RefSetId = (int)RefrenceErrorCode.EndedLettersCanNotBeRefrenced;
                return AutoRefInfo;
            }

            LetterLockManager LetterLockManager = new TSP.DataManager.Automation.LetterLockManager();
            if (LetterLockManager.CheckLetterLockState(LetterId) == true)
            {
                AutoRefInfo.RefSetId = (int)RefrenceErrorCode.CanNotReferLockLetter;
                return AutoRefInfo;
            }
            if (AutoRefInfo.ParentId != -1)
            {
                RecieverManager.FindById(AutoRefInfo.ParentId);
                if (Convert.ToInt32(RecieverManager[0]["RefType"]) != (int)LetterReferenceTypesManager.ReferenceTypes.Normal)
                {
                    AutoRefInfo.RefSetId = (int)RefrenceErrorCode.CanNotReferHiddenAndCopyReference;
                    return AutoRefInfo;
                }
            }

            if (AutoRefInfo.RefSetId == -1)
            {
                AutoRefInfo.RefSetId = (int)RefrenceErrorCode.CanNotFindTemplate;
                return AutoRefInfo;
            }

            if (AutoRefInfo.RefSetExeTime != RefExeTimeType)
            {
                AutoRefInfo.RefSetId = (int)RefrenceErrorCode.CanNotSaveRefrenceByUser;
                return AutoRefInfo;
            }

            if (LetterTypeId != (int)AutomationLetterTypes.Letter && AutoRefInfo.Aim == (int)AutomationLetterReferenceAims.Signing)
            {
                AutoRefInfo.RefSetId = (int)RefrenceErrorCode.OnlyEndedLettersCanBeSigned;
                return AutoRefInfo;
            }


            return AutoRefInfo;
        }

        private AutoRefrenceInfo FindRefrenceSetting(AutoRefrenceInfo AutoRefInfo, int RefSetSId, int RefSetUserId, int CartableLetterType, int CreationType, int SId, int LetterTitleTypeId, int LetterTypeId, int LetterDivId, int AssignerId, int LetRefPriorityId, int RefDivId, int LetRefAimId, int LetRefTypeId, int LetRefSenderId)
        {
            // AutoRefrenceInfo AutoRefInfo = new AutoRefrenceInfo();
            //  AutoRefInfo = ResetAutoRefRefrenceInfo(AutoRefInfo);

            ReferenceSettingManager ReferenceSetManager = new ReferenceSettingManager();
            if (RefSetSId > 0)
                ReferenceSetManager.SearchRefrenceSettingBySecreteriate(RefSetSId, CartableLetterType, CreationType, SId, LetterTitleTypeId, LetterTypeId, LetterDivId, AssignerId, LetRefPriorityId, RefDivId, LetRefAimId, LetRefTypeId, LetRefSenderId);
            else
                ReferenceSetManager.SearchRefrenceSettingByUser(RefSetUserId, CartableLetterType, CreationType, SId, LetterTitleTypeId, LetterTypeId, LetterDivId, AssignerId, LetRefPriorityId, RefDivId, LetRefAimId, LetRefTypeId, LetRefSenderId);
            if (ReferenceSetManager.Count == 1)
            {
                AutoRefInfo.RefSetId = Convert.ToInt32(ReferenceSetManager[0]["RefSetId"]);
                AutoRefInfo.Aim = Convert.ToInt32(ReferenceSetManager[0]["RefAim"]);
                AutoRefInfo.Division = Convert.ToInt32(ReferenceSetManager[0]["RefDivision"]);
                AutoRefInfo.Priority = Convert.ToInt32(ReferenceSetManager[0]["RefPriority"]);
                AutoRefInfo.Title = ReferenceSetManager[0]["RefTitle"].ToString();
                AutoRefInfo.RefSetName = ReferenceSetManager[0]["RefSetName"].ToString();
                AutoRefInfo.RefSetExeTime = Convert.ToInt32(ReferenceSetManager[0]["RefSetExeTime"]);
            }
            return AutoRefInfo;
        }

        public int SaveAutomaticReference(int LetterId, AutoRefrenceInfo AutoRefInfo, int CurrentUserNmcId, int CurrentUserId)
        {
            int ReferenceId = -1;
            ReferenceId = SaveReference(LetterId, AutoRefInfo, CurrentUserNmcId, CurrentUserId);
            if (ReferenceId <= 0)
            {
                return (int)RefrenceErrorCode.Error;
            }
            DataTable dtRefSetReciever = ReferenceSetReceiverManager.FindByRefSetId(AutoRefInfo.RefSetId);
            if (dtRefSetReciever.Rows.Count <= 0)
            {
                return (int)RefrenceErrorCode.CanNotFindRefReciever;
            }
            SaveRefrenceRecievers(LetterId, ReferenceId, AutoRefInfo, dtRefSetReciever, CurrentUserId);

            return (int)RefrenceErrorCode.SavedSuccefully;
        }

        /// <summary>
        /// ذخیره ارجاع اتوماتیک جهت گردش سند استفاده میشود
        /// </summary>
        /// <param name="LetterId"></param>
        /// <param name="ReferenceManager"></param>
        /// <param name="NezamMemberChartManager"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private int SaveReference(int LetterId, AutoRefrenceInfo AutoRefInfo, int CurrentUserNmcId, int CurrentUserId)
        {
            DataRow drReference = ReferenceManager.NewRow();
            //************RefType is in the RefrenceReciever's Table And SPSelects use those that*****************************
            drReference["RefType"] = (int)LetterReferenceTypesManager.ReferenceTypes.Normal;
            //**************************************************************************************************
            drReference["Letter"] = LetterId;
            drReference["Priority"] = AutoRefInfo.Priority;
            drReference["LetterDivision"] = AutoRefInfo.Division;
            drReference["Aim"] = AutoRefInfo.Aim;
            drReference["Title"] = "ارجاع اتوماتیک: " + AutoRefInfo.Title;// "ارجاع اتوماتیک سیستم";
            drReference["Description"] = "ارجاع اتوماتیک";
            drReference["CreateDate"] = Utility.GetDateOfToday();
            drReference["CreateTime"] = Utility.GetCurrentTime();
            drReference["ReferenceSender"] = CurrentUserNmcId;
            if (AutoRefInfo.Aim != (int)AutomationLetterReferenceAims.Notice)
                drReference["ActionExpireDate"] = AutoRefInfo.ActionExpireDate;
            drReference["UserId"] = CurrentUserId;
            drReference["ModifiedDate"] = DateTime.Now;
            ReferenceManager.AddRow(drReference);
            if (ReferenceManager.Save() > 0)
            {
                ReferenceManager.DataTable.AcceptChanges();
                return int.Parse(ReferenceManager[ReferenceManager.Count - 1]["RefId"].ToString());
            }
            return -1;
        }


        public void SaveRefrenceRecievers(int LetterId, int ReferenceId, AutoRefrenceInfo AutoRefInfo, DataTable dtRefSetReciever, int CurrentUserId)
        {
            foreach (DataRow Reciever in dtRefSetReciever.Rows)
            {
                NezamMeChartManager.FindByNmcId(Convert.ToInt32(Reciever["ReceiverId"]));

                if (NezamMeChartManager.Count == 0)
                {

                }
                DataRow drReciever = RecieverManager.NewRow();
                drReciever["LetterReference"] = ReferenceId;
                drReciever["RecieverType"] = (int)TSP.DataManager.AutomationLetterReferenceRecieverTypes.Employee;
                drReciever["Reciever"] = Convert.ToInt32(Reciever["ReceiverId"]);
                drReciever["RecieverNCId"] = NezamMeChartManager[0]["NCId"];
                if (AutoRefInfo.ParentId == -1)
                    drReciever["ParentId"] = DBNull.Value;
                else
                    drReciever["ParentId"] = AutoRefInfo.ParentId;
                drReciever["RefType"] = Reciever["RefType"];
                drReciever["UserId"] = CurrentUserId;
                drReciever["ModifiedDate"] = DateTime.Now;
                RecieverManager.AddRow(drReciever);
                if (RecieverManager.Save() > 0)
                {
                    RecieverManager.DataTable.AcceptChanges();
                    //****************LetterInReference
                    DataRow drLetterInReference = LetterInReferenceManager.NewRow();
                    drLetterInReference["ReferenceReciever"] = RecieverManager[RecieverManager.Count - 1]["RrId"].ToString();
                    drLetterInReference["IsComplete"] = false;
                    drLetterInReference["UserId"] = CurrentUserId;
                    drLetterInReference["ModifiedDate"] = DateTime.Now;
                    LetterInReferenceManager.AddRow(drLetterInReference);
                    if (LetterInReferenceManager.Save() > 0)
                    {
                        LetterInReferenceManager.DataTable.AcceptChanges();
                        LoginManager.FindByMeIdUltId(int.Parse(NezamMeChartManager[0]["EmpId"].ToString()), (int)UserType.Employee);
                        if (LoginManager.Count > 0)
                        {
                            DataRow drCartable = CartableManager.NewRow();
                            drCartable["LetterId"] = LetterId;
                            drCartable["LetterInReference"] = LetterInReferenceManager[LetterInReferenceManager.Count - 1]["RLId"].ToString();
                            if (AutoRefInfo.Aim == (int)AutomationLetterReferenceAims.FollowUp)
                                drCartable["CartableGroup"] = CartableGroupsManager.FollowUpGroup;
                            else
                                drCartable["CartableGroup"] = CartableGroupsManager.DefaultGroup;
                            drCartable["CartableLetterType"] = (int)CartableLetterTypesManager.LetterTypes.Reference;
                            drCartable["ViewState"] = false;
                            drCartable["InActive"] = false;
                            drCartable["CartableUserId"] = LoginManager[0]["UserId"].ToString();
                            drCartable["UserId"] = CurrentUserId;
                            drCartable["ModifiedDate"] = DateTime.Now;
                            CartableManager.AddRow(drCartable);
                            if (CartableManager.Save() > 0)
                                CartableManager.DataTable.AcceptChanges();
                        }

                        //************************Secretariat's Cartable
                        SecretariatNezamChartManager.FindByNezamChartId(Convert.ToInt32(NezamMeChartManager[0]["NCId"]));
                        if (SecretariatNezamChartManager.Count > 0)
                        {
                            int SId = Convert.ToInt32(SecretariatNezamChartManager[0]["SecretariatId"]);
                            if (CartableManager.SelectCountSecretariatAndLetter(SId, LetterId) == 0)
                            {
                                DataRow drCartable = CartableManager.NewRow();
                                drCartable["LetterId"] = LetterId;
                                drCartable["LetterInReference"] = LetterInReferenceManager[LetterInReferenceManager.Count - 1]["RLId"].ToString();
                                drCartable["CartableGroup"] = CartableGroupsManager.DefaultGroup;
                                drCartable["CartableLetterType"] = (int)CartableLetterTypesManager.LetterTypes.Reference;
                                drCartable["ViewState"] = false;
                                drCartable["InActive"] = false;
                                drCartable["CartableSecretariatId"] = SId;
                                drCartable["UserId"] = CurrentUserId;
                                drCartable["ModifiedDate"] = DateTime.Now;
                                CartableManager.AddRow(drCartable);
                                if (CartableManager.Save() > 0)
                                    CartableManager.DataTable.AcceptChanges();
                            }
                        }
                    }
                }

                LettersManager.FindById(LetterId);
                if (LettersManager.Count > 0)
                {
                    ResignationManager.SelectValidResignationForAutomation(Convert.ToInt32(NezamMeChartManager[0]["EmpId"]), Convert.ToInt32(LettersManager[0]["CreationType"]));
                    if (ResignationManager.Count > 0)
                        SaveResignedRecievers(AutoRefInfo, ReferenceId, LetterId, Convert.ToInt32(ResignationManager[0]["ReceptiveId"]), CurrentUserId);
                }
            }
            //****Secretriat
            //if (!Utility.IsDBNullOrNullValue(HiddenFieldRecieversSecretariat["Keys"]))
            //{
            //    String[] RecieversSecretariatHidden = HiddenFieldRecieversSecretariat["Keys"].ToString().Trim().Split(';');
            //    SendToSecreteriat(RecieversSecretariatHidden, ReferenceId, RecieverManager, LetterInReferenceManager, CartableManager, UserId, LettersManager);
            //}
        }

        private void SaveResignedRecievers(AutoRefrenceInfo AutoRefInfo, int ReferenceId, int LetterId, int RecieversNmcId, int CurrentUserId)
        {
            NezamMeChartManager.FindByEmpId(RecieversNmcId);

            DataRow drReciever = RecieverManager.NewRow();
            drReciever["LetterReference"] = ReferenceId;

            drReciever["IsResignation"] = 1;

            drReciever["Reciever"] = NezamMeChartManager[0]["NmcId"];
            drReciever["RecieverNCId"] = NezamMeChartManager[0]["NCId"];
            if (AutoRefInfo.ParentId == -1)
                drReciever["ParentId"] = DBNull.Value;
            else
                drReciever["ParentId"] = AutoRefInfo.ParentId;
            drReciever["UserId"] = CurrentUserId;
            drReciever["ModifiedDate"] = DateTime.Now;
            RecieverManager.AddRow(drReciever);
            if (RecieverManager.Save() > 0)
            {
                RecieverManager.DataTable.AcceptChanges();

                //*********************LetterInReference
                DataRow drLetterInReference = LetterInReferenceManager.NewRow();
                drLetterInReference["ReferenceReciever"] = RecieverManager[RecieverManager.Count - 1]["RrId"].ToString();
                drLetterInReference["IsComplete"] = false;
                drLetterInReference["UserId"] = CurrentUserId;
                drLetterInReference["ModifiedDate"] = DateTime.Now;
                LetterInReferenceManager.AddRow(drLetterInReference);
                if (LetterInReferenceManager.Save() > 0)
                {
                    LetterInReferenceManager.DataTable.AcceptChanges();

                    //*************Cartable                   
                    LoginManager.FindByMeIdUltId(int.Parse(NezamMeChartManager[0]["EmpId"].ToString()), (int)TSP.DataManager.UserType.Employee);
                    if (LoginManager.Count > 0)
                    {
                        DataRow drCartable = CartableManager.NewRow();
                        drCartable["LetterId"] = LetterId;
                        drCartable["LetterInReference"] = LetterInReferenceManager[LetterInReferenceManager.Count - 1]["RLId"].ToString();
                        if (AutoRefInfo.Aim == (int)TSP.DataManager.AutomationLetterReferenceAims.FollowUp)
                            drCartable["CartableGroup"] = CartableGroupsManager.FollowUpGroup;
                        else
                            drCartable["CartableGroup"] = CartableGroupsManager.DefaultGroup;
                        drCartable["CartableLetterType"] = (int)CartableLetterTypesManager.LetterTypes.Reference;
                        drCartable["ViewState"] = false;
                        drCartable["InActive"] = false;
                        drCartable["CartableUserId"] = LoginManager[0]["UserId"].ToString();
                        drCartable["UserId"] = CurrentUserId;
                        drCartable["ModifiedDate"] = DateTime.Now;
                        CartableManager.AddRow(drCartable);
                        if (CartableManager.Save() > 0)
                            CartableManager.DataTable.AcceptChanges();
                    }

                    //Secretariat's Cartable
                    SecretariatNezamChartManager.FindByNezamChartId(Convert.ToInt32(NezamMeChartManager[0]["NCId"]));
                    if (SecretariatNezamChartManager.Count > 0)
                    {
                        int SId = Convert.ToInt32(SecretariatNezamChartManager[0]["SecretariatId"]);
                        if (CartableManager.SelectCountSecretariatAndLetter(SId, LetterId) == 0)
                        {
                            DataRow drCartable = CartableManager.NewRow();
                            drCartable["LetterId"] = LetterId;
                            drCartable["LetterInReference"] = LetterInReferenceManager[LetterInReferenceManager.Count - 1]["RLId"].ToString();
                            drCartable["CartableGroup"] = CartableGroupsManager.DefaultGroup;
                            drCartable["CartableLetterType"] = (int)CartableLetterTypesManager.LetterTypes.Reference;
                            drCartable["ViewState"] = false;
                            drCartable["InActive"] = false;
                            drCartable["CartableSecretariatId"] = SId;
                            drCartable["UserId"] = CurrentUserId;
                            drCartable["ModifiedDate"] = DateTime.Now;
                            CartableManager.AddRow(drCartable);
                            if (CartableManager.Save() > 0)
                                CartableManager.DataTable.AcceptChanges();
                        }
                    }
                }
            }
        }

        #endregion

        public enum ReferenceSettingUserType
        {
            User = 1,
            Secretariat = 2
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AutomationReferenceSetting);
        }


        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {
                    this._dataTable = new DataManager.Automation.AutomationDataSet.ReferenceSettingDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }
                return this._dataTable;
            }
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "ReferenceSetting";
            tableMapping.ColumnMappings.Add("RefSetId", "RefSetId");
            tableMapping.ColumnMappings.Add("RefSetName", "RefSetName");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("RefSetUserId", "RefSetUserId");
            tableMapping.ColumnMappings.Add("RefSetSId", "RefSetSId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("RefSetExeTime", "RefSetExeTime");
            tableMapping.ColumnMappings.Add("CartableLetterType", "CartableLetterType");
            tableMapping.ColumnMappings.Add("CreationType", "CreationType");
            tableMapping.ColumnMappings.Add("RefTitle", "RefTitle");
            tableMapping.ColumnMappings.Add("RefAim", "RefAim");
            tableMapping.ColumnMappings.Add("RefPriority", "RefPriority");
            tableMapping.ColumnMappings.Add("RefDivision", "RefDivision");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("SId", "SId");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectAutomationReferenceSetting";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@RefSetId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@RefSetUserId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@RefSetSId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@RefSetName", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@CartableLetterType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@CreationType", System.Data.SqlDbType.NVarChar);
            this.Adapter.SelectCommand.Parameters.Add("@RefDivision", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@SId", System.Data.SqlDbType.Int);


            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteAutomationReferenceSetting";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RefSetId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertAutomationReferenceSetting";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetUserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetUserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetSId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetSId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetExeTime", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetExeTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CartableLetterType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CartableLetterType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreationType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CreationType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefTitle", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RefTitle", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefAim", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefAim", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefPriority", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefPriority", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefDivision", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefDivision", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateAutomationReferenceSetting";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetUserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetUserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetSId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetSId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetExeTime", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetExeTime", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CartableLetterType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CartableLetterType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreationType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CreationType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefTitle", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RefTitle", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefAim", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefAim", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefPriority", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefPriority", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefDivision", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefDivision", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RefSetId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "RefSetId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RefSetId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "RefSetId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public DataTable FindByCode(int RefSetId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RefSetId"].Value = RefSetId;
            this.Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByRefSetUserId(int UserType, int Id)
        {
            this.ResetAllParameters();
            if (UserType == (int)ReferenceSettingUserType.User)
                this.Adapter.SelectCommand.Parameters["@RefSetUserId"].Value = Id;
            else if (UserType == (int)ReferenceSettingUserType.Secretariat)
                this.Adapter.SelectCommand.Parameters["@RefSetSId"].Value = Id;
            this.Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByUser(int UserId, int SId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RefSetUserId"].Value = UserId;
            this.Adapter.SelectCommand.Parameters["@RefSetSId"].Value = SId;
            this.Fill();
            return this.DataTable;
        }

        public DataTable FindForDuplicate(String RefSetName, int CartableLetterType, int CreationType, int RefDivision,
            int SId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RefSetName"].Value = RefSetName;
            this.Adapter.SelectCommand.Parameters["@CartableLetterType"].Value = CartableLetterType;
            this.Adapter.SelectCommand.Parameters["@CreationType"].Value = CreationType;
            this.Adapter.SelectCommand.Parameters["@RefDivision"].Value = RefDivision;
            this.Adapter.SelectCommand.Parameters["@SId"].Value = SId;
            this.Fill();
            return this.DataTable;
        }


        public DataTable FindForDuplicate(int RefSetId, int CartableLetterType, int CreationType, int SId, String LetterTitleTypeIDs,
            String LetterDivisionIDs, String LetterTypeIDs, String AssignerIDs, String RefAimsIDS, String RefPriorityIDs,
            String RefTypeIDs, String RefSenderIDs, String RefDivisionIDs)
        {
            SqlCommand cmd = new SqlCommand("spSelectAutomationReferenceSettingFindDuplicate", this.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefSetId", RefSetId);
            cmd.Parameters.AddWithValue("@CartableLetterType", CartableLetterType);
            cmd.Parameters.AddWithValue("@CreationType", CreationType);
            cmd.Parameters.AddWithValue("@SId", SId);
            cmd.Parameters.AddWithValue("@LetterTitleTypeIDs", LetterTitleTypeIDs);
            cmd.Parameters.AddWithValue("@LetterDivisionIDs", LetterDivisionIDs);
            cmd.Parameters.AddWithValue("@LetterTypeIDs", LetterTypeIDs);
            cmd.Parameters.AddWithValue("@AssignerIDs", AssignerIDs);
            cmd.Parameters.AddWithValue("@RefAimsIDs", RefAimsIDS);
            cmd.Parameters.AddWithValue("@RefPriorityIDs", RefPriorityIDs);
            cmd.Parameters.AddWithValue("@RefTypeIDs", RefTypeIDs);
            cmd.Parameters.AddWithValue("@RefSenderIDs", RefSenderIDs);
            cmd.Parameters.AddWithValue("@RefDivisionIDs", RefDivisionIDs);
            cmd.Connection.Open();
            new SqlDataAdapter(cmd).Fill(this.DataTable);
            return this.DataTable;
        }


        public DataTable FindForDuplicateName(String RefSetName)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@RefSetName"].Value = RefSetName;
            this.Fill();
            return this.DataTable;
        }

        public DataTable FindForDuplicate(int CartableLetterType, int CreationType, int SId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@CartableLetterType"].Value = CartableLetterType;
            this.Adapter.SelectCommand.Parameters["@CreationType"].Value = CreationType;
            this.Adapter.SelectCommand.Parameters["@SId"].Value = SId;
            this.Fill();
            return this.DataTable;
        }

        public void SearchRefrenceSetting(int RefSetUserId, int RefSetSId, int CartableLetterType, int CreationType, int SId
           , int LetterTitleTypeId, int LetterTypeId, int LetterDivId, int AssignerId
            , int LetRefPriorityId, int LetRefDivId, int LetRefAimId, int LetRefTypeId, int LetRefSenderId)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlDataAdapter adapter = this.Adapter;
            ResetAllParameters();
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.Connection = this.Connection;
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.CommandText = "spSelectAutomationReferenceSettingByLetter";
            adapter.SelectCommand.Parameters.AddWithValue("@RefSetId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSetUserId", RefSetUserId);
            adapter.SelectCommand.Parameters.AddWithValue("@RefSetSId", RefSetSId);
            adapter.SelectCommand.Parameters.AddWithValue("@CartableLetterType", CartableLetterType);
            adapter.SelectCommand.Parameters.AddWithValue("@CreationType", CreationType);
            adapter.SelectCommand.Parameters.AddWithValue("@SId", SId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeTitleType", (int)TableCodes.AutomationLetterTittleType);
            adapter.SelectCommand.Parameters.AddWithValue("@LetterTitleTypeId", LetterTitleTypeId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeLetterType", (int)TableCodes.AutomationLetterType);
            adapter.SelectCommand.Parameters.AddWithValue("@LetterTypeId", LetterTypeId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeLetterDiv", (int)TableCodes.AutomationLetterDivision);
            adapter.SelectCommand.Parameters.AddWithValue("@LetterDivId", LetterDivId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeLetterAssiner", (int)TableCodes.AutomationLetterMainAssigner);
            adapter.SelectCommand.Parameters.AddWithValue("@AssignerId", AssignerId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypePriority", (int)TableCodes.AutomationPriority);
            adapter.SelectCommand.Parameters.AddWithValue("@LetRefPriorityId", LetRefPriorityId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeRefDiv", (int)TableCodes.AutomationRefrenceDivision);
            adapter.SelectCommand.Parameters.AddWithValue("@LetRefDivId", LetRefDivId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeRefAim", (int)TableCodes.AutomationLetterReferenceAims);
            adapter.SelectCommand.Parameters.AddWithValue("@LetRefAimId", LetRefAimId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeRefType", (int)TableCodes.AutomationLetterReferenceTypes);
            adapter.SelectCommand.Parameters.AddWithValue("@LetRefTypeId", LetRefTypeId);

            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeRefSender", (int)TableCodes.AutomationRefrenceSender);
            adapter.SelectCommand.Parameters.AddWithValue("@LetRefSenderId", LetRefSenderId);

            Fill();
        }

        public void SearchRefrenceSettingByUser(int RefSetUserId, int CartableLetterType, int CreationType, int SId
              , int LetterTitleTypeId, int LetterTypeId, int LetterDivId, int AssignerId
             , int LetRefPriorityId, int LetRefDivId, int LetRefAimId, int LetRefTypeId, int LetRefSenderId)
        {
            SearchRefrenceSetting(RefSetUserId, -1, CartableLetterType, CreationType, SId, LetterTitleTypeId, LetterTypeId, LetterDivId, AssignerId, LetRefPriorityId, LetRefDivId, LetRefAimId, LetRefTypeId, LetRefSenderId);
        }

        public void SearchRefrenceSettingBySecreteriate(int RefSetSId, int CartableLetterType, int CreationType, int SId
            , int LetterTitleTypeId, int LetterTypeId, int LetterDivId, int AssignerId
           , int LetRefPriorityId, int LetRefDivId, int LetRefAimId, int LetRefTypeId, int LetRefSenderId)
        {
            SearchRefrenceSetting(-1, RefSetSId, CartableLetterType, CreationType, SId, LetterTitleTypeId, LetterTypeId, LetterDivId, AssignerId, LetRefPriorityId, LetRefDivId, LetRefAimId, LetRefTypeId, LetRefSenderId);
        }
    }
}
