using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_TechnicalServices_ProjectRemainAmountSnap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["AccDocId"] == null)
            return;
        int AccDocId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["AccDocId"]));
        TSP.WebsiteReports.TechnicalService.ProjectRemainAmountSnap ProjectRemainAmountSnap = new TSP.WebsiteReports.TechnicalService.ProjectRemainAmountSnap(AccDocId, Utility.GetCurrentUser_UserId());
        RptRminAmnSnp.Report = ProjectRemainAmountSnap;
    }
}