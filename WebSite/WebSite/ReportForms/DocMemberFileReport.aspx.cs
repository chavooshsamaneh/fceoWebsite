using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_DocMemberFileReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string MFId, MeId;

            MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
            MFId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MFId"].ToString()));

            if (!(String.IsNullOrEmpty(MFId)) && !(String.IsNullOrEmpty(MeId)))
            {
                if (Utility.IsDBNullOrNullValue(Request.QueryString["IsBreif"]))
                {
                    TSP.WebsiteReports.Document.DocMemberReport dmr = new TSP.WebsiteReports.Document.DocMemberReport(Convert.ToInt32(MeId), Convert.ToInt32(MFId), Utility.GetCurrentUser_UserId());
                    ReportViewer1.Report = dmr;
                }
                else
                {
                    TSP.WebsiteReports.Document.DocMemberBriefReport dmr = new TSP.WebsiteReports.Document.DocMemberBriefReport(Convert.ToInt32(MeId), Convert.ToInt32(MFId));
                    ReportViewer1.Report = dmr;
                }

            }
            else
            {
                //  this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            //this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.GeneralErr).ToString());
        }
    }
}