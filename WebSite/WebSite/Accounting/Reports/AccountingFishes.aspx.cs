using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_Reports_AccountingFishes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermissionForAccountingFish(Utility.GetCurrentUser_UserId(),
                (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnPrint2.Enabled = btnPrint.Enabled = per.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }

        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];


        string script = @" function CheckSearch() { var txtFromDate = document.getElementById('" + txtFromDate.ClientID + "').value;";
        script += "var txtToDate = document.getElementById('" + txtToDate.ClientID + "').value;";

        script += "if (txtNumber.GetText()=='' && txtFromDate=='' && txtToDate=='' && txtAmount.GetText() == '' &&  cmbType.GetSelectedIndex() ==-1 ) return 0; else return 1;  }";
        script += @"function ClearSearch() {
        txtNumber.SetText('');       
        txtAmount.SetText('');
        cmbType.SetSelectedIndex(-1);
        document.getElementById('" + txtFromDate.ClientID + @"').value='';
        document.getElementById('" + txtToDate.ClientID + @"').value='';}";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script, true);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtNumber.Text = string.Empty;
        cmbType.SelectedIndex = -1;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        objdsAccounting.SelectParameters["Number"].DefaultValue = "%";
        objdsAccounting.SelectParameters["Type"].DefaultValue = "-1";
        objdsAccounting.SelectParameters["FromDate"].DefaultValue = "1";
        objdsAccounting.SelectParameters["ToDate"].DefaultValue = "2";
        GridViewFish.DataBind();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "AccountingFishReport";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void GridViewFish_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":

                    GridViewFish.JSProperties["cpPrint"] = 1;

                    Session["DataTable"] = GridViewFish.Columns;
                    Session["DataSource"] = objdsAccounting;

                    Session["Title"] = "گزارش خزانه";
                    break;
                    //case "search":
                    //    Search();
                    //    break;
                    //case "clear":
                    //    Clear();
                    //    break;
            }
        }
    }

    void Search()
    {
        if (!string.IsNullOrEmpty(txtNumber.Text))
            objdsAccounting.SelectParameters["Number"].DefaultValue = txtNumber.Text.Trim();
        else
            objdsAccounting.SelectParameters["Number"].DefaultValue = "%";

        if (cmbType.SelectedIndex != -1)
            objdsAccounting.SelectParameters["Type"].DefaultValue = cmbType.Value.ToString();
        else
            objdsAccounting.SelectParameters["Type"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtFromDate.Text))
            objdsAccounting.SelectParameters["FromDate"].DefaultValue = txtFromDate.Text.Trim();
        else
            objdsAccounting.SelectParameters["FromDate"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtToDate.Text))
            objdsAccounting.SelectParameters["ToDate"].DefaultValue = txtToDate.Text.Trim();
        else
            objdsAccounting.SelectParameters["ToDate"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtAmount.Text))
            objdsAccounting.SelectParameters["Amount"].DefaultValue = txtAmount.Text.Trim();
        else
            objdsAccounting.SelectParameters["Amount"].DefaultValue = "%";
        GridViewFish.DataBind();
    }

}