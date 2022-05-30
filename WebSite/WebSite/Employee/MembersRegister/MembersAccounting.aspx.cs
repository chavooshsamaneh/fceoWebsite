using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
public partial class Employee_MembersRegister_MembersAccounting : System.Web.UI.Page
{
    #region Properties
    private string PageMode
    {
        get
        {
            return HiddenFieldPage["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPage["PageMode"] = value;
        }
    }

    private int MeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["MeId"]);
        }
        set
        {
            HiddenFieldPage["MeId"] = value;
        }
    }

    private int MReId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["MReId"]);
        }
        set
        {
            HiddenFieldPage["MReId"] = value;
        }
    }

    private int AccountingId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["AccountingId"]);
        }
        set
        {
            HiddenFieldPage["AccountingId"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        if (!IsPostBack)
        {
            SetFilterExpression();
            SetLabelRegEnter();
            CheckUserTablePermision();
            SetKey();
            SetMenuItem();
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void MenuTop_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string TP = "";
        if (Request.QueryString["TP"] != null)
        {
            TP = Request.QueryString["TP"];
        }
        //string MReId = Utility.DecryptQS(MemberRequest.Value);

        string Mode = Utility.DecryptQS(HiddenFieldPage["Mode"].ToString());
        //string TempMe = "";
        int IsMeTemp = -1;
        if (Mode == "Request")
            IsMeTemp = 0;
        else if (Mode == "TempMe")
            IsMeTemp = 1;

        //int IsMeTemp = -1;
        //if (!Utility.IsDBNullOrNullValue(HiddenFieldPage["TMe"]))
        //    IsMeTemp = Convert.ToInt32(Utility.DecryptQS(HiddenFieldPage["TMe"].ToString()));
        //string Mode = "Request";
        //if (IsMeTemp == 1)
        //    Mode = "TempMe";
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attach":
                Response.Redirect("MemberAttachment.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Group":
                Response.Redirect("MemberGroups.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "PollAnswer":
                Response.Redirect("ReportMemberPollAnswers.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "AccFish":
                Response.Redirect("MembersAccounting.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&Mode=" + Utility.EncryptQS(Mode) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Utility.EncryptQS(PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Request":
                Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) +
             "&PageMode=" + Utility.EncryptQS("View") +
             "&MReId=" + Utility.EncryptQS(MReId.ToString()) +
             "&Mode=" + Utility.EncryptQS(Mode) +
             "&TP=" + Request.QueryString["TP"] +
             "&GrdFlt=" + Request.QueryString["GrdFlt"] +
             "&SrchFlt=" + Request.QueryString["SrchFlt"] +
             "&TMe=" + Utility.EncryptQS(IsMeTemp.ToString()) +
             "&Pt=" + Utility.EncryptQS("2"));
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Mode = Utility.DecryptQS(HiddenFieldPage["Mode"].ToString());
        string TempMe = "";
        if (Mode == "Request")
            TempMe = "0";
        else if (Mode == "TempMe")
            TempMe = "1";
        Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) +
              "&PageMode=" + Utility.EncryptQS("View") +
              "&MReId=" + Utility.EncryptQS(MReId.ToString()) +
              "&Mode=" + Utility.EncryptQS(Mode) +
              "&TP=" + Request.QueryString["TP"] +
              "&GrdFlt=" + Request.QueryString["GrdFlt"] +
              "&SrchFlt=" + Request.QueryString["SrchFlt"] +
              "&TMe=" + Utility.EncryptQS(TempMe.ToString()) +
              "&Pt=" + Utility.EncryptQS("2"));
        //Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(MeId.ToString())
        //    + "&MReId=" + Request.QueryString["MReId"]
        //    + "&TP=" + Request.QueryString["TP"]
        //    + "&PageMode=" + Utility.EncryptQS(PageMode)
        //    + "&GrdFlt=" + Request.QueryString["GrdFlt"]
        //    + "&SrchFlt=" + Request.QueryString["SrchFlt"]
        //    + "&TMe=" + Utility.EncryptQS(TempMe)
        //    + "&Pt=" + Utility.EncryptQS("2"));
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (cmbaType.SelectedItem == null)
        {
            ShowMessage("نحوه پرداخت را انتخاب نمایید.");
            return;
        }
        if (Convert.ToInt32(cmbaType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.EPayment)
        {
            ShowMessage("مجاز به انتخاب پرداخت الکترونیکی به عنوان نحوه پرداخت نمی باشید.");
            return;
        }
        if (Utility.IsDBNullOrNullValue(MReId))
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return;
        }
        switch (PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                Update(AccountingId);
                break;
        }
    }

    protected void btnPaymentConfirm_Click(object sender, EventArgs e)
    {

        SetFishPaymentStatus(true);
    }
    protected void btnPaymentReject_Click(object sender, EventArgs e)
    {

        SetFishPaymentStatus(false);
    }

    protected void CallBackPage_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallBackPage.JSProperties["cpPrint"] = 0;
        CallBackPage.JSProperties["cpMsg"] = "";
        CallBackPage.JSProperties["cpShowMsg"] = 0;
        switch (e.Parameter)
        {
            case "AccTypeChange":
                SetLabelRegEnter();
                txtaAmount.Text = (Convert.ToInt32(HiddenFieldPage["FishAmount"]) == 0) ? "0" : Convert.ToInt32(HiddenFieldPage["FishAmount"]).ToString("0");
                break;
            case "Print":
                if (AccountingId == -1)
                {
                    CallBackPage.JSProperties["cpShowMsg"] = 1;
                    CallBackPage.JSProperties["cpMsg"] = "ابتدا فیش را ذخیره نمایید";
                    return;
                }
                TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
                AccountingManager.FindByAccountingId(AccountingId);
                if (AccountingManager.Count != 1)
                {
                    CallBackPage.JSProperties["cpShowMsg"] = 1;
                    CallBackPage.JSProperties["cpMsg"] = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations);
                    return;
                }
                if (Convert.ToInt32(AccountingManager[0]["Type"]) != (int)TSP.DataManager.TSAccountingPaymentType.Fiche
                    && Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
                {
                    CallBackPage.JSProperties["cpShowMsg"] = 1;
                    CallBackPage.JSProperties["cpMsg"] = "تنها امکان چاپ پرداخت هایی که نحوه پرداخت آنها فیش می باشد وجود دارد.";
                    return;
                }
                if (!Utility.IsDBNullOrNullValue(AccountingId))
                {
                    CallBackPage.JSProperties["cpPrint"] = 1;
                    CallBackPage.JSProperties["cpPrintPath"] = "../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + Utility.EncryptQS(AccountingId.ToString());
                }
                break;
          
            //case "btnPaymentConfirm":
            //    SetFishPaymentStatus(true);
            //    break;
            //case "btnPaymentReject":
            //    SetFishPaymentStatus(false);
            //    break;

        }
    }

    #region GridMeLicence
    protected void GridViewMemberLicence_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (e.GetValue("MReId") == null)
            return;
        int CurretnMReId = Convert.ToInt32(e.GetValue("MReId").ToString());
        if (MReId == CurretnMReId)
        {
            e.Row.BackColor = System.Drawing.Color.LightGray;
        }
    }

    protected void GridViewMemberLicence_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "EndDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewMemberLicence_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";

    }
    #endregion

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;

        if (Utility.IsDBNullOrNullValue(MReId))
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }

        int MsId = -1;
        MemberRequestManager.FindByCode(MReId);
        if (MemberRequestManager.Count == 1)
        {
            MsId = Convert.ToInt32(MemberRequestManager[0]["MsId"]);
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }

        if (!Utility.IsDBNullOrNullValue(MsId))
        {
            if (MsId == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                WFCode = (int)TSP.DataManager.WorkFlows.MemberTransferConfirming;
        }
        else
        {
            WFUserControl.SetMsgText("نوع درخواست نامشخص می باشد.");
            return;
        }


        WFUserControl.PerformCallback(MReId, TableType, WFCode, e);
        GridViewMemberLicence.DataBind();
    }
    #endregion

    #region Methods
    private void SetWarningLableDisable()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void ShowMessage(string Message)
    {
        // this.DivReport.Attributes.Add("Style", "display:visible");
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void BackToManagementPage()
    {
        string Mode = Utility.DecryptQS(HiddenFieldPage["Mode"].ToString());
        string UserName = "";
        if (Mode == "Request")
            UserName = MeId.ToString();
        else if (Mode == "TempMe")
            UserName = "M" + MeId.ToString();
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Members.aspx?PostId=" + Utility.EncryptQS(UserName) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&MReId=" + Utility.EncryptQS(MReId.ToString()));
        }
        else
        {
            Response.Redirect("Members.aspx?MReId=" + Utility.EncryptQS(MReId.ToString()));
        }
    }

    private void SetKey()
    {
        if (string.IsNullOrEmpty(Request.QueryString["MReId"])
            || string.IsNullOrEmpty(Request.QueryString["MeId"]))
        {
            Response.Redirect("Members.aspx", true);
            return;
        }
        HiddenFieldPage["Mode"] = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();
        PageMode = Utility.DecryptQS(Request.QueryString["PageMode"]);
        MReId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MReId"]));
        MeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"]));
        PageMode = GetPageModeAndSetAccountingId();
        FillFormRequest(MReId);
        BindGrid();
        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = PanelAccountingInserting.Enabled = PanelAccountingInserting.Enabled = CheckWFPermissionForInsertFish();
        SetPageMode();
    }

    private string GetPageModeAndSetAccountingId()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Member);
        //TSP.DataManager.TableTypeManager TableTypeManager = new TSP.DataManager.TableTypeManager();
        AccountingManager.FindByTableTypeId(MReId, TableType);
        if (AccountingManager.Count == 0)
        {
            AccountingId = -1;
            return "New";
        }
        else
        {
            AccountingId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
            return "Edit";
        }
    }

    private void SetPageMode()
    {
        switch (PageMode)
        {
            case "New":
                SetNewMode();
                break;
            case "Edit":
                SetEditMode();
                break;
        }
    }

    private void SetNewMode()
    {
        ClearForm();
        txtCreateDate.Text = Utility.GetDateOfToday();
    }

    private void SetEditMode()
    {
        FillAccounting(AccountingId);
    }

    private void ClearForm()
    {
        txtaAmount.Text = "";
        txtaDate.Text = "";
        txtaDesc.Text = "";
        txtaNumber.Text = "";
        cmbAccType.SelectedIndex = -1;
        cmbaType.SelectedIndex = 0;
    }

    #region FillForm
    /// <summary>
    /// اطلاعات یک عضو را بر اساس دخواست انتخاب شده پر می کند
    /// it is called in following methods : InsertRequest,
    /// </summary>
    /// <param name="MReId"></param>
    protected void FillFormRequest(int MReId)
    {
        try
        {
            // string PageMode = Utility.DecryptQS(PgMode.Value);

            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            TSP.DataManager.TransferMemberManager TransferManager = new TSP.DataManager.TransferMemberManager();
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.Automation.LettersManager LettersManager = new TSP.DataManager.Automation.LettersManager();


            ReqManager.FindByCode(MReId);
            if (ReqManager.Count > 0)
            {
                Boolean IsTemp = false;
                if (ReqManager[0]["IsMeTemp"].ToString() == "True")
                    IsTemp = true;
                //if (ReqManager[0]["IsCreated"].ToString() == "1")
                //{
                //    ASPxRoundPanelAccounting.Visible = true;
                //FillGrid();
                //if (PageMode == "View")
                //    PanelAccountingInserting.Visible = false;
                //}
                //else
                //    PanelAccountingInserting.Visible = false;
                //txtMeStatus.Text = ReqManager[0]["MrsName"].ToString();
                txtMeNo.Text = ReqManager[0]["MeNo"].ToString();

                string htel = ReqManager[0]["HomeTel"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["HomeTel"]))
                {
                    if (ReqManager[0]["HomeTel"].ToString().IndexOf("-") > 0)
                    {
                        //txtHometel_cityCode.Text = ReqManager[0]["HomeTel"].ToString().Substring(0, ReqManager[0]["HomeTel"].ToString().IndexOf("-"));
                        txtHometel.Text = ReqManager[0]["HomeTel"].ToString().Substring(0, ReqManager[0]["HomeTel"].ToString().IndexOf("-"))
                        +"-"+ReqManager[0]["HomeTel"].ToString().Substring(ReqManager[0]["HomeTel"].ToString().IndexOf("-") + 1, ReqManager[0]["HomeTel"].ToString().Length - ReqManager[0]["HomeTel"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtHometel.Text = ReqManager[0]["HomeTel"].ToString();
                    }
                }

                string wtel = ReqManager[0]["WorkTel"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["WorkTel"]))
                {
                    if (ReqManager[0]["WorkTel"].ToString().IndexOf("-") > 0)
                    {
                        //txtWorkTel_cityCode.Text = ;
                        txtWorkTel.Text =ReqManager[0]["WorkTel"].ToString().Substring(0, ReqManager[0]["WorkTel"].ToString().IndexOf("-"))+"-"+ ReqManager[0]["WorkTel"].ToString().Substring(ReqManager[0]["WorkTel"].ToString().IndexOf("-") + 1, ReqManager[0]["WorkTel"].ToString().Length - ReqManager[0]["WorkTel"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtWorkTel.Text = ReqManager[0]["WorkTel"].ToString();
                    }
                }

                string ftel = ReqManager[0]["FaxNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["FaxNo"]))
                {
                    if (ReqManager[0]["FaxNo"].ToString().IndexOf("-") > 0)
                    {
                        //txtFaxNo_cityCode.Text =;
                        txtFaxNo.Text = ReqManager[0]["FaxNo"].ToString().Substring(0, ReqManager[0]["FaxNo"].ToString().IndexOf("-"))+"-"+ ReqManager[0]["FaxNo"].ToString().Substring(ReqManager[0]["FaxNo"].ToString().IndexOf("-") + 1, ReqManager[0]["FaxNo"].ToString().Length - ReqManager[0]["FaxNo"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtFaxNo.Text = ReqManager[0]["FaxNo"].ToString();
                    }
                }
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["BankAccNo"]))
                {
                    txtBankAccNo.Text = ReqManager[0]["BankAccNo"].ToString();
                }
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SexName"]))
                {

                    txtSexName.Text = ReqManager[0]["SexName"].ToString();
                }
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["CitId"]))
                {
                    txtCitName.Text = ReqManager[0]["CitName"].ToString();
                }
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["AgentId"]))
                {
                    txtAgentName.Text = ReqManager[0]["AgentName"].ToString();
                }

                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SexId"]))
                {
                    if (Convert.ToInt32(ReqManager[0]["SexId"]) == (int)TSP.DataManager.SexManager.Sex.Female)//زن
                    {

                        lblSol.ClientEnabled = false;
                        lblSoldireBack.ClientEnabled = false;
                    }
                    else//مرد
                    {
                        chbSoLdire.Visible =
                        lblSol.ClientEnabled = 
                        lblSolFile.ClientVisible = 
                        lblSoldireBack.ClientVisible = true;
                        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SoName"]))
                        {
                            txtSoName.Text = ReqManager[0]["SoName"].ToString();
                        }
                    }
                }

                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MarName"]))
                {
                    txtMarName.Text = ReqManager[0]["MarName"].ToString();
                }

                if ((!string.IsNullOrEmpty(ReqManager[0]["ImageUrl"].ToString())))
                {
                    imgMember.ImageUrl = ReqManager[0]["ImageUrl"].ToString();

                }
                if ((!string.IsNullOrEmpty(ReqManager[0]["SignUrl"].ToString())))
                {
                    ImgSign.ImageUrl = ReqManager[0]["SignUrl"].ToString();

                }

                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["Email"]))
                    txtEmail.Text = ReqManager[0]["Email"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["HomeAdr"]))
                    txtHomeAdr.Text = ReqManager[0]["HomeAdr"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["HomePO"]))
                    txtHomePO.Text = ReqManager[0]["HomePO"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["LastName"]))
                    txtLastName.Text = ReqManager[0]["LastName"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["LastNameEn"]))
                    txtLastNameEn.Text = ReqManager[0]["LastNameEn"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MobileNo"]))
                    txtMobileNo.Text = ReqManager[0]["MobileNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["FirstName"]))
                    txtFirstName.Text = ReqManager[0]["FirstName"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["FirstNameEn"]))
                    txtFirstNameEn.Text = ReqManager[0]["FirstNameEn"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["Website"]))
                    txtWebsite.Text = ReqManager[0]["Website"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["WorkAdr"]))
                    txtWorkAdr.Text = ReqManager[0]["WorkAdr"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["WorkPO"]))
                    txtWorkPO.Text = ReqManager[0]["WorkPO"].ToString();

                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["FatherName"]))
                    txtFatherName.Text = ReqManager[0]["FatherName"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["BirhtDate"]))
                    txtBirthDate.Text = ReqManager[0]["BirhtDate"].ToString().Trim();

                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["BirthPlace"]))
                    txtBirhtPlace.Text = ReqManager[0]["BirthPlace"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IdNo"]))
                    txtIdNo.Text = ReqManager[0]["IdNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IssuePlace"]))
                    txtIssuePlace.Text = ReqManager[0]["IssuePlace"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SSN"]))
                    txtSSN.Text = ReqManager[0]["SSN"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ArchitectorCode"]))
                    txtArchitectorCode.Text = ReqManager[0]["ArchitectorCode"].ToString();
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MilitaryCommitment"]))
                {
                    chbSoLdire.Checked = Convert.ToBoolean(ReqManager[0]["MilitaryCommitment"]);                    
                }

                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["NezamKardanConfirmURL"]))
                {
                    HpKardani.NavigateUrl = ReqManager[0]["NezamKardanConfirmURL"].ToString();
                    lblKardani.ClientVisible = HpKardani.ClientVisible =
                    ChkBKardani.Checked = true;
                }
                else
                {
                    ChkBKardani.Checked =
                    lblKardani.ClientVisible = HpKardani.ClientVisible = false;
                }


                txtStDesc.Text = ReqManager[0]["Description"].ToString();

                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MsId"]))
                {

                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MrsName"]) && String.IsNullOrWhiteSpace(ReqManager[0]["MrsName"].ToString()) == false)
                        txtMsName.Text = (ReqManager[0]["MrsName"].ToString());

                    #region Check MSId
                    switch (Convert.ToInt32(ReqManager[0]["MsId"]))
                    {
                        case (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince://انتقال
                            txtMsName.Text = (ReqManager[0]["MrsName"].ToString());

                            txtPrBody.Visible = true;
                            txtOtherPr.Visible = true;
                            lblOtherPr.Visible = true;
                            lblPrBody.Visible = true;

                            TransferManager.FindByMemberId(MReId, (int)TSP.DataManager.TransferMemberType.GoToOtherProvince);//2
                            if (TransferManager.Count > 0)
                            {
                                txtOtherPr.Text = (TransferManager[0]["PrName"].ToString());
                                txtPrBody.Text = TransferManager[0]["Body"].ToString();
                            }
                            break;

                        case (int)TSP.DataManager.MembershipRegistrationStatus.ReturnToCurrentProvince://بازگشت از انتقال
                            txtMsName.Text = (ReqManager[0]["MrsName"].ToString());

                            txtPrBody.Visible = true;
                            txtOtherPr.Visible = true;
                            lblOtherPr.Visible = true;
                            lblPrBody.Visible = true;
                            lblPrFileNo.Visible = true;
                            lblPrMeNo.Visible = true;
                            txtPrFileNo.Visible = true;
                            txtPrMeNo.Visible = true;

                            TransferManager.FindByMemberId(MReId, 3);
                            if (TransferManager.Count > 0)
                            {
                                txtOtherPr.Text = (TransferManager[0]["PrName"].ToString());
                                txtPrBody.Text = TransferManager[0]["Body"].ToString();
                                txtPrFileNo.Text = TransferManager[0]["FileNo"].ToString();
                                txtPrMeNo.Text = TransferManager[0]["MeNo"].ToString();

                            }
                            break;

                        case (int)TSP.DataManager.MembershipRegistrationStatus.Dead://فوت شده
                            txtStDesc.Text = (ReqManager[0]["MsStatus"].ToString());
                            break;

                        default:
                            //CmbStatus.SelectedIndex = 0;
                            break;

                    }
                    #endregion
                }

                #region Transfer Info
                //******بایستی بر اساس درخواست جاری بیاورد و اگر نه اگر  ویرایش هم کرده باشیم مربوط به درخواست های قبلی را می آورد
                TransferManager.FindByMemberId(MReId, -1);
                if (TransferManager.Count > 0)
                {
                    ChEnteghali.Checked = PanelTransferMember.Visible = true;
                    if (!Utility.IsDBNullOrNullValue(TransferManager[0]["TransferTypeName"]))
                        lblTransferStatus.Text = TransferManager[0]["TransferTypeName"].ToString();
                    txtTDate.Text = TransferManager[0]["TransferDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(TransferManager[0]["FileNo"]))
                        ChbTCheckFileNo.Checked = true;

                    if (!Utility.IsDBNullOrNullValue(TransferManager[0]["FirstDocRegDate"]))
                        txtFirstDocRegDate.Text = TransferManager[0]["FirstDocRegDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(TransferManager[0]["CurrentDocRegDate"]))
                        txtCurrentDocRegDate.Text = TransferManager[0]["CurrentDocRegDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(TransferManager[0]["CurrentDocExpDate"]))
                        txtCurrentDocExpDate.Text = TransferManager[0]["CurrentDocExpDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(TransferManager[0]["DocPrName"]))
                    {
                        txtDocPr.Text = (TransferManager[0]["DocPrName"].ToString());
                    }

                    txtTFileNo.Text = TransferManager[0]["FileNo"].ToString();
                    txtTMeNo.Text = TransferManager[0]["MeNo"].ToString();
                    txtTPr.Text = (TransferManager[0]["PrName"].ToString());
                    if (!Utility.IsDBNullOrNullValue(TransferManager[0]["ImageUrl"]))
                    {
                        Timg.ImageUrl = TransferManager[0]["ImageUrl"].ToString();
                    }
                }
                else
                {
                    ChEnteghali.Checked = PanelTransferMember.Visible = false;
                }

                #endregion

                #region Attachment Info From tblAttachment (تصویر شناسنامه ، تصویر کارت ملی ، تصویر کارت پایان خدمت)

                TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();       
                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNo);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNo);
                }
                if (attachManager.Count > 0)
                {
                    HpIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
                    HpIdNo.ImageUrl=attachManager[0]["FilePath"].ToString();

                }
                else
                    HpIdNo.ClientVisible = false;

                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoP2);
                }
                if (attachManager.Count > 0)
                {
                    HIdNoP2.NavigateUrl = attachManager[0]["FilePath"].ToString();
                    HIdNoP2.ImageUrl = attachManager[0]["FilePath"].ToString();

                }
                else
                    HIdNoP2.ClientVisible = false;

                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
                }
                if (attachManager.Count > 0)
                {
                    HIdNoPDes.NavigateUrl = attachManager[0]["FilePath"].ToString();
                    HIdNoPDes.ImageUrl = attachManager[0]["FilePath"].ToString();

                }
                else
                    HIdNoPDes.ClientVisible = false;


                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSN);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSN);
                }
                if (attachManager.Count > 0)
                {
                    HpSSN.NavigateUrl = attachManager[0]["FilePath"].ToString();
                    HpSSN.ImageUrl = attachManager[0]["FilePath"].ToString();

                }
                else
                    HpSSN.ClientVisible = false;

                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SSNBack);
                }
                if (attachManager.Count > 0)
                {
                    HssnBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
                    HssnBack.ImageUrl = attachManager[0]["FilePath"].ToString();

                }
                else
                    HssnBack.ClientVisible = false;

                if (!IsTemp)
                {
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
                }
                else
                {
                    attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.ResidentDoc);
                }
               
                if (attachManager.Count > 0)
                {
                    HypLinkResident.ClientVisible = true;
                    HypLinkResident.NavigateUrl = attachManager[0]["FilePath"].ToString();
                    HypLinkResident.ImageUrl = attachManager[0]["FilePath"].ToString();

                }
                else
                    HypLinkResident.ClientVisible = false;

               
                #endregion

                if (Convert.ToInt32(ReqManager[0]["SexId"]) == (int)TSP.DataManager.SexManager.Sex.Male)
                {
                    if (!IsTemp)
                    {
                        attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                    }
                    else
                    {
                        attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                    }
                    if (attachManager.Count > 0)
                    {
                        HpSoldier.ClientVisible = true;
                        HpSoldier.NavigateUrl = attachManager[0]["FilePath"].ToString();
                        HpSoldier.ImageUrl = attachManager[0]["FilePath"].ToString();
                    }
                    else
                        HpSoldier.ClientVisible = false;
                    if (!IsTemp)
                    {
                        attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                    }
                    else
                    {
                        attachManager.FindByTablePrimaryKey_AttIdTemp((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
                    }
                    if (attachManager.Count > 0)
                    {
                        HpSoldierBack.ClientVisible = true;
                        HpSoldierBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
                        HpSoldierBack.ImageUrl = attachManager[0]["FilePath"].ToString();
                    }
                    else
                        HpSoldierBack.ClientVisible = false;

                }

            }
            else
            {
                ShowMessage("امکان مشاهده اطلاعات وجود ندارد");
                return;
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }

    }

    private void FillAccounting(int AccountingId)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(AccountingId);
        if (AccountingManager.Count != 1)
            return;
        txtaAmount.Text = (Convert.ToInt32(AccountingManager[0]["Amount"])).ToString("#,#");
        txtaDate.Text = AccountingManager[0]["Date"].ToString();
        if (Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingPaymentType.EPayment)
        {
            lblFishNo.Text = "شناسه پرداخت/کد رهگیری";
            txtaNumber.Text = AccountingManager[0]["ReferenceId"].ToString();
        }
        else
        {
            if (Convert.ToInt32(AccountingManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingPaymentType.POS)
                lblFishNo.Text = "شناسه واریز";
            else
                lblFishNo.Text = "شماره فیش";
            txtaNumber.Text = AccountingManager[0]["Number"].ToString();
        }
        txtaDesc.Text = AccountingManager[0]["Description"].ToString();
        txtCreateDate.Text = AccountingManager[0]["CreateDate"].ToString();
        cmbAccType.DataBind();
        cmbAccType.SelectedIndex = cmbAccType.Items.FindByValue(AccountingManager[0]["AccType"]).Index;
        cmbaType.Value = AccountingManager[0]["Type"].ToString();
        txtStatus.Text = AccountingManager[0]["StatusName"].ToString();
        if (cmbaType.SelectedItem != null && Convert.ToInt32(cmbaType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.EPayment
             && Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
        {
            this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = PanelAccountingInserting.Enabled = PanelAccountingInserting.Enabled = false;
        }
        //if (!Utility.IsDBNullOrNullValue(AccountingManager[0]["ReferenceId"]))
        //    txtReferenceId.Text = AccountingManager[0]["ReferenceId"].ToString();
    }

    private void BindGrid()
    {
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        MemberRequestManager.FindByCode(MReId);
        if (Convert.ToInt32(MemberRequestManager[0]["IsMeTemp"]) == 0)
        {
            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            Session["FillMeLicence"] = MemberLicenceManager.SelectMemberLicence(MeId, MReId, -1, -1, -1);
            GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
            GridViewMemberLicence.KeyFieldName = "MlId";
        }
        else
        {
            TSP.DataManager.TempMemberLicenceManager TempMemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
            TempMemberLicenceManager.FindByRequest(MeId, MReId);
            DataTable dtLi = TempMemberLicenceManager.DataTable;
            GridViewMemberLicence.DataSource = dtLi;
            GridViewMemberLicence.KeyFieldName = "TMlId";
        }
        GridViewMemberLicence.DataBind();
    }
    #endregion

    private void CheckUserTablePermision()
    {
        TSP.DataManager.Permission perMeLicence = TSP.DataManager.MemberLicenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        RoundPanelMemberLicence.Visible = perMeLicence.CanView;
    }

    private Boolean CheckWFPermissionForInsertFish()
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
        TSP.DataManager.WFPermission WFPerSaveInfo = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming, WFCode, MReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPerMembershipUnitConfirming = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember, WFCode, MReId, Utility.GetCurrentUser_UserId());

        return (WFPerSaveInfo.BtnNew || WFPerMembershipUnitConfirming.BtnNew);
    }

    private void SetFilterExpression()
    {
        ObjectDataSourceAccType.FilterExpression = "AccTypeId IN (" + ((int)TSP.DataManager.TSAccountingAccType.Registeration).ToString() +
            "," + ((int)TSP.DataManager.TSAccountingAccType.Entrance).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance).ToString() + ")";
    }

    private void SetLabelRegEnter()
    {
        if (cmbAccType.SelectedItem == null) return;
        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
        decimal TotalCost = 0;
        switch (Convert.ToInt32(cmbAccType.SelectedItem.Value))
        {
            case (int)TSP.DataManager.TSAccountingAccType.Entrance://ورودی
                TotalCost = GetFirstMembershipCost(CostSettingsManager);
                break;
            case (int)TSP.DataManager.TSAccountingAccType.Registeration://سالانه
                TotalCost = YearlyMembershipCost(CostSettingsManager);
                break;
            case (int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance:
                TotalCost = (GetFirstMembershipCost(CostSettingsManager) + YearlyMembershipCost(CostSettingsManager));
                break;
        }

        lblRegEnter.Text = "مبلغ: " + TotalCost.ToString("#,#") + " ریال بابت عضویت و ورود باید پرداخت شود.";
        lblReg.Text = "مبلغ: " + GetFirstMembershipCost(CostSettingsManager).ToString("#,#") + " بابت عضویت باید پرداخت شود.";
        HiddenFieldPage["FishAmount"] = txtaAmount.Text = Convert.ToInt32(TotalCost).ToString();
    }

    private decimal GetFirstMembershipCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager)
    {
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }

    private decimal YearlyMembershipCost(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager)
    {
        CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.YearlyMembershipCost.ToString(), Utility.GetCurrentUser_AgentId());
        if (CostSettingsManager.Count > 0 && !string.IsNullOrEmpty(CostSettingsManager[0]["SValue"].ToString()) && Convert.ToDecimal(CostSettingsManager[0]["SValue"]) != 0)
            return Convert.ToDecimal(CostSettingsManager[0]["SValue"]);
        return -1;
    }

    protected void SetMenuItem()
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];

            if ((int)arr[0] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(MeId, MReId);

        }
    }

    protected void CheckMenuImage(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Attachment
        arr.Add(0);//arr[5]-->Request

        MemberActivitySubjectManager.FindForDelete(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        //ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        //if (ProjectJobHistoryManager.Count > 0)
        //{
        //    MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
        //    MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
        //    MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
        //    arr[1] = 1;
        //}
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        Session["MenuArrayList"] = arr;
    }

    #region Insert-Update

    private void Insert()
    {
        try
        {
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
            DataRow dr = AccountingManager.NewRow();

            dr["TableTypeId"] = MReId;
            dr["TableType"] = (int)TSP.DataManager.TableCodes.Member;
            dr["Type"] = cmbaType.SelectedItem.Value;
            dr["TypeName"] = cmbaType.SelectedItem.Text;
            dr["Bank"] = DBNull.Value;
            dr["BranchCode"] = DBNull.Value;
            dr["BranchName"] = DBNull.Value;
            dr["AccType"] = cmbAccType.Value;
            dr["AccTypeName"] = cmbAccType.Text;
            dr["Number"] = txtaNumber.Text;
            dr["Date"] = txtaDate.Text;
            dr["Description"] = txtaDesc.Text;
            dr["Amount"] = txtaAmount.Text;
            dr["Description"] = txtaDesc.Text;
            if (Convert.ToInt32(cmbaType.SelectedItem.Value) == (int)TSP.DataManager.TSAccountingPaymentType.POS)
            {
                dr["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            }
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            AccountingManager.AddRow(dr);
            AccountingManager.Save();
            AccountingManager.DataTable.AcceptChanges();
            PageMode = "Edit";
            AccountingId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            FillAccounting(AccountingId);
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private void Update(int AccountingId)
    {
        try
        {
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
            AccountingManager.FindByAccountingId(AccountingId);
            if (AccountingManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["TableTypeId"] = MReId;
            AccountingManager[0]["TableType"] = (int)TSP.DataManager.TableCodes.Member;
            AccountingManager[0]["Type"] = cmbaType.SelectedItem.Value;
            AccountingManager[0]["TypeName"] = cmbaType.SelectedItem.Text;
            AccountingManager[0]["Bank"] = DBNull.Value;
            AccountingManager[0]["BranchCode"] = DBNull.Value;
            AccountingManager[0]["BranchName"] = DBNull.Value;
            AccountingManager[0]["AccType"] = cmbAccType.Value;
            AccountingManager[0]["AccTypeName"] = cmbAccType.Text;
            AccountingManager[0]["Number"] = txtaNumber.Text;
            AccountingManager[0]["Date"] = txtaDate.Text;
            AccountingManager[0]["Description"] = txtaDesc.Text;
            AccountingManager[0]["Amount"] = txtaAmount.Text;
            AccountingManager[0]["Description"] = txtaDesc.Text;
            AccountingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            AccountingManager[0]["ModifiedDate"] = DateTime.Now;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    #endregion

    private void SetFishPaymentStatus(Boolean Payed)
    {
        try
        {
            if (Utility.IsDBNullOrNullValue(AccountingId) || AccountingId == -1)
            {
                ShowMessage("برای این فرد فیش ثبت نشده است.");
                return;
            }
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
            AccountingManager.FindByAccountingId(AccountingId);
            if (AccountingManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            if (Convert.ToInt32(AccountingManager[0]["Type"]) != (int)TSP.DataManager.TSAccountingPaymentType.Fiche)
            {
                ShowMessage("تنها امکان چاپ پرداخت هایی که نحوه پرداخت آنها فیش می باشد وجود دارد.");
                return;
            }
            AccountingManager[0].BeginEdit();
            if (Payed)
                AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            else
                AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.SaveInDB;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            FillAccounting(AccountingId);
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    #endregion

}