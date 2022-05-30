using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_ReportProjectOwner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.OwnerManager.GetUserPermissionForReportOwner(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnExportExcel.Visible = btnExportExcel2.Visible = GridViewOwner.Visible = per.CanView;
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjectDataSourceOwner.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            }
            else
            {
                ObjectDataSourceOwner.SelectParameters["AgentId"].DefaultValue = "-1";
            }
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewOwner.DataBind();
            GridViewExporter.FileName = "ReportOwner";
            GridViewExporter.WriteXlsToResponse(true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }
    private void ShowMessage(string Message)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Message;
    }
}