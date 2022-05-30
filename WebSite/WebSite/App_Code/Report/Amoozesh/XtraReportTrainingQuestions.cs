using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTrainingQuestions
/// </summary>
public class XtraReportTrainingQuestions : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private XRLabel lblQuestion;
    private XRPictureBox PictureBox1;
    private System.Data.DataTable dt;
    private XRLabel lblChoice1;
    private XRLabel lblChoice2;
    private XRLabel lblChoice3;
    private XRLabel lblChoice4;
    private XRPanel xrPanel1;
    private XRLabel lblTitle;
    private XRPictureBox xrPictureBox2;
    private XRPanel xrPanel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTrainingQuestions(String FilterExpression)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        TSP.DataManager.TrainingQuestionsManager TrainingQuestionsManager = new TSP.DataManager.TrainingQuestionsManager();
        System.Data.DataView Data = TrainingQuestionsManager.SelectTrainingQuestionsWithAnswers(FilterExpression);

        //for (int i = 0; i < Data.Rows.Count; i++)
        //{
        //    Data.Rows[i].BeginEdit();
        //    Data.Rows[i]["QuText"] = "سوال " + (i + 1) + ") " + Data.Rows[i]["QuText"];
        //    Data.Rows[i].EndEdit();
        //}

        lblQuestion.DataBindings.Add("Text", Data, "QuText");
        PictureBox1.DataBindings.Add("Text", Data, "FileUrl");
        lblChoice1.DataBindings.Add("Text", Data, "ChoiceText1");
        lblChoice2.DataBindings.Add("Text", Data, "ChoiceText2");
        lblChoice3.DataBindings.Add("Text", Data, "ChoiceText3");
        lblChoice4.DataBindings.Add("Text", Data, "ChoiceText4");           

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
        string resourceFileName = "XtraReportTrainingQuestions.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblChoice4 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblChoice3 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblChoice2 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblChoice1 = new DevExpress.XtraReports.UI.XRLabel();
        this.PictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblQuestion = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
        this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.Detail.BorderWidth = 1;
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
        this.Detail.HeightF = 215F;
        this.Detail.KeepTogether = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.StylePriority.UseBorderDashStyle = false;
        this.Detail.StylePriority.UseBorders = false;
        this.Detail.StylePriority.UseBorderWidth = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblChoice4
        // 
        this.lblChoice4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblChoice4.LocationFloat = new DevExpress.Utils.PointFloat(126.1822F, 136.3244F);
        this.lblChoice4.Name = "lblChoice4";
        this.lblChoice4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblChoice4.SizeF = new System.Drawing.SizeF(538.3179F, 22.99998F);
        this.lblChoice4.StylePriority.UseBorders = false;
        this.lblChoice4.StylePriority.UseTextAlignment = false;
        this.lblChoice4.Text = " ";
        this.lblChoice4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblChoice3
        // 
        this.lblChoice3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblChoice3.LocationFloat = new DevExpress.Utils.PointFloat(126.1822F, 113.3244F);
        this.lblChoice3.Name = "lblChoice3";
        this.lblChoice3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblChoice3.SizeF = new System.Drawing.SizeF(538.3179F, 22.99998F);
        this.lblChoice3.StylePriority.UseBorders = false;
        this.lblChoice3.StylePriority.UseTextAlignment = false;
        this.lblChoice3.Text = " ";
        this.lblChoice3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblChoice2
        // 
        this.lblChoice2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblChoice2.LocationFloat = new DevExpress.Utils.PointFloat(126.0417F, 90.3244F);
        this.lblChoice2.Name = "lblChoice2";
        this.lblChoice2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblChoice2.SizeF = new System.Drawing.SizeF(538.4584F, 22.99996F);
        this.lblChoice2.StylePriority.UseBorders = false;
        this.lblChoice2.StylePriority.UseTextAlignment = false;
        this.lblChoice2.Text = " ";
        this.lblChoice2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblChoice1
        // 
        this.lblChoice1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblChoice1.LocationFloat = new DevExpress.Utils.PointFloat(126.0417F, 67.32445F);
        this.lblChoice1.Name = "lblChoice1";
        this.lblChoice1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblChoice1.SizeF = new System.Drawing.SizeF(538.4584F, 22.99999F);
        this.lblChoice1.StylePriority.UseBorders = false;
        this.lblChoice1.StylePriority.UseTextAlignment = false;
        this.lblChoice1.Text = " ";
        this.lblChoice1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // PictureBox1
        // 
        this.PictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 67.32445F);
        this.PictureBox1.Name = "PictureBox1";
        this.PictureBox1.SizeF = new System.Drawing.SizeF(88.54166F, 91.79793F);
        this.PictureBox1.StylePriority.UseBorders = false;
        // 
        // lblQuestion
        // 
        this.lblQuestion.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblQuestion.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblQuestion.LocationFloat = new DevExpress.Utils.PointFloat(10F, 9.999975F);
        this.lblQuestion.Multiline = true;
        this.lblQuestion.Name = "lblQuestion";
        this.lblQuestion.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblQuestion.SizeF = new System.Drawing.SizeF(654.5001F, 57.32447F);
        this.lblQuestion.StylePriority.UseBorders = false;
        this.lblQuestion.StylePriority.UseFont = false;
        this.lblQuestion.StylePriority.UseTextAlignment = false;
        this.lblQuestion.Text = " ";
        this.lblQuestion.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 10F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 10F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Double;
        this.PageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PageHeader.BorderWidth = 1;
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.HeightF = 144.0417F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.StylePriority.UseBorderDashStyle = false;
        this.PageHeader.StylePriority.UseBorders = false;
        this.PageHeader.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrPictureBox2,
            this.lblTitle});
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(50.84874F, 26.04167F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(674F, 118F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // lblTitle
        // 
        this.lblTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(296.3898F, 40.16043F);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTitle.SizeF = new System.Drawing.SizeF(162.5902F, 22.94948F);
        this.lblTitle.StylePriority.UseBorders = false;
        this.lblTitle.StylePriority.UseFont = false;
        this.lblTitle.StylePriority.UseTextAlignment = false;
        this.lblTitle.Text = ".................... :عنوان درس";
        this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.ImageUrl = "~\\Images\\Reports\\arm for sara report.jpg";
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(545.8898F, 10.00001F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(118.1102F, 98.4252F);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrPanel2
        // 
        this.xrPanel2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblQuestion,
            this.PictureBox1,
            this.lblChoice1,
            this.lblChoice2,
            this.lblChoice3,
            this.lblChoice4});
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(51.34885F, 10.00001F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(674F, 193F);
        this.xrPanel2.StylePriority.UseBorders = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10.50011F, 10.00001F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(173.9583F, 21.95834F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "............................................ :نام";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(10.50011F, 40.16044F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(173.9583F, 21.95831F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "............................... :نام خانوادگی";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(10F, 73.95834F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(174.4585F, 21.95832F);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "              /         /               : تاریخ";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // XtraReportTrainingQuestions
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
        this.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Double;
        this.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.DisplayName = "گزارش سوالات امتحانی";
        this.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 10);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
