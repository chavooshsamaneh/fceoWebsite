using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_DocumentMemberCountByMajor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      //  try
       // {
        TSP.DataManager.Permission reportDocMe = TSP.DataManager.MemberManager.GetUserPermissionForReportDocMemberBySparateMajor(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
           if (reportDocMe.CanView == false)
            return;
            TSP.WebsiteReports.Document.DocumentCountByMajorReport dmr = new TSP.WebsiteReports.Document.DocumentCountByMajorReport();
            ReportViewer1.Report = dmr;
       // }
       // catch (Exception)
      //  {
    //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.GeneralErr).ToString());
     //   }
    }
}