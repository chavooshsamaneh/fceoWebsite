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

public partial class ReportForms_Accounting_PeriodDocBalanceBalance8Columns : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string DocDateFrom, DocDateTo, AgentId, FilterExp, PId, AccTypeId;

        DocDateFrom = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateFrom"]).ToString());
        DocDateTo = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateTo"]).ToString());
        AgentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AgentId"].ToString()));
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());
        PId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PId"]).ToString());
        AccTypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AccTypeId"].ToString()));

        XtraReportPeriodBalance8Columns Balance8R = new XtraReportPeriodBalance8Columns(Convert.ToInt32(PId), DocDateFrom, DocDateTo, Convert.ToInt32(AgentId), FilterExp,Convert.ToInt32(AccTypeId));
        ReportViewer1.Report = Balance8R;
    }
}
