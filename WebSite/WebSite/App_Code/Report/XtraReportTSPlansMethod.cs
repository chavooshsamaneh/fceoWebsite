using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTSPlansMethod
/// </summary>
public class XtraReportTSPlansMethod : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel12;
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
    private XRLabel xrLabel2;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTCommercialLimitation;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTAllowableHeight;
    private XRTableCell xrTableCell13;
    private XRLabel xrLabel3;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTBlockNum;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTStructureBuiltPlaceId;
    private XRTableCell xrTableCell17;
    private XRLabel xrLabel4;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTMantelet;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTLocationCriterion;
    private XRTableCell xrTableCell21;
    private XRLabel xrLabel5;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSPlansMethod(int ProjectId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        PlansMethodManager.FindConfirmedByProjectId(ProjectId);

        xrTPlansMethodNo.DataBindings.Add("Text", PlansMethodManager.DataTable, "PlansMethodNo");
        xrTRegisteredDate.DataBindings.Add("Text", PlansMethodManager.DataTable, "RegisteredDate");
        xrTTarakom.DataBindings.Add("Text", PlansMethodManager.DataTable, "Tarakom");
        xrTEshghalSurface.DataBindings.Add("Text", PlansMethodManager.DataTable, "EshghalSurface");
        xrTAllowableHeight.DataBindings.Add("Text", PlansMethodManager.DataTable, "AllowableHeight");
        xrTCommercialLimitation.DataBindings.Add("Text", PlansMethodManager.DataTable, "CommercialLimitation");
        xrTStructureBuiltPlaceId.DataBindings.Add("Text", PlansMethodManager.DataTable, "StructureBuiltPlace");
        xrTBlockNum.DataBindings.Add("Text", PlansMethodManager.DataTable, "BlockNum");
        xrTLocationCriterion.DataBindings.Add("Text", PlansMethodManager.DataTable, "LocationCriterion");
        xrTMantelet.DataBindings.Add("Text", PlansMethodManager.DataTable, "Mantelet");

        this.DataSource = PlansMethodManager.DataTable;
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
        string resourceFileName = "XtraReportTSPlansMethod.resx";
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
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTCommercialLimitation = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTAllowableHeight = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTBlockNum = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTStructureBuiltPlaceId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTMantelet = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTLocationCriterion = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 378;
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
        this.xrTable1.Size = new System.Drawing.Size(1849, 345);
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
        this.xrTable4.Location = new System.Drawing.Point(10, 10);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6});
        this.xrTable4.Size = new System.Drawing.Size(1829, 325);
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
        this.xrTRegisteredDate.Weight = 0.955640864141029;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "تاریخ صدور دستور نقشه";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 0.52655277517114674;
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
        this.xrTPlansMethodNo.Weight = 0.97016136291337363;
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
        this.xrTableCell4.Weight = 0.55256571948030064;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel1.Location = new System.Drawing.Point(21, 11);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.Size = new System.Drawing.Size(296, 42);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "شماره فرم دستور نقشه";
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
        this.xrTEshghalSurface.Weight = 0.955640864141029;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "سطح اشغال";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell7.Weight = 0.52655277517114674;
        // 
        // xrTTarakom
        // 
        this.xrTTarakom.Dpi = 254F;
        this.xrTTarakom.Name = "xrTTarakom";
        this.xrTTarakom.StylePriority.UseTextAlignment = false;
        this.xrTTarakom.Text = "xrTTarakom";
        this.xrTTarakom.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTTarakom.Weight = 0.970161362913374;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell9.Weight = 0.55256571948030053;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel2.Location = new System.Drawing.Point(100, 11);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(220, 50);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "تراکم ساختمانی";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
        this.xrTCommercialLimitation.Weight = 0.955640864141029;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "عمق حریم تجاری";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell11.Weight = 0.52655277517114674;
        // 
        // xrTAllowableHeight
        // 
        this.xrTAllowableHeight.Dpi = 254F;
        this.xrTAllowableHeight.Name = "xrTAllowableHeight";
        this.xrTAllowableHeight.StylePriority.UseTextAlignment = false;
        this.xrTAllowableHeight.Text = "xrTAllowableHeight";
        this.xrTAllowableHeight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTAllowableHeight.Weight = 0.970161362913374;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell13.Weight = 0.55256571948030053;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel3.Location = new System.Drawing.Point(79, 8);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.Size = new System.Drawing.Size(241, 50);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "حداکثر ارتفاع مجاز ";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
        this.xrTBlockNum.Weight = 0.955640864141029;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "تعداد بلوک";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell15.Weight = 0.52655277517114674;
        // 
        // xrTStructureBuiltPlaceId
        // 
        this.xrTStructureBuiltPlaceId.Dpi = 254F;
        this.xrTStructureBuiltPlaceId.Name = "xrTStructureBuiltPlaceId";
        this.xrTStructureBuiltPlaceId.StylePriority.UseTextAlignment = false;
        this.xrTStructureBuiltPlaceId.Text = "xrTStructureBuiltPlaceId";
        this.xrTStructureBuiltPlaceId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTStructureBuiltPlaceId.Weight = 0.970161362913374;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell17.Weight = 0.55256571948030053;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel4.Location = new System.Drawing.Point(121, 8);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.Size = new System.Drawing.Size(198, 50);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "محل احداث بنا ";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
        this.xrTMantelet.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTMantelet.Dpi = 254F;
        this.xrTMantelet.Name = "xrTMantelet";
        this.xrTMantelet.StylePriority.UseBorders = false;
        this.xrTMantelet.StylePriority.UseTextAlignment = false;
        this.xrTMantelet.Text = "xrTMantelet";
        this.xrTMantelet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTMantelet.Weight = 0.955640864141029;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "جان پناه";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell19.Weight = 0.52655277517114674;
        // 
        // xrTLocationCriterion
        // 
        this.xrTLocationCriterion.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTLocationCriterion.Dpi = 254F;
        this.xrTLocationCriterion.Name = "xrTLocationCriterion";
        this.xrTLocationCriterion.StylePriority.UseBorders = false;
        this.xrTLocationCriterion.StylePriority.UseTextAlignment = false;
        this.xrTLocationCriterion.Text = "xrTLocationCriterion";
        this.xrTLocationCriterion.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTLocationCriterion.Weight = 0.970161362913374;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseBorders = false;
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell21.Weight = 0.55256571948030053;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel5.Location = new System.Drawing.Point(163, 5);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.Size = new System.Drawing.Size(156, 50);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "ضابطه محل";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 80;
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
        this.xrPanel1.Size = new System.Drawing.Size(1849, 80);
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
            this.xrLabel12});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel12.Location = new System.Drawing.Point(1599, 11);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(220, 56);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "دستور نقشه";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // XtraReportTSPlansMethod
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

