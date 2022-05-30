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

public partial class Print : System.Web.UI.Page
{
    ObjectDataSource objsrc = null;

    ObjectDataSource objsrcdetail = null;
    DevExpress.Web.ASPxGridView Gv = null;

    DevExpress.Web.ASPxGridView GvDetails = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblPrintDate.Text = "تاریخ گزارش :" + Utility.GetDateOfToday();
        objsrc = ((ObjectDataSource)Session["DataSource"]);

        this.Controls.Add(objsrc);


        if (Gv == null)
        {
            //********************DetailRow*************************
            if (Session["GridDetailName"] != null)
            {
                objsrcdetail = ((ObjectDataSource)Session["DataSourceDetail"]);
                this.Controls.Add(objsrcdetail);

                if (GvDetails == null)
                {
                    GvDetails = ((DevExpress.Web.GridViewColumnCollection)(Session["DataTableDetail"])).Grid;
                    GvDetails.SettingsLoadingPanel.Mode = DevExpress.Web.GridViewLoadingPanelMode.Disabled;
                    GvDetails.ID = Session["GridDetailName"].ToString();
                    GvDetails.SettingsDetail.IsDetailGrid = true;
                    GvDetails.Settings.ShowFilterBar = DevExpress.Web.GridViewStatusBarMode.Hidden;
                    
                    if (Session["DeletedDetailColumnsName"] != null)
                    {
                        ArrayList DeletedDetailColumnsName = ((ArrayList)Session["DeletedDetailColumnsName"]);
                        for (int i = 0; i < DeletedDetailColumnsName.Count; i++)
                        {
                            for (int j = 0; j < GvDetails.Columns.Count; j++)
                            {
                                if (!string.IsNullOrEmpty(DeletedDetailColumnsName[i].ToString()))
                                {
                                    if (GvDetails.Columns[j].Name == DeletedDetailColumnsName[i].ToString())
                                        GvDetails.Columns.RemoveAt(GvDetails.Columns[DeletedDetailColumnsName[i].ToString()].Index);
                                }
                            }
                        }
                      //  GvDetails.Columns.RemoveAt(10);
                    }

                    GvDetails.DataSourceID = objsrcdetail.ID;
                }
            }
            //*****************************************************************


            Gv = ((DevExpress.Web.GridViewColumnCollection)(Session["DataTable"])).Grid;
           
            //this.form1.Controls.Add(Gv);
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

            if (GvDetails != null)
                Gv.TemplateControl.Controls.Add(GvDetails);

            this.DivContent.Controls.Add(Gv);
            Gv.DataSourceID = objsrc.ID;
            
        }
        else
            Gv.DataSourceID = objsrc.ID;


        Gv.DataBind();
        SetGridProperties();
        ClearClientSideEvents(ref Gv);
        this.Title = Session["Title"].ToString();
        LabelTitle.Text = Session["Title"].ToString();
        if (Session["Header"] != null)
            LabelHeader.Text = Session["Header"].ToString();
        else
            LabelHeader.Text = "";
        TablMain.Rows[1].Cells[0].Controls.Add(Gv);
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


    //protected void GridViewDetail_BeforePerformDataSelect(object sender, EventArgs e)
    //{
    //    //int ObsDocId = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
    //    //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    //DocMemberFileManager.ClearBeforeFill = true;
    //    //DocMemberFileManager.FindByCode(ObsDocId, 2);
    //    //if (DocMemberFileManager.Count == 1)
    //    //{
    //    //    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeId"]))
    //    //    {
    //    //        int MfId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
    //    //        DocMemberFileManager.FindByCode(MfId, 0);
    //    //        if (DocMemberFileManager.Count == 1)
    //    //        {
    //    //            Session["MeId"] = DocMemberFileManager[0]["MeId"].ToString();
    //    //        }
    //    //    }
    //    //}
    //    //int Index = GridViewMemberFile.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
    //    //GridViewMemberFile.FocusedRowIndex = Index;
    //}
}
