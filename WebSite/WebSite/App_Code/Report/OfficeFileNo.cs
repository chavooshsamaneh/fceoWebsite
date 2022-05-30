using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for OfficeFileNo
/// </summary>
public class OfficeFileNo : DevExpress.XtraReports.UI.XtraReport
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
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell23;
    private XRTable xrTable3;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTDesGrdName;
    private XRTableCell xrTMjName;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTObsCapacity;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTDsgnCapacity;
    private XRTableCell xrTObsGrdName;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OfficeFileNo(int OfId, int OfReId)
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
        //DataTable dt = GetOfficeDsgnAndObsCapacity(OfId);

        //xrTMjName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "MjName");
        //xrTDesGrdName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "DsgnGrdId");
        //xrTObsGrdName.DataBindings.Add("Text", OfficeMemberManager.DataTable, "ObsGrdId");
        //xrTDsgnCapacity.DataBindings.Add("Text", OfficeMemberManager.DataTable, "DsgnCapacity");
        //xrTObsCapacity.DataBindings.Add("Text", OfficeMemberManager.DataTable, "ObsCapacity");

        //this.DataSource = dt;
    }

    //protected DataTable GetOfficeDsgnAndObsCapacity(int OfId)
    //{
    //    TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
    //    DataTable dt = OfficeMemberManager.FindOfficeDsngAndObsGrade(OfId);

    //    double ArchCapacity = 0;
    //    double CivilCapacity = 0;
    //    double ElecCapacity = 0;
    //    double MapCapacity = 0;
    //    double MechCapacity = 0;
    //    double TrCapacity = 0;
    //    double UrbCapacity = 0;


    //    int MjId = 0;
    //    ArrayList arr = new ArrayList();

    //    Capacity Capacity = new Capacity();
    //    arr = Capacity.GetOfficeMembersDsgObsCapacity(OfId, (int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office);
    //    if (arr != null)
    //    {
    //        for (int i = 0; i < arr.Count; i++)
    //        {
    //            MjId = Convert.ToInt32(((ArrayList)arr[i])[5]);
    //            if (MjId == (int)TSP.DataManager.MainMajors.Architecture)
    //            {
    //                ArchCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Civil)
    //            {
    //                CivilCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Electronic)
    //            {
    //                ElecCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Mapping)
    //            {
    //                MapCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Mechanic)
    //            {
    //                MechCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Traffic)
    //            {
    //                TrCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Urbanism)
    //            {
    //                UrbCapacity += Convert.ToInt32(((ArrayList)arr[i])[10]);
    //            }
    //        }
    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Architecture;
    //        dt.DefaultView[0]["DsgnCapacity"] = ArchCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Civil;
    //        dt.DefaultView[0]["DsgnCapacity"] = CivilCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Electronic;
    //        dt.DefaultView[0]["DsgnCapacity"] = ElecCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Mapping;
    //        dt.DefaultView[0]["DsgnCapacity"] = MapCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Mechanic;
    //        dt.DefaultView[0]["DsgnCapacity"] = MechCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Traffic;
    //        dt.DefaultView[0]["DsgnCapacity"] = TrCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Urbanism;
    //        dt.DefaultView[0]["DsgnCapacity"] = UrbCapacity;
    //    }
    //    ArchCapacity = 0;
    //    CivilCapacity = 0;
    //    ElecCapacity = 0;
    //    MapCapacity = 0;
    //    MechCapacity = 0;
    //    TrCapacity = 0;
    //    UrbCapacity = 0;

    //    arr = Capacity.GetOfficeMembersDsgObsCapacity(OfId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office);
    //    if (arr != null)
    //    {
    //        for (int i = 0; i < arr.Count; i++)
    //        {
    //            MjId = Convert.ToInt32(((ArrayList)arr[i])[5]);
    //            if (MjId == (int)TSP.DataManager.MainMajors.Architecture)
    //            {
    //                ArchCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Civil)
    //            {
    //                CivilCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Electronic)
    //            {
    //                ElecCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Mapping)
    //            {
    //                MapCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Mechanic)
    //            {
    //                MechCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Traffic)
    //            {
    //                TrCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
    //            }
    //            if (MjId == (int)TSP.DataManager.MainMajors.Urbanism)
    //            {
    //                UrbCapacity += Convert.ToInt32(((ArrayList)arr[i])[11]);
    //            }
    //        }
    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Architecture;
    //        dt.DefaultView[0]["ObsCapacity"] = ArchCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Civil;
    //        dt.DefaultView[0]["ObsCapacity"] = CivilCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Electronic;
    //        dt.DefaultView[0]["ObsCapacity"] = ElecCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Mapping;
    //        dt.DefaultView[0]["ObsCapacity"] = MapCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Mechanic;
    //        dt.DefaultView[0]["ObsCapacity"] = MechCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Traffic;
    //        dt.DefaultView[0]["ObsCapacity"] = TrCapacity;

    //        dt.DefaultView.RowFilter = "MjId=" + (int)TSP.DataManager.MainMajors.Urbanism;
    //        dt.DefaultView[0]["ObsCapacity"] = UrbCapacity;

    //    }

    //    dt.DefaultView.RowFilter = "";
    //    return dt;
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
        string resourceFileName = "OfficeFileNo.resx";
        DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTObsCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDsgnCapacity = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTObsGrdName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDesGrdName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMjName = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 60;
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
        this.xrPanel2.Size = new System.Drawing.Size(1849, 60);
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
        this.xrTable3.Size = new System.Drawing.Size(1829, 55);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTObsCapacity,
            this.xrTDsgnCapacity,
            this.xrTObsGrdName,
            this.xrTDesGrdName,
            this.xrTMjName});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 0.64705882352941169;
        // 
        // xrTObsCapacity
        // 
        this.xrTObsCapacity.Dpi = 254F;
        this.xrTObsCapacity.Name = "xrTObsCapacity";
        this.xrTObsCapacity.Text = "xrTObsCapacity";
        this.xrTObsCapacity.Weight = 0.62147986178440173;
        // 
        // xrTDsgnCapacity
        // 
        this.xrTDsgnCapacity.Dpi = 254F;
        this.xrTDsgnCapacity.Name = "xrTDsgnCapacity";
        this.xrTDsgnCapacity.Text = "xrTDsgnCapacity";
        this.xrTDsgnCapacity.Weight = 0.57355667194996418;
        // 
        // xrTObsGrdName
        // 
        this.xrTObsGrdName.Dpi = 254F;
        this.xrTObsGrdName.Name = "xrTObsGrdName";
        this.xrTObsGrdName.Text = "xrTObsGrdName";
        this.xrTObsGrdName.Weight = 0.65477502282716959;
        // 
        // xrTDesGrdName
        // 
        this.xrTDesGrdName.Dpi = 254F;
        this.xrTDesGrdName.Name = "xrTDesGrdName";
        this.xrTDesGrdName.Text = "xrTDesGrdName";
        this.xrTDesGrdName.Weight = 0.65337752847904018;
        // 
        // xrTMjName
        // 
        this.xrTMjName.Dpi = 254F;
        this.xrTMjName.Name = "xrTMjName";
        this.xrTMjName.Text = "xrTMjName";
        this.xrTMjName.Weight = 1.5372708988735393;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Height = 148;
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
        this.xrPanel1.Size = new System.Drawing.Size(1849, 148);
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
        this.xrLabel12.Location = new System.Drawing.Point(1535, 11);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(262, 50);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "مشخصات پروانه";
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
            xrTableRow8});
        this.xrTable2.Size = new System.Drawing.Size(1829, 55);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow8
        // 
        xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell1,
            this.xrTableCell19,
            this.xrTableCell23});
        xrTableRow8.Dpi = 254F;
        xrTableRow8.Name = "xrTableRow8";
        xrTableRow8.Weight = 0.64705882352941169;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "ظرفیت نظارت";
        this.xrTableCell4.Weight = 0.69187853055023907;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "ظرفیت طراحی";
        this.xrTableCell3.Weight = 0.640213468426154;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "پایه نظارت";
        this.xrTableCell1.Weight = 0.73097168770416254;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "پایه طراحی";
        this.xrTableCell19.Weight = 0.73126346614205073;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "رشته";
        this.xrTableCell23.Weight = 1.7169776732659769;
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
        // OfficeFileNo
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