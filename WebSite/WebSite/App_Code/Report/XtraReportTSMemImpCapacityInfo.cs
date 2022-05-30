using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportTSMemImpCapacityInfo
/// </summary>
public class XtraReportTSMemImpCapacityInfo : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;

    ArrayList CapacityInfoArr = new ArrayList();
    ArrayList MemberArr = new ArrayList();
    ArrayList StageArr = new ArrayList();
    Capacity Cpcty = new Capacity();


    private XRTable xrTable2;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell79;
    private XRTable xrTable3;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell68;
    private XRTableCell xrTableCell64;
    private XRLabel xrLabel37;
    private XRTableCell xrTableCell63;
    private XRPictureBox xrPictureBox2;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell74;
    private XRLabel xrLabel35;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private XRLabel xrLabel33;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell67;
    private XRLabel xrLabel36;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRLabel xrLabel34;
    private XRTable xrTable5;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell80;
    private XRTable xrTable6;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell81;
    private XRLabel xrLabel38;
    private XRPageInfo xrPageInfo1;
    private XRSubreport xrSubOffInfo;
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
    private XRTableCell xrTMaxFloor;
    private XRTableCell xrTableCell9;
    private XRLabel xrLabel12;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTReserved;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTProjectCount;
    private XRTableCell xrTableCell13;
    private XRLabel xrLabel2;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTRemainCapacity;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTConditionalCapacity;
    private XRTableCell xrTableCell8;
    private XRLabel xrLabel3;
    private XRPanel xrPanel5;
    private XRLabel xrLabel10;
    private XRTable xrTable7;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell2;
    private XRTable xrTable8;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTStage;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTYear;
    private XRTableCell xrTableCell18;
    private XRLabel xrLabel4;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTJobCountPrcnt;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTCapacityPrcnt;
    private XRTableCell xrTableCell14;
    private XRLabel xrLabel5;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTCAMaxJobCount;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTCATotalCapacity;
    private XRTableCell xrTableCell17;
    private XRLabel xrLabel6;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTCAConditionalCapacity;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTCAProjectCount;
    private XRTableCell xrTableCell22;
    private XRLabel xrLabel7;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTCARemainCapacity;
    private XRTableCell xrTableCell26;
    private XRLabel xrLabel8;
    private XRPanel xrPanel1;
    private XRLabel xrLabel9;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTCATotalUsed;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTCAMaxFloor;
    private XRTableCell xrTableCell21;
    private XRLabel xrLabel11;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSMemImpCapacityInfo(int MeId)
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
        //XtraReportTSMembersInfo ReOffInfo = new XtraReportTSMembersInfo(MeId,(int)TSP.DataManager.TSProjectIngridientType.Implementer);
        //xrSubOffInfo.ReportSource = ReOffInfo;

        //SetImplementationCapacity(MeId);
        //SetCapacityAssignmentImplementationCapacity(MeId);
        //SetCurrentStage();        
        
    }

    //private void SetImplementationCapacity(int MeId)
    //{

    //    // CapacityInfoArr -----> ArrayList[0]: TotalCapacity(int), ArrayList[1]:UsedCapacity(int) , ArrayList[2]: RemainCapacity(int), 
    //    //                        ArrayList[3]: ReservedCapacity(int) , ArrayList[4]: ProjectNum(int), ArrayList[5]: MaxJoubCount(string), 
    //    //                        ArrayList[6]: MaxFloor(string), ArrayList[7]: ConditionalCapacity(int)

    //    // MemberArr -----> ArrayList[0]: MaxFloor, ArrayList[1]: MaxJobCapacity, ArrayList[2]: MaxUnitCount, ArrayList[3]: Grade, 
    //    //                  ArrayList[4]: ConditionalCapacity

    //    CapacityInfoArr = Cpcty.GetCapacityInformation((int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSMemberType.Member, MeId);
    //    MemberArr = Cpcty.GetMembersImpCapacity(Convert.ToInt32(MeId));

    //    if (CapacityInfoArr.Count > 0 && MemberArr.Count > 0)
    //    {
    //        SetImpTexts(CapacityInfoArr[0].ToString(), CapacityInfoArr[5].ToString(), CapacityInfoArr[1].ToString(), CapacityInfoArr[2].ToString(), CapacityInfoArr[3].ToString(), CapacityInfoArr[4].ToString(), CapacityInfoArr[7].ToString(), CapacityInfoArr[6].ToString());
    //    }

    //}

    //private void SetImpTexts(string TotalCapacity, string MaxJobCount, string TotalUsed, string RemainCapacity, string Reserve, string ProjectCount, string ConditionalCapacity, string MaxFloor)
    //{
    //    xrTTotalCapacity.Text = TotalCapacity;
    //    xrTMaxJobCount.Text = MaxJobCount;
    //    xrTTotalUsed.Text = TotalUsed;
    //    xrTProjectCount.Text = ProjectCount;
    //    xrTReserved.Text = Reserve;
    //    xrTMaxFloor.Text = MaxFloor;
    //    xrTConditionalCapacity.Text = ConditionalCapacity;
    //    xrTRemainCapacity.Text = RemainCapacity;
    //}

    //private void SetCapacityAssignmentImplementationCapacity(int MeId)
    //{
    //    // CapacityInfoArr -----> ArrayList[0]: TotalCapacity(int), ArrayList[1]:UsedCapacity(int) , ArrayList[2]: RemainCapacity(int), 
    //    //                        ArrayList[3]:ReservedCapacity(int) , ArrayList[4]: ProjectNum(int), ArrayList[5]: MaxJoubCount(string), 
    //    //                        ArrayList[6]: MaxFloor(string), ArrayList[7]: ConditionalCapacity(int)

    //    CapacityInfoArr = Cpcty.GetCapacityInformationPerStage((int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSMemberType.Member, MeId);


    //    if (CapacityInfoArr.Count > 0)
    //    {
    //        SetCapacityAssignmentImpTexts(CapacityInfoArr[0].ToString(), CapacityInfoArr[5].ToString(), CapacityInfoArr[1].ToString(), CapacityInfoArr[2].ToString(), CapacityInfoArr[4].ToString(), CapacityInfoArr[7].ToString(), CapacityInfoArr[6].ToString());
    //    }
    //}

    //private void SetCapacityAssignmentImpTexts(string TotalCapacity, string MaxJobCount, string TotalUsed, string RemainCapacity, string ProjectCount, string ConditionalCapacity, string MaxFloor)
    //{
    //    xrTCATotalCapacity.Text = TotalCapacity;
    //    xrTCAMaxJobCount.Text = MaxJobCount;
    //    xrTCATotalUsed.Text = TotalUsed;
    //    xrTCARemainCapacity.Text = RemainCapacity;
    //    xrTCAProjectCount.Text = ProjectCount;
    //    xrTCAConditionalCapacity.Text = ConditionalCapacity;
    //    xrTCAMaxFloor.Text = MaxFloor;
    //}

    //private void SetCurrentStage()
    //{
    //    // StageArr -----> ArrayList[0] = Year, ArrayList[1] = StageText, ArrayList[0] = CapacityPrcnt, ArrayList[1] = JobCountPrcnt

    //    StageArr = Cpcty.GetCurrentStage();
    //    if (StageArr.Count != 0)
    //    {
    //        xrTYear.Text = StageArr[0].ToString();
    //        xrTStage.Text = StageArr[1].ToString();
    //        xrTCapacityPrcnt.Text = StageArr[2].ToString();
    //        xrTJobCountPrcnt.Text = StageArr[3].ToString();
    //    }
    //}

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
        string resourceFileName = "XtraReportTSMemImpCapacityInfo.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrSubOffInfo = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTMaxJobCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTTotalUsed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMaxFloor = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTReserved = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTRemainCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTConditionalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTStage = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTYear = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTJobCountPrcnt = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCapacityPrcnt = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTCAMaxJobCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCATotalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTCATotalUsed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCAMaxFloor = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTCAConditionalCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCAProjectCount = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCARemainCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubOffInfo,
            this.xrTable1,
            this.xrTable7});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 1352;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrSubOffInfo
        // 
        this.xrSubOffInfo.Dpi = 254F;
        this.xrSubOffInfo.Location = new System.Drawing.Point(0, 127);
        this.xrSubOffInfo.Name = "xrSubOffInfo";
        this.xrSubOffInfo.Size = new System.Drawing.Size(1849, 64);
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Location = new System.Drawing.Point(0, 339);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.Size = new System.Drawing.Size(1849, 350);
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
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.BorderWidth = 2;
        this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4,
            this.xrPanel5});
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
        this.xrTable4.Location = new System.Drawing.Point(10, 85);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5});
        this.xrTable4.Size = new System.Drawing.Size(1829, 256);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
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
        this.xrTMaxJobCount.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
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
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "حداکثر تعداد کار";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 0.58241242773429536;
        // 
        // xrTTotalCapacity
        // 
        this.xrTTotalCapacity.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTTotalCapacity.Dpi = 254F;
        this.xrTTotalCapacity.Name = "xrTTotalCapacity";
        this.xrTTotalCapacity.StylePriority.UseBorders = false;
        this.xrTTotalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTTotalCapacity.Text = "xrTTotalCapacity";
        this.xrTTotalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTTotalCapacity.Weight = 0.76479499319591548;
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
        this.xrTableCell4.Weight = 0.65771330077563916;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel1.Location = new System.Drawing.Point(212, 10);
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
            this.xrTMaxFloor,
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
        this.xrTableCell7.Weight = 0.58241242773429536;
        // 
        // xrTMaxFloor
        // 
        this.xrTMaxFloor.Dpi = 254F;
        this.xrTMaxFloor.Name = "xrTMaxFloor";
        this.xrTMaxFloor.StylePriority.UseTextAlignment = false;
        this.xrTMaxFloor.Text = "xrTMaxFloor";
        this.xrTMaxFloor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTMaxFloor.Weight = 0.76479499319591571;
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
        this.xrTableCell9.Weight = 0.65771330077563916;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel2.Location = new System.Drawing.Point(106, 9);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.Size = new System.Drawing.Size(275, 50);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "حداکثر تعداد طبقات";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTReserved,
            this.xrTableCell11,
            this.xrTProjectCount,
            this.xrTableCell13});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTReserved
        // 
        this.xrTReserved.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTReserved.Dpi = 254F;
        this.xrTReserved.Name = "xrTReserved";
        this.xrTReserved.StylePriority.UseBorders = false;
        this.xrTReserved.StylePriority.UseTextAlignment = false;
        this.xrTReserved.Text = "xrTReserved";
        this.xrTReserved.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTReserved.Weight = 1;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "کل رزرو شده";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell11.Weight = 0.58241242773429536;
        // 
        // xrTProjectCount
        // 
        this.xrTProjectCount.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTProjectCount.Dpi = 254F;
        this.xrTProjectCount.Name = "xrTProjectCount";
        this.xrTProjectCount.StylePriority.UseBorders = false;
        this.xrTProjectCount.StylePriority.UseTextAlignment = false;
        this.xrTProjectCount.Text = "xrTProjectCount";
        this.xrTProjectCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTProjectCount.Weight = 0.76479499319591571;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell13.Weight = 0.65771330077563916;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel12.Location = new System.Drawing.Point(21, 10);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(360, 50);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "تعداد پروژه های در دست اجرا";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRemainCapacity,
            this.xrTableCell5,
            this.xrTConditionalCapacity,
            this.xrTableCell8});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.StylePriority.UseTextAlignment = false;
        this.xrTableRow5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableRow5.Weight = 1;
        // 
        // xrTRemainCapacity
        // 
        this.xrTRemainCapacity.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTRemainCapacity.Dpi = 254F;
        this.xrTRemainCapacity.Name = "xrTRemainCapacity";
        this.xrTRemainCapacity.StylePriority.UseBorders = false;
        this.xrTRemainCapacity.Text = "xrTRemainCapacity";
        this.xrTRemainCapacity.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.Text = "ظرفیت باقیمانده";
        this.xrTableCell5.Weight = 0.58241242773429536;
        // 
        // xrTConditionalCapacity
        // 
        this.xrTConditionalCapacity.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTConditionalCapacity.Dpi = 254F;
        this.xrTConditionalCapacity.Name = "xrTConditionalCapacity";
        this.xrTConditionalCapacity.StylePriority.UseBorders = false;
        this.xrTConditionalCapacity.Text = "xrTConditionalCapacity";
        this.xrTConditionalCapacity.Weight = 0.76479499319591571;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.Weight = 0.65771330077563916;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel3.Location = new System.Drawing.Point(106, 10);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.Size = new System.Drawing.Size(275, 50);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "کاهش/افزایش ظرفیت";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
        this.xrLabel10.Location = new System.Drawing.Point(1620, 11);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.Size = new System.Drawing.Size(190, 56);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseForeColor = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "ظرفیت کل";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTable7
        // 
        this.xrTable7.Dpi = 254F;
        this.xrTable7.Location = new System.Drawing.Point(0, 826);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable7.Size = new System.Drawing.Size(1849, 480);
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1.2782152230971129;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.BorderWidth = 2;
        this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8,
            this.xrPanel1});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseBorderWidth = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell2.Weight = 3;
        // 
        // xrTable8
        // 
        this.xrTable8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable8.BorderWidth = 1;
        this.xrTable8.Dpi = 254F;
        this.xrTable8.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrTable8.Location = new System.Drawing.Point(10, 85);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow12,
            this.xrTableRow10,
            this.xrTableRow11});
        this.xrTable8.Size = new System.Drawing.Size(1829, 384);
        this.xrTable8.StylePriority.UseBorders = false;
        this.xrTable8.StylePriority.UseBorderWidth = false;
        this.xrTable8.StylePriority.UseFont = false;
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
        this.xrLabel4.Location = new System.Drawing.Point(212, 10);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.Size = new System.Drawing.Size(169, 50);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "سال";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTJobCountPrcnt,
            this.xrTableCell10,
            this.xrTCapacityPrcnt,
            this.xrTableCell14});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
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
        this.xrLabel5.Location = new System.Drawing.Point(212, 0);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.Size = new System.Drawing.Size(169, 50);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "درصد ظرفیت";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCAMaxJobCount,
            this.xrTableCell12,
            this.xrTCATotalCapacity,
            this.xrTableCell17});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTCAMaxJobCount
        // 
        this.xrTCAMaxJobCount.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTCAMaxJobCount.Dpi = 254F;
        this.xrTCAMaxJobCount.Name = "xrTCAMaxJobCount";
        this.xrTCAMaxJobCount.StylePriority.UseBorders = false;
        this.xrTCAMaxJobCount.StylePriority.UseTextAlignment = false;
        this.xrTCAMaxJobCount.Text = "xrTCAMaxJobCount";
        this.xrTCAMaxJobCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTCAMaxJobCount.Weight = 1;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "حداکثر تعداد کار";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell12.Weight = 0.48219363931217574;
        // 
        // xrTCATotalCapacity
        // 
        this.xrTCATotalCapacity.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTCATotalCapacity.Dpi = 254F;
        this.xrTCATotalCapacity.Name = "xrTCATotalCapacity";
        this.xrTCATotalCapacity.StylePriority.UseBorders = false;
        this.xrTCATotalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTCATotalCapacity.Text = "xrTCATotalCapacity";
        this.xrTCATotalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTCATotalCapacity.Weight = 0.8650137816180351;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell17.Weight = 0.65771330077563916;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel6.Location = new System.Drawing.Point(212, 0);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.Size = new System.Drawing.Size(169, 50);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "ظرفیت کل";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCATotalUsed,
            this.xrTableCell15,
            this.xrTCAMaxFloor,
            this.xrTableCell21});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1;
        // 
        // xrTCATotalUsed
        // 
        this.xrTCATotalUsed.Dpi = 254F;
        this.xrTCATotalUsed.Name = "xrTCATotalUsed";
        this.xrTCATotalUsed.Text = "xrTCATotalUsed";
        this.xrTCATotalUsed.Weight = 1;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "کل کارکرد";
        this.xrTableCell15.Weight = 0.48219363931217574;
        // 
        // xrTCAMaxFloor
        // 
        this.xrTCAMaxFloor.Dpi = 254F;
        this.xrTCAMaxFloor.Name = "xrTCAMaxFloor";
        this.xrTCAMaxFloor.Text = "xrTCAMaxFloor";
        this.xrTCAMaxFloor.Weight = 0.8650137816180351;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.Weight = 0.65771330077563916;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel11.Location = new System.Drawing.Point(106, 10);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.Size = new System.Drawing.Size(275, 50);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "حداکثر تعداد طبقات";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCAConditionalCapacity,
            this.xrTableCell20,
            this.xrTCAProjectCount,
            this.xrTableCell22});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1;
        // 
        // xrTCAConditionalCapacity
        // 
        this.xrTCAConditionalCapacity.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTCAConditionalCapacity.Dpi = 254F;
        this.xrTCAConditionalCapacity.Name = "xrTCAConditionalCapacity";
        this.xrTCAConditionalCapacity.StylePriority.UseBorders = false;
        this.xrTCAConditionalCapacity.StylePriority.UseTextAlignment = false;
        this.xrTCAConditionalCapacity.Text = "xrTCAConditionalCapacity";
        this.xrTCAConditionalCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTCAConditionalCapacity.Weight = 1;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "کاهش/افزایش ظرفیت";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell20.Weight = 0.48219363931217574;
        // 
        // xrTCAProjectCount
        // 
        this.xrTCAProjectCount.Dpi = 254F;
        this.xrTCAProjectCount.Name = "xrTCAProjectCount";
        this.xrTCAProjectCount.StylePriority.UseTextAlignment = false;
        this.xrTCAProjectCount.Text = "xrTCAProjectCount";
        this.xrTCAProjectCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTCAProjectCount.Weight = 0.86501378161803533;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell22.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7});
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseBorders = false;
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell22.Weight = 0.65771330077563916;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel7.Location = new System.Drawing.Point(21, 0);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.Size = new System.Drawing.Size(360, 50);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "تعداد پروژه های در دست اجرا";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell24,
            this.xrTCARemainCapacity,
            this.xrTableCell26});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell19.Weight = 1;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.StylePriority.UseTextAlignment = false;
        this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell24.Weight = 0.48219363931217574;
        // 
        // xrTCARemainCapacity
        // 
        this.xrTCARemainCapacity.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTCARemainCapacity.Dpi = 254F;
        this.xrTCARemainCapacity.Name = "xrTCARemainCapacity";
        this.xrTCARemainCapacity.StylePriority.UseBorders = false;
        this.xrTCARemainCapacity.StylePriority.UseTextAlignment = false;
        this.xrTCARemainCapacity.Text = "xrTCARemainCapacity";
        this.xrTCARemainCapacity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTCARemainCapacity.Weight = 0.86501378161803533;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell26.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8});
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseBorders = false;
        this.xrTableCell26.StylePriority.UseTextAlignment = false;
        this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell26.Weight = 0.65771330077563916;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel8.Location = new System.Drawing.Point(169, 0);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.Size = new System.Drawing.Size(212, 50);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "ظرفیت باقیمانده";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.BorderWidth = 1;
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.Location = new System.Drawing.Point(10, 10);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel1.StylePriority.UseBorders = false;
        this.xrPanel1.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel9.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel9.Location = new System.Drawing.Point(1535, 11);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.Size = new System.Drawing.Size(275, 56);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseForeColor = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "اختصاص ظرفیت";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 495;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Location = new System.Drawing.Point(0, 0);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
        this.xrTable2.Size = new System.Drawing.Size(1849, 440);
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell79});
        this.xrTableRow26.Dpi = 254F;
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.Weight = 1;
        // 
        // xrTableCell79
        // 
        this.xrTableCell79.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell79.BorderWidth = 2;
        this.xrTableCell79.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrTableCell79.Dpi = 254F;
        this.xrTableCell79.Name = "xrTableCell79";
        this.xrTableCell79.StylePriority.UseBorders = false;
        this.xrTableCell79.StylePriority.UseBorderWidth = false;
        this.xrTableCell79.Weight = 3;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 1;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Location = new System.Drawing.Point(10, 10);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow25,
            this.xrTableRow22});
        this.xrTable3.Size = new System.Drawing.Size(1829, 421);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell68,
            this.xrTableCell64,
            this.xrTableCell63});
        this.xrTableRow23.Dpi = 254F;
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.Weight = 0.78947368421052644;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell68.Dpi = 254F;
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.StylePriority.UseBorders = false;
        this.xrTableCell68.Weight = 0.65650628758884633;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell64.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37});
        this.xrTableCell64.Dpi = 254F;
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.Weight = 1.7702296336796066;
        // 
        // xrLabel37
        // 
        this.xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel37.Location = new System.Drawing.Point(246, 159);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.Size = new System.Drawing.Size(545, 53);
        this.xrLabel37.StylePriority.UseBorders = false;
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "ظرفیت اجرا عضو حقیقی";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell63.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
        this.xrTableCell63.Dpi = 254F;
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.StylePriority.UseBorders = false;
        this.xrTableCell63.Weight = 0.57326407873154717;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageUrl = "~\\Images\\Reports\\arm for sara report.jpg";
        this.xrPictureBox2.Location = new System.Drawing.Point(21, 42);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.Size = new System.Drawing.Size(300, 250);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76});
        this.xrTableRow25.Dpi = 254F;
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 0.15789473684210506;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell74.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35});
        this.xrTableCell74.Dpi = 254F;
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.StylePriority.UseBorders = false;
        this.xrTableCell74.StylePriority.UseTextAlignment = false;
        this.xrTableCell74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell74.Weight = 0.65650628758884633;
        // 
        // xrLabel35
        // 
        this.xrLabel35.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel35.Location = new System.Drawing.Point(254, 0);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.Size = new System.Drawing.Size(127, 42);
        this.xrLabel35.StylePriority.UseBorders = false;
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = ":شماره";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell75.Dpi = 254F;
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseBorders = false;
        this.xrTableCell75.Weight = 1.7702296336796066;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell76.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel33});
        this.xrTableCell76.Dpi = 254F;
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.StylePriority.UseBorders = false;
        this.xrTableCell76.StylePriority.UseTextAlignment = false;
        this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell76.Weight = 0.57326407873154717;
        // 
        // xrLabel33
        // 
        this.xrLabel33.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel33.Location = new System.Drawing.Point(190, 0);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.Size = new System.Drawing.Size(156, 42);
        this.xrLabel33.StylePriority.UseBorders = false;
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = ":شماره فرم";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell67,
            this.xrTableCell71,
            this.xrTableCell72});
        this.xrTableRow22.Dpi = 254F;
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.StylePriority.UseBorders = false;
        this.xrTableRow22.Weight = 0.15789473684210531;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell67.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36});
        this.xrTableCell67.Dpi = 254F;
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseBorders = false;
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell67.Weight = 0.65650628758884633;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel36.Location = new System.Drawing.Point(296, 0);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.Size = new System.Drawing.Size(85, 42);
        this.xrLabel36.StylePriority.UseBorders = false;
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = ":تاریخ";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell71.Dpi = 254F;
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.StylePriority.UseBorders = false;
        this.xrTableCell71.Weight = 1.7702296336796066;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel34});
        this.xrTableCell72.Dpi = 254F;
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseBorders = false;
        this.xrTableCell72.Weight = 0.57326407873154717;
        // 
        // xrLabel34
        // 
        this.xrLabel34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel34.Location = new System.Drawing.Point(120, 0);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.Size = new System.Drawing.Size(220, 42);
        this.xrLabel34.StylePriority.UseBorders = false;
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = ":شماره ویرایش";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.Height = 156;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 2;
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Location = new System.Drawing.Point(0, 21);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable5.Size = new System.Drawing.Size(1849, 100);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
        this.xrTableRow27.Dpi = 254F;
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1.0079365079365079;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrTableCell80.Dpi = 254F;
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Weight = 3;
        // 
        // xrTable6
        // 
        this.xrTable6.BorderWidth = 1;
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Location = new System.Drawing.Point(10, 10);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable6.Size = new System.Drawing.Size(1829, 80);
        this.xrTable6.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81});
        this.xrTableRow28.Dpi = 254F;
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 1;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrPageInfo1});
        this.xrTableCell81.Dpi = 254F;
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Weight = 3;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel38.Location = new System.Drawing.Point(286, 11);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel38.Size = new System.Drawing.Size(1524, 64);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "سازمان نظام مهندسی ساختمان استان فارس ( واحد عضویت در سازمان )   تلفن:   8-071162" +
            "74194";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Location = new System.Drawing.Point(32, 11);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.Size = new System.Drawing.Size(64, 48);
        this.xrPageInfo1.StylePriority.UseBorders = false;
        // 
        // XtraReportTSMemImpCapacityInfo
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(155, 34, 150, 203);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}