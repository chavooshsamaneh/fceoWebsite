using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportUserInfo
/// </summary>
public class XtraReportUserInfo : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private XRPictureBox xrPictureBox1;
    private XRLine xrLine1;
    private XRLabel xrLabel2;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel1;
    private XRLabel lblPrintTime;
    private XRLabel xrLabel4;
    private XRLabel lblUltName;
    private XRLabel xrLabel354;
    private XRLabel lblName;
    private XRLabel lblUsername;
    private XRLabel xrLabel5;
    private XRLabel lblPass;
    private XRLabel xrLabel7;
    private XRLabel xrLabel9;
    private XRLine xrLine2;
    private XRLabel lblEmail;
    private XRLabel lblEmail_Label;
    private XRLabel lblCode;
    private XRLabel lblCode_Label;
    private XRLabel lblComment1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportUserInfo(int UserId, String Pass)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        Load_Data(UserId, Pass, "");
    }

    public XtraReportUserInfo(int UserId, String Pass, String Code)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        Load_Data(UserId, Pass, Code);
    }

    void Load_Data(int UserId, String Pass, String Code)
    {
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        LoginManager.FindByCode(UserId);
        if (LoginManager.Count > 0)
        {
            lblUsername.Text = LoginManager[0]["Username"].ToString();
            if (Utility.IsDBNullOrNullValue(LoginManager[0]["UserEmail"]))
            {
                //lblEmail.Visible = false;
                //lblEmail_Label.Visible = false;
            }
            else
                lblEmail.Text = LoginManager[0]["UserEmail"].ToString();
            lblName.Text = LoginManager[0]["FullName"].ToString();
            lblPass.Text = Pass;
            lblPrintTime.Text = Utility.GetDateOfToday() + " " + Utility.GetCurrentTime();
            lblUltName.Text = LoginManager[0]["UltName"].ToString();
            if (String.IsNullOrEmpty(Code) == false)
                lblCode.Text = Code;
            if (Convert.ToInt32(LoginManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Member || Convert.ToInt32(LoginManager[0]["UltId"]) == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                decimal MemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.FirstMembershipCost);
                decimal YearMemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.YearlyMembershipCost);
                decimal Total = MemberShipCost + YearMemberShipCost;

                lblComment1.Text ="";
                lblComment1.Text =
                     "توجه نمایید درصورت ورود اطلاعات ناقص و یا نادرست (بخصوص عدم مطابقت عنوان مدرک تحصیلی انتخاب شده با عنوان مدرک درج شده بر روی دانشنامه شما) عوابق بعدی این عمل به عهده ی شما می باشد و مبلغ واریز شده به حساب به هیچ عنوان  قابل بازگشت نمی باشد" + "\n" +
                    ": مدارک مورد نیاز" + "\n" +
                "اصل مدرک تحصیلی و دو برگ تصویر برابر اصل شده آن *    " + "\n" +
                "اصل و تصویر برابر اصل شده شناسنامه - صفحه اول و توضیحات *    " + "\n" +
                "اصل و کپی برابر اصل شده کارت ملی *    " + "\n" +
                "اصل و تصویر برابر اصل شده كارت پايان خدمت  یا هر نامه یا مدرکی که وضعیت خدمت را مشخص می کند" + "\n" +
                "اصل نامه عدم عضویت در نظام کاردانی در صورت داشتن مدرک کارشناسی ناپیوسته" + "\n" +
                "یک مدرک دال بر اینکه حداقل شش ماه ساکن استان فارس بوده اید مانند:گواهی معتبر اشتغال به کار/ سوابق پرداخت بیمه / اجاره نامه / تایییه شورای محل و ..." + "\n" +
             "پرداخت مبلغ " + Total.ToString("#,#") + " ریال به حساب بانک تجارت شعبه نظام مهندسی * " + "\n" +
                "پرداخت حق عضویت در هر موقع از سال، تا پایان همان سال معتبر است *    " + "\n" +
                //"قابل ذکر است جهت پیگیری روند ثبت نام خود از طریق همین سامانه و با استفاده از نام کاربری و رمز عبور خود اقدام نمایید. در صورت عدم پیگیری شما از این طریق و در صورت ناقص بودن اطلاعات، کارمندان سازمان هیچگونه مسئولیتی در قبال تائید به موقع و یا عدم تائید عضویت شما ندارند"
                "قابل ذکر است اطلاعات شما ظرف مدت 48 ساعت كاری كنترل می شود درصورت دريافت پيام (درخواست اطلاعات عضویت شما مورد بررسی قرارگرفت.لطفاً جهت مشاهده نتیجه بررسی و اقدام لازم، پس از 48 ساعت کاری، ازپورتال شخصی خود،منوی اصلی،مدیریت درخواستها،درنوار ابزار،گزینه پی گیری گردش کار، برروی آیکون آخرین پانوشت کلیک نمائید.) با دردست داشتن مدارك زير به سازمان نظام مهندسي واحد عضويت و پروانه اشتغال مراجعه نمائيد. ";
            }
            else if (Convert.ToInt32(LoginManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Office)
            {
                string MemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice).ToString();
                string YearMemberShipCost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.YearlyMembershipCostOffice).ToString();
                lblComment1.Text = "";
                lblComment1.Text = ": مدارک مورد نیاز" + "\n" +
                     "اساسنامه شرکت *    " + "\n" +
                       "اظهار نامه شرکت *    " + "\n" +
                       "روزنامه رسمی شرکت حاوی آگهی تاسیس *    " + "\n" +
                       "روزنامه های رسمی شرکت حاوی تغییرات مهم *    " + "\n" +
                       "آخرین روزنامه رسمی شرکت *    " + "\n" +
                       "آخرین صورت جلسه مربوط به انتخاب مدیران و دارندگان حق امضا *    " + "\n" +
                       "دو قطعه عکس مدیر عامل شرکت *    " + "\n" +
                       "شناسنامه مدیران *    " + "\n" +
                       "پروانه های اشتغال مدیر عامل, اعضای هیئت مدیره و شاغلین تمام وقت (مهندسین, کاردانها *     " + "\n" +
                       "(و معماران تجربی" + "\n" +
                       "فیش بانکی مربوط به پرداخت ورودیه به مبلغ " + MemberShipCost + " ریال به حساب بانک تجارت شعبه نظام مهندسی *    " + "\n" +
                       "فیش بانکی مربوط به پرداخت حق عضویت سالانه به مبلغ " + YearMemberShipCost + " ریال به حساب بانک تجارت شعبه نظام مهندسی *    ";
            }
            else
            {
                lblComment1.Text = "";
            }

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
        string resourceFileName = "XtraReportUserInfo.resx";
        System.Resources.ResourceManager resources = global::Resources.XtraReportUserInfo.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblComment1 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCode = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCode_Label = new DevExpress.XtraReports.UI.XRLabel();
        this.lblEmail = new DevExpress.XtraReports.UI.XRLabel();
        this.lblEmail_Label = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblPass = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblUsername = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblUltName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel354 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblName = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.lblPrintTime = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblComment1,
            this.lblCode,
            this.lblCode_Label,
            this.lblEmail,
            this.lblEmail_Label,
            this.xrLabel9,
            this.lblPass,
            this.xrLabel7,
            this.lblUsername,
            this.xrLabel5,
            this.xrLabel4,
            this.lblUltName,
            this.xrLabel354,
            this.lblName});
        this.Detail.HeightF = 540F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblComment1
        // 
        this.lblComment1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        this.lblComment1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblComment1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblComment1.LocationFloat = new DevExpress.Utils.PointFloat(9.999864F, 291.0417F);
        this.lblComment1.Multiline = true;
        this.lblComment1.Name = "lblComment1";
        this.lblComment1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 100F);
        this.lblComment1.SizeF = new System.Drawing.SizeF(503F, 228.5417F);
        this.lblComment1.StylePriority.UseBackColor = false;
        this.lblComment1.StylePriority.UseBorders = false;
        this.lblComment1.StylePriority.UseFont = false;
        this.lblComment1.StylePriority.UsePadding = false;
        this.lblComment1.StylePriority.UseTextAlignment = false;
        this.lblComment1.Text = resources.GetString("lblComment1.Text");
        this.lblComment1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblCode
        // 
        this.lblCode.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblCode.LocationFloat = new DevExpress.Utils.PointFloat(9.99999F, 173.9584F);
        this.lblCode.Name = "lblCode";
        this.lblCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblCode.SizeF = new System.Drawing.SizeF(378F, 33F);
        this.lblCode.StylePriority.UseFont = false;
        this.lblCode.StylePriority.UseTextAlignment = false;
        this.lblCode.Text = " ";
        this.lblCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblCode_Label
        // 
        this.lblCode_Label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblCode_Label.LocationFloat = new DevExpress.Utils.PointFloat(387.9999F, 173.9584F);
        this.lblCode_Label.Name = "lblCode_Label";
        this.lblCode_Label.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblCode_Label.SizeF = new System.Drawing.SizeF(125F, 33F);
        this.lblCode_Label.StylePriority.UseFont = false;
        this.lblCode_Label.StylePriority.UseTextAlignment = false;
        this.lblCode_Label.Text = ": کد رهگیری";
        this.lblCode_Label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblEmail
        // 
        this.lblEmail.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblEmail.LocationFloat = new DevExpress.Utils.PointFloat(9.999983F, 140.9584F);
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblEmail.SizeF = new System.Drawing.SizeF(378F, 33F);
        this.lblEmail.StylePriority.UseFont = false;
        this.lblEmail.StylePriority.UseTextAlignment = false;
        this.lblEmail.Text = " ";
        this.lblEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblEmail_Label
        // 
        this.lblEmail_Label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblEmail_Label.LocationFloat = new DevExpress.Utils.PointFloat(388F, 140.9584F);
        this.lblEmail_Label.Name = "lblEmail_Label";
        this.lblEmail_Label.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblEmail_Label.SizeF = new System.Drawing.SizeF(125F, 33F);
        this.lblEmail_Label.StylePriority.UseFont = false;
        this.lblEmail_Label.StylePriority.UseTextAlignment = false;
        this.lblEmail_Label.Text = ": پست الکترونیکی";
        this.lblEmail_Label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel9
        // 
        this.xrLabel9.BackColor = System.Drawing.Color.PeachPuff;
        this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(9.999864F, 221.1665F);
        this.xrLabel9.Multiline = true;
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(503F, 56.74997F);
        this.xrLabel9.StylePriority.UseBackColor = false;
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UsePadding = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "لطفاً در حفظ مشخصات کاربری خود کوشا بوده و هیچ گاه این مشخصات را در اختیار دیگران" +
            " قرار ندهید";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblPass
        // 
        this.lblPass.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblPass.LocationFloat = new DevExpress.Utils.PointFloat(9.999879F, 107.9583F);
        this.lblPass.Name = "lblPass";
        this.lblPass.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblPass.SizeF = new System.Drawing.SizeF(378F, 33F);
        this.lblPass.StylePriority.UseFont = false;
        this.lblPass.StylePriority.UseTextAlignment = false;
        this.lblPass.Text = " ";
        this.lblPass.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(388F, 107.9583F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(125F, 33F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = ": رمز عبور";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblUsername
        // 
        this.lblUsername.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblUsername.LocationFloat = new DevExpress.Utils.PointFloat(9.999943F, 74.95833F);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblUsername.SizeF = new System.Drawing.SizeF(378F, 33F);
        this.lblUsername.StylePriority.UseFont = false;
        this.lblUsername.StylePriority.UseTextAlignment = false;
        this.lblUsername.Text = " ";
        this.lblUsername.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(387.9999F, 74.95833F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(125F, 33F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = ": نام کاربری";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(387.9999F, 41.95833F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(125F, 33F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = ": نوع کاربری";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblUltName
        // 
        this.lblUltName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblUltName.LocationFloat = new DevExpress.Utils.PointFloat(9.999943F, 41.95833F);
        this.lblUltName.Name = "lblUltName";
        this.lblUltName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblUltName.SizeF = new System.Drawing.SizeF(378F, 33F);
        this.lblUltName.StylePriority.UseFont = false;
        this.lblUltName.StylePriority.UseTextAlignment = false;
        this.lblUltName.Text = " ";
        this.lblUltName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel354
        // 
        this.xrLabel354.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel354.LocationFloat = new DevExpress.Utils.PointFloat(388F, 8.95834F);
        this.xrLabel354.Name = "xrLabel354";
        this.xrLabel354.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel354.SizeF = new System.Drawing.SizeF(125F, 33F);
        this.xrLabel354.StylePriority.UseFont = false;
        this.xrLabel354.StylePriority.UseTextAlignment = false;
        this.xrLabel354.Text = ": نام";
        this.xrLabel354.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblName
        // 
        this.lblName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblName.LocationFloat = new DevExpress.Utils.PointFloat(9.999879F, 8.95834F);
        this.lblName.Name = "lblName";
        this.lblName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblName.SizeF = new System.Drawing.SizeF(378F, 33F);
        this.lblName.StylePriority.UseFont = false;
        this.lblName.StylePriority.UseTextAlignment = false;
        this.lblName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 30F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.TopMargin.Visible = false;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 30F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.BottomMargin.Visible = false;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLine1,
            this.xrPictureBox1});
        this.PageHeader.HeightF = 104.125F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(146.875F, 10.00001F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(199.9998F, 77.58334F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "اطلاعات کاربری";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLine1
        // 
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 94.12502F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(522.9999F, 9.708313F);
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(416.125F, 4.541683F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(96.875F, 89.58334F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.lblPrintTime,
            this.xrLabel1});
        this.PageFooter.HeightF = 54.16667F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLine2
        // 
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(3.973643E-05F, 2.791659F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(522.9999F, 9.708313F);
        // 
        // lblPrintTime
        // 
        this.lblPrintTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.lblPrintTime.LocationFloat = new DevExpress.Utils.PointFloat(3.973643E-05F, 15.54171F);
        this.lblPrintTime.Name = "lblPrintTime";
        this.lblPrintTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblPrintTime.SizeF = new System.Drawing.SizeF(416.125F, 28.62498F);
        this.lblPrintTime.StylePriority.UseFont = false;
        this.lblPrintTime.StylePriority.UseTextAlignment = false;
        this.lblPrintTime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(416.125F, 15.54171F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(96.87497F, 28.62498F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = ": تاریخ و زمان چاپ";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // XtraReportUserInfo
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
        this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.PageHeight = 827;
        this.PageWidth = 583;
        this.PaperKind = System.Drawing.Printing.PaperKind.A5;
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
