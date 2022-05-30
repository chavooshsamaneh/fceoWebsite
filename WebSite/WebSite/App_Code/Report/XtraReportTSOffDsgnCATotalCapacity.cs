using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTSOffDsgnCATotalCapacity
/// </summary>
public class XtraReportTSOffDsgnCATotalCapacity : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    Capacity Cpcty = new Capacity();

    ArrayList StageArr = new ArrayList();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel10;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTMaxJobCount;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTTotalCapacity;
    private XRTableCell xrTableCell4;
    private XRLabel xrLabel1;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTTotalUsed;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTProjectCount;
    private XRTableCell xrTableCell9;
    private XRLabel xrLabel12;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTConditionalCapacity;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTRemainCapacity;
    private XRTableCell xrTableCell13;
    private XRLabel xrLabel2;
    private XRSubreport xrSubOfficeMembers;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTJobCountPrcnt;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTCapacityPrcnt;
    private XRTableCell xrTableCell14;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTStage;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTYear;
    private XRTableCell xrTableCell18;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSOffDsgnCATotalCapacity(int OfficeId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        // CapacityInfoArr -----> ArrayList[0]: TotalCapacity(int), ArrayList[1]:UsedCapacity(int) , ArrayList[2]: RemainCapacity(int), 
        //                        ArrayList[3]:ReservedCapacity(int) , ArrayList[4]: ProjectNum(int), ArrayList[5]: MaxJoubCount(string), 
        //                        ArrayList[6]: MaxFloor(string), ArrayList[7]: ConditionalCapacity(int)

        Cpcty.GetCapacityInformation((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSMemberType.Office, Convert.ToInt32(OfficeId), true);

        //SetCapacityAssignmentDsgTexts(CapacityInfoArr[0].ToString(), CapacityInfoArr[5].ToString(), CapacityInfoArr[1].ToString(), CapacityInfoArr[2].ToString(), CapacityInfoArr[4].ToString(), CapacityInfoArr[7].ToString());
        SetCapacityAssignmentDsgTexts(Cpcty.IngridientMaxJobCapacity.ToString(), Cpcty.IngridientMaxJobCount.ToString(), Cpcty.IngridientUsedCapacityValue.ToString(), Cpcty.IngridientRemainCapacity.ToString(),Cpcty.IngridientProjectNum.ToString(), Cpcty.IngridientConditionalCapacityDesign.ToString());

        SetCurrentStage();

        XtraReportTSOffDsgnCAMembers ReMembers = new XtraReportTSOffDsgnCAMembers(OfficeId);
        xrSubOfficeMembers.ReportSource = ReMembers;
    }

    private void SetCapacityAssignmentDsgTexts(string TotalCapacity, string MaxJobCount, string TotalUsed, string RemainCapacity, string ProjectCount, string ConditionalCapacity)
    {
        xrTTotalCapacity.Text = TotalCapacity;
        xrTMaxJobCount.Text = MaxJobCount;
        xrTTotalUsed.Text = TotalUsed;
        xrTRemainCapacity.Text = RemainCapacity;
        xrTProjectCount.Text = ProjectCount;
        xrTConditionalCapacity.Text = ConditionalCapacity;
    }

    private void SetCurrentStage()
    {
        // StageArr -----> ArrayList[0] = Year, ArrayList[1] = StageText, ArrayList[0] = CapacityPrcnt, ArrayList[1] = JobCountPrcnt

        StageArr = Cpcty.GetCurrentStage();
        if (StageArr.Count != 0)
        {
            xrTYear.Text = StageArr[0].ToString();
            xrTStage.Text = StageArr[1].ToString();
            xrTCapacityPrcnt.Text = StageArr[2].ToString();
            xrTJobCountPrcnt.Text = StageArr[3].ToString();
        }
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
        string resourceFileName = "XtraReportTSOffDsgnCATotalCapacity.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTStage = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTYear = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTJobCountPrcnt = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCapacityPrcnt = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTMaxJobCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTTotalUsed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTConditionalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTRemainCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrSubOfficeMembers = new DevExpress.XtraReports.UI.XRSubreport();
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
            this.xrTable1,
            this.xrSubOfficeMembers});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 442;
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
        this.xrTable1.Size = new System.Drawing.Size(1849, 334);
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
        this.xrTable4.Location = new System.Drawing.Point(10, 10);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7,
            this.xrTableRow6,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4});
        this.xrTable4.Size = new System.Drawing.Size(1829, 320);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTStage,
            this.xrTableCell16,
            this.xrTYear,
            this.xrTableCell18});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTStage
        // 
        this.xrTStage.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTStage.Dpi = 254F;
        this.xrTStage.Name = "xrTStage";
        this.xrTStage.StylePriority.UseBorders = false;
        this.xrTStage.StylePriority.UseTextAlignment = false;
        this.xrTStage.Text = "xrTStage";
        this.xrTStage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTStage.Weight = 1;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "مرحله";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell16.Weight = 0.48219363931217574;
        // 
        // xrTYear
        // 
        this.xrTYear.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTYear.Dpi = 254F;
        this.xrTYear.Name = "xrTYear";
        this.xrTYear.StylePriority.UseBorders = false;
        this.xrTYear.StylePriority.UseTextAlignment = false;
        this.xrTYear.Text = "xrTYear";
        this.xrTYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTYear.Weight = 0.8650137816180351;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell18.Weight = 0.65771330077563916;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel4.Location = new System.Drawing.Point(212, 11);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.Size = new System.Drawing.Size(169, 50);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "سال";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTJobCountPrcnt,
            this.xrTableCell10,
            this.xrTCapacityPrcnt,
            this.xrTableCell14});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTJobCountPrcnt
        // 
        this.xrTJobCountPrcnt.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTJobCountPrcnt.Dpi = 254F;
        this.xrTJobCountPrcnt.Name = "xrTJobCountPrcnt";
        this.xrTJobCountPrcnt.StylePriority.UseBorders = false;
        this.xrTJobCountPrcnt.StylePriority.UseTextAlignment = false;
        this.xrTJobCountPrcnt.Text = "xrTJobCountPrcnt";
        this.xrTJobCountPrcnt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTJobCountPrcnt.Weight = 1;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "درصد تعداد کار";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell10.Weight = 0.48219363931217574;
        // 
        // xrTCapacityPrcnt
        // 
        this.xrTCapacityPrcnt.Dpi = 254F;
        this.xrTCapacityPrcnt.Name = "xrTCapacityPrcnt";
        this.xrTCapacityPrcnt.StylePriority.UseTextAlignment = false;
        this.xrTCapacityPrcnt.Text = "xrTCapacityPrcnt";
        this.xrTCapacityPrcnt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTCapacityPrcnt.Weight = 0.8650137816180351;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell14.Weight = 0.65771330077563916;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel5.Location = new System.Drawing.Point(212, 11);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.Size = new System.Drawing.Size(169, 50);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "درصد ظرفیت";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTMaxJobCount,
            this.xrTableCell3,
            this.xrTTotalCapacity,
            this.xrTableCell4});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTMaxJobCount
        // 
        this.xrTMaxJobCount.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTMaxJobCount.Dpi = 254F;
        this.xrTMaxJobCount.Name = "xrTMaxJobCount";
        this.xrTMaxJobCount.StylePriority.UseBorders = false;
        this.xrTMaxJobCount.StylePriority.UseTextAlignment = false;
        this.xrTMaxJobCount.Text = "xrTMaxJobCount";
        this.xrTMaxJobCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTMaxJobCount.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "حداکثر تعداد کار";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 0.48219363931217574;
        // 
        // xrTTotalCapacity
        // 
        this.xrTTotalCapacity.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTTotalCapacity.Dpi = 254F;
        this.xrTTotalCapacity.Name = "xrTTotalCapacity";
        this.xrTTotalCapacity.StylePriority.UseBorders = false;
        this.xrTTotalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTTotalCapacity.Text = "xrTTotalCapacity";
        this.xrTTotalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTTotalCapacity.Weight = 0.8650137816180351;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell4.Weight = 0.65771330077563916;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel1.Location = new System.Drawing.Point(212, 11);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.Size = new System.Drawing.Size(169, 50);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "ظرفیت کل";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTTotalUsed,
            this.xrTableCell7,
            this.xrTProjectCount,
            this.xrTableCell9});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTTotalUsed
        // 
        this.xrTTotalUsed.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTTotalUsed.Dpi = 254F;
        this.xrTTotalUsed.Name = "xrTTotalUsed";
        this.xrTTotalUsed.StylePriority.UseBorders = false;
        this.xrTTotalUsed.StylePriority.UseTextAlignment = false;
        this.xrTTotalUsed.Text = "xrTTotalUsed";
        this.xrTTotalUsed.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTTotalUsed.Weight = 1;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "کل کارکرد";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell7.Weight = 0.48219363931217574;
        // 
        // xrTProjectCount
        // 
        this.xrTProjectCount.Dpi = 254F;
        this.xrTProjectCount.Name = "xrTProjectCount";
        this.xrTProjectCount.StylePriority.UseTextAlignment = false;
        this.xrTProjectCount.Text = "xrTProjectCount";
        this.xrTProjectCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTProjectCount.Weight = 0.86501378161803533;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell9.Weight = 0.65771330077563916;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel12.Location = new System.Drawing.Point(22, 0);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(360, 50);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "تعداد پروژه های در دست اجرا";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTConditionalCapacity,
            this.xrTableCell11,
            this.xrTRemainCapacity,
            this.xrTableCell13});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTConditionalCapacity
        // 
        this.xrTConditionalCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTConditionalCapacity.Dpi = 254F;
        this.xrTConditionalCapacity.Name = "xrTConditionalCapacity";
        this.xrTConditionalCapacity.StylePriority.UseBorders = false;
        this.xrTConditionalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTConditionalCapacity.Text = "xrTConditionalCapacity";
        this.xrTConditionalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTConditionalCapacity.Weight = 1;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "کاهش/افزایش ظرفیت";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell11.Weight = 0.48219363931217574;
        // 
        // xrTRemainCapacity
        // 
        this.xrTRemainCapacity.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTRemainCapacity.Dpi = 254F;
        this.xrTRemainCapacity.Name = "xrTRemainCapacity";
        this.xrTRemainCapacity.StylePriority.UseBorders = false;
        this.xrTRemainCapacity.StylePriority.UseTextAlignment = false;
        this.xrTRemainCapacity.Text = "xrTRemainCapacity";
        this.xrTRemainCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTRemainCapacity.Weight = 0.86501378161803533;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell13.Weight = 0.65771330077563916;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel2.Location = new System.Drawing.Point(170, 0);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(212, 50);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "ظرفیت باقیمانده";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrSubOfficeMembers
        // 
        this.xrSubOfficeMembers.Dpi = 254F;
        this.xrSubOfficeMembers.Location = new System.Drawing.Point(0, 335);
        this.xrSubOfficeMembers.Name = "xrSubOfficeMembers";
        this.xrSubOfficeMembers.Size = new System.Drawing.Size(1799, 64);
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
        this.xrLabel10.Text = "اختصاص ظرفیت";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // XtraReportTSOffDsgnCATotalCapacity
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