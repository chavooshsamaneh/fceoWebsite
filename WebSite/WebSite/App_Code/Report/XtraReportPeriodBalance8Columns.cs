using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReportPeriodBalance8Columns
/// </summary>
public class XtraReportPeriodBalance8Columns : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;

    TSP.DataManager.AccountingDocBalanceDetailManager DocBalanceDetailManager = new TSP.DataManager.AccountingDocBalanceDetailManager();

    private XRTable xrTable3;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell79;
    private XRTable xrTable4;
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
    private XRTable xrTable8;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell5;
    private XRTable xrTable7;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTDateTo;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTDateFrom;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTAgent;
    private XRTableCell xrTableCell2;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell9;
    private XRTable xrTable9;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTAccCode;
    private XRTable xrTable5;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell80;
    private XRTable xrTable6;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell81;
    private XRLabel xrLabel38;
    private XRPageInfo xrPageInfo1;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTaccName;
    private XRTableCell xrTPrevRestBed;
    private XRTableCell xrTPrevRestBes;
    private XRTableCell xrTTotalCurrentBed;
    private XRTableCell xrTTotalCurrentBes;
    private XRTableCell xrTTotalCurrentRestBed;
    private XRTableCell xrTTotalCurrentRestBes;
    private XRTableCell xrTTotalRestBed;
    private XRTableCell xrTTotalRestBes;
    private XRControlStyle xrControlStyle1;


    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportPeriodBalance8Columns(int PId, string DocDateFrom, string DocDateTo, int AgentId, string FilterExp, int AccTypeId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        System.Data.DataTable dt = DocBalanceDetailManager.PeriodBalance8Columns(PId, DocDateFrom, DocDateTo, AgentId, AccTypeId);
        dt.DefaultView.RowFilter = FilterExp;
        this.DataSource = dt;

        xrTAccCode.DataBindings.Add("Text", this.DataSource, "AccCode");
        //xrTGroupName.DataBindings.Add("Text", this.DataSource, "GroupName");
        //xrTKolName.DataBindings.Add("Text", this.DataSource, "KolName");
        //xrTMoinName.DataBindings.Add("Text", this.DataSource, "MoinName");
        xrTaccName.DataBindings.Add("Text", this.DataSource, "AccName");
        xrTPrevRestBed.DataBindings.Add("Text", this.DataSource, "PrevRestBed");
        xrTPrevRestBes.DataBindings.Add("Text", this.DataSource, "PrevRestBes");
        xrTTotalCurrentBes.DataBindings.Add("Text", this.DataSource, "TotalCurrentBes");
        xrTTotalCurrentBed.DataBindings.Add("Text", this.DataSource, "TotalCurrentBed");
        xrTTotalCurrentRestBed.DataBindings.Add("Text", this.DataSource, "TotalCurrentRestBed");
        xrTTotalCurrentRestBes.DataBindings.Add("Text", this.DataSource, "TotalCurrentRestBes");
        xrTTotalRestBed.DataBindings.Add("Text", this.DataSource, "TotalRestBed");
        xrTTotalRestBes.DataBindings.Add("Text", this.DataSource, "TotalRestBes");

        xrTAgent.Text = GetAgentName(AgentId);
        xrTDateFrom.Text = DocDateFrom;
        xrTDateTo.Text = DocDateTo;

        xrTPrevRestBed.DataBindings[0].FormatString = "{0:#,#}";
        xrTPrevRestBes.DataBindings[0].FormatString = "{0:#,#}";
        xrTTotalCurrentBes.DataBindings[0].FormatString = "{0:#,#}";
        xrTTotalCurrentBed.DataBindings[0].FormatString = "{0:#,#}";
        xrTTotalCurrentRestBed.DataBindings[0].FormatString = "{0:#,#}";
        xrTTotalCurrentRestBes.DataBindings[0].FormatString = "{0:#,#}";
        xrTTotalRestBed.DataBindings[0].FormatString = "{0:#,#}";
        xrTTotalRestBes.DataBindings[0].FormatString = "{0:#,#}";

    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

    private string GetAgentName(int AgentId)
    {
        TSP.DataManager.AccountingAgentManager AgentManager = new TSP.DataManager.AccountingAgentManager();
        AgentManager.FindByCode(AgentId);
        if (AgentManager.Count == 1)
            return (AgentManager[0]["Name"].ToString());
        else
            return "";
    }

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
        string resourceFileName = "XtraReportPeriodBalance8Columns.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTTotalRestBes = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalRestBed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCurrentRestBes = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCurrentRestBed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCurrentBes = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalCurrentBed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTPrevRestBes = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTPrevRestBed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTaccName = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTAccCode = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTDateTo = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDateFrom = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTAgent = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
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
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
        this.Detail.Height = 42;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable9
        // 
        this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTable9.Location = new System.Drawing.Point(0, 0);
        this.xrTable9.Name = "xrTable9";
        this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable9.Size = new System.Drawing.Size(725, 42);
        this.xrTable9.StylePriority.UseBorders = false;
        this.xrTable9.StylePriority.UseTextAlignment = false;
        this.xrTable9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTTotalRestBes,
            this.xrTTotalRestBed,
            this.xrTTotalCurrentRestBes,
            this.xrTTotalCurrentRestBed,
            this.xrTTotalCurrentBes,
            this.xrTTotalCurrentBed,
            this.xrTPrevRestBes,
            this.xrTPrevRestBed,
            this.xrTaccName,
            this.xrTAccCode});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTTotalRestBes
        // 
        this.xrTTotalRestBes.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalRestBes.Name = "xrTTotalRestBes";
        this.xrTTotalRestBes.StylePriority.UseBorders = false;
        this.xrTTotalRestBes.Text = "xrTTotalRestBes";
        this.xrTTotalRestBes.Weight = 0.10344726562500002;
        // 
        // xrTTotalRestBed
        // 
        this.xrTTotalRestBed.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalRestBed.Name = "xrTTotalRestBed";
        this.xrTTotalRestBed.StylePriority.UseBorders = false;
        this.xrTTotalRestBed.Text = "xrTTotalRestBed";
        this.xrTTotalRestBed.Weight = 0.10344726562500002;
        // 
        // xrTTotalCurrentRestBes
        // 
        this.xrTTotalCurrentRestBes.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalCurrentRestBes.Name = "xrTTotalCurrentRestBes";
        this.xrTTotalCurrentRestBes.StylePriority.UseBorders = false;
        this.xrTTotalCurrentRestBes.Text = "xrTTotalCurrentRestBes";
        this.xrTTotalCurrentRestBes.Weight = 0.091032462284482779;
        // 
        // xrTTotalCurrentRestBed
        // 
        this.xrTTotalCurrentRestBed.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalCurrentRestBed.Name = "xrTTotalCurrentRestBed";
        this.xrTTotalCurrentRestBed.StylePriority.UseBorders = false;
        this.xrTTotalCurrentRestBed.Text = "xrTTotalCurrentRestBed";
        this.xrTTotalCurrentRestBed.Weight = 0.092409752155172442;
        // 
        // xrTTotalCurrentBes
        // 
        this.xrTTotalCurrentBes.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalCurrentBes.Name = "xrTTotalCurrentBes";
        this.xrTTotalCurrentBes.StylePriority.UseBorders = false;
        this.xrTTotalCurrentBes.StylePriority.UseTextAlignment = false;
        this.xrTTotalCurrentBes.Text = "xrTTotalCurrentBes";
        this.xrTTotalCurrentBes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTTotalCurrentBes.Weight = 0.10344019396551726;
        // 
        // xrTTotalCurrentBed
        // 
        this.xrTTotalCurrentBed.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTTotalCurrentBed.Name = "xrTTotalCurrentBed";
        this.xrTTotalCurrentBed.StylePriority.UseBorders = false;
        this.xrTTotalCurrentBed.StylePriority.UseTextAlignment = false;
        this.xrTTotalCurrentBed.Text = "xrTTotalCurrentBed";
        this.xrTTotalCurrentBed.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTTotalCurrentBed.Weight = 0.092397629310344842;
        // 
        // xrTPrevRestBes
        // 
        this.xrTPrevRestBes.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTPrevRestBes.Name = "xrTPrevRestBes";
        this.xrTPrevRestBes.StylePriority.UseBorders = false;
        this.xrTPrevRestBes.StylePriority.UseTextAlignment = false;
        this.xrTPrevRestBes.Text = "xrTPrevRestBes";
        this.xrTPrevRestBes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTPrevRestBes.Weight = 0.091002155172413779;
        // 
        // xrTPrevRestBed
        // 
        this.xrTPrevRestBed.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTPrevRestBed.Name = "xrTPrevRestBed";
        this.xrTPrevRestBed.StylePriority.UseBorders = false;
        this.xrTPrevRestBed.StylePriority.UseTextAlignment = false;
        this.xrTPrevRestBed.Text = "xrTPrevRestBed";
        this.xrTPrevRestBed.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTPrevRestBed.Weight = 0.0923491379310345;
        // 
        // xrTaccName
        // 
        this.xrTaccName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTaccName.Name = "xrTaccName";
        this.xrTaccName.StylePriority.UseBorders = false;
        this.xrTaccName.StylePriority.UseTextAlignment = false;
        this.xrTaccName.Text = "xrTaccName";
        this.xrTaccName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTaccName.Weight = 0.11573275862068966;
        // 
        // xrTAccCode
        // 
        this.xrTAccCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTAccCode.Name = "xrTAccCode";
        this.xrTAccCode.StylePriority.UseBorders = false;
        this.xrTAccCode.StylePriority.UseTextAlignment = false;
        this.xrTAccCode.Text = "xrTAccCode";
        this.xrTAccCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTAccCode.Weight = 0.11474137931034481;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrTable8,
            this.xrTable3});
        this.PageHeader.Height = 291;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.Location = new System.Drawing.Point(0, 258);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable2.Size = new System.Drawing.Size(725, 33);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell18,
            this.xrTableCell17,
            this.xrTableCell11,
            this.xrTableCell4,
            this.xrTableCell16,
            this.xrTableCell15,
            this.xrTableCell13,
            this.xrTableCell12,
            this.xrTableCell9});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "مانده کل  -  بس";
        this.xrTableCell19.Weight = 0.38017860555204325;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "مانده  کل  -   بد";
        this.xrTableCell18.Weight = 0.3801786055520433;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "مانده جاری-بس";
        this.xrTableCell17.Weight = 0.33363222738407566;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "مانده جاری - بد";
        this.xrTableCell11.Weight = 0.34214065764814316;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "گردش جاری-بس";
        this.xrTableCell4.Weight = 0.38197331103342141;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "گردش جاری-بد";
        this.xrTableCell16.Weight = 0.34133283639342021;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "مانده از قبل-بس";
        this.xrTableCell15.Weight = 0.33722163834683205;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "مانده از قبل-بد";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell13.Weight = 0.34030364890226056;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "نام حساب";
        this.xrTableCell12.Weight = 0.4266646575122629;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "شماره حساب";
        this.xrTableCell9.Weight = 0.41941682592559071;
        // 
        // xrTable8
        // 
        this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable8.BorderWidth = 2;
        this.xrTable8.Location = new System.Drawing.Point(0, 200);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable8.Size = new System.Drawing.Size(725, 33);
        this.xrTable8.StylePriority.UseBorders = false;
        this.xrTable8.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 1;
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable7.BorderWidth = 0;
        this.xrTable7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable7.Location = new System.Drawing.Point(8, 8);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable7.Size = new System.Drawing.Size(708, 17);
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseBorderWidth = false;
        this.xrTable7.StylePriority.UseFont = false;
        this.xrTable7.StylePriority.UseTextAlignment = false;
        this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow2.BorderWidth = 1;
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDateTo,
            this.xrTableCell3,
            this.xrTDateFrom,
            this.xrTableCell1,
            this.xrTAgent,
            this.xrTableCell2});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.StylePriority.UseBorders = false;
        this.xrTableRow2.StylePriority.UseBorderWidth = false;
        this.xrTableRow2.Weight = 1;
        // 
        // xrTDateTo
        // 
        this.xrTDateTo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTDateTo.BorderWidth = 0;
        this.xrTDateTo.Name = "xrTDateTo";
        this.xrTDateTo.StylePriority.UseBorders = false;
        this.xrTDateTo.StylePriority.UseBorderWidth = false;
        this.xrTDateTo.Text = "xrTDateTo";
        this.xrTDateTo.Weight = 0.18891098247983093;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BorderWidth = 0;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorderWidth = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "تا";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell3.Weight = 0.047834218453891436;
        // 
        // xrTDateFrom
        // 
        this.xrTDateFrom.BorderWidth = 0;
        this.xrTDateFrom.Name = "xrTDateFrom";
        this.xrTDateFrom.StylePriority.UseBorderWidth = false;
        this.xrTDateFrom.Text = "xrTDateFrom";
        this.xrTDateFrom.Weight = 0.19083734133451319;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BorderWidth = 0;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorderWidth = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "از  ";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell1.Weight = 0.071233865682371583;
        // 
        // xrTAgent
        // 
        this.xrTAgent.BorderWidth = 0;
        this.xrTAgent.Name = "xrTAgent";
        this.xrTAgent.StylePriority.UseBorderWidth = false;
        this.xrTAgent.Text = "xrTAgent";
        this.xrTAgent.Weight = 1.298922246739642;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.BorderWidth = 0;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorderWidth = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = " : نمایندگی";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell2.Weight = 0.20931279356028523;
        // 
        // xrTable3
        // 
        this.xrTable3.Location = new System.Drawing.Point(0, 8);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
        this.xrTable3.Size = new System.Drawing.Size(725, 173);
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell79});
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
            this.xrTable4});
        this.xrTableCell79.Name = "xrTableCell79";
        this.xrTableCell79.StylePriority.UseBorders = false;
        this.xrTableCell79.StylePriority.UseBorderWidth = false;
        this.xrTableCell79.Weight = 3;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.BorderWidth = 1;
        this.xrTable4.Location = new System.Drawing.Point(8, 8);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow25,
            this.xrTableRow22});
        this.xrTable4.Size = new System.Drawing.Size(708, 158);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseTextAlignment = false;
        this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell68,
            this.xrTableCell64,
            this.xrTableCell63});
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.Weight = 0.78947368421052644;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.StylePriority.UseBorders = false;
        this.xrTableCell68.Weight = 0.65650628758884633;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell64.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37});
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.Weight = 1.6993963003462733;
        // 
        // xrLabel37
        // 
        this.xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel37.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel37.Location = new System.Drawing.Point(92, 58);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel37.Size = new System.Drawing.Size(195, 21);
        this.xrLabel37.StylePriority.UseBorders = false;
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "تراز آزمایشی 8 ستونی";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell63.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.StylePriority.UseBorders = false;
        this.xrTableCell63.Weight = 0.64409741206488047;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.ImageUrl = "~\\Images\\Reports\\arm for sara report.jpg";
        this.xrPictureBox2.Location = new System.Drawing.Point(25, 8);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.Size = new System.Drawing.Size(118, 98);
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76});
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 0.15789473684210506;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell74.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35});
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.StylePriority.UseBorders = false;
        this.xrTableCell74.StylePriority.UseTextAlignment = false;
        this.xrTableCell74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell74.Weight = 0.65650628758884633;
        // 
        // xrLabel35
        // 
        this.xrLabel35.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel35.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel35.Location = new System.Drawing.Point(100, 0);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel35.Size = new System.Drawing.Size(50, 17);
        this.xrLabel35.StylePriority.UseBorders = false;
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = ":شماره";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseBorders = false;
        this.xrTableCell75.Weight = 1.6993963003462733;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell76.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel33});
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.StylePriority.UseBorders = false;
        this.xrTableCell76.StylePriority.UseTextAlignment = false;
        this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell76.Weight = 0.64409741206488047;
        // 
        // xrLabel33
        // 
        this.xrLabel33.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel33.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel33.Location = new System.Drawing.Point(75, 0);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel33.Size = new System.Drawing.Size(64, 17);
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
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.StylePriority.UseBorders = false;
        this.xrTableRow22.Weight = 0.15789473684210531;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell67.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36});
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseBorders = false;
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell67.Weight = 0.65650628758884633;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel36.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel36.Location = new System.Drawing.Point(117, 0);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel36.Size = new System.Drawing.Size(33, 17);
        this.xrLabel36.StylePriority.UseBorders = false;
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = ":تاریخ";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.StylePriority.UseBorders = false;
        this.xrTableCell71.Weight = 1.6993963003462733;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel34});
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseBorders = false;
        this.xrTableCell72.Weight = 0.64409741206488047;
        // 
        // xrLabel34
        // 
        this.xrLabel34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel34.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel34.Location = new System.Drawing.Point(64, 0);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel34.Size = new System.Drawing.Size(75, 17);
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
        this.PageFooter.Height = 68;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 2;
        this.xrTable5.Location = new System.Drawing.Point(0, 8);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable5.Size = new System.Drawing.Size(725, 50);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1.0079365079365079;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Weight = 3;
        // 
        // xrTable6
        // 
        this.xrTable6.BorderWidth = 1;
        this.xrTable6.Location = new System.Drawing.Point(8, 8);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable6.Size = new System.Drawing.Size(708, 33);
        this.xrTable6.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81});
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 1;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrPageInfo1});
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Weight = 3;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel38.Font = new System.Drawing.Font("Tahoma", 8F);
        this.xrLabel38.Location = new System.Drawing.Point(117, 8);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel38.Size = new System.Drawing.Size(567, 17);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "سازمان نظام مهندسی ساختمان استان فارس ( واحد امور مالی )   تلفن:   8-07116274194";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.Location = new System.Drawing.Point(33, 8);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.Size = new System.Drawing.Size(33, 19);
        this.xrPageInfo1.StylePriority.UseBorders = false;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // 
        // XtraReportPeriodBalance8Columns
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Margins = new System.Drawing.Printing.Margins(100, 0, 100, 100);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrLBalanceSum_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (e.Text == "")
            e.Text = "0";
    }

    private void xrLTBalanceSum_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        if (e.Text == "")
            e.Text = "0";
    }
}
