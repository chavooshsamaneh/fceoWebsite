using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportEnvelope
/// </summary>
public class XtraReportEnvelope : DevExpress.XtraReports.UI.XtraReport
{

    private DevExpress.XtraReports.UI.DetailBand Detail;
    private XRPanel xrPanel1;
    private XRLabel xrLabel1;
    private XRLabel lblName;
    private XRLabel xrLabel2;
    private XRLabel lblSenderAddress;
    private XRLabel xrLabel3;
    private XRLabel lblReceiverAddress;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRLine xrLine1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportEnvelope(int MeIdFrom, int MeIdTo, String CreateDateFrom, String CreateDateTo, String FileDateFrom,
        String FileDateTo, String MjParam, String FirstName, String LastName, String GrParam, int ComId,
        TSP.DataManager.AutomationLetterRecieverTypes receiverType, TSP.DataManager.AddressType addressType,
        String FilterExpression, bool pageBreak, String SenderAddress)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        if (pageBreak)
            this.Detail.PageBreak = PageBreak.AfterBand;

        TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();

        DataTable dt = memberManager.SelectMemberForSearchByExec(MeIdFrom, MeIdTo, MjParam, FirstName, LastName, GrParam, ComId,
            CreateDateFrom, CreateDateTo, FileDateFrom, FileDateTo);
        dt.DefaultView.RowFilter = FilterExpression;

        lblSenderAddress.Text = SenderAddress;

        this.DataSource = dt;

        if (receiverType == TSP.DataManager.AutomationLetterRecieverTypes.Member)
            lblName.DataBindings.Add("Text", dt, "FullName");

        if (addressType == TSP.DataManager.AddressType.HomeAddress)
            lblReceiverAddress.DataBindings.Add("Text", dt, "HomeAdr");
        else if (addressType == TSP.DataManager.AddressType.WorkAddress)
            lblReceiverAddress.DataBindings.Add("Text", dt, "WorkAdr");

    }

    //+ "&SId=" + Utility.EncryptQS(envDetails.SId.ToString())
    //             + "&PageBreak=" + Utility.EncryptQS(envDetails.PageBreak.ToString())
    //             + "&ReceiverType
    /// <summary>
    ///برای چاپ پاکت نامه
    /// </summary>
    /// <param name="receiverType"></param>
    /// <param name="addressType"></param>
    /// <param name="pageBreak"></param>
    /// <param name="SenderAddress"></param>
    /// <param name="MeIdParameters"></param>
    public XtraReportEnvelope(TSP.DataManager.AutomationLetterRecieverTypes receiverType
        , TSP.DataManager.AddressType addressType
        , bool pageBreak
        , String SenderAddress
        , string MeIdParameters)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        if (pageBreak)
            this.Detail.PageBreak = PageBreak.AfterBand;

        TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();

        DataTable dt = memberManager.SelectMemberForEnvelopePrint(MeIdParameters);

        lblSenderAddress.Text = SenderAddress;

        this.DataSource = dt;

        if (receiverType == TSP.DataManager.AutomationLetterRecieverTypes.Member)
            lblName.DataBindings.Add("Text", dt, "FullName");

        if (addressType == TSP.DataManager.AddressType.HomeAddress)
            lblReceiverAddress.DataBindings.Add("Text", dt, "FullHomeAdr");
        else if (addressType == TSP.DataManager.AddressType.WorkAddress)
            lblReceiverAddress.DataBindings.Add("Text", dt, "FullWorkAdr");
    }


    public XtraReportEnvelope(DataTable dtRecievers, string SenderAddress)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        //this.Detail.PageBreak = PageBreak.AfterBand;       

        int RecieverType = -1;
        if (dtRecievers.Rows.Count > 0)
            RecieverType = Convert.ToInt32(dtRecievers.Rows[0]["RecieverType"]);

        TSP.DataManager.Automation.LetterRecieversManager LetterRecieversManager = new TSP.DataManager.Automation.LetterRecieversManager();
        DataTable dtRecieversEmp = LetterRecieversManager.FindByLetterId(Convert.ToInt32(dtRecievers.Rows[0]["LetterId"]), RecieverType);

        lblSenderAddress.Text = SenderAddress;
        this.DataSource = dtRecievers;
        switch (RecieverType)
        {
            case (int)TSP.DataManager.AutomationLetterRecieverTypes.Employee:
                lblName.DataBindings.Add("Text", dtRecievers, "EmpFullName");
                lblReceiverAddress.DataBindings.Add("Text", dtRecievers, "EmpAddress");
                break;

            case (int)TSP.DataManager.AutomationLetterRecieverTypes.Member:
                lblName.DataBindings.Add("Text", dtRecievers, "MemberName");
                lblReceiverAddress.DataBindings.Add("Text", dtRecievers, "MeAddress");
                break;

            case (int)TSP.DataManager.AutomationLetterRecieverTypes.Office:
                lblName.DataBindings.Add("Text", dtRecievers, "OfName");
                lblReceiverAddress.DataBindings.Add("Text", dtRecievers, "OfAddress");
                break;

            case (int)TSP.DataManager.AutomationLetterRecieverTypes.Organization:
                lblName.DataBindings.Add("Text", dtRecievers, "OrgName");
                lblReceiverAddress.DataBindings.Add("Text", dtRecievers, "OrgAddress");
                break;

            case (int)TSP.DataManager.AutomationLetterRecieverTypes.OtherPerson:
                lblName.DataBindings.Add("Text", dtRecievers, "OtpFullName");
                lblReceiverAddress.DataBindings.Add("Text", dtRecievers, "OtpAddress");
                break;
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
        string resourceFileName = "XtraReportEnvelope.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblSenderAddress = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblReceiverAddress = new DevExpress.XtraReports.UI.XRLabel();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrPanel1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 624.4167F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.lblName,
            this.xrLabel2,
            this.lblSenderAddress,
            this.xrLabel3,
            this.lblReceiverAddress});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 20.31998F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(2031F, 488.2092F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1878.012F, 25.40002F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(127.9879F, 76.72917F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = ":گیرنده";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrLabel1.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.xrLabel1_HtmlItemCreated);
        // 
        // lblName
        // 
        this.lblName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblName.Dpi = 254F;
        this.lblName.Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Bold);
        this.lblName.LocationFloat = new DevExpress.Utils.PointFloat(888.4713F, 89.88F);
        this.lblName.Multiline = true;
        this.lblName.Name = "lblName";
        this.lblName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblName.SizeF = new System.Drawing.SizeF(989.5412F, 73F);
        this.lblName.StylePriority.UseBorders = false;
        this.lblName.StylePriority.UseFont = false;
        this.lblName.StylePriority.UseTextAlignment = false;
        this.lblName.Text = "lblName";
        this.lblName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.lblName.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.lblName_HtmlItemCreated);
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(1878.013F, 163.3834F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(127.9879F, 84.66667F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = ":نشانی";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblSenderAddress
        // 
        this.lblSenderAddress.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblSenderAddress.Dpi = 254F;
        this.lblSenderAddress.Font = new System.Drawing.Font("B Nazanin", 15F, System.Drawing.FontStyle.Bold);
        this.lblSenderAddress.LocationFloat = new DevExpress.Utils.PointFloat(75.67083F, 25.00001F);
        this.lblSenderAddress.Multiline = true;
        this.lblSenderAddress.Name = "lblSenderAddress";
        this.lblSenderAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblSenderAddress.SizeF = new System.Drawing.SizeF(618.1725F, 438.2093F);
        this.lblSenderAddress.StylePriority.UseBackColor = false;
        this.lblSenderAddress.StylePriority.UseBorders = false;
        this.lblSenderAddress.StylePriority.UseFont = false;
        this.lblSenderAddress.StylePriority.UseTextAlignment = false;
        this.lblSenderAddress.Text = "lblSenderAddress";
        this.lblSenderAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;        
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(705.4851F, 25.40002F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(172.8259F, 92.60416F);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = ":فرستنده";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblReceiverAddress
        // 
        this.lblReceiverAddress.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblReceiverAddress.Dpi = 254F;
        this.lblReceiverAddress.Font = new System.Drawing.Font("B Nazanin", 15F, System.Drawing.FontStyle.Bold);
        this.lblReceiverAddress.LocationFloat = new DevExpress.Utils.PointFloat(888.4711F, 163.3834F);
        this.lblReceiverAddress.Multiline = true;
        this.lblReceiverAddress.Name = "lblReceiverAddress";
        this.lblReceiverAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblReceiverAddress.SizeF = new System.Drawing.SizeF(989.5414F, 299.8259F);
        this.lblReceiverAddress.StylePriority.UseBorders = false;
        this.lblReceiverAddress.StylePriority.UseFont = false;
        this.lblReceiverAddress.StylePriority.UseTextAlignment = false;
        this.lblReceiverAddress.Text = "lblReceiverAddress";
        this.lblReceiverAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.lblReceiverAddress.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.lblReceiverAddress_HtmlItemCreated);
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Dpi = 254F;
        this.topMarginBand1.HeightF = 0F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.Dpi = 254F;
        this.bottomMarginBand1.HeightF = 0F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // xrLine1
        // 
        this.xrLine1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 564.0917F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(2081F, 21.16669F);
        this.xrLine1.StylePriority.UseBorderDashStyle = false;
        // 
        // XtraReportEnvelope
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.DisplayName = " ";
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("B Nazanin", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(10, 10, 0, 0);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "11.2";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void lblName_HtmlItemCreated(object sender, HtmlEventArgs e)
    {
        e.ContentCell.Attributes.CssStyle.Add("direction", "rtl");
    }

    private void lblReceiverAddress_HtmlItemCreated(object sender, HtmlEventArgs e)
    {
        e.ContentCell.Attributes.CssStyle.Add("direction", "rtl");
    }

    private void xrLabel1_HtmlItemCreated(object sender, HtmlEventArgs e)
    {
        //e.ContentCell.Attributes.CssStyle.Add("direction", "rtl");
    }
}
