using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportTSRMemImpTotalCapacity
/// </summary>
public class XtraReportTSRMemImpTotalCapacity : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    Capacity Cpcty = new Capacity();
    DataTable CapacityInfoDT = new DataTable();
    int MId;
    string Yr;

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel10;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTMaxJobCount;
    private XRTableCell xrTTotalCapacity;
    private XRTableCell xrTFNO;
    private XRTable xrTable2;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTConfirmDate;
    private XRTableCell xrTExpireDate;
    private XRTableCell xrTProjectCount;
    private XRTableCell xrTTotalUsed;
    private XRTableCell xrTConditionalCapacity;
    private XRTableCell xrTRemainCapacity;
    private GroupFooterBand GroupFooter1;
    private XRPanel xrPanel4;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTGrade;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTMaxFloor;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSRMemImpTotalCapacity(int MeId, string Year)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        //************************************************************
        //************************************************************
        //*********************Commented By tayebi********************
        //************************************************************
        //************************************************************
        //************************************************************ 
        //MId = MeId;
        //Yr = Year;

        //if (MeId > 0 && Year != "")
        //    CapacityInfoDT = Cpcty.GetYearInformation((int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSMemberType.Member, MeId, Year);

        //CapacityInfoDT.Columns.Add("GrdName");
        //for (int i = 0; i < CapacityInfoDT.Rows.Count; i++)
        //    CapacityInfoDT.Rows[i]["GrdName"] = GetGrdName(Convert.ToInt32(CapacityInfoDT.Rows[i]["Grade"]));

        //this.DataSource = CapacityInfoDT;

        //xrTFNO.DataBindings.Add("Text", this.DataSource, "FNO");
        //xrTConfirmDate.DataBindings.Add("Text", this.DataSource, "ConfirmDate");
        //xrTExpireDate.DataBindings.Add("Text", this.DataSource, "ExpireDate");
        //xrTGrade.DataBindings.Add("Text", this.DataSource, "GrdName");
        //xrTTotalCapacity.DataBindings.Add("Text", this.DataSource, "TotalCapacity");
        //xrTMaxJobCount.DataBindings.Add("Text", this.DataSource, "MaxJobCount");
        //xrTMaxFloor.DataBindings.Add("Text", this.DataSource, "MaxFloor");
        //xrTProjectCount.DataBindings.Add("Text", this.DataSource, "ProjectCount");
        //xrTTotalUsed.DataBindings.Add("Text", this.DataSource, "TotalUsed");
        //xrTConditionalCapacity.DataBindings.Add("Text", this.DataSource, "ConditionalCapacity");
        //xrTRemainCapacity.DataBindings.Add("Text", this.DataSource, "RemainCapacity");        
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
        string resourceFileName = "XtraReportTSRMemImpTotalCapacity.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRemainCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTConditionalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalUsed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMaxFloor = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMaxJobCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTGrade = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTExpireDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTConfirmDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTFNO = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 64;
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
        this.xrTable1.Size = new System.Drawing.Size(1849, 64);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1.2782152230971129;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
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
            this.xrTableRow2});
        this.xrTable4.Size = new System.Drawing.Size(1828, 64);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRemainCapacity,
            this.xrTConditionalCapacity,
            this.xrTTotalUsed,
            this.xrTProjectCount,
            this.xrTMaxFloor,
            this.xrTMaxJobCount,
            this.xrTTotalCapacity,
            this.xrTGrade,
            this.xrTExpireDate,
            this.xrTConfirmDate,
            this.xrTFNO});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTRemainCapacity
        // 
        this.xrTRemainCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTRemainCapacity.Dpi = 254F;
        this.xrTRemainCapacity.Name = "xrTRemainCapacity";
        this.xrTRemainCapacity.StylePriority.UseBorders = false;
        this.xrTRemainCapacity.StylePriority.UseTextAlignment = false;
        this.xrTRemainCapacity.Text = "xrTRemainCapacity";
        this.xrTRemainCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTRemainCapacity.Weight = 0.20810395621248176;
        // 
        // xrTConditionalCapacity
        // 
        this.xrTConditionalCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTConditionalCapacity.Dpi = 254F;
        this.xrTConditionalCapacity.Name = "xrTConditionalCapacity";
        this.xrTConditionalCapacity.StylePriority.UseBorders = false;
        this.xrTConditionalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTConditionalCapacity.Text = "xrTConditionalCapacity";
        this.xrTConditionalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTConditionalCapacity.Weight = 0.21816530849424509;
        // 
        // xrTTotalUsed
        // 
        this.xrTTotalUsed.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalUsed.Dpi = 254F;
        this.xrTTotalUsed.Name = "xrTTotalUsed";
        this.xrTTotalUsed.StylePriority.UseBorders = false;
        this.xrTTotalUsed.StylePriority.UseTextAlignment = false;
        this.xrTTotalUsed.Text = "xrTTotalUsed";
        this.xrTTotalUsed.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTTotalUsed.Weight = 0.19659203000335684;
        // 
        // xrTProjectCount
        // 
        this.xrTProjectCount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTProjectCount.Dpi = 254F;
        this.xrTProjectCount.Name = "xrTProjectCount";
        this.xrTProjectCount.StylePriority.UseBorders = false;
        this.xrTProjectCount.StylePriority.UseTextAlignment = false;
        this.xrTProjectCount.Text = "xrTProjectCount";
        this.xrTProjectCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTProjectCount.Weight = 0.19207472772134748;
        // 
        // xrTMaxFloor
        // 
        this.xrTMaxFloor.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTMaxFloor.Dpi = 254F;
        this.xrTMaxFloor.Name = "xrTMaxFloor";
        this.xrTMaxFloor.StylePriority.UseBorders = false;
        this.xrTMaxFloor.StylePriority.UseTextAlignment = false;
        this.xrTMaxFloor.Text = "xrTMaxFloor";
        this.xrTMaxFloor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTMaxFloor.Weight = 0.18000501189044693;
        // 
        // xrTMaxJobCount
        // 
        this.xrTMaxJobCount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTMaxJobCount.Dpi = 254F;
        this.xrTMaxJobCount.Name = "xrTMaxJobCount";
        this.xrTMaxJobCount.StylePriority.UseBorders = false;
        this.xrTMaxJobCount.StylePriority.UseTextAlignment = false;
        this.xrTMaxJobCount.Text = "xrTMaxJobCount";
        this.xrTMaxJobCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTMaxJobCount.Weight = 0.20947167527482036;
        // 
        // xrTTotalCapacity
        // 
        this.xrTTotalCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalCapacity.Dpi = 254F;
        this.xrTTotalCapacity.Name = "xrTTotalCapacity";
        this.xrTTotalCapacity.StylePriority.UseBorders = false;
        this.xrTTotalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTTotalCapacity.Text = "xrTTotalCapacity";
        this.xrTTotalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTTotalCapacity.Weight = 0.23987195541367551;
        // 
        // xrTGrade
        // 
        this.xrTGrade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTGrade.Dpi = 254F;
        this.xrTGrade.Name = "xrTGrade";
        this.xrTGrade.StylePriority.UseBorders = false;
        this.xrTGrade.StylePriority.UseTextAlignment = false;
        this.xrTGrade.Text = "xrTGrade";
        this.xrTGrade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTGrade.Weight = 0.17490554523036891;
        // 
        // xrTExpireDate
        // 
        this.xrTExpireDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTExpireDate.Dpi = 254F;
        this.xrTExpireDate.Name = "xrTExpireDate";
        this.xrTExpireDate.StylePriority.UseBorders = false;
        this.xrTExpireDate.StylePriority.UseTextAlignment = false;
        this.xrTExpireDate.Text = "xrTExpireDate";
        this.xrTExpireDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTExpireDate.Weight = 0.347547512271911;
        // 
        // xrTConfirmDate
        // 
        this.xrTConfirmDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTConfirmDate.Dpi = 254F;
        this.xrTConfirmDate.Name = "xrTConfirmDate";
        this.xrTConfirmDate.StylePriority.UseBorders = false;
        this.xrTConfirmDate.StylePriority.UseTextAlignment = false;
        this.xrTConfirmDate.Text = "xrTConfirmDate";
        this.xrTConfirmDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTConfirmDate.Weight = 0.35172245437623173;
        // 
        // xrTFNO
        // 
        this.xrTFNO.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTFNO.Dpi = 254F;
        this.xrTFNO.Name = "xrTFNO";
        this.xrTFNO.StylePriority.UseBorders = false;
        this.xrTFNO.StylePriority.UseTextAlignment = false;
        this.xrTFNO.Text = "xrTFNO";
        this.xrTFNO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTFNO.Weight = 0.43946989047188562;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 175;
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
        this.xrPanel1.Size = new System.Drawing.Size(1849, 175);
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
        this.xrLabel10.Text = "ظرفیت کل";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrTable2.KeepTogether = true;
        this.xrTable2.Location = new System.Drawing.Point(10, 90);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable2.Size = new System.Drawing.Size(1828, 85);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell15,
            this.xrTableCell32,
            this.xrTableCell6,
            this.xrTableCell3,
            this.xrTableCell10,
            this.xrTableCell31,
            this.xrTableCell2,
            this.xrTableCell27,
            this.xrTableCell12,
            this.xrTableCell14});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = "ظرفیت باقیمانده";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell18.Weight = 0.20774238048867916;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "کاهش/افزایش ظرفیت";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell15.Weight = 0.21930451120986813;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseBorders = false;
        this.xrTableCell32.StylePriority.UseTextAlignment = false;
        this.xrTableCell32.Text = "کل کارکرد";
        this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell32.Weight = 0.19561273682713556;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "تعداد پروژه های در دست اجرا";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.19128770483907512;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "حداکثر تعداد طبقات";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell3.Weight = 0.17920682139175045;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "حداکثر تعداد کار";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell10.Weight = 0.20991842994631321;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseBorders = false;
        this.xrTableCell31.StylePriority.UseTextAlignment = false;
        this.xrTableCell31.Text = "ظرفیت کل";
        this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell31.Weight = 0.24044782480819479;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "پایه";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell2.Weight = 0.17469463614942346;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseBorders = false;
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.Text = "تاریخ اعتبار پروانه";
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell27.Weight = 0.34772656864905588;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "تاریخ تایید پروانه";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell12.Weight = 0.3520266416067438;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "شماره پروانه";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell14.Weight = 0.44251361179744247;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.Height = 26;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel4.BorderWidth = 2;
        this.xrPanel4.Dpi = 254F;
        this.xrPanel4.Location = new System.Drawing.Point(0, 0);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.Size = new System.Drawing.Size(1849, 15);
        this.xrPanel4.StylePriority.UseBorders = false;
        this.xrPanel4.StylePriority.UseBorderWidth = false;
        // 
        // XtraReportTSRMemImpTotalCapacity
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.GroupFooter1});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}