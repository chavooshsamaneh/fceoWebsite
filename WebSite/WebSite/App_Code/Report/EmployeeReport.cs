using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// 
/// Summary description for EmployeeReport
/// </summary>
public class EmployeeReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrlabelEmpCode;
    private XRLabel xrlabelFirstName;
    private XRLabel xrlabelLastName;
    private XRLabel xrLabelFatherName;
    private XRLabel xrLabel11;
    private XRLabel xrLabel12;
    private XRLabel xrLabel13;
    private XRLabel xrLabel14;
    private XRLabel xrLabel15;
    private XRLabel xrlabelBirthDate;
    private XRLabel xrlabelIdNo;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRLabel xrLabel18;
    private XRLabel xrlabelWebSite;
    private XRLabel xrLabel20;
    private XRLabel xrlabelEmail;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell49;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabelFirstNameEn;
    private XRLabel xrLabelLastNameEn;
    private XRLabel xrLabel24;
    private XRLabel xrlabelBirthPlace;
    private XRLabel xrLabel5;
    private XRLabel xrlabelSSN;
    private XRLabel xrLabel26;
    private XRLabel xrlabelSexId;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRLabel xrLabel28;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRLabel xrLabel30;
    private XRLabel xrlabelTel;
    private XRLabel xrLabel32;
    private XRLabel xrlabelMobileNo;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRLabel xrlabeladdress;
    private XRLabel xrLabel35;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell72;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell78;
    private XRTableCell xrTableCell80;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell83;
    private XRLabel xrLabel36;
    private XRLabel xrlabelNationality;
    private XRLabel xrLabel38;
    private XRLabel xrlabelRelId;
    private XRLabel xrLabel40;
    private XRLabel xrlabelPartId;
    private XRLabel xrLabel42;
    private XRLabel xrlabelCreateDate;
    private XRLabel xrLabel44;
    private XRLabel xrLabel45;
    private XRLabel xrlabelEmpStatus;
    private XRLabel xrlabelDescription;
    private XRLabel xrlabelMarId;
    TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
    private XRLabel xrLblCurDate;
    private XRLabel xrLabel6;
    private XRPictureBox xrPictureBox2;
    private XRPageInfo xrPageInfo1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public EmployeeReport(int EmpId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        EmpManager.FindByCode(EmpId);
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt), pDate.GetMonth(dt), pDate.GetDayOfMonth(dt));
        this.xrLblCurDate.Text = PerDate;
        xrlabelEmpCode.DataBindings.Add("Text", EmpManager.DataTable, "EmpCode");
        xrPictureBox1.ImageUrl=EmpManager[0]["ImgUrl"].ToString();
        xrlabelFirstName.DataBindings.Add("Text", EmpManager.DataTable, "FirstName");
        xrlabelLastName.DataBindings.Add("Text", EmpManager.DataTable, "LastName");
        xrLabelFirstNameEn.DataBindings.Add("Text", EmpManager.DataTable, "FirstNameEn");
        xrLabelLastNameEn.DataBindings.Add("Text", EmpManager.DataTable, "LastNameEn");
        xrLabelFatherName.DataBindings.Add("Text", EmpManager.DataTable, "FatherName");
        xrlabelBirthDate.DataBindings.Add("Text", EmpManager.DataTable, "BirthDate");        
        xrlabelBirthPlace.DataBindings.Add("Text", EmpManager.DataTable, "BirthPlace");
        xrlabelIdNo.DataBindings.Add("Text", EmpManager.DataTable, "IdNo");
        xrlabelSSN.DataBindings.Add("Text", EmpManager.DataTable, "SSN");
        xrlabelSexId.DataBindings.Add("Text", EmpManager.DataTable, "SexName");
        xrlabelMarId.DataBindings.Add("Text", EmpManager.DataTable, "MarName");
        xrlabeladdress.DataBindings.Add("Text", EmpManager.DataTable, "Address");
        xrlabelTel.DataBindings.Add("Text", EmpManager.DataTable, "Tel");
        xrlabelMobileNo.DataBindings.Add("Text", EmpManager.DataTable, "MobileNo");
        xrlabelWebSite.DataBindings.Add("Text", EmpManager.DataTable, "WebSite");
        xrlabelEmail.DataBindings.Add("Text", EmpManager.DataTable, "Email");
        xrlabelNationality.DataBindings.Add("Text", EmpManager.DataTable, "Nationality");
        xrlabelRelId.DataBindings.Add("Text", EmpManager.DataTable, "RelName");
        xrlabelCreateDate.DataBindings.Add("Text", EmpManager.DataTable, "CreateDate");
        xrlabelEmpStatus.DataBindings.Add("Text", EmpManager.DataTable, "StatusType");       
     
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
        string resourceFileName = "EmployeeReport.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelEmpCode = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabelFirstNameEn = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelFirstName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabelLastNameEn = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelLastName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelBirthPlace = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelBirthDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabelFatherName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelSexId = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelSSN = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelIdNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelMarId = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabeladdress = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelMobileNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelTel = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelEmail = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelWebSite = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelPartId = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelRelId = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelNationality = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelEmpStatus = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelCreateDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrlabelDescription = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLblCurDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.BorderWidth = 1;
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Height = 685;
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
            this.xrTableRow5,
            this.xrTableRow4,
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow9,
            this.xrTableRow7,
            this.xrTableRow15,
            this.xrTableRow14,
            this.xrTableRow8});
        this.xrTable2.Size = new System.Drawing.Size(625, 675);
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.BorderColor = System.Drawing.Color.Navy;
        this.xrTableRow2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableRow2.BorderWidth = 2;
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28,
            this.xrTableCell46,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.StylePriority.UseBorderColor = false;
        this.xrTableRow2.StylePriority.UseBorders = false;
        this.xrTableRow2.StylePriority.UseBorderWidth = false;
        this.xrTableRow2.Weight = 2.5;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell28.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell28.BorderWidth = 2;
        this.xrTableCell28.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseBorderColor = false;
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.StylePriority.UseBorderWidth = false;
        this.xrTableCell28.Text = "xrTableCell28";
        this.xrTableCell28.Weight = 1.0132800000000002;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.Location = new System.Drawing.Point(42, 17);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.Size = new System.Drawing.Size(108, 92);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11});
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.Text = "xrTableCell46";
        this.xrTableCell46.Weight = 0.908274;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel11.Location = new System.Drawing.Point(9, 17);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.Size = new System.Drawing.Size(42, 25);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "تصویر";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelEmpCode});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 1.5503979999999995;
        // 
        // xrlabelEmpCode
        // 
        this.xrlabelEmpCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelEmpCode.Location = new System.Drawing.Point(125, 17);
        this.xrlabelEmpCode.Name = "xrlabelEmpCode";
        this.xrlabelEmpCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelEmpCode.Size = new System.Drawing.Size(109, 25);
        this.xrlabelEmpCode.StylePriority.UseBorders = false;
        this.xrlabelEmpCode.StylePriority.UseTextAlignment = false;
        this.xrlabelEmpCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
        this.xrLabel1.Location = new System.Drawing.Point(17, 17);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.Size = new System.Drawing.Size(56, 25);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "کد کارمند";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell8.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell8.BorderWidth = 2;
        this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelFirstNameEn});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorderColor = false;
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UseBorderWidth = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.Weight = 2.417936;
        // 
        // xrLabelFirstNameEn
        // 
        this.xrLabelFirstNameEn.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabelFirstNameEn.Location = new System.Drawing.Point(167, 17);
        this.xrLabelFirstNameEn.Name = "xrLabelFirstNameEn";
        this.xrLabelFirstNameEn.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelFirstNameEn.Size = new System.Drawing.Size(200, 25);
        this.xrLabelFirstNameEn.StylePriority.UseBorders = false;
        this.xrLabelFirstNameEn.StylePriority.UseTextAlignment = false;
        this.xrLabelFirstNameEn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.Weight = 0.47388799999999992;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Location = new System.Drawing.Point(24, 17);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.Size = new System.Drawing.Size(42, 25);
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "انگلیسی";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelFirstName});
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.Weight = 0.58654399999999973;
        // 
        // xrlabelFirstName
        // 
        this.xrlabelFirstName.Location = new System.Drawing.Point(0, 17);
        this.xrlabelFirstName.Name = "xrlabelFirstName";
        this.xrlabelFirstName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelFirstName.Size = new System.Drawing.Size(84, 25);
        this.xrlabelFirstName.StylePriority.UseTextAlignment = false;
        this.xrlabelFirstName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell11.BorderWidth = 2;
        this.xrTableCell11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorderColor = false;
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseBorderWidth = false;
        this.xrTableCell11.Text = "انگلیسی";
        this.xrTableCell11.Weight = 0.53163199999999988;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Location = new System.Drawing.Point(32, 17);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.Size = new System.Drawing.Size(43, 25);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "نام";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell20.BorderWidth = 2;
        this.xrTableCell20.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelLastNameEn});
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBorderColor = false;
        this.xrTableCell20.StylePriority.UseBorders = false;
        this.xrTableCell20.StylePriority.UseBorderWidth = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.Weight = 2.417936;
        // 
        // xrLabelLastNameEn
        // 
        this.xrLabelLastNameEn.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabelLastNameEn.Location = new System.Drawing.Point(167, 17);
        this.xrLabelLastNameEn.Name = "xrLabelLastNameEn";
        this.xrLabelLastNameEn.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelLastNameEn.Size = new System.Drawing.Size(200, 25);
        this.xrLabelLastNameEn.StylePriority.UseBorders = false;
        this.xrLabelLastNameEn.StylePriority.UseTextAlignment = false;
        this.xrLabelLastNameEn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13});
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.Weight = 0.47388799999999992;
        // 
        // xrLabel13
        // 
        this.xrLabel13.Location = new System.Drawing.Point(16, 17);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.Size = new System.Drawing.Size(50, 25);
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "انگلیسی";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelLastName});
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.Weight = 0.58654399999999973;
        // 
        // xrlabelLastName
        // 
        this.xrlabelLastName.Location = new System.Drawing.Point(0, 17);
        this.xrlabelLastName.Name = "xrlabelLastName";
        this.xrlabelLastName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelLastName.Size = new System.Drawing.Size(84, 25);
        this.xrlabelLastName.StylePriority.UseTextAlignment = false;
        this.xrlabelLastName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
        this.xrTableCell23.Weight = 0.53163199999999988;
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
        this.xrLabel12.Text = "نام خانوادگی";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell43,
            this.xrTableCell49,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1.0000000000000002;
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell43.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell43.BorderWidth = 2;
        this.xrTableCell43.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelBirthPlace});
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.StylePriority.UseBorderColor = false;
        this.xrTableCell43.StylePriority.UseBorders = false;
        this.xrTableCell43.StylePriority.UseBorderWidth = false;
        this.xrTableCell43.Text = "xrTableCell43";
        this.xrTableCell43.Weight = 1.0209640000000002;
        // 
        // xrlabelBirthPlace
        // 
        this.xrlabelBirthPlace.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelBirthPlace.Location = new System.Drawing.Point(17, 17);
        this.xrlabelBirthPlace.Name = "xrlabelBirthPlace";
        this.xrlabelBirthPlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelBirthPlace.Size = new System.Drawing.Size(133, 25);
        this.xrlabelBirthPlace.StylePriority.UseBorders = false;
        this.xrlabelBirthPlace.StylePriority.UseTextAlignment = false;
        this.xrlabelBirthPlace.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel24});
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.Text = "xrTableCell49";
        this.xrTableCell49.Weight = 0.42580600000000013;
        // 
        // xrLabel24
        // 
        this.xrLabel24.Location = new System.Drawing.Point(8, 17);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel24.Size = new System.Drawing.Size(50, 25);
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "محل تولد";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelBirthDate});
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.Weight = 0.971166;
        // 
        // xrlabelBirthDate
        // 
        this.xrlabelBirthDate.Location = new System.Drawing.Point(8, 17);
        this.xrlabelBirthDate.Name = "xrlabelBirthDate";
        this.xrlabelBirthDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelBirthDate.Size = new System.Drawing.Size(133, 25);
        this.xrlabelBirthDate.StylePriority.UseTextAlignment = false;
        this.xrlabelBirthDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14});
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "xrTableCell17";
        this.xrTableCell17.Weight = 0.47388799999999992;
        // 
        // xrLabel14
        // 
        this.xrLabel14.Location = new System.Drawing.Point(16, 17);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.Size = new System.Drawing.Size(49, 25);
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "تاریخ تولد";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelFatherName});
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.Weight = 0.59295999999999982;
        // 
        // xrLabelFatherName
        // 
        this.xrLabelFatherName.Location = new System.Drawing.Point(0, 17);
        this.xrLabelFatherName.Name = "xrLabelFatherName";
        this.xrLabelFatherName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabelFatherName.Size = new System.Drawing.Size(83, 25);
        this.xrLabelFatherName.StylePriority.UseTextAlignment = false;
        this.xrLabelFatherName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell19.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell19.BorderWidth = 2;
        this.xrTableCell19.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorderColor = false;
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UseBorderWidth = false;
        this.xrTableCell19.Text = "xrTableCell19";
        this.xrTableCell19.Weight = 0.52521599999999991;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Location = new System.Drawing.Point(24, 17);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.Size = new System.Drawing.Size(50, 25);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "نام پدر";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38,
            this.xrTableCell44,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell38.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell38.BorderWidth = 2;
        this.xrTableCell38.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelSexId});
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseBorderColor = false;
        this.xrTableCell38.StylePriority.UseBorders = false;
        this.xrTableCell38.StylePriority.UseBorderWidth = false;
        this.xrTableCell38.Text = "xrTableCell38";
        this.xrTableCell38.Weight = 1.0162640000000003;
        // 
        // xrlabelSexId
        // 
        this.xrlabelSexId.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelSexId.Location = new System.Drawing.Point(17, 17);
        this.xrlabelSexId.Name = "xrlabelSexId";
        this.xrlabelSexId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelSexId.Size = new System.Drawing.Size(133, 25);
        this.xrlabelSexId.StylePriority.UseBorders = false;
        this.xrlabelSexId.StylePriority.UseTextAlignment = false;
        this.xrlabelSexId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel26});
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.Text = "xrTableCell44";
        this.xrTableCell44.Weight = 0.42815600000000004;
        // 
        // xrLabel26
        // 
        this.xrLabel26.Location = new System.Drawing.Point(18, 17);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel26.Size = new System.Drawing.Size(40, 25);
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "جنسیت";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelSSN});
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.Weight = 0.973516;
        // 
        // xrlabelSSN
        // 
        this.xrlabelSSN.Location = new System.Drawing.Point(8, 17);
        this.xrlabelSSN.Name = "xrlabelSSN";
        this.xrlabelSSN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelSSN.Size = new System.Drawing.Size(133, 25);
        this.xrlabelSSN.StylePriority.UseTextAlignment = false;
        this.xrlabelSSN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.Weight = 0.46747199999999994;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Location = new System.Drawing.Point(7, 17);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.Size = new System.Drawing.Size(57, 25);
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "کد ملی";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelIdNo});
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.Weight = 0.59295999999999971;
        // 
        // xrlabelIdNo
        // 
        this.xrlabelIdNo.Location = new System.Drawing.Point(0, 17);
        this.xrlabelIdNo.Name = "xrlabelIdNo";
        this.xrlabelIdNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelIdNo.Size = new System.Drawing.Size(83, 25);
        this.xrlabelIdNo.StylePriority.UseTextAlignment = false;
        this.xrlabelIdNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell15.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell15.BorderWidth = 2;
        this.xrTableCell15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorderColor = false;
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.StylePriority.UseBorderWidth = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 0.53163199999999988;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel15.Location = new System.Drawing.Point(0, 17);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.Size = new System.Drawing.Size(74, 25);
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "شماره شناسنامه";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell58,
            this.xrTableCell59});
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell58.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell58.BorderWidth = 2;
        this.xrTableCell58.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelMarId});
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.StylePriority.UseBorderColor = false;
        this.xrTableCell58.StylePriority.UseBorders = false;
        this.xrTableCell58.StylePriority.UseBorderWidth = false;
        this.xrTableCell58.Text = "xrTableCell58";
        this.xrTableCell58.Weight = 3.4783680000000006;
        // 
        // xrlabelMarId
        // 
        this.xrlabelMarId.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelMarId.Location = new System.Drawing.Point(450, 17);
        this.xrlabelMarId.Name = "xrlabelMarId";
        this.xrlabelMarId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrlabelMarId.Size = new System.Drawing.Size(82, 25);
        this.xrlabelMarId.StylePriority.UseBorders = false;
        this.xrlabelMarId.StylePriority.UseTextAlignment = false;
        this.xrlabelMarId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell59
        // 
        this.xrTableCell59.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell59.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell59.BorderWidth = 2;
        this.xrTableCell59.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18});
        this.xrTableCell59.Name = "xrTableCell59";
        this.xrTableCell59.StylePriority.UseBorderColor = false;
        this.xrTableCell59.StylePriority.UseBorders = false;
        this.xrTableCell59.StylePriority.UseBorderWidth = false;
        this.xrTableCell59.Text = "xrTableCell59";
        this.xrTableCell59.Weight = 0.53163199999999988;
        // 
        // xrLabel18
        // 
        this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel18.Location = new System.Drawing.Point(8, 17);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel18.Size = new System.Drawing.Size(69, 25);
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = "وضعیت تاهل";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell64,
            this.xrTableCell65});
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell64.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell64.BorderWidth = 2;
        this.xrTableCell64.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabeladdress});
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseBorderColor = false;
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.StylePriority.UseBorderWidth = false;
        this.xrTableCell64.Text = "xrTableCell64";
        this.xrTableCell64.Weight = 3.4783680000000006;
        // 
        // xrlabeladdress
        // 
        this.xrlabeladdress.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabeladdress.Location = new System.Drawing.Point(8, 17);
        this.xrlabeladdress.Name = "xrlabeladdress";
        this.xrlabeladdress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabeladdress.Size = new System.Drawing.Size(525, 25);
        this.xrlabeladdress.StylePriority.UseBorders = false;
        this.xrlabeladdress.StylePriority.UseTextAlignment = false;
        this.xrlabeladdress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell65.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell65.BorderWidth = 2;
        this.xrTableCell65.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel28});
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.StylePriority.UseBorderColor = false;
        this.xrTableCell65.StylePriority.UseBorders = false;
        this.xrTableCell65.StylePriority.UseBorderWidth = false;
        this.xrTableCell65.Text = "xrTableCell65";
        this.xrTableCell65.Weight = 0.53163199999999988;
        // 
        // xrLabel28
        // 
        this.xrLabel28.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel28.Location = new System.Drawing.Point(25, 17);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel28.Size = new System.Drawing.Size(49, 25);
        this.xrLabel28.StylePriority.UseBorders = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "آدرس";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell32});
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell29.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell29.BorderWidth = 2;
        this.xrTableCell29.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelMobileNo});
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseBorderColor = false;
        this.xrTableCell29.StylePriority.UseBorders = false;
        this.xrTableCell29.StylePriority.UseBorderWidth = false;
        this.xrTableCell29.Text = "xrTableCell29";
        this.xrTableCell29.Weight = 1.0128320000000002;
        // 
        // xrlabelMobileNo
        // 
        this.xrlabelMobileNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelMobileNo.Location = new System.Drawing.Point(9, 17);
        this.xrlabelMobileNo.Name = "xrlabelMobileNo";
        this.xrlabelMobileNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelMobileNo.Size = new System.Drawing.Size(141, 25);
        this.xrlabelMobileNo.StylePriority.UseBorders = false;
        this.xrlabelMobileNo.StylePriority.UseTextAlignment = false;
        this.xrlabelMobileNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel32});
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.Text = "xrTableCell30";
        this.xrTableCell30.Weight = 0.42897599999999991;
        // 
        // xrLabel32
        // 
        this.xrLabel32.Location = new System.Drawing.Point(9, 17);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel32.Size = new System.Drawing.Size(50, 25);
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        this.xrLabel32.Text = "تلفن همراه";
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelTel});
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Text = "xrTableCell31";
        this.xrTableCell31.Weight = 2.03656;
        // 
        // xrlabelTel
        // 
        this.xrlabelTel.Location = new System.Drawing.Point(125, 17);
        this.xrlabelTel.Name = "xrlabelTel";
        this.xrlabelTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelTel.Size = new System.Drawing.Size(183, 25);
        this.xrlabelTel.StylePriority.UseTextAlignment = false;
        this.xrlabelTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell32.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell32.BorderWidth = 2;
        this.xrTableCell32.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel30});
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseBorderColor = false;
        this.xrTableCell32.StylePriority.UseBorders = false;
        this.xrTableCell32.StylePriority.UseBorderWidth = false;
        this.xrTableCell32.Text = "xrTableCell32";
        this.xrTableCell32.Weight = 0.53163199999999988;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel30.Location = new System.Drawing.Point(16, 17);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel30.Size = new System.Drawing.Size(58, 25);
        this.xrLabel30.StylePriority.UseBorders = false;
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.Text = "شماره تلفن";
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell24.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell24.BorderWidth = 2;
        this.xrTableCell24.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelEmail});
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBorderColor = false;
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.StylePriority.UseBorderWidth = false;
        this.xrTableCell24.Text = "xrTableCell24";
        this.xrTableCell24.Weight = 1.0128320000000002;
        // 
        // xrlabelEmail
        // 
        this.xrlabelEmail.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelEmail.Location = new System.Drawing.Point(9, 17);
        this.xrlabelEmail.Name = "xrlabelEmail";
        this.xrlabelEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelEmail.Size = new System.Drawing.Size(142, 25);
        this.xrlabelEmail.StylePriority.UseBorders = false;
        this.xrlabelEmail.StylePriority.UseTextAlignment = false;
        this.xrlabelEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20});
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Text = "xrTableCell25";
        this.xrTableCell25.Weight = 0.42897600000000014;
        // 
        // xrLabel20
        // 
        this.xrLabel20.Location = new System.Drawing.Point(26, 17);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel20.Size = new System.Drawing.Size(32, 25);
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = "ایمیل";
        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelWebSite});
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Text = "xrTableCell26";
        this.xrTableCell26.Weight = 2.0365599999999997;
        // 
        // xrlabelWebSite
        // 
        this.xrlabelWebSite.Location = new System.Drawing.Point(125, 17);
        this.xrlabelWebSite.Name = "xrlabelWebSite";
        this.xrlabelWebSite.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelWebSite.Size = new System.Drawing.Size(182, 25);
        this.xrlabelWebSite.StylePriority.UseTextAlignment = false;
        this.xrlabelWebSite.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
        this.xrLabel35.Location = new System.Drawing.Point(24, 17);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel35.Size = new System.Drawing.Size(50, 25);
        this.xrLabel35.StylePriority.UseBorders = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "وب سایت";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell77,
            this.xrTableCell78,
            this.xrTableCell80,
            this.xrTableCell81,
            this.xrTableCell82,
            this.xrTableCell83});
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 1;
        // 
        // xrTableCell77
        // 
        this.xrTableCell77.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell77.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell77.BorderWidth = 2;
        this.xrTableCell77.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelPartId});
        this.xrTableCell77.Name = "xrTableCell77";
        this.xrTableCell77.StylePriority.UseBorderColor = false;
        this.xrTableCell77.StylePriority.UseBorders = false;
        this.xrTableCell77.StylePriority.UseBorderWidth = false;
        this.xrTableCell77.Text = "xrTableCell77";
        this.xrTableCell77.Weight = 1.0162640000000003;
        // 
        // xrlabelPartId
        // 
        this.xrlabelPartId.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelPartId.Location = new System.Drawing.Point(9, 17);
        this.xrlabelPartId.Name = "xrlabelPartId";
        this.xrlabelPartId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrlabelPartId.Size = new System.Drawing.Size(142, 25);
        this.xrlabelPartId.StylePriority.UseBorders = false;
        this.xrlabelPartId.StylePriority.UseTextAlignment = false;
        this.xrlabelPartId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell78
        // 
        this.xrTableCell78.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel40});
        this.xrTableCell78.Name = "xrTableCell78";
        this.xrTableCell78.Text = "xrTableCell78";
        this.xrTableCell78.Weight = 0.4345719999999999;
        // 
        // xrLabel40
        // 
        this.xrLabel40.Location = new System.Drawing.Point(26, 17);
        this.xrLabel40.Name = "xrLabel40";
        this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel40.Size = new System.Drawing.Size(34, 25);
        this.xrLabel40.StylePriority.UseTextAlignment = false;
        this.xrLabel40.Text = "بخش";
        this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelRelId});
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Text = "xrTableCell80";
        this.xrTableCell80.Weight = 0.793868;
        // 
        // xrlabelRelId
        // 
        this.xrlabelRelId.Location = new System.Drawing.Point(16, 17);
        this.xrlabelRelId.Name = "xrlabelRelId";
        this.xrlabelRelId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrlabelRelId.Size = new System.Drawing.Size(100, 25);
        this.xrlabelRelId.StylePriority.UseTextAlignment = false;
        this.xrlabelRelId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38});
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Text = "xrTableCell81";
        this.xrTableCell81.Weight = 0.53163199999999988;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Location = new System.Drawing.Point(33, 17);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel38.Size = new System.Drawing.Size(42, 25);
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "مذهب";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell82
        // 
        this.xrTableCell82.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelNationality});
        this.xrTableCell82.Name = "xrTableCell82";
        this.xrTableCell82.Text = "xrTableCell82";
        this.xrTableCell82.Weight = 0.70203199999999977;
        // 
        // xrlabelNationality
        // 
        this.xrlabelNationality.Location = new System.Drawing.Point(0, 17);
        this.xrlabelNationality.Name = "xrlabelNationality";
        this.xrlabelNationality.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrlabelNationality.Size = new System.Drawing.Size(100, 25);
        this.xrlabelNationality.StylePriority.UseTextAlignment = false;
        this.xrlabelNationality.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell83
        // 
        this.xrTableCell83.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell83.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell83.BorderWidth = 2;
        this.xrTableCell83.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36});
        this.xrTableCell83.Name = "xrTableCell83";
        this.xrTableCell83.StylePriority.UseBorderColor = false;
        this.xrTableCell83.StylePriority.UseBorders = false;
        this.xrTableCell83.StylePriority.UseBorderWidth = false;
        this.xrTableCell83.Text = "xrTableCell83";
        this.xrTableCell83.Weight = 0.53163199999999988;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel36.Location = new System.Drawing.Point(41, 17);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel36.Size = new System.Drawing.Size(33, 25);
        this.xrLabel36.StylePriority.UseBorders = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = "ملیت";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell72,
            this.xrTableCell73,
            this.xrTableCell74,
            this.xrTableCell75});
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 1;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell72.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell72.BorderWidth = 2;
        this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelEmpStatus});
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseBorderColor = false;
        this.xrTableCell72.StylePriority.UseBorders = false;
        this.xrTableCell72.StylePriority.UseBorderWidth = false;
        this.xrTableCell72.Text = "xrTableCell72";
        this.xrTableCell72.Weight = 2.2382880000000007;
        // 
        // xrlabelEmpStatus
        // 
        this.xrlabelEmpStatus.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelEmpStatus.Location = new System.Drawing.Point(242, 17);
        this.xrlabelEmpStatus.Name = "xrlabelEmpStatus";
        this.xrlabelEmpStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrlabelEmpStatus.Size = new System.Drawing.Size(100, 25);
        this.xrlabelEmpStatus.StylePriority.UseBorders = false;
        this.xrlabelEmpStatus.StylePriority.UseTextAlignment = false;
        this.xrlabelEmpStatus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel45});
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.Text = "xrTableCell73";
        this.xrTableCell73.Weight = 0.53163199999999988;
        // 
        // xrLabel45
        // 
        this.xrLabel45.Location = new System.Drawing.Point(1, 17);
        this.xrLabel45.Name = "xrLabel45";
        this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel45.Size = new System.Drawing.Size(75, 25);
        this.xrLabel45.StylePriority.UseTextAlignment = false;
        this.xrLabel45.Text = "وضعیت کارمند";
        this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelCreateDate});
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.Text = "xrTableCell74";
        this.xrTableCell74.Weight = 0.70844799999999974;
        // 
        // xrlabelCreateDate
        // 
        this.xrlabelCreateDate.Location = new System.Drawing.Point(0, 17);
        this.xrlabelCreateDate.Name = "xrlabelCreateDate";
        this.xrlabelCreateDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrlabelCreateDate.Size = new System.Drawing.Size(100, 25);
        this.xrlabelCreateDate.StylePriority.UseTextAlignment = false;
        this.xrlabelCreateDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell75.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell75.BorderWidth = 2;
        this.xrTableCell75.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel42});
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseBorderColor = false;
        this.xrTableCell75.StylePriority.UseBorders = false;
        this.xrTableCell75.StylePriority.UseBorderWidth = false;
        this.xrTableCell75.Text = "xrTableCell75";
        this.xrTableCell75.Weight = 0.53163199999999988;
        // 
        // xrLabel42
        // 
        this.xrLabel42.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel42.Location = new System.Drawing.Point(32, 17);
        this.xrLabel42.Name = "xrLabel42";
        this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel42.Size = new System.Drawing.Size(42, 25);
        this.xrLabel42.StylePriority.UseBorders = false;
        this.xrLabel42.StylePriority.UseTextAlignment = false;
        this.xrLabel42.Text = "تاریخ";
        this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell35});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell34.BorderWidth = 2;
        this.xrTableCell34.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlabelDescription});
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBorderColor = false;
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.StylePriority.UseBorderWidth = false;
        this.xrTableCell34.StylePriority.UseTextAlignment = false;
        this.xrTableCell34.Text = "xrTableCell34";
        this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell34.Weight = 3.478368;
        // 
        // xrlabelDescription
        // 
        this.xrlabelDescription.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrlabelDescription.Location = new System.Drawing.Point(8, 17);
        this.xrlabelDescription.Name = "xrlabelDescription";
        this.xrlabelDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrlabelDescription.Size = new System.Drawing.Size(525, 25);
        this.xrlabelDescription.StylePriority.UseBorders = false;
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
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.PageHeader.Height = 133;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
        // EmployeeReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Margins = new System.Drawing.Printing.Margins(100, 99, 100, 100);
        this.Name = "EmployeeReport";
        this.PageHeight = 1100;
        this.PageWidth = 850;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
