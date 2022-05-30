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

public partial class Teachers_TeacherInfo_WorkFlowReport : System.Web.UI.Page
{
    #region Evetns
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] == null || Session["Login"].ToString() == "0")
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.WorkFlowStateManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewWFReport.Visible = Per.CanView;

            int TableType = (int)TSP.DataManager.TableCodes.Teachers;
            int TableId = Utility.GetCurrentUser_MeId();

            HiddenFieldWFState["TableType"] = Utility.EncryptQS(TableType.ToString());
            HiddenFieldWFState["TableId"] = Utility.EncryptQS(TableId.ToString());
            // string TableType = Utility.DecryptQS(HiddenFieldWFState["TableType"].ToString());
            // string TableId = Utility.DecryptQS(HiddenFieldWFState["TableId"].ToString());
            ObjdsWfReport.SelectParameters[0].DefaultValue = TableId.ToString();
            ObjdsWfReport.SelectParameters[1].DefaultValue = TableType.ToString();
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Teachers/TeacherHome.aspx");
    }

    protected void GridViewWFReport_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewWFReport_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
        {
            e.Cell.Style["direction"] = "ltr";
        }
    }
    #endregion
}
