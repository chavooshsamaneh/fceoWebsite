using System;
using System.Data;
using System.Configuration;
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

public partial class ReportForms_Accounting_PrintedBankFishReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DataTable"] != null)
        {
            XtraReportPrintedBankFishReport PrnR = new XtraReportPrintedBankFishReport((System.Data.DataTable)Session["DataTable"]);
            ReportViewer1.Report = PrnR;
        }
        else
        {
            string FilterExp, AgentId;

            AgentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AgentId"].ToString()));
            FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());

            PrintedBankFishReport PrnR = new PrintedBankFishReport(Convert.ToInt32(AgentId), FilterExp);
            ReportViewer1.Report = PrnR;
        }
    }
}
