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

public partial class ReportForms_MemberObsCapacity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string MeId, Year;

        MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
        Year = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Year"].ToString()));

        XtraReportTSMemObsCapacity TSMemObsCapInfo = new XtraReportTSMemObsCapacity(Convert.ToInt32(MeId), Year);
        ReportViewer1.Report = TSMemObsCapInfo;
    }
}
