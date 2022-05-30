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

public partial class Institue_InstitueInfo_InstitueActivity : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (string.IsNullOrEmpty(Request.QueryString["InsCId"]))
        {
            Response.Redirect("InstitueCertificates.aspx");
            return;
        }

        if (!IsPostBack)
        {

            HiddenFieldInsActivity["InsCId"] = Request.QueryString["InsCId"].ToString();
            string InsCId = Utility.DecryptQS(HiddenFieldInsActivity["InsCId"].ToString());
            string InsId = Utility.GetCurrentUser_MeId().ToString();
            HiddenFieldInsActivity["InsId"] = Utility.EncryptQS(InsId);
            ObjdsInsActivity.SelectParameters[0].DefaultValue = InsCId;
            ObjdsInsActivity.SelectParameters[1].DefaultValue = InsId;

            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(int.Parse(InsId));
            if (InstitueManager.Count > 0)
            {
                RoundPanelInsActivity.HeaderText = "فعالیت های آموزشی مؤسسه: " + InstitueManager[0]["InsName"].ToString();
            }
            else
            {
                Response.Redirect("InstitueHome.aspx");
                return;
            }

            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission per = TSP.DataManager.InstitueActivityManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewInsActivity.Visible = per.CanView;

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + HiddenFieldInsActivity["InsCId"].ToString());
       
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Facility":
                Response.Redirect("InstituteFacilities.aspx?InsCId=" + HiddenFieldInsActivity["InsCId"].ToString());
                break;
            case "MainInfo":
                Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + HiddenFieldInsActivity["InsCId"].ToString());
                break;
            case "Manager":
                Response.Redirect("InstitueManager.aspx?InsCId=" + HiddenFieldInsActivity["InsCId"].ToString());
                break;
            case "InsTeacher":
                Response.Redirect("InstitueTeacher.aspx?InsCId=" + HiddenFieldInsActivity["InsCId"].ToString());
                break;
        }
    }

    #endregion
}
