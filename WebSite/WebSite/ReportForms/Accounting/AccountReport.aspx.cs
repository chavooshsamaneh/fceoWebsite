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

public partial class ReportForms_Accounting_AccountReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] == null || Session["Login"].ToString() == "0")
        {
            //     Response.Redirect("../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            //    return;
        }




        if (!(String.IsNullOrEmpty(Request.QueryString["AccId"])))
        {
            string strId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AccId"]).ToString());

            if (string.IsNullOrEmpty(strId))
            {
                Response.Redirect("../../ErrorPage.aspx?ErrorType=1");
                return;
            }

            AccountReport AccR1 = new AccountReport(int.Parse(strId));
            ReportViewer1.Report = AccR1;
        }
        else
            if (Session["AccManager"] != null)
            {
                AccountReport AccR2 = new AccountReport((TSP.DataManager.AccountingAccountManager)Session["AccManager"]);
                ReportViewer1.Report = AccR2;
            }



        


    }
}
