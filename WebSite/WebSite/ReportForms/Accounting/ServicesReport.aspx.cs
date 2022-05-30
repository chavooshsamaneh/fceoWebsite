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

public partial class ReportForms_Accounting_ServicesReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FilterExp;

        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());

        XtraReportServices R = new XtraReportServices(FilterExp);
        ReportViewer1.Report = R;
    }
}
