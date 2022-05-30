using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_ReportDesignersCity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_DesignerManager.GetUserPermissionForDesignerReport(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            this.ViewState["btnView"] = GridViewProjectDesigner.Visible = btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjectDataSourceReportDesigners.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            }
            else
            {
                ObjectDataSourceReportDesigners.SelectParameters["AgentId"].DefaultValue = "-1";
            }
        }

        if (this.ViewState["btnView"] != null)
            GridViewProjectDesigner.Visible = btnExportExcel.Enabled = btnExportExcel2.Enabled = (bool)this.ViewState["btnView"];
        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtDateFrom = document.getElementById('" + txtDateFrom.ClientID + "').value;";
        script += "var txtDateTo = document.getElementById('" + txtDateTo.ClientID + "').value;";
        script += "var txtDateFromDecreased = document.getElementById('" + txtDateFromDecreased.ClientID + "').value;";
        script += "var txtDateToDecreased = document.getElementById('" + txtDateToDecreased.ClientID + "').value;";
        script += "if ( txtDateFrom=='' && txtDateTo=='' &&  txtDateFromDecreased=='' && txtDateToDecreased=='' && txtMeIdSearch.GetText()=='' && ComboBoxCity.GetSelectedIndex() == -1) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeIdSearch.SetText(''); ComboBoxCity.SetSelectedIndex(-1);
                    document.getElementById('" + txtDateFrom.ClientID + "').value = '';";
        script += " document.getElementById('" + txtDateTo.ClientID + "').value = '';";
        script += " document.getElementById('" + txtDateToDecreased.ClientID + "').value = '';";
        script += " document.getElementById('" + txtDateFromDecreased.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtMeIdSearch.Text))
            ObjectDataSourceReportDesigners.SelectParameters["MeId"].DefaultValue = txtMeIdSearch.Text;
        else
            ObjectDataSourceReportDesigners.SelectParameters["MeId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtDateFrom.Text))
            ObjectDataSourceReportDesigners.SelectParameters["FromDate"].DefaultValue = txtDateFrom.Text;
        else
            ObjectDataSourceReportDesigners.SelectParameters["FromDate"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtDateTo.Text))
            ObjectDataSourceReportDesigners.SelectParameters["ToDate"].DefaultValue = txtDateTo.Text;
        else
            ObjectDataSourceReportDesigners.SelectParameters["ToDate"].DefaultValue = "2";
        if (!string.IsNullOrEmpty(txtDateFromDecreased.Text))
            ObjectDataSourceReportDesigners.SelectParameters["FromDateDecreased"].DefaultValue = txtDateFromDecreased.Text;
        else
            ObjectDataSourceReportDesigners.SelectParameters["FromDateDecreased"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtDateToDecreased.Text))
            ObjectDataSourceReportDesigners.SelectParameters["ToDateDecreased"].DefaultValue = txtDateToDecreased.Text;
        else
            ObjectDataSourceReportDesigners.SelectParameters["ToDateDecreased"].DefaultValue = "2";
        if (!Utility.IsDBNullOrNullValue(ComboBoxCity.Value))
            ObjectDataSourceReportDesigners.SelectParameters["CitId"].DefaultValue = ComboBoxCity.Value.ToString();
        else
            ObjectDataSourceReportDesigners.SelectParameters["CitId"].DefaultValue = "-1";

        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            ObjectDataSourceReportDesigners.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
        else
            ObjectDataSourceReportDesigners.SelectParameters["AgentId"].DefaultValue = "-1";
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {

        GridViewProjectDesigner.DataBind();
        GridViewExporter.FileName = "ReportDesigner";
        GridViewExporter.WriteXlsToResponse(true);
    }
}