using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportTSImplementer
/// </summary>
public class XtraReportTSImplementer : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
    TSP.DataManager.TechnicalServices.ImplementerAgentManager ImplementerAgentManager = new TSP.DataManager.TechnicalServices.ImplementerAgentManager();

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
    private XRTableCell xrTWage;
    private XRTableCell xrTMemberTypeTitle;
    private XRTableCell xrTName;
    private XRTableCell xrTIsMother;
    private XRPanel xrPanel3;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTCapacityDecrement;
    private XRTableCell xrTID;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTManager;
    private XRCheckBox checkEditIsMother;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private ReportHeaderBand ReportHeader;
    private XRPanel xrPanel9;
    private XRPanel xrPanel2;
    private XRPanel xrPanel6;
    private XRLabel xrLabel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRPanel xrPanel14;
    private XRPanel xrPanel15;
    private XRTable xrTable6;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTNo;
    private XRTableCell xrTAName;
    private XRTableCell xrTMeOPersonIdeId;
    private GroupFooterBand GroupFooter1;
    private XRPanel xrPanel18;
    private XRPanel xrPanel19;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTAMemberTypeTitle;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSImplementer(int ProjectId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        this.DataSourceRowChanged += new DataSourceRowEventHandler(XtraReportTSImplementer_DataSourceRowChanged);

        ProjectImplementerManager.FindConfirmedByProjectId(ProjectId);
        ImplementerAgentManager.FindByProjectId(ProjectId);
      
        checkEditIsMother.DataBindings.Add("Checked", ProjectImplementerManager.DataTable,"IsMother");
        xrTID.DataBindings.Add("Text", ProjectImplementerManager.DataTable, "ID");
        xrTName.DataBindings.Add("Text", ProjectImplementerManager.DataTable, "Name");
        xrTMemberTypeTitle.DataBindings.Add("Text", ProjectImplementerManager.DataTable, "MemberTypeTitle");
        xrTManager.DataBindings.Add("Text", ProjectImplementerManager.DataTable, "Manager");
        xrTCapacityDecrement.DataBindings.Add("Text", ProjectImplementerManager.DataTable, "CapacityDecrement");
        xrTWage.DataBindings.Add("Text", ProjectImplementerManager.DataTable, "Wage");

        this.DataSource = ProjectImplementerManager.DataTable;
    }

    void XtraReportTSImplementer_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
    {
        DataRowView dr = (DataRowView)this.GetCurrentRow();

        ImplementerAgentManager.CurrentFilter = "PrjImpId=" + dr["PrjImpId"].ToString();
        DetailReport.DataSource = ImplementerAgentManager.DataTable;
        xrTMeOPersonIdeId.DataBindings.Add("Text", ImplementerAgentManager.DataTable, "MeOPersonIdeId");
        xrTAName.DataBindings.Add("Text", ImplementerAgentManager.DataTable, "Name");
        xrTAMemberTypeTitle.DataBindings.Add("Text", ImplementerAgentManager.DataTable, "MemberTypeTitle");
        xrTNo.DataBindings.Add("Text", ImplementerAgentManager.DataTable, "No");        
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
        string resourceFileName = "XtraReportTSImplementer.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTWage = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCapacityDecrement = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTManager = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMemberTypeTitle = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTID = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTIsMother = new DevExpress.XtraReports.UI.XRTableCell();
        this.checkEditIsMother = new DevExpress.XtraReports.UI.XRCheckBox();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.xrPanel14 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel15 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTAMemberTypeTitle = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTAName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeOPersonIdeId = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel9 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPanel6 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrPanel18 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel19 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
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
            this.xrTWage,
            this.xrTCapacityDecrement,
            this.xrTManager,
            this.xrTMemberTypeTitle,
            this.xrTName,
            this.xrTID,
            this.xrTIsMother});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTWage
        // 
        this.xrTWage.Dpi = 254F;
        this.xrTWage.Name = "xrTWage";
        this.xrTWage.Text = "xrTWage";
        this.xrTWage.Weight = 0.2253791556144204;
        // 
        // xrTCapacityDecrement
        // 
        this.xrTCapacityDecrement.Dpi = 254F;
        this.xrTCapacityDecrement.Name = "xrTCapacityDecrement";
        this.xrTCapacityDecrement.Text = "xrTCapacityDecrement";
        this.xrTCapacityDecrement.Weight = 0.23753608818277894;
        // 
        // xrTManager
        // 
        this.xrTManager.Dpi = 254F;
        this.xrTManager.Name = "xrTManager";
        this.xrTManager.Text = "xrTManager";
        this.xrTManager.Weight = 0.32329346220731925;
        // 
        // xrTMemberTypeTitle
        // 
        this.xrTMemberTypeTitle.Dpi = 254F;
        this.xrTMemberTypeTitle.Name = "xrTMemberTypeTitle";
        this.xrTMemberTypeTitle.Text = "xrTMemberTypeTitle";
        this.xrTMemberTypeTitle.Weight = 0.23600794356293861;
        // 
        // xrTName
        // 
        this.xrTName.Dpi = 254F;
        this.xrTName.Name = "xrTName";
        this.xrTName.Text = "xrTName";
        this.xrTName.Weight = 0.42981564221826274;
        // 
        // xrTID
        // 
        this.xrTID.Dpi = 254F;
        this.xrTID.Name = "xrTID";
        this.xrTID.Text = "xrTID";
        this.xrTID.Weight = 0.19130288778303653;
        // 
        // xrTIsMother
        // 
        this.xrTIsMother.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.checkEditIsMother});
        this.xrTIsMother.Dpi = 254F;
        this.xrTIsMother.Name = "xrTIsMother";
        this.xrTIsMother.Weight = 0.21300451346115279;
        // 
        // checkEditIsMother
        // 
        this.checkEditIsMother.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.checkEditIsMother.Dpi = 254F;
        this.checkEditIsMother.Location = new System.Drawing.Point(85, 21);
        this.checkEditIsMother.Name = "checkEditIsMother";
        this.checkEditIsMother.Size = new System.Drawing.Size(42, 42);
        this.checkEditIsMother.StylePriority.UseBorders = false;
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
            this.xrTableCell2,
            this.xrTableCell1,
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
        this.xrTableCell27.Text = "متراژ دستمزد";
        this.xrTableCell27.Weight = 0.45034352862135341;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "متراژ کسر ظرفیت";
        this.xrTableCell2.Weight = 0.475586340780739;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "مدیر مسئول";
        this.xrTableCell1.Weight = 0.64729823852592938;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Text = "نوع مجری";
        this.xrTableCell31.Weight = 0.47253991452260025;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = "نام";
        this.xrTableCell32.Weight = 0.85923341737346248;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.Text = "کد عضویت";
        this.xrTableCell33.Weight = 0.38129756521897296;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.Text = "مجری مادر";
        this.xrTableCell34.Weight = 0.43036349033006971;
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
        this.xrLabel12.Location = new System.Drawing.Point(1640, 11);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(177, 56);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "مجری";
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
            this.GroupFooter1});
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        this.DetailReport.PrintOnEmptyDataSource = false;
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel14});
        this.Detail1.Dpi = 254F;
        this.Detail1.Height = 85;
        this.Detail1.Name = "Detail1";
        this.Detail1.PrintOnEmptyDataSource = false;
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
            this.xrTNo,
            this.xrTAMemberTypeTitle,
            this.xrTAName,
            this.xrTMeOPersonIdeId});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTNo
        // 
        this.xrTNo.Dpi = 254F;
        this.xrTNo.Name = "xrTNo";
        this.xrTNo.Text = "xrTNo";
        this.xrTNo.Weight = 0.75235624954189473;
        // 
        // xrTAMemberTypeTitle
        // 
        this.xrTAMemberTypeTitle.Dpi = 254F;
        this.xrTAMemberTypeTitle.Name = "xrTAMemberTypeTitle";
        this.xrTAMemberTypeTitle.Text = "xrTAMemberTypeTitle";
        this.xrTAMemberTypeTitle.Weight = 0.62755164650969264;
        // 
        // xrTAName
        // 
        this.xrTAName.Dpi = 254F;
        this.xrTAName.Name = "xrTAName";
        this.xrTAName.Text = "xrTAName";
        this.xrTAName.Weight = 1.667458871742737;
        // 
        // xrTMeOPersonIdeId
        // 
        this.xrTMeOPersonIdeId.Dpi = 254F;
        this.xrTMeOPersonIdeId.Name = "xrTMeOPersonIdeId";
        this.xrTMeOPersonIdeId.Text = "xrTMeOPersonIdeId";
        this.xrTMeOPersonIdeId.Weight = 0.41899064964915633;
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
            this.xrTable1,
            this.xrPanel6});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.Location = new System.Drawing.Point(30, 10);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.Size = new System.Drawing.Size(1780, 175);
        this.xrPanel2.StylePriority.UseBorderColor = false;
        this.xrPanel2.StylePriority.UseBorders = false;
        this.xrPanel2.StylePriority.UseBorderWidth = false;
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
            this.xrTableCell6,
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
        this.xrTableCell3.Text = "شماره پروانه";
        this.xrTableCell3.Weight = 0.91068455417677274;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "نوع نماینده";
        this.xrTableCell6.Weight = 0.761565960447111;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "نام";
        this.xrTableCell4.Weight = 2.020603153890459;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "کد عضویت";
        this.xrTableCell5.Weight = 0.50393697629681722;
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
        this.xrLabel1.Location = new System.Drawing.Point(1463, 11);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.Size = new System.Drawing.Size(283, 42);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "نماینده";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel18});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.Height = 15;
        this.GroupFooter1.Name = "GroupFooter1";
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
        // XtraReportTSImplementer
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.DetailReport});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}