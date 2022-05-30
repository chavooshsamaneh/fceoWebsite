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

public partial class Search_OfficeSearch : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ObjectDataSourceOffice.CacheDuration = Utility.GetCacheDuration();
        if (!IsPostBack)
        {
            cmbMFType.DataBind();
            cmbMFType.Items.Insert(0, new ListEditItem("<همه>", null));
            cmbMFType.SelectedIndex = 0;
            comboBoxDocStatus.DataBind();
            comboBoxDocStatus.Items.Insert(0, new ListEditItem("<همه>", null));
            comboBoxDocStatus.SelectedIndex = 0;

            TSP.DataManager.Permission perExport = TSP.DataManager.OfficeManager.GetUserPermissionForExportExcelOfficeSeach(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perPrint = TSP.DataManager.OfficeManager.GetUserPermissionForPrintOfficeSearch(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnExportExcel.Visible = perExport.CanView;
            btnPrint.Visible = perPrint.CanView;
        }

        #region Members_Sender
        if (String.IsNullOrEmpty(txtOfId.Text.Trim()) == false && int.Parse(txtOfId.Text.Trim()) > 0)
            ObjectDataSourceOffice.SelectParameters["OfId"].DefaultValue = txtOfId.Text.Trim();
        else if (!IsPostBack)
            ObjectDataSourceOffice.SelectParameters["OfId"].DefaultValue = "-2";
        else
            ObjectDataSourceOffice.SelectParameters["OfId"].DefaultValue = "-1";

        if (cmbMFType.SelectedIndex > 0)
            ObjectDataSourceOffice.SelectParameters["MFType"].DefaultValue = cmbMFType.Value.ToString();
        ObjectDataSourceOffice.SelectParameters["OfName"].DefaultValue = txtFName.Text.Trim();
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjectDataSourceOffice.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        else
            ObjectDataSourceOffice.SelectParameters["MeId"].DefaultValue = "-1";
        ObjectDataSourceOffice.SelectParameters["FromDate"].DefaultValue = txtFromDate.Text.Trim();
        ObjectDataSourceOffice.SelectParameters["ToDate"].DefaultValue = txtToDate.Text.Trim();
        if (comboBoxDocStatus.SelectedIndex > 0)
            ObjectDataSourceOffice.SelectParameters["DocumentStatus"].DefaultValue = comboBoxDocStatus.Value.ToString();

        GridViewOffice.DataBind();
        #endregion

        String Script = @"
                        function CheckDate()
                        {
                        var StartDate = document.getElementById('"+txtFromDate.ClientID+@"').value;
                        var EndDate = document.getElementById('"+txtToDate.ClientID+@"').value;
                        if(EndDate<StartDate && EndDate!='')
                            return -1;
                        else
                            return 1;
                        }
                        function SetEmpty()
                        {
                        TextMeId.SetText('');
                        TextOfId.SetText('');
                        TextOfName.SetText('');
                        ComboMF.SetSelectedIndex(0);
                        document.getElementById('" + txtFromDate.ClientID+@"').value='';
                        document.getElementById('"+txtToDate.ClientID+@"').value='';
                        }";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", Script, true);


        Session["DataTable"] = GridViewOffice.Columns;
        Session["DataSource"] = ObjectDataSourceOffice;
        Session["Title"] = "ليست اشخاص حقوقي";
    }

    protected void cbSelectAll_Init(object sender, EventArgs e)
    {
        ASPxCheckBox chk = sender as ASPxCheckBox;
        ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
    }

    protected void GridViewOffice_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (String.IsNullOrEmpty(e.Parameters) == false)
        {
            String[] Param = e.Parameters.Split(';');
            if (Param.Length > 0)
            {
                if (Param[0] == "MultiSelect")
                    ((ASPxGridView)sender).Columns[0].Visible = Boolean.Parse(Param[1]);
            }
        }
    }

    protected void GridViewOffice_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;

        Int32 start = grid.VisibleStartIndex;
        Int32 end = grid.VisibleStartIndex + grid.SettingsPager.PageSize;
        Int32 selectNumbers = 0;
        end = (end > grid.VisibleRowCount ? grid.VisibleRowCount : end);

        for (int i = start; i < end; i++)
            if (grid.Selection.IsRowSelected(i))
                selectNumbers++;

        e.Properties["cpSelectedRowsOnPage"] = selectNumbers;
        e.Properties["cpVisibleRowCount"] = grid.VisibleRowCount;
    }

    protected void GridViewOffice_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if ( e.Column.FieldName == "MeNo" || e.Column.FieldName == "CreateDate" || e.Column.FieldName == "FileNo" || e.Column.FieldName == "FileDate" || e.Column.FieldName == "RegDate")
            e.Editor.Style["direction"] = "ltr";

    }

    protected void GridViewOffice_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "MeNo" || e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "FileNo" || e.DataColumn.FieldName == "FileDate" || e.DataColumn.FieldName == "RegDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Member";
        GridViewExporter.WriteXlsToResponse(true);
    }
    #endregion
}
