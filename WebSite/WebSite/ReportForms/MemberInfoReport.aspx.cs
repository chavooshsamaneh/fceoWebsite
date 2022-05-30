using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_MemberInfoReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string  MeId;

            MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));

            if ( !(String.IsNullOrEmpty(MeId)))
            {
                TSP.WebsiteReports.Members.MemberInfoReport MemberInfoReport = new TSP.WebsiteReports.Members.MemberInfoReport(Convert.ToInt32(MeId));
                RptViewerMembersInfo.Report = MemberInfoReport;
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.GeneralErr).ToString());
        }
    }
}