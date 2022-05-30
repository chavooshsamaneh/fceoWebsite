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

public partial class ReportForms_UserInfoReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string Pass;
            int UserId;

            UserId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["UId"].ToString())));
            Pass = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["P"].ToString()));
            String Code = "";
            try
            {
                if (Request.QueryString["C"] != null && String.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["C"].ToString()))) == false)
                    Code = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["C"].ToString()));
            }
            catch (Exception) { }

            if (!(String.IsNullOrEmpty(UserId.ToString())) && !(String.IsNullOrEmpty(Pass)) && !(String.IsNullOrEmpty(Code)))
            {
                XtraReportUserInfo rpt = new XtraReportUserInfo(UserId, Pass, Code);
                ReportViewer1.Report = rpt;
            }
            else if (!(String.IsNullOrEmpty(UserId.ToString())) && !(String.IsNullOrEmpty(Pass)))
            {
                XtraReportUserInfo rpt = new XtraReportUserInfo(UserId, Pass, "");
                ReportViewer1.Report = rpt;
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }


        }
        catch (Exception)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }
}
