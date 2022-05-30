using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_Amoozesh_Periodattender : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(Request.QueryString["PPId"]))
            return;
        try
        {
            string PPId = Utility.DecryptQS(Request.QueryString["PPId"]);
            TSP.WebsiteReports.Amoozesh.PeriodAttenderReport PeriodAttenderReport = new TSP.WebsiteReports.Amoozesh.PeriodAttenderReport(int.Parse(PPId));
            ReportViewer1.Report = PeriodAttenderReport;
        }
        catch
        {
        }

    }
}