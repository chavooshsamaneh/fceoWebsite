using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportTSRMemImpCATotalCapacity
/// </summary>
public class XtraReportTSRMemImpCATotalCapacity : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
    Capacity Cpcty = new Capacity();
    DataTable StageInfoDT = new DataTable();
    int MId;
    string Yr;
    int Stage;

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel10;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell18;
    private GroupFooterBand GroupFooter1;
    private XRPanel xrPanel4;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel9;
    private XRPanel xrPanel2;
    private XRPanel xrPanel17;
    private XRPanel xrPanel8;
    private XRTable xrTable4;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTRemainCapacity;
    private XRTableCell xrTConditionalCapacity;
    private XRTableCell xrTTotalUsed;
    private XRTableCell xrTProjectCount;
    private XRTableCell xrTMaxJobCount;
    private XRTableCell xrTTotalCapacity;
    private XRTableCell xrTExpireDate;
    private XRTableCell xrTConfirmDate;
    private XRTableCell xrTFNO;
    private XRTable xrTable3;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell2;
    private XRPanel xrPanel6;
    private XRTable xrTable6;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell25;
    private XRTable xrTable8;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTAssignmentDate;
    private XRTableCell xrTIsAssigned;
    private XRTableCell xrTRemainIsWaste;
    private XRTableCell xrTJobCountPrcnt;
    private XRTableCell xrTCapacityPrcnt;
    private XRTableCell xrTStageText;
    private XRTableCell xrTYear;
    private XRCheckBox checkEditRemainIsWaste;
    private XRCheckBox checkEditIsAssigned;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTGrade;
    private XRTableCell xrTMaxFloor;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSRMemImpCATotalCapacity(int MeId, string Year)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        this.DataSourceRowChanged += new DataSourceRowEventHandler(XtraReportTSROffDsgnTotalCapacity_DataSourceRowChanged);
        //************************************************************
        //************************************************************
        //*********************Commented By tayebi********************
        //************************************************************
        //************************************************************
        //************************************************************
        //MId = MeId;
        //Yr = Year;

        //if (Year != "")
        //    CapacityAssignmentManager = Cpcty.GetYearsStages(Year);

        //this.DataSource = CapacityAssignmentManager.DataTable;

        //xrTYear.DataBindings.Add("Text", this.DataSource, "Year");
        //xrTStageText.DataBindings.Add("Text", this.DataSource, "StageText");
        //xrTCapacityPrcnt.DataBindings.Add("Text", this.DataSource, "CapacityPrcnt");
        //xrTJobCountPrcnt.DataBindings.Add("Text", this.DataSource, "JobCountPrcnt");
        //checkEditRemainIsWaste.DataBindings.Add("Checked", this.DataSource, "RemainIsWaste");
        //checkEditIsAssigned.DataBindings.Add("Checked", this.DataSource, "IsAssigned");
        //xrTAssignmentDate.DataBindings.Add("Text", this.DataSource, "AssignmentDate");
        
    }

    private void XtraReportTSROffDsgnTotalCapacity_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
    {     //************************************************************
        //************************************************************
        //*********************Commented By tayebi********************
        //************************************************************
        //************************************************************
        //************************************************************
        //DataRowView dr = (DataRowView)this.GetCurrentRow();

        //Stage = Convert.ToInt32(dr["Stage"]);

        //StageInfoDT = Cpcty.GetStageInformation((int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSMemberType.Member, MId, Yr, Stage);
        //StageInfoDT.Columns.Add("GrdName");
        //for (int i = 0; i < StageInfoDT.Rows.Count; i++)
        //    StageInfoDT.Rows[i]["GrdName"] = GetGrdName(Convert.ToInt32(StageInfoDT.Rows[i]["Grade"]));

        //DetailReport.DataSource = StageInfoDT;

        //xrTFNO.DataBindings.Add("Text", StageInfoDT, "FNO");
        //xrTConfirmDate.DataBindings.Add("Text", StageInfoDT, "ConfirmDate");
        //xrTExpireDate.DataBindings.Add("Text", StageInfoDT, "ExpireDate");
        //xrTGrade.DataBindings.Add("Text", StageInfoDT, "GrdName");
        //xrTTotalCapacity.DataBindings.Add("Text", StageInfoDT, "TotalCapacity");
        //xrTMaxJobCount.DataBindings.Add("Text", StageInfoDT, "MaxJobCount");
        //xrTMaxFloor.DataBindings.Add("Text", StageInfoDT, "MaxFloor");
        //xrTProjectCount.DataBindings.Add("Text", StageInfoDT, "ProjectCount");
        //xrTTotalUsed.DataBindings.Add("Text", StageInfoDT, "TotalUsed");
        //xrTConditionalCapacity.DataBindings.Add("Text", StageInfoDT, "ConditionalCapacity");
        //xrTRemainCapacity.DataBindings.Add("Text", StageInfoDT, "RemainCapacity");
    }       

    private string GetGrdName(int GrdId)
    {
        TSP.DataManager.GradeManager GradeManager = new TSP.DataManager.GradeManager();
        GradeManager.FindByCode(GrdId);
        if (GradeManager.Count > 0)
            return GradeManager[0]["GrdName"].ToString();
        else
            return "";
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
        string resourceFileName = "XtraReportTSRMemImpCATotalCapacity.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTAssignmentDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTIsAssigned = new DevExpress.XtraReports.UI.XRTableCell();
        this.checkEditIsAssigned = new DevExpress.XtraReports.UI.XRCheckBox();
        this.xrTRemainIsWaste = new DevExpress.XtraReports.UI.XRTableCell();
        this.checkEditRemainIsWaste = new DevExpress.XtraReports.UI.XRCheckBox();
        this.xrTJobCountPrcnt = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCapacityPrcnt = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTStageText = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTYear = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPanel6 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRemainCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTConditionalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalUsed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMaxFloor = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMaxJobCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTGrade = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTExpireDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTConfirmDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFNO = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel9 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel17 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel8 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 95;
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
        this.xrTable1.Size = new System.Drawing.Size(1849, 95);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1.2782152230971129;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell1.BorderWidth = 2;
        this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseBorderWidth = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell1.Weight = 3;
        // 
        // xrTable8
        // 
        this.xrTable8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable8.BorderWidth = 1;
        this.xrTable8.Dpi = 254F;
        this.xrTable8.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrTable8.Location = new System.Drawing.Point(10, 10);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable8.Size = new System.Drawing.Size(1829, 85);
        this.xrTable8.StylePriority.UseBorders = false;
        this.xrTable8.StylePriority.UseBorderWidth = false;
        this.xrTable8.StylePriority.UseFont = false;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTAssignmentDate,
            this.xrTIsAssigned,
            this.xrTRemainIsWaste,
            this.xrTJobCountPrcnt,
            this.xrTCapacityPrcnt,
            this.xrTStageText,
            this.xrTYear});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTAssignmentDate
        // 
        this.xrTAssignmentDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTAssignmentDate.Dpi = 254F;
        this.xrTAssignmentDate.Name = "xrTAssignmentDate";
        this.xrTAssignmentDate.StylePriority.UseBorders = false;
        this.xrTAssignmentDate.StylePriority.UseTextAlignment = false;
        this.xrTAssignmentDate.Text = "xrTAssignmentDate";
        this.xrTAssignmentDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTAssignmentDate.Weight = 0.31311083799369765;
        // 
        // xrTIsAssigned
        // 
        this.xrTIsAssigned.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTIsAssigned.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.checkEditIsAssigned});
        this.xrTIsAssigned.Dpi = 254F;
        this.xrTIsAssigned.Name = "xrTIsAssigned";
        this.xrTIsAssigned.StylePriority.UseBorders = false;
        this.xrTIsAssigned.StylePriority.UseTextAlignment = false;
        this.xrTIsAssigned.Text = "xrTIsAssigned";
        this.xrTIsAssigned.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTIsAssigned.Weight = 0.34431931849342423;
        // 
        // checkEditIsAssigned
        // 
        this.checkEditIsAssigned.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.checkEditIsAssigned.Dpi = 254F;
        this.checkEditIsAssigned.Location = new System.Drawing.Point(111, 21);
        this.checkEditIsAssigned.Name = "checkEditIsAssigned";
        this.checkEditIsAssigned.Size = new System.Drawing.Size(42, 42);
        this.checkEditIsAssigned.StylePriority.UseBorders = false;
        // 
        // xrTRemainIsWaste
        // 
        this.xrTRemainIsWaste.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTRemainIsWaste.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.checkEditRemainIsWaste});
        this.xrTRemainIsWaste.Dpi = 254F;
        this.xrTRemainIsWaste.Name = "xrTRemainIsWaste";
        this.xrTRemainIsWaste.StylePriority.UseBorders = false;
        this.xrTRemainIsWaste.StylePriority.UseTextAlignment = false;
        this.xrTRemainIsWaste.Text = "xrTRemainIsWaste";
        this.xrTRemainIsWaste.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTRemainIsWaste.Weight = 0.36282275859039492;
        // 
        // checkEditRemainIsWaste
        // 
        this.checkEditRemainIsWaste.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.checkEditRemainIsWaste.Dpi = 254F;
        this.checkEditRemainIsWaste.Location = new System.Drawing.Point(127, 21);
        this.checkEditRemainIsWaste.Name = "checkEditRemainIsWaste";
        this.checkEditRemainIsWaste.Size = new System.Drawing.Size(42, 42);
        this.checkEditRemainIsWaste.StylePriority.UseBorders = false;
        // 
        // xrTJobCountPrcnt
        // 
        this.xrTJobCountPrcnt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTJobCountPrcnt.Dpi = 254F;
        this.xrTJobCountPrcnt.Name = "xrTJobCountPrcnt";
        this.xrTJobCountPrcnt.StylePriority.UseBorders = false;
        this.xrTJobCountPrcnt.StylePriority.UseTextAlignment = false;
        this.xrTJobCountPrcnt.Text = "xrTJobCountPrcnt";
        this.xrTJobCountPrcnt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTJobCountPrcnt.Weight = 0.38967196775870011;
        // 
        // xrTCapacityPrcnt
        // 
        this.xrTCapacityPrcnt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTCapacityPrcnt.Dpi = 254F;
        this.xrTCapacityPrcnt.Name = "xrTCapacityPrcnt";
        this.xrTCapacityPrcnt.StylePriority.UseBorders = false;
        this.xrTCapacityPrcnt.StylePriority.UseTextAlignment = false;
        this.xrTCapacityPrcnt.Text = "xrTCapacityPrcnt";
        this.xrTCapacityPrcnt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTCapacityPrcnt.Weight = 0.36189934911648536;
        // 
        // xrTStageText
        // 
        this.xrTStageText.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTStageText.Dpi = 254F;
        this.xrTStageText.Name = "xrTStageText";
        this.xrTStageText.StylePriority.UseBorders = false;
        this.xrTStageText.StylePriority.UseTextAlignment = false;
        this.xrTStageText.Text = "xrTStageText";
        this.xrTStageText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTStageText.Weight = 0.38996914348226352;
        // 
        // xrTYear
        // 
        this.xrTYear.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTYear.Dpi = 254F;
        this.xrTYear.Name = "xrTYear";
        this.xrTYear.StylePriority.UseBorders = false;
        this.xrTYear.StylePriority.UseTextAlignment = false;
        this.xrTYear.Text = "xrTYear";
        this.xrTYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTYear.Weight = 0.24892162676093926;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 170;
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
            this.xrPanel5,
            this.xrTable6});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.Location = new System.Drawing.Point(0, 0);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(1849, 170);
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
        this.xrLabel10.Location = new System.Drawing.Point(1450, 11);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.Size = new System.Drawing.Size(360, 56);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseForeColor = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "اختصاص ظرفیت";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTable6
        // 
        this.xrTable6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable6.BorderWidth = 1;
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrTable6.Location = new System.Drawing.Point(10, 85);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable6.Size = new System.Drawing.Size(1829, 85);
        this.xrTable6.StylePriority.UseBorders = false;
        this.xrTable6.StylePriority.UseBorderWidth = false;
        this.xrTable6.StylePriority.UseFont = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell25});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "تاریخ اختصاص";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell16.Weight = 0.31311083799369765;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "اختصاص داده شده";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell17.Weight = 0.34431931849342423;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "سوخت مرحله قبل";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell19.Weight = 0.36282275859039492;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBorders = false;
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "درصد تعداد کار";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell20.Weight = 0.38967196775870011;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseBorders = false;
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = "درصد ظرفیت";
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell21.Weight = 0.36189934911648536;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseBorders = false;
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        this.xrTableCell22.Text = "مرحله";
        this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell22.Weight = 0.38996914348226352;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.StylePriority.UseTextAlignment = false;
        this.xrTableCell25.Text = "سال";
        this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell25.Weight = 0.24892162676093926;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrTable2.Location = new System.Drawing.Point(10, 10);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable2.Size = new System.Drawing.Size(1760, 85);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell15,
            this.xrTableCell32,
            this.xrTableCell6,
            this.xrTableCell4,
            this.xrTableCell10,
            this.xrTableCell31,
            this.xrTableCell3,
            this.xrTableCell27,
            this.xrTableCell12,
            this.xrTableCell14});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.ForeColor = System.Drawing.SystemColors.ControlText;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.StylePriority.UseForeColor = false;
        this.xrTableRow6.Weight = 1;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = "ظرفیت باقیمانده";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell18.Weight = 0.25739316417235442;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "کاهش/افزایش ظرفیت";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell15.Weight = 0.2890069984013614;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseBorders = false;
        this.xrTableCell32.StylePriority.UseTextAlignment = false;
        this.xrTableCell32.Text = "کل کارکرد";
        this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell32.Weight = 0.25335389182341084;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "تعداد پروژه های در دست اجرا";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.21707498425316779;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "حداکثر تعداد طبقات";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 0.21606411149993138;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "حداکثر تعداد کار";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell10.Weight = 0.21606411149993135;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseBorders = false;
        this.xrTableCell31.StylePriority.UseTextAlignment = false;
        this.xrTableCell31.Text = "ظرفیت اشتغال";
        this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell31.Weight = 0.25517541368538693;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "پایه";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell3.Weight = 0.17987073474289742;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseBorders = false;
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.Text = "تاریخ اعتبار پروانه";
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell27.Weight = 0.36255625589206;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "تاریخ تایید پروانه";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell12.Weight = 0.36480368167566424;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "شماره پروانه";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell14.Weight = 0.39355737405968461;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.Height = 26;
        this.GroupFooter1.Name = "GroupFooter1";
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
            this.ReportFooter});
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        this.DetailReport.PrintOnEmptyDataSource = false;
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.Detail1.Dpi = 254F;
        this.Detail1.Height = 64;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable3
        // 
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Location = new System.Drawing.Point(0, 0);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable3.Size = new System.Drawing.Size(1849, 64);
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1.2782152230971129;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell2.BorderWidth = 2;
        this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel6});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseBorderWidth = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell2.Weight = 3;
        // 
        // xrPanel6
        // 
        this.xrPanel6.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel6.BorderWidth = 2;
        this.xrPanel6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.xrPanel6.Dpi = 254F;
        this.xrPanel6.Location = new System.Drawing.Point(30, 0);
        this.xrPanel6.Name = "xrPanel6";
        this.xrPanel6.Size = new System.Drawing.Size(1780, 64);
        this.xrPanel6.StylePriority.UseBorderColor = false;
        this.xrPanel6.StylePriority.UseBorders = false;
        this.xrPanel6.StylePriority.UseBorderWidth = false;
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
            this.xrTableRow2});
        this.xrTable4.Size = new System.Drawing.Size(1760, 64);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRemainCapacity,
            this.xrTConditionalCapacity,
            this.xrTTotalUsed,
            this.xrTProjectCount,
            this.xrTMaxFloor,
            this.xrTMaxJobCount,
            this.xrTTotalCapacity,
            this.xrTGrade,
            this.xrTExpireDate,
            this.xrTConfirmDate,
            this.xrTFNO});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.ForeColor = System.Drawing.SystemColors.ControlText;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.StylePriority.UseForeColor = false;
        this.xrTableRow2.Weight = 1;
        // 
        // xrTRemainCapacity
        // 
        this.xrTRemainCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTRemainCapacity.Dpi = 254F;
        this.xrTRemainCapacity.Name = "xrTRemainCapacity";
        this.xrTRemainCapacity.StylePriority.UseBorders = false;
        this.xrTRemainCapacity.StylePriority.UseTextAlignment = false;
        this.xrTRemainCapacity.Text = "xrTRemainCapacity";
        this.xrTRemainCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTRemainCapacity.Weight = 0.25790236685216666;
        // 
        // xrTConditionalCapacity
        // 
        this.xrTConditionalCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTConditionalCapacity.Dpi = 254F;
        this.xrTConditionalCapacity.Name = "xrTConditionalCapacity";
        this.xrTConditionalCapacity.StylePriority.UseBorders = false;
        this.xrTConditionalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTConditionalCapacity.Text = "xrTConditionalCapacity";
        this.xrTConditionalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTConditionalCapacity.Weight = 0.28779717589853754;
        // 
        // xrTTotalUsed
        // 
        this.xrTTotalUsed.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalUsed.Dpi = 254F;
        this.xrTTotalUsed.Name = "xrTTotalUsed";
        this.xrTTotalUsed.StylePriority.UseBorders = false;
        this.xrTTotalUsed.StylePriority.UseTextAlignment = false;
        this.xrTTotalUsed.Text = "xrTTotalUsed";
        this.xrTTotalUsed.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTTotalUsed.Weight = 0.25274254938175267;
        // 
        // xrTProjectCount
        // 
        this.xrTProjectCount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTProjectCount.Dpi = 254F;
        this.xrTProjectCount.Name = "xrTProjectCount";
        this.xrTProjectCount.StylePriority.UseBorders = false;
        this.xrTProjectCount.StylePriority.UseTextAlignment = false;
        this.xrTProjectCount.Text = "xrTProjectCount";
        this.xrTProjectCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTProjectCount.Weight = 0.2161780007370579;
        // 
        // xrTMaxFloor
        // 
        this.xrTMaxFloor.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTMaxFloor.Dpi = 254F;
        this.xrTMaxFloor.Name = "xrTMaxFloor";
        this.xrTMaxFloor.StylePriority.UseBorders = false;
        this.xrTMaxFloor.StylePriority.UseTextAlignment = false;
        this.xrTMaxFloor.Text = "xrTMaxFloor";
        this.xrTMaxFloor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTMaxFloor.Weight = 0.21788534205620896;
        // 
        // xrTMaxJobCount
        // 
        this.xrTMaxJobCount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTMaxJobCount.Dpi = 254F;
        this.xrTMaxJobCount.Name = "xrTMaxJobCount";
        this.xrTMaxJobCount.StylePriority.UseBorders = false;
        this.xrTMaxJobCount.StylePriority.UseTextAlignment = false;
        this.xrTMaxJobCount.Text = "xrTMaxJobCount";
        this.xrTMaxJobCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTMaxJobCount.Weight = 0.21762590905598844;
        // 
        // xrTTotalCapacity
        // 
        this.xrTTotalCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalCapacity.Dpi = 254F;
        this.xrTTotalCapacity.Name = "xrTTotalCapacity";
        this.xrTTotalCapacity.StylePriority.UseBorders = false;
        this.xrTTotalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTTotalCapacity.Text = "xrTTotalCapacity";
        this.xrTTotalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTTotalCapacity.Weight = 0.25404690234167376;
        // 
        // xrTGrade
        // 
        this.xrTGrade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTGrade.Dpi = 254F;
        this.xrTGrade.Name = "xrTGrade";
        this.xrTGrade.StylePriority.UseBorders = false;
        this.xrTGrade.StylePriority.UseTextAlignment = false;
        this.xrTGrade.Text = "xrTGrade";
        this.xrTGrade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTGrade.Weight = 0.17937252536853507;
        // 
        // xrTExpireDate
        // 
        this.xrTExpireDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTExpireDate.Dpi = 254F;
        this.xrTExpireDate.Name = "xrTExpireDate";
        this.xrTExpireDate.StylePriority.UseBorders = false;
        this.xrTExpireDate.StylePriority.UseTextAlignment = false;
        this.xrTExpireDate.Text = "xrTExpireDate";
        this.xrTExpireDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTExpireDate.Weight = 0.36205804651769757;
        // 
        // xrTConfirmDate
        // 
        this.xrTConfirmDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTConfirmDate.Dpi = 254F;
        this.xrTConfirmDate.Name = "xrTConfirmDate";
        this.xrTConfirmDate.StylePriority.UseBorders = false;
        this.xrTConfirmDate.StylePriority.UseTextAlignment = false;
        this.xrTConfirmDate.Text = "xrTConfirmDate";
        this.xrTConfirmDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTConfirmDate.Weight = 0.36621851962778806;
        // 
        // xrTFNO
        // 
        this.xrTFNO.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTFNO.Dpi = 254F;
        this.xrTFNO.Name = "xrTFNO";
        this.xrTFNO.StylePriority.UseBorders = false;
        this.xrTFNO.StylePriority.UseTextAlignment = false;
        this.xrTFNO.Text = "xrTFNO";
        this.xrTFNO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTFNO.Weight = 0.39309338386844306;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel9});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.Height = 95;
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
        this.xrPanel9.Size = new System.Drawing.Size(1849, 95);
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
            this.xrTable2});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.Location = new System.Drawing.Point(30, 10);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.Size = new System.Drawing.Size(1780, 85);
        this.xrPanel2.StylePriority.UseBorderColor = false;
        this.xrPanel2.StylePriority.UseBorders = false;
        this.xrPanel2.StylePriority.UseBorderWidth = false;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel17});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.Height = 15;
        this.ReportFooter.Name = "ReportFooter";
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
        // XtraReportTSRMemImpCATotalCapacity
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.GroupFooter1,
            this.DetailReport});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        this.DataSourceRowChanged += new DevExpress.XtraReports.UI.DataSourceRowEventHandler(this.XtraReportTSROffDsgnTotalCapacity_DataSourceRowChanged);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion    
}