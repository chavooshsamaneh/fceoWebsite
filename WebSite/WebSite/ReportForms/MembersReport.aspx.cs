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

public partial class ReportForms_MembersReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}


        string MReId, MeId, PageMode, Password;
        int UserId;

        MReId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MReId"].ToString()));
        MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
        PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));
        Password = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Password"].ToString()));
        UserId = int.Parse(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["UserId"].ToString())));

        if (!(String.IsNullOrEmpty(MReId)) && !(String.IsNullOrEmpty(MeId)))
        {
            XtraReportMembers MeR = new XtraReportMembers(Convert.ToInt32(MeId), Convert.ToInt32(MReId), PageMode, Password, UserId);
            RptVMembers.Report = MeR;
        }

        ////int.Parse(Request.QueryString["MeId"].ToString()) //else
        //    EPR = new ExpertPlaceReport(int.Parse(Request.QueryString["EpId2"].ToString()));

    }
}
