using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportMemberTemporaryCard
/// </summary>
public class XtraReportMemberTemporaryCard : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox xrPictureBox2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel6;
    private XRLabel lblIdNo;
    private XRLabel lblSSN;
    private XRLabel xrLabel7;
    private XRLabel lblFathername;
    private XRLabel xrLabel5;
    private XRLabel xrLabel354;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRPictureBox PicMember;
    private XRLabel lblLastName;
    private XRLabel lblMeCode;
    private XRLabel xrLabel4;
    private XRLabel lblFisrtName;
    private XRPictureBox xrPicSign;
    private XRLabel lblGovName;
    private XRLabel lblGovTitle;
    private XRLabel xrlblWarning;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportMemberTemporaryCard(int MeId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        TSP.DataManager.PrintAssignerSettingManager PrintAssignerSettingManager = new TSP.DataManager.PrintAssignerSettingManager();
        PrintAssignerSettingManager.FindByPrintTypeId((int)TSP.DataManager.PrintType.MemberTempreryCartPrinting);
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

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count > 0)
        {
            lblFisrtName.Text = MemberManager[0]["FirstName"].ToString();
            lblLastName.Text = MemberManager[0]["LastName"].ToString();
            MemberRequestManager.FindByMemberId(MeId, -1, -1);
            if (MemberRequestManager.Count > 0)
            {
                if (Utility.IsDBNullOrNullValue(MemberRequestManager[0]["ImageUrl"]) == false)
                    PicMember.ImageUrl = MemberRequestManager[0]["ImageUrl"].ToString();
            }
            if (Utility.IsDBNullOrNullValue(MemberManager[0]["MeNo"]) || MemberManager[0]["MeNo"].ToString().Trim() == "0")
                lblMeCode.Text = " --- ";
            else
                lblMeCode.Text = MemberManager[0]["MeNo"].ToString();
            lblIdNo.Text = MemberManager[0]["IdNo"].ToString();
            lblFathername.Text = MemberManager[0]["FatherName"].ToString();
            lblSSN.Text = MemberManager[0]["SSN"].ToString();

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
        string resourceFileName = "XtraReportMemberTemporaryCard.resx";
        System.Resources.ResourceManager resources = global::Resources.XtraReportMemberTemporaryCard.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblGovName = new DevExpress.XtraReports.UI.XRLabel();
        this.lblGovTitle = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblIdNo = new DevExpress.XtraReports.UI.XRLabel();
        this.lblSSN = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblFathername = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel354 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.PicMember = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblLastName = new DevExpress.XtraReports.UI.XRLabel();
        this.lblMeCode = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblFisrtName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPicSign = new DevExpress.XtraReports.UI.XRPictureBox();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrlblWarning = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblGovName,
            this.lblGovTitle,
            this.xrLabel1,
            this.xrLabel6,
            this.lblIdNo,
            this.lblSSN,
            this.xrLabel7,
            this.lblFathername,
            this.xrLabel5,
            this.xrLabel354,
            this.xrLabel3,
            this.xrLabel2,
            this.PicMember,
            this.lblLastName,
            this.lblMeCode,
            this.xrLabel4,
            this.lblFisrtName,
            this.xrPicSign});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1400F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblGovName
        // 
        this.lblGovName.Dpi = 254F;
        this.lblGovName.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblGovName.LocationFloat = new DevExpress.Utils.PointFloat(205.4515F, 1169.049F);
        this.lblGovName.Name = "lblGovName";
        this.lblGovName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblGovName.SizeF = new System.Drawing.SizeF(522.0602F, 75.87524F);
        this.lblGovName.StylePriority.UseFont = false;
        this.lblGovName.StylePriority.UseTextAlignment = false;
        this.lblGovName.Text = "با تشکر - حسین پور اسدی";
        this.lblGovName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // lblGovTitle
        // 
        this.lblGovTitle.Dpi = 254F;
        this.lblGovTitle.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblGovTitle.LocationFloat = new DevExpress.Utils.PointFloat(78.09618F, 1244.925F);
        this.lblGovTitle.Name = "lblGovTitle";
        this.lblGovTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblGovTitle.SizeF = new System.Drawing.SizeF(792.736F, 75.875F);
        this.lblGovTitle.StylePriority.UseFont = false;
        this.lblGovTitle.StylePriority.UseTextAlignment = false;
        this.lblGovTitle.Text = "رئیس سازمان نظام مهندسی ساختمان استان فارس";
        this.lblGovTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1003.617F, 460.0042F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(283.1044F, 76.19998F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = ": شماره شناسنامه";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1032.722F, 536.2042F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(254.0002F, 76.20001F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = ": کدملی";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblIdNo
        // 
        this.lblIdNo.Dpi = 254F;
        this.lblIdNo.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblIdNo.LocationFloat = new DevExpress.Utils.PointFloat(323.6384F, 460.0042F);
        this.lblIdNo.Name = "lblIdNo";
        this.lblIdNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblIdNo.SizeF = new System.Drawing.SizeF(679.9786F, 76.19998F);
        this.lblIdNo.StylePriority.UseFont = false;
        this.lblIdNo.StylePriority.UseTextAlignment = false;
        this.lblIdNo.Text = " ";
        this.lblIdNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblSSN
        // 
        this.lblSSN.Dpi = 254F;
        this.lblSSN.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblSSN.LocationFloat = new DevExpress.Utils.PointFloat(323.6384F, 536.2042F);
        this.lblSSN.Name = "lblSSN";
        this.lblSSN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblSSN.SizeF = new System.Drawing.SizeF(709.0831F, 76.20001F);
        this.lblSSN.StylePriority.UseFont = false;
        this.lblSSN.StylePriority.UseTextAlignment = false;
        this.lblSSN.Text = " ";
        this.lblSSN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(1032.722F, 383.8042F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(253.9999F, 76.20001F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = ": نام پدر";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblFathername
        // 
        this.lblFathername.Dpi = 254F;
        this.lblFathername.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblFathername.LocationFloat = new DevExpress.Utils.PointFloat(323.6384F, 383.8042F);
        this.lblFathername.Name = "lblFathername";
        this.lblFathername.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblFathername.SizeF = new System.Drawing.SizeF(709.0831F, 76.20001F);
        this.lblFathername.StylePriority.UseFont = false;
        this.lblFathername.StylePriority.UseTextAlignment = false;
        this.lblFathername.Text = " ";
        this.lblFathername.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel5
        // 
        this.xrLabel5.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(63.18255F, 646.8001F);
        this.xrLabel5.Multiline = true;
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(1177.713F, 330.9407F);
        this.xrLabel5.StylePriority.UseBackColor = false;
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = resources.GetString("xrLabel5.Text");
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabel354
        // 
        this.xrLabel354.Dpi = 254F;
        this.xrLabel354.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel354.LocationFloat = new DevExpress.Utils.PointFloat(1032.722F, 229.2876F);
        this.xrLabel354.Name = "xrLabel354";
        this.xrLabel354.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel354.SizeF = new System.Drawing.SizeF(254F, 76.20007F);
        this.xrLabel354.StylePriority.UseFont = false;
        this.xrLabel354.StylePriority.UseTextAlignment = false;
        this.xrLabel354.Text = ": نام";
        this.xrLabel354.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1032.722F, 307.6042F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(254.0002F, 76.20001F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = ": نام خانوادگی";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(172.8265F, 2.063553F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1010.708F, 115.0408F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "   مجوز ورود به جلسه آزمون حرفه ای مهندسان سال";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PicMember
        // 
        this.PicMember.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PicMember.Dpi = 254F;
        this.PicMember.LocationFloat = new DevExpress.Utils.PointFloat(36.72421F, 130.2276F);
        this.PicMember.Name = "PicMember";
        this.PicMember.SizeF = new System.Drawing.SizeF(256.6459F, 279.4F);
        this.PicMember.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.PicMember.StylePriority.UseBorders = false;
        // 
        // lblLastName
        // 
        this.lblLastName.Dpi = 254F;
        this.lblLastName.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblLastName.LocationFloat = new DevExpress.Utils.PointFloat(323.6384F, 307.6042F);
        this.lblLastName.Name = "lblLastName";
        this.lblLastName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblLastName.SizeF = new System.Drawing.SizeF(709.0834F, 76.20001F);
        this.lblLastName.StylePriority.UseFont = false;
        this.lblLastName.StylePriority.UseTextAlignment = false;
        this.lblLastName.Text = " ";
        this.lblLastName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblMeCode
        // 
        this.lblMeCode.Dpi = 254F;
        this.lblMeCode.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblMeCode.LocationFloat = new DevExpress.Utils.PointFloat(323.6384F, 153.0876F);
        this.lblMeCode.Name = "lblMeCode";
        this.lblMeCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblMeCode.SizeF = new System.Drawing.SizeF(709.0831F, 76.2F);
        this.lblMeCode.StylePriority.UseFont = false;
        this.lblMeCode.StylePriority.UseTextAlignment = false;
        this.lblMeCode.Text = " ";
        this.lblMeCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(1032.722F, 153.0876F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(254F, 76.2F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = ": شماره عضویت";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblFisrtName
        // 
        this.lblFisrtName.Dpi = 254F;
        this.lblFisrtName.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblFisrtName.LocationFloat = new DevExpress.Utils.PointFloat(323.6384F, 229.2876F);
        this.lblFisrtName.Name = "lblFisrtName";
        this.lblFisrtName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblFisrtName.SizeF = new System.Drawing.SizeF(709.0834F, 76.20007F);
        this.lblFisrtName.StylePriority.UseFont = false;
        this.lblFisrtName.StylePriority.UseTextAlignment = false;
        this.lblFisrtName.Text = " ";
        this.lblFisrtName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPicSign
        // 
        this.xrPicSign.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPicSign.Dpi = 254F;
        this.xrPicSign.Image = ((System.Drawing.Image)(resources.GetObject("xrPicSign.Image")));
        this.xrPicSign.LocationFloat = new DevExpress.Utils.PointFloat(324.6384F, 1013.4F);
        this.xrPicSign.Name = "xrPicSign";
        this.xrPicSign.SizeF = new System.Drawing.SizeF(300F, 310F);
        this.xrPicSign.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPicSign.StylePriority.UseBorders = false;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 41F;
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
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 341.3125F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(14.07577F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(1299.104F, 341.3125F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblWarning,
            this.xrPictureBox2});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 172F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrlblWarning
        // 
        this.xrlblWarning.Dpi = 254F;
        this.xrlblWarning.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrlblWarning.LocationFloat = new DevExpress.Utils.PointFloat(323.6384F, 0F);
        this.xrlblWarning.Name = "xrlblWarning";
        this.xrlblWarning.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrlblWarning.SizeF = new System.Drawing.SizeF(671.3138F, 64.99999F);
        this.xrlblWarning.StylePriority.UseFont = false;
        this.xrlblWarning.StylePriority.UseTextAlignment = false;
        this.xrlblWarning.Text = "نامه امضاء شده بدون مهر برجسته فاقد اعتبار است";
        this.xrlblWarning.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(14.07577F, 66.04F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(1299.104F, 100.5417F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        // 
        // XtraReportMemberTemporaryCard
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(74, 89, 41, 203);
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
