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

public partial class Members_Amoozesh_SeminarEPayment : System.Web.UI.Page
{
    string Amount;
    string SeId;
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

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
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["Amount"]) || string.IsNullOrEmpty(Request.QueryString["SeId"]))
            {
                Response.Redirect("PeriodRegisterHome.aspx");
                return;
            }

            SetKeys();
            CheckAccounts();
            Payment();

            this.ViewState["BtnFinish"] = btnFinish.Enabled;

        }
        if (this.ViewState["BtnFinish"] != null)
            this.btnFinish.Enabled = this.btnFinish2.Enabled = (bool)this.ViewState["BtnFinish"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PeriodRegisterHome.aspx");
    }

    private void Insert()
    {
        if (IsPageRefresh)
            return;
        int SeId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["SeId"].ToString()));

        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager=new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();

        TSP.DataManager.AccountingSettingsManager SettingsManager = new TSP.DataManager.AccountingSettingsManager();
        TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;
        if (MemberManager == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }


        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        TSP.DataManager.AccountingDocument Document = new TSP.DataManager.AccountingDocument(transact, Utility.GetCurrentUser_AgentId(), -1, -1);
        TSP.DataManager.AccountingDocument Document2 = new TSP.DataManager.AccountingDocument(transact, Utility.GetCurrentUser_AgentId(), -1, -1);

        transact.Add(PeriodRegisterManager);
        transact.Add(SeminarManager);
        transact.Add(SettingsManager);
        transact.Add(InstitueManager);
        transact.Add(MemberManager);

        try
        {
            transact.BeginSave();

            //long result = CheckPayment();
            //if (result == (long)Session["MePrice"])
            {
                InsertPeriodRegister(PeriodRegisterManager, SeminarManager);
                Document.AddDetails("Bed", Convert.ToDecimal(ASPxTextBoxTotalAmount.Text), GetMemberAccId(MemberManager), GetDescription());
                Document.AddDetails("Bes", GetInstituteAmount(), GetInstituteAccId(InstitueManager, SeminarManager), GetDescription());
                Document.AddDetails("Bes", GetTrainingAmount(), GetTrainingEarningsAccId(SettingsManager), GetDescription());
                Document.Save(GetDescription(), Convert.ToInt32(PeriodRegisterManager[0]["PRId"]), "", TSP.DataManager.AccountingTT.SeminarRegistration, Utility.GetCurrentUser_UserId());

                Document2.Insert(GetMainBankAccId(SettingsManager), GetMemberAccId(MemberManager), Convert.ToDecimal(ASPxTextBoxTotalAmount.Text), GetDescription2(), Convert.ToInt32(PeriodRegisterManager[0]["PRId"]), this.Request["referenceId"], TSP.DataManager.AccountingTT.SeminarRegistration, Utility.GetCurrentUser_UserId());

                transact.EndSave();

                btnFinish.PostBackUrl = "";
                btnFinish2.PostBackUrl = "";
                Session["MeIsPaid"] = true;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پرداخت با موفقیت انجام شد.";
            }
            //else
            //{
            //    transact.CancelSave();
            //    Func_Payment();
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = TSP.Utility.OnlinePayment.PaymentTransactionConfirmErrorCode(Convert.ToInt32(result));

            //}
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Func_Payment();
            SetError(err);
        }
    }

    private void InsertPeriodRegister(TSP.DataManager.PeriodRegisterManager PeriodRegisterManager,TSP.DataManager.SeminarManager SeminarManager)
    {
        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        try
        {
            int SeId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["SeId"].ToString()));
            SeminarManager.FindByCode(SeId);
            if (SeminarManager.Count == 1)
            {
                Capacity = int.Parse(SeminarManager[0]["Capacity"].ToString());
                RegisterCount = int.Parse(SeminarManager[0]["CountRegister"].ToString());
                RemainCapacity = Capacity - RegisterCount;
                if (RemainCapacity > 0)
                {
                    DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();
                    PeriodRegisterRow["PPId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["SeId"].ToString()));
                    if (!string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
                    {
                        PeriodRegisterRow["MeId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["MeId"].ToString()));
                    }
                    else
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    PeriodRegisterRow["PaymentType"] = 3;
                    PeriodRegisterRow["IsMember"] = 1;
                    PeriodRegisterRow["IsSeminar"] = 1;
                    PeriodRegisterRow["RegisterType"] = 0;
                    PeriodRegisterRow["InActive"] = 0;
                    PeriodRegisterRow["RegisterDate"] = Utility.GetDateOfToday();
                    PeriodRegisterRow["UserId"] = Utility.GetCurrentUser_UserId();
                    PeriodRegisterRow["ModifiedDate"] = DateTime.Now;

                    PeriodRegisterManager.AddRow(PeriodRegisterRow);

                    int cn = PeriodRegisterManager.Save();
                    if (cn > 0)
                    {
                        // RoundPanelSeminar.HeaderText = "مشاهده";
                        //HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("Edit");
                        //HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());
                        //TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)int.Parse(Session["LoginType"].ToString()));
                        //btnSave2.Enabled = per.CanEdit;
                        //btnSave.Enabled = per.CanEdit;
                        // btnInActive.Enabled = per.CanEdit;
                        // btnInActive2.Enabled = per.CanEdit;
                        //this.ViewState["BtnSave"] = btnSave.Enabled;
                        // this.ViewState["btnInActive"] = btnInActive.Enabled;

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
                }
            }
        }
        catch (Exception err)
        {
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
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }


    /*************************************************************************************************************/
    private int GetMainBankAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.MainBank.ToString(), Utility.GetMainAgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private int GetTrainingEarningsAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.TrainingEarnings.ToString(), Utility.GetMainAgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private int GetInstituteAccId(TSP.DataManager.InstitueManager InstitueManager, TSP.DataManager.SeminarManager SeminarManager)
    {
        int SeId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["SeId"].ToString()));
        SeminarManager.FindByCode(SeId);
        if (SeminarManager.Count > 0)
        {
            InstitueManager.FindByCode(Convert.ToInt32(SeminarManager[0]["InsId"]));
            if (InstitueManager.Count > 0)
                return Convert.ToInt32(InstitueManager[0]["AccId"]);
        }
        return -1;
    }

    private int GetMemberAccId(TSP.DataManager.MemberManager MemberManager)
    {
       // MemberManager.FindByCode(Convert.ToInt32(txtMeNo.Text));
        if (MemberManager.Count > 0)
            return Convert.ToInt32(MemberManager[0]["AccId"]);
        else
            return -1;
    }

    private decimal GetInstituteAmount()
    {
        decimal Amount = Convert.ToDecimal(ASPxTextBoxTotalAmount.Text);
        //return Amount * 85 / 100;
        return Math.Round(Amount * 85 / 100);
    }

    private decimal GetTrainingAmount()
    {
        decimal Amount = Convert.ToDecimal(ASPxTextBoxTotalAmount.Text);
        //return Amount * 15 / 100;
        return Amount - Math.Round(Amount * 85 / 100);
    }

    private void SetFileds()
    {
        Amount = Utility.DecryptQS(HFAmount.Value);
        ASPxTextBoxTotalAmount.Text = Convert.ToDecimal(Amount).ToString("#,#");
        PaymentDate.DateValue = DateTime.Now;

        int MeId = Utility.GetCurrentUser_MeId();
        txtMeNo.Text = MeId.ToString();
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;
        if (MemberManager == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

       // MemberManager.FindByCode(Convert.ToInt32(txtMeNo.Text));
        if (MemberManager.Count > 0)
        {
            txtMeFirstName.Text = MemberManager[0]["FirstName"].ToString();
            txtMeLastName.Text = MemberManager[0]["LastName"].ToString();
        }

        SeId = Utility.DecryptQS(HiddenFieldCourseRegister["SeId"].ToString());
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        SeminarManager.FindByCode(Convert.ToInt32(SeId));
        if (SeminarManager.Count > 0)
        {
            StartDate.Text = SeminarManager[0]["StartDate"].ToString();
            ASPxTextBoxSeminarTitle.Text = SeminarManager[0]["Subject"].ToString();
        }
    }

    private string GetDescription()
    {
        string Des = "ثبت نام عضو شماره " + txtMeNo.Text + " در سمینار " + ASPxTextBoxSeminarTitle.Text + " تاریخ برگزاری " + StartDate.Text + " در تاریخ " + PaymentDate.Text;
        return Des;
    }

    private string GetDescription2()
    {
        string Des = "شماره فیش " + this.Request["referenceId"] + " به نام آقای/خانم " + txtMeFirstName.Text + " " + txtMeLastName.Text + " به شماره عضویت " + txtMeNo.Text + " جهت ثبت نام در سمینار " + ASPxTextBoxSeminarTitle.Text + " در تاریخ " + PaymentDate.Text;
        return Des;
    }

    private void CheckAccounts()
    {
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        TSP.DataManager.AccountingSettingsManager SettingsManager = new TSP.DataManager.AccountingSettingsManager();
        TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;
        if (MemberManager == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        int MemberAccId = -1, InstitueAccId = -1, MainBankAccId = -1, TrainingEarningsAccId = -1;

        MemberAccId = GetMemberAccId(MemberManager);
        InstitueAccId = GetInstituteAccId(InstitueManager, SeminarManager);
        MainBankAccId = GetMainBankAccId(SettingsManager);
        TrainingEarningsAccId = GetTrainingEarningsAccId(SettingsManager);

        if (MemberAccId == -1 || InstitueAccId == -1 || MainBankAccId == -1 || TrainingEarningsAccId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "در حال حاضر امکان ثبت نام وجود ندارد";
            btnFinish.Enabled = false;
            btnFinish2.Enabled = false;
        }
    }

    /*************************************************************************************************************/
    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات وابسته معتبر نمی باشد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private void SetKeys()
    {
        try
        {
            HiddenFieldCourseRegister["SeId"] = null;

            if (!(string.IsNullOrEmpty(Request.QueryString["Amount"])))
                HFAmount.Value = Server.HtmlDecode(Request.QueryString["Amount"].ToString());
            else
                HFAmount.Value = Session["Amount"].ToString();

            if (!(string.IsNullOrEmpty(Request.QueryString["SeId"])))
                HiddenFieldCourseRegister["SeId"] = Server.HtmlDecode(Request.QueryString["SeId"]).ToString();
            else
                HiddenFieldCourseRegister["SeId"] = Session["SeId"].ToString();
            
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        Amount = Utility.DecryptQS(HFAmount.Value);
        SeId = Utility.DecryptQS(HiddenFieldCourseRegister["SeId"].ToString());

        if (string.IsNullOrEmpty(Amount) || string.IsNullOrEmpty(SeId) || Utility.GetCurrentUser_MeId() == -1)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        SetFileds();
    }

    /************************************************** Payment ******************************************************/
    private void Payment()
    {
        if (!Utility.IsDBNullOrNullValue(Session["MeIsPaid"]) && Convert.ToBoolean(Session["MeIsPaid"]) == true)
        {
            btnFinish.Enabled = false;
            btnFinish2.Enabled = false;
        }
        else
        {
            if (Session["MeIsPaid"] == null || Convert.ToBoolean(Session["MeIsPaid"]) == false)
            {
                if (string.IsNullOrEmpty(this.Request["paymentId"]) && string.IsNullOrEmpty(this.Request["resultCode"]))//Next
                {
                    SetSessions();
                    Func_Payment();
                }
                else//Bank
                {
                    SetPayed();
                    if (this.Request["resultCode"] == TSP.Utility.OnlinePayment.PaymentSuccessCode.ToString() && !string.IsNullOrEmpty(this.Request["referenceId"]))
                    {
                        btnFinish.Enabled = false;
                        btnFinish2.Enabled = false;
                        Insert();

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = TSP.Utility.OnlinePayment.PaymentResultCode(Convert.ToInt32(this.Request["resultCode"]));
                        Func_Payment();
                    }
                }
            }
            else
            {
                btnFinish.PostBackUrl = "";
                btnFinish2.PostBackUrl = "";
            }
        }
        this.ViewState["BtnFinish"] = btnFinish.Enabled;
    }

    protected void Func_Payment()
    {
        btnFinish.PostBackUrl = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress();//"../../TestEPayment.aspx"; 
        btnFinish2.PostBackUrl = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress();
        Utility.KeyGenerator grkey = new Utility.KeyGenerator();
        Session["MePaymentId"] = grkey.Generate(12, Utility.KeyGenerator.CharacterTypes.Numbers);
        if (ASPxTextBoxTotalAmount.Text != "")
            Session["MePrice"] = Int64.Parse(ASPxTextBoxTotalAmount.Text, System.Globalization.NumberStyles.Number);
        else
            Session["MePrice"] = 0;
    }

    protected long CheckPayment()
    {
        net.sabapardazesh.pg.verifyRequest verify = new net.sabapardazesh.pg.verifyRequest();
        net.sabapardazesh.pg.MerchantService merchant = new net.sabapardazesh.pg.MerchantService();

        verify.merchantId = TSP.Utility.OnlinePayment.GetNezamMerchantId();
        verify.referenceNumber = this.Request["referenceId"];
        long lresult = merchant.verify(verify);

        return lresult;
    }

    private void SetPayed()
    {
        if (Session["Amount"] != null && Session["SeId"] != null && Session["RegisterType"] != null)
        {
            Amount = Session["Amount"].ToString();
            HFAmount.Value = Utility.EncryptQS(Amount);

            SeId = Session["SeId"].ToString();
            HiddenFieldCourseRegister["SeId"] = SeId;            

            btnFinish.Enabled = false;
            btnFinish2.Enabled = false;
            SetFileds();
        }
    }

    private void SetSessions()
    {
        Session["Amount"] = ASPxTextBoxTotalAmount.Text;
        Session["SeId"] = HiddenFieldCourseRegister["SeId"].ToString();
    }
}