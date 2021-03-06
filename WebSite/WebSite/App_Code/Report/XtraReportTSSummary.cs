using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTSSummary
/// </summary>
public class XtraReportTSSummary : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;

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
    private XRSubreport xrSubDesigner;
    private XRSubreport xrSubPlansMethod;
    private XRSubreport xrSubBlock;
    private XRSubreport xrSubOwner;
    private XRSubreport xrSubRegisterNo;
    private XRTable xrTable5;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell80;
    private XRTable xrTable6;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell81;
    private XRLabel xrLabel38;
    private XRPageInfo xrPageInfo1;
    private XRSubreport xrSubImplementer;
    private XRSubreport xrSubObserver;
    private XRPageBreak xrPageBreak1;
    private XRSubreport xrSubProject;
    private XRPageBreak xrPageBreak2;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSSummary(int ProjectId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //         

        XtraReportTSProject ReProject = new XtraReportTSProject(ProjectId);
        xrSubProject.ReportSource = ReProject;

        XtraReportTSRegisterNo ReRegNo = new XtraReportTSRegisterNo(ProjectId);
        xrSubRegisterNo.ReportSource = ReRegNo;

        XtraReportTSPlansMethod RePlansM = new XtraReportTSPlansMethod(ProjectId);
        xrSubPlansMethod.ReportSource = RePlansM;

        XtraReportTSBlock ReBlock = new XtraReportTSBlock(ProjectId);
        xrSubBlock.ReportSource = ReBlock;

        XtraReportTSOwner ReOwner = new XtraReportTSOwner(ProjectId);
        xrSubOwner.ReportSource = ReOwner;

        XtraReportTSDesigner ReDesigner = new XtraReportTSDesigner(ProjectId);
        xrSubDesigner.ReportSource = ReDesigner;

        XtraReportTSObserver ReObserver = new XtraReportTSObserver(ProjectId);
        xrSubObserver.ReportSource = ReObserver;

        XtraReportTSImplementer ReImplementer = new XtraReportTSImplementer(ProjectId);
        xrSubImplementer.ReportSource = ReImplementer;
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
            string resourceFileName = "XtraReportTSSummary.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubDesigner = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubPlansMethod = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubBlock = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubOwner = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubRegisterNo = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubImplementer = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubObserver = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrSubProject = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrPageBreak2 = new DevExpress.XtraReports.UI.XRPageBreak();
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
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubDesigner,
            this.xrSubPlansMethod,
            this.xrSubBlock,
            this.xrSubOwner,
            this.xrSubRegisterNo,
            this.xrSubImplementer,
            this.xrSubObserver,
            this.xrPageBreak1,
            this.xrSubProject,
            this.xrPageBreak2});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 1111F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrSubDesigner
            // 
            this.xrSubDesigner.Dpi = 254F;
            this.xrSubDesigner.LocationFloat = new DevExpress.Utils.PointFloat(0F, 762F);
            this.xrSubDesigner.Name = "xrSubDesigner";
            this.xrSubDesigner.SizeF = new System.Drawing.SizeF(1849F, 64F);
            // 
            // xrSubPlansMethod
            // 
            this.xrSubPlansMethod.Dpi = 254F;
            this.xrSubPlansMethod.LocationFloat = new DevExpress.Utils.PointFloat(0F, 318F);
            this.xrSubPlansMethod.Name = "xrSubPlansMethod";
            this.xrSubPlansMethod.SizeF = new System.Drawing.SizeF(1849F, 64F);
            // 
            // xrSubBlock
            // 
            this.xrSubBlock.Dpi = 254F;
            this.xrSubBlock.LocationFloat = new DevExpress.Utils.PointFloat(0F, 487F);
            this.xrSubBlock.Name = "xrSubBlock";
            this.xrSubBlock.SizeF = new System.Drawing.SizeF(1849F, 64F);
            // 
            // xrSubOwner
            // 
            this.xrSubOwner.Dpi = 254F;
            this.xrSubOwner.LocationFloat = new DevExpress.Utils.PointFloat(0F, 635F);
            this.xrSubOwner.Name = "xrSubOwner";
            this.xrSubOwner.SizeF = new System.Drawing.SizeF(1849F, 64F);
            // 
            // xrSubRegisterNo
            // 
            this.xrSubRegisterNo.Dpi = 254F;
            this.xrSubRegisterNo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 148F);
            this.xrSubRegisterNo.Name = "xrSubRegisterNo";
            this.xrSubRegisterNo.SizeF = new System.Drawing.SizeF(1849F, 64F);
            // 
            // xrSubImplementer
            // 
            this.xrSubImplementer.Dpi = 254F;
            this.xrSubImplementer.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1016F);
            this.xrSubImplementer.Name = "xrSubImplementer";
            this.xrSubImplementer.SizeF = new System.Drawing.SizeF(1849F, 64F);
            // 
            // xrSubObserver
            // 
            this.xrSubObserver.Dpi = 254F;
            this.xrSubObserver.LocationFloat = new DevExpress.Utils.PointFloat(0F, 889F);
            this.xrSubObserver.Name = "xrSubObserver";
            this.xrSubObserver.SizeF = new System.Drawing.SizeF(1849F, 64F);
            // 
            // xrPageBreak1
            // 
            this.xrPageBreak1.Dpi = 254F;
            this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 444F);
            this.xrPageBreak1.Name = "xrPageBreak1";
            // 
            // xrSubProject
            // 
            this.xrSubProject.Dpi = 254F;
            this.xrSubProject.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21F);
            this.xrSubProject.Name = "xrSubProject";
            this.xrSubProject.SizeF = new System.Drawing.SizeF(1849F, 64F);
            // 
            // xrPageBreak2
            // 
            this.xrPageBreak2.Dpi = 254F;
            this.xrPageBreak2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 593F);
            this.xrPageBreak2.Name = "xrPageBreak2";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 479F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Dpi = 254F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1849F, 440F);
            // 
            // xrTableRow26
            // 
            this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell79});
            this.xrTableRow26.Dpi = 254F;
            this.xrTableRow26.Name = "xrTableRow26";
            this.xrTableRow26.Weight = 1D;
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
            this.xrTableCell79.Weight = 3D;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.BorderWidth = 1;
            this.xrTable3.Dpi = 254F;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow25,
            this.xrTableRow22});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1829F, 421F);
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
            this.xrTableRow23.Weight = 0.78947368421052644D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell68.Dpi = 254F;
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.StylePriority.UseBorders = false;
            this.xrTableCell68.Weight = 0.65650628758884633D;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell64.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37});
            this.xrTableCell64.Dpi = 254F;
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.StylePriority.UseBorders = false;
            this.xrTableCell64.Weight = 1.7702296336796066D;
            // 
            // xrLabel37
            // 
            this.xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel37.Dpi = 254F;
            this.xrLabel37.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(296F, 169F);
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(495F, 53F);
            this.xrLabel37.StylePriority.UseBorders = false;
            this.xrLabel37.StylePriority.UseFont = false;
            this.xrLabel37.StylePriority.UseTextAlignment = false;
            this.xrLabel37.Text = "خلاصه پرونده";
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
            this.xrTableCell63.Weight = 0.57326407873154717D;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox2.Dpi = 254F;
            this.xrPictureBox2.ImageUrl = "~\\Images\\Reports\\arm for sara report.jpg";
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(21F, 42F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(300F, 250F);
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
            this.xrTableRow25.Weight = 0.15789473684210506D;
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
            this.xrTableCell74.Weight = 0.65650628758884633D;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel35.Dpi = 254F;
            this.xrLabel35.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(254F, 0F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(127F, 42F);
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
            this.xrTableCell75.Weight = 1.7702296336796066D;
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
            this.xrTableCell76.Weight = 0.57326407873154717D;
            // 
            // xrLabel33
            // 
            this.xrLabel33.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel33.Dpi = 254F;
            this.xrLabel33.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(190F, 0F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(156F, 42F);
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
            this.xrTableRow22.Weight = 0.15789473684210531D;
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
            this.xrTableCell67.Weight = 0.65650628758884633D;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel36.Dpi = 254F;
            this.xrLabel36.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(296F, 0F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(85F, 42F);
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
            this.xrTableCell71.Weight = 1.7702296336796066D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel34});
            this.xrTableCell72.Dpi = 254F;
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.StylePriority.UseBorders = false;
            this.xrTableCell72.Weight = 0.57326407873154717D;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel34.Dpi = 254F;
            this.xrLabel34.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(120F, 0F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(220F, 42F);
            this.xrLabel34.StylePriority.UseBorders = false;
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.StylePriority.UseTextAlignment = false;
            this.xrLabel34.Text = ":شماره ویرایش";
            this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.PageFooter.Dpi = 254F;
            this.PageFooter.HeightF = 156F;
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
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1849F, 100F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseBorderWidth = false;
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
            this.xrTableRow27.Dpi = 254F;
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.Weight = 1.0079365079365079D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
            this.xrTableCell80.Dpi = 254F;
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.Weight = 3D;
            // 
            // xrTable6
            // 
            this.xrTable6.BorderWidth = 1;
            this.xrTable6.Dpi = 254F;
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
            this.xrTable6.SizeF = new System.Drawing.SizeF(1829F, 80F);
            this.xrTable6.StylePriority.UseBorderWidth = false;
            // 
            // xrTableRow28
            // 
            this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81});
            this.xrTableRow28.Dpi = 254F;
            this.xrTableRow28.Name = "xrTableRow28";
            this.xrTableRow28.Weight = 1D;
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrPageInfo1});
            this.xrTableCell81.Dpi = 254F;
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.Weight = 3D;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel38.Dpi = 254F;
            this.xrLabel38.Font = new System.Drawing.Font("Tahoma", 8F);
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(286F, 11F);
            this.xrLabel38.Multiline = true;
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(1524F, 64F);
            this.xrLabel38.StylePriority.UseBorders = false;
            this.xrLabel38.StylePriority.UseFont = false;
            this.xrLabel38.StylePriority.UseTextAlignment = false;
            this.xrLabel38.Text = "سازمان نظام مهندسی ساختمان استان فارس - واحد خدمات مهندسی";
            this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPageInfo1.Dpi = 254F;
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(32F, 11F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(64F, 48F);
            this.xrPageInfo1.StylePriority.UseBorders = false;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Dpi = 254F;
            this.topMarginBand1.HeightF = 150F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Dpi = 254F;
            this.bottomMarginBand1.HeightF = 203F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // XtraReportTSSummary
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(155, 34, 150, 203);
            this.PageHeight = 2969;
            this.PageWidth = 2101;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "11.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
