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

public partial class ReportForms_TechnicalServices_OffObsCapacityInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OfficeId;

        OfficeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfficeId"].ToString()));

        XtraReportTSOffObsCapacityInfo TSOffObsCapInfo = new XtraReportTSOffObsCapacityInfo(Convert.ToInt32(OfficeId));
        ReportViewer1.Report = TSOffObsCapInfo;
    }
}
