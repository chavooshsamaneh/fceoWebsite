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

public partial class ReportForms_Accounting_PeriodDocBalanceBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string DocDateFrom, DocDateTo, FilterExp, AccId, AgentId, DocStatusId, PId, K, M, DocNumberFrom, DocNumberTo;

        AccId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AccId"].ToString()));
        AgentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AgentId"].ToString()));
        DocDateFrom = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateFrom"]).ToString());
        DocDateTo = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateTo"]).ToString());
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());
        DocStatusId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocStatusId"]).ToString());
        PId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PId"]).ToString());
        K = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["K"]).ToString());
        M = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["M"]).ToString());
        DocNumberFrom = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocNumberFrom"]).ToString());
        DocNumberTo = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocNumberTo"]).ToString());

        XtraReportPeriodBook BookR = new XtraReportPeriodBook(Convert.ToInt32(PId), Convert.ToInt32(AccId), DocDateFrom, DocDateTo, Convert.ToInt32(AgentId), FilterExp, Convert.ToInt32(DocStatusId), K, M, Convert.ToInt32(DocNumberFrom), Convert.ToInt32(DocNumberTo));
        ReportViewer1.Report = BookR;
    }
}
