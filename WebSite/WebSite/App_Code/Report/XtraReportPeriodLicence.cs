using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportPeriodLicence
/// </summary>
public class XtraReportPeriodLicence : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private System.Data.DataTable dtPeriod;//= new TSP.DataManager.AccountingAccountManager();
    private XRLabel xrLabelFatherName;
    private XRLabel xrLabelIdNo;
    private XRLabel xrLabelDate;
    private XRLabel xrLabelName;
    private XRLabel xrLabelCommManager;
    private XRLabel xrLabelMaskanManager;
    private XRPictureBox xrPictureBox2;

    
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportPeriodLicence()
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

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "XtraReportPeriodLicence.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabelDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelIdNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelFatherName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelMaskanManager = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelCommManager = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.Detail.BorderWidth = 1;
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2,
            this.xrLabelCommManager,
            this.xrLabelMaskanManager,
            this.xrLabelName,
            this.xrLabelDate,
            this.xrLabelIdNo});
        this.Detail.Dpi = 254F;
        this.Detail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Detail.Height = 2391;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.StylePriority.UseBorders = false;
        this.Detail.StylePriority.UseBorderWidth = false;
        this.Detail.StylePriority.UseFont = false;
        this.Detail.StylePriority.UseTextAlignment = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelDate
        // 
        this.xrLabelDate.Dpi = 254F;
        this.xrLabelDate.Location = new System.Drawing.Point(190, 466);
        this.xrLabelDate.Name = "xrLabelDate";
        this.xrLabelDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelDate.Size = new System.Drawing.Size(351, 61);
        this.xrLabelDate.StylePriority.UseTextAlignment = false;
        this.xrLabelDate.Text = "1389/05/16";
        this.xrLabelDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelIdNo
        // 
        this.xrLabelIdNo.Dpi = 254F;
        this.xrLabelIdNo.Location = new System.Drawing.Point(64, 402);
        this.xrLabelIdNo.Name = "xrLabelIdNo";
        this.xrLabelIdNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelIdNo.Size = new System.Drawing.Size(466, 64);
        this.xrLabelIdNo.StylePriority.UseTextAlignment = false;
        this.xrLabelIdNo.Text = "0658";
        this.xrLabelIdNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelFatherName
        // 
        this.xrLabelFatherName.Location = new System.Drawing.Point(12, 8);
        this.xrLabelFatherName.Name = "xrLabelFatherName";
        this.xrLabelFatherName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelFatherName.Size = new System.Drawing.Size(85, 25);
        this.xrLabelFatherName.StylePriority.UseTextAlignment = false;
        this.xrLabelFatherName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelName
        // 
        this.xrLabelName.Dpi = 254F;
        this.xrLabelName.Location = new System.Drawing.Point(1968, 804);
        this.xrLabelName.Name = "xrLabelName";
        this.xrLabelName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelName.Size = new System.Drawing.Size(423, 64);
        this.xrLabelName.StylePriority.UseTextAlignment = false;
        this.xrLabelName.Text = "علی رضا صمیمی";
        this.xrLabelName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelMaskanManager
        // 
        this.xrLabelMaskanManager.Dpi = 254F;
        this.xrLabelMaskanManager.Location = new System.Drawing.Point(656, 1736);
        this.xrLabelMaskanManager.Name = "xrLabelMaskanManager";
        this.xrLabelMaskanManager.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelMaskanManager.Size = new System.Drawing.Size(444, 64);
        this.xrLabelMaskanManager.StylePriority.UseTextAlignment = false;
        this.xrLabelMaskanManager.Text = "محمد رضا دیهیمی";
        this.xrLabelMaskanManager.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelCommManager
        // 
        this.xrLabelCommManager.Dpi = 254F;
        this.xrLabelCommManager.Location = new System.Drawing.Point(1757, 1736);
        this.xrLabelCommManager.Name = "xrLabelCommManager";
        this.xrLabelCommManager.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelCommManager.Size = new System.Drawing.Size(444, 64);
        this.xrLabelCommManager.StylePriority.UseTextAlignment = false;
        this.xrLabelCommManager.Text = "محمد رضا راهنما";
        this.xrLabelCommManager.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageUrl = "C:\\Documents and Settings\\Reza\\Desktop\\New Folder\\Certificate.jpg";
        this.xrPictureBox2.Location = new System.Drawing.Point(0, 169);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.Size = new System.Drawing.Size(2964, 2032);
        // 
        // XtraReportPeriodLicence
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.Dpi = 254F;
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.Name = "XtraReportPeriodLicence";
        this.PageHeight = 2101;
        this.PageWidth = 2969;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
