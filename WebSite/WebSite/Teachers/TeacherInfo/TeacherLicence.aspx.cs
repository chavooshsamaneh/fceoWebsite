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

public partial class Teachers_TeacherInfo_TeacherLicence : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (Request.QueryString["TcId"] == null)
        {
            Response.Redirect("TeacherCertificate.aspx");
        }
        else
        {
            HiddenFieldTeacherLicence["TcId"] = Request.QueryString["TcId"].ToString();
        }

        int TeId = Utility.GetCurrentUser_MeId();
        ObjdsTeacherLicence.SelectParameters[0].DefaultValue = TeId.ToString();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeacherBasicInfo.aspx?TcId=" + HiddenFieldTeacherLicence["TcId"].ToString());
       
    }

     protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "BasicInfo":
                Response.Redirect("TeacherBasicInfo.aspx?TcId=" + HiddenFieldTeacherLicence["TcId"].ToString());
                break;
            case "Research":
                Response.Redirect("TeacherResearchAct.aspx?TcId=" + HiddenFieldTeacherLicence["TcId"].ToString());
                break;
            case "Job":
                Response.Redirect("TeacherJobHistory.aspx?TcId=" + HiddenFieldTeacherLicence["TcId"].ToString());
                break;
            case "Atachment":
                Response.Redirect("TeacherAttachment.aspx?TcId=" + HiddenFieldTeacherLicence["TcId"].ToString());
                break;          
            case "Course":
                Response.Redirect("TeacherCourse.aspx?TcId=" + HiddenFieldTeacherLicence["TcId"].ToString());
                break;
        }
    }

    protected void Grdv_Licence_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate" )
            e.Cell.Style["direction"] = "ltr";


    }

    protected void Grdv_Licence_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" )
            e.Editor.Style["direction"] = "ltr";
    }
    private void SetObjectDataSource(int TeacherId)
    {
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TeacherManager.FindByCode(TeacherId);
        if (TeacherManager.Count > 0)
        {            
            if (!string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
            {
                HiddenFieldTeacherLicence["IsMember"] = true;
                Grdv_Licence.DataSource = ObjdsMemberLicence;
                Session["TachearLicenceObjdss"] = ObjdsMemberLicence;
                Grdv_Licence.KeyFieldName = "MlId";
                ObjdsMemberLicence.DataBind();
                ObjdsMemberLicence.SelectParameters[0].DefaultValue = TeacherManager[0]["MeId"].ToString();
                Grdv_Licence.DataBind();

            }
            else
            {
                HiddenFieldTeacherLicence["IsMember"] = false;
                Grdv_Licence.DataSource = ObjdsTeacherLicence;
                Grdv_Licence.KeyFieldName = "TLiId";
                ObjdsTeacherLicence.DataBind();
                Session["TachearLicenceObjdss"] = ObjdsTeacherLicence;
                ObjdsTeacherLicence.SelectParameters[0].DefaultValue = TeacherId.ToString();
                Grdv_Licence.DataBind();
            }
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

}
