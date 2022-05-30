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

public partial class ReportForms_Accounting_DelayDebts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Date, FilterExp, AgentId;

        AgentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AgentId"].ToString()));
        Date = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Date"]).ToString());
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());

        XtraReportDelayDebts DelayDebtsR = new XtraReportDelayDebts(Date, Convert.ToInt32(AgentId), FilterExp);
        ReportViewer1.Report = DelayDebtsR;
    }
}
