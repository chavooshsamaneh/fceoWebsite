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

public partial class Employee_Amoozesh_AddInstitueManager : System.Web.UI.Page
{
    string InsMngId;
    string PageMode;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Cache["CachePersonMember"] == null)
                Cache["CachePersonMember"] = new object();
            TSP.DataManager.Permission per = TSP.DataManager.InstitueManagerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            //ُSet Buttom's Enabled        
            //   btnSave.Enabled = false;
            // btnSave2.Enabled = false;

            if (per.CanNew)
            {
                btnNew.Enabled = true;
                btnNew2.Enabled = true;
                btnSave.Enabled = per.CanNew;
                btnSave2.Enabled = per.CanNew;
            }
            if (per.CanEdit)
            {
                btnEdit.Enabled = true;
                btnEdit2.Enabled = true;
            }

            SetKeys();
            CheckWorkFlowPermission();

            int InsId = int.Parse(Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString()));
            CheckCertificatePermission(InsId);

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        int s;
        if (!int.TryParse(txtMeNo.Text, out s))
            return;

        if (!string.IsNullOrEmpty(txtMeNo.Text))
        {
            txtAddress.Text = "";
            txtBrithDate.Text = "";
            txtDescription.Text = "";
            txtFamily.Text = "";
            txtFather.Text = "";
            txtName.Text = "";
            txtSSN.Text = "";
            txtIdNo.Text = "";
            // txtMeNo.Text = "";
            txtMobile.Text = "";
            txtSSN.Text = "";
            txtTell.Text = "";
            txtEmail.Text = "";
            txtInsShares.Text = "";
            txtJobDuration.Text = "";
            cmbMajor.SelectedIndex = -1;
            cmbLicence.SelectedIndex = -1;

            int MeId = int.Parse(txtMeNo.Text);

            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count > 0)
            {
                int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                if (MRsId != 1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
                    txtName.Text = MemberManager[0]["FirstName"].ToString();
                    txtFamily.Text = MemberManager[0]["LastName"].ToString();
                    return;
                }
                txtAddress.Text = MemberManager[0]["HomeAdr"].ToString();
                txtBrithDate.Text = MemberManager[0]["BirhtDate"].ToString();
                txtEmail.Text = MemberManager[0]["Email"].ToString();
                txtFamily.Text = MemberManager[0]["LastName"].ToString();
                txtFather.Text = MemberManager[0]["FatherName"].ToString();
                txtIdNo.Text = MemberManager[0]["IdNo"].ToString();
                txtMobile.Text = MemberManager[0]["MobileNo"].ToString();
                txtName.Text = MemberManager[0]["FirstName"].ToString();
                txtSSN.Text = MemberManager[0]["SSN"].ToString();
                txtTell.Text = MemberManager[0]["HomeTel"].ToString();
                TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
                MemberLicenceManager.FindByMeId(MeId);
                //if (MemberLicenceManager.Count > 0)
                //{
                //    if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["LastLiId"]))
                //    cmbLicence.SelectedIndex = cmbLicence.Items.IndexOfValue(MemberLicenceManager[0]["LastLiId"].ToString());
                //if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["LastMjId"]))
                //    cmbMajor.SelectedIndex = cmbLicence.Items.IndexOfValue(MemberLicenceManager[0]["LastMjId"].ToString());
                //}
                //else
                //{
                //    cmbLicence.SelectedIndex = -1;
                //}

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastMjId"]))
                    cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(MemberManager[0]["LastMjId"].ToString()).Index;
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastLicenceId"]))
                    cmbLicence.SelectedIndex = cmbLicence.Items.FindByValue(MemberManager[0]["LastLicenceId"].ToString()).Index;

                DisableControls();
                txtJobDuration.Enabled = true;
                txtInsShares.Enabled = true;
                //btnSave.Enabled = false;
                //btnSave2.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کد عضویت وارد شده معتبر نمی باشد.";
            }
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        HiddenFieldInsManager["PageMode"] = Utility.EncryptQS("Edit");
        EnableControls();
        RoundPanelInsManager.HeaderText = "مشاهده";
        TSP.DataManager.Permission per = TSP.DataManager.InstitueManagerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int InsMngId = int.Parse(Utility.DecryptQS(HiddenFieldInsManager["InsMngId"].ToString()));
        DeleteInsManager(InsMngId);
        ClearForm();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string PageMode = Utility.DecryptQS(HiddenFieldInsManager["PageMode"].ToString());

        string InsMngId = Utility.DecryptQS(HiddenFieldInsManager["InsMngId"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                InsertInsManager();

                TSP.DataManager.Permission per = TSP.DataManager.InstitueManagerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                btnNew.Enabled = per.CanNew;
                btnNew2.Enabled = per.CanNew;
                this.ViewState["BtnNew"] = btnNew.Enabled;

                //Response.Redirect("AddCourse.aspx?TeId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(InsMngId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    EditInsManager(int.Parse(InsMngId));
                }

            }

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueManager.aspx?InsId=" + HiddenFieldInsManager["InsId"].ToString() + "&PgMd=" + HiddenFieldInsManager["PrePageMode"].ToString());
    }

    protected void cmbIsMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbIsMember.SelectedIndex == 0)
        {
            lblMeNo.Visible = true;
            txtMeNo.Visible = true;
            // btnSearch1.ClientVisible = true;

            btnEdit.Enabled = false;

            txtAddress.Enabled = false;
            txtBrithDate.Enabled = false;
            txtDescription.Enabled = false;
            txtEmail.Enabled = false;
            txtFamily.Enabled = false;
            txtFather.Enabled = false;
            txtIdNo.Enabled = false;
            txtMobile.Enabled = false;
            txtName.Enabled = false;
            txtSSN.Enabled = false;
            txtTell.Enabled = false;

            txtInsShares.Enabled = false;
            txtJobDuration.Enabled = false;
            cmbLicence.Enabled = false;
            cmbMajor.Enabled = false;
        }

        if (cmbIsMember.SelectedIndex == 1)
        {
            lblMeNo.Visible = false;
            txtMeNo.Visible = false;
            // btnSearch1.Visible = false;

            btnEdit.Enabled = true;

            txtAddress.Text = "";
            txtBrithDate.Text = "";
            txtDescription.Text = "";
            txtFamily.Text = "";
            txtFather.Text = "";
            txtName.Text = "";
            txtSSN.Text = "";
            txtIdNo.Text = "";
            txtMeNo.Text = "";
            txtMobile.Text = "";
            txtSSN.Text = "";
            txtTell.Text = "";
            txtEmail.Text = "";
            cmbMajor.SelectedIndex = 0;
            cmbLicence.SelectedIndex = 0;

            txtAddress.Enabled = true;
            txtBrithDate.Enabled = true;
            txtDescription.Enabled = true;
            txtEmail.Enabled = true;
            txtFamily.Enabled = true;
            txtFather.Enabled = true;
            txtIdNo.Enabled = true;
            txtMobile.Enabled = true;
            txtName.Enabled = true;
            txtSSN.Enabled = true;
            txtTell.Enabled = true;
            txtInsShares.Enabled = true;
            txtJobDuration.Enabled = true;
            cmbLicence.Enabled = true;
            cmbMajor.Enabled = true;
        }
    }

    protected void CallbackPanelManager_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        txtMeNo_TextChanged(this, new EventArgs());
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        HiddenFieldInsManager["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldInsManager["InsMngId"] = "";
        RoundPanelInsManager.HeaderText = "جدید";
        cmbIsMember.Enabled = true;
        cmbIsMember.SelectedIndex = 0;
        // EnableControls();
        btnNew.Enabled = false;
        btnNew2.Enabled = false;

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    #endregion

    #region Methods
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
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
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

    private void ClearForm()
    {
        txtAddress.Text = "";
        txtBrithDate.Text = "";
        txtDescription.Text = "";
        txtFamily.Text = "";
        txtFather.Text = "";
        txtName.Text = "";
        txtSSN.Text = "";
        txtIdNo.Text = "";
        txtMeNo.Text = "";
        txtMobile.Text = "";
        txtSSN.Text = "";
        txtTell.Text = "";
        txtInsShares.Text = "";
        txtJobDuration.Text = "";
        cmbMajor.SelectedIndex = -1;
        cmbLicence.SelectedIndex = -1;
        cmbIsMember.SelectedIndex = 0;
        cmbIsMember_SelectedIndexChanged(this, new EventArgs());
    }

    private void SetKeys()
    {
        //txtMeNo.Visible = false;
        //lblMeNo.Visible = false;      
        if (string.IsNullOrEmpty(Request.QueryString["InsId"]) || string.IsNullOrEmpty(Request.QueryString["PrePageMode"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["InsMngId"]))
        {
            Response.Redirect("Institue.aspx");
            return;
        }
        HiddenFieldInsManager["InsId"] = Request.QueryString["InsId"].ToString();
        HiddenFieldInsManager["PrePageMode"] = Request.QueryString["PrePageMode"];
        HiddenFieldInsManager["PageMode"] = Request.QueryString["PageMode"];
        HiddenFieldInsManager["InsMngId"] = Request.QueryString["InsMngId"];
        string InsId = Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString());
        PageMode = Utility.DecryptQS(HiddenFieldInsManager["PageMode"].ToString());
        InsMngId = Utility.DecryptQS(HiddenFieldInsManager["InsMngId"].ToString());
        TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
        InstitueManager.FindByCode(int.Parse(InsId));

        if (InstitueManager.Count > 0)
        {
            lblInsName.Text = "موسسه:" + InstitueManager[0]["InsName"].ToString();
        }
        else
        {
            Response.Redirect("Institue.aspx");
            return;
        }
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode();
    }

    private void SetMode()
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
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.InstitueManagerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        //Set Textboxe's & comboboxe's Enabled
        txtSSN.Enabled = false;
        txtName.Enabled = false;
        txtFather.Enabled = false;
        txtFamily.Enabled = false;
        txtDescription.Enabled = false;
        txtBrithDate.CausesValidation = false;
        txtAddress.Enabled = false;
        txtTell.Enabled = false;
        txtMobile.Enabled = false;
        cmbLicence.Enabled = false;
        cmbMajor.Enabled = false;
        cmbIsMember.Enabled = false;
        txtJobDuration.Enabled = false;
        txtInsShares.Enabled = false;
        txtBrithDate.Enabled = false;
        txtIdNo.Enabled = false;

        FillForm(int.Parse(InsMngId));

        RoundPanelInsManager.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        ClearForm();

        RoundPanelInsManager.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.InstitueManagerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        //ُSet Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        if (string.IsNullOrEmpty(InsMngId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnableControls();

        FillForm(int.Parse(InsMngId));
        cmbIsMember.Enabled = false;
        RoundPanelInsManager.HeaderText = "ویرایش";
    }

    private void FillForm(int InsMngId)
    {
        TSP.DataManager.InstitueManagerManager InstitueManagerManager = new TSP.DataManager.InstitueManagerManager();
        InstitueManagerManager.FindByCode(InsMngId);

        if (InstitueManagerManager.Count > 0)
        {
            txtDescription.Text = InstitueManagerManager[0]["Description"].ToString();
            txtAddress.Text = InstitueManagerManager[0]["Address"].ToString();
            txtBrithDate.Text = InstitueManagerManager[0]["BirthDate"].ToString();
            txtFamily.Text = InstitueManagerManager[0]["Family"].ToString();
            txtFather.Text = InstitueManagerManager[0]["Father"].ToString();
            txtName.Text = InstitueManagerManager[0]["Name"].ToString();
            txtSSN.Text = InstitueManagerManager[0]["SSN"].ToString();
            txtIdNo.Text = InstitueManagerManager[0]["IdNo"].ToString();
            txtMobile.Text = InstitueManagerManager[0]["MobileNo"].ToString();
            txtTell.Text = InstitueManagerManager[0]["Tel"].ToString();
            txtInsShares.Text = InstitueManagerManager[0]["InsShares"].ToString();
            txtJobDuration.Text = InstitueManagerManager[0]["JobDuration"].ToString();
            cmbLicence.DataBind();
            cmbMajor.DataBind();
            cmbMajor.SelectedIndex = cmbMajor.Items.IndexOfValue(InstitueManagerManager[0]["MjId"].ToString());
            cmbLicence.SelectedIndex = cmbLicence.Items.IndexOfValue(InstitueManagerManager[0]["LiId"].ToString());
            if (!Utility.IsDBNullOrNullValue(InstitueManagerManager[0]["MeId"]))
            {
                cmbIsMember.SelectedIndex = 0;
                txtMeNo.Text = InstitueManagerManager[0]["MeId"].ToString();
                txtAddress.Enabled = false;
                txtBrithDate.Enabled = false;
                txtEmail.Enabled = false;
                txtFamily.Enabled = false;
                txtFather.Enabled = false;
                txtIdNo.Enabled = false;
                txtMobile.Enabled = false;
                txtName.Enabled = false;
                txtSSN.Enabled = false;
                txtTell.Enabled = false;
                cmbLicence.Enabled = false;
                cmbMajor.Enabled = false;
            }
            else
                cmbIsMember.SelectedIndex = 1;
            // chbIsTeaching.Checked = Boolean.Parse(TeacherJobHistoryManager[0][""].ToString());

            //txtMeeting.Text = TeacherJobHistoryManager[0][" MeetingId"].ToString();
        }

    }

    private void EnableControls()
    {
        txtSSN.Enabled = true;
        txtName.Enabled = true;
        txtFather.Enabled = true;
        txtFamily.Enabled = true;
        txtDescription.Enabled = true;
        txtBrithDate.CausesValidation = true;
        txtAddress.Enabled = true;
        txtTell.Enabled = true;
        txtMobile.Enabled = true;
        cmbLicence.Enabled = true;
        cmbMajor.Enabled = true;
        txtInsShares.Enabled = true;
        txtJobDuration.Enabled = true;
    }

    private void DisableControls()
    {
        txtSSN.Enabled = false;
        txtName.Enabled = false;
        txtFather.Enabled = false;
        txtFamily.Enabled = false;
        txtDescription.Enabled = false;
        txtBrithDate.CausesValidation = false;
        txtAddress.Enabled = false;
        txtTell.Enabled = false;
        txtMobile.Enabled = false;
        txtBrithDate.Enabled = false;
        cmbLicence.Enabled = false;
        cmbMajor.Enabled = false;
        txtIdNo.Enabled = false;
        txtSSN.Enabled = false;
        txtJobDuration.Enabled = false;
        txtInsShares.Enabled = false;
        txtEmail.Enabled = false;
    }

    private void InsertInsManager()
    {
        TSP.DataManager.InstitueManagerManager InstitueManagerManager = new TSP.DataManager.InstitueManagerManager();
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        InstitueManagerManager.ClearBeforeFill = true;

        try
        {
            DataRow InsMngRow = InstitueManagerManager.NewRow();
            int InsId = int.Parse(Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString()));
            InsMngRow["InsId"] = InsId;
            DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(InsId, 0);
            if (dtInsCert.Rows.Count > 0)
            {
                int InsCId = int.Parse(dtInsCert.Rows[0]["InsCId"].ToString());
                InsMngRow["InsCId"] = InsCId;
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است.";
                return;
            }
            if (!string.IsNullOrEmpty(txtMeNo.Text))
            {
                DataTable dtInMng = InstitueManagerManager.SelectByInstitue(InsId);
                if (dtInMng.Rows.Count > 0)
                {
                    for (int i = 0; i < dtInMng.Rows.Count; i++)
                    {
                        if (!Utility.IsDBNullOrNullValue(dtInMng.Rows[i]["MeId"]))
                        {
                            string MeId = dtInMng.Rows[i]["MeId"].ToString();
                            if (MeId == txtMeNo.Text.Trim())
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "اطلاعات تکراری می باشد.";
                                return;
                            }
                        }
                    }
                }
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(int.Parse(txtMeNo.Text));
                if (MemberManager.Count > 0)
                {
                    InsMngRow["Name"] = MemberManager[0]["FirstName"].ToString();
                    InsMngRow["Family"] = MemberManager[0]["LastName"].ToString();
                    InsMngRow["Father"] = MemberManager[0]["FatherName"].ToString();
                    InsMngRow["BirthDate"] = MemberManager[0]["BirhtDate"].ToString();
                    InsMngRow["IdNo"] = MemberManager[0]["IdNo"].ToString();
                    InsMngRow["SSN"] = MemberManager[0]["SSN"].ToString();
                    InsMngRow["Tel"] = MemberManager[0]["HomeTel"].ToString();
                    InsMngRow["MobileNo"] = MemberManager[0]["MobileNo"].ToString();
                    InsMngRow["Email"] = MemberManager[0]["Email"].ToString();
                    InsMngRow["MeId"] = int.Parse(MemberManager[0]["MeId"].ToString());
                    ////
                    if (cmbLicence.SelectedIndex > -1)
                        InsMngRow["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString()); //LastLiId.Value.ToString();                        
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "مدرک تحصیلی نامشخص می باشد.";
                        return;
                    }

                    if (cmbMajor.SelectedIndex > -1)
                        InsMngRow["MjId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());//LastMjId.Value.ToString();                        
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "رشته تحصیلی نامشخص می باشد.";
                        return;
                    }

                    //if (cmbLicence.SelectedIndex > -1)
                    //    InsMngRow["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString()); //LastLiId.Value.ToString();
                    //if (cmbMajor.SelectedIndex > -1)
                    //    InsMngRow["MjId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());//LastMjId.Value.ToString();
                    ////                   
                    InsMngRow["Address"] = MemberManager[0]["HomeAdr"].ToString();
                    InsMngRow["JobDuration"] = txtJobDuration.Text;
                    InsMngRow["InsShares"] = txtInsShares.Text;
                    InsMngRow["Description"] = MemberManager[0]["Description"].ToString();
                    InsMngRow["InActive"] = false;
                    InsMngRow["UserId"] = Utility.GetCurrentUser_UserId();
                    InsMngRow["ModifiedDate"] = DateTime.Now;

                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است";
                    return;
                }
            }
            else
            {
                DataTable dtInMng = InstitueManagerManager.SelectByInstitue(InsId);
                if (dtInMng.Rows.Count > 0)
                {
                    for (int i = 0; i < dtInMng.Rows.Count; i++)
                    {
                        if (!Utility.IsDBNullOrNullValue(dtInMng.Rows[i]["MeId"]))
                        {
                            string SSN = dtInMng.Rows[i]["SSN"].ToString();
                            if (SSN == txtSSN.Text.Trim())
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "اطلاعات تکراری می باشد.";
                                return;
                            }
                        }
                    }
                }

                InsMngRow["Name"] = txtName.Text;
                InsMngRow["Family"] = txtFamily.Text;
                InsMngRow["Father"] = txtFather.Text;
                InsMngRow["BirthDate"] = txtBrithDate.Text;
                InsMngRow["IdNo"] = txtIdNo.Text;
                InsMngRow["SSN"] = txtSSN.Text;
                InsMngRow["Tel"] = txtTell.Text;
                InsMngRow["MobileNo"] = txtMobile.Text;
                InsMngRow["Email"] = txtEmail.Text;
                if (!string.IsNullOrEmpty(txtMeNo.Text))
                    InsMngRow["MeId"] = int.Parse(txtMeNo.Text.ToString());
                InsMngRow["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString());
                InsMngRow["MjId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());
                InsMngRow["Address"] = txtAddress.Text;
                InsMngRow["Description"] = txtDescription.Text;
                InsMngRow["JobDuration"] = txtJobDuration.Text;
                InsMngRow["InsShares"] = txtInsShares.Text;
                InsMngRow["InActive"] = false;
                InsMngRow["UserId"] = Utility.GetCurrentUser_UserId();
                InsMngRow["ModifiedDate"] = DateTime.Now;
            }
            InstitueManagerManager.AddRow(InsMngRow);

            int cn = InstitueManagerManager.Save();
            if (cn > 0)
            {
                cmbIsMember.Enabled = false;
                RoundPanelInsManager.HeaderText = "ویرایش";
                HiddenFieldInsManager["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldInsManager["InsMng"] = Utility.EncryptQS(InstitueManagerManager[0]["InsMngId"].ToString());


                TSP.DataManager.Permission per = TSP.DataManager.InstitueManagerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                //btn.Enabled = per.CanDelete;
                //btnDelete2.Enabled = per.CanDelete;
                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                // btnDisActive.Enabled = per.CanEdit;
                //  btnDisActive2.Enabled = per.CanEdit;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnDisActive"] = btnEdit.Enabled;
                // this.ViewState["BtnDelete"] = btnDelete.Enabled;

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void EditInsManager(int InsMngId)
    {
        TSP.DataManager.InstitueManagerManager InstitueManagerManager = new TSP.DataManager.InstitueManagerManager();
        try
        {
            InstitueManagerManager.FindByCode(InsMngId);
            InstitueManagerManager[0].BeginEdit();

            int InsId = int.Parse(Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString()));
            InstitueManagerManager[0]["InsId"] = InsId;
            InstitueManagerManager[0]["Name"] = txtName.Text;
            InstitueManagerManager[0]["Family"] = txtFamily.Text;
            InstitueManagerManager[0]["Father"] = txtFather.Text;
            InstitueManagerManager[0]["BirthDate"] = txtBrithDate.Text;
            InstitueManagerManager[0]["IdNo"] = txtIdNo.Text;
            InstitueManagerManager[0]["SSN"] = txtSSN.Text;
            InstitueManagerManager[0]["Tel"] = txtTell.Text;
            InstitueManagerManager[0]["InsShares"] = txtInsShares.Text;
            InstitueManagerManager[0]["JobDuration"] = txtJobDuration.Text;
            InstitueManagerManager[0]["MobileNo"] = txtMobile.Text;
            InstitueManagerManager[0]["Email"] = txtEmail.Text;
            if (!string.IsNullOrEmpty(txtMeNo.Text))
                InstitueManagerManager[0]["MeId"] = int.Parse(txtMeNo.Text.ToString());

            if (cmbLicence.SelectedIndex > -1)
                InstitueManagerManager[0]["LiId"] = int.Parse(cmbLicence.SelectedItem.Value.ToString());
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "مدرک تحصیلی نامشخص می باشد.";
                return;
            }

            if (cmbMajor.SelectedIndex > -1)
                InstitueManagerManager[0]["MjId"] = int.Parse(cmbMajor.SelectedItem.Value.ToString());
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رشته تحصیلی نامشخص می باشد.";
                return;
            }


            InstitueManagerManager[0]["Address"] = txtAddress.Text;
            InstitueManagerManager[0]["Description"] = txtDescription.Text;
            InstitueManagerManager[0]["InActive"] = false;
            InstitueManagerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            InstitueManagerManager[0]["ModifiedDate"] = DateTime.Now;

            InstitueManagerManager[0].EndEdit();

            int cn = InstitueManagerManager.Save();
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
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void DeleteInsManager(int InsMngId)
    {
        TSP.DataManager.InstitueManagerManager InstitueManagerManager = new TSP.DataManager.InstitueManagerManager();
        InstitueManagerManager.FindByCode(InsMngId);
        InstitueManagerManager[0].Delete();
        int cn = InstitueManagerManager.Save();
        if (cn > 0)
        {
            HiddenFieldInsManager["PageMode"] = Utility.EncryptQS("New");
            HiddenFieldInsManager["InsMngId"] = Utility.EncryptQS("");
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "حذف انجام شد.";
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int CurrentTaskOrder = -1;
        int TaskOrder = -1;
        //****TableId
        string InsId = Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString());
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.Institue;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, int.Parse(InsId));
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
        }
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveInstitueInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }

        if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
        {
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            TaskDoerManager.FindByTaskId(TaskId);

            if (TaskDoerManager.Count > 0)
            {
                int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
                NezamMemberChartManager.FindByNcId(NcId);

                int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

                LoginManager.FindByMeIdUltId(EmpId, 4);
                if (LoginManager.Count > 0)
                {
                    int userId = int.Parse(LoginManager[0]["UserId"].ToString());
                    int CurrentUserId = Utility.GetCurrentUser_UserId();
                    if (CurrentUserId == userId)
                    {
                        btnNew.Enabled = true;
                        btnNew2.Enabled = true;
                        btnEdit.Enabled = true;
                        btnEdit2.Enabled = true;
                    }
                    else
                    {

                        btnNew.Enabled = false;
                        btnNew2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                    }
                }
                else
                {
                    btnNew.Enabled = false;
                    btnNew2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                }
            }
            else
            {
                btnNew.Enabled = false;
                btnNew2.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
            }
        }
        else
        {
            btnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    private int FindInstitueCertificate(int InsId)
    {
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();

        DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(InsId, 0);
        int InsCertType = -1;
        if (dtInsCert.Rows.Count > 0)
        {
            InsCertType = int.Parse(dtInsCert.Rows[0]["Type"].ToString());
        }
        return InsCertType;


    }

    private void CheckCertificatePermission(int InsId)
    {
        int CertType = FindInstitueCertificate(InsId);
        if (CertType == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت پرونده مؤسسه انتخاب شده نامشخص است.";
            return;
        }
        string PageMode = Utility.DecryptQS(HiddenFieldInsManager["PageMode"].ToString());
        if (CertType == 1 || CertType == 2)
        {
            switch (PageMode)
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
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
    #endregion
}
