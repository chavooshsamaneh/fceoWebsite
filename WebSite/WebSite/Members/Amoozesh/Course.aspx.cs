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
    protected void btnView_Click(object sender, EventArgs e)
    {
        int CrsId = -1;
        if (GridViewCourse.FocusedRowIndex > -1)
        {
            DataRow row = GridViewCourse.GetDataRow(GridViewCourse.FocusedRowIndex);
            CrsId = (int)row["CrsId"];
        }
        if (CrsId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {            
            Response.Redirect("CourseView.aspx?CrsId=" + Utility.EncryptQS(CrsId.ToString()));
        }

    }
}
