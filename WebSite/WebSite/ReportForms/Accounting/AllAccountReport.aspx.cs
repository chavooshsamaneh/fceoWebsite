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

public partial class ReportForms_Accounting_AllAccountReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string FilterExp;

        //FilterExp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["FilterExp"]).ToString());
        if (Request.QueryString["IsHoverDetail"]!=null)
        {
            string AccType = Utility.DecryptQS(Request.QueryString["IsHoverDetail"].ToString());
            if (int.Parse(AccType) == (int)TSP.DataManager.AccountingAccType.HoverDetails)
            {
                XtraReportAccount AccountR = new XtraReportAccount((int)TSP.DataManager.AccountingAccType.HoverDetails);
                ReportViewer1.Report = AccountR;
                this.Title = "تفضیلی های شناور";
            }
        }
        else
        {
            XtraReportAccount AccountR = new XtraReportAccount();
            ReportViewer1.Report = AccountR;
        }
    }
}
