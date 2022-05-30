using DevExpress.Web;
using System;
using System.Data;
using System.IO;
using WorkRequestsrvEngineerToOthersTest;

public partial class UserControl_WorkRequestInsertInfoUserControl : System.Web.UI.UserControl
{
    #region Properties
    public string _PageMode
    {
        get
        {
            return HiddenFieldPage["PgMd"].ToString();
        }
        set
        {
            HiddenFieldPage["PgMd"] = value.ToString();
        }
    }
    public string _RoundPanelPageHeader
    {
        get
        {
            return HiddenFieldPage["RoundPanelPageHeader"].ToString();
        }
        set
        {
            HiddenFieldPage["RoundPanelPageHeader"] = value.ToString();
        }
    }

    public int _ObsWorkReqChangeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ObsWChangId"]);
        }
        set
        {
            HiddenFieldPage["ObsWChangId"] = value.ToString();
        }
    }
    int _MfId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["MfId"]);
        }
        set
        {
            HiddenFieldPage["MfId"] = value.ToString();
        }
    }
    int _CurrentCapacityAssignmentId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["CurrentCapacityAssignmentId"]);
        }
        set
        {
            HiddenFieldPage["CurrentCapacityAssignmentId"] = value.ToString();
        }
    }
    string _CurrentCapacityEndate
    {
        get
        {
            return HiddenFieldPage["CurrentCapacityEndate"].ToString();
        }
        set
        {
            HiddenFieldPage["CurrentCapacityEndate"] = value.ToString();
        }
    }

    string _CurrentCapacityYear
    {
        get
        {
            return HiddenFieldPage["CurrentCapacityYear"].ToString();
        }
        set
        {
            HiddenFieldPage["CurrentCapacityYear"] = value.ToString();
        }
    }

    int _StopmandatoryFileUploading
    {
        get
        {
            return Convert.ToInt16(HiddenFieldPage["StopmandatoryFileUploading"]);
        }
        set
        {
            HiddenFieldPage["StopmandatoryFileUploading"] = value;
        }
    }

    int _CurrentObsId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["CurrentObsId"]);
        }
        set
        {
            HiddenFieldPage["CurrentObsId"] = value.ToString();
        }
    }
    int _CurrentDesId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["CurrentDesId"]);
        }
        set
        {
            HiddenFieldPage["CurrentDesId"] = value.ToString();
        }
    }
    Int16 _CurrentDesIdInOfficeEngOff
    {
        get
        {
            return Convert.ToInt16(HiddenFieldPage["CurrentDesIdInOfficeEngOff"]);
        }
        set
        {
            HiddenFieldPage["CurrentDesIdInOfficeEngOff"] = value.ToString();
        }
    }
    int _CurrentUrbId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["CurrentUrbId"]);
        }
        set
        {
            HiddenFieldPage["CurrentUrbId"] = value.ToString();
        }
    }
    int _MfMjParentId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["MfMjParentId"]);
        }
        set
        {
            HiddenFieldPage["MfMjParentId"] = value.ToString();
        }
    }
    int _MaxObsJobCapacity
    {
        get
        {

            return Convert.ToInt32(HiddenFieldPage["MaxObsJobCapacity"]);

        }
        set
        {
            HiddenFieldPage["MaxObsJobCapacity"] = value.ToString();
        }
    }
    int _MaxJobCount
    {
        get
        {

            return Convert.ToInt32(HiddenFieldPage["MaxJobCount"]);

        }
        set
        {
            HiddenFieldPage["MaxJobCount"] = value.ToString();
        }
    }
    string _DocMeFileExpireDate
    {
        get
        {
            return HiddenFieldPage["DocMeFileExpireDate"].ToString();
        }
        set
        {
            HiddenFieldPage["DocMeFileExpireDate"] = value.ToString();
        }
    }
    int _AgentId
    {
        get
        {

            return Convert.ToInt32(HiddenFieldPage["AgentId"]);

        }
        set
        {
            HiddenFieldPage["AgentId"] = value.ToString();
        }
    }
    int _AgentIdMain
    {
        get
        {

            return Convert.ToInt32(HiddenFieldPage["AgentIdMain"]);

        }
        set
        {
            HiddenFieldPage["AgentIdMain"] = value.ToString();
        }
    }
    string _FullFilePath
    {
        get
        {
            try { return HiddenFieldPage["FullFilePath"].ToString(); }
            catch
            {
                return null;
            }

        }
        set
        {
            HiddenFieldPage["FullFilePath"] = value;
        }
    }
    string _FullFilePathObsCommitmentForm
    {
        get
        {
            try { return HiddenFieldPage["FullFilePathObsCommitmentForm"].ToString(); }
            catch
            {
                return null;
            }

        }
        set
        {
            HiddenFieldPage["FullFilePathObsCommitmentForm"] = value;
        }
    }

    #region Engoffice & Office Properties
    Boolean _HasEngOffice
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["HasEngOffice"]);
        }
        set
        {
            HiddenFieldPage["HasEngOffice"] = value;
        }
    }
    Boolean _HasEfficientGrade
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["HasEfficientGrade"]);
        }
        set
        {
            HiddenFieldPage["HasEfficientGrade"] = value;
        }
    }

    Boolean _IsEngOfficeIsExpired
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["IsEngOfficeIsExpired"]);
        }
        set
        {
            HiddenFieldPage["IsEngOfficeIsExpired"] = value;
        }
    }
    int _EngOfId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["EngOfId"]);
        }
        set
        {
            HiddenFieldPage["EngOfId"] = value.ToString();
        }
    }
    Boolean _HasOffice
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["HasOffice"]);
        }
        set
        {
            HiddenFieldPage["HasOffice"] = value;
        }
    }
    Boolean _IsOfficeIsExpired
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["IsOfficeIsExpired"]);
        }
        set
        {
            HiddenFieldPage["IsOfficeIsExpired"] = value;
        }
    }
    int _OfId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["OfId"]);
        }
        set
        {
            HiddenFieldPage["OfId"] = value.ToString();
        }
    }
    Boolean _HasError
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["HasError"]);
        }
        set
        {
            HiddenFieldPage["HasError"] = value;
        }
    }
    Boolean _NeedTaahod
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["NeedTaahod"]);
        }
        set
        {
            HiddenFieldPage["NeedTaahod"] = value;
        }
    }
    CapacityCalculations.MemberCapacity _MemberCapacity
    {
        get
        {
            return (CapacityCalculations.MemberCapacity)(Session["MemberCapacity"]);
        }
        set
        {
            Session["MemberCapacity"] = value;
        }
    }
    int _MeId
    {
        get
        {

            try { return Convert.ToInt32(HiddenFieldPage["MeId"]); }
            catch { return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"])); }

        }
        set
        {
            HiddenFieldPage["MeId"] = value.ToString();
        }
    }

    #endregion


    public Boolean _ErrorMessageVisible
    {
        get
        {
            return Utility.IsDBNullOrNullValue(HiddenFieldPage["WarningVisible"]) ? false : Convert.ToBoolean(HiddenFieldPage["WarningVisible"]);
        }
        set
        {
            HiddenFieldPage["WarningVisible"] = value;
        }
    }
    public string _ErrorMessage
    {
        get
        {
            return HiddenFieldPage["ErrorMessage"].ToString();
        }
        set
        {
            HiddenFieldPage["ErrorMessage"] = value.ToString();
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
            {
                CheckBoxWantShahrakSanati.Enabled = false;
            }
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
            {
                TSP.DataManager.Permission perShahrakSanati = TSP.DataManager.TechnicalServices.ObserverWorkRequestManager.GetUserPermissionSetShahraksanati(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                CheckBoxWantShahrakSanati.Enabled = perShahrakSanati.CanNew || perShahrakSanati.CanEdit;
            }

        }

        lblMaxJobObsCapacity.Text = _MaxObsJobCapacity.ToString();
        lblMaxJobCount.Text = _MaxJobCount.ToString();
    }
    public Boolean btnSave_Click(string PageMode, Boolean SendInfoToShahrdari)
    {
        Boolean ReturnValue = false;
        switch (PageMode)
        {
            case "New":
                ReturnValue = Insert(SendInfoToShahrdari);
                break;
            case "Change":
                ReturnValue = InsertNewRequest(TSP.DataManager.TSObserverWorkRequestChangeType.Change, SendInfoToShahrdari);
                break;
            case "Off":
                ReturnValue = InsertNewRequest(TSP.DataManager.TSObserverWorkRequestChangeType.Off, SendInfoToShahrdari);
                break;
        }
        //SetLableVisibityBasedOnCity1();
        return ReturnValue;
    }
    protected void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearForm();
        if (cmbMajor.SelectedItem.Value == null)
            return;
        txtMemberFileMajor.Text = cmbMajor.SelectedItem.Text;
        FillGradeAndCapacityInfo(Convert.ToInt32(cmbMajor.SelectedItem.Value));
        //؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟
        ////if (_MemberCapacity.CurrentObsId != -2 && _MemberCapacity.CurrentDesId != -2)
        ////{
        ////    if (_PageMode == "New")
        ////        PanelMain.ClientVisible = false;
        ////}
        /////؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟؟
    }
    protected void flpAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void flpObsCommitmentForm_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageObsCommitmentForm(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void comboCity1_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (comboCity2.SelectedItem != null && Convert.ToInt32(comboCity2.SelectedItem.Value) == Convert.ToInt32(comboCity1.SelectedItem.Value))
            {
                ShowMessage("شهر انتخابی اول و دوم نمی توانند یکسان باشند");
                comboCity1.SelectedIndex = 0;
                return;
            }
            if (_AgentId != Utility.GetCurrentAgentCode())
                return;
            #region Reset
            ObjectDataSourceCity2.SelectParameters["CitId"].DefaultValue = "-1";
            ObjectDataSourceCity2.SelectParameters["ShowInTsWorkRequest"].DefaultValue = "-1";
            ObjectDataSourceCity2.SelectParameters["CitId"].DefaultValue = "-1";
            ObjectDataSourceCity2.SelectParameters["CitIdExeption"].DefaultValue = "-1";
            ObjectDataSourceCity2.SelectParameters["AgentId"].DefaultValue = _AgentId.ToString();
            ObjectDataSourceCity2.SelectParameters["CitIdList"].DefaultValue = "-2";

            ////////////////نظارت شهرداری شهرداری
            lblComperChangesObsShirazMunicipality.ClientVisible = txtObsShirazMunicipality.ClientVisible = lblObsShirazMunicipality.ClientVisible = lblObsShirazMunicipalityLimitation.ClientVisible = false;
            ////         
            CheckBoxWantObserverSelect.Checked = false;
            checkboxRulls.Checked = false;
            lblSadraDescription.ClientVisible = false;
            lblCity2.ClientVisible = comboCity2.ClientVisible = false;

            #region SetTextBoxObserverZero
            txtObsShirazMunicipality.Text = "0";
            /////////////??????????????????
            //////SetComperObslbl();
            //////SetVisibleUploadControl();
            #endregion
            #endregion
            if (comboCity1.SelectedItem.Value == null) { return; }
            SetLableVisibityBasedOnCity1();


        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    protected void comboCity2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (comboCity2.SelectedItem == null && comboCity1.SelectedItem == null)
            {
                RoundPanelRules.ClientVisible = false;
                CheckBoxWantObserverSelect.Checked = false;
                checkboxRulls.Checked = false;
            }
            if (comboCity2.SelectedItem.Value == null && Convert.ToInt32(comboCity1.SelectedItem.Value) == (int)TSP.DataManager.CityCode.Shiraz)
            {
                RoundPanelRules.ClientVisible = false;
                CheckBoxWantObserverSelect.Checked = false;
                checkboxRulls.Checked = false;
                return;
            }
            if (comboCity2.SelectedItem.Value == comboCity1.SelectedItem.Value)
            {
                ShowMessage("شهر انتخابی اول و دوم نمی توانند یکسان باشند");
                comboCity2.SelectedIndex = 0;
                return;
            }
            SetLableVisibityBasedOnCity2();
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #region Methods
    #region Set Key-Mode
    public void SetKeys(string PageMode, int ObsWorkReqChangeId, int MeId)
    {
        _PageMode = PageMode;
        _ObsWorkReqChangeId = ObsWorkReqChangeId;
        _MeId = MeId;
        SetMode(_PageMode);
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
            case "New":
                SetNewModeKeys();
                break;
            case "View":
                SetViewModeKeys();
                break;

            case "Off":
                SetRequestModeKeys(PageMode);
                break;
            case "Change":
                SetRequestModeKeys(PageMode);
                break;
        }
    }
    private void SetNewModeKeys()
    {
        _RoundPanelPageHeader = "ثبت آماده به کاری جدید";
        SetControlsEnabled(true);
        SetComperVisible(false);
        ClearFormBasicInfo();
        ClearForm();
        SetRoundPanelVisible(false, false);
        if (_MeId > 0)
        {
            LoadMemberInfoForNewMode(_MeId);
        }
    }
    private void SetViewModeKeys()
    {

        FillFormBasedOnObsRequestChange(_ObsWorkReqChangeId);
        _RoundPanelPageHeader = "مشاهده آماده به کاری";
        RoundPanelMajor.ClientVisible = false;
        TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateManager = new TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager();
        WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming, _ObsWorkReqChangeId, (int)TSP.DataManager.TableType.TSWorkRequest, "مشاهده درخواست آماده بکاری", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.ViewInfo);

        SetControlsEnabled(false);
    }
    private void SetRequestModeKeys(string PageMode)
    {

        _RoundPanelPageHeader = "ثبت درخواست جدید-";
        RoundPanelMajor.ClientVisible = false;
        RoundPanelPage.ClientEnabled = true;
        SetControlsEnabled(true);
        FillFormBasedOnObsRequest();
        switch (PageMode)
        {
            case "Off":
                _RoundPanelPageHeader += "درخواست مرخصی";
                SetControlsEnabled(false);
                RoundPanelOffRequest.Visible =
                RoundPanelOffRequest.Enabled = true;
                RoundPanelCommitMuniciToll.ClientVisible = false;
                break;

            case "Change":
                _RoundPanelPageHeader += "درخواست تغییرات";
                RoundPanelOffRequest.Visible =
                RoundPanelCommitMuniciToll.ClientVisible = false;
                break;
        }
    }
    #endregion

    #region Set Visible & Enabled
    private void SetRoundPanelVisible(Boolean MajorPanelVisible, Boolean OtherPanelVisible)
    {
        RoundPanelMajor.Visible = MajorPanelVisible;
        RoundPanelMeInfo.ClientVisible =
        RoundPanelMemberEngOfficeInfo.ClientVisible =
        RoundPanelCity.ClientVisible =
        RoundPanelPrjTypes.ClientVisible =
        RoundPanelBasicCapacityInfo.ClientVisible =
        RoundPanelObserveCapacity.ClientVisible =
        RoundPanelDesignCapacity.ClientVisible = RounPanelUrbenismCapacity.ClientVisible = OtherPanelVisible;
    }
    private void SetLabaleVisibilityBasedonAgent()
    {//*** رشته نقشه برداری در صورتی که شیراز باشد "زیربنا نظارت شهرداری شیراز" به آن نمایش داده می شود و اگر نه هیچ فیلد اطلاعاتی را پر نمی کند
     //**شهرهای حومه برای کل استان ابتدا نمایش داده نمی شود.اگر نمایندگی شیراز بود در صورتی که شه متناسب انتخاب شود شهر مربوطه نمایش داده می شود
     //??????lblComperChangesObsShirazMunicipality.ClientVisible = false;
        ObjectDataSourceCity1.SelectParameters["AgentId"].DefaultValue = _AgentId.ToString();
        ObjectDataSourceCity1.SelectParameters["ShowInTsWorkRequest"].DefaultValue = "1";
        ObjectDataSourceCity1.SelectParameters["CitId"].DefaultValue = "-1";
        ObjectDataSourceCity1.SelectParameters["CitIdExeption"].DefaultValue = "-1";
        ObjectDataSourceCity1.SelectParameters["CitIdList"].DefaultValue = "";
        ObjectDataSourceCity1.SelectParameters["CitIdExeptionList"].DefaultValue = ((int)(TSP.DataManager.CityCode.KhanZenyan)).ToString() + "," + ((int)(TSP.DataManager.CityCode.Dareyon)).ToString() + "," + ((int)(TSP.DataManager.CityCode.Lapooy)).ToString() + "," + ((int)(TSP.DataManager.CityCode.SoltanShahr)).ToString();
        comboCity1.DataBind();
        comboCity1.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ObjectDataSourceCity2.SelectParameters["AgentId"].DefaultValue = _AgentId.ToString();
        ObjectDataSourceCity2.SelectParameters["ShowInTsWorkRequest"].DefaultValue = "-1";
        ObjectDataSourceCity2.SelectParameters["CitId"].DefaultValue = "-1";
        ObjectDataSourceCity2.SelectParameters["CitIdExeption"].DefaultValue = "-1";
        ObjectDataSourceCity2.SelectParameters["CitIdList"].DefaultValue = "";
        comboCity2.DataBind();
        comboCity2.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        if (_AgentId == Utility.GetCurrentAgentCode())//نمایندگی شیراز
        {
            SetComperVisible(true);
            //lblObsShirazMunicipality.ClientVisible = txtObsShirazMunicipality.ClientVisible = lblObsShirazMunicipalityLimitation.ClientVisible = true;
            //lblDesignShirazMunicipality.ClientVisible = txtDesignShirazMunicipality.ClientVisible = lblDesignShirazMunicipalityLimitation.ClientVisible = true;
        }
        else/////سایر استان
        {
            SetComperVisible(false);
            SetComperBonyadVisible(true);
            ////***بابت کارهای نظارتی که بعد از انتقالی عضو به شهر دیگر باید در سیستم ثبت شود دسترسی برای پرتال کارمندان داده می شود
            Boolean CanSetShirazMunicipalityObs = false;
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
            {
                TSP.DataManager.Permission perSetShirazMunicipalityObs = TSP.DataManager.TechnicalServices.ObserverWorkRequestManager.GetUserPermissionSetShirazObsMunicipalityMeterForOtherAgents(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                CanSetShirazMunicipalityObs = perSetShirazMunicipalityObs.CanNew || perSetShirazMunicipalityObs.CanEdit;
            }
            lblObsShirazMunicipality.ClientVisible = txtObsShirazMunicipality.ClientVisible = lblObsShirazMunicipalityLimitation.ClientVisible = CanSetShirazMunicipalityObs;
            //***طراحی استانی است اما از طریق پرتال کارمندان ثبت می شود
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
            {
                lblDesignShirazMunicipality.ClientVisible = txtDesignShirazMunicipality.ClientVisible = lblDesignShirazMunicipalityLimitation.ClientVisible = true;
            }
            else
            {
                lblDesignShirazMunicipality.ClientVisible = txtDesignShirazMunicipality.ClientVisible = lblDesignShirazMunicipalityLimitation.ClientVisible = false;
            }
        }
    }
    private void SetLableVisibityBasedOnCity1()
    {
        RoundPanelRules.ClientVisible = true;

        //***برای تعیین نظارت شهرداری شیراز یا باید شهر اول شیراز باشد و یا اینکه بابت کارهای نظارتی که بعد از انتقالی عضو به شهر دیگر باید در سیستم ثبت شود دسترسی برای پرتال کارمندان داده می شود
        Boolean CanSetShirazMunicipalityObs = false;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
        {
            TSP.DataManager.Permission perSetShirazMunicipalityObs = TSP.DataManager.TechnicalServices.ObserverWorkRequestManager.GetUserPermissionSetShirazObsMunicipalityMeterForOtherAgents(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            CanSetShirazMunicipalityObs = perSetShirazMunicipalityObs.CanNew || perSetShirazMunicipalityObs.CanEdit;
        }
        switch (Convert.ToInt32(comboCity1.SelectedItem.Value))
        {
            case (int)TSP.DataManager.CityCode.Beyza:
                #region بیضا   
                lblCity2.ClientVisible = comboCity2.ClientVisible = false;
                RoundPanelObserveCapacity.ClientVisible = true;
                SetComperVisible(false);
                #endregion
                break;
            case (int)TSP.DataManager.CityCode.Sadra:
                #region Sadra
                lblCity2.ClientVisible = comboCity2.ClientVisible = false;
                RoundPanelObserveCapacity.ClientVisible = true;
                #endregion
                break;
            case (int)TSP.DataManager.CityCode.Kharameh:
                #region خرامه               
                RoundPanelObserveCapacity.ClientVisible = true;
                SetComperVisible(false);
                ObjectDataSourceCity2.SelectParameters["CitIdList"].DefaultValue = ((int)(TSP.DataManager.CityCode.SoltanShahr)).ToString();
                comboCity2.DataBind();
                comboCity2.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
                comboCity2.SelectedIndex = 0;
                lblCity2.ClientVisible = comboCity2.ClientVisible = true;
                #endregion
                break;
            case (int)TSP.DataManager.CityCode.Zarghan:
                #region زرقان
                RoundPanelObserveCapacity.ClientVisible = true;
                SetComperVisible(false);
                ObjectDataSourceCity2.SelectParameters["CitIdList"].DefaultValue = ((int)(TSP.DataManager.CityCode.Lapooy)).ToString();
                comboCity2.DataBind();
                comboCity2.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
                comboCity2.SelectedIndex = 0;
                lblCity2.ClientVisible = comboCity2.ClientVisible = true;
                #endregion
                break;
            case (int)TSP.DataManager.CityCode.Shiraz:
                #region Shiraz
                ObjectDataSourceCity2.SelectParameters["AgentId"].DefaultValue = "-1";
                ObjectDataSourceCity2.SelectParameters["CitIdList"].DefaultValue = ((int)(TSP.DataManager.CityCode.KhanZenyan)).ToString() + "," + ((int)(TSP.DataManager.CityCode.Dareyon)).ToString();
                comboCity2.DataBind();
                comboCity2.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
                comboCity2.SelectedIndex = 0;
                lblCity2.ClientVisible = comboCity2.ClientVisible = true;
                //**نظارت شهرداری شیراز
                CanSetShirazMunicipalityObs = true;
                lblComperChangesObsShirazMunicipality.ClientVisible = (_PageMode == "New" ? false : true);
                ////////////////////////
                RoundPanelRules.ClientVisible = false;
                CheckBoxWantObserverSelect.Checked = false;
                checkboxRulls.Checked = false;
                ///////////////////////
                #endregion
                break;
            default:
                if (_AgentId == Utility.GetCurrentAgentCode())
                {
                    ObjectDataSourceCity2.SelectParameters["CitIdList"].DefaultValue = "-2";
                    comboCity2.DataBind();
                    comboCity2.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
                    comboCity2.SelectedIndex = 0;
                }
                break;

        }
        //***برای تعیین نظارت شهرداری شیراز یا باید شهر اول شیراز باشد و یا اینکه بابت کارهای نظارتی که بعد از انتقالی عضو به شهر دیگر باید در سیستم ثبت شود دسترسی برای پرتال کارمندان داده می شود
        lblObsShirazMunicipality.ClientVisible = txtObsShirazMunicipality.ClientVisible = lblObsShirazMunicipalityLimitation.ClientVisible = CanSetShirazMunicipalityObs;
        if (_MfMjParentId == (int)TSP.DataManager.MainMajors.Mapping)
        {
            RoundPanelObserveCapacity.ClientVisible = false;
        }
        if (Convert.ToInt32(comboCity1.SelectedItem.Value) == (int)TSP.DataManager.CityCode.Shiraz || Convert.ToInt32(comboCity1.SelectedItem.Value) == (int)TSP.DataManager.CityCode.Sadra)//**شیراز
        {
            #region  SetComperShirazMunicipalityVisible
            lblComperChangesDesignShirazMunicipality.ClientVisible =
            lblComperChangesShirazMunicipulityUrbenismTarh.ClientVisible =
            lblComperChangesShirazMunicipulityUrbenismEntebaghShahri.ClientVisible = (_PageMode == "New" ? false : true);
            #endregion    
            lblDesignShirazMunicipality.ClientVisible = txtDesignShirazMunicipality.ClientVisible = lblDesignShirazMunicipalityLimitation.ClientVisible = true;

            if (_MfMjParentId != (int)TSP.DataManager.MainMajors.Mapping)
            {
                SetComperVisible(true);
            }
            RoundPanelObserveCapacity.ClientVisible = true;
        }
    }
    private void SetLableVisibityBasedOnCity2()
    {
        switch (Convert.ToInt32(comboCity2.SelectedItem.Value))
        {
            case (int)TSP.DataManager.CityCode.Lapooy:
            case (int)TSP.DataManager.CityCode.KhanZenyan:
            case (int)TSP.DataManager.CityCode.Dareyon:
            case (int)TSP.DataManager.CityCode.SoltanShahr:
                RoundPanelRules.ClientVisible = true;
                break;
            default:
                if (_AgentId != Utility.GetCurrentAgentCode())
                {
                    RoundPanelRules.ClientVisible = true;
                }
                break;

        }
    }
    private void SetRoundPanelCommitMuniciTollVisible()
    {

        RoundPanelCommitMuniciToll.ClientVisible = RoundPanelCommitMuniciToll.ClientEnabled = _NeedTaahod = false;
        //if (_AgentId == Utility.GetCurrentAgentCode() && _PageMode == "New")
        //{
        //    TSP.DataManager.TechnicalServices.ObserverTaxManager ObserverTaxManager = new TSP.DataManager.TechnicalServices.ObserverTaxManager();
        //    DataTable dtTax = ObserverTaxManager.FindObserverTaxByMeId(_MeId, "%");
        //    if (dtTax.Rows.Count == 0)
        //    {
        //        RoundPanelCommitMuniciToll.ClientVisible = RoundPanelCommitMuniciToll.ClientEnabled = _NeedTaahod = true;
        //    }
        //    else _NeedTaahod = false;

        //}
    }
    #endregion

    #region Fill Form
    public Boolean LoadMemberInfoForNewMode(int MeId)
    {
        _MeId = MeId;
        if (_MeId > 0)
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(_MeId);
            if (MemberManager.Count == 0)
            {
                ShowMessage("کد عضویت وارد شده معتبر نمی باشد");
                return false;
            }
            int AgentId = Convert.ToInt32(MemberManager[0]["AgentId"]);
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
                _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
                _CurrentCapacityYear = CapacityAssignmentManager[0]["Year"].ToString();
                if (AgentId == Utility.GetCurrentAgentCode())
                {
                    if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["StopmandatoryFileUploadingMainAgent"]) &&
                        string.Compare(CapacityAssignmentManager[0]["StopmandatoryFileUploadingMainAgent"].ToString(), Utility.GetDateOfToday()) > 0)
                        _StopmandatoryFileUploading = 1;
                    else
                        _StopmandatoryFileUploading = 0;
                }
                else
                {
                    if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["StopmandatoryFileUploadingOtherAgent"]) &&
                       string.Compare(CapacityAssignmentManager[0]["StopmandatoryFileUploadingOtherAgent"].ToString(), Utility.GetDateOfToday()) > 0)
                        _StopmandatoryFileUploading = 1;
                    else
                        _StopmandatoryFileUploading = 0;
                }
            }
        }
        if (!CheckConditions(MeId))//*****_MfId GetValue*****
        {
            return false;
        }
        if (_MfId != -2)
        {
            #region FillMemberBasicInfoAndBindMajorComboBox
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(_MeId);
            if (MemberManager.Count != 1)
            {
                ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
                return false;
            }
            #region Fill Basic Info
            RoundPanelMeInfo.ClientVisible = true;
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AgentId"]))
            {
                _AgentId = Convert.ToInt32(MemberManager[0]["AgentId"]);
                SetLabaleVisibilityBasedonAgent();
                SetRoundPanelCommitMuniciTollVisible();
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AgentName"]))
            {
                txtAgent.Text = MemberManager[0]["AgentName"].ToString();
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
                txtExpireDateMember.Text = MemberManager[0]["FileDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
                txtMFNo.Text = MemberManager[0]["FileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MembershipDate"]))
                txtMembershipDate.Text = MemberManager[0]["MembershipDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
                lblMeName.Text = MemberManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
                lblMeLastName.Text = MemberManager[0]["LastName"].ToString();


            #endregion
            #region دفتر گاز
            TSP.DataManager.DocGasOfficeMembersManager DocGasOfficeMembersManager = new TSP.DataManager.DocGasOfficeMembersManager();
            DocGasOfficeMembersManager.FindByMeId(_MeId, (int)TSP.DataManager.GasOfficeMemberStatus.Confirmed);
            if (DocGasOfficeMembersManager.Count > 0)
            {
                _MemberCapacity.HasGasCert = true;
                lblHasGasCert.Text = "می باشد";
                lblHasGasCert.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblHasGasCert.Text = "نمی باشد";
            }
            #endregion
            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            DataTable dtMajor = DocMemberFileMajorManager.SelectDocMemberFileMajorForTSWorkRequest(_MfId, _MeId);
            if (dtMajor.Rows.Count == 0)
            {

                ShowMessage("خطا در بازیابی اطلاعات رشته پروانه اشتغال ایجاد شده است");
                return false;
            }
            if (dtMajor.Rows.Count > 1)//**اگر عضو دو رشته ای باشد باید از بین دورشته انتخاب کند
            {
                cmbMajor.DataSource = dtMajor;
                cmbMajor.DataBind();
                cmbMajor.SelectedIndex = -1;
                SetRoundPanelVisible(true, false);
            }
            else if (dtMajor.Rows.Count == 1)
            {
                cmbMajor.DataSource = "";
                cmbMajor.DataBind();
                SetRoundPanelVisible(false, true);
                if (!Utility.IsDBNullOrNullValue(dtMajor.Rows[0]["MajorParentName"]))
                    txtMemberFileMajor.Text = dtMajor.Rows[0]["MajorParentName"].ToString();
                FillGradeAndCapacityInfo(Convert.ToInt32(dtMajor.Rows[0]["MajorParentId"]));
            }
            #endregion
        }
        if (_CurrentObsId == (int)TSP.DataManager.DocumentGrads.Grade3 && _AgentId != _AgentIdMain)
        {
            RoundPanelUploadControlObsCommitmentForm.ClientVisible = true;
        }
        else RoundPanelUploadControlObsCommitmentForm.ClientVisible = false;
        return true;
    }
    private void FillGradeAndCapacityInfo(int MjParentId)
    {
        if (MjParentId == (int)TSP.DataManager.MainMajors.Mapping)
        {
            SetComperVisible(false);
            lblComperChangesObsShirazMunicipality.ClientVisible = false;
            lblBonyadMaskan.ClientVisible = txtBonyadMaskan.ClientVisible = false;
        }
        _MaxJobCount = 0; _MaxObsJobCapacity = 0;
        _HasError = false;
        lblMaxJobCount.Text = lblMaxJobObsCapacity.Text = lblMaxDesignCapacity.Text = "0";
        RoundPanelMeInfo.ClientVisible = RoundPanelBasicCapacityInfo.ClientVisible = true;
        #region دفتر طراحی
        UserControlMeEngOfficeInfoUserControl.FillInfo(_MeId);
        _MemberCapacity.IsEngOfficeIsExpired = UserControlMeEngOfficeInfoUserControl.IsExpired;
        _IsEngOfficeIsExpired = UserControlMeEngOfficeInfoUserControl.IsExpired;
        _MemberCapacity.HasEngOffice = _HasEngOffice = UserControlMeEngOfficeInfoUserControl.HasEngOffice;
        _EngOfId = UserControlMeEngOfficeInfoUserControl.EngOfId;
        UserControlMeEngOfficeInfoUserControl.Visible = _HasEngOffice;
        #endregion
        #region شرکت طراح و ناظر            
        UserControlMeOfficeInfoUserControl.FillInfo(_MeId);
        _MemberCapacity.IsOfficeIsExpired = _IsOfficeIsExpired = UserControlMeOfficeInfoUserControl.IsExpired;
        _MemberCapacity.HasOffice = _HasOffice = UserControlMeOfficeInfoUserControl.HasOffice;
        _HasEfficientGrade = UserControlMeOfficeInfoUserControl.HasEfficientGrade;

        _OfId = UserControlMeOfficeInfoUserControl.OfId;
        UserControlMeOfficeInfoUserControl.Visible = _HasOffice && _HasEfficientGrade;

        #endregion
        RoundPanelMemberEngOfficeInfo.ClientVisible = _HasOffice || _HasEngOffice;
        RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = CheckListStructureGroups.ClientVisible =
        RoundPanelDesignCapacity.ClientVisible = RounPanelUrbenismCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = false;

        if (_HasEngOffice)
        {
            _MemberCapacity.CurrentDesIdInOfficeEngOff = _CurrentDesIdInOfficeEngOff = UserControlMeEngOfficeInfoUserControl.MemberGradeInEngOffice;
        }
        else if (_HasOffice)
        {

            _MemberCapacity.CurrentDesIdInOfficeEngOff = _CurrentDesIdInOfficeEngOff = UserControlMeOfficeInfoUserControl.MemberGradeInOffice;
        }
        CapacityCalculations CapacityCalculations = new CapacityCalculations();
        if (_HasEngOffice && !_IsEngOfficeIsExpired && _HasEfficientGrade && _HasOffice && !_IsOfficeIsExpired)
        {
            ShowMessage("نام شما به طور همزمان در دفتر طراحی و شرکت طراح و ناظر ثبت شده است.لطفا اعلام مغایرت ثبت نمایید");
            return;
        }
        _MfMjParentId = MjParentId;
        _MemberCapacity = CapacityCalculations.CalculateMemberPotentialCapacityAndSetGradeInfo(_MeId, MjParentId, _MfId, false, (_HasEngOffice && !_IsEngOfficeIsExpired) ? _EngOfId : (_HasOffice && !_IsOfficeIsExpired && _HasEfficientGrade) ? _OfId : -2, (_HasEngOffice && !_IsEngOfficeIsExpired) ? TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice : TSP.DataManager.DocOffIncreaseJobCapacityType.Office, _MemberCapacity.HasGasCert, _CurrentDesIdInOfficeEngOff);

        _MaxJobCount = _MemberCapacity.MemberMaxJobCount;

        PanelMain.ClientVisible = true;
        #region نظارت
        _CurrentObsId = _MemberCapacity.CurrentObsId;
        if (MjParentId != (int)TSP.DataManager.MainMajors.Urbanism && _MemberCapacity.CurrentObsId != -2)
        {
            txtObsName.Text = _MemberCapacity.ObsGradeName;
            txtObsDate.Text = _MemberCapacity.ObsGradeDate;
            lblMaxJobCount.Text = _MemberCapacity.MemberMaxJobCount.ToString();
            lblMaxJobObsCapacity.Text = _MemberCapacity.MemberObservationCapacity.ToString();
            _MaxObsJobCapacity = _MemberCapacity.MemberObservationCapacity;
            ObjdsStructureGroups.SelectParameters["MeGradeId"].DefaultValue = _MemberCapacity.CurrentObsId.ToString();
            CheckListStructureGroups.DataBind();
            RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = true;
        }
        else { RoundPanelRules.ClientVisible = false; }
        #endregion
        #region نقشه برداری
        if (_MemberCapacity.CurrentMappingId != -2)
        {
            txtMappingName.Text = _MemberCapacity.MappingGradeName;
            txtMappingDate.Text = _MemberCapacity.MappingGradeDate;
            lblMaxJobCount.Text = _MemberCapacity.MemberMaxJobCount.ToString();
            if (_MemberCapacity.CurrentObsId == -2)
            {
                _CurrentObsId = _MemberCapacity.CurrentMappingId;
                lblMaxJobObsCapacity.Text = _MemberCapacity.MemberObservationCapacity.ToString();
                _MaxObsJobCapacity = _MemberCapacity.MemberObservationCapacity;

                ObjdsStructureGroups.SelectParameters["MeGradeId"].DefaultValue = "-1";// _MemberCapacity.CurrentMappingId.ToString();
                CheckListStructureGroups.DataBind();
            }
            RoundPanelRules.ClientVisible = RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = true;

        }
        #endregion
        #region طراحی
        if (_MemberCapacity.CurrentDesId != -2)
        {
            txtDesignName.Text = _MemberCapacity.DesignGradeName;
            txtDesignDate.Text = _MemberCapacity.DesignGradeDate;
            _CurrentDesId = _MemberCapacity.CurrentDesId;
            lblMaxJobCount.Text = _MemberCapacity.MemberMaxJobCount.ToString();
            lblMaxDesignCapacity.Text = _MemberCapacity.MemberDesignCapacity.ToString();

            if (_HasEngOffice || _HasOffice)
            {
                RoundPanelDesignCapacity.ClientVisible = RoundPanelMemberEngOfficeInfo.ClientVisible = true;
            }
            else
            {
                RoundPanelDesignCapacity.ClientVisible = RoundPanelMemberEngOfficeInfo.ClientVisible = false;
            }
            RoundPanelDesignCapacity.ClientEnabled = false;
            TitleDesignCapacity.InnerText = "تعیین زیربنا طراحی(فاقد دفتر طراحی/شرکت حقوقی طراح و ناظر با پروانه معتبر)";
            TitleDesignCapacity.Attributes["class"] = "HelpUL";
            if (_HasEngOffice && !_IsEngOfficeIsExpired)
            {
                RoundPanelDesignCapacity.ClientEnabled = true;
                TitleDesignCapacity.InnerText = "تعیین زیربنا طراحی";
                TitleDesignCapacity.Attributes["class"] = "legendTitle";
            }
            if (_HasOffice && !_IsOfficeIsExpired && _HasEfficientGrade)
            {
                RoundPanelDesignCapacity.ClientEnabled = true;
                TitleDesignCapacity.InnerText = "تعیین زیربنا طراحی";
                TitleDesignCapacity.Attributes["class"] = "legendTitle";
            }
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
            {
                if ((_HasEngOffice && _IsEngOfficeIsExpired) || (_HasOffice && _IsOfficeIsExpired))
                {
                    RoundPanelDesignCapacity.ClientEnabled = true;
                }
            }
        }
        #endregion
        #region شهرسازی
        if (_MemberCapacity.CurrentUrbenismId != -2)
        {

            _CurrentUrbId = _MemberCapacity.CurrentUrbenismId;

            txtUrbenismName.Text = _MemberCapacity.UrbenismGradeName;
            txtUrbenismDate.Text = _MemberCapacity.UrbenismGradeDate;
            lblMaxJobCount.Text = _MemberCapacity.MemberMaxJobCount.ToString();
            lblMaxJobUrbenismCapacityUrbenismTarh.Text = _MemberCapacity.MemberUrbenismTarhShahriCapacity.ToString();
            lblMaxJobUrbenismCapacityEntebaghShahri.Text = _MemberCapacity.MemberUrbenismEntebaghShahriSakhtemanCapacity.ToString();
            RounPanelUrbenismCapacity.ClientVisible = true;
            RoundPanelBasicCapacityInfo.ClientVisible = false;
            RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = false;
        }
        #endregion

        //****حداکثر ظرفیت برابر است با ماکزیمم حداکثر ظرفیت نظارت و  مجموع حداکثر ظرفیت طراحی و افزایش عضویت دفتر/شرکت
        lblMaxTotalCapacity.Text = _MemberCapacity.MemberMaxCapacity.ToString();
        if (_MemberCapacity.CurrentObsId == -2 && _MemberCapacity.CurrentDesId == -2
            && _MemberCapacity.CurrentMappingId == -2 && _MemberCapacity.CurrentUrbenismId == -2)
        {
            ShowMessage("شما هیچ یک از صلاحیت  های نظارت / طراحی/نقشه برداری/شهرسازی را نداردید");
        }
        #region  Municipality Capacity Limitation
        TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager CapacityInMunicipalityManager = new TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager();
        DataTable dtCapacityMun = CapacityInMunicipalityManager.Search(_MemberCapacity.MjParentId, _MemberCapacity.CurrentObsId != -2 ? _MemberCapacity.CurrentObsId : _MemberCapacity.CurrentMappingId != -2 ? _MemberCapacity.CurrentMappingId : -1
            , _MemberCapacity.CurrentDesId == -2 ? -1 : _MemberCapacity.CurrentDesIdInOfficeEngOff, 0, _MemberCapacity.CurrentUrbenismId == -2 ? -1 : _MemberCapacity.CurrentUrbenismId);
        if (dtCapacityMun.Rows.Count > 0)
        {
            txtObsShirazMunicipality.MaskSettings.Mask = "<0" + ".." + dtCapacityMun.Rows[0]["MaxObsCapacity"].ToString() + ">";
            lblObsShirazMunicipalityLimitation.Text = " (حداکثر " + dtCapacityMun.Rows[0]["MaxObsCapacity"].ToString() + ")";
            txtDesignShirazMunicipality.MaskSettings.Mask = "<0" + ".." + dtCapacityMun.Rows[0]["MaxDesCapacity"].ToString() + ">";
            lblDesignShirazMunicipalityLimitation.Text = " (حداکثر " + dtCapacityMun.Rows[0]["MaxDesCapacity"].ToString() + ")";

            txtShirazMunicipulityUrbenismTarh.MaskSettings.Mask = "<0" + ".." + dtCapacityMun.Rows[0]["MaxUrbenismTarhShahrsazi"].ToString() + ">";
            lblShirazMunicipulityUrbenismTarhLimitation.Text = " (حداکثر " + dtCapacityMun.Rows[0]["MaxUrbenismTarhShahrsazi"].ToString() + ")";

            txtShirazMunicipulityUrbenismEntebaghShahri.MaskSettings.Mask = "<0" + ".." + dtCapacityMun.Rows[0]["MaxUrbenismEntebaghShahri"].ToString() + ">";
            lblShirazMunicipulityUrbenismEntebaghShahriLimitation.Text = " (حداکثر " + dtCapacityMun.Rows[0]["MaxUrbenismEntebaghShahri"].ToString() + ")";
        }
        else
        {
            int i = 0;
            txtObsShirazMunicipality.MaskSettings.Mask = "<0" + ".." + lblMaxJobObsCapacity.Text + ">";
            lblObsShirazMunicipalityLimitation.Text = " (حداکثر " + lblMaxJobObsCapacity.Text + ")";
            txtDesignShirazMunicipality.MaskSettings.Mask = "<0" + ".." + lblMaxDesignCapacity.Text + ">";
            lblDesignShirazMunicipalityLimitation.Text = " (حداکثر " + lblMaxDesignCapacity.Text + ")";
            if (int.TryParse(lblMaxJobUrbenismCapacityUrbenismTarh.Text, out i))
                txtShirazMunicipulityUrbenismTarh.MaskSettings.Mask = "<0" + ".." + lblMaxJobUrbenismCapacityUrbenismTarh.Text + ">";
            else
                txtShirazMunicipulityUrbenismTarh.MaskSettings.Mask = "0";
            lblShirazMunicipulityUrbenismTarhLimitation.Text = " (حداکثر " + lblMaxJobUrbenismCapacityUrbenismTarh.Text + ")";

            if (int.TryParse(lblMaxJobUrbenismCapacityEntebaghShahri.Text, out i))
                txtShirazMunicipulityUrbenismEntebaghShahri.MaskSettings.Mask = "<0" + ".." + lblMaxJobUrbenismCapacityEntebaghShahri.Text + ">";
            else
                txtShirazMunicipulityUrbenismEntebaghShahri.MaskSettings.Mask = "0";
            lblShirazMunicipulityUrbenismEntebaghShahriLimitation.Text = " (حداکثر " + lblMaxJobUrbenismCapacityEntebaghShahri.Text + ")";

        }
        #endregion
    }
    /// <summary>
    /// تنها برای ثبت درخواست تغییرات جدید باید فراخوانی شود
    /// </summary>
    private void FillFormBasedOnObsRequest()
    {
        RoundPanelCommitMuniciToll.Visible = false;
        CheckBoxCommitMuniciToll.Checked = true;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(_MeId);
        if (MemberManager.Count != 1)
        {
            ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
            return;
        }
        #region Fill Basic Info

        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AgentId"]))
        {
            _AgentId = Convert.ToInt32(MemberManager[0]["AgentId"]);
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (_AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
                _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
                _CurrentCapacityYear = CapacityAssignmentManager[0]["Year"].ToString();

                if (_AgentId == Utility.GetCurrentAgentCode())
                {
                    if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["StopmandatoryFileUploadingMainAgent"]) &&
                        string.Compare(CapacityAssignmentManager[0]["StopmandatoryFileUploadingMainAgent"].ToString(), Utility.GetDateOfToday()) > 0)
                        _StopmandatoryFileUploading = 1;
                    else
                        _StopmandatoryFileUploading = 0;
                }
                else
                {
                    if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["StopmandatoryFileUploadingOtherAgent"]) &&
                       string.Compare(CapacityAssignmentManager[0]["StopmandatoryFileUploadingOtherAgent"].ToString(), Utility.GetDateOfToday()) > 0)
                        _StopmandatoryFileUploading = 1;
                    else
                        _StopmandatoryFileUploading = 0;
                }

            }
        }
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AgentName"]))
        {
            txtAgent.Text = MemberManager[0]["AgentName"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
            txtExpireDateMember.Text = MemberManager[0]["FileDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
            txtMFNo.Text = MemberManager[0]["FileNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MembershipDate"]))
            txtMembershipDate.Text = MemberManager[0]["MembershipDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
            lblMeName.Text = MemberManager[0]["FirstName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
            lblMeLastName.Text = MemberManager[0]["LastName"].ToString();

        #endregion
        #region اطلاعات آخرین درخواست پروانه اشتغال
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(_MeId, 0, 1);
        if (dtMeFile.Rows.Count <= 0)
        {
            ShowMessage("خطایی در بازیابی اطلاعات پروانه اشتغال بوجود آمده است");
            return;
        }
        _MfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
        _DocMeFileExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
        #endregion
        #region عضویت در دفتر گاز
        TSP.DataManager.DocGasOfficeMembersManager DocGasOfficeMembersManager = new TSP.DataManager.DocGasOfficeMembersManager();
        DocGasOfficeMembersManager.FindByMeId(_MeId, (int)TSP.DataManager.GasOfficeMemberStatus.Confirmed);
        if (DocGasOfficeMembersManager.Count > 0)
        {
            _MemberCapacity.HasGasCert = true;
            lblHasGasCert.Text = "می باشد";
            lblHasGasCert.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblHasGasCert.Text = "نمی باشد";
        }
        #endregion

        #region Fill Info from WorkRequest
        //برای مقایسه مقادیر متراژها از آخرین درخواست و تابع مقایسه استفاده می کنیم
        //آخرین  درخواست را برای هیدن فیلدها می خواهیم که بعد با جاوا اسکریپت سامانش دهیم و تابع مقایسه را برای نمایش در لیبل های مقایسه
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        ObserverWorkRequestManager.FindByMeId(_MeId);
        if (ObserverWorkRequestManager.Count != 1)
        {
            ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
            return;
        }
        #region تنظیمات شهر ها بر اساس نمایندگی و مقدار دهی آنها
        SetLabaleVisibilityBasedonAgent();

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["City1"]) && !Utility.IsDBNullOrNullValue(comboCity1.Items.FindByValue(ObserverWorkRequestManager[0]["City1"])))
            comboCity1.SelectedIndex = comboCity1.Items.FindByValue(ObserverWorkRequestManager[0]["City1"]).Index;
        SetLableVisibityBasedOnCity1();
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["City2"]) && !Utility.IsDBNullOrNullValue(comboCity2.Items.FindByValue(ObserverWorkRequestManager[0]["City2"])))
            comboCity2.SelectedIndex = comboCity2.Items.FindByValue(ObserverWorkRequestManager[0]["City2"]).Index;
        SetLableVisibityBasedOnCity2();
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["CityLastChangedWorkYear"])
            && _AgentId == Utility.GetCurrentAgentCode() && Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member
            && ObserverWorkRequestManager[0]["CityLastChangedWorkYear"].ToString() == _CurrentCapacityYear)
        {
            comboCity1.ClientEnabled = false;
        }
        #endregion

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ObsId"]))
            _CurrentObsId = Convert.ToInt32(ObserverWorkRequestManager[0]["ObsId"]);
        //if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["MfId"]))
        //    _MfId = Convert.ToInt32(ObserverWorkRequestManager[0]["MfId"]);
        //if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["DocMeFileExpireDate"]))
        //    _DocMeFileExpireDate = ObserverWorkRequestManager[0]["DocMeFileExpireDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["MasterMfMjParentId"]))
            _MfMjParentId = Convert.ToInt32(ObserverWorkRequestManager[0]["MasterMfMjParentId"]);
        CheckBoxWantObserverSelect.Checked = !(Convert.ToBoolean(ObserverWorkRequestManager[0]["IsObserverOff"]));//آیا تمایل به ارجاع کار نظارت دارید؟
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["StartOffDate"]) ||
            !Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["EndOffDate"]))
        {
            RoundPanelOffRequest.Visible = true;
            txtStartOffDate.Text = ObserverWorkRequestManager[0]["StartOffDate"].ToString();
            txtEndOffDate.Text = ObserverWorkRequestManager[0]["EndOffDate"].ToString();
        }

        CheckBoxWantCharity.Checked = Convert.ToBoolean(ObserverWorkRequestManager[0]["WantCharityWork"]);
        CheckBoxWantEghdamMeliMaskan.Checked = Convert.ToBoolean(ObserverWorkRequestManager[0]["WantEghdamMeliMaskan"]);
        CheckListStructureGroups.DataBind();
        if (Convert.ToBoolean(ObserverWorkRequestManager[0]["Group1"]))
        {
            CheckListStructureGroups.Items.FindByValue("1").Selected = Convert.ToBoolean(ObserverWorkRequestManager[0]["Group1"]);
        }
        if (Convert.ToBoolean(ObserverWorkRequestManager[0]["Group2"]))
        {
            CheckListStructureGroups.Items.FindByValue("2").Selected = Convert.ToBoolean(ObserverWorkRequestManager[0]["Group2"]);
        }
        if (Convert.ToBoolean(ObserverWorkRequestManager[0]["Group3"]))
        {
            CheckListStructureGroups.Items.FindByValue("3").Selected = Convert.ToBoolean(ObserverWorkRequestManager[0]["Group3"]);
        }
        if (Convert.ToBoolean(ObserverWorkRequestManager[0]["Group4"]))
        {
            CheckListStructureGroups.Items.FindByValue("4").Selected = Convert.ToBoolean(ObserverWorkRequestManager[0]["Group4"]);
        }
        HiddenFieldPage["MustHasAttach"] = false;

        hplPreview.NavigateUrl = "";
        HiddenFieldPage["flpAttachValidation"] = false;
        #region Set HiddenFieldPage last value For Comparing
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["BonyadMaskan"]))
            HiddenFieldPage["LastObsBonyadMaskan"] = txtBonyadMaskan.Text = ObserverWorkRequestManager[0]["BonyadMaskan"].ToString();
        else
            HiddenFieldPage["LastObsBonyadMaskan"] = txtBonyadMaskan.Text = "0";


        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]))
            HiddenFieldPage["LastObsShirazMunicipality"] = txtObsShirazMunicipality.Text = ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"].ToString();

        else
            HiddenFieldPage["LastObsShirazMunicipality"] = txtObsShirazMunicipality.Text = "0";

        //***طراحی و شهرسازی هر دو در این قسمت وارد می شوند چون هر دو با هم وجود ندارند
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"]))
            HiddenFieldPage["LastDesignShirazMunicipality"] = txtDesignShirazMunicipality.Text = ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"].ToString();
        else
            HiddenFieldPage["LastDesignShirazMunicipality"] = txtDesignShirazMunicipality.Text = "0";

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["BonyadMaskanDesignMeter"]))
            HiddenFieldPage["LastDesignBonyadMaskan"] = txtDesignBonyadMaskan.Text = ObserverWorkRequestManager[0]["BonyadMaskanDesignMeter"].ToString();
        else
            HiddenFieldPage["LastDesignBonyadMaskan"] = txtDesignBonyadMaskan.Text = "0";

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"]))
            HiddenFieldPage["LastMunicipulityUrbTarh"] = txtShirazMunicipulityUrbenismTarh.Text = ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"].ToString();
        else
            HiddenFieldPage["LastMunicipulityUrbTarh"] = txtShirazMunicipulityUrbenismTarh.Text = "0";
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"]))
            HiddenFieldPage["LastMunicipulityUrbEntebaghShahri"] = txtShirazMunicipulityUrbenismEntebaghShahri.Text = ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"].ToString();
        else
            HiddenFieldPage["LastMunicipulityUrbEntebaghShahri"] = txtShirazMunicipulityUrbenismEntebaghShahri.Text = "0";
        #endregion
        CheckBoxIsFullTimeWorker.Checked = Convert.ToBoolean(ObserverWorkRequestManager[0]["IsFullTimeWorker"]);
        CheckBoxWantShahrakSanati.Checked = Convert.ToBoolean(ObserverWorkRequestManager[0]["WantShahrakSanatiMeter"]);
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["MjParentName"]))
            txtMemberFileMajor.Text = ObserverWorkRequestManager[0]["MjParentName"].ToString();
        #endregion

        FillGradeAndCapacityInfoByChangeTable(Convert.ToInt32(ObserverWorkRequestManager[0]["MasterMfMjParentId"]));
        if (RoundPanelRules.ClientVisible && !Convert.ToBoolean(ObserverWorkRequestManager[0]["IsObserverOff"]))
            checkboxRulls.Checked = true;
        #region چک کردن ظرفیت شخص و متراژهای شهرداری شیراز و متراژ های مصرف شده در پروژه ها
        CheckMemberCapacityUsage(_MeId, Convert.ToInt32(txtDesignShirazMunicipality.Text), Convert.ToInt32(txtObsShirazMunicipality.Text), Convert.ToInt32(lblMaxDesignCapacity.Text), Convert.ToInt32(lblMaxJobObsCapacity.Text),Convert.ToInt32(txtBonyadMaskan.Text), Convert.ToInt32(txtDesignBonyadMaskan.Text));
        #endregion
    }
    /// <summary>
    /// برای مشاهده درخواست
    /// </summary>
    /// <param name="ObsWorkReqChangeId"></param>
    private void FillFormBasedOnObsRequestChange(int ObsWorkReqChangeId)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(_MeId);
        if (MemberManager.Count != 1)
        {
            ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
            return;
        }
        #region Fill Basic Info

        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
            txtExpireDateMember.Text = MemberManager[0]["FileDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileNo"]))
            txtMFNo.Text = MemberManager[0]["FileNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MembershipDate"]))
            txtMembershipDate.Text = MemberManager[0]["MembershipDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FirstName"]))
            lblMeName.Text = MemberManager[0]["FirstName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["LastName"]))
            lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
        #endregion
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager TSObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSObserverWorkRequestManager.FindByMeId(_MeId);
        if (TSObserverWorkRequestManager.Count == 0)
        {
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
            return;
        }
        int ObsWorkReqId = Convert.ToInt32(TSObserverWorkRequestManager[0]["ObsWorkReqId"]);


        //برای مقایسه مقادیر متراژها از آخرین درخواست و تابع مقایسه استفاده می کنیم
        //آخرین  درخواست را برای هیدن فیلدها می خواهیم که بعد با جاوا اسکریپت سامانش دهیم و تابع مقایسه را برای نمایش در لیبل های مقایسه
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();

        #region WorkRequest
        ObserverWorkRequestChangesManager.FindByObsWorkReqChangeId(_ObsWorkReqChangeId);
        if (ObserverWorkRequestChangesManager.Count != 1)
        {
            ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است");
            return;
        }

        DataTable dtLastRequest = ObserverWorkRequestChangesManager.SelectLastConfirmRequest(ObsWorkReqId, _MeId, _ObsWorkReqChangeId);
        DataTable dtComperChange = ObserverWorkRequestChangesManager.SelectComperChangeRequest(_ObsWorkReqChangeId, _MeId);

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["MeAgentId"]))
        {
            _AgentId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MeAgentId"]);
            SetLabaleVisibilityBasedonAgent();
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (_AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
                _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
                _CurrentCapacityYear = CapacityAssignmentManager[0]["Year"].ToString();

                if (_AgentId == Utility.GetCurrentAgentCode())
                {
                    if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["StopmandatoryFileUploadingMainAgent"]) &&
                        string.Compare(CapacityAssignmentManager[0]["StopmandatoryFileUploadingMainAgent"].ToString(), Utility.GetDateOfToday()) > 0)
                        _StopmandatoryFileUploading = 1;
                    else
                        _StopmandatoryFileUploading = 0;
                }
                else
                {
                    if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["StopmandatoryFileUploadingOtherAgent"]) &&
                       string.Compare(CapacityAssignmentManager[0]["StopmandatoryFileUploadingOtherAgent"].ToString(), Utility.GetDateOfToday()) > 0)
                        _StopmandatoryFileUploading = 1;
                    else
                        _StopmandatoryFileUploading = 0;
                }

            }
        }
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["City1"]))
            comboCity1.SelectedIndex = comboCity1.Items.FindByValue(ObserverWorkRequestChangesManager[0]["City1"]).Index;
        SetLableVisibityBasedOnCity1();
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["City2"]) && comboCity2.Items.FindByValue(ObserverWorkRequestChangesManager[0]["City2"]) != null)
            comboCity2.SelectedIndex = comboCity2.Items.FindByValue(ObserverWorkRequestChangesManager[0]["City2"]).Index;
        SetLableVisibityBasedOnCity2();
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["AgentName"]))
        {
            txtAgent.Text = ObserverWorkRequestChangesManager[0]["AgentName"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["ObsId"]))
            _CurrentObsId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["ObsId"]);
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["MfId"]))
            _MfId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MfId"]);
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["DocMeFileExpireDate"]))
            _DocMeFileExpireDate = ObserverWorkRequestChangesManager[0]["DocMeFileExpireDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["MasterMfMjParentId"]))
            _MfMjParentId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MasterMfMjParentId"]);
        CheckBoxWantObserverSelect.Checked = !(Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["IsObserverOff"]));//آیا تمایل به ارجاع کار نظارت دارید؟
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["StartOffDate"]) ||
            !Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["EndOffDate"]))
        {
            RoundPanelOffRequest.Visible = true;
            txtStartOffDate.Text = ObserverWorkRequestChangesManager[0]["StartOffDate"].ToString();
            txtEndOffDate.Text = ObserverWorkRequestChangesManager[0]["EndOffDate"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["HasGasCert"]) && Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["HasGasCert"]))
        {
            _MemberCapacity.HasGasCert = true;
            lblHasGasCert.Text = "می باشد";
            lblHasGasCert.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblHasGasCert.Text = "نمی باشد";
        }
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["UrlObserverCommitmentForm"]))
        {
            RoundPanelUploadControlObsCommitmentForm.ClientVisible = true;
            hplPreviewObsCommitmentForm.NavigateUrl = ObserverWorkRequestChangesManager[0]["UrlObserverCommitmentForm"].ToString();
        }


        CheckBoxWantCharity.Checked = Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["WantCharityWork"]);
        CheckBoxWantEghdamMeliMaskan.Checked =Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["WantEghdamMeliMaskan"])?false: Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["WantEghdamMeliMaskan"]);
        CheckListStructureGroups.DataBind();
        if (Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["Group1"]))
        {
            CheckListStructureGroups.Items.FindByValue("1").Selected = Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["Group1"]);
        }
        if (Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["Group2"]))
        {
            CheckListStructureGroups.Items.FindByValue("2").Selected = Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["Group2"]);
        }
        if (Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["Group3"]))
        {
            CheckListStructureGroups.Items.FindByValue("3").Selected = Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["Group3"]);
        }
        if (Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["Group4"]))
        {
            CheckListStructureGroups.Items.FindByValue("4").Selected = Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["Group4"]);
        }
        ///باید این هیدن فیلد را در جای مناسب مقدار دهی اولیه کرد که در هنگام ذخیره کردن به اشکال بر نخوریم

        if (dtComperChange.Rows.Count == 1 && dtLastRequest.Rows.Count == 1 && _PageMode == "View")
        {
            SetUploadControlVisibeld(dtComperChange);
            lblComperChangesObsBonyadMaskan.Text = TextLblComper(dtLastRequest.Rows[0]["BonyadMaskan"], dtComperChange.Rows[0]["SubBonyadMaskan"]);
            lblComperChangesObsShirazMunicipality.Text = TextLblComper(dtLastRequest.Rows[0]["ShirazMunicipalityMeter"], dtComperChange.Rows[0]["SubShirazMunicipalityMeter"]);
            lblComperChangesDesignShirazMunicipality.Text = TextLblComper(dtLastRequest.Rows[0]["ShirazMunicipalityDesignMeter"], dtComperChange.Rows[0]["SubShirazMunicipalityDesignMeter"]);
            lblComperChangesDesignBonyadMaskan.Text = TextLblComper(dtLastRequest.Rows[0]["BonyadMaskanDesignMeter"], dtComperChange.Rows[0]["SubBonyadMaskanDesignMeter"]);
            lblComperChangesShirazMunicipulityUrbenismTarh.Text = TextLblComper(dtLastRequest.Rows[0]["ShirazMunicipulityUrbenismTarh"], dtComperChange.Rows[0]["SubShirazMunicipulityUrbenismTarh"]);
            lblComperChangesShirazMunicipulityUrbenismEntebaghShahri.Text = TextLblComper(dtLastRequest.Rows[0]["ShirazMunicipulityUrbenismEntebaghShahri"], dtComperChange.Rows[0]["SubShirazMunicipulityUrbenismEntebaghShahri"]);

        }
        else
            HiddenFieldPage["MustHasAttach"] = false;

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["UrlAttachment"]))
        {
            hplPreview.NavigateUrl = ObserverWorkRequestChangesManager[0]["UrlAttachment"].ToString();

            RoundPanelUploadControl.ClientVisible = true;
            HiddenFieldPage["flpAttachValidation"] = true;
        }

        else
        {
            hplPreview.NavigateUrl = "";
            HiddenFieldPage["flpAttachValidation"] = false;
        }

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["BonyadMaskan"]))
            HiddenFieldPage["LastObsBonyadMaskan"] = txtBonyadMaskan.Text = ObserverWorkRequestChangesManager[0]["BonyadMaskan"].ToString();
        else
            HiddenFieldPage["LastObsBonyadMaskan"] = txtBonyadMaskan.Text = "0";


        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["ShirazMunicipalityMeter"]))
            HiddenFieldPage["LastObsShirazMunicipality"] = txtObsShirazMunicipality.Text = ObserverWorkRequestChangesManager[0]["ShirazMunicipalityMeter"].ToString();

        else
            HiddenFieldPage["LastObsShirazMunicipality"] = txtObsShirazMunicipality.Text = "0";

        //***طراحی و شهرسازی هر دو در این قسمت وارد می شوند چون هر دو با هم وجود ندارند
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["ShirazMunicipalityDesignMeter"]))
            HiddenFieldPage["LastDesignShirazMunicipality"] = txtDesignShirazMunicipality.Text = ObserverWorkRequestChangesManager[0]["ShirazMunicipalityDesignMeter"].ToString();
        else
            HiddenFieldPage["LastDesignShirazMunicipality"] = txtDesignShirazMunicipality.Text = "0";

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["BonyadMaskanDesignMeter"]))
            HiddenFieldPage["LastDesignBonyadMaskan"] = txtDesignBonyadMaskan.Text = ObserverWorkRequestChangesManager[0]["BonyadMaskanDesignMeter"].ToString();
        else
            HiddenFieldPage["LastDesignBonyadMaskan"] = txtDesignBonyadMaskan.Text = "0";

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["ShirazMunicipulityUrbenismTarh"]))
            HiddenFieldPage["LastMunicipulityUrbTarh"] = txtShirazMunicipulityUrbenismTarh.Text = ObserverWorkRequestChangesManager[0]["ShirazMunicipulityUrbenismTarh"].ToString();
        else
            HiddenFieldPage["LastMunicipulityUrbTarh"] = txtShirazMunicipulityUrbenismTarh.Text = "0";
        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"]))
            HiddenFieldPage["LastMunicipulityUrbEntebaghShahri"] = txtShirazMunicipulityUrbenismEntebaghShahri.Text = ObserverWorkRequestChangesManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"].ToString();
        else
            HiddenFieldPage["LastMunicipulityUrbEntebaghShahri"] = txtShirazMunicipulityUrbenismEntebaghShahri.Text = "0";

        CheckBoxIsFullTimeWorker.Checked = Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["IsFullTimeWorker"]);
        RoundPanelCommitMuniciToll.Visible = false;

        CheckBoxWantShahrakSanati.Checked = Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["WantShahrakSanatiMeter"]);

        #endregion

        if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestChangesManager[0]["MjParentName"]))
            txtMemberFileMajor.Text = ObserverWorkRequestChangesManager[0]["MjParentName"].ToString();

        int MjParentId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MasterMfMjParentId"]);
        FillGradeAndCapacityInfoByChangeTable(Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MasterMfMjParentId"]));

        if (RoundPanelRules.ClientVisible && !Convert.ToBoolean(ObserverWorkRequestChangesManager[0]["IsObserverOff"]))
            checkboxRulls.Checked = true;
    }
    private void FillGradeAndCapacityInfoByChangeTable(int MjParentId)
    {
        if (MjParentId == (int)TSP.DataManager.MainMajors.Mapping)
        {
            SetComperVisible(false);
            lblBonyadMaskan.ClientVisible = txtBonyadMaskan.ClientVisible = false;
        }
        _MaxJobCount = 0; _MaxObsJobCapacity = 0;
        _HasError = false;
        lblMaxJobCount.Text = lblMaxJobObsCapacity.Text =
         lblMaxDesignCapacity.Text = "0";
        RoundPanelMeInfo.ClientVisible = RoundPanelBasicCapacityInfo.ClientVisible = true;
        #region دفتر طراحی
        UserControlMeEngOfficeInfoUserControl.FillInfo(_MeId);
        _MemberCapacity.IsEngOfficeIsExpired = UserControlMeEngOfficeInfoUserControl.IsExpired;
        _IsEngOfficeIsExpired = UserControlMeEngOfficeInfoUserControl.IsExpired;
        _MemberCapacity.HasEngOffice = _HasEngOffice = UserControlMeEngOfficeInfoUserControl.HasEngOffice;
        _EngOfId = UserControlMeEngOfficeInfoUserControl.EngOfId;
        UserControlMeEngOfficeInfoUserControl.Visible = _HasEngOffice;
        #endregion
        #region شرکت طراح و ناظر            
        UserControlMeOfficeInfoUserControl.FillInfo(_MeId);
        _MemberCapacity.IsOfficeIsExpired = _IsOfficeIsExpired = UserControlMeOfficeInfoUserControl.IsExpired;
        _MemberCapacity.HasOffice = _HasOffice = UserControlMeOfficeInfoUserControl.HasOffice;
        _HasEfficientGrade = UserControlMeOfficeInfoUserControl.HasEfficientGrade;

        _OfId = UserControlMeOfficeInfoUserControl.OfId;
        UserControlMeOfficeInfoUserControl.Visible = _HasOffice && _HasEfficientGrade;
        #endregion
        RoundPanelMemberEngOfficeInfo.ClientVisible = _HasOffice || _HasEngOffice;
        RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = CheckListStructureGroups.ClientVisible =
        RoundPanelDesignCapacity.ClientVisible = RounPanelUrbenismCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = false;
        if (_HasEngOffice && !_IsEngOfficeIsExpired)
        {
            _MemberCapacity.CurrentDesIdInOfficeEngOff = _CurrentDesIdInOfficeEngOff = UserControlMeEngOfficeInfoUserControl.MemberGradeInEngOffice;
        }
        else if (_HasOffice && !_IsOfficeIsExpired)
        {

            _MemberCapacity.CurrentDesIdInOfficeEngOff = _CurrentDesIdInOfficeEngOff = UserControlMeOfficeInfoUserControl.MemberGradeInOffice;
        }
        CapacityCalculations CapacityCalculations = new CapacityCalculations();
        if (_HasEngOffice && !_IsEngOfficeIsExpired && _HasOffice && !_IsOfficeIsExpired && _HasEfficientGrade)
        {
            ShowMessage("نام شما به طور همزمان در دفتر طراحی و شرکت طراح و ناظر ثبت شده است.لطفا اعلام مغایرت ثبت نمایید");
            return;
        }
        _MfMjParentId = MjParentId;
        _MemberCapacity = CapacityCalculations.CalculateMemberPotentialCapacityAndSetGradeInfo(_MeId, MjParentId, _MfId, false, (_HasEngOffice && !_IsEngOfficeIsExpired) ? _EngOfId : (_HasOffice && !_IsOfficeIsExpired && _HasEfficientGrade) ? _OfId : -2, (_HasEngOffice && !_IsEngOfficeIsExpired) ? TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice : TSP.DataManager.DocOffIncreaseJobCapacityType.Office, _MemberCapacity.HasGasCert, _CurrentDesIdInOfficeEngOff);

        _MaxJobCount = _MemberCapacity.MemberMaxJobCount;
        PanelMain.ClientVisible = true;

        #region نظارت
        _CurrentObsId = _MemberCapacity.CurrentObsId;
        if (MjParentId != (int)TSP.DataManager.MainMajors.Urbanism && _MemberCapacity.CurrentObsId != -2)
        {
            txtObsName.Text = _MemberCapacity.ObsGradeName;
            txtObsDate.Text = _MemberCapacity.ObsGradeDate;
            lblMaxJobCount.Text = _MemberCapacity.MemberMaxJobCount.ToString();
            lblMaxJobObsCapacity.Text = _MemberCapacity.MemberObservationCapacity.ToString();
            _MaxObsJobCapacity = _MemberCapacity.MemberObservationCapacity;
            ObjdsStructureGroups.SelectParameters["MeGradeId"].DefaultValue = _MemberCapacity.CurrentObsId.ToString();
            CheckListStructureGroups.DataBind();
            RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = true;
        }
        else { RoundPanelRules.ClientVisible = false; }
        #endregion
        #region نقشه برداری
        txtMappingName.Text = _MemberCapacity.MappingGradeName;
        txtMappingDate.Text = _MemberCapacity.MappingGradeDate;
        if (_MemberCapacity.CurrentMappingId != -2)
        {
            lblMaxJobCount.Text = _MemberCapacity.MemberMaxJobCount.ToString();
            if (_MemberCapacity.CurrentObsId == -2)
            {
                _CurrentObsId = _MemberCapacity.CurrentMappingId;
                lblMaxJobObsCapacity.Text = _MemberCapacity.MemberObservationCapacity.ToString();
                _MaxObsJobCapacity = _MemberCapacity.MemberObservationCapacity;

                ObjdsStructureGroups.SelectParameters["MeGradeId"].DefaultValue = "-1";
                CheckListStructureGroups.DataBind();
            }
            RoundPanelRules.ClientVisible = RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = true;

        }
        #endregion
        #region طراحی
        txtDesignName.Text = _MemberCapacity.DesignGradeName;
        txtDesignDate.Text = _MemberCapacity.DesignGradeDate;
        if (_MemberCapacity.CurrentDesId != -2)
        {

            _CurrentDesId = _MemberCapacity.CurrentDesId;
            lblMaxJobCount.Text = _MemberCapacity.MemberMaxJobCount.ToString();
            lblMaxDesignCapacity.Text = _MemberCapacity.MemberDesignCapacity.ToString();
            if (_HasEngOffice || _HasOffice)
                RoundPanelDesignCapacity.ClientVisible = RoundPanelMemberEngOfficeInfo.ClientVisible = true;
            else
                RoundPanelDesignCapacity.ClientVisible = RoundPanelMemberEngOfficeInfo.ClientVisible = false;
            RoundPanelDesignCapacity.ClientEnabled = false;
            TitleDesignCapacity.InnerText = "تعیین زیربنا طراحی(فاقد دفتر طراحی/شرکت حقوقی طراح و ناظر با پروانه معتبر)";
            TitleDesignCapacity.Attributes["class"] = "HelpUL";
            if (_HasEngOffice && !_IsEngOfficeIsExpired)
            {
                RoundPanelDesignCapacity.ClientEnabled = true;
                TitleDesignCapacity.InnerText = "تعیین زیربنا طراحی";
                TitleDesignCapacity.Attributes["class"] = "legendTitle";
                ////lblEngOfficeCapacity.Text = _MemberCapacity.EngOfficeCapacity.ToString();
            }
            if (_HasOffice && !_IsOfficeIsExpired && _HasEfficientGrade)
            {
                RoundPanelDesignCapacity.ClientEnabled = true;
                TitleDesignCapacity.InnerText = "تعیین زیربنا طراحی";
                TitleDesignCapacity.Attributes["class"] = "legendTitle";
            }


            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
            {
                if ((_HasEngOffice && _IsEngOfficeIsExpired) || (_HasOffice && _IsOfficeIsExpired))
                {
                    RoundPanelDesignCapacity.ClientEnabled = true;
                }
            }
        }
        #endregion

        #region شهرسازی
        if (_MemberCapacity.CurrentUrbenismId != -2)
        {

            _CurrentUrbId = _MemberCapacity.CurrentUrbenismId;

            txtUrbenismName.Text = _MemberCapacity.UrbenismGradeName;
            txtUrbenismDate.Text = _MemberCapacity.UrbenismGradeDate;
            lblMaxJobCount.Text = _MemberCapacity.MemberMaxJobCount.ToString();
            lblMaxJobUrbenismCapacityUrbenismTarh.Text = _MemberCapacity.MemberUrbenismTarhShahriCapacity.ToString();
            lblMaxJobUrbenismCapacityEntebaghShahri.Text = _MemberCapacity.MemberUrbenismEntebaghShahriSakhtemanCapacity.ToString();
            RounPanelUrbenismCapacity.ClientVisible = true;
            RoundPanelBasicCapacityInfo.ClientVisible = false;
            RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = false;
        }
        #endregion

        //****حداکثر ظرفیت برابر است با ماکزیمم حداکثر ظرفیت نظارت و  مجموع حداکثر ظرفیت طراحی و افزایش عضویت دفتر/شرکت
        //int MaxCapacity = Math.Max(Convert.ToInt32(lblMaxJobObsCapacity.Text), Convert.ToInt32(lblSumationMaxDesignCapacity.Text));
        lblMaxTotalCapacity.Text = _MemberCapacity.MemberMaxCapacity.ToString();
        if (_MemberCapacity.CurrentObsId == -2 && _MemberCapacity.CurrentDesId == -2
            && _MemberCapacity.CurrentMappingId == -2 && _MemberCapacity.CurrentUrbenismId == -2)
        {
            ShowMessage("شما هیچ یک از صلاحیت  های نظارت / طراحی/نقشه برداری/شهرسازی را نداردید");
            //btnSave.ClientEnabled = false;
            //_HasError = !btnSave.ClientEnabled;
        }
        #region  Municipality Capacity Limitation
        TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager CapacityInMunicipalityManager = new TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager();
        DataTable dtCapacityMun = CapacityInMunicipalityManager.Search(_MemberCapacity.MjParentId, _MemberCapacity.CurrentObsId != -2 ? _MemberCapacity.CurrentObsId : _MemberCapacity.CurrentMappingId != -2 ? _MemberCapacity.CurrentMappingId : -1
            , _MemberCapacity.CurrentDesId == -2 ? -1 : _MemberCapacity.CurrentDesIdInOfficeEngOff, 0, _MemberCapacity.CurrentUrbenismId == -2 ? -1 : _MemberCapacity.CurrentUrbenismId);
        if (dtCapacityMun.Rows.Count > 0)
        {
            txtObsShirazMunicipality.MaskSettings.Mask = "<0" + ".." + dtCapacityMun.Rows[0]["MaxObsCapacity"].ToString() + ">";
            lblObsShirazMunicipalityLimitation.Text = " (حداکثر " + dtCapacityMun.Rows[0]["MaxObsCapacity"].ToString() + ")";
            txtDesignShirazMunicipality.MaskSettings.Mask = "<0" + ".." + dtCapacityMun.Rows[0]["MaxDesCapacity"].ToString() + ">";
            lblDesignShirazMunicipalityLimitation.Text = " (حداکثر " + dtCapacityMun.Rows[0]["MaxDesCapacity"].ToString() + ")";

            txtShirazMunicipulityUrbenismTarh.MaskSettings.Mask = "<0" + ".." + dtCapacityMun.Rows[0]["MaxUrbenismTarhShahrsazi"].ToString() + ">";
            lblShirazMunicipulityUrbenismTarhLimitation.Text = " (حداکثر " + dtCapacityMun.Rows[0]["MaxUrbenismTarhShahrsazi"].ToString() + ")";

            txtShirazMunicipulityUrbenismEntebaghShahri.MaskSettings.Mask = "<0" + ".." + dtCapacityMun.Rows[0]["MaxUrbenismEntebaghShahri"].ToString() + ">";
            lblShirazMunicipulityUrbenismEntebaghShahriLimitation.Text = " (حداکثر " + dtCapacityMun.Rows[0]["MaxUrbenismEntebaghShahri"].ToString() + ")";
        }
        else
        {
            int i = 0;
            txtObsShirazMunicipality.MaskSettings.Mask = "<0" + ".." + lblMaxJobObsCapacity.Text + ">";
            lblObsShirazMunicipalityLimitation.Text = " (حداکثر " + lblMaxJobObsCapacity.Text + ")";
            txtDesignShirazMunicipality.MaskSettings.Mask = "<0" + ".." + lblMaxDesignCapacity.Text + ">";
            lblDesignShirazMunicipalityLimitation.Text = " (حداکثر " + lblMaxDesignCapacity.Text + ")";
            if (int.TryParse(lblMaxJobUrbenismCapacityUrbenismTarh.Text, out i))
                txtShirazMunicipulityUrbenismTarh.MaskSettings.Mask = "<0" + ".." + lblMaxJobUrbenismCapacityUrbenismTarh.Text + ">";
            else
                txtShirazMunicipulityUrbenismTarh.MaskSettings.Mask = "0";
            lblShirazMunicipulityUrbenismTarhLimitation.Text = " (حداکثر " + lblMaxJobUrbenismCapacityUrbenismTarh.Text + ")";

            if (int.TryParse(lblMaxJobUrbenismCapacityEntebaghShahri.Text, out i))
                txtShirazMunicipulityUrbenismEntebaghShahri.MaskSettings.Mask = "<0" + ".." + lblMaxJobUrbenismCapacityEntebaghShahri.Text + ">";
            else
                txtShirazMunicipulityUrbenismEntebaghShahri.MaskSettings.Mask = "0";
            lblShirazMunicipulityUrbenismEntebaghShahriLimitation.Text = " (حداکثر " + lblMaxJobUrbenismCapacityEntebaghShahri.Text + ")";

        }
        #endregion
    }
    #endregion
    /// <summary>
    /// چک کردن ظرفیت شخص و متراژهای شهرداری شیراز و متراژ های مصرف شده در پروژه ها
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="ShirazMunicipalityDesignMeter"></param>
    /// <param name="ShirazMunicipalityObsMeter"></param>
    /// <param name="DesCapacity"></param>
    /// <param name="ObsCapacity"></param>
    private Boolean CheckMemberCapacityUsage(int MeId, int ShirazMunicipalityDesignMeter, int ShirazMunicipalityObsMeter, int DesCapacity, int ObsCapacity,int ObsBonyadMaskan, int DesBonyadMaskan)
    {
        lblWarningCapacity.Text = "";
        Boolean ReturnValue = true;
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        CapacityCalculations CapacityCalculations = new CapacityCalculations();
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementobsShiraz = CapacityCalculations.UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, TSP.DataManager.TSMemberType.Member, false, (int)TSP.DataManager.CityCode.Shiraz, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementdesShiraz = CapacityCalculations.UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, TSP.DataManager.TSMemberType.Member, false, (int)TSP.DataManager.CityCode.Shiraz, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);

        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementobsAll = CapacityCalculations.UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, TSP.DataManager.TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementdesAll = CapacityCalculations.UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, TSP.DataManager.TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);

        double UsedCapacityOtherCitiesDes = UsedCapacityProjectCapacityDecrementdesAll.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementdesShiraz.UsedCapacitySumCapacityDecrement;
        double UsedCapacityOtherCitiesObs = UsedCapacityProjectCapacityDecrementobsAll.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementobsShiraz.UsedCapacitySumCapacityDecrement;
        if (UsedCapacityProjectCapacityDecrementdesShiraz.UsedCapacitySumCapacityDecrement > ShirazMunicipalityDesignMeter)
        {
            lblWarningCapacity.Text += "کارکرد طراحی ثبت شده برای شهر شیراز بیشتر از متراژ انتخابی شما برای طراحی شهرداری شیراز است.";
            ReturnValue = false;
        }
        if (UsedCapacityProjectCapacityDecrementobsShiraz.UsedCapacitySumCapacityDecrement > ShirazMunicipalityObsMeter)
        {
            lblWarningCapacity.Text += "کارکرد نظارت ثبت شده برای شهر شیراز بیشتر از متراژ انتخابی شما برای نظارت شهرداری شیراز است.";
            ReturnValue = false;
        }
        if (UsedCapacityProjectCapacityDecrementdesAll.UsedCapacitySumCapacityDecrement + DesBonyadMaskan > DesCapacity)
        {
            lblWarningCapacity.Text += "کارکرد طراحی ثبت شده و بنیاد مسکن برای این عضویت در سیستم، بیشتر از حداکثر ظرفیت اشتغال طراحی در مدت یک سال است.";
            ReturnValue = false;
        }
        if (UsedCapacityProjectCapacityDecrementobsAll.UsedCapacitySumCapacityDecrement + ObsBonyadMaskan > ObsCapacity)
        {
            lblWarningCapacity.Text += "کارکرد نظارت ثبت شده و بنیاد مسکن برای این عضویت در سیستم، بیشتر از حداکثر ظرفیت اشتغال نظارت در مدت یک سال است.";
            ReturnValue = false;
        }
        if (ShirazMunicipalityDesignMeter > DesCapacity - UsedCapacityOtherCitiesDes )
        {
            lblWarningCapacity.Text += "ظرفیت ثبت شده برای طراحی شهرداری شیراز بیشتر از میزان ظرفیت باقی مانده جهت طراحی شیراز می باشد";
            ReturnValue = false;
        }
        if (ShirazMunicipalityObsMeter > ObsCapacity - UsedCapacityOtherCitiesObs)
        {
            lblWarningCapacity.Text += "ظرفیت ثبت شده برای نظارت شهرداری شیراز بیشتر از میزان ظرفیت باقی مانده جهت نظارت شیراز می باشد";
            ReturnValue = false;
        }
        if (ShirazMunicipalityObsMeter + UsedCapacityOtherCitiesObs > ObsCapacity)
        {
            lblWarningCapacity.Text += "مجموع ظرفیت اختصاص داده شده جهت  نظارت شهرداری شیراز  و کارکرد نظارت در سایر شهر ها بیشتر از حداکثر ظرفیت اشتغال نظارت در مدت یک سال می باشد";
            ReturnValue = false;
        }
        if (ShirazMunicipalityDesignMeter + UsedCapacityOtherCitiesDes > DesCapacity)
        {
            lblWarningCapacity.Text += "مجموع ظرفیت اختصاص داده شده جهت  طراحی شهرداری شیراز  و کارکرد نظارت در سایر شهر ها بیشتر از حداکثر ظرفیت اشتغال طراحی در مدت یک سال می باشد";
            ReturnValue = false;
        }
        if (ShirazMunicipalityObsMeter + ShirazMunicipalityDesignMeter + UsedCapacityOtherCitiesObs + UsedCapacityOtherCitiesDes + ObsBonyadMaskan + DesBonyadMaskan > Math.Max(ObsCapacity, DesCapacity))
        {
            lblWarningCapacity.Text += "مجموع ظرفیت اختصاص داده شده جهت  شهرداری شیراز  و بنیاد مسکن و کارکرد در سایر شهر ها بیشتر از حداکثر ظرفیت اشتغال در مدت یک سال می باشد";
            ReturnValue = false;
        }
        if (ReturnValue == false)
        {
            lblWarningCapacity.Visible = true;
            lblWarningCapacity.Text += "متراژهای ثبت شده جهت شهرداری شیراز را تصحیح نمایید";
        }
        else
            lblWarningCapacity.Visible = false;
        return ReturnValue;
    }
    bool CheckConditions(int MeId)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count == 0)
        {
            ShowMessage("کد عضویت وارد شده معتبر نمی باشد");
            return false;
        }
        int AgentId = Convert.ToInt32(MemberManager[0]["AgentId"]);
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        if (AgentId == Utility.GetCurrentAgentCode())
            CapacityAssignmentManager.SelectCurrentYearAndStage(1);
        else
            CapacityAssignmentManager.SelectCurrentYearAndStage(0);
        if (CapacityAssignmentManager.Count > 0)
        {
            _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
            _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
        }
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        System.Collections.ArrayList Result = ObserverWorkRequestManager.CheckConditionForNewObsWorkRequest(MeId, _CurrentCapacityEndate, -1);
        if (!Convert.ToBoolean(Result[0]))
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee && Convert.ToInt32(Result[5]) == (int)TSP.DataManager.TechnicalServices.TSWorkRequestConditionErrorType.DocumentExipration)
            {
                lblWarning.Visible = true;
                lblWarning.InnerText = "هشدار:" + Result[1].ToString();
            }
            else
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }

        _MfId = Convert.ToInt32(Result[2]);
        if (_MfId == -2)
        {
            ShowMessage("خطا در بازیابی اطلاعات پروانه اشتغال شما ایجاد شده است");
            return false;

        }
        _DocMeFileExpireDate = Result[3].ToString();
        return true;
    }

    #region Clear Form
    private void ClearFormBasicInfo()
    {
        txtStartOffDate.Text =
        txtEndOffDate.Text =
        lblMeName.Text =
        lblMeLastName.Text =
        txtMembershipDate.Text =
        txtMFNo.Text =
        txtMemberFileMajor.Text =
        txtExpireDateMember.Text =
        txtObsName.Text =
        txtObsDate.Text =
        txtMappingDate.Text =
        txtMappingName.Text =
        txtDesignDate.Text =
        txtDesignName.Text =
        txtUrbenismName.Text =
        txtUrbenismDate.Text =
        lblHasGasCert.Text = "";
    }
    private void ClearForm()
    {
        RoundPanelRules.ClientVisible = false;
        CheckBoxWantShahrakSanati.Checked = CheckBoxIsFullTimeWorker.Checked =
      CheckBoxWantEghdamMeliMaskan.Checked = CheckBoxWantCharity.Checked = false;
        comboCity1.SelectedIndex = comboCity2.SelectedIndex = -1;
        txtMappingDate.Text =
        txtMappingName.Text =
        txtDesignDate.Text =
        txtDesignName.Text =
        txtObsName.Text =
        txtObsDate.Text =
        txtMemberFileMajor.Text =
        lblComperChangesObsShirazMunicipality.Text =
        lblComperChangesObsBonyadMaskan.Text =
        lblComperChangesDesignShirazMunicipality.Text =
        lblComperChangesDesignBonyadMaskan.Text =
        lblComperChangesShirazMunicipulityUrbenismTarh.Text =
        lblComperChangesShirazMunicipulityUrbenismEntebaghShahri.Text = "";
        txtObsShirazMunicipality.Text =
        txtBonyadMaskan.Text =
        txtDesignShirazMunicipality.Text = txtDesignBonyadMaskan.Text = txtShirazMunicipulityUrbenismEntebaghShahri.Text = txtShirazMunicipulityUrbenismTarh.Text = "0";
    }
    public void ClearAndResetForm()
    {
        ClearFormBasicInfo();
        ClearForm();
        SetRoundPanelVisible(false, false);
    }
    public void ResetPropertiesAndHidenFields()
    {
        _AgentIdMain = Utility.GetCurrentAgentCode();
        #region Initialize Properties
        _MemberCapacity = new CapacityCalculations.MemberCapacity();
        _MaxObsJobCapacity = _MaxJobCount = 0;
        _DocMeFileExpireDate = ""; _ErrorMessage = "";
        _HasEngOffice = _IsEngOfficeIsExpired = _HasOffice = _HasEfficientGrade = _IsOfficeIsExpired = false; _HasError = false; _ErrorMessageVisible = false;
        _CurrentObsId = _CurrentDesId = _EngOfId = _OfId = _MfId = _CurrentCapacityAssignmentId = _MfMjParentId = _AgentId = _CurrentUrbId = _CurrentDesIdInOfficeEngOff = -2;
        #endregion

        HiddenFieldPage["LastObsBonyadMaskan"] =
        HiddenFieldPage["LastObsShirazMunicipality"] =
        HiddenFieldPage["LastDesignShirazMunicipality"] =
        HiddenFieldPage["LastDesignBonyadMaskan"] =
        HiddenFieldPage["LastMunicipulityUrbTarh"] =
        HiddenFieldPage["LastMunicipulityUrbEntebaghShahri"] = "0";
    }
    #endregion

    private void SetControlsEnabled(Boolean Enabled)
    {
        RoundPanelMajor.ClientEnabled =
        RoundPanelCity.ClientEnabled =
        /////
        CheckBoxWantObserverSelect.ClientEnabled =
        checkboxRulls.ClientEnabled =
        /////
        RoundPanelPrjTypes.ClientEnabled =
        RoundPanelBasicCapacityInfo.ClientEnabled =
        RoundPanelObserveCapacity.ClientEnabled =
        RoundPanelDesignCapacity.ClientEnabled =
        RounPanelUrbenismCapacity.ClientEnabled =
        flpAttach.ClientEnabled =
        RoundPanelOffRequest.Enabled =
        flpObsCommitmentForm.ClientVisible =
        Enabled;
        RoundPanelCommitMuniciToll.ClientEnabled = false;
    }
    private void SetComperVisible(Boolean Visibled)
    {
        if (_PageMode == "New")
            Visibled = false;
        lblComperChangesObsShirazMunicipality.ClientVisible =
        lblComperChangesDesignShirazMunicipality.ClientVisible =
        lblComperChangesShirazMunicipulityUrbenismTarh.ClientVisible =
        lblComperChangesShirazMunicipulityUrbenismEntebaghShahri.ClientVisible =
        lblComperChangesObsBonyadMaskan.ClientVisible =
        lblComperChangesDesignBonyadMaskan.ClientVisible = Visibled;
    }
    private void SetComperBonyadVisible(Boolean Visibled)
    {
        if (_PageMode == "New")
            Visibled = false;
        lblComperChangesObsBonyadMaskan.ClientVisible =
        lblComperChangesDesignBonyadMaskan.ClientVisible = Visibled;
    }
    private bool CheckBonyadAndMunici()
    {
        //**اگر تاریخ عدم اجبار نگذشته باشد،نیاز به ارسال به کارشناس خدمات مهندسی نمی باشد
        if (_StopmandatoryFileUploading == 1) return false;
        //*************************************************
        int DesignShirazMunicipality = 0;
        int DesignBonyadMaskan = 0;
        int ObsShirazMunicipality = 0;
        int ObsBonyadMaskan = 0;
        int ShirazMunicipulityUrbenismTarh = 0;
        int ShirazMunicipulityUrbenismEntebaghShahri = 0;

        int.TryParse(txtDesignShirazMunicipality.Text, out DesignShirazMunicipality);
        int.TryParse(txtDesignBonyadMaskan.Text, out DesignBonyadMaskan);
        int.TryParse(txtObsShirazMunicipality.Text, out ObsShirazMunicipality);
        int.TryParse(txtBonyadMaskan.Text, out ObsBonyadMaskan);
        int.TryParse(txtShirazMunicipulityUrbenismTarh.Text, out ShirazMunicipulityUrbenismTarh);
        int.TryParse(txtShirazMunicipulityUrbenismEntebaghShahri.Text, out ShirazMunicipulityUrbenismEntebaghShahri);

        int SubDesignMunicipalityChange = DesignShirazMunicipality - Convert.ToInt32(HiddenFieldPage["LastDesignShirazMunicipality"]);
        int SubDesignBonyadMaskan = DesignBonyadMaskan - Convert.ToInt32(HiddenFieldPage["LastDesignBonyadMaskan"]);
        int SubObsMunicipalityChange = ObsShirazMunicipality - Convert.ToInt32(HiddenFieldPage["LastObsShirazMunicipality"]);
        int SubObsBonyadMaskan = ObsBonyadMaskan - Convert.ToInt32(HiddenFieldPage["LastObsBonyadMaskan"]);
        int SubShirazMunicipulityUrbenismTarh = ShirazMunicipulityUrbenismTarh - Convert.ToInt32(HiddenFieldPage["LastMunicipulityUrbTarh"]); ;
        int SubShirazMunicipulityUrbenismEntebaghShahri = ShirazMunicipulityUrbenismEntebaghShahri - Convert.ToInt32(HiddenFieldPage["LastMunicipulityUrbEntebaghShahri"]); ;
        if (SubObsBonyadMaskan < 0 || SubDesignBonyadMaskan < 0 || SubShirazMunicipulityUrbenismEntebaghShahri < 0 || SubShirazMunicipulityUrbenismTarh < 0 || SubObsMunicipalityChange < 0 || SubDesignMunicipalityChange < 0)
            return true;
        else
            return false;
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

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeId" + (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member ? Utility.GetCurrentUser_MeId().ToString() : _MeId.ToString()) + "-" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Members/RequestWork/") + ret) == true);
            string FullFilePath = "~/Image/Members/RequestWork/" + ret;
            uploadedFile.SaveAs(MapPath(FullFilePath), true);
            _FullFilePath = FullFilePath;
        }
        return ret;
    }
    protected string SaveImageObsCommitmentForm(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "MeId" + (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member ? Utility.GetCurrentUser_MeId().ToString() : _MeId.ToString()) + "-" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Members/RequestWork/ObsCommitmentForm/") + ret) == true);
            string FullFilePathObsCommitmentForm = "~/Image/Members/RequestWork/ObsCommitmentForm/" + ret;
            uploadedFile.SaveAs(MapPath(FullFilePathObsCommitmentForm), true);
            _FullFilePathObsCommitmentForm = FullFilePathObsCommitmentForm;
        }
        return ret;
    }

    private void SetUploadControlVisibeld(DataTable dtComperChange)
    {
        int SubBonyadMaskan = 0;
        int SubShirazMunicipalityMeter = 0;
        int SubShirazMunicipalityDesignMeter = 0;
        int SubBonyadMaskanDesignMeter = 0;
        int SubShirazMunicipulityUrbenismTarh = 0;
        int SubShirazMunicipulityUrbenismEntebaghShahri = 0;

        if (dtComperChange.Rows.Count == 1 && _StopmandatoryFileUploading == 0)
        {
            if (!Utility.IsDBNullOrNullValue(dtComperChange.Rows[0]["SubBonyadMaskan"]))
                SubBonyadMaskan = Convert.ToInt32(dtComperChange.Rows[0]["SubBonyadMaskan"]);

            if (!Utility.IsDBNullOrNullValue(dtComperChange.Rows[0]["SubShirazMunicipalityMeter"]))
                SubShirazMunicipalityMeter = Convert.ToInt32(dtComperChange.Rows[0]["SubShirazMunicipalityMeter"]);

            if (!Utility.IsDBNullOrNullValue(dtComperChange.Rows[0]["SubShirazMunicipalityDesignMeter"]))
                SubShirazMunicipalityDesignMeter = Convert.ToInt32(dtComperChange.Rows[0]["SubShirazMunicipalityDesignMeter"]);

            if (!Utility.IsDBNullOrNullValue(dtComperChange.Rows[0]["SubBonyadMaskanDesignMeter"]))
                SubBonyadMaskanDesignMeter = Convert.ToInt32(dtComperChange.Rows[0]["SubBonyadMaskanDesignMeter"]);

            if (!Utility.IsDBNullOrNullValue(dtComperChange.Rows[0]["SubShirazMunicipulityUrbenismTarh"]))
                SubShirazMunicipulityUrbenismTarh = Convert.ToInt32(dtComperChange.Rows[0]["SubShirazMunicipulityUrbenismTarh"]);

            if (!Utility.IsDBNullOrNullValue(dtComperChange.Rows[0]["SubShirazMunicipulityUrbenismEntebaghShahri"]))
                SubShirazMunicipulityUrbenismEntebaghShahri = Convert.ToInt32(dtComperChange.Rows[0]["SubShirazMunicipulityUrbenismEntebaghShahri"]);

            if (SubBonyadMaskan < 0 || SubShirazMunicipalityMeter < 0 || SubShirazMunicipalityDesignMeter < 0 || SubBonyadMaskanDesignMeter < 0 || SubShirazMunicipulityUrbenismTarh < 0 || SubShirazMunicipulityUrbenismEntebaghShahri < 0)
            {
                RoundPanelUploadControl.ClientVisible = true;
                HiddenFieldPage["MustHasAttach"] = true;
            }
            else
            {
                RoundPanelUploadControl.ClientVisible = false;
                HiddenFieldPage["MustHasAttach"] = false;
            }
        }
        else
        {
            RoundPanelUploadControl.ClientVisible = false;
            HiddenFieldPage["MustHasAttach"] = false;
        }
    }

    #region Insert - Update
    private Boolean Insert(Boolean SendInfoToShahrdari)
    {
        if (_MemberCapacity.CurrentObsId == -2 && _MemberCapacity.CurrentDesId != -2)
        {
            if (!_HasEngOffice && !(_HasOffice && _HasEfficientGrade))
            {
                ShowMessage("امکان ثبت آماده به کاری وجود ندارد.شما تنها دارای پایه طراحی بوده و عضو  هیچ دفتر طراحی و یا شرکت طراح و ناظر نمی باشید");
                return false;
            }
        }
        if (_AgentId == Utility.GetCurrentAgentCode())
        {
            if (_NeedTaahod && !CheckBoxCommitMuniciToll.Checked)
            {
                ShowMessage("لطفا تعهد پرداخت عوارض شهرداری شیراز را انتخاب نمایید");
                return false;
            }
        }
        if (Utility.IsDBNullOrNullValue(comboCity1.Value) && Convert.ToInt32(txtObsShirazMunicipality.Text) != 0)
        {
            ShowMessage("با توجه به مشخص کردن متراژ های نظارت لطفا شهر محل نظارت را انتخاب نمایید");
            return false;

        }
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager TSObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateManager = new TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(TSObserverWorkRequestManager);
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            if (!CheckConditions(_MeId))
                return false;

            if (Utility.IsDBNullOrNullValue(_FullFilePathObsCommitmentForm) && _CurrentObsId == (int)TSP.DataManager.DocumentGrads.Grade3 && _AgentId != _AgentIdMain)
            {
                ShowMessage("بار گذاری فرم تعهد نظارت برای اعضای دارای پایه سه نظارت اجباری می باشد");
                return false;
            }

            if (Convert.ToInt32(txtBonyadMaskan.Text) + Convert.ToInt32(txtDesignBonyadMaskan.Text)
            + Convert.ToInt32(txtObsShirazMunicipality.Text) + Convert.ToInt32(txtDesignShirazMunicipality.Text)
            > Convert.ToInt32(lblMaxTotalCapacity.Text))
            {
                ShowMessage("مجموع متراژ گروه شهر های حومه و شهرداری شیراز و بنیاد مسکن نباید از حداکثر مجموع ظرفیت طراحی و نظارت در برش زمانی(" + lblMaxTotalCapacity.Text + ")بیشتر باشد");
                return false;
            }
            if (Convert.ToInt32(txtBonyadMaskan.Text) + Convert.ToInt32(txtObsShirazMunicipality.Text)
              > Convert.ToInt32(lblMaxJobObsCapacity.Text))
            {
                ShowMessage("مجموع متراژ نظارت گروه شهر های حومه و شهرداری شیراز و بنیاد مسکن نباید از حداکثر مجموع نظارت در برش زمانی(" + lblMaxJobObsCapacity.Text + ")بیشتر باشد");
                return false;
            }
            if (Convert.ToInt32(txtDesignBonyadMaskan.Text) + Convert.ToInt32(txtDesignShirazMunicipality.Text)
              > Convert.ToInt32(lblMaxDesignCapacity.Text))
            {
                ShowMessage("مجموع متراژ طراحی شهرداری شیراز و بنیاد مسکن نباید از حداکثر مجموع ظرفیت طراحی در برش زمانی(" + lblMaxDesignCapacity.Text + ")بیشتر باشد");
                return false;
            }

            int SaveInfoTaskId = -1;
            int StateTaskId = -2;
            string StateDescription = "";
            int ConfirmTSWorkRequestConfirminAndEndProccessTaskId = -1;
            int TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId = -1;
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo);
            if (WorkFlowTaskManager.Count > 0)
            {
                SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ConfirmTSWorkRequestConfirminAndEndProccess);
            if (WorkFlowTaskManager.Count > 0)
            {
                ConfirmTSWorkRequestConfirminAndEndProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.TSUnitEmployeeConfirminConfirmingTSWorkRequest);
            if (WorkFlowTaskManager.Count > 0)
            {
                TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }
            TransactionManager.BeginSave();
            #region InsertWorkRequest
            DataRow dr = TSObserverWorkRequestManager.NewRow();

            dr["MeId"] = _MeId;
            dr["MfId"] = _MfId;
            if (!Utility.IsDBNullOrNullValue(_FullFilePathObsCommitmentForm))
                dr["UrlObserverCommitmentForm"] = _FullFilePathObsCommitmentForm;

            dr["ObsId"] = _CurrentObsId;
            if (!string.IsNullOrWhiteSpace(txtObsDate.Text))
                dr["ObsDate"] = txtObsDate.Text;
            else
                dr["ObsDate"] = txtMappingDate.Text;
            if (_CurrentDesId != -2 && _CurrentObsId != -2)
                dr["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.ObseverAndDesign;
            else if (_CurrentDesId == -2 && _CurrentObsId != -2)
                dr["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.Obsever;
            else if (_CurrentDesId != -2 && _CurrentObsId == -2)
                dr["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.Design;
            if (!CheckBoxWantObserverSelect.Checked)//آیا تمایل به ارجاع کار نظارت دارید؟
                dr["IsObserverOff"] = true;
            else
                dr["IsObserverOff"] = false;

            dr["MembershipDate"] = txtMembershipDate.Text;

            dr["DocMeFileExpireDate"] = _DocMeFileExpireDate;
            dr["MasterMfMjParentId"] = _MfMjParentId;
            dr["MfMjId"] = 0;// _MfMjId;

            dr["Group1"] = false;
            dr["Group2"] = false;
            dr["Group3"] = false;
            dr["Group4"] = false;
            for (int i = 0; i < CheckListStructureGroups.Items.Count; i++)
            {
                if (CheckListStructureGroups.Items[i].Selected)
                {
                    switch (Convert.ToInt32(CheckListStructureGroups.Items[i].Value))
                    {
                        case 1:
                            dr["Group1"] = true;
                            break;
                        case 2:
                            dr["Group2"] = true;
                            break;
                        case 3:
                            dr["Group3"] = true;
                            break;
                        case 4:
                            dr["Group4"] = true;
                            break;
                    }
                }
            }
            if (comboCity1.SelectedItem.Value != null)
            {
                dr["CapacityAssignmentId"] = _CurrentCapacityAssignmentId;
                dr["City1"] = comboCity1.SelectedItem.Value;
            }
            else
                dr["City1"] = DBNull.Value;
            if (comboCity2.SelectedItem != null && comboCity2.SelectedItem.Value != null && Convert.ToInt32(comboCity1.SelectedItem.Value) != (int)TSP.DataManager.CityCode.Kharameh && Convert.ToInt32(comboCity1.SelectedItem.Value) != (int)TSP.DataManager.CityCode.Sarvestan)
                dr["City2"] = comboCity2.SelectedItem.Value;
            else
                dr["City2"] = DBNull.Value;
            if (_AgentId == Utility.GetCurrentAgentCode() && comboCity1.SelectedItem != null)
            {
                dr["CityLastChangedWorkYear"] = _CurrentCapacityYear;
            }
            else
            {
                dr["CityLastChangedWorkYear"] = "";
            }
            dr["MeAgentId"] = _AgentId;
            dr["StartOffDate"] = "";
            dr["EndOffDate"] = "";
            //**************************
            dr["WantCharityWork"] = CheckBoxWantCharity.Checked;
            dr["WantEghdamMeliMaskan"] = CheckBoxWantEghdamMeliMaskan.Checked;
            dr["IsFullTimeWorker"] = CheckBoxIsFullTimeWorker.Checked;
            dr["WantShahrakSanatiMeter"] = CheckBoxWantShahrakSanati.Checked;

            //***********ظرفیت ها***************            
            dr["ShahrakSanatiMeter"] = 0;//به صورت تیک شد و متراژ وارد نمی شود
            dr["TotalCapacity"] = lblMaxTotalCapacity.Text;
            #region نظارت

            dr["CapacityObs"] = lblMaxJobObsCapacity.Text;//ظرفیت کل پایه نظارت 
            dr["ShirazMunicipalityMeter"] = txtObsShirazMunicipality.Text;//شهرداری شیراز
            dr["BonyadMaskan"] = txtBonyadMaskan.Text;//بنیاد مسکن         
            #endregion
            #region طراحی
            dr["CapacityDesign"] = lblMaxDesignCapacity.Text;//ظرفیت کل پایه طراحی            
            dr["ShirazMunicipalityDesignMeter"] = txtDesignShirazMunicipality.Text;
            dr["BonyadMaskanDesignMeter"] = txtDesignBonyadMaskan.Text;
            #endregion
            #region شهرسازی
            dr["ShirazMunicipulityUrbenismTarh"] = txtShirazMunicipulityUrbenismTarh.Text;
            dr["ShirazMunicipulityUrbenismEntebaghShahri"] = txtShirazMunicipulityUrbenismEntebaghShahri.Text;
            #endregion
            #region ظرفیت مصرف شده
            //***ظرفیت مصرف شده هنگام ثبت آماده بکاری جدید= بنیاد مسکن طراحی + بنیاد مسکن نظارت               
            dr["UsedCapacity"] = Convert.ToInt32(txtBonyadMaskan.Text) + Convert.ToInt32(txtDesignBonyadMaskan.Text);
            dr["UsedCapacityDesShirazMun"] =
            dr["UsedCapacityDesOtherCities"] =
            dr["UsedCapacityObsShiraz"] =
            dr["UsedCapacityObsSadra"] =
            dr["UsedCapacityObsLapooy"] =
            dr["UsedCapacityObsKhanZenyan"] =
            dr["UsedCapacityObsDareyon"] =
            dr["UsedCapacityObsZarghan"] =
            dr["UsedCapacityObsOtherCities"] =
            dr["UsedCapacityUrbenismTarhShirazMun"] =
            dr["UsedCapacityEntebaghShahriShirazMun"] =
            dr["UsedCapacityUrbenismTarhOtherCities"] =
            dr["UsedCapacityEntebaghShahriOtherCities"] =
            dr["UsedCapacityCharity"] =
            dr["CountInproccesWorksObs"] =
            dr["CountInproccesWorksDesign"] = 0;
            //چون  مصرفی شهر ها در ابتدا صفر هستند با مصرفی کل جمع نزدیم
            #endregion

            #region  ظرفیت باقیمانده
            //***ظرفیت باقیمانده=
            //متراژ کل - ( بنیاد مسکن طراحی و نظارت)یا
            //متراژ کل -متراژ مصرف شده
            dr["RemainCapacity"] = Convert.ToInt32(lblMaxTotalCapacity.Text) - Convert.ToInt32(dr["UsedCapacity"]);
            dr["RemainCapacityObs"] = Convert.ToInt32(lblMaxJobObsCapacity.Text) - Convert.ToInt32(txtBonyadMaskan.Text);
            dr["RemainCapacityDesign"] = Convert.ToInt32(lblMaxDesignCapacity.Text) - Convert.ToInt32(txtDesignBonyadMaskan.Text);
            //***min((maxobs-usedObs),(MaxTotalCapacity-UsedObs-UsedDes))
            dr["RemainCapacityObsReal"] = Math.Min(Convert.ToInt32(lblMaxJobObsCapacity.Text) - (Convert.ToInt32(txtBonyadMaskan.Text) + Convert.ToInt32(txtObsShirazMunicipality.Text))
                , Convert.ToInt32(lblMaxTotalCapacity.Text) -
                (Convert.ToInt32(txtBonyadMaskan.Text) + Convert.ToInt32(txtObsShirazMunicipality.Text) + Convert.ToInt32(txtDesignBonyadMaskan.Text) + Convert.ToInt32(txtDesignShirazMunicipality.Text)));

            #endregion
            if ((!Utility.IsDBNullOrNullValue(txtBonyadMaskan.Text) && txtBonyadMaskan.Text != "0") || (!Utility.IsDBNullOrNullValue(txtObsShirazMunicipality.Text) && txtObsShirazMunicipality.Text != "0"))
                dr["PercentOfCapacityUsage"] = (Convert.ToInt32(txtBonyadMaskan.Text) + Convert.ToInt32(txtObsShirazMunicipality.Text)) / Convert.ToDouble(lblMaxJobObsCapacity.Text);
            else
                dr["PercentOfCapacityUsage"] = 0;
            //***************طراحی***********
            //if (_CurrentUrbId != -2)
            //    dr["CapacityDesign"] = lblMaxDesignCapacity.Text;
            //else
            dr["CountRemainWorkCount"] = lblMaxJobCount.Text;
            dr["CountUnder400MeterWork"] = 0;
            dr["CountUnder400MeterWorkDesign"] = 0;
            dr["CountWorks"] = lblMaxJobCount.Text;
            dr["CountInproccesWorks"] = 0;
            dr["CountRandomSelected"] = 0;
            dr["CountRejectByObs"] = 0;
            dr["CreateDate"] = Utility.GetDateOfToday();

            if (_CurrentObsId == (int)TSP.DataManager.DocumentGrads.Grade3 && _AgentId != _AgentIdMain && Utility.GetCurrentUser_NmcIdType() == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId)
            {
                StateDescription = "ارسال اتوماتیک درخواست تغییرات آماده به کاری توسط عضو به کارشناس خدمات مهندسی"; ;
                StateTaskId = TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId;
                dr["CurrentWfTasId"] = TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId;
                dr["Status"] = TSP.DataManager.TSObserverWorkRequestStatus.Pending;
            }
            else
            {
                StateDescription = "تایید اتوماتیک درخواست آماده به کاری توسط سیستم";
                StateTaskId = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
                dr["CurrentWfTasId"] = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
                dr["Status"] = TSP.DataManager.TSObserverWorkRequestStatus.Confirm;
            }
            dr["CurrentWfStateId"] = DBNull.Value;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["HasGasCert"] = _MemberCapacity.HasGasCert;
            TSObserverWorkRequestManager.AddRow(dr);
            TSObserverWorkRequestManager.Save();
            TSObserverWorkRequestManager.DataTable.AcceptChanges();
            int ObsWorkReqId = Convert.ToInt32(TSObserverWorkRequestManager[0]["ObsWorkReqId"]);
            #endregion
            #region InsertChange
            DataRow drChange = ObserverWorkRequestChangesManager.NewRow();
            drChange["Type"] = (int)TSP.DataManager.TSObserverWorkRequestChangeType.New;
            drChange["ObsWorkReqId"] = ObsWorkReqId;
            drChange["MeId"] = _MeId;
            drChange["MfId"] = _MfId;
            drChange["ObsId"] = _CurrentObsId;
            if (!Utility.IsDBNullOrNullValue(_FullFilePathObsCommitmentForm))
                drChange["UrlObserverCommitmentForm"] = _FullFilePathObsCommitmentForm;
            if (!string.IsNullOrWhiteSpace(txtObsDate.Text))
                drChange["ObsDate"] = txtObsDate.Text;
            else
                drChange["ObsDate"] = txtMappingDate.Text;
            drChange["MembershipDate"] = txtMembershipDate.Text;
            drChange["Group1"] = false;
            drChange["Group2"] = false;
            drChange["Group3"] = false;
            drChange["Group4"] = false;
            for (int i = 0; i < CheckListStructureGroups.Items.Count; i++)
            {
                if (CheckListStructureGroups.Items[i].Selected)
                {
                    switch (Convert.ToInt32(CheckListStructureGroups.Items[i].Value))
                    {
                        case 1:
                            drChange["Group1"] = true;
                            break;
                        case 2:
                            drChange["Group2"] = true;
                            break;
                        case 3:
                            drChange["Group3"] = true;
                            break;
                        case 4:
                            drChange["Group4"] = true;
                            break;
                    }
                }
            }
            if (comboCity1.SelectedItem.Value != null)
            {
                drChange["City1"] = comboCity1.SelectedItem.Value;
                drChange["CapacityAssignmentId"] = _CurrentCapacityAssignmentId;
            }
            else
                drChange["City1"] = DBNull.Value;
            if (comboCity2.SelectedItem != null && comboCity2.SelectedItem.Value != null && Convert.ToInt32(comboCity1.SelectedItem.Value) != (int)TSP.DataManager.CityCode.Kharameh && Convert.ToInt32(comboCity1.SelectedItem.Value) != (int)TSP.DataManager.CityCode.Sarvestan)
                drChange["City2"] = comboCity2.SelectedItem.Value;
            else
                drChange["City2"] = DBNull.Value;

            drChange["MeAgentId"] = _AgentId;
            if (!CheckBoxWantObserverSelect.Checked)//آیا تمایل به ارجاع کار نظارت دارید؟
                drChange["IsObserverOff"] = true;
            else
                drChange["IsObserverOff"] = false;
            drChange["StartOffDate"] = "";
            drChange["EndOffDate"] = "";
            drChange["Description"] = "";

            drChange["DocMeFileExpireDate"] = _DocMeFileExpireDate;
            drChange["MasterMfMjParentId"] = _MfMjParentId;
            drChange["MfMjId"] = 0;// _MfMjId;
            drChange["WantCharityWork"] = CheckBoxWantCharity.Checked;
            drChange["WantEghdamMeliMaskan"] = CheckBoxWantEghdamMeliMaskan.Checked;
            drChange["WantShahrakSanatiMeter"] = CheckBoxWantShahrakSanati.Checked;
            drChange["IsFullTimeWorker"] = CheckBoxIsFullTimeWorker.Checked;
            drChange["CommitMuniciToll"] = CheckBoxCommitMuniciToll.Checked;
            drChange["ShahrakSanatiMeter"] = 0;
            drChange["BonyadMaskan"] = txtBonyadMaskan.Text;
            drChange["BonyadMaskanDesignMeter"] = txtDesignBonyadMaskan.Text;
            drChange["ShirazMunicipalityMeter"] = txtObsShirazMunicipality.Text;
            drChange["ShirazMunicipalityDesignMeter"] = txtDesignShirazMunicipality.Text;
            drChange["ShirazMunicipulityUrbenismTarh"] = txtShirazMunicipulityUrbenismTarh.Text;
            drChange["ShirazMunicipulityUrbenismEntebaghShahri"] = txtShirazMunicipulityUrbenismEntebaghShahri.Text;
            drChange["CapacityDesign"] = lblMaxDesignCapacity.Text;//ظرفیت کل پایه طراحی    
            drChange["CapacityObs"] = lblMaxJobObsCapacity.Text;//ظرفیت کل پایه نظارت  
            drChange["TotalCapacity"] = lblMaxTotalCapacity.Text;

            drChange["CountWorks"] = lblMaxJobCount.Text;
            //drChange["CountInproccesWorks"] = 0;
            //??????????????
            ////if (RadioButtonListWantedWorkType.SelectedItem != null)
            if (_CurrentDesId != -2 && _CurrentObsId != -2)
                drChange["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.ObseverAndDesign;
            else if (_CurrentDesId == -2 && _CurrentObsId != -2)
                drChange["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.Obsever;
            else if (_CurrentDesId != -2 && _CurrentObsId == -2)
                drChange["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.Design;

            if (_CurrentObsId == (int)TSP.DataManager.DocumentGrads.Grade3 && _AgentId != _AgentIdMain && Utility.GetCurrentUser_NmcIdType() == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId)
            {
                drChange["CurrentWfTasId"] = TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId;
                drChange["IsConfirm"] = 0;
            }

            else
            {
                drChange["CurrentWfTasId"] = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
                drChange["IsConfirm"] = 1;
            }
            drChange["CurrentWfStateId"] = DBNull.Value;


            drChange["CreateDate"] = Utility.GetDateOfToday();
            drChange["UserId"] = Utility.GetCurrentUser_UserId();
            drChange["ModifiedDate"] = DateTime.Now;
            drChange["HasGasCert"] = _MemberCapacity.HasGasCert;
            ObserverWorkRequestChangesManager.AddRow(drChange);
            ObserverWorkRequestChangesManager.Save();
            ObserverWorkRequestChangesManager.DataTable.AcceptChanges();
            int TableId = int.Parse(ObserverWorkRequestChangesManager[ObserverWorkRequestChangesManager.Count - 1]["ObsWorkReqChangeId"].ToString());
            #endregion
            #region WorkFlow                 
            int NmcId = _MeId;
            if (NmcId <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return false;
            }


            int StateId1 = WorkFlowStateManager.SendDocToNextStep(TableId, SaveInfoTaskId, "ثبت درخواست آماده به کاری توسط عضو", NmcId, Utility.GetCurrentUser_NmcIdType(), Utility.GetCurrentUser_UserId(), 0, Utility.GetDateOfToday());
            if (StateId1 <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return false;
            }

            int StateId2 = WorkFlowStateManager.SendDocToNextStep(TableId, StateTaskId, StateDescription, NmcId, Utility.GetCurrentUser_NmcIdType(), Utility.GetCurrentUser_UserId(), 0, Utility.GetDateOfToday());
            if (StateId2 <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return false;
            }

            TSObserverWorkRequestManager[0].BeginEdit();
            TSObserverWorkRequestManager[0]["CurrentWfStateId"] = StateId2;
            TSObserverWorkRequestManager[0].EndEdit();
            TSObserverWorkRequestManager.Save();
            ObserverWorkRequestChangesManager[0].BeginEdit();
            ObserverWorkRequestChangesManager[0]["CurrentWfStateId"] = StateId2;
            ObserverWorkRequestChangesManager[0].EndEdit();
            ObserverWorkRequestChangesManager.Save();
            #endregion
            _PageMode = "View";
            _ObsWorkReqChangeId = TableId;
            _RoundPanelPageHeader = "مشاهده آماده به کاری";// + CapacityAssignmentManager[0]["Year"].ToString();
            TransactionManager.EndSave();
            ShowMessage("ذخیره انجام گرفت.");
            //btnSave.ClientEnabled = false;
            SetViewModeKeys();
            if (SendInfoToShahrdari)
            {
                if (Utility.IsWorkREquestInfoSendToShahrdari())
                {
                    if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                    {
                        ShahrdariWebservice(_MfMjParentId, txtObsDate.Text, txtDesignDate.Text, txtUrbenismDate.Text, txtMappingDate.Text
                          , Convert.ToDecimal(txtObsShirazMunicipality.Text)
                         , Convert.ToDecimal(txtDesignShirazMunicipality.Text)
                         , Convert.ToDecimal(txtShirazMunicipulityUrbenismTarh.Text) + Convert.ToDecimal(txtShirazMunicipulityUrbenismEntebaghShahri.Text), Convert.ToInt16(_CurrentCapacityYear), true);
                    }
                }
            }
            return true;

        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();

            Utility.SaveWebsiteError(ex);
            return false;
        }
    }
    private Boolean InsertNewRequest(TSP.DataManager.TSObserverWorkRequestChangeType TSObserverWorkRequestChangeType, Boolean SendInfoToShahrdari)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateManager = new TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager TSObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TransactionManager.Add(ProjectCapacityDecrementManager);
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TSObserverWorkRequestManager);

        try
        {
            if (!CheckMemberCapacityUsage(_MeId, Convert.ToInt32(txtDesignShirazMunicipality.Text), Convert.ToInt32(txtObsShirazMunicipality.Text), Convert.ToInt32(lblMaxDesignCapacity.Text), Convert.ToInt32(lblMaxJobObsCapacity.Text), Convert.ToInt32(txtBonyadMaskan.Text), Convert.ToInt32(txtDesignBonyadMaskan.Text)))
            {
                ShowMessage(lblWarningCapacity.Text);
                return false;
            }
            CapacityCalculations CapacityCalculations = new CapacityCalculations();

            if (TSObserverWorkRequestChangeType == TSP.DataManager.TSObserverWorkRequestChangeType.Off)
            {
                if (string.IsNullOrWhiteSpace(txtStartOffDate.Text) || string.IsNullOrWhiteSpace(txtEndOffDate.Text))
                {
                    ShowMessage("تاریخ شروع و پایان مرخصی نمی تواند خالی باشد");
                    return false;

                }
                if (string.Compare(Utility.GetDateOfToday(), txtStartOffDate.Text) > 0)
                {
                    ShowMessage("تاریخ شروع مرخصی نمی تواند قبل از روز جاری باشد");
                    return false;
                }
                if (string.Compare(txtStartOffDate.Text, txtEndOffDate.Text) > 0)
                {
                    ShowMessage("تاریخ پایان مرخصی نمی تواند قبل از تاریخ شروع مرخصی باشد");
                    return false;
                }
            }

            if (_MemberCapacity.CurrentObsId == -2 && _MemberCapacity.CurrentDesId != -2)
            {
                if (!_HasEngOffice && !!(_HasOffice && _HasEfficientGrade))
                {
                    ShowMessage("امکان ثبت آماده به کاری وجود ندارد.شما تنها دارای پایه طراحی بوده و عضو  هیچ دفتر طراحی و یا شرکت طراح و ناظر نمی باشید");
                    return false;
                }
            }
            if (Utility.IsDBNullOrNullValue(comboCity1.Value) && Convert.ToInt32(txtObsShirazMunicipality.Text) != 0
                )
            {
                ShowMessage("با توجه به مشخص کردن متراژ های نظارت لطفا شهر محل نظارت را انتخاب نمایید");
                return false;

            }
            if (Convert.ToInt32(txtBonyadMaskan.Text) + Convert.ToInt32(txtDesignBonyadMaskan.Text)
               + Convert.ToInt32(txtObsShirazMunicipality.Text) + Convert.ToInt32(txtDesignShirazMunicipality.Text)
               > Convert.ToInt32(lblMaxTotalCapacity.Text))
            {
                ShowMessage("مجموع متراژ گروه شهر های حومه و شهرداری شیراز و بنیاد مسکن نباید  از حداکثر مجموع ظرفیت طراحی و نظارت در برش زمانی(" + lblMaxTotalCapacity.Text + ")بیشتر باشد");
                return false;
            }
            if (Convert.ToInt32(txtBonyadMaskan.Text) + Convert.ToInt32(txtObsShirazMunicipality.Text) > Convert.ToInt32(lblMaxJobObsCapacity.Text))
            {
                ShowMessage("مجموع متراژ نظارت گروه شهر های حومه و شهرداری شیراز و بنیاد مسکن نباید از حداکثر مجموع نظارت در برش زمانی(" + lblMaxJobObsCapacity.Text + ")بیشتر باشد");
                return false;
            }
            if (Convert.ToInt32(txtDesignBonyadMaskan.Text) + Convert.ToInt32(txtDesignShirazMunicipality.Text)
              > Convert.ToInt32(lblMaxDesignCapacity.Text))
            {
                ShowMessage("مجموع متراژ طراحی شهرداری شیراز و بنیاد مسکن نباید از حداکثر مجموع ظرفیت طراحی در برش زمانی(" + lblMaxDesignCapacity.Text + ")بیشتر باشد");
                return false;
            }
            if (((!Utility.IsDBNullOrNullValue(HiddenFieldPage["MustHasAttach"]) && Convert.ToBoolean(HiddenFieldPage["MustHasAttach"]) == true) || CheckBonyadAndMunici()) && Utility.IsDBNullOrNullValue(_FullFilePath))
            {
                ShowMessage("فایل تاییدیه مربوط به بنیاد مسکن یا شهرداری شیراز یا هردو باید بارگذاری شود");
                return false;
            }
            System.Data.DataTable dt = ObserverWorkRequestChangesManager.SelectLastRequest(-1, _MeId);
            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["IsConfirm"]) == 0)
            {
                ShowMessage("به دلیل وجود درخواست تایید نشده قادر به ثبت درخواست جدید نمی باشید");
                return false;
            }

            int SaveInfoTaskId = -1;
            int StateTaskId = -2;
            int ConfirmTSWorkRequestConfirminAndEndProccessTaskId = -1;
            int TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId = -1;
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo);
            if (WorkFlowTaskManager.Count > 0)
            {
                SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ConfirmTSWorkRequestConfirminAndEndProccess);
            if (WorkFlowTaskManager.Count > 0)
            {
                ConfirmTSWorkRequestConfirminAndEndProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.TSUnitEmployeeConfirminConfirmingTSWorkRequest);
            if (WorkFlowTaskManager.Count > 0)
            {
                TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }

            TSObserverWorkRequestManager.FindByMeId(_MeId);
            if (TSObserverWorkRequestManager.Count == 0)
            {
                ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
                return false;
            }
            int ObsWorkReqId = Convert.ToInt32(TSObserverWorkRequestManager[0]["ObsWorkReqId"]);            
            TransactionManager.BeginSave();

            #region InsertChange
            DataRow drChange = ObserverWorkRequestChangesManager.NewRow();
            drChange["Type"] = (int)TSObserverWorkRequestChangeType;
            drChange["ObsWorkReqId"] = ObsWorkReqId;
            drChange["MeId"] = _MeId;
            drChange["MfId"] = _MfId;
            drChange["DocMeFileExpireDate"] = _DocMeFileExpireDate;
            drChange["MasterMfMjParentId"] = _MfMjParentId;
            drChange["MfMjId"] = 0;
            drChange["ObsId"] = _CurrentObsId;
            if (!string.IsNullOrWhiteSpace(txtObsDate.Text))
                drChange["ObsDate"] = txtObsDate.Text;
            else
                drChange["ObsDate"] = txtMappingDate.Text;
            drChange["MembershipDate"] = txtMembershipDate.Text;
            drChange["Group1"] = false;
            drChange["Group2"] = false;
            drChange["Group3"] = false;
            drChange["Group4"] = false;
            for (int i = 0; i < CheckListStructureGroups.Items.Count; i++)
            {
                if (CheckListStructureGroups.Items[i].Selected)
                {
                    switch (Convert.ToInt32(CheckListStructureGroups.Items[i].Value))
                    {
                        case 1:
                            drChange["Group1"] = true;
                            break;
                        case 2:
                            drChange["Group2"] = true;
                            break;
                        case 3:
                            drChange["Group3"] = true;
                            break;
                        case 4:
                            drChange["Group4"] = true;
                            break;
                    }
                }
            }
            if (comboCity1.Value != null && comboCity1.SelectedItem.Value != null)
            {
                drChange["CapacityAssignmentId"] = _CurrentCapacityAssignmentId;
                drChange["City1"] = comboCity1.SelectedItem.Value;
            }
            else
                drChange["City1"] = DBNull.Value;
            if (comboCity2.Value != null && comboCity2.SelectedItem.Value != null)
                drChange["City2"] = comboCity2.SelectedItem.Value;
            else
                drChange["City2"] = DBNull.Value;
            drChange["MeAgentId"] = _AgentId;
            if (!CheckBoxWantObserverSelect.Checked)
                drChange["IsObserverOff"] = true;
            else
                drChange["IsObserverOff"] = false;
            if (TSObserverWorkRequestChangeType == TSP.DataManager.TSObserverWorkRequestChangeType.Off)
            {
                drChange["StartOffDate"] = txtStartOffDate.Text;
                drChange["EndOffDate"] = txtEndOffDate.Text;
            }
            else
            {
                drChange["StartOffDate"] = "";
                drChange["EndOffDate"] = "";
            }

            drChange["CountWorks"] = lblMaxJobCount.Text;
            drChange["CountInproccesWorks"] = 0;

            drChange["WantCharityWork"] = CheckBoxWantCharity.Checked;
            drChange["WantEghdamMeliMaskan"] = CheckBoxWantEghdamMeliMaskan.Checked;
            drChange["WantShahrakSanatiMeter"] = CheckBoxWantShahrakSanati.Checked;
            drChange["IsFullTimeWorker"] = CheckBoxIsFullTimeWorker.Checked;

            drChange["ShahrakSanatiMeter"] = 0;
            drChange["BonyadMaskan"] = txtBonyadMaskan.Text;
            drChange["BonyadMaskanDesignMeter"] = txtDesignBonyadMaskan.Text;
            drChange["ShirazMunicipalityMeter"] = txtObsShirazMunicipality.Text;
            drChange["ShirazMunicipalityDesignMeter"] = txtDesignShirazMunicipality.Text;
            drChange["ShirazMunicipulityUrbenismTarh"] = txtShirazMunicipulityUrbenismTarh.Text;
            drChange["ShirazMunicipulityUrbenismEntebaghShahri"] = txtShirazMunicipulityUrbenismEntebaghShahri.Text;

            drChange["CapacityDesign"] = lblMaxDesignCapacity.Text;//ظرفیت کل پایه طراحی

            drChange["CapacityObs"] = lblMaxJobObsCapacity.Text;//ظرفیت کل پایه نظارت  
            drChange["TotalCapacity"] = Math.Max(Convert.ToInt32(drChange["CapacityDesign"]), Convert.ToInt32(drChange["CapacityObs"]));

            if (_CurrentDesId != -2 && _CurrentObsId != -2)
                drChange["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.ObseverAndDesign;
            else if (_CurrentDesId == -2 && _CurrentObsId != -2)
                drChange["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.Obsever;
            else if (_CurrentDesId != -2 && _CurrentObsId == -2)
                drChange["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.Design;
            drChange["CurrentWfStateId"] = DBNull.Value;
            if (!Utility.IsDBNullOrNullValue(_FullFilePath))
                drChange["UrlAttachment"] = _FullFilePath;
            if (!Utility.IsDBNullOrNullValue(_FullFilePathObsCommitmentForm))
                drChange["UrlObserverCommitmentForm"] = _FullFilePathObsCommitmentForm;
            drChange["HasGasCert"] = _MemberCapacity.HasGasCert;
            drChange["UserId"] = Utility.GetCurrentUser_UserId();
            drChange["ModifiedDate"] = DateTime.Now;
            drChange["CreateDate"] = Utility.GetDateOfToday();
            if (CheckBonyadAndMunici() && Utility.GetCurrentUser_NmcIdType() == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId)
            {

                drChange["CurrentWfTasId"] = TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId;
                drChange["IsConfirm"] = 0;
            }

            else
            {
                drChange["CurrentWfTasId"] = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
                drChange["IsConfirm"] = 1;
            }

            ObserverWorkRequestChangesManager.AddRow(drChange);
            ObserverWorkRequestChangesManager.Save();
            ObserverWorkRequestChangesManager.DataTable.AcceptChanges();
            int TableId = int.Parse(ObserverWorkRequestChangesManager[ObserverWorkRequestChangesManager.Count - 1]["ObsWorkReqChangeId"].ToString());
            #endregion


            #region UpdateObsWorkRequest

            TSObserverWorkRequestManager.FindByMeId(_MeId);
            if (TSObserverWorkRequestManager.Count == 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است");
                return false;
            }

            TSObserverWorkRequestManager[0].BeginEdit();
            if (_AgentId == Utility.GetCurrentAgentCode() && comboCity1.SelectedItem != null)
            {
                TSObserverWorkRequestManager[0]["CityLastChangedWorkYear"] = _CurrentCapacityYear;
            }
            else
            {
                TSObserverWorkRequestManager[0]["CityLastChangedWorkYear"] = "";
            }
            if (!CheckBonyadAndMunici() || Utility.GetCurrentUser_NmcIdType() != (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId)
            {
                #region در صورت تایید اتوماتیک بروزرسانی جدول اصلی آماده بکاری انجام می شود
                TSObserverWorkRequestManager[0]["MfId"] = _MfId;
                TSObserverWorkRequestManager[0]["ObsId"] = _CurrentObsId;
                if (!string.IsNullOrWhiteSpace(txtObsDate.Text))
                    TSObserverWorkRequestManager[0]["ObsDate"] = txtObsDate.Text;
                else
                    TSObserverWorkRequestManager[0]["ObsDate"] = txtMappingDate.Text;

                TSObserverWorkRequestManager[0]["MembershipDate"] = txtMembershipDate.Text;
                TSObserverWorkRequestManager[0]["DocMeFileExpireDate"] = _DocMeFileExpireDate;
                TSObserverWorkRequestManager[0]["MasterMfMjParentId"] = _MfMjParentId;
                TSObserverWorkRequestManager[0]["MfMjId"] = 0;
                TSObserverWorkRequestManager[0]["Group1"] = false;
                TSObserverWorkRequestManager[0]["Group2"] = false;
                TSObserverWorkRequestManager[0]["Group3"] = false;
                TSObserverWorkRequestManager[0]["Group4"] = false;
                for (int i = 0; i < CheckListStructureGroups.Items.Count; i++)
                {
                    if (CheckListStructureGroups.Items[i].Selected)
                    {
                        switch (Convert.ToInt32(CheckListStructureGroups.Items[i].Value))
                        {
                            case 1:
                                TSObserverWorkRequestManager[0]["Group1"] = true;
                                break;
                            case 2:
                                TSObserverWorkRequestManager[0]["Group2"] = true;
                                break;
                            case 3:
                                TSObserverWorkRequestManager[0]["Group3"] = true;
                                break;
                            case 4:
                                TSObserverWorkRequestManager[0]["Group4"] = true;
                                break;
                        }
                    }
                }
                if (comboCity1.Value != null && comboCity1.SelectedItem.Value != null)
                {
                    TSObserverWorkRequestManager[0]["CapacityAssignmentId"] = _CurrentCapacityAssignmentId;
                    TSObserverWorkRequestManager[0]["City1"] = comboCity1.SelectedItem.Value;
                }
                else
                    TSObserverWorkRequestManager[0]["City1"] = DBNull.Value;
                if (comboCity2.Value != null && comboCity2.SelectedItem.Value != null)
                    TSObserverWorkRequestManager[0]["City2"] = comboCity2.SelectedItem.Value;
                else
                    TSObserverWorkRequestManager[0]["City2"] = DBNull.Value;
                TSObserverWorkRequestManager[0]["MeAgentId"] = _AgentId;

                if (!CheckBoxWantObserverSelect.Checked)//آیا تمایل به ارجاع کار نظارت دارید؟
                    TSObserverWorkRequestManager[0]["IsObserverOff"] = true;
                else
                    TSObserverWorkRequestManager[0]["IsObserverOff"] = false;
                if (TSObserverWorkRequestChangeType == TSP.DataManager.TSObserverWorkRequestChangeType.Off)
                {
                    TSObserverWorkRequestManager[0]["StartOffDate"] = txtStartOffDate.Text;
                    TSObserverWorkRequestManager[0]["EndOffDate"] = txtEndOffDate.Text;
                }
                else
                {
                    TSObserverWorkRequestManager[0]["StartOffDate"] = "";
                    TSObserverWorkRequestManager[0]["EndOffDate"] = "";
                }
                if (!Utility.IsDBNullOrNullValue(_FullFilePath))
                    TSObserverWorkRequestManager[0]["UrlAttachment"] = _FullFilePath;
                if (!Utility.IsDBNullOrNullValue(_FullFilePathObsCommitmentForm))
                    TSObserverWorkRequestManager[0]["UrlObserverCommitmentForm"] = _FullFilePathObsCommitmentForm;
                TSObserverWorkRequestManager[0]["IsFullTimeWorker"] = CheckBoxIsFullTimeWorker.Checked;
                TSObserverWorkRequestManager[0]["WantCharityWork"] = CheckBoxWantCharity.Checked;
                TSObserverWorkRequestManager[0]["WantEghdamMeliMaskan"] = CheckBoxWantEghdamMeliMaskan.Checked;
                TSObserverWorkRequestManager[0]["WantShahrakSanatiMeter"] = CheckBoxWantShahrakSanati.Checked;

                //***********ظرفیت ها***************
                TSObserverWorkRequestManager[0]["ShahrakSanatiMeter"] = 0;//به صورت تیک شد و متراژ وارد نمی شود                
                TSObserverWorkRequestManager[0]["BonyadMaskan"] = txtBonyadMaskan.Text;//بنیاد مسکن
                TSObserverWorkRequestManager[0]["BonyadMaskanDesignMeter"] = txtDesignBonyadMaskan.Text;//بنیاد مسکن طراحی 
                TSObserverWorkRequestManager[0]["ShirazMunicipalityMeter"] = txtObsShirazMunicipality.Text;
                TSObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"] = txtDesignShirazMunicipality.Text;
                TSObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"] = txtShirazMunicipulityUrbenismTarh.Text;
                TSObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"] = txtShirazMunicipulityUrbenismEntebaghShahri.Text;
                //**************************           
                TSObserverWorkRequestManager[0]["CapacityDesign"] = lblMaxDesignCapacity.Text;//ظرفیت کل پایه طراحی  

                TSObserverWorkRequestManager[0]["CapacityObs"] = lblMaxJobObsCapacity.Text;//ظرفیت کل پایه نظارت 
                TSObserverWorkRequestManager[0]["TotalCapacity"] = Math.Max(Convert.ToInt32(TSObserverWorkRequestManager[0]["CapacityDesign"]), Convert.ToInt32(TSObserverWorkRequestManager[0]["CapacityObs"]));

                //**************************   
                TSObserverWorkRequestManager[0]["Status"] = TSP.DataManager.TSObserverWorkRequestStatus.Confirm;
                #endregion
            }


            if (CheckBonyadAndMunici() && Utility.GetCurrentUser_NmcIdType() == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId)
            {
                TSObserverWorkRequestManager[0]["CurrentWfTasId"] = TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId;
            }
            else
            {
                TSObserverWorkRequestManager[0]["CurrentWfTasId"] = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
                if (_CurrentDesId != -2 && _CurrentObsId != -2)
                    TSObserverWorkRequestManager[0]["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.ObseverAndDesign;
                else if (_CurrentDesId == -2 && _CurrentObsId != -2)
                    TSObserverWorkRequestManager[0]["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.Obsever;
                else if (_CurrentDesId != -2 && _CurrentObsId == -2)
                    TSObserverWorkRequestManager[0]["WantedWorkType"] = (int)TSP.DataManager.TSWorkRequestWantedWorkType.Design;
            }

            TSObserverWorkRequestManager[0]["CurrentWfStateId"] = DBNull.Value;
            TSObserverWorkRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            TSObserverWorkRequestManager[0]["ModifiedDate"] = DateTime.Now;

            TSObserverWorkRequestManager[0].EndEdit();
            if (TSObserverWorkRequestManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return false;
            }
            TSObserverWorkRequestManager.DataTable.AcceptChanges();
            int ReturnValue = CapacityCalculations.UpdateWorkRequestCapacityData(TSObserverWorkRequestManager, ProjectCapacityDecrementManager, _MeId, Utility.GetCurrentUser_UserId(), -2, -2, false, TSP.DataManager.TSProjectIngridientType.Nothing, null, false, false, false);
            if (ReturnValue != 0)
            {
                TransactionManager.CancelSave();
                ShowMessage(CapacityCalculations.FindErrorMessage(ReturnValue));
                return false;
            }

            #endregion
            #region WorkFlow    

            int NmcId = Utility.GetCurrentUser_NmcIdType() == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId ? _MeId : (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId;
            if (NmcId < 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return false;
            }
            string DesToNextStep = "";
            if (Utility.GetCurrentUser_NmcIdType() == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId)
                DesToNextStep = "ثبت درخواست تغییرات آماده به کاری توسط عضو";
            else
                DesToNextStep = "ثبت درخواست تغییرات آماده به کاری توسط کارمند خدمات مهندسی";

            int StateId1 = WorkFlowStateManager.SendDocToNextStep(TableId, SaveInfoTaskId, DesToNextStep, NmcId, Utility.GetCurrentUser_NmcIdType(), Utility.GetCurrentUser_UserId(), 0, Utility.GetDateOfToday());
            if (StateId1 <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return false;
            }

            string StateDescription = "";
            if (TSObserverWorkRequestChangeType == TSP.DataManager.TSObserverWorkRequestChangeType.Off)
            {
                StateTaskId = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
                StateDescription = "تایید اتوماتیک درخواست تغییرات آماده به کاری توسط سیستم";
            }
            else
            {

                if (CheckBonyadAndMunici() && Utility.GetCurrentUser_NmcIdType() == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId)
                {
                    StateTaskId = TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId;
                    StateDescription = "ارسال اتوماتیک درخواست تغییرات آماده به کاری توسط عضو به کارشناس خدمات مهندسی";
                }
                else
                {
                    StateTaskId = ConfirmTSWorkRequestConfirminAndEndProccessTaskId;
                    StateDescription = "تایید اتوماتیک درخواست تغییرات آماده به کاری عضو ";
                }
            }

            // آیا شرایط که تعییران داشتیم اعمال شده
            int StateId2 = WorkFlowStateManager.SendDocToNextStep(TableId, StateTaskId, StateDescription, NmcId, Utility.GetCurrentUser_NmcIdType(), Utility.GetCurrentUser_UserId(), 0, Utility.GetDateOfToday());
            if (StateId2 <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return false;
            }

            #endregion

            TSObserverWorkRequestManager.DataTable.AcceptChanges();
            TSObserverWorkRequestManager[0].BeginEdit();
            TSObserverWorkRequestManager[0]["CurrentWfStateId"] = StateId2;
            TSObserverWorkRequestManager[0].EndEdit();
            TSObserverWorkRequestManager.Save();
            ObserverWorkRequestChangesManager[0].BeginEdit();
            ObserverWorkRequestChangesManager[0]["CurrentWfStateId"] = StateId2;
            ObserverWorkRequestChangesManager[0].EndEdit();
            ObserverWorkRequestChangesManager.Save();
            _PageMode = "View";
            _ObsWorkReqChangeId = TableId;
            _RoundPanelPageHeader = "مشاهده آماده به کاری";
            TransactionManager.EndSave();
            ShowMessage("ذخیره انجام گرفت.");
            SetViewModeKeys();
            if (StateTaskId != TSUnitEmployeeConfirminConfirmingTSWorkRequestTaskId)
            {
                if (SendInfoToShahrdari)
                {
                    if (Utility.IsWorkREquestInfoSendToShahrdari())
                    {
                        if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                        {
                            ShahrdariWebservice(_MfMjParentId
                                , txtObsDate.Text == "---" ? "" : txtObsDate.Text
                                , txtDesignDate.Text == "---" ? "" : txtDesignDate.Text
                                , txtUrbenismDate.Text == "---" ? "" : txtUrbenismDate.Text
                                , txtMappingDate.Text == "---" ? "" : txtMappingDate.Text
                             , Convert.ToDecimal(txtObsShirazMunicipality.Text)
                            , Convert.ToDecimal(txtDesignShirazMunicipality.Text)
                            , Convert.ToDecimal(txtShirazMunicipulityUrbenismTarh.Text) + Convert.ToDecimal(txtShirazMunicipulityUrbenismEntebaghShahri.Text), Convert.ToInt16(_CurrentCapacityYear), true);
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            Utility.SaveWebsiteError(ex);
            return false;

        }

    }
    #endregion

    #region UsefulMethods
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
            ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }

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

    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = _ErrorMessage = str;
    }

    #endregion

    #region WF
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int WorkFlowCode = (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WorkFlowCode, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo;

                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                        if (Permission > 0)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            CheckWorkFlowPermissionForSave(_PageMode);
            if (_PageMode != "New")
                CheckWorkFlowPermissionForEdit(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            //BtnNew.Enabled = true;
            //BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                case "Change":
                case "Off":

                    break;
                case "View":
                    break;
                default:
                    break;
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست آماده به کاری را ندارید.";
        }

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string MfId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTSWorkRequestConfirminInfo;
        int WorkFlowCode = (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, int.Parse(MfId), TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    break;
                case "View":
                    break;
            }
        }
        else
        {
            switch (PageMode)
            {
                case "View":
                    break;
                case "Off":
                    break;
            }
        }



    }

    #endregion

    #region WebService Shahrdari
    private void ShahrdariWebservice(int MjParentId, string ObsDate, string DesignDate, string UrbenismDate, string MappingDate, decimal MunObsCapacity, decimal MunDesignCapacity, decimal MunUrbenismCapacity, Int16 Year, Boolean SendMeInfo)
    {

        //لیست ClsQtaInputs دارای اطلاعات زیر می باشد
        //CI_Ability : در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4
        //Meter: متراژ ظرفیت شیراز
        //date : تاریخ بازگشایی ظرفیت
        //Time  :  ساعت بازگشلیی ظرفیت
        //ChangeBaseDate  : تاریخ تغییر پایه
        try
        {
            if (Utility.IsesupTestServerUse())
            {
                #region TestServer Comment
                //WorkRequestsrvEngineerToOthersTest.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthersTest.IsrvEngineerToOthersClient();
                //if (SendMeInfo)
                //{
                //    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                //    DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(_MeId);
                //    if (dtMember.Rows.Count != 1)
                //        return;
                //    WorkRequestsrvEngineerToOthersTest.ClsEngineer1 ClsEngineer1Info = new WorkRequestsrvEngineerToOthersTest.ClsEngineer1();
                //    ClsEngineer1Info.Eng_Info = new WorkRequestsrvEngineerToOthersTest.Eng_Info();
                //    ClsEngineer1Info.Eng_Info.EngName = dtMember.Rows[0]["FirstName"].ToString();
                //    ClsEngineer1Info.Eng_Info.EngFamily = dtMember.Rows[0]["LastName"].ToString();
                //    ClsEngineer1Info.Eng_Info.MunicipalityCode = dtMember.Rows[0]["MeNo"].ToString();// کد عضویت نظام مهندسی
                //    ClsEngineer1Info.Eng_Info.FatherName = dtMember.Rows[0]["FatherName"].ToString();// نام پدر
                //    ClsEngineer1Info.Eng_Info.BirthDate = dtMember.Rows[0]["BirhtDate"].ToString(); //تاریخ تولد
                //    ClsEngineer1Info.Eng_Info.IdNo = dtMember.Rows[0]["IdNo"].ToString();//شماره شناسنامه
                //    ClsEngineer1Info.Eng_Info.Tel = dtMember.Rows[0]["HomeTel"].ToString();// تلفن
                //    ClsEngineer1Info.Eng_Info.Address = dtMember.Rows[0]["HomeAdr"].ToString();// آدرس
                //    ClsEngineer1Info.Eng_Info.NationalCode = dtMember.Rows[0]["SSN"].ToString();// کد ملی
                //    ClsEngineer1Info.Eng_Info.IdentityCode = _MeId.ToString();//کد عضویت
                //    ClsEngineer1Info.Eng_Info.AddressWork = ""; //آدرس محل کار
                //    ClsEngineer1Info.Eng_Info.Email = dtMember.Rows[0]["Email"].ToString();// آدرس ایمیل
                //    ClsEngineer1Info.Eng_Info.BirthPlace = dtMember.Rows[0]["BirthPlace"].ToString();// محل تولد
                //    ClsEngineer1Info.Eng_Info.MobileNo = dtMember.Rows[0]["MobileNo"].ToString(); //شماره همراه
                //    ClsEngineer1Info.Eng_Info.PostalCode = "";//کد پستی
                //    ClsEngineer1Info.Eng_Info.ArchitectureCode = "";// کد شهرداری
                //    ClsEngineer1Info.Eng_Info.PostalCodeWork = ""; //کد پستی محل کار
                //    switch (MjParentId)
                //    {
                //        case 1:
                //            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 100;
                //            break;
                //        case 2:
                //            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 210;
                //            break;
                //        case 3:
                //            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 300;
                //            break;
                //        case 4:
                //            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 400;
                //            break;
                //        case 5:
                //            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 500;
                //            break;
                //        case 6:
                //            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 600;
                //            break;
                //        case 7:
                //            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 700;
                //            break;
                //    }
                //    IsrvEngineerToOthersClient.SaveEng_InfoCompleted += new EventHandler<SaveEng_InfoCompletedEventArgs>(GetResultEngineerToOthersTest);
                //    IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ClsEngineer1Info);

                //}
                //int Count = 1;
                //if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                //    Count = 2;
                //string time = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                //WorkRequestsrvEngineerToOthersTest.ClsQtaInputs[] ListclsQtaInputs = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs[Count];
                ////CI_Ability: در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4 

                //if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                //{
                //    #region نظارت و طراحی
                //    //**طراحی
                //    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsde = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                //        ClsQtaInputsde.CI_Ability = 4;
                //    else
                //        ClsQtaInputsde.CI_Ability = 2;
                //    ClsQtaInputsde.Date = Utility.GetDateOfToday();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                //        ClsQtaInputsde.Meter = MunUrbenismCapacity;
                //    else
                //        ClsQtaInputsde.Meter = MunDesignCapacity;
                //    ClsQtaInputsde.Time = time;
                //    ClsQtaInputsde.ChangeBaseDate = DesignDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsde;
                //    //**نظارت
                //    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsOb = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                //    ClsQtaInputsOb.CI_Ability = 1;
                //    //ClsQtaInputsOb.Date = Utility.GetDateOfToday();
                //    ClsQtaInputsOb.Meter = MunObsCapacity;
                //    //ClsQtaInputsOb.Time = time;
                //    ClsQtaInputsOb.ChangeBaseDate = ObsDate;
                //    ListclsQtaInputs[1] = ClsQtaInputsOb;
                //    #endregion
                //}
                //else if ((MunDesignCapacity != 0 || MunUrbenismCapacity != 0) && string.IsNullOrEmpty(ObsDate))
                //{
                //    #region فقظ طراحی یا شهرسازی
                //    //**طراحی
                //    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsDes = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                //        ClsQtaInputsDes.CI_Ability = 4;
                //    else
                //        ClsQtaInputsDes.CI_Ability = 2;
                //    ClsQtaInputsDes.Date = Utility.GetDateOfToday();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                //        ClsQtaInputsDes.Meter = MunUrbenismCapacity;
                //    else
                //        ClsQtaInputsDes.Meter = MunDesignCapacity;
                //    ClsQtaInputsDes.Time = time;
                //    ClsQtaInputsDes.ChangeBaseDate = DesignDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsDes;
                //    #endregion
                //}
                //else if (!string.IsNullOrEmpty(ObsDate))
                //{
                //    #region فقظ نظارت
                //    //**نظارت
                //    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsObs = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                //    ClsQtaInputsObs.CI_Ability = 1;
                //    //ClsQtaInputsObs.Date = Utility.GetDateOfToday();
                //    ClsQtaInputsObs.Meter = MunObsCapacity;
                //    //ClsQtaInputsObs.Time = time;
                //    ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsObs;
                //    #endregion
                //}
                //IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthersTest.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClientTest);
                //IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListclsQtaInputs, 1, Year, (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.WorkRequest);

                #endregion
            }
            else
            {
                #region MainServer

                WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient();
                if (SendMeInfo)
                {
                    int MasterMfMjParentId = -2; int ObsId = -2; ; int DesId = 0; int UrbanismId = 0; int MappingId = 0;
                    Int16 ObsGrad = 0; Int16 DesGrad = 0; Int16 UrbanismGrad = 0; Int16 MappingGrad = 0;
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(_MeId);
                    if (dtMember.Rows.Count != 1)
                        return;
                    string LastDocRegDate = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["LastDocRegDate"]) ? "" : dtMember.Rows[0]["LastDocRegDate"].ToString();
                    MasterMfMjParentId = Convert.ToInt32(dtMember.Rows[0]["MasterMfMjParentId"]);
                    ObsId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["ObsId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["ObsId"]);
                    DesId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["DesId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["DesId"]);
                    MappingId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["MappingId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["MappingId"]);
                    UrbanismId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["UrbanismId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["UrbanismId"]);
                    switch (ObsId)
                    {
                        case (int)TSP.DataManager.DocumentGrads.Grade1:
                            ObsGrad = 1;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade2:
                            ObsGrad = 2;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade3:
                            ObsGrad = 3;
                            break;
                    }
                    switch (DesId)
                    {
                        case (int)TSP.DataManager.DocumentGrads.Grade1:
                            ObsGrad = 1;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade2:
                            DesGrad = 2;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade3:
                            DesGrad = 3;
                            break;
                    }
                    switch (UrbanismId)
                    {
                        case (int)TSP.DataManager.DocumentGrads.Grade1:
                            UrbanismGrad = 1;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade2:
                            UrbanismGrad = 2;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade3:
                            UrbanismGrad = 3;
                            break;
                    }
                    switch (MappingId)
                    {
                        case (int)TSP.DataManager.DocumentGrads.Grade1:
                            MappingGrad = 1;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade2:
                            MappingGrad = 2;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade3:
                            MappingGrad = 3;
                            break;
                    }

                    if (string.IsNullOrWhiteSpace(ObsDate))
                        ObsDate = MappingDate;

                    #region اطلاعات پایه مهندس
                    WorkRequestsrvEngineerToOthers.ClsEngineer1 ClsEngineer1Info = new WorkRequestsrvEngineerToOthers.ClsEngineer1();
                    ClsEngineer1Info.Eng_Info = new WorkRequestsrvEngineerToOthers.Eng_Info();
                    ClsEngineer1Info.Eng_Info.EngName = dtMember.Rows[0]["FirstName"].ToString();
                    ClsEngineer1Info.Eng_Info.EngFamily = dtMember.Rows[0]["LastName"].ToString();
                    ClsEngineer1Info.Eng_Info.MunicipalityCode = dtMember.Rows[0]["MeNo"].ToString();// کد عضویت نظام مهندسی
                    ClsEngineer1Info.Eng_Info.FatherName = dtMember.Rows[0]["FatherName"].ToString();// نام پدر
                    ClsEngineer1Info.Eng_Info.BirthDate = dtMember.Rows[0]["BirhtDate"].ToString(); //تاریخ تولد
                    ClsEngineer1Info.Eng_Info.IdNo = dtMember.Rows[0]["IdNo"].ToString();//شماره شناسنامه
                    ClsEngineer1Info.Eng_Info.Tel = dtMember.Rows[0]["HomeTel"].ToString();// تلفن
                    ClsEngineer1Info.Eng_Info.Address = dtMember.Rows[0]["HomeAdr"].ToString();// آدرس
                    ClsEngineer1Info.Eng_Info.NationalCode = dtMember.Rows[0]["SSN"].ToString();// کد ملی
                    ClsEngineer1Info.Eng_Info.IdentityCode = _MeId.ToString();//کد عضویت
                    ClsEngineer1Info.Eng_Info.AddressWork = ""; //آدرس محل کار
                    ClsEngineer1Info.Eng_Info.Email = dtMember.Rows[0]["Email"].ToString();// آدرس ایمیل
                    ClsEngineer1Info.Eng_Info.BirthPlace = dtMember.Rows[0]["BirthPlace"].ToString();// محل تولد
                    ClsEngineer1Info.Eng_Info.MobileNo = dtMember.Rows[0]["MobileNo"].ToString(); //شماره همراه
                    ClsEngineer1Info.Eng_Info.PostalCode = "";//کد پستی
                    ClsEngineer1Info.Eng_Info.ArchitectureCode = "";// کد شهرداری
                    ClsEngineer1Info.Eng_Info.PostalCodeWork = ""; //کد پستی محل کار
                    switch (MjParentId)
                    {
                        case 1:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 100;
                            break;
                        case 2:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 210;
                            break;
                        case 3:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 300;
                            break;
                        case 4:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 400;
                            break;
                        case 5:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 500;
                            break;
                        case 6:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 600;
                            break;
                        case 7:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 700;
                            break;
                    }
                    IsrvEngineerToOthersClient.SaveEng_InfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs>(GetResultEngineerToOthers);
                    IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ClsEngineer1Info);

                    #endregion
                    #region اطلاعات پروانه
                    WorkRequestsrvEngineerToOthers.Eng_JobAgreement PEngjob = new WorkRequestsrvEngineerToOthers.Eng_JobAgreement();
                    PEngjob.JobAgreementExportDate = LastDocRegDate;// تاریخ صدور پروانه اشتغال جاری
                    PEngjob.JobAgreementExpireDate = dtMember.Rows[0]["FileDate"].ToString();//تاریخ اعتبار پروانه اشتغال جاری                    
                    PEngjob.NIdJobAgreement_tmp = 0;
                    PEngjob.CI_JobAgreementType = 1;
                    PEngjob.CI_JobAgreementBaseExport = 0;
                    PEngjob.CI_Region = 1;

                    IsrvEngineerToOthersClient.SaveEng_JobAgreementCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs>(GetResultJobAgreement);
                    IsrvEngineerToOthersClient.SaveEng_JobAgreementAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), PEngjob);

                    #endregion
                    #region اطلاعات جدول صلاحیت

                    int Len = 1;
                    if ((DesGrad > 0 || UrbanismGrad > 0) && (ObsGrad > 0 || MappingGrad > 0))
                        Len = 2;
                    WorkRequestsrvEngineerToOthers.Eng_Competence[] ListengCompetence = new WorkRequestsrvEngineerToOthers.Eng_Competence[Len];
                    if (ObsGrad > 0 || MappingGrad > 0)
                    {

                        WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                        engCompetenceObj.CI_Ability = 1;
                        engCompetenceObj.CI_Base = ObsGrad > 0 ? ObsGrad : MappingGrad;
                        engCompetenceObj.IsActive = true;
                        ListengCompetence[0] = engCompetenceObj;

                    }
                    if (DesGrad > 0 || UrbanismGrad > 0)
                    {
                        WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                        if (MasterMfMjParentId == (int)TSP.DataManager.MainMajors.Civil)
                            engCompetenceObj.CI_Ability = 4;
                        else
                            engCompetenceObj.CI_Ability = 2;

                        engCompetenceObj.CI_Base = DesGrad > 0 ? DesGrad : UrbanismGrad;
                        engCompetenceObj.IsActive = true;
                        if (Len == 1)
                            ListengCompetence[0] = engCompetenceObj;
                        else if (Len == 2)
                            ListengCompetence[1] = engCompetenceObj;
                    }
                    IsrvEngineerToOthersClient.SaveEng_CompetenceCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs>(GetResultCompetence);
                    IsrvEngineerToOthersClient.SaveEng_CompetenceAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListengCompetence);

                    #endregion
                    #region اطلاعات ظرفیت مهندس در آماده بکاری
                    int Count = 1;
                    if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                        Count = 2;
                    string time = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                    WorkRequestsrvEngineerToOthers.ClsQtaInputs[] ListclsQtaInputs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs[Count];
                    //CI_Ability: در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4 
                    if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                    {
                        #region نظارت و طراحی
                        WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsde = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                        if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                            ClsQtaInputsde.CI_Ability = 4;
                        else
                            ClsQtaInputsde.CI_Ability = 2;
                        ClsQtaInputsde.Date = Utility.GetDateOfToday();
                        if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                            ClsQtaInputsde.Meter = MunUrbenismCapacity;
                        else
                            ClsQtaInputsde.Meter = MunDesignCapacity;
                        ClsQtaInputsde.Time = time;
                        ClsQtaInputsde.ChangeBaseDate = DesignDate;
                        ListclsQtaInputs[0] = ClsQtaInputsde;
                        //**نظارت
                        WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsOb = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                        ClsQtaInputsOb.CI_Ability = 1;
                        ClsQtaInputsOb.Meter = MunObsCapacity;
                        ClsQtaInputsOb.ChangeBaseDate = ObsDate;
                        ListclsQtaInputs[1] = ClsQtaInputsOb;
                        #endregion
                    }
                    else if ((MunDesignCapacity != 0 || MunUrbenismCapacity != 0) && string.IsNullOrEmpty(ObsDate))
                    {
                        #region فقظ طراحی یا شهرسازی
                        WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsDes = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                        if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                            ClsQtaInputsDes.CI_Ability = 4;
                        else
                            ClsQtaInputsDes.CI_Ability = 2;
                        ClsQtaInputsDes.Date = Utility.GetDateOfToday();
                        if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                            ClsQtaInputsDes.Meter = MunUrbenismCapacity;
                        else
                            ClsQtaInputsDes.Meter = MunDesignCapacity;
                        ClsQtaInputsDes.Time = time;
                        ClsQtaInputsDes.ChangeBaseDate = DesignDate;
                        ListclsQtaInputs[0] = ClsQtaInputsDes;
                        #endregion
                    }
                    else if (!string.IsNullOrEmpty(ObsDate))
                    {
                        #region فقظ نظارت
                        //**نظارت
                        WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsObs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                        ClsQtaInputsObs.CI_Ability = 1;
                        //ClsQtaInputsObs.Date = Utility.GetDateOfToday();
                        ClsQtaInputsObs.Meter = MunObsCapacity;
                        //ClsQtaInputsObs.Time = time;
                        ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                        ListclsQtaInputs[0] = ClsQtaInputsObs;
                        #endregion
                    }

                    IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClient);
                    IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListclsQtaInputs, 1, Year, (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.WorkRequest);
                    #endregion

                }

                #endregion
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است" + ex.ToString());
        }
    }

    private void GetResultEngineerToOthersTest(object sender, SaveEng_InfoCompletedEventArgs e)
    {
        try
        {
            TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
            DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
            try
            {
                dr["MeId"] = _MeId;
                dr["ModifiedDate"] = DateTime.Now;
                dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveEngInfo;
                WorkRequestsrvEngineerToOthersTest.ClsEngineer1 ResultClsEngineer1 = e.Result;
                if (ResultClsEngineer1.ErrorResult.BizErrors[0] != null)
                {
                    //ok;
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
                }
            }
            catch (Exception ex)
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                try
                {
                    if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                        dr["Message"] = e.Error.InnerException.Message;
                }
                catch (Exception exp)
                {

                    Utility.SaveWebsiteError(exp);
                }
                Utility.SaveWebsiteError(ex);
            }
            TSEsupWebserviceCallingLogManager.AddRow(dr);
            TSEsupWebserviceCallingLogManager.Save();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    private void GetGetResultEngineerToOthersClientTest(object sender, SaveQtaInfoCompletedEventArgs e)
    {
        try
        {
            TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
            DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
            try
            {
                dr["MeId"] = _MeId;
                dr["ModifiedDate"] = DateTime.Now;
                dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveQtaInfo;
                WorkRequestsrvEngineerToOthersTest.ClsErrorResult ErrorResult = e.Result;
                if (ErrorResult.BizErrors.Length == 0)
                {
                    //ok;     
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
                }
            }
            catch (Exception ex)
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                try
                {
                    if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                        dr["Message"] = e.Error.InnerException.Message;
                }
                catch (Exception exp)
                {

                    Utility.SaveWebsiteError(exp);
                }
                Utility.SaveWebsiteError(ex);
            }
            TSEsupWebserviceCallingLogManager.AddRow(dr);
            TSEsupWebserviceCallingLogManager.Save();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }

    }

    private void GetGetResultEngineerToOthersClient(object sender, WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs e)
    {
        try
        {
            TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
            DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
            try
            {
                dr["MeId"] = _MeId;
                dr["ModifiedDate"] = DateTime.Now;
                dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveQtaInfo;
                WorkRequestsrvEngineerToOthers.ClsErrorResult ErrorResult = e.Result;
                if (ErrorResult.BizErrors.Length == 0)
                {
                    //ok;     
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
                }
            }
            catch (Exception ex)
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                try
                {
                    if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                        dr["Message"] = e.Error.InnerException.Message;
                }
                catch (Exception exp)
                {

                    Utility.SaveWebsiteError(exp);
                }
                Utility.SaveWebsiteError(ex);
            }
            TSEsupWebserviceCallingLogManager.AddRow(dr);
            TSEsupWebserviceCallingLogManager.Save();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    private void GetResultEngineerToOthers(object sender, WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs e)
    {
        try
        {
            TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
            DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
            try
            {
                dr["MeId"] = _MeId;
                dr["ModifiedDate"] = DateTime.Now;
                dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveEngInfo;
                WorkRequestsrvEngineerToOthers.ClsEngineer1 ResultClsEngineer1 = e.Result;
                if (ResultClsEngineer1.ErrorResult.BizErrors[0] != null)
                {
                    //ok;
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
                }
            }
            catch (Exception ex)
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                try
                {
                    if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                        dr["Message"] = e.Error.InnerException.Message;
                }
                catch (Exception exp)
                {

                    Utility.SaveWebsiteError(exp);
                }
                Utility.SaveWebsiteError(ex);
            }
            TSEsupWebserviceCallingLogManager.AddRow(dr);
            TSEsupWebserviceCallingLogManager.Save();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    private void GetResultJobAgreement(object sender, WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs e)
    {
        TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
        try
        {
            dr["MeId"] = _MeId;
            dr["ModifiedDate"] = DateTime.Now;
            dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveJobAgreeMent;
            WorkRequestsrvEngineerToOthers.Eng_JobAgreement ResultJobAgreement = e.Result;
            if (ResultJobAgreement != null)
            {
                //ok;
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
            }
            else
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
            }
        }
        catch (Exception ex)
        {
            dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
            try
            {
                if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                    dr["Message"] = e.Error.InnerException.Message;
            }
            catch (Exception exp)
            {

                Utility.SaveWebsiteError(exp);
            }
            Utility.SaveWebsiteError(ex);
        }
        TSEsupWebserviceCallingLogManager.AddRow(dr);
        TSEsupWebserviceCallingLogManager.Save();
    }

    private void GetResultCompetence(object sender, WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs e)
    {
        TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
        try
        {
            dr["MeId"] = _MeId;
            dr["ModifiedDate"] = DateTime.Now;
            dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveCompetence;
            WorkRequestsrvEngineerToOthers.Eng_Competence[] ResultCompetence = e.Result;
            if (ResultCompetence.Length > 0)
            {
                //ok;
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
            }
            else
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
            }
        }
        catch (Exception ex)
        {
            dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
            try
            {
                if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                    dr["Message"] = e.Error.InnerException.Message;
            }
            catch (Exception exp)
            {

                Utility.SaveWebsiteError(exp);
            }
            Utility.SaveWebsiteError(ex);
        }
        TSEsupWebserviceCallingLogManager.AddRow(dr);
        TSEsupWebserviceCallingLogManager.Save();
    }
    #endregion
    #endregion
}