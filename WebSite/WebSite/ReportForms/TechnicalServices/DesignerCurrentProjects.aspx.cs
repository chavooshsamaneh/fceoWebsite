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

public partial class ReportForms_TechnicalServices_DesignerCurrentProjects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FilterExp, OfficeEngOId, MemberTypeId;

        OfficeEngOId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfficeEngOId"].ToString()));
        MemberTypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MemberTypeId"].ToString()));
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"].ToString()));

        XtraReportTSDesignerCurrentProjects TSImpProjects = new XtraReportTSDesignerCurrentProjects(Convert.ToInt32(OfficeEngOId), Convert.ToInt32(MemberTypeId), FilterExp);
        ReportViewer1.Report = TSImpProjects;
    }
}
