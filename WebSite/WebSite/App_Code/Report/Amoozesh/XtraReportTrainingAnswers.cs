using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTrainingAnswers
/// </summary>
public class XtraReportTrainingAnswers : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private XRLabel lblNo;
    private XRLabel xrLabel2;
    private XRLine xrLine1;
    private XRCheckBox chkChoice1;
    private XRCheckBox chkChoice2;
    private XRCheckBox chkChoice3;
    private XRCheckBox chkChoice4;
    private XRLabel xrLabel1;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRPanel xrPanel1;
    private XRPictureBox xrPictureBox2;
    private XRLabel lblTitle;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTrainingAnswers(String FilterExpression)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        TSP.DataManager.TrainingQuAnswersManager TrainingQuAnswersManager = new TSP.DataManager.TrainingQuAnswersManager();
        System.Data.DataView Data = TrainingQuAnswersManager.SelectTrainingQuAnswersForReport(FilterExpression);

        lblNo.DataBindings.Add("Text", Data, "No");
        chkChoice1.DataBindings.Add("Checked", Data, "Choice1");
        chkChoice2.DataBindings.Add("Checked", Data, "Choice2");
        chkChoice3.DataBindings.Add("Checked", Data, "Choice3");
        chkChoice4.DataBindings.Add("Checked", Data, "Choice4");

        this.DataSource = Data;
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
        string resourceFileName = "XtraReportTrainingAnswers.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.chkChoice1 = new DevExpress.XtraReports.UI.XRCheckBox();
        this.chkChoice2 = new DevExpress.XtraReports.UI.XRCheckBox();
        this.chkChoice3 = new DevExpress.XtraReports.UI.XRCheckBox();
        this.chkChoice4 = new DevExpress.XtraReports.UI.XRCheckBox();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.chkChoice4,
            this.chkChoice3,
            this.chkChoice2,
            this.chkChoice1,
            this.lblNo});
        this.Detail.HeightF = 25.00001F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel1,
            this.xrLine1,
            this.xrLabel2});
        this.PageHeader.HeightF = 181.3333F;
        this.PageHeader.Name = "PageHeader";
        // 
        // lblNo
        // 
        this.lblNo.LocationFloat = new DevExpress.Utils.PointFloat(478.5417F, 0F);
        this.lblNo.Name = "lblNo";
        this.lblNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNo.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.lblNo.StylePriority.UseTextAlignment = false;
        this.lblNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(477.5F, 150F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "شماره سوال";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLine1
        // 
        this.xrLine1.BorderWidth = 0;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 175F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(527.5F, 5.291672F);
        this.xrLine1.StylePriority.UseBorderWidth = false;
        // 
        // chkChoice1
        // 
        this.chkChoice1.LocationFloat = new DevExpress.Utils.PointFloat(390.625F, 0F);
        this.chkChoice1.Name = "chkChoice1";
        this.chkChoice1.SizeF = new System.Drawing.SizeF(17.70834F, 23F);
        this.chkChoice1.StylePriority.UseTextAlignment = false;
        this.chkChoice1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // chkChoice2
        // 
        this.chkChoice2.LocationFloat = new DevExpress.Utils.PointFloat(292.7083F, 0F);
        this.chkChoice2.Name = "chkChoice2";
        this.chkChoice2.SizeF = new System.Drawing.SizeF(17.70834F, 23F);
        this.chkChoice2.StylePriority.UseTextAlignment = false;
        this.chkChoice2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // chkChoice3
        // 
        this.chkChoice3.LocationFloat = new DevExpress.Utils.PointFloat(190.5349F, 0F);
        this.chkChoice3.Name = "chkChoice3";
        this.chkChoice3.SizeF = new System.Drawing.SizeF(16.66669F, 23F);
        this.chkChoice3.StylePriority.UseTextAlignment = false;
        this.chkChoice3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // chkChoice4
        // 
        this.chkChoice4.LocationFloat = new DevExpress.Utils.PointFloat(90.625F, 0F);
        this.chkChoice4.Name = "chkChoice4";
        this.chkChoice4.SizeF = new System.Drawing.SizeF(16.66669F, 23F);
        this.chkChoice4.StylePriority.UseTextAlignment = false;
        this.chkChoice4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(350F, 150F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "گزینه 1";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(250F, 150F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "گزینه 2";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(150F, 150F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "گزینه 3";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(50F, 150F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "گزینه 4";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2,
            this.lblTitle});
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(527.5F, 118F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.ImageUrl = "~\\Images\\Reports\\arm for sara report.jpg";
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(399.3897F, 10.00001F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(118.1102F, 98.4252F);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // lblTitle
        // 
        this.lblTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(140.5349F, 45.36877F);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTitle.SizeF = new System.Drawing.SizeF(162.5902F, 22.94948F);
        this.lblTitle.StylePriority.UseBorders = false;
        this.lblTitle.StylePriority.UseFont = false;
        this.lblTitle.StylePriority.UseTextAlignment = false;
        this.lblTitle.Text = ".................... :عنوان درس";
        this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // XtraReportTrainingAnswers
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
