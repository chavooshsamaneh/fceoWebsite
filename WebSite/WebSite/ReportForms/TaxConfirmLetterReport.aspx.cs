using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_TaxConfirmLetterReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {
            string MeId;

            MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));

            if (!(String.IsNullOrEmpty(MeId)))
            {
                TSP.WebsiteReports.Document.TaxConfirmLetter dmr = new TSP.WebsiteReports.Document.TaxConfirmLetter(Convert.ToInt32(MeId));
                ReportViewer1.Report = dmr;
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