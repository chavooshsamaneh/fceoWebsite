using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
//
public partial class EPayment_EpaymentFishes : System.Web.UI.Page
{
    private string PageMode
    {
        set
        {
            HiddenFieldEpayment["PageMode"] = value;
        }
        get
        {
            return HiddenFieldEpayment["PageMode"].ToString();
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
           
            if (!string.IsNullOrEmpty(Request.QueryString["P"]))
            {
                PageMode = Request.QueryString["P"];
                CheckPgMode(Request.QueryString["P"]);
            } 
            CheckPermission();
            AddNullItemToCmb();
        }
        Search();
    }

    protected void btnView_Click()//(object sender, EventArgs e)
    {

        if (GridViewAccounting.FocusedRowIndex <= -1)
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        DataRow row = GridViewAccounting.GetDataRow(GridViewAccounting.FocusedRowIndex);
        string AccountingId = row["AccountingId"].ToString();
        ASPxWebControl.RedirectOnCallback("EpaymentFisheView.aspx?AId=" + Utility.EncryptQS(AccountingId) + "&PgMd=" + Utility.EncryptQS("View") + "&PrePgMd=" + PageMode);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Epayment";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void CallbackPanelPage_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelPage.JSProperties["cpPrint"] = 0;
        CallbackPanelPage.JSProperties["cpPrintUrl"] = "";
        switch (e.Parameter)
        {

            case "View":
                btnView_Click();
                break;
            case "Print":
                GridViewAccounting.DetailRows.CollapseAllRows();
                Session["DeletedColumnsName"] = null;
                Session["DataTable"] = GridViewAccounting.Columns;
                Session["DataSource"] = ObjdsEpayment;
                Session["Title"] = GetPrintTitle();
                CallbackPanelPage.JSProperties["cpPrint"] = 1;
                CallbackPanelPage.JSProperties["cpPrintUrl"] = "../Print.aspx";
                break;
            case "Clear":
                ClearSearch();
                break;
            case "Search":
                Search();
                break;
        }
    }

    protected void GridViewAccounting_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void GridViewAccounting_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewAccountingDetails_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["AccountingId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }
    #endregion

    #region  Methods
    private void AddNullItemToCmb()
    {
        if (PageMode == null)
            return;
        switch (PageMode)
        {
            case "L":
                cmbPeriods.DataBind();
                cmbPeriods.Items.Insert(0, new DevExpress.Web.ListEditItem("---------------------", null));
                break;
        }
    }

    private void CheckPgMode(string PgMode)
    {
        switch (PgMode)
        {
            case "L":
                PeriodRegisterPageMode();
                break;

            case "M":
                MembershipPageMode();
                break;
            default:
                GridViewAccounting.SettingsDetail.ShowDetailRow = false;
                ObjdsEpaymentDetail.SelectParameters["AccountingId"].DefaultValue = "-2";
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                OdbPeriod.SelectParameters["IsFill"].DefaultValue = "false";
                break;
        }
    }

    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void PeriodRegisterPageMode()
    {
        ObjdsEpayment.SelectParameters["AccTypeList"].DefaultValue = ((int)TSP.DataManager.TSAccountingAccType.PeriodRegister).ToString();
        GridViewAccounting.Columns["FishPayerId"].Caption = "کد عضویت";
        
        PanelSearchPeriodReg.Visible = true;
        GridViewAccounting.SettingsDetail.ShowDetailRow = true;
        GridViewAccounting.Columns["FishPayerMembershipType"].Visible = false;
        //ODBCourse.SelectParameters["FillData"].DefaultValue = "1";
        OdbPeriod.SelectParameters["IsFill"].DefaultValue = "true";
    }

    private void MembershipPageMode()
    {
        ObjdsEpayment.SelectParameters["AccTypeList"].DefaultValue =
                                   ((int)TSP.DataManager.TSAccountingAccType.Registeration).ToString()
                            + "," + ((int)TSP.DataManager.TSAccountingAccType.Entrance).ToString()
                            + "," + ((int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance).ToString();
        GridViewAccounting.Columns["FishPayerId"].Caption = "کد عضویت";
        GridViewAccounting.Columns["TMeId"].Visible = true;
        GridViewAccounting.Columns["FishPayerMembershipType"].Visible = true;
        PanelSearchPeriodReg.Visible = false;
        GridViewAccounting.SettingsDetail.ShowDetailRow = false;
        OdbPeriod.SelectParameters["IsFill"].DefaultValue = "false";
    }

    private void Search()
    {
        switch (PageMode)
        {
            case "L":
                if (cmbPeriods.SelectedItem != null && cmbPeriods.SelectedItem.Value != null)
                    ObjdsEpayment.SelectParameters["PPId"].DefaultValue = cmbPeriods.SelectedItem.Value.ToString();
                else
                    ObjdsEpayment.SelectParameters["PPId"].DefaultValue = "-1";
                break;
        }
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjdsEpayment.SelectParameters["FishPayerId"].DefaultValue = txtMeId.Text;
        else
            ObjdsEpayment.SelectParameters["FishPayerId"].DefaultValue = "-1";

        if (cmbPaymentStatus.SelectedItem != null && cmbPaymentStatus.SelectedItem.Value != null)
            ObjdsEpayment.SelectParameters["Status"].DefaultValue = cmbPaymentStatus.SelectedItem.Value.ToString();
        else
            ObjdsEpayment.SelectParameters["Status"].DefaultValue = "-1";
        GridViewAccounting.DataBind();

    }

    private void ClearSearch()
    {
        switch (PageMode)
        {
            case "L":

                ObjdsEpayment.SelectParameters["PPId"].DefaultValue = "-1";
                ObjdsEpayment.SelectParameters["FishPayerId"].DefaultValue = "-1";
                ObjdsEpayment.SelectParameters["Status"].DefaultValue = "-1";
                GridViewAccounting.DataBind();
                AddNullItemToCmb();
                cmbPeriods.SelectedIndex = -1;
                cmbPaymentStatus.SelectedIndex = -1;
                txtMeId.Text = "";

                break;
        }
    }

    private string GetPrintTitle()
    {
        string Title = "لیست پرداخت های الکترونیکی";
        switch (PageMode)
        {
            case "L":
                Title = "لیست پرداخت های الکترونیکی دوره های آموزشی";
                break;
            case "M":
                Title = "لیست پرداخت های الکترونیکی ثبت نام عضویت";
                break;
        }
        return Title;
    }

    private void CheckPermission()
    {    
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        if (string.IsNullOrEmpty(Request.QueryString["P"]))
        {
            TSP.DataManager.Permission Per = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermissionForAccountingFish(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewAccounting.Visible = Per.CanView;
        }
        else
        {
            switch (PageMode)
            {
                case "L":
                    TSP.DataManager.Permission Per = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermissionForEpaymentPeriodRegister(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    GridViewAccounting.Visible = Per.CanView;
                    break;
                case "M":
                    TSP.DataManager.Permission PerMembership = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermissionForEpaymentPeriodMembership(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                    GridViewAccounting.Visible = PerMembership.CanView;
                    break;
            }
        }
    }
    #endregion
}