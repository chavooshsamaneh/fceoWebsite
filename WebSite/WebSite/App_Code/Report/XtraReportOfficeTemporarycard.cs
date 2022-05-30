using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportOfficeTemporarycard
/// </summary>
public class XtraReportOfficeTemporarycard : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox xrPictureBox2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel lblOfficeName;
    private XRLabel xrLabel6;
    private XRLabel lblOfficeMemberId;
    private XRLabel xrLabel8;
    private XRLabel lblDate;
    private XRLabel xrLabel10;
    private XRLabel xrLabel11;
    private XRLabel lblYear;
    private XRLabel xrLabel14;
    private XRLabel xrLabel12;
    private XRLabel lblGovTitle;
    private XRLabel lblGovName;
    private XRPictureBox xrPicSign;
    private XRLabel xrlblWarning;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportOfficeTemporarycard(int OfficeId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new TSP.DataManager.PrintAssignerSettingManager();
        PrintAssignerSettingManager.FindByPrintTypeId((int)TSP.DataManager.PrintType.OfficeTempreryCardPrinting);
        if (PrintAssignerSettingManager.Count > 0)
        {
            lblGovName.Text = "با تشکر - " + PrintAssignerSettingManager[0]["GmnName"].ToString();
            lblGovTitle.Text = PrintAssignerSettingManager[0]["GmtName"].ToString();
            if (string.IsNullOrEmpty(PrintAssignerSettingManager[0]["SignUrl"].ToString()))
            {
                xrPicSign.Visible = false;
                xrlblWarning.Visible = false;
            }
            else
            {
                xrPicSign.Visible = true;
                xrlblWarning.Visible = true;
                xrPicSign.ImageUrl = PrintAssignerSettingManager[0]["SignUrl"].ToString();
            }
        }

        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        OfficeManager.FindByCode(OfficeId);
        if (OfficeManager.Count > 0)
        {
            if (!Utility.IsDBNullOrNullValue(OfficeManager[0]["MeNo"]))
                lblOfficeMemberId.Text = OfficeManager[0]["MeNo"].ToString();
            lblOfficeName.Text = OfficeManager[0]["OfName"].ToString();
            lblDate.Text = OfficeManager[0]["CreateDate"].ToString();
            //  lblYear.Text = OfficeManager[0]["FileDate"].ToString();
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
        string resourceFileName = "XtraReportOfficeTemporarycard.resx";
        System.Resources.ResourceManager resources = global::Resources.XtraReportOfficeTemporarycard.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblGovTitle = new DevExpress.XtraReports.UI.XRLabel();
        this.lblGovName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPicSign = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblYear = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblOfficeMemberId = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblOfficeName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrlblWarning = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblGovTitle,
            this.lblGovName,
            this.xrPicSign,
            this.xrLabel12,
            this.xrLabel14,
            this.lblYear,
            this.xrLabel11,
            this.xrLabel10,
            this.lblDate,
            this.xrLabel8,
            this.lblOfficeMemberId,
            this.xrLabel6,
            this.lblOfficeName,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1295.021F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblGovTitle
        // 
        this.lblGovTitle.Dpi = 254F;
        this.lblGovTitle.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblGovTitle.LocationFloat = new DevExpress.Utils.PointFloat(73.63839F, 1185.887F);
        this.lblGovTitle.Name = "lblGovTitle";
        this.lblGovTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblGovTitle.SizeF = new System.Drawing.SizeF(758.3403F, 75.87488F);
        this.lblGovTitle.StylePriority.UseFont = false;
        this.lblGovTitle.StylePriority.UseTextAlignment = false;
        this.lblGovTitle.Text = "رئیس سازمان نظام مهندسی ساختمان استان فارس";
        this.lblGovTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // lblGovName
        // 
        this.lblGovName.Dpi = 254F;
        this.lblGovName.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblGovName.LocationFloat = new DevExpress.Utils.PointFloat(190.4104F, 1110.012F);
        this.lblGovName.Name = "lblGovName";
        this.lblGovName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblGovName.SizeF = new System.Drawing.SizeF(522.0602F, 75.87512F);
        this.lblGovName.StylePriority.UseFont = false;
        this.lblGovName.StylePriority.UseTextAlignment = false;
        this.lblGovName.Text = "با تشکر - حسین پور اسدی";
        this.lblGovName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPicSign
        // 
        this.xrPicSign.Dpi = 254F;
        this.xrPicSign.Image = ((System.Drawing.Image)(resources.GetObject("xrPicSign.Image")));
        this.xrPicSign.LocationFloat = new DevExpress.Utils.PointFloat(289.1382F, 950.2615F);
        this.xrPicSign.Name = "xrPicSign";
        this.xrPicSign.SizeF = new System.Drawing.SizeF(300F, 310F);
        this.xrPicSign.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(1167.659F, 811.1063F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(82.02087F, 111.3369F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "سال";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel14
        // 
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(114.6171F, 811.1066F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(682.625F, 111.3369F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = ".می باشد";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblYear
        // 
        this.lblYear.Dpi = 254F;
        this.lblYear.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblYear.LocationFloat = new DevExpress.Utils.PointFloat(797.2419F, 811.1063F);
        this.lblYear.Name = "lblYear";
        this.lblYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblYear.SizeF = new System.Drawing.SizeF(370.4165F, 111.3365F);
        this.lblYear.StylePriority.UseFont = false;
        this.lblYear.StylePriority.UseTextAlignment = false;
        this.lblYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(114.617F, 705.0616F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1135.063F, 106.0447F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "استان  فارس  پذیرفته  شده  است  و  اعتبار  عضویت  این  شرکت  تا پایان";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(114.617F, 588.4337F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(788.4589F, 116.628F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "به عضویت حقوقی سازمان نظام مهندسی ساختمان";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblDate
        // 
        this.lblDate.Dpi = 254F;
        this.lblDate.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblDate.LocationFloat = new DevExpress.Utils.PointFloat(903.0759F, 588.4334F);
        this.lblDate.Name = "lblDate";
        this.lblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDate.SizeF = new System.Drawing.SizeF(346.6036F, 116.6284F);
        this.lblDate.StylePriority.UseFont = false;
        this.lblDate.StylePriority.UseTextAlignment = false;
        this.lblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(114.617F, 477.0969F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(145.5211F, 111.3365F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "در تاریخ";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblOfficeMemberId
        // 
        this.lblOfficeMemberId.Dpi = 254F;
        this.lblOfficeMemberId.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblOfficeMemberId.LocationFloat = new DevExpress.Utils.PointFloat(260.1382F, 477.0966F);
        this.lblOfficeMemberId.Name = "lblOfficeMemberId";
        this.lblOfficeMemberId.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblOfficeMemberId.SizeF = new System.Drawing.SizeF(714.3745F, 111.3367F);
        this.lblOfficeMemberId.StylePriority.UseFont = false;
        this.lblOfficeMemberId.StylePriority.UseTextAlignment = false;
        this.lblOfficeMemberId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(974.5129F, 477.0966F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(275.1669F, 111.3367F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "به شماره عضویت";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblOfficeName
        // 
        this.lblOfficeName.Dpi = 254F;
        this.lblOfficeName.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblOfficeName.LocationFloat = new DevExpress.Utils.PointFloat(114.617F, 365.76F);
        this.lblOfficeName.Name = "lblOfficeName";
        this.lblOfficeName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblOfficeName.SizeF = new System.Drawing.SizeF(1010.708F, 111.3365F);
        this.lblOfficeName.StylePriority.UseFont = false;
        this.lblOfficeName.StylePriority.UseTextAlignment = false;
        this.lblOfficeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(1125.325F, 365.76F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(124.3541F, 111.3367F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "شرکت";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("B Nazanin", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(114.617F, 241.1941F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1135.063F, 124.5659F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = ": بدینوسیله گواهی میشود که";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("B Nazanin", 17F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
            | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(8.995413F, 98.10755F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1299.104F, 124.5659F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "کارت موقت عضویت حقوقی";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("B Nazanin", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(8.995413F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1299.104F, 98.10753F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "به نام خدا";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 104F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.TopMargin.Visible = false;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 203F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.BottomMargin.Visible = false;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(8.995413F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(1299.104F, 341.3125F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(7.62F, 65.00008F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(1299.104F, 100.5417F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 341F;
        this.PageHeader.Name = "PageHeader";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblWarning,
            this.xrPictureBox2});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 167F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrlblWarning
        // 
        this.xrlblWarning.Dpi = 254F;
        this.xrlblWarning.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrlblWarning.LocationFloat = new DevExpress.Utils.PointFloat(305.7388F, 0F);
        this.xrlblWarning.Name = "xrlblWarning";
        this.xrlblWarning.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrlblWarning.SizeF = new System.Drawing.SizeF(671.3139F, 64.99999F);
        this.xrlblWarning.StylePriority.UseFont = false;
        this.xrlblWarning.StylePriority.UseTextAlignment = false;
        this.xrlblWarning.Text = "نامه امضاء شده بدون مهر برجسته فاقد اعتبار است";
        this.xrlblWarning.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // XtraReportOfficeTemporarycard
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.Margins = new System.Drawing.Printing.Margins(79, 89, 104, 203);
        this.PageHeight = 2101;
        this.PageWidth = 1481;
        this.PaperKind = System.Drawing.Printing.PaperKind.A5;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "11.2";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
