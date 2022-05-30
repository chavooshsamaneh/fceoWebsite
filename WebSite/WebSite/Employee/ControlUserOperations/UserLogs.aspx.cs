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

public partial class Employee_ControlUserOperations_UserLogs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TraceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnExportExcel.Enabled = per.CanView;
            btnExportExcel2.Enabled = per.CanView;
            btnPrint.Enabled = per.CanView;
            btnPrint2.Enabled = per.CanView;
            GridViewUserLogs.Visible = per.CanView;

            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["GridView"] = GridViewUserLogs.Visible;

            GridViewUserLogs.JSProperties["cpPrint"] = "";
        }

        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["GridView"] != null)
            this.GridViewUserLogs.Visible = this.GridViewUserLogs.Visible = (bool)this.ViewState["GridView"];


        string script = @"<SCRIPT language='javascript'>"  ;
       // script += "alert(2); document.getElementById('" + txtDateFrom.ClientID + "').style.minWidth =\"400px\"; alert(3); ";
        script += " function CheckSearch() { var DateFrom=''; DateFrom = document.getElementById('" + txtDateFrom.ClientID + "').value;";
        script += "var Dateto=''; Dateto = document.getElementById('" + txtDateTo.ClientID + "').value;";
        script += "if ( DateFrom == null && DateTo == null  && txtUserName.GetText() == '' ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtUserName.SetText('');
                    document.getElementById('" + txtDateTo.ClientID + "').value = '';";
        script += "document.getElementById('" + txtDateFrom.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "UserLogs";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }

    protected void GridViewUserLogs_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewUserLogs.JSProperties["cpPrint"] = "";

        if (!string.IsNullOrEmpty(e.Parameters))
        {
            if (e.Parameters == "Print")
            {
                GridViewUserLogs.JSProperties["cpPrint"] = 1;

                ArrayList DeletedColumnsName = new ArrayList();
                DeletedColumnsName.Add("TypeId");

                Session["DeletedColumnsName"] = DeletedColumnsName;
                Session["DataTable"] = GridViewUserLogs.Columns;
                Session["DataSource"] = ObjdsTrace;

                Session["Title"] = "کنترل ورود و خروج کاربران";
            }
        }
        else
            GridViewUserLogs.DataBind();
    }

    protected void GridViewUserLogs_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "Date":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "Time":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "Address":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewUserLogs_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "Date":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "Time":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "Address":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txtUserName.Text))
            ObjdsTrace.SelectParameters["UserName"].DefaultValue = txtUserName.Text.Trim();
        else
            ObjdsTrace.SelectParameters["UserName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtDateTo.Text))
            ObjdsTrace.SelectParameters["DateTo"].DefaultValue = txtDateTo.Text.Trim();
        else
            ObjdsTrace.SelectParameters["DateTo"].DefaultValue = "9999/99/99";

        if (!string.IsNullOrEmpty(txtDateFrom.Text))
            ObjdsTrace.SelectParameters["DateFrom"].DefaultValue = txtDateFrom.Text.Trim();
        else
            ObjdsTrace.SelectParameters["DateFrom"].DefaultValue = "9999/99/99";
        GridViewUserLogs.DataBind();

    }
}
