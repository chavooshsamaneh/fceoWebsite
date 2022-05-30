using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_ObserverAnnounce : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string MeId;
            MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
            if (!(String.IsNullOrEmpty(MeId)))
            {
                TSP.WebsiteReports.Document.ObserverAnnounce rpt = new TSP.WebsiteReports.Document.ObserverAnnounce(Convert.ToInt32(MeId));
                ReportViewer1.Report = rpt;
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }
        catch (Exception)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }
}