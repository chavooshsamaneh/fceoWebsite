using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportTSOffImpMembers
/// </summary>
public class XtraReportTSOffImpMembers : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    ArrayList MemberArr = new ArrayList();
    Capacity Cpcty = new Capacity();

    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel12;
    private XRTable xrTable2;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTable xrTable3;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTMainMember;
    private XRTableCell xrTGrdType;
    private XRTableCell xrTMjName;
    private XRTableCell xrTMeId;
    private XRPanel xrPanel3;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTValue;
    private XRTableCell xrTMeName;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTGrdName;
    private XRTableCell xrTOfpName;
    private XRCheckBox checkEditMainMember;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportTSOffImpMembers(int OfficeId)
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
        //DataTable MemberDt = FillMembers(OfficeId);

        //xrTMeId.DataBindings.Add("Text", MemberDt, "MeId");
        //xrTMeName.DataBindings.Add("Text", MemberDt, "MeName");
        //xrTMjName.DataBindings.Add("Text", MemberDt, "MjName");
        //xrTGrdName.DataBindings.Add("Text", MemberDt, "GrdName");
        //xrTGrdType.DataBindings.Add("Text", MemberDt, "GrdType");
        //xrTOfpName.DataBindings.Add("Text", MemberDt, "OfpName");
        //xrTValue.DataBindings.Add("Text", MemberDt, "Value");
        //checkEditMainMember.DataBindings.Add("Checked", MemberDt, "MainMember");
        
        //this.DataSource = MemberDt;
    }

    //private DataTable FillMembers(int OfficeId)
    //{
    //    DataTable MemberDt = Cpcty.GetImpOffMembers(OfficeId);

    //    MemberDt.Columns.Add("MjName");
    //    MemberDt.Columns.Add("GrdName");

    //    for (int i = 0; i<MemberDt.Rows.Count; i++)
    //    {
    //        MemberDt.Rows[i]["MjName"] = GetMjName(Convert.ToInt32(MemberDt.Rows[i]["MjId"]));
    //        MemberDt.Rows[i]["GrdName"] = GetGrdName(Convert.ToInt32(MemberDt.Rows[i]["GrdId"]));
    //    }

    //    return MemberDt;
    //}
    
    //private string GetGrdName(int GrdId)
    //{
    //    TSP.DataManager.GradeManager GradeManager = new TSP.DataManager.GradeManager();
    //    GradeManager.FindByCode(GrdId);
    //    if (GradeManager.Count > 0)
    //        return GradeManager[0]["GrdName"].ToString();
    //    else
    //        return "";
    //}

    //private string GetMjName(int MjId)
    //{
    //    TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
    //    MajorManager.FindByCode(MjId);
    //    if (MajorManager.Count > 0)
    //        return MajorManager[0]["MjName"].ToString();
    //    else
    //        return "";
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
        string resourceFileName = "XtraReportTSOffImpMembers.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTMainMember = new DevExpress.XtraReports.UI.XRTableCell();
        this.checkEditMainMember = new DevExpress.XtraReports.UI.XRCheckBox();
        this.xrTValue = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTOfpName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTGrdType = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTGrdName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMjName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTMeId = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.Detail.Dpi = 254F;
        this.Detail.Height = 85;
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
        this.xrPanel3.Location = new System.Drawing.Point(0, 0);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.Size = new System.Drawing.Size(1849, 85);
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
            this.xrTMainMember,
            this.xrTValue,
            this.xrTOfpName,
            this.xrTGrdType,
            this.xrTGrdName,
            this.xrTMjName,
            this.xrTMeName,
            this.xrTMeId});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTMainMember
        // 
        this.xrTMainMember.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.checkEditMainMember});
        this.xrTMainMember.Dpi = 254F;
        this.xrTMainMember.Name = "xrTMainMember";
        this.xrTMainMember.Weight = 0.18376629207465753;
        // 
        // checkEditMainMember
        // 
        this.checkEditMainMember.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.checkEditMainMember.Dpi = 254F;
        this.checkEditMainMember.Location = new System.Drawing.Point(74, 21);
        this.checkEditMainMember.Name = "checkEditMainMember";
        this.checkEditMainMember.Size = new System.Drawing.Size(42, 42);
        this.checkEditMainMember.StylePriority.UseBorders = false;
        // 
        // xrTValue
        // 
        this.xrTValue.Dpi = 254F;
        this.xrTValue.Name = "xrTValue";
        this.xrTValue.Text = "xrTValue";
        this.xrTValue.Weight = 0.12891752300352677;
        // 
        // xrTOfpName
        // 
        this.xrTOfpName.Dpi = 254F;
        this.xrTOfpName.Name = "xrTOfpName";
        this.xrTOfpName.Text = "xrTOfpName";
        this.xrTOfpName.Weight = 0.20809758361659944;
        // 
        // xrTGrdType
        // 
        this.xrTGrdType.Dpi = 254F;
        this.xrTGrdType.Name = "xrTGrdType";
        this.xrTGrdType.Text = "xrTGrdType";
        this.xrTGrdType.Weight = 0.13513730264413529;
        // 
        // xrTGrdName
        // 
        this.xrTGrdName.Dpi = 254F;
        this.xrTGrdName.Name = "xrTGrdName";
        this.xrTGrdName.Text = "xrTGrdName";
        this.xrTGrdName.Weight = 0.12750492114574979;
        // 
        // xrTMjName
        // 
        this.xrTMjName.Dpi = 254F;
        this.xrTMjName.Name = "xrTMjName";
        this.xrTMjName.Text = "xrTMjName";
        this.xrTMjName.Weight = 0.25753563254010792;
        // 
        // xrTMeName
        // 
        this.xrTMeName.Dpi = 254F;
        this.xrTMeName.Name = "xrTMeName";
        this.xrTMeName.Text = "xrTMeName";
        this.xrTMeName.Weight = 0.43047929462691958;
        // 
        // xrTMeId
        // 
        this.xrTMeId.Dpi = 254F;
        this.xrTMeId.Name = "xrTMeId";
        this.xrTMeId.Text = "xrTMeId";
        this.xrTMeId.Weight = 0.17072799751273302;
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
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
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
        this.xrLabel12.Location = new System.Drawing.Point(1578, 11);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.Size = new System.Drawing.Size(241, 56);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "اعضای شرکت";
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
        this.xrTable2.Location = new System.Drawing.Point(10, 95);
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
            this.xrTableCell27,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell1,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.Text = "عضو اصلی";
        this.xrTableCell27.Weight = 0.32456937315223694;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "امتیاز";
        this.xrTableCell2.Weight = 0.2288093021425755;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "سمت";
        this.xrTableCell4.Weight = 0.36988825710910345;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "نوع";
        this.xrTableCell3.Weight = 0.2391702927428512;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "پایه";
        this.xrTableCell1.Weight = 0.22732768859360508;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Text = "رشته";
        this.xrTableCell31.Weight = 0.4572669983886517;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = "نام";
        this.xrTableCell32.Weight = 0.75728927957013292;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.Text = "کد عضویت";
        this.xrTableCell33.Weight = 0.30223952463838544;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.Height = 15;
        this.ReportFooter.Name = "ReportFooter";
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
        // XtraReportTSOffImpMembers
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 152, 157, 201);
        this.PageHeight = 2794;
        this.PageWidth = 2159;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}