using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_ReportEpaymentDesignerFish : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSProjectConfirming).ToString();
            TSP.DataManager.Permission perPrintHistory = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermissionForTSEpaymentDesinger(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridReportEpaymentFish.Visible = perPrintHistory.CanView;
            btnPrint2.Enabled = btnPrint.Enabled = perPrintHistory.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = perPrintHistory.CanView;
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
            cmbStatus.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            cmbStatus.SelectedIndex = 0;
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ComboAgent.ClientEnabled = false;
                ComboAgent.DataBind();
                ComboAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
                ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId()).Index;
            }
            else
            {
                ComboAgent.ClientEnabled = true;
                ComboAgent.DataBind();
                ComboAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
                ComboAgent.SelectedIndex = 0;
            }
            Search();
        }

        GridReportEpaymentFish.DataBind();
        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["btnExportExcel"] != null)
            GridReportEpaymentFish.Visible = this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];

        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";

        script += "if ( txtCreateDateTo=='' && txtCreateDateFrom=='' && cmbStatus.GetSelectedIndex() == 0 && ComboAgent.GetSelectedIndex() == 0  ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                   cmbStatus.SetSelectedIndex(0);ComboAgent.SetSelectedIndex(0);
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
        GridViewExporter.FileName = "EpayemntDesignerReport";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void GridReportEpaymentFish_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":

                    System.Collections.ArrayList DeletedColumnsName = new System.Collections.ArrayList();
                    DeletedColumnsName.Add("WFState");
                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    GridReportEpaymentFish.JSProperties["cpPrint"] = 1;
                    Session["DataTable"] = GridReportEpaymentFish.Columns;
                    Session["DataSource"] = ObjectReportEpaymentDesignerFish;

                    Session["Title"] = "گزارش پرداخت الکترونیکی";                    
                    break;
            }
        }
    }
    private void Search()
    {
        try
        {

            ObjectReportEpaymentDesignerFish.SelectParameters["ProjectIngridientTypeId"].DefaultValue = ((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString();
            ObjectReportEpaymentDesignerFish.SelectParameters["AccTypeList"].DefaultValue = ((int)TSP.DataManager.TSAccountingAccType.Designing5Percent).ToString() + ","
                + ((int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation).ToString() + ","
                + ((int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure).ToString();

            if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
                ObjectReportEpaymentDesignerFish.SelectParameters["FromDate"].DefaultValue = txtCreateDateFrom.Text;
            else
                ObjectReportEpaymentDesignerFish.SelectParameters["FromDate"].DefaultValue = "1";

            if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
                ObjectReportEpaymentDesignerFish.SelectParameters["ToDate"].DefaultValue = txtCreateDateTo.Text;
            else
                ObjectReportEpaymentDesignerFish.SelectParameters["ToDate"].DefaultValue = "2";

            if (cmbStatus.SelectedIndex != 0)
                ObjectReportEpaymentDesignerFish.SelectParameters["Status"].DefaultValue = cmbStatus.SelectedItem.Value.ToString();
            else
                ObjectReportEpaymentDesignerFish.SelectParameters["Status"].DefaultValue = "-1";

            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjectReportEpaymentDesignerFish.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
                ComboAgent.ClientEnabled = false;
                ComboAgent.DataBind();
                ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId()).Index;
            }
            else
            {
                if (ComboAgent.SelectedItem != null && ComboAgent.SelectedItem.Value != null)
                    ObjectReportEpaymentDesignerFish.SelectParameters["AgentId"].DefaultValue = ComboAgent.Value.ToString();
                else
                    ObjectReportEpaymentDesignerFish.SelectParameters["AgentId"].DefaultValue = "-1";
            }
            ObjectReportEpaymentDesignerFish.DataBind();
        }
        catch (Exception ex) { Utility.SaveWebsiteError(ex); }
    }
}