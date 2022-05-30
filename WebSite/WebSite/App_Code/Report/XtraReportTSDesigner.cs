using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTSDesigner
/// </summary>
public class XtraReportTSDesigner : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    TSP.DataManager.TechnicalServices.Designer_PlansManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel12;
    private XRTable xrTable2;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTable xrTable3;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTWage;
    private XRTableCell xrTMeType;
    private XRTableCell xrTDesignerName;
    private XRTableCell xrTIsMASter;
    private XRPanel xrPanel3;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTCapacityDecrement;
    private XRTableCell xrTOfficeEngOId;
    private XRCheckBox checkEditIsMASter;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTPlansType;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSDesigner(int ProjectId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        ProjectObserversManager.FindActivesByProjectId(ProjectId);

        checkEditIsMASter.DataBindings.Add("Checked", ProjectObserversManager.DataTable, "IsMASter");
        xrTOfficeEngOId.DataBindings.Add("Text", ProjectObserversManager.DataTable, "OfficeEngOId");
        xrTDesignerName.DataBindings.Add("Text", ProjectObserversManager.DataTable, "DesignerName");
        xrTMeType.DataBindings.Add("Text", ProjectObserversManager.DataTable, "MeType");
        xrTCapacityDecrement.DataBindings.Add("Text", ProjectObserversManager.DataTable, "CapacityDecrement");
        xrTWage.DataBindings.Add("Text", ProjectObserversManager.DataTable, "Wage");
        xrTPlansType.DataBindings.Add("Text", ProjectObserversManager.DataTable, "PlansType");

        this.DataSource = ProjectObserversManager.DataTable;
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
        string resourceFileName = "XtraReportTSDesigner.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTPlansType = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTWage = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCapacityDecrement = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeType = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDesignerName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTOfficeEngOId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTIsMASter = new DevExpress.XtraReports.UI.XRTableCell();
        this.checkEditIsMASter = new DevExpress.XtraReports.UI.XRCheckBox();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 85F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel3.BorderWidth = 2;
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(1849F, 85F);
        this.xrPanel3.StylePriority.UseBorders = false;
        this.xrPanel3.StylePriority.UseBorderWidth = false;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 1;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1829F, 85F);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTPlansType,
            this.xrTWage,
            this.xrTCapacityDecrement,
            this.xrTMeType,
            this.xrTDesignerName,
            this.xrTOfficeEngOId,
            this.xrTIsMASter});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTPlansType
        // 
        this.xrTPlansType.Dpi = 254F;
        this.xrTPlansType.Name = "xrTPlansType";
        this.xrTPlansType.Text = "xrTPlansType";
        this.xrTPlansType.Weight = 0.22390863817926457;
        // 
        // xrTWage
        // 
        this.xrTWage.Dpi = 254F;
        this.xrTWage.Name = "xrTWage";
        this.xrTWage.Text = "xrTWage";
        this.xrTWage.Weight = 0.18631350384973419;
        // 
        // xrTCapacityDecrement
        // 
        this.xrTCapacityDecrement.Dpi = 254F;
        this.xrTCapacityDecrement.Name = "xrTCapacityDecrement";
        this.xrTCapacityDecrement.Text = "xrTCapacityDecrement";
        this.xrTCapacityDecrement.Weight = 0.213369623843352;
        // 
        // xrTMeType
        // 
        this.xrTMeType.Dpi = 254F;
        this.xrTMeType.Name = "xrTMeType";
        this.xrTMeType.Text = "xrTMeType";
        this.xrTMeType.Weight = 0.30621649238472837;
        // 
        // xrTDesignerName
        // 
        this.xrTDesignerName.Dpi = 254F;
        this.xrTDesignerName.Name = "xrTDesignerName";
        this.xrTDesignerName.Text = "xrTDesignerName";
        this.xrTDesignerName.Weight = 0.52222403352864077;
        // 
        // xrTOfficeEngOId
        // 
        this.xrTOfficeEngOId.Dpi = 254F;
        this.xrTOfficeEngOId.Name = "xrTOfficeEngOId";
        this.xrTOfficeEngOId.Text = "xrTOfficeEngOId";
        this.xrTOfficeEngOId.Weight = 0.19130288778303653;
        // 
        // xrTIsMASter
        // 
        this.xrTIsMASter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.checkEditIsMASter});
        this.xrTIsMASter.Dpi = 254F;
        this.xrTIsMASter.Name = "xrTIsMASter";
        this.xrTIsMASter.Weight = 0.21300451346115279;
        // 
        // checkEditIsMASter
        // 
        this.checkEditIsMASter.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.checkEditIsMASter.Dpi = 254F;
        this.checkEditIsMASter.LocationFloat = new DevExpress.Utils.PointFloat(87F, 21F);
        this.checkEditIsMASter.Name = "checkEditIsMASter";
        this.checkEditIsMASter.SizeF = new System.Drawing.SizeF(42F, 42F);
        this.checkEditIsMASter.StylePriority.UseBorders = false;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 175F;
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
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1849F, 175F);
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
        this.xrPanel5.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.SizeF = new System.Drawing.SizeF(1829F, 70F);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(1640F, 11F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(177F, 56F);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "طراح";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 95F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1829F, 85F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell27,
            this.xrTableCell2,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "نوع نقشه";
        this.xrTableCell1.Weight = 0.44829769260301172;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.Text = "متراژ دستمزد";
        this.xrTableCell27.Weight = 0.37302666382837663;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "متراژ کسر ظرفیت";
        this.xrTableCell2.Weight = 0.42719712462567722;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Text = "نوع طراح";
        this.xrTableCell31.Weight = 0.61309000131913682;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = "نام";
        this.xrTableCell32.Weight = 1.0433899574478822;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.Text = "کد عضویت";
        this.xrTableCell33.Weight = 0.38129756521897296;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.Text = "نماینده اصلی";
        this.xrTableCell34.Weight = 0.43036349033006971;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 15F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel4.BorderWidth = 2;
        this.xrPanel4.Dpi = 254F;
        this.xrPanel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.SizeF = new System.Drawing.SizeF(1849F, 15F);
        this.xrPanel4.StylePriority.UseBorders = false;
        this.xrPanel4.StylePriority.UseBorderWidth = false;
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Dpi = 254F;
        this.topMarginBand1.HeightF = 157F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.Dpi = 254F;
        this.bottomMarginBand1.HeightF = 201F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // XtraReportTSDesigner
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
