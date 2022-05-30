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

public partial class Employee_Amoozesh_AddTeacher : System.Web.UI.Page
{
    Boolean CanChangeMemberType, EmlpoyeeKindeChange;
    string _PageMode
    {
        get { return PgMode.Value; }
        set { PgMode.Value = value; }
    }

    int _TcId
    {
        get { return Convert.ToInt32(HiddenFieldTeacher["TcId"]); }
        set { HiddenFieldTeacher["TcId"] = value.ToString(); }
    }
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        txtSerialNo.Attributes["onkeyup"] = "return ltr_override(event)";
        txtFileNo.Attributes["onkeyup"] = "return ltr_override(event)";
        if (cmbMemberType.SelectedIndex == 0)
        {
            cmbLicence.ValidationSettings.RequiredField.IsRequired = false;
            cmbMajor.ValidationSettings.RequiredField.IsRequired = false;
            txtTel.ValidationSettings.RegularExpression.ValidationExpression = "";
            txtEmail.ValidationSettings.RegularExpression.ValidationExpression = "";
            txtEmail.ValidationSettings.RequiredField.IsRequired = false;
            txtIdNo.ValidationSettings.RequiredField.IsRequired = false;
            txtSSN.ValidationSettings.RequiredField.IsRequired = false;

        }
        else
        {
            cmbLicence.ValidationSettings.RequiredField.IsRequired = true;
            cmbMajor.ValidationSettings.RequiredField.IsRequired = true;
            txtTel.ValidationSettings.RegularExpression.ValidationExpression = "0\\d{8,11}";
            txtEmail.ValidationSettings.RegularExpression.ValidationExpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            txtEmail.ValidationSettings.RequiredField.IsRequired = true;
            txtIdNo.ValidationSettings.RequiredField.IsRequired = true;
            txtSSN.ValidationSettings.RequiredField.IsRequired = true;

        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TeId"]))
                {
                    Response.Redirect("Teachers.aspx");
                    return;
                }
                Session["IsEdited_Teacher"] = false;
                CanChangeMemberType = true;
                EmlpoyeeKindeChange = false;
                CheckUserPermission();
                SetKey();


                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = BtnNew.Enabled;
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
            }

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        cmbMajor.DataBind();
        cmbLicence.DataBind();


    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddCourse.aspx?PageMode" + Utility.EncryptQS("New"));
        TeacherId.Value = Utility.EncryptQS("");
        _PageMode = "New";
        ASPxRoundPanel2.HeaderText = "جدید";

        TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        // btnDelete.Enabled = false;
        // btnDelete2.Enabled = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;
        // lblWorkFlowState.Visible = false;
        ClearForm();
        Enable();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        string TeId = Utility.DecryptQS(TeacherId.Value);

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            if (_PageMode == "New")
            {
                Insert();
            }
            else if (_PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(TeId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                else
                {
                    Edit(int.Parse(TeId));
                }

            }
            else if (_PageMode == "changeRequest")
            {
                InsertNewRequest(int.Parse(TeId), TSP.DataManager.TeacherCertificateManager.RequestType.Change);
            }

            else if (_PageMode == "RevivalRequest")
            {
                InsertNewRequest(int.Parse(TeId), TSP.DataManager.TeacherCertificateManager.RequestType.Revival);
            }

        }



    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Teachers.aspx?");

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string TeId = Utility.DecryptQS(TeacherId.Value);

        if (string.IsNullOrEmpty(TeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Enable();
                // chbInActive.Enabled = true;
                cmbMemberType.Enabled = false;

                TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                _PageMode = "Edit";
                ASPxRoundPanel2.HeaderText = "ویرایش";
            }
        }

    }

    protected void txtMeID_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtMeNo.Text.Trim()))
        {
            string MeId = txtMeNo.Text.Trim();
            ClearForm();
            int s;
            if (!int.TryParse(MeId, out s))
                return;
            txtMeNo.Text = MeId;
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();

            MemberManager.FindByCode(int.Parse(MeId));
            if (MemberManager.Count == 1)
            {
                int MrsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                if (MrsId == 1)
                    FillFormByMeIdCmb(int.Parse(MeId));
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

    protected void cmbMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_PageMode != "View")
        {
            if (_PageMode == "Edit" && CanChangeMemberType == false)
            {
                if (cmbMemberType.SelectedItem.Value.ToString() == "0")
                    cmbMemberType.SelectedIndex = 1;
                else
                    cmbMemberType.SelectedIndex = 0;
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان تغییر نوع عضویت وجود ندارد";
            }
            else
            {
                EmlpoyeeKindeChange = true;

                if (cmbMemberType.SelectedItem.Value.ToString() == "0")//Memebr
                {
                    txtSSN.Enabled = false;
                    lblMeId.Visible = true;
                    txtMeNo.Visible = true;
                    txtName.Visible = true;

                    cmbLicence.Enabled = false;
                    cmbMajor.Enabled = false;

                    txtName.Enabled = false;
                    txtLastName.Enabled = false;

                    txtFatherName.Enabled = false;
                    txtIdNo.Enabled = false;

                    txtTel.Enabled = false;
                    txtMobileNo.Enabled = false;
                    //txtBrithDate.Attributes.Add("disabled","true");
                    txtBrithDate.Enabled = false;
                    txtAddress.Enabled = false;
                    txtEmail.Enabled = false;

                    txtBrithDate.Text = "";
                    txtDesc.Text = "";
                    txtAddress.Text = "";
                    txtEmail.Text = "";
                    txtLastName.Text = "";
                    txtFatherName.Text = "";
                    txtIdNo.Text = "";
                    txtMeNo.Text = "";
                    txtMobileNo.Text = "";
                    txtName.Text = "";
                    txtSSN.Text = "";
                    txtTel.Text = "";

                    cmbLicence.SelectedIndex = -1;
                    cmbMajor.SelectedIndex = -1;

                }
                else//Is not Member
                {
                    lblMeId.Visible = false;
                    txtName.Enabled = true;
                    txtLastName.Enabled = true;
                    txtMeNo.Visible = false;
                    txtName.Enabled = true;
                    txtLastName.Enabled = true;
                    txtFatherName.Enabled = true;
                    txtIdNo.Enabled = true;
                    txtSSN.Enabled = true;
                    txtTel.Enabled = true;
                    txtMobileNo.Enabled = true;
                    txtBrithDate.Enabled = true;
                    txtAddress.Enabled = true;
                    txtEmail.Enabled = true;
                    //txtBrithDate.Attributes.Add("disabled", "false");
                    txtBrithDate.Enabled = true;

                    txtBrithDate.Text = "";
                    txtDesc.Text = "";
                    txtAddress.Text = "";
                    txtEmail.Text = "";
                    txtLastName.Text = "";
                    txtFatherName.Text = "";
                    txtIdNo.Text = "";
                    txtMeNo.Text = "";
                    txtMobileNo.Text = "";
                    txtName.Text = "";
                    txtSSN.Text = "";
                    txtTel.Text = "";


                    cmbLicence.Enabled = true;
                    cmbMajor.Enabled = true;

                    cmbLicence.SelectedIndex = -1;
                    cmbMajor.SelectedIndex = -1;
                }
            }
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Madrak":
                // string TeId = Utility.DecryptQS();
                Response.Redirect("TeachersLicence.aspx?TeacherId=" + TeacherId.Value.ToString() + "&PageMode=" + Request.QueryString["PageMode"]);
                break;
            case "Research":
                Response.Redirect("TeacherResearchAct.aspx?TeacherId=" + TeacherId.Value.ToString() + "&Pagemode=" + Request.QueryString["PageMode"]);
                break;
            case "Job":
                Response.Redirect("TeacherJobHistory.aspx?TeacherId=" + TeacherId.Value.ToString() + "&Pagemode=" + Request.QueryString["PageMode"]);
                break;
            case "Atachment":
                Response.Redirect("TeacherAttachment.aspx?TeId=" + TeacherId.Value.ToString() + "&PgMd=" + Request.QueryString["PageMode"]);
                break;
            case "Summery":
                Response.Redirect("TechearSummery.aspx?TeId=" + TeacherId.Value.ToString() + "&PgMd=" + Request.QueryString["PageMode"]);
                break;
            case "Course":
                Response.Redirect("TeacherCourse.aspx?TeId=" + TeacherId.Value.ToString() + "&PgMd=" + Request.QueryString["PageMode"]);
                break;
        }
    }


    #endregion

    #region Methods
    private void SetKey()
    {
        _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"]);
        TeacherId.Value = Server.HtmlDecode(Request.QueryString["TeId"]).ToString();
        _TcId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TcId"]).ToString()));
        string TeId = Utility.DecryptQS(TeacherId.Value);
        //if ((!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TeId"]).ToString()))) && (CrId == -1))
        //    //CrId = int.Parse(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TeId"]).ToString()));
        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        SetMode(_PageMode);
        CheckWorkFlowPermission();
        //if (PageMode != "New")
        //    CheckCertificatePermission(int.Parse(TeId));
    }

    private void SetMode(string PageMode)
    {
        string TeId = Utility.DecryptQS(TeacherId.Value.ToString());
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
            case "changeRequest":
                SetchangeRequestModeKeys();
                break;
            case "RevivalRequest":
                SetRevivalRequestModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        string TeId = Utility.DecryptQS(TeacherId.Value.ToString());
        if (string.IsNullOrEmpty(TeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());      
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }

        btnSave.Enabled = false;
        btnSave2.Enabled = false;


        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

        FillForm(int.Parse(TeId));

        Disable();

        int TableType = (int)TSP.DataManager.TableCodes.Teachers;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(TeId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            lblWorkFlowState.Visible = true;
            lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
        }
        InsertWorkFlowStateView(TableType, int.Parse(TeId));

        MenuTeacherInfo.Enabled = true;
        ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        Enable();
        ClearForm();

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        //btnDelete.Enabled = false;
        //btnDelete2.Enabled = false;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;

        ASPxRoundPanel2.HeaderText = "جدید";
        MenuTeacherInfo.Enabled = false;
        //   chbInActive.Checked = false;
    }

    private void SetEditModeKeys()
    {
        string TeId = Utility.DecryptQS(TeacherId.Value.ToString());
        //*****Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        if (string.IsNullOrEmpty(TeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        Enable();
        FillForm(int.Parse(TeId));
        cmbMemberType.Enabled = false;

        int TableType = (int)TSP.DataManager.TableCodes.Teachers;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(TeId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            lblWorkFlowState.Visible = true;
            lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
        }
        InsertWorkFlowStateView(TableType, int.Parse(TeId));


        ASPxRoundPanel2.Enabled = true;
        MenuTeacherInfo.Enabled = true;
        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetchangeRequestModeKeys()
    {
        string TeId = Utility.DecryptQS(TeacherId.Value.ToString());
        Enable();
        FillForm(int.Parse(TeId));
        cmbMemberType.Enabled = false;
        ASPxRoundPanel2.Enabled = true;
        MenuTeacherInfo.Enabled = true;
        ASPxRoundPanel2.HeaderText = "درخواست تغییرات";
    }

    private void SetRevivalRequestModeKeys()
    {
        string TeId = Utility.DecryptQS(TeacherId.Value.ToString());
        Enable();
        FillForm(int.Parse(TeId));
        cmbMemberType.Enabled = false;
        ASPxRoundPanel2.Enabled = true;
        MenuTeacherInfo.Enabled = true;
        ASPxRoundPanel2.HeaderText = "درخواست تمدید";
    }

    private void FillForm(int TeId)
    {
        cmbMajor.DataBind();
        cmbLicence.DataBind();

        TSP.DataManager.TeacherManager manager = new TSP.DataManager.TeacherManager();
        manager.FindByCode(TeId);
        if (manager.Count == 1)
        {
            txtName.Text = manager[0]["Name"].ToString();
            txtLastName.Text = manager[0]["Family"].ToString();
            txtFatherName.Text = manager[0]["Father"].ToString();
            txtBrithDate.Text = manager[0]["BirthDate"].ToString();
            txtIdNo.Text = manager[0]["IdNo"].ToString();
            txtSSN.Text = manager[0]["SSN"].ToString();
            txtTel.Text = manager[0]["Tel"].ToString();
            txtMobileNo.Text = manager[0]["MobileNo"].ToString();
            txtAddress.Text = manager[0]["Address"].ToString();
            txtEmail.Text = manager[0]["Email"].ToString();
            cmbLicence.SelectedIndex = cmbLicence.Items.IndexOfValue(manager[0]["LiId"].ToString());
            cmbMajor.SelectedIndex = cmbMajor.Items.IndexOfValue(manager[0]["MjId"].ToString());
            txtDesc.Text = manager[0]["Description"].ToString();
            if (!string.IsNullOrEmpty(manager[0]["MeId"].ToString()))
            {
                cmbMemberType.SelectedIndex = 0;
                cmbMemberType_SelectedIndexChanged(this, new EventArgs());
                txtMeNo.Text = manager[0]["MeID"].ToString();
                FillFormByMeIdCmb(int.Parse(txtMeNo.Text));
                txtAddress.Enabled = false;
                txtBrithDate.Enabled = false;
                txtEmail.Enabled = false;
                txtLastName.Enabled = false;
                txtFatherName.Enabled = false;
                txtIdNo.Enabled = false;
                txtName.Enabled = false;

            }
            else
            {
                cmbMemberType.SelectedIndex = 1;
                txtMeNo.Visible = false;
                lblMeId.Visible = false;
            }
            TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();

            TeacherCertificateManager.FindByCode(_TcId);
            if (TeacherCertificateManager.Count > 0)
            {
                txtEndDate.Text = TeacherCertificateManager[0]["EndDate"].ToString();
                txtFileDate.Text = TeacherCertificateManager[0]["StartDate"].ToString();
                txtFileNo.Text = TeacherCertificateManager[0]["FileNo"].ToString();
                txtSerialNo.Text = TeacherCertificateManager[0]["SerialNo"].ToString();
            }
        }
    }

    private void ClearForm()
    {
        txtBrithDate.Text = 
        txtDesc.Text =
        txtAddress.Text = 
        txtEmail.Text = 
        txtLastName.Text = 
        txtFatherName.Text = 
        txtIdNo.Text = 
        txtMeNo.Text =
        txtMobileNo.Text = 
        txtName.Text = 
        txtSSN.Text = 
        txtTel.Text = 
        txtName.Text =
        txtLastName.Text = 
        txtEndDate.Text =
        txtFileDate.Text =
        txtFileNo.Text =
        txtSerialNo.Text = "";
        cmbLicence.SelectedIndex = -1;
        cmbMajor.SelectedIndex = -1;
        if (_PageMode != "Edit")
        {
            cmbMemberType.SelectedIndex = 0;
            cmbMemberType_SelectedIndexChanged(this, new EventArgs());
        }
        
    }

    private void Disable()
    {
        txtAddress.Enabled = 
        txtBrithDate.Enabled =
        txtDesc.Enabled =
        txtEmail.Enabled = 
        txtLastName.Enabled = 
        txtFatherName.Enabled = 
        txtIdNo.Enabled =
        txtMeNo.Enabled = 
        txtMobileNo.Enabled = 
        txtName.Enabled =
        txtSSN.Enabled = 
        txtTel.Enabled = 
        cmbMemberType.Enabled = 
        cmbMemberType.Enabled = 
        cmbLicence.Enabled = 
        cmbMajor.Enabled = 
        txtName.Enabled =
        txtLastName.Enabled = PanelTeacherCertificateInfo.Enabled = false;

    }

    private void Enable()
    {
        txtName.Enabled = 
        txtLastName.Enabled = 
        txtFatherName.Enabled = 
        txtIdNo.Enabled = 
        txtSSN.Enabled =
        txtTel.Enabled = 
        txtMobileNo.Enabled = 
        txtBrithDate.Enabled = 
        txtAddress.Enabled =
        txtEmail.Enabled = 
        cmbMemberType.Enabled = 
        cmbMemberType.Enabled = 
        cmbLicence.Enabled = 
        cmbMajor.Enabled = 
        txtDesc.Enabled =
        txtName.Enabled = 
        txtLastName.Enabled = 
        txtMeNo.Enabled =PanelTeacherCertificateInfo.Enabled= true;

    }

    #region Insert-Edit
    private void Edit(int TeId)
    {

        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        TransactionManager.Add(TeacherManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TeacherCertificateManager);

        TeacherManager.FindByCode(TeId);
        if (TeacherManager.Count == 1)
        {

            try
            {
                TransactionManager.BeginSave();

                TeacherManager[0].BeginEdit();
                // TeacherManager[0]["TiId"] = cmbTiId.Value;
                TeacherManager[0]["Name"] = txtName.Text;
                TeacherManager[0]["Family"] = txtLastName.Text;
                TeacherManager[0]["Father"] = txtFatherName.Text;
                TeacherManager[0]["BirthDate"] = txtBrithDate.Text;
                TeacherManager[0]["IdNo"] = txtIdNo.Text;
                TeacherManager[0]["SSN"] = txtSSN.Text;
                TeacherManager[0]["Tel"] = txtTel.Text;
                TeacherManager[0]["MobileNo"] = txtMobileNo.Text;
                TeacherManager[0]["Address"] = txtAddress.Text;
                TeacherManager[0]["Email"] = txtEmail.Text;


                TeacherManager[0]["Description"] = txtDesc.Text;
                if (!string.IsNullOrEmpty(txtMeNo.Text))
                {
                    TeacherManager[0]["MeID"] = txtMeNo.Text;
                }
                //TeacherManager[0]["LiId"] = LastLiId.Value;
                //TeacherManager[0]["MjId"] = LastMjId.Value;                                                                     
                cmbLicence.DataBind();

                if (cmbLicence.SelectedIndex > -1)
                    TeacherManager[0]["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString()); //LastLiId.Value.ToString();
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "مدرک تحصیلی نامشخص می باشد.";
                    return;
                }
                cmbMajor.DataBind();
                if (cmbMajor.SelectedIndex > -1)
                    TeacherManager[0]["MjId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());//LastMjId.Value.ToString();
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رشته تحصیلی نامشخص می باشد.";
                    return;
                }
                //TeacherManager[0]["LiId"] = cmbLicence.SelectedItem.Value;
                //TeacherManager[0]["MjId"] = cmbMajor.SelectedItem.Value;

                TeacherManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherManager[0]["ModifiedDate"] = DateTime.Now;
                TeacherManager[0].EndEdit();

                if (TeacherManager.Save() != 1)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    return;
                }               
                TeacherCertificateManager.FindByCode(_TcId);
                if (TeacherCertificateManager.Count == 1)
                {
                    TeacherCertificateManager[0].BeginEdit();
                    TeacherCertificateManager[0]["FileNo"] = txtFileNo.Text;
                    TeacherCertificateManager[0]["StartDate"] = txtFileDate.Text;
                    TeacherCertificateManager[0]["SerialNo"] = txtSerialNo.Text;
                    TeacherCertificateManager[0]["EndDate"] = txtEndDate.Text;
                    TeacherCertificateManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    TeacherCertificateManager[0]["ModifiedDate"] = DateTime.Now;
                    TeacherCertificateManager[0].EndEdit();

                    int cn = TeacherCertificateManager.Save();
                    if (cn > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    }
                }


                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_Teacher"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.Teachers;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TeId, TableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                    return;
                }
                TeacherId.Value = Utility.EncryptQS(TeacherManager[0]["TeId"].ToString());
                _PageMode = "Edit";
                Session["IsEdited_Teacher"] = true;
                ASPxRoundPanel2.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".ذخیره انجام شد";
                TransactionManager.EndSave();
                TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
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
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
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
            this.LabelWarning.Text = "ذخیره انجام نشد";
        }


    }

    private void Insert()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();// (TransactionManager);
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();

        TransactionManager.Add(TeacherManager);
        TransactionManager.Add(LoginManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TeacherCertificateManager);

        String Password = "";

        try
        {
            string CurrentSSN = txtSSN.Text.Trim();
            if (Utility.IsDBNullOrNullValue(CurrentSSN))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد ملی وارد نشده است.";
                return;
            }
            if (Utility.IsDBNullOrNullValue(txtIdNo.Text))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "شماره شناسنامه وارد نشده است.";
                return;
            }
            if (!string.IsNullOrEmpty(txtMeNo.Text.Trim()))
            {
                TeacherManager.FindMeId(Convert.ToInt32(txtMeNo.Text.Trim()));
                TeacherManager.CurrentFilter = "InActive = 0";
                for (int i = 0; i < TeacherManager.Count; i++)
                {
                    int TeacheId = int.Parse(TeacherManager[i]["TeId"].ToString());
                    DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(TeacheId);
                    if (dtTeCert.Rows.Count > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "استاد با اطلاعات جاری موجود می باشد.";
                        return;
                    }
                }
            }
            else
            {
                DataTable dtTeacher = TeacherManager.SelectTeacherBySSN(CurrentSSN);
                if (dtTeacher.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTeacher.Rows.Count; i++)
                    {
                        int TeacheId = int.Parse(dtTeacher.Rows[i]["TeId"].ToString());
                        //int InActiveStatus = int.Parse(dtTeacher.Rows[i]["InActive"].ToString());
                        if (!Convert.ToBoolean(dtTeacher.Rows[i]["InActive"]))
                        {
                            DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(TeacheId);
                            if (dtTeCert.Rows.Count > 0)
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "استاد با اطلاعات جاری موجود می باشد.";
                                return;
                            }
                        }
                    }
                }

            }
            TransactionManager.BeginSave();
            DataRow row = TeacherManager.NewRow();
            row["Name"] = txtName.Text;
            row["Family"] = txtLastName.Text;
            row["Father"] = txtFatherName.Text;
            row["BirthDate"] = txtBrithDate.Text;
            row["IdNo"] = txtIdNo.Text;
            row["SSN"] = txtSSN.Text;
            row["Tel"] = txtTel.Text;
            row["MobileNo"] = txtMobileNo.Text;
            row["Address"] = txtAddress.Text;
            row["Email"] = txtEmail.Text;

            if (cmbLicence.SelectedIndex > -1)
                row["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString()); //LastLiId.Value.ToString();
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "مدرک تحصیلی نامشخص می باشد.";
                return;
            }

            if (cmbMajor.SelectedIndex > -1)
                row["MjId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());//LastMjId.Value.ToString();
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رشته تحصیلی نامشخص می باشد.";
                return;
            }

            row["Description"] = txtDesc.Text;
            if (!string.IsNullOrEmpty(txtMeNo.Text))
            {
                // row["TiId"] = cmbTiId.SelectedItem.Value.ToString();
                row["MeID"] = txtMeNo.Text;
            }
            row["InActive"] = 0;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            TeacherManager.AddRow(row);
            if (TeacherManager.Save() != 1)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            int TeId = int.Parse(TeacherManager[0]["TeId"].ToString());
            if (TeacherManager[0]["MeID"] != null)
            {
                int IdNo = int.Parse(TeacherManager[0]["IdNo"].ToString());
                string Email = TeacherManager[0]["Email"].ToString();
                Password = InsertLogin(TeId, IdNo, Email, LoginManager);
                if (Password == "")
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    return;
                }
            }

            DataRow TeCrtRow = TeacherCertificateManager.NewRow();

            TeCrtRow["Type"] = 0;
            TeCrtRow["TeId"] = TeId;
            TeCrtRow["FileNo"] = txtFileNo.Text;
            TeCrtRow["SerialNo"] = txtSerialNo.Text;
            TeCrtRow["StartDate"] = txtFileDate.Text;
            TeCrtRow["EndDate"] = txtEndDate.Text;
            TeCrtRow["CreateDate"] = DateTime.Now;
            TeCrtRow["UserId"] = Utility.GetCurrentUser_UserId();
            TeCrtRow["ModifiedDate"] = DateTime.Now;

            TeacherCertificateManager.AddRow(TeCrtRow);
            if (TeacherCertificateManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            //*****Check Is User In TaskDoer*****
            //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo);
            if (WorkFlowTaskManager.Count <= 0)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            //  TaskDoerManager.FindByTaskId(TaskId);
            //  if (TaskDoerManager.Count > 0)
            // {
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LoginManager);
            if (NmcId <= -1)
            {
                TransactionManager.CancelSave();
                return;
            }
            int StartWorkFlow = WorkFlowStateManager.StartWorkFlow(TeId, TaskId, NmcId, Utility.GetCurrentUser_UserId());
            if (StartWorkFlow < 0)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            //  }
            // }
            TransactionManager.EndSave();
            MenuTeacherInfo.Enabled = true;
            TeacherId.Value = Utility.EncryptQS(TeacherManager[0]["TeId"].ToString());
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";
            Session["IsEdited_Teacher"] = true;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "  ذخیره با نام کاربری " + "Te" + TeacherManager[0]["TeId"].ToString() + "و رمز عبور " + Password + " انجام شد";

            TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState((int)TSP.DataManager.TableCodes.Teachers, Convert.ToInt32(TeacherManager[0]["TeId"]));
            if (dtWorkFlowState.Rows.Count > 0)
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت پروانه: " + dtWorkFlowState.Rows[0]["TaskName"].ToString();
            }
            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;


        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
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
            // this.LabelWarning.Text += err.Message;
        }


    }

    private string InsertLogin(int TeId, int IdNo, string Email, TSP.DataManager.LoginManager LogManager)
    {
        String Password = Utility.GeneratePassword();
        DataRow logRow = LogManager.NewRow();
        logRow.BeginEdit();
        logRow["UserName"] = "Te" + TeId.ToString();
        logRow["Password"] = Utility.EncryptPassword(Password);
        logRow["UltId"] = 5;
        logRow["MeId"] = TeId;
        logRow["Email"] = Email;
        logRow["IsValid"] = 1;
        logRow["ModifiedDate"] = DateTime.Now;
        logRow.EndEdit();
        LogManager.AddRow(logRow);
        int cn = LogManager.Save();
        if (cn < 0)
        {
            return "";
        }
        return Password;
    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "View", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
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

    private void InsertNewRequest(int TeId, TSP.DataManager.TeacherCertificateManager.RequestType RequestType)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TeacherCertificateManager);
        TransactionManager.Add(TeacherManager);
        try
        {
            TransactionManager.BeginSave();
            DataTable dtTeacherCertificate = TeacherCertificateManager.SelectLastVersion(TeId);
            if (dtTeacherCertificate.Rows.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            if (Convert.ToInt32(dtTeacherCertificate.Rows[0]["IsConfirm"]) == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("برای استاد انتخاب شده درخواست درجریان وجود دارد");
                return;
            }

            DataRow TCertificateRow = TeacherCertificateManager.NewRow();
            TCertificateRow["Type"] = (int)RequestType;
            TCertificateRow["TeId"] = TeId;
            TCertificateRow["FileNo"] = txtFileNo.Text;
            TCertificateRow["SerialNo"] = txtSerialNo.Text;
            TCertificateRow["StartDate"] = txtFileDate.Text;
            TCertificateRow["EndDate"] = txtEndDate.Text;

            //if (!Utility.IsDBNullOrNullValue(dtTeacherCertificate.Rows[0]["FileNo"]))
            //    TCertificateRow["FileNo"] = dtTeacherCertificate.Rows[0]["FileNo"].ToString();
            //if (!Utility.IsDBNullOrNullValue(dtTeacherCertificate.Rows[0]["SerialNo"]))
            //    TCertificateRow["SerialNo"] = dtTeacherCertificate.Rows[0]["SerialNo"].ToString();
            TCertificateRow["IsConfirm"] = 0;
            TCertificateRow["UserId"] = Utility.GetCurrentUser_UserId();
            TCertificateRow["ModifiedDate"] = DateTime.Now;
            TCertificateRow["CreateDate"] = DateTime.Now;
            TCertificateRow["Date"] = Utility.GetDateOfToday();

            TeacherCertificateManager.AddRow(TCertificateRow);
            if (TeacherCertificateManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام شد.");
                return;
            }
            #region Edit Teacher
            TeacherManager.FindByCode(TeId);
            if (TeacherManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام شد.");
                return;
            }
            TeacherManager[0].BeginEdit();
            TeacherManager[0]["Name"] = txtName.Text;
            TeacherManager[0]["Family"] = txtLastName.Text;
            TeacherManager[0]["Father"] = txtFatherName.Text;
            TeacherManager[0]["BirthDate"] = txtBrithDate.Text;
            TeacherManager[0]["IdNo"] = txtIdNo.Text;
            TeacherManager[0]["SSN"] = txtSSN.Text;
            TeacherManager[0]["Tel"] = txtTel.Text;
            TeacherManager[0]["MobileNo"] = txtMobileNo.Text;
            TeacherManager[0]["Address"] = txtAddress.Text;
            TeacherManager[0]["Email"] = txtEmail.Text;
            TeacherManager[0]["Description"] = txtDesc.Text;
            if (!string.IsNullOrEmpty(txtMeNo.Text))
            {
                TeacherManager[0]["MeID"] = txtMeNo.Text;
            }
            cmbLicence.DataBind();
            if (cmbLicence.SelectedIndex > -1)
                TeacherManager[0]["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString()); //LastLiId.Value.ToString();
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "مدرک تحصیلی نامشخص می باشد.";
                return;
            }
            cmbMajor.DataBind();
            if (cmbMajor.SelectedIndex > -1)
                TeacherManager[0]["MjId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());//LastMjId.Value.ToString();
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رشته تحصیلی نامشخص می باشد.";
                return;
            }

            TeacherManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            TeacherManager[0]["ModifiedDate"] = DateTime.Now;
            TeacherManager[0].EndEdit();
            if (TeacherManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام شد.");
                return;
            }
            #endregion
            int SaveTeacherInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;

            WorkFlowTaskManager.FindByTaskCode(SaveTeacherInfoTaskCode);
            if (WorkFlowTaskManager.Count <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام شد.");
                return;
            }
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            int NmcId = FindNmcId(TaskId);
            if (NmcId < 0)
            {
                TransactionManager.CancelSave();
                return;
            }
            DataRow WorkflowStateRow = WorkFlowStateManager.NewRow();
            WorkflowStateRow["TaskId"] = TaskId;
            WorkflowStateRow["TableId"] = TeId;
            WorkflowStateRow["NmcId"] = NmcId;
            WorkflowStateRow["SubOrder"] = 0;
            WorkflowStateRow["UserId"] = Utility.GetCurrentUser_UserId();
            WorkflowStateRow["ModifiedDate"] = DateTime.Now;

            WorkFlowStateManager.AddRow(WorkflowStateRow);
            if (WorkFlowStateManager.Save() <= 0)
            {

                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام شد.");
                return;
            }

            TransactionManager.EndSave();
            ShowMessage("ذخیره انجام شد.");

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره انجام شد.");
        }
    }
    #endregion

    private void FillFormByMeId(int MeId)
    {
        cmbMajor.DataBind();
        // cmbTiId.DataBind();
        cmbLicence.DataBind();
        //cmbName.DataBind();
        TSP.DataManager.MemberManager MemManager = new TSP.DataManager.MemberManager();
        MemManager.FindByCode(MeId);
        if (MemManager.Count > 0)
        {
            // cmbTiId.SelectedIndex = cmbTiId.Items.IndexOfValue(MemManager[0]["TiId"].ToString());
            //cmbName.SelectedIndex = cmbName.Items.IndexOfValue(MemManager[0]["MeId"]);            
            txtName.Text = MemManager[0]["FirstName"].ToString();
            txtLastName.Text = MemManager[0]["LastName"].ToString();
            txtFatherName.Text = MemManager[0]["FatherName"].ToString();
            txtBrithDate.Text = MemManager[0]["BirhtDate"].ToString();
            txtIdNo.Text = MemManager[0]["IdNo"].ToString();
            txtSSN.Text = MemManager[0]["SSN"].ToString();
            txtTel.Text = MemManager[0]["HomeTel"].ToString();
            txtMobileNo.Text = MemManager[0]["MobileNo"].ToString();
            txtAddress.Text = MemManager[0]["HomeAdr"].ToString();
            txtEmail.Text = MemManager[0]["Email"].ToString();
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "عضوی با کد عضویت داده شده وجود ندارد.";
        }
    }

    private void FillFormByMeIdCmb(int MeId)
    {
        cmbMajor.DataBind();
        // cmbTiId.DataBind();
        cmbLicence.DataBind();
        //cmbName.DataBind();

        TSP.DataManager.MemberManager MemManager = new TSP.DataManager.MemberManager();
        MemManager.FindByCode(MeId);
        if (MemManager.Count > 0)
        {
            txtMeNo.Text = MeId.ToString();
            txtName.Text = MemManager[0]["FirstName"].ToString();
            txtLastName.Text = MemManager[0]["LastName"].ToString();
            txtFatherName.Text = MemManager[0]["FatherName"].ToString();
            txtBrithDate.Text = MemManager[0]["BirhtDate"].ToString();
            txtIdNo.Text = MemManager[0]["IdNo"].ToString();
            txtSSN.Text = MemManager[0]["SSN"].ToString();
            txtTel.Text = MemManager[0]["HomeTel"].ToString();
            txtMobileNo.Text = MemManager[0]["MobileNo"].ToString();
            txtAddress.Text = MemManager[0]["HomeAdr"].ToString();
            txtEmail.Text = MemManager[0]["Email"].ToString();
            LastMjId.Value = MemManager[0]["LastMjId"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemManager[0]["LastMjId"]))
                cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(MemManager[0]["LastMjId"].ToString()).Index;
            LastLiId.Value = MemManager[0]["LastLicenceId"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemManager[0]["LastLicenceId"]))
                cmbLicence.SelectedIndex = cmbLicence.Items.FindByValue(MemManager[0]["LastLicenceId"].ToString()).Index;
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "عضوی با کد عضویت داده شده وجود ندارد.";
            ClearForm();
        }
    }

    #region WF

    private int FindNmcId(int TaskId, TSP.DataManager.LoginManager LoginManager)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        //= new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            ShowMessage("اطلاعات شما به عنوان انجام دهنده عملیات انتخاب شده در جریان کار ثبت نشده است.");
            return (-1);
        }
    }

    private void CheckUserPermission()
    {
        //****Check table permission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = per.CanNew || per.CanEdit;
        btnSave2.Enabled = per.CanNew || per.CanEdit;


    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave(_PageMode);
        if (_PageMode != "New" && _PageMode != "changeRequest" && _PageMode != "RevivalRequest")
            CheckWorkFlowPermissionForEdit(_PageMode);
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        //******* SaveTaskCode
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;

        int TableType = (int)TSP.DataManager.TableCodes.Teachers;
        if (_PageMode == "changeRequest" || _PageMode == "RevivalRequest")
        {
            PageMode = "New";
        }
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(TaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew;
        btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string TeId = Utility.DecryptQS(TeacherId.Value.ToString());

        int WFCode = (int)TSP.DataManager.WorkFlows.TeachersConfirming;

        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, Convert.ToInt32(TeId), Utility.GetCurrentUser_UserId(), PageMode);
        btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;
        btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }
    #endregion


    private int FindTeacherCertificate(int TeId)
    {
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();

        DataTable dtTeCert = TeacherCertificateManager.SelectLastVersion(TeId);
        int TeCertType = -1;
        if (dtTeCert.Rows.Count > 0)
        {
            TeCertType = int.Parse(dtTeCert.Rows[0]["Type"].ToString());
        }
        return TeCertType;


    }

    private void CheckCertificatePermission(int TeId)
    {
        int CertType = FindTeacherCertificate(TeId);
        if (CertType == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت پرونده استاد انتخاب شده نامشخص است.";
            return;
        }
        if (CertType == 1 || CertType == 2)
        {
            switch (_PageMode)
            {
                case "View":

                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;

                    break;
                case "Edit":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
    }
    #endregion

}
