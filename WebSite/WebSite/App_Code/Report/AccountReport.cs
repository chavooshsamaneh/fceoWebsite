using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// 
/// Summary description for EmployeeReport
/// </summary>
public class AccountReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell8;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel4;
    private XRLabel xrlabelAccCode;
    private XRLabel xrlabelGroupName;
    private XRLabel xrLabel12;
    private XRLabel xrLabel13;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRLabel xrlabelFirstInvoice;
    private XRLabel xrlabelInActiveName;
    private XRLabel xrLabelAccName;
    private XRLabel xrLabelParent;
    private XRLabel xrlabelGroupTypeName;
    private XRLabel xrLabel5;
    private XRLabel xrlabelGroupStatusName;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRLabel xrLabel35;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell83;
    private XRLabel xrLabel42;
    private XRLabel xrLabel44;
    private XRLabel xrlabelCurrentInvoice;
    private XRLabel xrlabelAccDescription;
    TSP.DataManager.AccountingAccountManager AccManager;//= new TSP.DataManager.AccountingAccountManager();
    private XRPageInfo xrPageInfo1;
    private XRTableCell xrTableCell10;
    private XRLabel xrlabelAccTypeName;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell9;
    private XRLabel xrLabel7;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel3;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell12;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRLabel xrLblCurDate;
    private XRLabel xrLabel6;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRPictureBox xrPictureBox2;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public AccountReport(int AccId)
    {
        AccManager = new TSP.DataManager.AccountingAccountManager();
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        AccManager.FindByAccId(AccId);

        BindingControls();

     

     
    }

    public AccountReport(TSP.DataManager.AccountingAccountManager manager)
    {
        AccManager = manager;
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        BindingControls();

    }

   

    private void BindingControls()
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt), pDate.GetMonth(dt), pDate.GetDayOfMonth(dt));
        this.xrLblCurDate.Text = PerDate;

        xrlabelAccCode.DataBindings.Add("Text", AccManager.DataTable, "AccCode");
        //xrPictureBox1.ImageUrl=AccManager[0]["ImgUrl"].ToString();

        xrLabelAccName.DataBindings.Add("Text", AccManager.DataTable, "AccName");
        xrlabelAccCode.DataBindings.Add("Text", AccManager.DataTable, "AccCode");
        xrlabelAccTypeName.DataBindings.Add("Text", AccManager.DataTable, "AccTypeName");
        xrlabelGroupName.DataBindings.Add("Text", AccManager.DataTable, "GroupName");
        xrlabelGroupTypeName.DataBindings.Add("Text", AccManager.DataTable, "GroupTypeName");
        xrlabelGroupStatusName.DataBindings.Add("Text", AccManager.DataTable, "GroupStatusName");
        xrlabelAccDescription.DataBindings.Add("Text", AccManager.DataTable, "AccDescription");
        xrlabelCurrentInvoice.DataBindings.Add("Text", AccManager.DataTable, "CurrentInvoice");
        xrlabelFirstInvoice.DataBindings.Add("Text", AccManager.DataTable, "FirstInvoice");
        xrlabelInActiveName.DataBindings.Add("Text", AccManager.DataTable, "InActiveName");
        xrLabelParent.DataBindings.Add("Text", AccManager.DataTable, "ParentName");

        this.DataSource = AccManager.DataTable;
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
        string resourceFileName = "AccountReport.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelAccCode = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelAccName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelParent = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelAccTypeName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelGroupStatusName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelGroupTypeName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelGroupName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelCurrentInvoice = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelFirstInvoice = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelInActiveName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelAccDescription = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLblCurDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.BorderWidth = 1;
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Height = 343;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.StylePriority.UseBorderWidth = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Location = new System.Drawing.Point(17, 0);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow6,
            this.xrTableRow7,
            this.xrTableRow15,
            this.xrTableRow8});
        this.xrTable2.Size = new System.Drawing.Size(625, 333);
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.BorderColor = System.Drawing.Color.Navy;
        this.xrTableRow2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableRow2.BorderWidth = 2;
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28,
            this.xrTableCell12,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.StylePriority.UseBorderColor = false;
        this.xrTableRow2.StylePriority.UseBorders = false;
        this.xrTableRow2.StylePriority.UseBorderWidth = false;
        this.xrTableRow2.Weight = 1.34;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelAccCode});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 1.4988079999999997;
        // 
        // xrlabelAccCode
        // 
        this.xrlabelAccCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelAccCode.Location = new System.Drawing.Point(42, 25);
        this.xrlabelAccCode.Name = "xrlabelAccCode";
        this.xrlabelAccCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelAccCode.Size = new System.Drawing.Size(184, 25);
        this.xrlabelAccCode.StylePriority.UseBorders = false;
        this.xrlabelAccCode.StylePriority.UseTextAlignment = false;
        this.xrlabelAccCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabelAccName
        // 
        this.xrLabelAccName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabelAccName.Location = new System.Drawing.Point(33, 25);
        this.xrLabelAccName.Name = "xrLabelAccName";
        this.xrLabelAccName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelAccName.Size = new System.Drawing.Size(183, 25);
        this.xrLabelAccName.StylePriority.UseBorders = false;
        this.xrLabelAccName.StylePriority.UseTextAlignment = false;
        this.xrLabelAccName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell6.BorderWidth = 2;
        this.xrTableCell6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorderColor = false;
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseBorderWidth = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.Weight = 0.53804799999999986;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Location = new System.Drawing.Point(16, 25);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.Size = new System.Drawing.Size(56, 25);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "کد حساب";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Location = new System.Drawing.Point(16, 25);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.Size = new System.Drawing.Size(58, 25);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "نام حساب";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell4,
            this.xrTableCell10,
            this.xrTableCell11});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.84000000000000008;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell8.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell8.BorderWidth = 2;
        this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabelParent});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorderColor = false;
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UseBorderWidth = false;
        this.xrTableCell8.Weight = 1.6031039999999999;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.Location = new System.Drawing.Point(442, 8);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.Size = new System.Drawing.Size(200, 25);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabelParent
        // 
        this.xrLabelParent.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabelParent.Location = new System.Drawing.Point(25, 8);
        this.xrLabelParent.Name = "xrLabelParent";
        this.xrLabelParent.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelParent.Size = new System.Drawing.Size(218, 25);
        this.xrLabelParent.StylePriority.UseBorders = false;
        this.xrLabelParent.StylePriority.UseTextAlignment = false;
        this.xrLabelParent.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.Weight = 0.82535199999999986;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Location = new System.Drawing.Point(25, 8);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.Size = new System.Drawing.Size(92, 25);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "زیر حساب";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelAccTypeName});
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.Weight = 1.049912;
        // 
        // xrlabelAccTypeName
        // 
        this.xrlabelAccTypeName.Location = new System.Drawing.Point(46, 8);
        this.xrlabelAccTypeName.Name = "xrlabelAccTypeName";
        this.xrlabelAccTypeName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelAccTypeName.Size = new System.Drawing.Size(110, 25);
        this.xrlabelAccTypeName.StylePriority.UseTextAlignment = false;
        this.xrlabelAccTypeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell11.BorderWidth = 2;
        this.xrTableCell11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorderColor = false;
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseBorderWidth = false;
        this.xrTableCell11.Weight = 0.531632;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Location = new System.Drawing.Point(23, 8);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.Size = new System.Drawing.Size(52, 25);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "نوع حساب";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell9,
            this.xrTableCell7,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 0.9800000000000002;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell20.BorderWidth = 2;
        this.xrTableCell20.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelGroupStatusName});
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBorderColor = false;
        this.xrTableCell20.StylePriority.UseBorders = false;
        this.xrTableCell20.StylePriority.UseBorderWidth = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.Weight = 0.84601600000000043;
        // 
        // xrlabelGroupStatusName
        // 
        this.xrlabelGroupStatusName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelGroupStatusName.Location = new System.Drawing.Point(25, 17);
        this.xrlabelGroupStatusName.Name = "xrlabelGroupStatusName";
        this.xrlabelGroupStatusName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelGroupStatusName.Size = new System.Drawing.Size(101, 25);
        this.xrlabelGroupStatusName.StylePriority.UseBorders = false;
        this.xrlabelGroupStatusName.StylePriority.UseTextAlignment = false;
        this.xrlabelGroupStatusName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.Weight = 0.59646400000000011;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Location = new System.Drawing.Point(17, 17);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.Size = new System.Drawing.Size(67, 25);
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "وضعیت گروه";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelGroupTypeName});
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "xrTableCell7";
        this.xrTableCell7.Weight = 0.801776;
        // 
        // xrlabelGroupTypeName
        // 
        this.xrlabelGroupTypeName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelGroupTypeName.Location = new System.Drawing.Point(25, 17);
        this.xrlabelGroupTypeName.Name = "xrlabelGroupTypeName";
        this.xrlabelGroupTypeName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelGroupTypeName.Size = new System.Drawing.Size(91, 25);
        this.xrlabelGroupTypeName.StylePriority.UseBorders = false;
        this.xrlabelGroupTypeName.StylePriority.UseTextAlignment = false;
        this.xrlabelGroupTypeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13});
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.Weight = 0.480752;
        // 
        // xrLabel13
        // 
        this.xrLabel13.Location = new System.Drawing.Point(17, 17);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.Size = new System.Drawing.Size(50, 25);
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "نوع گروه حساب";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelGroupName});
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.Weight = 0.7533599999999997;
        // 
        // xrlabelGroupName
        // 
        this.xrlabelGroupName.Location = new System.Drawing.Point(25, 17);
        this.xrlabelGroupName.Name = "xrlabelGroupName";
        this.xrlabelGroupName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelGroupName.Size = new System.Drawing.Size(82, 25);
        this.xrlabelGroupName.StylePriority.UseTextAlignment = false;
        this.xrlabelGroupName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell23.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell23.BorderWidth = 2;
        this.xrTableCell23.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseBorderColor = false;
        this.xrTableCell23.StylePriority.UseBorders = false;
        this.xrTableCell23.StylePriority.UseBorderWidth = false;
        this.xrTableCell23.Text = "xrTableCell23";
        this.xrTableCell23.Weight = 0.531632;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Location = new System.Drawing.Point(7, 17);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel12.Size = new System.Drawing.Size(66, 23);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "گروه حساب";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 0.99999999999999978;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell24.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell24.BorderWidth = 2;
        this.xrTableCell24.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelCurrentInvoice});
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBorderColor = false;
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.StylePriority.UseBorderWidth = false;
        this.xrTableCell24.Text = "xrTableCell24";
        this.xrTableCell24.Weight = 1.4427040000000002;
        // 
        // xrlabelCurrentInvoice
        // 
        this.xrlabelCurrentInvoice.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelCurrentInvoice.Location = new System.Drawing.Point(25, 17);
        this.xrlabelCurrentInvoice.Name = "xrlabelCurrentInvoice";
        this.xrlabelCurrentInvoice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelCurrentInvoice.Size = new System.Drawing.Size(193, 25);
        this.xrlabelCurrentInvoice.StylePriority.UseBorders = false;
        this.xrlabelCurrentInvoice.StylePriority.UseTextAlignment = false;
        this.xrlabelCurrentInvoice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7});
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Text = "xrTableCell25";
        this.xrTableCell25.Weight = 0.58937600000000012;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.Location = new System.Drawing.Point(8, 17);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.Size = new System.Drawing.Size(76, 25);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "موجودی جاری";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelFirstInvoice});
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Text = "xrTableCell26";
        this.xrTableCell26.Weight = 1.4462879999999996;
        // 
        // xrlabelFirstInvoice
        // 
        this.xrlabelFirstInvoice.Location = new System.Drawing.Point(32, 17);
        this.xrlabelFirstInvoice.Name = "xrlabelFirstInvoice";
        this.xrlabelFirstInvoice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelFirstInvoice.Size = new System.Drawing.Size(182, 25);
        this.xrlabelFirstInvoice.StylePriority.UseTextAlignment = false;
        this.xrlabelFirstInvoice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell27.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell27.BorderWidth = 2;
        this.xrTableCell27.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35});
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseBorderColor = false;
        this.xrTableCell27.StylePriority.UseBorders = false;
        this.xrTableCell27.StylePriority.UseBorderWidth = false;
        this.xrTableCell27.Text = "xrTableCell27";
        this.xrTableCell27.Weight = 0.53163199999999988;
        // 
        // xrLabel35
        // 
        this.xrLabel35.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel35.Location = new System.Drawing.Point(8, 17);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel35.Size = new System.Drawing.Size(67, 25);
        this.xrLabel35.StylePriority.UseBorders = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "موجودی اولیه";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell77,
            this.xrTableCell83});
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 0.83999999999999986;
        // 
        // xrTableCell77
        // 
        this.xrTableCell77.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell77.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell77.BorderWidth = 2;
        this.xrTableCell77.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelInActiveName});
        this.xrTableCell77.Name = "xrTableCell77";
        this.xrTableCell77.StylePriority.UseBorderColor = false;
        this.xrTableCell77.StylePriority.UseBorders = false;
        this.xrTableCell77.StylePriority.UseBorderWidth = false;
        this.xrTableCell77.Text = "xrTableCell77";
        this.xrTableCell77.Weight = 3.4800080000000007;
        // 
        // xrlabelInActiveName
        // 
        this.xrlabelInActiveName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelInActiveName.Location = new System.Drawing.Point(400, 8);
        this.xrlabelInActiveName.Name = "xrlabelInActiveName";
        this.xrlabelInActiveName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelInActiveName.Size = new System.Drawing.Size(133, 25);
        this.xrlabelInActiveName.StylePriority.UseBorders = false;
        this.xrlabelInActiveName.StylePriority.UseTextAlignment = false;
        this.xrlabelInActiveName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell83
        // 
        this.xrTableCell83.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell83.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell83.BorderWidth = 2;
        this.xrTableCell83.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel42});
        this.xrTableCell83.Name = "xrTableCell83";
        this.xrTableCell83.StylePriority.UseBorderColor = false;
        this.xrTableCell83.StylePriority.UseBorders = false;
        this.xrTableCell83.StylePriority.UseBorderWidth = false;
        this.xrTableCell83.Text = "xrTableCell83";
        this.xrTableCell83.Weight = 0.52999199999999913;
        // 
        // xrLabel42
        // 
        this.xrLabel42.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel42.Location = new System.Drawing.Point(7, 8);
        this.xrLabel42.Name = "xrLabel42";
        this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel42.Size = new System.Drawing.Size(66, 25);
        this.xrLabel42.StylePriority.UseBorders = false;
        this.xrLabel42.StylePriority.UseTextAlignment = false;
        this.xrLabel42.Text = "وضعیت ";
        this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell35});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1.66;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell34.BorderWidth = 2;
        this.xrTableCell34.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelAccDescription});
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBorderColor = false;
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.StylePriority.UseBorderWidth = false;
        this.xrTableCell34.StylePriority.UseTextAlignment = false;
        this.xrTableCell34.Text = "xrTableCell34";
        this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell34.Weight = 3.478368;
        // 
        // xrlabelAccDescription
        // 
        this.xrlabelAccDescription.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelAccDescription.Location = new System.Drawing.Point(8, 17);
        this.xrlabelAccDescription.Multiline = true;
        this.xrlabelAccDescription.Name = "xrlabelAccDescription";
        this.xrlabelAccDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelAccDescription.Size = new System.Drawing.Size(525, 50);
        this.xrlabelAccDescription.StylePriority.UseBorders = false;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell35.BorderWidth = 2;
        this.xrTableCell35.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel44});
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseBorderColor = false;
        this.xrTableCell35.StylePriority.UseBorders = false;
        this.xrTableCell35.StylePriority.UseBorderWidth = false;
        this.xrTableCell35.Text = "xrTableCell35";
        this.xrTableCell35.Weight = 0.53163199999999988;
        // 
        // xrLabel44
        // 
        this.xrLabel44.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel44.Location = new System.Drawing.Point(24, 17);
        this.xrLabel44.Name = "xrLabel44";
        this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel44.Size = new System.Drawing.Size(50, 25);
        this.xrLabel44.StylePriority.UseBorders = false;
        this.xrLabel44.StylePriority.UseTextAlignment = false;
        this.xrLabel44.Text = "توضیحات";
        this.xrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.Height = 33;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Location = new System.Drawing.Point(333, 8);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.Size = new System.Drawing.Size(25, 25);
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
        // xrTableCell28
        // 
        this.xrTableCell28.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell28.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell28.BorderWidth = 2;
        this.xrTableCell28.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelAccName});
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseBorderColor = false;
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.StylePriority.UseBorderWidth = false;
        this.xrTableCell28.Text = "xrTableCell28";
        this.xrTableCell28.Weight = 1.443152;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.Weight = 0.5299919999999998;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.ReportHeader.Height = 139;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrTable1
        // 
        this.xrTable1.Location = new System.Drawing.Point(17, 8);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.Size = new System.Drawing.Size(625, 125);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.BorderColor = System.Drawing.Color.Navy;
        this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow1.BorderWidth = 2;
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.StylePriority.UseBorderColor = false;
        this.xrTableRow1.StylePriority.UseBorders = false;
        this.xrTableRow1.StylePriority.UseBorderWidth = false;
        this.xrTableRow1.Weight = 1.1574074074074074;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLblCurDate,
            this.xrLabel6});
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorderColor = false;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseBorderWidth = false;
        this.xrTableCell1.Text = "xrTableCell1";
        this.xrTableCell1.Weight = 1.12;
        // 
        // xrLblCurDate
        // 
        this.xrLblCurDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLblCurDate.Location = new System.Drawing.Point(58, 75);
        this.xrLblCurDate.Name = "xrLblCurDate";
        this.xrLblCurDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLblCurDate.Size = new System.Drawing.Size(100, 25);
        this.xrLblCurDate.StylePriority.UseBorders = false;
        this.xrLblCurDate.StylePriority.UseTextAlignment = false;
        this.xrLblCurDate.Text = "xrLblCurDate";
        this.xrLblCurDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.Location = new System.Drawing.Point(175, 75);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.Size = new System.Drawing.Size(33, 25);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "تاریخ";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorderColor = false;
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseBorderWidth = false;
        this.xrTableCell2.Weight = 0.88;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell3.BorderWidth = 2;
        this.xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorderColor = false;
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseBorderWidth = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.Weight = 1;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.ImageUrl = "~\\Image\\arm.jpg";
        this.xrPictureBox2.Location = new System.Drawing.Point(50, 17);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.Size = new System.Drawing.Size(117, 100);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // AccountReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageFooter,
            this.ReportHeader});
        this.Margins = new System.Drawing.Printing.Margins(100, 99, 100, 100);
        this.Name = "AccountReport";
        this.PageHeight = 1100;
        this.PageWidth = 850;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
