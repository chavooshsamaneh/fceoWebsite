namespace TSP.WebsiteReports.TechnicalService
{
    partial class ProjectObserverDetailReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.TRowAccPrjId = new DevExpress.XtraReports.UI.XRTableCell();
            this.TRowAccCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.TRowAccTypeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.TRowFishAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.TRowFishDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.TRowFishNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailObserver = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubreportObserverListDetail = new DevExpress.XtraReports.UI.XRSubreport();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubreportAccDetail = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportAccDetail,
            this.xrTable2});
            this.Detail.HeightF = 113.7989F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.White;
            this.xrTable2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Double;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.BorderWidth = 3;
            this.xrTable2.Font = new System.Drawing.Font("B Nazanin", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(366.3333F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(759.9584F, 30F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorderDashStyle = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.TRowAccPrjId,
            this.TRowAccCode,
            this.TRowAccTypeName,
            this.TRowFishAmount,
            this.TRowFishDate,
            this.TRowFishNumber});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // TRowAccPrjId
            // 
            this.TRowAccPrjId.Name = "TRowAccPrjId";
            this.TRowAccPrjId.Weight = 0.634395294851942D;
            // 
            // TRowAccCode
            // 
            this.TRowAccCode.Name = "TRowAccCode";
            this.TRowAccCode.Weight = 0.634395294851942D;
            // 
            // TRowAccTypeName
            // 
            this.TRowAccTypeName.Name = "TRowAccTypeName";
            this.TRowAccTypeName.Weight = 1.2184861172197974D;
            // 
            // TRowFishAmount
            // 
            this.TRowFishAmount.Name = "TRowFishAmount";
            this.TRowFishAmount.Weight = 1.2184861231583903D;
            // 
            // TRowFishDate
            // 
            this.TRowFishDate.Name = "TRowFishDate";
            this.TRowFishDate.Weight = 0.99085639169224837D;
            // 
            // TRowFishNumber
            // 
            this.TRowFishNumber.Name = "TRowFishNumber";
            this.TRowFishNumber.Weight = 1.5969534054439083D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.DetailObserver,
            this.ReportHeader});
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // DetailObserver
            // 
            this.DetailObserver.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportObserverListDetail});
            this.DetailObserver.HeightF = 30F;
            this.DetailObserver.Name = "DetailObserver";
            this.DetailObserver.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.DetailObserver_BeforePrint);
            // 
            // xrSubreportObserverListDetail
            // 
            this.xrSubreportObserverListDetail.LocationFloat = new DevExpress.Utils.PointFloat(46.04176F, 0F);
            this.xrSubreportObserverListDetail.Name = "xrSubreportObserverListDetail";
            this.xrSubreportObserverListDetail.SizeF = new System.Drawing.SizeF(1080.25F, 30F);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
            this.ReportHeader.HeightF = 30F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable6
            // 
            this.xrTable6.BackColor = System.Drawing.Color.White;
            this.xrTable6.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Double;
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.BorderWidth = 3;
            this.xrTable6.Font = new System.Drawing.Font("B Nazanin", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(153.3334F, 0F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable6.SizeF = new System.Drawing.SizeF(972.9583F, 30F);
            this.xrTable6.StylePriority.UseBackColor = false;
            this.xrTable6.StylePriority.UseBorderDashStyle = false;
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseBorderWidth = false;
            this.xrTable6.StylePriority.UseFont = false;
            this.xrTable6.StylePriority.UseTextAlignment = false;
            this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTable6.Visible = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.StylePriority.UseBackColor = false;
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBackColor = false;
            this.xrTableCell11.Text = "شماره حساب";
            this.xrTableCell11.Weight = 1.1395470257297575D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "سهم سازمان";
            this.xrTableCell12.Weight = 0.81966309451891972D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "سهم ناظر";
            this.xrTableCell13.Weight = 0.78716527705084649D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Text = "بیمه";
            this.xrTableCell14.Weight = 0.78716529566394966D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "شماره عضویت";
            this.xrTableCell15.Weight = 0.64011179490314418D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "ناظر";
            this.xrTableCell16.Weight = 1.0316625980870677D;
            // 
            // xrSubreportAccDetail
            // 
            this.xrSubreportAccDetail.LocationFloat = new DevExpress.Utils.PointFloat(50F, 37.5F);
            this.xrSubreportAccDetail.Name = "xrSubreportAccDetail";
            this.xrSubreportAccDetail.SizeF = new System.Drawing.SizeF(1075.033F, 30F);
            // 
            // ProjectObserverDetailReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4Rotated;
            this.Version = "11.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell TRowAccCode;
        private DevExpress.XtraReports.UI.XRTableCell TRowAccTypeName;
        private DevExpress.XtraReports.UI.XRTableCell TRowFishAmount;
        private DevExpress.XtraReports.UI.XRTableCell TRowFishDate;
        private DevExpress.XtraReports.UI.XRTableCell TRowFishNumber;
        private DevExpress.XtraReports.UI.XRTableCell TRowAccPrjId;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand DetailObserver;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRTable xrTable6;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportObserverListDetail;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportAccDetail;
    }
}
