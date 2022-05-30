using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTSProject
/// </summary>
public class XtraReportTSProject : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private PageHeaderBand PageHeader;

    TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
   // TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel10;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTRegisteredDate;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTPlansMethodNo;
    private XRTableCell xrTableCell4;
    private XRLabel xrLabel1;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTEshghalSurface;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTTarakom;
    private XRTableCell xrTableCell9;
    private XRLabel xrLabel12;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTCommercialLimitation;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTAllowableHeight;
    private XRTableCell xrTableCell13;
    private XRLabel xrLabel2;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTBlockNum;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTStructureBuiltPlaceId;
    private XRTableCell xrTableCell17;
    private XRLabel xrLabel3;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTMantelet;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTLocationCriterion;
    private XRTableCell xrTableCell21;
    private XRLabel xrLabel4;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTGroupId;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTUsageId;
    private XRTableCell xrTableCell25;
    private XRLabel xrLabel5;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTArea;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTDocumentArea;
    private XRTableCell xrTableCell29;
    private XRLabel xrLabel6;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTRemainArea;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTRecessArea;
    private XRTableCell xrTableCell33;
    private XRLabel xrLabel7;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTFoundation;
    private XRTableCell xrTableCell37;
    private XRLabel xrLabel8;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTAddress;
    private XRTableCell xrTableCell41;
    private XRLabel xrLabel9;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSProject(int ProjectId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        ProjectManager.FindByProjectId(ProjectId);

        this.DataSource = ProjectManager.DataTable;

        xrTPlansMethodNo.DataBindings.Add("Text", ProjectManager.DataTable, "ProjectId");
        xrTRegisteredDate.DataBindings.Add("Text", ProjectManager.DataTable, "Date");
        xrTEshghalSurface.DataBindings.Add("Text", ProjectManager.DataTable, "ProjectStatus");
        xrTTarakom.DataBindings.Add("Text", ProjectManager.DataTable, "ProjectName");
        xrTCommercialLimitation.DataBindings.Add("Text", ProjectManager.DataTable, "FileDate");
        xrTAllowableHeight.DataBindings.Add("Text", ProjectManager.DataTable, "FileNo");
        xrTBlockNum.DataBindings.Add("Text", ProjectManager.DataTable, "MunName");
        xrTStructureBuiltPlaceId.DataBindings.Add("Text", ProjectManager.DataTable, "CitName");
        xrTMantelet.DataBindings.Add("Text", ProjectManager.DataTable, "ComputerCode");
        xrTLocationCriterion.DataBindings.Add("Text", ProjectManager.DataTable, "ReconstructionCode");
        xrTGroupId.DataBindings.Add("Text", ProjectManager.DataTable, "GroupName");
        xrTUsageId.DataBindings.Add("Text", ProjectManager.DataTable, "Usage");
        xrTArea.DataBindings.Add("Text", ProjectManager.DataTable, "Area");
        xrTDocumentArea.DataBindings.Add("Text", ProjectManager.DataTable, "DocumentArea");
        xrTRemainArea.DataBindings.Add("Text", ProjectManager.DataTable, "RemainArea");
        xrTRecessArea.DataBindings.Add("Text", ProjectManager.DataTable, "RecessArea");
        xrTFoundation.DataBindings.Add("Text", ProjectManager.DataTable, "Foundation");
        xrTAddress.DataBindings.Add("Text", ProjectManager.DataTable, "Address");

        //BlockManager.FindByProjectId(Convert.ToInt32(ProjectId));
        //if (BlockManager.Count < 0)
        //{
        //    xrTGroupId.Text = "";
        //}
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "XtraReportTSProject.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRegisteredDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTPlansMethodNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTEshghalSurface = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTarakom = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTCommercialLimitation = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTAllowableHeight = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTBlockNum = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTStructureBuiltPlaceId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTMantelet = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTLocationCriterion = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTGroupId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTUsageId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTArea = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDocumentArea = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRemainArea = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTRecessArea = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFoundation = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTAddress = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 688;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Location = new System.Drawing.Point(0, 0);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.Size = new System.Drawing.Size(1849, 654);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.BorderWidth = 2;
        this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseBorderWidth = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell1.Weight = 3;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable4.BorderWidth = 1;
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrTable4.Location = new System.Drawing.Point(10, 0);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow10,
            this.xrTableRow11});
        this.xrTable4.Size = new System.Drawing.Size(1829, 640);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRegisteredDate,
            this.xrTableCell3,
            this.xrTPlansMethodNo,
            this.xrTableCell4});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTRegisteredDate
        // 
        this.xrTRegisteredDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTRegisteredDate.Dpi = 254F;
        this.xrTRegisteredDate.Name = "xrTRegisteredDate";
        this.xrTRegisteredDate.StylePriority.UseBorders = false;
        this.xrTRegisteredDate.StylePriority.UseTextAlignment = false;
        this.xrTRegisteredDate.Text = "xrTRegisteredDate";
        this.xrTRegisteredDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTRegisteredDate.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "تاریخ ثبت";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 0.48219363931217574;
        // 
        // xrTPlansMethodNo
        // 
        this.xrTPlansMethodNo.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTPlansMethodNo.Dpi = 254F;
        this.xrTPlansMethodNo.Name = "xrTPlansMethodNo";
        this.xrTPlansMethodNo.StylePriority.UseBorders = false;
        this.xrTPlansMethodNo.StylePriority.UseTextAlignment = false;
        this.xrTPlansMethodNo.Text = "xrTPlansMethodNo";
        this.xrTPlansMethodNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTPlansMethodNo.Weight = 1.017806360687824;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell4.Weight = 0.50492072170585023;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel1.Location = new System.Drawing.Point(119, 11);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.Size = new System.Drawing.Size(169, 50);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "کد پروژه";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTEshghalSurface,
            this.xrTableCell7,
            this.xrTTarakom,
            this.xrTableCell9});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTEshghalSurface
        // 
        this.xrTEshghalSurface.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTEshghalSurface.Dpi = 254F;
        this.xrTEshghalSurface.Name = "xrTEshghalSurface";
        this.xrTEshghalSurface.StylePriority.UseBorders = false;
        this.xrTEshghalSurface.StylePriority.UseTextAlignment = false;
        this.xrTEshghalSurface.Text = "xrTEshghalSurface";
        this.xrTEshghalSurface.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTEshghalSurface.Weight = 1;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "وضعیت پروژه";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell7.Weight = 0.48219363931217574;
        // 
        // xrTTarakom
        // 
        this.xrTTarakom.Dpi = 254F;
        this.xrTTarakom.Name = "xrTTarakom";
        this.xrTTarakom.StylePriority.UseTextAlignment = false;
        this.xrTTarakom.Text = "xrTTarakom";
        this.xrTTarakom.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTTarakom.Weight = 1.0178063606878243;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell9.Weight = 0.50492072170585023;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel12.Location = new System.Drawing.Point(127, 0);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(169, 50);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "نام پروژه";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCommercialLimitation,
            this.xrTableCell11,
            this.xrTAllowableHeight,
            this.xrTableCell13});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTCommercialLimitation
        // 
        this.xrTCommercialLimitation.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTCommercialLimitation.Dpi = 254F;
        this.xrTCommercialLimitation.Name = "xrTCommercialLimitation";
        this.xrTCommercialLimitation.StylePriority.UseBorders = false;
        this.xrTCommercialLimitation.StylePriority.UseTextAlignment = false;
        this.xrTCommercialLimitation.Text = "xrTCommercialLimitation";
        this.xrTCommercialLimitation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTCommercialLimitation.Weight = 1;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "تاریخ پرونده";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell11.Weight = 0.48219363931217574;
        // 
        // xrTAllowableHeight
        // 
        this.xrTAllowableHeight.Dpi = 254F;
        this.xrTAllowableHeight.Name = "xrTAllowableHeight";
        this.xrTAllowableHeight.StylePriority.UseTextAlignment = false;
        this.xrTAllowableHeight.Text = "xrTAllowableHeight";
        this.xrTAllowableHeight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTAllowableHeight.Weight = 1.0178063606878243;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell13.Weight = 0.50492072170585023;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel2.Location = new System.Drawing.Point(127, 0);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(169, 50);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "شماره پرونده";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTBlockNum,
            this.xrTableCell15,
            this.xrTStructureBuiltPlaceId,
            this.xrTableCell17});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTBlockNum
        // 
        this.xrTBlockNum.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTBlockNum.Dpi = 254F;
        this.xrTBlockNum.Name = "xrTBlockNum";
        this.xrTBlockNum.StylePriority.UseBorders = false;
        this.xrTBlockNum.StylePriority.UseTextAlignment = false;
        this.xrTBlockNum.Text = "xrTBlockNum";
        this.xrTBlockNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTBlockNum.Weight = 1;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "شهرداری";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell15.Weight = 0.48219363931217574;
        // 
        // xrTStructureBuiltPlaceId
        // 
        this.xrTStructureBuiltPlaceId.Dpi = 254F;
        this.xrTStructureBuiltPlaceId.Name = "xrTStructureBuiltPlaceId";
        this.xrTStructureBuiltPlaceId.StylePriority.UseTextAlignment = false;
        this.xrTStructureBuiltPlaceId.Text = "xrTStructureBuiltPlaceId";
        this.xrTStructureBuiltPlaceId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTStructureBuiltPlaceId.Weight = 1.0178063606878243;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell17.Weight = 0.50492072170585023;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel3.Location = new System.Drawing.Point(127, 0);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.Size = new System.Drawing.Size(169, 50);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "شهر";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTMantelet,
            this.xrTableCell19,
            this.xrTLocationCriterion,
            this.xrTableCell21});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTMantelet
        // 
        this.xrTMantelet.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTMantelet.Dpi = 254F;
        this.xrTMantelet.Name = "xrTMantelet";
        this.xrTMantelet.StylePriority.UseBorders = false;
        this.xrTMantelet.StylePriority.UseTextAlignment = false;
        this.xrTMantelet.Text = "xrTMantelet";
        this.xrTMantelet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTMantelet.Weight = 1;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "کد کامپیوتری";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell19.Weight = 0.48219363931217574;
        // 
        // xrTLocationCriterion
        // 
        this.xrTLocationCriterion.Dpi = 254F;
        this.xrTLocationCriterion.Name = "xrTLocationCriterion";
        this.xrTLocationCriterion.StylePriority.UseTextAlignment = false;
        this.xrTLocationCriterion.Text = "xrTLocationCriterion";
        this.xrTLocationCriterion.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTLocationCriterion.Weight = 1.0178063606878243;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseBorders = false;
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell21.Weight = 0.50492072170585023;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel4.Location = new System.Drawing.Point(21, 0);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.Size = new System.Drawing.Size(275, 50);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "کد نوسازی شهرداری";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTGroupId,
            this.xrTableCell23,
            this.xrTUsageId,
            this.xrTableCell25});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTGroupId
        // 
        this.xrTGroupId.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTGroupId.Dpi = 254F;
        this.xrTGroupId.Name = "xrTGroupId";
        this.xrTGroupId.StylePriority.UseBorders = false;
        this.xrTGroupId.StylePriority.UseTextAlignment = false;
        this.xrTGroupId.Text = "xrTGroupId";
        this.xrTGroupId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTGroupId.Weight = 1;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseTextAlignment = false;
        this.xrTableCell23.Text = "گروه ساختمانی";
        this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell23.Weight = 0.48219363931217574;
        // 
        // xrTUsageId
        // 
        this.xrTUsageId.Dpi = 254F;
        this.xrTUsageId.Name = "xrTUsageId";
        this.xrTUsageId.StylePriority.UseTextAlignment = false;
        this.xrTUsageId.Text = "xrTUsageId";
        this.xrTUsageId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTUsageId.Weight = 1.0178063606878243;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell25.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.StylePriority.UseTextAlignment = false;
        this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell25.Weight = 0.50492072170585023;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel5.Location = new System.Drawing.Point(127, 0);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.Size = new System.Drawing.Size(169, 50);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "کاربری";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTArea,
            this.xrTableCell27,
            this.xrTDocumentArea,
            this.xrTableCell29});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTArea
        // 
        this.xrTArea.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTArea.Dpi = 254F;
        this.xrTArea.Name = "xrTArea";
        this.xrTArea.StylePriority.UseBorders = false;
        this.xrTArea.StylePriority.UseTextAlignment = false;
        this.xrTArea.Text = "xrTArea";
        this.xrTArea.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTArea.Weight = 1;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.Text = "مساحت زمین";
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell27.Weight = 0.48219363931217574;
        // 
        // xrTDocumentArea
        // 
        this.xrTDocumentArea.Dpi = 254F;
        this.xrTDocumentArea.Name = "xrTDocumentArea";
        this.xrTDocumentArea.StylePriority.UseTextAlignment = false;
        this.xrTDocumentArea.Text = "xrTDocumentArea";
        this.xrTDocumentArea.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTDocumentArea.Weight = 1.0178063606878243;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell29.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6});
        this.xrTableCell29.Dpi = 254F;
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseBorders = false;
        this.xrTableCell29.StylePriority.UseTextAlignment = false;
        this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell29.Weight = 0.50492072170585023;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel6.Location = new System.Drawing.Point(106, 0);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.Size = new System.Drawing.Size(190, 50);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "مساحت سند";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRemainArea,
            this.xrTableCell31,
            this.xrTRecessArea,
            this.xrTableCell33});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTRemainArea
        // 
        this.xrTRemainArea.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTRemainArea.Dpi = 254F;
        this.xrTRemainArea.Name = "xrTRemainArea";
        this.xrTRemainArea.StylePriority.UseBorders = false;
        this.xrTRemainArea.StylePriority.UseTextAlignment = false;
        this.xrTRemainArea.Text = "xrTRemainArea";
        this.xrTRemainArea.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTRemainArea.Weight = 1;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseTextAlignment = false;
        this.xrTableCell31.Text = "مساحت باقی مانده";
        this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell31.Weight = 0.48219363931217574;
        // 
        // xrTRecessArea
        // 
        this.xrTRecessArea.Dpi = 254F;
        this.xrTRecessArea.Name = "xrTRecessArea";
        this.xrTRecessArea.StylePriority.UseTextAlignment = false;
        this.xrTRecessArea.Text = "xrTRecessArea";
        this.xrTRecessArea.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTRecessArea.Weight = 1.0178063606878243;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell33.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7});
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseBorders = false;
        this.xrTableCell33.StylePriority.UseTextAlignment = false;
        this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell33.Weight = 0.50492072170585023;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel7.Location = new System.Drawing.Point(21, 0);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.Size = new System.Drawing.Size(275, 50);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "مساحت عقب نشینی";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTFoundation,
            this.xrTableCell37});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.StylePriority.UseTextAlignment = false;
        this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell34.Weight = 1;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Dpi = 254F;
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseTextAlignment = false;
        this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell35.Weight = 0.48219363931217574;
        // 
        // xrTFoundation
        // 
        this.xrTFoundation.Dpi = 254F;
        this.xrTFoundation.Name = "xrTFoundation";
        this.xrTFoundation.StylePriority.UseTextAlignment = false;
        this.xrTFoundation.Text = "xrTFoundation";
        this.xrTFoundation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTFoundation.Weight = 1.0178063606878243;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell37.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8});
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.StylePriority.UseBorders = false;
        this.xrTableCell37.StylePriority.UseTextAlignment = false;
        this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell37.Weight = 0.50492072170585023;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel8.Location = new System.Drawing.Point(127, 0);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.Size = new System.Drawing.Size(169, 50);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "زیر بنا";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTAddress,
            this.xrTableCell41});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1;
        // 
        // xrTAddress
        // 
        this.xrTAddress.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTAddress.Dpi = 254F;
        this.xrTAddress.Name = "xrTAddress";
        this.xrTAddress.StylePriority.UseBorders = false;
        this.xrTAddress.StylePriority.UseTextAlignment = false;
        this.xrTAddress.Text = "xrTAddress";
        this.xrTAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTAddress.Weight = 2.5008201202843083;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell41.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9});
        this.xrTableCell41.Dpi = 254F;
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.StylePriority.UseBorders = false;
        this.xrTableCell41.StylePriority.UseTextAlignment = false;
        this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell41.Weight = 0.50410060142154178;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel9.Location = new System.Drawing.Point(127, 0);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.Size = new System.Drawing.Size(169, 50);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "آدرس";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 90;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel1.BorderWidth = 2;
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel5});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.Location = new System.Drawing.Point(0, 0);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(1849, 90);
        this.xrPanel1.StylePriority.UseBorders = false;
        this.xrPanel1.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel5
        // 
        this.xrPanel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel5.BorderWidth = 1;
        this.xrPanel5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel10.Location = new System.Drawing.Point(1598, 11);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.Size = new System.Drawing.Size(212, 56);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseForeColor = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "اطلاعات پایه";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // XtraReportTSProject
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
