using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_TechnicalServices_Report_ProjectObserverReport : System.Web.UI.Page
{
    #region properties
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        if (!IsPostBack)
        {
            CheckUserPermissions();
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjdObserveReport.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            }
            else
            {
                ObjdObserveReport.SelectParameters["AgentId"].DefaultValue = "-1";
            }
        }       
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
        string script = @"<SCRIPT language='javascript'> function CheckSearch() {";
        script += "var txtDateFromDecreased = document.getElementById('" + txtDateFromDecreased.ClientID + "').value;";        
        script += "var txtDateToDecreased = document.getElementById('" + txtDateToDecreased.ClientID + "').value;";
        script += "if ( txtDateFromDecreased==''  && txtDateToDecreased=='' && txtProjectId.GetText()=='' && txtRegNo.GetText()=='' && txtMeId.GetText() == '' && txtLisNo.GetText() == '') return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                      txtProjectId.SetText('');   txtRegNo.SetText('');   txtMeId.SetText(''); txtLisNo.SetText(''); }</SCRIPT>";
        
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewObserverReport.DataBind();
        GridViewExporter.FileName = "Observer";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnSearch_Onclick(object sender, EventArgs e)
    {
        Search();
    }

    #endregion

    #region Method
    private void SetWarningLableDisable()
    {
        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void Search()
    {
        try
        {
            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ObjdObserveReport.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            }
            else
            {
                ObjdObserveReport.SelectParameters["AgentId"].DefaultValue = "-1";
            }
            if (!string.IsNullOrEmpty(txtDateFromDecreased.Text))
                ObjdObserveReport.SelectParameters["FromDateDecreased"].DefaultValue = txtDateFromDecreased.Text;
            else
                ObjdObserveReport.SelectParameters["FromDateDecreased"].DefaultValue = "1";
            if (!string.IsNullOrEmpty(txtDateToDecreased.Text))
                ObjdObserveReport.SelectParameters["ToDateDecreased"].DefaultValue = txtDateToDecreased.Text;
            else
                ObjdObserveReport.SelectParameters["ToDateDecreased"].DefaultValue = "2";
            if (!string.IsNullOrEmpty(txtRegNo.Text))
                ObjdObserveReport.SelectParameters["RegisteredNo"].DefaultValue = txtRegNo.Text;
            else
                ObjdObserveReport.SelectParameters["RegisteredNo"].DefaultValue = "%";
            if (!string.IsNullOrEmpty(txtProjectId.Text))
                ObjdObserveReport.SelectParameters["projectId"].DefaultValue = txtProjectId.Text;
            else
                ObjdObserveReport.SelectParameters["projectId"].DefaultValue = "-1";
            if (!string.IsNullOrEmpty(txtLisNo.Text))
                ObjdObserveReport.SelectParameters["ListNo"].DefaultValue = txtLisNo.Text;
            else
                ObjdObserveReport.SelectParameters["ListNo"].DefaultValue = "-1";

            if (!string.IsNullOrEmpty(txtMeId.Text))
                ObjdObserveReport.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
            else
                ObjdObserveReport.SelectParameters["MeId"].DefaultValue = "-1";           
            
            GridViewObserverReport.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }    
    private void CheckUserPermissions()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermissionReportObserverWage(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnExportExcel.Enabled = btnExportExcel2.Enabled= GridViewObserverReport.Visible = per.CanView;
    }

    #endregion
}