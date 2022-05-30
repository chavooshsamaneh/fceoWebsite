using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTSMembersInfo
/// </summary>
public class XtraReportTSMembersInfo : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
    Capacity Cpty = new Capacity();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel10;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTMeName;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTMeId;
    private XRTableCell xrTableCell4;
    private XRLabel xrLabel1;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTFileDate;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTFileNo;
    private XRTableCell xrTableCell9;
    private XRLabel xrLabel12;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTGrade;
    private XRTableCell xrTableCell13;
    private XRLabel xrLabel2;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSMembersInfo(int MeId,int ProjectIngridientType)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        MemberManager.FindByCode(MeId);        
            
        this.DataSource = MemberManager.DataTable;

        xrTMeId.DataBindings.Add("Text", MemberManager.DataTable, "MeId");
        xrTMeName.DataBindings.Add("Text", MemberManager.DataTable, "MeName");
        xrTFileDate.DataBindings.Add("Text", MemberManager.DataTable, "FileDate");
        xrTFileNo.DataBindings.Add("Text", MemberManager.DataTable, "FileNo");

        xrTGrade.Text = GetGrdName(Cpty.GetMemGrade(MeId, ProjectIngridientType));

    }

    private string GetGrdName(int GrdId)
    {
        TSP.DataManager.GradeManager GradeManager = new TSP.DataManager.GradeManager();
        GradeManager.FindByCode(GrdId);
        if (GradeManager.Count > 0)
            return GradeManager[0]["GrdName"].ToString();
        else
            return "";
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
        string resourceFileName = "XtraReportTSMembersInfo.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTMeName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeId = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTFileDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFileNo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTGrade = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 241;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Location = new System.Drawing.Point(0, 0);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.Size = new System.Drawing.Size(1849, 206);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.BorderWidth = 2;
        this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseBorderWidth = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell1.Weight = 3;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable4.BorderWidth = 1;
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrTable4.Location = new System.Drawing.Point(10, 0);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4});
        this.xrTable4.Size = new System.Drawing.Size(1829, 192);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTMeName,
            this.xrTableCell3,
            this.xrTMeId,
            this.xrTableCell4});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTMeName
        // 
        this.xrTMeName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTMeName.Dpi = 254F;
        this.xrTMeName.Name = "xrTMeName";
        this.xrTMeName.StylePriority.UseBorders = false;
        this.xrTMeName.StylePriority.UseTextAlignment = false;
        this.xrTMeName.Text = "xrTMeName";
        this.xrTMeName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTMeName.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "نام";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 0.48219363931217574;
        // 
        // xrTMeId
        // 
        this.xrTMeId.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTMeId.Dpi = 254F;
        this.xrTMeId.Name = "xrTMeId";
        this.xrTMeId.StylePriority.UseBorders = false;
        this.xrTMeId.StylePriority.UseTextAlignment = false;
        this.xrTMeId.Text = "xrTMeId";
        this.xrTMeId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTMeId.Weight = 1.017806360687824;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell4.Weight = 0.50492072170585023;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel1.Location = new System.Drawing.Point(119, 11);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.Size = new System.Drawing.Size(169, 50);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "کد عضویت";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTFileDate,
            this.xrTableCell7,
            this.xrTFileNo,
            this.xrTableCell9});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTFileDate
        // 
        this.xrTFileDate.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTFileDate.Dpi = 254F;
        this.xrTFileDate.Name = "xrTFileDate";
        this.xrTFileDate.StylePriority.UseBorders = false;
        this.xrTFileDate.StylePriority.UseTextAlignment = false;
        this.xrTFileDate.Text = "xrTFileDate";
        this.xrTFileDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTFileDate.Weight = 1;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "تاریخ اعتبار پروانه";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell7.Weight = 0.48219363931217574;
        // 
        // xrTFileNo
        // 
        this.xrTFileNo.Dpi = 254F;
        this.xrTFileNo.Name = "xrTFileNo";
        this.xrTFileNo.StylePriority.UseTextAlignment = false;
        this.xrTFileNo.Text = "xrTFileNo";
        this.xrTFileNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTFileNo.Weight = 1.0178063606878243;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell9.Weight = 0.50492072170585023;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel2.Location = new System.Drawing.Point(119, 11);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(169, 50);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "شماره پروانه";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell11,
            this.xrTGrade,
            this.xrTableCell13});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell12.Weight = 1;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell11.Weight = 0.48219363931217574;
        // 
        // xrTGrade
        // 
        this.xrTGrade.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTGrade.Dpi = 254F;
        this.xrTGrade.Name = "xrTGrade";
        this.xrTGrade.StylePriority.UseBorders = false;
        this.xrTGrade.StylePriority.UseTextAlignment = false;
        this.xrTGrade.Text = "xrTGrade";
        this.xrTGrade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTGrade.Weight = 1.0178063606878243;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell13.Weight = 0.50492072170585023;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel12.Location = new System.Drawing.Point(119, 11);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(169, 50);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "پایه";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)));
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
        this.xrPanel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel5.BorderWidth = 1;
        this.xrPanel5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel10.Location = new System.Drawing.Point(1450, 11);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.Size = new System.Drawing.Size(360, 56);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseForeColor = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "اطلاعات عضو حقیقی";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // XtraReportTSMembersInfo
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}