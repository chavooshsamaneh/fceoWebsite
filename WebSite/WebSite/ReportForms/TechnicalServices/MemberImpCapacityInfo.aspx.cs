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

public partial class ReportForms_MemberImpCapacityInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string MeId;

        MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));

        XtraReportTSMemImpCapacityInfo TSMemImpCapInfo = new XtraReportTSMemImpCapacityInfo(Convert.ToInt32(MeId));
        ReportViewer1.Report = TSMemImpCapInfo;
    }
}
