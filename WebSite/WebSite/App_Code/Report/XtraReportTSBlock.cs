using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportTSBlock
/// </summary>
public class XtraReportTSBlock : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
    TSP.DataManager.TechnicalServices.WallsManager WallsManager = new TSP.DataManager.TechnicalServices.WallsManager();
    TSP.DataManager.TechnicalServices.EntranceManager EntranceManager = new TSP.DataManager.TechnicalServices.EntranceManager();
    TSP.DataManager.TechnicalServices.FoundationManager FoundationManager = new TSP.DataManager.TechnicalServices.FoundationManager();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel12;
    private XRTable xrTable2;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTable xrTable3;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTRoofType;
    private XRTableCell xrTStructureSkeleton;
    private XRTableCell xrTStructureSystem;
    private XRTableCell xrTStageNum;
    private XRTableCell xrTFoundation;
    private XRPanel xrPanel3;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private ReportHeaderBand ReportHeader;
    private XRPanel xrPanel2;
    private XRPanel xrPanel6;
    private XRLabel xrLabel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRPanel xrPanel7;
    private XRTable xrTable4;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTHeight;
    private XRTableCell xrTLength;
    private XRTableCell xrTWallMainDirectionsId;
    private ReportFooterBand ReportFooter1;
    private XRPanel xrPanel8;
    private XRPanel xrPanel9;
    private XRPanel xrPanel10;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private ReportFooterBand ReportFooter2;
    private ReportHeaderBand ReportHeader1;
    private XRPanel xrPanel11;
    private XRPanel xrPanel12;
    private XRPanel xrPanel13;
    private XRLabel xrLabel2;
    private XRTable xrTable5;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell6;
    private XRPanel xrPanel14;
    private XRPanel xrPanel15;
    private XRTable xrTable6;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTNum;
    private XRTableCell xrTMainDirectionsId;
    private XRTableCell xrTEntranceTypeId;
    private XRPanel xrPanel17;
    private XRPanel xrPanel18;
    private XRPanel xrPanel19;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private ReportHeaderBand ReportHeader2;
    private ReportFooterBand ReportFooter3;
    private XRPanel xrPanel16;
    private XRPanel xrPanel20;
    private XRPanel xrPanel21;
    private XRLabel xrLabel3;
    private XRTable xrTable7;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRPanel xrPanel22;
    private XRPanel xrPanel23;
    private XRTable xrTable8;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTCloseYard;
    private XRTableCell xrTArea;
    private XRTableCell xrTStageTitle;
    private XRPanel xrPanel24;
    private XRPanel xrPanel25;
    private XRTableCell xrTEshghalSurface;
    private XRTableCell xrTFHeight;
    private XRTableCell xrTUsageId;
    private XRTableCell xrTFlat;
    private XRTableCell xrTSecondaryUsageId;
    private XRTableCell xrTOpenYard;
    private XRTableCell xrTClosePathway;
    private XRTableCell xrTOpenPathway;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSBlock(int ProjectId)
    {
        InitializeComponent();
        //        
        // TODO: Add constructor logic here
        //

        this.DataSourceRowChanged += new DataSourceRowEventHandler(XtraReportTSBlock_DataSourceRowChanged);

        //BlockManager.FindConfirmedByProjectId(ProjectId);
        WallsManager.FindByProjectId(ProjectId);
        EntranceManager.FindByProjectId(ProjectId);
        FoundationManager.FindByProjectId(ProjectId);

        //xrTFoundation.DataBindings.Add("Text", BlockManager.DataTable, "Foundation");
        //xrTStageNum.DataBindings.Add("Text", BlockManager.DataTable, "StageNum");
        //xrTStructureSystem.DataBindings.Add("Text", BlockManager.DataTable, "StructureSystem");
        //xrTStructureSkeleton.DataBindings.Add("Text", BlockManager.DataTable, "StructureSkeleton");
        //xrTRoofType.DataBindings.Add("Text", BlockManager.DataTable, "RoofType");

        //this.DataSource = BlockManager.DataTable;
    }

    void XtraReportTSBlock_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
    {
        DataRowView dr = (DataRowView)this.GetCurrentRow();

        WallsManager.CurrentFilter = "BlockId=" + dr["BlockId"].ToString();
        DetailReport.DataSource = WallsManager.DataTable;
        xrTWallMainDirectionsId.DataBindings.Add("Text", WallsManager.DataTable, "MainDirections");
        xrTLength.DataBindings.Add("Text", WallsManager.DataTable, "Length");
        xrTHeight.DataBindings.Add("Text", WallsManager.DataTable, "Height");

        EntranceManager.CurrentFilter = "BlockId=" + dr["BlockId"].ToString();
        DetailReport1.DataSource = EntranceManager.DataTable;
        xrTEntranceTypeId.DataBindings.Add("Text", EntranceManager.DataTable, "EntranceType");
        xrTMainDirectionsId.DataBindings.Add("Text", EntranceManager.DataTable, "MainDirections");
        xrTNum.DataBindings.Add("Text", EntranceManager.DataTable, "Num");
        
        
        FoundationManager.CurrentFilter = "BlockId=" + dr["BlockId"].ToString();
        DetailReport2.DataSource = FoundationManager.DataTable;       
        xrTStageTitle.DataBindings.Add("Text", FoundationManager.DataTable, "StageTitle");
        xrTEshghalSurface.DataBindings.Add("Text", FoundationManager.DataTable, "EshghalSurface");
        xrTFHeight.DataBindings.Add("Text", FoundationManager.DataTable, "Height");
        xrTArea.DataBindings.Add("Text", FoundationManager.DataTable, "Area");
        xrTFlat.DataBindings.Add("Text", FoundationManager.DataTable, "Flat");
        xrTUsageId.DataBindings.Add("Text", FoundationManager.DataTable, "UsageTitle");
        xrTSecondaryUsageId.DataBindings.Add("Text", FoundationManager.DataTable, "SecondaryUsage");
        xrTCloseYard.DataBindings.Add("Text", FoundationManager.DataTable, "CloseYard");
        xrTClosePathway.DataBindings.Add("Text", FoundationManager.DataTable, "ClosePathway");
        xrTOpenYard.DataBindings.Add("Text", FoundationManager.DataTable, "OpenYard");
        xrTOpenPathway.DataBindings.Add("Text", FoundationManager.DataTable, "OpenPathway");        
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
        string resourceFileName = "XtraReportTSBlock.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRoofType = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTStructureSkeleton = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTStructureSystem = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTStageNum = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFoundation = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel10 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel7 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTHeight = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTLength = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTWallMainDirectionsId = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel9 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel6 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter1 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel17 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel8 = new DevExpress.XtraReports.UI.XRPanel();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel14 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel15 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTNum = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMainDirectionsId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTEntranceTypeId = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel11 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel12 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel13 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter2 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel18 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel19 = new DevExpress.XtraReports.UI.XRPanel();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel22 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel23 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTOpenPathway = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTOpenYard = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTClosePathway = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCloseYard = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTSecondaryUsageId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTUsageId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFlat = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTArea = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFHeight = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTEshghalSurface = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTStageTitle = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader2 = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel16 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel20 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel21 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter3 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel24 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel25 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 180;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel3.BorderWidth = 2;
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrTable2});
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.Location = new System.Drawing.Point(0, 0);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.Size = new System.Drawing.Size(1849, 180);
        this.xrPanel3.StylePriority.UseBorders = false;
        this.xrPanel3.StylePriority.UseBorderWidth = false;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 1;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.Location = new System.Drawing.Point(10, 95);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable3.Size = new System.Drawing.Size(1829, 85);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRoofType,
            this.xrTStructureSkeleton,
            this.xrTStructureSystem,
            this.xrTStageNum,
            this.xrTFoundation});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTRoofType
        // 
        this.xrTRoofType.Dpi = 254F;
        this.xrTRoofType.Name = "xrTRoofType";
        this.xrTRoofType.Text = "xrTRoofType";
        this.xrTRoofType.Weight = 0.94422211170857284;
        // 
        // xrTStructureSkeleton
        // 
        this.xrTStructureSkeleton.Dpi = 254F;
        this.xrTStructureSkeleton.Name = "xrTStructureSkeleton";
        this.xrTStructureSkeleton.Text = "xrTStructureSkeleton";
        this.xrTStructureSkeleton.Weight = 0.88282982806303312;
        // 
        // xrTStructureSystem
        // 
        this.xrTStructureSystem.Dpi = 254F;
        this.xrTStructureSystem.Name = "xrTStructureSystem";
        this.xrTStructureSystem.Text = "xrTStructureSystem";
        this.xrTStructureSystem.Weight = 0.80223816108350343;
        // 
        // xrTStageNum
        // 
        this.xrTStageNum.Dpi = 254F;
        this.xrTStageNum.Name = "xrTStageNum";
        this.xrTStageNum.Text = "xrTStageNum";
        this.xrTStageNum.Weight = 0.40125905400073325;
        // 
        // xrTFoundation
        // 
        this.xrTFoundation.Dpi = 254F;
        this.xrTFoundation.Name = "xrTFoundation";
        this.xrTFoundation.Text = "xrTFoundation";
        this.xrTFoundation.Weight = 0.43580826258763788;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.Location = new System.Drawing.Point(10, 10);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable2.Size = new System.Drawing.Size(1829, 85);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.Text = "نوع سقف";
        this.xrTableCell27.Weight = 1.142649268834151;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Text = "اسکلت ساختمان";
        this.xrTableCell31.Weight = 1.068957151474953;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = "سیستم سازه ای";
        this.xrTableCell32.Weight = 0.97152078172659162;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.Text = "تعداد طبقات";
        this.xrTableCell33.Weight = 0.48692750461484835;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.Text = "زیر بنا";
        this.xrTableCell34.Weight = 0.52673593816061581;
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
        this.xrLabel12.Location = new System.Drawing.Point(1640, 11);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(177, 56);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "بلوک";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.Height = 15;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel4.BorderWidth = 2;
        this.xrPanel4.Dpi = 254F;
        this.xrPanel4.Location = new System.Drawing.Point(0, 0);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.Size = new System.Drawing.Size(1849, 15);
        this.xrPanel4.StylePriority.UseBorders = false;
        this.xrPanel4.StylePriority.UseBorderWidth = false;
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.ReportHeader,
            this.ReportFooter1});
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        this.DetailReport.PrintOnEmptyDataSource = false;
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel10});
        this.Detail1.Dpi = 254F;
        this.Detail1.Height = 85;
        this.Detail1.Name = "Detail1";
        // 
        // xrPanel10
        // 
        this.xrPanel10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel10.BorderWidth = 2;
        this.xrPanel10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel7});
        this.xrPanel10.Dpi = 254F;
        this.xrPanel10.Location = new System.Drawing.Point(0, 0);
        this.xrPanel10.Name = "xrPanel10";
        this.xrPanel10.Size = new System.Drawing.Size(1849, 85);
        this.xrPanel10.StylePriority.UseBorders = false;
        this.xrPanel10.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel7
        // 
        this.xrPanel7.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel7.BorderWidth = 2;
        this.xrPanel7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.xrPanel7.Dpi = 254F;
        this.xrPanel7.Location = new System.Drawing.Point(30, 0);
        this.xrPanel7.Name = "xrPanel7";
        this.xrPanel7.Size = new System.Drawing.Size(1780, 85);
        this.xrPanel7.StylePriority.UseBorderColor = false;
        this.xrPanel7.StylePriority.UseBorders = false;
        this.xrPanel7.StylePriority.UseBorderWidth = false;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.BorderWidth = 1;
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable4.Location = new System.Drawing.Point(10, 0);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable4.Size = new System.Drawing.Size(1760, 85);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        this.xrTable4.StylePriority.UseTextAlignment = false;
        this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTHeight,
            this.xrTLength,
            this.xrTWallMainDirectionsId});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTHeight
        // 
        this.xrTHeight.Dpi = 254F;
        this.xrTHeight.Name = "xrTHeight";
        this.xrTHeight.Text = "xrTHeight";
        this.xrTHeight.Weight = 1.1009615125461538;
        // 
        // xrTLength
        // 
        this.xrTLength.Dpi = 254F;
        this.xrTLength.Name = "xrTLength";
        this.xrTLength.Text = "xrTLength";
        this.xrTLength.Weight = 1.3063108457770731;
        // 
        // xrTWallMainDirectionsId
        // 
        this.xrTWallMainDirectionsId.Dpi = 254F;
        this.xrTWallMainDirectionsId.Name = "xrTWallMainDirectionsId";
        this.xrTWallMainDirectionsId.Text = "xrTWallMainDirectionsId";
        this.xrTWallMainDirectionsId.Weight = 1.0590850591202536;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel9});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.Height = 185;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrPanel9
        // 
        this.xrPanel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel9.BorderWidth = 2;
        this.xrPanel9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
        this.xrPanel9.Dpi = 254F;
        this.xrPanel9.Location = new System.Drawing.Point(0, 0);
        this.xrPanel9.Name = "xrPanel9";
        this.xrPanel9.Size = new System.Drawing.Size(1849, 185);
        this.xrPanel9.StylePriority.UseBorders = false;
        this.xrPanel9.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel2
        // 
        this.xrPanel2.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel2.BorderWidth = 2;
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel6,
            this.xrTable1});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.Location = new System.Drawing.Point(30, 10);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.Size = new System.Drawing.Size(1780, 175);
        this.xrPanel2.StylePriority.UseBorderColor = false;
        this.xrPanel2.StylePriority.UseBorders = false;
        this.xrPanel2.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel6
        // 
        this.xrPanel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel6.BorderWidth = 1;
        this.xrPanel6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.xrPanel6.Dpi = 254F;
        this.xrPanel6.Location = new System.Drawing.Point(10, 10);
        this.xrPanel6.Name = "xrPanel6";
        this.xrPanel6.Size = new System.Drawing.Size(1760, 70);
        this.xrPanel6.StylePriority.UseBorders = false;
        this.xrPanel6.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel1.Location = new System.Drawing.Point(1460, 24);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.Size = new System.Drawing.Size(283, 42);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "مشخصات دیوارها";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.BorderWidth = 1;
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.Location = new System.Drawing.Point(10, 85);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.Size = new System.Drawing.Size(1760, 85);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "ارتفاع";
        this.xrTableCell3.Weight = 1.3327481587969858;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "طول";
        this.xrTableCell4.Weight = 1.5803608842325561;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "جهت";
        this.xrTableCell5.Weight = 1.2836816017816179;
        // 
        // ReportFooter1
        // 
        this.ReportFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel17});
        this.ReportFooter1.Dpi = 254F;
        this.ReportFooter1.Height = 15;
        this.ReportFooter1.Name = "ReportFooter1";
        // 
        // xrPanel17
        // 
        this.xrPanel17.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel17.BorderWidth = 2;
        this.xrPanel17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel8});
        this.xrPanel17.Dpi = 254F;
        this.xrPanel17.Location = new System.Drawing.Point(0, 0);
        this.xrPanel17.Name = "xrPanel17";
        this.xrPanel17.Size = new System.Drawing.Size(1849, 15);
        this.xrPanel17.StylePriority.UseBorders = false;
        this.xrPanel17.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel8
        // 
        this.xrPanel8.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel8.BorderWidth = 2;
        this.xrPanel8.Dpi = 254F;
        this.xrPanel8.Location = new System.Drawing.Point(30, 0);
        this.xrPanel8.Name = "xrPanel8";
        this.xrPanel8.Size = new System.Drawing.Size(1780, 15);
        this.xrPanel8.StylePriority.UseBorderColor = false;
        this.xrPanel8.StylePriority.UseBorders = false;
        this.xrPanel8.StylePriority.UseBorderWidth = false;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.ReportHeader1,
            this.ReportFooter2});
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 1;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.PrintOnEmptyDataSource = false;
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel14});
        this.Detail2.Dpi = 254F;
        this.Detail2.Height = 85;
        this.Detail2.Name = "Detail2";
        // 
        // xrPanel14
        // 
        this.xrPanel14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel14.BorderWidth = 2;
        this.xrPanel14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel15});
        this.xrPanel14.Dpi = 254F;
        this.xrPanel14.Location = new System.Drawing.Point(0, 0);
        this.xrPanel14.Name = "xrPanel14";
        this.xrPanel14.Size = new System.Drawing.Size(1849, 85);
        this.xrPanel14.StylePriority.UseBorders = false;
        this.xrPanel14.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel15
        // 
        this.xrPanel15.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel15.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel15.BorderWidth = 2;
        this.xrPanel15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrPanel15.Dpi = 254F;
        this.xrPanel15.Location = new System.Drawing.Point(30, 0);
        this.xrPanel15.Name = "xrPanel15";
        this.xrPanel15.Size = new System.Drawing.Size(1780, 85);
        this.xrPanel15.StylePriority.UseBorderColor = false;
        this.xrPanel15.StylePriority.UseBorders = false;
        this.xrPanel15.StylePriority.UseBorderWidth = false;
        // 
        // xrTable6
        // 
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable6.BorderWidth = 1;
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable6.Location = new System.Drawing.Point(10, 0);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable6.Size = new System.Drawing.Size(1760, 85);
        this.xrTable6.StylePriority.UseBorders = false;
        this.xrTable6.StylePriority.UseBorderWidth = false;
        this.xrTable6.StylePriority.UseFont = false;
        this.xrTable6.StylePriority.UseTextAlignment = false;
        this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTNum,
            this.xrTMainDirectionsId,
            this.xrTEntranceTypeId});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTNum
        // 
        this.xrTNum.Dpi = 254F;
        this.xrTNum.Name = "xrTNum";
        this.xrTNum.Text = "xrTNum";
        this.xrTNum.Weight = 1.1009615125461538;
        // 
        // xrTMainDirectionsId
        // 
        this.xrTMainDirectionsId.Dpi = 254F;
        this.xrTMainDirectionsId.Name = "xrTMainDirectionsId";
        this.xrTMainDirectionsId.Text = "xrTMainDirectionsId";
        this.xrTMainDirectionsId.Weight = 1.3063108457770731;
        // 
        // xrTEntranceTypeId
        // 
        this.xrTEntranceTypeId.Dpi = 254F;
        this.xrTEntranceTypeId.Name = "xrTEntranceTypeId";
        this.xrTEntranceTypeId.Text = "xrTEntranceTypeId";
        this.xrTEntranceTypeId.Weight = 1.0590850591202536;
        // 
        // ReportHeader1
        // 
        this.ReportHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel11});
        this.ReportHeader1.Dpi = 254F;
        this.ReportHeader1.Height = 185;
        this.ReportHeader1.Name = "ReportHeader1";
        // 
        // xrPanel11
        // 
        this.xrPanel11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel11.BorderWidth = 2;
        this.xrPanel11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel12});
        this.xrPanel11.Dpi = 254F;
        this.xrPanel11.Location = new System.Drawing.Point(0, 0);
        this.xrPanel11.Name = "xrPanel11";
        this.xrPanel11.Size = new System.Drawing.Size(1849, 185);
        this.xrPanel11.StylePriority.UseBorders = false;
        this.xrPanel11.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel12
        // 
        this.xrPanel12.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel12.BorderWidth = 2;
        this.xrPanel12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel13,
            this.xrTable5});
        this.xrPanel12.Dpi = 254F;
        this.xrPanel12.Location = new System.Drawing.Point(30, 10);
        this.xrPanel12.Name = "xrPanel12";
        this.xrPanel12.Size = new System.Drawing.Size(1780, 175);
        this.xrPanel12.StylePriority.UseBorderColor = false;
        this.xrPanel12.StylePriority.UseBorders = false;
        this.xrPanel12.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel13
        // 
        this.xrPanel13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel13.BorderWidth = 1;
        this.xrPanel13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrPanel13.Dpi = 254F;
        this.xrPanel13.Location = new System.Drawing.Point(10, 10);
        this.xrPanel13.Name = "xrPanel13";
        this.xrPanel13.Size = new System.Drawing.Size(1760, 70);
        this.xrPanel13.StylePriority.UseBorders = false;
        this.xrPanel13.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel2.Location = new System.Drawing.Point(1460, 21);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(283, 42);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "مشخصات درب ها";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 1;
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable5.Location = new System.Drawing.Point(10, 85);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable5.Size = new System.Drawing.Size(1760, 85);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        this.xrTable5.StylePriority.UseFont = false;
        this.xrTable5.StylePriority.UseTextAlignment = false;
        this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell6});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "تعداد";
        this.xrTableCell1.Weight = 1.3327481587969858;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "جهت";
        this.xrTableCell2.Weight = 1.5803608842325561;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "نوع";
        this.xrTableCell6.Weight = 1.2836816017816179;
        // 
        // ReportFooter2
        // 
        this.ReportFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel18});
        this.ReportFooter2.Dpi = 254F;
        this.ReportFooter2.Height = 15;
        this.ReportFooter2.Name = "ReportFooter2";
        // 
        // xrPanel18
        // 
        this.xrPanel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel18.BorderWidth = 2;
        this.xrPanel18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel19});
        this.xrPanel18.Dpi = 254F;
        this.xrPanel18.Location = new System.Drawing.Point(0, 0);
        this.xrPanel18.Name = "xrPanel18";
        this.xrPanel18.Size = new System.Drawing.Size(1849, 15);
        this.xrPanel18.StylePriority.UseBorders = false;
        this.xrPanel18.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel19
        // 
        this.xrPanel19.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel19.BorderWidth = 2;
        this.xrPanel19.Dpi = 254F;
        this.xrPanel19.Location = new System.Drawing.Point(30, 0);
        this.xrPanel19.Name = "xrPanel19";
        this.xrPanel19.Size = new System.Drawing.Size(1780, 15);
        this.xrPanel19.StylePriority.UseBorderColor = false;
        this.xrPanel19.StylePriority.UseBorders = false;
        this.xrPanel19.StylePriority.UseBorderWidth = false;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.ReportHeader2,
            this.ReportFooter3});
        this.DetailReport2.Dpi = 254F;
        this.DetailReport2.Level = 2;
        this.DetailReport2.Name = "DetailReport2";
        this.DetailReport2.PrintOnEmptyDataSource = false;
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel22});
        this.Detail3.Dpi = 254F;
        this.Detail3.Height = 85;
        this.Detail3.Name = "Detail3";
        // 
        // xrPanel22
        // 
        this.xrPanel22.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel22.BorderWidth = 2;
        this.xrPanel22.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel23});
        this.xrPanel22.Dpi = 254F;
        this.xrPanel22.Location = new System.Drawing.Point(0, 0);
        this.xrPanel22.Name = "xrPanel22";
        this.xrPanel22.Size = new System.Drawing.Size(1849, 85);
        this.xrPanel22.StylePriority.UseBorders = false;
        this.xrPanel22.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel23
        // 
        this.xrPanel23.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel23.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel23.BorderWidth = 2;
        this.xrPanel23.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
        this.xrPanel23.Dpi = 254F;
        this.xrPanel23.Location = new System.Drawing.Point(30, 0);
        this.xrPanel23.Name = "xrPanel23";
        this.xrPanel23.Size = new System.Drawing.Size(1780, 85);
        this.xrPanel23.StylePriority.UseBorderColor = false;
        this.xrPanel23.StylePriority.UseBorders = false;
        this.xrPanel23.StylePriority.UseBorderWidth = false;
        // 
        // xrTable8
        // 
        this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable8.BorderWidth = 1;
        this.xrTable8.Dpi = 254F;
        this.xrTable8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable8.Location = new System.Drawing.Point(10, 0);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable8.Size = new System.Drawing.Size(1760, 85);
        this.xrTable8.StylePriority.UseBorders = false;
        this.xrTable8.StylePriority.UseBorderWidth = false;
        this.xrTable8.StylePriority.UseFont = false;
        this.xrTable8.StylePriority.UseTextAlignment = false;
        this.xrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTOpenPathway,
            this.xrTOpenYard,
            this.xrTClosePathway,
            this.xrTCloseYard,
            this.xrTSecondaryUsageId,
            this.xrTUsageId,
            this.xrTFlat,
            this.xrTArea,
            this.xrTFHeight,
            this.xrTEshghalSurface,
            this.xrTStageTitle});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTOpenPathway
        // 
        this.xrTOpenPathway.Dpi = 254F;
        this.xrTOpenPathway.Name = "xrTOpenPathway";
        this.xrTOpenPathway.Text = "xrTOpenPathway";
        this.xrTOpenPathway.Weight = 0.20687610684806981;
        // 
        // xrTOpenYard
        // 
        this.xrTOpenYard.Dpi = 254F;
        this.xrTOpenYard.Name = "xrTOpenYard";
        this.xrTOpenYard.Text = "xrTOpenYard";
        this.xrTOpenYard.Weight = 0.20302459860646596;
        // 
        // xrTClosePathway
        // 
        this.xrTClosePathway.Dpi = 254F;
        this.xrTClosePathway.Name = "xrTClosePathway";
        this.xrTClosePathway.Text = "xrTClosePathway";
        this.xrTClosePathway.Weight = 0.28486914874054814;
        // 
        // xrTCloseYard
        // 
        this.xrTCloseYard.Dpi = 254F;
        this.xrTCloseYard.Name = "xrTCloseYard";
        this.xrTCloseYard.Text = "xrTCloseYard";
        this.xrTCloseYard.Weight = 0.32530998527738869;
        // 
        // xrTSecondaryUsageId
        // 
        this.xrTSecondaryUsageId.Dpi = 254F;
        this.xrTSecondaryUsageId.Name = "xrTSecondaryUsageId";
        this.xrTSecondaryUsageId.Text = "xrTSecondaryUsageId";
        this.xrTSecondaryUsageId.Weight = 0.44790022105479005;
        // 
        // xrTUsageId
        // 
        this.xrTUsageId.Dpi = 254F;
        this.xrTUsageId.Name = "xrTUsageId";
        this.xrTUsageId.Text = "xrTUsageId";
        this.xrTUsageId.Weight = 0.49026681171243264;
        // 
        // xrTFlat
        // 
        this.xrTFlat.Dpi = 254F;
        this.xrTFlat.Name = "xrTFlat";
        this.xrTFlat.Text = "xrTFlat";
        this.xrTFlat.Weight = 0.24377028424978514;
        // 
        // xrTArea
        // 
        this.xrTArea.Dpi = 254F;
        this.xrTArea.Name = "xrTArea";
        this.xrTArea.Text = "xrTArea";
        this.xrTArea.Weight = 0.24377028424978514;
        // 
        // xrTFHeight
        // 
        this.xrTFHeight.Dpi = 254F;
        this.xrTFHeight.Name = "xrTFHeight";
        this.xrTFHeight.Text = "xrTFHeight";
        this.xrTFHeight.Weight = 0.28595456010888465;
        // 
        // xrTEshghalSurface
        // 
        this.xrTEshghalSurface.Dpi = 254F;
        this.xrTEshghalSurface.Name = "xrTEshghalSurface";
        this.xrTEshghalSurface.Text = "xrTEshghalSurface";
        this.xrTEshghalSurface.Weight = 0.24551372357204407;
        // 
        // xrTStageTitle
        // 
        this.xrTStageTitle.Dpi = 254F;
        this.xrTStageTitle.Name = "xrTStageTitle";
        this.xrTStageTitle.Text = "xrTStageTitle";
        this.xrTStageTitle.Weight = 0.48910169302328621;
        // 
        // ReportHeader2
        // 
        this.ReportHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel16});
        this.ReportHeader2.Dpi = 254F;
        this.ReportHeader2.Height = 185;
        this.ReportHeader2.Name = "ReportHeader2";
        // 
        // xrPanel16
        // 
        this.xrPanel16.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel16.BorderWidth = 2;
        this.xrPanel16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel20});
        this.xrPanel16.Dpi = 254F;
        this.xrPanel16.Location = new System.Drawing.Point(0, 0);
        this.xrPanel16.Name = "xrPanel16";
        this.xrPanel16.Size = new System.Drawing.Size(1849, 185);
        this.xrPanel16.StylePriority.UseBorders = false;
        this.xrPanel16.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel20
        // 
        this.xrPanel20.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel20.BorderWidth = 2;
        this.xrPanel20.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel21,
            this.xrTable7});
        this.xrPanel20.Dpi = 254F;
        this.xrPanel20.Location = new System.Drawing.Point(30, 10);
        this.xrPanel20.Name = "xrPanel20";
        this.xrPanel20.Size = new System.Drawing.Size(1780, 175);
        this.xrPanel20.StylePriority.UseBorderColor = false;
        this.xrPanel20.StylePriority.UseBorders = false;
        this.xrPanel20.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel21
        // 
        this.xrPanel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel21.BorderWidth = 1;
        this.xrPanel21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.xrPanel21.Dpi = 254F;
        this.xrPanel21.Location = new System.Drawing.Point(10, 10);
        this.xrPanel21.Name = "xrPanel21";
        this.xrPanel21.Size = new System.Drawing.Size(1760, 70);
        this.xrPanel21.StylePriority.UseBorders = false;
        this.xrPanel21.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel3.Location = new System.Drawing.Point(1460, 24);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.Size = new System.Drawing.Size(283, 42);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseForeColor = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "زیربنا";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.BorderWidth = 1;
        this.xrTable7.Dpi = 254F;
        this.xrTable7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable7.Location = new System.Drawing.Point(10, 85);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable7.Size = new System.Drawing.Size(1760, 85);
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseBorderWidth = false;
        this.xrTable7.StylePriority.UseFont = false;
        this.xrTable7.StylePriority.UseTextAlignment = false;
        this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell16,
            this.xrTableCell18,
            this.xrTableCell17,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "بالکن باز معبر";
        this.xrTableCell23.Weight = 0.25029405433292234;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "بالکن باز حیاط";
        this.xrTableCell21.Weight = 0.24796250397469391;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "بالکن بسته معبر";
        this.xrTableCell22.Weight = 0.3458876190202877;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "بالکن بسته حیاط";
        this.xrTableCell19.Weight = 0.39485017654308446;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Text = "کاربری فرعی";
        this.xrTableCell20.Weight = 0.54255601511395635;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "کاربری";
        this.xrTableCell16.Weight = 0.591518572636753;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "تعداد واحد";
        this.xrTableCell18.Weight = 0.29541167714174343;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "مساحت";
        this.xrTableCell17.Weight = 0.29541167714174371;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "ارتفاع";
        this.xrTableCell13.Weight = 0.34601056666950281;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "سطح اشغال";
        this.xrTableCell14.Weight = 0.29567663684869533;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "عنوان طبقه";
        this.xrTableCell15.Weight = 0.59121114538777664;
        // 
        // ReportFooter3
        // 
        this.ReportFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel24});
        this.ReportFooter3.Dpi = 254F;
        this.ReportFooter3.Height = 15;
        this.ReportFooter3.Name = "ReportFooter3";
        // 
        // xrPanel24
        // 
        this.xrPanel24.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel24.BorderWidth = 2;
        this.xrPanel24.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel25});
        this.xrPanel24.Dpi = 254F;
        this.xrPanel24.Location = new System.Drawing.Point(0, 0);
        this.xrPanel24.Name = "xrPanel24";
        this.xrPanel24.Size = new System.Drawing.Size(1849, 15);
        this.xrPanel24.StylePriority.UseBorders = false;
        this.xrPanel24.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel25
        // 
        this.xrPanel25.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel25.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel25.BorderWidth = 2;
        this.xrPanel25.Dpi = 254F;
        this.xrPanel25.Location = new System.Drawing.Point(30, 0);
        this.xrPanel25.Name = "xrPanel25";
        this.xrPanel25.Size = new System.Drawing.Size(1780, 15);
        this.xrPanel25.StylePriority.UseBorderColor = false;
        this.xrPanel25.StylePriority.UseBorders = false;
        this.xrPanel25.StylePriority.UseBorderWidth = false;
        // 
        // XtraReportTSBlock
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.DetailReport,
            this.DetailReport1,
            this.DetailReport2});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
