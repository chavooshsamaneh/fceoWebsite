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

public partial class Employee_Amoozesh_AddPeriodRegister : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]))
        {
            Response.Redirect("PeriodRegister.aspx");
            return;
        }

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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            SetKey();
           // Check();
            this.ViewState["BtnFinish"] = btnFinish.Enabled;

            Session["MeIsPaid"] = false;
        }
        if (this.ViewState["BtnFinish"] != null)
            this.btnFinish.Enabled  = (bool)this.ViewState["BtnFinish"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PeriodRegister.aspx?PgMd=" + Utility.EncryptQS("New"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        //if (!Utility.CreateAccount())
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "به دلیل عدم ارتباط با بخش مالی،امکان پرداخت الکترونیکی وجود ندارد.";
        //    return;

        //}
        string PPId = HiddenFieldCourseRegister["PPId"].ToString();
        string Amount = Utility.EncryptQS(lblPeriodCost.Text);
        string RegisterType = Utility.EncryptQS(cmbRegisterType.SelectedIndex.ToString());        
        string BankURL = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress();
        Response.Redirect("PeriodEPayment.aspx?PPId=" + PPId + "&Amount=" + Amount + "&RegisterType=" + RegisterType);
    }

    protected void cmbRegisterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbRegisterType.SelectedIndex > -1)
        {
            if (HiddenFieldCourseRegister["PPId"] != null || (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
            {
                TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
                string PPId = Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString());
                PeriodPresentManager.FindByCode(int.Parse(PPId));
                if (PeriodPresentManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["PeriodCost"]) && !Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["TestCost"]))
                    {
                        if (cmbRegisterType.SelectedIndex == 0)//Period & Test
                        {
                            int PeriodCost = int.Parse(decimal.Parse(PeriodPresentManager[0]["PeriodCost"].ToString()).ToString("#,#"));
                            int TestCost = int.Parse(decimal.Parse(PeriodPresentManager[0]["TestCost"].ToString()).ToString("#,#"));
                            int Sum = PeriodCost + TestCost;
                            lblPeriodCost.Text = Sum.ToString();
                        }
                        else
                        {
                            if (cmbRegisterType.SelectedIndex == 1)//Test
                            {
                                string Cost = (decimal.Parse(PeriodPresentManager[0]["TestCost"].ToString())).ToString("#,#");
                                if(string.IsNullOrEmpty(Cost))
                                    Cost="0";
                                lblPeriodCost.Text = Cost;
                            }
                            else
                            {
                                if (cmbRegisterType.SelectedIndex == 2)//Only Period
                                {
                                    string Cost= decimal.Parse(PeriodPresentManager[0]["PeriodCost"].ToString()).ToString("#,#");
                                      if(string.IsNullOrEmpty(Cost))
                                    Cost="0";
                                lblPeriodCost.Text = Cost;
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
                        return;
                    }
                }
                else
                {

                }
            }
            else
            {
                Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
                return;
            }


        }
    }
    #endregion

    #region Methods
    private void SetKey()
    {
        try
        {
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
            if (LoginManager.Count == 1)
            {
                string MeId = LoginManager[0]["MeId"].ToString();
                HiddenFieldCourseRegister["MeId"] = Utility.EncryptQS(MeId);
            }
            else
            {
                Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
                return;
            }
            HiddenFieldCourseRegister["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldCourseRegister["PPId"] = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
            //HiddenFieldCourseRegister["PRId"] = Server.HtmlDecode(Request.QueryString["PRId"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldCourseRegister["PageMode"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            SetMode(PageMode);
        }
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
              //  Check();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        btnFinish.Enabled = false;
        this.ViewState["btnFinish"] = btnFinish.Enabled;

        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
        FillPeriodPresent(PPId);

        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
        int MeId = -1;
        if (LoginManager.Count == 1)
        {
            MeId = int.Parse(LoginManager[0]["MeId"].ToString());
        }
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        DataTable dtPeriodRegister = PeriodRegisterManager.SelectPeriodRegister(MeId, PPId, 0);
        int PRId = -1;
        if (dtPeriodRegister.Rows.Count == 1)
        {
            PRId = int.Parse(dtPeriodRegister.Rows[0]["PRId"].ToString());
            FillForm(PRId);
        }
        Disable();
    }

    private void SetNewModeKeys()
    {
      
        ClearForm();
        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
        FillPeriodPresent(PPId);
        //RoundPanelPeriodRegister.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string PRId = Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString());
        //Check UserPermission
       
        // this.ViewState["btnInActive"] = btnInActive.Enabled;

        if (string.IsNullOrEmpty(PRId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnableControls();
        FillForm(int.Parse(PRId));

        //RoundPanelPeriodRegister.HeaderText = "ویرایش";
    }

    private void FillForm(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count == 1)
        {
            cmbPaymentType.SelectedIndex = int.Parse(PeriodRegisterManager[0]["PaymentType"].ToString());
            cmbRegisterType.SelectedIndex = int.Parse(PeriodRegisterManager[0]["RegisterType"].ToString());
        }
    }

    private void FillPeriodPresent(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            lblEndDate.Text = PeriodPresentManager[0]["EndDate"].ToString();
            lblInsName.Text = PeriodPresentManager[0]["InsName"].ToString();
            lblPeriodCost.Text = decimal.Parse(PeriodPresentManager[0]["PeriodCost"].ToString()).ToString("#,#");
            lblPeriodPlace.Text = PeriodPresentManager[0]["Place"].ToString();
            lblPeriodTittle.Text = PeriodPresentManager[0]["PeriodTitle"].ToString();
            lblPPCode.Text = PeriodPresentManager[0]["PPCode"].ToString();
            lblStartDate.Text = PeriodPresentManager[0]["StartDate"].ToString();
            lblTestDate.Text = PeriodPresentManager[0]["TestDate"].ToString();
            lblTestHourse.Text = PeriodPresentManager[0]["TestHour"].ToString();
            lblPeriodType.Text = PeriodPresentManager[0]["PPType"].ToString();
            int PeriodType = int.Parse(PeriodPresentManager[0]["PeriodType"].ToString());
            if (PeriodType == 1)
            {
                cmbRegisterType.SelectedIndex = 1;
                cmbRegisterType.Enabled = false;
            }
            else
            {
                cmbRegisterType.SelectedIndex = 0;
                cmbRegisterType.Enabled = true;
            }
            lblCapacity.Text = PeriodPresentManager[0]["Capacity"].ToString();
            int Capacity = 0;
            Capacity = int.Parse(PeriodPresentManager[0]["Capacity"].ToString());
            int RegisterCount = 0;
            RegisterCount = int.Parse(PeriodPresentManager[0]["CountRegister"].ToString());
            int RemainCapacity = Capacity - RegisterCount;
            lblRemainCapacity.Text = RemainCapacity.ToString();
        }
    }

    private void ClearForm()
    {
        cmbRegisterType.SelectedIndex = 0;
        cmbPaymentType.SelectedIndex = 0;
    }

    private void Disable()
    {
        cmbPaymentType.Enabled = false;
        cmbRegisterType.Enabled = false;
    }

    private void EnableControls()
    {
        cmbPaymentType.Enabled = true;
    }

    //private void InsertPeriodRegister()
    //{
    //    if (IsPageRefresh)
    //        return;

    //    }
    //    TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
    //    TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
    //    TSP.DataManager.PeriodOpinionManager PeriodOpinionManager = new TSP.DataManager.PeriodOpinionManager();
    //    int Capacity = 0;
    //    int RemainCapacity = 0;
    //    int RegisterCount = 0;
    //    //string TodayDate = Utility.GetDateOfToday();
    //    string EndRegisterDate = "";
    //    string PeriodTestDate = "";
    //    int MeId = -1;
    //    try
    //    {
    //        if (!string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
    //        {
    //            MeId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["MeId"].ToString()));
    //        }
    //        else
    //        {
    //            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //            return;
    //        }
    //        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
    //        PeriodPresentManager.FindByCode(PPId);
    //        if (PeriodPresentManager.Count == 1)
    //        {

    //            DataTable dtPPOpinion = PeriodOpinionManager.SelectPeriodOpinionByType(PPId, 2);
    //            if (dtPPOpinion.Rows.Count > 0)
    //            {
    //                int OpinionEmpId = -1;
    //                int OpinionUltId = -1;
    //                for (int i = 0; i < dtPPOpinion.Rows.Count; i++)
    //                {
    //                    OpinionEmpId = int.Parse(dtPPOpinion.Rows[i]["EmpId"].ToString());
    //                    OpinionUltId = int.Parse(dtPPOpinion.Rows[i]["UltId"].ToString());
    //                    if (OpinionUltId == 1)
    //                    {
    //                        if (OpinionEmpId == MeId)
    //                        {
    //                            this.DivReport.Visible = true;
    //                            this.LabelWarning.Text = "امکان ثبت نام برای عضو انتخاب شده وجود ندارد.عضو انتخابی بازرس دوره می باشد.";
    //                            return;
    //                        }
    //                    }
    //                }
    //            }

    //            Capacity = int.Parse(PeriodPresentManager[0]["Capacity"].ToString());
    //            RegisterCount = int.Parse(PeriodPresentManager[0]["CountRegister"].ToString());
    //            PeriodTestDate = PeriodPresentManager[0]["TestDate"].ToString();
    //            EndRegisterDate = PeriodPresentManager[0]["EndRegisterDate"].ToString();
    //            int IsEndDate = string.Compare(EndRegisterDate, Utility.GetDateOfToday());
    //            int IsTestDate = string.Compare(PeriodTestDate, Utility.GetDateOfToday());
    //            if (IsEndDate > 0)
    //            {
    //                RemainCapacity = Capacity - RegisterCount;
    //                if (RemainCapacity > 0)
    //                {
    //                    DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();
    //                    PeriodRegisterRow["PPId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
    //                    PeriodRegisterRow["MeId"] = MeId;
    //                    PeriodRegisterRow["PaymentType"] = cmbPaymentType.SelectedIndex;
    //                    PeriodRegisterRow["IsMember"] = 1;
    //                    PeriodRegisterRow["InActive"] = 0;
    //                    PeriodRegisterRow["IsSeminar"] = 0;
    //                    PeriodRegisterRow["RegisterType"] = cmbRegisterType.SelectedIndex;
    //                    PeriodRegisterRow["RegisterDate"] = Utility.GetDateOfToday();
    //                    PeriodRegisterRow["UserId"] = Utility.GetCurrentUser_UserId();
    //                    PeriodRegisterRow["ModifiedDate"] = DateTime.Now;

    //                    PeriodRegisterManager.AddRow(PeriodRegisterRow);

    //                    int cn = PeriodRegisterManager.Save();
    //                    if (cn > 0)
    //                    {
    //                        // RoundPanelPeriodRegister.HeaderText = "مشاهده";
    //                        HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("Edit");
    //                        HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());                         

    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "ذخیره انجام شد.";
    //                    }
    //                    else
    //                    {
    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
    //                    }
    //                }
    //                else
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
    //                }
    //            }
    //            else
    //            {
    //                if ((IsEndDate < 0) && (IsTestDate > 0) && (cmbRegisterType.SelectedIndex == 1))
    //                {
    //                    RemainCapacity = Capacity - RegisterCount;
    //                    if (RemainCapacity > 0)
    //                    {
    //                        DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();
    //                        PeriodRegisterRow["PPId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
    //                        if (!string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
    //                        {
    //                            PeriodRegisterRow["MeId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["MeId"].ToString()));
    //                        }
    //                        else
    //                        {
    //                            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //                            return;
    //                        }
    //                        PeriodRegisterRow["PaymentType"] = cmbPaymentType.SelectedIndex;
    //                        PeriodRegisterRow["IsMember"] = 1;
    //                        PeriodRegisterRow["InActive"] = 0;
    //                        PeriodRegisterRow["IsSeminar"] = 0;
    //                        PeriodRegisterRow["RegisterType"] = cmbRegisterType.SelectedIndex;
    //                        PeriodRegisterRow["RegisterDate"] = Utility.GetDateOfToday();
    //                        PeriodRegisterRow["UserId"] = Utility.GetCurrentUser_UserId();
    //                        PeriodRegisterRow["ModifiedDate"] = DateTime.Now;

    //                        PeriodRegisterManager.AddRow(PeriodRegisterRow);

    //                        int cn = PeriodRegisterManager.Save();
    //                        if (cn > 0)
    //                        {
    //                            // RoundPanelPeriodRegister.HeaderText = "مشاهده";
    //                            HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("Edit");
    //                            HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());
                               

    //                            this.DivReport.Visible = true;
    //                            this.LabelWarning.Text = "ذخیره انجام شد.";
    //                        }
    //                        else
    //                        {
    //                            this.DivReport.Visible = true;
    //                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
    //                    }
    //                }
    //                else
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "در طی برگزاری دوره تنها قادر به ثبت نام در آزمون دوره می باشید.";
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //        {
    //            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //            if (se.Number == 2601)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //        }
    //    }
    //}

    private void Check()
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.PeriodOpinionManager PeriodOpinionManager = new TSP.DataManager.PeriodOpinionManager();
        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        //string TodayDate = Utility.GetDateOfToday();
        string EndRegisterDate = "";
        string PeriodTestDate = "";
        int MeId = -1;

        if (!string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
        {
            MeId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["MeId"].ToString()));
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            Capacity = int.Parse(PeriodPresentManager[0]["Capacity"].ToString());
            RegisterCount = int.Parse(PeriodPresentManager[0]["CountRegister"].ToString());
            PeriodTestDate = PeriodPresentManager[0]["TestDate"].ToString();
            EndRegisterDate = PeriodPresentManager[0]["EndRegisterDate"].ToString();
            int IsEndDate = string.Compare(EndRegisterDate, Utility.GetDateOfToday());
            int IsTestDate = string.Compare(PeriodTestDate, Utility.GetDateOfToday());
            RemainCapacity = Capacity - RegisterCount;
            if (RemainCapacity >0)
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
                                btnFinish.Enabled = false;
                                
                                return;
                            }
                        }
                    }
                }
                if (IsEndDate > 0)
                {
                    RemainCapacity = Capacity - RegisterCount;
                    if (RemainCapacity <= 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
                        btnFinish.Enabled = false;
                    }
                }
                else
                {
                    if ((IsEndDate < 0) && (IsTestDate > 0) && (cmbRegisterType.SelectedIndex == 1))
                    {
                        RemainCapacity = Capacity - RegisterCount;
                        if (RemainCapacity <= 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
                            btnFinish.Enabled = false;
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "در طی برگزاری دوره تنها قادر به ثبت نام در آزمون دوره می باشید.";
                        cmbRegisterType.Enabled = false;
                        cmbRegisterType.SelectedIndex = 1;
                        cmbRegisterType_SelectedIndexChanged(this, new EventArgs());
                    }
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
                btnFinish.Enabled = false;
            }
        }
    }

    #endregion
}
