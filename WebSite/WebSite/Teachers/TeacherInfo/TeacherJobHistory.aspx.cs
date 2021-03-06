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
using DevExpress.Web;

public partial class Teachers_TeacherInfo_TeacherJobHistory : System.Web.UI.Page
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
        string TcId =Utility.DecryptQS(HiddenFieldTeCertificate["TcId"].ToString());
        ObjdsTeacherJobHistory.SelectParameters[0].DefaultValue = TeId.ToString();
        ObjdsTeacherJobHistory.SelectParameters[1].DefaultValue = TcId;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeacherBasicInfo.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
       
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
            case "Atachment":
                Response.Redirect("TeacherAttachment.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
                break;
            case "Course":
                Response.Redirect("TeacherCourse.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
                break;
            case "BasicInfo":
                Response.Redirect("TeacherBasicInfo.aspx?TcId=" + HiddenFieldTeCertificate["TcId"].ToString());
                break;
        }
    }
    protected void Grdv_JobHistory_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";


    }

    protected void Grdv_JobHistory_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate")
            e.Editor.Style["direction"] = "ltr";
    }
}
