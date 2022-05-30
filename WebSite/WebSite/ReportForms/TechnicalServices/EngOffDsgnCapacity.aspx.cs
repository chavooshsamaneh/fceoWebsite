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

public partial class ReportForms_TechnicalServices_EngOffDsgnCapacity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string EngOfId, Year;

        EngOfId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EngOfId"].ToString()));
        Year = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Year"].ToString()));

        XtraReportTSEngOffDsgnCapacity TSOffDsgnCap = new XtraReportTSEngOffDsgnCapacity(Convert.ToInt32(EngOfId), Year);
        ReportViewer1.Report = TSOffDsgnCap;
    }
}
