using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for XtraReportPDocBalance
/// </summary>
public class XtraReportPDocBalance : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;

    TSP.DataManager.AccountingDocBalanceDetailManager docBalanceDetailManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
    TSP.DataManager.AccountingDocBalanceManager docBalanceManager = new TSP.DataManager.AccountingDocBalanceManager();
    TSP.DataManager.AccountingDocOperationManager docOperationManager = new TSP.DataManager.AccountingDocOperationManager();

    DataTable dt;

    private XRLabel xrLabel8;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable5;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell80;
    private XRTable xrTable4;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell81;
    private XRLabel xrLabel38;
    private XRPageInfo xrPageInfo1;
    private XRTable xrTable7;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell27;
    private XRTable xrTable8;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTDocNumber;
    private XRTableCell xrTableCell11;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTDocDate;
    private XRTableCell xrTableCell28;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTAgent;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell36;
    private XRPageInfo xrPageInfo2;
    private XRTableCell xrTableCell38;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell15;
    private XRLabel xrLabel15;
    private XRLabel xrLblCurDate;
    private XRTableCell xrTDescription;
    private XRTableCell xrTableCell35;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTTotalBes;
    private XRTableCell xrTTotalBed;
    private XRTableCell xrTBalance;
    private XRTableCell xrTAccName;
    private XRTableCell xrTAccCode;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTComment;
    private XRTableCell xrTableCell12;
    private XRTable xrTable9;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell6;
    private XRLabel xrLBedSum;
    private XRLabel xrLBesSum;
    private XRLabel xrLabel1;
    private FormattingRule formattingRule1;
    private FormattingRule formattingRule2;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportPDocBalance(int PeriodId,int DocBalanceId)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        docBalanceManager.FindPeriodById(PeriodId,DocBalanceId);
        docOperationManager.FindPeriodById(PeriodId,Convert.ToInt32(docBalanceManager[0]["DocOperationId"]));
        dt = docBalanceDetailManager.FindPeriodByParentIdForReport(PeriodId,DocBalanceId);
        //docBalanceDetailManager.SelectPeriodDocBalanceDetail(PeriodId,DocBalanceId);

        xrTAgent.Text = GetAgentName(Convert.ToInt32(docOperationManager[0]["AgentId"]));
        xrTDocNumber.Text = docOperationManager[0]["DocNumber"].ToString();
        //xrTDocStatus.Text = docOperationManager[0]["DocStatusName"].ToString();
        if (docOperationManager[0]["DocNumberFinaly"] != null && !string.IsNullOrEmpty(docOperationManager[0]["DocNumberFinaly"].ToString()))
            xrTDocNumber.Text = docOperationManager[0]["DocNumberFinaly"].ToString();
        
        xrTDocDate.Text = docBalanceManager[0]["DocDate"].ToString();
        xrTDescription.Text = docBalanceManager[0]["Description"].ToString();

        BindingControls();     
    }


    private void BindingControls()
    {
        DateTime date = new DateTime();
        date = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(date), pDate.GetMonth(date), pDate.GetDayOfMonth(date));
        this.xrLblCurDate.Text = PerDate;

        xrTAccCode.DataBindings.Add("Text", dt, "AccCode");
        xrTAccName.DataBindings.Add("Text", dt, "AccName");
        xrTTotalBed.DataBindings.Add("Text", dt, "TotalBed");
        xrTTotalBes.DataBindings.Add("Text", dt, "TotalBes");
        xrTComment.DataBindings.Add("Text", dt, "Comment");
        xrTBalance.DataBindings.Add("Text", dt, "Balance");

        xrLBedSum.DataBindings.Add("Text", this.DataSource, "TotalBed");
        xrLBesSum.DataBindings.Add("Text", this.DataSource, "TotalBes");
        xrTTotalBed.DataBindings[0].FormatString = "{0:#,#}";
        xrTTotalBes.DataBindings[0].FormatString = "{0:#,#}";
        xrTBalance.DataBindings[0].FormatString = "{0:#,#}";

        xrLBesSum.Summary.Func = SummaryFunc.RunningSum;
        xrLBesSum.Summary.Running = SummaryRunning.Report;
        xrLBedSum.Summary.Func = SummaryFunc.RunningSum;
        xrLBedSum.Summary.Running = SummaryRunning.Report;

        this.DataSource = dt;
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    /// 

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
        string resourceFileName = "XtraReportPDocBalance.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTTotalBes = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTTotalBed = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTBalance = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTAccName = new DevExpress.XtraReports.UI.XRTableCell();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.formattingRule2 = new DevExpress.XtraReports.UI.FormattingRule();
        this.xrTAccCode = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTComment = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLBedSum = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLBesSum = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDocNumber = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTDocDate = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTAgent = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLblCurDate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTDescription = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Height = 50;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.StylePriority.UseBorderWidth = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.Location = new System.Drawing.Point(0, 0);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow12});
        this.xrTable2.Size = new System.Drawing.Size(725, 50);
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.BorderColor = System.Drawing.Color.Navy;
        this.xrTableRow2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableRow2.BorderWidth = 2;
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTTotalBes,
            this.xrTTotalBed,
            this.xrTBalance,
            this.xrTAccName,
            this.xrTAccCode});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.StylePriority.UseBorderColor = false;
        this.xrTableRow2.StylePriority.UseBorders = false;
        this.xrTableRow2.StylePriority.UseBorderWidth = false;
        this.xrTableRow2.Weight = 0.84000000000000008;
        // 
        // xrTTotalBes
        // 
        this.xrTTotalBes.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTTotalBes.Name = "xrTTotalBes";
        this.xrTTotalBes.StylePriority.UseBorders = false;
        this.xrTTotalBes.StylePriority.UseTextAlignment = false;
        this.xrTTotalBes.Text = "xrTTotalBes";
        this.xrTTotalBes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTTotalBes.Weight = 0.597711931034483;
        // 
        // xrTTotalBed
        // 
        this.xrTTotalBed.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTTotalBed.Name = "xrTTotalBed";
        this.xrTTotalBed.StylePriority.UseBorderColor = false;
        this.xrTTotalBed.StylePriority.UseBorders = false;
        this.xrTTotalBed.StylePriority.UseTextAlignment = false;
        this.xrTTotalBed.Text = "xrTTotalBed";
        this.xrTTotalBed.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTTotalBed.Weight = 0.56082131034482718;
        // 
        // xrTBalance
        // 
        this.xrTBalance.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTBalance.Name = "xrTBalance";
        this.xrTBalance.StylePriority.UseBorders = false;
        this.xrTBalance.StylePriority.UseTextAlignment = false;
        this.xrTBalance.Text = "xrTBalance";
        this.xrTBalance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTBalance.Weight = 0.58933586206896549;
        // 
        // xrTAccName
        // 
        this.xrTAccName.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTAccName.FormattingRules.Add(this.formattingRule1);
        this.xrTAccName.FormattingRules.Add(this.formattingRule2);
        this.xrTAccName.Name = "xrTAccName";
        this.xrTAccName.StylePriority.UseBorders = false;
        this.xrTAccName.StylePriority.UseTextAlignment = false;
        this.xrTAccName.Text = "xrTAccName";
        this.xrTAccName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTAccName.Weight = 1.5738599999999996;
        // 
        // formattingRule1
        // 
        this.formattingRule1.Condition = "[Balance]=0   AND   [TotalBes]-[TotalBed]>0";
        // 
        // 
        // 
        this.formattingRule1.Formatting.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.formattingRule1.Name = "formattingRule1";
        // 
        // formattingRule2
        // 
        this.formattingRule2.Condition = "[Balance]!=0";
        // 
        // 
        // 
        this.formattingRule2.Formatting.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.formattingRule2.Name = "formattingRule2";
        // 
        // xrTAccCode
        // 
        this.xrTAccCode.BorderColor = System.Drawing.Color.Navy;
        this.xrTAccCode.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTAccCode.BorderWidth = 2;
        this.xrTAccCode.Name = "xrTAccCode";
        this.xrTAccCode.StylePriority.UseBorderColor = false;
        this.xrTAccCode.StylePriority.UseBorders = false;
        this.xrTAccCode.StylePriority.UseBorderWidth = false;
        this.xrTAccCode.StylePriority.UseTextAlignment = false;
        this.xrTAccCode.Text = "xrTAccCode";
        this.xrTAccCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTAccCode.Weight = 0.68827089655172391;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell10,
            this.xrTComment,
            this.xrTableCell12});
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 0.84000000000000008;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.BorderWidth = 2;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBorderColor = false;
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UseBorderWidth = false;
        this.xrTableCell7.Weight = 0.597711931034483;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.BorderWidth = 2;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorderColor = false;
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UseBorderWidth = false;
        this.xrTableCell8.Weight = 0.56082131034482718;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.BorderWidth = 2;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBorderColor = false;
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UseBorderWidth = false;
        this.xrTableCell10.Weight = 0.58933586206896549;
        // 
        // xrTComment
        // 
        this.xrTComment.BorderColor = System.Drawing.Color.Navy;
        this.xrTComment.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTComment.BorderWidth = 2;
        this.xrTComment.FormattingRules.Add(this.formattingRule1);
        this.xrTComment.FormattingRules.Add(this.formattingRule2);
        this.xrTComment.Name = "xrTComment";
        this.xrTComment.StylePriority.UseBorderColor = false;
        this.xrTComment.StylePriority.UseBorders = false;
        this.xrTComment.StylePriority.UseBorderWidth = false;
        this.xrTComment.StylePriority.UseTextAlignment = false;
        this.xrTComment.Text = "xrTComment";
        this.xrTComment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTComment.Weight = 1.5738599999999996;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.BorderWidth = 2;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBorderColor = false;
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseBorderWidth = false;
        this.xrTableCell12.Weight = 0.68827089655172391;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9,
            this.xrTable5});
        this.PageFooter.Height = 120;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable9
        // 
        this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable9.BorderWidth = 2;
        this.xrTable9.Location = new System.Drawing.Point(0, 0);
        this.xrTable9.Name = "xrTable9";
        this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
        this.xrTable9.Size = new System.Drawing.Size(725, 33);
        this.xrTable9.StylePriority.UseBorders = false;
        this.xrTable9.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6});
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLBedSum,
            this.xrLBesSum,
            this.xrLabel1});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorderColor = false;
        this.xrTableCell6.Weight = 3.0720338983050848;
        // 
        // xrLBedSum
        // 
        this.xrLBedSum.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLBedSum.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrLBedSum.Location = new System.Drawing.Point(108, 8);
        this.xrLBedSum.Name = "xrLBedSum";
        this.xrLBedSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLBedSum.Size = new System.Drawing.Size(100, 17);
        this.xrLBedSum.StylePriority.UseBorders = false;
        this.xrLBedSum.StylePriority.UseFont = false;
        this.xrLBedSum.StylePriority.UseTextAlignment = false;
        xrSummary1.FormatString = "{0:#,#}";
        xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RunningSum;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLBedSum.Summary = xrSummary1;
        this.xrLBedSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLBesSum
        // 
        this.xrLBesSum.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLBesSum.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrLBesSum.Location = new System.Drawing.Point(8, 8);
        this.xrLBesSum.Name = "xrLBesSum";
        this.xrLBesSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLBesSum.Size = new System.Drawing.Size(100, 17);
        this.xrLBesSum.StylePriority.UseBorders = false;
        this.xrLBesSum.StylePriority.UseFont = false;
        this.xrLBesSum.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:#,#}";
        xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.RunningSum;
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLBesSum.Summary = xrSummary2;
        this.xrLBesSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Location = new System.Drawing.Point(667, 8);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.Size = new System.Drawing.Size(33, 17);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "جمع";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 2;
        this.xrTable5.Location = new System.Drawing.Point(0, 58);
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
            this.xrTable4});
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Weight = 3;
        // 
        // xrTable4
        // 
        this.xrTable4.BorderWidth = 1;
        this.xrTable4.Location = new System.Drawing.Point(8, 8);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable4.Size = new System.Drawing.Size(708, 33);
        this.xrTable4.StylePriority.UseBorderWidth = false;
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
        // xrLabel8
        // 
        this.xrLabel8.Location = new System.Drawing.Point(12, 8);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.Size = new System.Drawing.Size(85, 25);
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7,
            this.xrTable8});
        this.ReportHeader.Height = 225;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.BorderWidth = 1;
        this.xrTable7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable7.Location = new System.Drawing.Point(0, 192);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
        this.xrTable7.Size = new System.Drawing.Size(725, 33);
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseBorderWidth = false;
        this.xrTable7.StylePriority.UseFont = false;
        this.xrTable7.StylePriority.UseTextAlignment = false;
        this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell17,
            this.xrTableCell4,
            this.xrTableCell19,
            this.xrTableCell27});
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell14.BorderWidth = 2;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorderColor = false;
        this.xrTableCell14.StylePriority.UseBorderWidth = false;
        this.xrTableCell14.Text = "بس";
        this.xrTableCell14.Weight = 0.6636056409036859;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell17.BorderWidth = 2;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorderColor = false;
        this.xrTableCell17.StylePriority.UseBorderWidth = false;
        this.xrTableCell17.Text = "بد";
        this.xrTableCell17.Weight = 0.6225467966706304;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell4.BorderWidth = 2;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorderColor = false;
        this.xrTableCell4.StylePriority.UseBorderWidth = false;
        this.xrTableCell4.Text = "مبلغ جزء";
        this.xrTableCell4.Weight = 0.65627308442330556;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell19.BorderWidth = 2;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorderColor = false;
        this.xrTableCell19.StylePriority.UseBorderWidth = false;
        this.xrTableCell19.Text = "نام حساب";
        this.xrTableCell19.Weight = 1.7519792818073718;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell27.BorderWidth = 2;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseBorderColor = false;
        this.xrTableCell27.StylePriority.UseBorderWidth = false;
        this.xrTableCell27.Text = "شماره حساب";
        this.xrTableCell27.Weight = 0.768443472057075;
        // 
        // xrTable8
        // 
        this.xrTable8.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrTable8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable8.Location = new System.Drawing.Point(0, 8);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow13,
            this.xrTableRow15,
            this.xrTableRow16});
        this.xrTable8.Size = new System.Drawing.Size(725, 170);
        this.xrTable8.StylePriority.UseBorderColor = false;
        this.xrTable8.StylePriority.UseFont = false;
        this.xrTable8.StylePriority.UseTextAlignment = false;
        this.xrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.BorderColor = System.Drawing.Color.Navy;
        this.xrTableRow3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow3.BorderWidth = 2;
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell30,
            this.xrTDocNumber,
            this.xrTableCell11});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.StylePriority.UseBorderColor = false;
        this.xrTableRow3.StylePriority.UseBorders = false;
        this.xrTableRow3.StylePriority.UseBorderWidth = false;
        this.xrTableRow3.Weight = 0.79821200510855683;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTableCell30.BorderWidth = 2;
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseBorderColor = false;
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.StylePriority.UseBorderWidth = false;
        this.xrTableCell30.StylePriority.UseTextAlignment = false;
        this.xrTableCell30.Text = "سازمان نظام مهندسی ساختمان استان فارس  - امور مالی";
        this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell30.Weight = 1.4348093793103454;
        // 
        // xrTDocNumber
        // 
        this.xrTDocNumber.BorderColor = System.Drawing.Color.Navy;
        this.xrTDocNumber.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTDocNumber.BorderWidth = 2;
        this.xrTDocNumber.Name = "xrTDocNumber";
        this.xrTDocNumber.StylePriority.UseBorderColor = false;
        this.xrTDocNumber.StylePriority.UseBorders = false;
        this.xrTDocNumber.StylePriority.UseBorderWidth = false;
        this.xrTDocNumber.Text = "xrTDocNumber";
        this.xrTDocNumber.Weight = 0.58429663733650428;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.Text = "شماره سند";
        this.xrTableCell11.Weight = 0.29052156956004749;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell37,
            this.xrTDocDate,
            this.xrTableCell28});
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 0.83812260536398464;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell37.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell37.BorderWidth = 2;
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.StylePriority.UseBorderColor = false;
        this.xrTableCell37.StylePriority.UseBorders = false;
        this.xrTableCell37.StylePriority.UseBorderWidth = false;
        this.xrTableCell37.StylePriority.UseTextAlignment = false;
        this.xrTableCell37.Text = "سند حسابداری";
        this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell37.Weight = 1.0480373650416168;
        // 
        // xrTDocDate
        // 
        this.xrTDocDate.BorderColor = System.Drawing.Color.Navy;
        this.xrTDocDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTDocDate.BorderWidth = 2;
        this.xrTDocDate.Name = "xrTDocDate";
        this.xrTDocDate.StylePriority.UseBorderColor = false;
        this.xrTDocDate.StylePriority.UseBorders = false;
        this.xrTDocDate.StylePriority.UseBorderWidth = false;
        this.xrTDocDate.Text = "xrTDocDate";
        this.xrTDocDate.Weight = 0.42652640190249697;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell28.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell28.BorderWidth = 2;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseBorderColor = false;
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.StylePriority.UseBorderWidth = false;
        this.xrTableCell28.Text = "تاریخ سند";
        this.xrTableCell28.Weight = 0.21575347443519619;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTAgent,
            this.xrTableCell34,
            this.xrTableCell36,
            this.xrTableCell38});
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 0.83812260536398464;
        // 
        // xrTAgent
        // 
        this.xrTAgent.BorderColor = System.Drawing.Color.Navy;
        this.xrTAgent.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTAgent.BorderWidth = 2;
        this.xrTAgent.Name = "xrTAgent";
        this.xrTAgent.StylePriority.UseBorderColor = false;
        this.xrTAgent.StylePriority.UseBorders = false;
        this.xrTAgent.StylePriority.UseBorderWidth = false;
        this.xrTAgent.Text = "xrTAgent";
        this.xrTAgent.Weight = 0.54267045897740773;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell34.BorderWidth = 2;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBorderColor = false;
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.StylePriority.UseBorderWidth = false;
        this.xrTableCell34.StylePriority.UseTextAlignment = false;
        this.xrTableCell34.Text = "  : نمایندگی    ";
        this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell34.Weight = 0.50536690606420909;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell36.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2});
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseBorders = false;
        this.xrTableCell36.Weight = 0.42652640190249697;
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo2.Location = new System.Drawing.Point(142, 17);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.Size = new System.Drawing.Size(33, 19);
        this.xrPageInfo2.StylePriority.UseBorders = false;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell38.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell38.BorderWidth = 2;
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseBorderColor = false;
        this.xrTableCell38.StylePriority.UseBorders = false;
        this.xrTableCell38.StylePriority.UseBorderWidth = false;
        this.xrTableCell38.Text = "صفحه";
        this.xrTableCell38.Weight = 0.21575347443519619;
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTDescription,
            this.xrTableCell35});
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 0.83812260536398464;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.BorderWidth = 2;
        this.xrTableCell15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15,
            this.xrLblCurDate});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorderColor = false;
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.StylePriority.UseBorderWidth = false;
        this.xrTableCell15.Weight = 0.27331894411414981;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel15.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrLabel15.Location = new System.Drawing.Point(83, 17);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.Size = new System.Drawing.Size(33, 17);
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = ":تاریخ";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLblCurDate
        // 
        this.xrLblCurDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLblCurDate.Location = new System.Drawing.Point(8, 17);
        this.xrLblCurDate.Name = "xrLblCurDate";
        this.xrLblCurDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLblCurDate.Size = new System.Drawing.Size(67, 17);
        this.xrLblCurDate.StylePriority.UseBorders = false;
        this.xrLblCurDate.StylePriority.UseTextAlignment = false;
        this.xrLblCurDate.Text = "xrLblCurDate";
        this.xrLblCurDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTDescription
        // 
        this.xrTDescription.BorderColor = System.Drawing.Color.Navy;
        this.xrTDescription.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTDescription.BorderWidth = 2;
        this.xrTDescription.Name = "xrTDescription";
        this.xrTDescription.StylePriority.UseBorderColor = false;
        this.xrTDescription.StylePriority.UseBorders = false;
        this.xrTDescription.StylePriority.UseBorderWidth = false;
        this.xrTDescription.Weight = 1.2012448228299639;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.BorderColor = System.Drawing.Color.Navy;
        this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell35.BorderWidth = 2;
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseBorderColor = false;
        this.xrTableCell35.StylePriority.UseBorders = false;
        this.xrTableCell35.StylePriority.UseBorderWidth = false;
        this.xrTableCell35.Text = "شرح سند";
        this.xrTableCell35.Weight = 0.21575347443519619;
        // 
        // XtraReportPDocBalance
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageFooter,
            this.ReportHeader});
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1,
            this.formattingRule2});
        this.Margins = new System.Drawing.Printing.Margins(100, 0, 100, 100);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
