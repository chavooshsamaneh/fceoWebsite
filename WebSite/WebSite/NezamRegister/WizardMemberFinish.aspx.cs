using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class NezamRegister_WizardMemberFinish : System.Web.UI.Page
{
    DataTable dtMember = new DataTable();
    DataTable dtActivity = new DataTable();
    DataTable dtActivity2 = new DataTable();
    DataTable dtJob = new DataTable();
    DataTable dtMadrak = new DataTable();
    DataTable dtResearch = new DataTable();
    DataTable dtLanguage = new DataTable();
    private string _PageMode = "";
    private string PageMode
    {
        get
        {
            try { return HiddenFieldWizardMe["PageMode"].ToString(); }
            catch { return ""; }

        }
        set
        {
            HiddenFieldWizardMe["PageMode"] = value;
        }
    }

    private bool IsPageRefresh = false;

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        string qs = this.Request.Url.ToString();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }
        if (PageMode != "SendToBank")
            SetKey();
        if (!IsPostBack)
        {
            // SetPageMode();
            //**** SetEPaymentMode();
            ViewState["Login"] = -1;
            HiddenPrintDetial["PrintUserInfo"] = "";
            HiddenPrintDetial["PrintUserData"] = "";
        }
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardMemberSummary.aspx");
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (ViewState["Login"] != null)
        {
            ClearSessions();
            //Response.Redirect("~/Members/MemberHome.aspx?MeId=" + HDMemberId.Value);

            Session["LoginFromOtherPage"] = true;
            Response.Redirect("~/Login.aspx?LId=" + Utility.EncryptQS(ViewState["Login"].ToString()) + "&qto=" + Utility.EncryptQS(DateTime.Now.ToFileTime().ToString()) + "&tsp=" + Utility.EncryptQS("0"));
        }
        else
            Response.Redirect("~/Login.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session["TblActivity"] = null;
        //Session["TblActivity2"] = null;
        //Session["TblResearch"] = null;
        //Session["TblJob"] = null;
        //Session["TblOfMadrak"] = null;
        //Session["TblLanguage"] = null;
        //Session["FileOfMember"] = null;
        //Session["LetterImg"] = null;
        //Session["Member"] = null;
        ClearSessions();

        Response.Redirect("~/Default.aspx");

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList MeReqResult = TSP.DataManager.Utility.CheckMemberRequestVisibility();
            if (Convert.ToBoolean(MeReqResult[0]))
            {
                ShowMessage(MeReqResult[1].ToString());
                return;
            }
            if (Session["MemberMembership"] == null || (Boolean)Session["MemberMembership"] == false)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - چهارچوب شئون حرفه ای مهندسی مورد موافقت قرار نگرفته است");
                return;
            }
            if (Session["Member"] == null || ((DataTable)Session["Member"]).Rows.Count == 0)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - مشخصات شخص وارد نشده است");
                return;
            }
            if (Session["TblOfMadrak"] == null || ((DataTable)Session["TblOfMadrak"]).Rows.Count == 0)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - مشخصات مدرک تحصیلی وارد نشده است");
                return;
            }
            if (Session["MemberSummary"] == null || (Boolean)Session["MemberSummary"] == false)
            {
                ShowMessage("اطلاعات وارد شده ناقص می باشد" + " - اطلاعات ثبت نام در مرحله خلاصه اطلاعات، تایید نشده است");
                return;
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            return;
        }
        InsertAndPayment();
    }

    protected void btnEpayment_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(ViewState["BankURL"].ToString(), false);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }

    #endregion

    #region Methods
    private Boolean CheckTsTimeOut()
    {
        if (Session["Member"] == null)
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است و یا پس از ورود به صفحه بانک از دکمه بازگشت مرورگر استفاده نموده اید.مجدداً اقدام نمایید");
            return false;
        }
        return true;
    }

    private void SetKey()
    {
        //if (!string.IsNullOrEmpty(Request.QueryString["QS"]))
        //{
        //    SetBankReplySetKey();
        //}
        //else
        //{
        //  SetOtherKey();
        // }
        if (!CheckTsTimeOut())
        {
            btnSave.Visible = false;
        }
        SetMenueImage();

        if (string.IsNullOrEmpty(Request.QueryString["PgMode"]))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        PageMode = Utility.DecryptQS(Request.QueryString["PgMode"]);

        if (EPaymentUC.AccType == null || EPaymentUC.AccType == -1)
        {
            if (CheckIfMeTransfer())
            {
                //EPaymentUC.AccType = (int)TSP.DataManager.TSAccountingAccType.Entrance;
                RadiobtnPaymentType.ClientVisible = false;
                liPaymentComEnt.Visible = true;
                liPaymentComGen.Visible = false;
                RadiobtnPaymentType.SelectedIndex = 1;
                RadiobtnPaymentType.ClientEnabled = false;
                PanelEpayment.ClientVisible = false;
            }
            else
                EPaymentUC.AccType = (int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance;
        }
        EPaymentUC.TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Member);
        EPaymentUC.PageMode = PageMode;

        //if (!Utility.IsDBNullOrNullValue(Request.Form["resultCode"]))
        //    EPaymentUC.ResultCode = Request.Form["resultCode"];
        ////if (!Utility.IsDBNullOrNullValue(Request.Form["paymentId"]))
        ////    EPaymentUC.PaymentId = int.Parse(Request.Form["paymentId"]);
        //if (!Utility.IsDBNullOrNullValue(Request.Form["InvoiceNumber"]))
        //    EPaymentUC.InvoiceNumber = int.Parse(Request.Form["InvoiceNumber"]);
        //if (!Utility.IsDBNullOrNullValue(Request.Form["token"]))
        //    EPaymentUC.Token = Request.Form["token"];
        //if (!Utility.IsDBNullOrNullValue(Request.Form["referenceId"]))
        //    EPaymentUC.ReferenceId = Request.Form["referenceId"];

        //EPaymentUC.ResultCode = "100";
        //EPaymentUC.PaymentId = 315668;
        //EPaymentUC.ReferenceId = "000000122802";
    }

    //private void SetBankReplySetKey()
    //{
    //    //////string Qs = Utility.DecryptQS(Request.QueryString["QS"]);
    //    //////string[] ArrayQS = Qs.Split(';');
    //    //////PageMode = ArrayQS[0];
    //    //////EPaymentUC.AccType = int.Parse(ArrayQS[1].ToString());
    //    //////EPaymentUC.TableId = int.Parse(ArrayQS[2].ToString());

    //    ////////////////
    //    PageMode = "BankReply";
    //    int AccountingId = -2;
    //    if (Request.Form["paymentId"] != null)
    //        AccountingId = int.Parse(Request.Form["paymentId"]);

    //    if (AccountingId == -2)
    //        return;
    //    TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
    //    AccountingManager.FindByAccountingId(AccountingId);
    //    if (AccountingManager.Count != 1)
    //    {
    //        ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
    //        return;
    //    }
    //    EPaymentUC.AccType = Convert.ToInt32(AccountingManager[0]["AccType"]);
    //    EPaymentUC.TableId = Convert.ToInt32(AccountingManager[0]["TableTypeId"]);

    //}

    //private void SetOtherKey()
    //{
    //    if (!CheckTsTimeOut())
    //    {
    //        btnSave.Visible = false;
    //    }
    //    SetMenueImage();

    //    if (string.IsNullOrEmpty(Request.QueryString["PgMode"]))
    //    {
    //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
    //        return;
    //    }
    //    PageMode = Utility.DecryptQS(Request.QueryString["PgMode"]);
    //}

    //private void SetPageMode()
    //{

    //    switch (PageMode)
    //    {
    //        case "BankReply":
    //            SetBankReplyMode();
    //            break;
    //        case "EPayment":
    //            SetEPaymentMode();
    //            break;
    //    }
    //}

    //private void SetEPaymentMode()
    //{
    //    //if (CheckTsTimeOut())
    //    //{
    //    //    btnSave.Enabled = false;
    //    //}
    //    //SetMenueImage();
    //    ViewState["Login"] = -1;
    //    HiddenPrintDetial["PrintUserInfo"] = "";
    //    HiddenPrintDetial["PrintUserData"] = "";
    //}

    //private void SetBankReplyMode()
    //{
    //    RadiobtnPaymentType.ClientVisible = false;
    //    RoundPanelPaymentType.ClientVisible = false;
    //    PanelEpayment.ClientVisible = true;

    //    TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
    //    int MReId = EPaymentUC.TableId;

    //    if (DoNextTaskOfPayment(MReId, LoginManager, MemberRequestManager) == -1)
    //        return;
    //    int UserId = int.Parse(LoginManager[0]["UserId"].ToString());
    //    int UltId = (int)TSP.DataManager.UserType.TemporaryMembers;
    //    Boolean PayedSucced = true;
    //    if (!EPaymentUC.DoNextTaskOfBankReply(UserId, UltId, EPaymentUC.Token))
    //    {
    //        PayedSucced = false;
    //    }
    //}

    private void DoNextTaskOfInsert(int MReId, TSP.DataManager.LoginManager LoginManager, TSP.DataManager.MemberRequestManager MemberRequestManager)
    {
        btnSave.Visible = false;
        btnPre.Visible = false;
        btnCancel.Visible = false;
        btnContinue.Visible = true;
        btnPrint.Visible = true;
        btnPrintUserInfo.Visible = true;

        MemberRequestManager.FindByCode(MReId);
        if (MemberRequestManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }

        int MemberId = int.Parse(MemberRequestManager[0]["MeId"].ToString());
        LoginManager.FindByMeIdUltId(MemberId, (int)TSP.DataManager.UserType.TemporaryMembers);
        if (LoginManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        String Password = Utility.GeneratePassword();
        LoginManager[0].BeginEdit();
        LoginManager[0]["Password"] = Utility.EncryptPassword(Password);
        LoginManager[0].EndEdit();
        LoginManager.Save();
        PanelUserInfo.Visible = true;
        ASUserName.Text = "M" + MemberId.ToString();
        ASEmailUser.Text = MemberRequestManager[0]["Email"].ToString();
        ASPassword.Text = Password;
        lblFollowCode.Text = MemberRequestManager[0]["FollowCode"].ToString();

        HiddenPrintDetial["PrintUserInfo"] = "../ReportForms/UserInfoReport.aspx?UId=" + Utility.EncryptQS(LoginManager[0]["UserId"].ToString()) + "&P=" + Utility.EncryptQS(Password) + "&C=" + Utility.EncryptQS(MemberRequestManager[0]["FollowCode"].ToString());
        HiddenPrintDetial["PrintUserData"] = "../ReportForms/MembersReport.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString())
                                              + "&PageMode=" + Utility.EncryptQS("Wizard") + "&Password=" + Utility.EncryptQS(Password) + "&UserId=" + Utility.EncryptQS(LoginManager[0]["UserId"].ToString());

        SendEmail(MemberRequestManager[0]["Email"].ToString(), MemberRequestManager[0]["FollowCode"].ToString(), MemberRequestManager[0]["FirstName"] + " " + MemberRequestManager[0]["LastName"], "M" + MemberId.ToString(), Password);
    }

    //private int DoNextTaskOfPayment(int MReId, TSP.DataManager.LoginManager LoginManager, TSP.DataManager.MemberRequestManager MemberRequestManager)
    //{
    //    btnSave.Visible = false;
    //    btnPre.Visible = false;
    //    btnCancel.Visible = false;

    //    MemberRequestManager.FindByCode(MReId);
    //    if (MemberRequestManager.Count != 1)
    //    {
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
    //        return -1;
    //    }

    //    int MemberId = int.Parse(MemberRequestManager[0]["MeId"].ToString());
    //    LoginManager.FindByMeIdUltId(MemberId, (int)TSP.DataManager.UserType.TemporaryMembers);
    //    if (LoginManager.Count != 1)
    //    {
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
    //        return -1;
    //    }
    //    String Password = Utility.GeneratePassword();
    //    LoginManager[0].BeginEdit();
    //    LoginManager[0]["Password"] = Utility.EncryptPassword(Password);
    //    LoginManager[0].EndEdit();
    //    LoginManager.Save();
    //    PanelUserInfo.Visible = true;
    //    ASUserName.Text = "M" + MemberId.ToString();
    //    ASEmailUser.Text = MemberRequestManager[0]["Email"].ToString();
    //    ASPassword.Text = Password;
    //    lblFollowCode.Text = MemberRequestManager[0]["FollowCode"].ToString();
    //    btnContinue.Visible = true;
    //    btnPrint.Visible = true;
    //    btnPrintUserInfo.Visible = true;
    //    HiddenPrintDetial["PrintUserInfo"] = "../ReportForms/UserInfoReport.aspx?UId=" + Utility.EncryptQS(LoginManager[0]["UserId"].ToString()) + "&P=" + Utility.EncryptQS(Password) + "&C=" + Utility.EncryptQS(MemberRequestManager[0]["FollowCode"].ToString());
    //    HiddenPrintDetial["PrintUserData"] = "../ReportForms/MembersReport.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString())
    //                                          + "&PageMode=" + Utility.EncryptQS("Wizard") + "&Password=" + Utility.EncryptQS(Password) + "&UserId=" + Utility.EncryptQS(LoginManager[0]["UserId"].ToString());

    //    SendEmail(MemberRequestManager[0]["Email"].ToString(), MemberRequestManager[0]["FollowCode"].ToString(), MemberRequestManager[0]["FirstName"] + " " + MemberRequestManager[0]["LastName"], "M" + MemberId.ToString(), Password);

    //    return Convert.ToInt32(LoginManager[0]["UserId"]);
    //}  

    private void SetMenueImage()
    {

        if (Session["MemberMembership"] != null && (Boolean)Session["MemberMembership"] == true)
        {
            ASPxMenu1.Items.FindByName("Membership").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Membership").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Membership").Image.Height = Unit.Pixel(15);
        }
        if (Session["Member"] != null && ((DataTable)Session["Member"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Member").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Member").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfMadrak"] != null && ((DataTable)Session["TblOfMadrak"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Madrak").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Madrak").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Madrak").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblJob"] != null && ((DataTable)Session["TblJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblActivity"] != null && Session["TblActivity2"] != null && ((DataTable)Session["TblActivity"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Activity").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Activity").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Activity").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblLanguage"] != null && ((DataTable)Session["TblLanguage"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Language").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Language").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Language").Image.Height = Unit.Pixel(15);
        }
        if (Session["MemberSummary"] != null && (Boolean)Session["MemberSummary"] == true)
        {
            ASPxMenu1.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }
    }

    #region Insert
    protected void InsertAndPayment()
    {
        if (IsPageRefresh)
            return;
        #region CheckCondition
        if (Session["Member"] == null)
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است.مجدداً اقدام نمایید");
            return;
        }
        dtMember = (DataTable)Session["Member"];
        if (dtMember.Rows.Count == 0)
        {
            ShowMessage("خطایی در ذخیره اطلاعات ایجاد شده است.مجدداً اقدام نمایید");
            return;
        }

        if (Session["TblOfMadrak"] == null)
        {
            ShowMessage("ثبت حداقل یک مدرک تحصیلی الزامی می باشد");
            return;
        }
        //جهت جلوگیری از چندین بار ثبت یک عضو در صورتی که به هر روش به ین صفحه بدون پاس کردن صفحه اول رسیده باشد*****
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dtMe2 = MemberManager.SelectActiveMembers(-1, dtMember.Rows[0]["SSN"].ToString(), dtMember.Rows[0]["FirstName"].ToString(), dtMember.Rows[0]["LastName"].ToString());
        if (dtMe2.Rows.Count > 0)
        {

            string message = "اطلاعات شما پیش از این در سیستم ثبت شده است.کد عضویت شما " + dtMe2.Rows[0]["MeId"].ToString() + " می باشد.در صورتی که از ثبت نام شما بیش از" + Utility.GetMembershipRegTimeout() + " روز گذشته است و هنوز اقدام به تحویل مدارک خود به سازمان ننموده اید،<b>بدون تلاش برای ورود به پرتال خود</b> جهت ارائه مدارک به سازمان مراجعه نمایید.";
            ShowMessage(message);
            return;
        }

        TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
        TempMemberManager.SearchTempMember(-1, dtMember.Rows[0]["FirstName"].ToString(), dtMember.Rows[0]["LastName"].ToString());
        if (TempMemberManager.DataTable.Rows.Count > 0)
        {
            DataRow[] drTMe = TempMemberManager.DataTable.Select("SSN= '" + dtMember.Rows[0]["SSN"].ToString() + "'" + " and MsId<>2" + " and InActive=0");
            if (drTMe.Length > 0)
            {
                string message = "اطلاعات شما پیش از این در سیستم ثبت شده است.نام کاربری شما " + "M" + drTMe[0]["TMeId"].ToString() + " می باشد.در صورت عدم تحویل مدارک خود به واحد عضویت در موعد مقرر ،هرچه سریعتر با در دست داشتن مدارک مربوطه به این واحد مراجعه نمایید.";
                ShowMessage(message);
                return;
            }
        }

        TempMemberManager.FindBySSN(dtMember.Rows[0]["SSN"].ToString());
        if (TempMemberManager.Count > 0)
        {
            DataRow[] drTMe = TempMemberManager.DataTable.Select("MsId<>2" + " and InActive=0");
            if (drTMe.Length > 0)
            {
                string message = "اطلاعات شما پیش از این در سیستم ثبت شده است.نام کاربری شما " + "M" + TempMemberManager[0]["TMeId"].ToString() + " می باشد.در صورت عدم تحویل مدارک خود به واحد عضویت در موعد مقرر ،هرچه سریعتر با در دست داشتن مدارک مربوطه به این واحد مراجعه نمایید.";
                ShowMessage(message);
                return;
            }
        }

        #endregion
        if (Session["TblActivity"] != null)
            dtActivity = (DataTable)Session["TblActivity"];
        if (Session["TblActivity2"] != null)
            dtActivity2 = (DataTable)Session["TblActivity2"];
        if (Session["TblJob"] != null)
            dtJob = (DataTable)Session["TblJob"];
        if (Session["TblOfMadrak"] != null)
            dtMadrak = (DataTable)Session["TblOfMadrak"];
        if (Session["TblLanguage"] != null)
            dtLanguage = (DataTable)Session["TblLanguage"];
        int AgentId = int.Parse(dtMember.Rows[0]["AgentId"].ToString());
        #region Define Managers
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TempMemberManager MeManager = new TSP.DataManager.TempMemberManager();
        TSP.DataManager.TempMemberActivitySubjectManager MemberAtSubjManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        TSP.DataManager.TempMemberLicenceManager MeLiManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.TempMemberJobHistoryManager MeJobManager = new TSP.DataManager.TempMemberJobHistoryManager();
        TSP.DataManager.TempMemberLanguageManager MeLanManager = new TSP.DataManager.TempMemberLanguageManager();
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(transact);

        TSP.DataManager.AttachmentsManager AttachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        transact.Add(LogManager);
        transact.Add(MeManager);
        transact.Add(MemberAtSubjManager);
        transact.Add(MeLiManager);
        transact.Add(MeJobManager);
        transact.Add(MeLanManager);
        transact.Add(transferManager);
        transact.Add(ReqManager);
        transact.Add(WorkFlowStateManager);
        transact.Add(AttachManager);

        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember);
        if (WorkFlowTaskManager.Count != 1)
        {
            ShowMessage("خطایی در ذخیره اطلاعات رخ داده است");
            return;
        }
        int WFTaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
        #endregion
        string PerDate = Utility.GetDateOfToday();
        Boolean SaveComplete = false;
        int MemberId = -2;
        try
        {
            decimal Amount = 0;
            String Password = Utility.GeneratePassword();

            transact.BeginSave();

            int MReId = -1;

            #region User
            DataRow logRow = LogManager.NewRow();
            logRow.BeginEdit();
            logRow["UserName"] = dtMember.Rows[0]["SSN"].ToString().GetHashCode().ToString();
            logRow["Password"] = Utility.EncryptPassword(Password);
            logRow["UltId"] = (int)TSP.DataManager.UserType.TemporaryMembers;
            logRow["Email"] = dtMember.Rows[0]["Email"].ToString();
            logRow["IsValid"] = 1;
            logRow["ModifiedDate"] = DateTime.Now;
            logRow["MeId"] = -1;
            logRow.EndEdit();
            LogManager.AddRow(logRow);
            LogManager.Save();
            LogManager.DataTable.AcceptChanges();
            int UserId = int.Parse(LogManager[0]["UserId"].ToString());
            #endregion
            #region Member
            DataRow MemberRow = MeManager.NewRow();
            MemberRow.BeginEdit();
            MemberRow["UserId"] = UserId;
            MemberRow["MeNo"] = "";
            MemberRow["FirstName"] = dtMember.Rows[0]["FirstName"].ToString();
            MemberRow["LastName"] = dtMember.Rows[0]["LastName"].ToString();
            MemberRow["FirstNameEn"] = dtMember.Rows[0]["FirstNameEn"].ToString();
            MemberRow["LastNameEn"] = dtMember.Rows[0]["LastNameEn"].ToString();
          
            int TiId = FindMaxLicenceTiid();
            MemberRow["TiId"] = TiId;

            MemberRow["FatherName"] = dtMember.Rows[0]["FatherName"].ToString();
            MemberRow["BirhtDate"] = dtMember.Rows[0]["BirthDate"].ToString().Trim();
            MemberRow["BirthPlace"] = dtMember.Rows[0]["BirthPlace"].ToString();
            MemberRow["IdNo"] = dtMember.Rows[0]["IdNo"];
            MemberRow["IssuePlace"] = dtMember.Rows[0]["IssuePlace"].ToString();
            MemberRow["SSN"] = dtMember.Rows[0]["SSN"];
            MemberRow["MobileNo"] = dtMember.Rows[0]["MobileNo"];
            MemberRow["HomeAdr"] = dtMember.Rows[0]["HomeAdr"].ToString();
            MemberRow["HomeTel"] = dtMember.Rows[0]["HomeTel"];
            MemberRow["HomePO"] = dtMember.Rows[0]["HomePO"];
            MemberRow["WorkAdr"] = dtMember.Rows[0]["WorkAdr"].ToString();
            MemberRow["WorkTel"] = dtMember.Rows[0]["WorkTel"];
            MemberRow["WorkPO"] = dtMember.Rows[0]["WorkPO"];
            MemberRow["FaxNo"] = dtMember.Rows[0]["FaxNo"];
            MemberRow["BankAccNo"] = dtMember.Rows[0]["BankAccNo"];
            MemberRow["SexId"] = dtMember.Rows[0]["SexId"];
            MemberRow["SoId"] = dtMember.Rows[0]["SoId"];
            MemberRow["MilitaryCommitment"] = Convert.ToBoolean(dtMember.Rows[0]["MilitaryCommitment"]);
            MemberRow["CitId"] = dtMember.Rows[0]["CitId"];
            MemberRow["MsId"] = (int)TSP.DataManager.TemporaryMemberStatus.Pending;
            MemberRow["MarId"] = dtMember.Rows[0]["MarId"];
            MemberRow["FileNo"] = dtMember.Rows[0]["FileNo"];
            MemberRow["RelId"] = dtMember.Rows[0]["RelId"];
            MemberRow["AgentId"] = dtMember.Rows[0]["AgentId"];
            MemberRow["ComId"] = dtActivity2.Rows[0]["ComId"];
            MemberRow["AtId"] = dtActivity2.Rows[0]["AtId"];
            MemberRow["Nationality"] = dtMember.Rows[0]["Nationality"].ToString();
            MemberRow["Website"] = dtMember.Rows[0]["Website"].ToString();
            MemberRow["Email"] = dtMember.Rows[0]["Email"].ToString();
            MemberRow["CreateDate"] = dtMember.Rows[0]["CreateDate"].ToString();
            MemberRow["Description"] = dtMember.Rows[0]["Description"].ToString();
            MemberRow["NezamKardanConfirmURL"] = dtMember.Rows[0]["NezamKardanConfirmURL"];
            MemberRow["UserId"] = UserId;
            MemberRow["ModifiedDate"] = DateTime.Now;
            MemberRow.EndEdit();
            MeManager.AddRow(MemberRow);

            int cc = MeManager.Save();
            if (cc <= 0)
            {
                transact.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
                return;
            }
            MemberId = int.Parse(MeManager[0]["TMeId"].ToString());

            HDMemberId.Value = Utility.EncryptQS(MemberId.ToString());          
            MeManager.DataTable.AcceptChanges();

            LogManager[0]["UserName"] = "M" + MemberId.ToString();
            LogManager[0]["MeId"] = MemberId;

            LogManager.Save();

            Session["LoginName"] = LogManager[0]["UserName"].ToString();
            Session["LoginType"] = LogManager[0]["UltId"].ToString();
            Session["MeId"] = MemberId;

            #endregion
            #region Request
            DataRow drq = ReqManager.NewRow();

            drq["IsMeTemp"] = 1;
            drq["MeId"] = MemberId;
            drq["FirstName"] = dtMember.Rows[0]["FirstName"].ToString();
            drq["MeNo"] = "";
            drq["LastName"] = dtMember.Rows[0]["LastName"].ToString();
            drq["FirstNameEn"] = dtMember.Rows[0]["FirstNameEn"].ToString();
            drq["LastNameEn"] = dtMember.Rows[0]["LastNameEn"].ToString();
            drq["MobileNo"] = dtMember.Rows[0]["MobileNo"].ToString();
            drq["HomeAdr"] = dtMember.Rows[0]["HomeAdr"].ToString();
            drq["HomeTel"] = dtMember.Rows[0]["HomeTel"];
            drq["HomePO"] = dtMember.Rows[0]["HomePO"];
            drq["WorkAdr"] = dtMember.Rows[0]["WorkAdr"];
            drq["WorkTel"] = dtMember.Rows[0]["WorkTel"];
            drq["FaxNo"] = dtMember.Rows[0]["FaxNo"];
            drq["WorkPO"] = dtMember.Rows[0]["WorkPO"];
            drq["BankAccNo"] = dtMember.Rows[0]["BankAccNo"];
            drq["MarId"] = dtMember.Rows[0]["MarId"];
            drq["SoId"] = dtMember.Rows[0]["SoId"];
            drq["MilitaryCommitment"] = Convert.ToBoolean(dtMember.Rows[0]["MilitaryCommitment"]);
            drq["Website"] = dtMember.Rows[0]["Website"].ToString();
            drq["Email"] = dtMember.Rows[0]["Email"].ToString();
            drq["IsCreated"] = 1;
            drq["CreateDate"] = dtMember.Rows[0]["CreateDate"].ToString();
            drq["UserId"] = UserId;
            drq["ModifiedDate"] = DateTime.Now;
            drq["SexId"] = dtMember.Rows[0]["SexId"];
            drq["CitId"] = dtMember.Rows[0]["CitId"];
            drq["AgentId"] = dtMember.Rows[0]["AgentId"];
            drq["FatherName"] = dtMember.Rows[0]["FatherName"];
            drq["BirhtDate"] = dtMember.Rows[0]["BirthDate"].ToString().Trim();
            drq["BirthPlace"] = dtMember.Rows[0]["BirthPlace"];
            drq["IdNo"] = dtMember.Rows[0]["IdNo"];
            drq["IssuePlace"] = dtMember.Rows[0]["IssuePlace"];
            drq["SSN"] = dtMember.Rows[0]["SSN"];
            drq["ComId"] = dtActivity2.Rows[0]["ComId"];
            drq["NezamKardanConfirmURL"] = dtMember.Rows[0]["NezamKardanConfirmURL"];

            drq["MsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Pending;//در جریان
            drq["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberRequest);
            drq["WFCurrentTaskId"] = WFTaskId;
            // drq["ArchitectorCode"] = DBNull.Value;
            ReqManager.AddRow(drq);
            int rcnt = ReqManager.Save();
            ReqManager.DataTable.AcceptChanges();
            if (rcnt <= 0)
            {
                transact.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
                return;
            }
            MReId = int.Parse(ReqManager[0]["MReId"].ToString());
            HDRequestId.Value = Utility.EncryptQS(MReId.ToString());

            ReqManager.DataTable.AcceptChanges();

            if (Session["FileOfMember"] != null)
            {
                MemberRow["ImageUrl"] = "~/image/Members/Person/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfMember"].ToString());
                ReqManager[0]["ImageUrl"] = "~/image/Members/Person/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfMember"].ToString());

            }
            if (Session["FileOfSign"] != null)
            {
                MemberRow["SignUrl"] = "~/image/Members/Sign/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfSign"].ToString());
                ReqManager[0]["SignUrl"] = "~/image/Members/Sign/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfSign"].ToString());

            }
            MemberRow["AccId"] = DBNull.Value;
            MeManager.Save();
            ReqManager.Save();


            #endregion
            #region Activity
            if (dtActivity.Rows.Count > 0)
            {
                for (int j = 0; j < dtActivity.Rows.Count; j++)
                {
                    DataRow drAtSubj = MemberAtSubjManager.NewRow();
                    drAtSubj.BeginEdit();

                    //drAtSubj["MasId"] = j;
                    drAtSubj["AsId"] = dtActivity.Rows[j]["AtId"];
                    drAtSubj["TMeId"] = MemberId;
                    drAtSubj["UserId"] = UserId;
                    if (!string.IsNullOrEmpty(dtActivity.Rows[j]["AsPercent"].ToString()))
                        drAtSubj["AsPercent"] = Convert.ToInt16(dtActivity.Rows[j]["AsPercent"].ToString());
                    drAtSubj["Description"] = dtActivity.Rows[j]["Description"].ToString();
                    drAtSubj["ModifiedDate"] = DateTime.Now;
                    drAtSubj["MReId"] = MReId;

                    drAtSubj.EndEdit();
                    MemberAtSubjManager.AddRow(drAtSubj);
                }
            }
            #endregion
            #region Job

            if (dtJob.Rows.Count > 0)
            {
                for (int i = 0; i < dtJob.Rows.Count; i++)
                {
                    DataRow drJob = MeJobManager.NewRow();
                    drJob.BeginEdit();

                    drJob["TMeId"] = MemberId;
                    drJob["RoeId"] = 1;//ثبت عضویت
                    drJob["PrTypeId"] = dtJob.Rows[i]["PrTypeId"];
                    drJob["SazeTypeId"] = dtJob.Rows[i]["SazeTypeId"];
                    drJob["ProjectName"] = dtJob.Rows[i]["ProjectName"].ToString();
                    drJob["Employer"] = dtJob.Rows[i]["Employer"].ToString();
                    drJob["CitName"] = dtJob.Rows[i]["CitName"].ToString();
                    drJob["CounId"] = dtJob.Rows[i]["CounId"];
                    drJob["PJPId"] = dtJob.Rows[i]["PJPId"].ToString();
                    drJob["StartOriginalDate"] = dtJob.Rows[i]["StartOriginalDate"].ToString();
                    drJob["StartCorporateDate"] = dtJob.Rows[i]["StartCorporateDate"].ToString();
                    drJob["StatusOfStartDate"] = dtJob.Rows[i]["StatusOfStartDate"].ToString();
                    drJob["EndCorporateDate"] = dtJob.Rows[i]["EndCorporateDate"].ToString();
                    drJob["StatusOfEndDate"] = dtJob.Rows[i]["StatusOfEndDate"].ToString();
                    drJob["ProjectVolume"] = dtJob.Rows[i]["ProjectVolume"].ToString();
                    if (!string.IsNullOrEmpty(dtJob.Rows[i]["Area"].ToString()))
                        drJob["Area"] = dtJob.Rows[i]["Area"];
                    else
                        drJob["Area"] = DBNull.Value;
                    if (!string.IsNullOrEmpty(dtJob.Rows[i]["Floors"].ToString()))
                        drJob["Floors"] = dtJob.Rows[i]["Floors"];
                    else
                        drJob["Floors"] = DBNull.Value;
                    drJob["CorTypeId"] = dtJob.Rows[i]["CorTypeId"];
                    drJob["ConfirmedByNezam"] = 0;
                    drJob["Description"] = dtJob.Rows[i]["Description"].ToString();
                    drJob["UserId"] = UserId;
                    drJob["ModifiedDate"] = DateTime.Now;
                    drJob["TableId"] = MReId;
                    drJob["TableType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
                    drJob["Type"] = 0;
                    drJob["CreateDate"] = Utility.GetDateOfToday();

                    drJob.EndEdit();
                    MeJobManager.AddRow(drJob);

                }
            }
            #endregion
            #region Madrak
            bool flag = false;
            bool HaveKardani = false;
            if (dtMadrak.Rows.Count > 0)
            {
                for (int z = 0; z < dtMadrak.Rows.Count; z++)
                {
                    DataRow drLicence = MeLiManager.NewRow();
                    drLicence.BeginEdit();
                    drLicence["LiId"] = dtMadrak.Rows[z]["LiId"];
                    drLicence["TMeId"] = MemberId;
                    drLicence["MjId"] = dtMadrak.Rows[z]["MajorId"];
                    drLicence["UnId"] = dtMadrak.Rows[z]["UniId"];
                    drLicence["UnName"] = dtMadrak.Rows[z]["UniName"];
                    drLicence["CounId"] = dtMadrak.Rows[z]["CounId"];
                    drLicence["CitId"] = dtMadrak.Rows[z]["CitId"];
                    drLicence["CitName"] = dtMadrak.Rows[z]["CityName"];
                    drLicence["Avg"] = float.Parse(dtMadrak.Rows[z]["Avg"].ToString(), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                    drLicence["NumUnit"] = dtMadrak.Rows[z]["NumUnit"];
                    drLicence["StartDate"] = dtMadrak.Rows[z]["StartDate"];
                    drLicence["EndDate"] = dtMadrak.Rows[z]["EndDate"];
                    drLicence["IsConfirm"] = 0;
                    drLicence["IsInquiry"] = 0;
                    drLicence["UserId"] = UserId;
                    drLicence["Description"] = dtMadrak.Rows[z]["Description"];

                    if ((Convert.ToInt32(dtMadrak.Rows[z]["LicenceCode"]) == (int)TSP.DataManager.Licence.Karshenasi || Convert.ToInt32(dtMadrak.Rows[z]["LicenceCode"]) == (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste || Convert.ToInt32(dtMadrak.Rows[z]["LicenceCode"]) == (int)TSP.DataManager.Licence.MoadeleKarshenasi || Convert.ToInt32(dtMadrak.Rows[z]["LicenceCode"]) == (int)TSP.DataManager.Licence.ArshadPeybaste || Convert.ToInt32(dtMadrak.Rows[z]["LicenceCode"]) == (int)TSP.DataManager.Licence.PHDPeyvaste) && !flag)
                    {
                        flag = true;
                        drLicence["DefaultValue"] = true;
                    }
                    else
                    {
                        drLicence["DefaultValue"] = false;
                    }
                    if ((Convert.ToInt32(dtMadrak.Rows[z]["LicenceCode"]) == (int)TSP.DataManager.Licence.kardani || Convert.ToInt32(dtMadrak.Rows[z]["LicenceCode"]) == (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste))
                        HaveKardani = true;
                    drLicence["Thesis"] = dtMadrak.Rows[z]["Thesis"];
                    drLicence["MReId"] = MReId;
                    drLicence["ImageURL"] = "~/image/Members/License/" + dtMadrak.Rows[z]["LicenseUrl"].ToString();
                    drLicence["ModifiedDate"] = DateTime.Now;
                    drLicence.EndEdit();
                    MeLiManager.AddRow(drLicence);
                    MeLiManager.Save();
                    MeLiManager.DataTable.AcceptChanges();
                    //InsertLicenseAttachment(AttachManager, Convert.ToInt32(MeLiManager[MeLiManager.Count - 1]["TMlId"]), dtMadrak.Rows[z]["LicenseUrl"].ToString(), dtMadrak.Rows[z]["LicenseURLName"].ToString(), UserId);
                }
            }
            else
            {
                ShowMessage("ثبت حداقل یک مدرک تحصیلی الزامی می باشد");
                transact.CancelSave();
                return;
            }

            if (HaveKardani && Session["FileOfKardani"] == null)
            {
                ShowMessage("بارگذاری تصویر استعلام عدم عضویت در کانون کاردانی الزامی است");
                transact.CancelSave();
                return;
            }
            #endregion
            #region Language
            if (dtLanguage.Rows.Count > 0)
            {
                for (int y = 0; y < dtLanguage.Rows.Count; y++)
                {
                    DataRow drLan = MeLanManager.NewRow();
                    drLan.BeginEdit();
                    drLan["LanId"] = int.Parse(dtLanguage.Rows[y]["LanId"].ToString());
                    drLan["TMeId"] = MemberId;
                    drLan["LqId"] = int.Parse(dtLanguage.Rows[y]["LqId"].ToString());
                    drLan["UserId"] = UserId;
                    drLan["Description"] = dtLanguage.Rows[y]["Description"].ToString();
                    drLan["ModifiedDate"] = DateTime.Now;
                    drLan["MReId"] = MReId;
                    drLan.EndEdit();
                    MeLanManager.AddRow(drLan);
                }
            }
            #endregion
            #region TransferMember
            if (!string.IsNullOrEmpty(dtMember.Rows[0]["TPrId"].ToString()))
            {
                //check validator
                DataRow drTransfer = transferManager.NewRow();
                drTransfer.BeginEdit();

                drTransfer["PrId"] = dtMember.Rows[0]["TPrId"];
                drTransfer["TransferDate"] = dtMember.Rows[0]["TransferDate"].ToString();
                drTransfer["TransferType"] = 1;
                drTransfer["TableId"] = MReId;
                drTransfer["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
                drTransfer["Body"] = "";
                drTransfer["IsConfirmed"] = 0;
                drTransfer["MeNo"] = dtMember.Rows[0]["TMeNo"];
                drTransfer["FileNo"] = dtMember.Rows[0]["TFileNo"];

                drTransfer["UserId"] = UserId;
                drTransfer["ModifiedDate"] = DateTime.Now;

                if (Session["LetterImg"] != null)
                {
                    transferManager.DataTable.AcceptChanges();
                    drTransfer["ImageUrl"] = "~/image/Members/Transport/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["LetterImg"].ToString());
                }

                transferManager.AddRow(drTransfer);
                transferManager.Save();

            }
            #endregion

            InsertAttachments(AttachManager, MReId, MemberId, UserId);
            MemberAtSubjManager.Save();
            MeJobManager.Save();
            MeLanManager.Save();

            if (!InsertWorkFlow(WorkFlowStateManager, MReId, MemberId, UserId))
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                transact.CancelSave();
                return;
            }
            //if (Convert.ToInt32(RadiobtnPaymentType.SelectedItem.Value) == 1)
            //{
            //ارسال به مرحله تایید کارمند واحد عضویت در نوع پرداخت همان ابتدای کار انجام پذیرد
            string Url = "<a href='../Employee/MembersRegister/MemberInsert.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";
            if (!ReqManager.DoNextTaskOfBankReply(MReId, MemberId, Url, (int)TSP.DataManager.UserType.TemporaryMembers, UserId, transact))
            {
                transact.CancelSave();
                ShowMessage("خطایی در ذخیره اطلاعات ایجاد شده است");
                return;
            }
            //}
            if (Convert.ToInt32(RadiobtnPaymentType.SelectedItem.Value) == 0 && !CheckIfMeTransfer())
            {
                if (EPaymentUC.SaveFish(transact, MReId, UserId, TSP.DataManager.EpaymentType.WizardMemberRegistration, MemberId.ToString()) <= 0)
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    transact.CancelSave();
                    return;
                }
                btnEpayment.Visible = true;
                PageMode = "SendToBank";
            }
            transact.EndSave();
            if (Convert.ToInt32(RadiobtnPaymentType.SelectedItem.Value) == 1)
            {
                ShowMessage("پیش ثبت نام با موفقیت انجام شد.جهت تکمیل روند ثبت نام با در دست داشتن مدارک ذکر شده به سازمان مراجعه نمایید.");
            }
            else
            {
                ShowMessage("ذخیره با موفقیت انجام شد.لطفا نام کاربری و رمز عبور خود رایادداشت نموده و سپس جهت تکمیل روند ثبت نام بر روی دکمه پرداخت الکترونیکی(در پایین صفحه)کلیک نمایید.در صورت عدم موفقیت به پرداخت وارد پرتال خود شده واز منوی واحد مالی>> مدیریت فیش های پرداخت نشده مجددا تلاش به پرداخت نمایید");
            }
            ViewState["Login"] = UserId;
            Session["MeIsPaid"] = true;
            SaveComplete = true;

            // if (Convert.ToInt32(RadiobtnPaymentType.SelectedItem.Value) == 1)
            DoNextTaskOfInsert(MReId, LogManager, ReqManager);
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";// +err.Message;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";// +err.Message;
            }
            return;
        }
        #region Move Images
        try
        {
            string MeID = Utility.DecryptQS(HDMemberId.Value);
            string MReId = Utility.DecryptQS(HDRequestId.Value);

            try
            {
                if (Session["FileOfMember"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfMember"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/Person/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["FileOfSign"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfSign"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/Sign/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["LetterImg"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["LetterImg"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/Transport/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["FileOfIdNo"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfIdNo"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/IdNo/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    if (Session["FileOfResident"] == null)
                    {
                        string ImgTarget2 = Server.MapPath("~/image/Members/Resident/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                        System.IO.File.Copy(ImgSoource, ImgTarget2);
                    }
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["FileOfIdNoP2"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfIdNoP2"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/IdNo/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["FileOfIdNoPDes"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfIdNoPDes"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/IdNo/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["FileOfSSN"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfSSN"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/SSN/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["FileOfSSNBack"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfSSNBack"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/SSN/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["FileOfSol"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfSol"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/Soldier/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
            try
            {
                if (Session["FileOfSolBack"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfSolBack"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/Soldier/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }
            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }

            ////////////////////try
            ////////////////////{
            ////////////////////    if (dtMadrak.Rows.Count > 0)
            ////////////////////    {
            ////////////////////        for (int z = 0; z < dtMadrak.Rows.Count; z++)
            ////////////////////        {
            ////////////////////            String LicenseUrl = dtMadrak.Rows[z]["LicenseUrl"].ToString();
            ////////////////////            if (String.IsNullOrEmpty(LicenseUrl) == false)
            ////////////////////            {
            ////////////////////                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(LicenseUrl);
            ////////////////////                string ImgTarget = Server.MapPath("~/Image/Members/License/") + Path.GetFileName(LicenseUrl);
            ////////////////////                System.IO.File.Move(ImgSoource, ImgTarget);
            ////////////////////            }
            ////////////////////        }
            ////////////////////    }
            ////////////////////}
            ////////////////////catch (Exception ex) { Utility.SaveWebsiteError(ex); }

            try
            {
                if (Session["FileOfResident"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["FileOfResident"].ToString());
                    string ImgTarget = Server.MapPath("~/image/Members/Resident/Request/") + MReId + "_" + System.IO.Path.GetFileName(ImgSoource);
                    System.IO.File.Move(ImgSoource, ImgTarget);
                }

            }
            catch (Exception ex) { Utility.SaveWebsiteError(ex); }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        #endregion
        if (SaveComplete)
            ClearSessions();
        if (Convert.ToInt32(RadiobtnPaymentType.SelectedItem.Value) == 0)
            ViewState["BankURL"] = "~/Epayment/Epayment.aspx?InvoiceNo=" + Utility.EncryptQS(EPaymentUC.InvoiceNumber.ToString()) + "&TMeId=" + Utility.EncryptQS(MemberId.ToString());
        //////ViewState["BankURL"] = EPaymentUC.BankURL;
        //////    Response.Redirect(EPaymentUC.BankURL, false);
    }

    protected void InsertAttachments(TSP.DataManager.AttachmentsManager AttachManager, int MReId, int MeId, int UserId)
    {
        if (Session["FileOfIdNo"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();
            drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drAtt["RefTable"] = MReId;
            drAtt["AttId"] = (int)TSP.DataManager.AttachType.IdNo;
            drAtt["IsValid"] = 1;
            drAtt["FilePath"] = "~/image/Members/IdNo/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfIdNo"].ToString());
            drAtt["FileName"] = MeId;
            drAtt["Description"] = DBNull.Value;
            drAtt["UserId"] = UserId;
            drAtt["ModfiedDate"] = DateTime.Now;
            AttachManager.AddRow(drAtt);
            AttachManager.Save();
            AttachManager.DataTable.AcceptChanges();
        }
        if (Session["FileOfIdNoP2"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();
            drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drAtt["RefTable"] = MReId;
            drAtt["AttId"] = (int)TSP.DataManager.AttachType.IdNoP2;
            drAtt["IsValid"] = 1;
            drAtt["FilePath"] = "~/image/Members/IdNo/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfIdNoP2"].ToString());
            drAtt["FileName"] = MeId;
            drAtt["Description"] = DBNull.Value;
            drAtt["UserId"] = UserId;
            drAtt["ModfiedDate"] = DateTime.Now;
            AttachManager.AddRow(drAtt);
            AttachManager.Save();
            AttachManager.DataTable.AcceptChanges();
        }
        if (Session["FileOfIdNoPDes"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();
            drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drAtt["RefTable"] = MReId;
            drAtt["AttId"] = (int)TSP.DataManager.AttachType.IdNoPDes;
            drAtt["IsValid"] = 1;
            drAtt["FilePath"] = "~/image/Members/IdNo/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfIdNoPDes"].ToString());
            drAtt["FileName"] = MeId;
            drAtt["Description"] = DBNull.Value;
            drAtt["UserId"] = UserId;
            drAtt["ModfiedDate"] = DateTime.Now;
            AttachManager.AddRow(drAtt);
            AttachManager.Save();
            AttachManager.DataTable.AcceptChanges();
        }
        if (Session["FileOfSSN"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();
            drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drAtt["RefTable"] = MReId;
            drAtt["AttId"] = (int)TSP.DataManager.AttachType.SSN;
            drAtt["IsValid"] = 1;
            drAtt["FilePath"] = "~/image/Members/SSN/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfSSN"].ToString());
            drAtt["FileName"] = MeId;
            drAtt["Description"] = DBNull.Value;
            drAtt["UserId"] = UserId;
            drAtt["ModfiedDate"] = DateTime.Now;
            AttachManager.AddRow(drAtt);
            AttachManager.Save();
            AttachManager.DataTable.AcceptChanges();
        }
        if (Session["FileOfSSNBack"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();
            drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drAtt["RefTable"] = MReId;
            drAtt["AttId"] = (int)TSP.DataManager.AttachType.SSNBack;
            drAtt["IsValid"] = 1;
            drAtt["FilePath"] = "~/image/Members/SSN/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfSSNBack"].ToString());
            drAtt["FileName"] = MeId;
            drAtt["Description"] = DBNull.Value;
            drAtt["UserId"] = UserId;
            drAtt["ModfiedDate"] = DateTime.Now;
            AttachManager.AddRow(drAtt);
            AttachManager.Save();
            AttachManager.DataTable.AcceptChanges();
        }
        if (Session["FileOfSol"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();
            drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drAtt["RefTable"] = MReId;
            drAtt["AttId"] = (int)TSP.DataManager.AttachType.SoldierCard;
            drAtt["IsValid"] = 1;
            drAtt["FilePath"] = "~/image/Members/Soldier/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfSol"].ToString());
            drAtt["FileName"] = MeId;
            drAtt["Description"] = DBNull.Value;
            drAtt["UserId"] = UserId;
            drAtt["ModfiedDate"] = DateTime.Now;
            AttachManager.AddRow(drAtt);
            AttachManager.Save();
            AttachManager.DataTable.AcceptChanges();
        }
        if (Session["FileOfSolback"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();
            drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drAtt["RefTable"] = MReId;
            drAtt["AttId"] = (int)TSP.DataManager.AttachType.SoldierCardBack;
            drAtt["IsValid"] = 1;
            drAtt["FilePath"] = "~/image/Members/Soldier/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfSolBack"].ToString());
            drAtt["FileName"] = MeId;
            drAtt["Description"] = DBNull.Value;
            drAtt["UserId"] = UserId;
            drAtt["ModfiedDate"] = DateTime.Now;
            AttachManager.AddRow(drAtt);
            AttachManager.Save();
            AttachManager.DataTable.AcceptChanges();
        }
        if (Session["FileOfResident"] != null)
        {
            DataRow drAtt = AttachManager.NewRow();
            drAtt["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            drAtt["RefTable"] = MReId;
            drAtt["AttId"] = (int)TSP.DataManager.AttachType.ResidentDoc;//---مدرک سکونت حداقل شش ماه در استان
            drAtt["IsValid"] = 1;
            drAtt["FilePath"] = "~/image/Members/Resident/Request/" + MReId.ToString() + "_" + System.IO.Path.GetFileName(Session["FileOfResident"].ToString());
            drAtt["FileName"] = MeId;
            drAtt["Description"] = DBNull.Value;
            drAtt["UserId"] = UserId;
            drAtt["ModfiedDate"] = DateTime.Now;
            AttachManager.AddRow(drAtt);
            AttachManager.Save();
            AttachManager.DataTable.AcceptChanges();
        }
    }  

    private Boolean InsertWorkFlow(TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, int TableId, int NmcId, int UserId)
    {
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        int StartWF = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, NmcId, UserId, (int)TSP.DataManager.WorkFlowStateNmcIdType.TempMember, "شروع گردش کار عضویت در سازمان توسط عضو");
        if (StartWF <= 0)
        {
            return false;
        }
        WorkFlowStateManager.DataTable.AcceptChanges();
        return true;
    }
    #endregion

    int FindMaxLiId(out int Index)
    {
        dtMadrak = (DataTable)Session["TblOfMadrak"];
        Index = -1;
        int Max = 0;
        for (int i = 0; i < dtMadrak.Rows.Count; i++)
        {
            //DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
            if (Max < int.Parse(dtMadrak.Rows[i]["LiId"].ToString()))
            {
                Max = int.Parse(dtMadrak.Rows[i]["LiId"].ToString());
                Index = i;
            }
        }
        return Max;
    }

    int FindMaxLiId()
    {
        int Index;
        return FindMaxLiId(out Index);
    }

    void ClearSessions()
    {
        Session["TblActivity"] =
        Session["TblActivity2"] =
        Session["TblResearch"] =
        Session["TblJob"] =
        Session["TblOfMadrak"] =
        Session["TblLanguage"] =
        Session["FileOfMember"] =
        Session["LetterImg"] =
        Session["Member"] =
        Session["FileOfSol"] =
        Session["FileOfSign"] =

        Session["FileOfIdNo"] =
        Session["FileOfIdNoP2"] =
        Session["FileOfIdNoPDes"] =
        Session["FileOfSSN"] =
        Session["FileOfSSNBack"] =
        Session["FileOfSol"] =
        Session["FileOfSolBack"] =
        Session["MemberMembership"] =
        Session["MemberSummary"] =
        Session["FileOfResident"] =
        Session["FileOfKardani"] = null;
    }

    int FindMaxLicenceTiid()
    {
        dtMadrak = (DataTable)Session["TblOfMadrak"];
        int Max = 0;
        int Tiid = -1;
        for (int i = 0; i < dtMadrak.Rows.Count; i++)
        {
            if (Max < int.Parse(dtMadrak.Rows[i]["LicenceCode"].ToString()))
            {
                Max = int.Parse(dtMadrak.Rows[i]["LicenceCode"].ToString());
                Tiid = int.Parse(dtMadrak.Rows[i]["Tiid"].ToString()); 
            }
        }
        return Tiid;
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    Boolean SendEmail(String Email, String FollowCode, String FullName, String Username, String Password)
    {
        Utility.Notifications Notification = new Utility.Notifications(Utility.Notifications.SendTypes.Email, Utility.Notifications.NotificationTypes.MemberRegisterData);

        DataRow dr = Notification.NotificationData.NewRow();
        dr["EmailAddress"] = Email;
        dr["FollowCode"] = FollowCode;
        dr["FullName"] = FullName;
        dr["Username"] = Username;
        dr["Password"] = Password;
        Notification.NotificationData.Rows.Add(dr);
        Notification.NotificationData.AcceptChanges();

        return Notification.Send();
    }

    private Boolean CheckIfMeTransfer()
    {
        if (Session["Member"] == null)
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است و یا پس از ورود به صفحه بانک از دکمه بازگشت مرورگر استفاده نموده اید.مجدداً اقدام نمایید");
            return false;
        }
        dtMember = (DataTable)Session["Member"];
        if (dtMember.Rows.Count == 0)
        {
            ShowMessage("اطلاعات وارد شده ناقص می باشد.");
            return false;
        }
        if (!string.IsNullOrEmpty(dtMember.Rows[0]["TPrId"].ToString()))
        {
            return true;
        }
        return false;
    }

    #endregion
}
