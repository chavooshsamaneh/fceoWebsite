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

public partial class Institue_InstitueInfo_InstitueManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Request.QueryString["InsCId"]))
        {
            Response.Redirect("InstitueCertificates.aspx");
            return;
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.InstitueManagerManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewInsManager.Visible = per.CanView;
            HiddenFieldInsManager["InsCId"] = Request.QueryString["InsCId"];
            HiddenFieldInsManager["InsId"] =Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
            string InsCId = Utility.DecryptQS(HiddenFieldInsManager["InsCId"].ToString());
            string InsId = Utility.DecryptQS(HiddenFieldInsManager["InsId"].ToString());
            ObjdsInsManager.SelectParameters[0].DefaultValue = InsCId;
            ObjdsInsManager.SelectParameters[1].DefaultValue = InsId;
            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(int.Parse(InsId));
            if (InstitueManager.Count > 0)
            {
                RoundPanelInsManager.HeaderText = "هیئت اجرایی موسسه: " + InstitueManager[0]["InsName"].ToString();
            }
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + HiddenFieldInsManager["InsCId"].ToString());
       
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Facility":
                Response.Redirect("InstituteFacilities.aspx?InsCId=" + HiddenFieldInsManager["InsCId"].ToString());
                break;
            case "BasicInfo":
                Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + HiddenFieldInsManager["InsCId"].ToString());
                break;
            case "Activity":
                Response.Redirect("InstitueActivity.aspx?InsCId=" + HiddenFieldInsManager["InsCId"].ToString());
                break;
            case "InsTeacher":
                Response.Redirect("InstitueTeacher.aspx?InsCId=" + HiddenFieldInsManager["InsCId"].ToString());
                break;
        }
    }

  
}
