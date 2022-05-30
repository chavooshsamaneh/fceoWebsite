using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_TechnicalServices_DesignerPermitted : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ProjectId; string PlansTypeId;
        ProjectId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ProjectId"].ToString())));
        PlansTypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PlansTypeId"].ToString()));
        if (!Utility.IsDBNullOrNullValue(ProjectId))
        {
            TSP.WebsiteReports.TechnicalService.DesignerPermitted dp = new TSP.WebsiteReports.TechnicalService.DesignerPermitted(ProjectId, PlansTypeId);
            RptVMembers.Report = dp;
        }
    }
}