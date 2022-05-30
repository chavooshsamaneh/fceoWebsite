using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Members
{
    public partial class MemberCardRequestReport : DevExpress.XtraReports.UI.XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private XRLabel xrLabel14;
        private XRLabel xrLabel13;
        private XRLabel xrLabel12;
        private XRLabel xrLabel11;
        private XRLabel xrLabel10;
        private XRLabel xrLabel9;
        private XRLabel xrLabel8;
        private XRLabel xrLabel7;
        private XRLabel xrLabel6;
        private XRLabel xrLabel5;
        private XRLabel xrLabel4;
        private XRLabel xrLabel3;
        private XRLabel xrLabel2;
        private XRLabel xrLabel1;
        private XRControlStyle xrControlStyle1;
        private XRCheckBox xrCheckBox1;
        private XRLabel xrLabel23;
        private XRCheckBox xrCheckBox6;
        private XRLabel xrLabel22;
        private XRCheckBox xrCheckBox5;
        private XRLabel xrLabel21;
        private XRCheckBox xrCheckBox4;
        private XRLabel xrLabel20;
        private XRCheckBox xrCheckBox3;
        private XRLabel xrLabel19;
        private XRCheckBox xrCheckBox2;
        private XRLabel xrLabel18;
        private XRLabel xrLabel30;
        private XRLabel xrLabel29;
        private XRLabel xrLabel28;
        private XRLabel xrLabel27;
        private XRLabel xrLabel26;
        private XRLabel lblGovTitle;
        private XRLabel xrLabel24;
        private XRLabel xrLabel17;
        private XRLabel xrLabel15;
        private XRLabel xrLabel31;
        private XRLabel lblName;
        private XRLabel xrLabel16;
        private XRLabel lblMemberShipDate;
        private XRLabel lblBirthDay;
        private XRLabel lblIssuePlace;
        private XRLabel lblIdNo;
        private XRLabel lblMajor;
        private XRLabel lblCurrentDateTime;
        private XRLabel lblDateArchive;
        private XRLabel lblMeId;
        private XRPictureBox xrPictureBox2;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        int meid, userid;
        public MemberCardRequestReport(int MeId, int UserId)
        {
            InitializeComponent();

            meid = MeId;
            userid = UserId;
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new DataManager.PrintAssignerSettingManager();
            PrintAssignerSettingManager.FindByPrintTypeId((int)TSP.DataManager.PrintType.MemberCartRequestPrinting);
            if (PrintAssignerSettingManager.Count > 0)
            {
                //    lblGovName.Text = "با تشکر - " + PrintAssignerSettingManager[0]["GmnName"].ToString();
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

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 1)
            {
                lblName.Text = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                lblMajor.Text = MemberManager[0]["LastMjName"].ToString();
                lblIdNo.Text = MemberManager[0]["IdNo"].ToString();
                lblIssuePlace.Text = MemberManager[0]["IssuePlace"].ToString();
                lblBirthDay.Text = MemberManager[0]["BirhtDate"].ToString();
                lblMemberShipDate.Text = MemberManager[0]["MembershipDate"].ToString();
                lblMeId.Text = MemberManager[0]["MeId"].ToString();
                lblDateArchive.Text = MemberManager[0]["DefaultMjInquerySaveDate"].ToString();
                lblCurrentDateTime.Text = "تاریخ " + TSP.DataManager.Utility.GetDateOfToday() + " ساعت " + TSP.DataManager.Utility.GetCurrentTime();
                if (!TSP.DataManager.Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
                    Pic.ImageUrl = (MemberManager[0]["ImageUrl"].ToString());

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberCardRequestReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.Pic = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCurrentDateTime = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDateArchive = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMeId = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMemberShipDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblBirthDay = new DevExpress.XtraReports.UI.XRLabel();
            this.lblIssuePlace = new DevExpress.XtraReports.UI.XRLabel();
            this.lblIdNo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMajor = new DevExpress.XtraReports.UI.XRLabel();
            this.lblName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblGovTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCheckBox6 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCheckBox5 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCheckBox4 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCheckBox3 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCheckBox2 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCheckBox1 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblWarning = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPicSign = new DevExpress.XtraReports.UI.XRPictureBox();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.MeId = new DevExpress.XtraReports.Parameters.Parameter();
            this.UserId = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Pic,
            this.xrLabel34,
            this.xrLabel33,
            this.xrLabel32,
            this.lblCurrentDateTime,
            this.lblDateArchive,
            this.lblMeId,
            this.lblMemberShipDate,
            this.lblBirthDay,
            this.lblIssuePlace,
            this.lblIdNo,
            this.lblMajor,
            this.lblName,
            this.xrLabel16,
            this.xrLabel31,
            this.xrLabel17,
            this.xrLabel15,
            this.xrLabel30,
            this.xrLabel29,
            this.xrLabel28,
            this.xrLabel27,
            this.xrLabel26,
            this.lblGovTitle,
            this.xrLabel24,
            this.xrLabel23,
            this.xrCheckBox6,
            this.xrLabel22,
            this.xrCheckBox5,
            this.xrLabel21,
            this.xrCheckBox4,
            this.xrLabel20,
            this.xrCheckBox3,
            this.xrLabel19,
            this.xrCheckBox2,
            this.xrLabel18,
            this.xrCheckBox1,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrlblWarning,
            this.xrPicSign});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 1497.542F;
            this.Detail.KeepTogether = true;
            this.Detail.KeepTogetherWithDetailReports = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseTextAlignment = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // Pic
            // 
            this.Pic.Dpi = 254F;
            this.Pic.LocationFloat = new DevExpress.Utils.PointFloat(75.87308F, 0F);
            this.Pic.Name = "Pic";
            this.Pic.SizeF = new System.Drawing.SizeF(158.75F, 187.6984F);
            this.Pic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabel34.Dpi = 254F;
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(81.87321F, 1271.006F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(1296.128F, 5F);
            this.xrLabel34.StylePriority.UseBorders = false;
            // 
            // xrLabel33
            // 
            this.xrLabel33.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabel33.Dpi = 254F;
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(81.87311F, 1123.101F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(1296.128F, 5F);
            this.xrLabel33.StylePriority.UseBorders = false;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabel32.Dpi = 254F;
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(81.87307F, 905.7885F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(1296.128F, 5F);
            this.xrLabel32.StylePriority.UseBorders = false;
            // 
            // lblCurrentDateTime
            // 
            this.lblCurrentDateTime.Dpi = 254F;
            this.lblCurrentDateTime.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentDateTime.LocationFloat = new DevExpress.Utils.PointFloat(1036.105F, 1364.487F);
            this.lblCurrentDateTime.Name = "lblCurrentDateTime";
            this.lblCurrentDateTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCurrentDateTime.SizeF = new System.Drawing.SizeF(328.0594F, 53.99988F);
            this.lblCurrentDateTime.StylePriority.UseFont = false;
            this.lblCurrentDateTime.StylePriority.UseTextAlignment = false;
            this.lblCurrentDateTime.Text = "تاریخ1391/03/13 ساعت 13:20";
            this.lblCurrentDateTime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
            // 
            // lblDateArchive
            // 
            this.lblDateArchive.Dpi = 254F;
            this.lblDateArchive.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblDateArchive.LocationFloat = new DevExpress.Utils.PointFloat(81.87311F, 744.9458F);
            this.lblDateArchive.Name = "lblDateArchive";
            this.lblDateArchive.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDateArchive.SizeF = new System.Drawing.SizeF(259.334F, 59.99994F);
            this.lblDateArchive.StylePriority.UseFont = false;
            this.lblDateArchive.StylePriority.UseTextAlignment = false;
            this.lblDateArchive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblMeId
            // 
            this.lblMeId.Dpi = 254F;
            this.lblMeId.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblMeId.LocationFloat = new DevExpress.Utils.PointFloat(945.733F, 561.9459F);
            this.lblMeId.Name = "lblMeId";
            this.lblMeId.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblMeId.SizeF = new System.Drawing.SizeF(196.8774F, 63F);
            this.lblMeId.StylePriority.UseFont = false;
            this.lblMeId.StylePriority.UseTextAlignment = false;
            this.lblMeId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblMemberShipDate
            // 
            this.lblMemberShipDate.Dpi = 254F;
            this.lblMemberShipDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblMemberShipDate.LocationFloat = new DevExpress.Utils.PointFloat(963.5038F, 316.6985F);
            this.lblMemberShipDate.Name = "lblMemberShipDate";
            this.lblMemberShipDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblMemberShipDate.SizeF = new System.Drawing.SizeF(283.7738F, 63F);
            this.lblMemberShipDate.StylePriority.UseFont = false;
            this.lblMemberShipDate.StylePriority.UseTextAlignment = false;
            this.lblMemberShipDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblBirthDay
            // 
            this.lblBirthDay.Dpi = 254F;
            this.lblBirthDay.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblBirthDay.LocationFloat = new DevExpress.Utils.PointFloat(75.87308F, 252.6985F);
            this.lblBirthDay.Name = "lblBirthDay";
            this.lblBirthDay.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblBirthDay.SizeF = new System.Drawing.SizeF(244.1251F, 63.00002F);
            this.lblBirthDay.StylePriority.UseFont = false;
            this.lblBirthDay.StylePriority.UseTextAlignment = false;
            this.lblBirthDay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblIssuePlace
            // 
            this.lblIssuePlace.Dpi = 254F;
            this.lblIssuePlace.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblIssuePlace.LocationFloat = new DevExpress.Utils.PointFloat(426.7896F, 252.6985F);
            this.lblIssuePlace.Name = "lblIssuePlace";
            this.lblIssuePlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblIssuePlace.SizeF = new System.Drawing.SizeF(384.1707F, 63.00002F);
            this.lblIssuePlace.StylePriority.UseFont = false;
            this.lblIssuePlace.StylePriority.UseTextAlignment = false;
            this.lblIssuePlace.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblIdNo
            // 
            this.lblIdNo.Dpi = 254F;
            this.lblIdNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblIdNo.LocationFloat = new DevExpress.Utils.PointFloat(931.8014F, 252.6985F);
            this.lblIdNo.Name = "lblIdNo";
            this.lblIdNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblIdNo.SizeF = new System.Drawing.SizeF(198.9922F, 63F);
            this.lblIdNo.StylePriority.UseFont = false;
            this.lblIdNo.StylePriority.UseTextAlignment = false;
            this.lblIdNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblMajor
            // 
            this.lblMajor.Dpi = 254F;
            this.lblMajor.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblMajor.LocationFloat = new DevExpress.Utils.PointFloat(75.87308F, 187.6984F);
            this.lblMajor.Name = "lblMajor";
            this.lblMajor.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblMajor.SizeF = new System.Drawing.SizeF(532.1416F, 63.00002F);
            this.lblMajor.StylePriority.UseFont = false;
            this.lblMajor.StylePriority.UseTextAlignment = false;
            this.lblMajor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblName
            // 
            this.lblName.Dpi = 254F;
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.LocationFloat = new DevExpress.Utils.PointFloat(724.8571F, 189.6984F);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblName.SizeF = new System.Drawing.SizeF(503.8998F, 63.00002F);
            this.lblName.StylePriority.UseFont = false;
            this.lblName.StylePriority.UseTextAlignment = false;
            this.lblName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Dpi = 254F;
            this.xrLabel16.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(490.9941F, 444.6125F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(41.3241F, 60F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "4";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel31
            // 
            this.xrLabel31.Dpi = 254F;
            this.xrLabel31.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(341.2072F, 744.9459F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(1036.793F, 60.00006F);
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UseTextAlignment = false;
            this.xrLabel31.Text = "از دانشگاه ......................................................................" +
                "...................... دریافت گردیده و در تاریخ";
            this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Dpi = 254F;
            this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(739.523F, 804.9457F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(638.4767F, 60.00006F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = ".تاییدیه استعلام ایشان در سیستم ثبت گردیده است";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 254F;
            this.xrLabel15.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(81.87321F, 684.946F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(201.1256F, 60.00006F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "............................";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Dpi = 254F;
            this.xrLabel30.Font = new System.Drawing.Font("B Nazanin", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(259.1439F, 1276.006F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(1118.856F, 59.99976F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = ".در تاریخ ........................... کارت هوشمند عضویت تحویل اینجانب ..........." +
                "............................. گردید";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 254F;
            this.xrLabel29.Font = new System.Drawing.Font("B Titr", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(81.87309F, 1342.934F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(287.4407F, 65F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.Text = "امضاء";
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Dpi = 254F;
            this.xrLabel28.Font = new System.Drawing.Font("B Titr", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(81.87308F, 1194.164F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(287.4407F, 65.00012F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "IT مسئول واحد";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel27
            // 
            this.xrLabel27.Dpi = 254F;
            this.xrLabel27.Font = new System.Drawing.Font("B Nazanin", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(319.9981F, 1128.101F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(1058.001F, 60.00012F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.Text = ".مسئول واحد عضویت ، کارت هوشمند نامبرده در تاریخ ................................" +
                "..... صادر گردید";
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Dpi = 254F;
            this.xrLabel26.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(280.013F, 975.7886F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(1097.987F, 60.00006F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = ".با سلام صدور کارت عضویت هوشمند نامبرده، با رعایت مقررات و ضوابط جاری بلامانع است" +
                "";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // lblGovTitle
            // 
            this.lblGovTitle.Dpi = 254F;
            this.lblGovTitle.Font = new System.Drawing.Font("B Titr", 10F, System.Drawing.FontStyle.Bold);
            this.lblGovTitle.LocationFloat = new DevExpress.Utils.PointFloat(81.87311F, 1051.259F);
            this.lblGovTitle.Name = "lblGovTitle";
            this.lblGovTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblGovTitle.SizeF = new System.Drawing.SizeF(560.8683F, 65F);
            this.lblGovTitle.StylePriority.UseFont = false;
            this.lblGovTitle.StylePriority.UseTextAlignment = false;
            this.lblGovTitle.Text = "رئیس سازمان نظام مهندسی ساختمان فارس";
            this.lblGovTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Dpi = 254F;
            this.xrLabel24.Font = new System.Drawing.Font("B Titr", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(983.5466F, 910.7885F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(394.4532F, 65F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "مدیریت محترم طرح و برنامه";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Dpi = 254F;
            this.xrLabel23.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(81.87308F, 504.6124F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(450.4453F, 60.00003F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.Text = "فرم های تکمیل شده جهت عضویت";
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrCheckBox6
            // 
            this.xrCheckBox6.Dpi = 254F;
            this.xrCheckBox6.Font = new System.Drawing.Font("B Nazanin", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrCheckBox6.LocationFloat = new DevExpress.Utils.PointFloat(555.2383F, 512.1957F);
            this.xrCheckBox6.Name = "xrCheckBox6";
            this.xrCheckBox6.SizeF = new System.Drawing.SizeF(36F, 40F);
            this.xrCheckBox6.StylePriority.UseFont = false;
            this.xrCheckBox6.StylePriority.UseTextAlignment = false;
            this.xrCheckBox6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Dpi = 254F;
            this.xrLabel22.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(336.2581F, 444.5528F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(154.7361F, 60F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "قطعه عکس";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrCheckBox5
            // 
            this.xrCheckBox5.Dpi = 254F;
            this.xrCheckBox5.Font = new System.Drawing.Font("B Nazanin", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrCheckBox5.LocationFloat = new DevExpress.Utils.PointFloat(554.4289F, 449.9667F);
            this.xrCheckBox5.Name = "xrCheckBox5";
            this.xrCheckBox5.SizeF = new System.Drawing.SizeF(36F, 42F);
            this.xrCheckBox5.StylePriority.UseFont = false;
            this.xrCheckBox5.StylePriority.UseTextAlignment = false;
            this.xrCheckBox5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Dpi = 254F;
            this.xrLabel21.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(81.87308F, 384.5529F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(450.4453F, 59.99997F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "تصویر تاییدشده صفحات شناسنامه";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrCheckBox4
            // 
            this.xrCheckBox4.Dpi = 254F;
            this.xrCheckBox4.Font = new System.Drawing.Font("B Nazanin", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrCheckBox4.LocationFloat = new DevExpress.Utils.PointFloat(554.4289F, 389.5529F);
            this.xrCheckBox4.Name = "xrCheckBox4";
            this.xrCheckBox4.SizeF = new System.Drawing.SizeF(36F, 42F);
            this.xrCheckBox4.StylePriority.UseFont = false;
            this.xrCheckBox4.StylePriority.UseTextAlignment = false;
            this.xrCheckBox4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Dpi = 254F;
            this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(913.8562F, 500.6124F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(398.5818F, 60.00003F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "فیش پرداخت حق عضویت";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrCheckBox3
            // 
            this.xrCheckBox3.Dpi = 254F;
            this.xrCheckBox3.Font = new System.Drawing.Font("B Nazanin", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrCheckBox3.LocationFloat = new DevExpress.Utils.PointFloat(1328.164F, 507.1958F);
            this.xrCheckBox3.Name = "xrCheckBox3";
            this.xrCheckBox3.SizeF = new System.Drawing.SizeF(36F, 40F);
            this.xrCheckBox3.StylePriority.UseFont = false;
            this.xrCheckBox3.StylePriority.UseTextAlignment = false;
            this.xrCheckBox3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Dpi = 254F;
            this.xrLabel19.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(913.856F, 440.6123F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(398.5817F, 60.00003F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "تصویر تاییدشده کارت ملی";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrCheckBox2
            // 
            this.xrCheckBox2.Dpi = 254F;
            this.xrCheckBox2.Font = new System.Drawing.Font("B Nazanin", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrCheckBox2.LocationFloat = new DevExpress.Utils.PointFloat(1328.164F, 444.9666F);
            this.xrCheckBox2.Name = "xrCheckBox2";
            this.xrCheckBox2.SizeF = new System.Drawing.SizeF(36F, 40F);
            this.xrCheckBox2.StylePriority.UseFont = false;
            this.xrCheckBox2.StylePriority.UseTextAlignment = false;
            this.xrCheckBox2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Dpi = 254F;
            this.xrLabel18.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(911.8562F, 380.5529F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(400.5815F, 60F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "تصویر تاییدشده مدارک تحصیلی";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrCheckBox1
            // 
            this.xrCheckBox1.Dpi = 254F;
            this.xrCheckBox1.Font = new System.Drawing.Font("B Nazanin", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrCheckBox1.LocationFloat = new DevExpress.Utils.PointFloat(1328.164F, 384.5529F);
            this.xrCheckBox1.Name = "xrCheckBox1";
            this.xrCheckBox1.SizeF = new System.Drawing.SizeF(36F, 40F);
            this.xrCheckBox1.StylePriority.UseFont = false;
            this.xrCheckBox1.StylePriority.UseTextAlignment = false;
            this.xrCheckBox1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Dpi = 254F;
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(282.9987F, 684.946F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(1095.001F, 60.00006F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = " ضمناً جواب استعلام مدرک تحصیلی نامبرده به شماره نامه ..........................." +
                ".......... و تاریخ نامه";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("B Titr", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(81.87321F, 839.9457F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(287.4407F, 65F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "مسئول واحد عضویت";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(523.3481F, 624.9459F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(854.6519F, 60.00012F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = ".خواهشمند است دستور صدور کارت عضویت نامبرده را صادر فرمائید";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(452.2507F, 564.9459F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(493.4823F, 60F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = ".به عضویت سازمان پذیرفته شده اند";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(1142.61F, 561.9459F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(235.3893F, 63F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "با شماره عضویت";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(414.8199F, 316.6985F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(548.684F, 60F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = ": پس از ارائه مدارک لازم به شرح ذیل";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(1247.278F, 317.6984F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(130.7222F, 59.99998F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "در تاریخ";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(319.9981F, 255.6985F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(106.7915F, 59.99998F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "متولد";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(810.9603F, 252.6985F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(119.6905F, 60.00002F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "صادره از";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(1130.794F, 252.6984F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(247.2062F, 59.99998F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "به شماره شناسنامه";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(608.0147F, 189.6984F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(116.8423F, 60F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "در رشته";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1228.757F, 189.6984F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(149.243F, 59.99998F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "آقای/خانم";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(752.7522F, 129.6984F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(625.2483F, 59.99998F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = ": با سلام ، احتراماً بدینوسیله به استحضار می رساند";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("B Titr", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(628.3977F, 62F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(749.6023F, 65F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "ریاست محترم سازمان نظام مهندسی ساختمان استان فارس";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrlblWarning
            // 
            this.xrlblWarning.Dpi = 254F;
            this.xrlblWarning.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.xrlblWarning.LocationFloat = new DevExpress.Utils.PointFloat(361.9032F, 1432.542F);
            this.xrlblWarning.Name = "xrlblWarning";
            this.xrlblWarning.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlblWarning.SizeF = new System.Drawing.SizeF(671.3138F, 65F);
            this.xrlblWarning.StylePriority.UseFont = false;
            this.xrlblWarning.StylePriority.UseTextAlignment = false;
            this.xrlblWarning.Text = "نامه امضاء شده بدون مهر برجسته فاقد اعتبار است";
            this.xrlblWarning.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrPicSign
            // 
            this.xrPicSign.Dpi = 254F;
            this.xrPicSign.Image = ((System.Drawing.Image)(resources.GetObject("xrPicSign.Image")));
            this.xrPicSign.LocationFloat = new DevExpress.Utils.PointFloat(75.125F, 909.5605F);
            this.xrPicSign.Name = "xrPicSign";
            this.xrPicSign.SizeF = new System.Drawing.SizeF(200F, 210F);
            this.xrPicSign.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Dpi = 254F;
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(1412.5F, 100.5417F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 1497.542F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 341.3125F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(1412.5F, 341.3125F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.Name = "xrControlStyle2";
            this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // MeId
            // 
            this.MeId.Name = "MeId";
            this.MeId.Type = typeof(int);
            // 
            // UserId
            // 
            this.UserId.Name = "UserId";
            this.UserId.Type = typeof(int);
            // 
            // MemberCardRequestReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter,
            this.ReportHeader});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 5, 0, 50);
            this.PageHeight = 2101;
            this.PageWidth = 1481;
            this.PaperKind = System.Drawing.Printing.PaperKind.A5;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.MeId,
            this.UserId});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2});
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Version = "11.2";
            this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        //private void MemberCardRequestReport_AfterPrint(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        TSP.DataManager.PrintingHistoryManager PrintingHistoryManager = new DataManager.PrintingHistoryManager();
        //        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        //        MemberManager.FindByCode(meid);
        //        System.Data.DataRow dr = PrintingHistoryManager.NewRow();
        //        dr["TableId"] = meid;
        //        dr["TableType"] = TSP.DataManager.TableType.MemberCardRequestPrint;
        //        dr["Description"] = "چاپ درخواست کارت عضویت";
        //        dr["CreateDate"] = TSP.DataManager.Utility.GetDateOfToday();
        //        dr["CreateTime"] = TSP.DataManager.Utility.GetCurrentTime();
        //        dr["UserId"] = userid;
        //        dr["ModifiedDate"] = DateTime.Now;
        //        PrintingHistoryManager.AddRow(dr);
        //        if (PrintingHistoryManager.Save() > 0)
        //        {
        //            PrintingHistoryManager.DataTable.AcceptChanges();
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}


    }
}

