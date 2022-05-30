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

public partial class Members_Amoozesh_PeriodEPayment : System.Web.UI.Page
{
    string Amount;
    string PPId;
    string RegisterType;
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {

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
            if (string.IsNullOrEmpty(Request.QueryString["Amount"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]) || string.IsNullOrEmpty(Request.QueryString["RegisterType"]))
            {
                Response.Redirect("PeriodRegisterHome.aspx");
                return;
            }            

            SetKeys();
            //CheckAccounts();
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
        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));

        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.PeriodOpinionManager PeriodOpinionManager = new TSP.DataManager.PeriodOpinionManager();

        TSP.DataManager.AccountingSettingsManager SettingsManager=new TSP.DataManager.AccountingSettingsManager();
        TSP.DataManager.InstitueManager InstitueManager=new TSP.DataManager.InstitueManager();
       // TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;//new TSP.DataManager.MemberManager();
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
        transact.Add(PeriodPresentManager);
        transact.Add(PeriodOpinionManager);
        transact.Add(SettingsManager);
        transact.Add(InstitueManager);
        transact.Add(MemberManager);

        try
        {
            transact.BeginSave();

            long result = CheckPayment();
            if (result == (long)Session["MePrice"])
            {
                InsertPeriodRegister(PeriodRegisterManager, PeriodPresentManager, PeriodOpinionManager);
                Document.AddDetails("Bed", Convert.ToDecimal(txtTotalAmount.Text), GetMemberAccId(MemberManager), GetDescription());
                Document.AddDetails("Bes", GetInstituteAmount(), GetInstituteAccId(InstitueManager, PeriodPresentManager), GetDescription());
                Document.AddDetails("Bes", GetTrainingAmount(), GetTrainingEarningsAccId(SettingsManager), GetDescription());
                Document.Save(GetDescription(), Convert.ToInt32(PeriodRegisterManager[0]["PRId"]), "", TSP.DataManager.AccountingTT.PeriodRegistration, Utility.GetCurrentUser_UserId());

                Document2.Insert(GetMainBankAccId(SettingsManager), GetMemberAccId(MemberManager), Convert.ToDecimal(txtTotalAmount.Text), GetDescription2(), Convert.ToInt32(PeriodRegisterManager[0]["PRId"]), this.Request["referenceId"], TSP.DataManager.AccountingTT.PeriodRegistration, Utility.GetCurrentUser_UserId());
                
                transact.EndSave();

                btnFinish.PostBackUrl = "";
                btnFinish2.PostBackUrl = "";
                Session["MeIsPaid"] = true;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پرداخت با موفقیت انجام شد.";
            }
            else
            {
                transact.CancelSave();
                Func_Payment();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = TSP.Utility.OnlinePayment.PaymentTransactionConfirmErrorCode(Convert.ToInt32(result));

            }
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Func_Payment();
            SetError(err);
        }
    }

    private void InsertPeriodRegister(TSP.DataManager.PeriodRegisterManager PeriodRegisterManager, TSP.DataManager.PeriodPresentManager PeriodPresentManager, TSP.DataManager.PeriodOpinionManager PeriodOpinionManager)
    {
        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        //string TodayDate = Utility.GetDateOfToday();
        string EndRegisterDate = "";
        string PeriodTestDate = "";
        int MeId = -1;
        RegisterType = Utility.DecryptQS(HFRegisterType.Value);

        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {

            DataTable dtPPOpinion = PeriodOpinionManager.SelectPeriodOpinionByType(PPId, 2);
            if (dtPPOpinion.Rows.Count > 0)
            {
                int OpinionEmpId = -1;
                int OpinionUltId = -1;
                for (int i = 0; i < dtPPOpinion.Rows.Count; i++)
                {
                    OpinionEmpId = int.Parse(dtPPOpinion.Rows[i]["EmpId"].ToString());
                    OpinionUltId = int.Parse(dtPPOpinion.Rows[i]["UltId"].ToString());
                    if (OpinionUltId == 1)
                    {
                        if (OpinionEmpId == MeId)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ثبت نام برای عضو انتخاب شده وجود ندارد.عضو انتخابی بازرس دوره می باشد.";
                            return;
                        }
                    }
                }
            }

            Capacity = int.Parse(PeriodPresentManager[0]["Capacity"].ToString());
            RegisterCount = int.Parse(PeriodPresentManager[0]["CountRegister"].ToString());
            PeriodTestDate = PeriodPresentManager[0]["TestDate"].ToString();
            EndRegisterDate = PeriodPresentManager[0]["EndRegisterDate"].ToString();
            int IsEndDate = string.Compare(EndRegisterDate, Utility.GetDateOfToday());
            int IsTestDate = string.Compare(PeriodTestDate, Utility.GetDateOfToday());
            if (IsEndDate > 0)
            {
                RemainCapacity = Capacity - RegisterCount;
                if (RemainCapacity > 0)
                {
                    DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();
                    PeriodRegisterRow["PPId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
                    PeriodRegisterRow["MeId"] = MeId;
                    PeriodRegisterRow["PaymentType"] = 3;
                    PeriodRegisterRow["IsMember"] = 1;
                    PeriodRegisterRow["InActive"] = 0;
                    PeriodRegisterRow["IsSeminar"] = 0;
                    PeriodRegisterRow["RegisterType"] = RegisterType;
                    PeriodRegisterRow["RegisterDate"] = Utility.GetDateOfToday();
                    PeriodRegisterRow["UserId"] = (int)Session["Login"];
                    PeriodRegisterRow["ModifiedDate"] = DateTime.Now;

                    PeriodRegisterManager.AddRow(PeriodRegisterRow);

                    int cn = PeriodRegisterManager.Save();
                    if (cn > 0)
                    {
                        // RoundPanelPeriodRegister.HeaderText = "مشاهده";
                        //HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("Edit");
                        //HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());
                        

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
            else
            {
                if ((IsEndDate < 0) && (IsTestDate > 0) && (Convert.ToInt32(RegisterType) == 1))
                {
                    RemainCapacity = Capacity - RegisterCount;
                    if (RemainCapacity > 0)
                    {
                        DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();
                        PeriodRegisterRow["PPId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
                        if (Utility.GetCurrentUser_MeId() != -1)
                        {
                            PeriodRegisterRow["MeId"] = Utility.GetCurrentUser_MeId();
                        }
                        else
                        {
                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                            return;
                        }
                        PeriodRegisterRow["PaymentType"] = 3;
                        PeriodRegisterRow["IsMember"] = 1;
                        PeriodRegisterRow["InActive"] = 0;
                        PeriodRegisterRow["IsSeminar"] = 0;
                        PeriodRegisterRow["RegisterType"] = RegisterType;
                        PeriodRegisterRow["RegisterDate"] = Utility.GetDateOfToday();
                        PeriodRegisterRow["UserId"] = (int)Session["Login"];
                        PeriodRegisterRow["ModifiedDate"] = DateTime.Now;

                        PeriodRegisterManager.AddRow(PeriodRegisterRow);

                        int cn = PeriodRegisterManager.Save();
                        if (cn > 0)
                        {
                            //// RoundPanelPeriodRegister.HeaderText = "مشاهده";
                            //HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("Edit");
                            //HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());
                            
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
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "در طی برگزاری دوره تنها قادر به ثبت نام در آزمون دوره می باشید.";
                }
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

    private int GetInstituteAccId(TSP.DataManager.InstitueManager InstitueManager, TSP.DataManager.PeriodPresentManager PeriodPresentManager)
    {
        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count > 0)
        {
            InstitueManager.FindByCode(Convert.ToInt32(PeriodPresentManager[0]["InsId"]));
            if (InstitueManager.Count > 0 && !Utility.IsDBNullOrNullValue(InstitueManager[0]["AccId"]))
                return Convert.ToInt32(InstitueManager[0]["AccId"]);
        }        
        return -1;
    }

    private int GetMemberAccId(TSP.DataManager.MemberManager MemberManager)
    {
      //  MemberManager.FindByCode(Convert.ToInt32(txtMeNo.Text));
        if (MemberManager.Count > 0)
            return Convert.ToInt32(MemberManager[0]["AccId"]);
        else
            return -1;
    }

    private decimal GetInstituteAmount()
    {
        decimal Amount = Convert.ToDecimal(txtTotalAmount.Text);
        //return Amount * 85 / 100;
        return Math.Round(Amount * 85 / 100);
    }

    private decimal GetTrainingAmount()
    {
        decimal Amount = Convert.ToDecimal(txtTotalAmount.Text);
        //return Amount * 15 / 100;
        return Amount - Math.Round(Amount * 85 / 100);
    }

    private void SetFileds()
    {
        Amount = Utility.DecryptQS(HFAmount.Value);
        txtTotalAmount.Text = Convert.ToDecimal(Amount).ToString("#,#");
        PaymentDate.DateValue = DateTime.Now;

        int MeId = Utility.GetCurrentUser_MeId();
        txtMeNo.Text = MeId.ToString();
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;//new TSP.DataManager.MemberManager();
        if (MemberManager == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

      //  MemberManager.FindByCode(Convert.ToInt32(txtMeNo.Text));
        if (MemberManager.Count > 0)
        {
            txtMeFirstName.Text = MemberManager[0]["FirstName"].ToString();
            txtMeLastName.Text = MemberManager[0]["LastName"].ToString();
        }

        PPId = Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString());
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(Convert.ToInt32(PPId));
        if (PeriodPresentManager.Count > 0)
        {
            StartDate.Text = PeriodPresentManager[0]["StartDate"].ToString();
            TSP.DataManager.CourseManager CourseManager = new TSP.DataManager.CourseManager();
            CourseManager.FindByCode(Convert.ToInt32(PeriodPresentManager[0]["CrsId"]));
            if (CourseManager.Count > 0)
                ASPxTextBoxPeriodTitle.Text = CourseManager[0]["CrsName"].ToString();
        }
    }

    private string GetDescription()
    {
        string Des = "ثبت نام عضو شماره " + txtMeNo.Text + " در دوره آموزشی " + ASPxTextBoxPeriodTitle.Text + " تاریخ برگزاری " + StartDate.Text + " در تاریخ " + PaymentDate.Text;
        return Des;
    }

    private string GetDescription2()
    {
        string Des = "شماره فیش " + this.Request["referenceId"] + " به نام آقای/خانم " + txtMeFirstName.Text + " " + txtMeLastName.Text + " به شماره عضویت " + txtMeNo.Text + " جهت ثبت نام در دوره " + ASPxTextBoxPeriodTitle.Text + " در تاریخ " + PaymentDate.Text;
        return Des;
    }

    private void CheckAccounts()
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.AccountingSettingsManager SettingsManager = new TSP.DataManager.AccountingSettingsManager();
        TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;//new TSP.DataManager.MemberManager();
        if (MemberManager == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }


        int MemberAccId = -1, InstitueAccId = -1,MainBankAccId = -1, TrainingEarningsAccId = -1;

        MemberAccId = GetMemberAccId(MemberManager);
        InstitueAccId = GetInstituteAccId(InstitueManager,PeriodPresentManager);
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
            HiddenFieldCourseRegister["PPId"] = null;

            if (!(string.IsNullOrEmpty(Request.QueryString["Amount"])))
                HFAmount.Value = Server.HtmlDecode(Request.QueryString["Amount"].ToString());
            else
                HFAmount.Value = Session["Amount"].ToString();

            if (!(string.IsNullOrEmpty(Request.QueryString["PPId"])))
                HiddenFieldCourseRegister["PPId"] = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
            else
                HiddenFieldCourseRegister["PPId"] = Session["PPId"].ToString();

            if(!string.IsNullOrEmpty(Request.QueryString["RegisterType"]))
                HFRegisterType.Value = Server.HtmlDecode(Request.QueryString["RegisterType"].ToString());
            else
                HFRegisterType.Value = Session["RegisterType"].ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        Amount = Utility.DecryptQS(HFAmount.Value);
        PPId = Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString());
        RegisterType = Utility.DecryptQS(HFRegisterType.Value);

        if (string.IsNullOrEmpty(Amount) || string.IsNullOrEmpty(PPId) || Utility.GetCurrentUser_MeId() == -1 || string.IsNullOrEmpty(RegisterType))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        SetFileds();
    }

    /************************************************** Payment ******************************************************/
    private void Payment()
    {
        //*************اگر یک دور پرداخت شده باشد
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
                    SetSessions();//*****اطلاعات دوره را تنظیم می کند
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
        btnFinish.PostBackUrl = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress();//"Test.aspx" 
        btnFinish2.PostBackUrl = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress(); //"Test.aspx";
        Utility.KeyGenerator grkey = new Utility.KeyGenerator();
        Session["MePaymentId"] = grkey.Generate(12, Utility.KeyGenerator.CharacterTypes.Numbers);
        if (txtTotalAmount.Text != "")
            Session["MePrice"] = Int64.Parse(txtTotalAmount.Text, System.Globalization.NumberStyles.Number);
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
        if (Session["Amount"] != null && Session["PPId"] != null && Session["RegisterType"]!=null)
        {
            Amount = Session["Amount"].ToString();
            HFAmount.Value = Utility.EncryptQS(Amount);

            PPId = Session["PPId"].ToString();
            HiddenFieldCourseRegister["PPId"] = PPId;

            RegisterType = Session["RegisterType"].ToString();
            HFRegisterType.Value = RegisterType;

            btnFinish.Enabled = false;
            btnFinish2.Enabled = false;
            SetFileds();
        }
    }

    private void SetSessions()
    {
        Session["Amount"] = txtTotalAmount.Text;
        Session["PPId"]= HiddenFieldCourseRegister["PPId"].ToString();
        Session["RegisterType"] = HFRegisterType.Value;
    }
}