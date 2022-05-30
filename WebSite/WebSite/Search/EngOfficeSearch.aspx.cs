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

public partial class Search_EngOfficeSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            CmbEngType.DataBind();
            CmbEngType.Items.Insert(0, new ListEditItem("<همه>", null));
            CmbEngType.SelectedIndex = 0;

        }

        if (!Page.IsPostBack)
        {
            drdEngOfficeStatus.DataBind();
            ((ASPxListBox)(drdEngOfficeStatus.FindControl("ListBoxEngOfficeStatus"))).Items.Insert(0, new ListEditItem("<همه>", null));
            ((ASPxListBox)(drdEngOfficeStatus.FindControl("ListBoxEngOfficeStatus"))).Items.FindByValue(((int)TSP.DataManager.EngOfficeConfirmationType.Confirmed).ToString()).Selected = true;
            drdEngOfficeStatus.Text = ((ASPxListBox)(drdEngOfficeStatus.FindControl("ListBoxEngOfficeStatus"))).Items.FindByValue(((int)TSP.DataManager.EngOfficeConfirmationType.Confirmed).ToString()).Text;

            TSP.DataManager.Permission perExport = TSP.DataManager.EngOfficeManager.GetUserPermissionForExportExcelEngOfficeSeach(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perPrint = TSP.DataManager.EngOfficeManager.GetUserPermissionForPrintEngOfficeSearch(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnExportExcel.Visible = perExport.CanView;
            btnPrint.Visible = perPrint.CanView;
        }
        if (IsCallback) CustomAspxDevGridView1.DataBind();

        #region Members_Sender

        if (String.IsNullOrEmpty(txtEngOfId.Text.Trim()) == false && int.Parse(txtEngOfId.Text.Trim()) > 0)
            ObjectDataSource1.SelectParameters["EngOfId"].DefaultValue = txtEngOfId.Text.Trim();
        else if (!IsPostBack)
            ObjectDataSource1.SelectParameters["EngOfId"].DefaultValue = "-2";
        else
            ObjectDataSource1.SelectParameters["EngOfId"].DefaultValue = "-1";

        if (CmbEngType.SelectedIndex > 0)
            ObjectDataSource1.SelectParameters["EOfTId"].DefaultValue = CmbEngType.Value.ToString();
        else ObjectDataSource1.SelectParameters["EOfTId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjectDataSource1.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        else
            ObjectDataSource1.SelectParameters["MeId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtManagerName.Text))
            ObjectDataSource1.SelectParameters["MeName"].DefaultValue = txtManagerName.Text.Trim();
        else
            ObjectDataSource1.SelectParameters["MeName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtName.Text))
            ObjectDataSource1.SelectParameters["EngOffName"].DefaultValue = txtName.Text.Trim();
        else
            ObjectDataSource1.SelectParameters["EngOffName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtTel.Text))
            ObjectDataSource1.SelectParameters["TellNo"].DefaultValue = txtTel.Text.Trim();
        else
            ObjectDataSource1.SelectParameters["TellNo"].DefaultValue = "%";

        string IsConfirmParam = GetSelectedInDxDropDown(drdEngOfficeStatus, "ListBoxEngOfficeStatus");
        if (String.IsNullOrWhiteSpace(IsConfirmParam) == false)
            ObjectDataSource1.SelectParameters["IsConfirm"].DefaultValue = IsConfirmParam;
        else
            ObjectDataSource1.SelectParameters["IsConfirm"].DefaultValue = "(1)";

        if (!string.IsNullOrEmpty(txtFromDate.Text))
            ObjectDataSource1.SelectParameters["FromDate"].DefaultValue = txtFromDate.Text.Trim();
        else
            ObjectDataSource1.SelectParameters["FromDate"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtToDate.Text))
            ObjectDataSource1.SelectParameters["ToDate"].DefaultValue = txtToDate.Text.Trim();
        else
            ObjectDataSource1.SelectParameters["ToDate"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
            ObjectDataSource1.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else ObjectDataSource1.SelectParameters["EndDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtEndDateTo.Text))
            ObjectDataSource1.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else ObjectDataSource1.SelectParameters["EndDateTo"].DefaultValue = "2";

        if (CmbReqType.SelectedIndex != -1)
            ObjectDataSource1.SelectParameters["ReqType"].DefaultValue = CmbReqType.Value.ToString();
        else ObjectDataSource1.SelectParameters["ReqType"].DefaultValue = "-1";


        if (!string.IsNullOrEmpty(txtFirstRegDateFrom.Text))
            ObjectDataSource1.SelectParameters["FirstRegDateFrom"].DefaultValue = txtFirstRegDateFrom.Text;
        else ObjectDataSource1.SelectParameters["FirstRegDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtFirstRegDateTo.Text))
            ObjectDataSource1.SelectParameters["FirstRegDateTo"].DefaultValue = txtFirstRegDateTo.Text;
        else ObjectDataSource1.SelectParameters["FirstRegDateTo"].DefaultValue = "2";


        if (!string.IsNullOrEmpty(txtLastRegDateFrom.Text))
            ObjectDataSource1.SelectParameters["LastRegDateFrom"].DefaultValue = txtLastRegDateFrom.Text;
        else ObjectDataSource1.SelectParameters["LastRegDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtLastRegDateTo.Text))
            ObjectDataSource1.SelectParameters["LastRegDateTo"].DefaultValue = txtLastRegDateTo.Text;
        else ObjectDataSource1.SelectParameters["LastRegDateTo"].DefaultValue = "2";

        CustomAspxDevGridView1.DataBind();

        #endregion


        String Script = @"function CheckDate()
                        {
                            var StartDate = document.getElementById('" + txtFromDate.ClientID + @"').value;
                            var EndDate = document.getElementById('" + txtToDate.ClientID + @"').value;
                            if(EndDate<StartDate && EndDate!='')
                                return -1;
                            else
                                return 1;
                        }
                        function SetEmpty()
                        {
                            TextMeId.SetText('');
                            TextEngOfId.SetText('');
                            txtManagerName.SetText('');
                            txtTel.SetText('');
                            txtName.SetText('');
                            ComboEngType.SetSelectedIndex(0);
                            CmbReqType.SetSelectedIndex(-1);
                            var index=ListBoxEngOfficeStatus.FindItemByValue(" + (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed + @").index;
                            ListBoxEngOfficeStatus.SelectIndices(index);
                            UpdateText(ListBoxEngOfficeStatus,drdEngOfficeStatus,0);
                            document.getElementById('" + txtFromDate.ClientID + @"').value='';
                            document.getElementById('" + txtToDate.ClientID + @"').value='';
                            document.getElementById('" + txtEndDateFrom.ClientID + @"').value='';
                            document.getElementById('" + txtEndDateTo.ClientID + @"').value='';

                            document.getElementById('" + txtFirstRegDateFrom.ClientID + @"').value='';
                            document.getElementById('" + txtFirstRegDateTo.ClientID + @"').value='';
                            document.getElementById('" + txtLastRegDateFrom.ClientID + @"').value='';
                            document.getElementById('" + txtLastRegDateTo.ClientID + @"').value='';
                        }";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", Script, true);


        Session["DataTable"] = CustomAspxDevGridView1.Columns;
        Session["DataSource"] = ObjectDataSource1;
        Session["Title"] = "ليست دفاتر";
    }
    protected void cbSelectAll_Init(object sender, EventArgs e)
    {
        ASPxCheckBox chk = sender as ASPxCheckBox;
        ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
    }
    protected void CustomAspxDevGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
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
    protected void CustomAspxDevGridView1_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
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


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Member";
        GridViewExporter.WriteXlsToResponse(true);
    }

    String GetSelectedInDxDropDown(ASPxDropDownEdit DropDown, String ListBoxName)
    {
        string Param = "(";
        bool flag = false;

        ASPxListBox ListBox = (ASPxListBox)DropDown.FindControl(ListBoxName);
        if (ListBox == null)
            return "";

        for (int i = 0; i < ListBox.SelectedItems.Count; i++)
        {
            if (ListBox.SelectedItems[i].Value != null)
            {
                if (Param != "(")
                    Param += "," + ListBox.SelectedItems[i].Value.ToString();
                else
                    Param += ListBox.SelectedItems[i].Value.ToString();
                flag = true;
            }
        }

        if (flag)
        {
            Param += ")";
            return Param;
        }
        return "";
    }
}
