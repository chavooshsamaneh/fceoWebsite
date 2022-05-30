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

public partial class Members_Amoozesh_CourseRefrences : System.Web.UI.Page
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
            if (string.IsNullOrEmpty(Request.QueryString["CrsId"]))
            {
                Response.Redirect("Course.aspx");
            }

            try
            {
                CourseId.Value = Server.HtmlDecode(Request.QueryString["CrsId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

        }
        btnBack.PostBackUrl = "CourseView.aspx?CrsId=" + CourseId.Value;
        btnBack2.PostBackUrl = "CourseView.aspx?CrsId=" + CourseId.Value;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CourseView.aspx?CrsId=" + CourseId.Value );
    }
    protected void MenuCourseDetail_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Prerequisite":
                Response.Redirect("CoursePrerequisite.aspx?CrsId=" + CourseId.Value);
                break;
            case "CourseDetail":
                Response.Redirect("CourseDetails.aspx?CrsId=" + CourseId.Value);
                break;
            case "Course":
                Response.Redirect("CourseView.aspx?CrsId=" + CourseId.Value);
                break;
            case "Group":
                Response.Redirect("CourseGroups.aspx?CrsId=" + CourseId.Value);
                break;
            case "Attachment":
                Response.Redirect("CourseAttachments.aspx?CrsId=" + CourseId.Value);
                break;
        }
    }
}
