using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
namespace TSP.DataManager
{
    public class MemberRequestManager : BaseObject
    {

        #region Private Manager
        TSP.DataManager.MemberManager MemberManager;
        TSP.DataManager.MemberLicenceManager MemberLicenceManager;
        TSP.DataManager.TransferMemberManager TransferMemberManager;
        TSP.DataManager.MemberRequestManager MemberRequestMng;
        TSP.DataManager.MemberCardsManager MemberCardsManager;
        TSP.DataManager.MemberMarkaziLogManager MemberMarkaziLogManager;
       TSP.DataManager.WorkFlowStateManager WorkFlowStateManager;
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager;
        TSP.DataManager.LoginManager LoginManager;
        TSP.DataManager.TechnicalServices.AccountingManager AccountingFishManager;

        TSP.DataManager.MemberLanguageManager MeLanguageManager;
        TSP.DataManager.MemberActivitySubjectManager MeActivitySubjectManager;
        TSP.DataManager.ProjectJobHistoryManager MeJobHistoryManager;

        TSP.DataManager.TempMemberActivitySubjectManager TempMemberActivitySubjectManager;
        TSP.DataManager.TempMemberLicenceManager TempMemberLicenceManager;
        TSP.DataManager.TempMemberJobHistoryManager TempMemberJobHistoryManager;
        TSP.DataManager.TempMemberLanguageManager TempMemberLanguageManager;
        TSP.DataManager.TempMemberManager TempMemberManager;
        TSP.DataManager.TransactionManager TransManager;
        TSP.DataManager.RequestInActivesManager RequestInActivesManager;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager;
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager;
        #endregion

        #region Constructors
        public MemberRequestManager()
        {

        }

        public MemberRequestManager(TSP.DataManager.TransactionManager TransactionManager, int CurrentUser_AgentId)
        {
            MemberRequestMng = this;
            MemberManager = new TSP.DataManager.MemberManager();
            MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            TransferMemberManager = new TSP.DataManager.TransferMemberManager();
            MemberCardsManager = new TSP.DataManager.MemberCardsManager();
            MemberMarkaziLogManager = new MemberMarkaziLogManager();

            WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowTaskManager = new WorkFlowTaskManager();
            LoginManager = new LoginManager();

            MeLanguageManager = new MemberLanguageManager();
            MeActivitySubjectManager = new MemberActivitySubjectManager();
            MeJobHistoryManager = new ProjectJobHistoryManager();

            AccountingFishManager = new TSP.DataManager.TechnicalServices.AccountingManager();

            TempMemberLicenceManager = new TempMemberLicenceManager();
            TempMemberActivitySubjectManager = new TempMemberActivitySubjectManager();
            TempMemberJobHistoryManager = new TempMemberJobHistoryManager();
            TempMemberLanguageManager = new TempMemberLanguageManager();
            TempMemberManager = new TempMemberManager();
            RequestInActivesManager = new DataManager.RequestInActivesManager();
            DocMemberFileManager = new DataManager.DocMemberFileManager();
            ObserverWorkRequestChangesManager = new TechnicalServices.ObserverWorkRequestChangesManager(TransactionManager);
            TransManager = TransactionManager;
            if (TransactionManager != null)
            {
                TransactionManager.Add(MemberManager);
                TransactionManager.Add(TransferMemberManager);
                TransactionManager.Add(MemberCardsManager);
                TransactionManager.Add(WorkFlowStateManager);
                TransactionManager.Add(WorkFlowTaskManager);
                TransactionManager.Add(LoginManager);
                TransactionManager.Add(AccountingFishManager);
                TransactionManager.Add(TempMemberLicenceManager);
                TransactionManager.Add(TempMemberActivitySubjectManager);
                TransactionManager.Add(TempMemberJobHistoryManager);
                TransactionManager.Add(TempMemberLanguageManager);
                TransactionManager.Add(TempMemberManager);
                TransactionManager.Add(MeLanguageManager);
                TransactionManager.Add(MeActivitySubjectManager);
                TransactionManager.Add(MeJobHistoryManager);
                TransactionManager.Add(MemberLicenceManager);
                TransactionManager.Add(MemberMarkaziLogManager);
                TransactionManager.Add(RequestInActivesManager);
                TransactionManager.Add(DocMemberFileManager);
                TransManager.Add(ObserverWorkRequestChangesManager);
            }
        }


        public MemberRequestManager(TSP.DataManager.TransactionManager TransactionManager)
        {
            WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowTaskManager = new WorkFlowTaskManager();
            MemberManager = new MemberManager();
            TransactionManager.Add(WorkFlowStateManager);
            TransactionManager.Add(WorkFlowTaskManager);
            TransactionManager.Add(MemberManager);
        }
        #endregion

        #region Utility Methods
        private Boolean IsZeroInvoiceCheck()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["IsZeroInvoiceCheck"] == null)
                return true;
            else
                return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsZeroInvoiceCheck"]));
        }

        public static Boolean CreateAccount()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["CreateAccount"] == null)
                return false;
            else
                return (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["CreateAccount"]));
        }

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

        #region WF Methods
        public int UpdateRequestTaskId(int MReId, int TaskId)
        {
            int Per = 0;
            this.FindByCode(MReId);
            if (this.Count <= 0)
                return (int)ErrorRequest.RequestIsConfirmed;
            this[0].BeginEdit();
            this[0]["WfCurrentTaskId"] = TaskId;
            this[0].EndEdit();
            this.Save();
            this.DataTable.AcceptChanges();
            return Per;
        }
        /// <summary>
        /// Check the permission of Request for WF befor selecting SendBackTask DataTable
        /// </summary>
        /// <param name="MReId">TableId in the WF</param>
        /// <param name="CurrentTaskCode"></param>
        /// <returns></returns>
        public int CheckPermissionMemberConfirmingSendBackTask(int MReId, int CurrentTaskCode)
        {
            TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
            TSP.DataManager.MemberManager MemberManager = new MemberManager();
            int Per = 0;
            int MeId = -1;
            int IsMeTemp = -1;
            this.FindByCode(MReId);
            if (this.Count > 0)
            {
                MeId = int.Parse(this[0]["MeId"].ToString());
                if (!Utility.IsDBNullOrNullValue(this[0]["IsMeTemp"]))
                    IsMeTemp = Convert.ToInt32(this[0]["IsMeTemp"]);
                if (this[0]["IsConfirm"].ToString() != "0")
                {
                    return (int)ErrorRequest.RequestIsConfirmed;
                }
            }
            else
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            if (CurrentTaskCode == (int)WorkFlowTask.SaveMemberInfoForConfirming)
            {
                if (IsMeTemp == 0)
                {
                    #region CheckLicence
                    MemberLicenceManager MeLicenceManager = new MemberLicenceManager();
                    bool DefaultValue = false;
                    Boolean HasKarshenasi = false;

                    MeLicenceManager.FindByMeId(MeId);
                    if (MeLicenceManager.Count == 0)
                    {
                        // "ثبت حداقل یک مدرک تحصیلی برای ثبت عضویت الزامی می باشد";
                        Per = (int)TSP.DataManager.ErrorWFNextStep.InsertAtLeastOneMemberLicence;
                    }
                    else
                    {
                        for (int i = 0; i < MeLicenceManager.Count; i++)
                        {
                            if (Convert.ToBoolean(MeLicenceManager[i]["DefaultValue"]))
                                DefaultValue = true;
                            int LicenceCode = Convert.ToInt32(MeLicenceManager[i]["LicenceCode"]);
                            if (LicenceCode == (int)Licence.Karshenasi ||
                                 LicenceCode == (int)Licence.KarshenasiNaPeyvaste || LicenceCode == (int)Licence.MoadeleKarshenasi
                                || LicenceCode == (int)Licence.KarshenasiArshad || LicenceCode == (int)Licence.MoadeleKarshenasiArshad || LicenceCode == (int)Licence.ArshadPeybaste || LicenceCode == (int)Licence.PHDPeyvaste)
                                HasKarshenasi = true;
                        }
                        if (DefaultValue == false)
                        {
                            //lblInstitueWarning.Text = "لطفاً ابتدا مدرک پیش فرض را انتخاب نمایید";
                            Per = (int)TSP.DataManager.ErrorWFNextStep.ChooseDefualtLicence;
                        }
                        if (!HasKarshenasi)
                        {
                            Per = (int)ErrorWFNextStep.KarshenasiLicenceIsNecessaryForMembership;
                        }
                    }
                    #endregion
                }
                else if (IsMeTemp == 1)
                {
                    #region CheckTempMeLicence
                    TempMemberLicenceManager MeLicenceManager = new TempMemberLicenceManager();
                    bool DefaultValue = false;
                    Boolean HasKarshenasi = false;

                    MeLicenceManager.FindByTMeId(MeId);
                    if (MeLicenceManager.Count == 0)
                    {
                        // "ثبت حداقل یک مدرک تحصیلی برای ثبت عضویت الزامی می باشد";
                        Per = (int)TSP.DataManager.ErrorWFNextStep.InsertAtLeastOneMemberLicence;
                    }
                    else
                    {
                        for (int i = 0; i < MeLicenceManager.Count; i++)
                        {
                            if (Convert.ToBoolean(MeLicenceManager[i]["DefaultValue"]))
                                DefaultValue = true;
                            int LicenceCode = Convert.ToInt32(MeLicenceManager[i]["LicenceCode"]);
                            if (LicenceCode == (int)Licence.Karshenasi ||
                                 LicenceCode == (int)Licence.KarshenasiNaPeyvaste || LicenceCode == (int)Licence.MoadeleKarshenasi
                                || LicenceCode == (int)Licence.KarshenasiArshad || LicenceCode == (int)Licence.MoadeleKarshenasiArshad || LicenceCode == (int)Licence.ArshadPeybaste || LicenceCode == (int)Licence.PHDPeyvaste)
                                HasKarshenasi = true;
                        }
                        if (DefaultValue == false)
                        {
                            //lblInstitueWarning.Text = "لطفاً ابتدا مدرک پیش فرض را انتخاب نمایید";
                            Per = (int)TSP.DataManager.ErrorWFNextStep.ChooseDefualtLicence;
                        }
                        if (!HasKarshenasi)
                        {
                            Per = (int)ErrorWFNextStep.KarshenasiLicenceIsNecessaryForMembership;
                        }
                    }
                    #endregion
                }
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Confirming MemberRequest
        /// </summary>
        /// <param name="MReId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfConfirming(int MReId, int CurrentUserAgentId, int CurrentUserId, int CurrentNmcId, ref ArrayList ArrayReturnValue)
        {
            int Per = 0;
            int MeId = -1;
            string ImageUrl = "";
            string SignUrl = "";
            Int16 NoaForMarkazi;
            string ExtraMessage = "";
            ArrayReturnValue.Add(ExtraMessage);
            ArrayReturnValue.Add(ImageUrl);
            ArrayReturnValue.Add(SignUrl);
            this.ClearBeforeFill = false;
            this.FindByCode(MReId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            MeId = int.Parse(this[0]["MeId"].ToString());
            if (Convert.ToBoolean(this[0]["IsMeTemp"]))
            {
                return (int)ErrorWFNextStep.TempMemberCanNotBeConfirmed;
            }

            if (Convert.ToInt32(this[0]["IsCreated"]) != (int)TSP.DataManager.MemberRequestType.Cancel
              && Convert.ToInt32(this[0]["IsCreated"]) != (int)TSP.DataManager.MemberRequestType.Dead)
            {
                #region CheckLicence
                bool DefaultValue = false;
                Boolean HasKarshenasi = false;
                MemberLicenceManager.FindActiveLicence(MeId, 0);
                if (MemberLicenceManager.Count == 0)
                {
                    //"ثبت حداقل یک مدرک تحصیلی برای ثبت عضویت الزامی می باشد";
                    return (int)TSP.DataManager.ErrorWFNextStep.InsertAtLeastOneMemberLicence;
                }
                else
                {
                    for (int i = 0; i < MemberLicenceManager.Count; i++)
                    {
                        if (Convert.ToBoolean(MemberLicenceManager[i]["DefaultValue"]))
                            DefaultValue = true;
                        int LicenceCode = Convert.ToInt32(MemberLicenceManager[i]["LicenceCode"]);
                        if (LicenceCode == (int)Licence.Karshenasi ||
                             LicenceCode == (int)Licence.KarshenasiNaPeyvaste || LicenceCode == (int)Licence.MoadeleKarshenasi
                            || LicenceCode == (int)Licence.KarshenasiArshad || LicenceCode == (int)Licence.MoadeleKarshenasiArshad || LicenceCode == (int)Licence.ArshadPeybaste || LicenceCode == (int)Licence.PHDPeyvaste)
                            HasKarshenasi = true;
                        Boolean OldMemberNeedInquiry = CheckIfOldMemberNeedInquiry(MeId, MReId, Convert.ToInt32(MemberLicenceManager[i]["MReId"]));
                        if (MemberLicenceManager[i]["IsConfirm"].ToString() == "0" || MemberLicenceManager[i]["IsInquiry"].ToString() != "1")
                        {
                            if (OldMemberNeedInquiry)
                            {
                                // "برای ثبت عضویت باید تمامی مدارک تحصیلی تأیید شده باشند";
                                return (int)TSP.DataManager.ErrorWFNextStep.AllMemberLicenceNotConfirmed;
                            }
                        }
                        else if (MemberLicenceManager[i]["IsConfirm"].ToString() == "2")
                            return (int)TSP.DataManager.ErrorWFNextStep.MemberLicenceIsFake;
                    }
                    if (DefaultValue == false)
                    {
                        //"لطفاً ابتدا مدرک پیش فرض را انتخاب نمایید";
                        return (int)TSP.DataManager.ErrorWFNextStep.ChooseDefualtLicence;
                    }
                    if (!HasKarshenasi)
                    {
                        return (int)ErrorWFNextStep.KarshenasiLicenceIsNecessaryForMembership;
                    }
                }
                #endregion
            }

            UpdateMeNo(MeId, TransManager);
            #region InsertConfirm
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 1;//تایید
            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = GetDateOfToday();
            this[0].EndEdit();
            if (this.Save() <= 0)
            {
                return ((int)ErrorWFNextStep.Error);
            }

            #region Find MasterMlId for Update MeManager
            int MasterMlId = -1;
            if (Convert.ToInt32(this[0]["IsCreated"]) != (int)TSP.DataManager.MemberRequestType.Cancel
               && Convert.ToInt32(this[0]["IsCreated"]) != (int)TSP.DataManager.MemberRequestType.Dead)
            {
                MemberLicenceManager.FindMasterLicence(MeId, MReId, 0, 1);
                if (MemberLicenceManager.Count != 1)
                {
                    return ((int)ErrorWFNextStep.Error);
                }
                MasterMlId = Convert.ToInt32(MemberLicenceManager[0]["MlId"]);
            }
            #endregion

            #region SetMemberFromRequest
            MemberManager.FindByCode(MeId);

            bool ChangeAgent = false;
            if (Convert.ToInt32(MemberManager[0]["AgentId"]) != Convert.ToInt32(this[0]["AgentId"]))
                ChangeAgent = true;
            string FileDate = "%";
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
                FileDate = MemberManager[0]["FileDate"].ToString();
            MemberManager[0].BeginEdit();
            if (MasterMlId != -1)
                MemberManager[0]["MasterMlId"] = MasterMlId;            
            MemberManager[0]["NezamKardanConfirmURL"] = this[0] ["NezamKardanConfirmURL"];
            MemberManager[0]["MeNo"] = this[0]["MeNo"];
            MemberManager[0]["FirstName"] = this[0]["FirstName"];
            MemberManager[0]["LastName"] = this[0]["LastName"];
            MemberManager[0]["FirstNameEn"] = this[0]["FirstNameEn"];
            MemberManager[0]["LastNameEn"] = this[0]["LastNameEn"];
            MemberManager[0]["MobileNo"] = this[0]["MobileNo"];
            MemberManager[0]["HomeAdr"] = this[0]["HomeAdr"];
            MemberManager[0]["HomeTel"] = this[0]["HomeTel"];
            MemberManager[0]["HomePO"] = this[0]["HomePO"];
            MemberManager[0]["WorkAdr"] = this[0]["WorkAdr"];
            MemberManager[0]["WorkTel"] = this[0]["WorkTel"];
            MemberManager[0]["FaxNo"] = this[0]["FaxNo"];
            MemberManager[0]["WorkPO"] = this[0]["WorkPO"];
            MemberManager[0]["BankAccNo"] = this[0]["BankAccNo"];
            MemberManager[0]["MarId"] = this[0]["MarId"];
            MemberManager[0]["SoId"] = this[0]["SoId"];
            MemberManager[0]["Website"] = this[0]["Website"];
            MemberManager[0]["Email"] = this[0]["Email"];

            MemberManager[0]["ImageUrl"] = this[0]["ImageUrl"];
            MemberManager[0]["SignUrl"] = this[0]["SignUrl"];

            MemberManager[0]["BirthPlace"] = this[0]["BirthPlace"];
            if (!Utility.IsDBNullOrNullValue(this[0]["CitId"]))
                MemberManager[0]["CitId"] = this[0]["CitId"];
            MemberManager[0]["AgentId"] = this[0]["AgentId"];
            MemberManager[0]["FatherName"] = this[0]["FatherName"];
            MemberManager[0]["BirhtDate"] = this[0]["BirhtDate"];
            MemberManager[0]["BirthPlace"] = this[0]["BirthPlace"];
            MemberManager[0]["IdNo"] = this[0]["IdNo"];
            MemberManager[0]["IssuePlace"] = this[0]["IssuePlace"];
            MemberManager[0]["SSN"] = this[0]["SSN"];
            if (!Utility.IsDBNullOrNullValue(this[0]["SexId"]))
                MemberManager[0]["SexId"] = this[0]["SexId"];
            //MemberManager[0]["ArchitectorCode"] = this[0]["ArchitectorCode"];
            // int MaxLicenceCode = FindMaxLicenceCode(MemberLicenceManager, MeId);
            int TiId = FindMaxLicenceTiId(MemberLicenceManager, MeId);
            //  TiId = (MaxLicenceCode < 7) ? 1 : 2;
            MemberManager[0]["TiId"] = TiId;

            #region Image Comment

            //if (IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]) == false && String.IsNullOrWhiteSpace(MemberManager[0]["ImageUrl"].ToString()) == false)
            //    ArrayReturnValue[1] = MemberManager[0]["ImageUrl"].ToString();

            //if (IsDBNullOrNullValue(MemberManager[0]["SignUrl"]) == false && String.IsNullOrWhiteSpace(MemberManager[0]["SignUrl"].ToString()) == false)
            //    ArrayReturnValue[2] = MemberManager[0]["SignUrl"].ToString();

            //MemberManager[0]["ImageUrl"] = "~/Image/Members/Person/" + MeId.ToString() + Path.GetExtension(this[0]["ImageUrl"].ToString());
            //MemberManager[0]["SignUrl"] = "~/Image/Members/Sign/" + MeId.ToString() + Path.GetExtension(this[0]["SignUrl"].ToString());
            #endregion

            #region Transfer/Return
            if (!IsDBNullOrNullValue(this[0]["IsCreated"]))
            {
                if (Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.TransferToOtherProvince)//انتقالی
                {
                    TransferMemberManager.FindByMemberId(MReId, 2);
                    if (TransferMemberManager.Count > 0)
                    {
                        TransferMemberManager[0].BeginEdit();
                        TransferMemberManager[0]["TransferDate"] = GetDateOfToday();
                        TransferMemberManager[0]["IsConfirmed"] = 1;
                        TransferMemberManager[0]["UserId"] = CurrentUserId;
                        TransferMemberManager[0].EndEdit();
                        TransferMemberManager.Save();
                    }
                    MemberManager[0]["InActive"] = 1;//غیر فعال
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.TransferToOtherProvince;

                }
                else if (Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.ReturnToCurrentProvince)//بازگشت
                {
                    TransferMemberManager.FindByMemberId(MReId, 3);
                    if (TransferMemberManager.Count > 0)
                    {
                        TransferMemberManager[0].BeginEdit();
                        TransferMemberManager[0]["TransferDate"] = GetDateOfToday();
                        TransferMemberManager[0]["IsConfirmed"] = 1;
                        TransferMemberManager[0]["UserId"] = CurrentUserId;
                        TransferMemberManager[0].EndEdit();
                        TransferMemberManager.Save();
                    }
                    MemberManager[0]["InActive"] = 0;//فعال
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Confirmed;//تایید                   
                }
            }
            //*********انتقال از دیگر استان******************//
            Boolean IsTransfer = false;
            TransferMemberManager.FindByMemberId(MReId, 1);
            if (TransferMemberManager.Count > 0)
            {
                IsTransfer = true;
                TransferMemberManager[0].BeginEdit();
                TransferMemberManager[0]["IsConfirmed"] = 1;
                TransferMemberManager[0]["UserId"] = CurrentUserId;
                TransferMemberManager[0].EndEdit();
                TransferMemberManager.Save();
            }

            #endregion

            #endregion
           
            switch (Convert.ToInt32(this[0]["IsCreated"]))
            {
                case (int)TSP.DataManager.MemberRequestType.Create:
                    NoaForMarkazi = (int)TSP.DataManager.NoaForMarkazi.insert;
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Confirmed;
                    break;
                case (int)TSP.DataManager.MemberRequestType.Cancel:
                    NoaForMarkazi = (int)TSP.DataManager.NoaForMarkazi.delete;
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Cancel;//لغو شده
                    break;
                case (int)TSP.DataManager.MemberRequestType.Dead:
                    NoaForMarkazi = (int)TSP.DataManager.NoaForMarkazi.delete;
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Dead;//فوت شده
                    break;
                case (int)TSP.DataManager.MemberRequestType.Fired:
                    NoaForMarkazi = (int)TSP.DataManager.NoaForMarkazi.delete;
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Fired;//اخراج از سازمان
                    break;
                case (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince:
                    NoaForMarkazi = (int)TSP.DataManager.NoaForMarkazi.delete;
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.TransferToOtherProvince;
                    break;
                case (int)TSP.DataManager.MemberRequestType.FakeLicense:
                    NoaForMarkazi = (int)TSP.DataManager.NoaForMarkazi.delete;
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.FakeLicense;
                    break;
                case (int)TSP.DataManager.MemberRequestType.CancelDebtorMember:
                    NoaForMarkazi = (int)TSP.DataManager.NoaForMarkazi.delete;
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.CancelDebtorMember;
                    break;
                default:
                    NoaForMarkazi = (int)TSP.DataManager.NoaForMarkazi.update;
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Confirmed;
                    break;
            }

            MemberManager[0]["UserId"] = CurrentUserId;
            MemberManager[0]["ModifiedDate"] = DateTime.Now;
            MemberManager[0].EndEdit();

            if (MemberManager.Save() <= 0)
            {
                Per = (int)ErrorWFNextStep.Error;
            }

            if (this[0]["IsCreated"].ToString() == "1")
            {
                if (!IsTransfer)
                {
                    #region Automatic Insert ForMemberCard
                    DataRow MeCardRow = MemberCardsManager.NewRow();
                    MeCardRow["MeId"] = MeId;
                    MeCardRow["MeCrdType"] = 0;
                    MeCardRow["CreateDate"] = GetDateOfToday();
                    MeCardRow["IsConfirmed"] = 0;// 1;
                    MeCardRow["IsPrinted"] = 0;
                    MeCardRow["UserId"] = CurrentUserId;
                    MeCardRow["ModifiedDate"] = DateTime.Now;

                    MemberCardsManager.AddRow(MeCardRow);
                    if (MemberCardsManager.Save() > 0)
                    {
                        int TableId = int.Parse(MemberCardsManager[0]["MeCrdId"].ToString());
                        int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, (int)WorkFlowTask.SaveMemberCardInfo, 0, CurrentUserId, (int)WorkFlowStateNmcIdType.System);
                        if (WfStart > 0)
                        {
                            Per = 0;
                        }
                        WorkFlowStateManager.DataTable.AcceptChanges();
                        //if (WorkFlowStateManager.StartWorkFlow(TableId, (int)WorkFlowTask.ConfirmingMemberCardAndEndProccess, 0, CurrentUserId, (int)WorkFlowStateNmcIdType.System) <= 0)
                        //{
                        //    Per = (int)ErrorWFNextStep.Error;
                        //}

                    }
                    else
                    {
                        Per = (int)ErrorWFNextStep.Error;
                    }
                    #endregion
                }
            }

            #region MemberMarkaziLog
            DataRow drMemberMarkaziLog = MemberMarkaziLogManager.NewRow();
            drMemberMarkaziLog["MeId"] = MeId;
            drMemberMarkaziLog["Flag"] = 0;
            drMemberMarkaziLog["CreateDateTime"] = DateTime.Now;
            drMemberMarkaziLog["ModifiedDate"] = DateTime.Now;
            drMemberMarkaziLog["CreateDate"] = Utility.GetDateOfToday();
            drMemberMarkaziLog["CreateTime"] = Utility.GetCurrentTime();
            drMemberMarkaziLog["Noa"] = NoaForMarkazi;
            MemberMarkaziLogManager.AddRow(drMemberMarkaziLog);
            if (MemberMarkaziLogManager.Save() <= 0)
            {
                Per = (int)ErrorWFNextStep.Error;
            }
            
            #endregion


            #endregion

            if (Convert.ToInt32(this[0]["IsCreated"]) == (int)TSP.DataManager.MemberRequestType.Fired
                || Convert.ToInt32(this[0]["IsCreated"]) == (int)TSP.DataManager.MemberRequestType.Dead
                || Convert.ToInt32(this[0]["IsCreated"]) == (int)TSP.DataManager.MemberRequestType.Cancel)
                //if (DocMemberFileManager.DoAutomaticConfirmChangeMemberFile(DocMemberFileManager, WorkFlowStateManager, WorkFlowTaskManager, MeId,
                if (DocMemberFileManager.DoAutomaticConfirmChangeMemberFile(WorkFlowStateManager, WorkFlowTaskManager, MeId,
                        CurrentUserId) == -1)
                {
                    return (int)ErrorRequest.LoseRequestInfo;
                }
            int LastMfId = -1;
            if (!IsDBNullOrNullValue(this[0]["IsCreated"]) &&
                (Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.Dead
                || Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.FakeLicense
                || Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.Fired
                || Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.TransferToOtherProvince
                || ChangeAgent))
            {
                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                if (dtMeFile.Rows.Count > 0)
                    LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                if (LastMfId != -1)
                {
                    if (!IsDBNullOrNullValue(this[0]["IsCreated"]) && Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.Dead)
                        Per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل فوت در عضویت", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست تغییرات عضویت توسط سیستم"
                        , TSObserverWorkRequestChangeType.InActive, CurrentUserId, LastMfId, FileDate, Convert.ToInt32(this[0]["AgentId"].ToString()), TransManager);

                    if (!IsDBNullOrNullValue(this[0]["IsCreated"]) && Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.FakeLicense)
                        Per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل ارائه مدرک تقلبی در عضویت", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست تغییرات عضویت توسط سیستم"
                        , TSObserverWorkRequestChangeType.InActive, CurrentUserId, LastMfId, FileDate, Convert.ToInt32(this[0]["AgentId"].ToString()), TransManager);



                    if (!IsDBNullOrNullValue(this[0]["IsCreated"]) && Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.Fired)
                        Per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل اخراج از سازمان در عضویت", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست تغییرات عضویت توسط سیستم"
                        , TSObserverWorkRequestChangeType.InActive, CurrentUserId, LastMfId, FileDate, Convert.ToInt32(this[0]["AgentId"].ToString()), TransManager);


                    if (!IsDBNullOrNullValue(this[0]["IsCreated"]) && Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.TransferToOtherProvince)
                        Per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل انتقال به دیگر استان در عضویت", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست تغییرات عضویت توسط سیستم"
                        , TSObserverWorkRequestChangeType.InActive, CurrentUserId, LastMfId, FileDate, Convert.ToInt32(this[0]["AgentId"].ToString()), TransManager);

                    if (ChangeAgent)
                        Per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل تغییر نمایندگی در عضویت", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست تغییرات عضویت توسط سیستم"
                       , TSObserverWorkRequestChangeType.AgentChange, CurrentUserId, LastMfId, FileDate, Convert.ToInt32(this[0]["AgentId"].ToString()), TransManager);


                }
            }
            return Per;
        }

        int FindMaxLicenceTiId(MemberLicenceManager MemberLicenceManager, int MeId)
        {
            int Tiid = -1; // TSP.DataManager.MemberLicenceManager MemberLicenceManager = new DataManager.MemberLicenceManager();
            MemberLicenceManager.FindActiveLicence(MeId, 0);
            if (MemberLicenceManager.Count > 0)
            {
                int Max = 0;
                foreach (DataRow row in MemberLicenceManager.DataTable.Rows)
                {
                    if (Max < int.Parse(row["LicenceCode"].ToString()))
                    {
                        Max = int.Parse(row["LicenceCode"].ToString());
                        Tiid = int.Parse(row["Tiid"].ToString());
                    }
                }
                return Tiid;
            }
            else return -1;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting MemberRequest
        /// </summary>
        /// <param name="MReId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfRejecting(int MReId, int CurrentUserId)
        {
            int Per = 0;
            int MeId = -1;
            Boolean IsMeTemp = false;
            this.FindByCode(MReId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            MeId = (int)this[0]["MeId"];
            if (Convert.ToBoolean(this[0]["IsMeTemp"]))
            {
                IsMeTemp = true;
            }

            #region CheckLicence
            bool HasFakeLicense = false;
            if (!IsMeTemp)
            {
                MemberLicenceManager.FindByMeId(MeId);
                for (int i = 0; i < MemberLicenceManager.Count; i++)
                {
                    if (MemberLicenceManager[i]["IsConfirm"].ToString() == "2")
                        HasFakeLicense = true;
                }
            }
            #endregion

            this[0].BeginEdit();
            this[0]["IsConfirm"] = 2;//تایید نشده;

            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = GetDateOfToday();

            this[0].EndEdit();
            if (this.Save() > 0)
            {
                if (this[0]["IsCreated"].ToString() == "1")
                {
                    if (IsMeTemp)
                    {

                        TempMemberManager.FindByCode(MeId);
                        TempMemberManager[0].BeginEdit();
                        TempMemberManager[0]["MsId"] = (int)TemporaryMemberStatus.Canceled;//تایید نشده
                        TempMemberManager[0]["UserId"] = CurrentUserId;
                        TempMemberManager[0].EndEdit();
                        TempMemberManager.Save();
                    }
                    else
                    {
                        MemberManager.FindByCode(MeId);
                        MemberManager[0].BeginEdit();
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.NotConfirmed;//تایید نشده
                        MemberManager[0]["UserId"] = CurrentUserId;
                        MemberManager[0].EndEdit();
                        MemberManager.Save();
                    }
                }
                if (HasFakeLicense)
                {
                    MemberManager.FindByCode(MeId);
                    MemberManager[0].BeginEdit();
                    MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.FakeLicense;//مدرک جعلی
                    MemberManager[0]["UserId"] = CurrentUserId;
                    MemberManager[0].EndEdit();
                    MemberManager.Save();
                }

                RequestInActivesManager.UpdateInActiveRowByRequest(MReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), 1);

                Per = 0;
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }

            return Per;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int DoNextTaskOfMemberLicenceInquiryAndConfirming(int MReId, int CurrentUserId)
        {
            int Per = 0;
            int TMeId = -1;
            int MeId = -1;
            int IsMeTemp = -1;
            string MeNo = "";
            this.FindByCode(MReId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            TMeId = int.Parse(this[0]["MeId"].ToString());
            IsMeTemp = Convert.ToInt32(this[0]["IsMeTemp"]);
            if (IsMeTemp == 0)
                return Per;

            #region CheckTempMeLicence
            // TempMemberLicenceManager MeLicenceManager = new TempMemberLicenceManager();
            bool DefaultValue = false;
            Boolean HasKarshenasi = false;

            TempMemberLicenceManager.FindByTMeId(TMeId);
            if (TempMemberLicenceManager.Count == 0)
            {
                // "ثبت حداقل یک مدرک تحصیلی برای ثبت عضویت الزامی می باشد";
                return (int)TSP.DataManager.ErrorWFNextStep.InsertAtLeastOneMemberLicence;
            }
            else
            {
                for (int i = 0; i < TempMemberLicenceManager.Count; i++)
                {
                    if (Convert.ToBoolean(TempMemberLicenceManager[i]["DefaultValue"]))
                        DefaultValue = true;
                    int LicenceCode = Convert.ToInt32(TempMemberLicenceManager[i]["LicenceCode"]);
                    if (LicenceCode == (int)Licence.Karshenasi ||
                         LicenceCode == (int)Licence.KarshenasiNaPeyvaste || LicenceCode == (int)Licence.MoadeleKarshenasi
                        || LicenceCode == (int)Licence.KarshenasiArshad || LicenceCode == (int)Licence.MoadeleKarshenasiArshad || LicenceCode == (int)Licence.ArshadPeybaste || LicenceCode == (int)Licence.PHDPeyvaste)
                        HasKarshenasi = true;

                }
                if (DefaultValue == false)
                {
                    //lblInstitueWarning.Text = "لطفاً ابتدا مدرک پیش فرض را انتخاب نمایید";
                    return (int)TSP.DataManager.ErrorWFNextStep.ChooseDefualtLicence;
                }
                if (!HasKarshenasi)
                {
                    return (int)ErrorWFNextStep.KarshenasiLicenceIsNecessaryForMembership;
                }
            }
            #endregion

            TempMemberManager.FindByCode(TMeId);
            if (TempMemberManager.Count != 1)
                return (int)ErrorRequest.LoseRequestInfo;

            if (InsertMember(TempMemberManager, MemberManager) == 0)
                return (int)ErrorRequest.Error;
            MeId = Convert.ToInt32(MemberManager[0]["MeId"]);

            if (!InsertMeActivity(TMeId, MReId, MeId, MeActivitySubjectManager, TempMemberActivitySubjectManager))
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);

            if (!InsertMeJobhistory(TMeId, MReId, MeId, MeJobHistoryManager, TempMemberJobHistoryManager))
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);

            if (!InsertMeLanguage(TMeId, MReId, MeId, MeLanguageManager, TempMemberLanguageManager))
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);

            if (!InsertMemberLicence(TMeId, MReId, MeId, MemberLicenceManager, TempMemberLicenceManager, CurrentUserId))
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);


            TSP.DataManager.MemberManager.UpdateMeNo(MeId, TransManager);

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 0)
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);
            MeNo = MemberManager[0]["MeNo"].ToString();

            #region Update RequestManager
            this[0].BeginEdit();
            this[0]["MeId"] = MeId;
            this[0]["MeNo"] = MeNo;
            this[0]["IsMeTemp"] = 0;
            this[0].EndEdit();
            if (this.Save() == 0)
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);
            #endregion

            #region InActived TempMember
            TempMemberManager.FindByCode(TMeId);
            if (TempMemberManager.Count == 0)
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);
            TempMemberManager[0].BeginEdit();
            TempMemberManager[0]["InActive"] = 1;
            TempMemberManager[0].EndEdit();
            if (TempMemberManager.Save() == 0)
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);
            #endregion

            #region tblLogin-UserName
            LoginManager.FindByMeIdUltId(TMeId, (int)UserType.TemporaryMembers);
            if (LoginManager.Count == 0)
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);
            LoginManager[0].BeginEdit();
            LoginManager[0]["UserName"] = MeId;
            LoginManager[0]["UltId"] = (int)UserType.Member;
            LoginManager[0]["MeId"] = MeId;
            LoginManager[0]["UserId2"] = CurrentUserId;
            LoginManager[0]["ModifiedDate"] = DateTime.Now;
            LoginManager[0].EndEdit();
            if (LoginManager.Save() == 0)
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);
            #endregion

            if (!UpdateMeAccountingFish(TMeId, MeId, AccountingFishManager))
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);

            if (!UpdateWFStateTempMe(MReId, MeId, WorkFlowStateManager))
                return ((int)TSP.DataManager.ErrorWFNextStep.Error);

            return Per;
        }

        private int InsertMember(TSP.DataManager.TempMemberManager TempMeManager, TSP.DataManager.MemberManager MeManager)
        {
            DataRow drMember = MeManager.NewRow();
            drMember["MeId"] = 0;
            drMember["MeNo"] = "";            
            drMember["NezamKardanConfirmURL"] = TempMeManager[0]["NezamKardanConfirmURL"];
            drMember["TMeId"] = TempMeManager[0]["TMeId"];
            drMember["FirstName"] = TempMeManager[0]["FirstName"];
            drMember["LastName"] = TempMeManager[0]["LastName"];
            drMember["FirstNameEn"] = TempMeManager[0]["FirstNameEn"];
            drMember["LastNameEn"] = TempMeManager[0]["LastNameEn"];
            drMember["TiId"] = TempMeManager[0]["TiId"];
            drMember["FatherName"] = TempMeManager[0]["FatherName"];
            drMember["BirhtDate"] = TempMeManager[0]["BirhtDate"];
            drMember["BirthPlace"] = TempMeManager[0]["BirthPlace"];
            drMember["IdNo"] = TempMeManager[0]["IdNo"];
            drMember["IssuePlace"] = TempMeManager[0]["IssuePlace"];
            drMember["SSN"] = TempMeManager[0]["SSN"];
            drMember["MobileNo"] = TempMeManager[0]["MobileNo"];
            drMember["HomeAdr"] = TempMeManager[0]["HomeAdr"];
            drMember["HomeTel"] = TempMeManager[0]["HomeTel"];
            drMember["HomePO"] = TempMeManager[0]["HomePO"];
            drMember["WorkAdr"] = TempMeManager[0]["WorkAdr"];
            drMember["WorkTel"] = TempMeManager[0]["WorkTel"];
            drMember["FaxNo"] = TempMeManager[0]["FaxNo"];
            drMember["WorkPO"] = TempMeManager[0]["WorkPO"];
            drMember["BankAccNo"] = TempMeManager[0]["BankAccNo"];
            drMember["MsId"] = 1;
            // drMember["IsCreated"] = (int)MemberRequestType.Create;
            drMember["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Pending;// در جریان
            drMember["SexId"] = TempMeManager[0]["SexId"];
            drMember["SoId"] = TempMeManager[0]["SoId"];
            drMember["MarId"] = TempMeManager[0]["MarId"];
            drMember["AgentId"] = TempMeManager[0]["AgentId"];
            drMember["FileNo"] = TempMeManager[0]["FileNo"];
            drMember["RelId"] = TempMeManager[0]["RelId"];
            drMember["ComId"] = TempMeManager[0]["ComId"];
            drMember["AtId"] = TempMeManager[0]["AtId"];

            drMember["CitId"] = TempMeManager[0]["CitId"];
            drMember["Nationality"] = TempMeManager[0]["Nationality"];
            drMember["Website"] = TempMeManager[0]["Website"];
            drMember["Email"] = TempMeManager[0]["Email"];
            drMember["CreateDate"] = TempMeManager[0]["CreateDate"];
            drMember["Description"] = TempMeManager[0]["Description"];
            drMember["UserId"] = TempMeManager[0]["UserId"];
            drMember["ModifiedDate"] = DateTime.Now;
            drMember["InActive"] = 0;
            drMember["MembershipDate"] = Utility.GetDateOfToday();

            MeManager.AddRow(drMember);
            return (MeManager.Save());
        }

        private Boolean InsertMemberLicence(int TMeId, int MReId, int MeId, TSP.DataManager.MemberLicenceManager MeLicenceManager, TSP.DataManager.TempMemberLicenceManager TempMeLicenceManager, int CurrentUserId)
        {
            TempMeLicenceManager.FindByRequest(TMeId, MReId);
            for (int i = 0; i < TempMeLicenceManager.Count; i++)
            {
                int TMLId = Convert.ToInt32(TempMeLicenceManager[i]["TMlId"]);
                DataRow dr = MeLicenceManager.NewRow();
                dr["LiId"] = TempMeLicenceManager[i]["LiId"];
                dr["CounId"] = TempMeLicenceManager[i]["CounId"];
                dr["MeId"] = MeId;
                dr["MjId"] = TempMeLicenceManager[i]["MjId"];
                dr["UnId"] = TempMeLicenceManager[i]["UnId"];
                dr["UnName"] = TempMeLicenceManager[i]["UnName"];
                dr["CitName"] = TempMeLicenceManager[i]["CitName"];
                dr["CitId"] = TempMeLicenceManager[i]["CitId"];
                dr["Avg"] = TempMeLicenceManager[i]["Avg"];
                dr["NumUnit"] = TempMeLicenceManager[i]["NumUnit"];
                dr["StartDate"] = TempMeLicenceManager[i]["StartDate"];
                dr["EndDate"] = TempMeLicenceManager[i]["EndDate"];
                dr["IsConfirm"] = TempMeLicenceManager[i]["IsConfirm"];
                dr["IsInquiry"] = TempMeLicenceManager[i]["IsInquiry"];
                dr["UserId"] = TempMeLicenceManager[i]["UserId"];
                dr["Thesis"] = TempMeLicenceManager[i]["Thesis"];
                dr["DefaultValue"] = TempMeLicenceManager[i]["DefaultValue"];
                dr["Description"] = TempMeLicenceManager[i]["Description"];
                if (!Utility.IsDBNullOrNullValue(TempMeLicenceManager[i]["ImageURL"]))
                {
                    dr["ImageURL"] = TempMeLicenceManager[i]["ImageURL"];
                }
                dr["ModifiedDate"] = DateTime.Now;
                dr["MReId"] = MReId;
                MeLicenceManager.AddRow(dr);
                if (MeLicenceManager.Save() == 0)
                    return false;
                MeLicenceManager.AcceptChanges();
                int MLId = Convert.ToInt32(MeLicenceManager[MeLicenceManager.Count - 1]["MlId"]);
                //AttManager.FindByTablePrimaryKey_AttId(TableTypeManager.FindTtId(TableType.MemberLicence), TMLId, (int)AttachType.MemberLicense);
                //if (AttachmentsManager.Count > 0)
                //{
                //    AttachmentsManager[0].BeginEdit();
                //    AttachmentsManager[0]["RefTable"] = MLId;
                //    AttachmentsManager[0].EndEdit();
                //    AttachmentsManager.Save();
                //    AttachmentsManager.AcceptChanges();
                //}
                //if (!Utility.IsDBNullOrNullValue(TempMeLicenceManager[i]["ImageURL"]))
                //{
                //DataRow drAtt = AttManager.NewRow();
                //    drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberLicence;
                //    drAtt["RefTable"] = MLId;
                //    drAtt["AttId"] = (int)TSP.DataManager.AttachType.MemberLicense;
                //    drAtt["IsValid"] = 1;
                //    drAtt["FilePath"] = TempMeLicenceManager[i]["ImageURL"];
                //    drAtt["FileName"] = "";
                //    drAtt["Description"] = DBNull.Value;
                //    drAtt["UserId"] = CurrentUserId;
                //    drAtt["ModfiedDate"] = DateTime.Now;
                //    AttManager.AddRow(drAtt);
                //    AttManager.Save();
                //    AttManager.AcceptChanges();
                //}
            }
            return true;
        }

        private Boolean InsertMeActivity(int TMeId, int MReId, int MeId, TSP.DataManager.MemberActivitySubjectManager MeActivitySubjectManager, TSP.DataManager.TempMemberActivitySubjectManager TempMeActivitySubjectManager)
        {
            TempMeActivitySubjectManager.FindByRequest(TMeId, MReId);
            for (int i = 0; i < TempMeActivitySubjectManager.Count; i++)
            {
                DataRow dr = MeActivitySubjectManager.NewRow();
                dr["AsId"] = TempMeActivitySubjectManager[i]["AsId"];
                dr["MeId"] = MeId;
                dr["UserId"] = TempMeActivitySubjectManager[i]["UserId"];
                dr["AsPercent"] = TempMeActivitySubjectManager[i]["AsPercent"];
                dr["Description"] = TempMeActivitySubjectManager[i]["Description"];
                dr["ModifiedDate"] = DateTime.Now;
                dr["MReId"] = MReId;

                MeActivitySubjectManager.AddRow(dr);
                if (MeActivitySubjectManager.Save() == 0)
                    return false;
                MeActivitySubjectManager.AcceptChanges();
            }
            return true;
        }

        private Boolean InsertMeJobhistory(int TMeId, int MReId, int MeId, TSP.DataManager.ProjectJobHistoryManager MeHistoryManager, TSP.DataManager.TempMemberJobHistoryManager TempMeJobHistoryManager)
        {
            TempMeJobHistoryManager.FindByRequest(TMeId, MReId);
            for (int i = 0; i < TempMeJobHistoryManager.Count; i++)
            {
                DataRow drJob = MeHistoryManager.NewRow();
                drJob["MeId"] = MeId;
                drJob["RoeId"] = 1;//ثبت عضویت

                drJob["PrTypeId"] = TempMeJobHistoryManager[i]["PrTypeId"];
                drJob["SazeTypeId"] = TempMeJobHistoryManager[i]["SazeTypeId"];
                drJob["ProjectName"] = TempMeJobHistoryManager[i]["ProjectName"];
                drJob["Employer"] = TempMeJobHistoryManager[i]["Employer"];
                drJob["CitName"] = TempMeJobHistoryManager[i]["CitName"];
                drJob["CounId"] = TempMeJobHistoryManager[i]["CounId"];
                drJob["PJPId"] = TempMeJobHistoryManager[i]["PJPId"];
                drJob["StartOriginalDate"] = TempMeJobHistoryManager[i]["StartOriginalDate"];
                drJob["StartCorporateDate"] = TempMeJobHistoryManager[i]["StartCorporateDate"];
                drJob["StatusOfStartDate"] = TempMeJobHistoryManager[i]["StatusOfStartDate"];
                drJob["EndCorporateDate"] = TempMeJobHistoryManager[i]["EndCorporateDate"];
                drJob["StatusOfEndDate"] = TempMeJobHistoryManager[i]["StatusOfEndDate"];
                drJob["ProjectVolume"] = TempMeJobHistoryManager[i]["ProjectVolume"];
                drJob["Area"] = TempMeJobHistoryManager[i]["Area"];
                drJob["Floors"] = TempMeJobHistoryManager[i]["Floors"];
                drJob["CorTypeId"] = TempMeJobHistoryManager[i]["CorTypeId"];
                drJob["ConfirmedByNezam"] = TempMeJobHistoryManager[i]["ConfirmedByNezam"];
                drJob["Description"] = TempMeJobHistoryManager[i]["Description"];
                drJob["UserId"] = TempMeJobHistoryManager[i]["UserId"];
                drJob["ModifiedDate"] = DateTime.Now;
                drJob["TableId"] = TempMeJobHistoryManager[i]["TableId"];
                drJob["TableType"] = TempMeJobHistoryManager[i]["TableType"];
                drJob["CreateDate"] = TempMeJobHistoryManager[i]["CreateDate"];
                drJob["Type"] = TempMeJobHistoryManager[i]["Type"];
                MeHistoryManager.AddRow(drJob);
                if (MeHistoryManager.Save() == 0)
                    return false;
                MeHistoryManager.AcceptChanges();
            }
            return true;
        }

        private Boolean InsertMeLanguage(int TMeId, int MReId, int MeId, TSP.DataManager.MemberLanguageManager MeLanguageManager, TSP.DataManager.TempMemberLanguageManager TempMeLanguageManager)
        {
            TempMeLanguageManager.FindByRequest(TMeId, MReId);
            for (int i = 0; i < TempMeLanguageManager.Count; i++)
            {
                DataRow dr = MeLanguageManager.NewRow();
                dr["LanId"] = TempMeLanguageManager[i]["LanId"];
                dr["MeId"] = MeId;
                dr["LqId"] = TempMeLanguageManager[i]["LqId"];
                dr["UserId"] = TempMeLanguageManager[i]["UserId"];
                dr["Description"] = TempMeLanguageManager[i]["Description"];
                dr["ModifiedDate"] = DateTime.Now;
                dr["MReId"] = TempMeLanguageManager[i]["MReId"];

                MeLanguageManager.AddRow(dr);
                if (MeLanguageManager.Save() == 0)
                    return false;
                MeLanguageManager.AcceptChanges();
            }
            return true;
        }

        private Boolean UpdateMeAccountingFish(int TMeId, int MeId, TSP.DataManager.TechnicalServices.AccountingManager AccMeFish)
        {
            int tblTypeTempMe = -1;
            int tblTypeMe = -1;
            TableTypeManager tblTypeManager = new TableTypeManager();

            tblTypeManager.FindByTtCode(TSP.DataManager.TableType.TempMember);
            if (tblTypeManager.Count == 0)
                return false;
            tblTypeTempMe = Convert.ToInt32(tblTypeManager[0]["TtId"]);

            tblTypeManager.FindByTtCode(TSP.DataManager.TableType.Member);
            if (tblTypeManager.Count == 0)
                return false;
            tblTypeMe = Convert.ToInt32(tblTypeManager[0]["TtId"]);

            AccMeFish.FindByTableTypeId(TMeId, tblTypeTempMe);
            for (int i = 0; i < AccMeFish.Count; i++)
            {
                AccMeFish[i].BeginEdit();
                AccMeFish[i]["TableType"] = tblTypeMe;
                AccMeFish[i]["TableTypeId"] = MeId;
                AccMeFish[i].EndEdit();
                if (AccMeFish.Save() == 0)
                    return false;
                AccMeFish.AcceptChanges();
            }
            return true;
        }

        private Boolean UpdateWFStateTempMe(int MReId, int MeId, TSP.DataManager.WorkFlowStateManager WFStateManager)
        {
            DataTable dtWfState = WFStateManager.SelectByWorkFlowCode((int)WorkFlows.MemberConfirming, MReId);
            DataRow[] dr = dtWfState.Select("NmcIdType=" + ((int)WorkFlowStateNmcIdType.TempMember));
            for (int i = 0; i < dr.Length; i++)
            {
                int StateId = Convert.ToInt32(dr[i]["StateId"]);
                WFStateManager.FindByCode(StateId);
                if (WFStateManager.Count == 0)
                    return false;
                WFStateManager[0].BeginEdit();
                WFStateManager[0]["NmcId"] = MeId;
                WFStateManager[0]["NmcIdType"] = (int)WorkFlowStateNmcIdType.MeId;
                WFStateManager[0].EndEdit();
                if (WFStateManager.Save() == 0)
                    return false;
            }
            return true;
        }
        //****************

        /// <summary>
        /// Check the permission of Request for WF befor selecting SendBackTask DataTable
        /// </summary>
        /// <param name="MReId">TableId in the WF</param>
        /// <param name="CurrentTaskCode"></param>
        /// <returns></returns>
        public int CheckPermissionMemberTansferConfirmingSendBackTask(int MReId, int CurrentTaskCode)
        {
            TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
            TSP.DataManager.MemberManager MemberManager = new MemberManager();
            int Per = 0;
            int MeId = -1;

            this.FindByCode(MReId);
            if (this.Count > 0)
            {
                MeId = int.Parse(this[0]["MeId"].ToString());
                if (this[0]["IsConfirm"].ToString() != "0")
                {
                    return (int)ErrorRequest.RequestIsConfirmed;
                }
            }
            else
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Confirming MemberRequest
        /// </summary>
        /// <param name="MReId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMemberTransferConfirming(int MReId, int CurrentUserAgentId, int CurrentUserId, int CurrentNmcId)
        {
            int Per = 0;
            int MeId = -1;

            #region PreCondition
            this.FindByCode(MReId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            MeId = int.Parse(this[0]["MeId"].ToString());

            #endregion

            #region InsertConfirm

            this[0].BeginEdit();
            this[0]["IsConfirm"] = 1;//تایید
            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = GetDateOfToday();
            this[0].EndEdit();

            if (this.Save() > 0)
            {
                MemberManager.FindByCode(MeId);

                #region SetMemberFromRequest

                MemberManager[0].BeginEdit();
                MemberManager[0]["MeNo"] = this[0]["MeNo"];
                MemberManager[0]["FirstName"] = this[0]["FirstName"];
                MemberManager[0]["LastName"] = this[0]["LastName"];
                MemberManager[0]["FirstNameEn"] = this[0]["FirstNameEn"];
                MemberManager[0]["LastNameEn"] = this[0]["LastNameEn"];
                MemberManager[0]["MobileNo"] = this[0]["MobileNo"];
                MemberManager[0]["HomeAdr"] = this[0]["HomeAdr"];
                MemberManager[0]["HomeTel"] = this[0]["HomeTel"];
                MemberManager[0]["HomePO"] = this[0]["HomePO"];
                MemberManager[0]["WorkAdr"] = this[0]["WorkAdr"];
                MemberManager[0]["WorkTel"] = this[0]["WorkTel"];
                MemberManager[0]["FaxNo"] = this[0]["FaxNo"];
                MemberManager[0]["WorkPO"] = this[0]["WorkPO"];
                MemberManager[0]["BankAccNo"] = this[0]["BankAccNo"];
                MemberManager[0]["MarId"] = this[0]["MarId"];
                MemberManager[0]["SoId"] = this[0]["SoId"];
                MemberManager[0]["Website"] = this[0]["Website"];
                MemberManager[0]["Email"] = this[0]["Email"];
                //MemberManager[0]["ImageUrl"] = this[0]["ImageUrl"];
                MemberManager[0]["CitId"] = this[0]["CitId"];
                MemberManager[0]["AgentId"] = this[0]["AgentId"];
                MemberManager[0]["FatherName"] = this[0]["FatherName"];
                MemberManager[0]["BirhtDate"] = this[0]["BirhtDate"];
                MemberManager[0]["BirthPlace"] = this[0]["BirthPlace"];
                MemberManager[0]["IdNo"] = this[0]["IdNo"];
                MemberManager[0]["IssuePlace"] = this[0]["IssuePlace"];
                MemberManager[0]["SSN"] = this[0]["SSN"];
                MemberManager[0]["SexId"] = this[0]["SexId"];


                #region Transfer/Return
                if (!IsDBNullOrNullValue(this[0]["IsCreated"]))
                {
                    if (Convert.ToInt32(this[0]["IsCreated"]) == (int)MemberRequestType.TransferToOtherProvince)//انتقالی
                    {
                        TransferMemberManager.FindByMemberId(MReId, (int)TransferMemberType.GoToOtherProvince);//2
                        if (TransferMemberManager.Count > 0)
                        {
                            TransferMemberManager[0].BeginEdit();
                            TransferMemberManager[0]["TransferDate"] = GetDateOfToday();
                            TransferMemberManager[0]["IsConfirmed"] = 1;
                            TransferMemberManager[0]["UserId"] = CurrentUserId;
                            TransferMemberManager[0].EndEdit();
                            TransferMemberManager.Save();
                        }
                        MemberManager[0]["InActive"] = 1;//غیر فعال
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.TransferToOtherProvince;
                    }

                }
                //*********انتقال از دیگر استان******************//
                TransferMemberManager.FindByMemberId(MReId, 1);
                if (TransferMemberManager.Count > 0)
                {
                    TransferMemberManager[0].BeginEdit();
                    TransferMemberManager[0]["IsConfirmed"] = 1;
                    TransferMemberManager[0]["UserId"] = CurrentUserId;
                    TransferMemberManager[0].EndEdit();
                    TransferMemberManager.Save();
                }

                #endregion

                #endregion

                MemberManager[0]["UserId"] = CurrentUserId;
                MemberManager[0]["ModifiedDate"] = DateTime.Now;
                MemberManager[0].EndEdit();

                if (MemberManager.Save() <= 0)
                {
                    Per = (int)ErrorWFNextStep.Error;
                }
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }
            #endregion

            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting MemberRequest
        /// </summary>
        /// <param name="MReId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMemberTransferRejecting(int MReId, int CurrentUserId)
        {
            int Per = 0;
            int MeId = -1;

            this.FindByCode(MReId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            MeId = (int)this[0]["MeId"];

            this[0].BeginEdit();
            this[0]["IsConfirm"] = 2;//تایید نشده;

            this[0]["UserId"] = CurrentUserId;
            this[0]["AnswerDate"] = GetDateOfToday();

            this[0].EndEdit();
            if (this.Save() > 0)
            {
                Per = 0;
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }

            return Per;
        }

        #region DoAutomaticConfirmChangeMemberData

        /// <summary>
        /// فراخوانی شده در صفحه Members_ChangeMemberData 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="Email"></param>
        /// <param name="MobileNo"></param>
        /// <returns></returns>
        public int DoAutomaticConfirmChangeMemberData(int MeId, int CurrentUserId, string Email, string MobileNo)
        {
            int CurrentMeReqId = -2;
            return DoAutomaticConfirmChangeMemberData(MeId, MemberRequestType.Request, MembershipRequest.Member, "آغاز گردش کار اتوماتیک سیستم جهت تغییر تغییر اطلاعات شخص حقیقی", "تایید و پایان بررسی تغییر اطلاعات شخص حقیقی توسط سیستم", CurrentUserId,"", "",Email,MobileNo, "", MemberManager, TransferMemberManager, "",true, ref CurrentMeReqId,WorkFlowTask.ConfirmMemberAndEndProccess);      
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="AgentId"></param>
        /// <param name="MemberManager"></param>
        /// <param name="TransferMemberManager"></param>
        /// <param name="HomeAddress"></param>
        /// <param name="WorkFlowTask"></param>
        /// <returns></returns>
        public int DoAutomaticConfirmChangeMemberData(int MeId, int CurrentUserId, string AgentId, TSP.DataManager.MemberManager MemberManager, TSP.DataManager.TransferMemberManager TransferMemberManager, string HomeAddress, WorkFlowTask WorkFlowTask)
        {
            int CurrentMeReqId = -2;
            return DoAutomaticConfirmChangeMemberData(MeId, MemberRequestType.AgentChange, MembershipRequest.Employee, "آغاز گردش کار اتوماتیک سیستم جهت تغییر دفتر نمایندگی شخص حقیقی", WorkFlowTask == WorkFlowTask.ConfirmMemberAndEndProccess ? "تایید و پایان بررسی تغییر دفتر نمایندگی شخص حقیقی توسط سیستم" : "ارسال اتوماتیک توسط سیستم", CurrentUserId, AgentId, "", "", "", "", MemberManager, TransferMemberManager, HomeAddress, WorkFlowTask == WorkFlowTask.ConfirmMemberAndEndProccess ? true : false, ref CurrentMeReqId, WorkFlowTask);
        }

        public int DoAutomaticConfirmChangeMemberData(int MeId, int CurrentUserId, string AgentId, TSP.DataManager.MemberManager MemberManager, TSP.DataManager.TransferMemberManager TransferMemberManager)
        {
            return DoAutomaticConfirmChangeMemberData(MeId, MemberRequestType.AgentChange, MembershipRequest.Employee, "آغاز گردش کار اتوماتیک سیستم جهت تغییر دفتر نمایندگی شخص حقیقی", "تایید و پایان بررسی تغییر دفتر نمایندگی شخص حقیقی توسط سیستم", CurrentUserId, AgentId, "", "", "", "", MemberManager, TransferMemberManager, "");
        }
        public int DoAutomaticConfirmChangeMemberData(int MeId, int CurrentUserId, DataManager.MemberRequestType MemberRequestType, string ChangedValue, TSP.DataManager.MemberManager MemberManager, TSP.DataManager.TransferMemberManager TransferMemberManager)
        {
            return DoAutomaticConfirmChangeMemberData(MeId, MemberRequestType, MembershipRequest.Employee, "آغاز گردش کار اتوماتیک سیستم جهت تغییر شماره حساب شخص حقیقی", "تایید و پایان بررسی تغییر شماره حساب شخص حقیقی توسط سیستم", CurrentUserId, "", "", "", "", ChangedValue, MemberManager, TransferMemberManager, "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="MemberRequestType"></param>
        /// <param name="MembershipRequester"></param>
        /// <param name="DescriptionWFStart"></param>
        /// <param name="DescriptionWFConfirm"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="AgentId"></param>
        /// <param name="ArchitectorCode"></param>
        /// <param name="Email"></param>
        /// <param name="MobileNo"></param>
        /// <param name="BankAccNo"></param>
        /// <param name="MemberManager"></param>
        /// <param name="transferManager"></param>
        /// <param name="HomeAddress"></param>
        /// <returns></returns>
        public int DoAutomaticConfirmChangeMemberData(int MeId, TSP.DataManager.MemberRequestType MemberRequestType, MembershipRequest MembershipRequester, string DescriptionWFStart, string DescriptionWFConfirm, int CurrentUserId, string AgentId, string ArchitectorCode, string Email, string MobileNo, string BankAccNo, TSP.DataManager.MemberManager MemberManager, TSP.DataManager.TransferMemberManager transferManager, string HomeAddress)
        {
            int CurrentMeReqId = -2;
            return DoAutomaticConfirmChangeMemberData(MeId, MemberRequestType, MembershipRequester, DescriptionWFStart, DescriptionWFConfirm, CurrentUserId, AgentId, ArchitectorCode, Email, MobileNo, BankAccNo, MemberManager, transferManager, HomeAddress, true, ref CurrentMeReqId);
        }
        /////////////////////
        public int DoAutomaticConfirmChangeMemberData(int MeId, TSP.DataManager.MemberRequestType MemberRequestType, MembershipRequest MembershipRequester, string DescriptionWFStart, string DescriptionWFConfirm, int CurrentUserId
            , TSP.DataManager.MemberManager MemberManager, TSP.DataManager.TransferMemberManager transferManager, Boolean ConfirmeWf, ref int CurrentMeReqId)
        {
            return DoAutomaticConfirmChangeMemberData(MeId, MemberRequestType, MembershipRequester, DescriptionWFStart, DescriptionWFConfirm, CurrentUserId, "", "", "", "", "", MemberManager, transferManager, "", ConfirmeWf, ref CurrentMeReqId);
        }
        public int DoAutomaticConfirmChangeMemberData(int MeId, TSP.DataManager.MemberRequestType MemberRequestType, MembershipRequest MembershipRequester, string DescriptionWFStart, string DescriptionWFConfirm, int CurrentUserId, string AgentId, string ArchitectorCode, string Email, string MobileNo, string BankAccNo, TSP.DataManager.MemberManager MemberManager, TSP.DataManager.TransferMemberManager transferManager, string HomeAddress, Boolean ConfirmeWf, ref int CurrentMeReqId)
        {
            return DoAutomaticConfirmChangeMemberData(MeId, MemberRequestType, MembershipRequester, DescriptionWFStart, DescriptionWFConfirm, CurrentUserId, AgentId, ArchitectorCode, Email, MobileNo, BankAccNo, MemberManager, transferManager, HomeAddress, ConfirmeWf, ref CurrentMeReqId, WorkFlowTask.ConfirmMemberAndEndProccess);
        }
        public int DoAutomaticConfirmChangeMemberData(int MeId, TSP.DataManager.MemberRequestType MemberRequestType, MembershipRequest MembershipRequester, string DescriptionWFStart, string DescriptionWFConfirm, int CurrentUserId, string AgentId, string ArchitectorCode, string Email, string MobileNo, string BankAccNo, TSP.DataManager.MemberManager MemberManager, TSP.DataManager.TransferMemberManager transferManager, string HomeAddress, Boolean ConfirmeWf, ref int CurrentMeReqId, WorkFlowTask WorkFlowTaskSecondStep)
        {
            #region InserMeRequest
            this.FindLastReqByMemberId(MeId, 0, 1);
            if (this.Count <= 0) return (int)ErrorWFNextStep.Error;
            int PreMReId = Convert.ToInt32(this[0]["MReId"]);
            this.DataTable.Clear();
            DataRow dr = this.NewRow();
            dr["MeId"] = MeId;
            if (AgentId == "")
                dr["AgentId"] = MemberManager[0]["AgentId"];
            else
                dr["AgentId"] = AgentId;

            dr["FirstName"] = MemberManager[0]["FirstName"].ToString();
            dr["LastName"] = MemberManager[0]["LastName"].ToString();
            dr["FirstNameEn"] = MemberManager[0]["FirstNameEn"].ToString();
            dr["LastNameEn"] = MemberManager[0]["LastNameEn"].ToString();
            if (string.IsNullOrEmpty(MobileNo))
                dr["MobileNo"] = MemberManager[0]["MobileNo"].ToString();
            else
                dr["MobileNo"] = MobileNo;
            if (string.IsNullOrEmpty(Email))
                dr["Email"] = MemberManager[0]["Email"].ToString();
            else
                dr["Email"] = Email;
            if (string.IsNullOrEmpty(BankAccNo))
                dr["BankAccNo"] = MemberManager[0]["BankAccNo"].ToString();
            else
                dr["BankAccNo"] = BankAccNo;
            if (string.IsNullOrEmpty(HomeAddress))
                dr["HomeAdr"] = MemberManager[0]["HomeAdr"].ToString();
            else
                dr["HomeAdr"] = HomeAddress;
            dr["HomeTel"] = MemberManager[0]["HomeTel"].ToString();
            dr["HomePO"] = MemberManager[0]["HomePO"].ToString();
            dr["WorkAdr"] = MemberManager[0]["WorkAdr"].ToString();
            dr["WorkTel"] = MemberManager[0]["WorkTel"].ToString();
            dr["FaxNo"] = MemberManager[0]["FaxNo"].ToString();
            dr["WorkPO"] = MemberManager[0]["WorkPO"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MarId"]))
                dr["MarId"] = MemberManager[0]["MarId"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SoId"]))
                dr["SoId"] = MemberManager[0]["SoId"].ToString();
            dr["Website"] = MemberManager[0]["Website"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
                dr["ImageUrl"] = MemberManager[0]["ImageUrl"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SignUrl"]))
                dr["SignUrl"] = MemberManager[0]["SignUrl"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SignUrl"]))
                dr["CitId"] = MemberManager[0]["CitId"].ToString();
            dr["FatherName"] = MemberManager[0]["FatherName"].ToString();
            dr["BirhtDate"] = MemberManager[0]["BirhtDate"].ToString();
            dr["BirthPlace"] = MemberManager[0]["BirthPlace"].ToString();
            dr["IdNo"] = MemberManager[0]["IdNo"].ToString();
            dr["IssuePlace"] = MemberManager[0]["IssuePlace"].ToString();
            dr["SSN"] = MemberManager[0]["SSN"].ToString();
            dr["MeNo"] = MemberManager[0]["MeNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SexId"]))
                dr["SexId"] = MemberManager[0]["SexId"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MsId"]))
                dr["MsId"] = MemberManager[0]["MsId"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ComId"]))
                dr["ComId"] = MemberManager[0]["ComId"].ToString();
            dr["CreateDate"] = GetDateOfToday();
            dr["AnswerDate"] = GetDateOfToday();

            dr["IsCreated"] = (int)MemberRequestType;
            dr["InActive"] = false;
            dr["Requester"] = (int)MembershipRequester;
            dr["UserId"] = CurrentUserId;
            dr["ModifiedDate"] = DateTime.Now;
            if (ConfirmeWf)
            {
                dr["IsConfirm"] = 1;
            }
            else
            {
                dr["IsConfirm"] = 0;
            }
            int SecondStepTaskId = -2;
            WorkFlowTaskManager.FindByTaskCode((int)WorkFlowTaskSecondStep);
            SecondStepTaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
            dr["WFCurrentTaskId"] = SecondStepTaskId;
            this.AddRow(dr);
            if (this.Save() <= 0)
                return (int)ErrorWFNextStep.Error;
            int CurrentMreId = CurrentMeReqId = Convert.ToInt32(this[0]["MReId"]);
            #endregion
            #region InsertTransfer//اگر در درخواست قبل اطلاعات انتقالی داشته است بایستی مجددا در این درخواست نیز ثبت شود

            transferManager.FindByMemberId(PreMReId, -1);
            if (transferManager.Count > 0)
            {
                DataRow drTransfer = transferManager.NewRow();
                drTransfer["PrId"] = transferManager[0]["PrId"].ToString();
                drTransfer["TransferDate"] = transferManager[0]["TransferDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(transferManager[0]["TransferType"]))
                    drTransfer["TransferType"] = transferManager[0]["TransferType"];
                drTransfer["TableId"] = CurrentMreId;
                drTransfer["TtId"] = transferManager[0]["TtId"];
                if (!Utility.IsDBNullOrNullValue(transferManager[0]["Body"]))
                    drTransfer["Body"] = transferManager[0]["Body"].ToString();
                drTransfer["IsConfirmed"] = 0;
                drTransfer["MeNo"] = transferManager[0]["MeNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(transferManager[0]["FileNo"]))
                    drTransfer["FileNo"] = transferManager[0]["FileNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(transferManager[0]["DocPrId"]))
                    drTransfer["DocPrId"] = transferManager[0]["DocPrId"].ToString();

                if (!Utility.IsDBNullOrNullValue(transferManager[0]["FirstDocRegDate"]))
                    drTransfer["FirstDocRegDate"] = transferManager[0]["FirstDocRegDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(transferManager[0]["CurrentDocRegDate"]))
                    drTransfer["CurrentDocRegDate"] = transferManager[0]["CurrentDocRegDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(transferManager[0]["CurrentDocExpDate"]))
                    drTransfer["CurrentDocExpDate"] = transferManager[0]["CurrentDocExpDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(transferManager[0]["ImageUrl"]))
                    drTransfer["ImageUrl"] = transferManager[0]["ImageUrl"].ToString();
                drTransfer["UserId"] = CurrentUserId;
                drTransfer["ModifiedDate"] = DateTime.Now;

                transferManager.AddRow(drTransfer);
                if (transferManager.Save() <= 0)
                    return -1;

            }
            #endregion        
            WorkFlowTaskManager.FindByTaskCode((int)WorkFlowTask.SaveMemberInfoForConfirming);
            WorkFlowStateManager.ClearBeforeFill = false;
            int TaskIdSaveMemberInfo = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
            int NmcId = -2;
            int WfNmcIdType = -2;
            if (MembershipRequester == MembershipRequest.Member)
            {
                NmcId = MeId;
                WfNmcIdType = (int)WorkFlowStateNmcIdType.MeId;
            }
            else if (MembershipRequester == MembershipRequest.Employee)
            {
                NmcId = FindNmcId(TaskIdSaveMemberInfo, CurrentUserId);
                WfNmcIdType = (int)WorkFlowStateNmcIdType.NmcId;
            }
            if (WorkFlowStateManager.InsertWorkFlowState((int)TableCodes.MemberRequest, CurrentMreId, TaskIdSaveMemberInfo, DescriptionWFStart, NmcId, WfNmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) > 0)
            {
                WorkFlowStateManager.DataTable.AcceptChanges();
                if (ConfirmeWf || WorkFlowTaskSecondStep == WorkFlowTask.ConfirmMemberAndEndProccess)
                {
                    return ConfirmedAutomaticRequest(MeId, CurrentMreId, MemberRequestType, SecondStepTaskId, DescriptionWFConfirm, NmcId, WfNmcIdType, CurrentUserId
                                 , AgentId, ArchitectorCode, Email, MobileNo, BankAccNo, HomeAddress, MemberManager, this);
                }
                else
                {
                    if (WorkFlowStateManager.InsertWorkFlowState((int)TableCodes.MemberRequest, CurrentMreId, SecondStepTaskId, DescriptionWFConfirm, NmcId, WfNmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) > 0)
                        return 0;

                }
            }
            return (int)ErrorWFNextStep.Error;
        }
        #endregion
        public int ConfirmedAutomaticRequest(int MeId, int CurrentMreId, TSP.DataManager.MemberRequestType MemberRequestType, int ConfirmedTaskId, string DescriptionWFConfirm, int NmcId, int WfNmcIdType, int CurrentUserId
            , string AgentId, string ArchitectorCode, string Email, string MobileNo, string BankAccNo, string HomeAddress, TSP.DataManager.MemberManager MemberManager, TSP.DataManager.MemberRequestManager MemberRequestManager)
        {
            if (WorkFlowStateManager.InsertWorkFlowState((int)TableCodes.MemberRequest, CurrentMreId, ConfirmedTaskId, DescriptionWFConfirm, NmcId, WfNmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) > 0)
            {
                MemberRequestManager.FindByCode(CurrentMreId);
                if (MemberRequestManager.Count == 0)
                    return (int)ErrorWFNextStep.Error;
                MemberRequestManager[0].BeginEdit();
                MemberRequestManager[0]["WFCurrentTaskId"] = ConfirmedTaskId;
                MemberRequestManager[0]["IsConfirm"] = 1;
                MemberRequestManager[0].EndEdit();
                if (MemberRequestManager.Save() <= 0)
                    return (int)ErrorWFNextStep.Error;

                MemberManager.FindByCode(MeId);
                string FileDate = MemberManager[0]["FileDate"].ToString();
                MemberManager[0].BeginEdit();

                switch ((int)MemberRequestType)
                {
                    case (int)MemberRequestType.AgentChange:
                        break;
                    case (int)TSP.DataManager.MemberRequestType.Cancel:
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Cancel;//لغو شده
                        break;
                    case (int)TSP.DataManager.MemberRequestType.Dead:
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Dead;//فوت شده
                        break;
                    case (int)TSP.DataManager.MemberRequestType.Fired:
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Fired;//اخراج از سازمان
                        break;
                    case (int)TSP.DataManager.MemberRequestType.TransferToOtherProvince:
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.TransferToOtherProvince;
                        break;
                    case (int)TSP.DataManager.MemberRequestType.FakeLicense:
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.FakeLicense;
                        break;
                    case (int)TSP.DataManager.MemberRequestType.CancelDebtorMember:
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.CancelDebtorMember;
                        break;
                    case (int)TSP.DataManager.MemberRequestType.ActivateDebtorMember:
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Confirmed;
                        break;
                    default:
                        MemberManager[0]["MrsId"] = (int)MembershipRegistrationStatus.Confirmed;
                        break;
                }
                if (!string.IsNullOrEmpty(BankAccNo) || !string.IsNullOrEmpty(AgentId))
                {
                    if (!string.IsNullOrEmpty(BankAccNo))
                        MemberManager[0]["BankAccNo"] = BankAccNo;
                    if (!string.IsNullOrEmpty(AgentId))
                        MemberManager[0]["AgentId"] = AgentId;
                }
                if (!string.IsNullOrEmpty(HomeAddress))
                    MemberManager[0]["HomeAdr"] = HomeAddress;
                MemberManager[0]["UserId"] = CurrentUserId;
                MemberManager[0]["ModifiedDate"] = DateTime.Now;
                MemberManager[0].EndEdit();
                if (MemberManager.Save() <= 0)
                    return (int)ErrorWFNextStep.Error;                
                return 0;
            }
            return (int)ErrorWFNextStep.Error;
        }

        public int ConfirmedAutomaticRequest(int MeId, int CurrentMreId, TSP.DataManager.MemberRequestType MemberRequestType, int ConfirmedTaskId, string DescriptionWFConfirm, int NmcId, int WfNmcIdType, int CurrentUserId, TSP.DataManager.MemberManager MemberManager, TSP.DataManager.MemberRequestManager MemberRequestManager)
        {
            return ConfirmedAutomaticRequest(MeId, CurrentMreId, MemberRequestType, ConfirmedTaskId, DescriptionWFConfirm, NmcId
                , WfNmcIdType, CurrentUserId, "", "", "", "", "", "", MemberManager, MemberRequestManager);
        }
        #endregion

        protected Boolean CheckIfOldMemberNeedInquiry(int MeId, int CurrentMReId, int LicenceMReId)
        {
            if (!CheckIsLicenceBelongToFirstRequest(LicenceMReId))
                return true;
            TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
            TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
            TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();

            MemberActivitySubjectManager.FindForDelete(MeId, CurrentMReId);
            if (MemberActivitySubjectManager.Count > 0)
            {
                return true;
            }
            MemberLanguageManager.FindForDelete(MeId, CurrentMReId);
            if (MemberLanguageManager.Count > 0)
            {
                return true;
            }

            MemberLicenceManager.FindForDelete(MeId, CurrentMReId);
            if (MemberLicenceManager.Count > 0)
            {
                return true;
            }
            ////ProjectJobHistoryManager.FindForDelete(0, CurrentMReId, (int)TSP.DataManager.TableCodes.MemberRequest);
            ////if (ProjectJobHistoryManager.Count > 0)
            ////{
            ////    return true;
            ////}
            ////AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, CurrentMReId, (short)TSP.DataManager.AttachType.Attachments);
            ////if (AttachmentsManager.Count > 0)
            ////{
            ////    return true;
            ////}
            return false;
        }

        private Boolean CheckIsLicenceBelongToFirstRequest(int LicenceMReId)
        {
            //   TSP.DataManager.MemberRequestManager MeReqManager = new TSP.DataManager.MemberRequestManager();
            this.FindByCode(LicenceMReId);
            if (this.Count != 2)
            {
                return false;
            }
            if (Convert.ToBoolean(this[this.Count - 1]["IsCreated"]))
            {
                return true;
            }
            return false;
            //TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
            //FindByCode(LicenceMReId);
            //if (this.Count != 1)
            //{
            //    return false;
            //}
            //if (Convert.ToBoolean(this[0]["IsCreated"]))
            //{
            //    return true;
            //}
            //return false;
        }

        private int FindNmcId(int TaskId, int CurrentUserId)
        {
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            int NmcId = -1;
            NmcId = NezamChartManager.FindNmcId(CurrentUserId, TaskId, LoginManager);
            if (NmcId > 0)
            {
                return NmcId;
            }
            else
                return (-1);
        }     

        #region EPayment
        public Boolean DoNextTaskOfBankReply(int TableId, int NmcId, string WFMessageURL, int UltId, int UserId, TransactionManager Trans)
        {
            int TableType = TableTypeManager.FindTtId(DataManager.TableType.MemberRequest);
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            int WorkflowCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
            if (WorkFlowTaskManager == null)
            {
                WorkFlowTaskManager = new DataManager.WorkFlowTaskManager();
                Trans.Add(WorkFlowTaskManager);
            }
            DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, SaveInfoTaskCode, WorkflowCode);
            if (dtNextTopTask.Rows.Count <= 0)
                return false;
            int NextStepTaskCode = int.Parse(dtNextTopTask.Rows[0]["TaskCode"].ToString());
            WorkFlowTaskManager.FindByTaskCode(NextStepTaskCode);
            if (WorkFlowTaskManager.Count != 1)
                return false;
            int NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            int NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.TempMember;
            string MsgContent = "";
            if (WorkFlowStateManager == null)
            {
                WorkFlowStateManager = new DataManager.WorkFlowStateManager();
                Trans.Add(WorkFlowStateManager);
            }
            Int64 SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, TableId, NextStepTaskId, "ارسال درخواست بررسی پرونده عضویت به واحد عضویت توسط عضو", NmcId, NmcIdType, UserId, MsgContent, WFMessageURL);
            return true;
        }
        #endregion

        #region AccountingMethods
        private int GetParentAccId(TSP.DataManager.AccountingSettingsManager SettingsManager, int CurrentUserAgentId)
        {
            SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembersCurrentAccount.ToString(), CurrentUserAgentId, "Accounting");
            if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
                return Convert.ToInt32(SettingsManager[0]["SValue"]);
            return -1;
        }

        //private string GetAccName(DataRow Member)
        //{
        //    string Name = Member["LastName"].ToString() + " " + Member["FirstName"].ToString();
        //    return Name;
        //}

        private int GetMembershipEarningsAccId(TSP.DataManager.AccountingSettingsManager SettingsManager, int CurrentUserAgentId)
        {
            SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MembershipEarnings.ToString(), CurrentUserAgentId, "Accounting");
            if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
                return Convert.ToInt32(SettingsManager[0]["SValue"]);
            return -1;
        }

        private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager, int CurrentUserAgentId)
        {
            SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), CurrentUserAgentId, "Accounting");
            if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
                return Convert.ToInt32(SettingsManager[0]["SValue"]);
            return -1;
        }

        private decimal GetFirstMembershipCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager, int CurrentUserAgentId)
        {
            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString(), CurrentUserAgentId);
            if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
                return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
            return -1;
        }

        private string GetDes1(DataRow Member, decimal Amount)
        {
            string Des = "جهت حق عضویت جدید آقای/خانم " + Member["FirstName"].ToString() + " " + Member["LastName"].ToString() + " " + "به مبلغ" + " " + Amount.ToString("#,#") + " در تاریخ " + GetDateOfToday();
            return Des;
        }
        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberRequest);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "tblMemberRequest";
            tableMapping.ColumnMappings.Add("MReId", "MReId");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("FirstName", "FirstName");
            tableMapping.ColumnMappings.Add("LastName", "LastName");
            tableMapping.ColumnMappings.Add("FirstNameEn", "FirstNameEn");
            tableMapping.ColumnMappings.Add("LastNameEn", "LastNameEn");
            tableMapping.ColumnMappings.Add("MobileNo", "MobileNo");
            tableMapping.ColumnMappings.Add("HomeAdr", "HomeAdr");
            tableMapping.ColumnMappings.Add("HomeTel", "HomeTel");
            tableMapping.ColumnMappings.Add("HomePO", "HomePO");
            tableMapping.ColumnMappings.Add("WorkAdr", "WorkAdr");
            tableMapping.ColumnMappings.Add("WorkTel", "WorkTel");
            tableMapping.ColumnMappings.Add("WorkPO", "WorkPO");
            tableMapping.ColumnMappings.Add("FaxNo", "FaxNo");
            tableMapping.ColumnMappings.Add("BankAccNo", "BankAccNo");
            tableMapping.ColumnMappings.Add("SoId", "SoId");
            tableMapping.ColumnMappings.Add("MarId", "MarId");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("SexId", "SexId");
            tableMapping.ColumnMappings.Add("WebSite", "WebSite");
            tableMapping.ColumnMappings.Add("Email", "Email");
            tableMapping.ColumnMappings.Add("FatherName", "FatherName");
            tableMapping.ColumnMappings.Add("BirhtDate", "BirhtDate");
            tableMapping.ColumnMappings.Add("BirthPlace", "BirthPlace");
            tableMapping.ColumnMappings.Add("IdNo", "IdNo");
            tableMapping.ColumnMappings.Add("IssuePlace", "IssuePlace");
            tableMapping.ColumnMappings.Add("SSN", "SSN");
            tableMapping.ColumnMappings.Add("ImageUrl", "ImageUrl");
            tableMapping.ColumnMappings.Add("SignUrl", "SignUrl");
            tableMapping.ColumnMappings.Add("FollowCode", "FollowCode");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("RequestDesc", "RequestDesc");
            tableMapping.ColumnMappings.Add("AnswerDesc", "AnswerDesc");
            tableMapping.ColumnMappings.Add("AnswerDate", "AnswerDate");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("IsCreated", "IsCreated");
            tableMapping.ColumnMappings.Add("MsId", "MsId");
            tableMapping.ColumnMappings.Add("ComId", "ComId");
            tableMapping.ColumnMappings.Add("LetterNo", "LetterNo");
            tableMapping.ColumnMappings.Add("LetterDate", "LetterDate");
            tableMapping.ColumnMappings.Add("FilePath", "FilePath");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("Requester", "Requester");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("MeNo", "MeNo");
            tableMapping.ColumnMappings.Add("IsMeTemp", "IsMeTemp");
            tableMapping.ColumnMappings.Add("ArchitectorCode", "ArchitectorCode");
            tableMapping.ColumnMappings.Add("WfCurrentTaskId", "WfCurrentTaskId");
            tableMapping.ColumnMappings.Add("MilitaryCommitment", "MilitaryCommitment");
            tableMapping.ColumnMappings.Add("NezamKardanConfirmURL", "NezamKardanConfirmURL");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectMemberRequest";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@MReId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@IsCreated", SqlDbType.SmallInt);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsMeTemp", SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = (int)TableCodes.MemberRequest;

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteMemberRequest";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertMemberRequest";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMeTemp", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMeTemp", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MeNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomeAdr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "HomeAdr", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomeTel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "HomeTel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomePO", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "HomePO", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkAdr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkAdr", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkTel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkTel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkPO", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkPO", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FaxNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FaxNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BankAccNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BankAccNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SoId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SoId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MarId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MarId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SexId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SexId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSite", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WebSite", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FatherName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FatherName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirhtDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "BirhtDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IdNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IssuePlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IssuePlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SSN", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImageUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImageUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDesc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RequestDesc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AnswerDesc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AnswerDesc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AnswerDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsCreated", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsCreated", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ComId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FilePath", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FilePath", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Requester", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "Requester", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArchitectorCode", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ArchitectorCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WfCurrentTaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "WfCurrentTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MilitaryCommitment", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "MilitaryCommitment", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NezamKardanConfirmURL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "NezamKardanConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateMemberRequest";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsMeTemp", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "IsMeTemp", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MeNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FirstNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastNameEn", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LastNameEn", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MobileNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MobileNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomeAdr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "HomeAdr", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomeTel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "HomeTel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HomePO", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "HomePO", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkAdr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkAdr", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkTel", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkTel", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkPO", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "WorkPO", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FaxNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FaxNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BankAccNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BankAccNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SoId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SoId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MarId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MarId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CitId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CitId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "AgentId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SexId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "SexId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSite", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "WebSite", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Email", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FatherName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FatherName", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirhtDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "BirhtDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BirthPlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "BirthPlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdNo", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IdNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IssuePlace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "IssuePlace", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SSN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "SSN", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImageUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImageUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SignUrl", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "SignUrl", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequestDesc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "RequestDesc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AnswerDesc", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AnswerDesc", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AnswerDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "AnswerDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsCreated", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsCreated", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MsId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MsId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ComId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ComId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LetterDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "LetterDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FilePath", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FilePath", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Requester", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "Requester", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MReId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsNull_LastTimeStamp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MReId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ArchitectorCode", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ArchitectorCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WfCurrentTaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "WfCurrentTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MilitaryCommitment", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "MilitaryCommitment", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NezamKardanConfirmURL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "NezamKardanConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));

        }

        public override DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.MemberDataSet.tblMemberRequestDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        #region Select Methods
        public void FindByCode(int MReId)
        {
            this.ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@MReId"].Value = MReId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = (int)TableCodes.MemberRequest;
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByMemberId(int MeId, Int16 IsConfirm, Int16 IsCreated)
        {
            this.ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            this.Adapter.SelectCommand.Parameters["@IsCreated"].Value = IsCreated;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = (int)TableCodes.MemberRequest;

            Fill();
            return this.DataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="IsMeTemp">0:find from tblMember,1:find from tblTempMember</param>
        /// <param name="IsConfirm"></param>
        /// <param name="IsCreated"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByMemberId(int MeId, int IsMeTemp, Int16 IsConfirm, Int16 IsCreated)
        {
            this.ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            this.Adapter.SelectCommand.Parameters["@IsCreated"].Value = IsCreated;
            this.Adapter.SelectCommand.Parameters["@IsMeTemp"].Value = IsMeTemp;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TableType.MemberRequest);

            Fill();
            return this.DataTable;
        }

        public void FindLastReqByMemberId(int MeId, int IsMeTemp, Int16 IsConfirm)
        {
            this.ResetAllParameters();

            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@IsConfirm"].Value = IsConfirm;
            this.Adapter.SelectCommand.Parameters["@IsMeTemp"].Value = IsMeTemp;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TableType.MemberRequest);

            Fill();
        }

        public void DeleteRequest(int MReId, int MeId)
        {
            SqlCommand cmd = new SqlCommand("spDeleteMemberRequestsForMembersInfo", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@MReId", MReId);
                cmd.Parameters.AddWithValue("@MeId", MeId);
                cmd.Parameters.AddWithValue("@TableType", (int)TableCodes.MemberRequest);
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


        public void RejectGroupofTempMember(string ListOfMReId, int NmcId, int UserId, int TaskId)
        {
            SqlCommand cmd = new SqlCommand("RejectGroupOfTempMemmber", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@ListOfMReId", ListOfMReId);
                cmd.Parameters.AddWithValue("@userId", UserId);
                cmd.Parameters.AddWithValue("@DateOfToday", Utility.GetDateOfToday());
                cmd.Parameters.AddWithValue("@TaskId", TaskId);
                cmd.Parameters.AddWithValue("@NmcIdType", (int)WorkFlowStateNmcIdType.NmcId);
                cmd.Parameters.AddWithValue("@NmcId", NmcId);

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
        public static void UpdateMeNo(int MReId, TransactionManager trans)
        {
            SqlConnection scon = null;
            SqlCommand scom = new SqlCommand("CreateMemberRequestMeNo", scon);
            scom.CommandType = CommandType.StoredProcedure;
            if (trans != null)
            {
                scom.Transaction = trans.Transaction;
                scon = trans.Transaction.Connection;

            }
            else
                scon = new SqlConnection(DBManager.CnnStr);
            scom.Connection = scon;
            scom.Parameters.Add("@MReId", SqlDbType.Int);
            scom.Parameters[0].Value = MReId;
            if (scon.State != ConnectionState.Open)
                scon.Open();
            scom.ExecuteNonQuery();
            if (trans == null)
                scon.Close();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public System.Data.DataTable SelectMemberRequestForManagmentPage(int MeId, int IsMeTemp)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectMemberRequestForManagmentPage", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsMeTemp", IsMeTemp);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.MemberRequest));//(int)TableCodes.MemberRequest

            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.Fill(dt);//this.DataTable);
            return dt;// (this.DataTable);
        }

        public DataTable SelectMemberRequestFromMember(int IsMeTemp)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("spSelectMemberRequestCountFromMember", this.Connection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@TaskCodeEmployeeCheck", (int)WorkFlowTask.MembershipUnitConfirmingMember);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.MemberConfirming);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@IsMeTemp", IsMeTemp);
            sqlAdapter.Fill(dt);
            return dt;
        }
        public DataTable SelectMemberRequestForMeUserControl(int MReId)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("SelectMemberRequestForMeUserControl", this.Connection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MReId", MReId);
            sqlAdapter.Fill(dt);
            return dt;
        }
        
        public DataTable SelectMemberRequestFromMember()
        {
            return SelectMemberRequestFromMember(-1);

        }
        #endregion

        public static int InsertMemberRequest(TransactionManager TransactionManager, int MeId, int IsMeTemp, string MeNo, string FirstName, string LastName
        , string FirstNameEn, string LastNameEn, string MobileNo, string HomeAdr, string HomeTel, string HomePO, string WorkAdr, string WorkTel, string WorkPO, string FaxNo, string BankAccNo, int SoId, int MarId, int CitId, int AgentId, int SexId, string WebSite, string Email, string FatherName, string BirhtDate,
            string BirthPlace, string IdNo, string IssuePlace, string SSN, string ImageUrl, string SignUrl, string FollowCode, string CreateDate, string RequestDesc, string AnswerDesc, string AnswerDate, int IsConfirm, int IsCreated, int MsId, int ComId, string LetterNo, string LetterDate, string FilePath
           , string Description, int InActive, int Requester, int UserId, string ArchitectorCode, int WFCurrentTaskId)
        {
            MemberRequestManager MemberRequestManager = new DataManager.MemberRequestManager();
            if (TransactionManager != null)
                TransactionManager.Add(MemberRequestManager);
            // ArrayList Result = new ArrayList();

            #region Insert MemberRequest
            DataRow dr = MemberRequestManager.NewRow();

            //if (comboRequestType.SelectedItem != null && comboRequestType.SelectedItem.Value.ToString() != "0")
            //{
            //    String Message;
            //    if (!CheckCondition(MeId, out Message))
            //    {
            //        ShowMessage(Message);
            //    }
            //    else
            //        InsertTransfer(MeId);
            //    return;
            //}

            dr["MeId"] = MeId;
            dr["MeNo"] = MeNo;
            dr["FirstName"] = FirstName;
            dr["LastName"] = LastName;
            dr["FirstNameEn"] = FirstNameEn;
            dr["LastNameEn"] = LastNameEn;
            dr["MobileNo"] = MobileNo;
            dr["HomeAdr"] = HomeAdr;
            dr["ArchitectorCode"] = ArchitectorCode;
            dr["HomeTel"] = HomeTel;
            dr["HomePO"] = HomePO;
            dr["WorkAdr"] = WorkAdr;
            dr["WorkTel"] = WorkTel;
            dr["FaxNo"] = FaxNo;
            dr["WorkPO"] = WorkPO;
            dr["SexId"] = SexId;
            dr["MarId"] = MarId;
            dr["SoId"] = SoId;
            dr["CitId"] = CitId;

            dr["AgentId"] = AgentId;

            dr["Website"] = WebSite;
            dr["Email"] = Email;
            dr["RequestDesc"] = RequestDesc;
            dr["IsConfirm"] = IsConfirm;// (int)TSP.DataManager.MemberConfirmType.Pending;//معلق
            dr["MsId"] = MsId;// (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed;//تایید شده
            //dr["IsCreated"] = MsId;
            dr["BankAccNo"] = BankAccNo;
            dr["FatherName"] = FatherName;
            dr["BirhtDate"] = BirhtDate;
            dr["BirthPlace"] = BirthPlace;
            dr["IdNo"] = IdNo;
            dr["IssuePlace"] = IssuePlace;
            dr["SSN"] = SSN;


            dr["ComId"] = ComId;
            dr["ComId"] = 0;

            dr["MsId"] = 1;
            dr["UserId"] = UserId;
            dr["ModifiedDate"] = DateTime.Now;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["Requester"] = Requester;
            dr["ImageUrl"] = ImageUrl;
            dr["SignUrl"] = SignUrl;
            dr["FollowCode"] = FollowCode;
            dr["WFCurrentTaskId"] = WFCurrentTaskId;
            MemberRequestManager.AddRow(dr);
            if (MemberRequestManager.Save() != 1)
                return -1;
            #endregion

            MemberRequestManager.DataTable.AcceptChanges();
            return int.Parse(MemberRequestManager[0]["MReId"].ToString());
        }
    }
}
