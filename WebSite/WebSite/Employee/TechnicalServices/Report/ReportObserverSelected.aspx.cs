using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_ReportObserverSelected : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager.GetUserPermissionReportObserverSelected(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            this.ViewState["btnView"] = GridViewObserverWorkRequest.Visible = btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjectDataSourceSelectObs.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            }
            else
            {
                ObjectDataSourceSelectObs.SelectParameters["AgentId"].DefaultValue = "-1";
            }
        }
        if (this.ViewState["btnView"] != null)
            GridViewObserverWorkRequest.Visible = btnExportExcel.Enabled = btnExportExcel2.Enabled = (bool)this.ViewState["btnView"];



        string script = @" function CheckSearch() { var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";
        script += "var txtProjectId = document.getElementById('" + txtProjectId.ClientID + "').value;";
        script += "var txtMeId = document.getElementById('" + txtMeId.ClientID + "').value;";

        script += "if ( txtCreateDateFrom=='' && txtEndDateTo==''  && txtProjectId=='' && txtMeId=='') return 0; else return 1;  }";
        script += @"function ClearSearch() {
        
        txtMeId.SetText('');
        txtProjectId.SetText('');
        document.getElementById('" + txtCreateDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateTo.ClientID + @"').value='';
      }";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script, true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
        {
            ObjectDataSourceSelectObs.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
        }
        else
        {
            ObjectDataSourceSelectObs.SelectParameters["AgentId"].DefaultValue = "-1";
        }
        if (!string.IsNullOrWhiteSpace(txtMeId.Text))
            ObjectDataSourceSelectObs.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else
            ObjectDataSourceSelectObs.SelectParameters["MeId"].DefaultValue = "-1";

        if (!string.IsNullOrWhiteSpace(txtProjectId.Text))
            ObjectDataSourceSelectObs.SelectParameters["ProjectId"].DefaultValue = txtProjectId.Text;
        else
            ObjectDataSourceSelectObs.SelectParameters["ProjectId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
            ObjectDataSourceSelectObs.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
        else
            ObjectDataSourceSelectObs.SelectParameters["CreateDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
            ObjectDataSourceSelectObs.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
        else
            ObjectDataSourceSelectObs.SelectParameters["CreateDateTo"].DefaultValue = "2";

        GridViewObserverWorkRequest.DataBind();
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewObserverWorkRequest.DataBind();
        GridViewExporter.FileName = "ReportObserver";
        GridViewExporter.WriteXlsxToResponse(true);
    }
}