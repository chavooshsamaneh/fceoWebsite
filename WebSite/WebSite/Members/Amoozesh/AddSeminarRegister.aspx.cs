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

public partial class Members_Amoozesh_SeminarRegister : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}
        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]))
        {
            Response.Redirect("PeriodRegister.aspx");
            return;
        }
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

            TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew || per.CanEdit;
            SetKey();
            Check();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["btnFinish"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["btnFinish"] != null)
            this.btnFinish.Enabled = this.btnFinish2.Enabled = (bool)this.ViewState["btnFinish"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PeriodRegister.aspx?PgMd=" + Utility.EncryptQS("New"));
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;        
        string PageMode = Utility.DecryptQS(HiddenFieldCourseRegister["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertPeriodRegister();

            }
            else if (PageMode == "Edit")
            {
                string PRId = Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString());

                if (string.IsNullOrEmpty(PRId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditPeriodRegister(int.Parse(PRId));
                }
            }
        }

    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        string SeId = HiddenFieldCourseRegister["PPId"].ToString();
        string Amount = Utility.EncryptQS(txtSeminarCost.Text);
        Response.Redirect("SeminarEPayment.aspx?SeId=" + SeId + "&Amount=" + Amount);
    }

    #endregion

    #region Methods
    private void SetKey()
    {
        try
        {
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(int.Parse(Session["Login"].ToString()));
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
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnFinish.Enabled = false;
        btnFinish2.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["btnFinish"] = btnFinish.Enabled;

        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
        FillSeminar(PPId);

        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        LoginManager.FindByCode(int.Parse(Session["Login"].ToString()));
        int MeId = -1;
        if (LoginManager.Count == 1)
        {
            MeId = int.Parse(LoginManager[0]["MeId"].ToString());
        }
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        DataTable dtPeriodRegister = PeriodRegisterManager.SelectPeriodRegister(MeId, PPId, 1);
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
        TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //Set Button's Enable
        //btnInActive.Enabled = false;
        //btnInActive2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        // this.ViewState["btnInActive"] = btnInActive.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        ClearForm();
        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
        FillSeminar(PPId);
        // FillPeriodPresent(PPId);
        //RoundPanelSeminar.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string PRId = Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString());
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable
        // btnInActive.Enabled = per.CanEdit;
        // btnInActive2.Enabled = per.CanEdit;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        // this.ViewState["btnInActive"] = btnInActive.Enabled;

        if (string.IsNullOrEmpty(PRId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnableControls();
        FillSeminar(int.Parse(PRId));

        //  RoundPanelSeminar.HeaderText = "ویرایش";
    }

    protected void FillSeminar(int SeId)
    {
        TSP.DataManager.SeminarManager SeManager = new TSP.DataManager.SeminarManager();
        TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();

        TSP.DataManager.TrainingJudgmentManager JudgeManager = new TSP.DataManager.TrainingJudgmentManager();

        try
        {
            SeManager.FindByCode(SeId);
            if (SeManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["StartDate"]))
                    txtStartDate.Text = SeManager[0]["StartDate"].ToString();

                if (!Utility.IsDBNullOrNullValue(SeManager[0]["EndDate"]))
                    txtEndDate.Text = SeManager[0]["EndDate"].ToString();

                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Description"]))
                    txtDesc.Text = SeManager[0]["Description"].ToString();

                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Place"]))
                    txtPlace.Text = SeManager[0]["Place"].ToString();

                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Topic"]))
                    txtTopic.Text = SeManager[0]["Topic"].ToString();

                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Duration"]))
                    txtDuration.Text = SeManager[0]["Duration"].ToString();

                decimal SeminarCost = Convert.ToDecimal(SeManager[0]["SeminarCost"].ToString());
                txtSeminarCost.Text = SeminarCost.ToString("#,#");

                txtSubject.Text = SeManager[0]["Subject"].ToString();
                txtTime.Text = SeManager[0]["Time"].ToString();

                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Capacity"]))
                    lblCapacity.Text = SeManager[0]["Capacity"].ToString();

                int Capacity = int.Parse(SeManager[0]["Capacity"].ToString());
                int RegisterCount = int.Parse(SeManager[0]["CountRegister"].ToString());
                int  RemainCapacity = Capacity - RegisterCount;
                lblRemainCapacity.Text = RemainCapacity.ToString();

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

        }
    }

    private void FillForm(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count == 1)
        {
            cmbPaymentType.SelectedIndex = int.Parse(PeriodRegisterManager[0]["PaymentType"].ToString());
        }
    }

    private void ClearForm()
    {
        cmbPaymentType.SelectedIndex = 0;
    }

    private void Disable()
    {
        cmbPaymentType.Enabled = false;
    }

    private void EnableControls()
    {
        cmbPaymentType.Enabled = true;
    }

    private void InsertPeriodRegister()
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        try
        {
            int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
            SeminarManager.FindByCode(PPId);
            if (SeminarManager.Count == 1)
            {
                Capacity = int.Parse(SeminarManager[0]["Capacity"].ToString());
                RegisterCount = int.Parse(SeminarManager[0]["CountRegister"].ToString());
                RemainCapacity = Capacity - RegisterCount;
                if (RemainCapacity > 0)
                {
                    DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();
                    PeriodRegisterRow["PPId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
                    if (!string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
                    {
                        PeriodRegisterRow["MeId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["MeId"].ToString()));
                    }
                    else
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    PeriodRegisterRow["PaymentType"] = cmbPaymentType.SelectedIndex;
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
                        HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("Edit");
                        HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());
                        TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                        btnSave2.Enabled = per.CanEdit;
                        btnSave.Enabled = per.CanEdit;
                        // btnInActive.Enabled = per.CanEdit;
                        // btnInActive2.Enabled = per.CanEdit;
                        this.ViewState["BtnSave"] = btnSave.Enabled;
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

    private void EditPeriodRegister(int PRId)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        try
        {
            PeriodRegisterManager.FindByCode(PRId);
            if (PeriodRegisterManager.Count == 1)
            {
                PeriodRegisterManager[0].BeginEdit();
                PeriodRegisterManager[0]["IsSeminar"] = 1;
                PeriodRegisterManager[0]["RegisterType"] = 0;
                PeriodRegisterManager[0]["PaymentType"] = cmbPaymentType.SelectedIndex;
                PeriodRegisterManager[0]["RegisterDate"] = Utility.GetDateOfToday();
                PeriodRegisterManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                PeriodRegisterManager[0]["ModifiedDate"] = DateTime.Now;

                PeriodRegisterManager[0].EndEdit();
                int cn = PeriodRegisterManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
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

    private void Check()
    {
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;

        int PPId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
        SeminarManager.FindByCode(PPId);
        if (SeminarManager.Count == 1)
        {
            Capacity = int.Parse(SeminarManager[0]["Capacity"].ToString());
            RegisterCount = int.Parse(SeminarManager[0]["CountRegister"].ToString());
            RemainCapacity = Capacity - RegisterCount;
            if (RemainCapacity <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
            }
        }
    }

    #endregion
}
