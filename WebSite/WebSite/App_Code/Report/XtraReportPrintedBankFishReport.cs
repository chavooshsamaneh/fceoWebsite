using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportXtraReportPrintedBankFishReport
/// </summary>
public class XtraReportPrintedBankFishReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private System.Data.DataTable dtPrint;//= new TSP.DataManager.AccountingAccountManager();
    private XRLabel xrLabel8;
    private XRLabel xrLabelBankNamet;
    private XRLabel xrLabelOwnerNamet;
    private XRLabel xrLabelBankAccNumbert;
    private XRLabel xrLabelPrice1t;
    private XRLabel xrLabelPrice2t;
    private XRLabel xrLabelAccNamet;
    private XRLabel xrLabelDescriptiont;
    private XRLabel xrLabelBankNameb;
    private XRLabel xrLabelBankAccNumberb;
    private XRLabel xrLabelOwnerNameb;
    private XRLabel xrLabelPrice2b;
    private XRLabel xrLabelPrice1b;
    private XRLabel xrLabelAccNameb;
    private XRLabel xrLabelDescriptionb;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportPrintedBankFishReport(System.Data.DataTable dt)
    {

        dtPrint = dt;

        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        BindingControls();

    }



    private void BindingControls()
    {
        //DateTime dt = new DateTime();
        //dt = DateTime.Now;
        //System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        //string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt), pDate.GetMonth(dt), pDate.GetDayOfMonth(dt));
        //this.xrLblCurDate.Text = PerDate;

        xrLabelBankNamet.DataBindings.Add("Text", dtPrint, "BankName");
        xrLabelBankNameb.DataBindings.Add("Text", dtPrint, "BankName");

        xrLabelOwnerNamet.DataBindings.Add("Text", dtPrint, "OwnerName");
        xrLabelOwnerNameb.DataBindings.Add("Text", dtPrint, "OwnerName");

        xrLabelBankAccNumbert.DataBindings.Add("Text", dtPrint, "BankAccNumber");
        xrLabelBankAccNumberb.DataBindings.Add("Text", dtPrint, "BankAccNumber");


        xrLabelPrice1t.DataBindings.Add("Text", dtPrint, "Price1","{0:#,#}");
        xrLabelPrice1b.DataBindings.Add("Text", dtPrint, "Price1","{0:#,#}");

        xrLabelPrice2t.DataBindings.Add("Text", dtPrint, "Price2");
        xrLabelPrice2b.DataBindings.Add("Text", dtPrint, "Price2");

        xrLabelAccNamet.DataBindings.Add("Text", dtPrint, "AccName");
        xrLabelAccNameb.DataBindings.Add("Text", dtPrint, "AccName");

        xrLabelDescriptiont.DataBindings.Add("Text", dtPrint, "Description");
        xrLabelDescriptionb.DataBindings.Add("Text", dtPrint, "Description");
                
        this.DataSource = dtPrint;

        xrLabelPrice1t.DataBindings[0].FormatString = "{0:#,#}";
        xrLabelPrice1b.DataBindings[0].FormatString = "{0:#,#}";

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
        string resourceFileName = "XtraReportPrintedBankFishReport.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabelDescriptionb = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelAccNameb = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPrice2b = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPrice1b = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelBankAccNumberb = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelOwnerNameb = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelBankNameb = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelDescriptiont = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelAccNamet = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPrice2t = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelPrice1t = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelBankAccNumbert = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelOwnerNamet = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelBankNamet = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.BorderWidth = 1;
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelDescriptionb,
            this.xrLabelAccNameb,
            this.xrLabelPrice2b,
            this.xrLabelPrice1b,
            this.xrLabelBankAccNumberb,
            this.xrLabelOwnerNameb,
            this.xrLabelBankNameb,
            this.xrLabelDescriptiont,
            this.xrLabelAccNamet,
            this.xrLabelPrice2t,
            this.xrLabelPrice1t,
            this.xrLabelBankAccNumbert,
            this.xrLabelOwnerNamet,
            this.xrLabelBankNamet});
        this.Detail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Detail.Height = 907;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.StylePriority.UseBorderWidth = false;
        this.Detail.StylePriority.UseFont = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabelDescriptionb
        // 
        this.xrLabelDescriptionb.Location = new System.Drawing.Point(17, 775);
        this.xrLabelDescriptionb.Multiline = true;
        this.xrLabelDescriptionb.Name = "xrLabelDescriptionb";
        this.xrLabelDescriptionb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelDescriptionb.Size = new System.Drawing.Size(242, 92);
        this.xrLabelDescriptionb.StylePriority.UseTextAlignment = false;
        this.xrLabelDescriptionb.Text = "xrLabelDescription";
        this.xrLabelDescriptionb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelAccNameb
        // 
        this.xrLabelAccNameb.Location = new System.Drawing.Point(225, 633);
        this.xrLabelAccNameb.Name = "xrLabelAccNameb";
        this.xrLabelAccNameb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelAccNameb.Size = new System.Drawing.Size(242, 25);
        this.xrLabelAccNameb.StylePriority.UseTextAlignment = false;
        this.xrLabelAccNameb.Text = "xrLabelAccNameb";
        this.xrLabelAccNameb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPrice2b
        // 
        this.xrLabelPrice2b.Location = new System.Drawing.Point(50, 598);
        this.xrLabelPrice2b.Name = "xrLabelPrice2b";
        this.xrLabelPrice2b.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPrice2b.Size = new System.Drawing.Size(283, 25);
        this.xrLabelPrice2b.StylePriority.UseTextAlignment = false;
        this.xrLabelPrice2b.Text = "xrLabelPrice2";
        this.xrLabelPrice2b.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPrice1b
        // 
        this.xrLabelPrice1b.Location = new System.Drawing.Point(433, 600);
        this.xrLabelPrice1b.Name = "xrLabelPrice1b";
        this.xrLabelPrice1b.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPrice1b.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPrice1b.StylePriority.UseTextAlignment = false;
        this.xrLabelPrice1b.Text = "xrLabelPrice1";
        this.xrLabelPrice1b.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabelBankAccNumberb
        // 
        this.xrLabelBankAccNumberb.Location = new System.Drawing.Point(50, 567);
        this.xrLabelBankAccNumberb.Name = "xrLabelBankAccNumberb";
        this.xrLabelBankAccNumberb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelBankAccNumberb.Size = new System.Drawing.Size(208, 25);
        this.xrLabelBankAccNumberb.StylePriority.UseTextAlignment = false;
        this.xrLabelBankAccNumberb.Text = "xrLabelBankAccNumber";
        this.xrLabelBankAccNumberb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelOwnerNameb
        // 
        this.xrLabelOwnerNameb.Location = new System.Drawing.Point(330, 568);
        this.xrLabelOwnerNameb.Name = "xrLabelOwnerNameb";
        this.xrLabelOwnerNameb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelOwnerNameb.Size = new System.Drawing.Size(217, 25);
        this.xrLabelOwnerNameb.StylePriority.UseTextAlignment = false;
        this.xrLabelOwnerNameb.Text = "xrLabelOwnerName";
        this.xrLabelOwnerNameb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelBankNameb
        // 
        this.xrLabelBankNameb.Location = new System.Drawing.Point(53, 528);
        this.xrLabelBankNameb.Name = "xrLabelBankNameb";
        this.xrLabelBankNameb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelBankNameb.Size = new System.Drawing.Size(267, 25);
        this.xrLabelBankNameb.StylePriority.UseTextAlignment = false;
        this.xrLabelBankNameb.Text = "xrLabelBankName";
        this.xrLabelBankNameb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelDescriptiont
        // 
        this.xrLabelDescriptiont.Location = new System.Drawing.Point(17, 325);
        this.xrLabelDescriptiont.Multiline = true;
        this.xrLabelDescriptiont.Name = "xrLabelDescriptiont";
        this.xrLabelDescriptiont.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelDescriptiont.Size = new System.Drawing.Size(242, 92);
        this.xrLabelDescriptiont.StylePriority.UseTextAlignment = false;
        this.xrLabelDescriptiont.Text = "xrLabelDescriptiont";
        this.xrLabelDescriptiont.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelAccNamet
        // 
        this.xrLabelAccNamet.Location = new System.Drawing.Point(225, 200);
        this.xrLabelAccNamet.Name = "xrLabelAccNamet";
        this.xrLabelAccNamet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelAccNamet.Size = new System.Drawing.Size(242, 25);
        this.xrLabelAccNamet.StylePriority.UseTextAlignment = false;
        this.xrLabelAccNamet.Text = "xrLabelAccNamet";
        this.xrLabelAccNamet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPrice2t
        // 
        this.xrLabelPrice2t.Location = new System.Drawing.Point(50, 167);
        this.xrLabelPrice2t.Name = "xrLabelPrice2t";
        this.xrLabelPrice2t.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPrice2t.Size = new System.Drawing.Size(283, 25);
        this.xrLabelPrice2t.StylePriority.UseTextAlignment = false;
        this.xrLabelPrice2t.Text = "xrLabelPrice2t";
        this.xrLabelPrice2t.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPrice1t
        // 
        this.xrLabelPrice1t.Location = new System.Drawing.Point(429, 167);
        this.xrLabelPrice1t.Name = "xrLabelPrice1t";
        this.xrLabelPrice1t.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPrice1t.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPrice1t.StylePriority.UseTextAlignment = false;
        this.xrLabelPrice1t.Text = "xrLabelPrice1t";
        this.xrLabelPrice1t.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabelBankAccNumbert
        // 
        this.xrLabelBankAccNumbert.Location = new System.Drawing.Point(55, 133);
        this.xrLabelBankAccNumbert.Name = "xrLabelBankAccNumbert";
        this.xrLabelBankAccNumbert.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelBankAccNumbert.Size = new System.Drawing.Size(208, 25);
        this.xrLabelBankAccNumbert.StylePriority.UseTextAlignment = false;
        this.xrLabelBankAccNumbert.Text = "xrLabelBankAccNumbert";
        this.xrLabelBankAccNumbert.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelOwnerNamet
        // 
        this.xrLabelOwnerNamet.Location = new System.Drawing.Point(325, 133);
        this.xrLabelOwnerNamet.Name = "xrLabelOwnerNamet";
        this.xrLabelOwnerNamet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelOwnerNamet.Size = new System.Drawing.Size(225, 33);
        this.xrLabelOwnerNamet.StylePriority.UseTextAlignment = false;
        this.xrLabelOwnerNamet.Text = "xrLabelOwnerNamet";
        this.xrLabelOwnerNamet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelBankNamet
        // 
        this.xrLabelBankNamet.Location = new System.Drawing.Point(58, 92);
        this.xrLabelBankNamet.Name = "xrLabelBankNamet";
        this.xrLabelBankNamet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelBankNamet.Size = new System.Drawing.Size(267, 33);
        this.xrLabelBankNamet.StylePriority.UseTextAlignment = false;
        this.xrLabelBankNamet.Text = "xrLabelBankNamet";
        this.xrLabelBankNamet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Location = new System.Drawing.Point(12, 8);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.Size = new System.Drawing.Size(85, 25);
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // XtraReportPrintedBankFishReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.Margins = new System.Drawing.Printing.Margins(100, 99, 100, 100);
        this.Name = "XtraReportPrintedBankFishReport";
        this.PageHeight = 1100;
        this.PageWidth = 850;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
