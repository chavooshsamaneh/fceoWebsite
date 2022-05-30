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

public partial class Printdt : System.Web.UI.Page
{
    DataTable objsrc = null;
    DevExpress.Web.ASPxGridView Gv = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblPrintDate.Text = "تاریخ گزارش :" + Utility.GetDateOfToday();
        if (Session["DataSource"] == null)
            return;
        if (Session["DataTable"] == null)
            return;        
        objsrc = ((DataTable)Session["DataSource"]);

        if (Gv == null)
        {
            Gv = ((DevExpress.Web.GridViewColumnCollection)(Session["DataTable"])).Grid;
            Gv.SettingsLoadingPanel.Mode = DevExpress.Web.GridViewLoadingPanelMode.Disabled;
            Gv.Settings.ShowFilterBar = DevExpress.Web.GridViewStatusBarMode.Hidden;
            if (Session["DeletedColumnsName"] != null)
            {
                ArrayList DeletedColumnsName = ((ArrayList)Session["DeletedColumnsName"]);
                for (int i = 0; i < DeletedColumnsName.Count; i++)
                {
                    for (int j = 0; j < Gv.Columns.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(DeletedColumnsName[i].ToString()))
                        {
                            if (Gv.Columns[j].Name == DeletedColumnsName[i].ToString())
                                Gv.Columns.RemoveAt(Gv.Columns[DeletedColumnsName[i].ToString()].Index);
                        }
                    }
                }
            }
            Gv.SettingsDetail.ShowDetailButtons = false;
            this.form1.Controls.Add(Gv);
            Gv.DataSource = objsrc;
        }
        else
            Gv.DataSource = objsrc;
        Gv.DataBind();
        SetGridProperties();
        ClearClientSideEvents(ref Gv);
        this.Title = Session["Title"].ToString();
        LabelTitle.Text = Session["Title"].ToString();
        if (Session["Header"] != null)
            LabelHeader.Text = Session["Header"].ToString();
        else
            LabelHeader.Text = "";
        tbl1.Rows[1].Cells[0].Controls.Add(Gv);
    }
    private void SetGridProperties()
    {
        //Gv.Width = Unit.Percentage(100);
        //Gv.SettingsPager.PageSize = 25;        
        Gv.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        Gv.Settings.ShowFilterRow = false;
        Gv.Settings.ShowHorizontalScrollBar = false;
        Gv.Settings.ShowGroupPanel = false;
        Gv.SettingsBehavior.AllowGroup = false;
        Gv.SettingsBehavior.AllowDragDrop = false;
        Gv.SettingsBehavior.AllowFocusedRow = false;
        Gv.SettingsBehavior.AllowSort = false;
        Gv.SettingsBehavior.ColumnResizeMode = DevExpress.Web.ColumnResizeMode.Control;

        ASPxRoundPanel1.Width = Gv.Width;
    }

    void ClearClientSideEvents(ref DevExpress.Web.ASPxGridView Grid)
    {
        Grid.ClientSideEvents.BeginCallback = "";
        Grid.ClientSideEvents.CallbackError = "";
        Grid.ClientSideEvents.ColumnGrouping = "";
        Grid.ClientSideEvents.ColumnMoving = "";
        Grid.ClientSideEvents.ColumnResized = "";
        Grid.ClientSideEvents.ColumnResizing = "";
        Grid.ClientSideEvents.ColumnSorting = "";
        Grid.ClientSideEvents.ColumnStartDragging = "";
        Grid.ClientSideEvents.ContextMenu = "";
        Grid.ClientSideEvents.CustomButtonClick = "";
        Grid.ClientSideEvents.CustomizationWindowCloseUp = "";
        Grid.ClientSideEvents.DetailRowCollapsing = "";
        Grid.ClientSideEvents.DetailRowExpanding = "";
        Grid.ClientSideEvents.EndCallback = "";
        Grid.ClientSideEvents.FocusedRowChanged = "";
        Grid.ClientSideEvents.Init = "";
        Grid.ClientSideEvents.RowClick = "";
        Grid.ClientSideEvents.RowCollapsing = "";
        Grid.ClientSideEvents.RowDblClick = "";
        Grid.ClientSideEvents.RowExpanding = "";
        Grid.ClientSideEvents.SelectionChanged = "";
    }
}
