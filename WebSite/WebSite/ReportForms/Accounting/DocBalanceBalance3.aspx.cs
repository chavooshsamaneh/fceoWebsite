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

public partial class ReportForms_Accounting_DocBalanceBalance3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //string AgentId, FilterExp;

        //AgentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AgentId"].ToString()));
        //FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());

        //XtraReport Balance3R = new XtraReport(((ObjectDataSource)Session["DataSource"]), ((DevExpress.Web.GridViewColumnCollection)(Session["DataTable"])), Convert.ToInt32(AgentId), FilterExp);
        //ReportViewer1.Report = Balance3R;

        /*************************************************************************************************/
        string DocDateFrom, DocDateTo, AgentId, FilterExp, DocStatusId, Level;

        DocDateFrom = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateFrom"]).ToString());
        DocDateTo = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocDateTo"]).ToString());
        AgentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AgentId"].ToString()));
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());
        DocStatusId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocStatusId"]).ToString());
        Level = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Level"]).ToString());

        XtraReportBalance3 Balance3R = new XtraReportBalance3(DocDateFrom, DocDateTo, Convert.ToInt32(AgentId), FilterExp, Convert.ToInt32(DocStatusId), Convert.ToInt32(Level));
        ReportViewer1.Report = Balance3R;
    }
}
