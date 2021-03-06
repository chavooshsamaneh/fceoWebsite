using System;
using System.Collections;
using System.Data;

public partial class Members_MemberHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
            {
                // panelMeDebt.Visible = false;
                panelMeDebt.Visible = true;
                txtMeDebt.InnerHtml = "پرداخت بدهی آنلاین" + "<br/> <b>" + TSP.DataManager.Utility.CheckMemberOfflineDebt(Utility.GetCurrentUser_MeId()) + "ریال";
            }
            else
            {
                panelMeDebt.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            txtMeDebt.InnerHtml = "خطا در ارتباط با وب سرویس پرداخت بدهی";
        }
        try
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
            {

                panelMeLoan.Visible = false;
                panelMeLoan.Visible = true;
                txtMeLoan.InnerHtml = "پرداخت آنلاین اقساط وام" + "<br/> <b>" + TSP.DataManager.Utility.CheckMemberLoanDebt(Utility.GetCurrentUser_MeId()) + "ریال";
            }
            else
            {
                panelMeLoan.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            txtMeLoan.InnerHtml = "خطا در ارتباط با وب سرویس پرداخت وام";
        }

        ////////if (Utility.GetCurrentUser_MeId() == 13 || Utility.GetCurrentUser_MeId() == 5773 || Utility.GetCurrentUser_MeId() == 20146 || Utility.GetCurrentUser_MeId() == 4442 || Utility.GetCurrentUser_MeId() == 18737)
        ////////{
        ////////    btnRevival.Visible = LinkButtonRevival.Visible = true;
        ////////}
        ////////else
        ////////{
        ////////    btnRevival.Visible = LinkButtonRevival.Visible = false;
        ////////}
        if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())//Utility.GetCurrentUser_MeId() == 756
            btnQueueListMunRequest.Visible = true;
        else
            btnQueueListMunRequest.Visible = false;
        SetDocumentRequestMenueVisibility();

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {

            HiddenHelp["LetterReport"] = "../../ReportForms/TaxConfirmLetterReport.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
            SetHelpAddress();

            switch (Utility.GetCurrentUser_LoginType())
            {
                case (int)TSP.DataManager.UserType.TemporaryMembers:
                    #region TemporaryMembers
                    RoundPanelDocumentRequest.Visible = RoundPanelDocumentRequest2.Visible = PanelTechnicalService.Visible = false;
                    TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
                    TempMemberManager.FindByCode(Utility.GetCurrentUser_MeId());
                    if (TempMemberManager.Count > 0)
                    {
                        DateTime dt = new DateTime();
                        dt = DateTime.Now;
                        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
                        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));


                        int Year = int.Parse(TempMemberManager[0]["CreateDate"].ToString().Substring(0, 4));
                        int Month = int.Parse(TempMemberManager[0]["CreateDate"].ToString().Substring(5, 2));
                        int Day = int.Parse(TempMemberManager[0]["CreateDate"].ToString().Substring(8, 2));
                        DateTime dt2 = pDate.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
                        dt2 = dt2.AddDays(double.Parse(Utility.GetMembershipRegTimeout().ToString()));
                        TimeSpan ts = dt2.Subtract(dt);
                        double d = ts.TotalDays;

                        d = Math.Ceiling(d);

                        if (TempMemberManager[0]["MsId"].ToString() == ((int)TSP.DataManager.TemporaryMemberStatus.Pending).ToString())
                        {
                            if (((int)d) > 0)
                            {
                                ShowError("شما حداکثر  " + (int)d + " " + "روز جهت تحویل مدارک خود به دفتر عضویت زمان دارید");
                            }
                            else if (((int)d) == 0)
                            {
                                ShowError("شما حداکثر تا پایان امروز جهت تحویل مدارک خود به دفتر عضویت زمان دارید");

                            }

                        }
                    }
                    #endregion
                    break;
                case (int)TSP.DataManager.UserType.Member:
                    #region Member
                    TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                    MeManager.FindByCode(Utility.GetCurrentUser_MeId());
                    if ((bool)MeManager[0]["IsLock"] == true)
                    {
                        TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                        string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                        return;
                    }

                    if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                    {
                        PanelTechnicalService.Visible = Utility.IsWorkRequestMainAgent();
                        if (Utility.GettestMemberId() == Utility.GetCurrentUser_MeId())
                        {
                            PanelTechnicalService.Visible = true;
                        }
                    }
                    else
                    {
                        PanelTechnicalService.Visible = Utility.IsWorkRequestOtheAgentOpen();
                    }


                    #endregion
                    break;
            }


            if (Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                SetMeFileRequestLabel();
                SetMeImplementerRequestLabel();
                SetMeObservationRequestLabel();
            }
            else
            {

                HiddenHelp["DocMemberVisiblety"] = false;
            }

        }
        btnQualification.Visible = !Convert.ToBoolean(HiddenHelp["DocMemberVisiblety"]);
        btnNewDoc.Visible = Convert.ToBoolean(HiddenHelp["DocMemberVisiblety"]);
        btnRevival.Visible = false;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindDocumentByRequestType(Utility.GetCurrentUser_MeId(), 0, -1, -1, -1);
        if (DocMemberFileManager.Count > 0)
        {
            btnRevival.Visible = true;
        }
    }

    protected void btnChangeBaseInfo_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            ShowMessage(MeReqResult[1].ToString());
            return;
        }
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
        {
            ShowMessage("این لینک جهت استفاده اعضای تایید شده سازمان جهت تغییر اطلاعات پایه خود می باشند.در صورتی که قصد تغییر اطلاعات خود را دارید از منوی ''واحد عضویت>>مدیریت درخواست ها'' استفاده وارد صفحه مربوطه شده و دکمه ویرایش را کلیک نمایید");
            return;
        }
        int MeId = Utility.GetCurrentUser_MeId();
        if (!CheckLocker(MeId)) return;

        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        ReqManager.FindByMemberId(MeId, 0, -1);
        if (ReqManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
            return;
        }
        if (TSP.DataManager.DocMemberFileManager.IsMemmeberDocumentInSettlementstate(MeId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود پروانه اشتغال به کار در مرحله بررسی سازمان راه و شهرسازی امکان درخواست تغییرات در واحد عضویت وجود ندارد";
            return;
        }
        Response.Redirect("MemberInfo/MemberInsertBaseInfo.aspx?PageMode=" + Utility.EncryptQS("ChangeBaseInfo") + "&UrlRef=" + Utility.EncryptQS("MemberHome"));

    }

    protected void btnLicenceRequest_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            ShowMessage(MeReqResult[1].ToString());
            return;
        }
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
        {
            ShowMessage("این لینک جهت استفاده اعضای تایید شده سازمان جهت تغییر اطلاعات پایه خود می باشند.در صورتی که قصد تغییر اطلاعات خود را دارید از منوی ''واحد عضویت>>مدیریت درخواست ها'' استفاده وارد صفحه مربوطه شده و دکمه ویرایش را کلیک نمایید");
            return;
        }
        int MeId = Utility.GetCurrentUser_MeId();
        if (!CheckLocker(MeId)) return;

        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        ReqManager.FindByMemberId(MeId, 0, -1);
        if (ReqManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
            return;
        }
        if (TSP.DataManager.DocMemberFileManager.IsMemmeberDocumentInSettlementstate(MeId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود پروانه اشتغال به کار در مرحله بررسی سازمان راه و شهرسازی امکان درخواست تغییرات در واحد عضویت وجود ندارد";
            return;
        }
        Response.Redirect("MemberInfo/MemberLicenceRequest.aspx?PageMode=" + Utility.EncryptQS("NewReq") + "&UrlRef=" + Utility.EncryptQS("MemberHome"));
    }

    #region document
    protected void btnNewDoc_Click(object sender, EventArgs e)
    {
        if (CheckConditions())
        {
            ResetDocSessions();

            Response.Redirect("Documents/WizardDocOath.aspx");
        }
    }

    protected void btnQualification_Click(object sender, EventArgs e)
    {
        if (CheckDocQualificationConditions())
        {
            ResetQualification();

            Response.Redirect("Documents/WizardQualificationOath.aspx");
        }
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        if (CheckDocRevivalConditions())
        {
            ResetRevival();

            Response.Redirect("Documents/WizardRevivalOath.aspx");
        }
    }

    protected void btnUpgrade_ServerClick(object sender, EventArgs e)
    {
        try
        {
            ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentUpgrade(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return;
            }
            ResetUpgrad();

            Response.Redirect("Documents/WizardUpgradeOath.aspx", false);
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
            return;
        }


    }
    #endregion

    protected void btnEditMemberInfo_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            ShowMessage(MeReqResult[1].ToString());
            return;
        }
        int IsMeTemp = 0;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            IsMeTemp = 1;
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        MemberRequestManager.FindLastReqByMemberId(Utility.GetCurrentUser_MeId(), IsMeTemp, 0);
        if (MemberRequestManager.Count != 1)
        {
            ShowMessage("شما دارای درخواست درجریان در بخش عضویت نمی باشید");
            return;
        }

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        int MReId = Convert.ToInt32(MemberRequestManager[0]["MReId"]);
        try
        {
            int TaskId = -1;
            int MeId = Utility.GetCurrentUser_MeId();

            int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
            int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }
            DataTable dtWF = WorkFlowStateManager.SelectLastState(TableType, MReId);
            if (dtWF.Rows.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای آخرین درخواست ثبت شده شما در عضویت ، گردش کاری تعریف نشده است.جهت اطلاعات بیشتر  از منوی ''واحد عضویت>>مدیریت درخواست ها'' استفاده وارد صفحه مربوطه شده";
                return;
            }
            //در صورتی که قصد تغییر اطلاعات خود را دارید از منوی ''واحد عضویت>>مدیریت درخواست ها'' استفاده وارد صفحه مربوطه شده 
            if (int.Parse(dtWF.Rows[0]["TaskId"].ToString()) != TaskId)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "با توجه به مرحله جاری گردش کار درخواست شما در عضویت امکان ویرایش درخواست وجود ندارد.جهت اطلاعات بیشتر  از منوی ''واحد عضویت>>مدیریت درخواست ها'' استفاده وارد صفحه مربوطه شده";
                return;
            }
            ReqManager.FindByCode(MReId);
            if (ReqManager.Count > 0)
            {
                if (Convert.ToBoolean(ReqManager[0]["Requester"]))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "درخواست  عضویت شما از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد.جهت اطلاعات بیشتر  از منوی ''واحد عضویت>>مدیریت درخواست ها'' استفاده وارد صفحه مربوطه شده";
                    return;
                }
                if (ReqManager[0]["IsConfirm"].ToString() == "0")
                {
                    if (CheckPermitionForEdit(MReId))
                        Response.Redirect("MemberInfo/MemberRequestInsert.aspx?MReId=" + Utility.EncryptQS(MReId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&MeId=" + Utility.EncryptQS(MeId.ToString()), false);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "آخرین درخواست شما در واحد عضویت پاسخ داده شده است.جهت اطلاعات بیشتر  از منوی ''واحد عضویت>>مدیریت درخواست ها'' استفاده وارد صفحه مربوطه شده";
                }
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }
    protected void btnNewMemberReq_Click(object sender, EventArgs e)
    {
        ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
        if (Convert.ToBoolean(MeReqResult[0]))
        {
            ShowMessage(MeReqResult[1].ToString());
            return;
        }
        int IsMeTemp = 0;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            IsMeTemp = 1;
        int MeId = Utility.GetCurrentUser_MeId();
        try
        {
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();

            if (IsMeTemp == 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ثبت درخواست جدید برای اعضای موقت وجود ندارد";
                return;
            }
            if (IsMeTemp == 0)
            {
                TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                MeManager.FindByCode(MeId);
                if ((bool)MeManager[0]["IsLock"] == true)
                {
                    TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                    string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                    return;
                }
            }

            ReqManager.FindByMemberId(MeId, 0, -1);
            if (ReqManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                return;
            }
            if (TSP.DataManager.DocMemberFileManager.IsMemmeberDocumentInSettlementstate(MeId))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل وجود پروانه اشتغال به کار در مرحله بررسی سازمان راه و شهرسازی امکان درخواست تغییرات در واحد عضویت وجود ندارد";
                return;
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اطلاعات رخ داده است";
            return;
        }
        Response.Redirect("MemberInfo/MemberRequestInsert.aspx?MReId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New") + "&MeId=" + Utility.EncryptQS(MeId.ToString()));

    }

    #region TechnicalService
    protected void btnWorkRequestNew_ServerClick(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
            CapacityAssignmentManager.SelectCurrentYearAndStage(1);
        else
            CapacityAssignmentManager.SelectCurrentYearAndStage(0);
        int _CurrentCapacityAssignmentId = -2;
        string _CurrentCapacityEndate = "";
        if (CapacityAssignmentManager.Count > 0)
        {
            _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
            _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
        }
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        System.Collections.ArrayList Result = ObserverWorkRequestManager.CheckConditionForNewObsWorkRequest(Utility.GetCurrentUser_MeId(), _CurrentCapacityEndate, _CurrentCapacityAssignmentId);
        if (!Convert.ToBoolean(Result[0]))
        {
            ShowMessage(Result[1].ToString());
            return;
        }
        else
            Response.Redirect("TechnicalServices/Capacity/ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("New") + "&ObsWChangId=" + Utility.EncryptQS("-2"));
    }
    protected void btnWorkRequest_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("TechnicalServices/Capacity/ObserverWorkRequest.aspx");
    }
    protected void btnWorkRequestChangeRequest_ServerClick(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        DataTable dtReq = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(Utility.GetCurrentUser_MeId(), TSP.DataManager.TSObserverWorkRequestStatus.All);
        if (dtReq.Rows.Count == 0)
        {
            ShowMessage("تا کنون درخواست آماده به کاری ثبت نکرده اید.لطفا از طریق لینک''اعلام آماده به کاری'' در همین صفحه اقدام نمایید");
            return;
        }
        System.Data.DataTable dt = ObserverWorkRequestChangesManager.SelectLastRequest(-1, Utility.GetCurrentUser_MeId());
        if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["IsConfirm"]) == 0)
        {
            ShowMessage("به دلیل وجود درخواست تایید نشده قادر به ثبت درخواست جدید نمی باشید");
            return;
        }
        int ObsWorkReqChangeId = Convert.ToInt32(dt.Rows[0]["ObsWorkReqChangeId"]);
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
            CapacityAssignmentManager.SelectCurrentYearAndStage(1);
        else
            CapacityAssignmentManager.SelectCurrentYearAndStage(0);
        int _CurrentCapacityAssignmentId = -2;
        string _CurrentCapacityEndate = "";
        if (CapacityAssignmentManager.Count > 0)
        {
            _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
            _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
        }
        System.Collections.ArrayList Result = ObserverWorkRequestManager.CheckConditionForChangeWorkRequest(Utility.GetCurrentUser_MeId(), _CurrentCapacityEndate);
        if (!Convert.ToBoolean(Result[0]))
        {
            ShowMessage(Result[1].ToString());
            return;
        }
        else
            Response.Redirect("TechnicalServices/Capacity/ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("Change") + "&ObsWChangId=" + Utility.EncryptQS(ObsWorkReqChangeId.ToString()).ToString());
    }
    protected void btnWorkRequestOffeRequest_ServerClick(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        DataTable dtReq = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(Utility.GetCurrentUser_MeId(), TSP.DataManager.TSObserverWorkRequestStatus.All);
        if (dtReq.Rows.Count == 0)
        {
            ShowMessage("تا کنون درخواست آماده به کاری ثبت نکرده اید.لطفا از طریق لینک''اعلام آماده به کاری'' در همین صفحه اقدام نمایید");
            return;
        }
        System.Data.DataTable dt = ObserverWorkRequestChangesManager.SelectLastRequest(Convert.ToInt32(dtReq.Rows[0]["ObsWorkReqId"]), -1);
        if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["IsConfirm"]) == 0)
        {
            ShowMessage("به دلیل وجود درخواست تایید نشده قادر به ثبت درخواست جدید نمی باشید");
            return;
        }
        Response.Redirect("TechnicalServices/Capacity/ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("Off") + "&ObsWChangId=" + Utility.EncryptQS(Convert.ToInt32(dt.Rows[0]["ObsWorkReqChangeId"]).ToString()));
    }

    protected void btnControler_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("TechnicalServices/Project/ControlPlans.aspx");
    }
    protected void btnNewDesign_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("TechnicalServices/Project/DesignerLogin.aspx");
    }
    protected void btnMapManagmentPage_ServerClick(object sender, EventArgs e)
    {

        Response.Redirect("TechnicalServices/Project/MemberPlans.aspx");
    }
    protected void btnProjectManagmentPage_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("TechnicalServices/Project/Project.aspx");
    }
    protected void btnQueueListMunRequest_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("TechnicalServices/Capacity/QueueListMunicipality.aspx");
    }
    #endregion


    #region Methods

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetMeFileRequestLabel()
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
        if (DocMemberFileManager.Count > 0)
            HiddenHelp["DocMemberVisiblety"] = false;
        else
            HiddenHelp["DocMemberVisiblety"] = true; // btnQualification.ClientVisible = linkbtnQualification.Visible = false;= btnNewDoc1.ClientVisible = btnNewDoc.Visible =
    }

    private void SetMeImplementerRequestLabel()
    {
    }

    private void SetMeObservationRequestLabel()
    {
    }


    bool CheckLocker(int MeId)
    {
        string IsMeTemp;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            IsMeTemp = "1";
        else IsMeTemp = "0";

        if (IsMeTemp == "0")
        {
            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(MeId);
            if ((bool)MeManager[0]["IsLock"] == true)
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                return false;
            }
        }

        return true;
    }

    private Boolean CheckPermitionForSendDoc(int TableId, int WfCode, int SaveTaskCode)
    {
        TSP.DataManager.WFPermission WFPermission = new TSP.DataManager.WFPermission();
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        return (WorkFlowPermission.CheckPermissionForSendDocToNextStepByUserForOtherPortals(TableId, WfCode, SaveTaskCode, Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_NmcIdType()));
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.MemberPortal).ToString());
        // HyperLinkHelp.NavigateUrl = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.MemberPortal).ToString());
    }

    void ShowError(String Error)
    {
        lblError.Visible = true;
        lblError.Text = "* " + Error + "<br><br>";
    }

    private void ResetDocSessions()
    {
        Session["WizardDocOath"] =
        Session["WizardDocExam"] =
        Session["WizardDocMajor"] =
        Session["WizardDocResposblity"] =
        Session["WizardDocPeriods"] =
        Session["WizardDocJob"] =
        Session["WizardDocSummary"] =
        Session["JobFileURL"] =
        Session["WizardDocJobConfirm"] =
        Session["JobGrdURL"] =
        Session["ACCFileURL"] =
        Session["FishFileURL"] =
        Session["chbIAgree"] =
        Session["ExamFileImgURLSoource"] =
        Session["ImgTaxOfficeLetter"] = null;
    }

    private void ResetQualification()
    {
        Session["WizardDocQualificationExam"] =
        Session["WizardDocQualificationSummary"] =
        Session["WizardDocQualificationOath"] =
        Session["WizardQualificationJobConfirm"] =
        Session["DocQualificationJobFileURL"] =
        Session["DocQualificationJobGrdURL"] =
        Session["WizardQualificationchbIAgree"] =
        Session["WizardQualificationImgTaxOfficeLetter"] =
        Session["WizardQualificationImgfrontDoc"] =
Session["WizardQualificationImgBackDoc"] =
        null;
    }

    private void ResetRevival()
    {
        Session["WizardDocRevivalSummary"] =
        Session["WizardDocRevivalOath"] =
        Session["WizardRevivalJobConfirm"] =
        Session["DocRevivalJobFileURL"] =
        Session["DocRevivalJobGrdURL"] =
        Session["WizardRevivalImgTaxOfficeLetter"] =
        Session["WizardRevivalImgfrontDoc"] =
        Session["WizardRevivalImgBackDoc"] =
        Session["ImgPeriodImage"] =
        Session["ImgTaxOfficeLetter"] =
        Session["WizardRevivalCivilLicence"] = null;
    }

    private void ResetUpgrad()
    {
        Session["WizardUpgradeHasConnKard"] =
       Session["WizardDocUpgradeOath"] =
       Session["WizardUpgradeImgfrontDoc"] =
       Session["WizardUpgradeImgBackDoc"] =
       Session["WizardUpgradeImgTaxOfficeLetter"] =
       Session["WizardDocUpgradeSummary"] =
       Session["WizardUpgradeJobConfirm"] =
       Session["WizardUpgradeKardFileURL"] =
       Session["ImgPeriodImage"] =
       Session["DocUpgradeJobFileURL"] =
       Session["DocUpgradeJobGrdURL"] = null;
    }
    private Boolean CheckConditions()
    {
        try
        {
            ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForNewDocument(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }

    private Boolean CheckDocQualificationConditions()
    {
        try
        {
            ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentQualification(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }

    private Boolean CheckDocRevivalConditions()
    {
        try
        {
            ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentRevival(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                return true;


                            }

                        }

                    }

                }

            }

        }
        return false;

    }

    private void SetDocumentRequestMenueVisibility()
    {
        TSP.DataManager.DocMemberRequestVisibilityManager DocMemberRequestVisibilityManager = new TSP.DataManager.DocMemberRequestVisibilityManager();
        TSP.DataManager.DocMemberRequestVisibilityExceptionManager DocMemberRequestVisibilityExceptionManager = new TSP.DataManager.DocMemberRequestVisibilityExceptionManager();

        DocMemberRequestVisibilityManager.SelectDocMemberRequest(TSP.DataManager.DocMemberRequestVisibilityReqType.NewDocumentRequest, Utility.GetDateOfToday(), 0);
        if (DocMemberRequestVisibilityManager.Count > 0)
        {
            btnNewDoc.Visible = false;
            DocMemberRequestVisibilityExceptionManager.SelectDocMemberRequestException(Utility.GetCurrentUser_MeId(), TSP.DataManager.DocMemberRequestVisibilityReqType.NewDocumentRequest, Utility.GetDateOfToday(), 0);
            if (DocMemberRequestVisibilityExceptionManager.Count > 0)
                btnNewDoc.Visible = true;
        }
        else
            btnNewDoc.Visible = true;

        DocMemberRequestVisibilityManager.SelectDocMemberRequest(TSP.DataManager.DocMemberRequestVisibilityReqType.QualificationRequest, Utility.GetDateOfToday(), 0);
        if (DocMemberRequestVisibilityManager.Count > 0)
        {
            btnQualification.Visible = false;
            DocMemberRequestVisibilityExceptionManager.SelectDocMemberRequestException(Utility.GetCurrentUser_MeId(), TSP.DataManager.DocMemberRequestVisibilityReqType.QualificationRequest, Utility.GetDateOfToday(), 0);
            if (DocMemberRequestVisibilityExceptionManager.Count > 0)
                btnQualification.Visible = true;
        }
        else
            btnQualification.Visible = true;
    }
    #endregion

}
