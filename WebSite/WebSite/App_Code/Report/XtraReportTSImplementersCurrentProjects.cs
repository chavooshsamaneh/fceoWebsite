using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportTSImplementersCurrentProjects
/// </summary>
public class XtraReportTSImplementersCurrentProjects : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;

    TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();        

    private XRTable xrTable2;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell79;
    private XRTable xrTable3;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell68;
    private XRTableCell xrTableCell64;
    private XRLabel xrLabel37;
    private XRTableCell xrTableCell63;
    private XRPictureBox xrPictureBox2;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell74;
    private XRLabel xrLabel35;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private XRLabel xrLabel33;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell67;
    private XRLabel xrLabel36;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRLabel xrLabel34;
    private XRTable xrTable5;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell80;
    private XRTable xrTable6;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell81;
    private XRLabel xrLabel38;
    private XRPageInfo xrPageInfo1;
    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRTable xrTable1;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRPanel xrPanel3;
    private XRTable xrTable4;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTSObserversPercent;
    private XRTableCell xrTProjectStatus;
    private XRTableCell xrTDate;
    private XRTableCell xrTRegisteredNo;
    private XRTableCell xrTProjectId;
    private XRTableCell xrTProjectIngridientType;
    private XRTableCell xrTName;
    private WinControlContainer winControlContainer1;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private WinControlContainer winControlContainer2;
    private DevExpress.XtraEditors.LabelControl labelMeOfficeId;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTProjectName;
    private XRTableCell xrTGroupName;
    private XRTableCell xrTDevelopmentPercent;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSImplementersCurrentProjects(int MeOfficeId,int MemberTypeId,string FilterExp)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //         

        labelMeOfficeId.Text = MeOfficeId.ToString();
        System.Data.DataTable dt = ProjectManager.SelectImplementersCurrentProjects(MeOfficeId, MemberTypeId);
        dt.DefaultView.RowFilter = FilterExp;
        this.DataSource = dt;

        xrTName.DataBindings.Add("Text", this.DataSource, "Name");
        xrTProjectIngridientType.DataBindings.Add("Text", this.DataSource, "ProjectIngridientType");
        xrTProjectId.DataBindings.Add("Text", this.DataSource, "ProjectId");
        xrTProjectName.DataBindings.Add("Text", this.DataSource, "ProjectName");
        xrTRegisteredNo.DataBindings.Add("Text", this.DataSource, "RegisteredNo");
        xrTGroupName.DataBindings.Add("Text", this.DataSource, "GroupName");
        xrTDate.DataBindings.Add("Text", this.DataSource, "Date");
        xrTProjectStatus.DataBindings.Add("Text", this.DataSource, "ProjectStatus");
        xrTDevelopmentPercent.DataBindings.Add("Text", this.DataSource, "DevelopmentPercent");
        xrTSObserversPercent.DataBindings.Add("Text", this.DataSource, "SObserversPercent");
        
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
        string resourceFileName = "XtraReportTSImplementersCurrentProjects.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTSObserversPercent = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDevelopmentPercent = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectStatus = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTGroupName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTRegisteredNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectIngridientType = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTName = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.winControlContainer1 = new DevExpress.XtraReports.UI.WinControlContainer();
        this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
        this.winControlContainer2 = new DevExpress.XtraReports.UI.WinControlContainer();
        this.labelMeOfficeId = new DevExpress.XtraEditors.LabelControl();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 95;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel3.BorderWidth = 2;
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.Location = new System.Drawing.Point(0, 0);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.Size = new System.Drawing.Size(1849, 95);
        this.xrPanel3.StylePriority.UseBorders = false;
        this.xrPanel3.StylePriority.UseBorderWidth = false;
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
            this.xrTableRow9});
        this.xrTable4.Size = new System.Drawing.Size(1829, 85);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        this.xrTable4.StylePriority.UseTextAlignment = false;
        this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTSObserversPercent,
            this.xrTDevelopmentPercent,
            this.xrTProjectStatus,
            this.xrTDate,
            this.xrTGroupName,
            this.xrTRegisteredNo,
            this.xrTProjectName,
            this.xrTProjectId,
            this.xrTProjectIngridientType,
            this.xrTName});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTSObserversPercent
        // 
        this.xrTSObserversPercent.Dpi = 254F;
        this.xrTSObserversPercent.Name = "xrTSObserversPercent";
        this.xrTSObserversPercent.Text = "xrTSObserversPercent";
        this.xrTSObserversPercent.Weight = 0.26908119101425337;
        // 
        // xrTDevelopmentPercent
        // 
        this.xrTDevelopmentPercent.Dpi = 254F;
        this.xrTDevelopmentPercent.Name = "xrTDevelopmentPercent";
        this.xrTDevelopmentPercent.Text = "xrTDevelopmentPercent";
        this.xrTDevelopmentPercent.Weight = 0.25130918909785316;
        // 
        // xrTProjectStatus
        // 
        this.xrTProjectStatus.Dpi = 254F;
        this.xrTProjectStatus.Name = "xrTProjectStatus";
        this.xrTProjectStatus.Text = "xrTProjectStatus";
        this.xrTProjectStatus.Weight = 0.43110554007824126;
        // 
        // xrTDate
        // 
        this.xrTDate.Dpi = 254F;
        this.xrTDate.Name = "xrTDate";
        this.xrTDate.Text = "xrTDate";
        this.xrTDate.Weight = 0.32232742658620867;
        // 
        // xrTGroupName
        // 
        this.xrTGroupName.Dpi = 254F;
        this.xrTGroupName.Name = "xrTGroupName";
        this.xrTGroupName.Text = "xrTGroupName";
        this.xrTGroupName.Weight = 0.25434161028491648;
        // 
        // xrTRegisteredNo
        // 
        this.xrTRegisteredNo.Dpi = 254F;
        this.xrTRegisteredNo.Name = "xrTRegisteredNo";
        this.xrTRegisteredNo.Text = "xrTRegisteredNo";
        this.xrTRegisteredNo.Weight = 0.28656925810215594;
        // 
        // xrTProjectName
        // 
        this.xrTProjectName.Dpi = 254F;
        this.xrTProjectName.Name = "xrTProjectName";
        this.xrTProjectName.Text = "xrTProjectName";
        this.xrTProjectName.Weight = 0.46287061723929179;
        // 
        // xrTProjectId
        // 
        this.xrTProjectId.Dpi = 254F;
        this.xrTProjectId.Name = "xrTProjectId";
        this.xrTProjectId.Text = "xrTProjectId";
        this.xrTProjectId.Weight = 0.2152265866436629;
        // 
        // xrTProjectIngridientType
        // 
        this.xrTProjectIngridientType.Dpi = 254F;
        this.xrTProjectIngridientType.Name = "xrTProjectIngridientType";
        this.xrTProjectIngridientType.Text = "xrTProjectIngridientType";
        this.xrTProjectIngridientType.Weight = 0.18375083802687076;
        // 
        // xrTName
        // 
        this.xrTName.Dpi = 254F;
        this.xrTName.Name = "xrTName";
        this.xrTName.Text = "xrTName";
        this.xrTName.Weight = 0.42575289333343125;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 698;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Location = new System.Drawing.Point(0, 0);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
        this.xrTable2.Size = new System.Drawing.Size(1849, 440);
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell79});
        this.xrTableRow26.Dpi = 254F;
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.Weight = 1;
        // 
        // xrTableCell79
        // 
        this.xrTableCell79.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell79.BorderWidth = 2;
        this.xrTableCell79.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrTableCell79.Dpi = 254F;
        this.xrTableCell79.Name = "xrTableCell79";
        this.xrTableCell79.StylePriority.UseBorders = false;
        this.xrTableCell79.StylePriority.UseBorderWidth = false;
        this.xrTableCell79.Weight = 3;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 1;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Location = new System.Drawing.Point(10, 10);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow25,
            this.xrTableRow22});
        this.xrTable3.Size = new System.Drawing.Size(1829, 421);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell68,
            this.xrTableCell64,
            this.xrTableCell63});
        this.xrTableRow23.Dpi = 254F;
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.Weight = 0.78947368421052644;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell68.Dpi = 254F;
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.StylePriority.UseBorders = false;
        this.xrTableCell68.Weight = 0.65650628758884633;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell64.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37});
        this.xrTableCell64.Dpi = 254F;
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.Weight = 1.7702296336796066;
        // 
        // xrLabel37
        // 
        this.xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel37.Location = new System.Drawing.Point(225, 159);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.Size = new System.Drawing.Size(587, 53);
        this.xrLabel37.StylePriority.UseBorders = false;
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "پروژه های در دست اجرا مجری";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell63.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
        this.xrTableCell63.Dpi = 254F;
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.StylePriority.UseBorders = false;
        this.xrTableCell63.Weight = 0.57326407873154717;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageUrl = "~\\Images\\Reports\\arm for sara report.jpg";
        this.xrPictureBox2.Location = new System.Drawing.Point(21, 42);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.Size = new System.Drawing.Size(300, 250);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76});
        this.xrTableRow25.Dpi = 254F;
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 0.15789473684210506;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell74.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35});
        this.xrTableCell74.Dpi = 254F;
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.StylePriority.UseBorders = false;
        this.xrTableCell74.StylePriority.UseTextAlignment = false;
        this.xrTableCell74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell74.Weight = 0.65650628758884633;
        // 
        // xrLabel35
        // 
        this.xrLabel35.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel35.Location = new System.Drawing.Point(254, 0);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.Size = new System.Drawing.Size(127, 42);
        this.xrLabel35.StylePriority.UseBorders = false;
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = ":شماره";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell75.Dpi = 254F;
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseBorders = false;
        this.xrTableCell75.Weight = 1.7702296336796066;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell76.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel33});
        this.xrTableCell76.Dpi = 254F;
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.StylePriority.UseBorders = false;
        this.xrTableCell76.StylePriority.UseTextAlignment = false;
        this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell76.Weight = 0.57326407873154717;
        // 
        // xrLabel33
        // 
        this.xrLabel33.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel33.Location = new System.Drawing.Point(190, 0);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.Size = new System.Drawing.Size(156, 42);
        this.xrLabel33.StylePriority.UseBorders = false;
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = ":شماره فرم";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell67,
            this.xrTableCell71,
            this.xrTableCell72});
        this.xrTableRow22.Dpi = 254F;
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.StylePriority.UseBorders = false;
        this.xrTableRow22.Weight = 0.15789473684210531;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell67.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36});
        this.xrTableCell67.Dpi = 254F;
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseBorders = false;
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell67.Weight = 0.65650628758884633;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel36.Location = new System.Drawing.Point(296, 0);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.Size = new System.Drawing.Size(85, 42);
        this.xrLabel36.StylePriority.UseBorders = false;
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = ":تاریخ";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell71.Dpi = 254F;
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.StylePriority.UseBorders = false;
        this.xrTableCell71.Weight = 1.7702296336796066;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel34});
        this.xrTableCell72.Dpi = 254F;
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseBorders = false;
        this.xrTableCell72.Weight = 0.57326407873154717;
        // 
        // xrLabel34
        // 
        this.xrLabel34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel34.Location = new System.Drawing.Point(120, 0);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.Size = new System.Drawing.Size(220, 42);
        this.xrLabel34.StylePriority.UseBorders = false;
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = ":شماره ویرایش";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel1.BorderWidth = 2;
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel5,
            this.xrTable1});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.Location = new System.Drawing.Point(0, 508);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(1849, 190);
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
            this.winControlContainer1,
            this.winControlContainer2});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // winControlContainer1
        // 
        this.winControlContainer1.Dpi = 254F;
        this.winControlContainer1.Location = new System.Drawing.Point(1640, 11);
        this.winControlContainer1.Name = "winControlContainer1";
        this.winControlContainer1.Size = new System.Drawing.Size(180, 40);
        this.winControlContainer1.WinControl = this.labelControl1;
        // 
        // labelControl1
        // 
        this.labelControl1.Location = new System.Drawing.Point(0, 0);
        this.labelControl1.Name = "labelControl1";
        this.labelControl1.Size = new System.Drawing.Size(66, 13);
        this.labelControl1.TabIndex = 1;
        this.labelControl1.Text = ":کد عضویت";
        // 
        // winControlContainer2
        // 
        this.winControlContainer2.Dpi = 254F;
        this.winControlContainer2.Location = new System.Drawing.Point(1429, 11);
        this.winControlContainer2.Name = "winControlContainer2";
        this.winControlContainer2.Size = new System.Drawing.Size(198, 34);
        this.winControlContainer2.WinControl = this.labelMeOfficeId;
        // 
        // labelMeOfficeId
        // 
        this.labelMeOfficeId.Location = new System.Drawing.Point(0, 0);
        this.labelMeOfficeId.Name = "labelMeOfficeId";
        this.labelMeOfficeId.Size = new System.Drawing.Size(75, 13);
        this.labelMeOfficeId.TabIndex = 0;
        this.labelMeOfficeId.Text = "labelMeOfficeId";
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
            this.xrTableRow8});
        this.xrTable1.Size = new System.Drawing.Size(1829, 110);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell1,
            this.xrTableCell3,
            this.xrTableCell2,
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
        this.xrTableCell27.Text = "درصد پیشرفت کار ناظرین عالی";
        this.xrTableCell27.Weight = 0.36595953725436431;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "درصد پیشرفت کار";
        this.xrTableCell4.Weight = 0.33873110464076572;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "وضعیت پروژه";
        this.xrTableCell5.Weight = 0.58353761978261054;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "تاریخ ایجاد";
        this.xrTableCell1.Weight = 0.4366843659455879;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "گروه ساختمانی";
        this.xrTableCell3.Weight = 0.34446755986877453;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "پلاک ثبتی";
        this.xrTableCell2.Weight = 0.38806461960164029;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Text = "نام پروژه";
        this.xrTableCell31.Weight = 0.62610280787268213;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = "کد پروژه";
        this.xrTableCell32.Weight = 0.29232448273036182;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.Text = "سمت";
        this.xrTableCell33.Weight = 0.24829096712968712;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.Text = "نام مجری";
        this.xrTableCell34.Weight = 0.57262757998468528;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.Height = 169;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 2;
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Location = new System.Drawing.Point(0, 21);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable5.Size = new System.Drawing.Size(1849, 100);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
        this.xrTableRow27.Dpi = 254F;
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1.0079365079365079;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrTableCell80.Dpi = 254F;
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Weight = 3;
        // 
        // xrTable6
        // 
        this.xrTable6.BorderWidth = 1;
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Location = new System.Drawing.Point(10, 10);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable6.Size = new System.Drawing.Size(1829, 80);
        this.xrTable6.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81});
        this.xrTableRow28.Dpi = 254F;
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 1;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrPageInfo1});
        this.xrTableCell81.Dpi = 254F;
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Weight = 3;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel38.Location = new System.Drawing.Point(286, 11);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel38.Size = new System.Drawing.Size(1524, 64);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "سازمان نظام مهندسی ساختمان استان فارس ( واحد عضویت در سازمان )   تلفن:   8-071162" +
            "74194";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Location = new System.Drawing.Point(32, 11);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.Size = new System.Drawing.Size(64, 48);
        this.xrPageInfo1.StylePriority.UseBorders = false;
        // 
        // XtraReportTSImplementersCurrentProjects
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(155, 34, 150, 203);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}