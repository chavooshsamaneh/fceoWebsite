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

public partial class ReportForms_MemberTemporaryCardReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string MeId;

            MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));

            if (!(String.IsNullOrEmpty(MeId)))
            {
                XtraReportMemberTemporaryCard rpt = new XtraReportMemberTemporaryCard(Convert.ToInt32(MeId));
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
