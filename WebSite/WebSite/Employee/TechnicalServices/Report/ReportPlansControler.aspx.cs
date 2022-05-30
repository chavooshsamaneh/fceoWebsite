using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_ReportPlansControler : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSPlansConfirming).ToString();
            TSP.DataManager.Permission perPlan = TSP.DataManager.TechnicalServices.Plans_ControlerManager.GetUserPermissionPlanControlerReport(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewPlansControler.Visible = perPlan.CanView;

            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }


    #endregion

    #region Methods

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewPlansControler.DataBind();
        GridViewExporter.FileName = "Report";
        GridViewExporter.WriteXlsToResponse(true);
    }


    #endregion
}