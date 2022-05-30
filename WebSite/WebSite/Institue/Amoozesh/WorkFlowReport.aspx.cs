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

public partial class Institue_Amoozesh_WorkFlowReport : System.Web.UI.Page
{
    #region Evetns
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["TblType"]) && string.IsNullOrEmpty(Request.QueryString["TblId"]))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowStateManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewWFReport.Visible = Per.CanView;

            HiddenFieldWFState["TableType"] = Request.QueryString["TblType"];
            HiddenFieldWFState["TableId"] = Request.QueryString["TblId"];
            string TableType = Utility.DecryptQS(HiddenFieldWFState["TableType"].ToString());
            string TableId = Utility.DecryptQS(HiddenFieldWFState["TableId"].ToString());
            ObjdsWfReport.SelectParameters[0].DefaultValue = TableId;
            ObjdsWfReport.SelectParameters[1].DefaultValue = TableType;
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["PgName"]))
        {
            string PgName = Utility.DecryptQS(Request.QueryString["PgName"]);
            Response.Redirect("~/Institue/Amoozesh/"+PgName);
        }
        else
            Response.Redirect("~/Institue/Amoozesh/InstitueHome.aspx");//?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString()));
    }
    #endregion
}
