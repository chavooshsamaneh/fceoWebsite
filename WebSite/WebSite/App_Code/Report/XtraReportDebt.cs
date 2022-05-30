using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportDebt
/// </summary>
public class XtraReportDebt : DevExpress.XtraReports.UI.XtraReport
{
    TSP.DataManager.AccountingAccountManager AccountManager;
    TSP.DataManager.AccountingDebtManager DebtManager;

    private DevExpress.XtraReports.UI.DetailBand Detail;
    private XRLabel xrLabel8;
    private XRTable xrTable5;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell80;
    private XRTable xrTable4;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell81;
    private XRLabel xrLabel38;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLBedSum;
    private XRLabel xrLBesSum;
    private XRLabel xrLabelBankNamet;
    private XRLabel xrLabelAccNameb;
    private XRLabel xrLabelPrice2b;
    private XRLabel xrLabelPrice1b;
    private XRLabel xrLabelBankAccNumberb;
    private XRLabel xrLabelOwnerNameb;
    private XRLabel xrLabelBankNameb;
    private XRLabel xrLabelDescriptiont;
    private XRLabel xrLabelAccNamet;
    private XRLabel xrLabelPrice2t;
    private XRLabel xrLabelPrice1t;
    private XRLabel xrLabelBankAccNumbert;
    private XRLabel xrLabelOwnerNamet;
    private XRLabel xrLabelDescriptionb;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportDebt(int LoanPaymentId, int BankAccId, int PaymentFacilitiesAccId, string FilterExp)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        AccountManager = new TSP.DataManager.AccountingAccountManager();
        DebtManager = new TSP.DataManager.AccountingDebtManager();

        AccountManager.FindByAccId(BankAccId);
        xrLabelBankNameb.Text = AccountManager[0]["AccName"].ToString();
        xrLabelBankNamet.Text = AccountManager[0]["AccName"].ToString();
        xrLabelBankAccNumberb.Text = AccountManager[0]["AccCode"].ToString();
        xrLabelBankAccNumbert.Text = AccountManager[0]["AccCode"].ToString();

        AccountManager.FindByAccId(PaymentFacilitiesAccId);
        xrLabelAccNamet.Text = AccountManager[0]["AccName"].ToString() + "  " + AccountManager[0]["AccCode"].ToString();
        xrLabelAccNameb.Text = AccountManager[0]["AccName"].ToString() + "  " + AccountManager[0]["AccCode"].ToString();       

        xrLabelOwnerNamet.Text = "سازمان نظام مهندسی ساختمان فارس";
        xrLabelOwnerNameb.Text = "سازمان نظام مهندسی ساختمان فارس";

        xrLabelDescriptiont.Text = "بابت پرداخت قسط";
        xrLabelDescriptionb.Text = "بابت پرداخت قسط";

        xrLabelPrice2t.Text = "0";
        xrLabelPrice2b.Text = "0";

        System.Data.DataTable dt = DebtManager.FindByLoanPaymentId(LoanPaymentId);
        dt.DefaultView.RowFilter = FilterExp;
        this.DataSource = dt;

        xrLabelPrice1t.DataBindings.Add("Text", dt, "TotalAmount", "{0:#,#}");
        xrLabelPrice1b.DataBindings.Add("Text", dt, "TotalAmount", "{0:#,#}");
        
        xrLabelPrice1t.DataBindings[0].FormatString = "{0:#,#}";
        xrLabelPrice1b.DataBindings[0].FormatString = "{0:#,#}";
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

    private string GetAgentName(int AgentId)
    {
        TSP.DataManager.AccountingAgentManager AgentManager = new TSP.DataManager.AccountingAgentManager();
        AgentManager.FindByCode(AgentId);
        if (AgentManager.Count == 1)
            return (AgentManager[0]["Name"].ToString());
        else
            return "";
    }

    private string GetAccName(int AccId)
    {
        string Name = "";
        TSP.DataManager.AccountingAccountManager Manager = new TSP.DataManager.AccountingAccountManager();
        Manager.FindByAccId(AccId);
        if (Manager.Count == 1)
        {
            Name = Manager[0]["AccName"].ToString();

            while (!string.IsNullOrEmpty(Manager[0]["ParentId"].ToString()))
            {
                AccId = Convert.ToInt32(Manager[0]["ParentId"]);
                Manager.DataTable.Clear();
                Manager.FindByAccId(AccId);
                Name = Manager[0]["AccName"].ToString() + " - " + Name;
            }
        }
        return Name;
    }

    private string GetAccCode(int AccId)
    {
        TSP.DataManager.AccountingAccountManager Manager = new TSP.DataManager.AccountingAccountManager();
        Manager.FindByAccId(AccId);
        if (Manager.Count == 1)
        {
            return (Manager[0]["AccCode"].ToString());
        }
        else
            return "";
    }

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
        string resourceFileName = "XtraReportDebt.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabelBankNamet = new DevExpress.XtraReports.UI.XRLabel();
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
        this.xrLabelDescriptionb = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLBedSum = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLBesSum = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelBankNamet,
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
            this.xrLabelDescriptionb});
        this.Detail.Height = 906;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.Detail.StylePriority.UseBorderWidth = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabelBankNamet
        // 
        this.xrLabelBankNamet.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelBankNamet.Location = new System.Drawing.Point(108, 92);
        this.xrLabelBankNamet.Name = "xrLabelBankNamet";
        this.xrLabelBankNamet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelBankNamet.Size = new System.Drawing.Size(267, 33);
        this.xrLabelBankNamet.StylePriority.UseFont = false;
        this.xrLabelBankNamet.StylePriority.UseTextAlignment = false;
        this.xrLabelBankNamet.Text = "xrLabelBankNamet";
        this.xrLabelBankNamet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelAccNameb
        // 
        this.xrLabelAccNameb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelAccNameb.Location = new System.Drawing.Point(275, 633);
        this.xrLabelAccNameb.Name = "xrLabelAccNameb";
        this.xrLabelAccNameb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelAccNameb.Size = new System.Drawing.Size(242, 25);
        this.xrLabelAccNameb.StylePriority.UseFont = false;
        this.xrLabelAccNameb.StylePriority.UseTextAlignment = false;
        this.xrLabelAccNameb.Text = "xrLabelAccNameb";
        this.xrLabelAccNameb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPrice2b
        // 
        this.xrLabelPrice2b.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelPrice2b.Location = new System.Drawing.Point(100, 600);
        this.xrLabelPrice2b.Name = "xrLabelPrice2b";
        this.xrLabelPrice2b.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPrice2b.Size = new System.Drawing.Size(283, 25);
        this.xrLabelPrice2b.StylePriority.UseFont = false;
        this.xrLabelPrice2b.StylePriority.UseTextAlignment = false;
        this.xrLabelPrice2b.Text = "xrLabelPrice2";
        this.xrLabelPrice2b.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPrice1b
        // 
        this.xrLabelPrice1b.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelPrice1b.Location = new System.Drawing.Point(483, 600);
        this.xrLabelPrice1b.Name = "xrLabelPrice1b";
        this.xrLabelPrice1b.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPrice1b.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPrice1b.StylePriority.UseFont = false;
        this.xrLabelPrice1b.StylePriority.UseTextAlignment = false;
        this.xrLabelPrice1b.Text = "xrLabelPrice1";
        this.xrLabelPrice1b.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabelBankAccNumberb
        // 
        this.xrLabelBankAccNumberb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelBankAccNumberb.Location = new System.Drawing.Point(100, 567);
        this.xrLabelBankAccNumberb.Name = "xrLabelBankAccNumberb";
        this.xrLabelBankAccNumberb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelBankAccNumberb.Size = new System.Drawing.Size(208, 25);
        this.xrLabelBankAccNumberb.StylePriority.UseFont = false;
        this.xrLabelBankAccNumberb.StylePriority.UseTextAlignment = false;
        this.xrLabelBankAccNumberb.Text = "xrLabelBankAccNumber";
        this.xrLabelBankAccNumberb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelOwnerNameb
        // 
        this.xrLabelOwnerNameb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelOwnerNameb.Location = new System.Drawing.Point(383, 558);
        this.xrLabelOwnerNameb.Name = "xrLabelOwnerNameb";
        this.xrLabelOwnerNameb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelOwnerNameb.Size = new System.Drawing.Size(225, 33);
        this.xrLabelOwnerNameb.StylePriority.UseFont = false;
        this.xrLabelOwnerNameb.StylePriority.UseTextAlignment = false;
        this.xrLabelOwnerNameb.Text = "xrLabelOwnerName";
        this.xrLabelOwnerNameb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelBankNameb
        // 
        this.xrLabelBankNameb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelBankNameb.Location = new System.Drawing.Point(100, 525);
        this.xrLabelBankNameb.Name = "xrLabelBankNameb";
        this.xrLabelBankNameb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelBankNameb.Size = new System.Drawing.Size(267, 25);
        this.xrLabelBankNameb.StylePriority.UseFont = false;
        this.xrLabelBankNameb.StylePriority.UseTextAlignment = false;
        this.xrLabelBankNameb.Text = "xrLabelBankName";
        this.xrLabelBankNameb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelDescriptiont
        // 
        this.xrLabelDescriptiont.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelDescriptiont.Location = new System.Drawing.Point(67, 325);
        this.xrLabelDescriptiont.Multiline = true;
        this.xrLabelDescriptiont.Name = "xrLabelDescriptiont";
        this.xrLabelDescriptiont.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelDescriptiont.Size = new System.Drawing.Size(242, 92);
        this.xrLabelDescriptiont.StylePriority.UseFont = false;
        this.xrLabelDescriptiont.StylePriority.UseTextAlignment = false;
        this.xrLabelDescriptiont.Text = "xrLabelDescriptiont";
        this.xrLabelDescriptiont.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabelAccNamet
        // 
        this.xrLabelAccNamet.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelAccNamet.Location = new System.Drawing.Point(275, 200);
        this.xrLabelAccNamet.Name = "xrLabelAccNamet";
        this.xrLabelAccNamet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelAccNamet.Size = new System.Drawing.Size(242, 25);
        this.xrLabelAccNamet.StylePriority.UseFont = false;
        this.xrLabelAccNamet.StylePriority.UseTextAlignment = false;
        this.xrLabelAccNamet.Text = "xrLabelAccNamet";
        this.xrLabelAccNamet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPrice2t
        // 
        this.xrLabelPrice2t.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelPrice2t.Location = new System.Drawing.Point(100, 167);
        this.xrLabelPrice2t.Name = "xrLabelPrice2t";
        this.xrLabelPrice2t.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPrice2t.Size = new System.Drawing.Size(283, 25);
        this.xrLabelPrice2t.StylePriority.UseFont = false;
        this.xrLabelPrice2t.StylePriority.UseTextAlignment = false;
        this.xrLabelPrice2t.Text = "xrLabelPrice2t";
        this.xrLabelPrice2t.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelPrice1t
        // 
        this.xrLabelPrice1t.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelPrice1t.Location = new System.Drawing.Point(483, 167);
        this.xrLabelPrice1t.Name = "xrLabelPrice1t";
        this.xrLabelPrice1t.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelPrice1t.Size = new System.Drawing.Size(175, 25);
        this.xrLabelPrice1t.StylePriority.UseFont = false;
        this.xrLabelPrice1t.StylePriority.UseTextAlignment = false;
        this.xrLabelPrice1t.Text = "xrLabelPrice1t";
        this.xrLabelPrice1t.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabelBankAccNumbert
        // 
        this.xrLabelBankAccNumbert.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelBankAccNumbert.Location = new System.Drawing.Point(108, 133);
        this.xrLabelBankAccNumbert.Name = "xrLabelBankAccNumbert";
        this.xrLabelBankAccNumbert.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelBankAccNumbert.Size = new System.Drawing.Size(208, 25);
        this.xrLabelBankAccNumbert.StylePriority.UseFont = false;
        this.xrLabelBankAccNumbert.StylePriority.UseTextAlignment = false;
        this.xrLabelBankAccNumbert.Text = "xrLabelBankAccNumbert";
        this.xrLabelBankAccNumbert.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelOwnerNamet
        // 
        this.xrLabelOwnerNamet.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelOwnerNamet.Location = new System.Drawing.Point(375, 133);
        this.xrLabelOwnerNamet.Name = "xrLabelOwnerNamet";
        this.xrLabelOwnerNamet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelOwnerNamet.Size = new System.Drawing.Size(225, 33);
        this.xrLabelOwnerNamet.StylePriority.UseFont = false;
        this.xrLabelOwnerNamet.StylePriority.UseTextAlignment = false;
        this.xrLabelOwnerNamet.Text = "xrLabelOwnerNamet";
        this.xrLabelOwnerNamet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelDescriptionb
        // 
        this.xrLabelDescriptionb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabelDescriptionb.Location = new System.Drawing.Point(67, 775);
        this.xrLabelDescriptionb.Multiline = true;
        this.xrLabelDescriptionb.Name = "xrLabelDescriptionb";
        this.xrLabelDescriptionb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelDescriptionb.Size = new System.Drawing.Size(242, 92);
        this.xrLabelDescriptionb.StylePriority.UseFont = false;
        this.xrLabelDescriptionb.StylePriority.UseTextAlignment = false;
        this.xrLabelDescriptionb.Text = "xrLabelDescription";
        this.xrLabelDescriptionb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 2;
        this.xrTable5.Location = new System.Drawing.Point(0, 8);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable5.Size = new System.Drawing.Size(725, 50);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1.0079365079365079;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Weight = 3;
        // 
        // xrTable4
        // 
        this.xrTable4.BorderWidth = 1;
        this.xrTable4.Location = new System.Drawing.Point(8, 8);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable4.Size = new System.Drawing.Size(708, 33);
        this.xrTable4.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81});
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 1;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrPageInfo1});
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Weight = 3;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel38.Location = new System.Drawing.Point(117, 8);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel38.Size = new System.Drawing.Size(567, 17);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "سازمان نظام مهندسی ساختمان استان فارس ( واحد مالی )   تلفن:   8-071162" +
            "74194";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.Location = new System.Drawing.Point(33, 8);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.Size = new System.Drawing.Size(33, 19);
        this.xrPageInfo1.StylePriority.UseBorders = false;
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
        // xrLBedSum
        // 
        this.xrLBedSum.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLBedSum.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrLBedSum.Location = new System.Drawing.Point(108, 0);
        this.xrLBedSum.Name = "xrLBedSum";
        this.xrLBedSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLBedSum.Size = new System.Drawing.Size(100, 17);
        this.xrLBedSum.StylePriority.UseBorders = false;
        this.xrLBedSum.StylePriority.UseFont = false;
        this.xrLBedSum.StylePriority.UseTextAlignment = false;
        xrSummary1.FormatString = "{0:#,#}";
        this.xrLBedSum.Summary = xrSummary1;
        this.xrLBedSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLBesSum
        // 
        this.xrLBesSum.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLBesSum.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrLBesSum.Location = new System.Drawing.Point(0, 0);
        this.xrLBesSum.Name = "xrLBesSum";
        this.xrLBesSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLBesSum.Size = new System.Drawing.Size(108, 17);
        this.xrLBesSum.StylePriority.UseBorders = false;
        this.xrLBesSum.StylePriority.UseFont = false;
        this.xrLBesSum.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:#,#}";
        this.xrLBesSum.Summary = xrSummary2;
        this.xrLBesSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // XtraReportDebt
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.Margins = new System.Drawing.Printing.Margins(100, 0, 100, 100);
        this.Name = "XtraReportDebt";
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
