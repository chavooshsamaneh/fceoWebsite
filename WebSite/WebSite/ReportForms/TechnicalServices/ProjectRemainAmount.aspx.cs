using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_TechnicalServices_ProjectRemainAmount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["AccDocId"] == null)
            return;
        int AccDocId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["AccDocId"]));
        TSP.WebsiteReports.TechnicalService.ProjectRemainAmount ProjectRemainAmount = new TSP.WebsiteReports.TechnicalService.ProjectRemainAmount(AccDocId);
        RptVObserver.Report = ProjectRemainAmount;
    }  
}