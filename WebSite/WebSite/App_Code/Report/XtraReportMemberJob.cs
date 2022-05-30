using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportMemberJob
/// </summary>
public class XtraReportMemberJob : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;


    private XRLabel xrLabel34;
    private XRPanel xrPanel1;
    private XRPanel xrPanel2;
    private XRPanel xrPanel3;
    private ReportFooterBand ReportFooter;
    private XRPanel xrPanel4;
    private XRPanel xrPanel5;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTable xrTable3;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTCorName;
    private XRTableCell xrTEmployer;
    private XRTableCell xrTTypeName;
    private XRTableCell xrTProjectName;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTStartCorporateDate;
    private XRTableCell xrTCitName;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTCreateDate;
    private XRTableCell xrTInActiveName;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportMemberJob(int MeId, int MReId, bool IsMeTemp)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        if (!IsMeTemp)
        {
            TSP.DataManager.ProjectJobHistoryManager JobManager = new TSP.DataManager.ProjectJobHistoryManager();
            JobManager.FindByMeRequest(MeId, MReId, -1, 0, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest));
            xrTProjectName.DataBindings.Add("Text", JobManager.DataTable, "ProjectName");
            xrTTypeName.DataBindings.Add("Text", JobManager.DataTable, "TypeName");
            xrTEmployer.DataBindings.Add("Text", JobManager.DataTable, "Employer");
            xrTStartCorporateDate.DataBindings.Add("Text", JobManager.DataTable, "StartCorporateDate");
            xrTCitName.DataBindings.Add("Text", JobManager.DataTable, "CitName");
            xrTCorName.DataBindings.Add("Text", JobManager.DataTable, "CorName");
            xrTCreateDate.DataBindings.Add("Text", JobManager.DataTable, "CreateDate");
            xrTInActiveName.DataBindings.Add("Text", JobManager.DataTable, "InActiveName");
            this.DataSource = JobManager.DataTable;
        }
        else
        {
            TSP.DataManager.TempMemberJobHistoryManager JobManager = new TSP.DataManager.TempMemberJobHistoryManager();
            JobManager.FindByRequest(-1, MReId);
            xrTProjectName.DataBindings.Add("Text", JobManager.DataTable, "ProjectName");
            xrTTypeName.DataBindings.Add("Text", JobManager.DataTable, "TypeName");
            xrTEmployer.DataBindings.Add("Text", JobManager.DataTable, "Employer");
            xrTStartCorporateDate.DataBindings.Add("Text", JobManager.DataTable, "StartCorporateDate");
            xrTCitName.DataBindings.Add("Text", JobManager.DataTable, "CitName");
            xrTCorName.DataBindings.Add("Text", JobManager.DataTable, "CorName");
            xrTCreateDate.DataBindings.Add("Text", JobManager.DataTable, "CreateDate");
            xrTInActiveName.DataBindings.Add("Text", JobManager.DataTable, "InActiveName");
            this.DataSource = JobManager.DataTable;
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
        string resourceFileName = "XtraReportMemberJob.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTInActiveName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCreateDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCorName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTCitName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTStartCorporateDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTEmployer = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTypeName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTProjectName = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
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
        this.Detail.Font = new System.Drawing.Font("Tahoma", 8F);
        this.Detail.Height = 85;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.StylePriority.UseFont = false;
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
            this.xrTableRow5});
        this.xrTable3.Size = new System.Drawing.Size(1829, 85);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTInActiveName,
            this.xrTCreateDate,
            this.xrTCorName,
            this.xrTCitName,
            this.xrTStartCorporateDate,
            this.xrTEmployer,
            this.xrTTypeName,
            this.xrTProjectName});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTInActiveName
        // 
        this.xrTInActiveName.Dpi = 254F;
        this.xrTInActiveName.Name = "xrTInActiveName";
        this.xrTInActiveName.Text = "xrTInActiveName";
        this.xrTInActiveName.Weight = 0.3401331089978572;
        // 
        // xrTCreateDate
        // 
        this.xrTCreateDate.Dpi = 254F;
        this.xrTCreateDate.Name = "xrTCreateDate";
        this.xrTCreateDate.Text = "xrTCreateDate";
        this.xrTCreateDate.Weight = 0.41906244439142448;
        // 
        // xrTCorName
        // 
        this.xrTCorName.Dpi = 254F;
        this.xrTCorName.Name = "xrTCorName";
        this.xrTCorName.Text = "xrTCorName";
        this.xrTCorName.Weight = 0.83565834705180009;
        // 
        // xrTCitName
        // 
        this.xrTCitName.Dpi = 254F;
        this.xrTCitName.Name = "xrTCitName";
        this.xrTCitName.Text = "xrTCitName";
        this.xrTCitName.Weight = 0.41597926722761325;
        // 
        // xrTStartCorporateDate
        // 
        this.xrTStartCorporateDate.Dpi = 254F;
        this.xrTStartCorporateDate.Name = "xrTStartCorporateDate";
        this.xrTStartCorporateDate.Text = "xrTStartCorporateDate";
        this.xrTStartCorporateDate.Weight = 0.41104618376551533;
        // 
        // xrTEmployer
        // 
        this.xrTEmployer.Dpi = 254F;
        this.xrTEmployer.Name = "xrTEmployer";
        this.xrTEmployer.Text = "xrTEmployer";
        this.xrTEmployer.Weight = 0.47677652518417368;
        // 
        // xrTTypeName
        // 
        this.xrTTypeName.Dpi = 254F;
        this.xrTTypeName.Name = "xrTTypeName";
        this.xrTTypeName.Text = "xrTTypeName";
        this.xrTTypeName.Weight = 0.42004591193933943;
        // 
        // xrTProjectName
        // 
        this.xrTProjectName.Dpi = 254F;
        this.xrTProjectName.Name = "xrTProjectName";
        this.xrTProjectName.Text = "xrTProjectName";
        this.xrTProjectName.Weight = 1.1926030375308598;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Font = new System.Drawing.Font("Tahoma", 8F);
        this.PageHeader.Height = 174;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageHeader.StylePriority.UseFont = false;
        this.PageHeader.StylePriority.UseTextAlignment = false;
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            this.xrLabel34});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.Location = new System.Drawing.Point(10, 10);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.Size = new System.Drawing.Size(1829, 70);
        this.xrPanel5.StylePriority.UseBorders = false;
        this.xrPanel5.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel34
        // 
        this.xrLabel34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel34.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.xrLabel34.Location = new System.Drawing.Point(1598, 11);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.Size = new System.Drawing.Size(217, 50);
        this.xrLabel34.StylePriority.UseBorders = false;
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseForeColor = false;
        this.xrLabel34.Text = "سوابق کاری";
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
            this.xrTableRow2});
        this.xrTable2.Size = new System.Drawing.Size(1829, 85);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell19,
            this.xrTableCell6,
            this.xrTableCell23,
            this.xrTableCell13,
            this.xrTableCell7,
            this.xrTableCell14,
            this.xrTableCell15});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Text = "وضعیت";
        this.xrTableCell20.Weight = 0.3401331089978572;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "تاریخ ایجاد";
        this.xrTableCell19.Weight = 0.41906244439142448;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "نحوه مشارکت";
        this.xrTableCell6.Weight = 0.83565834705180009;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "شهر";
        this.xrTableCell23.Weight = 0.41597926722761325;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "تاریخ شروع همکاری";
        this.xrTableCell13.Weight = 0.41104618376551533;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "کارفرما";
        this.xrTableCell7.Weight = 0.47677652518417368;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "نوع پروژه";
        this.xrTableCell14.Weight = 0.42004591193933943;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "نام پروژه";
        this.xrTableCell15.Weight = 1.1926030375308598;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.Height = 40;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageFooter.Visible = false;
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPanel3.BorderWidth = 2;
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.Location = new System.Drawing.Point(0, 0);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.Size = new System.Drawing.Size(1849, 40);
        this.xrPanel3.StylePriority.UseBorders = false;
        this.xrPanel3.StylePriority.UseBorderWidth = false;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.Height = 40;
        this.ReportFooter.KeepTogether = true;
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
        // XtraReportMemberJob
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(152, 155, 155, 203);
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
