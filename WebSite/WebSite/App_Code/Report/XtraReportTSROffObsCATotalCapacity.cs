using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportTSROffObsCATotalCapacity
/// </summary>
public class XtraReportTSROffObsCATotalCapacity : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
    Capacity Cpcty = new Capacity();
    DataTable StageInfoDT = new DataTable();
    DataTable MemberDt = new DataTable();
    int OffId;
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
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private ReportHeaderBand ReportHeader1;
    private ReportFooterBand ReportFooter1;
    private XRPanel xrPanel3;
    private XRPanel xrPanel11;
    private XRPanel xrPanel10;
    private XRPanel xrPanel7;
    private XRTable xrTable5;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTTotalDsgCapacity;
    private XRTableCell xrTFactor;
    private XRTableCell xrTMemTotalCapacity;
    private XRTableCell xrTMemMaxJobCount;
    private XRTableCell xrTGrdName;
    private XRTableCell xrTMjName;
    private XRTableCell xrTMeName;
    private XRTableCell xrTMeId;
    private XRPanel xrPanel13;
    private XRPanel xrPanel14;
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
    private XRPanel xrPanel15;
    private XRPanel xrPanel16;
    private XRLabel xrLabel2;
    private XRTable xrTable7;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRPanel xrPanel12;
    private XRPanel xrPanel18;
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
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSROffObsCATotalCapacity(int OfficeId, string Year)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        //************************************************************
        //************************************************************
        //*********************Commented By tayebi********************
        //************************************************************
        //************************************************************
        //************************************************************
        this.DataSourceRowChanged += new DataSourceRowEventHandler(XtraReportTSROffDsgnTotalCapacity_DataSourceRowChanged);

        //OffId = OfficeId;
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
        ////************************************************************
        //DataRowView dr = (DataRowView)this.GetCurrentRow();

        //Stage = Convert.ToInt32(dr["Stage"]);

        //StageInfoDT = Cpcty.GetStageInformation((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSMemberType.Office, OffId, Yr, Stage);

        //DetailReport.DataSource = StageInfoDT;

        //xrTFNO.DataBindings.Add("Text", StageInfoDT, "FNO");
        //xrTConfirmDate.DataBindings.Add("Text", StageInfoDT, "ConfirmDate");
        //xrTExpireDate.DataBindings.Add("Text", StageInfoDT, "ExpireDate");
        //xrTTotalCapacity.DataBindings.Add("Text", StageInfoDT, "TotalCapacity");
        //xrTMaxJobCount.DataBindings.Add("Text", StageInfoDT, "MaxJobCount");
        //xrTProjectCount.DataBindings.Add("Text", StageInfoDT, "ProjectCount");
        //xrTTotalUsed.DataBindings.Add("Text", StageInfoDT, "TotalUsed");
        //xrTConditionalCapacity.DataBindings.Add("Text", StageInfoDT, "ConditionalCapacity");
        //xrTRemainCapacity.DataBindings.Add("Text", StageInfoDT, "RemainCapacity");
    }

    private void DetailReport_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
    {     //************************************************************
        //************************************************************
        //*********************Commented By tayebi********************
        //************************************************************
        //************************************************************
        //************************************************************
        //DataRowView dr = (DataRowView)DetailReport.GetCurrentRow();

        //int FId = Convert.ToInt32(dr["FId"]);

        //MemberDt = Cpcty.GetOfficeMembersDsgObsCapacityInYearPerStage(OffId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office, FId, Yr, Stage);
        //MemberDt.Columns.Add("GrdName");
        //MemberDt.Columns.Add("MjName");
        //for (int i = 0; i < MemberDt.Rows.Count; i++)
        //{
        //    MemberDt.Rows[i]["GrdName"] = GetGrdName(Convert.ToInt32(MemberDt.Rows[i]["Grade"]));
        //    MemberDt.Rows[i]["MjName"] = GetMjName(Convert.ToInt32(MemberDt.Rows[i]["MjId"]));
        //}

        //DetailReport1.DataSource = MemberDt;

        //xrTMeId.DataBindings.Add("Text", MemberDt, "MeId");
        //xrTMeName.DataBindings.Add("Text", MemberDt, "MeName");
        //xrTMjName.DataBindings.Add("Text", MemberDt, "MjName");
        //xrTGrdName.DataBindings.Add("Text", MemberDt, "GrdName");
        //xrTMemMaxJobCount.DataBindings.Add("Text", MemberDt, "MaxJobCount");
        //xrTMemTotalCapacity.DataBindings.Add("Text", MemberDt, "ObservationCapacity");
        //xrTFactor.DataBindings.Add("Text", MemberDt, "Factor");
        //xrTTotalDsgCapacity.DataBindings.Add("Text", MemberDt, "TotalObsCapacity");
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

    private string GetMjName(int MjId)
    {
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
        MajorManager.FindByCode(MjId);
        if (MajorManager.Count > 0)
            return MajorManager[0]["MjName"].ToString();
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
        string resourceFileName = "XtraReportTSROffObsCATotalCapacity.resx";
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
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.xrTMaxJobCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTExpireDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTConfirmDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFNO = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel9 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel17 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel8 = new DevExpress.XtraReports.UI.XRPanel();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel10 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel7 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel12 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTTotalDsgCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFactor = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMemTotalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMemMaxJobCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTGrdName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMjName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeId = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel11 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel15 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel16 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter1 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel13 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel14 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel18 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
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
            this.xrTableCell10,
            this.xrTableCell31,
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
        this.xrTableCell18.Weight = 0.28641796659792224;
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
        this.xrTableCell15.Weight = 0.30778775291202293;
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
        this.xrTableCell32.Weight = 0.31311083799369765;
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
        this.xrTableCell6.Weight = 0.3126860981256267;
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
        this.xrTableCell10.Weight = 0.2784675042762681;
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
        this.xrTableCell31.Weight = 0.31322501853652268;
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
        this.xrTableCell27.Weight = 0.34608273893258656;
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
        this.xrTableCell12.Weight = 0.33065685529264316;
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
        this.xrTableCell14.Weight = 0.51648594903856027;
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
            this.ReportFooter,
            this.DetailReport1});
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        this.DetailReport.PrintOnEmptyDataSource = false;
        this.DetailReport.DataSourceRowChanged += new DevExpress.XtraReports.UI.DataSourceRowEventHandler(this.DetailReport_DataSourceRowChanged);
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
            this.xrTMaxJobCount,
            this.xrTTotalCapacity,
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
        this.xrTRemainCapacity.Weight = 0.28692716927773454;
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
        this.xrTConditionalCapacity.Weight = 0.30657793040919912;
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
        this.xrTTotalUsed.Weight = 0.31249949555203943;
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
        this.xrTProjectCount.Weight = 0.31284210913354221;
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
        this.xrTMaxJobCount.Weight = 0.27909019654542627;
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
        this.xrTTotalCapacity.Weight = 0.31209650719280951;
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
        this.xrTExpireDate.Weight = 0.34679366150301277;
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
        this.xrTConfirmDate.Weight = 0.3320716932447671;
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
        this.xrTFNO.Weight = 0.51602195884731872;
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
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.ReportHeader1,
            this.ReportFooter1});
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.PrintOnEmptyDataSource = false;
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel10});
        this.Detail2.Dpi = 254F;
        this.Detail2.Height = 85;
        this.Detail2.Name = "Detail2";
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
            this.xrPanel12});
        this.xrPanel7.Dpi = 254F;
        this.xrPanel7.Location = new System.Drawing.Point(30, 0);
        this.xrPanel7.Name = "xrPanel7";
        this.xrPanel7.Size = new System.Drawing.Size(1780, 85);
        this.xrPanel7.StylePriority.UseBorderColor = false;
        this.xrPanel7.StylePriority.UseBorders = false;
        this.xrPanel7.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel12
        // 
        this.xrPanel12.BorderColor = System.Drawing.Color.SteelBlue;
        this.xrPanel12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel12.BorderWidth = 2;
        this.xrPanel12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.xrPanel12.Dpi = 254F;
        this.xrPanel12.Location = new System.Drawing.Point(60, 0);
        this.xrPanel12.Name = "xrPanel12";
        this.xrPanel12.Size = new System.Drawing.Size(1660, 85);
        this.xrPanel12.StylePriority.UseBorderColor = false;
        this.xrPanel12.StylePriority.UseBorders = false;
        this.xrPanel12.StylePriority.UseBorderWidth = false;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 1;
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable5.Location = new System.Drawing.Point(9, 0);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable5.Size = new System.Drawing.Size(1640, 85);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        this.xrTable5.StylePriority.UseFont = false;
        this.xrTable5.StylePriority.UseTextAlignment = false;
        this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTTotalDsgCapacity,
            this.xrTFactor,
            this.xrTMemTotalCapacity,
            this.xrTMemMaxJobCount,
            this.xrTGrdName,
            this.xrTMjName,
            this.xrTMeName,
            this.xrTMeId});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTTotalDsgCapacity
        // 
        this.xrTTotalDsgCapacity.Dpi = 254F;
        this.xrTTotalDsgCapacity.Name = "xrTTotalDsgCapacity";
        this.xrTTotalDsgCapacity.Text = "xrTTotalDsgCapacity";
        this.xrTTotalDsgCapacity.Weight = 0.16675829863014174;
        // 
        // xrTFactor
        // 
        this.xrTFactor.Dpi = 254F;
        this.xrTFactor.Name = "xrTFactor";
        this.xrTFactor.Text = "xrTFactor";
        this.xrTFactor.Weight = 0.14651090084237725;
        // 
        // xrTMemTotalCapacity
        // 
        this.xrTMemTotalCapacity.Dpi = 254F;
        this.xrTMemTotalCapacity.Name = "xrTMemTotalCapacity";
        this.xrTMemTotalCapacity.Text = "xrTMemTotalCapacity";
        this.xrTMemTotalCapacity.Weight = 0.16267338866273523;
        // 
        // xrTMemMaxJobCount
        // 
        this.xrTMemMaxJobCount.Dpi = 254F;
        this.xrTMemMaxJobCount.Name = "xrTMemMaxJobCount";
        this.xrTMemMaxJobCount.Text = "xrTMemMaxJobCount";
        this.xrTMemMaxJobCount.Weight = 0.14505520788082155;
        // 
        // xrTGrdName
        // 
        this.xrTGrdName.Dpi = 254F;
        this.xrTGrdName.Name = "xrTGrdName";
        this.xrTGrdName.Text = "xrTGrdName";
        this.xrTGrdName.Weight = 0.12925353070671108;
        // 
        // xrTMjName
        // 
        this.xrTMjName.Dpi = 254F;
        this.xrTMjName.Name = "xrTMjName";
        this.xrTMjName.Text = "xrTMjName";
        this.xrTMjName.Weight = 0.23683800717536624;
        // 
        // xrTMeName
        // 
        this.xrTMeName.Dpi = 254F;
        this.xrTMeName.Name = "xrTMeName";
        this.xrTMeName.Text = "xrTMeName";
        this.xrTMeName.Weight = 0.41084281896724945;
        // 
        // xrTMeId
        // 
        this.xrTMeId.Dpi = 254F;
        this.xrTMeId.Name = "xrTMeId";
        this.xrTMeId.Text = "xrTMeId";
        this.xrTMeId.Weight = 0.11989370721565335;
        // 
        // ReportHeader1
        // 
        this.ReportHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.ReportHeader1.Dpi = 254F;
        this.ReportHeader1.Height = 170;
        this.ReportHeader1.Name = "ReportHeader1";
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel3.BorderWidth = 2;
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel11});
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.Location = new System.Drawing.Point(0, 0);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.Size = new System.Drawing.Size(1849, 170);
        this.xrPanel3.StylePriority.UseBorders = false;
        this.xrPanel3.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel11
        // 
        this.xrPanel11.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel11.BorderWidth = 2;
        this.xrPanel11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel15});
        this.xrPanel11.Dpi = 254F;
        this.xrPanel11.Location = new System.Drawing.Point(29, 0);
        this.xrPanel11.Name = "xrPanel11";
        this.xrPanel11.Size = new System.Drawing.Size(1781, 170);
        this.xrPanel11.StylePriority.UseBorderColor = false;
        this.xrPanel11.StylePriority.UseBorders = false;
        this.xrPanel11.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel15
        // 
        this.xrPanel15.BorderColor = System.Drawing.Color.SteelBlue;
        this.xrPanel15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel15.BorderWidth = 2;
        this.xrPanel15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel16,
            this.xrTable7});
        this.xrPanel15.Dpi = 254F;
        this.xrPanel15.Location = new System.Drawing.Point(60, 10);
        this.xrPanel15.Name = "xrPanel15";
        this.xrPanel15.Size = new System.Drawing.Size(1660, 160);
        this.xrPanel15.StylePriority.UseBorderColor = false;
        this.xrPanel15.StylePriority.UseBorders = false;
        this.xrPanel15.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel16
        // 
        this.xrPanel16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel16.BorderWidth = 1;
        this.xrPanel16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrPanel16.Dpi = 254F;
        this.xrPanel16.Location = new System.Drawing.Point(10, 10);
        this.xrPanel16.Name = "xrPanel16";
        this.xrPanel16.Size = new System.Drawing.Size(1640, 70);
        this.xrPanel16.StylePriority.UseBorders = false;
        this.xrPanel16.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel2.Location = new System.Drawing.Point(1365, 11);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(241, 56);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "اعضای شرکت";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
        this.xrTable7.Size = new System.Drawing.Size(1640, 85);
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseBorderWidth = false;
        this.xrTable7.StylePriority.UseFont = false;
        this.xrTable7.StylePriority.UseTextAlignment = false;
        this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell11,
            this.xrTableCell13,
            this.xrTableCell23,
            this.xrTableCell24});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "حداکثر ظرفیت اشتغال";
        this.xrTableCell4.Weight = 0.36037219826480743;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "ضریب موثر";
        this.xrTableCell7.Weight = 0.31655036566141104;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "ظرفیت اشتغال";
        this.xrTableCell8.Weight = 0.352485222210897;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "حداکثر تعداد کار";
        this.xrTableCell9.Weight = 0.31436503580491637;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "پایه";
        this.xrTableCell11.Weight = 0.28040508176047724;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "رشته";
        this.xrTableCell13.Weight = 0.51328345870188563;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "نام";
        this.xrTableCell23.Weight = 0.88972068558968853;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "کد عضویت";
        this.xrTableCell24.Weight = 0.25911695704897408;
        // 
        // ReportFooter1
        // 
        this.ReportFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel13});
        this.ReportFooter1.Dpi = 254F;
        this.ReportFooter1.Height = 15;
        this.ReportFooter1.Name = "ReportFooter1";
        // 
        // xrPanel13
        // 
        this.xrPanel13.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel13.BorderWidth = 2;
        this.xrPanel13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel14});
        this.xrPanel13.Dpi = 254F;
        this.xrPanel13.Location = new System.Drawing.Point(0, 0);
        this.xrPanel13.Name = "xrPanel13";
        this.xrPanel13.Size = new System.Drawing.Size(1849, 15);
        this.xrPanel13.StylePriority.UseBorders = false;
        this.xrPanel13.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel14
        // 
        this.xrPanel14.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrPanel14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel14.BorderWidth = 2;
        this.xrPanel14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel18});
        this.xrPanel14.Dpi = 254F;
        this.xrPanel14.Location = new System.Drawing.Point(30, 0);
        this.xrPanel14.Name = "xrPanel14";
        this.xrPanel14.Size = new System.Drawing.Size(1780, 15);
        this.xrPanel14.StylePriority.UseBorderColor = false;
        this.xrPanel14.StylePriority.UseBorders = false;
        this.xrPanel14.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel18
        // 
        this.xrPanel18.BorderColor = System.Drawing.Color.SteelBlue;
        this.xrPanel18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel18.BorderWidth = 2;
        this.xrPanel18.Dpi = 254F;
        this.xrPanel18.Location = new System.Drawing.Point(60, 0);
        this.xrPanel18.Name = "xrPanel18";
        this.xrPanel18.Size = new System.Drawing.Size(1660, 15);
        this.xrPanel18.StylePriority.UseBorderColor = false;
        this.xrPanel18.StylePriority.UseBorders = false;
        this.xrPanel18.StylePriority.UseBorderWidth = false;
        // 
        // XtraReportTSROffObsCATotalCapacity
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
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    

    
}