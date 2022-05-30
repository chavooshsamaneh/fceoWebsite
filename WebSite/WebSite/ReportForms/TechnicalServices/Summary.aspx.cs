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

public partial class ReportForms_TechnicalServices_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ProjectId;

        ProjectId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ProjectId"].ToString()));

        XtraReportTSSummary TSSummary = new XtraReportTSSummary(Convert.ToInt32(ProjectId));
        ReportViewer1.Report = TSSummary;
    }
}
