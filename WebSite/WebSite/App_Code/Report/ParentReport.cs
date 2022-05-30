using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for ParentReport
/// </summary>
public class ParentReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private XRLabel xrLabel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRPictureBox xrPictureBox1;
    private XRPanel xrPanel1;
    private XRLine xrLine1;
    private XRRichText xrRichText1;
    private XRControlStyle FontStyle;
    private XRControlStyle xrControlStyle1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public ParentReport()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
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
    virtual protected  void SetDataBindings(object  dataSource)
    {

    }
    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "ParentReport.resx";
        System.Resources.ResourceManager resources = global::Resources.ParentReport.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.FontStyle = new DevExpress.XtraReports.UI.XRControlStyle();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1,
            this.xrLine1,
            this.xrPanel1,
            this.xrPictureBox1,
            this.xrTable1,
            this.xrLabel1});
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrRichText1
        // 
        this.xrRichText1.Location = new System.Drawing.Point(92, 17);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.Size = new System.Drawing.Size(141, 50);
        // 
        // xrLine1
        // 
        this.xrLine1.Location = new System.Drawing.Point(342, 75);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.Size = new System.Drawing.Size(150, 8);
        // 
        // xrPanel1
        // 
        this.xrPanel1.Location = new System.Drawing.Point(258, 17);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(50, 58);
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Location = new System.Drawing.Point(517, 58);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.Size = new System.Drawing.Size(91, 34);
        // 
        // xrTable1
        // 
        this.xrTable1.Location = new System.Drawing.Point(333, 8);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.Size = new System.Drawing.Size(142, 50);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "xrTableCell1";
        this.xrTableCell1.Weight = 1;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.Weight = 1;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Location = new System.Drawing.Point(500, 17);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.Size = new System.Drawing.Size(125, 33);
        this.xrLabel1.Text = "xrLabel1";
        // 
        // PageHeader
        // 
        this.PageHeader.Height = 30;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageFooter
        // 
        this.PageFooter.Height = 30;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // FontStyle
        // 
        this.FontStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
        this.FontStyle.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.FontStyle.Font = new System.Drawing.Font("Tahoma", 9F);
        this.FontStyle.Name = "FontStyle";
        this.FontStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
        this.xrControlStyle1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrControlStyle1.Name = "xrControlStyle1";
        // 
        // ParentReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Name = "ParentReport";
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.FontStyle,
            this.xrControlStyle1});
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
