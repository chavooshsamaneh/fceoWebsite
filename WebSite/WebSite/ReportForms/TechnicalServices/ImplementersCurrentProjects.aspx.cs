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

public partial class ReportForms_TechnicalServices_ImplementersCurrentProjects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FilterExp, MeOfficeId, MemberTypeId;

        MeOfficeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeOfficeId"].ToString()));
        MemberTypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MemberTypeId"].ToString()));
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"].ToString()));

        XtraReportTSImplementersCurrentProjects TSImpProjects = new XtraReportTSImplementersCurrentProjects(Convert.ToInt32(MeOfficeId),Convert.ToInt32(MemberTypeId),FilterExp);
        ReportViewer1.Report = TSImpProjects;
    }
}
