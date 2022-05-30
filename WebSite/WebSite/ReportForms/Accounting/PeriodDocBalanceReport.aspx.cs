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

public partial class ReportForms_Accounting_PeriodDocBalanceReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string DocBalanceId, PId;

        DocBalanceId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocBalanceId"]).ToString());
        PId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PId"]).ToString());

        XtraReportPDocBalance PDocBalanceR = new XtraReportPDocBalance(Convert.ToInt32(PId),Convert.ToInt32(DocBalanceId));
        ReportViewer1.Report = PDocBalanceR;
    }
}
