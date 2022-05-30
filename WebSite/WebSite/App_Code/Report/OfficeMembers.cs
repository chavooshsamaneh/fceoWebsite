using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for OfficeMembers
/// </summary>
public class OfficeMembers : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;

    private TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel12;
    private XRPanel xrPanel2;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell13;
    private XRTable xrTable3;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTDesGrdName;
    private XRTableCell xrTFileNo;
    private XRTableCell xrTLastName;
    private XRTableCell xrTFirstName;
    private XRTableCell xrTPersonId;
    private XRTableCell xrTActive;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTImpGrdName;
    private XRTableCell xrTTypeName;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTOfpName;
    private XRTableCell xrTObsGrdName;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OfficeMembers(int OfId, int OfReId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        //OfficeMemberManager.FindByOffRequest(OfId, OfReId, -1, -1);
        OfficeMemberManager.FindByOffRequest(OfId, OfReId, -1);
        xrTPersonId.DataBindings.Add("Text", OfficeMemberManager.DataTable, "PersonId");
        xrTFirstName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "FirstName");
        xrTLastName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "LastName");
        xrTFileNo.DataBindings.Add("Text", OfficeMemberManager.DataTable, "FileNo");
        xrTDesGrdName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "DesGrdName");
        xrTObsGrdName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "ObsGrdName");
        xrTImpGrdName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "ImpGrdName");
        xrTOfpName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "OfpName");
        xrTTypeName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "TypeName");
        xrTActive.DataBindings.Add("Text", OfficeMemberManager.DataTable, "Active");

        this.DataSource = OfficeMemberManager.DataTable;
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
        string resourceFileName = "OfficeMembers.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTActive = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTypeName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTOfpName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTImpGrdName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTObsGrdName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDesGrdName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFileNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTLastName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFirstName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTPersonId = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTActive,
            this.xrTTypeName,
            this.xrTOfpName,
            this.xrTImpGrdName,
            this.xrTObsGrdName,
            this.xrTDesGrdName,
            this.xrTFileNo,
            this.xrTLastName,
            this.xrTFirstName,
            this.xrTPersonId});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTActive
        // 
        this.xrTActive.Dpi = 254F;
        this.xrTActive.Name = "xrTActive";
        this.xrTActive.Text = "xrTActive";
        this.xrTActive.Weight = 0.34917360024094635;
        // 
        // xrTTypeName
        // 
        this.xrTTypeName.Dpi = 254F;
        this.xrTTypeName.Name = "xrTTypeName";
        this.xrTTypeName.Text = "xrTTypeName";
        this.xrTTypeName.Weight = 0.27230626154345533;
        // 
        // xrTOfpName
        // 
        this.xrTOfpName.Dpi = 254F;
        this.xrTOfpName.Name = "xrTOfpName";
        this.xrTOfpName.Text = "xrTOfpName";
        this.xrTOfpName.Weight = 0.7591218106316403;
        // 
        // xrTImpGrdName
        // 
        this.xrTImpGrdName.Dpi = 254F;
        this.xrTImpGrdName.Name = "xrTImpGrdName";
        this.xrTImpGrdName.Text = "xrTImpGrdName";
        this.xrTImpGrdName.Weight = 0.23335391770022468;
        // 
        // xrTObsGrdName
        // 
        this.xrTObsGrdName.Dpi = 254F;
        this.xrTObsGrdName.Name = "xrTObsGrdName";
        this.xrTObsGrdName.Text = "xrTObsGrdName";
        this.xrTObsGrdName.Weight = 0.23364685765143942;
        // 
        // xrTDesGrdName
        // 
        this.xrTDesGrdName.Dpi = 254F;
        this.xrTDesGrdName.Name = "xrTDesGrdName";
        this.xrTDesGrdName.Text = "xrTDesGrdName";
        this.xrTDesGrdName.Weight = 0.23806507523909837;
        // 
        // xrTFileNo
        // 
        this.xrTFileNo.Dpi = 254F;
        this.xrTFileNo.Name = "xrTFileNo";
        this.xrTFileNo.Text = "xrTFileNo";
        this.xrTFileNo.Weight = 0.46242670673868558;
        // 
        // xrTLastName
        // 
        this.xrTLastName.Dpi = 254F;
        this.xrTLastName.Name = "xrTLastName";
        this.xrTLastName.Text = "xrTLastName";
        this.xrTLastName.Weight = 0.65459912545813348;
        // 
        // xrTFirstName
        // 
        this.xrTFirstName.Dpi = 254F;
        this.xrTFirstName.Name = "xrTFirstName";
        this.xrTFirstName.Text = "xrTFirstName";
        this.xrTFirstName.Weight = 0.51487644646195219;
        // 
        // xrTPersonId
        // 
        this.xrTPersonId.Dpi = 254F;
        this.xrTPersonId.Name = "xrTPersonId";
        this.xrTPersonId.Text = "xrTPersonId";
        this.xrTPersonId.Weight = 0.32289018224853921;
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
        this.xrLabel12.Location = new System.Drawing.Point(1556, 11);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(241, 50);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "اعضای شرکت";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            this.xrTableCell13,
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell2,
            this.xrTableCell1,
            this.xrTableCell19,
            this.xrTableCell24,
            this.xrTableCell20,
            this.xrTableCell22,
            this.xrTableCell23});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "وضعیت";
        this.xrTableCell13.Weight = 0.38958127927779507;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "نوع";
        this.xrTableCell4.Weight = 0.30229725127244406;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "سمت";
        this.xrTableCell3.Weight = 0.84740297383426832;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "اجرا";
        this.xrTableCell2.Weight = 0.26174520192908;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "نظارت";
        this.xrTableCell1.Weight = 0.26203698036696821;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "طراحی";
        this.xrTableCell19.Weight = 0.26755362070484257;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "شماره پروانه";
        this.xrTableCell24.Weight = 0.51534001708174282;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Text = "نام خانوادگی";
        this.xrTableCell20.Weight = 0.7299291476830041;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "نام";
        this.xrTableCell22.Weight = 0.5740398797710845;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "کد عضویت";
        this.xrTableCell23.Weight = 0.36137847416735369;
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
        // OfficeMembers
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(119, 124, 107, 201);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}