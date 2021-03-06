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

public partial class Office_WorkFlowReport : System.Web.UI.Page
{
    #region Evetns
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrEmpty(Request.QueryString["TblType"]) && string.IsNullOrEmpty(Request.QueryString["TblId"]))
    //    {
    //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //        return;
    //    }

    //    if (!IsPostBack)
    //    {
    //        //TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowStateManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)int.Parse(Session["LoginType"].ToString()));
    //        //GridViewWFReport.Visible = Per.CanView;

    //        HiddenFieldWFState["TableType"] = Request.QueryString["TblType"];
    //        HiddenFieldWFState["TableId"] = Request.QueryString["TblId"];
    //        string TableType = Utility.DecryptQS(HiddenFieldWFState["TableType"].ToString());
    //        string TableId = Utility.DecryptQS(HiddenFieldWFState["TableId"].ToString());
    //        ObjdsWfReport.SelectParameters[0].DefaultValue = TableId;
    //        ObjdsWfReport.SelectParameters[1].DefaultValue = TableType;
    //    }

    //    this.DivReport.Visible = false;
    //    this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
    //    this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["TblType"]) && string.IsNullOrEmpty(Request.QueryString["TblId"]))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
            {
                HiddenFieldWFState["PageRefrence"] = Request.UrlReferrer.ToString();
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowStateManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewWFReport.Visible = Per.CanView;
            if (!string.IsNullOrEmpty(Request.QueryString["WorkFlowCode"]))
            {
                HiddenFieldWFState["WorkFlowCode"] = Request.QueryString["WorkFlowCode"];
            }
            else
            {
                HiddenFieldWFState["WorkFlowCode"] = Utility.EncryptQS("-1");
            }
            HiddenFieldWFState["TableType"] = Request.QueryString["TblType"];
            HiddenFieldWFState["TableId"] = Request.QueryString["TblId"];
            HiddenFieldWFState["TableType"] = Request.QueryString["TblType"];
            string TableType = Utility.DecryptQS(HiddenFieldWFState["TableType"].ToString());
            string TableId = Utility.DecryptQS(HiddenFieldWFState["TableId"].ToString());
            string WorkFlowCode = Utility.DecryptQS(HiddenFieldWFState["WorkFlowCode"].ToString());
            ObjdsWfReport.SelectParameters["TableId"].DefaultValue = TableId;
            ObjdsWfReport.SelectParameters["TableType"].DefaultValue = TableType;
            ObjdsWfReport.SelectParameters["WfCode"].DefaultValue = WorkFlowCode;
            GridViewWFReport.DataBind();
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Office/OfficeRequest.aspx");
        if (HiddenFieldWFState["PageRefrence"] != null)
            Response.Redirect(HiddenFieldWFState["PageRefrence"].ToString());
    }
    #endregion
}
