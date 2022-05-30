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

public partial class Teachers_TeacherInfo_TeacherAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (Request.QueryString["TcId"] == null)
        {
            Response.Redirect("TeacherCertificate.aspx");
        }
        else
        {
            HiddenFieldTeCertificate["TcId"] = Request.QueryString["TcId"].ToString();
        }

        int TeId = Utility.GetCurrentUser_MeId();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        GridViewAttachment.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Teachers, TeId);
        GridViewAttachment.DataBind();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeacherBasicInfo.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
       
    }
    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text =System.IO.Path.GetFileName(hp.Value.ToString());
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Madrak":
                Response.Redirect("TeacherLicence.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
                break;
            case "Research":
                Response.Redirect("TeacherResearchAct.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
                break;
            case "Job":
                Response.Redirect("TeacherJobHistory.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
                break;           
            case "Course":
                Response.Redirect("TeacherCourse.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
                break;
            case "BasicInfo":
                Response.Redirect("TeacherBasicInfo.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
                break;
        }
    }
}
