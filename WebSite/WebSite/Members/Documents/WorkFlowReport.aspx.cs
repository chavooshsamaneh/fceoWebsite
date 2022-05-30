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

public partial class Members_Documents_WorkFlowReport : System.Web.UI.Page
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
        Response.Redirect("~/Members/Documents/MemberFiles.aspx");
    }
    #endregion
}
