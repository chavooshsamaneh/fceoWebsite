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

public partial class ReportForms_Accounting_DocBalanceBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string DocDateFrom, DocDateTo, FilterExp, AccId, AgentId, DocStatusId, K, M, DocNumberFrom, DocNumberTo;

        AccId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AccId"].ToString()));
        AgentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AgentId"].ToString()));
        DocDateFrom = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateFrom"]).ToString());
        DocDateTo = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateTo"]).ToString());
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());
        DocStatusId=Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocStatusId"]).ToString());
        K = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["K"]).ToString());
        M = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["M"]).ToString());
        DocNumberFrom = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocNumberFrom"]).ToString());
        DocNumberTo = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocNumberTo"]).ToString());

        XtraReportBook BookR = new XtraReportBook(Convert.ToInt32(AccId), DocDateFrom, DocDateTo, Convert.ToInt32(AgentId), FilterExp, Convert.ToInt32(DocStatusId), K, M, Convert.ToInt32(DocNumberFrom), Convert.ToInt32(DocNumberTo));
        ReportViewer1.Report = BookR;

        if (!IsPostBack)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyNegativePattern = 14;
        }
    }
}
