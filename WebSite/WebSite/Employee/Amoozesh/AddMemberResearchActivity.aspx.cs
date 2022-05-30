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

public partial class Employee_Amoozesh_AddMemberResearchActivity : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        txtMailNo.Attributes["onkeyup"] = "return ltr_override(event)";
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["MraId"]) || string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("MemberResearchActivity.aspx");
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;
            // btnDisActive.Enabled = per.CanDelete;
            //  btnInActive2.Enabled = per.CanDelete;
            SetKey();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            // this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        //if (this.ViewState["BtnInActive"] != null)
        //    this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        // btnInActive.Enabled = false;
        // btnInActive2.Enabled = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnInActive"] = btnInActive.Enabled;

        HiddenFieldResearch["MeId"] = "";
        HiddenFieldResearch["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldResearch["MraId"] = "";
        RoundPanelResearch.HeaderText = "جدید";
        ClearForm();
        Enable();
        RoundPanelJudgment.Visible = false;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        //HiddenFieldResearch["MeId"] = "";
        //HiddenFieldResearch["PageMode"] = Utility.EncryptQS("New");
        //HiddenFieldResearch["MraId"] = "";
        RoundPanelResearch.HeaderText = "ویرایش";
        //ClearForm();
        Enable();
        RoundPanelJudgment.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldResearch["PageMode"].ToString());


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                Insert();
            }
            else if (PageMode == "Edit")
            {
                string MraId = Utility.DecryptQS(HiddenFieldResearch["MraId"].ToString());
                if (string.IsNullOrEmpty(MraId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(MraId));
                }

            }
            else if (PageMode == "Judge")
            {
                string MraId = Utility.DecryptQS(HiddenFieldResearch["MraId"].ToString());
                if (string.IsNullOrEmpty(MraId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    string JudgeId = Utility.DecryptQS(HiddenFieldResearch["JudgeId"].ToString());
                    EditJuge(int.Parse(JudgeId));
                }
            }

        }
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberResearchActivity.aspx");
    }

    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtMeNo.Text.Trim()))
        {
            string MeId = txtMeNo.Text.Trim();
            ClearForm();
            txtMeNo.Text = MeId;
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(int.Parse(MeId));
            if (MemberManager.Count == 1)
            {
                int MrsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                if (MrsId == 1)
                {
                    //FillFormByMeIdCmb(int.Parse(MeId));
                    txtName.Text = MemberManager[0]["FirstName"].ToString();
                    txtFamily.Text = MemberManager[0]["LastName"].ToString();
                    HiddenFieldResearch["MeId"] = Utility.EncryptQS(MeId);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "وضعیت عضویت عضو انتخاب شده عدم تائید می باشد.";
                }
            }
            else
            {
                ClearForm();
                txtMeNo.Text = MeId;
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت وارد شده معتبر نمی باشد.";
            }
        }

    }

    protected void CallbackPanelReq_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] Parameters = e.Parameter.Split(';');
        string LetterNo = Parameters[1];
        string ReType = Parameters[0];
        switch (ReType)
        {
            case "CheckLetter":
                if (CheckLetterValidationAndFill(LetterNo) < 0)
                {
                    lblErrorMail.ClientVisible = true;
                    txtMailDate.Text = "";
                    txtMailTitle.Text = "";
                }
                else
                    lblErrorMail.ClientVisible = false;

                break;
        }
    }

    #endregion

    #region Methods
    private void SetKey()
    {
        try
        {
            HiddenFieldResearch["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldResearch["MraId"] = Server.HtmlDecode(Request.QueryString["MraId"]).ToString();
            HiddenFieldResearch["MeId"] = Server.HtmlDecode(Request.QueryString["MeId"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldResearch["PageMode"].ToString());
        string MraId = Utility.DecryptQS(HiddenFieldResearch["MraId"].ToString());
        string MeId = Utility.DecryptQS(HiddenFieldResearch["MeId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            SetMode(PageMode);
            CheckWorkFlowPermission();
            // CheckWorkFlowPermission();
            //  if (PageMode != "New")
            //    CheckCertificatePermission(int.Parse(TeId));
        }
    }

    private void SetMode(string PageMode)
    {
        //  string TeId = Utility.DecryptQS(TeacherId.Value.ToString());
        switch (PageMode)
        {

            case "View":
                SetViewModeKeys();
                break;
            case "New":
                SetNewModeKeys();
                lblWorkFlowState.Visible = false;
                break;
            case "Edit":
                SetEditModeKeys();
                break;
            case "Judge":
                SetJudgeModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        string MraId = Utility.DecryptQS(HiddenFieldResearch["MraId"].ToString());
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        DataTable dtTrainingJudg = TrainingJudgmentManager.SelectByResearchActLastVersion(int.Parse(MraId));
        if (dtTrainingJudg.Rows.Count > 0)
        {
            int JudgeId = int.Parse(dtTrainingJudg.Rows[0]["JudgeId"].ToString());
            HiddenFieldResearch["JudgeId"] = Utility.EncryptQS(JudgeId.ToString());
            int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, JudgeId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
            }
        }
        if (string.IsNullOrEmpty(MraId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
            // btnDisActive.Enabled = true;
            //  btnDisActive2.Enabled = true;
        }

        btnSave.Enabled = false;
        btnSave2.Enabled = false;


        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        // this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

        FillForm(int.Parse(MraId));

        Disable();


        //  InsertWorkFlowStateView(TableType, int.Parse(TeId));

        RoundPanelResearch.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        Enable();
        ClearForm();

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        RoundPanelJudgment.Visible = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        // this.ViewState["BtnDisActive"] = btnDisActive.Enabled;      

        RoundPanelResearch.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string MraId = Utility.DecryptQS(HiddenFieldResearch["MraId"].ToString());
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        DataTable dtTrainingJudg = TrainingJudgmentManager.SelectByResearchActLastVersion(int.Parse(MraId));
        if (dtTrainingJudg.Rows.Count > 0)
        {
            int JudgeId = int.Parse(dtTrainingJudg.Rows[0]["JudgeId"].ToString());
            HiddenFieldResearch["JudgeId"] = Utility.EncryptQS(JudgeId.ToString());
            int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, JudgeId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
            }
        }
        if (string.IsNullOrEmpty(MraId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        //*****Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        // btnDisActive.Enabled = true;
        // btnDisActive2.Enabled = true;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        // this.ViewState["BtnDisActive"] = btnDisActive.Enabled;

        if (string.IsNullOrEmpty(MraId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        Enable();
        FillForm(int.Parse(MraId));

        //int TableType = (int)TSP.DataManager.TableCodes.MemberResearchActivity;
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(MraId));
        //if (dtWorkFlowState.Rows.Count > 0)
        //{
        //    lblWorkFlowState.Visible = true;
        //    lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
        //}
        // InsertWorkFlowStateView(TableType, int.Parse(TeId));
        RoundPanelResearch.HeaderText = "ویرایش";
    }

    private void SetJudgeModeKeys()
    {
        string MraId = Utility.DecryptQS(HiddenFieldResearch["MraId"].ToString());
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        DataTable dtTrainingJudg = TrainingJudgmentManager.SelectByResearchActLastVersion(int.Parse(MraId));
        if (dtTrainingJudg.Rows.Count > 0)
        {
            int JudgeId = int.Parse(dtTrainingJudg.Rows[0]["JudgeId"].ToString());
            HiddenFieldResearch["JudgeId"] = Utility.EncryptQS(JudgeId.ToString());
            int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, JudgeId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
            }
        }
        if (string.IsNullOrEmpty(MraId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perJudge = TSP.DataManager.TrainingJudgmentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = perJudge.CanEdit;
        btnSave2.Enabled = perJudge.CanEdit;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
            // btnDisActive.Enabled = true;
            //  btnDisActive2.Enabled = true;
        }

        //btnSave.Enabled = false;
        //btnSave2.Enabled = false;


        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        // this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

        FillForm(int.Parse(MraId));

        Disable();


        //  InsertWorkFlowStateView(TableType, int.Parse(TeId));
        RoundPanelJudgment.Visible = true;
        txtJudgGrad.Enabled = true;
        txtJudgViewPoint.Enabled = true;
        txtMailNo.Enabled = true;
        txtMailDate.Enabled = true;
        RoundPanelResearch.HeaderText = "مشاهده";
    }

    private void SetChangeRequestModeKeys()
    {
        string MraId = Utility.DecryptQS(HiddenFieldResearch["MraId"].ToString());
        //*****Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        // btnDisActive.Enabled = true;
        // btnDisActive2.Enabled = true;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        // this.ViewState["BtnDisActive"] = btnDisActive.Enabled;

        if (string.IsNullOrEmpty(MraId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        Enable();
        FillForm(int.Parse(MraId));

        int TableType = (int)TSP.DataManager.TableCodes.MemberResearchActivity;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(MraId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            lblWorkFlowState.Visible = true;
            lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
        }
        // InsertWorkFlowStateView(TableType, int.Parse(TeId));
        RoundPanelResearch.HeaderText = "درخواست بررسی مجدد";
        RoundPanelJudgment.Visible = false;
    }

    private void FillForm(int MraId)
    {
        TSP.DataManager.MemberResearchActivityManager MemberResearchActivityManager = new TSP.DataManager.MemberResearchActivityManager();
        MemberResearchActivityManager.FindByCode(MraId);
        if (MemberResearchActivityManager.Count > 0)
        {
            txtFamily.Text = MemberResearchActivityManager[0]["LastName"].ToString();
            txtMeNo.Text = MemberResearchActivityManager[0]["MeId"].ToString();
            txtName.Text = MemberResearchActivityManager[0]["FirstName"].ToString();
            txtResearch.Text = MemberResearchActivityManager[0]["Name"].ToString();
            txtResearchDesc.Text = MemberResearchActivityManager[0]["Description"].ToString();
            cmbResearch.DataBind();
            cmbResearch.SelectedIndex = cmbResearch.Items.FindByValue(MemberResearchActivityManager[0]["RaId"].ToString()).Index;

            TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
            DataTable dtTrainingJudg = TrainingJudgmentManager.SelectByResearchActLastVersion(MraId);
            if (dtTrainingJudg.Rows.Count > 0)
            {
                int JudgeId = int.Parse(dtTrainingJudg.Rows[0]["JudgeId"].ToString());
                txtJudgViewPoint.Text = dtTrainingJudg.Rows[0]["JudgeViewPoint"].ToString();
                txtJudgGrad.Text = dtTrainingJudg.Rows[0]["JudgeGrade"].ToString();
                txtMailDate.Text = dtTrainingJudg.Rows[0]["MeetingDate"].ToString();
                txtMailNo.Text = dtTrainingJudg.Rows[0]["MeetingId"].ToString();
            }
        }
    }

    private void ClearForm()
    {
        txtFamily.Text = "";
        txtJudgGrad.Text = "";
        txtJudgViewPoint.Text = "";
        txtMeNo.Text = "";
        txtName.Text = "";
        txtResearch.Text = "";
        txtResearchDesc.Text = "";
        cmbResearch.SelectedIndex = 0;
        lblWorkFlowState.Text = "وضعیت درخواست:نامشخص";
    }

    private void Disable()
    {
        txtFamily.Enabled = false;
        txtJudgGrad.Enabled = false;
        txtJudgViewPoint.Enabled = false;
        txtMeNo.Enabled = false;
        txtName.Enabled = false;
        txtResearch.Enabled = false;
        txtResearchDesc.Enabled = false;
        cmbResearch.Enabled = false;
        txtMailDate.Enabled = false;
        txtMailNo.Enabled = false;

    }

    private void Enable()
    {
        txtFamily.Enabled = true;
        txtJudgGrad.Enabled = true;
        txtJudgViewPoint.Enabled = true;
        txtMailDate.Enabled = true;
        txtMailNo.Enabled = true;
        txtMeNo.Enabled = true;
        txtName.Enabled = true;
        txtResearch.Enabled = true;
        txtResearchDesc.Enabled = true;
        cmbResearch.Enabled = true;
    }

    protected void Insert()
    {
        try
        {
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldResearch["MeId"].ToString()));
            TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            TSP.DataManager.MemberResearchActivityManager MeRaManager = new TSP.DataManager.MemberResearchActivityManager();
            TSP.DataManager.MemberResearchActivityManager MeRaManager1 = new TSP.DataManager.MemberResearchActivityManager();
            TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
            MeRaManager.ClearBeforeFill = true;
            TransactionManager.Add(WorkFlowStateManager);
            TransactionManager.Add(MeRaManager);
            TransactionManager.Add(TrainingJudgmentManager);
            //TSP.DataManager.MemberResearchActivityManager MeRaManager2 = new TSP.DataManager.MemberResearchActivityManager();


            MeRaManager1.FindByMeId(MeId);

            for (int i = 0; i < MeRaManager1.Count; i++)
            {
                if (MeRaManager1[i]["RaId"].ToString() == cmbResearch.Value.ToString() && MeRaManager1[i]["Name"].ToString() == txtResearch.Text)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }
            TransactionManager.BeginSave();

            DataRow drResearch = MeRaManager.NewRow();
            if (cmbResearch.Value != null)
                drResearch["RaId"] = int.Parse(cmbResearch.Value.ToString());
            drResearch["MeId"] = MeId;
            drResearch["Name"] = txtResearch.Text;
            drResearch["UserId"] = Utility.GetCurrentUser_UserId();
            drResearch["Description"] = txtResearchDesc.Text;
            drResearch["ModifiedDate"] = DateTime.Now;
            // drResearch["MReId"] = int.Parse(Utility.DecryptQS(MemberRequest.Value));
            MeRaManager.AddRow(drResearch);
            int cnt = MeRaManager.Save();
            if (cnt > 0)
            {
                DataRow TrainJudgRow = TrainingJudgmentManager.NewRow();
                TrainJudgRow["PkId"] = int.Parse(MeRaManager[0]["MraId"].ToString());
                TrainJudgRow["CreateDate"] = Utility.GetDateOfToday();
                TrainJudgRow["JudgeGrade"] = 0;
                TrainJudgRow["IsConfirmed"] = 2;
                TrainJudgRow["Type"] = 2;
                TrainJudgRow["UserId"] = Utility.GetCurrentUser_UserId();
                TrainJudgRow["ModifiedDate"] = DateTime.Now;

                TrainingJudgmentManager.AddRow(TrainJudgRow);
                int count = TrainingJudgmentManager.Save();
                if (count > 0)
                {
                    int TableId = int.Parse(TrainingJudgmentManager[0]["JudgeId"].ToString());// int.Parse(MeRaManager[0]["MraId"].ToString());
                    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberResearchAct;
                    int CurrentMeId = Utility.GetCurrentUser_MeId();
                    int CurrentNmcId = FindNmcId();
                    int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
                    if (WfStart > 0)
                    {
                        TransactionManager.EndSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";

                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState((int)TSP.DataManager.TableCodes.TrainingJudgment, Convert.ToInt32(TrainingJudgmentManager[0]["JudgeId"]));
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
                        }

                        HiddenFieldResearch["MraId"] = Utility.EncryptQS(MeRaManager[0]["MraId"].ToString());
                        HiddenFieldResearch["PageMode"] = Utility.EncryptQS("Edit");
                        RoundPanelResearch.HeaderText = "ویرایش";
                        TSP.DataManager.Permission per = TSP.DataManager.MemberResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew.Enabled = per.CanNew;
                        btnNew2.Enabled = per.CanNew;
                        btnSave.Enabled = per.CanEdit;
                        btnSave2.Enabled = per.CanEdit;

                        this.ViewState["BtnSave"] = btnSave.Enabled;
                        this.ViewState["BtnEdit"] = btnEdit.Enabled;
                        this.ViewState["BtnNew"] = BtnNew.Enabled;
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
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

    protected void Edit(int MraId)
    {
        TSP.DataManager.MemberResearchActivityManager MeRaManager = new TSP.DataManager.MemberResearchActivityManager();
        TSP.DataManager.MemberResearchActivityManager MeRaManager2 = new TSP.DataManager.MemberResearchActivityManager();

        MeRaManager.FindByCode(MraId);
        if (MeRaManager.Count == 1)
        {
            try
            {
                int MeId = int.Parse(Utility.DecryptQS(HiddenFieldResearch["MeId"].ToString()));

                MeRaManager[0].BeginEdit();
                if (cmbResearch.Value != null)
                    MeRaManager[0]["RaId"] = int.Parse(cmbResearch.Value.ToString());
                //MeRaManager[0]["MeId"] = Utility.DecryptQS(MemberId.Value);
                MeRaManager[0]["Name"] = txtResearch.Text;
                MeRaManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                MeRaManager[0]["Description"] = txtResearchDesc.Text;
                MeRaManager[0]["ModifiedDate"] = DateTime.Now;
                MeRaManager[0].EndEdit();
                int cnt = MeRaManager.Save();
                if (cnt > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";

                    // Session["FillMeResearch"] = MeRaManager2.FindByMeRequest(MeId, -1, 1);
                    //  CustomAspxDevGridView1.DataSource = (DataTable)Session["FillMeResearch"];
                    // CustomAspxDevGridView1.DataBind();
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
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
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private void EditJuge(int JudgeId)
    {
        int NmcId = FindNmcId();
        if (NmcId == -1)
        {
            DivReport.Visible = true;
            LabelWarning.Text = "اطلاعات شما در چارت سازمانی ثبت نشده است.";
            return;
        }
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        TrainingJudgmentManager.FindByCode(JudgeId);
        if (TrainingJudgmentManager.Count == 1)
        {
            TrainingJudgmentManager[0].BeginEdit();

            TrainingJudgmentManager[0]["JudgeViewPoint"] = txtJudgViewPoint.Text;
            TrainingJudgmentManager[0]["JudgeGrade"] = txtJudgGrad.Text;
            //  TrainingJudgmentManager[0]["MeetingId"] = txtMailNo.Text;
            //            TrainingJudgmentManager[0]["MeetingDate"] = txtMailDate.Text;


            TrainingJudgmentManager[0]["NmcId"] = NmcId;

            TrainingJudgmentManager[0].EndEdit();
            int cn = TrainingJudgmentManager.Save();
            if (cn > 0)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
            }
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
        }
    }

    //protected void SaveChangeRequest(int MraId)
    //{
    //    TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();


    //    TSP.DataManager.MemberResearchActivityManager MeRaManager = new TSP.DataManager.MemberResearchActivityManager();
    //    TSP.DataManager.MemberResearchActivityManager MeRaManager2 = new TSP.DataManager.MemberResearchActivityManager();

    //    TransactionManager.Add(TrainingJudgmentManager);
    //    TransactionManager.Add(WorkFlowStateManager);
    //    TransactionManager.Add(MeRaManager);

    //    MeRaManager.FindByCode(MraId);
    //    if (MeRaManager.Count == 1)
    //    {
    //        int JudgeGrade = -1;
    //        string JudgeViewPoint = "";

    //        try
    //        {
    //            TransactionManager.BeginSave();

    //            DataRow JudgeRow = TrainingJudgmentManager.NewRow();
    //            JudgeRow["PkId"] = MraId;
    //            JudgeRow["CreateDate"] = Utility.GetDateOfToday();
    //            JudgeRow["JudgeGrade"] = JudgeGrade;
    //            //JudgeRow["JudgeViewPoint"] = JudgeViewPoint;
    //            JudgeRow["IsConfirmed"] = 2;
    //            JudgeRow["Type"] = 2;
    //            JudgeRow["UserId"] = Utility.GetCurrentUser_UserId();
    //            JudgeRow["ModifiedDate"] = DateTime.Now;

    //            TrainingJudgmentManager.AddRow(JudgeRow);                
    //            if (TrainingJudgmentManager.Save() > 0)
    //            {
    //                // NextPage("Judge");
    //                DataRow WFStateRow = WorkFlowStateManager.NewRow();
    //                WFStateRow["TaskId"] = (int)TSP.DataManager.WorkFlowTask.;
    //                WFStateRow["TableId"] = TrainingJudgmentManager[0]["JudgeId"];
    //                WFStateRow["NmcIdType"] = 0;
    //                WFStateRow["NmcId"] = FindNmcId();
    //                WFStateRow["SubOrder"] = 1;
    //                WFStateRow["StateType"] = 0;
    //                // WFStateRow["UpdateTableType"] = "";
    //                WFStateRow["Description"] = "درخواست بررسی مجدد تالیفات و تحقیقات";
    //                WFStateRow["Date"] = Utility.GetDateOfToday();
    //                WFStateRow["UserId"] = Utility.GetCurrentUser_UserId();
    //                WFStateRow["ModifiedDate"] = DateTime.Now;

    //                WorkFlowStateManager.AddRow(WFStateRow);
    //                if (WorkFlowStateManager.Save() > 0)
    //                {

    //                    int MeId = int.Parse(Utility.DecryptQS(HiddenFieldResearch["MeId"].ToString()));

    //                    MeRaManager[0].BeginEdit();
    //                    if (cmbResearch.Value != null)
    //                        MeRaManager[0]["RaId"] = int.Parse(cmbResearch.Value.ToString());
    //                    //MeRaManager[0]["MeId"] = Utility.DecryptQS(MemberId.Value);
    //                    MeRaManager[0]["Name"] = txtResearch.Text;
    //                    MeRaManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //                    MeRaManager[0]["Description"] = txtResearchDesc.Text;
    //                    MeRaManager[0]["ModifiedDate"] = DateTime.Now;
    //                    MeRaManager[0].EndEdit();
    //                    int cnt = MeRaManager.Save();
    //                    if (cnt > 0)
    //                    {
    //                        TransactionManager.EndSave();
    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = " ذخیره انجام شد";
    //                    }
    //                    else
    //                    {
    //                        TransactionManager.CancelSave();

    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //                    }
    //                }
    //                else
    //                {

    //                    TransactionManager.CancelSave();
    //                    DivReport.Visible = true;
    //                    LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
    //                }
    //            }
    //            else
    //            {
    //                TransactionManager.CancelSave();
    //                DivReport.Visible = true;
    //                LabelWarning.Text = "خطایی در ذخیره انجام گرفت.";
    //            }              

    //        }
    //        catch (Exception err)
    //        {
    //            TransactionManager.CancelSave();
    //            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //            {
    //                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //                if (se.Number == 2601)
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
    //                }
    //                else
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //                }
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //    }
    //    else
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //    }
    //}


    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(UserId);
        int NmcId = -1;
        if (LoginManager.Count > 0)
        {
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId);
            if (NezamChartManager.Count > 0)
            {
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            }
        }
        else
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }

    #region WF
    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(HiddenFieldResearch["PageMode"].ToString());

        CheckWorkFlowPermissionForSave(PageMode);
        if (PageMode != "New")
            CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        //******* SaveTaskCode
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberResearchAct;

        int TableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(TaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
        btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string JudgeId = Utility.DecryptQS(HiddenFieldResearch["JudgeId"].ToString());

        int WFCode = (int)TSP.DataManager.WorkFlows.MemberResearchActivity;

        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberResearchAct;
        int TaskCodeCommitteeConfirm = (int)TSP.DataManager.WorkFlowTask.CommitteeConfirmingMemberResearchAct;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, Convert.ToInt32(JudgeId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPerCommitteeConfirm = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCodeCommitteeConfirm, WFCode, Convert.ToInt32(JudgeId), Utility.GetCurrentUser_UserId(), PageMode);
        btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPerCommitteeConfirm.BtnEdit;
        RoundPanelJudgment.Enabled = WFPerCommitteeConfirm.BtnSave;
        btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave || WFPerCommitteeConfirm.BtnSave;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }
    #endregion

    #region Letters Number
    private int CheckLetterValidationAndFill(string LetterNo)
    {
        int LetterId = -1;
        TSP.DataManager.Automation.LettersManager LettersManager = new TSP.DataManager.Automation.LettersManager();
        LettersManager.FindByLetterNumber(LetterNo);
        if (LettersManager.Count > 0)
        {
            LetterId = int.Parse(LettersManager[0]["LetterId"].ToString());
            txtMailDate.Text = LettersManager[0]["LetterDate"].ToString();
            txtMailTitle.Text = LettersManager[0]["Title"].ToString();
        }
        return LetterId;
    }

    protected void InsertLetterRelatedTables(TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager, int LetterId, int TableId, int TableType)
    {
        DataRow dr = LetterRelatedTablesManager.NewRow();
        dr["LetterId"] = LetterId;
        dr["TableId"] = TableId;
        dr["TableType"] = TableType;
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        LetterRelatedTablesManager.AddRow(dr);
        LetterRelatedTablesManager.Save();
    }
    #endregion
    #endregion
}
