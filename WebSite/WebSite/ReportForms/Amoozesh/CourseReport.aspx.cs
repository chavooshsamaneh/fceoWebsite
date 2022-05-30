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
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;

public partial class ReportForms_Amoozesh_CourseReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue(Request.QueryString["P"]) && !Utility.IsDBNullOrNullValue(Request.QueryString["M"]))
        {          
            try
            {


                

                //string MeId =Utility.DecryptQS(Request.QueryString["MeId"]);
                //string PPId = Utility.DecryptQS(Request.QueryString["PPId"]);
                string MeId =Request.QueryString["M"];
                string PPId = Request.QueryString["P"];
                TSP.WebsiteReports.Amoozesh.CertificatePreview CertificatePreview = new TSP.WebsiteReports.Amoozesh.CertificatePreview(int.Parse(PPId), int.Parse(MeId));
                ReportViewer1.Report = CertificatePreview;


            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }
    }
}
