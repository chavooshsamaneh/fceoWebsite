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

public partial class Members_Amoozesh_CourseView : System.Web.UI.Page
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

            string CrsId = Utility.DecryptQS(CourseId.Value);
            if (!string.IsNullOrEmpty(CrsId))
                FillForm(int.Parse(CrsId));
            else
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

        }

        btnBack.PostBackUrl = "Course.aspx";
        ASPxButton6.PostBackUrl = "Course.aspx";

    }
    protected void MenuCourseDetails_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Prerequisite":
                Response.Redirect("CoursePrerequisite.aspx?CrsId=" + CourseId.Value );
                break;
            case "CourseRefrence":
                Response.Redirect("CourseRefrences.aspx?CrsId=" + CourseId.Value );
                break;
            case "CourseDetail":
                Response.Redirect("CourseDetails.aspx?CrsId=" + CourseId.Value );
                break;
            case "Group":
                Response.Redirect("CourseGroups.aspx?CrsId=" + CourseId.Value );
                break;
            case "Attachment":
                Response.Redirect("CourseAttachments.aspx?CrsId=" + CourseId.Value);
                break;
        }
    }
    protected void FillForm(int CrsId)
    {
        try
        {
            TSP.DataManager.CourseManager manager = new TSP.DataManager.CourseManager();


            manager.FindByCode(CrsId);
            if (manager.Count == 1)
            {
                txtCourseId.Text = manager[0]["CrsCode"].ToString();
                txtCourseName.Text = manager[0]["CrsName"].ToString();
                txtDuration.Text = manager[0]["Duration"].ToString();
                txtPoint.Text = manager[0]["Point"].ToString();
                txtValidDiuration.Text = manager[0]["ValidDuration"].ToString();
                // cmbType.SelectedIndex = cmbType.Items.IndexOfValue(manager[0]["Type"].ToString());
                txtbPracticalDuration.Text = manager[0]["PracticalDuration"].ToString();
                txtbWorkroomDuration.Text = manager[0]["WorkroomDuration"].ToString();
                txtbNonPracticalDuration.Text = manager[0]["NonPracticalDuration"].ToString();


            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
 
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Course.aspx");

    }
}
