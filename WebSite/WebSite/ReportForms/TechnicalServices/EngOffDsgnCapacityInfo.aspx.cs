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

public partial class ReportForms_TechnicalServices_EngOffDsgnCapacityInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string EngOfId;

        EngOfId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EngOfId"].ToString()));

        XtraReportTSEngOffDsgnCapacityInfo TSEngOffDsgnCapInfo = new XtraReportTSEngOffDsgnCapacityInfo(Convert.ToInt32(EngOfId));
        ReportViewer1.Report = TSEngOffDsgnCapInfo;
    }
}
