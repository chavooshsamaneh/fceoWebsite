using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TraceDocuments_TraceDocMemberFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //  Load_PageData(int.Parse(Utility.DecryptQS(Request.QueryString["MfId"])));

            string MfId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MfId"].ToString()));
            if (!String.IsNullOrEmpty(MfId))
            {
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                System.Data.DataTable dt = DocMemberFileManager.SelectForReportMemberFile(-1, -1, Convert.ToInt32(MfId));
                if (dt.Rows.Count == 1)
                {
                   // ImgMeURL.ImageUrl = dt.Rows[0]["ImageUrl"].ToString();
                    if (Convert.ToInt32(dt.Rows[0]["IsConfirm"]) ==0)
                        lblWarning.Text = "وضعیت تایید پروانه مورد نظر بر اساس بارکد وارد نموده در جریان می باشد.بنابراین بر اساس قوانین تا تایید نهایی این پروانه توسط سازمان راه و شهرسازی  شماره پروانه در این صفحه نمایش داده نمی شود";
                    else
                        lblWarning.Text = "";
                }
                TSP.WebsiteReports.Document.ReportDocPersonPrePrint ReportDocPersonPrePrint = new TSP.WebsiteReports.Document.ReportDocPersonPrePrint(Convert.ToInt32(MfId), true);
                RptMemberFile.Report = ReportDocPersonPrePrint;
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/TraceDocuments/TraceDocuments.aspx");
        }
    }
}