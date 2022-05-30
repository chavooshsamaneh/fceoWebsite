using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_ReportAccountingFishHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission perPrintHistory = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermissionForTSAccountingFishPrint(Utility.GetCurrentUser_UserId(),(TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewPrintingHistory.Visible = perPrintHistory.CanView;
            btnPrint2.Enabled = btnPrint.Enabled = perPrintHistory.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = perPrintHistory.CanView;
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        //objdsPrintingHistory.SelectParameters["TableType"].DefaultValue = ((int)TSP.DataManager.TableType.AccountingPrintedBankFish).ToString();
        GridViewPrintingHistory.DataBind();
        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];

        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";

        script += "if ( txtCreateDateTo=='' && txtCreateDateFrom=='' ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                   
                    document.getElementById('" + txtCreateDateTo.ClientID + "').value = ''; document.getElementById('" + txtCreateDateFrom.ClientID + "').value = '';";
        script += "}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "SearchFish", script);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "AccountingFishPrintHistoryReport";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void GridViewPrintingHistory_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":

                    GridViewPrintingHistory.JSProperties["cpPrint"] = 1;

                    Session["DataTable"] = GridViewPrintingHistory.Columns;
                    Session["DataSource"] = objdsPrintingHistory;

                    Session["Title"] = "گزارش چاپ فیش";
                    break;
            }
        }
    }
    private void Search()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
                objdsPrintingHistory.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
            else
                objdsPrintingHistory.SelectParameters["CreateDateFrom"].DefaultValue = "1";

            if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
                objdsPrintingHistory.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
            else
                objdsPrintingHistory.SelectParameters["CreateDateTo"].DefaultValue = "2";


            GridViewPrintingHistory.DataBind();
        }
        catch (Exception ex) { Utility.SaveWebsiteError(ex); }
    }
}