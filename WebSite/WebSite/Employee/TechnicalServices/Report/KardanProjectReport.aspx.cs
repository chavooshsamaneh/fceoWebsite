using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Report_KardanProjectReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager.GetUserPermissionKardanReport(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            this.ViewState["btnView"] = GridViewProject.Visible = btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;     

            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                ComboAgent.ClientEnabled = false;
                ComboAgent.DataBind();
                ComboAgent.SelectedIndex = ComboAgent.Items.FindByValue(Utility.GetCurrentUser_AgentId()).Index;
            }
            else
            {
                ComboAgent.ClientEnabled = true;
                ComboAgent.DataBind();
                ComboAgent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
                ComboAgent.SelectedIndex = 0;
            }
        }
        if (this.ViewState["btnView"] != null)
            GridViewProject.Visible = btnExportExcel.Enabled = btnExportExcel2.Enabled = (bool)this.ViewState["btnView"];
        string script = @"<SCRIPT language='javascript'> function CheckSearch() {";

        script += "if ( txtProjectId.GetText() == '' && txtFirstName.GetText() == '' && txtLastName.GetText() == '' && txtOtpCode.GetText() == ''  && txtProjectId.GetText() == '' && ComboAgent.GetSelectedIndex() == 0 ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtProjectId.SetText(''); txtFirstName.SetText('');  txtLastName.SetText('');  txtOtpCode.SetText('');  txtProjectId.SetText(''); 
                 ComboAgent.SetSelectedIndex(0);";
        script += "}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {

        GridViewProject.DataBind();
        GridViewExporter.FileName = "ReportKardan";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnSearch_Onclick(object sender, EventArgs e)
    {
        Search();
    }
    #region Methods
    private void Search()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtProjectId.Text))
                ObjdKardanProject.SelectParameters["projectId"].DefaultValue = txtProjectId.Text;
            else
                ObjdKardanProject.SelectParameters["projectId"].DefaultValue = "-1";

            if (!string.IsNullOrEmpty(txtFirstName.Text))
                ObjdKardanProject.SelectParameters["FirstName"].DefaultValue = txtFirstName.Text;
            else
                ObjdKardanProject.SelectParameters["FirstName"].DefaultValue = "%";

            if (!string.IsNullOrEmpty(txtLastName.Text))
                ObjdKardanProject.SelectParameters["LastName"].DefaultValue = txtLastName.Text;
            else
                ObjdKardanProject.SelectParameters["LastName"].DefaultValue = "%";

            if (!string.IsNullOrEmpty(txtOtpCode.Text))
                ObjdKardanProject.SelectParameters["OtpCode"].DefaultValue = txtOtpCode.Text;
            else
                ObjdKardanProject.SelectParameters["OtpCode"].DefaultValue = "%";

            if (!string.IsNullOrEmpty(txtProjectId.Text))
                ObjdKardanProject.SelectParameters["ProjectId"].DefaultValue = txtProjectId.Text;
            else
                ObjdKardanProject.SelectParameters["ProjectId"].DefaultValue = "-1";

            if (ComboAgent.SelectedItem.Value != null)
                ObjdKardanProject.SelectParameters["AgentId"].DefaultValue = ComboAgent.SelectedItem.Value.ToString();
            else
                ObjdKardanProject.SelectParameters["AgentId"].DefaultValue = "-1";

            GridViewProject.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion
}