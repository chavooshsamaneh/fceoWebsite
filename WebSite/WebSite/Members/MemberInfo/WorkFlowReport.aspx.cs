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

public partial class Members_MemberInfo_WorkFlowReport : System.Web.UI.Page
{
    #region Evetns
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

        if (string.IsNullOrEmpty(Request.QueryString["TblType"]) && string.IsNullOrEmpty(Request.QueryString["TblId"]))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        if (!IsPostBack)
        {
            //TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowStateManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)int.Parse(Session["LoginType"].ToString()));
            //GridViewWFReport.Visible = Per.CanView;

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
        Response.Redirect("~/Members/MemberInfo/MemberRequest.aspx");
    }

    protected void GridViewWFReport_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void GridViewWFReport_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";
    }
    #endregion
}
