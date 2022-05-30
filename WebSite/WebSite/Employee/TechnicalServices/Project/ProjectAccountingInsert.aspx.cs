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
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.IO;
using System.Web.Services.Protocols;
using System.ServiceModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

public partial class Employee_TechnicalServices_Project_ProjectAccountingInsert : System.Web.UI.Page
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

    public Boolean _IsShahrakSanaati
    {

        get
        {
            return Convert.ToBoolean(HiddenFieldAcc["IsShahrakSanaati"]);
        }
        set
        {
            HiddenFieldAcc["IsShahrakSanaati"] = value.ToString();
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
            SetNewButtonEnabled();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            ConfigParamPcPos();
        }
        if (this.ViewState["BtnSave"] != null)
            btnSavePayed.Enabled = btnSavePayed2.Enabled = this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (IsPostBack && (PageMode == "New" || PageMode == "Edit"))
            SetAccTypeCombo();
        BindObserverCombo();
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
                else if (PageMode == "New")
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
        BindObserverCombo();
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
        if (Convert.ToInt32(cmbPaymentType.Value) != (int)TSP.DataManager.AccountingPaymentType.EPayment)
        {
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
        }

        if (PageMode == "New")
        {
            Insert(true);
        }
        else if (PageMode == "Edit" || PageMode == "EditNumber")
        {
            Edit(int.Parse(AccountingId), true);
        }
        BindObserverCombo();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (_ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
            Response.Redirect("ProjectAccounting.aspx?ProjectId=" + HDProjectId.Value
                + "&PageMode=" + Request.QueryString["PageMode"]
                + "&PrjReId=" + RequestId.Value
                + "&IngT=" + Utility.EncryptQS(_ProjectIngridientTypeId.ToString())
                + "&PlnId=" + Request.QueryString["PlnId"].ToString()
                + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString()
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt, true);
        else
            Response.Redirect("ProjectAccounting.aspx?ProjectId=" + HDProjectId.Value
                    + "&PageMode=" + Request.QueryString["PageMode"]
                    + "&PrjReId=" + RequestId.Value
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
        string PrjReId = RequestId.Value;
        string PageMode = PgMode.Value;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        PrjMainMenu MainMenu = new PrjMainMenu("Accounting", Convert.ToInt32(ProjectId));
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
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
                        CallbackPanelMain.JSProperties["cptxtPrice"] = txtaAmount.Text = (Convert.ToDecimal(HiddenFieldObserver["Cost"]) == 0) ? "0" : Convert.ToDecimal(HiddenFieldObserver["Cost"]).ToString("0");
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
                          "&ProjectId=" + HDProjectId.Value + "&PrjReId=" + RequestId.Value + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString();
                else
                    CallbackPanelMain.JSProperties["cpPrintFishPath"] = "../../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + HDAccountingId.Value +
                          "&ProjectId=" + HDProjectId.Value + "&PrjReId=" + RequestId.Value + "&PlnTypeId=" + Utility.EncryptQS("-1");

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
        string QS = "~/Employee/TechnicalServices/Project/ProjectAccountingInsert.aspx?ProjectId=" + HDProjectId.Value
                  + "&AccountingId=" + Utility.EncryptQS("-1")
                  + "&PageMode=" + Request.QueryString["PageMode"]
                  + "&PageMode2=" + Utility.EncryptQS("New")
                  + "&PrjReId=" + RequestId.Value
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

        cmbAccType.DataBind();
        cmbAccType.SelectedIndex = -1;
        cmbPaymentType.DataBind();
        cmbPaymentType.SelectedIndex = 0;
        lblPrice.Text = "";
        HiddenFieldAcc["Price"] = "";
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


            _PlansId = -1; _PlansTypeId = -1; HiddenFieldAcc["PaymentIdPOS"] = "";
            string PrjPgMd = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());

            if (string.IsNullOrEmpty(PrjPgMd) || _ProjectId == -1 || _ProjecReqtId == -1 || string.IsNullOrEmpty(_PageMode) || Utility.IsDBNullOrNullValue(_ProjectIngridientTypeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            _PlansId = int.Parse(Utility.DecryptQS(Request.QueryString["PlnId"]).ToString());
            _PlansTypeId = int.Parse(Utility.DecryptQS(Request.QueryString["PlnTypeId"]).ToString());
            if (_PlansTypeId != -1)
            {
                CheckWorkFlowPermissionForDesigner();
                ObjectDataSourcePlanType.SelectParameters["ProjectId"].DefaultValue = _ProjectId.ToString();
                cmbPlanType.DataBind();
            }
            else
            {
                ObjectDataSourcePlanType.SelectParameters["ProjectId"].DefaultValue = _ProjectId.ToString();
                // ObjectDataSourcePlanType.SelectParameters["ProjectId"].DefaultValue = "-2";
                cmbPlanType.DataBind();
            }

            FillProjectInfo(Convert.ToInt32(_ProjecReqtId));
            _CurrentTaskCode = TSP.DataManager.WorkFlowPermission.GetCurrentTaskCode_StaticFunc((int)TSP.DataManager.TableCodes.TSProjectRequest, _ProjecReqtId);
            SetMode();
            lblWarningObs.Visible = false;
            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
            Project_ObserversManager.FindObsMother(_ProjectId);
            if (Project_ObserversManager.Count == 0 && !_IsShahrakSanaati)
            {
                lblWarningObs.Visible = true;
                lblWarningObs.Text = "ناظر هماهنگ کننده این پروژه انتخاب نشده است";

            }
            if (Project_ObserversManager.Count > 0)
            {
                lblWarningObs.Visible = true;
                lblWarningObs.Text = "این پروژه دارای " + Project_ObserversManager.Count.ToString() + " ناظر هماهنگ کننده دارد.قبل از ثبت فیش از صحت  ناظران هماهنگ کننده اطمینان حاصل کنید";
            }

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
        //  string PageMode = Utility.DecryptQS(PgMode.Value);
        //   string PrjReqId = Utility.DecryptQS(RequestId.Value);
        //    string ProjectId = Utility.DecryptQS(HDProjectId.Value);

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
        SetControlEnableAndValue();
        BindObserverCombo();
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
        BindObserverCombo();
    }

    private void SetEditNumber()
    {
        SetControlsEditMode();
        SetAccTypeCombo();
        FillControls();
        FillAccountingLabels();
        BindObserverCombo();
        cmbPaymentType.Enabled = true;
        txtaNumber.Enabled = true;
        txtaDate.Enabled = true;
        txtPaymentDate.Enabled = true;
        txtaAmount.ClientEnabled = false;
        txtaBank.Enabled = false;
        txtaBranchCode.Enabled = false;
        txtaBranchName.Enabled = false;
        cmbObserver.ReadOnly = true;
        ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).ReadOnly = true;
        cmbAccType.Enabled = false;
        cmbDesigner.Enabled = false;
        cmbPlanType.Enabled = false;
    }

    private void SetControlsNewMode()
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
        txtaAmount.ClientEnabled =
        txtaBank.Enabled =
        txtaBranchCode.Enabled =
        txtaBranchName.Enabled =
        cmbAccType.Enabled =
        cmbDesigner.Enabled =
        cmbPlanType.Enabled = txtPaymentDate.Enabled = Enable;
        ////RoundPanelAccounting.Enabled = Enable;
        cmbObserver.ReadOnly = !Enable;
        ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).ReadOnly = !Enable;
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


    /// <summary>
    ///نمایش نوع نقشه و نام نقشه را تنظیم می کند
    /// </summary>
    /// <param name="IsVisible"></param>
    private void SetDesignerInfoVisible(Boolean IsVisible)
    {
        cmbPlanType.ClientVisible = IsVisible;
        lblPlanType.ClientVisible = IsVisible;
        cmbDesigner.ClientVisible = IsVisible;
        lblDesigner.ClientVisible = IsVisible;
        txtWage.ClientVisible = IsVisible;
        lblWage.ClientVisible = IsVisible;
        txtPriceArchive.ClientVisible = IsVisible;
        lblPriceArchive.ClientVisible = IsVisible;
    }

    /// <summary>
    ///نمایش لیست ناظران را تنظیم می کند
    /// </summary>
    /// <param name="IsVisible"></param>
    private void SetObserverInfoVisible(Boolean IsVisible)
    {
        lblOserver.ClientVisible = IsVisible;
        cmbObserver.ClientVisible = IsVisible;
    }
    #endregion

    #region Fill Info
    protected void FillAccountingLabels()
    {
        if (cmbAccType.SelectedItem == null) return;
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        int PrjReqId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
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
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche://"1"://??????????????????????/
                    decimal ObsPrice;
                    ObsPrice = ProjectRequestManager.GetObserversPriceByRequest(ProjectId, PrjReqId, 100, GetObserverListIdForFiltring());
                    lblPriceDesc.Text = "دستمزد ناظرین : ";
                    lblPrice.Text = (ObsPrice == 0) ? "0" : ObsPrice.ToString("#,#") + " ریال";
                    lblWarningPrice.Text = "";
                    HiddenFieldObserver["Cost"] = ObsPrice;

                    if (PageMode == "New")
                        HiddenFieldAcc["Price"] = txtaAmount.Text = (ObsPrice == 0) ? "0" : ObsPrice.ToString("0");
                    break;

                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    decimal Obs5PersentPrice;
                    Obs5PersentPrice = ProjectRequestManager.GetObserversPriceByRequest(ProjectId, PrjReqId, 3, GetObserverListIdForFiltring());
                    lblPriceDesc.Text = "دستمزد ناظرین : ";
                    lblPrice.Text = (Obs5PersentPrice == 0) ? "0" : Obs5PersentPrice.ToString("#,#") + " ریال";
                    lblWarningPrice.Text = "";
                    HiddenFieldObserver["Cost"] = Obs5PersentPrice;
                    if (PageMode == "New")
                        HiddenFieldAcc["Price"] = txtaAmount.Text = (Obs5PersentPrice == 0) ? "0" : Obs5PersentPrice.ToString("0");
                    break;
                #endregion
                case (int)TSP.DataManager.TSAccountingAccType._5In1000://"2":
                    decimal OwnerPrice;
                    if (Utility.TSProject_IsBasedOnStep())
                        OwnerPrice = ProjectRequestManager.Get5In1000PriceByStep(PrjReqId);
                    else
                        OwnerPrice = ProjectRequestManager.Get5In1000PriceByRequest(PrjReqId);
                    lblPriceDesc.Text = "هزینه دفترچه فنی ملکی (پنج در هزار): ";
                    lblPrice.Text = (OwnerPrice == 0) ? "0" : OwnerPrice.ToString("#,#") + " ریال";
                    lblWarningPrice.Text = "هزینه دفترچه فنی ملکی: 0.005 * هزینه ساخت هر متر مربع بنا براساس گروه ساختمانی و تعداد طبقات در تعرفه سال مربوطه";
                    if (PageMode != "Edit")
                        HiddenFieldAcc["Price"] = txtaAmount.Text = (OwnerPrice == 0) ? "0" : OwnerPrice.ToString("0");
                    break;

                case (int)TSP.DataManager.TSAccountingAccType._2In1000:// "3":
                    string Year = "";
                    ProjectImplementerManager.FindImpMother(ProjectId);
                    if (ProjectImplementerManager.Count > 0)
                    {
                        int PrjImpId = Convert.ToInt32(ProjectImplementerManager[0]["PrjImpId"]);
                        Year = ContractManager.GetContracetYear(PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                    }
                    if (string.IsNullOrEmpty(Year))
                        SetWarningLabel("به علت ثبت نشدن قرارداد مجری، مبلغ فیش قابل محاسبه نمی باشد");

                    PriceArchiveManager.FindByYear(Year);
                    if (PriceArchiveManager.Count <= 0)
                        SetWarningLabel("در سال جاری تعرفه خدمات مهندسی تعریف نشده است و کلیه محاسبات بر اساس آخرین تعرفه سال گذشته انجام خواهد شد");

                    decimal ImpPrice;
                    if (Utility.TSProject_IsBasedOnStep())
                        ImpPrice = ProjectRequestManager.Get2In1000PriceByStep(ProjectId, PrjReqId);
                    else
                        ImpPrice = ProjectRequestManager.Get2In1000PriceByRequest(ProjectId, PrjReqId);
                    lblPriceDesc.Text = "کل مبلغ قابل پرداخت بر اساس متراژ دستمزد و تعرفه سال جاری : ";
                    lblPrice.Text = (ImpPrice == 0) ? "0" : ImpPrice.ToString("#,#") + " ریال";
                    lblWarningPrice.Text = "";
                    if (PageMode == "New")
                        HiddenFieldAcc["Price"] = txtaAmount.Text = (ImpPrice == 0) ? "0" : ImpPrice.ToString("0");
                    break;

                case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent://"4":

                    SetDesignertCostByDesigner();
                    break;
            }
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
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        int PrjReqId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
        string PageMode = Utility.DecryptQS(PgMode.Value);
        //DesignerPlansManager
        PlansManager.SelectMaxVersionForFish(ProjectId, 0, _PlansTypeId);
        if (PlansManager.Count <= 0)
            SetWarningLabel("به علت عدم تایید نقشه، مبلغ فیش قابل محاسبه نمی باشد");
        decimal DesPrice;
        TSP.DataManager.TechnicalServices.PlansManager.DesignerInfo DesignerInfo = new TSP.DataManager.TechnicalServices.PlansManager.DesignerInfo();
        DesPrice = PlansManager.Get5PercentPriceByStep(ProjectId, _PlansTypeId, PrjReqId, _DesignerPlansId, ref DesignerInfo);
        txtPriceArchive.Text = DesignerInfo.PricaArchiveName;
        txtWage.Text = DesignerInfo.Wage.ToString();
        lblPriceDesc.Text = "کل مبلغ قابل پرداخت بر اساس متراژ دستمزد و تعرفه سال جاری : ";
        lblPrice.Text = (DesPrice == 0) ? "0" : DesPrice.ToString("#,#") + " ریال";
        lblWarningPrice.Text = "";
        if (PageMode == "New")
            HiddenFieldAcc["Price"] = txtaAmount.Text = (DesPrice == 0) ? "0" : DesPrice.ToString("0");
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

        if (Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.Designing5Percent
            || Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation
            || Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure)
        {
            #region //*******پنج درصد طراحی*********
            SetDesignerInfoVisible(true);
            int PrjDesignerId = Convert.ToInt32(AccountingManager[0]["TableTypeId"]);
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
        else if (Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.ObserversFiche
            || Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation
            || Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure)
        {
            SetObserverInfoVisible(true);
        }
        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
        FilterObserverObjectDataSource();
        ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).DataBind();
        string ObserverListName = "";
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        AccountingDetailManager.FindByAccountingId(AccountingId);
        for (int i = 0; i < AccountingDetailManager.Count; i++)
        {
            if (!Utility.IsDBNullOrNullValue(((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).Items.FindByValue(AccountingDetailManager[i]["TableId"].ToString())))
            {
                ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).Items.FindByValue(AccountingDetailManager[i]["TableId"].ToString()).Selected = true;
                if (ObserverListName != "")
                    ObserverListName += ";";
                ObserverListName += ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).Items.FindByValue(AccountingDetailManager[i]["TableId"].ToString()).Text;
            }
        }
        cmbObserver.Text = ObserverListName;
        if (AccountingDetailManager.Count == 0)
        {
            cmbObserver.ClientVisible = false;
            lblOserver.Text = "ثبت فیش فاقد ناظر";
        }
        else
        {
            lblOserver.Text = "ناظرین";
        }
    }

    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _MunId = prjInfo.MunId;
        _FundationDifference = prjInfo.FundationDifference;
        _GroupId = prjInfo.GroupId;
        _AgentCode = prjInfo.AgentCode;
        _AgentCodeForPaymentIdProvince = prjInfo.AgentCodeForPaymentIdProvince;
        _OwnerMobileNo = prjInfo.OwnerMobileNo;
        _OwnerName = prjInfo.OwnerName;
        _Foundation = prjInfo.Foundation;
        _MunName = prjInfo.MunName;
        _CitName = prjInfo.CitName;
        _IsShahrakSanaati = prjInfo.IsShahrakSanaati;
    }

    void FillDesigner(int PlansTypeId, string PlansTypeIdList)
    {
        ObjdDesigner.SelectParameters["ProjectId"].DefaultValue = _ProjectId.ToString();
        ObjdDesigner.SelectParameters["PlansTypeId"].DefaultValue = PlansTypeId.ToString();
        ObjdDesigner.SelectParameters["PlansTypeIdList"].DefaultValue = PlansTypeIdList;
        cmbDesigner.DataBind();
        cmbDesigner.SelectedIndex = -1;
    }

    private void FilterObserverObjectDataSource()
    {
        ObjdObserver.SelectParameters["ProjectId"].DefaultValue = Utility.DecryptQS(HDProjectId.Value);
        ObjdObserver.SelectParameters["PrjReId"].DefaultValue = Utility.DecryptQS(RequestId.Value);
    }

    /// <summary>
    /// فیلتر لیست بابت
    /// </summary>
    void SetAccTypeCombo()
    {
        if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo)
        {
            ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType._5In1000).ToString();
            cmbAccType.DataBind();
        }
        else if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject
             || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectObserverRequestInfo)
        {
            ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString()
                + " OR " + "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString()
                + " OR " + "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString();
            cmbAccType.DataBind();
            HiddenFieldAcc["PcPosType"] = 2;
        }
        else if ((_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject && IsImplementerExist())
             || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectImplementerRequestInfo)
        {
            ObjectDataSourceAccType.FilterExpression = "AccTypeId = " + ((int)TSP.DataManager.TSAccountingAccType._2In1000).ToString();
            cmbAccType.DataBind();
        }

        else if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject)
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
            if (Utility.DecryptQS(PgMode.Value) != "View")
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
    private void BindObserverCombo()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).DataBind();
        ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).Items.Insert(0, new ListEditItem("<همه>", null));

        //if (PageMode != "New" && 
        if (((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).SelectedItems.Count
        == ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).Items.Count - 1)
        {
            ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).Items[0].Selected = true;
            cmbObserver.Text = "<همه>";
        }

        if (!IsPostBack && PageMode == "New")
        {
            ((ASPxListBox)(cmbObserver.FindControl("ListBoxGroupObserver"))).SelectAll();
            cmbObserver.Text = "<همه>";
        }

    }


    #endregion

    private void SetControlEnableAndValue()
    {
        if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo)
        {
            SetDesignerInfoVisible(false);
            SetObserverInfoVisible(false);
            cmbAccType.SelectedIndex = 0;
        }
        else if ((_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject && IfObserverExist())
             || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectObserverRequestInfo)
        {
            SetDesignerInfoVisible(false);
            SetObserverInfoVisible(true);
            FilterObserverObjectDataSource();
            cmbAccType.SelectedIndex = 0;
        }
        else if ((_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject && IsImplementerExist())
             || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectImplementerRequestInfo)
        {
            SetDesignerInfoVisible(false);
            SetObserverInfoVisible(false);
            cmbAccType.SelectedIndex = 0;
        }
        else if ((_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo
           || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
           || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
        || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
        || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject
        || _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject))
        {
            #region SetDesignerSettings
            SetDesignerInfoVisible(true);
            SetObserverInfoVisible(false);

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
            else
            {
                cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(_PlansTypeId.ToString()).Index;
                SetAccTypeSelectedIndex(_PlansTypeId);
            }
            FillDesigner(_PlansTypeId,
                _CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject ? ((int)(int)TSP.DataManager.TSPlansType.TasisatMechanic).ToString()
                + "," + ((int)(int)TSP.DataManager.TSPlansType.TasisatBargh)
                + "," + ((int)(int)TSP.DataManager.TSPlansType.Sazeh) : "");
            #endregion
        }
        else
        {
            if (Utility.DecryptQS(PgMode.Value) != "View")
            {
                SetLabelWarning("در این مرحله از گردش کار امکان ثبت فیش وجود ندارد");
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
            }
        }
    }

    #region Insert-Update
    protected void Insert(Boolean AutoPayment)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        TSP.DataManager.TechnicalServices.Project_ImplementerManager Project_ImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        TransactionManager.Add(AccountingManager);

        try
        {
            TransactionManager.BeginSave();
            int PrjReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
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

                case (int)TSP.DataManager.TSAccountingAccType._5In1000:
                    #region Owner
                    OwnerManager.FindOwnerAgent(ProjectId);
                    if (OwnerManager.Count != 1)
                    {
                        SetLabelWarning("نماینده مالکین نامشخص می باشد");
                        return;
                    }

                    TableTypeId = Convert.ToInt32(OwnerManager[0]["OwnerId"]);
                    TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSOwner);
                    #endregion
                    break;

                case (int)TSP.DataManager.TSAccountingAccType._2In1000:
                    #region Implementer
                    Project_ImplementerManager.FindImpMother(ProjectId);
                    if (Project_ImplementerManager.Count != 1)
                    {
                        SetLabelWarning("نماینده مجری نامشخص می باشد");
                        return;
                    }

                    TableTypeId = Convert.ToInt32(Project_ImplementerManager[0]["PrjImpId"]);
                    TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Implementer);
                    #endregion
                    break;

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
            dr["IsSMSSent"] = 0;
            if (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.POS || AutoPayment)//*****POS
                dr["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            else
                dr["Status"] = (int)TSP.DataManager.TSAccountingStatus.SaveInDB;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            AccountingManager.AddRow(dr);
            AccountingManager.Save();
            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
                    TransactionManager.Add(AccountingDetailManager);
                    if (!InsertAccountingDetail(Convert.ToInt32(AccountingManager[0]["AccountingId"]), AccountingDetailManager))
                    {
                        TransactionManager.CancelSave();
                        //  SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                        return;
                    }
                    TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
                    ProjectRequestManager.FindByCode(PrjReId);
                    ProjectRequestManager.UpdateObserverPrice(ProjectId, PrjReId, TransactionManager);
                    break;
            }
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
                string SmsResult = SendSms(AccType, _AccountingId);
                SetLabelWarning("ذخیره فیش با موفقیت انجام شد" + " " + SmsResult);
            }
            else
                SetLabelWarning("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }


        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
    }

    protected void Edit(int AccountingId, Boolean AutoPayment)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        TSP.DataManager.TechnicalServices.Project_ImplementerManager Project_ImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TransactionManager.Add(AccountingManager);
        try
        {
            int AccType = Convert.ToInt32(cmbAccType.Value);
            int PrjReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
            int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));

            TransactionManager.BeginSave();
            AccountingManager.FindByAccountingId(AccountingId);
            if (AccountingManager.Count != 1)
            {
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            AccountingManager[0].BeginEdit();

            AccountingManager[0]["Type"] = cmbPaymentType.Value;
            if (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.POS || AutoPayment)//*****POS
                AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            else
                AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.SaveInDB;

            if (Convert.ToInt32(cmbPaymentType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.Fiche)
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
            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:


                    TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
                    TransactionManager.Add(AccountingDetailManager);
                    AccountingDetailManager.FindByAccountingId(AccountingId);
                    if (AccountingDetailManager.Count > 0)
                    {
                        if (!UpdateAccountingDetail(AccountingId, AccountingDetailManager))
                        {
                            TransactionManager.CancelSave();
                            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                            return;
                        }
                    }
                    TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
                    ProjectRequestManager.UpdateObserverPrice(ProjectId, PrjReId, TransactionManager);

                    break;
            }
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
        SetControlsByPaymentType(Convert.ToInt32(cmbPaymentType.Value));
    }

    private Boolean InsertAccountingDetail(int AccountingId, TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager)
    {
        ASPxListBox ListBoxGroupObserver = (ASPxListBox)cmbObserver.FindControl("ListBoxGroupObserver");
        if (ListBoxGroupObserver == null)
            return false;
        ListBoxGroupObserver.DataBind();
        if (ListBoxGroupObserver.SelectedItems.Count == 0)
        {
            SetLabelWarning("ناظرین را انتخاب نمایید");
            return false;
        }
        for (int i = 0; i < ListBoxGroupObserver.SelectedItems.Count; i++)
        {
            if (ListBoxGroupObserver.SelectedItems[i].Value != null)
            {
                DataRow dr = AccountingDetailManager.NewRow();
                dr["AccountingId"] = AccountingId;
                dr["TableId"] = Convert.ToInt32(ListBoxGroupObserver.SelectedItems[i].Value);
                dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
                dr["Amount"] = txtaAmount.Text;
                dr["Description"] = "";
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["InActive"] = 0;
                dr["ModifedDate"] = DateTime.Now;

                AccountingDetailManager.AddRow(dr);
                AccountingDetailManager.Save();
                AccountingDetailManager.DataTable.AcceptChanges();
            }
        }
        return true;
    }

    private Boolean UpdateAccountingDetail(int AccountingId, TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager)
    {
        ASPxListBox ListBoxGroupObserver = (ASPxListBox)cmbObserver.FindControl("ListBoxGroupObserver");
        if (ListBoxGroupObserver == null)
            return false;
        if (ListBoxGroupObserver.SelectedItems.Count == 0)
        {
            SetLabelWarning("ناظرین را انتخاب نمایید");
            return false;
        }
        AccountingDetailManager.FindByAccountingId(AccountingId);
        ListBoxGroupObserver.DataBind();
        for (int i = 0; i < ListBoxGroupObserver.Items.Count; i++)
        {
            if (ListBoxGroupObserver.Items[i].Value == null)
                continue;
            if (ListBoxGroupObserver.Items[i].Selected)//If Selected
            {
                AccountingDetailManager.CurrentFilter = "TableId= " + ListBoxGroupObserver.Items[i].Value.ToString();
                if (AccountingDetailManager.DataTable.DefaultView.Count == 0)//If Not In
                {
                    DataRow dr = AccountingDetailManager.NewRow();
                    dr["AccountingId"] = AccountingId;
                    dr["TableId"] = Convert.ToInt32(ListBoxGroupObserver.SelectedItems[i].Value);
                    dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
                    dr["Amount"] = txtaAmount.Text;
                    dr["Description"] = "";
                    dr["UserId"] = Utility.GetCurrentUser_UserId();
                    dr["InActive"] = 0;
                    dr["ModifedDate"] = DateTime.Now;
                    AccountingDetailManager.AddRow(dr);
                    AccountingDetailManager.Save();
                    AccountingDetailManager.DataTable.AcceptChanges();
                }
                AccountingDetailManager.CurrentFilter = "";
            }
            if (!ListBoxGroupObserver.Items[i].Selected)//If Not Selected
            {
                AccountingDetailManager.CurrentFilter = "TableId= " + ListBoxGroupObserver.Items[i].Value.ToString();
                if (AccountingDetailManager.DataTable.DefaultView.Count > 0)//But If Inserted
                {
                    int cnt = AccountingDetailManager.DataTable.DefaultView.Count;
                    for (int j = 0; j < cnt; j++)
                    {
                        AccountingDetailManager.DataTable.DefaultView[j].Delete();
                        AccountingDetailManager.Save();
                        AccountingDetailManager.DataTable.AcceptChanges();
                    }
                }
                AccountingDetailManager.CurrentFilter = "";
            }
        }
        return true;
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
    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
    }

    private void CheckWorkFlowPermissionForEditOnObserverOfProject(string PageMode)
    {
        //*****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
    }


    void SetNewButtonEnabled()
    {
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

        if (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo
          || (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject && IfObserverExist())
          || (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject && IsImplementerExist())
          || (_CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo && IsDesignerExist())
          || _CurrentTaskCode == ArchitecturalPlanTaskCode
          || _CurrentTaskCode == StructurePlanTaskCode
          || _CurrentTaskCode == ElectricalInsPlanTaskCode
          || _CurrentTaskCode == MechanicInsPlanTaskCode)
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

    private void CheckWorkFlowPermissionForDesigner()
    {
        CheckWorkFlowPermissionForEdit();
    }

    private void CheckWorkFlowPermissionForEdit()
    {
        TSP.DataManager.WFPermission PerProject = CheckProjectWorkFlowPermissionForEdit();
        TSP.DataManager.WFPermission PerChangePlans = CheckPlanWorkFlowPermissionForEdit();
        TSP.DataManager.WFPermission PerPlanType = CheckPlanWorkFlowPermissionByPlanType();
        //**************در مرحله ثبت نقشه های گردش کار پروژه باشد و یا در مرحله ثبت اطلاعات گردش کار تغییرات نقشه باشد و همچنین نوع نقشه مورد نظر باشد**************
        btnNew2.Enabled = btnNew.Enabled = (PerProject.BtnNew || PerChangePlans.BtnNew) && PerPlanType.BtnNew;
        //(PerProject.BtnNew || PerChangePlans.BtnNew) && PerPlanType.BtnNew;
        btnEdit2.Enabled = btnEdit.Enabled = (PerProject.BtnEdit || PerChangePlans.BtnEdit) && PerPlanType.BtnEdit;
        //(PerProject.BtnEdit || PerChangePlans.BtnEdit) && PerPlanType.BtnEdit;

        this.ViewState["BtnNew"] = btnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    /// <summary>
    /// Check for diffrent Plan's Type
    /// </summary>
    /// <returns></returns>
    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForEdit()
    {
        //****TableId
        int PrjReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());


        //*****TableId
        //  PlansId = Utility.DecryptQS(HiddenFieldDesPlans["PlnId"].ToString());

        WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;

        //*******Editing Task Code
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, _PlansId, Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit) && (PerPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave) && (PerPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew) && (PerPlan.BtnNew);
        WFPer.BtnInactive = (PerAtchitecturalPlan.BtnInactive || PerStructurePlan.BtnInactive || PerElectricalInsPlan.BtnInactive || PerMechanicInsPlan.BtnInactive) && (PerPlan.BtnInactive);

        return WFPer;
    }

    private TSP.DataManager.WFPermission CheckPlanWorkFlowPermissionForEdit()
    {
        //*****TableId
        //  PlansId = Utility.DecryptQS(HiddenFieldDesPlans["PlnId"].ToString());

        int WFCode = (int)TSP.DataManager.WorkFlows.TSChangePlansAndDesigner;

        //*******Editing Task Code
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo;

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, Convert.ToInt32(_PlansId), Utility.GetCurrentUser_UserId());

        return PerPlan;
    }

    private TSP.DataManager.WFPermission CheckPlanWorkFlowPermissionByPlanType()
    {
        //*****TableId
        int PrjReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int PlanTaskCode = GetCurrentTaskCode(WFCode, Convert.ToInt32(PrjReId));
        bool Permit = false;

        switch (_PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Memari:
                switch (PlanTaskCode)
                {
                    case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                        Permit = true;
                        break;

                    default:
                        Permit = false;
                        break;
                }
                break;

            case (int)TSP.DataManager.TSPlansType.Sazeh:
            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
            case (int)TSP.DataManager.TSPlansType.Shahrsazi:
                switch (PlanTaskCode)
                {
                    case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                    case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                    case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                        Permit = true;
                        break;

                    default:
                        Permit = false;
                        break;
                }
                break;

            default:
                Permit = false;
                break;
        }

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = Permit;
        WFPer.BtnSave = Permit;
        WFPer.BtnNew = Permit;
        WFPer.BtnInactive = Permit;

        return WFPer;
    }

    private int GetCurrentTaskCode(int WfCode, int TableId)
    {
        int CurrentTaskOrder = -2;
        int CurrentTaskCode = -2;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        }

        return CurrentTaskCode;
    }
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
            case (int)TSP.DataManager.TSAccountingAccType._5In1000:
                if (_CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo)
                {
                    SetLabelWarning("در این مرحله از گردش کار امکان تغییر اطلاعات فیش دفترچه فنی ملکی وجود ندارد ");
                    return false;
                }
                break;
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                if (_CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject)
                {

                    SetLabelWarning("در این مرحله از گردش کار امکان تغییر اطلاعات فیش دستمزد ناظرین وجود ندارد ");
                    return false;
                }
                break;
            case (int)TSP.DataManager.TSAccountingAccType._2In1000:
                if (_CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject || !IsImplementerExist())
                {
                    SetLabelWarning("در این مرحله از گردش کار امکان تغییر اطلاعات فیش دستمزد مجریان وجود ندارد ");
                    return false;
                }
                break;
            case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:

                if (_CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo
                 && _CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
                 && _CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
                 && _CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
                 && _CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject
                 && IsDesignerExist())
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
    }
    #endregion

    private string GetObserverListIdForFiltring()
    {
        string List = "";
        ASPxListBox ListBoxGroupObserver = (ASPxListBox)cmbObserver.FindControl("ListBoxGroupObserver");
        if (ListBoxGroupObserver == null)
            return "ProjectObserversId=-2";
        //ListBoxGroupObserver.DataBind();
        if (ListBoxGroupObserver.SelectedItems.Count == 0)
            return "ProjectObserversId=-2";
        for (int i = 0; i < ListBoxGroupObserver.SelectedItems.Count; i++)
        {
            if (ListBoxGroupObserver.SelectedItems[i].Value == null)
                continue;
            List += "ProjectObserversId= " + ListBoxGroupObserver.SelectedItems[i].Value.ToString();
            if (i != ListBoxGroupObserver.SelectedItems.Count - 1)
                List += " OR ";
        }
        return List;
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
            case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                #region Designer
                if (cmbDesigner.Value == null)
                {
                    return;
                }

                TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
                TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();

                _DesignerPlansId = Convert.ToInt32(cmbDesigner.Value);
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

                    //TafziliCode = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTS((TSP.DataManager.TSAccountingAccType)AccType, _ProjectId.ToString(), MeId);// MeId;                    
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
                break;
        }
        
        HiddenFieldAcc["PaymentIdPOS"] = PaymentIdPOS = TSP.Utility.OnlinePayment.GetPaymentIdForPOS((TSP.DataManager.TSAccountingAccType)Convert.ToInt32(cmbAccType.SelectedItem.Value), TafziliCode, TafziliCodeProvince, true, 0, _MunId, IsKardanPayment);
        cmbDesigner.JSProperties["cpPaymentIdPOS"] = PaymentIdPOS;
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
