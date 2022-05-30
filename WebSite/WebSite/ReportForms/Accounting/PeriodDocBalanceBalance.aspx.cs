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

public partial class ReportForms_Accounting_PeriodDocBalanceBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string DocDateFrom, DocDateTo, FilterExp, AccTypeId, AgentId, DocNumberFrom, DocNumberTo, DocStatusId,PId;

        AccTypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AccTypeId"].ToString()));
        AgentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AgentId"].ToString()));
        DocDateFrom = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateFrom"]).ToString());
        DocDateTo = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateTo"]).ToString());
        DocNumberFrom = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocNumberFrom"]).ToString());
        DocNumberTo = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocNumberTo"]).ToString());
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());
        DocStatusId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocStatusId"]).ToString());
        PId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PId"]).ToString());

        XtraReportPeriodBalance BalanceR = new XtraReportPeriodBalance(Convert.ToInt32(PId),Convert.ToInt32(AccTypeId), DocDateFrom, DocDateTo, Convert.ToInt32(AgentId), Convert.ToInt32(DocNumberFrom), Convert.ToInt32(DocNumberTo), FilterExp, Convert.ToInt32(DocStatusId));
        ReportViewer1.Report = BalanceR;
    }
}
