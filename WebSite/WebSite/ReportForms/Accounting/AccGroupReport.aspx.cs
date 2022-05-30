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

public partial class ReportForms_Accounting_AccGroupReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FilterExp;

        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());

        XtraReportAccGroup GroupR = new XtraReportAccGroup(FilterExp);
        ReportViewer1.Report = GroupR;
    }
}
