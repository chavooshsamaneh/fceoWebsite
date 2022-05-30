using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for OfficeAgent
/// </summary>
public class OfficeAgent : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private TSP.DataManager.OfficeAgentManager OfAgManager = new TSP.DataManager.OfficeAgentManager();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel12;
    private XRPanel xrPanel2;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTable xrTable3;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTTel;
    private XRTableCell xrTResponsible;
    private XRTableCell xrTOagName;
    private XRTableCell xrTFax;
    private XRTableCell xrTAddres;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OfficeAgent(int OfId, int OfReId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        //OfAgManager.FindByOfCode(OfId);

        //xrlOfAgAddres .DataBindings.Add("Text", OfAgManager.DataTable, "Addres");
        //xrlOfAgEmail .DataBindings.Add("Text", OfAgManager.DataTable, "Email");
        //xrlOfAgFax .DataBindings.Add("Text", OfAgManager.DataTable, "Fax");
        //xrlOfAgManager.DataBindings.Add("Text", OfAgManager.DataTable, "Responsible");
        //xrlOfAgName.DataBindings.Add("Text", OfAgManager.DataTable, "OagName");
        //xrlOfAgTel.DataBindings.Add("Text", OfAgManager.DataTable, "Tel");
        //xrlOfAgWebsite.DataBindings.Add("Text", OfAgManager.DataTable, "WebSite");

        OfAgManager.FindByOffRequest(OfId, OfReId, -1, -1);

        xrTOagName.DataBindings.Add("Text", OfAgManager.DataTable, "OagName");
        xrTResponsible.DataBindings.Add("Text", OfAgManager.DataTable, "Responsible");
        xrTTel.DataBindings.Add("Text", OfAgManager.DataTable, "Tel");
        xrTFax.DataBindings.Add("Text", OfAgManager.DataTable, "Fax");
        xrTAddres.DataBindings.Add("Text", OfAgManager.DataTable, "Addres");

        this.DataSource = OfAgManager.DataTable;
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
        string resourceFileName = "OfficeAgent.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTAddres = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFax = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTel = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTResponsible = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTOagName = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 85;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel2
        // 
        this.xrPanel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel2.BorderWidth = 2;
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.Location = new System.Drawing.Point(0, 0);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.Size = new System.Drawing.Size(1849, 85);
        this.xrPanel2.StylePriority.UseBorders = false;
        this.xrPanel2.StylePriority.UseBorderWidth = false;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 1;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.Location = new System.Drawing.Point(10, 0);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable3.Size = new System.Drawing.Size(1829, 85);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTAddres,
            this.xrTFax,
            this.xrTTel,
            this.xrTResponsible,
            this.xrTOagName});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTAddres
        // 
        this.xrTAddres.Dpi = 254F;
        this.xrTAddres.Name = "xrTAddres";
        this.xrTAddres.Text = "xrTAddres";
        this.xrTAddres.Weight = 1.5643178476630479;
        // 
        // xrTFax
        // 
        this.xrTFax.Dpi = 254F;
        this.xrTFax.Name = "xrTFax";
        this.xrTFax.Text = "xrTFax";
        this.xrTFax.Weight = 0.51544531779059288;
        // 
        // xrTTel
        // 
        this.xrTTel.Dpi = 254F;
        this.xrTTel.Name = "xrTTel";
        this.xrTTel.Text = "xrTTel";
        this.xrTTel.Weight = 0.51542527144687666;
        // 
        // xrTResponsible
        // 
        this.xrTResponsible.Dpi = 254F;
        this.xrTResponsible.Name = "xrTResponsible";
        this.xrTResponsible.Text = "xrTResponsible";
        this.xrTResponsible.Weight = 0.75125108740170632;
        // 
        // xrTOagName
        // 
        this.xrTOagName.Dpi = 254F;
        this.xrTOagName.Name = "xrTOagName";
        this.xrTOagName.Text = "xrTOagName";
        this.xrTOagName.Weight = 0.69402045961189152;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 174;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel1.BorderWidth = 2;
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel5,
            this.xrTable2});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.Location = new System.Drawing.Point(0, 0);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(1849, 174);
        this.xrPanel1.StylePriority.UseBorders = false;
        this.xrPanel1.StylePriority.UseBorderWidth = false;
        // 
        // xrPanel5
        // 
        this.xrPanel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel5.BorderWidth = 1;
        this.xrPanel5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel12.Location = new System.Drawing.Point(1640, 11);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(148, 53);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.Text = "شعبه ها";
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.Location = new System.Drawing.Point(10, 90);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable2.Size = new System.Drawing.Size(1829, 85);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell24,
            this.xrTableCell20,
            this.xrTableCell22,
            this.xrTableCell23});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "آدرس";
        this.xrTableCell19.Weight = 1.74604691712679;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "فکس";
        this.xrTableCell24.Weight = 0.57453701862691819;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Text = "تلفن";
        this.xrTableCell20.Weight = 0.57453701862691831;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "نام مدیر مسئول";
        this.xrTableCell22.Weight = 0.83795984499332543;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "نام شعبه";
        this.xrTableCell23.Weight = 0.7782240267146312;
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
        // OfficeAgent
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
