using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for OfficeAaza_Kardan
/// </summary>
public class OfficeAaza_Kardan : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private TSP.DataManager.OfficeMemberManager OfKaManager = new TSP.DataManager.OfficeMemberManager();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel12;
    private XRTable xrTable4;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell73;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell4;
    private XRLabel xrLFamily;
    private XRTableCell xrTableCell1;
    private XRLabel xrLabel3;
    private XRTableCell xrTableCell2;
    private XRLabel xrLName;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel2;
    private XRTableCell xrTableCell28;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell9;
    private XRLabel xrLSSN;
    private XRTableCell xrTableCell10;
    private XRLabel xrLabel14;
    private XRTableCell xrTableCell11;
    private XRLabel xrLIdNo;
    private XRTableCell xrTableCell12;
    private XRLabel xrLabel4;
    private XRTableCell xrTableCell29;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell13;
    private XRLabel xrLBirthDate;
    private XRTableCell xrTableCell14;
    private XRLabel xrLabel15;
    private XRTableCell xrTableCell15;
    private XRLabel xrLFatherName;
    private XRTableCell xrTableCell16;
    private XRLabel xrLabel5;
    private XRTableCell xrTableCell30;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell21;
    private XRLabel xrLIssuePlace;
    private XRTableCell xrTableCell22;
    private XRLabel xrLabel16;
    private XRTableCell xrTableCell23;
    private XRLabel xrLBirthPlace;
    private XRTableCell xrTableCell24;
    private XRLabel xrLabel6;
    private XRTableCell xrTableCell31;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRLabel xrLOfPosition;
    private XRTableCell xrTableCell6;
    private XRLabel xrLabel11;
    private XRTableCell xrTableCell7;
    private XRLabel xrLFileNo;
    private XRTableCell xrTableCell8;
    private XRLabel xrLabel7;
    private XRTableCell xrTableCell32;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell17;
    private XRLabel xrLEndDate;
    private XRTableCell xrTableCell18;
    private XRLabel xrLabel9;
    private XRTableCell xrTableCell25;
    private XRLabel xrLStartDate;
    private XRTableCell xrTableCell26;
    private XRLabel xrLabel8;
    private XRTableCell xrTableCell33;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell19;
    private XRLabel xrLIsFullTime;
    private XRTableCell xrTableCell20;
    private XRLabel xrLabel10;
    private XRTableCell xrTableCell34;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell65;
    private XRPictureBox xrPictureBox1;
    private XRTableCell xrTableCell66;
    private XRLabel xrLabel30;
    private XRTableCell xrTableCell35;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell27;
    private XRLabel xrLHasSignRight;
    private XRTableCell xrTableCell37;
    private XRLabel xrLabel13;
    private XRTableCell xrTableCell38;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell39;
    private XRPictureBox xrPictureBox2;
    private XRTableCell xrTableCell40;
    private XRLabel xrLabel17;
    private XRTableCell xrTableCell41;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell69;
    private XRLabel xrLDesc;
    private XRTableCell xrTableCell70;
    private XRLabel xrLabel31;
    private XRTableCell xrTableCell36;
    private XRLabel xrLabel1;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell51;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRLabel xrLabel18;
    private XRLabel xrLabel19;
    private XRLabel xrLabel20;
    private XRLabel xrLAddress;
    private XRLabel xrLTel;
    private XRLabel xrLMobileNo;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OfficeAaza_Kardan(int OfId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        OfKaManager.SearchMember(OfId, 2, -1);
        xrLBirthDate.DataBindings.Add("Text", OfKaManager.DataTable, "BrithDate");
        xrLBirthPlace.DataBindings.Add("Text", OfKaManager.DataTable, "BirthPlace");
        xrLDesc.DataBindings.Add("Text", OfKaManager.DataTable, "Description");
        xrLFamily.DataBindings.Add("Text", OfKaManager.DataTable, "LastName");
     
        xrLFatherName.DataBindings.Add("Text", OfKaManager.DataTable, "FatherName");
        xrLFileNo.DataBindings.Add("Text", OfKaManager.DataTable, "FileNo");
        xrLIdNo.DataBindings.Add("Text", OfKaManager.DataTable, "IdNo");
        xrLIssuePlace.DataBindings.Add("Text", OfKaManager.DataTable, "IssuePlace");
        xrLName.DataBindings.Add("Text", OfKaManager.DataTable, "FirstName");
        xrLSSN.DataBindings.Add("Text", OfKaManager.DataTable, "SSN");
        xrLEndDate.DataBindings.Add("Text", OfKaManager.DataTable, "EndDate");
        xrLStartDate.DataBindings.Add("Text", OfKaManager.DataTable, "StartDate");
        xrLHasSignRight.DataBindings.Add("Text", OfKaManager.DataTable, "SignRight");
        if(xrLHasSignRight.Text=="حق امضا دارد.")
            xrTableRow8.Visible = true; 
        else
            xrTableRow8.Visible = false;
            xrLIsFullTime.DataBindings.Add("Text", OfKaManager.DataTable, "FullTime");
        xrLOfPosition.DataBindings.Add("Text", OfKaManager.DataTable, "OfpName");
        xrLAddress.DataBindings.Add("Text", OfKaManager.DataTable, "Address");
        xrLMobileNo.DataBindings.Add("Text", OfKaManager.DataTable, "MobileNo");
        xrLTel.DataBindings.Add("Text", OfKaManager.DataTable, "Tel");
        xrPictureBox1.DataBindings.Add("ImageUrl", OfKaManager.DataTable, "ImageUrl");
        xrPictureBox2.DataBindings.Add("ImageUrl", OfKaManager.DataTable, "SignUrl");
       
        this.DataSource = OfKaManager.DataTable;
       

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
        string resourceFileName = "OfficeAaza_Kardan.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLFamily = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLSSN = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLIdNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLBirthDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLFatherName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLIssuePlace = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLBirthPlace = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLMobileNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLTel = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLAddress = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLOfPosition = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLFileNo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLEndDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLStartDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLIsFullTime = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLHasSignRight = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLDesc = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 840;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTable4.BorderWidth = 2;
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Location = new System.Drawing.Point(0, 0);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow24});
        this.xrTable4.Size = new System.Drawing.Size(1849, 840);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow24
        // 
        this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell73});
        this.xrTableRow24.Dpi = 254F;
        this.xrTableRow24.Name = "xrTableRow24";
        this.xrTableRow24.Weight = 1;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.xrTableCell73.Dpi = 254F;
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.Text = "xrTableCell73";
        this.xrTableCell73.Weight = 3;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable1.BorderWidth = 1;
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Location = new System.Drawing.Point(10, 10);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow6,
            this.xrTableRow12,
            this.xrTableRow10,
            this.xrTableRow2,
            this.xrTableRow11,
            this.xrTableRow5,
            this.xrTableRow19,
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow21});
        this.xrTable1.Size = new System.Drawing.Size(1829, 823);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        this.xrTable1.StylePriority.UsePadding = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell28});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.StylePriority.UseTextAlignment = false;
        this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableRow1.Weight = 0.49999999999999989;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLFamily});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell4.Weight = 2.08;
        // 
        // xrLFamily
        // 
        this.xrLFamily.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLFamily.Dpi = 254F;
        this.xrLFamily.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLFamily.Location = new System.Drawing.Point(64, 5);
        this.xrLFamily.Name = "xrLFamily";
        this.xrLFamily.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLFamily.Size = new System.Drawing.Size(475, 51);
        this.xrLFamily.StylePriority.UseBorders = false;
        this.xrLFamily.StylePriority.UseFont = false;
        this.xrLFamily.Text = "  ";
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "xrTableCell1";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell1.Weight = 1.42;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel3.Location = new System.Drawing.Point(85, 0);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.Size = new System.Drawing.Size(254, 51);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "نام خانوادگی";
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLName});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell2.Weight = 1.8902788408966649;
        // 
        // xrLName
        // 
        this.xrLName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLName.Dpi = 254F;
        this.xrLName.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLName.Location = new System.Drawing.Point(21, 5);
        this.xrLName.Name = "xrLName";
        this.xrLName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLName.Size = new System.Drawing.Size(463, 50);
        this.xrLName.StylePriority.UseBorders = false;
        this.xrLName.StylePriority.UseFont = false;
        this.xrLName.Text = "  ";
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBackColor = false;
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 1.0921323127392018;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel2.Location = new System.Drawing.Point(21, 0);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(254, 51);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "نام";
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell28.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.xrTableCell28.Dpi = 254F;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.Weight = 0.43758884636413342;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Location = new System.Drawing.Point(1, 1);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.Size = new System.Drawing.Size(53, 64);
        this.xrLabel1.StylePriority.UseBorders = false;
        xrSummary1.FormatString = "{0:.}";
        xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel1.Summary = xrSummary1;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell29});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.5;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLSSN});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.Weight = 2.08;
        // 
        // xrLSSN
        // 
        this.xrLSSN.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLSSN.Dpi = 254F;
        this.xrLSSN.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLSSN.Location = new System.Drawing.Point(64, 5);
        this.xrLSSN.Name = "xrLSSN";
        this.xrLSSN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLSSN.Size = new System.Drawing.Size(475, 51);
        this.xrLSSN.StylePriority.UseBorders = false;
        this.xrLSSN.StylePriority.UseFont = false;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14});
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBackColor = false;
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.Weight = 1.42;
        // 
        // xrLabel14
        // 
        this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel14.Location = new System.Drawing.Point(85, 0);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.Size = new System.Drawing.Size(254, 51);
        this.xrLabel14.StylePriority.UseBorders = false;
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.Text = "کد ملی";
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLIdNo});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 1.8902788408966649;
        // 
        // xrLIdNo
        // 
        this.xrLIdNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLIdNo.Dpi = 254F;
        this.xrLIdNo.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLIdNo.Location = new System.Drawing.Point(21, 5);
        this.xrLIdNo.Name = "xrLIdNo";
        this.xrLIdNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLIdNo.Size = new System.Drawing.Size(463, 50);
        this.xrLIdNo.StylePriority.UseBorders = false;
        this.xrLIdNo.StylePriority.UseFont = false;
        this.xrLIdNo.Text = "  ";
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBackColor = false;
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.Weight = 1.0921323127392018;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel4.Location = new System.Drawing.Point(21, 0);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.Size = new System.Drawing.Size(254, 51);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "شماره شناسنامه";
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell29.Dpi = 254F;
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseBorders = false;
        this.xrTableCell29.Weight = 0.43758884636413342;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell30});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 0.49999999999999989;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLBirthDate});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.Weight = 2.08;
        // 
        // xrLBirthDate
        // 
        this.xrLBirthDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLBirthDate.Dpi = 254F;
        this.xrLBirthDate.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLBirthDate.Location = new System.Drawing.Point(64, 5);
        this.xrLBirthDate.Name = "xrLBirthDate";
        this.xrLBirthDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLBirthDate.Size = new System.Drawing.Size(475, 51);
        this.xrLBirthDate.StylePriority.UseBorders = false;
        this.xrLBirthDate.StylePriority.UseFont = false;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell14.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBackColor = false;
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.Weight = 1.42;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel15.Location = new System.Drawing.Point(85, 0);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.Size = new System.Drawing.Size(254, 51);
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "تاریخ تولد";
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLFatherName});
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 1.8902788408966649;
        // 
        // xrLFatherName
        // 
        this.xrLFatherName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLFatherName.Dpi = 254F;
        this.xrLFatherName.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLFatherName.Location = new System.Drawing.Point(21, 5);
        this.xrLFatherName.Name = "xrLFatherName";
        this.xrLFatherName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLFatherName.Size = new System.Drawing.Size(463, 50);
        this.xrLFatherName.StylePriority.UseBorders = false;
        this.xrLFatherName.StylePriority.UseFont = false;
        this.xrLFatherName.Text = "  ";
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell16.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBackColor = false;
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.Weight = 1.0921323127392018;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel5.Location = new System.Drawing.Point(21, 0);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.Size = new System.Drawing.Size(254, 51);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "نام پدر";
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.Weight = 0.43758884636413342;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell31});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 0.5;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLIssuePlace});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseBorders = false;
        this.xrTableCell21.Weight = 2.08;
        // 
        // xrLIssuePlace
        // 
        this.xrLIssuePlace.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLIssuePlace.Dpi = 254F;
        this.xrLIssuePlace.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLIssuePlace.Location = new System.Drawing.Point(64, 5);
        this.xrLIssuePlace.Name = "xrLIssuePlace";
        this.xrLIssuePlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLIssuePlace.Size = new System.Drawing.Size(475, 51);
        this.xrLIssuePlace.StylePriority.UseBorders = false;
        this.xrLIssuePlace.StylePriority.UseFont = false;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell22.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell22.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel16});
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseBackColor = false;
        this.xrTableCell22.StylePriority.UseBorders = false;
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.Weight = 1.42;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel16.Location = new System.Drawing.Point(42, 0);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.Size = new System.Drawing.Size(295, 51);
        this.xrLabel16.StylePriority.UseBorders = false;
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.Text = "محل صدور شناسنامه";
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell23.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLBirthPlace});
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseBorders = false;
        this.xrTableCell23.Text = "xrTableCell23";
        this.xrTableCell23.Weight = 1.8902788408966649;
        // 
        // xrLBirthPlace
        // 
        this.xrLBirthPlace.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLBirthPlace.Dpi = 254F;
        this.xrLBirthPlace.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLBirthPlace.Location = new System.Drawing.Point(21, 5);
        this.xrLBirthPlace.Name = "xrLBirthPlace";
        this.xrLBirthPlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLBirthPlace.Size = new System.Drawing.Size(463, 50);
        this.xrLBirthPlace.StylePriority.UseBorders = false;
        this.xrLBirthPlace.StylePriority.UseFont = false;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell24.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell24.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6});
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBackColor = false;
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.Text = "xrTableCell24";
        this.xrTableCell24.Weight = 1.0921323127392018;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel6.Location = new System.Drawing.Point(21, 0);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.Size = new System.Drawing.Size(254, 51);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "محل تولد";
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseBorders = false;
        this.xrTableCell31.Weight = 0.43758884636413342;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell52,
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 0.5;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLMobileNo});
        this.xrTableCell52.Dpi = 254F;
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.Text = "xrTableCell52";
        this.xrTableCell52.Weight = 2.08;
        // 
        // xrLMobileNo
        // 
        this.xrLMobileNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLMobileNo.Dpi = 254F;
        this.xrLMobileNo.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLMobileNo.Location = new System.Drawing.Point(64, 5);
        this.xrLMobileNo.Name = "xrLMobileNo";
        this.xrLMobileNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLMobileNo.Size = new System.Drawing.Size(475, 51);
        this.xrLMobileNo.StylePriority.UseBorders = false;
        this.xrLMobileNo.StylePriority.UseFont = false;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20});
        this.xrTableCell53.Dpi = 254F;
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.Text = "xrTableCell53";
        this.xrTableCell53.Weight = 1.42;
        // 
        // xrLabel20
        // 
        this.xrLabel20.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel20.Location = new System.Drawing.Point(74, 5);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.Size = new System.Drawing.Size(254, 51);
        this.xrLabel20.StylePriority.UseBorders = false;
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.Text = "شماره همراه";
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLTel});
        this.xrTableCell54.Dpi = 254F;
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.Text = "xrTableCell54";
        this.xrTableCell54.Weight = 1.8902788408966649;
        // 
        // xrLTel
        // 
        this.xrLTel.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLTel.Dpi = 254F;
        this.xrLTel.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLTel.Location = new System.Drawing.Point(21, 5);
        this.xrLTel.Name = "xrLTel";
        this.xrLTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLTel.Size = new System.Drawing.Size(463, 50);
        this.xrLTel.StylePriority.UseBorders = false;
        this.xrLTel.StylePriority.UseFont = false;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18});
        this.xrTableCell55.Dpi = 254F;
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.Weight = 1.0921323127392018;
        // 
        // xrLabel18
        // 
        this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel18.Location = new System.Drawing.Point(26, 5);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.Size = new System.Drawing.Size(254, 51);
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.Text = "شماره تلفن";
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.Dpi = 254F;
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.Weight = 0.43758884636413342;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell51});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 0.5;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLAddress});
        this.xrTableCell49.Dpi = 254F;
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.Text = "xrTableCell49";
        this.xrTableCell49.Weight = 5.3902788408966646;
        // 
        // xrLAddress
        // 
        this.xrLAddress.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLAddress.Dpi = 254F;
        this.xrLAddress.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLAddress.Location = new System.Drawing.Point(42, 5);
        this.xrLAddress.Name = "xrLAddress";
        this.xrLAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLAddress.Size = new System.Drawing.Size(1364, 51);
        this.xrLAddress.StylePriority.UseBorders = false;
        this.xrLAddress.StylePriority.UseFont = false;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel19});
        this.xrTableCell50.Dpi = 254F;
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.Text = "xrTableCell50";
        this.xrTableCell50.Weight = 1.0921323127392018;
        // 
        // xrLabel19
        // 
        this.xrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel19.Location = new System.Drawing.Point(26, 5);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.Size = new System.Drawing.Size(254, 51);
        this.xrLabel19.StylePriority.UseBorders = false;
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.Text = "آدرس";
        // 
        // xrTableCell51
        // 
        this.xrTableCell51.Dpi = 254F;
        this.xrTableCell51.Name = "xrTableCell51";
        this.xrTableCell51.Weight = 0.43758884636413342;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell32});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.5;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLOfPosition});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 2.08;
        // 
        // xrLOfPosition
        // 
        this.xrLOfPosition.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLOfPosition.Dpi = 254F;
        this.xrLOfPosition.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLOfPosition.Location = new System.Drawing.Point(64, 5);
        this.xrLOfPosition.Name = "xrLOfPosition";
        this.xrLOfPosition.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLOfPosition.Size = new System.Drawing.Size(475, 50);
        this.xrLOfPosition.StylePriority.UseBorders = false;
        this.xrLOfPosition.StylePriority.UseFont = false;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.Weight = 1.42;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel11.Location = new System.Drawing.Point(85, 0);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.Size = new System.Drawing.Size(254, 51);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "سمت";
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLFileNo});
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "xrTableCell7";
        this.xrTableCell7.Weight = 1.8902788408966649;
        // 
        // xrLFileNo
        // 
        this.xrLFileNo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLFileNo.Dpi = 254F;
        this.xrLFileNo.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLFileNo.Location = new System.Drawing.Point(21, 5);
        this.xrLFileNo.Name = "xrLFileNo";
        this.xrLFileNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLFileNo.Size = new System.Drawing.Size(466, 50);
        this.xrLFileNo.StylePriority.UseBorders = false;
        this.xrLFileNo.StylePriority.UseFont = false;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7});
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.Weight = 1.0921323127392018;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel7.Location = new System.Drawing.Point(21, 0);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.Size = new System.Drawing.Size(254, 51);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "شماره پروانه اشتغال";
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseBorders = false;
        this.xrTableCell32.Weight = 0.43758884636413342;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell33});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 0.5;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLEndDate});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.Weight = 2.08;
        // 
        // xrLEndDate
        // 
        this.xrLEndDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLEndDate.Dpi = 254F;
        this.xrLEndDate.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLEndDate.Location = new System.Drawing.Point(64, 5);
        this.xrLEndDate.Name = "xrLEndDate";
        this.xrLEndDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLEndDate.Size = new System.Drawing.Size(475, 50);
        this.xrLEndDate.StylePriority.UseBorders = false;
        this.xrLEndDate.StylePriority.UseFont = false;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.Weight = 1.42;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel9.Location = new System.Drawing.Point(85, 0);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.Size = new System.Drawing.Size(254, 51);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.Text = "تاریخ پایان همکاری";
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLStartDate});
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Text = "xrTableCell25";
        this.xrTableCell25.Weight = 1.8902788408966649;
        // 
        // xrLStartDate
        // 
        this.xrLStartDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLStartDate.Dpi = 254F;
        this.xrLStartDate.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLStartDate.Location = new System.Drawing.Point(21, 5);
        this.xrLStartDate.Name = "xrLStartDate";
        this.xrLStartDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLStartDate.Size = new System.Drawing.Size(466, 50);
        this.xrLStartDate.StylePriority.UseBorders = false;
        this.xrLStartDate.StylePriority.UseFont = false;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell26.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8});
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseBorders = false;
        this.xrTableCell26.Text = "xrTableCell26";
        this.xrTableCell26.Weight = 1.0921323127392018;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel8.Location = new System.Drawing.Point(21, 0);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.Size = new System.Drawing.Size(254, 51);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "تاریخ شروع همکاری";
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseBorders = false;
        this.xrTableCell33.Weight = 0.43758884636413342;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell34});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 0.49999999999999978;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell19.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLIsFullTime});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.Text = "xrTableCell19";
        this.xrTableCell19.Weight = 5.3902788408966646;
        // 
        // xrLIsFullTime
        // 
        this.xrLIsFullTime.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLIsFullTime.Dpi = 254F;
        this.xrLIsFullTime.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLIsFullTime.Location = new System.Drawing.Point(940, 5);
        this.xrLIsFullTime.Name = "xrLIsFullTime";
        this.xrLIsFullTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLIsFullTime.Size = new System.Drawing.Size(466, 50);
        this.xrLIsFullTime.StylePriority.UseBorders = false;
        this.xrLIsFullTime.StylePriority.UseFont = false;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell20.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10});
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBackColor = false;
        this.xrTableCell20.StylePriority.UseBorders = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.Weight = 1.0921323127392018;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel10.Location = new System.Drawing.Point(21, 0);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.Size = new System.Drawing.Size(254, 51);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.Text = "نوع همکاری";
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.Weight = 0.43758884636413342;
        // 
        // xrTableRow19
        // 
        this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell35});
        this.xrTableRow19.Dpi = 254F;
        this.xrTableRow19.Name = "xrTableRow19";
        this.xrTableRow19.Weight = 0.49999999999999983;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell65.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.xrTableCell65.Dpi = 254F;
        this.xrTableCell65.Multiline = true;
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.StylePriority.UseBorders = false;
        this.xrTableCell65.Text = "xrTableCell65";
        this.xrTableCell65.Weight = 5.3902788408966646;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Location = new System.Drawing.Point(1143, 0);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.Size = new System.Drawing.Size(250, 250);
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrTableCell66
        // 
        this.xrTableCell66.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell66.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell66.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel30});
        this.xrTableCell66.Dpi = 254F;
        this.xrTableCell66.Name = "xrTableCell66";
        this.xrTableCell66.StylePriority.UseBackColor = false;
        this.xrTableCell66.StylePriority.UseBorders = false;
        this.xrTableCell66.Text = "xrTableCell66";
        this.xrTableCell66.Weight = 1.0921323127392018;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel30.Location = new System.Drawing.Point(21, 0);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.Size = new System.Drawing.Size(254, 51);
        this.xrLabel30.StylePriority.UseBorders = false;
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.Text = "تصویر";
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell35.Dpi = 254F;
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseBorders = false;
        this.xrTableCell35.Weight = 0.43758884636413342;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell37,
            this.xrTableCell38});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 0.49999999999999983;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLHasSignRight});
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.Weight = 5.3902788408966646;
        // 
        // xrLHasSignRight
        // 
        this.xrLHasSignRight.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLHasSignRight.Dpi = 254F;
        this.xrLHasSignRight.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLHasSignRight.Location = new System.Drawing.Point(940, 5);
        this.xrLHasSignRight.Name = "xrLHasSignRight";
        this.xrLHasSignRight.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLHasSignRight.Size = new System.Drawing.Size(466, 50);
        this.xrLHasSignRight.StylePriority.UseBorders = false;
        this.xrLHasSignRight.StylePriority.UseFont = false;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13});
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Weight = 1.0921323127392018;
        // 
        // xrLabel13
        // 
        this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel13.Location = new System.Drawing.Point(21, 0);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.Size = new System.Drawing.Size(254, 51);
        this.xrLabel13.StylePriority.UseBorders = false;
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.Text = "حق امضاء";
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.Weight = 0.43758884636413342;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 0.49999999999999983;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
        this.xrTableCell39.Dpi = 254F;
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.Weight = 5.3902788408966646;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.Location = new System.Drawing.Point(1143, 0);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.Size = new System.Drawing.Size(250, 250);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17});
        this.xrTableCell40.Dpi = 254F;
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.Weight = 1.0921323127392018;
        // 
        // xrLabel17
        // 
        this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel17.Location = new System.Drawing.Point(21, 0);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.Size = new System.Drawing.Size(254, 51);
        this.xrLabel17.StylePriority.UseBorders = false;
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.Text = "تصویر امضاء";
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Dpi = 254F;
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.Weight = 0.43758884636413342;
        // 
        // xrTableRow21
        // 
        this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell36});
        this.xrTableRow21.Dpi = 254F;
        this.xrTableRow21.Name = "xrTableRow21";
        this.xrTableRow21.Weight = 0.49999999999999967;
        // 
        // xrTableCell69
        // 
        this.xrTableCell69.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell69.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLDesc});
        this.xrTableCell69.Dpi = 254F;
        this.xrTableCell69.Name = "xrTableCell69";
        this.xrTableCell69.StylePriority.UseBorders = false;
        this.xrTableCell69.Text = "xrTableCell69";
        this.xrTableCell69.Weight = 5.3902788408966646;
        // 
        // xrLDesc
        // 
        this.xrLDesc.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLDesc.Dpi = 254F;
        this.xrLDesc.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLDesc.Location = new System.Drawing.Point(42, 5);
        this.xrLDesc.Name = "xrLDesc";
        this.xrLDesc.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLDesc.Size = new System.Drawing.Size(1364, 51);
        this.xrLDesc.StylePriority.UseBorders = false;
        this.xrLDesc.StylePriority.UseFont = false;
        // 
        // xrTableCell70
        // 
        this.xrTableCell70.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell70.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell70.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel31});
        this.xrTableCell70.Dpi = 254F;
        this.xrTableCell70.Name = "xrTableCell70";
        this.xrTableCell70.StylePriority.UseBackColor = false;
        this.xrTableCell70.StylePriority.UseBorders = false;
        this.xrTableCell70.Text = "xrTableCell70";
        this.xrTableCell70.Weight = 1.0921323127392018;
        // 
        // xrLabel31
        // 
        this.xrLabel31.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel31.Location = new System.Drawing.Point(21, 0);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.Size = new System.Drawing.Size(254, 51);
        this.xrLabel31.StylePriority.UseBorders = false;
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.Text = "توضیحات";
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell36.Dpi = 254F;
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseBorders = false;
        this.xrTableCell36.Weight = 0.43758884636413342;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 90;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.BorderWidth = 2;
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel5});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.Location = new System.Drawing.Point(0, 0);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(1849, 90);
        this.xrPanel1.StylePriority.UseBorders = false;
        this.xrPanel1.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel5
        // 
        this.xrPanel5.BorderWidth = 1;
        this.xrPanel5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel12.Location = new System.Drawing.Point(1609, 10);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(164, 58);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.Text = "کاردان ها";
        // 
        // PageFooter
        // 
        this.PageFooter.Dpi = 254F;
        this.PageFooter.Height = 76;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageFooter.Visible = false;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.Height = 40;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPanel4.BorderWidth = 2;
        this.xrPanel4.Dpi = 254F;
        this.xrPanel4.Location = new System.Drawing.Point(0, 0);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.Size = new System.Drawing.Size(1849, 40);
        this.xrPanel4.StylePriority.UseBorders = false;
        this.xrPanel4.StylePriority.UseBorderWidth = false;
        // 
        // OfficeAaza_Kardan
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(122, 119, 99, 201);
        this.Name = "OfficeAaza_Kardan";
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
