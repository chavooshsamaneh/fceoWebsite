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
public partial class Employee_WorkFlow_WorkFlowReport : System.Web.UI.Page
{
    int _WfCode
    {
        get
        {
            return Convert.ToInt32(HiddenFieldWFState["WorkFlowCode"]);
        }
        set
        {
            HiddenFieldWFState["WorkFlowCode"] = value;
        }
    }
    int _TableId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldWFState["TableId"]);
        }
        set
        {
            HiddenFieldWFState["TableId"] = value;
        }
    }

    int _TableType
    {
        get
        {
            return Convert.ToInt32(HiddenFieldWFState["TableType"]);
        }
        set
        {
            HiddenFieldWFState["TableType"] = value;
        }
    }

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
                HiddenFieldWFState["PageRefrence"] = Utility.EncryptQS(Request.UrlReferrer.ToString());
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
                _WfCode = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["WorkFlowCode"]));
            }
            else
            {
                _WfCode = -1;
            }
            _TableType = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["TblType"]));
            _TableId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["TblId"]));
        }
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt;
        switch (_WfCode)
        {
            case (int)TSP.DataManager.WorkFlows.TSPlansConfirming:
                dt = WorkFlowStateManager.SelectWorkFlowStateReportForTSPlansConfirming(_TableId, _WfCode);
                break;
            case (int)TSP.DataManager.WorkFlows.TSWorkRequestConfirming:

                TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateObserverWorkManager = new TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager();
                dt = WorkFlowStateObserverWorkManager.SelectWorkFlowStateReportForTSWorkRequest(_TableId, _WfCode);
                break;
            default:
                dt = WorkFlowStateManager.SelectStateReportsById(_TableId, _TableType, _WfCode, -1);
                break;
        }
        GridViewWFReport.DataSource = dt;
        GridViewWFReport.DataBind();

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
        if (HiddenFieldWFState["PageRefrence"] != null)
            Response.Redirect(Utility.DecryptQS(HiddenFieldWFState["PageRefrence"].ToString()));
    }
    #endregion
}
