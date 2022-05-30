using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_TechnicalServices_ProjectObserverWageByDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["AccDocId"] == null)
            return;
        int AccDocId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["AccDocId"]));
        int AgentId = -2;
        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
        {
            AgentId = Utility.GetCurrentUser_AgentId();
        }
        TSP.WebsiteReports.TechnicalService.ProjectObserverWageByDocuments ProjectObserverWageByDocuments = new TSP.WebsiteReports.TechnicalService.ProjectObserverWageByDocuments(AccDocId, AgentId);
        RptVObserver.Report = ProjectObserverWageByDocuments;
    }
}