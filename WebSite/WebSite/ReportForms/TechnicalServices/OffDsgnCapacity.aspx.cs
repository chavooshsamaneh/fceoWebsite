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

public partial class ReportForms_TechnicalServices_OffDsgnCapacity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OfficeId, Year;

        OfficeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfficeId"].ToString()));
        Year = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Year"].ToString()));

        XtraReportTSOffDsgnCapacity TSOffDsgnCap = new XtraReportTSOffDsgnCapacity(Convert.ToInt32(OfficeId), Year);
        ReportViewer1.Report = TSOffDsgnCap;
    }
}
