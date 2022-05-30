using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Employee_TechnicalServices_Project_ProjectAccountingDesigner : System.Web.UI.Page
{
    #region Events

    #region Properties
    private int ProjectIngridientTypeId
    {
        get
        {
            return (int)TSP.DataManager.TSProjectIngridientType.Designer;
        }
    }

    private int ProjectId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        }
        set
        {
            HDProjectId.Value = Utility.EncryptQS(value.ToString());
        }
    }

    private string ProjectPageMode
    {
        get
        {
            return Utility.DecryptQS(ProjectPgMode.Value);
        }
        set
        {
            ProjectPgMode.Value = Utility.EncryptQS(value.ToString());
        }
    }

    private int PrjReId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
        }
        set
        {
            RequestId.Value = Utility.EncryptQS(value.ToString());
        }
    }

    private int PlansId
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

    private int PlansTypeId
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
    private int _CurrentPrjTaskCode
    {
        get
        {
            return Convert.ToInt32(HiddenFieldAcc["CurrentPrjTaskCode"]);
        }
        set
        {
            HiddenFieldAcc["CurrentPrjTaskCode"] = value.ToString();
        }
    }
    private Boolean _CanEditProjectInfoInThisRequest
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldAcc["CanEditProjectInfoInThisRequest"]);
        }
        set
        {
            HiddenFieldAcc["CanEditProjectInfoInThisRequest"] = value.ToString();
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            drdPlanType.DataBind();
            ((DevExpress.Web.ASPxListBox)(drdPlanType.FindControl("ListBoxPlanType"))).Items.Insert(0, new DevExpress.Web.ListEditItem("<همه>", null));
            Session["SendBackDataTable_EmpAccPrj"] = "";
            #region Check Table Permission
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = btnView2.Enabled = per.CanView;
            btnDelete.Enabled = btnDelete2.Enabled = per.CanDelete;
            TSP.DataManager.Permission perChangeStatus = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermissionForTSAccountingFishChangeStatus(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnCancelPayed.Visible = btnCancelPayed2.Visible = perChangeStatus.CanNew;

            TSP.DataManager.Permission perCanEditNum = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermissionForTSAccountingFishEditPayedFishNumber(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEditFishNo.Enabled = btnEditFishNo2.Enabled = perCanEditNum.CanEdit;
            btnEditFishNo.Visible = btnEditFishNo2.Visible = perCanEditNum.CanView;
            #endregion
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx");
            }

            PlansId = -1; PlansTypeId = -1;
            ProjectId = int.Parse(Utility.DecryptQS(Request.QueryString["ProjectId"].ToString()));
            ProjectPageMode = Utility.DecryptQS(Request.QueryString["PageMode"]).ToString();
            PrjReId = int.Parse(Utility.DecryptQS(Request.QueryString["PrjReId"]).ToString());

            if (Utility.IsDBNullOrNullValue(ProjectId) || Utility.IsDBNullOrNullValue(PrjReId) || Utility.IsDBNullOrNullValue(ProjectIngridientTypeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }            

            ObjectDataSourceTsAcc.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
            ObjectDataSourceTsAcc.SelectParameters["PrjReId"].DefaultValue = PrjReId.ToString();
            ObjectDataSourceTsAcc.SelectParameters["ProjectIngridientTypeId"].DefaultValue = ProjectIngridientTypeId.ToString();


            #region FillProjectInfo
            prjInfo.Fill(PrjReId);
            _OwnerMobileNo = prjInfo.OwnerMobileNo;
            _OwnerName = prjInfo.OwnerName;
            _Foundation = prjInfo.Foundation;
            _MunName = prjInfo.MunName;
            _CitName = prjInfo.CitName;
            _CurrentPrjTaskCode = prjInfo.CurrentTaskCode;
            _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
            #endregion
            //چاپ طراح شهرداری
            TSP.DataManager.Permission perDesPrint = TSP.DataManager.TechnicalServices.Project_DesignerManager.GetUserPermissionPrintMunicipalityDesPermit(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDesPermit.Visible = btnDesPermit2.Visible = perDesPrint.CanView;
            CheckWorkFlowPermission();
            if (!_CanEditProjectInfoInThisRequest)
                btnCancelPayed.Enabled = btnCancelPayed2.Enabled = btnDelete.Enabled = btnDelete2.Enabled
                    = btnEdit.Enabled = btnEdit2.Enabled = btnEditFishNo.Enabled = btnEditFishNo2.Enabled
                    = BtnNew.Enabled = btnNew2.Enabled = 
                    btnPaying.Enabled = btnPaying2.Enabled = btnSendSms.Enabled = btnSendSms1.Enabled = false;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;


            this.ViewState["btnDesPermit"] = btnDesPermit.Visible;

        }

        if (this.ViewState["btnDesPermit"] != null)
            this.btnDesPermit.Visible = this.btnDesPermit2.Visible = (bool)this.ViewState["btnDesPermit"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (this.ViewState["btnCancelPayed"] != null)
            this.btnCancelPayed.Visible = this.btnCancelPayed2.Enabled = (bool)this.ViewState["btnCancelPayed"];


        if (this.ViewState["btnEditFishNo"] != null)
            btnEditFishNo.Enabled = btnEditFishNo2.Enabled = (bool)this.ViewState["btnEditFishNo"];

        if (this.ViewState["btnEditFishNoVisible"] != null)
            this.btnEditFishNo.Visible = this.btnEditFishNo2.Enabled = (bool)this.ViewState["btnEditFishNoVisible"];


    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        if (Request.QueryString["UrlReferrer"] != null)
        {
            string PgName = Utility.DecryptQS(Request.QueryString["UrlReferrer"]);
            if (PgName == "PlanDesigner.aspx")
            {
                Response.Redirect(PgName + "?ProjectId=" + HDProjectId.Value
                         + "&PageMode=" + ProjectPgMode.Value + "&PrjReId=" + RequestId.Value
                         + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt
                         + "&PlnId=" + Request.QueryString["PlnId"].ToString()
                         + "&PlanPageMode=" + Request.QueryString["PlanPageMode"].ToString());
                return;
            }

            if (PgName == "Plans.aspx")
            {
                Response.Redirect(PgName + "?ProjectId=" + HDProjectId.Value
                         + "&PageMode=" + ProjectPgMode.Value + "&PrjReId=" + RequestId.Value
                         + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
                return;
            }

            Response.Redirect(PgName + "?ProjectId=" + HDProjectId.Value
                + "&PageMode=" + ProjectPgMode.Value + "&PrjReId=" + RequestId.Value
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
            Response.Redirect("ProjectInsert.aspx?ProjectId=" + HDProjectId.Value
                + "&PageMode=" + ProjectPgMode.Value + "&PrjReId=" + RequestId.Value
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    protected void btnPaying_Click(object sender, EventArgs e)
    {
        ChangeAccountingStatus(TSP.DataManager.TSAccountingStatus.Payment);
    }

    protected void CancelPayment_Click(object sender, EventArgs e)
    {
        try
        {
            int AccountingId = -1;
            if (GridViewProjectAccounting.FocusedRowIndex > -1)
            {
                DataRow row = GridViewProjectAccounting.GetDataRow(GridViewProjectAccounting.FocusedRowIndex);
                AccountingId = (int)row["AccountingId"];
            }
            if (AccountingId == -1)
            {
                SetMessage("لطفاً ابتدا یک فیش را انتخاب نمائید");
                return;
            }
            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
            TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager();
            AccountingDetailManager.FindByAccountingId(AccountingId);
            for (int i = 0; i < AccountingDetailManager.Count; i++)
            {
                AccountingDocumentDetailManager.FindByObsever(Convert.ToInt32(AccountingDetailManager[i]["TableId"]));
                if (AccountingDocumentDetailManager.Count > 0)
                {
                    SetMessage("امکان تغییر وضعیت فیش وجود ندارد.اطلاعات این فیش در لیست شماره " + AccountingDocumentDetailManager[0]["ListNo"].ToString() + " حق الزحمه ناظرین،جهت پرداخت حق الزحمه ناظران مربطه به واحد مالی ارائه شده است.");
                    return;
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        ChangeAccountingStatus(TSP.DataManager.TSAccountingStatus.SaveInDB);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Delete();
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Project.aspx?PostId=" + HDProjectId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Project.aspx");
        }
    }

    protected void btnEditFishNo_Click(object sender, EventArgs e)
    {
        try
        {
            int AccountingId = -1;
            if (GridViewProjectAccounting.FocusedRowIndex > -1)
            {
                DataRow row = GridViewProjectAccounting.GetDataRow(GridViewProjectAccounting.FocusedRowIndex);
                AccountingId = (int)row["AccountingId"];
            }
            if (AccountingId == -1)
            {
                SetMessage("لطفاً ابتدا یک فیش را انتخاب نمائید");
                return;
            }
            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
            TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager();
            AccountingDetailManager.FindByAccountingId(AccountingId);
            for (int i = 0; i < AccountingDetailManager.Count; i++)
            {
                AccountingDocumentDetailManager.FindByObsever(Convert.ToInt32(AccountingDetailManager[i]["TableId"]));
                if (AccountingDocumentDetailManager.Count > 0)
                {
                    SetMessage("امکان تغییر اطلاعات این فیش وجود ندارد.اطلاعات این فیش در لیست شماره " + AccountingDocumentDetailManager[0]["ListNo"].ToString() + " حق الزحمه ناظرین،جهت پرداخت حق الزحمه ناظران مربطه به واحد مالی ارائه شده است.");
                    return;
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        NextPage("EditNumber");
    }
    protected void btnSendSms_Click(object sender, EventArgs e)
    {
        try
        {
            string SMSBody = "";
            string MobileNo = "";
            TSP.DataManager.UserType SMSUltId = TSP.DataManager.UserType.Member;
            int AccountingId = -1;
            if (GridViewProjectAccounting.FocusedRowIndex > -1)
            {
                DataRow row = GridViewProjectAccounting.GetDataRow(GridViewProjectAccounting.FocusedRowIndex);
                AccountingId = (int)row["AccountingId"];

            }
            if (AccountingId == -1)
            {
                SetMessage("لطفاً ابتدا یک فیش را انتخاب نمائید");
                return;
            }
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
            DataTable dtAcc = AccountingManager.SelectTSAccountingPayerMobileNo(AccountingId);
            if (dtAcc.Rows.Count != 1)
            {
                SetMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
                return;
            }
            int AccType = Convert.ToInt32(dtAcc.Rows[0]["AccType"]);
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
                        + " فیش پنج درصد سهم سازمان از طراحی مربوط به پروژه " + ProjectId.ToString() + " به مالکیت خانم/آقای " + _OwnerName + "در سامانه سازمان نظام مهندسی ساختمان استان فارس ثبت گردید.جهت پرداخت حداکثر ظرف 48 ساعت در پرتال اعضا قسمت مدیریت فیش های پرداخت نشده اقدام نمایید"
                        + "\n"
                       + "https://fceo.ir/Members/Accounting/EpaymentFishes.aspx?P=N";

                    break;
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    string Password = "";
                    if (!Convert.ToBoolean(dtAcc.Rows[0]["IsSMSSent"]))
                    {
                        string errMsg = ResetOwnerPass(ProjectId, ref Password);
                        if (!string.IsNullOrWhiteSpace(errMsg))
                        {
                            SetMessage(errMsg);
                            return;
                        }
                    }
                    MobileNo = _OwnerMobileNo;
                    SMSUltId = TSP.DataManager.UserType.TSProjectOwner;
                    SMSBody = "خانم/آقای " + _OwnerName + " مالک محترم پروژه ساختمانی مربوط به شهر " + _CitName + " و شهرداری " + _MunName + " با زیربنای " + _Foundation + " متر مربع در سامانه نظام مهندسی ساختمان استان فارس ثبت گردیده است.جهت پرداخت وجه حق الزحمه نظارت با مشخصات کاربری زیر و از طریق لینک زیر  اقدام نمایید"
                        + "\n"
                     + "نام کاربری " + "prj" + ProjectId.ToString()
                     + (!string.IsNullOrWhiteSpace(Password) ? "\n" + "رمز عبور " + Password : "")
                       + "\n"
                       + "https://fceo.ir/Owner/ProjectAccounting.aspx"
                    ;
                    break;
            }
            if (string.IsNullOrWhiteSpace(MobileNo))
            {

                SetMessage("شماره همراه جهت ارسال پیامک برای این فیش یافت نشد");
                return;
            }
            SendSMSNotification(Utility.Notifications.NotificationTypes.TSProjectOwner, SMSBody, MobileNo, ProjectId.ToString(), SMSUltId);
            AccountingManager.FindByAccountingId(AccountingId);
            if (dtAcc.Rows.Count != 1)
            {
                SetMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
                return;
            }
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["IsSMSSent"] = 1;
            AccountingManager[0]["SendSMSDate"] = Utility.GetDateOfToday();
            AccountingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            AccountingManager[0]["ModifiedDate"] = DateTime.Now;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewProjectAccounting.DataBind();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    #endregion

    #region Grid
    protected void GridViewProjectAccounting_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (RequestId.Value != null)
        {
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            if (e.GetValue("PrjReId") == null)
                return;
            string CurretnPrjReId = e.GetValue("PrjReId").ToString();
            if (PrjReId == CurretnPrjReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void GridViewProjectAccounting_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        SetMessageAfterCallbak("");
        if (Utility.IsDBNullOrNullValue(ProjectIngridientTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        GridViewProjectAccounting.JSProperties["cpPrintFish"] = 0;
        GridViewProjectAccounting.JSProperties["cpPrintFishPath"] = "";
        switch (e.Parameters)
        {
            case "PrintFish":
                #region PrintFish
                int focucedIndex = GridViewProjectAccounting.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewProjectAccounting.GetDataRow(focucedIndex);
                    int AccountingId = (int)row["AccountingId"];
                    if (Convert.ToInt32(row["Type"]) == (int)TSP.DataManager.TSAccountingPaymentType.POS)
                    {
                        SetMessageAfterCallbak("امکان چاپ فیش برای نوع پرداخت دستگاه کارت خوان وجود ندارد.");
                        return;
                    }
                    GridViewProjectAccounting.JSProperties["cpPrintFish"] = 1;
                    if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
                        GridViewProjectAccounting.JSProperties["cpPrintFishPath"] = "../../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + Utility.EncryptQS(AccountingId.ToString()) +
                              "&ProjectId=" + HDProjectId.Value + "&PrjReId=" + RequestId.Value + "&PlnTypeId=" + HiddenFieldAcc["PlnTypeId"].ToString();
                    else
                        GridViewProjectAccounting.JSProperties["cpPrintFishPath"] = "../../../ReportForms/Accounting/BankFish.aspx?AccountingId=" + Utility.EncryptQS(AccountingId.ToString()) +
                              "&ProjectId=" + HDProjectId.Value + "&PrjReId=" + RequestId.Value + "&PlnTypeId=" + Utility.EncryptQS("-1");
                }
                #endregion
                break;
            case "ObsPermitPrint":
            case "ObsPermitPrint2":
                int HasText = 0;
                if (e.Parameters == "ObsPermitPrint2") HasText = 1; else HasText = 0;
                #region ObsPermitPrint
                TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
                AccountingManager.SelectAccountingForProject(ProjectId, -1, (int)TSP.DataManager.TSProjectIngridientType.Observer);
                if (AccountingManager.Count <= 0)
                {
                    SetMessageAfterCallbak("امکان چاپ نامه شهرداری وجود ندارد. فیش دستمزد ناظرین وارد نشده است");
                    return;
                }

                for (int i = 0; i < AccountingManager.Count; i++)
                {
                    if (Convert.ToInt32(AccountingManager[i]["Status"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
                    {
                        SetMessageAfterCallbak("امکان چاپ نامه شهرداری وجود ندارد. فیش دستمزد ناظرین پرداخت نشده است");
                        return;
                    }
                }

                int AccId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
                GridViewProjectAccounting.JSProperties["cpPrintFish"] = 1;
                GridViewProjectAccounting.JSProperties["cpPrintFishPath"] = "../../../ReportForms/TechnicalServices/ObserverPermitted.aspx?ProjectId="
                 + Utility.EncryptQS(ProjectId.ToString()) + "&AccountingId=" + Utility.EncryptQS(AccId.ToString()) + "&PlansTypeId=" + Utility.EncryptQS("-1") + "&HasText=" + Utility.EncryptQS(HasText.ToString());

                #endregion
                break;
            case "DesPermitPrint":
                #region DesPermitPrint
                if (ProjectId == -1)
                {
                    SetMessageAfterCallbak("لطفا ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                string PlansTypeId = GetSelectedInDxDropDown(drdPlanType, "ListBoxPlanType");



                GridViewProjectAccounting.JSProperties["cpPrintDesPermit"] = 1;
                GridViewProjectAccounting.JSProperties["cpPrintDesPermitPath"] = "../../../ReportForms/TechnicalServices/DesignerPermitted.aspx?ProjectId="
                    + Utility.EncryptQS(ProjectId.ToString()) + "&PlansTypeId=" + Utility.EncryptQS(PlansTypeId);

                #endregion
                break;
        }
    }

    String GetSelectedInDxDropDown(DevExpress.Web.ASPxDropDownEdit DropDown, String ListBoxName)
    {
        string Param = "(";
        bool flag = false;

        DevExpress.Web.ASPxListBox ListBox = (DevExpress.Web.ASPxListBox)DropDown.FindControl(ListBoxName);
        if (ListBox == null)
            return "";

        for (int i = 0; i < ListBox.SelectedItems.Count; i++)
        {
            if (ListBox.SelectedItems[i].Value != null)
            {
                if (Param != "(")
                    Param += "," + ListBox.SelectedItems[i].Value.ToString();
                else
                    Param += ListBox.SelectedItems[i].Value.ToString();

                flag = true;
            }
        }

        if (flag)
        {
            Param += ")";
            return Param;
        }
        return "(0)";
    }
    #endregion

    #region Menu Click
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        //string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string PrjReId = RequestId.Value;
        string PageMode = ProjectPgMode.Value;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        PrjMainMenu MainMenu = new PrjMainMenu("Owner", Convert.ToInt32(ProjectId));
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }

    private void SetProjectMainMenuEnabled()
    {
        //string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        if (ProjectId == -1)
            ProjectId = -2;

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Accounting", Convert.ToInt32(ProjectId));
        MainMenu.Items.FindByName("Accounting").Selected = true; //Accounting
        //MainMenu.Items.FindByName("StatusAnnouncement").Enabled = PrjMainMenu.GetEnabled("StatusAnnouncement");
        //MainMenu.Items.FindByName("BuildingsLicense").Enabled = PrjMainMenu.GetEnabled("BuildingsLicense");
        //MainMenu.Items.FindByName("Timing").Enabled = PrjMainMenu.GetEnabled("Timing");
        MainMenu.Items.FindByName("Contract").Enabled = PrjMainMenu.GetEnabled("Contract");
        MainMenu.Items.FindByName("Implementer").Enabled = PrjMainMenu.GetEnabled("Implementer");
        MainMenu.Items.FindByName("Observers").Enabled = PrjMainMenu.GetEnabled("Observers");
        MainMenu.Items.FindByName("Plans").Enabled = PrjMainMenu.GetEnabled("Plans");
        MainMenu.Items.FindByName("Owner").Enabled = PrjMainMenu.GetEnabled("Owner");
        MainMenu.Items.FindByName("Project").Enabled = PrjMainMenu.GetEnabled("Project");
        MainMenu.Items.FindByName("Accounting").Enabled = PrjMainMenu.GetEnabled("Accounting");
        MainMenu.Items.FindByName("Designer").Enabled = PrjMainMenu.GetEnabled("Designer");
    }

    private void CheckMenueViewPermission()
    {
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

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (Utility.IsDBNullOrNullValue(PrjReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, PrjReId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }
        //string QS = "~/Employee/TechnicalServices/Project/ProjectAccountingInsert.aspx?ProjectId=" + HDProjectId.Value
        //        + "&AccountingId=" + Utility.EncryptQS("-1")
        //        + "&PageMode=" + Request.QueryString["PageMode"]
        //        + "&PageMode2=" + Utility.EncryptQS("New")
        //        + "&PrjReId=" + RequestId.Value
        //        + "&IngT=" + Request.QueryString["IngT"].ToString()
        //        + "&PlnId=" + Request.QueryString["PlnId"].ToString()
        //        + "&PlnTypeId=" + Request.QueryString["PlnTypeId"].ToString()
        //        + "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString()
        //        + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString()
        //        + "&ReqSender=" + Utility.EncryptQS("AccInsert");

        //WFUserControl.QueryStringForRedirect = QS;
        WFUserControl.PerformCallback(PrjReId, ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods

    #region WorkFlow Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        Boolean PerCanEdit = false;
        Boolean PerCanNew = false;
        //Save
        //Designers
        TSP.DataManager.WFPermission WFPerDesignerOfProject = CheckProjectWorkFlowPermissionForDesigner();
        if (WFPerDesignerOfProject.BtnNew || WFPerDesignerOfProject.BtnEdit)
        {
            PerCanNew = WFPerDesignerOfProject.BtnNew;
            PerCanEdit = WFPerDesignerOfProject.BtnEdit;
        }
        this.ViewState["BtnNew"] = BtnNew.Enabled = btnNew2.Enabled = PerCanNew;
        this.ViewState["btnEditFishNo"] = this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = btnEditFishNo.Enabled = btnEditFishNo2.Enabled
              = btnCancelPayed.Enabled = btnCancelPayed2.Enabled = btnDelete.Enabled = btnDelete2.Enabled = PerCanEdit;
    }

    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForDesigner()
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;
        int StructureAndInstallationTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructureAndInstallationPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructureAndInstallationTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());


        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit || PerStructureAndInstallationPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave || PerStructureAndInstallationPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew || PerStructureAndInstallationPlan.BtnNew);
        WFPer.BtnInactive = (PerAtchitecturalPlan.BtnInactive || PerStructurePlan.BtnInactive || PerElectricalInsPlan.BtnInactive || PerMechanicInsPlan.BtnInactive || PerStructureAndInstallationPlan.BtnInactive);

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

    #region ProjectStatus Methods
    private void CheckProjectStatusPermission()
    {
        bool Flag = false;

        //int ProjectId = Convert.ToInt32(HDProjectId.Value);
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        ProjectManager.FindByProjectId(Convert.ToInt32(ProjectId));
        if (ProjectManager.Count > 0)
        {
            int ProjectStatus = Convert.ToInt32(ProjectManager[0]["ProjectStatusId"]);
            if (ProjectStatus == (int)TSP.DataManager.TSProjectStatus.LicenseRequest && CheckBuildingsLicenseExistance(Convert.ToInt32(ProjectId)))
                Flag = true;
            else
                Flag = false;
        }

        BtnNew.Enabled = Flag;
        btnNew2.Enabled = Flag;
    }

    private bool CheckBuildingsLicenseExistance(int ProjectId)
    {
        TSP.DataManager.TechnicalServices.BuildingsLicenseManager BuildingsLicenseManager = new TSP.DataManager.TechnicalServices.BuildingsLicenseManager();
        BuildingsLicenseManager.FindByProjectId(ProjectId);
        if (BuildingsLicenseManager.Count > 0)
            return true;
        else
            return false;
    }
    #endregion

    void NextPage(string PageMode)
    {
        int AccountingId = -1;
        int AccType = -1;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        if (PageMode != "New")
        {
            if (GridViewProjectAccounting.FocusedRowIndex > -1)
            {
                DataRow row = GridViewProjectAccounting.GetDataRow(GridViewProjectAccounting.FocusedRowIndex);
                AccountingId = (int)row["AccountingId"];
                AccType = (int)row["AccType"];
            }

            if (AccountingId == -1)
            {
                SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
                return;
            }

            if (PageMode == "Edit")
            {
                if (AccType == -1)
                {
                    SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
                    return;
                }
                if (!CheckConditions(AccType, AccountingId)) return;
            }
        }

        if (Utility.IsDBNullOrNullValue(ProjectIngridientTypeId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        Response.Redirect("ProjectAccountingDesignerInsert.aspx?ProjectId=" + HDProjectId.Value
                  + "&AccountingId=" + Utility.EncryptQS(AccountingId.ToString())
                  + "&PageMode=" + ProjectPgMode.Value
                  + "&PageMode2=" + Utility.EncryptQS(PageMode)
                  + "&PrjReId=" + RequestId.Value
                  + "&IngT=" + Utility.EncryptQS(ProjectIngridientTypeId.ToString()) +
                   "&PlnId=" + HiddenFieldAcc["PlnId"].ToString() +
                   "&PlnTypeId=" + HiddenFieldAcc["PlnTypeId"].ToString() +
                   "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    private void InActive_Delete()
    {
        int PrjDesignerId = -1;
        int PrjDesignerReId = -1;
        int DesignerPlansId = -1;
        int PlansId = -1;

        if (GridViewProjectAccounting.FocusedRowIndex > -1)
        {
            DataRow row = GridViewProjectAccounting.GetDataRow(GridViewProjectAccounting.FocusedRowIndex);
            PrjDesignerId = (int)row["PrjDesignerId"];
            PrjDesignerReId = (int)row["PrjDesignerReId"];
            //DesignerPlansId = (int)row["DesignerPlansId"];
            //PlansId = (int)row["PlansId"];
        }

        if (PrjDesignerId == -1 || PrjDesignerReId == -1 || DesignerPlansId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاًابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (PrjDesignerReId == Convert.ToInt32(PrjReId))
                Delete();
            //else
            //    InActive();
        }
    }

    private void Delete()
    {
        try
        {
            int PrjReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
            int AccountingId = -1;
            int AccPrjReId = -1;//TableTypeId
            int AccType = -1;
            if (GridViewProjectAccounting.FocusedRowIndex > -1)
            {
                DataRow row = GridViewProjectAccounting.GetDataRow(GridViewProjectAccounting.FocusedRowIndex);
                AccountingId = (int)row["AccountingId"];
                AccPrjReId = (int)row["PrjReId"];
                AccType = (int)row["AccType"];
            }

            if (AccountingId == -1 || AccPrjReId == -1)
            {
                SetMessage("لطفاًابتدا یک رکورد را انتخاب نمائید");
            }
            else
            {
                if (!CheckConditions(AccType, AccountingId)) return;

                //  if (AccPrjReId == Convert.ToInt32(PrjReId))
                DeleteAccounting(AccountingId);
                //  else
                // {
                //     SetMessage("امکان حذف فیش تنها در درخواست مربوط به خودش موجود می باشد");
                //     return;
                //  }
                // else
                //  InActive(PrjDesignerId);
                GridViewProjectAccounting.DataBind();
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
        }
    }

    bool CheckConditions(int AccType, int AccountingId)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(AccountingId);
        if (AccountingManager.Count == 1)
        {
            if (Convert.ToInt32(AccountingManager[0]["Type"]) != (int)TSP.DataManager.TSAccountingPaymentType.POS
                && Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
            {
                SetMessage("امکان تغییر فیش پرداخت شده وجود ندارد");
                return false;
            }
        }
        if (AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5Percent
            || AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation
            ||AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure)
        {
            if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo
             || _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
             || _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
             || _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
             || _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject
             || _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
            {

                return true;
            }
            else
            {
                SetMessage("در این مرحله از گردش کار امکان تغییر اطلاعات فیش دستمزد طراحان وجود ندارد ");
                return false;
            }
        }
        return false;
    }

    private bool IfObserverExist()
    {
        //int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        ProjectObserversManager.FindByProjectId(Convert.ToInt32(ProjectId));
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

    private void DeleteAccounting(int AccountingId)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(AccountingManager);
        TransactionManager.Add(AccountingDetailManager);
        try
        {
            TransactionManager.BeginSave();
            AccountingDetailManager.FindByAccountingId(AccountingId);
            int countAccDetail = AccountingDetailManager.Count;
            for (int i = 0; i < countAccDetail; i++)
            {
                AccountingDetailManager[0].Delete();
                AccountingDetailManager.Save();
                AccountingDetailManager.DataTable.AcceptChanges();
            }
            AccountingManager.FindByAccountingId(AccountingId);
            if (AccountingManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            AccountingManager[0].Delete();
            AccountingManager.Save();
            TransactionManager.EndSave();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
        }
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetMessageAfterCallbak(string Message)
    {
        GridViewProjectAccounting.JSProperties["cpMessage"] = Message;
    }    

    private void ChangeAccountingStatus(TSP.DataManager.TSAccountingStatus PaymentStatus)
    {
        try
        {
            int AccountingId = -1;
            if (GridViewProjectAccounting.FocusedRowIndex > -1)
            {
                DataRow row = GridViewProjectAccounting.GetDataRow(GridViewProjectAccounting.FocusedRowIndex);
                AccountingId = (int)row["AccountingId"];

            }
            if (AccountingId == -1)
            {
                SetMessage("لطفاً ابتدا یک فیش را انتخاب نمائید");
                return;
            }
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
            AccountingManager.FindByAccountingId(AccountingId);
            if (AccountingManager.Count == 1)
            {
                AccountingManager[0].BeginEdit();
                AccountingManager[0]["Status"] = (int)PaymentStatus;
                AccountingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                AccountingManager[0]["ModifiedDate"] = DateTime.Now;
                AccountingManager[0].EndEdit();
                AccountingManager.Save();
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                GridViewProjectAccounting.DataBind();
            }
            else
            {
                SetMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return;
        }
    }

    #region SendSmsAndPass
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
        Password = (new Random().Next(0, 1000000)).ToString();
        string ReturnValue = "";
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
                ReturnValue = "خطایی در ذخیره انجام گرفته است";
                trans.CancelSave();
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