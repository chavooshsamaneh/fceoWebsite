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

public partial class Members_Amoozesh_Course : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            OdbCourse.FilterParameters[0].DefaultValue = "0";//active
        }
    }

    protected void btnViewCourse_DataBinding(object sender, EventArgs e)
    {
        LinkButton btnViewCourse = (LinkButton)sender;
        btnViewCourse.PostBackUrl = "CourseView.aspx?CrsId=" + Utility.EncryptQS(btnViewCourse.ToolTip);
        btnViewCourse.ToolTip = "";
    }

}
