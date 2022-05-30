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
using DevExpress.Web;
using System.Web.Services.Protocols;
using System.ServiceModel;
using System.Net;


public partial class Employee_TechnicalServices_Project_ProjectAccountingDesignerInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    #region Peroperties


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

    private int _ProjecReqtId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(RequestId.Value));
        }
        set
        {
            RequestId.Value = Utility.EncryptQS(value.ToString());
        }
    }

    private int _CurrentTaskCode
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldAcc["CurrentTaskCode"].ToString()));
        }
        set
        {
            HiddenFieldAcc["CurrentTaskCode"] = Utility.EncryptQS(value.ToString());
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

            cmbDesigner.JSProperties["cpPaymentIdPOS"] = "";
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
            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            ConfigParamPcPos();
        }

        if (this.ViewState["btnNew"] != null)
            btnNew.Enabled = btnNew2.Enabled = (bool)this.ViewState["btnNew"];
        if (this.ViewState["BtnSave"] != null)
            btnSavePayed.Enabled = btnSavePayed2.Enabled = this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];

        if (IsPostBack && (_PageMode == "New" || _PageMode == "Edit"))
            SetAccTypeCombo();
        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        _PageMode = "New";
        SetNewMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!CheckConditions()) return;
        _PageMode = "Edit";
        SetEditMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_PageMode) || Utility.IsDBNullOrNullValue(_ProjectIngridientTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString(), true);
            return;
        }


        if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
            if (Utility.IsDBNullOrNullValue(_PlansTypeId) || _PlansTypeId == -1)
            {
                SetLabelWarning("نوع نقشه نامشخص است");
                return;
            }
        if (Convert.ToInt32(cmbPaymentType.Value) != (int)TSP.DataManager.AccountingPaymentType.EPayment)
        {
            ArrayList ArrayAcc = (new TSP.DataManager.TechnicalServices.AccountingManager()).CheckAccountingNumberAndReturnFishInfo(txtaNumber.Text.Trim());
            if (Convert.ToBoolean(ArrayAcc[0]) == false)
            {
                if (_PageMode != "New" && ArrayAcc[2].ToString() != _AccountingId.ToString())
                {
                    SetLabelWarning(ArrayAcc[1].ToString());
                    return;
                }
                else if (_PageMode == "New")
                {
                    SetLabelWarning(ArrayAcc[1].ToString());
                    return;
                }
            }
        }

        if (_PageMode == "New")
        {
            Insert(false);
        }
        else if (_PageMode == "Edit" || _PageMode == "EditNumber")
        {
            Edit(_AccountingId, false);
        }
    }

    protected void btnSavePayed_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_PageMode) || Utility.IsDBNullOrNullValue(_ProjectIngridientTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString(), true);
            return;
        }

        if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
            if (Utility.IsDBNullOrNullValue(_PlansTypeId) || _PlansTypeId == -1)
            {
                SetLabelWarning("نوع نقشه نامشخص است");
                return;
            }
        if (_PageMode != "New" || (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) != (int)TSP.DataManager.TSAccountingPaymentType.EPayment) || !Utility.IsDBNullOrNullValue(txtaNumber.Text.Trim()))
        {
            ArrayList ArrayAcc = (new TSP.DataManager.TechnicalServices.AccountingManager()).CheckAccountingNumberAndReturnFishInfo(txtaNumber.Text.Trim());
            if (Convert.ToBoolean(ArrayAcc[0]) == false)
            {
                if (_PageMode != "New" && ArrayAcc[2].ToString() != _AccountingId.ToString())
                {
                    SetLabelWarning(ArrayAcc[1].ToString());
                    return;
                }
                else if (_PageMode == "New")
                {
                    SetLabelWarning(ArrayAcc[1].ToString());
                    return;
                }
            }
        }

        if (_PageMode == "New")
        {
            Insert(true);
        }
        else if (_PageMode == "Edit" || _PageMode == "EditNumber")
        {
            Edit(_AccountingId, true);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
            Response.Redirect("ProjectAccountingDesigner.aspx?ProjectId=" + Utility.EncryptQS(_ProjectId.ToString())
                + "&PageMode=" + Request.QueryString["PageMode"]
                + "&PrjReId=" + Utility.EncryptQS(_ProjecReqtId.ToString())
                + "&IngT=" + Utility.EncryptQS(_ProjectIngridientTypeId.ToString())
                + "&PlnId=" + Request.QueryString["PlnId"].ToString()
                + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString()
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt, true);
        else
            Response.Redirect("ProjectAccountingDesigner.aspx?ProjectId=" + Utility.EncryptQS(_ProjectId.ToString())
                    + "&PageMode=" + Request.QueryString["PageMode"]
                    + "&PrjReId=" + Utility.EncryptQS(_ProjecReqtId.ToString())
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

        }
        catch (System.Xml.XmlException err)
        {

            txtPCPosResponce.Text += err.Message;
            Utility.SaveWebsiteError(err);
            SetError(err);
        }



    }

    protected void btnBuyPcPos_Click(object sender, EventArgs e)
    {

        try
        {

            //PcPosBuyWeb();
            // PcPosBuy();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }

    }

    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        PrjMainMenu MainMenu = new PrjMainMenu("Accounting", _ProjectId);
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, _ProjecReqtId.ToString(), _PageMode, GrdFlt, SrchFlt));
    }
    /*************************************************************************************************************************************/
    protected void cmbPlanType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearForm();
        _PlansTypeId = Convert.ToInt32(cmbPlanType.Value);
        SetAccTypeSelectedIndex(_PlansTypeId);
        FillDesigner(_PlansTypeId, "");
    }

    protected void cmbDesigner_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetDesignertCostByDesigner();
            SetPaymentId();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }

    protected void CallbackPanelMain_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelMain.JSProperties["cpPrintMsg"] = "";
        switch (e.Parameter)
        {
            case "AccTypeChange":
                switch (Convert.ToInt32(cmbAccType.SelectedItem.Value))
                {
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                    case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                        FillAccountingLabels();
                        HiddenFieldAcc["Price"] = txtaAmount.Text = (Convert.ToDecimal(HiddenFieldObserver["Cost"]) == 0) ? "0" : Convert.ToDecimal(HiddenFieldObserver["Cost"]).ToString("0");
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
                    CallbackPanelMain.JSProperties["cpPrintFishPath"] = "../../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + Utility.EncryptQS(_AccountingId.ToString()) +
                          "&ProjectId=" + Utility.EncryptQS(_ProjectId.ToString()) + "&PrjReId=" + Utility.EncryptQS(_ProjecReqtId.ToString()) + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString();
                else
                    CallbackPanelMain.JSProperties["cpPrintFishPath"] = "../../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + Utility.EncryptQS(_AccountingId.ToString()) +
                          "&ProjectId=" + Utility.EncryptQS(_ProjectId.ToString()) + "&PrjReId=" + Utility.EncryptQS(_ProjecReqtId.ToString()) + "&PlnTypeId=" + Utility.EncryptQS("-1");

                break;
        }

    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        string PrjReqId = Utility.DecryptQS(RequestId.Value);
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
        string QS = "~/Employee/TechnicalServices/Project/ProjectAccountingDesignerInsert.aspx?ProjectId=" + Utility.EncryptQS(_ProjectId.ToString())
                  + "&AccountingId=" + Utility.EncryptQS("-1")
                  + "&PageMode=" + Request.QueryString["PageMode"]
                  + "&PageMode2=" + Utility.EncryptQS("New")
                  + "&PrjReId=" + Utility.EncryptQS(_ProjecReqtId.ToString())
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

        if (_PageMode == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (_PageMode == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    protected void ClearForm()
    {
        cmbAccType.DataBind();
        cmbAccType.SelectedIndex = -1;
        cmbPaymentType.DataBind();
        cmbPaymentType.SelectedIndex = 0;
        lblPrice.Text = "";
        HiddenFieldAcc["Price"] =
        txtWage.Text =
        txtPriceArchive.Text =
        txtaAmount.Text =
        txtaBank.Text =
        txtaBranchCode.Text =
        txtaBranchName.Text =
        txtaNumber.Text = "";
        txtaDesc.Text = txtPaymentDate.Text = txtPCPosResponce.Text = "";
        txtaDate.Text = Utility.GetDateOfToday();
        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
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

            _PlansTypeId = -1;
            HiddenFieldAcc["PaymentIdPOS"] = "";
            string PrjPgMd = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());

            if (string.IsNullOrEmpty(PrjPgMd) || _ProjectId == -1 || _ProjecReqtId == -1 || string.IsNullOrEmpty(_PageMode) || Utility.IsDBNullOrNullValue(_ProjectIngridientTypeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            ObjectDataSourcePlanType.SelectParameters["ProjectId"].DefaultValue = _ProjectId.ToString();
            cmbPlanType.DataBind();

            FillProjectInfo(_ProjecReqtId);

            SetMode();
            CheckWorkFlowPermission();
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
            case "New":
                SetNewMode();
                break;

            case "Edit":
                SetEditMode();
                break;
            case "View":
                SetViewMode();
                break;
            case "EditNumber":
                SetEditNumber();
                break;
        }
    }

    protected void SetNewMode()
    {

        btnSavePayed.Enabled = btnSavePayed2.Enabled = btnSave2.Enabled = btnSave.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnPrintFish.Enabled = btnPrintFish2.Enabled = false;
        CheckAccess();
        cmbPlanType.DataBind();
        cmbPlanType.SelectedIndex = -1;
        ClearForm();
        SetControlsEnable(true);
        RoundPanelAccounting.HeaderText = "جدید";
        lblPrice.ClientVisible = true;
        lblPriceDesc.ClientVisible = true;
        SetAccTypeCombo();
        SetControlEnableAndValue();
        FillAccountingLabels();
    }

    protected void SetViewMode()
    {
        FillControls();
        FillAccountingLabels();
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
        txtaNumber.ClientEnabled = true;
        txtaDate.Enabled = txtPaymentDate.Enabled = true;
        txtaAmount.ClientEnabled = false;
        txtaBank.Enabled = false;
        txtaBranchCode.Enabled = false;
        txtaBranchName.Enabled = false;
        cmbAccType.Enabled = false;
        cmbDesigner.Enabled = false;
        cmbPlanType.Enabled = false;
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
        cmbPaymentType.Enabled = Enable;
        txtaNumber.ClientEnabled = Enable;
        txtaDate.Enabled = txtPaymentDate.Enabled = Enable;
        txtaAmount.ClientEnabled = Enable;
        txtaBank.Enabled = Enable;
        txtaBranchCode.Enabled = Enable;
        txtaBranchName.Enabled = Enable;
        cmbAccType.Enabled = Enable;
        cmbDesigner.Enabled = Enable;
        cmbPlanType.Enabled = Enable;
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

    #endregion

    #region Fill Info
    protected void FillAccountingLabels()
    {
        if (cmbAccType.SelectedIndex == -1)
            return;
        switch (Convert.ToInt32(cmbAccType.SelectedItem.Value))
        {
            case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                SetDesignertCostByDesigner();
                break;
        }

    }

    private void SetDesignertCostByDesigner()
    {
        if (cmbDesigner.Value == null)
        {
            lblPrice.Text = "0";
            HiddenFieldAcc["Price"] = txtaAmount.Text = "0";
            return;
        }
        _DesignerPlansId = Convert.ToInt32(cmbDesigner.Value);

        if (Utility.IsDBNullOrNullValue(_PlansTypeId) || _PlansTypeId == -1 || Utility.IsDBNullOrNullValue(_DesignerPlansId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();

        PlansManager.SelectMaxVersionForFish(_ProjectId, 0, _PlansTypeId);
        if (PlansManager.Count <= 0)
            SetWarningLabel("به علت عدم تایید نقشه، مبلغ فیش قابل محاسبه نمی باشد");
        decimal DesPrice;
        TSP.DataManager.TechnicalServices.PlansManager.DesignerInfo DesignerInfo = new TSP.DataManager.TechnicalServices.PlansManager.DesignerInfo();
        DesPrice = PlansManager.Get5PercentPriceByStep(_ProjectId, _PlansTypeId, _ProjecReqtId, _DesignerPlansId, ref DesignerInfo);
        txtPriceArchive.Text = DesignerInfo.PricaArchiveName;
        txtWage.Text = DesignerInfo.Wage.ToString();
        lblPriceDesc.Text = "کل مبلغ قابل پرداخت بر اساس متراژ دستمزد و تعرفه سال جاری : ";
        lblPrice.Text = (DesPrice == 0) ? "0" : DesPrice.ToString("#,#") + " ریال";
        lblWarningPrice.Text = "";
        if (_PageMode == "New")
            HiddenFieldAcc["Price"] = txtaAmount.Text = (DesPrice == 0) ? "0" : DesPrice.ToString("0");
    }

    public void FillControls()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(_AccountingId);
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

        if (Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.Designing5Percent
            || Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation
            || Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure)
        {
            #region //*******پنج درصد طراحی*********
            int PrjDesignerId = Convert.ToInt32(AccountingManager[0]["TableTypeId"]);//Designer_PlansManager[0]["PrjDesignerId"]);
            TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
            Designer_PlansManager.FindByDesignerId(PrjDesignerId);
            if (Designer_PlansManager.Count == 1)
            {
                cmbPlanType.DataBind();
                cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(Designer_PlansManager[0]["PlansTypeId"].ToString()).Index;
                _PlansTypeId = Convert.ToInt32(Designer_PlansManager[0]["PlansTypeId"]);
                FillDesigner(_PlansTypeId, "");
                cmbDesigner.SelectedIndex = cmbDesigner.Items.FindByValue(Designer_PlansManager[0]["DesignerPlansId"].ToString()).Index;
            }
            #endregion
        }

        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
    }

    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _MunId = prjInfo.MunId;
        _AgentCode = prjInfo.AgentCode;
        _AgentCodeForPaymentIdProvince = prjInfo.AgentCodeForPaymentIdProvince;        
        _OwnerMobileNo = prjInfo.OwnerMobileNo;
        _OwnerName = prjInfo.OwnerName;
        _Foundation = prjInfo.Foundation;
        _MunName = prjInfo.MunName;
        _CitName = prjInfo.CitName;
        _CurrentTaskCode = prjInfo.CurrentTaskCode;
    }

    void FillDesigner(int PlansTypeId, string PlansTypeIdList)
    {
        ObjdDesigner.SelectParameters["ProjectId"].DefaultValue = _ProjectId.ToString();
        ObjdDesigner.SelectParameters["PlansTypeId"].DefaultValue = PlansTypeId.ToString();
        ObjdDesigner.SelectParameters["PlansTypeIdList"].DefaultValue = PlansTypeIdList;
        cmbDesigner.DataBind();
        cmbDesigner.SelectedIndex = -1;
    }

    /// <summary>
    /// فیلتر لیست بابت
    /// </summary>
    void SetAccTypeCombo()
    {
        if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject)
        {
            ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.Designing5Percent).ToString();
            cmbAccType.DataBind();
            HiddenFieldAcc["PcPosType"] = 1;
        }
        else if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject)
        {
            ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure).ToString();
            cmbAccType.DataBind();
            HiddenFieldAcc["PcPosType"] = 1;
        }
        else if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
        || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject)
        {
            ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation).ToString();
            cmbAccType.DataBind();
            HiddenFieldAcc["PcPosType"] = 1;
        }
        else if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
        {
            ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation).ToString() + " OR " + "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure).ToString();
            cmbAccType.DataBind();
            HiddenFieldAcc["PcPosType"] = 1;
        }
        else
        {
            cmbAccType.SelectedIndex = -1;
            if (_PageMode != "View")
            {
                SetLabelWarning("در این مرحله از گردش کار امکان ثبت فیش وجود ندارد");
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
            }
        }
    }

    private void SetAccTypeSelectedIndex(int PlansTypeId)
    {
        switch (PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Memari:
                cmbAccType.SelectedIndex = cmbAccType.Items.FindByValue(((int)TSP.DataManager.TSAccountingAccType.Designing5Percent)).Index;
                break;
            case (int)TSP.DataManager.TSPlansType.Sazeh:
                cmbAccType.SelectedIndex = cmbAccType.Items.FindByValue(((int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure)).Index;
                break;
            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                cmbAccType.SelectedIndex = cmbAccType.Items.FindByValue(((int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation)).Index;
                break;
            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                cmbAccType.SelectedIndex = cmbAccType.Items.FindByValue(((int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation)).Index;
                break;
        }
    }
    #endregion
    private void SetControlEnableAndValue()
    {
        if ((_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo
           || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
           || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
        || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
        || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject
        || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject))
        {
            switch (_CurrentTaskCode)
            {
                case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
                    break;

                case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.Sazeh;
                    break;

                case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatBargh;
                    break;

                case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatMechanic;
                    break;
                case (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject:
                    _PlansTypeId = -1;
                    break;
            }

            if (_PlansTypeId == -1)
                cmbPlanType.SelectedIndex = -1;
            else if(cmbPlanType.Items.Count!=0)
            {
                cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(_PlansTypeId.ToString()).Index;
                SetAccTypeSelectedIndex(_PlansTypeId);
            }

            FillDesigner(_PlansTypeId,
                _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject ? ((int)(int)TSP.DataManager.TSPlansType.TasisatMechanic).ToString()
                + "," + ((int)(int)TSP.DataManager.TSPlansType.TasisatBargh)
                + "," + ((int)(int)TSP.DataManager.TSPlansType.Sazeh) : "");
        }
        else
        {

            SetLabelWarning("در این مرحله از گردش کار پروژه امکان ثبت فیش طراحی جدید وجود ندارد");
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
        }
    }

    #region Insert-Update
    protected void Insert(Boolean AutoPayment)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();

        try
        {
            DataRow dr = AccountingManager.NewRow();

            int AccType = Convert.ToInt32(cmbAccType.Value);
            int TableTypeId = -1;
            int TableType = -1;

            switch (AccType)
            {

                case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                    #region Designer

                    Designer_PlansManager.FindByDesignerPlansId(_DesignerPlansId);
                    if (Designer_PlansManager.Count != 1)
                    {
                        SetLabelWarning("امکان ثبت فیش وجود ندارد.نقشه تایید نشده و یا طراح نقشه نامشخص می باشد");
                        return;
                    }

                    TableTypeId = Convert.ToInt32(Designer_PlansManager[0]["PrjDesignerId"]);
                    TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer);
                    #endregion
                    break;
                default:
                    SetLabelWarning("خطایی در ذخیره انجام شد");
                    return;
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

            if (cmbPaymentType.Value.ToString() == "1" || cmbPaymentType.Value.ToString() == "3")
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
            dr["IsSMSSent"] = 0;
            if (cmbPaymentType.Value.ToString() == "3" || AutoPayment)//*****POS
                dr["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            else
                dr["Status"] = (int)TSP.DataManager.TSAccountingStatus.SaveInDB;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            AccountingManager.AddRow(dr);
            AccountingManager.Save();

            _AccountingId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
            if (AutoPayment)
            {
                _PageMode = "View";
                SetViewMode();
            }
            else
            {
                _PageMode = "Edit";
                SetEditMode();
            }

            if (Convert.ToInt32(cmbPaymentType.Value) == (int)TSP.DataManager.AccountingPaymentType.EPayment)
            {
                string SmsResult = SendSms(AccType, _AccountingId);
                SetLabelWarning("ذخیره فیش با موفقیت انجام شد" + " " + SmsResult);
            }
            else
                SetLabelWarning("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
    }

    protected void Edit(int AccountingId, Boolean AutoPayment)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();

        try
        {
            AccountingManager.FindByAccountingId(AccountingId);
            if (AccountingManager.Count != 1)
            {
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            AccountingManager[0].BeginEdit();

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
            _PageMode = "Edit";
            SetControlsEditMode();
            SetLabelWarning("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
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



    private void CheckWorkFlowPermission()
    {

        if (_PageMode == "New")
            CheckWorkFlowPermissionForSave("New", _CurrentTaskCode);
        else
            CheckWorkFlowPermissionForEdit();
    }
    private void CheckWorkFlowPermissionForSave(string PageMode, int TaskCode)
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(TaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        btnNew.Enabled = WFPer.BtnNew;
        btnNew2.Enabled = WFPer.BtnNew;
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit()
    {
        TSP.DataManager.WFPermission PerProject = CheckProjectWorkFlowPermissionForEdit();
        //TSP.DataManager.WFPermission PerPlanType = CheckPlanWorkFlowPermissionByPlanType();
        //**************در مرحله ثبت نقشه های گردش کار پروژه باشد و یا در مرحله ثبت اطلاعات گردش کار تغییرات نقشه باشد و همچنین نوع نقشه مورد نظر باشد**************
        btnNew2.Enabled = btnNew.Enabled = PerProject.BtnNew;// && PerPlanType.BtnNew;
        btnEdit2.Enabled = btnEdit.Enabled = PerProject.BtnEdit;// && PerPlanType.BtnEdit;

        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    /// <summary>
    /// Check for diffrent Plan's Type
    /// </summary>
    /// <returns></returns>
    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForEdit()
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;
        int StructureAndInstallationPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, _ProjecReqtId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, _ProjecReqtId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, _ProjecReqtId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, _ProjecReqtId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructureAndInstallationPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructureAndInstallationPlanTaskCode, WFCode, _ProjecReqtId, Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit || PerStructureAndInstallationPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave || PerStructureAndInstallationPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew || PerStructureAndInstallationPlan.BtnNew);
        WFPer.BtnInactive = (PerAtchitecturalPlan.BtnInactive || PerStructurePlan.BtnInactive || PerElectricalInsPlan.BtnInactive || PerMechanicInsPlan.BtnInactive || PerStructureAndInstallationPlan.BtnInactive);

        return WFPer;
    }
   

    #endregion

    #region Check Condition
    bool CheckConditions()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(_AccountingId);
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
            case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:

                if (_CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo
                 && _CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
                 && _CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
                 && _CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
                 && _CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject)
                //&& IsDesignerExist())
                {
                    SetLabelWarning("در این مرحله از گردش کار امکان تغییر اطلاعات فیش دستمزد طراحان وجود ندارد ");
                    return false;
                }
                break;
        }
        return true;
    }

    public void CheckCurrentStepsByGroup(int PrjReqId)
    {
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        string msg = BlockManager.CheckCurrentMaxStepByGroup(PrjReqId);
        if (!string.IsNullOrEmpty(msg))
        {
            SetWarningLabel(msg);
        }
        //  else lblWarning.Text = "";
    }
    
    #endregion

    private void SetPaymentId()
    {
        if (cmbDesigner.Value == null)
        {
            return;
        }
        if (cmbAccType.Value == null)
        {
            return;
        }
        _DesignerPlansId = Convert.ToInt32(cmbDesigner.Value);
        int AccType = Convert.ToInt32(cmbAccType.SelectedItem.Value);
        string TafziliCode = "";
        string PaymentIdPOS = "";
        string TafziliCodeProvince = "";
        Boolean IsKardanPayment = false;
        #region Designer

        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();

        DataTable dtDesignerPlansManager = Designer_PlansManager.FindPrjDesignerIdByDesignerPlansId(_DesignerPlansId);
        int PrjDesignerId = Convert.ToInt32(dtDesignerPlansManager.Rows[0]["PrjDesignerId"]);
        ProjectOfficeMembersManager.SelectProjectMembersByPrjImpObsDsgnId((int)TSP.DataManager.TSProjectIngridientType.Designer, PrjDesignerId);
        if (ProjectOfficeMembersManager.Count > 0)
        {
            string MeId = "";
            for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
            {

                MeId = ProjectOfficeMembersManager[0]["MeOthPId"].ToString();
            }

            if (_MunId == 76)
            {
                //**برای صدرا اول کدعضویت باید باشد بعد کد پروژه 
                TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, MeId, _ProjectId.ToString());
            }
            else
            {
                TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, _ProjectId.ToString(), MeId);
            }
            TafziliCodeProvince = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince((TSP.DataManager.TSAccountingAccType)AccType, _AgentCodeForPaymentIdProvince, _ProjectId.ToString(), MeId);
        }
        else
        {
            Designer_PlansManager.FindByDesignerId(PrjDesignerId);
            if (Designer_PlansManager.Count == 1)
            {
                if (Convert.ToInt32(Designer_PlansManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.OtherPerson)
                {
                    IsKardanPayment = true;

                }
                string MeId = Designer_PlansManager[0]["OfficeEngOId"].ToString();
                TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, _ProjectId.ToString(), MeId);// 
                if (Convert.ToInt32(Designer_PlansManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.OtherPerson)
                {
                    TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
                    OthpManager.FindByCode(Convert.ToInt32(Designer_PlansManager[0]["OfficeEngOId"]));
                    MeId = OthpManager[0]["OtpCode"].ToString();

                    TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, _ProjectId.ToString(), MeId);
                    TafziliCodeProvince = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince((TSP.DataManager.TSAccountingAccType)AccType, _AgentCodeForPaymentIdProvince, _ProjectId.ToString(), MeId);
                }
                else
                {
                    if (_MunId == 76)
                    {
                        //**برای صدرا اول کدعضویت باید باشد بعد کد پروژه 
                        TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, MeId, _ProjectId.ToString());
                    }
                    else
                    {
                        TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, _ProjectId.ToString(), MeId);
                    }
                    TafziliCodeProvince = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince((TSP.DataManager.TSAccountingAccType)AccType, _AgentCodeForPaymentIdProvince, _ProjectId.ToString(), MeId);
                }

            }
        }
        #endregion
        HiddenFieldAcc["PaymentIdPOS"] = PaymentIdPOS = TSP.Utility.OnlinePayment.GetPaymentIdForPOS((TSP.DataManager.TSAccountingAccType)Convert.ToInt32(cmbAccType.SelectedItem.Value), TafziliCode, TafziliCodeProvince, true, 0, _MunId, IsKardanPayment);
        cmbDesigner.JSProperties["cpPaymentIdPOS"] = PaymentIdPOS;
        CallbackPanelMain.JSProperties["cpPaymentIdPOS"] = PaymentIdPOS;
    }
    private void ConfigParamPcPos()
    {
        int AccType = -1;
        if (cmbAccType.SelectedItem != null)
            AccType = Convert.ToInt32(cmbAccType.SelectedItem.Value);
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
        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["ComPortPcPos"]))
            HiddenFieldAcc["ComPortPcPos2"] = employeeManager[0]["ComPortPcPos2"].ToString();
        else
            HiddenFieldAcc["ComPortPcPos2"] = "Com0";

        if (!Utility.IsDBNullOrNullValue(employeeManager[0]["TerminalIdPcPos2"]))
            HiddenFieldAcc["TerminalIdPcPos2"] = employeeManager[0]["TerminalIdPcPos2"].ToString();
        else
            HiddenFieldAcc["TerminalIdPcPos2"] = "00000000";

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
                    string Password = "";
                    if (!Convert.ToBoolean(dtAcc.Rows[0]["IsSMSSent"]))
                    {
                        ReturnValue = ResetOwnerPass(_ProjectId, ref Password);
                        if (!string.IsNullOrWhiteSpace(ReturnValue))
                        {
                            return ReturnValue;
                        }
                    }

                    MobileNo = _OwnerMobileNo;
                    SMSUltId = TSP.DataManager.UserType.TSProjectOwner;
                    SMSBody = "خانم/آقای " + _OwnerName + " مالک محترم پروژه ساختمانی مربوط به شهر " + _CitName + " و شهرداری " + _MunName + " با زیربنای " + _Foundation + " متر مربع در سامانه نظام مهندسی ساختمان استان فارس ثبت گردیده است.جهت پرداخت وجه حق الزحمه نظارت با مشخصات کاربری زیر و از طریق لینک زیر  اقدام نمایید"

                       + "\n"
                       + "https://fceo.ir/Owner/ProjectAccounting.aspx"
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MeId">ProjectID</param>
    protected string ResetOwnerPass(int MeId, ref string Password)
    {
        string ReturnValue = "";
        Password = (new Random().Next(0, 1000000)).ToString();

        int UltId = (int)TSP.DataManager.UserType.TSProjectOwner;
        TSP.DataManager.ResetPasswordManager ResetManager = new TSP.DataManager.ResetPasswordManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();

        trans.Add(LogManager);
        trans.Add(ResetManager);

        try
        {
            LogManager.FindByMeIdUltId(MeId, UltId);
            if (LogManager.Count <= 0)
            {
                ReturnValue = "خطایی در ذخیره انجام گرفته است";
                return ReturnValue;
            }
            trans.BeginSave();

            DataRow dr = ResetManager.NewRow();
            dr["MeId"] = MeId;
            dr["Type"] = UltId;
            dr["RequestUserId"] = LogManager[0]["UserId"].ToString();
            dr["RequestIpAddress"] = Request.UserHostAddress;
            dr["RequestDate"] = Utility.GetDateOfToday();
            dr["RequestTime"] = Utility.GetCurrentTime();
            dr["RequestDateTime"] = DateTime.Now;
            dr["RequestDateTimeDetail"] = DateTime.Now.ToFileTime();
            dr["IsChangePass"] = true;
            dr["ChangeDate"] = Utility.GetDateOfToday();
            dr["ChangeTime"] = Utility.GetCurrentTime();
            dr["ChangeDateTime"] = DateTime.Now;
            dr["ChangeIpAddress"] = Request.UserHostAddress;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            ResetManager.AddRow(dr);
            if (ResetManager.Save() <= 0)
            {
                trans.CancelSave();
                ReturnValue = "خطایی در ذخیره انجام گرفته است";
                return ReturnValue;
            }

            LogManager[0].BeginEdit();
            LogManager[0]["Password"] = Utility.EncryptPassword(Password);
            LogManager[0]["UserId2"] = Utility.GetCurrentUser_UserId();
            LogManager[0]["ModifiedDate"] = DateTime.Now;
            LogManager[0].EndEdit();
            if (LogManager.Save() <= 0)
            {
                trans.CancelSave();
                ReturnValue = "خطایی در ذخیره انجام گرفته است";
                return ReturnValue;
            }
            trans.EndSave();

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            ReturnValue = "خطایی در ذخیره انجام گرفته است";
            return ReturnValue;
        }

        return ReturnValue;
    }
    #endregion

    #endregion

}