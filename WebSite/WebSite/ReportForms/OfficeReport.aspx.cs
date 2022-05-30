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

public partial class ReportForms_OfficeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

        try
        {
            string OfReId, OfId;

            OfReId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfReId"].ToString()));
            OfId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["OfId"].ToString()));

            if (!(String.IsNullOrEmpty(OfReId)) && !(String.IsNullOrEmpty(OfId)))
            {
                OfficeReports OffR = new OfficeReports(Convert.ToInt32(OfId), Convert.ToInt32(OfReId));
                ReportViewer1.Report = OffR;
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
