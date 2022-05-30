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

public partial class ReportForms_EmployeeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] == null || Session["Login"].ToString() == "0")
        {
       //     Response.Redirect("../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        }
        EmployeeReport EmpR;

        // if(!(String.IsNullOrEmpty(Request.QueryString["EpId"])))
        string s = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpId"]).ToString());

        if (string.IsNullOrEmpty(s))
        {
            Response.Redirect("../../ErrorPage.aspx?ErrorType=1");
            return;
        }

        int ID = int.Parse(s);
        
        EmpR = new EmployeeReport(ID);      
        ReportViewer1.Report = EmpR;
  

    }
}
