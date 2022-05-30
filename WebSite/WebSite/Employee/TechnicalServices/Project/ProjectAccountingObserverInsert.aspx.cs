using System;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.Web.UI;

public partial class Employee_TechnicalServices_Project_ProjectAccountingObserverInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    #region Peroperties
    private int _GroupId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldAcc["GroupId"].ToString()));
        }
        set
        {
            HiddenFieldAcc["GroupId"] = Utility.EncryptQS(value.ToString());
        }
    }
    private int _MunId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldAcc["MunId"].ToString()));
        }
        set
        {
            HiddenFieldAcc["MunId"] = Utility.EncryptQS(value.ToString());
        }
    }
    private string _AgentCode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldAcc["AgentCode"].ToString());
        }
        set
        {
            HiddenFieldAcc["AgentCode"] = Utility.EncryptQS(value.ToString());
        }
    }
    private string _AgentCodeForPaymentIdProvince
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldAcc["AgentCodeForPaymentIdProvince"].ToString());
        }
        set
        {
            HiddenFieldAcc["AgentCodeForPaymentIdProvince"] = Utility.EncryptQS(value.ToString());
        }
    }
    private int _PlansId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldAcc["PlnId"].ToString()));
        }
        set
        {
            HiddenFieldAcc["PlnId"] = Utility.EncryptQS(value.ToString());
        }
    }

    private int _PlansTypeId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldAcc["PlnTypeId"].ToString()));
        }
        set
        {
            HiddenFieldAcc["PlnTypeId"] = Utility.EncryptQS(value.ToString());
        }
    }

    private int _ProjectIngridientTypeId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldAcc["ProjectIngridientTypeId"].ToString()));
        }
        set
        {
            HiddenFieldAcc["ProjectIngridientTypeId"] = Utility.EncryptQS(value.ToString());
        }
    }

    private int _DesignerPlansId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldAcc["DesignerPlansId"].ToString()));
        }
        set
        {
            HiddenFieldAcc["DesignerPlansId"] = Utility.EncryptQS(value.ToString());
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

    private int _AccountingId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HDAccountingId.Value));
        }
        set
        {
            HDAccountingId.Value = Utility.EncryptQS(value.ToString());
        }
    }

    private int _ProjectId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HDProjectId.Value));
        }
        set
        {
            HDProjectId.Value = Utility.EncryptQS(value.ToString());
        }
    }
    private int _Foundation
    {
        get
        {
            return Convert.ToInt32(HiddenFieldAcc["Foundation"]);
        }
        set
        {
            HiddenFieldAcc["Foundation"] = value.ToString();
        }
    }
    private int _ProjecReqtId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HDRequestId.Value));
        }
        set
        {
            HDRequestId.Value = Utility.EncryptQS(value.ToString());
        }
    }

    private int _CurrentTaskCode
    {
        get
        {
            return Convert.ToInt32(HiddenFieldAcc["CurrentTaskCode"]);
        }
        set
        {
            HiddenFieldAcc["CurrentTaskCode"] = value.ToString();
        }
    }
    private int _StructureSkeletonId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldAcc["StructureSkeletonId"]);
        }
        set
        {
            HiddenFieldAcc["StructureSkeletonId"] = value.ToString();
        }
    }    

    public int _FundationDifference
    {
        get
        {
            return Convert.ToInt32(HiddenFieldAcc["FundationDifference"]);
        }
        set
        {
            HiddenFieldAcc["FundationDifference"] = value.ToString();
        }
    }
    public string _CitName
    {
        get
        {
            return HiddenFieldAcc["CitName"].ToString();
        }
        set
        {
            HiddenFieldAcc["CitName"] = value.ToString();
        }
    }
    public string _MunName
    {
        get
        {
            return HiddenFieldAcc["MunName"].ToString();
        }
        set
        {
            HiddenFieldAcc["MunName"] = value.ToString();
        }
    }
    private string _OwnerName
    {
        get
        {
            return HiddenFieldAcc["OwnerName"].ToString();
        }
        set
        {
            HiddenFieldAcc["OwnerName"] = value.ToString();
        }
    }
    private string _OwnerMobileNo
    {
        get
        {
            return HiddenFieldAcc["OwnerMobileNo"].ToString();
        }
        set
        {
            HiddenFieldAcc["OwnerMobileNo"] = value.ToString();
        }
    }
    public Boolean _IsPopulationUnder25000
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldAcc["IsPopulationUnder25000"]);
        }
        set
        {
            HiddenFieldAcc["IsPopulationUnder25000"] = value.ToString();
        }
    }
    public int _ProjectStatusId
    {

        get
        {
            return Convert.ToInt32(HiddenFieldAcc["ProjectStatusId"]);
        }
        set
        {
            HiddenFieldAcc["ProjectStatusId"] = value.ToString();
        }
    }
    string HostName = System.Web.HttpContext.Current.Request.UserHostName;
    string Address = System.Web.HttpContext.Current.Request.UserHostAddress;
    KicccPos.IntracterClient IntracterClient;

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        #region Refresh
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

            CallbackPanelMain.JSProperties["cpPaymentIdPOS"] = "";
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx");
                return;
            }

            #region Check PagePermission
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSavePayed.Enabled = btnSavePayed2.Enabled = btnSave2.Enabled = btnSave.Enabled = per.CanNew;

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode2"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode2"].ToString())) != "New"))
            {
                Response.Redirect("OwnerAccounting.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]) + "&PageMode=" + Request.QueryString["PageMode"] + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]));
                return;
            }
            #endregion

            SetKeys();
            SetNewButtonEnabled();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            ConfigParamPcPos();
        }
        //IntracterClient = new KicccPos.IntracterClient();
        if (this.ViewState["BtnSave"] != null)
            btnSavePayed.Enabled = btnSavePayed2.Enabled = this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (IsPostBack && (PageMode == "New" || PageMode == "Edit"))
            SetAccTypeCombo();


        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        SetNewMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!CheckConditions()) return;
        PgMode.Value = Utility.EncryptQS("Edit");
        SetEditMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string AccountingId = Utility.DecryptQS(HDAccountingId.Value);
        if (string.IsNullOrEmpty(PageMode) || Utility.IsDBNullOrNullValue(_ProjectIngridientTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString(), true);
            return;
        }

        if (string.IsNullOrEmpty(AccountingId) && PageMode != "New")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
            if (Utility.IsDBNullOrNullValue(_PlansTypeId) || _PlansTypeId == -1)
            {
                SetLabelWarning("نوع نقشه نامشخص است");
                return;
            }

        if (PageMode != "New" || (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) != (int)TSP.DataManager.TSAccountingPaymentType.EPayment) || !Utility.IsDBNullOrNullValue(txtaNumber.Text.Trim()))
        {
            ArrayList ArrayAcc = (new TSP.DataManager.TechnicalServices.AccountingManager()).CheckAccountingNumberAndReturnFishInfo(txtaNumber.Text.Trim());
            if (Convert.ToBoolean(ArrayAcc[0]) == false)
            {
                if (PageMode != "New" && ArrayAcc[2].ToString() != AccountingId)
                {
                    SetLabelWarning(ArrayAcc[1].ToString());
                    return;
                }
                else if (PageMode == "New" && (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) != (int)TSP.DataManager.TSAccountingPaymentType.EPayment))
                {
                    SetLabelWarning(ArrayAcc[1].ToString());
                    return;
                }
            }
        }

        if (PageMode == "New")
        {
            Insert(false);
        }
        else if (PageMode == "Edit" || PageMode == "EditNumber")
        {
            Edit(int.Parse(AccountingId), false);
        }

    }
    protected void btnSavePayed_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string AccountingId = Utility.DecryptQS(HDAccountingId.Value);
        if (string.IsNullOrEmpty(PageMode) || Utility.IsDBNullOrNullValue(_ProjectIngridientTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString(), true);
            return;
        }

        if (string.IsNullOrEmpty(AccountingId) && PageMode != "New")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
            if (Utility.IsDBNullOrNullValue(_PlansTypeId) || _PlansTypeId == -1)
            {
                SetLabelWarning("نوع نقشه نامشخص است");
                return;
            }

        ArrayList ArrayAcc = (new TSP.DataManager.TechnicalServices.AccountingManager()).CheckAccountingNumberAndReturnFishInfo(txtaNumber.Text.Trim());
        if (Convert.ToBoolean(ArrayAcc[0]) == false)
        {
            if (PageMode != "New" && ArrayAcc[2].ToString() != AccountingId)
            {
                SetLabelWarning(ArrayAcc[1].ToString());
                return;
            }
            else if (PageMode == "New")
            {
                SetLabelWarning(ArrayAcc[1].ToString());
                return;
            }
        }

        if (PageMode == "New")
        {
            Insert(true);
        }
        else if (PageMode == "Edit" || PageMode == "EditNumber")
        {
            Edit(int.Parse(AccountingId), true);
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
            Response.Redirect("ProjectAccounting.aspx?ProjectId=" + HDProjectId.Value
                + "&PageMode=" + Request.QueryString["PageMode"]
                + "&PrjReId=" + HDRequestId.Value
                + "&IngT=" + Utility.EncryptQS(_ProjectIngridientTypeId.ToString())
                + "&PlnId=" + Request.QueryString["PlnId"].ToString()
                + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString()
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt, true);
        else
            Response.Redirect("ProjectAccounting.aspx?ProjectId=" + HDProjectId.Value
                    + "&PageMode=" + Request.QueryString["PageMode"]
                    + "&PrjReId=" + HDRequestId.Value
                    + "&IngT=" + Utility.EncryptQS(_ProjectIngridientTypeId.ToString())
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt, true);
    }
    protected void btnConfigPcPos_Click(object sender, EventArgs e)
    {
        try
        {


            KicccPos.IntracterClient IntracterClient = new KicccPos.IntracterClient();
            string Address = System.Web.HttpContext.Current.Request.UserHostAddress;
            IntracterClient.Endpoint.Address = new EndpointAddress("http://" + "127.0.0.1" + ":50000/KicccPosDriver");
            //if (IPAddress.IsIPv6SiteLocal)
            //{
            //    IntracterClient.Endpoint.Address = new EndpointAddress("http://" + "127.0.0.1" + ":50000/KicccPosDriver");
            //}
            //else
            //{
            //    IntracterClient.Endpoint.Address = new EndpointAddress("http://" + Address + ":50000/KicccPosDriver");
            //}

            //lblPCPosResponce.Text = IntracterClient.SetConfig(PcPosConfig());

        }
        catch (System.Xml.XmlException err)
        {

            lblPCPosResponce.Text += err.Message;
            Utility.SaveWebsiteError(err);
            SetError(err);
        }



    }
    protected void btnBuyPcPos_Click(object sender, EventArgs e)
    {

        try
        {

            // PcPosBuyWeb();
            // PcPosBuy();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }

    }
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string PrjReId = HDRequestId.Value;
        string PageMode = PgMode.Value;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        PrjMainMenu MainMenu = new PrjMainMenu("Accounting", Convert.ToInt32(ProjectId));
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }
    /*************************************************************************************************************************************/

    protected void CallbackPanelMain_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelMain.JSProperties["cpPrintMsg"] = "";
        CallbackPanelMain.JSProperties["cptxtPrice"] = "";
        CallbackPanelMain.JSProperties["cplblPrice"] = "";
        switch (e.Parameter)
        {
            case "AccTypeChange":
                switch (Convert.ToInt32(cmbAccType.SelectedItem.Value))
                {
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                        FillAccountingLabels();
                        CallbackPanelMain.JSProperties["cptxtPrice"] = HiddenFieldAcc["Price"] = txtaAmount.Text = (Convert.ToDecimal(HiddenFieldObserver["Cost"]) == 0) ? "0" : Convert.ToDecimal(HiddenFieldObserver["Cost"]).ToString("0");
                        CallbackPanelMain.JSProperties["cplblPrice"] = lblPrice.Text;
                        SetPaymentId();
                        break;
                }
                break;

            case "PrintFish":
                if (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.POS)
                {
                    CallbackPanelMain.JSProperties["cpPrintMsg"] = "امکان چاپ فیش برای نوع پرداخت دستگاه کارت خوان وجود ندارد.";
                    return;
                }
                CallbackPanelMain.JSProperties["cpPrintFish"] = 1;
                if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
                    CallbackPanelMain.JSProperties["cpPrintFishPath"] = "../../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + HDAccountingId.Value +
                          "&ProjectId=" + HDProjectId.Value + "&PrjReId=" + HDRequestId.Value + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString();
                else
                    CallbackPanelMain.JSProperties["cpPrintFishPath"] = "../../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + HDAccountingId.Value +
                          "&ProjectId=" + HDProjectId.Value + "&PrjReId=" + HDRequestId.Value + "&PlnTypeId=" + Utility.EncryptQS("-1");

                break;
        }

    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        string PrjReqId = Utility.DecryptQS(HDRequestId.Value);
        if (Utility.IsDBNullOrNullValue(PrjReqId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, Convert.ToInt32(PrjReqId));
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }
        string QS = "~/Employee/TechnicalServices/Project/ProjectAccountingInsert.aspx?ProjectId=" + HDProjectId.Value
                  + "&AccountingId=" + Utility.EncryptQS("-1")
                  + "&PageMode=" + Request.QueryString["PageMode"]
                  + "&PageMode2=" + Utility.EncryptQS("New")
                  + "&PrjReId=" + HDRequestId.Value
                  + "&IngT=" + Request.QueryString["IngT"].ToString()
                  + "&PlnId=" + Request.QueryString["PlnId"].ToString()
                  + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString()
                  + "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString()
                  + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString()
                  + "&ReqSender=" + Utility.EncryptQS("AccInsert");

        WFUserControl.QueryStringForRedirect = QS;
        WFUserControl.PerformCallback(Convert.ToInt32(PrjReqId), ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods
    private void SetWarningLabel(string Message)
    {
        lblWarning.Visible = true;
        ImgWarningMsg.ClientVisible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningMsg');</script>");
        lblWarning.Text = Message;
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

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

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void ClearForm()
    {
        cmbPaymentType.DataBind();
        cmbPaymentType.SelectedIndex = 0;
        //cmbAccType.DataBind();
        //cmbAccType.SelectedIndex = 0;
        HiddenFieldAcc["Price"] =
        txtaAmount.Text =
        txtaBank.Text =
        txtaBranchCode.Text =
        txtaBranchName.Text =
        txtaDate.Text =
        txtaNumber.Text =
        txtaDesc.Text = txtPaymentDate.Text = "";
        SetFiche();
    }

    #region SetKeys-Modes
    private void SetKeys()
    {
        try
        {
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode2"].ToString());
            _AccountingId = int.Parse(Utility.DecryptQS(Request.QueryString["AccountingId"].ToString()));
            _ProjectId = int.Parse(Utility.DecryptQS(Request.QueryString["ProjectId"].ToString()));
            _ProjecReqtId = int.Parse(Utility.DecryptQS(Request.QueryString["PrjReId"].ToString()));
            _ProjectIngridientTypeId = int.Parse(Utility.DecryptQS(Request.QueryString["IngT"]));


            _PlansId = -1; _PlansTypeId = -1;
            string PrjPgMd = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());

            if (string.IsNullOrEmpty(PrjPgMd) || _ProjectId == -1 || _ProjecReqtId == -1 || string.IsNullOrEmpty(_PageMode) || Utility.IsDBNullOrNullValue(_ProjectIngridientTypeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            _PlansId = int.Parse(Utility.DecryptQS(Request.QueryString["PlnId"]).ToString());
            _PlansTypeId = int.Parse(Utility.DecryptQS(Request.QueryString["PlnTypeId"]).ToString());
            if (!string.IsNullOrEmpty(Request.QueryString["ReqSender"])
               && _PageMode == "New")
            {
                try
                {
                    CheckNewConditionAndReSetValues();
                }
                catch (Exception err)
                {
                    Utility.SaveWebsiteError(err);
                    return;
                }
            }

            FillProjectInfo(Convert.ToInt32(_ProjecReqtId));
            _CurrentTaskCode = TSP.DataManager.WorkFlowPermission.GetCurrentTaskCode_StaticFunc((int)TSP.DataManager.TableCodes.TSProjectRequest, _ProjecReqtId);
            SetMode();
            lblWarningObs.Visible = false;
            SetPaymentId();
        }
        catch (Exception Err)
        {
            Utility.SaveWebsiteError(Err);
            SetLabelWarning("خطایی در بازیابی اطلاعات بوجود آمده است.");
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
                SetViewMode();
                break;

            case "New":
                SetNewMode();
                break;

            case "Edit":
                SetEditMode();
                break;
            case "EditNumber":
                SetEditNumber();
                break;
        }
    }

    protected void SetNewMode()
    {
        SetControlsNewMode();
        // فیلتر لیست بابت
        SetAccTypeCombo();

        if (_CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject )//|| IfObserverExistInCurrentReq())
        {
            if (Utility.DecryptQS(PgMode.Value) != "View")
            {
                //SetLabelWarning("به علت انتخاب ناظران در این درخواست یا مرحله گردش کار امکان ثبت فیش وجود ندارد");
                SetLabelWarning("با توجه به مرحله گردش کار امکان ثبت فیش وجود ندارد");
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
            }
        }

        FillAccountingLabels();
        txtaDate.Text = Utility.GetDateOfToday();
    }

    protected void SetViewMode()
    {
        // SetAccTypeCombo();
        FillControls();
        SetControlsViewMode();
    }

    protected void SetEditMode()
    {
        SetControlsEditMode();
        SetAccTypeCombo();
        FillControls();
        FillAccountingLabels();

    }

    private void SetEditNumber()
    {
        SetControlsEditMode();
        SetAccTypeCombo();
        FillControls();
        FillAccountingLabels();

        cmbPaymentType.Enabled = true;
        txtaNumber.Enabled = true;
        txtaDate.Enabled = true;
        txtPaymentDate.Enabled = true;
        txtaAmount.ClientEnabled = false;
        txtaBank.Enabled = false;
        txtaBranchCode.Enabled = false;
        txtaBranchName.Enabled = false;
        cmbAccType.Enabled = false;

    }

    private void SetControlsNewMode()
    {
        btnSavePayed.Enabled = btnSavePayed2.Enabled = btnSave2.Enabled = btnSave.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnPrintFish.Enabled = btnPrintFish2.Enabled = false;
        CheckAccess();
        ClearForm();
        SetControlsEnable(true);
        RoundPanelAccounting.HeaderText = "جدید";
        lblPrice.ClientVisible = true;
        lblPriceDesc.ClientVisible = true;
    }

    private void SetControlsViewMode()
    {
        btnSavePayed2.Enabled = btnSavePayed2.Enabled = btnSave2.Enabled = btnSave.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        CheckAccess();
        SetControlsEnable(false);
        RoundPanelAccounting.HeaderText = "مشاهده";
        lblPrice.ClientVisible = false;
        lblPriceDesc.ClientVisible = false;
    }

    private void SetControlsEditMode()
    {
        btnSavePayed2.Enabled = btnSavePayed2.Enabled = false;
        btnSave2.Enabled = btnSave.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();
        SetControlsEnable(true);
        RoundPanelAccounting.HeaderText = "ویرایش";
        lblPrice.ClientVisible = true;
        lblPriceDesc.ClientVisible = true;
    }
    #endregion

    #region Set Enable-Invisible
    private void SetControlsEnable(bool Enable)
    {
        cmbPaymentType.Enabled =
        txtaNumber.Enabled =
        txtaDate.Enabled =
        txtaBank.Enabled =
        txtaBranchCode.Enabled =
        txtaBranchName.Enabled =
        cmbAccType.Enabled =
        txtPaymentDate.Enabled = Enable;
    }

    private void SetControlsByPaymentType(int PaymentType)
    {
        switch (PaymentType)
        {
            case (int)TSP.DataManager.AccountingPaymentType.Fiche:
                SetFiche();
                break;
            case (int)TSP.DataManager.AccountingPaymentType.Cheque:
                SetCheque();
                break;
            case (int)TSP.DataManager.AccountingPaymentType.POS:
                SetPos();
                break;
            case (int)TSP.DataManager.AccountingPaymentType.EPayment:
                SetEpayment();
                break;
        }
    }
    protected void SetEpayment()
    {
        ASPxLabelBank.ClientVisible = false;
        ASPxLabelBranchCode.ClientVisible = false;
        ASPxLabelBranchName.ClientVisible = false;
        txtaBank.ClientVisible = false;
        txtaBranchCode.ClientVisible = false;
        txtaBranchName.ClientVisible = false;
        lblFishNo.Text = "شناسه پیگیری";
        btnPos.ClientVisible = false;
        btnPos2.ClientVisible = false;
        txtPCPosResponce.ClientVisible = false;
        txtaNumber.ClientEnabled = false;
        if (Convert.ToInt64(HiddenFieldAcc["Price"]) < 500000000)
        {
            txtaAmount.Text = HiddenFieldAcc["Price"].ToString();
            txtaAmount.ClientEnabled = false;
        }
        else
            txtaAmount.ClientEnabled = true;
    }
    protected void SetFiche()
    {
        ASPxLabelBank.ClientVisible = false;
        ASPxLabelBranchCode.ClientVisible = false;
        ASPxLabelBranchName.ClientVisible = false;
        txtaBank.ClientVisible = false;
        txtaBranchCode.ClientVisible = false;
        txtaBranchName.ClientVisible = false;
        lblFishNo.Text = "شماره فیش";
        btnPos.ClientVisible = false;
        btnPos2.ClientVisible = false;
        txtaAmount.ClientEnabled = false;
        txtaAmount.Text = HiddenFieldAcc["Price"].ToString();
    }

    protected void SetPos()
    {
        ASPxLabelBank.ClientVisible = false;
        ASPxLabelBranchCode.ClientVisible = false;
        ASPxLabelBranchName.ClientVisible = false;
        txtaBank.ClientVisible = false;
        txtaBranchCode.ClientVisible = false;
        txtaBranchName.ClientVisible = false;
        lblFishNo.Text = "شماره واریز";
        btnPos.ClientVisible = true;
        btnPos2.ClientVisible = true;
        txtaAmount.ClientEnabled = false;
        txtaAmount.Text = HiddenFieldAcc["Price"].ToString();

    }

    protected void SetCheque()
    {
        ASPxLabelBank.ClientVisible = true;
        ASPxLabelBranchCode.ClientVisible = true;
        ASPxLabelBranchName.ClientVisible = true;
        txtaBank.ClientVisible = true;
        txtaBranchCode.ClientVisible = true;
        txtaBranchName.ClientVisible = true;
        lblFishNo.Text = "شماره فیش";
        btnPos.ClientVisible = false;
        btnPos2.ClientVisible = false;
        txtaAmount.ClientEnabled = true;
    }

    #endregion

    #region Fill Info
    protected void FillAccountingLabels()
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        int PrjReqId = Convert.ToInt32(Utility.DecryptQS(HDRequestId.Value));
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
        TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();

        if (cmbAccType.SelectedIndex != -1)
        {
            switch (Convert.ToInt32(cmbAccType.SelectedItem.Value))//.ToString())
            {
                #region Observer Fish
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche://"1"
                    decimal ObsPrice;

                    ObsPrice = Convert.ToDecimal(ProjectRequestManager.GetObserversPriceByingredientsPercentList(ProjectId, PrjReqId, 100, GetIngredientsPercentList())[0]);

                    lblPriceDesc.Text = "دستمزد ناظرین : ";
                    lblPrice.Text = (ObsPrice == 0) ? "0" : ObsPrice.ToString("#,#") + " ریال";
                    lblWarningPrice.Text = "";
                    HiddenFieldObserver["Cost"] = ObsPrice;

                    if (PageMode == "New")
                        HiddenFieldAcc["Price"] = txtaAmount.Text = (ObsPrice == 0) ? "0" : ObsPrice.ToString("0");
                    break;

                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation://قبلا 5 درصد بوده در حال حاضر 3 درصد شده 
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    decimal Obs3PersentPrice;

                    Obs3PersentPrice = Convert.ToDecimal(ProjectRequestManager.GetObserversPriceByingredientsPercentList(ProjectId, PrjReqId, 3, GetIngredientsPercentList())[0]);

                    lblPriceDesc.Text = "دستمزد ناظرین : ";
                    lblPrice.Text = (Obs3PersentPrice == 0) ? "0" : Obs3PersentPrice.ToString("#,#") + " ریال";
                    lblWarningPrice.Text = "";
                    HiddenFieldObserver["Cost"] = Obs3PersentPrice;
                    if (PageMode == "New")
                        HiddenFieldAcc["Price"] = txtaAmount.Text = (Obs3PersentPrice == 0) ? "0" : Obs3PersentPrice.ToString("0");
                    break;
                    #endregion

            }
        }
    }

    public void FillControls()
    {
        int AccountingId = Convert.ToInt32(Utility.DecryptQS(HDAccountingId.Value));
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(AccountingId);
        if (AccountingManager.Count != 1)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        HiddenFieldAcc["Price"] = txtaAmount.Text = Convert.ToInt64(AccountingManager[0]["Amount"]).ToString("0");
        txtaBank.Text = AccountingManager[0]["Bank"].ToString();
        txtaBranchCode.Text = AccountingManager[0]["BranchCode"].ToString();
        txtaBranchName.Text = AccountingManager[0]["BranchName"].ToString();
        txtaDate.Text = AccountingManager[0]["Date"].ToString();
        txtPaymentDate.Text = AccountingManager[0]["PaymentDate"].ToString();
        txtaDesc.Text = AccountingManager[0]["Description"].ToString();
        txtaNumber.Text = AccountingManager[0]["Number"].ToString().Trim();
        cmbAccType.DataBind();
        cmbAccType.SelectedIndex = cmbAccType.Items.FindByValue(AccountingManager[0]["AccType"]).Index;
        cmbPaymentType.SelectedIndex = cmbPaymentType.Items.FindByValue(AccountingManager[0]["Type"].ToString()).Index;


        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));


    }

    protected void FillProjectInfo(int ProjecReqtId)
    {
        prjInfo.Fill(ProjecReqtId);
        _MunId = prjInfo.MunId;
        _FundationDifference = prjInfo.FundationDifference;
        _GroupId = prjInfo.GroupId;
        _AgentCode = prjInfo.AgentCode;
        _AgentCodeForPaymentIdProvince = prjInfo.AgentCodeForPaymentIdProvince;
        _Foundation = prjInfo.Foundation;
        _StructureSkeletonId = prjInfo.StructureSkeletonId;
        _OwnerMobileNo = prjInfo.OwnerMobileNo;
        _OwnerName = prjInfo.OwnerName;
        _MunName = prjInfo.MunName;
        _CitName = prjInfo.CitName;
        _IsPopulationUnder25000 = prjInfo.IsPopulationUnder25000;
        _ProjectStatusId = prjInfo.ProjectStatusId;
    }


    /// <summary>
    /// فیلتر لیست بابت
    /// </summary>
    void SetAccTypeCombo()
    {
        if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject)
        {
            //cmbAccType.SelectedIndex = 0;//دستمزد ناظرین            
            ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString()
                + " OR " + "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString()
                + " OR " + "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString();
            cmbAccType.DataBind();
            HiddenFieldAcc["PcPosType"] = 2;
        }
        else
        {
            cmbAccType.SelectedIndex = -1;
            if (Utility.DecryptQS(PgMode.Value) != "View")
            {
                SetLabelWarning("در این مرحله از گردش کار امکان ثبت فیش وجود ندارد");
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
            }
        }
    }

    #endregion

    #region Insert-Update
    protected void Insert(Boolean AutoPayment)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();

        TransactionManager.Add(AccountingManager);

        try
        {
            TransactionManager.BeginSave();
            int PrjReId = Convert.ToInt32(Utility.DecryptQS(HDRequestId.Value));
            int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
            DataRow dr = AccountingManager.NewRow();

            int AccType = Convert.ToInt32(cmbAccType.Value);
            int TableTypeId = -1;
            int TableType = -1;

            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    #region Observer

                    TableTypeId = PrjReId;
                    TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
                    #endregion
                    break;
            }

            if ((TableType == -1) || (TableTypeId == -1))
            {
                SetLabelWarning("نوع فیش نامشخص است");
                return;
            }

            dr["TableTypeId"] = TableTypeId;
            dr["TableType"] = TableType;
            dr["AccType"] = AccType;



            if (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.Fiche || Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.POS)
            {
                dr["Bank"] = DBNull.Value;
                dr["BranchCode"] = DBNull.Value;
                dr["BranchName"] = DBNull.Value;
            }
            else
            {
                dr["Bank"] = txtaBank.Text;
                dr["BranchCode"] = txtaBranchCode.Text;
                dr["BranchName"] = txtaBranchName.Text;
            }
            dr["Type"] = cmbPaymentType.Value;
            dr["Number"] = txtaNumber.Text;
            dr["Date"] = txtaDate.Text;
            dr["PaymentDate"] = txtPaymentDate.Text;
            dr["Amount"] = txtaAmount.Text;
            dr["Description"] = txtaDesc.Text;
            if (cmbPaymentType.Value.ToString() == "3" || AutoPayment)//*****POS
                dr["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            else
                dr["Status"] = (int)TSP.DataManager.TSAccountingStatus.SaveInDB;
            dr["IsSMSSent"] = 0;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            AccountingManager.AddRow(dr);
            AccountingManager.Save();

            TransactionManager.EndSave();

            HDAccountingId.Value = Utility.EncryptQS(AccountingManager[0]["AccountingId"].ToString());
            if (AutoPayment)
            {
                PgMode.Value = Utility.EncryptQS("View");
                SetViewMode();
            }
            else
            {
                PgMode.Value = Utility.EncryptQS("Edit");
                SetEditMode();
            }

            if (Convert.ToInt32(cmbPaymentType.Value) == (int)TSP.DataManager.AccountingPaymentType.EPayment)
            {
                string SmsResult = "";
                SendSms(AccType, _AccountingId);
                SetLabelWarning("ذخیره فیش با موفقیت انجام شد" + " " + SmsResult);
            }
            else
                SetLabelWarning("ذخیره انجام شد");
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            string GroupName = "";
            switch(_GroupId)
            {
                case (int)TSP.DataManager.TSStructureGroups.A:
                    GroupName = "الف";
                    break;
                case (int)TSP.DataManager.TSStructureGroups.B :
                    GroupName = "ب";
                    break;
                case (int)TSP.DataManager.TSStructureGroups.C:
                    GroupName = "ج";
                    break;
                case (int)TSP.DataManager.TSStructureGroups.D:
                    GroupName = "د";
                    break;
            }
            
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _ProjecReqtId, (int)TSP.DataManager.TableType.TSAccounting, "ثبت فیش جدید نظارت بدون ناظر  "+" -متراژ پروژه"+_Foundation.ToString()+"-گروه ساختمانی "+ GroupName + (_IsPopulationUnder25000 ? "-جمعیت شهر پروژه زیر 25000 نفر است " : ""), Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.InsertNewRow);

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }

        if (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.Fiche)
            SetFiche();
        else if (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.Cheque)
            SetCheque();
        else if (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.POS)
            SetPos();
    }

    protected void Edit(int AccountingId, Boolean AutoPayment)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();

        TransactionManager.Add(AccountingManager);
        try
        {
            int AccType = Convert.ToInt32(cmbAccType.Value);
            int PrjReId = Convert.ToInt32(Utility.DecryptQS(HDRequestId.Value));
            int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));

            TransactionManager.BeginSave();
            AccountingManager.FindByAccountingId(AccountingId);
            if (AccountingManager.Count != 1)
            {
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            AccountingManager[0].BeginEdit();
            //AccountingManager[0]["TableTypeId"] = TableTypeId;
            //AccountingManager[0]["TableType"] = TableType;
            //AccountingManager[0]["AccType"] = AccType;          

            AccountingManager[0]["Type"] = cmbPaymentType.Value;
            if (cmbPaymentType.Value.ToString() == "3" || AutoPayment)//*****POS
                AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            else
                AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.SaveInDB;

            if (cmbPaymentType.Value.ToString() == "1")
            {
                AccountingManager[0]["Bank"] = DBNull.Value;
                AccountingManager[0]["BranchCode"] = DBNull.Value;
                AccountingManager[0]["BranchName"] = DBNull.Value;
            }
            else
            {
                AccountingManager[0]["Bank"] = txtaBank.Text;
                AccountingManager[0]["BranchCode"] = txtaBranchCode.Text;
                AccountingManager[0]["BranchName"] = txtaBranchName.Text;
            }

            AccountingManager[0]["Number"] = txtaNumber.Text;
            AccountingManager[0]["Description"] = txtaDesc.Text;
            AccountingManager[0]["Date"] = txtaDate.Text;
            AccountingManager[0]["PaymentDate"] = txtPaymentDate.Text;
            AccountingManager[0]["Amount"] = txtaAmount.Text;
            AccountingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            AccountingManager[0]["ModifiedDate"] = DateTime.Now;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();

            TransactionManager.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetControlsEditMode();
            SetLabelWarning("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }

        if (cmbPaymentType.Value.ToString() == "1")
            SetFiche();
        else if (cmbPaymentType.Value.ToString() == "2")
            SetCheque();
        else if (cmbPaymentType.Value.ToString() == "3")
            SetPos();
    }
    #endregion

    #region Check Obs-Imp-Des-Owner Exist
    private bool IfObserverExist()
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        ProjectObserversManager.FindByProjectId(ProjectId);
        if (ProjectObserversManager.Count > 0)
            return true;
        else
            return false;
    }
    //**این قسمت کامنت شد چون در قوانین جدید اضافه اشکوب اینکه بخشی از ناظرین به صورت دستی ثبت شده باشند و محاسبات با ضریب یک انجام شود در نظر گرفته می شود
    //private bool IfObserverExistInCurrentReq()
    //{

    //    TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
    //    ProjectObserversManager.FindByPrjReId(_ProjecReqtId, 0);
    //    if (ProjectObserversManager.Count > 0)
    //        return true;
    //    else
    //        return false;
    //}
    private bool IsImplementerExist()
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        TSP.DataManager.TechnicalServices.Project_ImplementerManager Project_ImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        Project_ImplementerManager.FindActivesByProjectId(ProjectId);
        if (Project_ImplementerManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool IsDesignerExist()
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        TSP.DataManager.TechnicalServices.Project_DesignerManager Project_DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        Project_DesignerManager.FindActivesByProjectId(ProjectId);
        if (Project_DesignerManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool IsOwnerExist()
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        OwnerManager.FindActivesByProjectId(ProjectId);
        if (OwnerManager.Count > 0)
            return true;
        else
            return false;
    }
    #endregion

    #region Set Warning-Error
    private void SetLabelWarning(string Warning)
    {
        //this.DivReport.Visible = true;
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Warning;
    }

    private void SetError(Exception err)
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
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
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
    #endregion

    #region WorkFlow

    void SetNewButtonEnabled()
    {

        if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject)
        {
            btnNew.Enabled = btnNew2.Enabled = true;
            btnEdit.Enabled = btnEdit2.Enabled = true;
        }
        else
        {
            btnNew.Enabled = btnNew2.Enabled = false;
            btnEdit.Enabled = btnEdit2.Enabled = false;
        }
    }

    /// <summary>
    /// Check for diffrent Plan's Type
    /// </summary>
    /// <returns></returns>

    #endregion

    #region Check Condition
    bool CheckConditions()
    {
        int AccountingId = Convert.ToInt32(Utility.DecryptQS(HDAccountingId.Value));
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(AccountingId);
        if (AccountingManager.Count == 1)
        {
            if (Convert.ToInt32(AccountingManager[0]["Type"]) != (int)TSP.DataManager.TSAccountingPaymentType.POS
                && Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
            {
                SetLabelWarning("امکان تغییر فیش پرداخت شده وجود ندارد");
                return false;
            }
        }
        int AccType = Convert.ToInt32(cmbAccType.Value);
        switch (AccType)
        {
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                if (_CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject )//|| !IfObserverExistInCurrentReq())
                {
                    SetLabelWarning("در این مرحله از گردش کار امکان تغییر اطلاعات فیش دستمزد ناظرین وجود ندارد ");
                    return false;
                }
                break;
        }
        return true;
    }
    #endregion

    private DataTable GetIngredientsPercentList()
    {
        int StructureSkeletonId = _StructureSkeletonId == 3 ? 3 : -1;
        int ProjectFoundation = 0;
        //***تنها در حالتی که زمین فاقد ساختمان باشد ارجاع کل زیر بنای پروژه در نظر گرفته می شود در بقیه موارد توسعه بنا میزان توسعه برای ارجاع ملاک می باشد********************
        if (_ProjectStatusId != (int)TSP.DataManager.TSProjectStatus.BuildingNotStarted && _FundationDifference > 0)
        {
            ProjectFoundation = _FundationDifference;
        }
        else
        {
            ProjectFoundation = _Foundation;
        }
        string ExecptionMajorIdList = "";
        if (_GroupId == (int)TSP.DataManager.TSStructureGroups.A && prjInfo.StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory && _IsPopulationUnder25000)//***گروه ساختمانی الف اسکلت بنایی و  جمعیت شهر زیر 25000 نفر
        {
            ExecptionMajorIdList = ((int)TSP.DataManager.MainMajors.Electronic).ToString() + "," + ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
        }
        else
            ExecptionMajorIdList = "";
        TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();
        DataTable dtProjectIngridien = ProjectIngridientMajorsManager.SelectTSProjectObserverMajorByProjectInfo(_GroupId, ProjectFoundation, StructureSkeletonId, ExecptionMajorIdList);
        return dtProjectIngridien;
    }

    #region Check Condition For New Acc In WF

    private void CheckNewConditionAndReSetValues()
    {
        if (_CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject)// || IfObserverExistInCurrentReq())
        {
            if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
                Response.Redirect("ProjectAccounting.aspx?ProjectId=" + HDProjectId.Value
                    + "&PageMode=" + Request.QueryString["PageMode"]
                    + "&PrjReId=" + HDRequestId.Value
                    + "&IngT=" + Utility.EncryptQS(_ProjectIngridientTypeId.ToString())
                    + "&PlnId=" + Request.QueryString["PlnId"].ToString()
                    + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString()
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString() + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString(), true);
            else
                Response.Redirect("ProjectAccounting.aspx?ProjectId=" + HDProjectId.Value
                        + "&PageMode=" + Request.QueryString["PageMode"]
                        + "&PrjReId=" + HDRequestId.Value
                        + "&IngT=" + Utility.EncryptQS(_ProjectIngridientTypeId.ToString())
                        + "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString() + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString(), true);
        }

    }

    #endregion

    private void ConfigParamPcPos()
    {
        int AccType = Convert.ToInt32(cmbAccType.SelectedItem.Value);
        TSP.DataManager.EmployeeManager employeeManager = new TSP.DataManager.EmployeeManager();
        employeeManager.FindByCode(Utility.GetCurrentUser_MeId());

        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["SerialNoPcPos"]))
            HiddenFieldAcc["SerialNoPcPos"] = employeeManager[0]["SerialNoPcPos"].ToString();
        else
            HiddenFieldAcc["SerialNoPcPos"] = "5000000000";

        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["AcceptorIdPcPos"]))
            HiddenFieldAcc["AcceptorIdPcPos"] = employeeManager[0]["AcceptorIdPcPos"].ToString();
        else
            HiddenFieldAcc["AcceptorIdPcPos"] = "000000000000000";
        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["ComPortPcPos"]))
            HiddenFieldAcc["ComPortPcPos"] = employeeManager[0]["ComPortPcPos"].ToString();
        else
            HiddenFieldAcc["ComPortPcPos"] = "Com0";

        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["TerminalIdPcPos"]))
            HiddenFieldAcc["TerminalIdPcPos"] = employeeManager[0]["TerminalIdPcPos"].ToString();
        else
            HiddenFieldAcc["TerminalIdPcPos"] = "00000000";


        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["SerialNoPcPos2"]))
            HiddenFieldAcc["SerialNoPcPos2"] = employeeManager[0]["SerialNoPcPos2"].ToString();
        else
            HiddenFieldAcc["SerialNoPcPos2"] = "5000000000";

        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["AcceptorIdPcPos2"]))
            HiddenFieldAcc["AcceptorIdPcPos2"] = employeeManager[0]["AcceptorIdPcPos2"].ToString();
        else
            HiddenFieldAcc["AcceptorIdPcPos2"] = "000000000000000";
        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["ComPortPcPos2"]))
            HiddenFieldAcc["ComPortPcPos2"] = employeeManager[0]["ComPortPcPos2"].ToString();
        else
            HiddenFieldAcc["ComPortPcPos2"] = "Com0";

        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["TerminalIdPcPos2"]))
            HiddenFieldAcc["TerminalIdPcPos2"] = employeeManager[0]["TerminalIdPcPos2"].ToString();
        else
            HiddenFieldAcc["TerminalIdPcPos2"] = "00000000";



    }

    private void SetPaymentId()
    {

        if (cmbAccType.Value == null)
        {
            return;
        }
        int AccType = Convert.ToInt32(cmbAccType.SelectedItem.Value);
        string TafziliCode = "";
        string PaymentIdPOS = "";
        string TafziliCodeProvince = "";
        Boolean IsKardanPayment = false;
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();

        switch (AccType)
        {
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                #region Observer
                ProjectObserversManager.FindActivesByProjectId(_ProjectId);
                TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS(TSP.DataManager.TSAccountingAccType.ObserversFiche, _ProjectId.ToString(), "99999");// ProjectId.ToString();
                TafziliCodeProvince = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince(TSP.DataManager.TSAccountingAccType.ObserversFiche, _AgentCodeForPaymentIdProvince, _ProjectId.ToString(), "99999");
                #endregion
                break;
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:

                TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, _ProjectId.ToString(), "99999");// ObsMeId;
                TafziliCodeProvince = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince((TSP.DataManager.TSAccountingAccType)AccType, _AgentCodeForPaymentIdProvince, _ProjectId.ToString(), "99999");
                break;

        }
       
        HiddenFieldAcc["PaymentIdPOS"] = PaymentIdPOS = TSP.Utility.OnlinePayment.GetPaymentIdForPOS((TSP.DataManager.TSAccountingAccType)Convert.ToInt32(cmbAccType.SelectedItem.Value), TafziliCode, TafziliCodeProvince, true, 0, _MunId, IsKardanPayment);
        //cmbDesigner.JSProperties["cpPaymentIdPOS"] = PaymentIdPOS;
        CallbackPanelMain.JSProperties["cpPaymentIdPOS"] = PaymentIdPOS;

    }


    #region SendSms

    private string SendSms(int AccType, int AccountingId)
    {
        string ReturnValue = "";
        try
        {

            string SMSBody = "";
            string MobileNo = "";
            TSP.DataManager.UserType SMSUltId = TSP.DataManager.UserType.Member;
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
            DataTable dtAcc = AccountingManager.SelectTSAccountingPayerMobileNo(AccountingId);
            if (dtAcc.Rows.Count != 1)
            {
                ReturnValue = "اطلاعات توسط کاربر دیگری تغییر یافته است";
                return ReturnValue;
            }

            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                    MobileNo = dtAcc.Rows[0]["FishPayerMobileNo"].ToString();
                    SMSUltId = TSP.DataManager.UserType.Member;
                    SMSBody = "طراح محترم "
                        +
                        (AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5Percent ? "معماری " :
                        AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure ? "سازه " :
                        AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation ? "تاسیسات " : "")
                        + " فیش پنج درصد سهم سازمان از طراحی مربوط به پروژه " + _ProjectId.ToString() + " به مالکیت خانم/آقای " + _OwnerName + " " + "در سامانه سازمان نظام مهندسی ساختمان استان فارس ثبت گردید.جهت پرداخت حداکثر ظرف 48 ساعت در پرتال اعضا قسمت مدیریت فیش های پرداخت نشده اقدام نمایید"
                        + "\n"
                       + "https://fceo.ir/Members/Accounting/EpaymentFishes.aspx?P=N";

                    break;
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    MobileNo = _OwnerMobileNo;
                    SMSUltId = TSP.DataManager.UserType.TSProjectOwner;
                    SMSBody = "خانم/آقای " + _OwnerName + " " + " مالک محترم پروژه ساختمانی مربوط به شهر " + _CitName + " و شهرداری " + _MunName + " با زیربنای " + _Foundation + " متر مربع در سامانه نظام مهندسی ساختمان استان فارس ثبت گردیده است.جهت پرداخت وجه حق الزحمه نظارت با مشخصات کاربری زیر و از طریق لینک زیر  اقدام نمایید"
                     + "\n"
                       + "https://fceo.ir/Owner/ProjectAccounting.aspx"
                       + "\n"
                     + "نام کاربری " + "prj" + _ProjectId.ToString()
                     + "\n" +
                     "در صورت در دسترس نداشتن رمز عبور از گزینه ''رمزعبور را فراموش کرده ام'' استفاده نمایید"
                    ;
                    break;
            }
            if (string.IsNullOrWhiteSpace(MobileNo))
            {

                ReturnValue = "شماره همراه جهت ارسال پیامک برای این فیش یافت نشد";
                return ReturnValue;
            }
            SendSMSNotification(Utility.Notifications.NotificationTypes.TSProjectOwner, SMSBody, MobileNo, _ProjectId.ToString(), SMSUltId);
            AccountingManager.FindByAccountingId(AccountingId);
            if (dtAcc.Rows.Count != 1)
            {
                ReturnValue = "اطلاعات توسط کاربر دیگری تغییر یافته است";
                return ReturnValue;
            }
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["IsSMSSent"] = 1;
            AccountingManager[0]["SendSMSDate"] = Utility.GetDateOfToday();
            AccountingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            AccountingManager[0]["ModifiedDate"] = DateTime.Now;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ReturnValue = "خطا ارسال پیامک ایجاد شده است";
        }
        return ReturnValue;
    }

    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId, TSP.DataManager.UserType SMSUltId)
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
            dr["SMSUltId"] = ((int)SMSUltId).ToString();
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms contain TempPass
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }
    #endregion
    #endregion
}