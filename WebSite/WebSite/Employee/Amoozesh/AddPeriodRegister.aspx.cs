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

public partial class Employee_Amoozesh_AddPeriodRegister : System.Web.UI.Page
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
            SetKey();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            ViewState["btnEditVisible"] = btnEdit.Visible;
        }

        //if (HiddenFieldCourseRegister["MeId"] != null && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString())))
        //{
        //    int MeId = int.Parse(HiddenFieldCourseRegister["MeId"].ToString());
        //    FillMe(MeId);
        //}
        //if (HiddenFieldCourseRegister["PType"] != null && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PType"].ToString())))
        //{
        //    int PType = int.Parse(HiddenFieldCourseRegister["PType"].ToString());
        //    int PPId = -1;
        //    if (HiddenFieldCourseRegister["PPId"] != null && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
        //    {
        //        PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        //    }
        //    if (PType == 0)
        //    {
        //        FillPeriod(PPId);
        //    }
        //}
        if (this.ViewState["BtnSave"] != null)
            btnAutoSave2.Enabled = btnAutoSave.Enabled = this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (ViewState["btnEditVisible"] != null)
            btnEdit.Visible = btnEdit2.Visible = (bool)ViewState["btnEditVisible"];
        if (ViewState["btnAutoSaveVisible"] != null)
            this.ViewState["btnAutoSaveVisible"] = btnAutoSave.Visible;
        if (!Utility.IsDBNullOrNullValue(Session["ConfLetter"]))
        {
            HpflpConfAttach.NavigateUrl = Session["ConfLetter"].ToString();
            HpflpConfAttach.ClientVisible = true;
        }

        //  setControlByMemberType();

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PrPg"] != null)
        {
            if (Utility.DecryptQS(Request.QueryString["PrPg"].ToString()) == "Periods" && !string.IsNullOrEmpty(Request.QueryString["PPId"]))
            {
                int PPId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PPId"]));
                if (CheckIfCanNew(PPId))
                    Response.Redirect("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("NewRegister") + "&PRId=" + Utility.EncryptQS("-1") + "&PrPg=" + Utility.EncryptQS("Periods") + "&PPId=" + Request.QueryString["PPId"], true);
            }
            else if (Utility.DecryptQS(Request.QueryString["PrPg"].ToString()) == "MeLicence")
                Response.Redirect("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("New") + "&PRId=" + Utility.EncryptQS("-1") + "&PrPg=" + Utility.EncryptQS("MeLicence"));
        }
        else
            Response.Redirect("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("New") + "&PRId=" + Utility.EncryptQS("-1") + "&PrPg=" + Utility.EncryptQS("MeLicence"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int PRId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString()));
        if (!CheckIfCanEdit(PRId))
            return;
        SetEditModeKeys();
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        setControlByMemberType();
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
                    Insert(false);
                    break;
                case "Edit":
                    if ((HiddenFieldCourseRegister["PRId"] == null) || (string.IsNullOrEmpty(HiddenFieldCourseRegister["PRId"].ToString())))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    int PRId = int.Parse(Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString()));
                    Update(PRId);
                    break;
                case "NewRegister":
                    InsertRegistration();
                    break;
            }
        }

    }

    protected void btnAutoSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldCourseRegister["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (PageMode != "New")
        {
            SetMessage("تنها امکان ذخیره اتوماتیک برای گواهینامه های جدید وجود دارد");
        }
        Insert(true);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PrPg"] != null)
        {
            if (Utility.DecryptQS(Request.QueryString["PrPg"].ToString()) == "Periods" && !string.IsNullOrEmpty(Request.QueryString["PPId"]))
                Response.Redirect("PeriodAttender.aspx?PPId=" + Request.QueryString["PPId"]);
            //else if (Utility.DecryptQS(Request.QueryString["PrPg"].ToString()) == "MeLicence" && !string.IsNullOrEmpty(Request.QueryString["PPId"]))
            //    Response.Redirect("MemberLicence.aspx");
            else
                Response.Redirect("MemberLicence.aspx");
        }
        else
            Response.Redirect("MemberLicence.aspx");
    }

    protected void cmbMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbMemberType.SelectedIndex == 0)
        {
            SetMemberEnable();
            ClearMember();
        }
        else if (cmbMemberType.SelectedIndex == 1)
        {
            SetOtherPersonEnable();
            ClearOtherPerson();
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

    protected void CallbackPanelMain_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        switch (e.Parameter)
        {
            case "RegisterTypeChange":
                if (HiddenFieldCourseRegister["PPId"] != null && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
                {
                    int PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
                    FillPeriod(PPId);
                }
                break;
        }
        setControlByMemberType();
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
                ret = Path.GetRandomFileName() + "OthPer" + Utility.GetCurrentUser_UserId() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Amoozesh/Period/") + ret) == true);
            string OthPerFileName = "~/image/Amoozesh/Period/" + ret;
            uploadedFile.SaveAs(MapPath(OthPerFileName), true);
            Session["ConfLetter"] = OthPerFileName;
        }
        return ret;
    }

    private void setControlByMemberType()
    {
        if (cmbMemberType.SelectedIndex == 0)
        {
            SetMemberEnable();
        }
        else if (cmbMemberType.SelectedIndex == 1)
        {
            SetOtherPersonEnable();
        }
    }

    private void SetMemberEnable()
    {
        txtMeNo.ClientVisible = true;
        lblMeId.ClientVisible = true;
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


    }

    private void SetOtherPersonEnable()
    {
        txtMeNo.ClientVisible = false;
        lblMeId.ClientVisible = false;
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

        lblflpCon.ClientVisible = true;
        flpConfAttach.ClientVisible = true;


    }

    #region Clear
    private void ClearMember()
    {
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

    private void ClearOtherPerson()
    {
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
        txtMeNoOtp.Text = "";
        cmbRegisterType.SelectedIndex = 0;
        cmbMemberType.SelectedIndex = 0;
        cmbMemberType_SelectedIndexChanged(this, new EventArgs());
    }
    #endregion

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #region Set Control
    private void SetKey()
    {
        if (string.IsNullOrEmpty(Request.QueryString["PRId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]))
        {
            Response.Redirect("MemberLicence.aspx");
            return;
        }
        try
        {
            ObjdsPeriodPresent.SelectParameters["ISOutTimePeriodReg"].DefaultValue = ((int)TSP.DataManager.PeriodRegisterType.OutOfTime).ToString();
            HiddenFieldCourseRegister["PageMode"] = Server.HtmlDecode(Request.QueryString["PgMd"].ToString());
            HiddenFieldCourseRegister["PRId"] = Server.HtmlDecode(Request.QueryString["PRId"]).ToString();

            if (!string.IsNullOrEmpty(Request.QueryString["PPId"]))
                HiddenFieldCourseRegister["PPId"] = Utility.DecryptQS(Request.QueryString["PPId"].ToString());
            else
                HiddenFieldCourseRegister["PPId"] = "";

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        string PageMode = Utility.DecryptQS(HiddenFieldCourseRegister["PageMode"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
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
            case "NewRegister":
                SetNewRegisterModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        string PRId = Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnAutoSave.Enabled = btnAutoSave2.Enabled = false;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        FillForm(int.Parse(PRId));
        setControlByMemberType();
        Disable();
        RoundPanelRegister.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnAutoSave.Enabled = btnAutoSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        ClearForm();
        ResetHiddenFields();
        RoundPanelRegister.HeaderText = "جدید";
        RoundPanelAccounting.Visible = false;
        ViewState["btnEditVisible"] = btnEdit.Visible = btnEdit2.Visible = false;
        Title = "ثبت گواهینامه دوره آموزشی خارج از نوبت";
        cmbRegisterType.Enabled = false;
    }


    private void SetEditModeKeys()
    {
        string PRId = Utility.DecryptQS(HiddenFieldCourseRegister["PRId"].ToString());
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnAutoSave.Enabled = btnAutoSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        if (string.IsNullOrEmpty(PRId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("Edit");


        //  EnableControls();
        setControlByMemberType();
        Disable();


        FillForm(int.Parse(PRId));

        RoundPanelRegister.HeaderText = "ویرایش";

        //btnSearchPeriod.Visible = false;
        //txtMeNo.ClientEnabled = false;
        //cmbMemberType.Enabled = false;
        //cmbRegisterType.ClientEnabled = false;

        RoundPanelAccounting.Enabled = true;
        lblCost.Visible = true;
    }

    private void SetNewRegisterModeKeys()
    {
        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = true;
        this.ViewState["btnAutoSaveVisible"] = btnAutoSave.Visible = btnAutoSave2.Visible = false;
        ClearForm();
        if (HiddenFieldCourseRegister["PPId"] != null && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
        {
            int PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
            FillPeriod(PPId);
        }
        txtaDate.Text = Utility.GetDateOfToday();
        cmbRegisterType.ClientEnabled = true;
        RoundPanelAccounting.Visible = true;
        lblWorkFlowState.Visible = false;
        Title = "ثبت نام دوره آموزشی";
        RoundPanelRegister.HeaderText = "ثبت نام جدید";
    }
    #endregion

    #region FillForm
    private void FillForm(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //   PeriodRegisterManager.FindByCode(PRId);
        DataTable dtPeriodRegister = PeriodRegisterManager.SelectPeriodRegister(PRId, -1, -1, -1, -1, -1, -1);
        if (dtPeriodRegister.Rows.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (Convert.ToInt32(dtPeriodRegister.Rows[0]["RegisterType"]) != (int)TSP.DataManager.PeriodRegisterType.OutOfTime)
        {
            this.ViewState["btnAutoSaveVisible"] = btnAutoSave.Visible = btnAutoSave2.Visible = false;
            Title = "ثبت نام دوره آموزشی";
            RoundPanelAccounting.Visible = true;
            lblWorkFlowState.Visible = false;
            ViewState["btnEditVisible"] = btnEdit.Visible = btnEdit2.Visible = true;

            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
            AccountingDetailManager.SelectAccDetailByTableId(Convert.ToInt32(dtPeriodRegister.Rows[0]["PRId"]), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
            if (AccountingDetailManager.Count > 0)
            {
                txtaAmount.Text = (Convert.ToInt32(AccountingDetailManager[0]["DetailAmount"])).ToString("#,#");
                txtaDate.Text = AccountingDetailManager[0]["FishDate"].ToString();
                txtaNumber.Text = AccountingDetailManager[0]["Number"].ToString();
                txtaDesc.Text = AccountingDetailManager[0]["description"].ToString();
            }
        }
        else
        {
            this.ViewState["btnAutoSaveVisible"] = btnAutoSave.Visible = btnAutoSave2.Visible = true;
            Title = "ثبت گواهینامه دوره آموزشی خارج از نوبت";
            RoundPanelAccounting.Visible = false;
            lblWorkFlowState.Visible = true;
            ViewState["btnEditVisible"] = btnEdit.Visible = btnEdit2.Visible = false;

            DataTable dtWFState = WorkFlowStateManager.SelectLastStateByWfCode((int)TSP.DataManager.WorkFlows.PeriodRegisterLicenceOutOfTime, PRId);
            if (dtWFState.Rows.Count == 1)
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت درخواست: " + dtWFState.Rows[0]["TaskName"].ToString();
            }
            else
            {
                lblWorkFlowState.Text = "وضعیت درخواست:نامشخص ";
            }
        }
        Boolean IsMemebr = Convert.ToBoolean(dtPeriodRegister.Rows[0]["IsMember"]);
        if (IsMemebr)
        {
            cmbMemberType.SelectedIndex = 0;
            txtMeNo.Text = dtPeriodRegister.Rows[0]["MeId"].ToString();
            cmbProvince.DataBind();
            if (cmbProvince.Items.FindByValue(Utility.GetCurrentProvinceId().ToString()) != null)
                cmbProvince.SelectedIndex = cmbProvince.Items.FindByValue(Utility.GetCurrentProvinceId().ToString()).Index;

            lblflpCon.ClientVisible = false;
            flpConfAttach.ClientVisible = false;
            lblImageWarning.ClientVisible = false;
            imgEndUploadImgflpConfAttach.ClientVisible = false;
            HpflpConfAttach.ClientVisible = false;

        }
        else
        {
            cmbMemberType.SelectedIndex = 1;
            cmbProvince.DataBind();
            if (!Utility.IsDBNullOrNullValue(dtPeriodRegister.Rows[0]["ProvinceId"]) && cmbProvince.Items.FindByValue(dtPeriodRegister.Rows[0]["ProvinceId"].ToString()) != null)
                cmbProvince.SelectedIndex = cmbProvince.Items.FindByValue(dtPeriodRegister.Rows[0]["ProvinceId"].ToString()).Index;
            txtMeNoOtp.Text = dtPeriodRegister.Rows[0]["MeNo"].ToString();
        }
        txtGrade.Text = dtPeriodRegister.Rows[0]["GradName"].ToString();
        txtBirthPlace.Text = dtPeriodRegister.Rows[0]["BirthPlace"].ToString();
        txtAddress.Text = dtPeriodRegister.Rows[0]["address"].ToString();
        txtBrithDate.Text = dtPeriodRegister.Rows[0]["BirhtDate"].ToString();
        txtDesc.Text = dtPeriodRegister.Rows[0]["Description"].ToString();
        txtFatherName.Text = dtPeriodRegister.Rows[0]["FatherName"].ToString();
        txtIdNo.Text = dtPeriodRegister.Rows[0]["IdNo"].ToString();
        txtLastName.Text = dtPeriodRegister.Rows[0]["LastName"].ToString();
        txtMobileNo.Text = dtPeriodRegister.Rows[0]["MobileNo"].ToString();
        txtName.Text = dtPeriodRegister.Rows[0]["FirstName"].ToString();
        txtSSN.Text = dtPeriodRegister.Rows[0]["SSN"].ToString();
        txtTel.Text = dtPeriodRegister.Rows[0]["Tel"].ToString();
        txtFileNo.Text = dtPeriodRegister.Rows[0]["FileNo"].ToString();
        cmbMajor.DataBind();
        if (!Utility.IsDBNullOrNullValue(dtPeriodRegister.Rows[0]["MjId"]) && cmbMajor.Items.FindByValue(dtPeriodRegister.Rows[0]["MjId"].ToString()) != null)
            cmbMajor.SelectedIndex = cmbMajor.Items.FindByValue(dtPeriodRegister.Rows[0]["MjId"].ToString()).Index;
        // cmbPaymentType.SelectedIndex = int.Parse(PeriodRegisterManager[0]["PaymentType"].ToString());
        cmbRegisterType.SelectedIndex = int.Parse(dtPeriodRegister.Rows[0]["RegisterType"].ToString());
        txtPeriodTittle.Text = dtPeriodRegister.Rows[0]["PName"].ToString() + "(" + dtPeriodRegister.Rows[0]["PPCode"].ToString() + ")";

        if (Convert.ToInt32(dtPeriodRegister.Rows[0]["RegisterType"]) == (int)TSP.DataManager.PeriodRegisterType.OnlyExam)
        {
            if (!Utility.IsDBNullOrNullValue(dtPeriodRegister.Rows[0]["TestCost"]))
            {
                txtaAmount.Text = Convert.ToInt32(dtPeriodRegister.Rows[0]["TestCost"]).ToString();
                lblCost.Text = "مبلغ قابل پرداخت:" + Convert.ToInt32(dtPeriodRegister.Rows[0]["TestCost"]).ToString("#,#");
            }
            else
            {
                txtaAmount.Text = "0";
                lblCost.Text = "مبلغ قابل پرداخت:" + "0";
            }
        }
        else if (Convert.ToInt32(dtPeriodRegister.Rows[0]["RegisterType"]) == (int)TSP.DataManager.PeriodRegisterType.PeriodAndExam)
        {
            if (!Utility.IsDBNullOrNullValue(dtPeriodRegister.Rows[0]["PeriodCost"]))
            {
                txtaAmount.Text = Convert.ToInt32(dtPeriodRegister.Rows[0]["PeriodCost"]).ToString();
                lblCost.Text = "مبلغ قابل پرداخت:" + Convert.ToInt32(dtPeriodRegister.Rows[0]["PeriodCost"]).ToString("#,#");
            }
            else
            {
                txtaAmount.Text = "0";
                lblCost.Text = "مبلغ قابل پرداخت:" + "0";
            }
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
            if (cmbRegisterType.SelectedItem != null && cmbRegisterType.SelectedItem.Value != null)
            {
                if (Convert.ToInt32(cmbRegisterType.SelectedItem.Value) == (int)TSP.DataManager.PeriodRegisterType.OnlyExam)
                {
                    if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["TestCost"]))
                    {
                        txtaAmount.Text = Convert.ToInt32(PeriodPresentManager[0]["TestCost"]).ToString();
                        lblCost.Text = "مبلغ قابل پرداخت:" + Convert.ToInt32(PeriodPresentManager[0]["TestCost"]).ToString("#,#");
                    }
                    else
                    {
                        txtaAmount.Text = "0";
                        lblCost.Text = "مبلغ قابل پرداخت:" + "0";
                    }
                }
                else if (Convert.ToInt32(cmbRegisterType.SelectedItem.Value) == (int)TSP.DataManager.PeriodRegisterType.PeriodAndExam)
                {
                    if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["PeriodCost"]))
                    {
                        txtaAmount.Text = Convert.ToInt32(PeriodPresentManager[0]["PeriodCost"]).ToString();
                        lblCost.Text = "مبلغ قابل پرداخت:" + Convert.ToInt32(PeriodPresentManager[0]["PeriodCost"]).ToString("#,#");
                    }
                    else
                    {
                        txtaAmount.Text = "0";
                        lblCost.Text = "مبلغ قابل پرداخت:" + "0";
                    }
                }
            }
            //txtaDesc.Text = "فیش  پرداخت الکترونیکی مبلغ " + txtaAmount.Text + "ريال" + " بابت " + TSP.DataManager.TechnicalServices.AccTypeManager.FindAccTypeName((int)TSP.DataManager.TSAccountingAccType.PeriodRegister) + "  در تاریخ" + Utility.GetDateOfToday() + " توسط " + Utility.GetCurrentUser_FullName() + "ثبت گردید";
        }
    }
    #endregion

    private void ResetHiddenFields()
    {
        HiddenFieldCourseRegister["PPId"] = "";
        HiddenFieldCourseRegister["PType"] = "";
        HiddenFieldCourseRegister["MeId"] = "";
    }

    #region Set Enable
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
        btnSearchPeriod.Visible = false;
        txtPeriodTittle.ClientEnabled = false;
        cmbRegisterType.ClientEnabled = false;
        txtMeNoOtp.ClientEnabled = false;
        RoundPanelAccounting.Enabled = false;
        lblCost.Visible = false;

        flpConfAttach.ClientVisible = false;

    }

    private void EnableControls()
    {
        txtName.ClientEnabled = true;
        txtLastName.ClientEnabled = true;
        txtFatherName.ClientEnabled = true;
        txtSSN.ClientEnabled = true;
        txtIdNo.ClientEnabled = true;
        txtTel.ClientEnabled = true;
        txtMobileNo.ClientEnabled = true;
        txtAddress.ClientEnabled = true;
        txtBrithDate.Attributes.Add("disabled", "true");
        txtBirthPlace.ClientEnabled = true;
        txtDesc.ClientEnabled = true;
        cmbProvince.ClientEnabled = true;
        txtMeNo.ClientEnabled = true;
        cmbMemberType.Enabled = true;
        txtFileNo.ClientEnabled = true;

        cmbMajor.ClientEnabled = true;

        txtGrade.ClientEnabled = true;
        btnSearchPeriod.Visible = true;
        // txtPeriodTittle.ClientEnabled = true;
        cmbRegisterType.ClientEnabled = true;
        txtMeNoOtp.ClientEnabled = true;
        RoundPanelAccounting.Enabled = true;
    }
    #endregion

    #region Insert & Updates Methods
    private void Insert(Boolean IsAutoSave)
    {
        if ((HiddenFieldCourseRegister["PPId"] == null) || (string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            return;
        }
        int PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        #region Insert Period
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.PeriodOpinionManager PeriodOpinionManager = new TSP.DataManager.PeriodOpinionManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(transact);
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        transact.Add(WorkFlowStateManager);
        transact.Add(PeriodRegisterManager);
        transact.Add(PeriodPresentManager);
        transact.Add(PeriodOpinionManager);
        transact.Add(OtherPersonManager);


        try
        {
            transact.BeginSave();
            int IsConfirm = 0;
            if (IsAutoSave)
                IsConfirm = 1;
            int PRId = InsertPeriodRegister((int)TSP.DataManager.PeriodRegisterType.OutOfTime, PeriodRegisterManager, PeriodPresentManager, OtherPersonManager, IsConfirm);

            if (PRId <= 0)
            {
                // SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                transact.CancelSave();
                return;
            }
            int TableId = PRId;
            if (!IsAutoSave)
            {
                int TaskId = -1;
                int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodRegLicenceReqInfo;
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                if (WorkFlowTaskManager.Count != 1)
                {
                    SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    transact.CancelSave();
                    return;
                }
                TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                int CurrentMeId = Utility.GetCurrentUser_MeId();
                int CurrentNmcId = FindNmcId(TaskId);
                if (CurrentNmcId <= 0)
                {
                    transact.CancelSave();
                }
                int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
                if (WfStart <= 0)
                {
                    SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    transact.CancelSave();
                    return;
                }
            }
            else
            {
                if (!InsertWorkFlow(TableId, WorkFlowStateManager, WorkFlowTaskManager))
                {
                    transact.CancelSave();
                    return;
                }
            }
            transact.EndSave();
            HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());
            if (!IsAutoSave)
            {
                SetWFState((int)TSP.DataManager.WorkFlowTask.SavePeriodRegLicenceReqInfo);
                SetEditModeKeys();
            }
            else
            {
                SetWFState((int)TSP.DataManager.WorkFlowTask.ConfirmingPeriodRegLicenceReqAndEndProccess);
                SetViewModeKeys();
            }
            SetMessage("ذخیره انجام شد.");


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
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        #endregion
    }

    private int InsertPeriodRegister(int RegisterType, TSP.DataManager.PeriodRegisterManager PeriodRegisterManager, TSP.DataManager.PeriodPresentManager PeriodPresentManager, TSP.DataManager.OtherPersonManager OtherPersonManager, int Isconfirm = 0)
    {
        int ReturnValue = -1;
        int Capacity = 0;
        int RemainCapacity = 0;
        int RegisterCount = 0;
        string EndRegisterDate = "";
        string PeriodTestDate = "";
        int PType = -1;
        int PPId = -1;
        int MeId = -1;

        if (!string.IsNullOrEmpty(txtMeNo.Text))
            MeId = int.Parse(txtMeNo.Text);
        else if (cmbMemberType.SelectedIndex == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "کد عضویت را وارد نمایید";
            return ReturnValue;
        }

        if ((HiddenFieldCourseRegister["PPId"] != null) && (!string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
        {
            PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
        }
        else
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return -1;
        }

        if (cmbMemberType.SelectedIndex == 0 && !string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
        {
            MeId = int.Parse(HiddenFieldCourseRegister["MeId"].ToString());
            DataTable dtPeReg = PeriodRegisterManager.SelectPeriodRegister(MeId, PPId, 0);
            dtPeReg.DefaultView.RowFilter = "InActive=0";
            if (dtPeReg.DefaultView.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "عضو انتخاب شده پیش از این در این دوره ثبت نام نموده است.";
                return ReturnValue;
            }
        }
        else if (cmbMemberType.SelectedIndex == 1)
        {
            MeId = InsertOtherPerson(OtherPersonManager);
            HiddenFieldCourseRegister["MeId"] = MeId;
            if (MeId < 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                return ReturnValue;
            }
        }


        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازیابی اطلاعات دوره انجام گرفته است.";
            return ReturnValue;
        }
        Capacity = int.Parse(PeriodPresentManager[0]["Capacity"].ToString());
        RegisterCount = int.Parse(PeriodPresentManager[0]["CountRegister"].ToString());
        //PeriodTestDate = PeriodPresentManager[0]["TestDate"].ToString();
        //EndRegisterDate = PeriodPresentManager[0]["EndRegisterDate"].ToString();
        //PType = int.Parse(PeriodPresentManager[0]["PeriodType"].ToString());

        //int IsEndDate = string.Compare(EndRegisterDate, Utility.GetDateOfToday());
        //int IsTestDate = string.Compare(PeriodTestDate, Utility.GetDateOfToday());
        //if (Utility.IsAmoozeshConditionChecked() && IsTestDate <= 0)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "به دلیل پایان امتحان دوره قادر به ثبت نام در این دوره نمی باشید.";
        //    return false;
        //}

        RemainCapacity = Capacity - RegisterCount;
        if (RemainCapacity <= 0 && RegisterType == (int)TSP.DataManager.PeriodRegisterType.PeriodAndExam)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ظرفیت دوره مورد نظر تکمیل می باشد.";
            return ReturnValue;
        }
        DataRow PeriodRegisterRow = PeriodRegisterManager.NewRow();

        PeriodRegisterRow["PPId"] = PPId;
        PeriodRegisterRow["MeId"] = MeId;
        if (cmbaType.SelectedItem != null && cmbaType.SelectedItem.Value.ToString() == "1")
            PeriodRegisterRow["PaymentType"] = 1;//فیش
        else if (cmbaType.SelectedItem != null && cmbaType.SelectedItem.Value.ToString() == "3")
            PeriodRegisterRow["PaymentType"] = 2;//دستگاه کارت خوان
        if (cmbMemberType.SelectedIndex == 0)
            PeriodRegisterRow["IsMember"] = 1;
        if (cmbMemberType.SelectedIndex == 1)
            PeriodRegisterRow["IsMember"] = 0;
        PeriodRegisterRow["IsPassed"] = 1;
        PeriodRegisterRow["IsConfirm"] = Isconfirm;
        PeriodRegisterRow["InActive"] = 0;
        PeriodRegisterRow["IsSeminar"] = 0;
        PeriodRegisterRow["RegisterType"] = RegisterType;
        PeriodRegisterRow["RegisterDate"] = Utility.GetDateOfToday();
        PeriodRegisterRow["UserId"] = Utility.GetCurrentUser_UserId();
        PeriodRegisterRow["ModifiedDate"] = DateTime.Now;

        if (cmbMemberType.SelectedIndex == 1)
        {
            if (!Utility.IsDBNullOrNullValue(Session["ConfLetter"]))
            {
                PeriodRegisterRow["ConfAttachUrl"] = HpflpConfAttach.NavigateUrl = Session["ConfLetter"].ToString();
                HpflpConfAttach.ClientVisible = true;
            }

        }

        PeriodRegisterManager.AddRow(PeriodRegisterRow);

        if (PeriodRegisterManager.Save() <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            return ReturnValue;
        }
        ReturnValue = Convert.ToInt32(PeriodRegisterManager[PeriodRegisterManager.Count - 1]["PRId"]);
        return ReturnValue;
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
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;

        OtherPersonManager.AddRow(dr);
        if (OtherPersonManager.Save() > 0)
        {
            return (Convert.ToInt32(OtherPersonManager[OtherPersonManager.Count - 1]["OtpId"]));
        }
        return -1;
    }

    private void InsertRegistration()
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();

        transact.Add(PeriodRegisterManager);
        transact.Add(PeriodPresentManager);
        transact.Add(OtherPersonManager);
        transact.Add(AccountingManager);
        transact.Add(AccountingDetailManager);
        try
        {
            transact.BeginSave();
            if ((HiddenFieldCourseRegister["PPId"] == null) || (string.IsNullOrEmpty(HiddenFieldCourseRegister["PPId"].ToString())))
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                transact.CancelSave();
                return;
            }
            int PPId = int.Parse(HiddenFieldCourseRegister["PPId"].ToString());
            int RegisterType = -1;
            if (cmbRegisterType.SelectedItem == null || cmbRegisterType.SelectedItem.Value == null)
            {
                SetMessage("نوع ثبت را انتخاب نمایید");
                transact.CancelSave();
                return;
            }
            RegisterType = Convert.ToInt32(cmbRegisterType.SelectedItem.Value);

            int PRId = InsertPeriodRegister(RegisterType, PeriodRegisterManager, PeriodPresentManager, OtherPersonManager, 1);
            if (PRId <= 0)
            {
                // SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                transact.CancelSave();
                return;
            }
            int MeId = -1;
            if (string.IsNullOrEmpty(HiddenFieldCourseRegister["MeId"].ToString()))
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                transact.CancelSave();
                return;
            }
            MeId = int.Parse(HiddenFieldCourseRegister["MeId"].ToString());
            if (!InsertAccounting(PRId, MeId, AccountingManager, AccountingDetailManager))
                return;

            transact.EndSave();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            RoundPanelRegister.HeaderText = "مشاهده";
            HiddenFieldCourseRegister["PageMode"] = Utility.EncryptQS("View");
            HiddenFieldCourseRegister["PRId"] = Utility.EncryptQS(PeriodRegisterManager[0]["PRId"].ToString());
            btnSave2.Enabled = btnSave.Enabled = false;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            Disable();
            if (cmbMemberType.SelectedIndex == 1)
            {
                txtMeNo.ClientVisible = false;
            }
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private Boolean InsertAccounting(int PRId, int MeId, TSP.DataManager.TechnicalServices.AccountingManager AccountingManager, TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager)
    {

        DataRow drAcc = AccountingManager.NewRow();
        drAcc["TableTypeId"] = MeId;
        drAcc["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister);
        if (cmbaType.SelectedItem == null)
        {
            SetMessage("نحوه پرداخت را انتخاب نمایید.");
            return false;
        }
        drAcc["Type"] = cmbaType.SelectedItem.Value;
        drAcc["Bank"] = DBNull.Value;
        drAcc["BranchCode"] = DBNull.Value;
        drAcc["BranchName"] = DBNull.Value;
        drAcc["AccType"] = (int)TSP.DataManager.TSAccountingAccType.PeriodRegister;
        drAcc["FollowNumber"] = "";// Utility.GenFollowCode(Utility.FollowType.EPayment);
        drAcc["Number"] = txtaNumber.Text;
        drAcc["Description"] = txtaDesc.Text;
        drAcc["Date"] = txtaDate.Text;
        drAcc["Time"] = DateTime.Today.TimeOfDay.ToString();
        drAcc["Amount"] = txtaAmount.Text;
        drAcc["CreateDate"] = Utility.GetDateOfToday();
        drAcc["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
        drAcc["UserId"] = Utility.GetCurrentUser_UserId();
        drAcc["ModifiedDate"] = DateTime.Now;
        AccountingManager.AddRow(drAcc);
        AccountingManager.Save();
        AccountingManager.DataTable.AcceptChanges();
        int AccountingId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
        DataRow drAccDetail = AccountingDetailManager.NewRow();
        drAccDetail["AccountingId"] = AccountingId;
        drAccDetail["TableId"] = PRId;
        drAccDetail["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister);
        drAccDetail["Amount"] = txtaAmount.Text;
        drAccDetail["Description"] = txtaDesc.Text;
        drAccDetail["UserId"] = Utility.GetCurrentUser_UserId();
        drAccDetail["InActive"] = 0;
        drAccDetail["ModifedDate"] = DateTime.Now;
        AccountingDetailManager.AddRow(drAccDetail);
        AccountingDetailManager.Save();
        AccountingDetailManager.DataTable.AcceptChanges();
        return true;
    }


    private void Update(int PRId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        TransactionManager.Add(AccountingManager);
        TransactionManager.Add(AccountingDetailManager);
        try
        {
            TransactionManager.BeginSave();
            AccountingDetailManager.SelectAccDetailByTableId(PRId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister));
            if (AccountingDetailManager.Count <= 0)
            {
                TransactionManager.CancelSave();
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            AccountingDetailManager[0].BeginEdit();
            AccountingDetailManager[0]["Amount"] = txtaAmount.Text;
            AccountingDetailManager[0]["Description"] = txtaDesc.Text;
            AccountingDetailManager[0]["ModifedDate"] = DateTime.Now;
            AccountingDetailManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            AccountingDetailManager[0].EndEdit();
            AccountingDetailManager.Save();

            int Accounting = Convert.ToInt32(AccountingDetailManager[0]["AccountingId"]);
            AccountingManager.FindByAccountingId(Accounting);
            if (AccountingManager.Count != 1)
            {
                TransactionManager.CancelSave();
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["Type"] = cmbaType.SelectedItem.Value;
            AccountingManager[0]["Number"] = txtaNumber.Text;
            AccountingManager[0]["Description"] = txtaDesc.Text;
            AccountingManager[0]["Amount"] = txtaAmount.Text;
            AccountingManager[0]["ModifiedDate"] = DateTime.Now;
            AccountingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
            TransactionManager.EndSave();
            SetMessage("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
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

    private Boolean CheckIfCanEdit(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        if (Convert.ToInt32(PeriodRegisterManager[0]["PaymentType"]) == (int)TSP.DataManager.PeriodRegisterPaymentType.EPayment)
        {
            SetMessage("امکان ویرایش ثبت نام هایی که از طریق پرداخت الکترونیکی انجام شده است وجود ندارد.");
            return false;
        }
        return true;
    }

    private Boolean CheckIfCanNew(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.PeriodRegister)
        {
            SetMessage("امکان ثبت نام در این دوره وجود ندارد.وضعیت دوره بایستی ''ثبت نام'' باشد");
            return false;
        }
        return true;
    }

    #region WF
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
            LabelWarning.Text = "شما سطح دسترسی گردش کار جهت انجام این عملیات را ندارید.";
            return (-1);
        }
    }

    private void SetWFState(int TaskCode)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
            {
                lblWorkFlowState.Visible = true;
                lblWorkFlowState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
            }
        }
    }

    bool InsertWorkFlow(int TableId, TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager)
    {
        int TaskId = -1;
        int CurrentUserId = Utility.GetCurrentUser_UserId();
        int NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId;
        String Description1 = "آغاز گردش کار اتوماتیک سیستم جهت ثبت گواهینامه خارج از نوبت";
        String Description2 = "تایید اتوماتیک گردش کار گواهینامه خارج از نوبت توسط سیستم";
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister);

        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SavePeriodRegLicenceReqInfo);
        TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        int CurrentNmcId = FindNmcId(TaskId);
        if (CurrentNmcId <= 0)
            return false;
        if (WorkFlowStateManager.InsertWorkFlowState(TableType, TableId, TaskId, Description1, CurrentNmcId, NmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) <= 0)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }

        WorkFlowStateManager.DataTable.AcceptChanges();

        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ConfirmingPeriodRegLicenceReqAndEndProccess);
        TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        if (WorkFlowStateManager.InsertWorkFlowState(TableType, TableId, TaskId, Description2, CurrentNmcId, NmcIdType, CurrentUserId, 1, Utility.GetDateOfToday()) <= 0)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        return true;
    }
    #endregion

    #endregion
}
