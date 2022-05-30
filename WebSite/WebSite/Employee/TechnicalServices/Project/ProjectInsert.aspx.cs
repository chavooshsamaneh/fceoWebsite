using DevExpress.Web;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Web.UI;

public partial class Employee_TechnicalServices_Project_ProjectInsert : System.Web.UI.Page
{
    #region Peroperties
    private int _ProjectId
    {
        get
        {
            if (Utility.DecryptQS(PkProjectId.Value) != null)
                return Convert.ToInt32(Utility.DecryptQS(PkProjectId.Value));
            else
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"]));
        }
        set
        {
            PkProjectId.Value = Utility.EncryptQS(value.ToString());
        }
    }

    private int _PrjReId
    {
        get
        {
            if (Utility.DecryptQS(PkPrjReId.Value) != null)
                return Convert.ToInt32(Utility.DecryptQS(PkPrjReId.Value));
            else
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"]));
        }
        set
        {
            PkPrjReId.Value = Utility.EncryptQS(value.ToString());
        }
    }

    private string _PageMode
    {
        get
        {
            return Utility.DecryptQS(PgMode.Value);
        }
        set
        {
            PgMode.Value = Utility.EncryptQS(value.ToString());
        }
    }
    bool IsPageRefresh = false;

    private string _MainRegNo
    {
        get
        {
            return HiddenFieldPage["MainRegNo"].ToString();
        }
        set
        {
            HiddenFieldPage["MainRegNo"] = value;
        }
    }
    private int _MainRegNoPrjReId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["MainRegNoPrjReId"]);
        }
        set
        {
            HiddenFieldPage["MainRegNoPrjReId"] = value.ToString();
        }
    }
    private string _BuildingCertificateAttachName
    {
        get
        {
            try
            {
                return HiddenFieldPage["BuildingCertificateAttachName"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["BuildingCertificateAttachName"] = value;
        }
    }
    private string _BuldingCheckAttachName
    {
        get
        {
            try
            {
                return HiddenFieldPage["BuldingCheckAttachName"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["BuldingCheckAttachName"] = value;
        }
    }

    private string _BuildingLicenceAttachNameRnd
    {
        get
        {
            try
            {
                return HiddenFieldPage["BuildingLicenceAttachNameRnd"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["BuildingLicenceAttachNameRnd"] = value;
        }
    }
    private string _TechnicalBookletAttachName
    {
        get
        {
            try
            {
                return HiddenFieldPage["TechnicalBookletAttachName"].ToString();
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HiddenFieldPage["TechnicalBookletAttachName"] = value;
        }
    }

    public int _FundationDifference
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["FundationDifference"]);
        }
        set
        {
            HiddenFieldPage["FundationDifference"] = value.ToString();
        }
    }
    private int _OwnerPrjReId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["OwnerPrjReId"]);
        }
        set
        {
            HiddenFieldPage["OwnerPrjReId"] = value.ToString();
        }
    }
    private bool _HasFoundationMixSkeleton
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["HasFoundationMixSkeleton"]);
        }
        set
        {
            HiddenFieldPage["HasFoundationMixSkeleton"] = value.ToString();
        }
    }
    private int _PrjReTypeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["RequestType"]);
        }
        set
        {
            HiddenFieldPage["RequestType"] = value.ToString();
        }
    }
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

        #region Set ltr
        ASPxTextBoxReconstructionCode.Attributes["onkeyup"] = "return ltr_override(event)";
        ASPxTextBoxComputerCode.Attributes["onkeyup"] = "return ltr_override(event)";
        ASPxTextBoxFileNo.Attributes["onkeyup"] = "return ltr_override(event)";
        #endregion

        #region PageRefresh
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
        #endregion

        if (!IsPostBack)
        {
            HiddenFieldPage["LastFundation"] = 0;
            HiddenFieldPage["LastStageNum"] = 0;

            Session["SendBackDataTable_EmpPrjIns"] = "";
            CheckPriceArchive();

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            if (Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName] != null)
            {
                String QueryValue = Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName];
                if (TSP.DataManager.Automation.AttachPageToLetter.CheckPageParameterValue(QueryValue) == false)
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }


            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["PrjReId"])) || (string.IsNullOrEmpty(Request.QueryString["ProjectId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("Project.aspx");
                return;
            }

            ObjectDataSourceCity.SelectParameters["LoginId"].DefaultValue = Utility.GetCurrentUser_UserId().ToString();
            ASPxComboBoxCity.DataBind();


            SetKeys();
            TSP.DataManager.Permission perPrjWithoutDes = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermissionSaveProjectWithOutDesigner(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            chbHasDesigner.Visible = perPrjWithoutDes.CanView;

            TSP.DataManager.Permission perPrjWithoutObs = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermissionSaveTSProjectWithOutObserver(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            chbHasObserver.Visible = perPrjWithoutObs.CanView;
            
            TSP.DataManager.Permission perPrjChange = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermissionTSChangeProjectRequestType(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            ComboBoxProjectRequestType.DataBind();
            if (_PageMode == "Edit" && Convert.ToInt32( ComboBoxProjectRequestType.SelectedItem.Value)!= (int)TSP.DataManager.TSProjectRequestType.InsertProject)
                ComboBoxProjectRequestType.Enabled = perPrjChange.CanEdit;
            else ComboBoxProjectRequestType.Enabled = false;         
            SetProjectMenuEnabled();
            SetProjectMainMenuEnabled();

            this.ViewState["chbHasDesigner"] = chbHasDesigner.Visible;
            this.ViewState["chbHasObserver"] = chbHasObserver.Visible;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["ComboBoxAgen"] = this.ASPxComboBoxAgent.Enabled;
            this.ViewState["ComboBoxProjectRequestType"] = this.ComboBoxProjectRequestType.Enabled;

        }

        if (this.ViewState["ComboBoxAgen"] != null)
            this.ASPxComboBoxAgent.Enabled = (bool)this.ViewState["ComboBoxAgen"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["ComboBoxProjectRequestType"] != null)
            this.ComboBoxProjectRequestType.Enabled = (bool)this.ViewState["ComboBoxProjectRequestType"];

        SetComperVisible(true);
    }
    #region btn Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        PkProjectId.Value = Utility.EncryptQS("-1");
        SetProjectMenuEnabled();
        SetProjectMainMenuEnabled();
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetProjectMenuEnabled();
        SetProjectMainMenuEnabled();
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (_PageMode)
        {
            case "New":
                Insert();
                break;

            case "Edit":
                Update();
                break;

            case "ChangeBaseInfo":
                InsertNewRequest((int)TSP.DataManager.TSProjectRequestType.ChangeProject, (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo);
                break;

            case "PlansMethodRequest":
                InsertNewRequest((int)TSP.DataManager.TSProjectRequestType.ChangePlansMethodRequest, (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo);
                break;
            case "EndPrj":
                InsertNewRequest((int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest, (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo);
                break;
            case "ChangeRequestLicence":
                InsertNewRequest((int)TSP.DataManager.TSProjectRequestType.BuildingsLicenseConfirming, (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo);
                break;
            case "IncreaseBuildingAreaRequest":
                InsertNewRequest((int)TSP.DataManager.TSProjectRequestType.IncreaseBuildingAreaRequest, (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo);
                break;
            case "AdditionalStageRequest":
                InsertNewRequest((int)TSP.DataManager.TSProjectRequestType.AdditionalStageRequest, (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo);
                break;
            case "BuildingNotStarted":
                InsertNewRequest((int)TSP.DataManager.TSProjectRequestType.BuildingNotStarted, (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo);
                break;

        }
        SetProjectMenuEnabled();
        SetProjectMainMenuEnabled();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("Project.aspx?PostId=" + PkProjectId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    protected void txtProjectFoundation_TextChanged(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(txtProjectFoundation.Text)) return;
        try
        {
            //txtBlockFoundation.Text = txtProjectFoundation.Text;
            //ASPxComboBoxStructureGroups.SelectedIndex = -1;
            //string Date = RegDate.Text.Trim();
            //TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager();
            //int GroupId = PriceArchiveStructureItemsManager.GetCurrentGroupIdByFoundation(Convert.ToInt32(txtProjectFoundation.Text.Trim()));
            //if (GroupId != -1)
            //    ASPxComboBoxStructureGroups.Value = GroupId;
            //else SetLabelWarning("زیربنای وارد شده نامعتبر است");
            ASPxComboBoxStructureGroups.SelectedItem = null;
            SetGroupStructure();
            ASPxComboBoxStructureGroups.SelectedIndex = 0;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning("زیربنای وارد شده نامعتبر است");
        }
    }
    #endregion
    #region combobox
    protected void ASPxComboBoxDiscountPercent_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckDiscountPercent();
        SetDiscountItems();
    }

    protected void ASPxComboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ASPxComboBoxMunicipality.DataBind();
        ASPxComboBoxMunicipality.SelectedIndex = -1;
    }

    protected void ComboBoxStructureSkeleton_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((int)ComboBoxStructureSkeleton.SelectedItem.Value == (int)TSP.DataManager.TSStructureSkeleton.Mix)
        {
            lblSazehTarkibiFoundation.ClientVisible = true;
            txtSazehTarkibiFoundation.ClientVisible = true;
        }
        else
        {
            lblSazehTarkibiFoundation.ClientVisible = false;
            txtSazehTarkibiFoundation.ClientVisible = false;
            txtSazehTarkibiFoundation.Text = "0";
        }
    }
    #endregion
    #region Menu Click
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        //ProjectId = Utility.DecryptQS(PkProjectId.Value);
        //PrjReId = PkPrjReId.Value;
        //PageMode = PgMode.Value;

        PrjMainMenu MainMenu = new PrjMainMenu("Project", _ProjectId);
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, Utility.EncryptQS(_PrjReId.ToString()), Utility.EncryptQS(_PageMode), GrdFlt, SrchFlt));
    }

    protected void ProjectMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        //ProjectId = Utility.DecryptQS(PkProjectId.Value);
        //PrjReId = PkPrjReId.Value;
        //PageMode = PgMode.Value;

        PrjMenu PrjMenu = new PrjMenu("BaseInfo", _ProjectId);
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        Response.Redirect(PrjMenu.GetRedirectLink(e.Item.Name, Utility.EncryptQS(_PrjReId.ToString()), Utility.EncryptQS(_PageMode), GrdFlt, SrchFlt));
    }
    #endregion
    #region Fil Upload
    protected void flpOfCroquis_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveTechnicalBookletFile(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void flpBuildingLicence_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageBuildingLicence(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void flpBuildingCertificate_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageBuildingCertificate(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }

    }
    protected void flpBuldingCheck_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageBuldingCheck(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion
    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        //PrjReId = Utility.DecryptQS(PkPrjReId.Value);
        if (Utility.IsDBNullOrNullValue(_PrjReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, _PrjReId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }


        string Qs = "~/Employee/TechnicalServices/Project/ProjectInsert.aspx?" + "ProjectId=" + PkProjectId.Value
            + "&PrjReId=" + PkPrjReId.Value
            + "&PageMode=" + PgMode.Value
            + "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString()
            + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString();

        WFUserControl.QueryStringForRedirect = Qs;
        WFUserControl.PerformCallback(_PrjReId, ProjectReTableType, WfCode, e);
        SetKeys();
    }
    #endregion
    #region Methods
    #region Set Keys-Mode
    private void SetKeys()
    {
        try
        {
            _MainRegNo = "";
            PgMode.Value = Request.QueryString["PageMode"];
            PkProjectId.Value = Request.QueryString["ProjectId"];
            PkPrjReId.Value = Request.QueryString["PrjReId"];
            _ProjectId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"]));
            _PrjReId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"]));
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"]);

            if (CheckIsMainvalueNull())
                return;

            SetMode();
            CheckWorkFlowPermissionForProject();
            CheckMenueViewPermission();
        }
        catch (Exception Err)
        {
            Utility.SaveWebsiteError(Err);
            SetLabelWarning("خطا در بازیابی اطلاعات ایجاد شده است");
        }
    }
    private void SetMode()
    {
        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        switch (_PageMode)
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

            case "ChangeObs":
            case "ChangeImp":
            case "ChangeBaseInfo":
            case "PlansMethodRequest":
            case "EndPrj":
            case "IncreaseBuildingAreaRequest":
            case "ChangeRequestLicence":
            case "AdditionalStageRequest":
            case "BuildingNotStarted":
                SetRequestModeKeys();
                break;
        }
    }
    private void SetNewModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        SetControlEnables(true);
        CheckAccess();

        #region Clear Form

        cmbOwnerShipType.DataBind();
        cmbOwnerShipType.SelectedIndex = -1;
        cmbOwnerShipType.Enabled = true;

        ObjectDataSourceDiscountPercent.FilterExpression = "InActive = 0";
        ASPxComboBoxDiscountPercent.DataBind();
        ASPxComboBoxDiscountPercent.SelectedIndex = ASPxComboBoxDiscountPercent.Items.IndexOfValue((int)TSP.DataManager.TSDiscountPercent.Usual);
        SetDiscountItems();

        RegDate.DateValue = DateTime.Now;
        ASPxComboBoxProjectStatus.Value = (int)TSP.DataManager.TSProjectStatus.SaveProjectInfo;
        ASPxComboBoxAgent.Value = Utility.GetCurrentUser_AgentId();
        ASPxComboBoxMunicipality.DataBind();
        ASPxComboBoxMunicipality.SelectedIndex = -1;
        ASPxComboBoxCity.DataBind();
        try
        {
            ASPxComboBoxCity.SelectedIndex = ASPxComboBoxCity.Items.IndexOfValue(Utility.GetCurrentCitId());
        }
        catch (Exception)
        {
            ASPxComboBoxCity.SelectedIndex = 3;
        }        

        ASPxComboBoxUsage.DataBind();
        ASPxComboBoxUsage.SelectedIndex = -1;
        ASPxComboBoxStructureGroups.SelectedIndex = -1;
        ComboBoxRoofType.SelectedIndex = -1;
        ComboBoxStructureSkeleton.SelectedIndex = -1;
        ASPxTextBoxCode.Text =
        ASPxTextBoxProjectName.Text =
        txtArchiveNo.Text =
        ASPxTextBoxFileNo.Text =
        FileDate.Text =
        ASPxTextBoxReconstructionCode.Text =
        ASPxTextBoxComputerCode.Text =
        txtProjectFoundation.Text =
        ASPxTextBoxArea.Text =
        ASPxTextBoxRecessArea.Text =
        ASPxTextBoxRemainArea.Text =
        ASPxTextBoxDocumentArea.Text =
        TextBoxAddress.Text =
        txtOwnerFirstName.Text =
        txtOwnerLastName.Text =
        txtSSN.Text =
        txtStageNum.Text =
        txtRegisteredNo.Text =
        txtTraceCode.Text =
        txtMainRegion.Text =
        txtMainSection.Text = txtOwnerMobileNo.Text =
        txtBuildingCertificateExpirDate.Text =
        txtBuildingCertificateNum.Text =
        txtSazehTarkibiFoundation.Text =
        txtBuildingCertificateStartDate.Text =
        txtEndProjectNum.Text = txtEndProjectStartDate.Text = txtBuldingCheckDate.Text = "";
        txtStageNum.Text = "0";
        ComboBoxProjectRequestType.DataBind();
        ComboBoxProjectRequestType.SelectedIndex = ComboBoxProjectRequestType.Items.FindByValue((int)TSP.DataManager.TSProjectRequestType.InsertProject).Index;
        SetGroupStructure();

        chbHasDesigner.Checked = false;
        chbHasObserver.Checked = false;
        ASPxHyperLinkCroquis.Text = "";
        ASPxHyperLinkCroquis.NavigateUrl = "";
        ASPxHiddenFieldCroquis["name"] = 0;
        ASPxHyperLinkCroquis.ClientVisible = false;

        HyperLinkBuildingLicence.Text = "";
        HyperLinkBuildingLicence.NavigateUrl = "";
        HyperLinkBuildingLicence.ClientVisible = false;
        ASPxHiddenFieldCroquis["BuildingLicence"] = 0;

        HyperLinkBuildingCertificate.Text = "";
        HyperLinkBuildingCertificate.NavigateUrl = "";
        HyperLinkBuildingCertificate.ClientVisible = false;
        ASPxHiddenFieldCroquis["BuildingCertificate"] = 0;

        HyperLinkBuldingCheck.Text = "";
        HyperLinkBuldingCheck.NavigateUrl = "";
        HyperLinkBuldingCheck.ClientVisible = false;
        ASPxHiddenFieldCroquis["BuldingCheck"] = 0;
        #endregion

        RoundPanelProjectInfo.HeaderText = "جدید";
    }
    private void SetControlEnables(Boolean Enabled)
    {
        ASPxComboBoxAgent.Enabled =
        ASPxTextBoxProjectName.Enabled =
        txtArchiveNo.Enabled =
        ASPxComboBoxDiscountPercent.Enabled =
        ASPxComboBoxStructureGroups.Enabled =
        ASPxTextBoxFileNo.Enabled = Enabled;
        FileDate.Enabled =
        ASPxComboBoxMunicipality.Enabled =
        ASPxComboBoxCity.Enabled =
        ASPxTextBoxReconstructionCode.Enabled =
        ASPxTextBoxComputerCode.Enabled =
        ASPxComboBoxUsage.Enabled =
        txtProjectFoundation.Enabled =
        ASPxTextBoxArea.Enabled =
        ASPxTextBoxRecessArea.Enabled =
        ASPxTextBoxRemainArea.Enabled =
        ASPxTextBoxDocumentArea.Enabled =
        cmbOwnerShipType.Enabled =
        TextBoxAddress.Enabled =
        flpOfCroquis.Enabled =
        flpBuildingCertificate.Enabled = txtBuildingCertificateExpirDate.Enabled = txtBuildingCertificateNum.Enabled = txtBuildingCertificateStartDate.Enabled =
        chbHasDesigner.Enabled =
        chbHasObserver.Enabled=
        ComboBoxProjectRequestType.Enabled=
        txtOwnerLastName.Enabled =
        txtSSN.Enabled =
        txtOwnerFirstName.Enabled =
       flpBuildingLicence.Enabled = txtEndProjectNum.Enabled = txtEndProjectStartDate.Enabled =
        txtStageNum.ClientEnabled =
        txtRegisteredNo.Enabled =
        txtMainRegion.Enabled =
        txtMainSection.Enabled = ComboBoxRoofType.Enabled = txtSazehTarkibiFoundation.Enabled =
        ComboBoxStructureSkeleton.Enabled = CmbType.ClientEnabled = txtOwnerMobileNo.Enabled =
        flpBuldingCheck.Enabled = txtBuldingCheckDate.Enabled = Enabled;
    }
    private void SetPanalEnabledBasedOnRequest()
    {
        switch (_PrjReTypeId)
        {
            case (int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest:
                pnlMainInformation.Enabled = PanelOwnerInfo.Enabled = PanelSubInfo.Enabled = pnlMaxBlock.Enabled = PanelBuldingCheck.Enabled = PanelBuildingCertificate.Enabled = false;
                PanelEndProject.Enabled = true;
                break;
            case (int)TSP.DataManager.TSProjectRequestType.BuildingNotStarted:
                pnlMainInformation.Enabled = PanelOwnerInfo.Enabled = PanelSubInfo.Enabled = pnlMaxBlock.Enabled = PanelEndProject.Enabled = PanelBuildingCertificate.Enabled = false;
                PanelBuldingCheck.Enabled = true;
                break;
            case (int)TSP.DataManager.TSProjectRequestType.BuildingsLicenseConfirming:
                pnlMainInformation.Enabled = PanelOwnerInfo.Enabled = PanelSubInfo.Enabled = pnlMaxBlock.Enabled = PanelEndProject.Enabled = PanelBuldingCheck.Enabled = false;
                PanelBuildingCertificate.Enabled = true;
                break;
            case (int)TSP.DataManager.TSProjectRequestType.IncreaseBuildingAreaRequest:
                txtStageNum.Enabled = false;
                PanelBuildingCertificate.Enabled = PanelEndProject.Enabled = PanelBuldingCheck.Enabled = false;
                break;

            case (int)TSP.DataManager.TSProjectRequestType.AdditionalStageRequest:
                PanelBuildingCertificate.Enabled = PanelEndProject.Enabled = PanelBuldingCheck.Enabled = false;
                break;
            case (int)TSP.DataManager.TSProjectRequestType.InsertProject:
                break;
            case (int)TSP.DataManager.TSProjectRequestType.ChangeProject:
                PanelEndProject.Enabled =  PanelBuldingCheck.Enabled = PanelBuildingCertificate.Enabled = false;
               pnlMainInformation.Enabled = pnlMaxBlock.Enabled = PanelOwnerInfo.Enabled = PanelSubInfo.Enabled = true;
                break;
            default:
                PanelEndProject.Enabled = pnlMainInformation.Enabled =  pnlMaxBlock.Enabled = PanelBuldingCheck.Enabled = PanelBuildingCertificate.Enabled = false;
                PanelOwnerInfo.Enabled = PanelSubInfo.Enabled = true;
                break;
        }
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (btnNew.Enabled == true)
        {
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        if (Utility.DecryptQS(PgMode.Value) == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (Utility.DecryptQS(PgMode.Value) == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        TSP.DataManager.Permission perComboProjectAgent = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermissionChangeAgent(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (perComboProjectAgent.CanEdit)
        {
            this.ViewState["ComboBoxAgen"] = ASPxComboBoxAgent.Enabled = perComboProjectAgent.CanEdit;
        }
        else
            this.ViewState["ComboBoxAgen"] = ASPxComboBoxAgent.Enabled = false;
        TSP.DataManager.Permission perPrjChange = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermissionTSChangeProjectRequestType(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (_PageMode == "Edit" && Convert.ToInt32(ComboBoxProjectRequestType.SelectedItem.Value) != (int)TSP.DataManager.TSProjectRequestType.InsertProject)
            ComboBoxProjectRequestType.Enabled = perPrjChange.CanEdit;
        else ComboBoxProjectRequestType.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
    private void SetEditModeKeys()
    {
        if (CheckIsMainvalueNull())
            return;
        btnNew.Enabled =
        btnNew2.Enabled =
        btnSave.Enabled =
        btnSave2.Enabled =
        btnEdit.Enabled =
        btnEdit2.Enabled = true;

        SetControlEnables(true);

        SetValuesWithRequest();
        SetControlEnableForProjectIngrediants();//اگر ناظر یا طراح در این درخواست ثبت شده باشند نگذارد متراژ و تعداد طبقات تغییر کند         
        SetPanalEnabledBasedOnRequest();
        CheckAccess();
        InsertWorkFlowStateLog("مشاهده اطلاعات در حالت ویرایش توسط کاربر", TSP.DataManager.WorkFlowStateType.ViewInfo);
        RoundPanelProjectInfo.HeaderText = "ویرایش";
    }
    private void SetControlEnableForProjectIngrediants()
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        DataTable dt = ProjectRequestManager.SelectTSProjectCountIngrediant(_PrjReId);
        if (Convert.ToInt32(dt.Rows[0]["CountIngrediant"]) != 0 && CheckAccountingConditions())
        {

            ASPxComboBoxMunicipality.Enabled =
            ASPxComboBoxCity.Enabled =
            ASPxComboBoxDiscountPercent.Enabled =
            ASPxComboBoxAgent.Enabled =
            pnlMainInformation.Enabled =
            pnlMaxBlock.Enabled = false;

            lblCountIngrediant.Visible = true;

            ImgWarningCountIngrediant.ClientVisible = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningCountIngrediant');</script>");
            lblCountIngrediant.Text = "به دلیل ثبت ناظران پروژه و یا فیش پرداخت شده نظارت در این درخواست قادر به ویرایش زیر بنای کل، تعداد طبقات پروژه، شهر و ... نیستید ";
        }
        else
        {
            ImgWarningCountIngrediant.ClientVisible = false;
            lblCountIngrediant.Visible = false;
        }
    }
    private bool CheckAccountingConditions()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        string AccTypeList = ((int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString();
        int TableTypeId = _PrjReId;
        DataTable dt = AccountingManager.SelectExistAccountingByAccTypeList(TableTypeId, TableType, _ProjectId, AccTypeList);//**ثبت فیش نظارت پرداخت شده در درخواست جاری داشته باشیم اجازه تغییر متراژ و ... داده نمی شود
        if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
        {
            return true;
        }
        return false;
    }
    private void SetViewModeKeys()
    {
        if (CheckIsMainvalueNull())
            return;
        btnNew.Enabled =
        btnNew2.Enabled =
        btnEdit.Enabled =
        btnEdit2.Enabled = true;
        btnSave.Enabled =
        btnSave2.Enabled = false;
        CheckAccess();
        SetControlEnables(false);
        SetValuesWithRequest();
        InsertWorkFlowStateLog("مشاهده اطلاعات توسط کاربر", TSP.DataManager.WorkFlowStateType.ViewInfo);
        RoundPanelProjectInfo.HeaderText = "مشاهده";
    }
    private void SetRequestModeKeys()
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        if (CheckIsMainvalueNull())
            return;

        btnNew.Enabled =
        btnNew2.Enabled =
        btnSave.Enabled =
        btnSave2.Enabled = true;
        btnEdit.Enabled =
        btnEdit2.Enabled = false;
        CheckAccess();

        SetValuesWithProject();
        RegDate.Text = Utility.GetDateOfToday();
        switch (_PageMode)
        {
            case "ChangeBaseInfo":
                RoundPanelProjectInfo.HeaderText = "درخواست تغییرات";
                _PrjReTypeId = (int)TSP.DataManager.TSProjectRequestType.ChangeProject;
                break;
            case "PlansMethodRequest":
                RoundPanelProjectInfo.HeaderText = "درخواست تغییرات دستور نقشه";
                _PrjReTypeId = (int)TSP.DataManager.TSProjectRequestType.ChangePlansMethodRequest;
                break;
            case "EndPrj":
                RoundPanelProjectInfo.HeaderText = "درخواست صدور پروانه پایان کار";
                _PrjReTypeId = (int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest;
                break;
            case "IncreaseBuildingAreaRequest":
                RoundPanelProjectInfo.HeaderText = "درخواست توسعه بنا";
                _PrjReTypeId = (int)TSP.DataManager.TSProjectRequestType.IncreaseBuildingAreaRequest;
                break;
            case "AdditionalStageRequest":
                RoundPanelProjectInfo.HeaderText = "درخواست اضافه اشکوب";
                _PrjReTypeId = (int)TSP.DataManager.TSProjectRequestType.AdditionalStageRequest;
                break;
            case "ChangeRequestLicence":
                RoundPanelProjectInfo.HeaderText = "درخواست صدور پروانه ساخت";
                _PrjReTypeId = (int)TSP.DataManager.TSProjectRequestType.BuildingsLicenseConfirming;
                break;
            case "BuildingNotStarted":
                RoundPanelProjectInfo.HeaderText = "درخواست اعلام شروع نشدن ساخت و ساز";
                _PrjReTypeId = (int)TSP.DataManager.TSProjectRequestType.BuildingNotStarted;
                break;
        }

        SetControlEnables(true);
        SetPanalEnabledBasedOnRequest();
    }
    private void SetComperVisible(Boolean Visibled)
    {
        if (_PageMode == "New")
            Visibled = false;
        lblCompareChangesProjectFoundation.ClientVisible =
      lblCompareChangesStageNum.ClientVisible = Visibled;
        if (_PageMode != "New")
        {
            lblCompareChangesProjectFoundation.Text = TextResult(Convert.ToInt32(HiddenFieldPage["LastFundation"].ToString()), Convert.ToInt32(txtProjectFoundation.Text));
            lblCompareChangesStageNum.Text = TextResult(Convert.ToInt32(HiddenFieldPage["LastStageNum"].ToString()), Convert.ToInt32(txtStageNum.Text));
        }
    }
    private string TextResult(int LastValue, int CurrentValue)
    {
        return "مقدار قبلی:" + LastValue + "   تفاوت:" + (CurrentValue - LastValue).ToString();


    }

    private string TextLblComper(object LastValue, object ComperValue)
    {
        int intLastValue = 0;
        int intComperValue = 0;

        if (!Utility.IsDBNullOrNullValue(ComperValue))
            intComperValue = Convert.ToInt32(ComperValue);

        if (!Utility.IsDBNullOrNullValue(LastValue))
            intLastValue = Convert.ToInt32(LastValue);

        if (Utility.IsDBNullOrNullValue(LastValue) && Utility.IsDBNullOrNullValue(LastValue))
            return "";

        return string.Format("مقدار قبلی: {0}   تفاوت: {1}", intLastValue.ToString(), Math.Abs(intComperValue).ToString());
    }
    #endregion

    #region Find and set Info
    private void SetValuesWithRequest()
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        DataTable dtLastFundation = ProjectRequestManager.SelectPreviousProjectRequestStageAndFoundation(_ProjectId, _PrjReId, -1);
        ProjectRequestManager.FindByCode(_PrjReId);
        if (ProjectRequestManager.Count != 1)
        {
            SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            return;
        }

        ObjectDataSourceDiscountPercent.FilterExpression = "InActive= 0" + " OR DiscountPercentId=" + Convert.ToInt32(ProjectRequestManager[0]["DiscountPercentId"]);
        ASPxComboBoxDiscountPercent.DataBind();
        ASPxComboBoxDiscountPercent.Value = Convert.ToInt32(ProjectRequestManager[0]["DiscountPercentId"]);
        SetDiscountItems();

        ASPxTextBoxCode.Text = _ProjectId.ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["PrjReTypeId"]))
        {
            _PrjReTypeId = Convert.ToInt32(ProjectRequestManager[0]["PrjReTypeId"]);
            ComboBoxProjectRequestType.DataBind();
            ComboBoxProjectRequestType.SelectedIndex = ComboBoxProjectRequestType.Items.FindByValue(_PrjReTypeId).Index;
        }
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["ProjectNo"]))
            txtProjectNo.Text = ProjectRequestManager[0]["ProjectNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["Description"]))
            txtReqDesc.Text = ProjectRequestManager[0]["Description"].ToString();
        RegDate.Text = ProjectRequestManager[0]["RequestDate"].ToString();
        ASPxTextBoxProjectName.Text = ProjectRequestManager[0]["ProjectName"].ToString();
        txtArchiveNo.Text = ProjectRequestManager[0]["ArchiveNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["TraceCode"]))
            txtTraceCode.Text = ProjectRequestManager[0]["TraceCode"].ToString();
        ASPxComboBoxProjectStatus.Value = Convert.ToInt32(ProjectRequestManager[0]["ProjectStatusId"]);
        ASPxComboBoxAgent.Value = Convert.ToInt32(ProjectRequestManager[0]["AgentId"]);
        ASPxTextBoxFileNo.Text = ProjectRequestManager[0]["FileNo"].ToString();
        FileDate.Text = ProjectRequestManager[0]["FileDate"].ToString();
        ASPxComboBoxCity.DataBind();
        ASPxComboBoxCity.Value = Convert.ToInt32(ProjectRequestManager[0]["CitId"]);
        ASPxComboBoxMunicipality.DataBind();
        ASPxComboBoxMunicipality.Value = Convert.ToInt32(ProjectRequestManager[0]["MunId"]);
        ASPxTextBoxReconstructionCode.Text = ProjectRequestManager[0]["ReconstructionCode"].ToString();
        ASPxTextBoxComputerCode.Text = ProjectRequestManager[0]["ComputerCode"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["UsageId"]))
        {
            ASPxComboBoxUsage.DataBind();
            ASPxComboBoxUsage.Value = Convert.ToInt32(ProjectRequestManager[0]["UsageId"]);
        }

        if (dtLastFundation.Rows.Count == 1 && _PageMode == "View" && !Utility.IsDBNullOrNullValue(dtLastFundation.Rows[0]["Foundation"]))
            HiddenFieldPage["LastFundation"] = dtLastFundation.Rows[0]["Foundation"].ToString();
        else
            HiddenFieldPage["LastFundation"] = ProjectRequestManager[0]["Foundation"].ToString();
        txtProjectFoundation.Text = ProjectRequestManager[0]["Foundation"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["FoundationMixSkeleton"]))
        {
            txtSazehTarkibiFoundation.Text = ProjectRequestManager[0]["FoundationMixSkeleton"].ToString();
            if (Convert.ToInt32(ProjectRequestManager[0]["FoundationMixSkeleton"]) > 0)
                _HasFoundationMixSkeleton = lblSazehTarkibiFoundation.ClientVisible = txtSazehTarkibiFoundation.ClientVisible = true;
            else
                _HasFoundationMixSkeleton = lblSazehTarkibiFoundation.ClientVisible = txtSazehTarkibiFoundation.ClientVisible = false;
        }
        else
            _HasFoundationMixSkeleton = lblSazehTarkibiFoundation.ClientVisible = txtSazehTarkibiFoundation.ClientVisible = false;

        ASPxTextBoxArea.Text = ProjectRequestManager[0]["Area"].ToString();
        ASPxTextBoxRecessArea.Text = ProjectRequestManager[0]["RecessArea"].ToString();
        ASPxTextBoxRemainArea.Text = ProjectRequestManager[0]["RemainArea"].ToString();
        ASPxTextBoxDocumentArea.Text = ProjectRequestManager[0]["DocumentArea"].ToString();
        TextBoxAddress.Text = ProjectRequestManager[0]["Address"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["OwnershipTypeId"]))
        {
            cmbOwnerShipType.DataBind();
            cmbOwnerShipType.Value = Convert.ToInt32(ProjectRequestManager[0]["OwnershipTypeId"]);

            if (Convert.ToInt32(ProjectRequestManager[0]["OwnershipTypeId"]) == 1)//otherperson
            {
                txtOwnerFirstName.ClientVisible = true;
                txtOwnerLastName.ClientVisible = true;
                txtSSN.ClientVisible = true;
                lblOwnerLastName.ClientVisible = true;
                lblSSN.ClientVisible = true;
                lbloFirstName.Text = "نام نماینده مالکین";
            }
            else if (Convert.ToInt32(ProjectRequestManager[0]["OwnershipTypeId"]) == 2)//otherorganization
            {
                txtOwnerLastName.ClientVisible = false;
                txtSSN.ClientVisible = false;
                lblOwnerLastName.ClientVisible = false;
                lblSSN.ClientVisible = false;
                lbloFirstName.Text = "نام سازمان";
            }
        }

        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["TaskName"]))
            lblWorkFlowState.Text = "وضعیت درخواست: " + ProjectRequestManager[0]["TaskName"].ToString();
        else
            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["DesignerSaved"]))
            chbHasDesigner.Checked = !Convert.ToBoolean(ProjectRequestManager[0]["DesignerSaved"]);
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["ObserverSaved"]))
            chbHasObserver.Checked = !Convert.ToBoolean(ProjectRequestManager[0]["ObserverSaved"]);

        if (dtLastFundation.Rows.Count == 1 && _PageMode == "View" && !Utility.IsDBNullOrNullValue(dtLastFundation.Rows[0]["MaxStageNum"]))
            HiddenFieldPage["LastStageNum"] = dtLastFundation.Rows[0]["MaxStageNum"].ToString();
        else
            HiddenFieldPage["LastStageNum"] = ProjectRequestManager[0]["MaxStageNum"].ToString();
        ASPxHiddenFieldCroquis["MaxStageNum"] = txtStageNum.Text = ProjectRequestManager[0]["MaxStageNum"].ToString(); //BlockManager.GetMaxStageNumByRequest(_PrjReId).ToString();


        SetGroupStructure();
        if (ASPxComboBoxStructureGroups.Items.FindByValue(Convert.ToInt32(ProjectRequestManager[0]["GroupId"])) != null)
            ASPxComboBoxStructureGroups.SelectedIndex = ASPxComboBoxStructureGroups.Items.FindByValue(Convert.ToInt32(ProjectRequestManager[0]["GroupId"])).Index;

        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["MainRegisterNo"]) && ProjectRequestManager[0]["MainRegisterNo"].ToString() != "(فاقد پلاک ثبتی)")
            txtRegisteredNo.Text = ProjectRequestManager[0]["MainRegisterNo"].ToString();

        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["MainRegion"]))
            txtMainRegion.Text = ProjectRequestManager[0]["MainRegion"].ToString();

        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["MainSection"]))
            txtMainSection.Text = ProjectRequestManager[0]["MainSection"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["FileUrlTechnicalBooklet"]))
        {
            ASPxHyperLinkCroquis.ClientVisible = true;
            ASPxHyperLinkCroquis.NavigateUrl = ProjectRequestManager[0]["FileUrlTechnicalBooklet"].ToString();
            ASPxHiddenFieldCroquis["name"] = 1;
        }
        #region پروانه ساخت
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["BuildingCertificateStartDate"]))
            txtBuildingCertificateStartDate.Text = ProjectRequestManager[0]["BuildingCertificateStartDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["BuildingCertificateExpirDate"]))
            txtBuildingCertificateExpirDate.Text = ProjectRequestManager[0]["BuildingCertificateExpirDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["BuildingCertificateNum"]))
            txtBuildingCertificateNum.Text = ProjectRequestManager[0]["BuildingCertificateNum"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["FileUrlBuildingCertificate"]))
        {
            HyperLinkBuildingCertificate.ClientVisible = true;
            HyperLinkBuildingCertificate.NavigateUrl = ProjectRequestManager[0]["FileUrlBuildingCertificate"].ToString();
            ASPxHiddenFieldCroquis["BuildingCertificate"] = 1;
        }
        #endregion
        #region پایان کار
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["EndProjectStartDate"]))
            txtEndProjectStartDate.Text = ProjectRequestManager[0]["EndProjectStartDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["EndProjectNum"]))
            txtEndProjectNum.Text = ProjectRequestManager[0]["EndProjectNum"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["FileUrlBuildingLicence"]))
        {
            HyperLinkBuildingLicence.ClientVisible = true;
            HyperLinkBuildingLicence.NavigateUrl = ProjectRequestManager[0]["FileUrlBuildingLicence"].ToString();
            ASPxHiddenFieldCroquis["BuildingLicence"] = 1;
        }
        #endregion
        #region شروع نشدن ساخت و ساز
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["BuldingCheckDate"]))
            txtBuldingCheckDate.Text = ProjectRequestManager[0]["BuldingCheckDate"].ToString();
       
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["FileURLBuldingCheck"]))
        {
            HyperLinkBuldingCheck.ClientVisible = true;
            HyperLinkBuldingCheck.NavigateUrl = ProjectRequestManager[0]["FileURLBuldingCheck"].ToString();
            ASPxHiddenFieldCroquis["BuldingCheck"] = 1;
        }
        #endregion
        FindOwnerAgent(_ProjectId, true);

        txtStageNum.Text = ProjectRequestManager[0]["MaxStageNum"].ToString();
        ComboBoxStructureSkeleton.DataBind();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeleton.SelectedIndex = ComboBoxStructureSkeleton.Items.FindByValue(ProjectRequestManager[0]["StructureSkeletonId"]).Index;
        ComboBoxRoofType.DataBind();
        if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["RoofTypeId"]))
            ComboBoxRoofType.SelectedIndex = ComboBoxRoofType.Items.FindByValue(ProjectRequestManager[0]["RoofTypeId"]).Index;
    }

    private void SetValuesWithProject()
    {
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();

        ProjectManager.FindByProjectId(_ProjectId);
        if (ProjectManager.Count != 1)
        {
            SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            return;
        }


        ObjectDataSourceDiscountPercent.FilterExpression = "InActive= 0" + " OR DiscountPercentId=" + Convert.ToInt32(ProjectManager[0]["DiscountPercentId"]);
        ASPxComboBoxDiscountPercent.DataBind();
        ASPxComboBoxDiscountPercent.Value = Convert.ToInt32(ProjectManager[0]["DiscountPercentId"]);
        SetDiscountItems();

        ASPxTextBoxCode.Text = _ProjectId.ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["TraceCode"]))
            txtTraceCode.Text = ProjectManager[0]["TraceCode"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["ProjectNo"]))
            txtProjectNo.Text = ProjectManager[0]["ProjectNo"].ToString();
        ASPxTextBoxProjectName.Text = ProjectManager[0]["ProjectName"].ToString();
        txtArchiveNo.Text = ProjectManager[0]["ArchiveNo"].ToString();
        ASPxComboBoxProjectStatus.Value = Convert.ToInt32(ProjectManager[0]["ProjectStatusId"]);
        ASPxComboBoxAgent.Value = Convert.ToInt32(ProjectManager[0]["AgentId"]);
        ASPxTextBoxFileNo.Text = ProjectManager[0]["FileNo"].ToString();
        FileDate.Text = ProjectManager[0]["FileDate"].ToString();
        ASPxComboBoxCity.DataBind();
        ASPxComboBoxCity.Value = Convert.ToInt32(ProjectManager[0]["CitId"]);
        ASPxComboBoxMunicipality.DataBind();
        ASPxComboBoxMunicipality.Value = Convert.ToInt32(ProjectManager[0]["MunId"]);
        ASPxTextBoxReconstructionCode.Text = ProjectManager[0]["ReconstructionCode"].ToString();
        ASPxTextBoxComputerCode.Text = ProjectManager[0]["ComputerCode"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["UsageId"]))
        {
            ASPxComboBoxUsage.DataBind();
            ASPxComboBoxUsage.Value = Convert.ToInt32(ProjectManager[0]["UsageId"]);
        }
        HiddenFieldPage["LastFundation"] = ProjectManager[0]["Foundation"].ToString();
        txtProjectFoundation.Text = ProjectManager[0]["Foundation"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["FoundationMixSkeleton"]))
        {
            txtSazehTarkibiFoundation.Text = ProjectManager[0]["FoundationMixSkeleton"].ToString();
            if (Convert.ToInt32(ProjectManager[0]["FoundationMixSkeleton"]) > 0)
                _HasFoundationMixSkeleton = lblSazehTarkibiFoundation.ClientVisible = txtSazehTarkibiFoundation.ClientVisible = true;
            else
                _HasFoundationMixSkeleton = lblSazehTarkibiFoundation.ClientVisible = txtSazehTarkibiFoundation.ClientVisible = false;
        }
        else
            _HasFoundationMixSkeleton = lblSazehTarkibiFoundation.ClientVisible = txtSazehTarkibiFoundation.ClientVisible = false;

        ASPxTextBoxArea.Text = ProjectManager[0]["Area"].ToString();
        ASPxTextBoxRecessArea.Text = ProjectManager[0]["RecessArea"].ToString();
        ASPxTextBoxRemainArea.Text = ProjectManager[0]["RemainArea"].ToString();
        ASPxTextBoxDocumentArea.Text = ProjectManager[0]["DocumentArea"].ToString();
        TextBoxAddress.Text = ProjectManager[0]["Address"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["OwnershipTypeId"]))
        {
            cmbOwnerShipType.DataBind();
            cmbOwnerShipType.Value = Convert.ToInt32(ProjectManager[0]["OwnershipTypeId"]);

            if (Convert.ToInt32(ProjectManager[0]["OwnershipTypeId"]) == 1)//otherperson
            {
                txtOwnerFirstName.ClientVisible = true;
                txtOwnerLastName.ClientVisible = true;
                txtSSN.ClientVisible = true;
                lblOwnerLastName.ClientVisible = true;
                lblSSN.ClientVisible = true;
                lbloFirstName.Text = "نام نماینده مالکین";
            }
            else if (Convert.ToInt32(ProjectManager[0]["OwnershipTypeId"]) == 2)//otherorganization
            {
                txtOwnerLastName.ClientVisible = false;
                txtSSN.ClientVisible = false;
                lblOwnerLastName.ClientVisible = false;
                lblSSN.ClientVisible = false;
                lbloFirstName.Text = "نام سازمان";
            }
        }

        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["DesignerSaved"]))
            chbHasDesigner.Checked = !Convert.ToBoolean(ProjectManager[0]["DesignerSaved"]);
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["ObserverSaved"]))
            chbHasObserver.Checked = !Convert.ToBoolean(ProjectManager[0]["ObserverSaved"]);
        HiddenFieldPage["LastStageNum"] = ASPxHiddenFieldCroquis["MaxStageNum"] = txtStageNum.Text = ProjectManager[0]["MaxStageNum"].ToString();
        SetGroupStructure();
        if (ASPxComboBoxStructureGroups.Items.FindByValue(Convert.ToInt32(ProjectManager[0]["GroupId"])) != null)
            ASPxComboBoxStructureGroups.SelectedIndex = ASPxComboBoxStructureGroups.Items.FindByValue(Convert.ToInt32(ProjectManager[0]["GroupId"])).Index;

        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["MainRegisterNo"]) && ProjectManager[0]["MainRegisterNo"].ToString() != "(فاقد پلاک ثبتی)")
            txtRegisteredNo.Text = ProjectManager[0]["MainRegisterNo"].ToString();

        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["MainRegion"]))
            txtMainRegion.Text = ProjectManager[0]["MainRegion"].ToString();

        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["MainSection"]))
            txtMainSection.Text = ProjectManager[0]["MainSection"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["FileUrlTechnicalBooklet"]))
        {
            ASPxHyperLinkCroquis.ClientVisible = true;
            ASPxHyperLinkCroquis.NavigateUrl = ProjectManager[0]["FileUrlTechnicalBooklet"].ToString();
            ASPxHiddenFieldCroquis["name"] = 1;
        }
        #region پروانه ساخت
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["BuildingCertificateStartDate"]))
            txtBuildingCertificateStartDate.Text = ProjectManager[0]["BuildingCertificateStartDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["BuildingCertificateExpirDate"]))
            txtBuildingCertificateExpirDate.Text = ProjectManager[0]["BuildingCertificateExpirDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["BuildingCertificateNum"]))
            txtBuildingCertificateNum.Text = ProjectManager[0]["BuildingCertificateNum"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["FileUrlBuildingCertificate"]))
        {
            HyperLinkBuildingCertificate.ClientVisible = true;
            HyperLinkBuildingCertificate.NavigateUrl = ProjectManager[0]["FileUrlBuildingCertificate"].ToString();
            ASPxHiddenFieldCroquis["BuildingCertificate"] = 1;
        }
        #endregion
        #region پایان کار
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["EndProjectStartDate"]))
            txtEndProjectStartDate.Text = ProjectManager[0]["EndProjectStartDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["EndProjectNum"]))
            txtEndProjectNum.Text = ProjectManager[0]["EndProjectNum"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["FileUrlBuildingLicence"]))
        {
            HyperLinkBuildingLicence.ClientVisible = true;
            HyperLinkBuildingLicence.NavigateUrl = ProjectManager[0]["FileUrlBuildingLicence"].ToString();
            ASPxHiddenFieldCroquis["BuildingLicence"] = 1;
        }
        #endregion
        #region شروع نشدن ساخت و ساز
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["BuldingCheckDate"]))
            txtBuldingCheckDate.Text = ProjectManager[0]["BuldingCheckDate"].ToString();

        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["FileURLBuldingCheck"]))
        {
            HyperLinkBuldingCheck.ClientVisible = true;
            HyperLinkBuldingCheck.NavigateUrl = ProjectManager[0]["FileURLBuldingCheck"].ToString();
            ASPxHiddenFieldCroquis["BuldingCheck"] = 1;
        }
        #endregion

        FindOwnerAgent(_ProjectId, true);

        txtStageNum.Text = ProjectManager[0]["MaxStageNum"].ToString();
        ComboBoxStructureSkeleton.DataBind();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeleton.SelectedIndex = ComboBoxStructureSkeleton.Items.FindByValue(ProjectManager[0]["StructureSkeletonId"]).Index;
        ComboBoxRoofType.DataBind();
        if (!Utility.IsDBNullOrNullValue(ProjectManager[0]["RoofTypeId"]))
            ComboBoxRoofType.SelectedIndex = ComboBoxRoofType.Items.FindByValue(ProjectManager[0]["RoofTypeId"]).Index;
    }

    private int FindOwnerAgent(int ProjectId, Boolean IsTextBoxSet)
    {
        _OwnerPrjReId = -2;
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        OwnerManager.FindOwnerAgent(ProjectId);
        if (OwnerManager.Count == 1)
        {
            if (IsTextBoxSet)
            {
                CmbType.DataBind();
                CmbType.SelectedIndex = CmbType.Items.IndexOfValue(OwnerManager[0]["Type"].ToString());

                if (OwnerManager[0]["Type"].ToString() == "1")//otherperson
                {
                    txtOwnerFirstName.Text = OwnerManager[0]["FirstName"].ToString();
                    txtOwnerLastName.Text = OwnerManager[0]["LastName"].ToString();
                    txtSSN.Text = OwnerManager[0]["SSNOtherPerson"].ToString();
                    txtOwnerLastName.ClientVisible = true;
                    txtSSN.ClientVisible = true;
                    lblOwnerLastName.ClientVisible = true;
                    lblSSN.ClientVisible = true;
                    lbloFirstName.Text = "نام نماینده مالکین";
                }
                else //organization
                {
                    txtOwnerFirstName.Text = OwnerManager[0]["Name"].ToString();
                    txtOwnerLastName.ClientVisible = false;
                    txtSSN.ClientVisible = false;
                    lblOwnerLastName.ClientVisible = false;
                    lblSSN.ClientVisible = false;
                    lbloFirstName.Text = "نام سازمان";

                }
                _OwnerPrjReId = Convert.ToInt32(OwnerManager[0]["PrjReId"]);
                txtOwnerMobileNo.Text = OwnerManager[0]["MobileNo"].ToString();
            }

            return Convert.ToInt32(OwnerManager[0]["OwnerId"]);
        }
        return -2;
    }

    private int FindMainRegisteredNo(int ProjectId, Boolean IsTextBoxSet)
    {
        _MainRegNoPrjReId = -2;
        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        DataTable dtRegisteNo = RegisteredNoManager.GetMainRegisteredNo(ProjectId);
        if (dtRegisteNo.Rows.Count > 0)
        {
            // if (IsTextBoxSet)
            //{
            //txtRegisteredNo.Text =
            _MainRegNo = dtRegisteNo.Rows[0]["RegisteredNo"].ToString();
            _MainRegNoPrjReId = Convert.ToInt32(dtRegisteNo.Rows[0]["PrjReId"]);
            //  }
            return Convert.ToInt32(dtRegisteNo.Rows[0]["RegisteredNoId"]);
        }
        return -2;
    }

    private int FindBlock(int ProjectId, int PrjReId, Boolean IsTextBoxSet)
    {
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        DataTable dtBlock = BlockManager.SelectTSBlockByProjectAndPrjReId(ProjectId, PrjReId);
        if (dtBlock.Rows.Count > 0)
        {
            if (IsTextBoxSet)
            {
                // txtBlockFoundation.Text = dtBlock.Rows[0]["Foundation"].ToString();
                txtStageNum.Text = dtBlock.Rows[0]["StageNum"].ToString();
                ComboBoxStructureSkeleton.DataBind();
                if (!Utility.IsDBNullOrNullValue(dtBlock.Rows[0]["StructureSkeletonId"]))
                    ComboBoxStructureSkeleton.SelectedIndex = ComboBoxStructureSkeleton.Items.FindByValue(dtBlock.Rows[0]["StructureSkeletonId"]).Index;
                ComboBoxRoofType.DataBind();
                if (!Utility.IsDBNullOrNullValue(dtBlock.Rows[0]["RoofTypeId"]))
                    ComboBoxRoofType.SelectedIndex = ComboBoxRoofType.Items.FindByValue(dtBlock.Rows[0]["RoofTypeId"]).Index;
            }
            return Convert.ToInt32(dtBlock.Rows[0]["BlockId"]);
        }
        return -2;
    }
    #endregion


    #region  Insert-Update

    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }
        if (Convert.ToInt32(txtStageNum.Text.Trim()) <= 0)
        {
            SetLabelWarning("تعداد طبقات از روی شالوده نامشخص است");
            return;
        }
        if (Convert.ToInt32(txtProjectFoundation.Text.Trim()) <= 0)
        {
            SetLabelWarning("متراژ پروژه نامشخص است");
            return;
        }
        if ((int)ComboBoxStructureSkeleton.SelectedItem.Value == (int)TSP.DataManager.TSStructureSkeleton.Mix && (string.IsNullOrEmpty(txtSazehTarkibiFoundation.Text) || Convert.ToInt32(txtSazehTarkibiFoundation.Text.Trim()) <= 0 || Convert.ToInt32(txtProjectFoundation.Text.Trim()) <= Convert.ToInt32(txtSazehTarkibiFoundation.Text.Trim())))
        {
            SetLabelWarning("متراژ طراح سازه در اسکلت ترکیبی صحیح نمی باشد");
            return;
        }
        #region Define Managers
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TechnicalServices.StructureGroupsManager StructureGroupsManager = new TSP.DataManager.TechnicalServices.StructureGroupsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(LoginManager);
        transact.Add(ProjectManager);
        transact.Add(AttachManager);
        transact.Add(ProjectRequestManager);
        transact.Add(StructureGroupsManager);
        transact.Add(WorkFlowStateManager);
        transact.Add(WorkFlowTaskManager);
        #endregion
        try
        {
            if (ASPxComboBoxStructureGroups.SelectedIndex == -1 || Utility.IsDBNullOrNullValue(ASPxComboBoxStructureGroups.Value))
            {
                SetLabelWarning("گروه ساختمانی نامشخص است");
                return;
            }


            transact.BeginSave();

            InsertProject(ProjectManager, StructureGroupsManager);

            InsertProjectRequest(ProjectRequestManager, TSP.DataManager.TSProjectRequestType.InsertProject);

            if (!string.IsNullOrEmpty(txtRegisteredNo.Text))
                if (!InsertMainRegisterNo(_ProjectId, _PrjReId, transact))
                {
                    transact.CancelSave();
                    return;
                }
            if (!string.IsNullOrEmpty(txtOwnerFirstName.Text) || !string.IsNullOrWhiteSpace(txtOwnerLastName.Text))
                if (!InsertProjectOwner(transact, _ProjectId, _PrjReId))
                {
                    transact.CancelSave();
                    return;
                }
            //if (!string.IsNullOrWhiteSpace(txtProjectFoundation.Text))
            //    if (!InsertProjectBlock(transact, _ProjectId, _PrjReId))
            //    {
            //        transact.CancelSave();
            //        return;
            //    }
            //     InsertWorkFlowState(WorkFlowStateManager, WorkFlowTaskManager, (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo);
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
            int NmcId = FindNmcId(GetTaskId(TaskCode));
            if (NmcId == -1)
            {
                transact.CancelSave();
                return;
            }

            if (WorkFlowStateManager.StartWorkFlow(_PrjReId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), 0, WorkFlowTaskManager) <= 0)
            {
                transact.CancelSave();
                SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
                return;
            }

            #region UserLog
            String Password = (new Random().Next(0, 1000000)).ToString();
            DataRow logRow = LoginManager.NewRow();
            logRow.BeginEdit();
            logRow["UserName"] = "prj" + _ProjectId.ToString();
            logRow["Password"] = Utility.EncryptPassword(Password);
            logRow["UltId"] = (int)TSP.DataManager.UserType.TSProjectOwner;
            logRow["MeId"] = _ProjectId;
            logRow["Email"] = "";
            logRow["IsValid"] = 1;
            logRow["UserId2"] = Utility.GetCurrentUser_UserId();
            logRow["ModifiedDate"] = DateTime.Now;
            logRow.EndEdit();
            LoginManager.AddRow(logRow);
            if (LoginManager.Save() <= 0)
            {
                transact.CancelSave();
                SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            #endregion
            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();
            SetLabelWarning("ذخیره انجام شد.نام کاربری جهت ارائه به مالک" + "prj" + _ProjectId.ToString() + "و رمز عبور " + Password.ToString() + "می باشد");
            try
            {
                if (!string.IsNullOrWhiteSpace(txtOwnerMobileNo.Text) && !Utility.IsSmsToProjectOwnerOff())
                {
                    string OwnerName = "";
                    if (CmbType.Value.ToString() == "1")//OtherPerson   
                        OwnerName = txtOwnerFirstName.Text + " " + txtOwnerLastName.Text;
                    else
                        OwnerName = txtOwnerFirstName.Text;
                    string SMSBody = "مالک محترم " + OwnerName + " با کد ملی " + txtSSN.Text + " پروژه شما در سازمان نظام مهندسی ساختمان با کد " + _ProjectId.ToString() + ((Convert.ToInt32(ASPxComboBoxCity.Value) == (int)TSP.DataManager.CityCode.Sadra || Convert.ToInt32(ASPxComboBoxCity.Value) == (int)TSP.DataManager.CityCode.Shiraz) ? "" : " و شناسه دسترسی طراح " + txtTraceCode.Text + " ") + "ثبت گردید.مابقی مراحل متعاقبا به شما اعلام خواهد شد" + ((Convert.ToInt32(ASPxComboBoxDiscountPercent.Value) != (int)TSP.DataManager.TSDiscountPercent.Industrial && (Convert.ToInt32(txtStageNum.Text) >= 7 || Convert.ToInt32(txtProjectFoundation.Text) > 2000)) ? "پروژه شما مشمول اخذ سازنده ذیصلاح (مجری) می باشد. لطفا نسبت به معرفی مجری پروژه خود به سازمان نظام مهندسی اقدام نمایید." : "")
                          + "\n"
                     + "نام کاربری " + "prj" + _ProjectId.ToString()
                     + (!string.IsNullOrWhiteSpace(Password) ? "\n" + "رمز عبور " + Password : "")
                       + "\n"
                       + "https://fceo.ir/login.aspx"
                    + "\n" + "باتوجه به اینکه تمام مراحل پرداخت حق الزحمه نظارت و پیگیری پرداخت ها از طریق نام کاربری و کلمه عبور زیر انجام می گیرد در نگهداری آن کوشا باشید.";


                    SendSMSNotification(Utility.Notifications.NotificationTypes.TSProjectOwner, SMSBody, txtOwnerMobileNo.Text, _ProjectId.ToString()
                      );
                }
            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
            }

        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err, 'I');
        }
    }

    #region InsertProject

    private void InsertProject(TSP.DataManager.TechnicalServices.ProjectManager ProjectManager, TSP.DataManager.TechnicalServices.StructureGroupsManager StructureGroupsManager)
    {
        DataRow rowProject = ProjectManager.NewRow();

        rowProject.BeginEdit();
        rowProject["ProjectName"] = ASPxTextBoxProjectName.Text;
        rowProject["ArchiveNo"] = txtArchiveNo.Text;
        Random rand = new Random();
        rowProject["TraceCode"] = txtTraceCode.Text = rand.Next(10000000, 99999999).ToString();

        rowProject["FileNo"] = ASPxTextBoxFileNo.Text;
        rowProject["FileDate"] = FileDate.Text;
        if (ASPxComboBoxUsage.Value != null)
            rowProject["UsageId"] = ASPxComboBoxUsage.Value;
        if (ASPxComboBoxStructureGroups.Value != null && ASPxComboBoxStructureGroups.SelectedItem != null)
            rowProject["GroupId"] = ASPxComboBoxStructureGroups.Value;
        rowProject["ReconstructionCode"] = ASPxTextBoxReconstructionCode.Text;
        rowProject["Foundation"] = txtProjectFoundation.Text;
        rowProject["MaxStageNum"] = txtStageNum.Text;

        if (!string.IsNullOrEmpty(txtSazehTarkibiFoundation.Text))
            rowProject["FoundationMixSkeleton"] = txtSazehTarkibiFoundation.Text;
        if (ComboBoxStructureSkeleton.Value != null)
            rowProject["StructureSkeletonId"] = ComboBoxStructureSkeleton.Value;
        if (ComboBoxRoofType.Value != null)
            rowProject["RoofTypeId"] = ComboBoxRoofType.Value;

        if (!string.IsNullOrEmpty(ASPxTextBoxArea.Text))
            rowProject["Area"] = ASPxTextBoxArea.Text;
        if (!string.IsNullOrEmpty(ASPxTextBoxRecessArea.Text))
            rowProject["RecessArea"] = ASPxTextBoxRecessArea.Text;
        if (!string.IsNullOrEmpty(ASPxTextBoxRemainArea.Text))
            rowProject["RemainArea"] = ASPxTextBoxRemainArea.Text;
        if (!string.IsNullOrEmpty(ASPxTextBoxDocumentArea.Text))
            rowProject["DocumentArea"] = ASPxTextBoxDocumentArea.Text;
        if (ASPxComboBoxCity.Value != null)
            rowProject["CitId"] = ASPxComboBoxCity.Value;
        if (ASPxComboBoxMunicipality.Value != null)
            rowProject["MunId"] = ASPxComboBoxMunicipality.Value;
        rowProject["Address"] = TextBoxAddress.Text;
        rowProject["Date"] = RegDate.Text;
        rowProject["ComputerCode"] = ASPxTextBoxComputerCode.Text;
        rowProject["AgentId"] = ASPxComboBoxAgent.Value;
        rowProject["DiscountPercentId"] = ASPxComboBoxDiscountPercent.Value;
        rowProject["ProjectStatusId"] = ASPxComboBoxProjectStatus.Value;
        if (cmbOwnerShipType.Value != null)
            rowProject["OwnershipTypeId"] = cmbOwnerShipType.Value;
        rowProject["DesignerSaved"] = !chbHasDesigner.Checked;
        rowProject["ObserverSaved"] = !chbHasObserver.Checked;
        if (!string.IsNullOrEmpty(txtRegisteredNo.Text))
            rowProject["MainRegisterNo"] = txtRegisteredNo.Text;
        if (CmbType.Value.ToString() == "1")//OtherPerson   
            rowProject["OwnerFullName"] = txtOwnerFirstName.Text + " " + txtOwnerLastName.Text;
        else
            rowProject["OwnerFullName"] = txtOwnerFirstName.Text;
        if (!string.IsNullOrEmpty(txtMainRegion.Text))
            rowProject["MainRegion"] = txtMainRegion.Text;
        if (!string.IsNullOrEmpty(txtMainSection.Text))
            rowProject["MainSection"] = txtMainSection.Text;
        if (_TechnicalBookletAttachName != null)
        {
            rowProject["FileUrlTechnicalBooklet"] = _TechnicalBookletAttachName;
        }
        #region پروانه ساخت
        if (!string.IsNullOrEmpty(txtBuildingCertificateStartDate.Text))
            rowProject["BuildingCertificateStartDate"] = txtBuildingCertificateStartDate.Text;
        if (!string.IsNullOrEmpty(txtBuildingCertificateExpirDate.Text))
            rowProject["BuildingCertificateExpirDate"] = txtBuildingCertificateExpirDate.Text;
        if (!string.IsNullOrEmpty(txtBuildingCertificateNum.Text))
            rowProject["BuildingCertificateNum"] = txtBuildingCertificateNum.Text;
        if (_BuildingCertificateAttachName != null)
        {
            rowProject["FileUrlBuildingCertificate"] = _BuildingCertificateAttachName;
        }
        #endregion
        #region پایان کار
        if (!string.IsNullOrEmpty(txtEndProjectStartDate.Text))
            rowProject["EndProjectStartDate"] = txtEndProjectStartDate.Text;
        if (!string.IsNullOrEmpty(txtEndProjectNum.Text))
            rowProject["EndProjectNum"] = txtEndProjectNum.Text;
        if (_BuildingLicenceAttachNameRnd != null)
        {
            rowProject["FileUrlBuildingLicence"] = _BuildingLicenceAttachNameRnd;
        }
        #endregion
        #region شروع نشدن ساخت و ساز
        if (_BuldingCheckAttachName != null)
        {
            rowProject["FileURLBuldingCheck"] = _BuldingCheckAttachName;
        }
        if (!string.IsNullOrEmpty(txtBuldingCheckDate.Text))
            rowProject["BuldingCheckDate"] = txtBuldingCheckDate.Text;
        #endregion


        rowProject["UserId"] = Utility.GetCurrentUser_UserId();
        rowProject["ModifiedDate"] = DateTime.Now;
        rowProject.EndEdit();

        ProjectManager.AddRow(rowProject);

        ProjectManager.Save();

        ProjectManager.DataTable.AcceptChanges();
        _ProjectId = Convert.ToInt32(ProjectManager[0]["ProjectId"]);
    }

    private void InsertProjectRequest(TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager, TSP.DataManager.TSProjectRequestType ProjectRequestType)
    {
        DataRow rowProjectRequest = ProjectRequestManager.NewRow();

        rowProjectRequest["ProjectId"] = _ProjectId;
        rowProjectRequest["PrjReTypeId"] = (int)ProjectRequestType;
        rowProjectRequest["RequestDate"] = RegDate.Text;
        rowProjectRequest["ProjectName"] = ASPxTextBoxProjectName.Text;
        rowProjectRequest["ArchiveNo"] = txtArchiveNo.Text;
        if (ASPxComboBoxUsage.Value != null)
            rowProjectRequest["UsageId"] = ASPxComboBoxUsage.Value;
        if (ASPxComboBoxStructureGroups.Value != null && ASPxComboBoxStructureGroups.SelectedItem != null)
            rowProjectRequest["GroupId"] = ASPxComboBoxStructureGroups.Value;
        rowProjectRequest["ReconstructionCode"] = ASPxTextBoxReconstructionCode.Text;
        rowProjectRequest["Foundation"] = txtProjectFoundation.Text;
        rowProjectRequest["MaxStageNum"] = txtStageNum.Text;

        if (!string.IsNullOrEmpty(txtSazehTarkibiFoundation.Text))
            rowProjectRequest["FoundationMixSkeleton"] = txtSazehTarkibiFoundation.Text;
        if (ComboBoxStructureSkeleton.Value != null)
            rowProjectRequest["StructureSkeletonId"] = ComboBoxStructureSkeleton.Value;
        if (ComboBoxRoofType.Value != null)
            rowProjectRequest["RoofTypeId"] = ComboBoxRoofType.Value;

        if (!string.IsNullOrEmpty(ASPxTextBoxArea.Text))
            rowProjectRequest["Area"] = ASPxTextBoxArea.Text;
        if (!string.IsNullOrEmpty(ASPxTextBoxRecessArea.Text))
            rowProjectRequest["RecessArea"] = ASPxTextBoxRecessArea.Text;
        if (!string.IsNullOrEmpty(ASPxTextBoxRemainArea.Text))
            rowProjectRequest["RemainArea"] = ASPxTextBoxRemainArea.Text;
        if (!string.IsNullOrEmpty(ASPxTextBoxDocumentArea.Text))
            rowProjectRequest["DocumentArea"] = ASPxTextBoxDocumentArea.Text;
        if (ASPxComboBoxCity.Value != null)
            rowProjectRequest["CitId"] = ASPxComboBoxCity.Value;
        if (ASPxComboBoxMunicipality.Value != null)
            rowProjectRequest["MunId"] = ASPxComboBoxMunicipality.Value;
        rowProjectRequest["Address"] = TextBoxAddress.Text;
        rowProjectRequest["ComputerCode"] = ASPxTextBoxComputerCode.Text;
        rowProjectRequest["DiscountPercentId"] = ASPxComboBoxDiscountPercent.Value;
        rowProjectRequest["AgentId"] = ASPxComboBoxAgent.Value;
        rowProjectRequest["ProjectStatusId"] = ASPxComboBoxProjectStatus.Value;
        rowProjectRequest["IsConfirmed"] = 0;
        rowProjectRequest["InActive"] = 0;
        rowProjectRequest["DesignerSaved"] = !chbHasDesigner.Checked;
        rowProjectRequest["ObserverSaved"] = !chbHasObserver.Checked;
        rowProjectRequest["Description"] = txtReqDesc.Text.Trim();
        if (cmbOwnerShipType.Value != null)
            rowProjectRequest["OwnershipTypeId"] = cmbOwnerShipType.Value;


        if (!string.IsNullOrEmpty(txtRegisteredNo.Text))
            rowProjectRequest["MainRegisterNo"] = txtRegisteredNo.Text;

        if (CmbType.Value.ToString() == "1")//OtherPerson   
            rowProjectRequest["OwnerFullName"] = txtOwnerFirstName.Text + " " + txtOwnerLastName.Text;
        else
            rowProjectRequest["OwnerFullName"] = txtOwnerFirstName.Text;


        if (!string.IsNullOrEmpty(txtMainRegion.Text))
            rowProjectRequest["MainRegion"] = txtMainRegion.Text;
        if (!string.IsNullOrEmpty(txtMainSection.Text))
            rowProjectRequest["MainSection"] = txtMainSection.Text;

        if (_TechnicalBookletAttachName != null)
        {
            rowProjectRequest["FileUrlTechnicalBooklet"] = _TechnicalBookletAttachName;
        }
        #region پروانه ساخت
        if (!string.IsNullOrEmpty(txtBuildingCertificateStartDate.Text))
            rowProjectRequest["BuildingCertificateStartDate"] = txtBuildingCertificateStartDate.Text;
        if (!string.IsNullOrEmpty(txtBuildingCertificateExpirDate.Text))
            rowProjectRequest["BuildingCertificateExpirDate"] = txtBuildingCertificateExpirDate.Text;
        if (!string.IsNullOrEmpty(txtBuildingCertificateNum.Text))
            rowProjectRequest["BuildingCertificateNum"] = txtBuildingCertificateNum.Text;
        if (_BuildingCertificateAttachName != null)
        {
            rowProjectRequest["FileUrlBuildingCertificate"] = _BuildingCertificateAttachName;
        }
        #endregion
        #region  پایان کار
        if (!string.IsNullOrEmpty(txtEndProjectStartDate.Text))
            rowProjectRequest["EndProjectStartDate"] = txtEndProjectStartDate.Text;
        if (!string.IsNullOrEmpty(txtEndProjectNum.Text))
            rowProjectRequest["EndProjectNum"] = txtEndProjectNum.Text;
        if (_BuildingLicenceAttachNameRnd != null)
        {
            rowProjectRequest["FileUrlBuildingLicence"] = _BuildingLicenceAttachNameRnd;
        }
        #endregion
        #region شروع نشدن ساخت و ساز
        if (_BuldingCheckAttachName != null)
        {
            rowProjectRequest["FileURLBuldingCheck"] = _BuldingCheckAttachName;
        }
        if (!string.IsNullOrEmpty(txtBuldingCheckDate.Text))
            rowProjectRequest["BuldingCheckDate"] = txtBuldingCheckDate.Text;
        #endregion
        rowProjectRequest["UserId"] = Utility.GetCurrentUser_UserId();
        rowProjectRequest["ModifiedDate"] = DateTime.Now;

        ProjectRequestManager.AddRow(rowProjectRequest);

        ProjectRequestManager.Save();

        ProjectRequestManager.DataTable.AcceptChanges();
        _PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);

    }

    private bool InsertWorkFlowState(TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, int TaskCode)
    {
        int NmcId = FindNmcId(GetTaskId(TaskCode));
        //PrjReId = Utility.DecryptQS(PkPrjReId.Value);

        if (NmcId == -1)
            return false;

        //  PrjReId = Utility.DecryptQS(PkPrjReId.Value);
        WorkFlowStateManager.StartWorkFlow(_PrjReId, TaskCode, NmcId, Utility.GetCurrentUser_UserId(), 0, WorkFlowTaskManager);
        return true;
    }

    private void InsertNewRequest(int ProjectRequestType, int WorkFlowTask)
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TransactionManager Transact = new TSP.DataManager.TransactionManager();

        Transact.Add(ProjectRequestManager);
        Transact.Add(WorkFlowStateManager);
        Transact.Add(WorkFlowTaskManager);

        int RegisteredNoId = -2; int OwnerId = -2;
        try
        {
            RegisteredNoId = FindMainRegisteredNo(_ProjectId, false);
            OwnerId = FindOwnerAgent(_ProjectId, false);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return;
        }
        try
        {
            if (ASPxComboBoxStructureGroups.Value == null || ASPxComboBoxStructureGroups.SelectedItem == null)
            {
                SetLabelWarning("گروه ساختمانی را  بر اساس تعداد طبقات از روی شالوده  و زیر بنا انتخاب نمایید.");
                return;
            }

            Transact.BeginSave();

            ProjectRequestManager.FindByCode(_PrjReId);
            if (ProjectRequestManager.Count != 1)
            {
                Transact.CancelSave();
                SetLabelWarning("خطایی در بازخوانی اطلاعات انجام گرفته است.");
                return;
            }

            DataRow PrjReRow = ProjectRequestManager.NewRow();

            PrjReRow["ProjectId"] = ProjectRequestManager[0]["ProjectId"].ToString();

            PrjReRow["PrjReTypeId"] = ProjectRequestType;
            PrjReRow["ProjectStatusId"] = ProjectRequestManager[0]["ProjectStatusId"];

            PrjReRow["ProjectName"] = ASPxTextBoxProjectName.Text;
            PrjReRow["ArchiveNo"] = txtArchiveNo.Text;
            PrjReRow["FileNo"] = ASPxTextBoxFileNo.Text;
            PrjReRow["FileDate"] = FileDate.Text;
            if (ASPxComboBoxUsage.Value != null)
                PrjReRow["UsageId"] = ASPxComboBoxUsage.Value;
            if (ASPxComboBoxStructureGroups.Value != null && ASPxComboBoxStructureGroups.SelectedItem != null)
                PrjReRow["GroupId"] = ASPxComboBoxStructureGroups.Value;
            PrjReRow["ReconstructionCode"] = ASPxTextBoxReconstructionCode.Text;
            PrjReRow["Foundation"] = txtProjectFoundation.Text;
            PrjReRow["MaxStageNum"] = txtStageNum.Text;

            if (!string.IsNullOrEmpty(txtSazehTarkibiFoundation.Text))
                PrjReRow["FoundationMixSkeleton"] = txtSazehTarkibiFoundation.Text;
            if (ComboBoxStructureSkeleton.Value != null)
                PrjReRow["StructureSkeletonId"] = ComboBoxStructureSkeleton.Value;
            if (ComboBoxRoofType.Value != null)
                PrjReRow["RoofTypeId"] = ComboBoxRoofType.Value;

            if (!string.IsNullOrEmpty(ASPxTextBoxArea.Text))
                PrjReRow["Area"] = ASPxTextBoxArea.Text;
            if (!string.IsNullOrEmpty(ASPxTextBoxRecessArea.Text))
                PrjReRow["RecessArea"] = ASPxTextBoxRecessArea.Text;
            if (!string.IsNullOrEmpty(ASPxTextBoxRemainArea.Text))
                PrjReRow["RemainArea"] = ASPxTextBoxRemainArea.Text;
            if (!string.IsNullOrEmpty(ASPxTextBoxDocumentArea.Text))
                PrjReRow["DocumentArea"] = ASPxTextBoxDocumentArea.Text;
            PrjReRow["Description"] = txtReqDesc.Text.Trim();
            if (ASPxComboBoxCity.Value != null)
                PrjReRow["CitId"] = ASPxComboBoxCity.Value;
            if (ASPxComboBoxMunicipality.Value != null)
                PrjReRow["MunId"] = ASPxComboBoxMunicipality.Value;
            PrjReRow["Address"] = TextBoxAddress.Text;
            PrjReRow["ComputerCode"] = ASPxTextBoxComputerCode.Text;
            PrjReRow["DiscountPercentId"] = ASPxComboBoxDiscountPercent.Value;
            PrjReRow["AgentId"] = ASPxComboBoxAgent.Value;
            if (cmbOwnerShipType.Value != null)
                PrjReRow["OwnershipTypeId"] = cmbOwnerShipType.Value;

            if (!string.IsNullOrEmpty(txtRegisteredNo.Text))
                PrjReRow["MainRegisterNo"] = txtRegisteredNo.Text;
            if (CmbType.Value.ToString() == "1")//OtherPerson   
                PrjReRow["OwnerFullName"] = txtOwnerFirstName.Text + " " + txtOwnerLastName.Text;
            else
                PrjReRow["OwnerFullName"] = txtOwnerFirstName.Text;
            if (!string.IsNullOrEmpty(txtMainRegion.Text))
                PrjReRow["MainRegion"] = txtMainRegion.Text;
            if (!string.IsNullOrEmpty(txtMainSection.Text))
                PrjReRow["MainSection"] = txtMainSection.Text;
            PrjReRow["RequestDate"] = Utility.GetDateOfToday();
            PrjReRow["DesignerSaved"] = !chbHasDesigner.Checked;
            PrjReRow["ObserverSaved"] = !chbHasObserver.Checked;            
            PrjReRow["IsConfirmed"] = 0;
            PrjReRow["InActive"] = 0;
            if (_TechnicalBookletAttachName != null)
            {
                PrjReRow["FileUrlTechnicalBooklet"] = _TechnicalBookletAttachName;
            }
            else
                PrjReRow["FileUrlTechnicalBooklet"] = ProjectRequestManager[0]["FileUrlTechnicalBooklet"];

            #region پروانه ساخت
            if (!string.IsNullOrEmpty(txtBuildingCertificateStartDate.Text))
                PrjReRow["BuildingCertificateStartDate"] = txtBuildingCertificateStartDate.Text;
            if (!string.IsNullOrEmpty(txtBuildingCertificateExpirDate.Text))
                PrjReRow["BuildingCertificateExpirDate"] = txtBuildingCertificateExpirDate.Text;
            if (!string.IsNullOrEmpty(txtBuildingCertificateNum.Text))
                PrjReRow["BuildingCertificateNum"] = txtBuildingCertificateNum.Text;
            if (_BuildingCertificateAttachName != null)
            {
                PrjReRow["FileUrlBuildingCertificate"] = _BuildingCertificateAttachName;
            }
            else
                PrjReRow["FileUrlBuildingCertificate"] = ProjectRequestManager[0]["FileUrlBuildingCertificate"];
            #endregion
            #region پایان کار
            if (!string.IsNullOrEmpty(txtEndProjectStartDate.Text))
                PrjReRow["EndProjectStartDate"] = txtEndProjectStartDate.Text;
            if (!string.IsNullOrEmpty(txtEndProjectNum.Text))
                PrjReRow["EndProjectNum"] = txtEndProjectNum.Text;
            if (_BuildingLicenceAttachNameRnd != null)
            {
                PrjReRow["FileUrlBuildingLicence"] = _BuildingLicenceAttachNameRnd;
            }
            else
                PrjReRow["FileUrlBuildingLicence"] = ProjectRequestManager[0]["FileUrlBuildingLicence"];

            #endregion
            #region شروع نشدن ساخت و ساز
            if (_BuldingCheckAttachName != null)
            {
                PrjReRow["FileURLBuldingCheck"] = _BuldingCheckAttachName;
            }
            else
                PrjReRow["FileURLBuldingCheck"] = ProjectRequestManager[0]["FileURLBuldingCheck"];

            if (!string.IsNullOrEmpty(txtBuldingCheckDate.Text))
                PrjReRow["BuldingCheckDate"] = txtBuldingCheckDate.Text;
            #endregion
            PrjReRow["UserId"] = Utility.GetCurrentUser_UserId();
            PrjReRow["ModifiedDate"] = DateTime.Now;

            ProjectRequestManager.AddRow(PrjReRow);
            ProjectRequestManager.Save();


            int NmcId = FindNmcId(GetTaskId(WorkFlowTask));
            if (NmcId == -1)
            {
                Transact.CancelSave();
                return;
            }

            if (WorkFlowStateManager.StartWorkFlow(Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"]), WorkFlowTask, NmcId, Utility.GetCurrentUser_UserId(), 0, WorkFlowTaskManager) <= 0)
            {
                Transact.CancelSave();
                SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
                return;
            }


            #region RegisterNo

            if (RegisteredNoId < 0 && !string.IsNullOrWhiteSpace(txtRegisteredNo.Text))
            {

                if (!InsertMainRegisterNo(_ProjectId, Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"]), Transact))
                {
                    Transact.CancelSave();
                    return;
                }
            }
            else if (RegisteredNoId > 0 && !string.IsNullOrWhiteSpace(txtRegisteredNo.Text) && Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"]) != _MainRegNoPrjReId)
            {

                if (!InsertMainRegisterNo(_ProjectId, Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"]), Transact))
                {
                    Transact.CancelSave();
                    return;
                }
            }
            else if (RegisteredNoId > 0 && !string.IsNullOrWhiteSpace(txtRegisteredNo.Text) && Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"]) == _MainRegNoPrjReId)
            {
                if (!UpdateRegisteredNo(Transact, RegisteredNoId))
                {
                    Transact.CancelSave();
                    return;
                }
            }
            #endregion

            #region AgentOwner
            if (OwnerId < 0 && (!string.IsNullOrEmpty(txtOwnerFirstName.Text) || !string.IsNullOrWhiteSpace(txtOwnerLastName.Text)))
            {
                if (!InsertProjectOwner(Transact, _ProjectId, Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"])))
                {
                    Transact.CancelSave();
                    return;
                }
            }
            else if (OwnerId > 0 && (!string.IsNullOrEmpty(txtOwnerFirstName.Text) || !string.IsNullOrWhiteSpace(txtOwnerLastName.Text)) && Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"]) != _OwnerPrjReId)
            {
                if (!InsertProjectOwner(Transact, _ProjectId, Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"])))
                {
                    Transact.CancelSave();
                    return;
                }
            }
            else if (OwnerId > 0 && (!string.IsNullOrEmpty(txtOwnerFirstName.Text) || !string.IsNullOrWhiteSpace(txtOwnerLastName.Text)) && Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"]) == _OwnerPrjReId)
            {
                if (!UpdateOwner(Transact, OwnerId))
                {
                    Transact.CancelSave();
                    return;
                }
            }
            #endregion

            _PrjReId = Convert.ToInt32(ProjectRequestManager[ProjectRequestManager.Count - 1]["PrjReId"]);
            Transact.EndSave();
            SetLabelWarning("ذخیره انجام شد.");


            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();
        }
        catch (Exception err)
        {
            Transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err, 'I');
        }
    }

    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId)
    {

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {

            DataTable dtRecivers = new DataTable();
            dtRecivers.Columns.Add("SMSMobileNo");
            dtRecivers.Columns.Add("SMSMeId");
            dtRecivers.Columns.Add("SMSUltId");
            dtRecivers.Columns.Add("Description");

            DataRow dr = dtRecivers.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = ((int)TSP.DataManager.UserType.TSProjectOwner).ToString();
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms contain TempPass
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }
    #endregion

    #region Insert RegisterNo

    private Boolean InsertMainRegisterNo(int ProjectId, int PrjReId, TSP.DataManager.TransactionManager transact)
    {

        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        TSP.DataManager.TechnicalServices.DirectionsManager DirectionsManager = new TSP.DataManager.TechnicalServices.DirectionsManager();
        transact.Add(RegisteredNoManager);
        transact.Add(DirectionsManager);
        RegisteredNoManager.FindByProjectId(ProjectId);
        if (RegisteredNoManager.Count > 0)
        {
            for (int i = 0; i < RegisteredNoManager.Count; i++)
            {
                RegisteredNoManager[i].BeginEdit();
                RegisteredNoManager[i]["IsMain"] = 0;
                RegisteredNoManager[i].EndEdit();
            }
            RegisteredNoManager.Save();
            RegisteredNoManager.DataTable.AcceptChanges();
        }
        //if (!CheckIsRegNoExist(txtRegisteredNo.Text, RegisteredNoManager))
        //    return false;
        DataRow rowRegisteredNo = RegisteredNoManager.NewRow();
        rowRegisteredNo["ProjectId"] = ProjectId;
        rowRegisteredNo["RegisteredNo"] = _MainRegNo = txtRegisteredNo.Text;
        rowRegisteredNo["PostalCode"] = "";
        rowRegisteredNo["IsMain"] = 1;
        rowRegisteredNo["InActive"] = 0;
        rowRegisteredNo["PrjReId"] = PrjReId;
        rowRegisteredNo["Division"] = 0;
        rowRegisteredNo["UserId"] = Utility.GetCurrentUser_UserId();
        rowRegisteredNo["ModifiedDate"] = DateTime.Now;
        RegisteredNoManager.AddRow(rowRegisteredNo);
        RegisteredNoManager.Save();
        RegisteredNoManager.DataTable.AcceptChanges();

        int RegisteredNoId = Convert.ToInt32(RegisteredNoManager[0]["RegisteredNoId"]);
        InsertRegisteredNoDirection(RegisteredNoId, DirectionsManager, (int)TSP.DataManager.TSDirectionType.Dimension);
        InsertRegisteredNoDirection(RegisteredNoId, DirectionsManager, (int)TSP.DataManager.TSDirectionType.Length);
        InsertRegisteredNoDirection(RegisteredNoId, DirectionsManager, (int)TSP.DataManager.TSDirectionType.Wideness);
        InsertRegisteredNoDirection(RegisteredNoId, DirectionsManager, (int)TSP.DataManager.TSDirectionType.RemainDimension);
        InsertRegisteredNoDirection(RegisteredNoId, DirectionsManager, (int)TSP.DataManager.TSDirectionType.PathWayWidth);
        InsertRegisteredNoDirection(RegisteredNoId, DirectionsManager, (int)TSP.DataManager.TSDirectionType.Limit);
        return true;
    }

    private void InsertRegisteredNoDirection(int RegisteredNoId, TSP.DataManager.TechnicalServices.DirectionsManager DirectionsManager, int DirectionTypeId)
    {
        DataRow rowDirections = DirectionsManager.NewRow();

        rowDirections["RegisteredNoId"] = RegisteredNoId;
        rowDirections["North"] = "";
        rowDirections["East"] = "";
        rowDirections["South"] = "";
        rowDirections["West"] = "";
        rowDirections["DirectionTypeId"] = DirectionTypeId;
        rowDirections["InActive"] = 0;
        rowDirections["UserId"] = Utility.GetCurrentUser_UserId();
        rowDirections["ModifiedDate"] = DateTime.Now;

        DirectionsManager.AddRow(rowDirections);
        DirectionsManager.Save();
        DirectionsManager.DataTable.AcceptChanges();
    }

    private bool CheckIsRegNoExist(string RegNo, TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager)
    {
        RegisteredNoManager.FindByActiveRegisteredNo(RegNo);
        if (RegisteredNoManager.Count > 0)
        {
            if (_MainRegNo != RegNo)
            {
                SetLabelWarning("پیش از این پلاک ثبتی وارد شده به عنوان پلاک ثبتی پروژه با کد " + RegisteredNoManager[0]["ProjectId"].ToString() + " ثبت شده است.");
                return false;
            }
        }
        return true;
    }
    #endregion

    #region Insert Owner
    private Boolean InsertProjectOwner(TSP.DataManager.TransactionManager trans, int ProjectId, int PrjReId)
    {
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OrganizationManager OrganizationManager = new TSP.DataManager.OrganizationManager();

        trans.Add(OwnerManager);
        trans.Add(OtherPersonManager);
        trans.Add(OrganizationManager);

        if (CmbType.Value == null)
        {
            SetLabelWarning("نوع مالک را انتخاب نمایید");
            return false;
        }
        int OtherPersOrgId = -1;
        if (CmbType.Value.ToString() == "1")//OtherPerson            
            OtherPersOrgId = InsertOwnerOtherPerson(OtherPersonManager);
        else // Organization            
            OtherPersOrgId = InsertOwnerOrganization(OrganizationManager);

        InsertOwner(OwnerManager, ProjectId, PrjReId, OtherPersOrgId);

        return true;
        //  SetControlBycmbType();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="OwnerManager"></param>
    /// <param name="ProjectId"></param>
    /// <param name="PrjReId"></param>
    /// <returns>OwnerId</returns>
    private int InsertOwner(TSP.DataManager.TechnicalServices.OwnerManager OwnerManager, int ProjectId, int PrjReId, int OtherPersOrgId)
    {
        OwnerManager.FindByProjectId(ProjectId);
        if (OwnerManager.Count > 0)
        {
            for (int i = 0; i < OwnerManager.Count; i++)
            {
                OwnerManager[i].BeginEdit();
                OwnerManager[i]["IsAgent"] = false;
                OwnerManager[i].EndEdit();
            }
            OwnerManager.Save();
            OwnerManager.DataTable.AcceptChanges();
        }

        DataRow drOwner = OwnerManager.NewRow();

        drOwner["ProjectId"] = ProjectId;
        drOwner["OtherPersOrgId"] = OtherPersOrgId;
        drOwner["Type"] = CmbType.Value;
        drOwner["HaveLawyer"] = false;
        drOwner["LawyerId"] = DBNull.Value;

        if (!Utility.IsDBNullOrNullValue(txtSSN.Text) && CheckOwnerIsMemmberOfNezam(txtSSN.Text) != -1)
            drOwner["MeId"] = CheckOwnerIsMemmberOfNezam(txtSSN.Text);
        else
            drOwner["MeId"] = DBNull.Value;
        drOwner["IsAgent"] = true;
        drOwner["PrjReId"] = PrjReId;
        drOwner["CreateDate"] = Utility.GetDateOfToday();
        drOwner["UserId"] = Utility.GetCurrentUser_UserId();
        drOwner["ModifiedDate"] = DateTime.Now;

        OwnerManager.AddRow(drOwner);
        OwnerManager.Save();
        OwnerManager.DataTable.AcceptChanges();
        return Convert.ToInt32(OwnerManager[0]["OwnerId"]);
    }

    private int InsertOwnerOtherPerson(TSP.DataManager.OtherPersonManager OtherPersonManager)
    {
        DataRow drOth = OtherPersonManager.NewRow();

        drOth["Address"] = "";
        drOth["BirthPlace"] = "";
        drOth["FatherName"] = "";
        drOth["FirstName"] = txtOwnerFirstName.Text;
        drOth["IdNo"] = "";
        drOth["LastName"] = txtOwnerLastName.Text;
        drOth["MobileNo"] = txtOwnerMobileNo.Text;
        drOth["SSN"] = txtSSN.Text;
        drOth["Tel"] = "";
        drOth["OtpType"] = TSP.DataManager.OtherPersonType.Owner;
        drOth["UserId"] = Utility.GetCurrentUser_UserId();
        drOth["ModifiedDate"] = DateTime.Now;

        OtherPersonManager.AddRow(drOth);
        OtherPersonManager.Save();

        OtherPersonManager.DataTable.AcceptChanges();
        return Convert.ToInt32(OtherPersonManager[0]["OtpId"]);
    }

    private int InsertOwnerOrganization(TSP.DataManager.OrganizationManager OrganizationManager)
    {
        DataRow drOrg = OrganizationManager.NewRow();

        drOrg["ManagerName"] = "";
        drOrg["OrgName"] = txtOwnerFirstName.Text;
        drOrg["Tel"] = "";
        drOrg["MobileNo"] = txtOwnerMobileNo.Text;
        drOrg["Address"] = "";
        drOrg["CreateDate"] = Utility.GetDateOfToday();
        drOrg["UserId"] = Utility.GetCurrentUser_UserId();
        drOrg["ModifiedDate"] = DateTime.Now;
        drOrg["Type"] = 1;

        OrganizationManager.AddRow(drOrg);
        OrganizationManager.Save();

        OrganizationManager.DataTable.AcceptChanges();
        return Convert.ToInt32(OrganizationManager[0]["OrgId"]);
    }

    #endregion

    #region InserBlock
    private Boolean InsertProjectBlock(TSP.DataManager.TransactionManager transact, int ProjectId, int PrjReId)
    {
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();

        transact.Add(BlockManager);
        transact.Add(PlansMethodManager);
        transact.Add(ProjectManager);

        int BlockId = -1;
        if (!CheckFoundationWithProject(Convert.ToDouble(txtProjectFoundation.Text.Trim()), ProjectId, BlockId, BlockManager))
        {
            SetLabelWarning("مجموع مساحت زیربنای بلوک ها از زیربنای پروژه بیشتر است");
            return false;
        }

        if (Convert.ToInt32(txtStageNum.Text.Trim()) <= 0)
        {
            SetLabelWarning("تعداد طبقات از روی شالوده نامشخص است");
            return false;
        }

        BlockId = InsertBlock(BlockManager, PlansMethodManager, ProjectId, PrjReId);
        if (BlockId < 0)
            return false;

        UpdateDefualtPlanMethods(BlockManager, PlansMethodManager, ProjectId, PrjReId);


        return true;
    }

    private int GetPlansMethodId(TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        //ProjectId = Utility.DecryptQS(PkProjectId.Value);

        if (_ProjectId == -1)
            return -1;

        PlansMethodManager.FindByProjectId(_ProjectId);
        if (PlansMethodManager.Count > 0)
            return Convert.ToInt32(PlansMethodManager[0]["PlansMethodId"]);
        else
            return -1;
    }

    private int InsertBlock(TSP.DataManager.TechnicalServices.BlockManager BlockManager, TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager, int ProjectId, int PrjReId)
    {
        DataRow rowBlock = BlockManager.NewRow();

        int PlansMethodsId = GetPlansMethodId(PlansMethodManager);

        PlansMethodManager.FindByProjectId(ProjectId);
        if (PlansMethodManager.Count > 0)
            PlansMethodsId = Convert.ToInt32(PlansMethodManager[0]["PlansMethodId"]);
        else
        {
            PlansMethodsId = InsertDefualtPlanMethods(PlansMethodManager, ProjectId, PrjReId);
            if (PlansMethodsId == -1)
            {
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return -1;
            }
        }
        rowBlock["PlansMethodId"] = PlansMethodsId;
        //rowBlock["Foundation"] = txtBlockFoundation.Text;
        rowBlock["StageNum"] = txtStageNum.Text;
        rowBlock["StructureSystemId"] = (int)TSP.DataManager.TSStructureSystem.Others;
        rowBlock["StructureSystem"] = "";
        rowBlock["StructureSkeletonId"] = ComboBoxStructureSkeleton.Value;
        rowBlock["StructureSkeleton"] = "";
        if (ComboBoxRoofType.Value != null)
            rowBlock["RoofTypeId"] = ComboBoxRoofType.Value;
        else
            rowBlock["RoofTypeId"] = (int)TSP.DataManager.TSRoofType.Others;
        rowBlock["RoofType"] = "";
        rowBlock["UserId"] = Utility.GetCurrentUser_UserId();
        rowBlock["ModifiedDate"] = DateTime.Now;

        BlockManager.AddRow(rowBlock);
        BlockManager.Save();
        BlockManager.DataTable.AcceptChanges();
        return Convert.ToInt32(BlockManager[0]["BlockId"].ToString());
    }

    private int InsertDefualtPlanMethods(TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager, int ProjectId, int PrjReId)
    {
        DataRow rowPlansMethod = PlansMethodManager.NewRow();

        rowPlansMethod["ProjectId"] = ProjectId;
        rowPlansMethod["PlansMethodNo"] = "پیش فرض/" + ProjectId;
        rowPlansMethod["RegisteredDate"] = Utility.GetDateOfToday();
        rowPlansMethod["StructureBuiltPlaceId"] = 1;
        rowPlansMethod["EshghalSurface"] = 0;
        rowPlansMethod["Tarakom"] = 0;
        rowPlansMethod["AllowableHeight"] = 0;
        rowPlansMethod["CommercialLimitation"] = 0;
        rowPlansMethod["BlockNum"] = 0;
        rowPlansMethod["InActive"] = 0;
        rowPlansMethod["PrjReId"] = PrjReId;
        rowPlansMethod["LocationCriterion"] = 0;
        rowPlansMethod["Mantelet"] = 0;
        rowPlansMethod["UserId"] = Utility.GetCurrentUser_UserId();
        rowPlansMethod["ModifiedDate"] = DateTime.Now;

        PlansMethodManager.AddRow(rowPlansMethod);
        PlansMethodManager.Save();

        return Convert.ToInt32(PlansMethodManager[0]["PlansMethodId"]);
    }

    private void UpdateDefualtPlanMethods(TSP.DataManager.TechnicalServices.BlockManager BlockManager, TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager, int ProjectId, int PrjReId)
    {
        int Cnt = BlockManager.SelectTSBlockCountByProjectAndPrjReId(ProjectId, PrjReId);
        PlansMethodManager.FindByProjectAndPrjReId(ProjectId, PrjReId);
        if (PlansMethodManager.Count == 1)
        {
            PlansMethodManager[0].BeginEdit();
            PlansMethodManager[0]["BlockNum"] = Cnt;
            PlansMethodManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PlansMethodManager[0]["ModifiedDate"] = DateTime.Now;
            PlansMethodManager[0].EndEdit();
            PlansMethodManager.Save();
        }
    }

    private bool CheckFoundationWithProject(double Foundation, int ProjectId, int BlockId, TSP.DataManager.TechnicalServices.BlockManager BlockManager)
    {
        //TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();

        DataTable dtBock = BlockManager.SelectTSBlockByProject(ProjectId);
        for (int i = 0; i < dtBock.Rows.Count; i++)
            if (BlockId != Convert.ToInt32(dtBock.Rows[i]["BlockId"]))
                Foundation = Convert.ToDouble(dtBock.Rows[i]["Foundation"]) + Foundation;

        double ProjectFoundation = Convert.ToDouble(txtProjectFoundation.Text);

        if (Foundation > ProjectFoundation) return false;
        else return true;
    }

    private bool CheckStepByGroup(int Step)
    {
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager();

        ArrayList ArrStepRange = new ArrayList();
        ArrStepRange = PriceArchiveStructureItemsManager.GetCurrentStepFromAndStepTo(_PrjReId);

        if (Convert.ToInt32(ArrStepRange[0]) == -1 || Convert.ToInt32(ArrStepRange[1]) == -1)
        {
            SetLabelWarning("زیربنا یا گروه ساختمانی پروژه نامشخص است");
            return false;
        }

        //------------error-------------------------
        if (Convert.ToInt32(ArrStepRange[1]) != -2)
            if (Step > Convert.ToInt32(ArrStepRange[1]))
            {
                SetLabelWarning("هشدار : با توجه به زیربنا و گروه ساختمانی پروژه تعداد طبقات باید در محدوده "
                                        + ArrStepRange[0].ToString() + " تا " + ArrStepRange[1].ToString() + " طبقه باشد");
                return false;
            }

        //----------------just warning--------------
        if (Step < Convert.ToInt32(ArrStepRange[0]))
        {
            string Msg = "";
            if (Convert.ToInt32(ArrStepRange[1]) != -2)
                Msg = "هشدار : با توجه به زیربنا و گروه ساختمانی پروژه تعداد طبقات باید در محدوده "
                                        + ArrStepRange[0].ToString() + " تا " + ArrStepRange[1].ToString() + " طبقه باشد";
            else
                Msg = "هشدار : با توجه به زیربنا و گروه ساختمانی پروژه تعداد طبقات باید  "
                                        + ArrStepRange[0].ToString() + " به بالا باشد ";
            SetLabelWarning(Msg);
        }
        else
        {
            SetLabelWarning("");
        }
        return true;
    }
    #endregion

    #region UpdateProject
    private void Update()
    {
        if (IsPageRefresh)
            return;

        #region Define Managers
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(ProjectManager);
        transact.Add(AttachManager);
        transact.Add(ProjectRequestManager);
        int RegisteredNoId = -2; int OwnerId = -2;
        try
        {
            RegisteredNoId = FindMainRegisteredNo(_ProjectId, false);
            OwnerId = FindOwnerAgent(_ProjectId, false);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return;
        }
        #endregion
        try
        {

            transact.BeginSave();

            if (!UpdateProjectRequest(ProjectRequestManager, ProjectManager))
            {
                transact.CancelSave();
                return;
            }

            #region RegisterNo

            if (RegisteredNoId < 0 && !string.IsNullOrWhiteSpace(txtRegisteredNo.Text))
            {
                if (!InsertMainRegisterNo(_ProjectId, _PrjReId, transact))
                {
                    transact.CancelSave();
                    return;
                }
            }
            else if (RegisteredNoId > 0 && !string.IsNullOrWhiteSpace(txtRegisteredNo.Text) && _PrjReId != _MainRegNoPrjReId)
            {

                if (!InsertMainRegisterNo(_ProjectId, _PrjReId, transact))
                {
                    transact.CancelSave();
                    return;
                }
            }
            else if (RegisteredNoId > 0 && !string.IsNullOrWhiteSpace(txtRegisteredNo.Text) && _PrjReId == _MainRegNoPrjReId)
            {
                if (!UpdateRegisteredNo(transact, RegisteredNoId))
                {
                    transact.CancelSave();
                    return;
                }
            }
            #endregion

            #region AgentOwner
            if (OwnerId < 0)
            {
                if (!string.IsNullOrEmpty(txtOwnerFirstName.Text) || !string.IsNullOrWhiteSpace(txtOwnerLastName.Text))
                    if (!InsertProjectOwner(transact, _ProjectId, _PrjReId))
                    {
                        transact.CancelSave();
                        return;
                    }
            }
            else if (OwnerId > 0 && (!string.IsNullOrEmpty(txtOwnerFirstName.Text) || !string.IsNullOrWhiteSpace(txtOwnerLastName.Text)) && _PrjReId != _OwnerPrjReId)
            {
                if (!InsertProjectOwner(transact, _ProjectId, _PrjReId))
                {
                    transact.CancelSave();
                    return;
                }
            }
            else if (OwnerId > 0 && !string.IsNullOrEmpty(txtOwnerFirstName.Text))
            {
                if (!UpdateOwner(transact, OwnerId))
                {
                    transact.CancelSave();
                    return;
                }
            }
            if (CmbType.Value.ToString() == "1")//otherperson
            {
                txtOwnerFirstName.ClientVisible = true;
                txtOwnerLastName.ClientVisible = true;
                txtSSN.ClientVisible = true;
                lblOwnerLastName.ClientVisible = true;
                lblSSN.ClientVisible = true;
                lbloFirstName.Text = "نام نماینده مالکین";

            }
            else if (CmbType.Value.ToString() == "2")//otherorganization
            {
                txtOwnerLastName.ClientVisible = false;
                txtSSN.ClientVisible = false;
                lblOwnerLastName.ClientVisible = false;
                lblSSN.ClientVisible = false;
                lbloFirstName.Text = "نام سازمان";
            }
            #endregion          

            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();
            InsertWorkFlowStateLog("ویرایش اطلاعات توسط کاربر", TSP.DataManager.WorkFlowStateType.UpdateInfo);
            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err, 'U');
        }
    }

    private Boolean UpdateProject(TSP.DataManager.TechnicalServices.ProjectManager ProjectManager)
    {
        ProjectManager.FindByProjectId(_ProjectId);

        if (ProjectManager.Count != 1)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        ProjectManager[0].BeginEdit();
        ProjectManager[0]["ProjectName"] = ASPxTextBoxProjectName.Text;
        ProjectManager[0]["ArchiveNo"] = txtArchiveNo.Text;
        ProjectManager[0]["FileNo"] = ASPxTextBoxFileNo.Text;
        ProjectManager[0]["FileDate"] = FileDate.Text;
        ProjectManager[0]["MaxStageNum"] = txtStageNum.Text;
        if (ASPxComboBoxUsage.Value != null)
            ProjectManager[0]["UsageId"] = ASPxComboBoxUsage.Value;
        if (ASPxComboBoxStructureGroups.Value != null && ASPxComboBoxStructureGroups.SelectedItem != null)
            ProjectManager[0]["GroupId"] = ASPxComboBoxStructureGroups.Value;
        ProjectManager[0]["ReconstructionCode"] = ASPxTextBoxReconstructionCode.Text;
        ProjectManager[0]["Foundation"] = txtProjectFoundation.Text;
        if (!string.IsNullOrEmpty(ASPxTextBoxArea.Text))
            ProjectManager[0]["Area"] = ASPxTextBoxArea.Text;
        else
            ProjectManager[0]["Area"] = DBNull.Value;

        if (!string.IsNullOrEmpty(ASPxTextBoxRecessArea.Text))
            ProjectManager[0]["RecessArea"] = ASPxTextBoxRecessArea.Text;
        else
            ProjectManager[0]["RecessArea"] = DBNull.Value;

        if (!string.IsNullOrEmpty(ASPxTextBoxRemainArea.Text))
            ProjectManager[0]["RemainArea"] = ASPxTextBoxRemainArea.Text;
        else
            ProjectManager[0]["RemainArea"] = DBNull.Value;

        if (!string.IsNullOrEmpty(ASPxTextBoxDocumentArea.Text))
            ProjectManager[0]["DocumentArea"] = ASPxTextBoxDocumentArea.Text;
        else
            ProjectManager[0]["DocumentArea"] = DBNull.Value;

        ProjectManager[0]["CitId"] = ASPxComboBoxCity.Value;
        ProjectManager[0]["MunId"] = ASPxComboBoxMunicipality.Value;
        ProjectManager[0]["Address"] = TextBoxAddress.Text;
        ProjectManager[0]["Date"] = RegDate.Text;
        ProjectManager[0]["ComputerCode"] = ASPxTextBoxComputerCode.Text;
        ProjectManager[0]["AgentId"] = ASPxComboBoxAgent.Value;
        ProjectManager[0]["DiscountPercentId"] = ASPxComboBoxDiscountPercent.Value;
        ProjectManager[0]["ProjectStatusId"] = ASPxComboBoxProjectStatus.Value;
        if (cmbOwnerShipType.Value != null)
            ProjectManager[0]["OwnershipTypeId"] = cmbOwnerShipType.Value;
        if (CmbType.Value.ToString() == "1")//OtherPerson   
            ProjectManager[0]["OwnerFullName"] = txtOwnerFirstName.Text + " " + txtOwnerLastName.Text;
        else
            ProjectManager[0]["OwnerFullName"] = txtOwnerFirstName.Text;


        if (!string.IsNullOrEmpty(txtRegisteredNo.Text))
            ProjectManager[0]["MainRegisterNo"] = txtRegisteredNo.Text;
        if (!string.IsNullOrEmpty(txtMainRegion.Text))
            ProjectManager[0]["MainRegion"] = txtMainRegion.Text;
        if (!string.IsNullOrEmpty(txtMainSection.Text))
            ProjectManager[0]["MainSection"] = txtMainSection.Text;
        if (ComboBoxStructureSkeleton.Value != null)
            ProjectManager[0]["StructureSkeletonId"] = ComboBoxStructureSkeleton.Value;
        if (_TechnicalBookletAttachName != null)
        {
            ProjectManager[0]["FileUrlTechnicalBooklet"] = _TechnicalBookletAttachName;
        }
        ProjectManager[0]["DesignerSaved"] = !chbHasDesigner.Checked;
        ProjectManager[0]["ObserverSaved"] = !chbHasObserver.Checked;        
        #region پروانه ساخت
        if (!string.IsNullOrEmpty(txtBuildingCertificateStartDate.Text))
            ProjectManager[0]["BuildingCertificateStartDate"] = txtBuildingCertificateStartDate.Text;
        else
            ProjectManager[0]["BuildingCertificateStartDate"] = "";
        if (!string.IsNullOrEmpty(txtBuildingCertificateExpirDate.Text))
            ProjectManager[0]["BuildingCertificateExpirDate"] = txtBuildingCertificateExpirDate.Text;
        else
            ProjectManager[0]["BuildingCertificateExpirDate"] = "";
        if (!string.IsNullOrEmpty(txtBuildingCertificateNum.Text))
            ProjectManager[0]["BuildingCertificateNum"] = txtBuildingCertificateNum.Text;
        else
            ProjectManager[0]["BuildingCertificateNum"] = "";
        if (_BuildingCertificateAttachName != null)
        {
            ProjectManager[0]["FileUrlBuildingCertificate"] = _BuildingCertificateAttachName;
        }
        #endregion
        #region پایان کار
        if (!string.IsNullOrEmpty(txtEndProjectStartDate.Text))
            ProjectManager[0]["EndProjectStartDate"] = txtEndProjectStartDate.Text;
        if (!string.IsNullOrEmpty(txtEndProjectNum.Text))
            ProjectManager[0]["EndProjectNum"] = txtEndProjectNum.Text;
        if (_BuildingLicenceAttachNameRnd != null)
        {
            ProjectManager[0]["FileUrlBuildingLicence"] = _BuildingLicenceAttachNameRnd;
        }
        #endregion
        #region شروع نشدن ساخت و ساز
        if (_BuldingCheckAttachName != null)
        {
            ProjectManager[0]["FileURLBuldingCheck"] = _BuldingCheckAttachName;
        }
        if (!string.IsNullOrEmpty(txtBuldingCheckDate.Text))
            ProjectManager[0]["BuldingCheckDate"] = txtBuldingCheckDate.Text;
        #endregion
        ProjectManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        ProjectManager[0]["ModifiedDate"] = DateTime.Now;


        ProjectManager[0].EndEdit();

        ProjectManager.Save();
        ProjectManager.DataTable.AcceptChanges();
        return true;
    }

    private Boolean UpdateProjectRequest(TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager, TSP.DataManager.TechnicalServices.ProjectManager ProjectManager)
    {
        if (Convert.ToInt32(txtStageNum.Text.Trim()) <= 0)
        {
            SetLabelWarning("تعداد طبقات از روی شالوده نامشخص است");
            return false;
        }
        if (Convert.ToInt32(txtProjectFoundation.Text.Trim()) <= 0)
        {
            SetLabelWarning("متراژ پروژه نامشخص است");
            return false;
        }
        if ((int)ComboBoxStructureSkeleton.SelectedItem.Value == (int)TSP.DataManager.TSStructureSkeleton.Mix && (string.IsNullOrEmpty(txtSazehTarkibiFoundation.Text) || Convert.ToInt32(txtSazehTarkibiFoundation.Text.Trim()) <= 0 || Convert.ToInt32(txtProjectFoundation.Text.Trim()) <= Convert.ToInt32(txtSazehTarkibiFoundation.Text.Trim())))
        {
            SetLabelWarning("متراژ طراح سازه در اسکلت ترکیبی صحیح نمی باشد");
            return false;
        }
        ProjectRequestManager.FindByCode(_PrjReId);

        if (ProjectRequestManager.Count <= 0)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        if (Convert.ToInt32(ProjectRequestManager[0]["PrjReTypeId"]) != (int)TSP.DataManager.TSProjectRequestType.InsertProject 
            && Convert.ToInt32(ComboBoxProjectRequestType.SelectedItem.Value) == (int)TSP.DataManager.TSProjectRequestType.InsertProject)
        {
            SetLabelWarning("امکان تغییر نوع درخواست به ''درخواست ثبت اطلاعات پروژه'' وجود ندارد");
            return false;
        }
        if (Convert.ToInt32(ProjectRequestManager[0]["PrjReTypeId"]) == (int)TSP.DataManager.TSProjectRequestType.InsertProject)
            if (!UpdateProject(ProjectManager))
                return false;

        ProjectRequestManager[0].BeginEdit();
        ProjectRequestManager[0]["PrjReTypeId"] = Convert.ToInt32(ComboBoxProjectRequestType.SelectedItem.Value);
        ProjectRequestManager[0]["ArchiveNo"] = txtArchiveNo.Text;
        ProjectRequestManager[0]["ProjectId"] = _ProjectId;
        ProjectRequestManager[0]["RequestDate"] = RegDate.Text;
        ProjectRequestManager[0]["ProjectName"] = ASPxTextBoxProjectName.Text;
        if (ASPxComboBoxUsage.Value != null)
            ProjectRequestManager[0]["UsageId"] = ASPxComboBoxUsage.Value;
        if (ASPxComboBoxStructureGroups.Value != null && ASPxComboBoxStructureGroups.SelectedItem != null)
            ProjectRequestManager[0]["GroupId"] = ASPxComboBoxStructureGroups.Value;
        ProjectRequestManager[0]["ReconstructionCode"] = ASPxTextBoxReconstructionCode.Text;
        ProjectRequestManager[0]["Foundation"] = txtProjectFoundation.Text;
        ProjectRequestManager[0]["MaxStageNum"] = txtStageNum.Text;
        if (ComboBoxStructureSkeleton.Value != null)
            ProjectRequestManager[0]["StructureSkeletonId"] = ComboBoxStructureSkeleton.Value;
        if (ComboBoxRoofType.Value != null)
            ProjectRequestManager[0]["RoofTypeId"] = ComboBoxRoofType.Value;
        else
            ProjectRequestManager[0]["RoofTypeId"] = (int)TSP.DataManager.TSRoofType.Others;

        if (!string.IsNullOrEmpty(txtSazehTarkibiFoundation.Text))
            ProjectRequestManager[0]["FoundationMixSkeleton"] = txtSazehTarkibiFoundation.Text;

        if (!string.IsNullOrEmpty(ASPxTextBoxArea.Text))
            ProjectRequestManager[0]["Area"] = ASPxTextBoxArea.Text;
        else
            ProjectRequestManager[0]["Area"] = DBNull.Value;

        if (!string.IsNullOrEmpty(ASPxTextBoxRecessArea.Text))
            ProjectRequestManager[0]["RecessArea"] = ASPxTextBoxRecessArea.Text;
        else
            ProjectRequestManager[0]["RecessArea"] = DBNull.Value;

        if (!string.IsNullOrEmpty(ASPxTextBoxRemainArea.Text))
            ProjectRequestManager[0]["RemainArea"] = ASPxTextBoxRemainArea.Text;
        else
            ProjectRequestManager[0]["RemainArea"] = DBNull.Value;

        if (!string.IsNullOrEmpty(ASPxTextBoxDocumentArea.Text))
            ProjectRequestManager[0]["DocumentArea"] = ASPxTextBoxDocumentArea.Text;
        else
            ProjectRequestManager[0]["DocumentArea"] = DBNull.Value;

        if (ASPxComboBoxCity.Value != null)
            ProjectRequestManager[0]["CitId"] = ASPxComboBoxCity.Value;
        if (ASPxComboBoxMunicipality.Value != null)
            ProjectRequestManager[0]["MunId"] = ASPxComboBoxMunicipality.Value;
        ProjectRequestManager[0]["Address"] = TextBoxAddress.Text;
        ProjectRequestManager[0]["ComputerCode"] = ASPxTextBoxComputerCode.Text;
        ProjectRequestManager[0]["DiscountPercentId"] = ASPxComboBoxDiscountPercent.Value;
        ProjectRequestManager[0]["AgentId"] = ASPxComboBoxAgent.Value;
        ProjectRequestManager[0]["IsConfirmed"] = 0;
        ProjectRequestManager[0]["InActive"] = 0;
        ProjectRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        ProjectRequestManager[0]["ModifiedDate"] = DateTime.Now;
        ProjectRequestManager[0]["Description"] = txtReqDesc.Text.Trim();
        if (cmbOwnerShipType.Value != null)
            ProjectRequestManager[0]["OwnershipTypeId"] = cmbOwnerShipType.Value;

        if (!string.IsNullOrEmpty(txtRegisteredNo.Text))
            ProjectRequestManager[0]["MainRegisterNo"] = txtRegisteredNo.Text;
        if (CmbType.Value.ToString() == "1")//OtherPerson   
            ProjectRequestManager[0]["OwnerFullName"] = txtOwnerFirstName.Text + " " + txtOwnerLastName.Text;
        else
            ProjectRequestManager[0]["OwnerFullName"] = txtOwnerFirstName.Text;
        if (!string.IsNullOrEmpty(txtMainRegion.Text))
            ProjectRequestManager[0]["MainRegion"] = txtMainRegion.Text;
        if (!string.IsNullOrEmpty(txtMainSection.Text))
            ProjectRequestManager[0]["MainSection"] = txtMainSection.Text;

        if (_TechnicalBookletAttachName != null)
        {
            ProjectRequestManager[0]["FileUrlTechnicalBooklet"] = _TechnicalBookletAttachName;
        }
        ProjectRequestManager[0]["FileNo"] = ASPxTextBoxFileNo.Text;
        ProjectRequestManager[0]["FileDate"] = FileDate.Text;
        ProjectRequestManager[0]["DesignerSaved"] = !chbHasDesigner.Checked;
        ProjectRequestManager[0]["ObserverSaved"] = !chbHasObserver.Checked;        
        #region پروانه ساخت
        if (!string.IsNullOrEmpty(txtBuildingCertificateStartDate.Text))
            ProjectRequestManager[0]["BuildingCertificateStartDate"] = txtBuildingCertificateStartDate.Text;
        else
            ProjectRequestManager[0]["BuildingCertificateStartDate"] = "";
        if (!string.IsNullOrEmpty(txtBuildingCertificateExpirDate.Text))
            ProjectRequestManager[0]["BuildingCertificateExpirDate"] = txtBuildingCertificateExpirDate.Text;
        else
            ProjectRequestManager[0]["BuildingCertificateExpirDate"] = "";
        if (!string.IsNullOrEmpty(txtBuildingCertificateNum.Text))
            ProjectRequestManager[0]["BuildingCertificateNum"] = txtBuildingCertificateNum.Text;
        else
            ProjectRequestManager[0]["BuildingCertificateNum"] = "";
        if (_BuildingCertificateAttachName != null)
        {
            ProjectRequestManager[0]["FileUrlBuildingCertificate"] = _BuildingCertificateAttachName;
        }
        #endregion
        #region پایان کار
        if (!string.IsNullOrEmpty(txtEndProjectStartDate.Text))
            ProjectRequestManager[0]["EndProjectStartDate"] = txtEndProjectStartDate.Text;
        if (!string.IsNullOrEmpty(txtEndProjectNum.Text))
            ProjectRequestManager[0]["EndProjectNum"] = txtEndProjectNum.Text;
        if (_BuildingLicenceAttachNameRnd != null)
        {
            ProjectRequestManager[0]["FileUrlBuildingLicence"] = _BuildingLicenceAttachNameRnd;
        }
        #endregion
        #region شروع نشدن ساخت و ساز
        if (_BuldingCheckAttachName != null)
        {
            ProjectRequestManager[0]["FileURLBuldingCheck"] = _BuldingCheckAttachName;

        }
        if (!string.IsNullOrEmpty(txtBuldingCheckDate.Text))
            ProjectRequestManager[0]["BuldingCheckDate"] = txtBuldingCheckDate.Text;
        #endregion
        ProjectRequestManager[0].EndEdit();
        ProjectRequestManager.Save();

        ProjectRequestManager.DataTable.AcceptChanges();

        return true;
    }

    #endregion

    #region UpdateRegisterNo
    private Boolean UpdateRegisteredNo(TSP.DataManager.TransactionManager transact, int RegisteredNoId)
    {
        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        transact.Add(RegisteredNoManager);

        //if (!CheckIsRegNoExist(txtRegisteredNo.Text, RegisteredNoManager))
        //    return false;

        RegisteredNoManager.FindByRegisteredNoId(RegisteredNoId);

        if (RegisteredNoManager.Count != 1)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        RegisteredNoManager[0].BeginEdit();
        RegisteredNoManager[0]["ProjectId"] = _ProjectId;
        RegisteredNoManager[0]["RegisteredNo"] = _MainRegNo = txtRegisteredNo.Text;
        RegisteredNoManager[0]["InActive"] = 0;
        RegisteredNoManager[0]["PrjReId"] = _PrjReId;
        RegisteredNoManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        RegisteredNoManager[0]["ModifiedDate"] = DateTime.Now;
        RegisteredNoManager[0].EndEdit();
        RegisteredNoManager.Save();
        RegisteredNoManager.DataTable.AcceptChanges();

        return true;
    }
    #endregion

    #region Update Owner
    private Boolean UpdateOwner(TSP.DataManager.TransactionManager trans, int OwnerId)
    {
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OrganizationManager OrganizationManager = new TSP.DataManager.OrganizationManager();

        trans.Add(OwnerManager);
        trans.Add(OtherPersonManager);
        trans.Add(OrganizationManager);

        if (CmbType.Value == null)
        {
            SetLabelWarning("نوع مالک را انتخاب نمایید.");
            return false;
        }

        OwnerManager.FindByOwnerId(OwnerId);

        if (OwnerManager.Count <= 0)
        {
            SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            return false;
        }
        OwnerManager[0].BeginEdit();
        OwnerManager[0]["ProjectId"] = _ProjectId;
        OwnerManager[0]["Type"] = CmbType.Value;
        OwnerManager[0]["IsAgent"] = true;
        OwnerManager[0]["PrjReId"] = _PrjReId;
        if (!Utility.IsDBNullOrNullValue(txtSSN.Text) && CheckOwnerIsMemmberOfNezam(txtSSN.Text) != -1)
            OwnerManager[0]["MeId"] = CheckOwnerIsMemmberOfNezam(txtSSN.Text);
        else
            OwnerManager[0]["MeId"] = DBNull.Value;
        OwnerManager[0]["CreateDate"] = Utility.GetDateOfToday();
        OwnerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        OwnerManager[0]["ModifiedDate"] = DateTime.Now;
        OwnerManager[0].EndEdit();
        OwnerManager.Save();

        if (CmbType.Value.ToString() == "1") //OtherPerson            
            UpdateOtherPerson(OtherPersonManager, Convert.ToInt32(OwnerManager[0]["OtherPersOrgId"].ToString()));
        else // Organization
            UpdateOrganization(OrganizationManager, Convert.ToInt32(OwnerManager[0]["OtherPersOrgId"].ToString()));

        return true;
    }

    private void UpdateOtherPerson(TSP.DataManager.OtherPersonManager OtherPersonManager, int OtherPersOrgId)
    {
        OtherPersonManager.FindByCode(OtherPersOrgId);
        if (OtherPersonManager.Count == 1)
        {
            OtherPersonManager[0].BeginEdit();

            OtherPersonManager[0]["FirstName"] = txtOwnerFirstName.Text;
            OtherPersonManager[0]["LastName"] = txtOwnerLastName.Text;
            OtherPersonManager[0]["SSN"] = txtSSN.Text;
            OtherPersonManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            OtherPersonManager[0]["MobileNo"] = txtOwnerMobileNo.Text;
            OtherPersonManager[0]["ModifiedDate"] = DateTime.Now;
            OtherPersonManager[0].EndEdit();
            OtherPersonManager.Save();
            OtherPersonManager.DataTable.AcceptChanges();
        }
    }

    private void UpdateOrganization(TSP.DataManager.OrganizationManager OrganizationManager, int OtherPersOrgId)
    {
        OrganizationManager.FindByCodeForTS(OtherPersOrgId);
        if (OrganizationManager.Count == 1)
        {
            OrganizationManager[0].BeginEdit();
            OrganizationManager[0]["OrgName"] = txtOwnerFirstName.Text;
            OrganizationManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            OrganizationManager[0]["UserId"] = txtOwnerMobileNo.Text;
            OrganizationManager[0]["ModifiedDate"] = DateTime.Now;
            OrganizationManager[0].EndEdit();

            OrganizationManager.Save();
            OrganizationManager.DataTable.AcceptChanges();
        }
    }


    #endregion

    #endregion

    #region Delete
    private Boolean DeleteOwner(TSP.DataManager.TransactionManager transact, int OwnerId)
    {
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OrganizationManager OrganizationManager = new TSP.DataManager.OrganizationManager();

        transact.Add(OwnerManager);
        transact.Add(OtherPersonManager);
        transact.Add(OrganizationManager);

        OwnerManager.FindByOwnerId(OwnerId);
        if (OwnerManager.Count != 1)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }

        int OtherPersOrgId = Convert.ToInt32(OwnerManager[0]["OtherPersOrgId"]);
        int Type = Convert.ToInt32(OwnerManager[0]["Type"]);
        OwnerManager[0].Delete();
        OwnerManager.Save();
        OwnerManager.DataTable.AcceptChanges();
        if (Type == 1) //OtherPerson 
        {
            OtherPersonManager.FindByCode(OtherPersOrgId);
            if (OtherPersonManager.Count != 1)
            {
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return false;
            }
            OtherPersonManager[0].Delete();
            OtherPersonManager.Save();
            OtherPersonManager.DataTable.AcceptChanges();
        }
        else
        {
            OrganizationManager.FindByCodeForTS(OtherPersOrgId);
            if (OrganizationManager.Count != 1)
            {
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return false;
            }
            OrganizationManager[0].Delete();
            OrganizationManager.Save();
            OrganizationManager.DataTable.AcceptChanges();
        }

        return true;
    }

    private Boolean DeleteRegisterNo(TSP.DataManager.TransactionManager transact, int RegisteredNoId)
    {
        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        TSP.DataManager.TechnicalServices.DirectionsManager DirectionsManager = new TSP.DataManager.TechnicalServices.DirectionsManager();
        transact.Add(RegisteredNoManager);
        transact.Add(DirectionsManager);

        DirectionsManager.FindByRegisteredNoId(Convert.ToInt32(RegisteredNoId));
        int cnt = DirectionsManager.Count;
        for (int i = 0; i < cnt; i++)
        {
            DirectionsManager[0].Delete();
            DirectionsManager.Save();
            DirectionsManager.DataTable.AcceptChanges();
        }
        RegisteredNoManager.FindByRegisteredNoId(RegisteredNoId);
        if (RegisteredNoManager.Count != 1)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        RegisteredNoManager[0].Delete();
        RegisteredNoManager.Save();
        RegisteredNoManager.DataTable.AcceptChanges();
        return true;
    }

    private Boolean DeleteBlock(TSP.DataManager.TransactionManager transact, int BlockId)
    {
        if (!TSP.DataManager.TechnicalServices.BlockManager.DeleteBlock(BlockId, transact, false))
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        return true;
    }
    #endregion
    private int GetTaskId(int TaskCode)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count == 1)
            return Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
        else
            return -1;
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
            SetLabelWarning("اطلاعات شما به عنوان انجام دهنده عملیات انتخاب شده در جریان کار ثبت نشده است.");
            return (-1);
        }
    }

    private void SetError(Exception err, char Flag)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                if (Flag == 'D')
                {
                    SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
                }
                else
                {
                    SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
                }
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    #region  File Upload
    /// <summary>
    /// دفترچه فنی ملکی
    /// </summary>
    /// <param name="uploadedFile"></param>
    /// <returns></returns>
    protected string SaveTechnicalBookletFile(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);

                ret = "_PrjReId_" + _PrjReId.ToString() + "_ProjectId_" + _ProjectId.ToString() + "_UserId_" + Utility.GetCurrentUser_UserId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/TechnicalServices/TechnicalBooklet/") + ret) == true);
            string tempFileName = "~/Image/TechnicalServices/TechnicalBooklet/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _TechnicalBookletAttachName = tempFileName;
        }
        return ret;
    }
    /// <summary>
    /// پایان کار
    /// </summary>
    /// <param name="uploadedFile"></param>
    /// <returns></returns>
    protected string SaveImageBuildingLicence(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "_PrjReId_" + _PrjReId.ToString() + "_ProjectId_" + _ProjectId.ToString() + "_UserId_" + Utility.GetCurrentUser_UserId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/TechnicalServices/BuildingLicence/") + ret) == true);
            string tempFileName = "~/Image/TechnicalServices/BuildingLicence/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _BuildingLicenceAttachNameRnd = tempFileName;
        }
        return ret;
    }
    /// <summary>
    /// پروانه ساخت
    /// </summary>
    /// <param name="uploadedFile"></param>
    /// <returns></returns>
    protected string SaveImageBuildingCertificate(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "_PrjReId_" + _PrjReId.ToString() + "_ProjectId_" + _ProjectId.ToString() + "_UserId_" + Utility.GetCurrentUser_UserId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/TechnicalServices/BuildingCertificate/") + ret) == true);
            string tempFileName = "~/Image/TechnicalServices/BuildingCertificate/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _BuildingCertificateAttachName = tempFileName;
        }
        return ret;
    }

    /// <summary>
    /// شروع نشدن ساخت و ساز
    /// </summary>
    /// <param name="uploadedFile"></param>
    /// <returns></returns>
    protected string SaveImageBuldingCheck(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "_PrjReId_" + _PrjReId.ToString() + "_ProjectId_" + _ProjectId.ToString() + "_UserId_" + Utility.GetCurrentUser_UserId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/TechnicalServices/BuldingCheck/") + ret) == true);
            string tempFileName = "~/Image/TechnicalServices/BuldingCheck/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            _BuldingCheckAttachName = tempFileName;
        }
        return ret;
    }
    #endregion

    private void SetDiscountItems()
    {
        TSP.DataManager.TechnicalServices.DiscountPercentManager DiscountPercentManager = new TSP.DataManager.TechnicalServices.DiscountPercentManager();
        DiscountPercentManager.FindByDiscountPercentId(Convert.ToInt32(ASPxComboBoxDiscountPercent.Value));

        if (DiscountPercentManager.Count > 0)
        {
            ASPxTextBoxDecrementPercent.Text = DiscountPercentManager[0]["DecrementPercent"].ToString();
            ASPxTextBoxWagePercent.Text = DiscountPercentManager[0]["WagePercent"].ToString();
        }
    }

    private void CheckDiscountPercent()
    {
        TSP.DataManager.TechnicalServices.DiscountPercentManager DiscountPercentManager = new TSP.DataManager.TechnicalServices.DiscountPercentManager();
        DiscountPercentManager.FindByDiscountPercentId(Convert.ToInt32(ASPxComboBoxDiscountPercent.Value));
        if (DiscountPercentManager.Count > 0 && Convert.ToBoolean(DiscountPercentManager[0]["InActive"]))
        {
            SetLabelWarning("نوع پروِژه انتخاب شده غیر فعال می باشد. لطفا دوباره انتخاب کنید.");
            ASPxComboBoxDiscountPercent.SelectedIndex = ASPxComboBoxDiscountPercent.Items.IndexOfValue((int)TSP.DataManager.TSDiscountPercent.Usual);
        }
    }

    #region Set Menu
    private void SetProjectMainMenuEnabled()
    {
        if (_ProjectId == -1)
            _ProjectId = -2;
        if (_PageMode != "Edit" && _PageMode != "View")
        {
            MainMenu.Items.FindByName("Project").Selected = true;
            MainMenu.Enabled = false;
        }
        else
        {
            PrjMainMenu PrjMainMenu = new PrjMainMenu("Project", _ProjectId);
            MainMenu.Items.FindByName("Project").Selected = true;
            MainMenu.Enabled = true;
        }
    }


    private void SetProjectMenuEnabled()
    {
        if (_ProjectId == -1)
            _ProjectId = -2;

        if (_PageMode != "Edit" && _PageMode != "View")
        {
            ProjectMenu.Enabled = false;
        }
        else
        {
            ProjectMenu.Items.FindByName("BaseInfo").Selected = true; //BaseInfo
            ProjectMenu.Enabled = true;
        }
    }

    private void CheckMenueViewPermission()
    {
        PrjMenu.ProjectMenusViewPermission PrjMenuPer = PrjMenu.CheckProjectMenusViewPermission();
        ProjectMenu.Items.FindByName("Insurance").Visible = PrjMenuPer.CanViewInsurance;
        ProjectMenu.Items.FindByName("Block").Visible = PrjMenuPer.CanViewBlock;
        ProjectMenu.Items.FindByName("PlansMethod").Visible = PrjMenuPer.CanViewPlansMethod;
        ProjectMenu.Items.FindByName("RegisteredNo").Visible = PrjMenuPer.CanViewRegisteredNo;
        ProjectMenu.Items.FindByName("BaseInfo").Visible = PrjMenuPer.CanViewBaseInfo;

        PrjMainMenu.ProjectMainMenusViewPermission PrjMainMenuPer = PrjMainMenu.CheckProjectMenusViewPermission();
        //MainMenu.Items.FindByName("StatusAnnouncement").Visible = PrjMainMenuPer.CanViewStatusAnnouncement;
        //MainMenu.Items.FindByName("BuildingsLicense").Visible = PrjMainMenuPer.CanViewBuildingsLicense;
        //MainMenu.Items.FindByName("Timing").Visible = PrjMainMenuPer.CanViewTiming;
        MainMenu.Items.FindByName("Contract").Visible = PrjMainMenuPer.CanViewContract;
        MainMenu.Items.FindByName("Implementer").Visible = PrjMainMenuPer.CanViewImplementer;
        MainMenu.Items.FindByName("Observers").Visible = PrjMainMenuPer.CanViewObservers;
        MainMenu.Items.FindByName("Plans").Visible = PrjMainMenuPer.CanViewPlans;
        MainMenu.Items.FindByName("Owner").Visible = PrjMainMenuPer.CanViewOwner;
        MainMenu.Items.FindByName("Project").Visible = PrjMainMenuPer.CanViewProject;
        MainMenu.Items.FindByName("Accounting").Visible = PrjMainMenuPer.CanViewTSAccounting;
        MainMenu.Items.FindByName("Designer").Visible = PrjMainMenuPer.CanViewDesigner;
    }
    #endregion      

    #region WorkFlow Methods
    private void CheckWorkFlowPermissionForProject()
    {
        CheckWorkFlowPermissionForSave(_PageMode);
        if (_PageMode == "Edit" || _PageMode == "View")
            CheckWorkFlowPermissionForEdit(_PageMode);
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew((int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest), Utility.GetCurrentUser_UserId(), PageMode);
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;
        btnNew.Enabled = WFPer.BtnNew;
        btnNew2.Enabled = WFPer.BtnNew;

        if (PageMode == "New")
        {
            btnSave.Enabled = WFPer.BtnSave;
            btnSave2.Enabled = WFPer.BtnSave;
        }
        else if (PageMode != "Edit" && _PageMode != "View")
        {
            btnSave.Enabled = btnSave2.Enabled = WFPer.BtnNewRequest;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit((int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo, (int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReId, Utility.GetCurrentUser_UserId(), PageMode);
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    private void InsertWorkFlowStateLog(string Desc, TSP.DataManager.WorkFlowStateType WorkFlowStateType)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReId, (int)TSP.DataManager.TableType.TSProject, Desc, Utility.GetCurrentUser_UserId(), WorkFlowStateType);
    }
    #endregion

    private void CheckPriceArchive()
    {
        TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        PriceArchiveManager.FindByYear(Utility.GetYearOfToday());
        if (PriceArchiveManager.Count <= 0)
        {
            lblWarningText.Visible = true;
            ImgWarningMsg.ClientVisible = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningMsg');</script>");
            lblWarningText.Text = "در سال جاری تعرفه خدمات مهندسی تعریف نشده است و کلیه محاسبات بر اساس آخرین تعرفه سال گذشته انجام خواهد شد";
        }
    }

    private void SetGroupStructure()
    {
        string Step = "-1";
        string Fundation = "-1";
        if (!string.IsNullOrWhiteSpace(txtStageNum.Text) && txtStageNum.Text != "0")
            Step = txtStageNum.Text;
        if (!string.IsNullOrWhiteSpace(txtProjectFoundation.Text))
            Fundation = txtProjectFoundation.Text;
        ObjectdatasourceStructureGroups.SelectParameters["Step"].DefaultValue = Step;
        ObjectdatasourceStructureGroups.SelectParameters["Fundation"].DefaultValue = Fundation;
        ASPxComboBoxStructureGroups.DataBind();
    }

    private Boolean CheckIsMainvalueNull()
    {
        if (string.IsNullOrEmpty(_PageMode) || Utility.IsDBNullOrNullValue(_ProjectId) || Utility.IsDBNullOrNullValue(_PrjReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return true;
        }
        return false;
    }

    private int CheckOwnerIsMemmberOfNezam(string SSN)
    {
        int MeId = -1;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dtMe = MemberManager.SelectActiveMembers(MeId, SSN);
        if (dtMe.Rows.Count > 0)
        {
            MeId = Convert.ToInt32(dtMe.Rows[0]["MeId"].ToString());
        }
        return MeId;
    }

    #endregion   
}