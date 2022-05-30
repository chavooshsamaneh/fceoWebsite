using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Document_Reports_MemberFilePrintingHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.PrintingHistoryManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnPrint2.Enabled = btnPrint.Enabled = per.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }
        //this.DivReport.Visible = false;
        //this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        //this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];


        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";
        script += "if ( txtCreateDateFrom=='' && txtCreateDateTo=='' && txtMeId.GetText() == '' && txtPrtSerialNo.GetText()=='' && CmbIsValid.GetSelectedIndex() == 0 && cmbMaxGrade.GetSelectedIndex() == -1  ) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtPrtSerialNo.SetText('');
                    CmbIsValid.SetSelectedIndex(0);  cmbMaxGrade.SetSelectedIndex(-1);
                    document.getElementById('" + txtCreateDateTo.ClientID + "').value = '';";
        script += "document.getElementById('" + txtCreateDateFrom.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "PrintingHistory";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        if (txtCreateDateFrom.Text != "")
            objdsPrintingHistory.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
        else
            objdsPrintingHistory.SelectParameters["CreateDateFrom"].DefaultValue = "9999/99/99";

        if (txtCreateDateTo.Text != "")
            objdsPrintingHistory.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
        else
            objdsPrintingHistory.SelectParameters["CreateDateTo"].DefaultValue = "9999/99/99";

        if (txtMeId.Text != "")
            objdsPrintingHistory.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else
            objdsPrintingHistory.SelectParameters["MeId"].DefaultValue = "-1";

        if (txtPrtSerialNo.Text != "")
            objdsPrintingHistory.SelectParameters["PrtSerialNo"].DefaultValue = txtPrtSerialNo.Text;
        else
            objdsPrintingHistory.SelectParameters["PrtSerialNo"].DefaultValue = "%";

        if (CmbIsValid.SelectedItem != null || CmbIsValid.SelectedIndex != 0)
            objdsPrintingHistory.SelectParameters["IsValid"].DefaultValue = CmbIsValid.Value.ToString();
        else
            objdsPrintingHistory.SelectParameters["IsValid"].DefaultValue = "-1";
        if (cmbMaxGrade.SelectedItem != null || cmbMaxGrade.SelectedIndex != -1)
            objdsPrintingHistory.SelectParameters["MaxGradeId"].DefaultValue = cmbMaxGrade.Value.ToString();
        else
            objdsPrintingHistory.SelectParameters["MaxGradeId"].DefaultValue = "-1";



    }

    protected void GridViewPrintingHistory_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":

                    GridViewPrintingHistory.JSProperties["cpPrint"] = 1;

                    Session["DataTable"] = GridViewPrintingHistory.Columns;
                    Session["DataSource"] = objdsPrintingHistory;

                    Session["Title"] = "گزارش چاپ کارت های پروانه اشتغال به کار";
                    break;
            }
        }
    }
}