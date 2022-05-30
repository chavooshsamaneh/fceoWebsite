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

public partial class ReportForms_Accounting_DebtReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string LoanPaymentId, FilterExp, BankAccId, PaymentFacilitiesAccId;

        LoanPaymentId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["LoanPaymentId"].ToString()));
        FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());
        BankAccId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["BankAccId"]).ToString());
        PaymentFacilitiesAccId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PaymentFacilitiesAccId"]).ToString());

        XtraReportDebt DebtR = new XtraReportDebt(Convert.ToInt32(LoanPaymentId), int.Parse(BankAccId), int.Parse(PaymentFacilitiesAccId), FilterExp);
        ReportViewer1.Report = DebtR;
    }
}
