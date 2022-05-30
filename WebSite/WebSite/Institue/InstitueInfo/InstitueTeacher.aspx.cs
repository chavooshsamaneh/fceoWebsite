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

public partial class Institue_InstitueInfo_InstitueTeacher : System.Web.UI.Page
{

    #region Methods
    protected void Page_Load(object sender, EventArgs e)
    {
       
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
        //    if (string.IsNullOrEmpty(Request.QueryString["InsId"]))
        //    {
        //        Response.Redirect("InstitueHome.aspx");
        //        return;
        //    }
            HiddenFieldInstitueTeacher["InsCId"] = Request.QueryString["InsCId"];
            TSP.DataManager.Permission per = TSP.DataManager.InstitueTeachersManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewInsTeacher.Visible = per.CanView;
            int InsId = Utility.GetCurrentUser_UserId();
            string InsCId = Utility.DecryptQS(HiddenFieldInstitueTeacher["InsCId"].ToString());
            ObjdsTeacherInstitue.SelectParameters[0].DefaultValue = InsCId;
            ObjdsTeacherInstitue.SelectParameters[1].DefaultValue = InsId.ToString();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + HiddenFieldInstitueTeacher["InsCId"].ToString());
       
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Manager":
                Response.Redirect("InstitueManager.aspx?InsCId=" + HiddenFieldInstitueTeacher["InsCId"].ToString());
                break;
            case "BasicInfo":
                Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + HiddenFieldInstitueTeacher["InsCId"].ToString());
                break;
            case "Activity":
                Response.Redirect("InstitueActivity.aspx?InsCId=" + HiddenFieldInstitueTeacher["InsCId"].ToString());
                break;
            case "Facility":
                Response.Redirect("InstituteFacilities.aspx?InsCId=" + HiddenFieldInstitueTeacher["InsCId"].ToString());
                break;
        }
    }
    #endregion

}
