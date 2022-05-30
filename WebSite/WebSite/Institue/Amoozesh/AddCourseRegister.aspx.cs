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
using DevExpress.Web;

public partial class Institue_Amoozesh_AddCourseRegister : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            HiddenFieldCourseRegister["MeId"] = "";
            HiddenFieldCourseRegister["PType"] = "";
            HiddenFieldCourseRegister["PPId"] = "";

            if (Cache["CachePersonMember"] == null)
                Cache["CachePersonMember"] = new object();
            if (string.IsNullOrEmpty(Request.QueryString["PRId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
            {
                Response.Redirect("InstitueHome.aspx");
                return;
            }
            SetKey();

            string InsId = Utility.DecryptQS(HiddenFieldCourseRegister["InsId"].ToString());
            ObjdsPeriodPresent.SelectParameters[1].DefaultValue = InsId;

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
        }

        if (HiddenFieldCourseRegister["MeId"] != null && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString())))
        {
            int MeId = int.Parse(HiddenFieldCourseRegister["MeId"].ToString());
            FillMe(MeId);
        }
        if (HiddenFieldCourseRegister["PType"] != null && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PType"].ToString())))
        {
            int PType = int.Parse(HiddenFieldCourseRegister["PType"].ToString());
            int PPId = -1;
            if (HiddenFieldCourseRegister["PPId"] != null && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
            {
                PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
            }
            if (PType == 0)
            {
                FillPeriod(PPId);
            }
            if (PType == 1)
            {
                FillSeminar(PPId);
            }
        }
        if (!Utility.IsDBNullOrNullValue(Session["ConfLetter"]))
        {
            HpflpConfAttach.NavigateUrl=Session["ConfLetter"].ToString();
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        RoundPanelRegister.HeaderText = "جدید";
        HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldCourseRegister["PRId"] = "";
        EnableControls();
        ClearForm();
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EnableControls();
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        int PRId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString()));
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count == 1)
        {
            if (Convert.ToBoolean(PeriodRegisterManager[0]["IsMember"].ToString()))
            {
                txtTel.ValidationSettings.RegularExpression.ValidationExpression = "";
                txtAddress.Enabled = false;
                txtBirthPlace.Enabled = false;
                txtBrithDate.Enabled = false;
                txtDesc.Enabled = false;
                txtFatherName.Enabled = false;
                txtIdNo.Enabled = false;
                txtLastName.Enabled = false;
                txtMobileNo.Enabled = false;
                txtName.Enabled = false;
                txtSSN.Enabled = false;
                txtTel.Enabled = false;
            }
        }
        else
        {
            Response.Redirect("InstitueHome.aspx");
        }
        HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("Edit");
        RoundPanelRegister.HeaderText = "ویرایش";

        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldCourseRegister["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            switch (PageMode)
            {
                case "New":
                    Insert();
                    break;
                case "Edit":
                    if ((HiddenFieldCourseRegister["PRId"] == null) || (string.IsNullOrEmpty(HiddenFieldCourseRegister["PRId"].ToString())))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    int PRId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString()));
                    // EditPeriodRegister(PRId);
                    break;
            }
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CourseRegister.aspx?InsId=" + HiddenFieldCourseRegister["InsId"].ToString());
    }

    protected void cmbMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbMemberType.SelectedIndex == 0)
        {
            txtMeNo.Visible = true;
            lblMeId.Visible = true;
            txtTel.ValidationSettings.RegularExpression.ValidationExpression = "";
            txtAddress.ClientEnabled = false;
            txtBirthPlace.ClientEnabled = false;
            //txtBrithDate.ClientEnabled = false;
            txtBrithDate.Attributes.Add("disabled", "false");
            txtDesc.ClientEnabled = false;
            txtFatherName.ClientEnabled = false;
            txtIdNo.ClientEnabled = false;
            txtLastName.ClientEnabled = false;
            txtMobileNo.ClientEnabled = false;
            txtName.ClientEnabled = false;
            txtSSN.ClientEnabled = false;
            txtTel.ClientEnabled = false;

            cmbProvince.ClientEnabled = false;
            cmbMajor.ClientEnabled = false;
            txtGrade.ClientEnabled = false;
            txtFileNo.ClientEnabled = false;
            txtMeNoOtp.ClientVisible = false;
            lblMeNoOtp.ClientVisible = false;
            lblflpCon.ClientVisible = false;
            flpConfAttach.ClientVisible = false;
            HpflpConfAttach.ClientVisible = false;
            imgEndUploadImgflpConfAttach.ClientVisible = false;
            lblImageWarning.ClientVisible = false;


            txtMeNoOtp.Text = "";

            txtFileNo.Text = "";
            txtGrade.Text = "";
            cmbMajor.SelectedIndex = -1;
            cmbProvince.SelectedIndex = -1;

            txtMeNo.Text = "";
            txtAddress.Text = "";
            txtBirthPlace.Text = "";
            txtBrithDate.Text = "";
            txtDesc.Text = "";
            txtFatherName.Text = "";
            txtIdNo.Text = "";
            txtLastName.Text = "";
            txtMobileNo.Text = "";
            txtName.Text = "";
            txtSSN.Text = "";
            txtTel.Text = "";
            HpflpConfAttach.NavigateUrl = "";
            Session["ConfLetter"] = "";
        }

        if (cmbMemberType.SelectedIndex == 1)
        {
            txtMeNo.Visible = false;
            lblMeId.Visible = false;
            txtTel.ValidationSettings.RegularExpression.ValidationExpression = "0\\d{8,11}";

            txtAddress.ClientEnabled = true;
            txtBirthPlace.ClientEnabled = true;
            txtBrithDate.Attributes.Add("disabled", "true");
            // txtBrithDate.Enabled = true;
            txtDesc.ClientEnabled = true;
            txtFatherName.ClientEnabled = true;
            txtIdNo.ClientEnabled = true;
            txtLastName.ClientEnabled = true;
            txtMobileNo.ClientEnabled = true;
            txtName.ClientEnabled = true;
            txtSSN.ClientEnabled = true;
            txtTel.ClientEnabled = true;

            cmbProvince.ClientEnabled = true;
            cmbMajor.ClientEnabled = true;
            txtGrade.ClientEnabled = true;
            txtFileNo.ClientEnabled = true;
            txtMeNoOtp.ClientVisible = true;
            lblMeNoOtp.ClientVisible = true;
            flpConfAttach.ClientVisible = true;
            HpflpConfAttach.ClientVisible = true;

            txtMeNoOtp.Text = "";
            txtFileNo.Text = "";
            txtGrade.Text = "";
            cmbMajor.SelectedIndex = -1;
            cmbProvince.SelectedIndex = -1;

            txtMeNo.Text = "";
            txtAddress.Text = "";
            txtBirthPlace.Text = "";
            txtBrithDate.Text = "";
            txtDesc.Text = "";
            txtFatherName.Text = "";
            txtIdNo.Text = "";
            txtLastName.Text = "";
            txtMobileNo.Text = "";
            txtName.Text = "";
            txtSSN.Text = "";
            txtTel.Text = "";
            HpflpConfAttach.NavigateUrl = "";
            Session["ConfLetter"] = "";

        }
    }

    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtMeNo.Text))
        {
            int MeId = int.Parse(txtMeNo.Text.Trim());
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 1)
            {
                HiddenFieldCourseRegister["MeId"] = MeId.ToString();


                cmbProvince.DataBind();
                if (cmbProvince.Items.FindByValue(Utility.GetCurrentProvinceId().ToString()) != null)
                    cmbProvince.SelectedIndex = cmbProvince.Items.FindByValue(Utility.GetCurrentProvinceId().ToString()).Index;

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["HomeAdr"]))
                    txtAddress.Text = MemberManager[0]["HomeAdr"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["BirthPlace"]))
                    txtBirthPlace.Text = MemberManager[0]["BirthPlace"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["BirhtDate"]))
                    txtBrithDate.Text = MemberManager[0]["BirhtDate"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["Description"]))
                    txtDesc.Text = MemberManager[0]["Description"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FatherName"]))
                    txtFatherName.Text = MemberManager[0]["FatherName"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["IdNo"]))
                    txtIdNo.Text = MemberManager[0]["IdNo"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
                    txtLastName.Text = MemberManager[0]["LastName"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MobileNo"]))
                    txtMobileNo.Text = MemberManager[0]["MobileNo"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
                    txtName.Text = MemberManager[0]["FirstName"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
                    txtSSN.Text = MemberManager[0]["SSN"].ToString();

                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["HomeTel"]))
                    txtTel.Text = MemberManager[0]["HomeTel"].ToString();
                cmbMajor.DataBind();
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastMjId"]) && cmbMajor.Items.FindByValue(MemberManager[0]["LastMjId"].ToString()) != null)
                    cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(MemberManager[0]["LastMjId"].ToString()).Index;
                txtFileNo.Text = MemberManager[0]["FileNo"].ToString();
                txtGrade.Text = GetResGrade(MemberManager[0]) + "-" + GetMaxGradeName(MemberManager[0]);
            }
            else
            {
                txtAddress.Text = "";
                txtBirthPlace.Text = "";
                txtBrithDate.Text = "";
                txtDesc.Text = "";
                txtFatherName.Text = "";
                txtIdNo.Text = "";
                txtLastName.Text = "";
                txtMobileNo.Text = "";
                txtName.Text = "";
                txtSSN.Text = "";
                txtTel.Text = "";
                cmbProvince.SelectedIndex = -1;
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد عضویت وارد شده معتبر نمی باشد.";
            }
        }
    }

    protected void GridViewCourseRegister_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate")
        {
            e.Editor.Style["direction"] = "ltr";
        }
    }

    protected void GridViewCourseRegister_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
        {
            e.Cell.Style["direction"] = "ltr";
        }
    }

    protected void flpConfAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageConfAttach(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods
    protected string SaveImageConfAttach(UploadedFile uploadedFile)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() +"OthPer"+ Utility.GetCurrentUser_UserId()+ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Amoozesh/Period/") + ret) == true);
            string OthPerFileName = MapPath("~/image/Amoozesh/Period/") + ret;
            uploadedFile.SaveAs(OthPerFileName, true);
            Session["ConfLetter"] = OthPerFileName;
        }
        return ret;
    }

    private void SetKey()
    {
        try
        {
            HiddenFieldCourseRegister["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldCourseRegister["InsId"] = Server.HtmlDecode(Request.QueryString["InsId"]).ToString();
            HiddenFieldCourseRegister["PRId"] = Server.HtmlDecode(Request.QueryString["PRId"]).ToString();
            HiddenFieldCourseRegister["PPId"] = "";
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PageMode = Utility.DecryptQS(HiddenFieldCourseRegister["PageMode"].ToString());
        string InsId = Utility.DecryptQS(HiddenFieldCourseRegister["InsId"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            SetMode(PageMode);
            //  CheckWorkFlowPermission();
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
        string PRId = Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;

        FillForm(int.Parse(PRId));
        Disable();
        RoundPanelRegister.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        ClearForm();
        RoundPanelRegister.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string PRId = Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString());
        //ُSet Button's Enable       
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        if (string.IsNullOrEmpty(PRId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        EnableControls();
        FillForm(int.Parse(PRId));

        RoundPanelRegister.HeaderText = "ویرایش";
    }

    private void FillForm(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count == 1)
        {
            Boolean IsMemebr = Convert.ToBoolean(PeriodRegisterManager[0]["IsMember"]);
            if (IsMemebr)
            {
                cmbMemberType.SelectedIndex = 0;

                txtMeNo.Visible = true;
                txtMeNo.Text = PeriodRegisterManager[0]["MeId"].ToString();
                txtMeNo.Visible = true;
                lblMeId.Visible = true;
                cmbProvince.DataBind();
                if (cmbProvince.Items.FindByValue(Utility.GetCurrentProvinceId().ToString()) != null)
                    cmbProvince.SelectedIndex = cmbProvince.Items.FindByValue(Utility.GetCurrentProvinceId().ToString()).Index;
                txtAddress.ClientEnabled = false;
                txtBirthPlace.ClientEnabled = false;
                txtBrithDate.Attributes.Add("disabled", "false");
                txtDesc.ClientEnabled = false;
                txtFatherName.ClientEnabled = false;
                txtIdNo.ClientEnabled = false;
                txtLastName.ClientEnabled = false;
                txtMobileNo.ClientEnabled = false;
                txtName.ClientEnabled = false;
                txtSSN.ClientEnabled = false;
                txtTel.ClientEnabled = false;
                txtMeNoOtp.ClientVisible = false;
                lblMeNoOtp.ClientVisible = false;

                //image Upload invisible
                if (cmbMemberType.SelectedIndex == 0)
                {
                    lblflpCon.ClientVisible = false;
                    flpConfAttach.ClientVisible = false;
                    lblImageWarning.ClientVisible = false;
                    imgEndUploadImgflpConfAttach.ClientVisible = false;
                    HpflpConfAttach.ClientVisible = false;
                }

            }
            else
            {
                cmbMemberType.SelectedIndex = 1;
                cmbProvince.DataBind();
                if (!Utility.IsDBNullOrNullValue(PeriodRegisterManager[0]["ProvinceId"]) && cmbProvince.Items.FindByValue(PeriodRegisterManager[0]["ProvinceId"].ToString()) != null)
                    cmbProvince.SelectedIndex = cmbProvince.Items.FindByValue(PeriodRegisterManager[0]["ProvinceId"].ToString()).Index;
                txtMeNoOtp.Text = PeriodRegisterManager[0]["MeNo"].ToString();
            }
            txtGrade.Text = PeriodRegisterManager[0]["GradName"].ToString();
            txtBirthPlace.Text = PeriodRegisterManager[0]["BirthPlace"].ToString();
            txtAddress.Text = PeriodRegisterManager[0]["address"].ToString();
            txtBrithDate.Text = PeriodRegisterManager[0]["BirhtDate"].ToString();
            txtDesc.Text = PeriodRegisterManager[0]["Description"].ToString();
            txtFatherName.Text = PeriodRegisterManager[0]["FatherName"].ToString();
            txtIdNo.Text = PeriodRegisterManager[0]["IdNo"].ToString();
            txtLastName.Text = PeriodRegisterManager[0]["LastName"].ToString();
            txtMobileNo.Text = PeriodRegisterManager[0]["MobileNo"].ToString();
            txtName.Text = PeriodRegisterManager[0]["FirstName"].ToString();
            txtSSN.Text = PeriodRegisterManager[0]["SSN"].ToString();
            txtTel.Text = PeriodRegisterManager[0]["Tel"].ToString();
            txtFileNo.Text = PeriodRegisterManager[0]["FileNo"].ToString();
            cmbMajor.DataBind();
            if (!Utility.IsDBNullOrNullValue(PeriodRegisterManager[0]["MjId"]) && cmbMajor.Items.FindByValue(PeriodRegisterManager[0]["MjId"].ToString()) != null)
                cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(PeriodRegisterManager[0]["MjId"].ToString()).Index;
            cmbPaymentType.SelectedIndex = int.Parse(PeriodRegisterManager[0]["PaymentType"].ToString());
            cmbRegisterType.SelectedIndex = int.Parse(PeriodRegisterManager[0]["RegisterType"].ToString());
            txtPeriodTittle.Text = PeriodRegisterManager[0]["PName"].ToString() + "(" + PeriodRegisterManager[0]["PPCode"].ToString() + ")";
            decimal PeriodCost = Convert.ToDecimal(PeriodRegisterManager[0]["PeriodCost"].ToString());
            txtCost.Text = PeriodCost.ToString("#,#");
            //cmbPeriods.DataBind();
            //cmbPeriods.SelectedIndex = cmbPeriods.Items.FindByValue(PeriodRegisterManager[0]["PPId"]).Index;
        }
    }

    private void FillMe(int MeId)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 1)
        {
            txtAddress.Text = MemberManager[0]["HomeAdr"].ToString();
            txtBirthPlace.Text = MemberManager[0]["BirthPlace"].ToString();
            txtBrithDate.Text = MemberManager[0]["BirhtDate"].ToString();
            txtDesc.Text = MemberManager[0]["Description"].ToString();
            txtFatherName.Text = MemberManager[0]["FatherName"].ToString();
            txtIdNo.Text = MemberManager[0]["IdNo"].ToString();
            txtLastName.Text = MemberManager[0]["LastName"].ToString();
            txtMobileNo.Text = MemberManager[0]["MobileNo"].ToString();
            txtName.Text = MemberManager[0]["FirstName"].ToString();
            txtSSN.Text = MemberManager[0]["SSN"].ToString();
            txtTel.Text = MemberManager[0]["HomeTel"].ToString();
        }
    }

    private void FillPeriod(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            txtPeriodTittle.Text = PeriodPresentManager[0]["PeriodTitle"].ToString() + "(" + PeriodPresentManager[0]["PPCode"].ToString() + ")";
        }
    }

    private void FillSeminar(int PPId)
    {
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        SeminarManager.FindByCode(PPId);
        if (SeminarManager.Count == 1)
        {
            txtPeriodTittle.Text = SeminarManager[0]["Subject"].ToString();
        }
    }

    private void ClearForm()
    {
        txtAddress.Text = "";
        txtBrithDate.Text = "";
        txtBirthPlace.Text = "";
        txtBrithDate.Text = "";
        txtFatherName.Text = "";
        txtIdNo.Text = "";
        txtLastName.Text = "";
        txtMeNo.Text = "";
        txtMobileNo.Text = "";
        txtName.Text = "";
        txtSSN.Text = "";
        txtTel.Text = "";

        txtPeriodTittle.Text = "";
        txtCost.Text = "";
        txtMeNoOtp.Text = "";
        cmbRegisterType.SelectedIndex = 0;
        cmbMemberType.SelectedIndex = 0;
        cmbMemberType_SelectedIndexChanged(this, new EventArgs());
        cmbPaymentType.SelectedIndex = 0;
        ASPxTextBoxFicheCode.Text = "";
        HiddenFieldCourseRegister["PPId"] = "";
        HiddenFieldCourseRegister["PType"] = "";
        HiddenFieldCourseRegister["MeId"] = "";
        // cmbPeriods.SelectedIndex = 0;
    }

    private void Disable()
    {
        txtAddress.ClientEnabled = false;
        txtBrithDate.Attributes.Add("disabled", "false");
        txtBirthPlace.ClientEnabled = false;
        txtDesc.ClientEnabled = false;
        txtFatherName.ClientEnabled = false;
        txtIdNo.ClientEnabled = false;
        txtLastName.ClientEnabled = false;
        if (txtMeNo.ClientVisible == true)
            txtMeNo.ClientEnabled = false;
        txtMobileNo.ClientEnabled = false;
        txtName.ClientEnabled = false;
        txtSSN.ClientEnabled = false;
        txtTel.ClientEnabled = false;
        cmbMajor.ClientEnabled = false;
        cmbProvince.ClientEnabled = false;
        txtFileNo.ClientEnabled = false;
        txtGrade.ClientEnabled = false;
        cmbMemberType.Enabled = false;
        cmbPaymentType.Enabled = false;
        btnSearchPeriod.Visible = false;
        txtCost.ClientEnabled = false;
        cmbPaymentType.ClientEnabled = false;
        txtPeriodTittle.ClientEnabled = false;
        ASPxTextBoxFicheCode.ClientEnabled = false;
        cmbRegisterType.ClientEnabled = false;
        txtMeNoOtp.ClientEnabled = false;
    }

    private void EnableControls()
    {
        txtAddress.ClientEnabled = true;
        // txtBrithDate.ClientEnabled = true;
        txtBrithDate.Attributes.Add("disabled", "true");
        txtBirthPlace.ClientEnabled = true;
        txtDesc.ClientEnabled = true;
        txtFatherName.ClientEnabled = true;
        txtIdNo.ClientEnabled = true;
        txtLastName.ClientEnabled = true;
        txtMeNo.ClientEnabled = true;
        txtMobileNo.ClientEnabled = true;
        txtName.ClientEnabled = true;
        txtSSN.ClientEnabled = true;
        txtTel.ClientEnabled = true;

        //  btnSearch1.Visible = true;
        // this.ViewState["BtnSearch"] = btnSearch1.Visible;

        cmbMemberType.Enabled = true;
        cmbPaymentType.Enabled = true;

        cmbMajor.ClientEnabled = true;
        cmbProvince.ClientEnabled = true;
        txtFileNo.ClientEnabled = true;
        txtGrade.ClientEnabled = true;
        btnSearchPeriod.Visible = true;
        txtCost.ClientEnabled = true;
        cmbPaymentType.ClientEnabled = true;
        txtPeriodTittle.ClientEnabled = true;
        ASPxTextBoxFicheCode.ClientEnabled = true;
        cmbRegisterType.ClientEnabled = true;
        txtMeNoOtp.ClientEnabled = true;
    }

    #region Insert & Updates Methods
    private void Insert()
    {
        if ((HiddenFieldCourseRegister["PPId"] == null) || (string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            return;
        }
        int PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        int PType = -1;
        if ((HiddenFieldCourseRegister["PType"] != null) && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PType"].ToString())))
        {
            PType = int.Parse(HiddenFieldCourseRegister["PType"].ToString());
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            return;
        }
        #region Insert Period
        if (PType == 0)
        {
            if (CheckPeriod())
            {
                TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
                TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
                TSP.DataManager.PeriodOpinionManager PeriodOpinionManager = new TSP.DataManager.PeriodOpinionManager();
                TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();

                TSP.DataManager.AccountingSettingsManager SettingsManager = new TSP.DataManager.AccountingSettingsManager();
                TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();

                TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

                TSP.DataManager.AccountingDocument Document = new TSP.DataManager.AccountingDocument(transact, Utility.GetCurrentUser_AgentId(), -1, -1);
                TSP.DataManager.AccountingDocument Document2 = new TSP.DataManager.AccountingDocument(transact, Utility.GetCurrentUser_AgentId(), -1, -1);

                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();

                transact.Add(PeriodRegisterManager);
                transact.Add(PeriodPresentManager);
                transact.Add(PeriodOpinionManager);
                transact.Add(OtherPersonManager);
                transact.Add(SettingsManager);
                transact.Add(InstitueManager);
                transact.Add(MemberManager);


                try
                {
                    transact.BeginSave();
                    if (InsertPeriodRegister(PeriodRegisterManager, PeriodPresentManager, PeriodOpinionManager, OtherPersonManager))
                    {
                        if (Utility.CreateAccount())
                        {
                            int AccId = -1;
                            if (Convert.ToInt32(cmbMemberType.Value) == 0 && !string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
                            {
                                int MeId = int.Parse(HiddenFieldCourseRegister["MeId"].ToString());
                                MemberManager.FindByCode(MeId);
                                if (MemberManager.Count > 0)
                                    AccId = Convert.ToInt32(MemberManager[0]["AccId"]);
                            }
                            else
                                AccId = GetOtherPersonAccId(SettingsManager);

                            Document.AddDetails("Bed", Convert.ToDecimal(txtCost.Text), AccId, GetDescription());
                            Document.AddDetails("Bes", GetInstituteAmount(), GetInstituteAccId(InstitueManager, PeriodPresentManager), GetDescription());
                            Document.AddDetails("Bes", GetTrainingAmount(), GetTrainingEarningsAccId(SettingsManager), GetDescription());
                            Document.Save(GetDescription(), Convert.ToInt32(PeriodRegisterManager[0]["PRId"]), "", TSP.DataManager.AccountingTT.PeriodRegistration, Utility.GetCurrentUser_UserId());

                            Document2.Insert(GetMainBankAccId(SettingsManager), AccId, Convert.ToDecimal(txtCost.Text), GetDescription2(), Convert.ToInt32(PeriodRegisterManager[0]["PRId"]), ASPxTextBoxFicheCode.Text, TSP.DataManager.AccountingTT.PeriodRegistration, Utility.GetCurrentUser_UserId());
                        }
                        transact.EndSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }

                }
                catch (Exception err)
                {
                    transact.CancelSave();
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
        }
        #endregion

        #region Insert Seminar
        if (PType == 1)
        {
            if (CheckSeminar())
            {
                TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
                TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
                TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();

                TSP.DataManager.AccountingSettingsManager SettingsManager = new TSP.DataManager.AccountingSettingsManager();
                TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
                TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();

                TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

                TSP.DataManager.AccountingDocument Document = new TSP.DataManager.AccountingDocument(transact, Utility.GetCurrentUser_AgentId(), -1, -1);
                TSP.DataManager.AccountingDocument Document2 = new TSP.DataManager.AccountingDocument(transact, Utility.GetCurrentUser_AgentId(), -1, -1);

                transact.Add(PeriodRegisterManager);
                transact.Add(PeriodPresentManager);
                transact.Add(SeminarManager);
                transact.Add(SettingsManager);
                transact.Add(InstitueManager);
                transact.Add(OtherPersonManager);
                try
                {

                    transact.BeginSave();
                    if (InsertSeminarRegister(PeriodRegisterManager, SeminarManager, OtherPersonManager))
                    {
                        Document.AddDetails("Bed", Convert.ToDecimal(txtCost.Text), GetOtherPersonAccId(SettingsManager), GetDescription());
                        Document.AddDetails("Bes", GetInstituteAmount(), GetInstituteAccId(InstitueManager, PeriodPresentManager), GetDescription());
                        Document.AddDetails("Bes", GetTrainingAmount(), GetTrainingEarningsAccId(SettingsManager), GetDescription());
                        Document.Save(GetDescription(), Convert.ToInt32(PeriodRegisterManager[0]["PRId"]), "", TSP.DataManager.AccountingTT.SeminarRegistration, Utility.GetCurrentUser_UserId());

                        Document2.Insert(GetMainBankAccId(SettingsManager), GetOtherPersonAccId(SettingsManager), Convert.ToDecimal(txtCost.Text), GetDescription2(), Convert.ToInt32(PeriodRegisterManager[0]["PRId"]), ASPxTextBoxFicheCode.Text, TSP.DataManager.AccountingTT.SeminarRegistration, Utility.GetCurrentUser_UserId());

                        transact.EndSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
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
        }
        #endregion
    }

    private bool InsertPeriodRegister(TSP.DataManager.PeriodRegisterManager PeriodRegisterManager, TSP.DataManager.PeriodPresentManager PeriodPresentManager, TSP.DataManager.PeriodOpinionManager PeriodOpinionManager, TSP.DataManager.OtherPersonManager OtherPersonManager)
    {
        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        string EndRegisterDate = "";
        string PeriodTestDate = "";
        int PType = -1;
        int PPId = -1;
        int MeId = -1;

        if ((HiddenFieldCourseRegister["PPId"] != null) && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
        {
            PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

        }

        if (cmbMemberType.SelectedIndex == 0 && !string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
        {
            MeId = int.Parse(HiddenFieldCourseRegister["MeId"].ToString());
            DataTable dtPeReg = PeriodRegisterManager.SelectPeriodRegister(MeId, PPId, 0);
            if (dtPeReg.Rows.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "عضو انتخاب شده پیش از این در این دوره ثبت نام نموده است.";
                return false;
            }
        }
        else if (cmbMemberType.SelectedIndex == 1)
        {
            MeId = InsertOtherPerson(OtherPersonManager);
            if (MeId < 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                return false;
            }
        }


        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازیابی اطلاعات دوره انجام گرفته است.";
            return false;
        }
        Capacity = int.Parse(PeriodPresentManager[0]["Capacity"].ToString());
        RegisterCount = int.Parse(PeriodPresentManager[0]["CountRegister"].ToString());
        PeriodTestDate = PeriodPresentManager[0]["TestDate"].ToString();
        EndRegisterDate = PeriodPresentManager[0]["EndRegisterDate"].ToString();
        PType = int.Parse(PeriodPresentManager[0]["PeriodType"].ToString());

        int IsEndDate = string.Compare(EndRegisterDate, Utility.GetDateOfToday());
        int IsTestDate = string.Compare(PeriodTestDate, Utility.GetDateOfToday());
        if (Utility.IsAmoozeshConditionChecked() && IsTestDate <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل پایان امتحان دوره قادر به ثبت نام در این دوره نمی باشید.";
            return false;
        }

        RemainCapacity = Capacity - RegisterCount;
        if (RemainCapacity > 0)
        {
            DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();

            PeriodRegisterRow["PPId"] = PPId;//int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
            PeriodRegisterRow["MeId"] = MeId;

            PeriodRegisterRow["PaymentType"] = cmbPaymentType.SelectedIndex;
            if (cmbMemberType.SelectedIndex == 0)
                PeriodRegisterRow["IsMember"] = 1;
            if (cmbMemberType.SelectedIndex == 1)
                PeriodRegisterRow["IsMember"] = 0;
            if (!Utility.IsAmoozeshConditionChecked())
                PeriodRegisterRow["IsPassed"] = 1;
            if (cmbMemberType.SelectedIndex == 0)
            {
                if (!Utility.IsDBNullOrNullValue(Session["ConfLetter"]))
                {
                    PeriodRegisterRow["ConfAttachUrl"] = Session["ConfLetter"].ToString();   
                }
                
            }
                
            PeriodRegisterRow["IsConfirm"] = 1;
            PeriodRegisterRow["InActive"] = 0;
            PeriodRegisterRow["IsSeminar"] = 0;
            PeriodRegisterRow["RegisterType"] = cmbRegisterType.SelectedIndex;
            PeriodRegisterRow["RegisterDate"] = Utility.GetDateOfToday();
            PeriodRegisterRow["UserId"] = Utility.GetCurrentUser_UserId();
            PeriodRegisterRow["ModifiedDate"] = DateTime.Now;

            PeriodRegisterManager.AddRow(PeriodRegisterRow);

            int cn = PeriodRegisterManager.Save();
            if (cn > 0)
            {
                RoundPanelRegister.HeaderText = "مشاهده";
                HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("View");
                HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());
                btnSave2.Enabled = false;
                btnSave.Enabled = false;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                Disable();
                return true;
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
            return false;
        }

        this.DivReport.Visible = true;
        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        return false;
    }

    private int InsertOtherPerson(TSP.DataManager.OtherPersonManager OtherPersonManager)
    {
        DataRow dr = OtherPersonManager.NewRow();
        dr["FirstName"] = txtName.Text;
        dr["LastName"] = txtLastName.Text;
        dr["FatherName"] = txtFatherName.Text;
        dr["IdNo"] = txtIdNo.Text;
        dr["SSN"] = txtSSN.Text;
        dr["BirthPlace"] = txtBirthPlace.Text;
        dr["BirthDate"] = txtBrithDate.Text;
        dr["OtpType"] = (int)TSP.DataManager.OtherPersonType.PeriodAttender;
        dr["FileNo"] = txtFileNo.Text;
        //dr["FileNoDate"] = txtfile;
        dr["MeNo"] = txtMeNoOtp.Text;
        if (cmbProvince.SelectedItem != null && cmbProvince.SelectedItem.Value != null)
            dr["PrId"] = cmbProvince.SelectedItem.Value.ToString();
        dr["GradeName"] = txtGrade.Text.Trim();
        dr["Description"] = "";
        dr["Tel"] = txtTel.Text.Trim();
        dr["MobileNo"] = txtMobileNo.Text.Trim();
        dr["Address"] = txtAddress.Text.Trim();
        dr["InActive"] = 0;
        if (cmbMajor.SelectedItem != null && cmbMajor.SelectedItem.Value != null)
        {
            dr["MjId"] = cmbMajor.SelectedItem.Value.ToString();
            dr["MjName"] = cmbMajor.SelectedItem.Text;
        }
        //if (!Utility.IsDBNullOrNullValue(Session["ConfLetter"]))
        //dr["ImageUrl"] = Session["ConfLetter"].ToString(); 
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;

        OtherPersonManager.AddRow(dr);
        if (OtherPersonManager.Save() > 0)
        {
            return (Convert.ToInt32(OtherPersonManager[OtherPersonManager.Count - 1]["OtpId"]));
        }
        return -1;
    }

    private bool InsertSeminarRegister(TSP.DataManager.PeriodRegisterManager PeriodRegisterManager, TSP.DataManager.SeminarManager SeminarManager, TSP.DataManager.OtherPersonManager OtherPersonManager)
    {

        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        string EndDate = "";

        int PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        SeminarManager.FindByCode(PPId);
        if (SeminarManager.Count == 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازیابی اطلاعات سمینار انجام گرفته است.";
            return false;
        }
        Capacity = int.Parse(SeminarManager[0]["Capacity"].ToString());
        RegisterCount = int.Parse(SeminarManager[0]["CountRegister"].ToString());
        EndDate = SeminarManager[0]["EndDate"].ToString();
        int IsEndDate = string.Compare(EndDate, Utility.GetDateOfToday());
        if (Utility.IsAmoozeshConditionChecked() && IsEndDate <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل پایان رسیند سمینار قادر به ثبت نام در این سمینار نمی باشید.";
            return false;
        }

        RemainCapacity = Capacity - RegisterCount;
        if (RemainCapacity > 0)
        {
            DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();
            PeriodRegisterRow["PPId"] = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PPId"].ToString()));
            if (cmbMemberType.SelectedIndex == 0 && !string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
            {
                PeriodRegisterRow["MeId"] = int.Parse(HiddenFieldCourseRegister["MeId"].ToString());
            }
            else if (cmbMemberType.SelectedIndex == 1)
            {
                int MeId = InsertOtherPerson(OtherPersonManager);
                if (MeId < 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    return false;
                }
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

            PeriodRegisterRow["PaymentType"] = cmbPaymentType.SelectedIndex;
            if (cmbMemberType.SelectedIndex == 0)
                PeriodRegisterRow["IsMember"] = 1;
            if (cmbMemberType.SelectedIndex == 1)
                PeriodRegisterRow["IsMember"] = 0;
            PeriodRegisterRow["InActive"] = 0;
            PeriodRegisterRow["IsSeminar"] = 1;
            PeriodRegisterRow["RegisterType"] = cmbRegisterType.SelectedIndex;
            PeriodRegisterRow["RegisterDate"] = Utility.GetDateOfToday();
            PeriodRegisterRow["UserId"] = Utility.GetCurrentUser_UserId();
            PeriodRegisterRow["ModifiedDate"] = DateTime.Now;

            PeriodRegisterManager.AddRow(PeriodRegisterRow);

            int cn = PeriodRegisterManager.Save();
            if (cn > 0)
            {
                RoundPanelRegister.HeaderText = "مشاهده";
                HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("View");
                HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());                
                btnSave2.Enabled = false;
                btnSave.Enabled = false;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                Disable();
                return true;

            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
            return false;
        }

        this.DivReport.Visible = true;
        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        return false;

    }

    //private void Update(int PRId)
    //{
    //    TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
    //    TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
    //    TransactionManager.Add(AccountingManager);
    //    TransactionManager.Add(AccountingDetailManager);
    //    try
    //    {
    //        TransactionManager.BeginSave();
    //        AccountingDetailManager.SelectAccDetailByTableId(PRId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
    //        if (AccountingDetailManager.Count <= 0)
    //        {
    //            TransactionManager.CancelSave();
    //            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
    //            return;
    //        }
    //        AccountingDetailManager[0].BeginEdit();
    //        AccountingDetailManager[0]["Amount"] = txtaAmount.Text;
    //        AccountingDetailManager[0]["Description"] = txtaDesc.Text;
    //        AccountingDetailManager[0]["ModifedDate"] = DateTime.Now;
    //        AccountingDetailManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //        AccountingDetailManager[0].EndEdit();
    //        AccountingDetailManager.Save();

    //        int Accounting = Convert.ToInt32(AccountingDetailManager[0]["AccountingId"]);
    //        AccountingManager.FindByAccountingId(Accounting);
    //        if (AccountingManager.Count != 1)
    //        {
    //            TransactionManager.CancelSave();
    //            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
    //            return;
    //        }
    //        AccountingManager[0].BeginEdit();
    //        AccountingManager[0]["Type"] = cmbaType.SelectedItem.Value;
    //        AccountingManager[0]["Number"] = txtaNumber.Text;
    //        AccountingManager[0]["Description"] = txtaDesc.Text;
    //        AccountingManager[0]["Amount"] = txtaAmount.Text;
    //        AccountingManager[0]["ModifiedDate"] = DateTime.Now;
    //        AccountingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //        AccountingManager[0].EndEdit();
    //        AccountingManager.Save();
    //        TransactionManager.EndSave();
    //        SetMessage("ذخیره انجام شد.");
    //    }
    //    catch (Exception err)
    //    {
    //        TransactionManager.CancelSave();
    //        Utility.SaveWebsiteError(err);
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


    #endregion

    private bool CheckPeriod()
    {
        if (!Utility.IsAmoozeshConditionChecked())
            return true;
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.PeriodOpinionManager PeriodOpinionManager = new TSP.DataManager.PeriodOpinionManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();

        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        string EndRegisterDate = "";
        string PeriodTestDate = "";
        int PType = -1;
        int PPId = -1;
        int MeId = -1;

        if (!string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
        {
            MeId = int.Parse(HiddenFieldCourseRegister["MeId"].ToString());
        }
        //else
        //{
        //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        //}

        if ((HiddenFieldCourseRegister["PPId"] != null) && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
        {
            PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            return false; ;
        }
        //PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            if (cmbMemberType.SelectedIndex == 0)
            {
                string SSN = txtSSN.Text;
                DataTable dtPPOpinion = PeriodOpinionManager.SelectPeriodOpinionByType(PPId, 2);
                if (dtPPOpinion.Rows.Count > 0)
                {
                    string OpinionSSN = "";
                    for (int i = 0; i < dtPPOpinion.Rows.Count; i++)
                    {
                        OpinionSSN = dtPPOpinion.Rows[i]["SSN"].ToString();
                        int IsSSNEqual = -1;
                        IsSSNEqual = string.Compare(OpinionSSN, SSN);
                        if (IsSSNEqual == 0)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ثبت نام برای عضو انتخاب شده وجود ندارد.عضو انتخابی بازرس دوره می باشد.";
                            return false;
                        }
                    }
                }

            }
            if (cmbMemberType.SelectedIndex == 1)
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
                                return false;
                            }
                        }
                    }
                }
            }
            Capacity = int.Parse(PeriodPresentManager[0]["Capacity"].ToString());
            RegisterCount = int.Parse(PeriodPresentManager[0]["CountRegister"].ToString());
            PeriodTestDate = PeriodPresentManager[0]["TestDate"].ToString();
            EndRegisterDate = PeriodPresentManager[0]["EndRegisterDate"].ToString();
            PType = int.Parse(PeriodPresentManager[0]["PeriodType"].ToString());
            if (PType == 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تنها قادر به ثبت نام در آزمون دوره می باشید.";
                return false;
            }
            int IsEndDate = string.Compare(EndRegisterDate, Utility.GetDateOfToday());
            int IsTestDate = string.Compare(PeriodTestDate, Utility.GetDateOfToday());
            if (IsTestDate > 0)
            {
                RemainCapacity = Capacity - RegisterCount;
                if (RemainCapacity <= 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
                    return false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "قادر به ثبت نام در این دوره نمی باشید.";
                return false;
            }
        }
        return true;
    }

    private bool CheckSeminar()
    {
        if (!Utility.IsAmoozeshConditionChecked())
            return true;
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        string EndDate = "";

        int PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        SeminarManager.FindByCode(PPId);
        if (SeminarManager.Count == 1)
        {
            Capacity = int.Parse(SeminarManager[0]["Capacity"].ToString());
            RegisterCount = int.Parse(SeminarManager[0]["CountRegister"].ToString());
            EndDate = SeminarManager[0]["EndDate"].ToString();
            int IsEndDate = string.Compare(EndDate, Utility.GetDateOfToday());
            if (IsEndDate > 0)
            {
                RemainCapacity = Capacity - RegisterCount;
                if (RemainCapacity <= 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
                    return false;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "قادر به ثبت نام در این دوره نمی باشید.";
                return false;
            }
        }
        return true;
    }

    private string GetResGrade(System.Data.DataRow dr)
    {
        string res = "";
        if (!Utility.IsDBNullOrNullValue(dr["ObsGrdName"]) && dr["ObsGrdName"].ToString() != "---")
            res = "نظارت";
        if (!Utility.IsDBNullOrNullValue(dr["DesGrdName"]) && dr["DesGrdName"].ToString() != "---")
        {
            if (string.IsNullOrEmpty(res))
                res = "طراحی";
            else
                res += " وطراحی";
        }
        if (!Utility.IsDBNullOrNullValue(dr["ImpGrdName"]) && dr["ImpGrdName"].ToString() != "---")
        {
            if (string.IsNullOrEmpty(res))
                res = "اجرا";
            else
                res += " واجرا";
        }
        return res;

    }

    private string GetMaxGradeName(System.Data.DataRow dr)
    {
        string grade1 = "پایه یک";
        string grade2 = "پایه دو";
        string grade3 = "پایه سه";
        string arshad = "پایه ارشد";

        if ((!Utility.IsDBNullOrNullValue(dr["ImpGrdName"]) && dr["ImpGrdName"].ToString().Contains("1"))
            || (!Utility.IsDBNullOrNullValue(dr["ObsGrdName"]) && dr["ObsGrdName"].ToString().Contains("1"))
            || (!Utility.IsDBNullOrNullValue(dr["DesGrdName"]) && dr["DesGrdName"].ToString().Contains("1")))
            return grade1;
        if ((!Utility.IsDBNullOrNullValue(dr["ImpGrdName"]) && dr["ImpGrdName"].ToString().Contains("2"))
            || (!Utility.IsDBNullOrNullValue(dr["ObsGrdName"]) && dr["ObsGrdName"].ToString().Contains("2"))
            || (!Utility.IsDBNullOrNullValue(dr["DesGrdName"]) && dr["DesGrdName"].ToString().Contains("2")))
            return grade2;
        if ((!Utility.IsDBNullOrNullValue(dr["ImpGrdName"]) && dr["ImpGrdName"].ToString().Contains("3"))
            || (!Utility.IsDBNullOrNullValue(dr["ObsGrdName"]) && dr["ObsGrdName"].ToString().Contains("3"))
            || (!Utility.IsDBNullOrNullValue(dr["DesGrdName"]) && dr["DesGrdName"].ToString().Contains("3")))
            return grade3;
        return "";
    }
    /*************************************************************************************************************/
    #region Accounting Methods
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
        int PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count > 0)
        {
            InstitueManager.FindByCode(Convert.ToInt32(PeriodPresentManager[0]["InsId"]));
            if (InstitueManager.Count > 0)
                return Convert.ToInt32(InstitueManager[0]["AccId"]);
        }
        return -1;
    }

    private int GetOtherPersonAccId(TSP.DataManager.AccountingSettingsManager SettingsManager)
    {
        SettingsManager.FindBySData(TSP.DataManager.AccSettingsSData.OtherPersonAccount.ToString(), Utility.GetMainAgentId(), "Accounting");
        if (SettingsManager.Count > 0 && !string.IsNullOrEmpty(SettingsManager[0]["SValue"].ToString()))
            return Convert.ToInt32(SettingsManager[0]["SValue"]);
        return -1;
    }

    private decimal GetInstituteAmount()
    {
        decimal Amount = Convert.ToDecimal(txtCost.Text);
        //return Amount * 85 / 100;
        return Math.Round(Amount * 85 / 100);
    }

    private decimal GetTrainingAmount()
    {
        decimal Amount = Convert.ToDecimal(txtCost.Text);
        //return Amount * 15 / 100;
        return Amount - Math.Round(Amount * 85 / 100);
    }

    private string GetDescription()
    {
        string Des = "ثبت نام شخص " + " در دوره/سمینار آموزشی " + txtPeriodTittle.Text + " تاریخ برگزاری " + HiddenFieldCourseRegister["StartDate"].ToString() + " در تاریخ " + Utility.GetDateOfToday();
        return Des;
    }

    private string GetDescription2()
    {
        string Des = "شماره فیش " + ASPxTextBoxFicheCode.Text + " به نام آقای/خانم " + txtName.Text + " " + txtLastName.Text + " جهت ثبت نام در دوره/سمینار " + txtPeriodTittle.Text + " در تاریخ " + Utility.GetDateOfToday();
        return Des;
    }

    private bool CheckAccounts()
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.AccountingSettingsManager SettingsManager = new TSP.DataManager.AccountingSettingsManager();
        TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();

        int OtherPersonAccId = -1, InstitueAccId = -1, MainBankAccId = -1, TrainingEarningsAccId = -1;

        OtherPersonAccId = GetOtherPersonAccId(SettingsManager);
        InstitueAccId = GetInstituteAccId(InstitueManager, PeriodPresentManager);
        MainBankAccId = GetMainBankAccId(SettingsManager);
        TrainingEarningsAccId = GetTrainingEarningsAccId(SettingsManager);

        if (OtherPersonAccId == -1 || InstitueAccId == -1 || MainBankAccId == -1 || TrainingEarningsAccId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "در حال حاضر امکان ثبت نام وجود ندارد";
            return false;
        }
        return true;
    }
    #endregion

    #endregion

}
