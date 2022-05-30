using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Employee_SMS_SMSInbox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //  ASPxButton1.Visible = false;
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.SmsManager.GetUserPermissionForSmsInbox(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnExportExcel.Enabled = btnExportExcel2.Enabled = GridViewInbox.Visible = per.CanView;

        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtSMSDateFrom = document.getElementById('" + txtSMSDateFrom.ClientID + "').value;";
        script += "var txtSMSDateTo = document.getElementById('" + txtSMSDateTo.ClientID + "').value;";

        script += "if (txtSMSDateFrom==''&& txtSMSDateTo=='') return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                  
                    document.getElementById('" + txtSMSDateFrom.ClientID + "').value = ''; document.getElementById('" + txtSMSDateTo.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "SMSInbox";
        //GridViewExporter.ExportedRowType = DevExpress.Web.GridViewExportedRowType.Selected;
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        string OurNumber = "09830007957950106";
        string UserNumber = "09177029545";
        string Message = "test";
        Response.Redirect("~/RecievedSMS.aspx?To=" + OurNumber + "&From=" + UserNumber + "&TEXT=" + Message);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    private void Search()
    {
        if (!string.IsNullOrEmpty(txtSMSDateFrom.Text))
            objdsSMS.SelectParameters["SMSDateFrom"].DefaultValue = txtSMSDateFrom.Text;
        else
            objdsSMS.SelectParameters["SMSDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtSMSDateTo.Text))
            objdsSMS.SelectParameters["SMSDateTo"].DefaultValue = txtSMSDateTo.Text;
        else
            objdsSMS.SelectParameters["SMSDateTo"].DefaultValue = "2";
        GridViewInbox.DataBind();
    }
}
