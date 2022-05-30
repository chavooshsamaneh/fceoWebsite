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

public partial class ReportForms_Accounting_DocBalanceReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] == null || Session["Login"].ToString() == "0")
        {
            //     Response.Redirect("../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            //    return;
        }



        if (!string.IsNullOrEmpty(Request.QueryString["DocBalanceId"]))
        {
            string strId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["DocBalanceId"]).ToString());

            if (string.IsNullOrEmpty(strId))
            {
                Response.Redirect("../../ErrorPage.aspx?ErrorType=1");
                return;
            }

            DocBalanceReport docR1 = new DocBalanceReport(Convert.ToInt32(strId));
            ReportViewer1.Report = docR1;

        }
        else
        {


            if (Session["DocBalanceDetailManager"] != null)
            {
                DocBalanceReport docR2 = new DocBalanceReport((TSP.DataManager.AccountingDocBalanceDetailManager)Session["DocBalanceDetailManager"]);
                ReportViewer1.Report = docR2;
            }
        }
        

    }
}
