using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportMemberLanguage
/// </summary>
public class XtraReportMemberLanguage : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;


    private XRControlStyle xrControlStyle1;
    private XRPanel xrPanel1;
    private XRLabel xrLabel36;
    private XRPanel xrPanel3;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    private XRPanel xrPanel5;
    private XRTable xrTable2;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRPanel xrPanel6;
    private XRTable xrTable3;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTInActiveName;
    private XRTableCell xrTDescription;
    private XRTableCell xrTLqName;
    private XRTableCell xrTLanName;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportMemberLanguage(int MeId, int MReId, bool IsMeTemp)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        if (!IsMeTemp)
        {
            TSP.DataManager.MemberLanguageManager MeLanManager = new TSP.DataManager.MemberLanguageManager();
            MeLanManager.FindByMeRequest(MeId, MReId, -1);
            xrTDescription.DataBindings.Add("Text", MeLanManager.DataTable, "Description");
            xrTLanName.DataBindings.Add("Text", MeLanManager.DataTable, "LanName");
            xrTLqName.DataBindings.Add("Text", MeLanManager.DataTable, "LqName");
            xrTInActiveName.DataBindings.Add("Text", MeLanManager.DataTable, "InActiveName");
            this.DataSource = MeLanManager.DataTable;
        }
        else
        {
            TSP.DataManager.TempMemberLanguageManager MeLanManager = new TSP.DataManager.TempMemberLanguageManager();
            MeLanManager.FindByRequest(-1, MReId);
            xrTDescription.DataBindings.Add("Text", MeLanManager.DataTable, "Description");
            xrTLanName.DataBindings.Add("Text", MeLanManager.DataTable, "LanName");
            xrTLqName.DataBindings.Add("Text", MeLanManager.DataTable, "LqName");
            xrTInActiveName.DataBindings.Add("Text", MeLanManager.DataTable, "InActiveName");
            this.DataSource = MeLanManager.DataTable;
        }
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
        string resourceFileName = "XtraReportMemberLanguage.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel6 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTInActiveName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDescription = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTLqName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTLanName = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel6});
        this.Detail.Dpi = 254F;
        this.Detail.Font = new System.Drawing.Font("Tahoma", 8F);
        this.Detail.Height = 85;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.StylePriority.UseFont = false;
        this.Detail.StylePriority.UseTextAlignment = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPanel6
        // 
        this.xrPanel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel6.BorderWidth = 2;
        this.xrPanel6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrPanel6.Dpi = 254F;
        this.xrPanel6.Location = new System.Drawing.Point(0, 0);
        this.xrPanel6.Name = "xrPanel6";
        this.xrPanel6.Size = new System.Drawing.Size(1849, 85);
        this.xrPanel6.StylePriority.UseBorders = false;
        this.xrPanel6.StylePriority.UseBorderWidth = false;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 1;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.Location = new System.Drawing.Point(10, 0);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable3.Size = new System.Drawing.Size(1829, 85);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTInActiveName,
            this.xrTDescription,
            this.xrTLqName,
            this.xrTLanName});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTInActiveName
        // 
        this.xrTInActiveName.Dpi = 254F;
        this.xrTInActiveName.Name = "xrTInActiveName";
        this.xrTInActiveName.Text = "xrTInActiveName";
        this.xrTInActiveName.Weight = 0.47084484217446776;
        // 
        // xrTDescription
        // 
        this.xrTDescription.Dpi = 254F;
        this.xrTDescription.Name = "xrTDescription";
        this.xrTDescription.Text = "xrTDescription";
        this.xrTDescription.Weight = 2.4228779509818184;
        // 
        // xrTLqName
        // 
        this.xrTLqName.Dpi = 254F;
        this.xrTLqName.Name = "xrTLqName";
        this.xrTLqName.Text = "xrTLqName";
        this.xrTLqName.Weight = 0.45211095444297617;
        // 
        // xrTLanName
        // 
        this.xrTLanName.Dpi = 254F;
        this.xrTLanName.Name = "xrTLanName";
        this.xrTLanName.Text = "xrTLanName";
        this.xrTLanName.Weight = 1.165471078489321;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 174;
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
            this.xrTable2});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.Location = new System.Drawing.Point(0, 0);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(1849, 174);
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
            this.xrLabel36});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel36.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel36.Location = new System.Drawing.Point(1450, 11);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.Size = new System.Drawing.Size(360, 55);
        this.xrLabel36.StylePriority.UseBorders = false;
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseForeColor = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = "آشنایی با دیگر زبان ها";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.Location = new System.Drawing.Point(10, 90);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable2.Size = new System.Drawing.Size(1829, 85);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell11,
            this.xrTableCell14,
            this.xrTableCell15});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "وضعیت";
        this.xrTableCell13.Weight = 0.47084484217446776;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "توضیحات";
        this.xrTableCell11.Weight = 2.42180957047511;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "میزان دانش";
        this.xrTableCell14.Weight = 0.45071279321863567;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "نام زبان";
        this.xrTableCell15.Weight = 1.16793762022037;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.Height = 40;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageFooter.Visible = false;
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPanel3.BorderWidth = 2;
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.Location = new System.Drawing.Point(0, 0);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.Size = new System.Drawing.Size(1849, 40);
        this.xrPanel3.StylePriority.UseBorders = false;
        this.xrPanel3.StylePriority.UseBorderWidth = false;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.Height = 40;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPanel4.BorderWidth = 2;
        this.xrPanel4.Dpi = 254F;
        this.xrPanel4.Location = new System.Drawing.Point(0, 0);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.Size = new System.Drawing.Size(1849, 40);
        this.xrPanel4.StylePriority.UseBorders = false;
        this.xrPanel4.StylePriority.UseBorderWidth = false;
        // 
        // XtraReportMemberLanguage
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
