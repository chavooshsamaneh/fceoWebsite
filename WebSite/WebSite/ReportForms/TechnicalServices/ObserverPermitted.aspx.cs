using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_TechnicalServices_ObserverPermitted : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ProjectId, AccountingId,HasText;
        ProjectId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ProjectId"].ToString())));
        AccountingId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AccountingId"].ToString())));
        if (Request.QueryString["HasText"] != null)
            HasText = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["HasText"].ToString())));
        else HasText = 0;

        if (!Utility.IsDBNullOrNullValue(ProjectId))
        {
            TSP.WebsiteReports.TechnicalService.ObserverPermitted dp = new TSP.WebsiteReports.TechnicalService.ObserverPermitted(ProjectId, AccountingId, HasText);
            RptVMembers.Report = dp;
        }
    }
}