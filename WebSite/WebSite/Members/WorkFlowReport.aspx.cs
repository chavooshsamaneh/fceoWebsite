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
public partial class Members_WorkFlowReport : System.Web.UI.Page
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
            if (!string.IsNullOrEmpty(Request.QueryString["UrlReferrer"]))
            {
                HiddenFieldWFState["PageRefrence"] = Request.QueryString["UrlReferrer"];
            }
            else if (Request.UrlReferrer != null)
            {
                HiddenFieldWFState["PageRefrence"] = Request.UrlReferrer.ToString();
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
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

    protected void GridViewWFReport_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewWFReport_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "Date":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewWFReport_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;

        try
        {
            if (!Utility.IsDBNullOrNullValue(e.GetValue("PriorityRate")))
                e.Row.BackColor = TSP.DataManager.Automation.PriorityManager.GetPriorityColor(int.Parse(e.GetValue("PriorityRate").ToString()));
            
        }
        catch (Exception err) { }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if(HiddenFieldWFState["PageRefrence"]!=null)
          Response.Redirect(Utility.DecryptQS(HiddenFieldWFState["PageRefrence"].ToString()));
    }
    #endregion
}
